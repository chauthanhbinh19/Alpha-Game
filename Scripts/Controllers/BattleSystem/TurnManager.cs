using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour
{
    public List<Turn> turns = new List<Turn>();
    public bool isPaused = false;

    private void Awake()
    {
        Turn turn1 = new Turn(1);
        turn1.Phases.Add(new PreparationPhase(5f));
        turn1.Phases.Add(new CombatPhase(7f));
        turn1.Phases.Add(new RewardPhase(2f));

        Turn turn2 = new Turn(2);
        turn2.Phases.Add(new PreparationPhase(5f));
        turn2.Phases.Add(new CombatPhase(7f));

        turns.Add(turn1);
        turns.Add(turn2);
    }

    private void Start()
    {
        // GenerateTurns(5); // tạo 5 round
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        foreach (var turn in turns)
        {
            while (isPaused)
                yield return null;

            yield return turn.Execute();
        }
    }

    void GenerateTurns(int totalRounds)
    {
        for (int i = 1; i <= totalRounds; i++)
        {
            Turn turn = new Turn(i);

            // Phase 1: Preparation
            turn.Phases.Add(new PreparationPhase(5f));

            // Phase đặc biệt mỗi 3 round
            if (i % 3 == 0)
            {
                turn.Phases.Add(new RewardPhase(2f));
            }

            // Phase Combat
            turn.Phases.Add(new CombatPhase(7f));

            turns.Add(turn);
        }
    }
}