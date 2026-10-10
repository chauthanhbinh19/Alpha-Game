using System.Collections.Generic;
public static class QualityEvaluatorHelper
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
            case AppConstants.Rare.UR_PLUS:
                return 13;
            case AppConstants.Rare.LG:
                return 15;
            case AppConstants.Rare.LG_Plus:
                return 20;
            case AppConstants.Rare.MR:
                return 25;
            case AppConstants.Rare.MR_Plus:
                return 30;
            case AppConstants.Rare.SLG:
                return 35;
            case AppConstants.Rare.SLG_Plus:
                return 40;
            case AppConstants.Rare.SP:
                return 45;
            case AppConstants.Rare.SP_Plus:
                return 50;
            default:
                return 0;
        }
    }
    public static string CheckRareColor(string rare)
    {
        switch (rare)
        {
            case AppConstants.Rare.SR:
                return ColorConstants.Rare.SR_COLOR;
            case AppConstants.Rare.SSR:
                return ColorConstants.Rare.SSR_COLOR;
            case AppConstants.Rare.UR:
                return ColorConstants.Rare.UR_COLOR;
            case AppConstants.Rare.UR_PLUS:
                return ColorConstants.Rare.UR_PLUS_COLOR;
            case AppConstants.Rare.LG:
                return ColorConstants.Rare.LG_COLOR;
            case AppConstants.Rare.LG_Plus:
                return ColorConstants.Rare.LG_Plus_COLOR;
            case AppConstants.Rare.MR:
                return ColorConstants.Rare.MR_COLOR;
            case AppConstants.Rare.MR_Plus:
                return ColorConstants.Rare.MR_Plus_COLOR;
            case AppConstants.Rare.SLG:
                return ColorConstants.Rare.SLG_COLOR;
            case AppConstants.Rare.SLG_Plus:
                return ColorConstants.Rare.SLG_Plus_COLOR;
            case AppConstants.Rare.SP:
                return ColorConstants.Rare.SP_COLOR;
            case AppConstants.Rare.SP_Plus:
                return ColorConstants.Rare.SP_Plus_COLOR;
            default:
                return ColorConstants.Rare.SR_COLOR;
        }
    }
    private static readonly Dictionary<string, int> qualityMap = new Dictionary<string, int>
    {
        { AppConstants.Rare.SR, 2 },
        { AppConstants.Rare.SSR, 5 },
        { AppConstants.Rare.UR, 10 },
        { AppConstants.Rare.UR_PLUS, 13 },
        { AppConstants.Rare.LG, 15 },
        { AppConstants.Rare.LG_Plus, 20 },
        { AppConstants.Rare.MR, 25 },
        { AppConstants.Rare.MR_Plus, 30 },
        { AppConstants.Rare.SP, 35 },
        { AppConstants.Rare.SP_Plus, 40 },
        { AppConstants.Rare.SSP, 45 },
        { AppConstants.Rare.SSP_Plus, 50 },
        { AppConstants.Rare.SRP, 55 },
        { AppConstants.Rare.SRP_Plus, 60 },
        { AppConstants.Rare.SUP, 65 },
        { AppConstants.Rare.SUP_Plus, 70 },
        { AppConstants.Rare.SLG, 75 },
        { AppConstants.Rare.SLG_Plus, 80 },
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
        AppConstants.Rare.UR_PLUS,
        AppConstants.Rare.LG,
        AppConstants.Rare.LG_Plus,
        AppConstants.Rare.MR,
        AppConstants.Rare.MR_Plus,
        AppConstants.Rare.SP,
        AppConstants.Rare.SP_Plus,
        AppConstants.Rare.SSP,
        AppConstants.Rare.SSP_Plus,
        AppConstants.Rare.SRP,
        AppConstants.Rare.SRP_Plus,
        AppConstants.Rare.SUP,
        AppConstants.Rare.SUP_Plus,
        AppConstants.Rare.SLG,
        AppConstants.Rare.SLG_Plus
    };
    public static List<string> rarities = new List<string>
    {
        AppConstants.Rare.ALL,
        AppConstants.Rare.SR,
        AppConstants.Rare.SSR,
        AppConstants.Rare.UR,
        AppConstants.Rare.UR_PLUS,
        AppConstants.Rare.LG,
        AppConstants.Rare.LG_Plus,
        AppConstants.Rare.MR,
        AppConstants.Rare.MR_Plus,
        AppConstants.Rare.SP,
        AppConstants.Rare.SP_Plus,
        AppConstants.Rare.SSP,
        AppConstants.Rare.SSP_Plus,
        AppConstants.Rare.SRP,
        AppConstants.Rare.SRP_Plus,
        AppConstants.Rare.SUP,
        AppConstants.Rare.SUP_Plus,
        AppConstants.Rare.SLG,
        AppConstants.Rare.SLG_Plus
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
    public static List<T> GetQualityPower<T>(List<T> list) where T : IStats
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
    public static T GetQualityPower<T>(T entity) where T : IStats
    {
        if (entity == null) return default;

        double multiplier = 1 + entity.Quality / 10.0;

        entity.Health *= multiplier;
        entity.PhysicalAttack *= multiplier;
        entity.PhysicalDefense *= multiplier;
        entity.MagicalAttack *= multiplier;
        entity.MagicalDefense *= multiplier;
        entity.ChemicalAttack *= multiplier;
        entity.ChemicalDefense *= multiplier;
        entity.AtomicAttack *= multiplier;
        entity.AtomicDefense *= multiplier;
        entity.MentalAttack *= multiplier;
        entity.MentalDefense *= multiplier;
        entity.Speed *= multiplier;
        entity.CriticalDamageRate *= multiplier;
        entity.CriticalRate *= multiplier;
        entity.CriticalResistanceRate *= multiplier;
        entity.IgnoreCriticalRate *= multiplier;
        entity.PenetrationRate *= multiplier;
        entity.PenetrationResistanceRate *= multiplier;
        entity.EvasionRate *= multiplier;
        entity.DamageAbsorptionRate *= multiplier;
        entity.IgnoreDamageAbsorptionRate *= multiplier;
        entity.AbsorbedDamageRate *= multiplier;
        entity.VitalityRegenerationRate *= multiplier;
        entity.VitalityRegenerationResistanceRate *= multiplier;
        entity.AccuracyRate *= multiplier;
        entity.LifestealRate *= multiplier;
        entity.Mana = (float)(entity.Mana * multiplier);
        entity.ManaRegenerationRate *= multiplier;
        entity.ShieldStrength *= multiplier;
        entity.Tenacity *= multiplier;
        entity.ResistanceRate *= multiplier;
        entity.ComboRate *= multiplier;
        entity.IgnoreComboRate *= multiplier;
        entity.ComboDamageRate *= multiplier;
        entity.ComboResistanceRate *= multiplier;
        entity.StunRate *= multiplier;
        entity.IgnoreStunRate *= multiplier;
        entity.ReflectionRate *= multiplier;
        entity.IgnoreReflectionRate *= multiplier;
        entity.ReflectionDamageRate *= multiplier;
        entity.ReflectionResistanceRate *= multiplier;
        entity.DamageToDifferentFactionRate *= multiplier;
        entity.ResistanceToDifferentFactionRate *= multiplier;
        entity.DamageToSameFactionRate *= multiplier;
        entity.ResistanceToSameFactionRate *= multiplier;
        entity.NormalDamageRate *= multiplier;
        entity.NormalResistanceRate *= multiplier;
        entity.SkillDamageRate *= multiplier;
        entity.SkillResistanceRate *= multiplier;

        entity.Power = PowerHelper.CalculatePower(
            entity.Health,
            entity.PhysicalAttack, entity.PhysicalDefense,
            entity.MagicalAttack, entity.MagicalDefense,
            entity.ChemicalAttack, entity.ChemicalDefense,
            entity.AtomicAttack, entity.AtomicDefense,
            entity.MentalAttack, entity.MentalDefense,
            entity.Speed,
            entity.CriticalDamageRate, entity.CriticalRate, entity.CriticalResistanceRate, entity.IgnoreCriticalRate,
            entity.PenetrationRate, entity.PenetrationResistanceRate, entity.EvasionRate,
            entity.DamageAbsorptionRate, entity.IgnoreDamageAbsorptionRate, entity.AbsorbedDamageRate,
            entity.VitalityRegenerationRate, entity.VitalityRegenerationResistanceRate,
            entity.AccuracyRate, entity.LifestealRate,
            entity.ShieldStrength, entity.Tenacity, entity.ResistanceRate,
            entity.ComboRate, entity.IgnoreComboRate, entity.ComboDamageRate, entity.ComboResistanceRate,
            entity.StunRate, entity.IgnoreStunRate,
            entity.ReflectionRate, entity.IgnoreReflectionRate, entity.ReflectionDamageRate, entity.ReflectionResistanceRate,
            entity.Mana, entity.ManaRegenerationRate,
            entity.DamageToDifferentFactionRate, entity.ResistanceToDifferentFactionRate,
            entity.DamageToSameFactionRate, entity.ResistanceToSameFactionRate,
            entity.NormalDamageRate, entity.NormalResistanceRate,
            entity.SkillDamageRate, entity.SkillResistanceRate
        );

        return entity;
    }
}