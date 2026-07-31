using System;
using System.Collections.Generic;
public static class LevelEvaluatorHelper
{
    public static List<T> GetLevelPower<T>(List<T> list) where T : IStats
    {
        foreach (var c in list)
        {
            int effectiveLevel = Math.Max(1, c.Level);
            double multiplier = 1 + effectiveLevel / 100.0;

            c.Health *= multiplier;
            c.PhysicalAttack *= multiplier;
            c.PhysicalDefense *= multiplier;
            c.MagicalAttack *= multiplier;
            c.MagicalDefense *= multiplier;
            c.ChemicalAttack *= multiplier;
            c.ChemicalDefense *= multiplier;
            c.AtomicAttack *= multiplier;
            c.AtomicDefense *= multiplier;
            c.MentalAttack *= multiplier;
            c.MentalDefense *= multiplier;
            c.Speed *= multiplier;
            c.CriticalDamageRate *= multiplier;
            c.CriticalRate *= multiplier;
            c.CriticalResistanceRate *= multiplier;
            c.IgnoreCriticalRate *= multiplier;
            c.PenetrationRate *= multiplier;
            c.PenetrationResistanceRate *= multiplier;
            c.EvasionRate *= multiplier;
            c.DamageAbsorptionRate *= multiplier;
            c.IgnoreDamageAbsorptionRate *= multiplier;
            c.AbsorbedDamageRate *= multiplier;
            c.VitalityRegenerationRate *= multiplier;
            c.VitalityRegenerationResistanceRate *= multiplier;
            c.AccuracyRate *= multiplier;
            c.LifestealRate *= multiplier;
            c.Mana = (float)(c.Mana * multiplier);
            c.ManaRegenerationRate *= multiplier;
            c.ShieldStrength *= multiplier;
            c.Tenacity *= multiplier;
            c.ResistanceRate *= multiplier;
            c.ComboRate *= multiplier;
            c.IgnoreComboRate *= multiplier;
            c.ComboDamageRate *= multiplier;
            c.ComboResistanceRate *= multiplier;
            c.StunRate *= multiplier;
            c.IgnoreStunRate *= multiplier;
            c.ReflectionRate *= multiplier;
            c.IgnoreReflectionRate *= multiplier;
            c.ReflectionDamageRate *= multiplier;
            c.ReflectionResistanceRate *= multiplier;
            c.DamageToDifferentFactionRate *= multiplier;
            c.ResistanceToDifferentFactionRate *= multiplier;
            c.DamageToSameFactionRate *= multiplier;
            c.ResistanceToSameFactionRate *= multiplier;
            c.NormalDamageRate *= multiplier;
            c.NormalResistanceRate *= multiplier;
            c.SkillDamageRate *= multiplier;
            c.SkillResistanceRate *= multiplier;

            c.Power = PowerHelper.CalculatePower(
            c.Health,
            c.PhysicalAttack, c.PhysicalDefense,
            c.MagicalAttack, c.MagicalDefense,
            c.ChemicalAttack, c.ChemicalDefense,
            c.AtomicAttack, c.AtomicDefense,
            c.MentalAttack, c.MentalDefense,
            c.Speed,
            c.CriticalDamageRate, c.CriticalRate, c.CriticalResistanceRate, c.IgnoreCriticalRate,
            c.PenetrationRate, c.PenetrationResistanceRate, c.EvasionRate,
            c.DamageAbsorptionRate, c.IgnoreDamageAbsorptionRate, c.AbsorbedDamageRate,
            c.VitalityRegenerationRate, c.VitalityRegenerationResistanceRate,
            c.AccuracyRate, c.LifestealRate,
            c.ShieldStrength, c.Tenacity, c.ResistanceRate,
            c.ComboRate, c.IgnoreComboRate, c.ComboDamageRate, c.ComboResistanceRate,
            c.StunRate, c.IgnoreStunRate,
            c.ReflectionRate, c.IgnoreReflectionRate, c.ReflectionDamageRate, c.ReflectionResistanceRate,
            c.Mana, c.ManaRegenerationRate,
            c.DamageToDifferentFactionRate, c.ResistanceToDifferentFactionRate,
            c.DamageToSameFactionRate, c.ResistanceToSameFactionRate,
            c.NormalDamageRate, c.NormalResistanceRate,
            c.SkillDamageRate, c.SkillResistanceRate
        );
        }
        return list;
    }
    public static T GetLevelPower<T>(T item) where T : IStats
    {
        if (item == null) return default;

        int effectiveLevel = Math.Max(1, item.Level);
        double multiplier = effectiveLevel;

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