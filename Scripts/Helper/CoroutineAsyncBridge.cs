using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public static class CoroutineAsyncBridge
{
    public static IEnumerator WaitTask<T>(Task<T> task, Action<T> onCompleted)
    {
        while (!task.IsCompleted)
            yield return null;

        if (task.IsFaulted)
        {
            Debug.LogException(task.Exception);
            onCompleted?.Invoke(default);
        }
        else
        {
            onCompleted?.Invoke(task.Result);
        }
    }
}
