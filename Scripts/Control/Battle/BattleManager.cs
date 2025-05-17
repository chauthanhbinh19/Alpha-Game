using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // public LoadTeams loadTeams;
    private bool isPlayerTurn = true;
    private int playerAttackIndex = 0;
    private int enemyAttackIndex = 0;
    private int turn = 1;

    void Start()
    {
        // Initialize the battle
        StartBattle();
    }

    void StartBattle()
    {
        // Load teams and set up the initial state
        FindAnyObjectByType<LoadTeams>().LoadPlayerTeamCard(1);
        FindAnyObjectByType<LoadTeams>().LoadEnemyTeamCard(1);
        StartCoroutine(BattleLoop());
    }

    IEnumerator BattleLoop()
    {
        while (turn <= 10)
        {
            if (isPlayerTurn)
            {
                // Player's turn to attack
                PlayerAttack();
                isPlayerTurn = false;
            }
            else
            {
                // Enemy's turn to attack
                EnemyAttack();
                isPlayerTurn = true;
            }

            // Wait for a moment before the next turn
            yield return new WaitForSeconds(1f);
            turn++;
        }
    }

    void PlayerAttack()
    {
        var playerCardPos = FindAnyObjectByType<LoadTeams>().playerCardField.GetChild(playerAttackIndex);
        var enemyCardPos = FindAnyObjectByType<LoadTeams>().enemyCardField.GetChild(enemyAttackIndex);

        // Lấy cardInstance là con đầu tiên của CardPositionX
        if (playerCardPos.childCount == 0 || enemyCardPos.childCount == 0)
        {
            Debug.Log("Không có cardInstance trong CardPosition!");
            return;
        }

        CardBase playerCard = playerCardPos.GetComponentInChildren<CardBase>();
        CardBase enemyCard = enemyCardPos.GetComponentInChildren<CardBase>();

        if (playerCard != null && enemyCard != null)
        {
            // Nếu là CardHeroesBattle thì dùng hàm Attack riêng
            var hero = playerCard as CardHeroesBattle;
            // var enemy = enemyCard as CardHeroesBattle;
            if (hero != null)
            {
                hero.Attack(enemyCard);
            }
            else
            {
                // Nếu không thì dùng logic mặc định
                enemyCard.TakeDamage(playerCard.attackPower);
                    
            }

            Debug.Log($"{hero.name} attacks {enemyCard.name} for {playerCard.attackPower} damage!");

            if (!enemyCard.IsAlive())
            {
                // Debug.Log($"{enemyCard.name} has been defeated!");
                enemyAttackIndex++;
            }

            playerAttackIndex = (playerAttackIndex + 1) % FindAnyObjectByType<LoadTeams>().playerCardField.childCount;
        }
    }

    void EnemyAttack()
    {
        var enemyCardObj = FindAnyObjectByType<LoadTeams>().enemyCardField.GetChild(enemyAttackIndex);
        var playerCardObj = FindAnyObjectByType<LoadTeams>().playerCardField.GetChild(playerAttackIndex);

        CardBase enemyCard = enemyCardObj.GetComponent<CardBase>();
        CardBase playerCard = playerCardObj.GetComponent<CardBase>();

        if (enemyCard != null && playerCard != null)
        {
            // Nếu là CardHeroesBattle thì dùng hàm Attack riêng
            var hero = enemyCard as CardHeroesBattle;
            if (hero != null)
            {
                hero.Attack(playerCard);
            }
            else
            {
                // Nếu không thì dùng logic mặc định
                playerCard.TakeDamage(enemyCard.attackPower);
            }

            Debug.Log($"{enemyCard.name} attacks {playerCard.name} for {enemyCard.attackPower} damage!");

            if (!playerCard.IsAlive())
            {
                Debug.Log($"{playerCard.name} has been defeated!");
                playerAttackIndex++;
            }

            enemyAttackIndex = (enemyAttackIndex + 1) % FindAnyObjectByType<LoadTeams>().enemyCardField.childCount;
        }
    }
}
