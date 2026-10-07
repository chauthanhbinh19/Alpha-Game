using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ExceptionServices;

public class ServiceRateLimitMiddleware<TService> : DispatchProxy
    where TService : class
{
    private const int MaximumCalls = 5;
    private static readonly TimeSpan Window = TimeSpan.FromSeconds(1);
    private static readonly SlidingWindowRateLimiter RateLimiter =
        new SlidingWindowRateLimiter(MaximumCalls, Window);

    private TService _service;

    public static TService Create(TService service)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));

        TService proxy = Create<TService, ServiceRateLimitMiddleware<TService>>();
        ((ServiceRateLimitMiddleware<TService>)(object)proxy)._service = service;
        return proxy;
    }

    protected override object Invoke(MethodInfo targetMethod, object[] args)
    {
        if (targetMethod == null)
            throw new ArgumentNullException(nameof(targetMethod));

        if (targetMethod.DeclaringType == typeof(object))
            return InvokeTarget(targetMethod, args);

        string serviceName = typeof(TService).Name;
        string methodName = targetMethod.Name;
        string accountId = User.CurrentUserId;

        if (!RateLimiter.TryAcquire(
                accountId,
                typeof(TService).FullName + "." + targetMethod,
                out TimeSpan retryAfter))
        {
            throw new ServiceRateLimitExceededException(serviceName, methodName, retryAfter);
        }

        return InvokeTarget(targetMethod, args);
    }

    private object InvokeTarget(MethodInfo targetMethod, object[] args)
    {
        try
        {
            return targetMethod.Invoke(_service, args);
        }
        catch (TargetInvocationException exception) when (exception.InnerException != null)
        {
            ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
            throw;
        }
    }
}

public sealed class ServiceRateLimitExceededException : Exception
{
    public string ServiceName { get; }
    public string MethodName { get; }
    public TimeSpan RetryAfter { get; }

    public ServiceRateLimitExceededException(string serviceName, string methodName, TimeSpan retryAfter)
        : base($"Rate limit exceeded for {serviceName}.{methodName}. Retry after {retryAfter.TotalMilliseconds:0} ms.")
    {
        ServiceName = serviceName;
        MethodName = methodName;
        RetryAfter = retryAfter;
    }
}

internal sealed class SlidingWindowRateLimiter
{
    private const int CleanupInterval = 256;

    private readonly int _maximumCalls;
    private readonly long _windowTicks;
    private readonly Dictionary<string, Queue<long>> _callTimestamps =
        new Dictionary<string, Queue<long>>();
    private readonly object _syncRoot = new object();
    private int _callsSinceCleanup;

    public SlidingWindowRateLimiter(int maximumCalls, TimeSpan window)
    {
        if (maximumCalls <= 0)
            throw new ArgumentOutOfRangeException(nameof(maximumCalls));
        if (window <= TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(window));

        _maximumCalls = maximumCalls;
        _windowTicks = (long)(window.TotalSeconds * Stopwatch.Frequency);
    }

    public bool TryAcquire(string accountId, string methodKey, out TimeSpan retryAfter)
    {
        string normalizedAccountId = string.IsNullOrWhiteSpace(accountId)
            ? string.Empty
            : accountId.Trim();
        string bucketKey = normalizedAccountId.Length + ":" + normalizedAccountId + methodKey;
        long now = Stopwatch.GetTimestamp();
        long cutoff = now - _windowTicks;

        lock (_syncRoot)
        {
            if (!_callTimestamps.TryGetValue(bucketKey, out Queue<long> timestamps))
            {
                timestamps = new Queue<long>();
                _callTimestamps.Add(bucketKey, timestamps);
            }

            while (timestamps.Count > 0 && timestamps.Peek() <= cutoff)
                timestamps.Dequeue();

            if (timestamps.Count >= _maximumCalls)
            {
                long ticksUntilAllowed = timestamps.Peek() + _windowTicks - now;
                retryAfter = TimeSpan.FromSeconds((double)ticksUntilAllowed / Stopwatch.Frequency);
                return false;
            }

            timestamps.Enqueue(now);
            retryAfter = TimeSpan.Zero;

            _callsSinceCleanup++;
            if (_callsSinceCleanup >= CleanupInterval)
            {
                RemoveExpiredBuckets(cutoff);
                _callsSinceCleanup = 0;
            }

            return true;
        }
    }

    private void RemoveExpiredBuckets(long cutoff)
    {
        List<string> expiredKeys = null;

        foreach (KeyValuePair<string, Queue<long>> entry in _callTimestamps)
        {
            Queue<long> timestamps = entry.Value;
            while (timestamps.Count > 0 && timestamps.Peek() <= cutoff)
                timestamps.Dequeue();

            if (timestamps.Count == 0)
            {
                if (expiredKeys == null)
                    expiredKeys = new List<string>();
                expiredKeys.Add(entry.Key);
            }
        }

        if (expiredKeys == null)
            return;

        foreach (string expiredKey in expiredKeys)
            _callTimestamps.Remove(expiredKey);
    }
}
