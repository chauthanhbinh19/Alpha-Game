using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private int maxTurn = 10;

    private TurnManager turnManager;
    private PlayerController attacker;
    private PlayerController defender;

    private void Start()
    {
        InitializeBattle();
        StartCoroutine(turnManager.RunTurns(attacker, defender));
    }

    private void InitializeBattle()
    {
        attacker = new PlayerController();
        defender = new PlayerController();

        turnManager = new TurnManager(maxTurn);

        // Giả lập thêm vài lá bài vào field
        // attacker.AddCard(new DummyCard("🔥 Attacker Card A"));
        // attacker.AddCard(new DummyCard("⚔️ Attacker Card B"));
        // defender.AddCard(new DummyCard("🛡️ Defender Card X"));
        // defender.AddCard(new DummyCard("🧱 Defender Card Y"));

        Debug.Log("Battle initialized successfully!");
    }
}
