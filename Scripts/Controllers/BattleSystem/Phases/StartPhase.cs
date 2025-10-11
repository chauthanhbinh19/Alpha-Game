using System.Collections;
using UnityEngine;

public class StartPhase : IBattlePhase
{
    public IEnumerator ExecutePhase(PlayerController attacker, PlayerController defender)
    {
        Debug.Log("=== Start Phase ===");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Start Phase Complete");
    }
}