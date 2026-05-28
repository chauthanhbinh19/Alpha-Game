public static class ComboCalculator
{
    public static bool IsComboHit(CardBase playerCard, CardBase enemyCard)
    {
        double chance = playerCard.CurrentComboRate - enemyCard.CurrentIgnoreComboRate;

        if (chance <= 0)
            return false;

        if (chance >= 100)
            return true;

        double roll = UnityEngine.Random.Range(0f, 100f);
        return roll < chance;
    }

    public static double ApplyComboDamage(CardBase playerCard, CardBase enemyCard, double baseDamage)
    {
        // Tính multiplier (hệ số nhân sát thương chí mạng)
        double multiplier = 1.0 + (playerCard.CurrentComboDamageRate / 100.0) - (enemyCard.CurrentComboResistanceRate / 100.0);

        // Đảm bảo không giảm dưới sát thương cơ bản
        if (multiplier < 1.0)
            multiplier = 1.0;

        return baseDamage * multiplier;
    }
}