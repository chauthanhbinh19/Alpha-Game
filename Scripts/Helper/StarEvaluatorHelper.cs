using System;
using System.Collections.Generic;
public static class StarEvaluatorHelper
{
    public static List<T> GetStarPower<T>(List<T> list) where T : IStats
    {
        foreach (var item in list)
        {
            //if level <= 0 do nothing, skip for this step
            if (item.Star <= 0) continue;

            double multiplier = item.Star;

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
    public static T GetStarPower<T>(T item) where T : IStats
    {
        if (item == null) return default;

        //if level <= 0 do nothing, skip for this step
        if (item.Star <= 0) return item;

        double multiplier = item.Star;

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
    public static List<T> GetStarGalleryPower<T>(List<T> list) where T : IStats
    {
        foreach (var item in list)
        {
            //if level <= 0 do nothing, skip for this step
            if (item.CurrentStar <= 0) continue;

            double multiplier = item.CurrentStar;

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

            item.PercentAllHealth *= multiplier;
            item.PercentAllPhysicalAttack *= multiplier;
            item.PercentAllPhysicalDefense *= multiplier;
            item.PercentAllMagicalAttack *= multiplier;
            item.PercentAllMagicalDefense *= multiplier;
            item.PercentAllChemicalAttack *= multiplier;
            item.PercentAllChemicalDefense *= multiplier;
            item.PercentAllAtomicAttack *= multiplier;
            item.PercentAllAtomicDefense *= multiplier;
            item.PercentAllMentalAttack *= multiplier;
            item.PercentAllMentalDefense *= multiplier;

            item.Power = PowerHelper.CalculatePower(
            item.Health * item.PercentAllHealth,
            item.PhysicalAttack * item.PercentAllPhysicalAttack, item.PhysicalDefense * item.PercentAllPhysicalDefense,
            item.MagicalAttack * item.PercentAllMagicalAttack, item.MagicalDefense * item.PercentAllMagicalDefense,
            item.ChemicalAttack * item.PercentAllChemicalAttack, item.ChemicalDefense * item.PercentAllChemicalDefense,
            item.AtomicAttack * item.PercentAllAtomicAttack, item.AtomicDefense * item.PercentAllAtomicDefense,
            item.MentalAttack * item.PercentAllMentalAttack, item.MentalDefense * item.PercentAllMentalDefense,
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
    public static T GetStarGalleryPower<T>(T item) where T : IStats
    {
        if (item == null) return default;

        //if level <= 0 do nothing, skip for this step
        if (item.CurrentStar <= 0) return item;

        double multiplier = item.CurrentStar;

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

        item.PercentAllHealth *= multiplier;
        item.PercentAllPhysicalAttack *= multiplier;
        item.PercentAllPhysicalDefense *= multiplier;
        item.PercentAllMagicalAttack *= multiplier;
        item.PercentAllMagicalDefense *= multiplier;
        item.PercentAllChemicalAttack *= multiplier;
        item.PercentAllChemicalDefense *= multiplier;
        item.PercentAllAtomicAttack *= multiplier;
        item.PercentAllAtomicDefense *= multiplier;
        item.PercentAllMentalAttack *= multiplier;
        item.PercentAllMentalDefense *= multiplier;

        item.Power = PowerHelper.CalculatePower(
            item.Health * item.PercentAllHealth,
            item.PhysicalAttack * item.PercentAllPhysicalAttack, item.PhysicalDefense * item.PercentAllPhysicalDefense,
            item.MagicalAttack * item.PercentAllMagicalAttack, item.MagicalDefense * item.PercentAllMagicalDefense,
            item.ChemicalAttack * item.PercentAllChemicalAttack, item.ChemicalDefense * item.PercentAllChemicalDefense,
            item.AtomicAttack * item.PercentAllAtomicAttack, item.AtomicDefense * item.PercentAllAtomicDefense,
            item.MentalAttack * item.PercentAllMentalAttack, item.MentalDefense * item.PercentAllMentalDefense,
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