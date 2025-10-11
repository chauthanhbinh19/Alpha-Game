using System.Collections;
using UnityEngine;

public class BattlePhase : IBattlePhase
{
    public IEnumerator ExecutePhase(PlayerController attacker, PlayerController defender)
    {
        Debug.Log("=== Battle Phase ===");
        yield return new WaitForSeconds(0.5f);

        attacker.Attack(defender);
        yield return new WaitForSeconds(0.5f);

        Debug.Log("Battle Phase Complete");
    }
}
