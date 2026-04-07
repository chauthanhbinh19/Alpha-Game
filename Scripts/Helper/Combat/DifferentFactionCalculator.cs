public static class DifferentFactionCalculator
{
    public static double ApplyDamageToDifferentFaction(CardBase playerCard, CardBase enemyCard, double baseDamage)
    {
        double damage = baseDamage;

        if (!playerCard.Type.Equals(enemyCard.Type))
        {
            // Tăng sát thương theo tỉ lệ phần trăm
            damage += baseDamage * (playerCard.CurrentDamageToDifferentFactionRate / 100.0);
        }

        return damage;
    }

    public static double ApplyResistanceToDifferentFaction(CardBase playerCard, CardBase enemyCard, double incomingDamage)
    {
        if (!playerCard.Type.Equals(enemyCard.Type))
        {
            // Kháng theo tỉ lệ phần trăm
            if (playerCard.CurrentResistanceToDifferentFactionRate < 100)
            {
                double reducedDamage = incomingDamage * (1 - playerCard.CurrentResistanceToDifferentFactionRate / 100.0);
                return reducedDamage;
            }
            else
            {
                double reducedDamage = incomingDamage / (playerCard.CurrentResistanceToDifferentFactionRate / 100.0);
                return reducedDamage;
            }

        }

        // Nếu cùng phe, không kháng thêm
        return incomingDamage;
    }
}