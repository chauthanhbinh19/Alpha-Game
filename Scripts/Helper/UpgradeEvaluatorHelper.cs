using System;
using System.Collections.Generic;
public static class UpgradeEvaluatorHelper
{
    public static List<T> GetUpgradePower<T>(List<T> list) where T : IStats
    {
        foreach (var item in list)
        {
            double multiplier = 1 + item.UserUpgrades.CurrentMultiplier/100;

            item.Health *= multiplier;
            item.PhysicalAttack *= multiplier;
            item.PhysicalDefense *= multiplier;
            item.MagicalAttack *= multiplier;
            item.MagicalDefense *= multiplier;
            item.ChemicalAttack *= multiplier;
            item.ChemicalDefense *= multiplier;
            item.AtomicAttack *= multiplier;
            item.AtomicDefense *= multiplier;
            item.MentalAttack *= multiplier;
            item.MentalDefense *= multiplier;
            item.Speed *= multiplier;
            item.CriticalDamageRate *= multiplier;
            item.CriticalRate *= multiplier;
            item.CriticalResistanceRate *= multiplier;
            item.IgnoreCriticalRate *= multiplier;
            item.PenetrationRate *= multiplier;
            item.PenetrationResistanceRate *= multiplier;
            item.EvasionRate *= multiplier;
            item.DamageAbsorptionRate *= multiplier;
            item.IgnoreDamageAbsorptionRate *= multiplier;
            item.AbsorbedDamageRate *= multiplier;
            item.VitalityRegenerationRate *= multiplier;
            item.VitalityRegenerationResistanceRate *= multiplier;
            item.AccuracyRate *= multiplier;
            item.LifestealRate *= multiplier;
            item.Mana = (float)(item.Mana * multiplier);
            item.ManaRegenerationRate *= multiplier;
            item.ShieldStrength *= multiplier;
            item.Tenacity *= multiplier;
            item.ResistanceRate *= multiplier;
            item.ComboRate *= multiplier;
            item.IgnoreComboRate *= multiplier;
            item.ComboDamageRate *= multiplier;
            item.ComboResistanceRate *= multiplier;
            item.StunRate *= multiplier;
            item.IgnoreStunRate *= multiplier;
            item.ReflectionRate *= multiplier;
            item.IgnoreReflectionRate *= multiplier;
            item.ReflectionDamageRate *= multiplier;
            item.ReflectionResistanceRate *= multiplier;
            item.DamageToDifferentFactionRate *= multiplier;
            item.ResistanceToDifferentFactionRate *= multiplier;
            item.DamageToSameFactionRate *= multiplier;
            item.ResistanceToSameFactionRate *= multiplier;
            item.NormalDamageRate *= multiplier;
            item.NormalResistanceRate *= multiplier;
            item.SkillDamageRate *= multiplier;
            item.SkillResistanceRate *= multiplier;

            item.Power = PowerHelper.CalculatePower(
            item.Health,
            item.PhysicalAttack, item.PhysicalDefense,
            item.MagicalAttack, item.MagicalDefense,
            item.ChemicalAttack, item.ChemicalDefense,
            item.AtomicAttack, item.AtomicDefense,
            item.MentalAttack, item.MentalDefense,
            item.Speed,
            item.CriticalDamageRate, item.CriticalRate, item.CriticalResistanceRate, item.IgnoreCriticalRate,
            item.PenetrationRate, item.PenetrationResistanceRate, item.EvasionRate,
            item.DamageAbsorptionRate, item.IgnoreDamageAbsorptionRate, item.AbsorbedDamageRate,
            item.VitalityRegenerationRate, item.VitalityRegenerationResistanceRate,
            item.AccuracyRate, item.LifestealRate,
            item.ShieldStrength, item.Tenacity, item.ResistanceRate,
            item.ComboRate, item.IgnoreComboRate, item.ComboDamageRate, item.ComboResistanceRate,
            item.StunRate, item.IgnoreStunRate,
            item.ReflectionRate, item.IgnoreReflectionRate, item.ReflectionDamageRate, item.ReflectionResistanceRate,
            item.Mana, item.ManaRegenerationRate,
            item.DamageToDifferentFactionRate, item.ResistanceToDifferentFactionRate,
            item.DamageToSameFactionRate, item.ResistanceToSameFactionRate,
            item.NormalDamageRate, item.NormalResistanceRate,
            item.SkillDamageRate, item.SkillResistanceRate
        );
        }
        return list;
    }
    public static T GetUpgradePower<T>(T item) where T : IStats
    {
        if (item == null) return default;

        double multiplier = 1 + item.UserUpgrades.CurrentMultiplier/100;

        item.Health *= multiplier;
        item.PhysicalAttack *= multiplier;
        item.PhysicalDefense *= multiplier;
        item.MagicalAttack *= multiplier;
        item.MagicalDefense *= multiplier;
        item.ChemicalAttack *= multiplier;
        item.ChemicalDefense *= multiplier;
        item.AtomicAttack *= multiplier;
        item.AtomicDefense *= multiplier;
        item.MentalAttack *= multiplier;
        item.MentalDefense *= multiplier;
        item.Speed *= multiplier;
        item.CriticalDamageRate *= multiplier;
        item.CriticalRate *= multiplier;
        item.CriticalResistanceRate *= multiplier;
        item.IgnoreCriticalRate *= multiplier;
        item.PenetrationRate *= multiplier;
        item.PenetrationResistanceRate *= multiplier;
        item.EvasionRate *= multiplier;
        item.DamageAbsorptionRate *= multiplier;
        item.IgnoreDamageAbsorptionRate *= multiplier;
        item.AbsorbedDamageRate *= multiplier;
        item.VitalityRegenerationRate *= multiplier;
        item.VitalityRegenerationResistanceRate *= multiplier;
        item.AccuracyRate *= multiplier;
        item.LifestealRate *= multiplier;
        item.Mana = (float)(item.Mana * multiplier);
        item.ManaRegenerationRate *= multiplier;
        item.ShieldStrength *= multiplier;
        item.Tenacity *= multiplier;
        item.ResistanceRate *= multiplier;
        item.ComboRate *= multiplier;
        item.IgnoreComboRate *= multiplier;
        item.ComboDamageRate *= multiplier;
        item.ComboResistanceRate *= multiplier;
        item.StunRate *= multiplier;
        item.IgnoreStunRate *= multiplier;
        item.ReflectionRate *= multiplier;
        item.IgnoreReflectionRate *= multiplier;
        item.ReflectionDamageRate *= multiplier;
        item.ReflectionResistanceRate *= multiplier;
        item.DamageToDifferentFactionRate *= multiplier;
        item.ResistanceToDifferentFactionRate *= multiplier;
        item.DamageToSameFactionRate *= multiplier;
        item.ResistanceToSameFactionRate *= multiplier;
        item.NormalDamageRate *= multiplier;
        item.NormalResistanceRate *= multiplier;
        item.SkillDamageRate *= multiplier;
        item.SkillResistanceRate *= multiplier;

        item.Power = PowerHelper.CalculatePower(
            item.Health,
            item.PhysicalAttack, item.PhysicalDefense,
            item.MagicalAttack, item.MagicalDefense,
            item.ChemicalAttack, item.ChemicalDefense,
            item.AtomicAttack, item.AtomicDefense,
            item.MentalAttack, item.MentalDefense,
            item.Speed,
            item.CriticalDamageRate, item.CriticalRate, item.CriticalResistanceRate, item.IgnoreCriticalRate,
            item.PenetrationRate, item.PenetrationResistanceRate, item.EvasionRate,
            item.DamageAbsorptionRate, item.IgnoreDamageAbsorptionRate, item.AbsorbedDamageRate,
            item.VitalityRegenerationRate, item.VitalityRegenerationResistanceRate,
            item.AccuracyRate, item.LifestealRate,
            item.ShieldStrength, item.Tenacity, item.ResistanceRate,
            item.ComboRate, item.IgnoreComboRate, item.ComboDamageRate, item.ComboResistanceRate,
            item.StunRate, item.IgnoreStunRate,
            item.ReflectionRate, item.IgnoreReflectionRate, item.ReflectionDamageRate, item.ReflectionResistanceRate,
            item.Mana, item.ManaRegenerationRate,
            item.DamageToDifferentFactionRate, item.ResistanceToDifferentFactionRate,
            item.DamageToSameFactionRate, item.ResistanceToSameFactionRate,
            item.NormalDamageRate, item.NormalResistanceRate,
            item.SkillDamageRate, item.SkillResistanceRate
        );

        return item;
    }
}