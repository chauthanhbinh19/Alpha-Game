using UnityEngine;
public class CardMilitaryBattle : CardBase
{
    // Specific properties for Admiral cards can be added here

    public void Attack(CardBase target)
    {
        // Implement attack logic specific to Admiral cards
        target.TakeDamage(attackPower);
    }
}