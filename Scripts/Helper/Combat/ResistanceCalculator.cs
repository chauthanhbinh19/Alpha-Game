using System;

public static class ResistanceCalculator
{
    public static double ApplyResistance(CardBase playerCard, double incomingDamage)
    {
        // Áp dụng công thức bão hòa cho kháng cao
        double resistanceFactor = 1.0 / (1.0 + playerCard.CurrentResistanceRate / 100.0);
        double reducedDamage = incomingDamage * resistanceFactor;
        return Math.Max(0, reducedDamage);
    }
}