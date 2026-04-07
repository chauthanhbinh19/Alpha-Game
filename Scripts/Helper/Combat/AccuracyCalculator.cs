public static class AccuracyCalculator
{
    public static bool IsAttackHit(CardBase playerCard, CardBase enemyCard)
    {
        double chance = playerCard.CurrentAccuracyRate - enemyCard.CurrentEvasionRate;

        if (chance <= 0)
            return false;

        if (chance >= 100)
            return true;

        double roll = UnityEngine.Random.Range(0f, 100f);
        return roll < chance;
    }
}