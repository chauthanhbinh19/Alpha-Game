public static class ReflectionCalculator
{
    public static bool IsReflectionHit(CardBase playerCard, CardBase enemyCard)
    {
        double chance = playerCard.CurrentReflectionRate - enemyCard.CurrentIgnoreReflectionRate;

        if (chance <= 0)
            return false;

        if (chance >= 100)
            return true;

        double roll = UnityEngine.Random.Range(0f, 100f);
        return roll < chance;
    }

    public static double ApplyReflectionDamage(CardBase playerCard, CardBase enemyCard, double baseDamage)
    {
        // Tính multiplier (hệ số nhân sát thương chí mạng)
        double multiplier = 1.0 + (playerCard.CurrentReflectionDamageRate / 100.0) - (enemyCard.CurrentReflectionResistanceRate / 100.0);

        // Đảm bảo không giảm dưới sát thương cơ bản
        if (multiplier < 1.0)
            multiplier = 1.0;

        return baseDamage * multiplier;
    }
}