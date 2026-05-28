public static class SkillCalculator
{
    public static double ApplyDamageToSkill(CardBase playerCard, AttackType attackType, double baseDamage)
    {
        double damage = baseDamage;

        if (attackType.Equals(AttackType.Skill))
        {
            // Tăng sát thương theo tỉ lệ phần trăm
            damage += baseDamage * (playerCard.CurrentSkillDamageRate / 100.0);
        }

        return damage;
    }

    public static double ApplyResistanceToSkill(CardBase playerCard, AttackType attackType, double incomingDamage)
    {
        if (attackType.Equals(AttackType.Skill))
        {
            // Kháng theo tỉ lệ phần trăm
            if (playerCard.CurrentSkillResistanceRate < 100)
            {
                double reducedDamage = incomingDamage * (1 - playerCard.CurrentSkillResistanceRate / 100.0);
                return reducedDamage;
            }
            else
            {
                double reducedDamage = incomingDamage / (playerCard.CurrentSkillResistanceRate / 100.0);
                return reducedDamage;
            }

        }

        // Nếu cùng phe, không kháng thêm
        return incomingDamage;
    }
}