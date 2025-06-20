using System.Collections.Generic;
public static class EvaluatePower
{
    public static double CalculatePower(
    double health, double physicalAttack, double physicalDefense, double magicalAttack, double magicalDefense,
    double chemicalAttack, double chemicalDefense, double atomicAttack, double atomicDefense, double mentalAttack, double mentalDefense,
    double speed, double criticalDamageRate, double criticalRate, double criticalResistanceRate, double ignoreCriticalRate,
    double penetrationRate, double penetrationResistanceRate, double evasionRate,
    double damageAbsorptionRate, double ignoreDamageAbsorptionRate, double absorbedDamageRate,
    double vitalityRegenerationRate, double vitalityRegenerationResistanceRate, double accuracyRate, double lifestealRate,
    double shieldStrength, double tenacity, double resistanceRate, double comboRate, double ignoreComboRate, double comboDamageRate, double comboResistanceRate,
    double stunRate, double ignoreStunRate, double reflectionRate, double ignoreReflectionRate, double reflectionDamageRate, double reflectionResistanceRate,
    double mana, double manaRegenerationRate,
    double damageToDifferentFactionRate, double resistanceToDifferentFactionRate,
    double damageToSameFactionRate, double resistanceToSameFactionRate,
    double normalDamageRate, double normalResistanceRate,
    double skillDamageRate, double skillResistanceRate
)
    {
        double weight = 0.5;

        double totalAttack = (physicalAttack + magicalAttack + chemicalAttack + atomicAttack + mentalAttack) * weight;
        double totalDefense = (physicalDefense + magicalDefense + chemicalDefense + atomicDefense + mentalDefense) * weight + shieldStrength * weight;

        // Điều chỉnh các chỉ số tỷ lệ
        double adjusted_critical_rate = (criticalRate / 100) * totalAttack / 100;
        double adjusted_critical_damage = (criticalDamageRate / 100) * totalAttack / 100;
        double adjusted_critical_resistance_rate = (criticalResistanceRate / 100) * totalDefense / 100;
        double adjusted_ignore_critical_rate = (ignoreCriticalRate / 100) * totalAttack / 100;
        double adjusted_penetration = (penetrationRate / 100) * totalAttack / 100;
        double adjusted_penetration_resistance_rate = (penetrationResistanceRate / 100) * totalDefense / 100;
        double adjusted_evasion = (evasionRate / 100) * totalDefense / 100;
        double adjusted_absorption = (damageAbsorptionRate / 100) * totalDefense / 100;
        double adjusted_ignore_damage_absorption_rate = (ignoreDamageAbsorptionRate / 100) * totalDefense / 100;
        double adjusted_absorbed_damage_rate = (absorbedDamageRate / 100) * totalDefense / 100;
        double adjusted_regeneration = (vitalityRegenerationRate / 100) * health / 100;
        double adjusted_vitality_regeneration_resistance_rate = (vitalityRegenerationResistanceRate / 100) * health / 100;
        double adjusted_accuracy = (accuracyRate / 100) * totalAttack / 100;
        double adjusted_lifesteal = (lifestealRate / 100) * totalAttack / 100;
        double adjusted_tenacity = (tenacity / 100) * totalDefense / 100;
        double adjusted_resistance = (resistanceRate / 100) * (totalDefense + health * 0.5) / 100;
        double adjusted_combo = (comboRate / 100) * totalAttack / 100;
        double adjusted_ignore_combo_rate = (ignoreComboRate / 100) * totalAttack / 100;
        double adjusted_combo_damage_rate = (comboDamageRate / 100) * totalAttack / 100;
        double adjusted_combo_resistance_rate = (comboResistanceRate / 100) * totalDefense / 100;
        double adjusted_stun_rate = (stunRate / 100) * totalAttack / 100;
        double adjusted_ignore_stun_rate = (ignoreStunRate / 100) * totalAttack / 100;
        double adjusted_reflection = (reflectionRate / 100) * (totalDefense + health * 0.5) / 100;
        double adjusted_ignore_reflection_rate = (ignoreReflectionRate / 100) * totalAttack / 100;
        double adjusted_reflection_damage_rate = (reflectionDamageRate / 100) * totalAttack / 100;
        double adjusted_reflection_resistance_rate = (reflectionResistanceRate / 100) * totalDefense / 100;

        // Điều chỉnh thuộc tính faction
        double adjustedDamageToDifferentFaction = (damageToDifferentFactionRate / 100) * totalAttack / 100;
        double adjustedResistanceToDifferentFaction = (resistanceToDifferentFactionRate / 100) * totalDefense / 100;
        double adjustedDamageToSameFaction = (damageToSameFactionRate / 100) * totalAttack / 100;
        double adjustedResistanceToSameFaction = (resistanceToSameFactionRate / 100) * totalDefense / 100;

        double adjustedNormalDamageRate = (normalDamageRate / 100) * totalAttack / 100;
        double adjustedNormalResistanceRate = (normalResistanceRate / 100) * totalDefense / 100;
        double adjustedSkillDamageRate = (skillDamageRate / 100) * totalAttack / 100;
        double adjustedSkillResistanceRate = (skillResistanceRate / 100) * totalDefense / 100;

        // Điều chỉnh mana
        double adjustedMana = mana * 0.5;
        double adjustedManaRegeneration = (manaRegenerationRate / 100) * mana;

        // Tổng sức mạnh
        return health * weight + totalAttack + totalDefense + speed * weight +
        adjusted_critical_rate + adjusted_critical_damage + adjusted_critical_resistance_rate + adjusted_ignore_critical_rate + 
        adjusted_penetration + adjusted_penetration_resistance_rate +
        adjusted_evasion + adjusted_absorption + adjusted_ignore_damage_absorption_rate + adjusted_absorbed_damage_rate + 
        adjusted_regeneration + adjusted_vitality_regeneration_resistance_rate + adjusted_accuracy +
        adjusted_lifesteal + adjusted_tenacity + adjusted_resistance + 
        adjusted_combo + adjusted_ignore_combo_rate + adjusted_combo_damage_rate + adjusted_combo_resistance_rate + 
        adjusted_stun_rate + adjusted_ignore_stun_rate + 
        adjusted_reflection + adjusted_ignore_reflection_rate + adjusted_reflection_damage_rate + adjusted_reflection_resistance_rate +
        adjustedDamageToDifferentFaction + adjustedResistanceToDifferentFaction +
        adjustedDamageToSameFaction + adjustedResistanceToSameFaction +
        adjustedMana + adjustedManaRegeneration + 
        adjustedNormalDamageRate + adjustedNormalResistanceRate +
        adjustedSkillDamageRate + adjustedSkillResistanceRate;
    }
}