using System.Collections;

public class BossPhase : Phase
{
    public BossPhase(float duration) : base("Boss", duration) { }

    protected override IEnumerator OnExecute()
    {
        UnityEngine.Debug.Log("Giving Bosses...");
        yield return new UnityEngine.WaitForSeconds(Duration);
    }
}