using System;

public static class AbsorptionCalculator
{
    public static bool IsAbsorptionHit(CardBase playerCard, CardBase enemyCard)
    {
        double chance = playerCard.CurrentDamageAbsorptionRate - enemyCard.CurrentIgnoreDamageAbsorptionRate;

        if (chance <= 0)
            return false;

        if (chance >= 100)
            return true;

        double roll = UnityEngine.Random.Range(0f, 100f);
        return roll < chance;
    }

    public static double ApplyDamageAbsorption(CardBase playerCard, double incomingDamage)
    {
        // Áp dụng công thức bão hòa
        double absorptionFactor = 1.0 / (1.0 + playerCard.CurrentAbsorbedDamageRate / 100.0);

        // Damage bị hấp thụ
        double absorbedDamage = incomingDamage * (1 - absorptionFactor);

        // Không để âm
        return Math.Max(0, incomingDamage - absorbedDamage);
    }
}