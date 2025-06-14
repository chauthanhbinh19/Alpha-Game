using System.Collections.Generic;
public static class QualityEvaluator
{
    public static int CheckQuality(string rare)
    {
        switch (rare)
        {
            case "SR":
                return 2;
            case "SSR":
                return 5;
            case "UR":
                return 10;
            case "LG":
                return 15;
            case "LG+":
                return 20;
            case "MR":
                return 25;
            case "SLR":
                return 30;
            case "SLR+":
                return 35;
            case "SP":
                return 40;
            default:
                return 0;
        }
    }
    private static readonly Dictionary<string, int> qualityMap = new Dictionary<string, int>
    {
        { "SR", 2 },
        { "SSR", 5 },
        { "UR", 10 },
        { "LG", 15 },
        { "LG+", 20 },
        { "MR", 25 },
        { "SLR", 30 },
        { "SLR+", 35 },
        { "SP", 40 },
    };

    public static int GetQualityValue(string rare)
    {
        return qualityMap.TryGetValue(rare, out int value) ? value : 0;
    }

    public static string GetHigherQuality(string currentRare, string newRare)
    {
        int current = GetQualityValue(currentRare);
        int next = GetQualityValue(newRare);
        return next > current ? newRare : currentRare;
    }
    private static readonly List<string> qualityOrder = new List<string>
    {
        "SR", "SSR", "UR", "LG", "LG+", "MR", "SLR", "SLR+", "SP"
    };

