public static class NormalCalculator
{
    public static double ApplyDamageToNormal(CardBase playerCard, AttackType attackType, double baseDamage)
    {
        double damage = baseDamage;

        if (attackType.Equals(AttackType.Normal))
        {
            // Tăng sát thương theo tỉ lệ phần trăm
            damage += baseDamage * (playerCard.CurrentNormalDamageRate / 100.0);
        }

        return damage;
    }

    public static double ApplyResistanceToNormal(CardBase playerCard, AttackType attackType, double incomingDamage)
    {
        if (attackType.Equals(AttackType.Normal))
        {
            // Kháng theo tỉ lệ phần trăm
            if (playerCard.CurrentNormalResistanceRate < 100)
            {
                double reducedDamage = incomingDamage * (1 - playerCard.CurrentNormalResistanceRate / 100.0);
                return reducedDamage;
            }
            else
            {
                double reducedDamage = incomingDamage / (playerCard.CurrentNormalResistanceRate / 100.0);
                return reducedDamage;
            }

        }

        // Nếu cùng phe, không kháng thêm
        return incomingDamage;
    }
}