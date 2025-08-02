using System;
using UnityEngine;

public static class NumberFormatter
{
    private static readonly string[] suffixes = { "", "K", "M", "B", "T", "Q" };

    public static string FormatNumber(long number, bool shorten = true)
    {
        // if (!shorten)
        //     return number.ToString("N0");

        if (!shorten)
            return number.ToString();

        if (number < 1000)
            return number.ToString();

        int suffixIndex = 0;
        double shortenedNumber = number;

        while (shortenedNumber >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            shortenedNumber /= 1000.0;
            suffixIndex++;
        }

        return $"{shortenedNumber:0.##}{suffixes[suffixIndex]}";
    }

    public static string FormatNumber(double number, bool shorten = true)
    {
        // if (!shorten)
        //     return number.ToString("N2");

        if (!shorten)
            return number.ToString();

        if (number < 1000)
            return number.ToString("0.##");

        int suffixIndex = 0;
        double shortenedNumber = number;

        while (shortenedNumber >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            shortenedNumber /= 1000.0;
            suffixIndex++;
        }

        return $"{shortenedNumber:0.##}{suffixes[suffixIndex]}";
    }

    public static string FormatNumber(float number, bool shorten = true)
    {
        return FormatNumber((double)number, shorten);
    }

    public static string FormatNumber(int number, bool shorten = true)
    {
        return FormatNumber((long)number, shorten);
    }
}
