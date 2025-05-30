using System.Collections.Generic;
public static class EvaluatePower
{
    public static double CalculatePower(
    double health, double physicalAttack, double physicalDefense, double magicalAttack, double magicalDefense,
    double chemicalAttack, double chemicalDefense, double atomicAttack, double atomicDefense, double mentalAttack, double mentalDefense,
    double speed, double criticalDamageRate, double criticalRate, double penetrationRate, double evasionRate,
    double damageAbsorptionRate, double vitalityRegenerationRate, double accuracyRate, double lifestealRate,
    double shieldStrength, double tenacity, double resistanceRate, double comboRate, double reflectionRate,
    double mana, double manaRegenerationRate,
    double damageToDifferentFactionRate, double resistanceToDifferentFactionRate,
    double damageToSameFactionRate, double resistanceToSameFactionRate
)
{
    double weight = 0.5;

    double totalAttack = (physicalAttack + magicalAttack + chemicalAttack + atomicAttack + mentalAttack) * weight;
    double totalDefense = (physicalDefense + magicalDefense + chemicalDefense + atomicDefense + mentalDefense) * weight + shieldStrength * weight;

    // Điều chỉnh các chỉ số tỷ lệ
    double adjustedCriticalRate = (criticalRate / 100) * totalAttack / 100;
    double adjustedCriticalDamage = (criticalDamageRate / 100) * totalAttack / 100;
    double adjustedPenetration = (penetrationRate / 100) * totalAttack / 100;
    double adjustedEvasion = (evasionRate / 100) * totalDefense / 100;
    double adjustedAbsorption = (damageAbsorptionRate / 100) * (totalDefense + health * 0.5) / 100;
    double adjustedRegeneration = (vitalityRegenerationRate / 100) * health / 100;
    double adjustedAccuracy = (accuracyRate / 100) * totalAttack / 100;
    double adjustedLifesteal = (lifestealRate / 100) * totalAttack / 100;
    double adjustedTenacity = (tenacity / 100) * totalDefense / 100;
    double adjustedResistance = (resistanceRate / 100) * (totalDefense + health * 0.5) / 100;
    double adjustedCombo = (comboRate / 100) * totalAttack / 100;
    double adjustedReflection = (reflectionRate / 100) * (totalDefense + health * 0.5) / 100;

    // Điều chỉnh thuộc tính faction
    double adjustedDamageToDifferentFaction = (damageToDifferentFactionRate / 100) * totalAttack / 100;
    double adjustedResistanceToDifferentFaction = (resistanceToDifferentFactionRate / 100) * totalDefense / 100;
    double adjustedDamageToSameFaction = (damageToSameFactionRate / 100) * totalAttack / 100;
    double adjustedResistanceToSameFaction = (resistanceToSameFactionRate / 100) * totalDefense / 100;

    // Điều chỉnh mana
    double adjustedMana = mana * 0.5;
    double adjustedManaRegeneration = (manaRegenerationRate / 100) * mana;

    // Tổng sức mạnh
    return health * weight + totalAttack + totalDefense + speed * weight +
           adjustedCriticalRate + adjustedCriticalDamage + adjustedPenetration +
           adjustedEvasion + adjustedAbsorption + adjustedRegeneration + adjustedAccuracy +
           adjustedLifesteal + adjustedTenacity + adjustedResistance + adjustedCombo + adjustedReflection +
           adjustedDamageToDifferentFaction + adjustedResistanceToDifferentFaction +
           adjustedDamageToSameFaction + adjustedResistanceToSameFaction +
           adjustedMana + adjustedManaRegeneration;
}
}