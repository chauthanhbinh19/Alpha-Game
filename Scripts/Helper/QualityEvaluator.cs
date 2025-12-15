using System.Collections.Generic;
public static class QualityEvaluator
{
    public static int CheckQuality(string rare)
    {
        switch (rare)
        {
            case AppConstants.Rare.SR:
                return 2;
            case AppConstants.Rare.SSR:
                return 5;
            case AppConstants.Rare.UR:
                return 10;
            case AppConstants.Rare.LG:
                return 15;
            case AppConstants.Rare.LGPlus:
                return 20;
            case AppConstants.Rare.MR:
                return 25;
            case AppConstants.Rare.SLG:
                return 30;
            case AppConstants.Rare.SLGPlus:
                return 35;
            case AppConstants.Rare.SP:
                return 40;
            default:
                return 0;
        }
    }
    private static readonly Dictionary<string, int> qualityMap = new Dictionary<string, int>
    {
        { AppConstants.Rare.SR, 2 },
        { AppConstants.Rare.SSR, 5 },
        { AppConstants.Rare.UR, 10 },
        { AppConstants.Rare.LG, 15 },
        { AppConstants.Rare.LGPlus, 20 },
        { AppConstants.Rare.MR, 25 },
        { AppConstants.Rare.SLG, 30 },
        { AppConstants.Rare.SLGPlus, 35 },
        { AppConstants.Rare.SP, 40 },
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
        AppConstants.Rare.SR,
        AppConstants.Rare.SSR,
        AppConstants.Rare.UR,
        AppConstants.Rare.LG,
        AppConstants.Rare.LGPlus,
        AppConstants.Rare.MR,
        AppConstants.Rare.SLG,
        AppConstants.Rare.SLGPlus,
        AppConstants.Rare.SP
    };
    public static List<string> rarities = new List<string>
    {
        AppConstants.Rare.ALL,
        AppConstants.Rare.SR,
        AppConstants.Rare.SSR,
        AppConstants.Rare.UR,
        AppConstants.Rare.LG,
        AppConstants.Rare.LGPlus,
        AppConstants.Rare.MR,
        AppConstants.Rare.SLG,
        AppConstants.Rare.SLGPlus,
        AppConstants.Rare.SP
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
            double multiplier = 1 + c.Quality / 10.0;

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


            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Alchemies> GetQualityPower(List<Alchemies> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Avatars> GetQualityPower(List<Avatars> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Borders> GetQualityPower(List<Borders> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Books> GetQualityPower(List<Books> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<CardAdmirals> GetQualityPower(List<CardAdmirals> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<CardCaptains> GetQualityPower(List<CardCaptains> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<CardColonels> GetQualityPower(List<CardColonels> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<CardGenerals> GetQualityPower(List<CardGenerals> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<CardHeroes> GetQualityPower(List<CardHeroes> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<CardLives> GetQualityPower(List<CardLives> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<CardMilitaries> GetQualityPower(List<CardMilitaries> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<CardMonsters> GetQualityPower(List<CardMonsters> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<CardSpells> GetQualityPower(List<CardSpells> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<CollaborationEquipments> GetQualityPower(List<CollaborationEquipments> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Collaborations> GetQualityPower(List<Collaborations> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Equipments> GetQualityPower(List<Equipments> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Forges> GetQualityPower(List<Forges> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<MagicFormationCircles> GetQualityPower(List<MagicFormationCircles> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Medals> GetQualityPower(List<Medals> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Pets> GetQualityPower(List<Pets> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Puppets> GetQualityPower(List<Puppets> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Relics> GetQualityPower(List<Relics> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Skills> GetQualityPower(List<Skills> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Symbols> GetQualityPower(List<Symbols> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Talismans> GetQualityPower(List<Talismans> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Titles> GetQualityPower(List<Titles> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Artworks> GetQualityPower(List<Artworks> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<SpiritBeasts> GetQualityPower(List<SpiritBeasts> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<SpiritCards> GetQualityPower(List<SpiritCards> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Architectures> GetQualityPower(List<Architectures> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Technologies> GetQualityPower(List<Technologies> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Cards> GetQualityPower(List<Cards> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Vehicles> GetQualityPower(List<Vehicles> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Cores> GetQualityPower(List<Cores> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Weapons> GetQualityPower(List<Weapons> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Robots> GetQualityPower(List<Robots> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Badges> GetQualityPower(List<Badges> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<MechaBeasts> GetQualityPower(List<MechaBeasts> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Runes> GetQualityPower(List<Runes> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Worlds> GetQualityPower(List<Worlds> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Cities> GetQualityPower(List<Cities> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Bases> GetQualityPower(List<Bases> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Trains> GetQualityPower(List<Trains> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Employees> GetQualityPower(List<Employees> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Researchs> GetQualityPower(List<Researchs> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Furnitures> GetQualityPower(List<Furnitures> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Foods> GetQualityPower(List<Foods> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Beverages> GetQualityPower(List<Beverages> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Buildings> GetQualityPower(List<Buildings> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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
    public static List<Plants> GetQualityPower(List<Plants> list)
    {
        foreach (var c in list)
        {
            double multiplier = 1 + c.Quality / 10.0;

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

            c.Power = EvaluatePower.CalculatePower(
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