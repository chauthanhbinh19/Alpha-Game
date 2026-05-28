public static class StunCalculator
{
    public static bool IsStunHit(CardBase playerCard, CardBase enemyCard)
    {
        double chance = playerCard.CurrentStunRate - enemyCard.CurrentIgnoreStunRate;

        if (chance <= 0)
            return false;

        if (chance >= 100)
            return true;

        double roll = UnityEngine.Random.Range(0f, 100f);
        return roll < chance;
    }
}