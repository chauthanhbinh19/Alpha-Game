using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour
{
    public List<Turn> turns = new List<Turn>();
    public bool isPaused = false;
    public static TurnManager Instance;
    public Phase CurrentPhase { get; private set; }

    private void Awake()
    {
        Instance = this;

        Turn turn1 = new Turn(1);
        turn1.Phases.Add(new PreparationPhase(5f));
        turn1.Phases.Add(new StartPhase(7f));
        turn1.Phases.Add(new CombatPhase(30f));
        turn1.Phases.Add(new EndPhase(7f));
        turn1.Phases.Add(new RewardPhase(2f));

        Turn turn2 = new Turn(2);
        turn2.Phases.Add(new StartPhase(7f));
        turn2.Phases.Add(new CombatPhase(30f));
        turn2.Phases.Add(new EndPhase(7f));
        turn2.Phases.Add(new RewardPhase(2f));

        Turn turn3 = new Turn(3);
        turn3.Phases.Add(new StartPhase(7f));
        turn3.Phases.Add(new CombatPhase(30f));
        turn3.Phases.Add(new EndPhase(7f));
        turn3.Phases.Add(new RewardPhase(2f));

        Turn turn4 = new Turn(4);
        turn4.Phases.Add(new StartPhase(7f));
        turn4.Phases.Add(new CombatPhase(30f));
        turn4.Phases.Add(new EndPhase(7f));
        turn4.Phases.Add(new RewardPhase(2f));

        Turn turn5 = new Turn(5);
        turn5.Phases.Add(new StartPhase(7f));
        turn5.Phases.Add(new CombatPhase(30f));
        turn5.Phases.Add(new EndPhase(7f));
        turn5.Phases.Add(new BossPhase(30f));
        turn5.Phases.Add(new RewardPhase(2f));

        turns.Add(turn1);
        turns.Add(turn2);
        turns.Add(turn3);
        turns.Add(turn4);
        turns.Add(turn5);
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
            foreach (var phase in turn.Phases)
            {
                CurrentPhase = phase;
                Debug.Log($"Bắt đầu Phase: {phase.GetType().Name}");
                yield return phase.Execute();
            }
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