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
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;


            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<Alchemy> GetQualityPower(List<Alchemy> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<Avatars> GetQualityPower(List<Avatars> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<Borders> GetQualityPower(List<Borders> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<Books> GetQualityPower(List<Books> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.all_power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<CardAdmirals> GetQualityPower(List<CardAdmirals> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.all_power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<CardCaptains> GetQualityPower(List<CardCaptains> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.all_power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<CardColonels> GetQualityPower(List<CardColonels> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.all_power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<CardGenerals> GetQualityPower(List<CardGenerals> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.all_power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<CardHeroes> GetQualityPower(List<CardHeroes> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.all_power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<CardLife> GetQualityPower(List<CardLife> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<CardMilitary> GetQualityPower(List<CardMilitary> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.all_power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<CardMonsters> GetQualityPower(List<CardMonsters> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.all_power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<CardSpell> GetQualityPower(List<CardSpell> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.all_power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<CollaborationEquipment> GetQualityPower(List<CollaborationEquipment> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<Collaboration> GetQualityPower(List<Collaboration> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<Equipments> GetQualityPower(List<Equipments> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<Forge> GetQualityPower(List<Forge> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<MagicFormationCircle> GetQualityPower(List<MagicFormationCircle> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<Medals> GetQualityPower(List<Medals> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<Pets> GetQualityPower(List<Pets> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.all_power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<Puppet> GetQualityPower(List<Puppet> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<Relics> GetQualityPower(List<Relics> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<Skills> GetQualityPower(List<Skills> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<Symbols> GetQualityPower(List<Symbols> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<Talisman> GetQualityPower(List<Talisman> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
    public static List<Titles> GetQualityPower(List<Titles> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.quality / 10.0;

            c.health *= multiplier;
            c.physical_attack *= multiplier;
            c.physical_defense *= multiplier;
            c.magical_attack *= multiplier;
            c.magical_defense *= multiplier;
            c.chemical_attack *= multiplier;
            c.chemical_defense *= multiplier;
            c.atomic_attack *= multiplier;
            c.atomic_defense *= multiplier;
            c.mental_attack *= multiplier;
            c.mental_defense *= multiplier;
            c.speed *= multiplier;
            c.critical_damage_rate *= multiplier;
            c.critical_rate *= multiplier;
            c.critical_resistance_rate *= multiplier;
            c.ignore_critical_rate *= multiplier;
            c.penetration_rate *= multiplier;
            c.penetration_resistance_rate *= multiplier;
            c.evasion_rate *= multiplier;
            c.damage_absorption_rate *= multiplier;
            c.ignore_damage_absorption_rate *= multiplier;
            c.absorbed_damage_rate *= multiplier;
            c.vitality_regeneration_rate *= multiplier;
            c.vitality_regeneration_resistance_rate *= multiplier;
            c.accuracy_rate *= multiplier;
            c.lifesteal_rate *= multiplier;
            c.mana = (float)(c.mana * multiplier);
            c.mana_regeneration_rate *= multiplier;
            c.shield_strength *= multiplier;
            c.tenacity *= multiplier;
            c.resistance_rate *= multiplier;
            c.combo_rate *= multiplier;
            c.ignore_combo_rate *= multiplier;
            c.combo_damage_rate *= multiplier;
            c.combo_resistance_rate *= multiplier;
            c.stun_rate *= multiplier;
            c.ignore_stun_rate *= multiplier;
            c.reflection_rate *= multiplier;
            c.ignore_reflection_rate *= multiplier;
            c.reflection_damage_rate *= multiplier;
            c.reflection_resistance_rate *= multiplier;
            c.damage_to_different_faction_rate *= multiplier;
            c.resistance_to_different_faction_rate *= multiplier;
            c.damage_to_same_faction_rate *= multiplier;
            c.resistance_to_same_faction_rate *= multiplier;
            c.normal_damage_rate *= multiplier;
            c.normal_resistance_rate *= multiplier;
            c.skill_damage_rate *= multiplier;
            c.skill_resistance_rate *= multiplier;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return list;
    }
}