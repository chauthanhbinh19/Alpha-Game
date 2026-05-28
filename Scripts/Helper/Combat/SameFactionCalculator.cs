public static class SameFactionCalculator
{
    public static double ApplyDamageToSameFaction(CardBase playerCard, CardBase enemyCard, double baseDamage)
    {
        double damage = baseDamage;

        if (playerCard.Type.Equals(enemyCard.Type))
        {
            // Tăng sát thương theo tỉ lệ phần trăm
            damage += baseDamage * (playerCard.CurrentDamageToSameFactionRate / 100.0);
        }

        return damage;
    }

    public static double ApplyResistanceToSameFaction(CardBase playerCard, CardBase enemy, double incomingDamage)
    {
        if (playerCard.Type.Equals(enemy.Type))
        {
            // Kháng theo tỉ lệ phần trăm
            if (playerCard.CurrentResistanceToSameFactionRate < 100)
            {
                double reducedDamage = incomingDamage * (1 - playerCard.CurrentResistanceToSameFactionRate / 100.0);
                return reducedDamage;
            }
            else
            {
                double reducedDamage = incomingDamage / (playerCard.CurrentResistanceToSameFactionRate / 100.0);
                return reducedDamage;
            }

        }

        // Nếu cùng phe, không kháng thêm
        return incomingDamage;
    }
}