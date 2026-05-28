using System;

public static class LifeStealCalculator
{
    public static double ApplyLifesteal(CardBase playerCard, double damageDealt)
    {
        double effectiveLifestealRate = Math.Min(playerCard.CurrentLifestealRate, 100.0); // Giới hạn 100%
        double healAmount = damageDealt * effectiveLifestealRate;
        return healAmount;
    }
}