    public static string GetNextQuality(string currentRare)
    {
        int index = qualityOrder.IndexOf(currentRare);
        if (index >= 0 && index < qualityOrder.Count - 1)
        {
            return qualityOrder[index + 1];
        }
        return currentRare; // không tăng được nữa
    }
    public static List<Achievements> GetQualityPower(List<Achievements> list)
    {
        foreach (var c in list)
        {
            c.health = c.health * (1 + c.quality / 10.0);
            c.physical_attack = c.physical_attack * (1 + c.quality / 10.0);
            c.physical_defense = c.physical_defense * (1 + c.quality / 10.0);
            c.magical_attack = c.magical_attack * (1 + c.quality / 10.0);
            c.magical_defense = c.magical_defense * (1 + c.quality / 10.0);
            c.chemical_attack = c.chemical_attack * (1 + c.quality / 10.0);
            c.chemical_defense = c.chemical_defense * (1 + c.quality / 10.0);
            c.atomic_attack = c.atomic_attack * (1 + c.quality / 10.0);
            c.atomic_defense = c.atomic_defense * (1 + c.quality / 10.0);
            c.mental_attack = c.mental_attack * (1 + c.quality / 10.0);
            c.mental_defense = c.mental_defense * (1 + c.quality / 10.0);
            c.speed = c.speed * (1 + c.quality / 10.0);
            c.critical_damage_rate = c.critical_damage_rate * (1 + c.quality / 10.0);
            c.critical_rate = c.critical_rate * (1 + c.quality / 10.0);
            c.penetration_rate = c.penetration_rate * (1 + c.quality / 10.0);
            c.evasion_rate = c.evasion_rate * (1 + c.quality / 10.0);
            c.damage_absorption_rate = c.damage_absorption_rate * (1 + c.quality / 10.0);
            c.vitality_regeneration_rate = c.vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.accuracy_rate = c.accuracy_rate * (1 + c.quality / 10.0);
            c.lifesteal_rate = c.lifesteal_rate * (1 + c.quality / 10.0);
            c.shield_strength = c.shield_strength * (1 + c.quality / 10.0);
            c.tenacity = c.tenacity * (1 + c.quality / 10.0);
            c.resistance_rate = c.resistance_rate * (1 + c.quality / 10.0);
            c.combo_rate = c.combo_rate * (1 + c.quality / 10.0);
            c.reflection_rate = c.reflection_rate * (1 + c.quality / 10.0);
            c.mana = (float)(c.mana * (1 + c.quality / 10.0));
            c.mana_regeneration_rate = c.mana_regeneration_rate * (1 + c.quality / 10.0);
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<Alchemy> GetQualityPower(List<Alchemy> list)
    {
        foreach (var c in list)
        {
            c.health = c.health * (1 + c.quality / 10.0);
            c.physical_attack = c.physical_attack * (1 + c.quality / 10.0);
            c.physical_defense = c.physical_defense * (1 + c.quality / 10.0);
            c.magical_attack = c.magical_attack * (1 + c.quality / 10.0);
            c.magical_defense = c.magical_defense * (1 + c.quality / 10.0);
            c.chemical_attack = c.chemical_attack * (1 + c.quality / 10.0);
            c.chemical_defense = c.chemical_defense * (1 + c.quality / 10.0);
            c.atomic_attack = c.atomic_attack * (1 + c.quality / 10.0);
            c.atomic_defense = c.atomic_defense * (1 + c.quality / 10.0);
            c.mental_attack = c.mental_attack * (1 + c.quality / 10.0);
            c.mental_defense = c.mental_defense * (1 + c.quality / 10.0);
            c.speed = c.speed * (1 + c.quality / 10.0);
            c.critical_damage_rate = c.critical_damage_rate * (1 + c.quality / 10.0);
            c.critical_rate = c.critical_rate * (1 + c.quality / 10.0);
            c.penetration_rate = c.penetration_rate * (1 + c.quality / 10.0);
            c.evasion_rate = c.evasion_rate * (1 + c.quality / 10.0);
            c.damage_absorption_rate = c.damage_absorption_rate * (1 + c.quality / 10.0);
            c.vitality_regeneration_rate = c.vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.accuracy_rate = c.accuracy_rate * (1 + c.quality / 10.0);
            c.lifesteal_rate = c.lifesteal_rate * (1 + c.quality / 10.0);
            c.shield_strength = c.shield_strength * (1 + c.quality / 10.0);
            c.tenacity = c.tenacity * (1 + c.quality / 10.0);
            c.resistance_rate = c.resistance_rate * (1 + c.quality / 10.0);
            c.combo_rate = c.combo_rate * (1 + c.quality / 10.0);
            c.reflection_rate = c.reflection_rate * (1 + c.quality / 10.0);
            c.mana = (float)(c.mana * (1 + c.quality / 10.0));
            c.mana_regeneration_rate = c.mana_regeneration_rate * (1 + c.quality / 10.0);
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<Avatars> GetQualityPower(List<Avatars> list)
    {
        foreach (var c in list)
        {
            c.health = c.health * (1 + c.quality / 10.0);
            c.physical_attack = c.physical_attack * (1 + c.quality / 10.0);
            c.physical_defense = c.physical_defense * (1 + c.quality / 10.0);
            c.magical_attack = c.magical_attack * (1 + c.quality / 10.0);
            c.magical_defense = c.magical_defense * (1 + c.quality / 10.0);
            c.chemical_attack = c.chemical_attack * (1 + c.quality / 10.0);
            c.chemical_defense = c.chemical_defense * (1 + c.quality / 10.0);
            c.atomic_attack = c.atomic_attack * (1 + c.quality / 10.0);
            c.atomic_defense = c.atomic_defense * (1 + c.quality / 10.0);
            c.mental_attack = c.mental_attack * (1 + c.quality / 10.0);
            c.mental_defense = c.mental_defense * (1 + c.quality / 10.0);
            c.speed = c.speed * (1 + c.quality / 10.0);
            c.critical_damage_rate = c.critical_damage_rate * (1 + c.quality / 10.0);
            c.critical_rate = c.critical_rate * (1 + c.quality / 10.0);
            c.penetration_rate = c.penetration_rate * (1 + c.quality / 10.0);
            c.evasion_rate = c.evasion_rate * (1 + c.quality / 10.0);
            c.damage_absorption_rate = c.damage_absorption_rate * (1 + c.quality / 10.0);
            c.vitality_regeneration_rate = c.vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.accuracy_rate = c.accuracy_rate * (1 + c.quality / 10.0);
            c.lifesteal_rate = c.lifesteal_rate * (1 + c.quality / 10.0);
            c.shield_strength = c.shield_strength * (1 + c.quality / 10.0);
            c.tenacity = c.tenacity * (1 + c.quality / 10.0);
            c.resistance_rate = c.resistance_rate * (1 + c.quality / 10.0);
            c.combo_rate = c.combo_rate * (1 + c.quality / 10.0);
            c.reflection_rate = c.reflection_rate * (1 + c.quality / 10.0);
            c.mana = (float)(c.mana * (1 + c.quality / 10.0));
            c.mana_regeneration_rate = c.mana_regeneration_rate * (1 + c.quality / 10.0);
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<Borders> GetQualityPower(List<Borders> list)
    {
        foreach (var c in list)
        {
            c.health = c.health * (1 + c.quality / 10.0);
            c.physical_attack = c.physical_attack * (1 + c.quality / 10.0);
            c.physical_defense = c.physical_defense * (1 + c.quality / 10.0);
            c.magical_attack = c.magical_attack * (1 + c.quality / 10.0);
            c.magical_defense = c.magical_defense * (1 + c.quality / 10.0);
            c.chemical_attack = c.chemical_attack * (1 + c.quality / 10.0);
            c.chemical_defense = c.chemical_defense * (1 + c.quality / 10.0);
            c.atomic_attack = c.atomic_attack * (1 + c.quality / 10.0);
            c.atomic_defense = c.atomic_defense * (1 + c.quality / 10.0);
            c.mental_attack = c.mental_attack * (1 + c.quality / 10.0);
            c.mental_defense = c.mental_defense * (1 + c.quality / 10.0);
            c.speed = c.speed * (1 + c.quality / 10.0);
            c.critical_damage_rate = c.critical_damage_rate * (1 + c.quality / 10.0);
            c.critical_rate = c.critical_rate * (1 + c.quality / 10.0);
            c.penetration_rate = c.penetration_rate * (1 + c.quality / 10.0);
            c.evasion_rate = c.evasion_rate * (1 + c.quality / 10.0);
            c.damage_absorption_rate = c.damage_absorption_rate * (1 + c.quality / 10.0);
            c.vitality_regeneration_rate = c.vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.accuracy_rate = c.accuracy_rate * (1 + c.quality / 10.0);
            c.lifesteal_rate = c.lifesteal_rate * (1 + c.quality / 10.0);
            c.shield_strength = c.shield_strength * (1 + c.quality / 10.0);
            c.tenacity = c.tenacity * (1 + c.quality / 10.0);
            c.resistance_rate = c.resistance_rate * (1 + c.quality / 10.0);
            c.combo_rate = c.combo_rate * (1 + c.quality / 10.0);
            c.reflection_rate = c.reflection_rate * (1 + c.quality / 10.0);
            c.mana = (float)(c.mana * (1 + c.quality / 10.0));
            c.mana_regeneration_rate = c.mana_regeneration_rate * (1 + c.quality / 10.0);
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<Books> GetQualityPower(List<Books> list)
    {
        foreach (var c in list)
        {
            c.all_health = c.all_health * (1 + c.quality / 10.0);
            c.all_physical_attack = c.all_physical_attack * (1 + c.quality / 10.0);
            c.all_physical_defense = c.all_physical_defense * (1 + c.quality / 10.0);
            c.all_magical_attack = c.all_magical_attack * (1 + c.quality / 10.0);
            c.all_magical_defense = c.all_magical_defense * (1 + c.quality / 10.0);
            c.all_chemical_attack = c.all_chemical_attack * (1 + c.quality / 10.0);
            c.all_chemical_defense = c.all_chemical_defense * (1 + c.quality / 10.0);
            c.all_atomic_attack = c.all_atomic_attack * (1 + c.quality / 10.0);
            c.all_atomic_defense = c.all_atomic_defense * (1 + c.quality / 10.0);
            c.all_mental_attack = c.all_mental_attack * (1 + c.quality / 10.0);
            c.all_mental_defense = c.all_mental_defense * (1 + c.quality / 10.0);
            c.all_speed = c.all_speed * (1 + c.quality / 10.0);
            c.all_critical_damage_rate = c.all_critical_damage_rate * (1 + c.quality / 10.0);
            c.all_critical_rate = c.all_critical_rate * (1 + c.quality / 10.0);
            c.all_penetration_rate = c.all_penetration_rate * (1 + c.quality / 10.0);
            c.all_evasion_rate = c.all_evasion_rate * (1 + c.quality / 10.0);
            c.all_damage_absorption_rate = c.all_damage_absorption_rate * (1 + c.quality / 10.0);
            c.all_vitality_regeneration_rate = c.all_vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.all_accuracy_rate = c.all_accuracy_rate * (1 + c.quality / 10.0);
            c.all_lifesteal_rate = c.all_lifesteal_rate * (1 + c.quality / 10.0);
            c.all_shield_strength = c.all_shield_strength * (1 + c.quality / 10.0);
            c.all_tenacity = c.all_tenacity * (1 + c.quality / 10.0);
            c.all_resistance_rate = c.all_resistance_rate * (1 + c.quality / 10.0);
            c.all_combo_rate = c.all_combo_rate * (1 + c.quality / 10.0);
            c.all_reflection_rate = c.all_reflection_rate * (1 + c.quality / 10.0);
            c.all_mana = (float)(c.all_mana * (1 + c.quality / 10.0));
            c.all_mana_regeneration_rate = c.all_mana_regeneration_rate * (1 + c.quality / 10.0);
            c.all_damage_to_different_faction_rate = c.all_damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_different_faction_rate = c.all_resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_damage_to_same_faction_rate = c.all_damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_same_faction_rate = c.all_resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.all_power = EvaluatePower.CalculatePower(
            c.all_health,
            c.all_physical_attack, c.all_physical_defense,
            c.all_magical_attack, c.all_magical_defense,
            c.all_chemical_attack, c.all_chemical_defense,
            c.all_atomic_attack, c.all_atomic_defense,
            c.all_mental_attack, c.all_mental_defense,
            c.all_speed,
            c.all_critical_damage_rate, c.all_critical_rate,
            c.all_penetration_rate, c.all_evasion_rate,
            c.all_damage_absorption_rate, c.all_vitality_regeneration_rate,
            c.all_accuracy_rate, c.all_lifesteal_rate,
            c.all_shield_strength, c.all_tenacity, c.all_resistance_rate,
            c.all_combo_rate, c.all_reflection_rate,
            c.all_mana, c.all_mana_regeneration_rate,
            c.all_damage_to_different_faction_rate, c.all_resistance_to_different_faction_rate,
            c.all_damage_to_same_faction_rate, c.all_resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<CardAdmirals> GetQualityPower(List<CardAdmirals> list)
    {
        foreach (var c in list)
        {
            c.all_health = c.all_health * (1 + c.quality / 10.0);
            c.all_physical_attack = c.all_physical_attack * (1 + c.quality / 10.0);
            c.all_physical_defense = c.all_physical_defense * (1 + c.quality / 10.0);
            c.all_magical_attack = c.all_magical_attack * (1 + c.quality / 10.0);
            c.all_magical_defense = c.all_magical_defense * (1 + c.quality / 10.0);
            c.all_chemical_attack = c.all_chemical_attack * (1 + c.quality / 10.0);
            c.all_chemical_defense = c.all_chemical_defense * (1 + c.quality / 10.0);
            c.all_atomic_attack = c.all_atomic_attack * (1 + c.quality / 10.0);
            c.all_atomic_defense = c.all_atomic_defense * (1 + c.quality / 10.0);
            c.all_mental_attack = c.all_mental_attack * (1 + c.quality / 10.0);
            c.all_mental_defense = c.all_mental_defense * (1 + c.quality / 10.0);
            c.all_speed = c.all_speed * (1 + c.quality / 10.0);
            c.all_critical_damage_rate = c.all_critical_damage_rate * (1 + c.quality / 10.0);
            c.all_critical_rate = c.all_critical_rate * (1 + c.quality / 10.0);
            c.all_penetration_rate = c.all_penetration_rate * (1 + c.quality / 10.0);
            c.all_evasion_rate = c.all_evasion_rate * (1 + c.quality / 10.0);
            c.all_damage_absorption_rate = c.all_damage_absorption_rate * (1 + c.quality / 10.0);
            c.all_vitality_regeneration_rate = c.all_vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.all_accuracy_rate = c.all_accuracy_rate * (1 + c.quality / 10.0);
            c.all_lifesteal_rate = c.all_lifesteal_rate * (1 + c.quality / 10.0);
            c.all_shield_strength = c.all_shield_strength * (1 + c.quality / 10.0);
            c.all_tenacity = c.all_tenacity * (1 + c.quality / 10.0);
            c.all_resistance_rate = c.all_resistance_rate * (1 + c.quality / 10.0);
            c.all_combo_rate = c.all_combo_rate * (1 + c.quality / 10.0);
            c.all_reflection_rate = c.all_reflection_rate * (1 + c.quality / 10.0);
            c.all_mana = (float)(c.all_mana * (1 + c.quality / 10.0));
            c.all_mana_regeneration_rate = c.all_mana_regeneration_rate * (1 + c.quality / 10.0);
            c.all_damage_to_different_faction_rate = c.all_damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_different_faction_rate = c.all_resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_damage_to_same_faction_rate = c.all_damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_same_faction_rate = c.all_resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.all_power = EvaluatePower.CalculatePower(
            c.all_health,
            c.all_physical_attack, c.all_physical_defense,
            c.all_magical_attack, c.all_magical_defense,
            c.all_chemical_attack, c.all_chemical_defense,
            c.all_atomic_attack, c.all_atomic_defense,
            c.all_mental_attack, c.all_mental_defense,
            c.all_speed,
            c.all_critical_damage_rate, c.all_critical_rate,
            c.all_penetration_rate, c.all_evasion_rate,
            c.all_damage_absorption_rate, c.all_vitality_regeneration_rate,
            c.all_accuracy_rate, c.all_lifesteal_rate,
            c.all_shield_strength, c.all_tenacity, c.all_resistance_rate,
            c.all_combo_rate, c.all_reflection_rate,
            c.all_mana, c.all_mana_regeneration_rate,
            c.all_damage_to_different_faction_rate, c.all_resistance_to_different_faction_rate,
            c.all_damage_to_same_faction_rate, c.all_resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<CardCaptains> GetQualityPower(List<CardCaptains> list)
    {
        foreach (var c in list)
        {
            c.all_health = c.all_health * (1 + c.quality / 10.0);
            c.all_physical_attack = c.all_physical_attack * (1 + c.quality / 10.0);
            c.all_physical_defense = c.all_physical_defense * (1 + c.quality / 10.0);
            c.all_magical_attack = c.all_magical_attack * (1 + c.quality / 10.0);
            c.all_magical_defense = c.all_magical_defense * (1 + c.quality / 10.0);
            c.all_chemical_attack = c.all_chemical_attack * (1 + c.quality / 10.0);
            c.all_chemical_defense = c.all_chemical_defense * (1 + c.quality / 10.0);
            c.all_atomic_attack = c.all_atomic_attack * (1 + c.quality / 10.0);
            c.all_atomic_defense = c.all_atomic_defense * (1 + c.quality / 10.0);
            c.all_mental_attack = c.all_mental_attack * (1 + c.quality / 10.0);
            c.all_mental_defense = c.all_mental_defense * (1 + c.quality / 10.0);
            c.all_speed = c.all_speed * (1 + c.quality / 10.0);
            c.all_critical_damage_rate = c.all_critical_damage_rate * (1 + c.quality / 10.0);
            c.all_critical_rate = c.all_critical_rate * (1 + c.quality / 10.0);
            c.all_penetration_rate = c.all_penetration_rate * (1 + c.quality / 10.0);
            c.all_evasion_rate = c.all_evasion_rate * (1 + c.quality / 10.0);
            c.all_damage_absorption_rate = c.all_damage_absorption_rate * (1 + c.quality / 10.0);
            c.all_vitality_regeneration_rate = c.all_vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.all_accuracy_rate = c.all_accuracy_rate * (1 + c.quality / 10.0);
            c.all_lifesteal_rate = c.all_lifesteal_rate * (1 + c.quality / 10.0);
            c.all_shield_strength = c.all_shield_strength * (1 + c.quality / 10.0);
            c.all_tenacity = c.all_tenacity * (1 + c.quality / 10.0);
            c.all_resistance_rate = c.all_resistance_rate * (1 + c.quality / 10.0);
            c.all_combo_rate = c.all_combo_rate * (1 + c.quality / 10.0);
            c.all_reflection_rate = c.all_reflection_rate * (1 + c.quality / 10.0);
            c.all_mana = (float)(c.all_mana * (1 + c.quality / 10.0));
            c.all_mana_regeneration_rate = c.all_mana_regeneration_rate * (1 + c.quality / 10.0);
            c.all_damage_to_different_faction_rate = c.all_damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_different_faction_rate = c.all_resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_damage_to_same_faction_rate = c.all_damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_same_faction_rate = c.all_resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.all_power = EvaluatePower.CalculatePower(
            c.all_health,
            c.all_physical_attack, c.all_physical_defense,
            c.all_magical_attack, c.all_magical_defense,
            c.all_chemical_attack, c.all_chemical_defense,
            c.all_atomic_attack, c.all_atomic_defense,
            c.all_mental_attack, c.all_mental_defense,
            c.all_speed,
            c.all_critical_damage_rate, c.all_critical_rate,
            c.all_penetration_rate, c.all_evasion_rate,
            c.all_damage_absorption_rate, c.all_vitality_regeneration_rate,
            c.all_accuracy_rate, c.all_lifesteal_rate,
            c.all_shield_strength, c.all_tenacity, c.all_resistance_rate,
            c.all_combo_rate, c.all_reflection_rate,
            c.all_mana, c.all_mana_regeneration_rate,
            c.all_damage_to_different_faction_rate, c.all_resistance_to_different_faction_rate,
            c.all_damage_to_same_faction_rate, c.all_resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<CardColonels> GetQualityPower(List<CardColonels> list)
    {
        foreach (var c in list)
        {
            c.all_health = c.all_health * (1 + c.quality / 10.0);
            c.all_physical_attack = c.all_physical_attack * (1 + c.quality / 10.0);
            c.all_physical_defense = c.all_physical_defense * (1 + c.quality / 10.0);
            c.all_magical_attack = c.all_magical_attack * (1 + c.quality / 10.0);
            c.all_magical_defense = c.all_magical_defense * (1 + c.quality / 10.0);
            c.all_chemical_attack = c.all_chemical_attack * (1 + c.quality / 10.0);
            c.all_chemical_defense = c.all_chemical_defense * (1 + c.quality / 10.0);
            c.all_atomic_attack = c.all_atomic_attack * (1 + c.quality / 10.0);
            c.all_atomic_defense = c.all_atomic_defense * (1 + c.quality / 10.0);
            c.all_mental_attack = c.all_mental_attack * (1 + c.quality / 10.0);
            c.all_mental_defense = c.all_mental_defense * (1 + c.quality / 10.0);
            c.all_speed = c.all_speed * (1 + c.quality / 10.0);
            c.all_critical_damage_rate = c.all_critical_damage_rate * (1 + c.quality / 10.0);
            c.all_critical_rate = c.all_critical_rate * (1 + c.quality / 10.0);
            c.all_penetration_rate = c.all_penetration_rate * (1 + c.quality / 10.0);
            c.all_evasion_rate = c.all_evasion_rate * (1 + c.quality / 10.0);
            c.all_damage_absorption_rate = c.all_damage_absorption_rate * (1 + c.quality / 10.0);
            c.all_vitality_regeneration_rate = c.all_vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.all_accuracy_rate = c.all_accuracy_rate * (1 + c.quality / 10.0);
            c.all_lifesteal_rate = c.all_lifesteal_rate * (1 + c.quality / 10.0);
            c.all_shield_strength = c.all_shield_strength * (1 + c.quality / 10.0);
            c.all_tenacity = c.all_tenacity * (1 + c.quality / 10.0);
            c.all_resistance_rate = c.all_resistance_rate * (1 + c.quality / 10.0);
            c.all_combo_rate = c.all_combo_rate * (1 + c.quality / 10.0);
            c.all_reflection_rate = c.all_reflection_rate * (1 + c.quality / 10.0);
            c.all_mana = (float)(c.all_mana * (1 + c.quality / 10.0));
            c.all_mana_regeneration_rate = c.all_mana_regeneration_rate * (1 + c.quality / 10.0);
            c.all_damage_to_different_faction_rate = c.all_damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_different_faction_rate = c.all_resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_damage_to_same_faction_rate = c.all_damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_same_faction_rate = c.all_resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.all_power = EvaluatePower.CalculatePower(
            c.all_health,
            c.all_physical_attack, c.all_physical_defense,
            c.all_magical_attack, c.all_magical_defense,
            c.all_chemical_attack, c.all_chemical_defense,
            c.all_atomic_attack, c.all_atomic_defense,
            c.all_mental_attack, c.all_mental_defense,
            c.all_speed,
            c.all_critical_damage_rate, c.all_critical_rate,
            c.all_penetration_rate, c.all_evasion_rate,
            c.all_damage_absorption_rate, c.all_vitality_regeneration_rate,
            c.all_accuracy_rate, c.all_lifesteal_rate,
            c.all_shield_strength, c.all_tenacity, c.all_resistance_rate,
            c.all_combo_rate, c.all_reflection_rate,
            c.all_mana, c.all_mana_regeneration_rate,
            c.all_damage_to_different_faction_rate, c.all_resistance_to_different_faction_rate,
            c.all_damage_to_same_faction_rate, c.all_resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<CardGenerals> GetQualityPower(List<CardGenerals> list)
    {
        foreach (var c in list)
        {
            c.all_health = c.all_health * (1 + c.quality / 10.0);
            c.all_physical_attack = c.all_physical_attack * (1 + c.quality / 10.0);
            c.all_physical_defense = c.all_physical_defense * (1 + c.quality / 10.0);
            c.all_magical_attack = c.all_magical_attack * (1 + c.quality / 10.0);
            c.all_magical_defense = c.all_magical_defense * (1 + c.quality / 10.0);
            c.all_chemical_attack = c.all_chemical_attack * (1 + c.quality / 10.0);
            c.all_chemical_defense = c.all_chemical_defense * (1 + c.quality / 10.0);
            c.all_atomic_attack = c.all_atomic_attack * (1 + c.quality / 10.0);
            c.all_atomic_defense = c.all_atomic_defense * (1 + c.quality / 10.0);
            c.all_mental_attack = c.all_mental_attack * (1 + c.quality / 10.0);
            c.all_mental_defense = c.all_mental_defense * (1 + c.quality / 10.0);
            c.all_speed = c.all_speed * (1 + c.quality / 10.0);
            c.all_critical_damage_rate = c.all_critical_damage_rate * (1 + c.quality / 10.0);
            c.all_critical_rate = c.all_critical_rate * (1 + c.quality / 10.0);
            c.all_penetration_rate = c.all_penetration_rate * (1 + c.quality / 10.0);
            c.all_evasion_rate = c.all_evasion_rate * (1 + c.quality / 10.0);
            c.all_damage_absorption_rate = c.all_damage_absorption_rate * (1 + c.quality / 10.0);
            c.all_vitality_regeneration_rate = c.all_vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.all_accuracy_rate = c.all_accuracy_rate * (1 + c.quality / 10.0);
            c.all_lifesteal_rate = c.all_lifesteal_rate * (1 + c.quality / 10.0);
            c.all_shield_strength = c.all_shield_strength * (1 + c.quality / 10.0);
            c.all_tenacity = c.all_tenacity * (1 + c.quality / 10.0);
            c.all_resistance_rate = c.all_resistance_rate * (1 + c.quality / 10.0);
            c.all_combo_rate = c.all_combo_rate * (1 + c.quality / 10.0);
            c.all_reflection_rate = c.all_reflection_rate * (1 + c.quality / 10.0);
            c.all_mana = (float)(c.all_mana * (1 + c.quality / 10.0));
            c.all_mana_regeneration_rate = c.all_mana_regeneration_rate * (1 + c.quality / 10.0);
            c.all_damage_to_different_faction_rate = c.all_damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_different_faction_rate = c.all_resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_damage_to_same_faction_rate = c.all_damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_same_faction_rate = c.all_resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.all_power = EvaluatePower.CalculatePower(
            c.all_health,
            c.all_physical_attack, c.all_physical_defense,
            c.all_magical_attack, c.all_magical_defense,
            c.all_chemical_attack, c.all_chemical_defense,
            c.all_atomic_attack, c.all_atomic_defense,
            c.all_mental_attack, c.all_mental_defense,
            c.all_speed,
            c.all_critical_damage_rate, c.all_critical_rate,
            c.all_penetration_rate, c.all_evasion_rate,
            c.all_damage_absorption_rate, c.all_vitality_regeneration_rate,
            c.all_accuracy_rate, c.all_lifesteal_rate,
            c.all_shield_strength, c.all_tenacity, c.all_resistance_rate,
            c.all_combo_rate, c.all_reflection_rate,
            c.all_mana, c.all_mana_regeneration_rate,
            c.all_damage_to_different_faction_rate, c.all_resistance_to_different_faction_rate,
            c.all_damage_to_same_faction_rate, c.all_resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<CardHeroes> GetQualityPower(List<CardHeroes> list)
    {
        foreach (var c in list)
        {
            c.all_health = c.all_health * (1 + c.quality / 10.0);
            c.all_physical_attack = c.all_physical_attack * (1 + c.quality / 10.0);
            c.all_physical_defense = c.all_physical_defense * (1 + c.quality / 10.0);
            c.all_magical_attack = c.all_magical_attack * (1 + c.quality / 10.0);
            c.all_magical_defense = c.all_magical_defense * (1 + c.quality / 10.0);
            c.all_chemical_attack = c.all_chemical_attack * (1 + c.quality / 10.0);
            c.all_chemical_defense = c.all_chemical_defense * (1 + c.quality / 10.0);
            c.all_atomic_attack = c.all_atomic_attack * (1 + c.quality / 10.0);
            c.all_atomic_defense = c.all_atomic_defense * (1 + c.quality / 10.0);
            c.all_mental_attack = c.all_mental_attack * (1 + c.quality / 10.0);
            c.all_mental_defense = c.all_mental_defense * (1 + c.quality / 10.0);
            c.all_speed = c.all_speed * (1 + c.quality / 10.0);
            c.all_critical_damage_rate = c.all_critical_damage_rate * (1 + c.quality / 10.0);
            c.all_critical_rate = c.all_critical_rate * (1 + c.quality / 10.0);
            c.all_penetration_rate = c.all_penetration_rate * (1 + c.quality / 10.0);
            c.all_evasion_rate = c.all_evasion_rate * (1 + c.quality / 10.0);
            c.all_damage_absorption_rate = c.all_damage_absorption_rate * (1 + c.quality / 10.0);
            c.all_vitality_regeneration_rate = c.all_vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.all_accuracy_rate = c.all_accuracy_rate * (1 + c.quality / 10.0);
            c.all_lifesteal_rate = c.all_lifesteal_rate * (1 + c.quality / 10.0);
            c.all_shield_strength = c.all_shield_strength * (1 + c.quality / 10.0);
            c.all_tenacity = c.all_tenacity * (1 + c.quality / 10.0);
            c.all_resistance_rate = c.all_resistance_rate * (1 + c.quality / 10.0);
            c.all_combo_rate = c.all_combo_rate * (1 + c.quality / 10.0);
            c.all_reflection_rate = c.all_reflection_rate * (1 + c.quality / 10.0);
            c.all_mana = (float)(c.all_mana * (1 + c.quality / 10.0));
            c.all_mana_regeneration_rate = c.all_mana_regeneration_rate * (1 + c.quality / 10.0);
            c.all_damage_to_different_faction_rate = c.all_damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_different_faction_rate = c.all_resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_damage_to_same_faction_rate = c.all_damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_same_faction_rate = c.all_resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.all_power = EvaluatePower.CalculatePower(
            c.all_health,
            c.all_physical_attack, c.all_physical_defense,
            c.all_magical_attack, c.all_magical_defense,
            c.all_chemical_attack, c.all_chemical_defense,
            c.all_atomic_attack, c.all_atomic_defense,
            c.all_mental_attack, c.all_mental_defense,
            c.all_speed,
            c.all_critical_damage_rate, c.all_critical_rate,
            c.all_penetration_rate, c.all_evasion_rate,
            c.all_damage_absorption_rate, c.all_vitality_regeneration_rate,
            c.all_accuracy_rate, c.all_lifesteal_rate,
            c.all_shield_strength, c.all_tenacity, c.all_resistance_rate,
            c.all_combo_rate, c.all_reflection_rate,
            c.all_mana, c.all_mana_regeneration_rate,
            c.all_damage_to_different_faction_rate, c.all_resistance_to_different_faction_rate,
            c.all_damage_to_same_faction_rate, c.all_resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<CardLife> GetQualityPower(List<CardLife> list)
    {
        foreach (var c in list)
        {
            c.health = c.health * (1 + c.quality / 10.0);
            c.physical_attack = c.physical_attack * (1 + c.quality / 10.0);
            c.physical_defense = c.physical_defense * (1 + c.quality / 10.0);
            c.magical_attack = c.magical_attack * (1 + c.quality / 10.0);
            c.magical_defense = c.magical_defense * (1 + c.quality / 10.0);
            c.chemical_attack = c.chemical_attack * (1 + c.quality / 10.0);
            c.chemical_defense = c.chemical_defense * (1 + c.quality / 10.0);
            c.atomic_attack = c.atomic_attack * (1 + c.quality / 10.0);
            c.atomic_defense = c.atomic_defense * (1 + c.quality / 10.0);
            c.mental_attack = c.mental_attack * (1 + c.quality / 10.0);
            c.mental_defense = c.mental_defense * (1 + c.quality / 10.0);
            c.speed = c.speed * (1 + c.quality / 10.0);
            c.critical_damage_rate = c.critical_damage_rate * (1 + c.quality / 10.0);
            c.critical_rate = c.critical_rate * (1 + c.quality / 10.0);
            c.penetration_rate = c.penetration_rate * (1 + c.quality / 10.0);
            c.evasion_rate = c.evasion_rate * (1 + c.quality / 10.0);
            c.damage_absorption_rate = c.damage_absorption_rate * (1 + c.quality / 10.0);
            c.vitality_regeneration_rate = c.vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.accuracy_rate = c.accuracy_rate * (1 + c.quality / 10.0);
            c.lifesteal_rate = c.lifesteal_rate * (1 + c.quality / 10.0);
            c.shield_strength = c.shield_strength * (1 + c.quality / 10.0);
            c.tenacity = c.tenacity * (1 + c.quality / 10.0);
            c.resistance_rate = c.resistance_rate * (1 + c.quality / 10.0);
            c.combo_rate = c.combo_rate * (1 + c.quality / 10.0);
            c.reflection_rate = c.reflection_rate * (1 + c.quality / 10.0);
            c.mana = (float)(c.mana * (1 + c.quality / 10.0));
            c.mana_regeneration_rate = c.mana_regeneration_rate * (1 + c.quality / 10.0);
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<CardMilitary> GetQualityPower(List<CardMilitary> list)
    {
        foreach (var c in list)
        {
            c.all_health = c.all_health * (1 + c.quality / 10.0);
            c.all_physical_attack = c.all_physical_attack * (1 + c.quality / 10.0);
            c.all_physical_defense = c.all_physical_defense * (1 + c.quality / 10.0);
            c.all_magical_attack = c.all_magical_attack * (1 + c.quality / 10.0);
            c.all_magical_defense = c.all_magical_defense * (1 + c.quality / 10.0);
            c.all_chemical_attack = c.all_chemical_attack * (1 + c.quality / 10.0);
            c.all_chemical_defense = c.all_chemical_defense * (1 + c.quality / 10.0);
            c.all_atomic_attack = c.all_atomic_attack * (1 + c.quality / 10.0);
            c.all_atomic_defense = c.all_atomic_defense * (1 + c.quality / 10.0);
            c.all_mental_attack = c.all_mental_attack * (1 + c.quality / 10.0);
            c.all_mental_defense = c.all_mental_defense * (1 + c.quality / 10.0);
            c.all_speed = c.all_speed * (1 + c.quality / 10.0);
            c.all_critical_damage_rate = c.all_critical_damage_rate * (1 + c.quality / 10.0);
            c.all_critical_rate = c.all_critical_rate * (1 + c.quality / 10.0);
            c.all_penetration_rate = c.all_penetration_rate * (1 + c.quality / 10.0);
            c.all_evasion_rate = c.all_evasion_rate * (1 + c.quality / 10.0);
            c.all_damage_absorption_rate = c.all_damage_absorption_rate * (1 + c.quality / 10.0);
            c.all_vitality_regeneration_rate = c.all_vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.all_accuracy_rate = c.all_accuracy_rate * (1 + c.quality / 10.0);
            c.all_lifesteal_rate = c.all_lifesteal_rate * (1 + c.quality / 10.0);
            c.all_shield_strength = c.all_shield_strength * (1 + c.quality / 10.0);
            c.all_tenacity = c.all_tenacity * (1 + c.quality / 10.0);
            c.all_resistance_rate = c.all_resistance_rate * (1 + c.quality / 10.0);
            c.all_combo_rate = c.all_combo_rate * (1 + c.quality / 10.0);
            c.all_reflection_rate = c.all_reflection_rate * (1 + c.quality / 10.0);
            c.all_mana = (float)(c.all_mana * (1 + c.quality / 10.0));
            c.all_mana_regeneration_rate = c.all_mana_regeneration_rate * (1 + c.quality / 10.0);
            c.all_damage_to_different_faction_rate = c.all_damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_different_faction_rate = c.all_resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_damage_to_same_faction_rate = c.all_damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_same_faction_rate = c.all_resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.all_power = EvaluatePower.CalculatePower(
            c.all_health,
            c.all_physical_attack, c.all_physical_defense,
            c.all_magical_attack, c.all_magical_defense,
            c.all_chemical_attack, c.all_chemical_defense,
            c.all_atomic_attack, c.all_atomic_defense,
            c.all_mental_attack, c.all_mental_defense,
            c.all_speed,
            c.all_critical_damage_rate, c.all_critical_rate,
            c.all_penetration_rate, c.all_evasion_rate,
            c.all_damage_absorption_rate, c.all_vitality_regeneration_rate,
            c.all_accuracy_rate, c.all_lifesteal_rate,
            c.all_shield_strength, c.all_tenacity, c.all_resistance_rate,
            c.all_combo_rate, c.all_reflection_rate,
            c.all_mana, c.all_mana_regeneration_rate,
            c.all_damage_to_different_faction_rate, c.all_resistance_to_different_faction_rate,
            c.all_damage_to_same_faction_rate, c.all_resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<CardMonsters> GetQualityPower(List<CardMonsters> list)
    {
        foreach (var c in list)
        {
            c.all_health = c.all_health * (1 + c.quality / 10.0);
            c.all_physical_attack = c.all_physical_attack * (1 + c.quality / 10.0);
            c.all_physical_defense = c.all_physical_defense * (1 + c.quality / 10.0);
            c.all_magical_attack = c.all_magical_attack * (1 + c.quality / 10.0);
            c.all_magical_defense = c.all_magical_defense * (1 + c.quality / 10.0);
            c.all_chemical_attack = c.all_chemical_attack * (1 + c.quality / 10.0);
            c.all_chemical_defense = c.all_chemical_defense * (1 + c.quality / 10.0);
            c.all_atomic_attack = c.all_atomic_attack * (1 + c.quality / 10.0);
            c.all_atomic_defense = c.all_atomic_defense * (1 + c.quality / 10.0);
            c.all_mental_attack = c.all_mental_attack * (1 + c.quality / 10.0);
            c.all_mental_defense = c.all_mental_defense * (1 + c.quality / 10.0);
            c.all_speed = c.all_speed * (1 + c.quality / 10.0);
            c.all_critical_damage_rate = c.all_critical_damage_rate * (1 + c.quality / 10.0);
            c.all_critical_rate = c.all_critical_rate * (1 + c.quality / 10.0);
            c.all_penetration_rate = c.all_penetration_rate * (1 + c.quality / 10.0);
            c.all_evasion_rate = c.all_evasion_rate * (1 + c.quality / 10.0);
            c.all_damage_absorption_rate = c.all_damage_absorption_rate * (1 + c.quality / 10.0);
            c.all_vitality_regeneration_rate = c.all_vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.all_accuracy_rate = c.all_accuracy_rate * (1 + c.quality / 10.0);
            c.all_lifesteal_rate = c.all_lifesteal_rate * (1 + c.quality / 10.0);
            c.all_shield_strength = c.all_shield_strength * (1 + c.quality / 10.0);
            c.all_tenacity = c.all_tenacity * (1 + c.quality / 10.0);
            c.all_resistance_rate = c.all_resistance_rate * (1 + c.quality / 10.0);
            c.all_combo_rate = c.all_combo_rate * (1 + c.quality / 10.0);
            c.all_reflection_rate = c.all_reflection_rate * (1 + c.quality / 10.0);
            c.all_mana = (float)(c.all_mana * (1 + c.quality / 10.0));
            c.all_mana_regeneration_rate = c.all_mana_regeneration_rate * (1 + c.quality / 10.0);
            c.all_damage_to_different_faction_rate = c.all_damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_different_faction_rate = c.all_resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_damage_to_same_faction_rate = c.all_damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_same_faction_rate = c.all_resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.all_power = EvaluatePower.CalculatePower(
            c.all_health,
            c.all_physical_attack, c.all_physical_defense,
            c.all_magical_attack, c.all_magical_defense,
            c.all_chemical_attack, c.all_chemical_defense,
            c.all_atomic_attack, c.all_atomic_defense,
            c.all_mental_attack, c.all_mental_defense,
            c.all_speed,
            c.all_critical_damage_rate, c.all_critical_rate,
            c.all_penetration_rate, c.all_evasion_rate,
            c.all_damage_absorption_rate, c.all_vitality_regeneration_rate,
            c.all_accuracy_rate, c.all_lifesteal_rate,
            c.all_shield_strength, c.all_tenacity, c.all_resistance_rate,
            c.all_combo_rate, c.all_reflection_rate,
            c.all_mana, c.all_mana_regeneration_rate,
            c.all_damage_to_different_faction_rate, c.all_resistance_to_different_faction_rate,
            c.all_damage_to_same_faction_rate, c.all_resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<CardSpell> GetQualityPower(List<CardSpell> list)
    {
        foreach (var c in list)
        {
            c.all_health = c.all_health * (1 + c.quality / 10.0);
            c.all_physical_attack = c.all_physical_attack * (1 + c.quality / 10.0);
            c.all_physical_defense = c.all_physical_defense * (1 + c.quality / 10.0);
            c.all_magical_attack = c.all_magical_attack * (1 + c.quality / 10.0);
            c.all_magical_defense = c.all_magical_defense * (1 + c.quality / 10.0);
            c.all_chemical_attack = c.all_chemical_attack * (1 + c.quality / 10.0);
            c.all_chemical_defense = c.all_chemical_defense * (1 + c.quality / 10.0);
            c.all_atomic_attack = c.all_atomic_attack * (1 + c.quality / 10.0);
            c.all_atomic_defense = c.all_atomic_defense * (1 + c.quality / 10.0);
            c.all_mental_attack = c.all_mental_attack * (1 + c.quality / 10.0);
            c.all_mental_defense = c.all_mental_defense * (1 + c.quality / 10.0);
            c.all_speed = c.all_speed * (1 + c.quality / 10.0);
            c.all_critical_damage_rate = c.all_critical_damage_rate * (1 + c.quality / 10.0);
            c.all_critical_rate = c.all_critical_rate * (1 + c.quality / 10.0);
            c.all_penetration_rate = c.all_penetration_rate * (1 + c.quality / 10.0);
            c.all_evasion_rate = c.all_evasion_rate * (1 + c.quality / 10.0);
            c.all_damage_absorption_rate = c.all_damage_absorption_rate * (1 + c.quality / 10.0);
            c.all_vitality_regeneration_rate = c.all_vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.all_accuracy_rate = c.all_accuracy_rate * (1 + c.quality / 10.0);
            c.all_lifesteal_rate = c.all_lifesteal_rate * (1 + c.quality / 10.0);
            c.all_shield_strength = c.all_shield_strength * (1 + c.quality / 10.0);
            c.all_tenacity = c.all_tenacity * (1 + c.quality / 10.0);
            c.all_resistance_rate = c.all_resistance_rate * (1 + c.quality / 10.0);
            c.all_combo_rate = c.all_combo_rate * (1 + c.quality / 10.0);
            c.all_reflection_rate = c.all_reflection_rate * (1 + c.quality / 10.0);
            c.all_mana = (float)(c.all_mana * (1 + c.quality / 10.0));
            c.all_mana_regeneration_rate = c.all_mana_regeneration_rate * (1 + c.quality / 10.0);
            c.all_damage_to_different_faction_rate = c.all_damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_different_faction_rate = c.all_resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_damage_to_same_faction_rate = c.all_damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_same_faction_rate = c.all_resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.all_power = EvaluatePower.CalculatePower(
            c.all_health,
            c.all_physical_attack, c.all_physical_defense,
            c.all_magical_attack, c.all_magical_defense,
            c.all_chemical_attack, c.all_chemical_defense,
            c.all_atomic_attack, c.all_atomic_defense,
            c.all_mental_attack, c.all_mental_defense,
            c.all_speed,
            c.all_critical_damage_rate, c.all_critical_rate,
            c.all_penetration_rate, c.all_evasion_rate,
            c.all_damage_absorption_rate, c.all_vitality_regeneration_rate,
            c.all_accuracy_rate, c.all_lifesteal_rate,
            c.all_shield_strength, c.all_tenacity, c.all_resistance_rate,
            c.all_combo_rate, c.all_reflection_rate,
            c.all_mana, c.all_mana_regeneration_rate,
            c.all_damage_to_different_faction_rate, c.all_resistance_to_different_faction_rate,
            c.all_damage_to_same_faction_rate, c.all_resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<CollaborationEquipment> GetQualityPower(List<CollaborationEquipment> list)
    {
        foreach (var c in list)
        {
            c.health = c.health * (1 + c.quality / 10.0);
            c.physical_attack = c.physical_attack * (1 + c.quality / 10.0);
            c.physical_defense = c.physical_defense * (1 + c.quality / 10.0);
            c.magical_attack = c.magical_attack * (1 + c.quality / 10.0);
            c.magical_defense = c.magical_defense * (1 + c.quality / 10.0);
            c.chemical_attack = c.chemical_attack * (1 + c.quality / 10.0);
            c.chemical_defense = c.chemical_defense * (1 + c.quality / 10.0);
            c.atomic_attack = c.atomic_attack * (1 + c.quality / 10.0);
            c.atomic_defense = c.atomic_defense * (1 + c.quality / 10.0);
            c.mental_attack = c.mental_attack * (1 + c.quality / 10.0);
            c.mental_defense = c.mental_defense * (1 + c.quality / 10.0);
            c.speed = c.speed * (1 + c.quality / 10.0);
            c.critical_damage_rate = c.critical_damage_rate * (1 + c.quality / 10.0);
            c.critical_rate = c.critical_rate * (1 + c.quality / 10.0);
            c.penetration_rate = c.penetration_rate * (1 + c.quality / 10.0);
            c.evasion_rate = c.evasion_rate * (1 + c.quality / 10.0);
            c.damage_absorption_rate = c.damage_absorption_rate * (1 + c.quality / 10.0);
            c.vitality_regeneration_rate = c.vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.accuracy_rate = c.accuracy_rate * (1 + c.quality / 10.0);
            c.lifesteal_rate = c.lifesteal_rate * (1 + c.quality / 10.0);
            c.shield_strength = c.shield_strength * (1 + c.quality / 10.0);
            c.tenacity = c.tenacity * (1 + c.quality / 10.0);
            c.resistance_rate = c.resistance_rate * (1 + c.quality / 10.0);
            c.combo_rate = c.combo_rate * (1 + c.quality / 10.0);
            c.reflection_rate = c.reflection_rate * (1 + c.quality / 10.0);
            c.mana = (float)(c.mana * (1 + c.quality / 10.0));
            c.mana_regeneration_rate = c.mana_regeneration_rate * (1 + c.quality / 10.0);
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<Collaboration> GetQualityPower(List<Collaboration> list)
    {
        foreach (var c in list)
        {
            c.health = c.health * (1 + c.quality / 10.0);
            c.physical_attack = c.physical_attack * (1 + c.quality / 10.0);
            c.physical_defense = c.physical_defense * (1 + c.quality / 10.0);
            c.magical_attack = c.magical_attack * (1 + c.quality / 10.0);
            c.magical_defense = c.magical_defense * (1 + c.quality / 10.0);
            c.chemical_attack = c.chemical_attack * (1 + c.quality / 10.0);
            c.chemical_defense = c.chemical_defense * (1 + c.quality / 10.0);
            c.atomic_attack = c.atomic_attack * (1 + c.quality / 10.0);
            c.atomic_defense = c.atomic_defense * (1 + c.quality / 10.0);
            c.mental_attack = c.mental_attack * (1 + c.quality / 10.0);
            c.mental_defense = c.mental_defense * (1 + c.quality / 10.0);
            c.speed = c.speed * (1 + c.quality / 10.0);
            c.critical_damage_rate = c.critical_damage_rate * (1 + c.quality / 10.0);
            c.critical_rate = c.critical_rate * (1 + c.quality / 10.0);
            c.penetration_rate = c.penetration_rate * (1 + c.quality / 10.0);
            c.evasion_rate = c.evasion_rate * (1 + c.quality / 10.0);
            c.damage_absorption_rate = c.damage_absorption_rate * (1 + c.quality / 10.0);
            c.vitality_regeneration_rate = c.vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.accuracy_rate = c.accuracy_rate * (1 + c.quality / 10.0);
            c.lifesteal_rate = c.lifesteal_rate * (1 + c.quality / 10.0);
            c.shield_strength = c.shield_strength * (1 + c.quality / 10.0);
            c.tenacity = c.tenacity * (1 + c.quality / 10.0);
            c.resistance_rate = c.resistance_rate * (1 + c.quality / 10.0);
            c.combo_rate = c.combo_rate * (1 + c.quality / 10.0);
            c.reflection_rate = c.reflection_rate * (1 + c.quality / 10.0);
            c.mana = (float)(c.mana * (1 + c.quality / 10.0));
            c.mana_regeneration_rate = c.mana_regeneration_rate * (1 + c.quality / 10.0);
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<Equipments> GetQualityPower(List<Equipments> list)
    {
        foreach (var c in list)
        {
            c.health = c.health * (1 + c.quality / 10.0);
            c.physical_attack = c.physical_attack * (1 + c.quality / 10.0);
            c.physical_defense = c.physical_defense * (1 + c.quality / 10.0);
            c.magical_attack = c.magical_attack * (1 + c.quality / 10.0);
            c.magical_defense = c.magical_defense * (1 + c.quality / 10.0);
            c.chemical_attack = c.chemical_attack * (1 + c.quality / 10.0);
            c.chemical_defense = c.chemical_defense * (1 + c.quality / 10.0);
            c.atomic_attack = c.atomic_attack * (1 + c.quality / 10.0);
            c.atomic_defense = c.atomic_defense * (1 + c.quality / 10.0);
            c.mental_attack = c.mental_attack * (1 + c.quality / 10.0);
            c.mental_defense = c.mental_defense * (1 + c.quality / 10.0);
            c.speed = c.speed * (1 + c.quality / 10.0);
            c.critical_damage_rate = c.critical_damage_rate * (1 + c.quality / 10.0);
            c.critical_rate = c.critical_rate * (1 + c.quality / 10.0);
            c.penetration_rate = c.penetration_rate * (1 + c.quality / 10.0);
            c.evasion_rate = c.evasion_rate * (1 + c.quality / 10.0);
            c.damage_absorption_rate = c.damage_absorption_rate * (1 + c.quality / 10.0);
            c.vitality_regeneration_rate = c.vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.accuracy_rate = c.accuracy_rate * (1 + c.quality / 10.0);
            c.lifesteal_rate = c.lifesteal_rate * (1 + c.quality / 10.0);
            c.shield_strength = c.shield_strength * (1 + c.quality / 10.0);
            c.tenacity = c.tenacity * (1 + c.quality / 10.0);
            c.resistance_rate = c.resistance_rate * (1 + c.quality / 10.0);
            c.combo_rate = c.combo_rate * (1 + c.quality / 10.0);
            c.reflection_rate = c.reflection_rate * (1 + c.quality / 10.0);
            c.mana = (float)(c.mana * (1 + c.quality / 10.0));
            c.mana_regeneration_rate = c.mana_regeneration_rate * (1 + c.quality / 10.0);
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<Forge> GetQualityPower(List<Forge> list)
    {
        foreach (var c in list)
        {
            c.health = c.health * (1 + c.quality / 10.0);
            c.physical_attack = c.physical_attack * (1 + c.quality / 10.0);
            c.physical_defense = c.physical_defense * (1 + c.quality / 10.0);
            c.magical_attack = c.magical_attack * (1 + c.quality / 10.0);
            c.magical_defense = c.magical_defense * (1 + c.quality / 10.0);
            c.chemical_attack = c.chemical_attack * (1 + c.quality / 10.0);
            c.chemical_defense = c.chemical_defense * (1 + c.quality / 10.0);
            c.atomic_attack = c.atomic_attack * (1 + c.quality / 10.0);
            c.atomic_defense = c.atomic_defense * (1 + c.quality / 10.0);
            c.mental_attack = c.mental_attack * (1 + c.quality / 10.0);
            c.mental_defense = c.mental_defense * (1 + c.quality / 10.0);
            c.speed = c.speed * (1 + c.quality / 10.0);
            c.critical_damage_rate = c.critical_damage_rate * (1 + c.quality / 10.0);
            c.critical_rate = c.critical_rate * (1 + c.quality / 10.0);
            c.penetration_rate = c.penetration_rate * (1 + c.quality / 10.0);
            c.evasion_rate = c.evasion_rate * (1 + c.quality / 10.0);
            c.damage_absorption_rate = c.damage_absorption_rate * (1 + c.quality / 10.0);
            c.vitality_regeneration_rate = c.vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.accuracy_rate = c.accuracy_rate * (1 + c.quality / 10.0);
            c.lifesteal_rate = c.lifesteal_rate * (1 + c.quality / 10.0);
            c.shield_strength = c.shield_strength * (1 + c.quality / 10.0);
            c.tenacity = c.tenacity * (1 + c.quality / 10.0);
            c.resistance_rate = c.resistance_rate * (1 + c.quality / 10.0);
            c.combo_rate = c.combo_rate * (1 + c.quality / 10.0);
            c.reflection_rate = c.reflection_rate * (1 + c.quality / 10.0);
            c.mana = (float)(c.mana * (1 + c.quality / 10.0));
            c.mana_regeneration_rate = c.mana_regeneration_rate * (1 + c.quality / 10.0);
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<MagicFormationCircle> GetQualityPower(List<MagicFormationCircle> list)
    {
        foreach (var c in list)
        {
            c.health = c.health * (1 + c.quality / 10.0);
            c.physical_attack = c.physical_attack * (1 + c.quality / 10.0);
            c.physical_defense = c.physical_defense * (1 + c.quality / 10.0);
            c.magical_attack = c.magical_attack * (1 + c.quality / 10.0);
            c.magical_defense = c.magical_defense * (1 + c.quality / 10.0);
            c.chemical_attack = c.chemical_attack * (1 + c.quality / 10.0);
            c.chemical_defense = c.chemical_defense * (1 + c.quality / 10.0);
            c.atomic_attack = c.atomic_attack * (1 + c.quality / 10.0);
            c.atomic_defense = c.atomic_defense * (1 + c.quality / 10.0);
            c.mental_attack = c.mental_attack * (1 + c.quality / 10.0);
            c.mental_defense = c.mental_defense * (1 + c.quality / 10.0);
            c.speed = c.speed * (1 + c.quality / 10.0);
            c.critical_damage_rate = c.critical_damage_rate * (1 + c.quality / 10.0);
            c.critical_rate = c.critical_rate * (1 + c.quality / 10.0);
            c.penetration_rate = c.penetration_rate * (1 + c.quality / 10.0);
            c.evasion_rate = c.evasion_rate * (1 + c.quality / 10.0);
            c.damage_absorption_rate = c.damage_absorption_rate * (1 + c.quality / 10.0);
            c.vitality_regeneration_rate = c.vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.accuracy_rate = c.accuracy_rate * (1 + c.quality / 10.0);
            c.lifesteal_rate = c.lifesteal_rate * (1 + c.quality / 10.0);
            c.shield_strength = c.shield_strength * (1 + c.quality / 10.0);
            c.tenacity = c.tenacity * (1 + c.quality / 10.0);
            c.resistance_rate = c.resistance_rate * (1 + c.quality / 10.0);
            c.combo_rate = c.combo_rate * (1 + c.quality / 10.0);
            c.reflection_rate = c.reflection_rate * (1 + c.quality / 10.0);
            c.mana = (float)(c.mana * (1 + c.quality / 10.0));
            c.mana_regeneration_rate = c.mana_regeneration_rate * (1 + c.quality / 10.0);
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<Medals> GetQualityPower(List<Medals> list)
    {
        foreach (var c in list)
        {
            c.health = c.health * (1 + c.quality / 10.0);
            c.physical_attack = c.physical_attack * (1 + c.quality / 10.0);
            c.physical_defense = c.physical_defense * (1 + c.quality / 10.0);
            c.magical_attack = c.magical_attack * (1 + c.quality / 10.0);
            c.magical_defense = c.magical_defense * (1 + c.quality / 10.0);
            c.chemical_attack = c.chemical_attack * (1 + c.quality / 10.0);
            c.chemical_defense = c.chemical_defense * (1 + c.quality / 10.0);
            c.atomic_attack = c.atomic_attack * (1 + c.quality / 10.0);
            c.atomic_defense = c.atomic_defense * (1 + c.quality / 10.0);
            c.mental_attack = c.mental_attack * (1 + c.quality / 10.0);
            c.mental_defense = c.mental_defense * (1 + c.quality / 10.0);
            c.speed = c.speed * (1 + c.quality / 10.0);
            c.critical_damage_rate = c.critical_damage_rate * (1 + c.quality / 10.0);
            c.critical_rate = c.critical_rate * (1 + c.quality / 10.0);
            c.penetration_rate = c.penetration_rate * (1 + c.quality / 10.0);
            c.evasion_rate = c.evasion_rate * (1 + c.quality / 10.0);
            c.damage_absorption_rate = c.damage_absorption_rate * (1 + c.quality / 10.0);
            c.vitality_regeneration_rate = c.vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.accuracy_rate = c.accuracy_rate * (1 + c.quality / 10.0);
            c.lifesteal_rate = c.lifesteal_rate * (1 + c.quality / 10.0);
            c.shield_strength = c.shield_strength * (1 + c.quality / 10.0);
            c.tenacity = c.tenacity * (1 + c.quality / 10.0);
            c.resistance_rate = c.resistance_rate * (1 + c.quality / 10.0);
            c.combo_rate = c.combo_rate * (1 + c.quality / 10.0);
            c.reflection_rate = c.reflection_rate * (1 + c.quality / 10.0);
            c.mana = (float)(c.mana * (1 + c.quality / 10.0));
            c.mana_regeneration_rate = c.mana_regeneration_rate * (1 + c.quality / 10.0);
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<Pets> GetQualityPower(List<Pets> list)
    {
        foreach (var c in list)
        {
            c.all_health = c.all_health * (1 + c.quality / 10.0);
            c.all_physical_attack = c.all_physical_attack * (1 + c.quality / 10.0);
            c.all_physical_defense = c.all_physical_defense * (1 + c.quality / 10.0);
            c.all_magical_attack = c.all_magical_attack * (1 + c.quality / 10.0);
            c.all_magical_defense = c.all_magical_defense * (1 + c.quality / 10.0);
            c.all_chemical_attack = c.all_chemical_attack * (1 + c.quality / 10.0);
            c.all_chemical_defense = c.all_chemical_defense * (1 + c.quality / 10.0);
            c.all_atomic_attack = c.all_atomic_attack * (1 + c.quality / 10.0);
            c.all_atomic_defense = c.all_atomic_defense * (1 + c.quality / 10.0);
            c.all_mental_attack = c.all_mental_attack * (1 + c.quality / 10.0);
            c.all_mental_defense = c.all_mental_defense * (1 + c.quality / 10.0);
            c.all_speed = c.all_speed * (1 + c.quality / 10.0);
            c.all_critical_damage_rate = c.all_critical_damage_rate * (1 + c.quality / 10.0);
            c.all_critical_rate = c.all_critical_rate * (1 + c.quality / 10.0);
            c.all_penetration_rate = c.all_penetration_rate * (1 + c.quality / 10.0);
            c.all_evasion_rate = c.all_evasion_rate * (1 + c.quality / 10.0);
            c.all_damage_absorption_rate = c.all_damage_absorption_rate * (1 + c.quality / 10.0);
            c.all_vitality_regeneration_rate = c.all_vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.all_accuracy_rate = c.all_accuracy_rate * (1 + c.quality / 10.0);
            c.all_lifesteal_rate = c.all_lifesteal_rate * (1 + c.quality / 10.0);
            c.all_shield_strength = c.all_shield_strength * (1 + c.quality / 10.0);
            c.all_tenacity = c.all_tenacity * (1 + c.quality / 10.0);
            c.all_resistance_rate = c.all_resistance_rate * (1 + c.quality / 10.0);
            c.all_combo_rate = c.all_combo_rate * (1 + c.quality / 10.0);
            c.all_reflection_rate = c.all_reflection_rate * (1 + c.quality / 10.0);
            c.all_mana = (float)(c.all_mana * (1 + c.quality / 10.0));
            c.all_mana_regeneration_rate = c.all_mana_regeneration_rate * (1 + c.quality / 10.0);
            c.all_damage_to_different_faction_rate = c.all_damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_different_faction_rate = c.all_resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.all_damage_to_same_faction_rate = c.all_damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.all_resistance_to_same_faction_rate = c.all_resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.all_power = EvaluatePower.CalculatePower(
            c.all_health,
            c.all_physical_attack, c.all_physical_defense,
            c.all_magical_attack, c.all_magical_defense,
            c.all_chemical_attack, c.all_chemical_defense,
            c.all_atomic_attack, c.all_atomic_defense,
            c.all_mental_attack, c.all_mental_defense,
            c.all_speed,
            c.all_critical_damage_rate, c.all_critical_rate,
            c.all_penetration_rate, c.all_evasion_rate,
            c.all_damage_absorption_rate, c.all_vitality_regeneration_rate,
            c.all_accuracy_rate, c.all_lifesteal_rate,
            c.all_shield_strength, c.all_tenacity, c.all_resistance_rate,
            c.all_combo_rate, c.all_reflection_rate,
            c.all_mana, c.all_mana_regeneration_rate,
            c.all_damage_to_different_faction_rate, c.all_resistance_to_different_faction_rate,
            c.all_damage_to_same_faction_rate, c.all_resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<Puppet> GetQualityPower(List<Puppet> list)
    {
        foreach (var c in list)
        {
            c.health = c.health * (1 + c.quality / 10.0);
            c.physical_attack = c.physical_attack * (1 + c.quality / 10.0);
            c.physical_defense = c.physical_defense * (1 + c.quality / 10.0);
            c.magical_attack = c.magical_attack * (1 + c.quality / 10.0);
            c.magical_defense = c.magical_defense * (1 + c.quality / 10.0);
            c.chemical_attack = c.chemical_attack * (1 + c.quality / 10.0);
            c.chemical_defense = c.chemical_defense * (1 + c.quality / 10.0);
            c.atomic_attack = c.atomic_attack * (1 + c.quality / 10.0);
            c.atomic_defense = c.atomic_defense * (1 + c.quality / 10.0);
            c.mental_attack = c.mental_attack * (1 + c.quality / 10.0);
            c.mental_defense = c.mental_defense * (1 + c.quality / 10.0);
            c.speed = c.speed * (1 + c.quality / 10.0);
            c.critical_damage_rate = c.critical_damage_rate * (1 + c.quality / 10.0);
            c.critical_rate = c.critical_rate * (1 + c.quality / 10.0);
            c.penetration_rate = c.penetration_rate * (1 + c.quality / 10.0);
            c.evasion_rate = c.evasion_rate * (1 + c.quality / 10.0);
            c.damage_absorption_rate = c.damage_absorption_rate * (1 + c.quality / 10.0);
            c.vitality_regeneration_rate = c.vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.accuracy_rate = c.accuracy_rate * (1 + c.quality / 10.0);
            c.lifesteal_rate = c.lifesteal_rate * (1 + c.quality / 10.0);
            c.shield_strength = c.shield_strength * (1 + c.quality / 10.0);
            c.tenacity = c.tenacity * (1 + c.quality / 10.0);
            c.resistance_rate = c.resistance_rate * (1 + c.quality / 10.0);
            c.combo_rate = c.combo_rate * (1 + c.quality / 10.0);
            c.reflection_rate = c.reflection_rate * (1 + c.quality / 10.0);
            c.mana = (float)(c.mana * (1 + c.quality / 10.0));
            c.mana_regeneration_rate = c.mana_regeneration_rate * (1 + c.quality / 10.0);
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<Relics> GetQualityPower(List<Relics> list)
    {
        foreach (var c in list)
        {
            c.health = c.health * (1 + c.quality / 10.0);
            c.physical_attack = c.physical_attack * (1 + c.quality / 10.0);
            c.physical_defense = c.physical_defense * (1 + c.quality / 10.0);
            c.magical_attack = c.magical_attack * (1 + c.quality / 10.0);
            c.magical_defense = c.magical_defense * (1 + c.quality / 10.0);
            c.chemical_attack = c.chemical_attack * (1 + c.quality / 10.0);
            c.chemical_defense = c.chemical_defense * (1 + c.quality / 10.0);
            c.atomic_attack = c.atomic_attack * (1 + c.quality / 10.0);
            c.atomic_defense = c.atomic_defense * (1 + c.quality / 10.0);
            c.mental_attack = c.mental_attack * (1 + c.quality / 10.0);
            c.mental_defense = c.mental_defense * (1 + c.quality / 10.0);
            c.speed = c.speed * (1 + c.quality / 10.0);
            c.critical_damage_rate = c.critical_damage_rate * (1 + c.quality / 10.0);
            c.critical_rate = c.critical_rate * (1 + c.quality / 10.0);
            c.penetration_rate = c.penetration_rate * (1 + c.quality / 10.0);
            c.evasion_rate = c.evasion_rate * (1 + c.quality / 10.0);
            c.damage_absorption_rate = c.damage_absorption_rate * (1 + c.quality / 10.0);
            c.vitality_regeneration_rate = c.vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.accuracy_rate = c.accuracy_rate * (1 + c.quality / 10.0);
            c.lifesteal_rate = c.lifesteal_rate * (1 + c.quality / 10.0);
            c.shield_strength = c.shield_strength * (1 + c.quality / 10.0);
            c.tenacity = c.tenacity * (1 + c.quality / 10.0);
            c.resistance_rate = c.resistance_rate * (1 + c.quality / 10.0);
            c.combo_rate = c.combo_rate * (1 + c.quality / 10.0);
            c.reflection_rate = c.reflection_rate * (1 + c.quality / 10.0);
            c.mana = (float)(c.mana * (1 + c.quality / 10.0));
            c.mana_regeneration_rate = c.mana_regeneration_rate * (1 + c.quality / 10.0);
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<Skills> GetQualityPower(List<Skills> list)
    {
        foreach (var c in list)
        {
            c.health = c.health * (1 + c.quality / 10.0);
            c.physical_attack = c.physical_attack * (1 + c.quality / 10.0);
            c.physical_defense = c.physical_defense * (1 + c.quality / 10.0);
            c.magical_attack = c.magical_attack * (1 + c.quality / 10.0);
            c.magical_defense = c.magical_defense * (1 + c.quality / 10.0);
            c.chemical_attack = c.chemical_attack * (1 + c.quality / 10.0);
            c.chemical_defense = c.chemical_defense * (1 + c.quality / 10.0);
            c.atomic_attack = c.atomic_attack * (1 + c.quality / 10.0);
            c.atomic_defense = c.atomic_defense * (1 + c.quality / 10.0);
            c.mental_attack = c.mental_attack * (1 + c.quality / 10.0);
            c.mental_defense = c.mental_defense * (1 + c.quality / 10.0);
            c.speed = c.speed * (1 + c.quality / 10.0);
            c.critical_damage_rate = c.critical_damage_rate * (1 + c.quality / 10.0);
            c.critical_rate = c.critical_rate * (1 + c.quality / 10.0);
            c.penetration_rate = c.penetration_rate * (1 + c.quality / 10.0);
            c.evasion_rate = c.evasion_rate * (1 + c.quality / 10.0);
            c.damage_absorption_rate = c.damage_absorption_rate * (1 + c.quality / 10.0);
            c.vitality_regeneration_rate = c.vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.accuracy_rate = c.accuracy_rate * (1 + c.quality / 10.0);
            c.lifesteal_rate = c.lifesteal_rate * (1 + c.quality / 10.0);
            c.shield_strength = c.shield_strength * (1 + c.quality / 10.0);
            c.tenacity = c.tenacity * (1 + c.quality / 10.0);
            c.resistance_rate = c.resistance_rate * (1 + c.quality / 10.0);
            c.combo_rate = c.combo_rate * (1 + c.quality / 10.0);
            c.reflection_rate = c.reflection_rate * (1 + c.quality / 10.0);
            c.mana = (float)(c.mana * (1 + c.quality / 10.0));
            c.mana_regeneration_rate = c.mana_regeneration_rate * (1 + c.quality / 10.0);
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<Symbols> GetQualityPower(List<Symbols> list)
    {
        foreach (var c in list)
        {
            c.health = c.health * (1 + c.quality / 10.0);
            c.physical_attack = c.physical_attack * (1 + c.quality / 10.0);
            c.physical_defense = c.physical_defense * (1 + c.quality / 10.0);
            c.magical_attack = c.magical_attack * (1 + c.quality / 10.0);
            c.magical_defense = c.magical_defense * (1 + c.quality / 10.0);
            c.chemical_attack = c.chemical_attack * (1 + c.quality / 10.0);
            c.chemical_defense = c.chemical_defense * (1 + c.quality / 10.0);
            c.atomic_attack = c.atomic_attack * (1 + c.quality / 10.0);
            c.atomic_defense = c.atomic_defense * (1 + c.quality / 10.0);
            c.mental_attack = c.mental_attack * (1 + c.quality / 10.0);
            c.mental_defense = c.mental_defense * (1 + c.quality / 10.0);
            c.speed = c.speed * (1 + c.quality / 10.0);
            c.critical_damage_rate = c.critical_damage_rate * (1 + c.quality / 10.0);
            c.critical_rate = c.critical_rate * (1 + c.quality / 10.0);
            c.penetration_rate = c.penetration_rate * (1 + c.quality / 10.0);
            c.evasion_rate = c.evasion_rate * (1 + c.quality / 10.0);
            c.damage_absorption_rate = c.damage_absorption_rate * (1 + c.quality / 10.0);
            c.vitality_regeneration_rate = c.vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.accuracy_rate = c.accuracy_rate * (1 + c.quality / 10.0);
            c.lifesteal_rate = c.lifesteal_rate * (1 + c.quality / 10.0);
            c.shield_strength = c.shield_strength * (1 + c.quality / 10.0);
            c.tenacity = c.tenacity * (1 + c.quality / 10.0);
            c.resistance_rate = c.resistance_rate * (1 + c.quality / 10.0);
            c.combo_rate = c.combo_rate * (1 + c.quality / 10.0);
            c.reflection_rate = c.reflection_rate * (1 + c.quality / 10.0);
            c.mana = (float)(c.mana * (1 + c.quality / 10.0));
            c.mana_regeneration_rate = c.mana_regeneration_rate * (1 + c.quality / 10.0);
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<Talisman> GetQualityPower(List<Talisman> list)
    {
        foreach (var c in list)
        {
            c.health = c.health * (1 + c.quality / 10.0);
            c.physical_attack = c.physical_attack * (1 + c.quality / 10.0);
            c.physical_defense = c.physical_defense * (1 + c.quality / 10.0);
            c.magical_attack = c.magical_attack * (1 + c.quality / 10.0);
            c.magical_defense = c.magical_defense * (1 + c.quality / 10.0);
            c.chemical_attack = c.chemical_attack * (1 + c.quality / 10.0);
            c.chemical_defense = c.chemical_defense * (1 + c.quality / 10.0);
            c.atomic_attack = c.atomic_attack * (1 + c.quality / 10.0);
            c.atomic_defense = c.atomic_defense * (1 + c.quality / 10.0);
            c.mental_attack = c.mental_attack * (1 + c.quality / 10.0);
            c.mental_defense = c.mental_defense * (1 + c.quality / 10.0);
            c.speed = c.speed * (1 + c.quality / 10.0);
            c.critical_damage_rate = c.critical_damage_rate * (1 + c.quality / 10.0);
            c.critical_rate = c.critical_rate * (1 + c.quality / 10.0);
            c.penetration_rate = c.penetration_rate * (1 + c.quality / 10.0);
            c.evasion_rate = c.evasion_rate * (1 + c.quality / 10.0);
            c.damage_absorption_rate = c.damage_absorption_rate * (1 + c.quality / 10.0);
            c.vitality_regeneration_rate = c.vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.accuracy_rate = c.accuracy_rate * (1 + c.quality / 10.0);
            c.lifesteal_rate = c.lifesteal_rate * (1 + c.quality / 10.0);
            c.shield_strength = c.shield_strength * (1 + c.quality / 10.0);
            c.tenacity = c.tenacity * (1 + c.quality / 10.0);
            c.resistance_rate = c.resistance_rate * (1 + c.quality / 10.0);
            c.combo_rate = c.combo_rate * (1 + c.quality / 10.0);
            c.reflection_rate = c.reflection_rate * (1 + c.quality / 10.0);
            c.mana = (float)(c.mana * (1 + c.quality / 10.0));
            c.mana_regeneration_rate = c.mana_regeneration_rate * (1 + c.quality / 10.0);
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return list;
    }
    public static List<Titles> GetQualityPower(List<Titles> list)
    {
        foreach (var c in list)
        {
            c.health = c.health * (1 + c.quality / 10.0);
            c.physical_attack = c.physical_attack * (1 + c.quality / 10.0);
            c.physical_defense = c.physical_defense * (1 + c.quality / 10.0);
            c.magical_attack = c.magical_attack * (1 + c.quality / 10.0);
            c.magical_defense = c.magical_defense * (1 + c.quality / 10.0);
            c.chemical_attack = c.chemical_attack * (1 + c.quality / 10.0);
            c.chemical_defense = c.chemical_defense * (1 + c.quality / 10.0);
            c.atomic_attack = c.atomic_attack * (1 + c.quality / 10.0);
            c.atomic_defense = c.atomic_defense * (1 + c.quality / 10.0);
            c.mental_attack = c.mental_attack * (1 + c.quality / 10.0);
            c.mental_defense = c.mental_defense * (1 + c.quality / 10.0);
            c.speed = c.speed * (1 + c.quality / 10.0);
            c.critical_damage_rate = c.critical_damage_rate * (1 + c.quality / 10.0);
            c.critical_rate = c.critical_rate * (1 + c.quality / 10.0);
            c.penetration_rate = c.penetration_rate * (1 + c.quality / 10.0);
            c.evasion_rate = c.evasion_rate * (1 + c.quality / 10.0);
            c.damage_absorption_rate = c.damage_absorption_rate * (1 + c.quality / 10.0);
            c.vitality_regeneration_rate = c.vitality_regeneration_rate * (1 + c.quality / 10.0);
            c.accuracy_rate = c.accuracy_rate * (1 + c.quality / 10.0);
            c.lifesteal_rate = c.lifesteal_rate * (1 + c.quality / 10.0);
            c.shield_strength = c.shield_strength * (1 + c.quality / 10.0);
            c.tenacity = c.tenacity * (1 + c.quality / 10.0);
            c.resistance_rate = c.resistance_rate * (1 + c.quality / 10.0);
            c.combo_rate = c.combo_rate * (1 + c.quality / 10.0);
            c.reflection_rate = c.reflection_rate * (1 + c.quality / 10.0);
            c.mana = (float)(c.mana * (1 + c.quality / 10.0));
            c.mana_regeneration_rate = c.mana_regeneration_rate * (1 + c.quality / 10.0);
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate * (1 + c.quality / 10.0);
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate * (1 + c.quality / 10.0);
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate * (1 + c.quality / 10.0);

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return list;
    }
}