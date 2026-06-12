using System.Collections;
using System;

public abstract class Phase
{
    public string PhaseName;
    public float Duration;
    public bool IsSkipped = false;

    public Action OnPhaseStart;
    public Action OnPhaseEnd;

    public Phase(string name, float duration)
    {
        PhaseName = name;
        Duration = duration;
    }

    public virtual IEnumerator Execute()
    {
        if (IsSkipped)
            yield break;

        OnPhaseStart?.Invoke();
        UnityEngine.Debug.Log($"Start Phase: {PhaseName}");

        yield return OnExecute();

        UnityEngine.Debug.Log($"End Phase: {PhaseName}");
        OnPhaseEnd?.Invoke();
    }

    protected abstract IEnumerator OnExecute();
}