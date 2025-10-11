using System.Collections;
using UnityEngine;

public class EndPhase : IBattlePhase
{
    public IEnumerator ExecutePhase(PlayerController attacker, PlayerController defender)
    {
        Debug.Log("=== End Phase ===");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("End Phase Complete");
    }
}
