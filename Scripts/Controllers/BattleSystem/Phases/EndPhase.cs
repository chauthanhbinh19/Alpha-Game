using System.Collections;
using UnityEngine;

public class EndPhase : IBattlePhase
{
    public IEnumerator ExecutePhase(PlayerController attacker, PlayerController defender)
    {
        Debug.Log("=== End Phase ===");

        foreach (var card in attacker.GetCards())
        {
            if (card != null && card.IsAlive)
            {
                card.RegenerateVitality();
            }
        }

        foreach (var card in defender.GetCards())
        {
            if (card != null && card.IsAlive)
            {
                card.RegenerateVitality();
            }
        }

        yield return new WaitForSeconds(0.5f);
    }
}
