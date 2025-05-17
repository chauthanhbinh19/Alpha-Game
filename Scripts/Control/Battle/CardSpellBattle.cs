using UnityEngine;
public class CardSpellBattle : CardBase
{
    // Specific properties for Admiral cards can be added here

    public void Attack(CardBase target)
    {
        // Implement attack logic specific to Admiral cards
        target.TakeDamage(attackPower);
    }
}