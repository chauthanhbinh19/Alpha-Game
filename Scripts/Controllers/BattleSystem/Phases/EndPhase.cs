using System.Collections;

public class EndPhase : Phase
{
    public EndPhase(float duration) : base("End", duration) { }

    protected override IEnumerator OnExecute()
    {
        UnityEngine.Debug.Log("End phase...");
        yield return new UnityEngine.WaitForSeconds(Duration);
    }
}