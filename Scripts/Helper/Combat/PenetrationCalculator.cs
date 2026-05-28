using System;

public static class PenetrationCalculator
{
    public static double ApplyDefenseWithPenetration(CardBase playerCard, CardBase enemyCard, double enemyDefense)
    {
        // Phần trăm xuyên giáp sau khi bị kháng
        double actualPenetration = playerCard.CurrentPenetrationRate - enemyCard.CurrentPenetrationResistanceRate;

        // Tính defense sau khi xuyên
        double effectiveDefense = enemyDefense * (1 - actualPenetration / 100.0);

        // Không để defense âm
        return Math.Max(0, effectiveDefense);
    }
}