using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattlePhase : IBattlePhase
{
    private enum BattleSide
    {
        Attacker,
        Defender
    }

    private class ActionEntry
    {
        public CardBase Card { get; set; }
        public BattleSide Side { get; set; }
        public float Speed { get; set; }
    }

    public IEnumerator ExecutePhase(PlayerController attacker, PlayerController defender)
    {
        Debug.Log("=== Battle Phase ===");

        var actionQueue = BuildActionQueue(attacker, defender);

        foreach (var entry in actionQueue)
        {
            if (entry.Card == null || !entry.Card.IsAlive)
            {
                continue;
            }

            var opponent = entry.Side == BattleSide.Attacker ? defender : attacker;
            if (!HasAliveTargets(opponent))
            {
                continue;
            }

            Debug.Log($"[Battle] {entry.Side} card '{entry.Card.CardName}' acts with speed {entry.Speed}");
            entry.Card.PerformAction(opponent);
            yield return new WaitForSeconds(0.15f);

            if (!HasAliveTargets(attacker) || !HasAliveTargets(defender))
            {
                break;
            }
        }

        yield return null;
    }

    private List<ActionEntry> BuildActionQueue(PlayerController attacker, PlayerController defender)
    {
        var entries = new List<ActionEntry>();

        foreach (var card in attacker.GetCards())
        {
            if (card != null && card.IsAlive)
            {
                entries.Add(new ActionEntry
                {
                    Card = card,
                    Side = BattleSide.Attacker,
                    Speed = GetEffectiveSpeed(card)
                });
            }
        }

        foreach (var card in defender.GetCards())
        {
            if (card != null && card.IsAlive)
            {
                entries.Add(new ActionEntry
                {
                    Card = card,
                    Side = BattleSide.Defender,
                    Speed = GetEffectiveSpeed(card)
                });
            }
        }

        return entries
            .OrderByDescending(entry => entry.Speed)
            .ThenBy(entry => entry.Side)
            .ThenBy(entry => entry.Card.CardName)
            .ToList();
    }

    private float GetEffectiveSpeed(CardBase card)
    {
        var speed = card.CurrentSpeed > 0 ? card.CurrentSpeed : card.Speed;
        return speed > 0 ? (float)speed : 1f;
    }

    private bool HasAliveTargets(PlayerController controller)
    {
        return controller.GetCards().Any(card => card != null && card.IsAlive);
    }
}
