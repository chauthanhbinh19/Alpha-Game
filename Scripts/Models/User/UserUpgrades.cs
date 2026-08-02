public class UserUpgrades : BaseEntity
{
    public string Id { get; set; }
    public int CurrentLevel { get; set; } =  0;
    public double CurrentMultiplier { get; set; } =  0;
    public double PercentAllHealth { get; set; }
    public double PercentAllPhysicalAttack { get; set; }
    public double PercentAllPhysicalDefense { get; set; }
    public double PercentAllMagicalAttack { get; set; }
    public double PercentAllMagicalDefense { get; set; }
    public double PercentAllChemicalAttack { get; set; }
    public double PercentAllChemicalDefense { get; set; }
    public double PercentAllAtomicAttack { get; set; }
    public double PercentAllAtomicDefense { get; set; }
    public double PercentAllMentalAttack { get; set; }
    public double PercentAllMentalDefense { get; set; }
    public UserUpgrades()
    {

    }
    public UserUpgrades CloneUserUpgrade(UserUpgrades source)
    {
        if (source == null)
            return null;

        return new UserUpgrades
        {
            Id = source.Id,
            CodeName = source.CodeName,
            AvailabilityType = source.AvailabilityType,
            Rarity = source.Rarity,
            Star = source.Star,
            Level = source.Level,
            Experience = source.Experience,
            Power = source.Power,
            Health = source.Health,
            PhysicalAttack = source.PhysicalAttack,
            PhysicalDefense = source.PhysicalDefense,
            MagicalAttack = source.MagicalAttack,
            MagicalDefense = source.MagicalDefense,
            ChemicalAttack = source.ChemicalAttack,
            ChemicalDefense = source.ChemicalDefense,
            AtomicAttack = source.AtomicAttack,
            AtomicDefense = source.AtomicDefense,
            MentalAttack = source.MentalAttack,
            MentalDefense = source.MentalDefense,
            Speed = source.Speed,
            CriticalDamageRate = source.CriticalDamageRate,
            CriticalRate = source.CriticalRate,
            CriticalResistanceRate = source.CriticalResistanceRate,
            IgnoreCriticalRate = source.IgnoreCriticalRate,
            PenetrationRate = source.PenetrationRate,
            PenetrationResistanceRate = source.PenetrationResistanceRate,
            EvasionRate = source.EvasionRate,
            DamageAbsorptionRate = source.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = source.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = source.AbsorbedDamageRate,
            VitalityRegenerationRate = source.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = source.VitalityRegenerationResistanceRate,
            AccuracyRate = source.AccuracyRate,
            LifestealRate = source.LifestealRate,
            Mana = source.Mana,
            ManaRegenerationRate = source.ManaRegenerationRate,
            ShieldStrength = source.ShieldStrength,
            Tenacity = source.Tenacity,
            ResistanceRate = source.ResistanceRate,
            ComboRate = source.ComboRate,
            IgnoreComboRate = source.IgnoreComboRate,
            ComboDamageRate = source.ComboDamageRate,
            ComboResistanceRate = source.ComboResistanceRate,
            StunRate = source.StunRate,
            IgnoreStunRate = source.IgnoreStunRate,
            ReflectionRate = source.ReflectionRate,
            IgnoreReflectionRate = source.IgnoreReflectionRate,
            ReflectionDamageRate = source.ReflectionDamageRate,
            ReflectionResistanceRate = source.ReflectionResistanceRate,
            DamageToDifferentFactionRate = source.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = source.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = source.DamageToSameFactionRate,
            ResistanceToSameFactionRate = source.ResistanceToSameFactionRate,
            NormalDamageRate = source.NormalDamageRate,
            NormalResistanceRate = source.NormalResistanceRate,
            SkillDamageRate = source.SkillDamageRate,
            SkillResistanceRate = source.SkillResistanceRate,
            PercentAllHealth = source.PercentAllHealth,
            PercentAllPhysicalAttack = source.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = source.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = source.PercentAllMagicalAttack,
            PercentAllMagicalDefense = source.PercentAllMagicalDefense,
            PercentAllChemicalAttack = source.PercentAllChemicalAttack,
            PercentAllChemicalDefense = source.PercentAllChemicalDefense,
            PercentAllAtomicAttack = source.PercentAllAtomicAttack,
            PercentAllAtomicDefense = source.PercentAllAtomicDefense,
            PercentAllMentalAttack = source.PercentAllMentalAttack,
            PercentAllMentalDefense = source.PercentAllMentalDefense
        };
    }
}
