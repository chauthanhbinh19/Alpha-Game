using System.Collections.Generic;
public static class LevelEvaluatorHelper
{
    public static List<T> GetLevelPower<T>(List<T> list) where T : IStats
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Level / 100.0;

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
}