using System.Collections;

public class RewardPhase : Phase
{
    public RewardPhase(float duration) : base("Reward", duration) { }

    protected override IEnumerator OnExecute()
    {
        UnityEngine.Debug.Log("Giving Rewards...");
        yield return new UnityEngine.WaitForSeconds(Duration);
    }
}