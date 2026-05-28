using System.Collections.Generic;
using System.Reflection;

public class EmblemSystem
{
    private List<Emblems> allEmblems;

    public EmblemSystem(List<Emblems> emblems)
    {
        allEmblems = emblems;
    }

    //Đếm số lượng emblem trong team
    public Dictionary<string, int> CountEmblems(List<CardBase> team)
    {
        Dictionary<string, int> dict = new();

        foreach (var card in team)
        {
            foreach (var emblem in card.Emblems)
            {
                if (!dict.ContainsKey(emblem))
                    dict[emblem] = 0;

                dict[emblem]++;
            }
        }

        return dict;
    }

    //Lấy modifier đang active
    public List<StatModifier> GetActiveModifiers(Dictionary<string, int> counts)
    {
        List<StatModifier> result = new();

        foreach (var emblem in allEmblems)
        {
            if (!counts.ContainsKey(emblem.Id)) continue;

            int count = counts[emblem.Id];

            foreach (var threshold in emblem.Thresholds)
            {
                if (count >= threshold.RequiredCount)
                {
                    result.AddRange(threshold.Modifiers);
                }
            }
        }

        return result;
    }

    //Apply modifier vào card
    public void ApplyModifiers(CardBase card, List<StatModifier> modifiers)
    {
        foreach (var mod in modifiers)
        {
            PropertyInfo prop = typeof(CardBase).GetProperty(mod.Stat);
            if (prop == null) continue;

            double currentValue = (double)prop.GetValue(card);

            double newValue = mod.IsPercent
                ? currentValue * (1 + mod.Value)
                : currentValue + mod.Value;

            prop.SetValue(card, newValue);
        }
    }
}