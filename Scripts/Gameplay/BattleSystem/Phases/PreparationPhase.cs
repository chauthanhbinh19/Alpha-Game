using System.Collections;

public class PreparationPhase : Phase
{
    public PreparationPhase(float duration) : base("Preparation", duration) { }

    protected override IEnumerator OnExecute()
    {
        float timer = Duration;

        while (timer > 0)
        {
            UnityEngine.Debug.Log($"Preparation: {timer:F1}s");
            timer -= UnityEngine.Time.deltaTime;
            yield return null;
        }
    }
}