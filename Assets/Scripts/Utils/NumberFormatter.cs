using System;

public static class NumberFormatter
{
    public static string GetShortNumber(double number)
    {
        string[] suffixes = { "", "k", "M", "B", "T" };
        int suffixIndex = 0;

        while (Math.Abs(number) >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            number /= 1000;
            suffixIndex++;
        }

        string format = number % 1 == 0 ? "0" : "0.0";

        return number.ToString(format) + suffixes[suffixIndex];
    }
}