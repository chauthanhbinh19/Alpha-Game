using System;

public static class VitalityRegenerationCalculator
{
    public static double ApplyVitalityRegeneration(CardBase playerCard, CardBase enemyCard)
    {
        double multiplier = (playerCard.CurrentVitalityRegenerationRate / 100.0) - (enemyCard.CurrentVitalityRegenerationResistanceRate / 100.0);

        multiplier = Math.Max(0, multiplier); // không cho âm

        return playerCard.CurrentHealth * multiplier;
    }
}