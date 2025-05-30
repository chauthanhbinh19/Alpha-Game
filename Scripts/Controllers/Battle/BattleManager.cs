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
        FindAnyObjectByType<LoadTeams>().LoadPlayerTeamCard("1");
        FindAnyObjectByType<LoadTeams>().LoadEnemyTeamCard("!");
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
            // Ưu tiên gọi Attack nếu có (cho mọi loại kế thừa CardBase)
            bool attacked = false;

            if (playerCard is CardHeroesBattle heroes)
            {
                heroes.Attack(enemyCard);
                attacked = true;
            }
            else if (playerCard is CardCaptainsBattle captains)
            {
                captains.Attack(enemyCard);
                attacked = true;
            }
            else if (playerCard is CardColonelsBattle colonels)
            {
                colonels.Attack(enemyCard);
                attacked = true;
            }
            else if (playerCard is CardGeneralsBattle generals)
            {
                generals.Attack(enemyCard);
                attacked = true;
            }
            else if (playerCard is CardAdmiralsBattle admirals)
            {
                admirals.Attack(enemyCard);
                attacked = true;
            }
            else if (playerCard is CardMonstersBattle monsters)
            {
                monsters.Attack(enemyCard);
                attacked = true;
            }
            else if (playerCard is CardMilitaryBattle military)
            {
                military.Attack(enemyCard);
                attacked = true;
            }
            else if (playerCard is CardSpellBattle spell)
            {
                // admirals.Attack(enemyCard);
                attacked = true;
            }

            if (!attacked)
            {
                // Nếu không phải các loại trên thì dùng logic mặc định
                // enemyCard.TakeDamage(playerCard.attackPower);
            }

            // Debug.Log($"{hero.name} attacks {enemyCard.name} for {playerCard.attackPower} damage!");

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
            // Ưu tiên gọi Attack nếu có (cho mọi loại kế thừa CardBase)
            bool attacked = false;

            if (playerCard is CardHeroesBattle heroes)
            {
                heroes.Attack(enemyCard);
                attacked = true;
            }
            else if (playerCard is CardCaptainsBattle captains)
            {
                captains.Attack(enemyCard);
                attacked = true;
            }
            else if (playerCard is CardColonelsBattle colonels)
            {
                colonels.Attack(enemyCard);
                attacked = true;
            }
            else if (playerCard is CardGeneralsBattle generals)
            {
                generals.Attack(enemyCard);
                attacked = true;
            }
            else if (playerCard is CardAdmiralsBattle admirals)
            {
                admirals.Attack(enemyCard);
                attacked = true;
            }
            else if (playerCard is CardMonstersBattle monsters)
            {
                monsters.Attack(enemyCard);
                attacked = true;
            }
            else if (playerCard is CardMilitaryBattle military)
            {
                military.Attack(enemyCard);
                attacked = true;
            }
            else if (playerCard is CardSpellBattle spell)
            {
                // admirals.Attack(enemyCard);
                attacked = true;
            }

            if (!attacked)
            {
                // Nếu không phải các loại trên thì dùng logic mặc định
                // enemyCard.TakeDamage(playerCard.attackPower);
            }

            // Debug.Log($"{enemyCard.name} attacks {playerCard.name} for {enemyCard.attackPower} damage!");

            if (!playerCard.IsAlive())
            {
                Debug.Log($"{playerCard.name} has been defeated!");
                playerAttackIndex++;
            }

            enemyAttackIndex = (enemyAttackIndex + 1) % FindAnyObjectByType<LoadTeams>().enemyCardField.childCount;
        }
    }
}
