using System.Collections;

public class StartPhase : Phase
{
    public StartPhase(float duration) : base("Start", duration) { }

    protected override IEnumerator OnExecute()
    {
        UnityEngine.Debug.Log("Start phase...");
        yield return new UnityEngine.WaitForSeconds(Duration);
    }
}