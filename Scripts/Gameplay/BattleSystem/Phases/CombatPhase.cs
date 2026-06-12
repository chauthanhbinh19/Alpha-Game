using System.Collections;

public class CombatPhase : Phase
{
    public CombatPhase(float duration) : base("Combat", duration) { }

    protected override IEnumerator OnExecute()
    {
        UnityEngine.Debug.Log("Combat Started!");

        yield return new UnityEngine.WaitForSeconds(Duration);

        UnityEngine.Debug.Log("Combat Ended!");
    }
}