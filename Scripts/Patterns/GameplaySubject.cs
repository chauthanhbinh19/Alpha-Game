using System;
using System.Collections.Generic;
using UnityEngine;

public interface IGameplayObserver<in T>
{
    void OnEvent(T eventData);
}

public sealed class GameplaySubject<T>
{
    private readonly List<IGameplayObserver<T>> observers = new List<IGameplayObserver<T>>();

    public IDisposable Subscribe(IGameplayObserver<T> observer)
    {
        if (observer == null)
        {
            throw new ArgumentNullException(nameof(observer));
        }

        observers.Add(observer);
        return new Subscription(this, observer);
    }

    public void Publish(T eventData)
    {
        IGameplayObserver<T>[] snapshot = observers.ToArray();
        foreach (IGameplayObserver<T> observer in snapshot)
        {
            try
            {
                observer.OnEvent(eventData);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }
    }

    private void Unsubscribe(IGameplayObserver<T> observer)
    {
        observers.Remove(observer);
    }

    private sealed class Subscription : IDisposable
    {
        private GameplaySubject<T> subject;
        private IGameplayObserver<T> observer;

        public Subscription(GameplaySubject<T> subject, IGameplayObserver<T> observer)
        {
            this.subject = subject;
            this.observer = observer;
        }

        public void Dispose()
        {
            if (subject == null)
            {
                return;
            }

            subject.Unsubscribe(observer);
            subject = null;
            observer = null;
        }
    }
}
