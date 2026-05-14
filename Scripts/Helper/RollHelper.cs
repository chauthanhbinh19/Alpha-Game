using System;
using System.Collections.Generic;
using System.Linq;

public static class RollHelper
{
    private static readonly Random random = new();

    public static string RollByRate(Dictionary<string, double> rates)
    {
        double totalRate = rates.Values.Sum();
        double roll = random.NextDouble() * totalRate;

        double current = 0;

        foreach (var rate in rates)
        {
            current += rate.Value;

            if (roll <= current)
                return rate.Key;
        }

        return rates.Keys.First();
    }
}