public class PowerManager
{
    public double Power { get; set; } = 0;
    public double Health { get; set; } = 0;
    public double PhysicalAttack { get; set; } = 0;
    public double PhysicalDefense { get; set; } = 0;
    public double MagicalAttack { get; set; } = 0;
    public double MagicalDefense { get; set; } = 0;
    public double ChemicalAttack { get; set; } = 0;
    public double ChemicalDefense { get; set; } = 0;
    public double AtomicAttack { get; set; } = 0;
    public double AtomicDefense { get; set; } = 0;
    public double MentalAttack { get; set; } = 0;
    public double MentalDefense { get; set; } = 0;
    public double Speed { get; set; } = 0;
    public double CriticalDamageRate { get; set; } = 0;
    public double CriticalRate { get; set; } = 0;
    public double CriticalResistanceRate { get; set; } = 0;
    public double IgnoreCriticalRate { get; set; } = 0;
    public double PenetrationRate { get; set; } = 0;
    public double PenetrationResistanceRate { get; set; } = 0;
    public double EvasionRate { get; set; } = 0;
    public double DamageAbsorptionRate { get; set; } = 0;
    public double IgnoreDamageAbsorptionRate { get; set; } = 0;
    public double AbsorbedDamageRate { get; set; } = 0;
    public double VitalityRegenerationRate { get; set; } = 0;
    public double VitalityRegenerationResistanceRate { get; set; } = 0;
    public double AccuracyRate { get; set; } = 0;
    public double LifestealRate { get; set; } = 0;
    public double Mana { get; set; } = 0;
    public double ManaRegenerationRate { get; set; } = 0;
    public double ShieldStrength { get; set; } = 0;
    public double Tenacity { get; set; } = 0;
    public double ResistanceRate { get; set; } = 0;
    public double ComboRate { get; set; } = 0;
    public double IgnoreComboRate { get; set; } = 0;
    public double ComboDamageRate { get; set; } = 0;
    public double ComboResistanceRate { get; set; } = 0;
    public double StunRate { get; set; } = 0;
    public double IgnoreStunRate { get; set; } = 0;
    public double ReflectionRate { get; set; } = 0;
    public double IgnoreReflectionRate { get; set; } = 0;
    public double ReflectionDamageRate { get; set; } = 0;
    public double ReflectionResistanceRate { get; set; } = 0;
    public double DamageToDifferentFactionRate { get; set; } = 0;
    public double ResistanceToDifferentFactionRate { get; set; } = 0;
    public double DamageToSameFactionRate { get; set; } = 0;
    public double ResistanceToSameFactionRate { get; set; } = 0;
    public double NormalDamageRate { get; set; } = 0;
    public double NormalResistanceRate { get; set; } = 0;
    public double SkillDamageRate { get; set; } = 0;
    public double SkillResistanceRate { get; set; } = 0;
    public double PercentAllHealth { get; set; } = 0;
    public double PercentAllPhysicalAttack { get; set; } = 0;
    public double PercentAllPhysicalDefense { get; set; } = 0;
    public double PercentAllMagicalAttack { get; set; } = 0;
    public double PercentAllMagicalDefense { get; set; } = 0;
    public double PercentAllChemicalAttack { get; set; } = 0;
    public double PercentAllChemicalDefense { get; set; } = 0;
    public double PercentAllAtomicAttack { get; set; } = 0;
    public double PercentAllAtomicDefense { get; set; } = 0;
    public double PercentAllMentalAttack { get; set; } = 0;
    public double PercentAllMentalDefense { get; set; } = 0;
    public const double coefficient = 0.5;

    // Start is called before the first frame update
    public PowerManager()
    {

    }
    /// <summary>
    /// Toán tử CỘNG (+): Áp dụng một Delta (phần chênh lệch) vào Baseline (Chỉ số gốc).
    /// Công thức: NewTotal = Baseline + Delta
    /// </summary>
    public static PowerManager operator +(PowerManager baseline, PowerManager delta)
    {
        if (baseline == null && delta == null) return new PowerManager();
        if (baseline == null) return delta;
        if (delta == null) return baseline;

        return new PowerManager
        {
            // Base Stats
            Power = baseline.Power + delta.Power,
            Health = baseline.Health + delta.Health,
            PhysicalAttack = baseline.PhysicalAttack + delta.PhysicalAttack,
            PhysicalDefense = baseline.PhysicalDefense + delta.PhysicalDefense,
            MagicalAttack = baseline.MagicalAttack + delta.MagicalAttack,
            MagicalDefense = baseline.MagicalDefense + delta.MagicalDefense,
            ChemicalAttack = baseline.ChemicalAttack + delta.ChemicalAttack,
            ChemicalDefense = baseline.ChemicalDefense + delta.ChemicalDefense,
            AtomicAttack = baseline.AtomicAttack + delta.AtomicAttack,
            AtomicDefense = baseline.AtomicDefense + delta.AtomicDefense,
            MentalAttack = baseline.MentalAttack + delta.MentalAttack,
            MentalDefense = baseline.MentalDefense + delta.MentalDefense,

            // Rates & Speed
            Speed = baseline.Speed + delta.Speed,
            CriticalDamageRate = baseline.CriticalDamageRate + delta.CriticalDamageRate,
            CriticalRate = baseline.CriticalRate + delta.CriticalRate,
            PenetrationRate = baseline.PenetrationRate + delta.PenetrationRate,
            EvasionRate = baseline.EvasionRate + delta.EvasionRate,
            DamageAbsorptionRate = baseline.DamageAbsorptionRate + delta.DamageAbsorptionRate,
            VitalityRegenerationRate = baseline.VitalityRegenerationRate + delta.VitalityRegenerationRate,
            AccuracyRate = baseline.AccuracyRate + delta.AccuracyRate,
            LifestealRate = baseline.LifestealRate + delta.LifestealRate,
            ShieldStrength = baseline.ShieldStrength + delta.ShieldStrength,
            Tenacity = baseline.Tenacity + delta.Tenacity,
            ResistanceRate = baseline.ResistanceRate + delta.ResistanceRate,
            ComboRate = baseline.ComboRate + delta.ComboRate,
            ReflectionRate = baseline.ReflectionRate + delta.ReflectionRate,
            Mana = baseline.Mana + delta.Mana,
            ManaRegenerationRate = baseline.ManaRegenerationRate + delta.ManaRegenerationRate,

            // Faction Stats
            DamageToDifferentFactionRate = baseline.DamageToDifferentFactionRate + delta.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = baseline.ResistanceToDifferentFactionRate + delta.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = baseline.DamageToSameFactionRate + delta.DamageToSameFactionRate,
            ResistanceToSameFactionRate = baseline.ResistanceToSameFactionRate + delta.ResistanceToSameFactionRate,

            // Percent Buffs
            PercentAllHealth = baseline.PercentAllHealth + delta.PercentAllHealth,
            PercentAllPhysicalAttack = baseline.PercentAllPhysicalAttack + delta.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = baseline.PercentAllPhysicalDefense + delta.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = baseline.PercentAllMagicalAttack + delta.PercentAllMagicalAttack,
            PercentAllMagicalDefense = baseline.PercentAllMagicalDefense + delta.PercentAllMagicalDefense,
            PercentAllChemicalAttack = baseline.PercentAllChemicalAttack + delta.PercentAllChemicalAttack,
            PercentAllChemicalDefense = baseline.PercentAllChemicalDefense + delta.PercentAllChemicalDefense,
            PercentAllAtomicAttack = baseline.PercentAllAtomicAttack + delta.PercentAllAtomicAttack,
            PercentAllAtomicDefense = baseline.PercentAllAtomicDefense + delta.PercentAllAtomicDefense,
            PercentAllMentalAttack = baseline.PercentAllMentalAttack + delta.PercentAllMentalAttack,
            PercentAllMentalDefense = baseline.PercentAllMentalDefense + delta.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Toán tử TRỪ (-): Tính Delta (chênh lệch) giữa chỉ số MỚI và chỉ số CŨ.
    /// Công thức: Delta = NewStats - OldStats
    /// </summary>
    public static PowerManager operator -(PowerManager newStats, PowerManager oldStats)
    {
        if (newStats == null && oldStats == null) return new PowerManager();
        if (newStats == null) return oldStats;
        if (oldStats == null) return newStats;

        return new PowerManager
        {
            // Base Stats
            Power = newStats.Power - oldStats.Power,
            Health = newStats.Health - oldStats.Health,
            PhysicalAttack = newStats.PhysicalAttack - oldStats.PhysicalAttack,
            PhysicalDefense = newStats.PhysicalDefense - oldStats.PhysicalDefense,
            MagicalAttack = newStats.MagicalAttack - oldStats.MagicalAttack,
            MagicalDefense = newStats.MagicalDefense - oldStats.MagicalDefense,
            ChemicalAttack = newStats.ChemicalAttack - oldStats.ChemicalAttack,
            ChemicalDefense = newStats.ChemicalDefense - oldStats.ChemicalDefense,
            AtomicAttack = newStats.AtomicAttack - oldStats.AtomicAttack,
            AtomicDefense = newStats.AtomicDefense - oldStats.AtomicDefense,
            MentalAttack = newStats.MentalAttack - oldStats.MentalAttack,
            MentalDefense = newStats.MentalDefense - oldStats.MentalDefense,

            // Rates & Speed
            Speed = newStats.Speed - oldStats.Speed,
            CriticalDamageRate = newStats.CriticalDamageRate - oldStats.CriticalDamageRate,
            CriticalRate = newStats.CriticalRate - oldStats.CriticalRate,
            PenetrationRate = newStats.PenetrationRate - oldStats.PenetrationRate,
            EvasionRate = newStats.EvasionRate - oldStats.EvasionRate,
            DamageAbsorptionRate = newStats.DamageAbsorptionRate - oldStats.DamageAbsorptionRate,
            VitalityRegenerationRate = newStats.VitalityRegenerationRate - oldStats.VitalityRegenerationRate,
            AccuracyRate = newStats.AccuracyRate - oldStats.AccuracyRate,
            LifestealRate = newStats.LifestealRate - oldStats.LifestealRate,
            ShieldStrength = newStats.ShieldStrength - oldStats.ShieldStrength,
            Tenacity = newStats.Tenacity - oldStats.Tenacity,
            ResistanceRate = newStats.ResistanceRate - oldStats.ResistanceRate,
            ComboRate = newStats.ComboRate - oldStats.ComboRate,
            ReflectionRate = newStats.ReflectionRate - oldStats.ReflectionRate,
            Mana = newStats.Mana - oldStats.Mana,
            ManaRegenerationRate = newStats.ManaRegenerationRate - oldStats.ManaRegenerationRate,

            // Faction Stats
            DamageToDifferentFactionRate = newStats.DamageToDifferentFactionRate - oldStats.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = newStats.ResistanceToDifferentFactionRate - oldStats.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = newStats.DamageToSameFactionRate - oldStats.DamageToSameFactionRate,
            ResistanceToSameFactionRate = newStats.ResistanceToSameFactionRate - oldStats.ResistanceToSameFactionRate,

            // Percent Buffs
            PercentAllHealth = newStats.PercentAllHealth - oldStats.PercentAllHealth,
            PercentAllPhysicalAttack = newStats.PercentAllPhysicalAttack - oldStats.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = newStats.PercentAllPhysicalDefense - oldStats.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = newStats.PercentAllMagicalAttack - oldStats.PercentAllMagicalAttack,
            PercentAllMagicalDefense = newStats.PercentAllMagicalDefense - oldStats.PercentAllMagicalDefense,
            PercentAllChemicalAttack = newStats.PercentAllChemicalAttack - oldStats.PercentAllChemicalAttack,
            PercentAllChemicalDefense = newStats.PercentAllChemicalDefense - oldStats.PercentAllChemicalDefense,
            PercentAllAtomicAttack = newStats.PercentAllAtomicAttack - oldStats.PercentAllAtomicAttack,
            PercentAllAtomicDefense = newStats.PercentAllAtomicDefense - oldStats.PercentAllAtomicDefense,
            PercentAllMentalAttack = newStats.PercentAllMentalAttack - oldStats.PercentAllMentalAttack,
            PercentAllMentalDefense = newStats.PercentAllMentalDefense - oldStats.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Achievements to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Achievements achievement)
    {
        if (achievement == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = achievement.Power,
            Health = achievement.Health,
            PhysicalAttack = achievement.PhysicalAttack,
            PhysicalDefense = achievement.PhysicalDefense,
            MagicalAttack = achievement.MagicalAttack,
            MagicalDefense = achievement.MagicalDefense,
            ChemicalAttack = achievement.ChemicalAttack,
            ChemicalDefense = achievement.ChemicalDefense,
            AtomicAttack = achievement.AtomicAttack,
            AtomicDefense = achievement.AtomicDefense,
            MentalAttack = achievement.MentalAttack,
            MentalDefense = achievement.MentalDefense,

            // Rates & Combat Mechanics
            Speed = achievement.Speed,
            CriticalDamageRate = achievement.CriticalDamageRate,
            CriticalRate = achievement.CriticalRate,
            CriticalResistanceRate = achievement.CriticalResistanceRate,
            IgnoreCriticalRate = achievement.IgnoreCriticalRate,
            PenetrationRate = achievement.PenetrationRate,
            PenetrationResistanceRate = achievement.PenetrationResistanceRate,
            EvasionRate = achievement.EvasionRate,
            DamageAbsorptionRate = achievement.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = achievement.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = achievement.AbsorbedDamageRate,
            VitalityRegenerationRate = achievement.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = achievement.VitalityRegenerationResistanceRate,
            AccuracyRate = achievement.AccuracyRate,
            LifestealRate = achievement.LifestealRate,
            Mana = achievement.Mana,
            ManaRegenerationRate = achievement.ManaRegenerationRate,
            ShieldStrength = achievement.ShieldStrength,
            Tenacity = achievement.Tenacity,
            ResistanceRate = achievement.ResistanceRate,

            // Combo & Control
            ComboRate = achievement.ComboRate,
            IgnoreComboRate = achievement.IgnoreComboRate,
            ComboDamageRate = achievement.ComboDamageRate,
            ComboResistanceRate = achievement.ComboResistanceRate,
            StunRate = achievement.StunRate,
            IgnoreStunRate = achievement.IgnoreStunRate,

            // Reflection
            ReflectionRate = achievement.ReflectionRate,
            IgnoreReflectionRate = achievement.IgnoreReflectionRate,
            ReflectionDamageRate = achievement.ReflectionDamageRate,
            ReflectionResistanceRate = achievement.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = achievement.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = achievement.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = achievement.DamageToSameFactionRate,
            ResistanceToSameFactionRate = achievement.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = achievement.NormalDamageRate,
            NormalResistanceRate = achievement.NormalResistanceRate,
            SkillDamageRate = achievement.SkillDamageRate,
            SkillResistanceRate = achievement.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = achievement.PercentAllHealth,
            PercentAllPhysicalAttack = achievement.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = achievement.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = achievement.PercentAllMagicalAttack,
            PercentAllMagicalDefense = achievement.PercentAllMagicalDefense,
            PercentAllChemicalAttack = achievement.PercentAllChemicalAttack,
            PercentAllChemicalDefense = achievement.PercentAllChemicalDefense,
            PercentAllAtomicAttack = achievement.PercentAllAtomicAttack,
            PercentAllAtomicDefense = achievement.PercentAllAtomicDefense,
            PercentAllMentalAttack = achievement.PercentAllMentalAttack,
            PercentAllMentalDefense = achievement.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Alchemies to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Alchemies alchemy)
    {
        if (alchemy == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = alchemy.Power,
            Health = alchemy.Health,
            PhysicalAttack = alchemy.PhysicalAttack,
            PhysicalDefense = alchemy.PhysicalDefense,
            MagicalAttack = alchemy.MagicalAttack,
            MagicalDefense = alchemy.MagicalDefense,
            ChemicalAttack = alchemy.ChemicalAttack,
            ChemicalDefense = alchemy.ChemicalDefense,
            AtomicAttack = alchemy.AtomicAttack,
            AtomicDefense = alchemy.AtomicDefense,
            MentalAttack = alchemy.MentalAttack,
            MentalDefense = alchemy.MentalDefense,

            // Rates & Combat Mechanics
            Speed = alchemy.Speed,
            CriticalDamageRate = alchemy.CriticalDamageRate,
            CriticalRate = alchemy.CriticalRate,
            CriticalResistanceRate = alchemy.CriticalResistanceRate,
            IgnoreCriticalRate = alchemy.IgnoreCriticalRate,
            PenetrationRate = alchemy.PenetrationRate,
            PenetrationResistanceRate = alchemy.PenetrationResistanceRate,
            EvasionRate = alchemy.EvasionRate,
            DamageAbsorptionRate = alchemy.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = alchemy.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = alchemy.AbsorbedDamageRate,
            VitalityRegenerationRate = alchemy.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = alchemy.VitalityRegenerationResistanceRate,
            AccuracyRate = alchemy.AccuracyRate,
            LifestealRate = alchemy.LifestealRate,
            Mana = alchemy.Mana,
            ManaRegenerationRate = alchemy.ManaRegenerationRate,
            ShieldStrength = alchemy.ShieldStrength,
            Tenacity = alchemy.Tenacity,
            ResistanceRate = alchemy.ResistanceRate,

            // Combo & Control
            ComboRate = alchemy.ComboRate,
            IgnoreComboRate = alchemy.IgnoreComboRate,
            ComboDamageRate = alchemy.ComboDamageRate,
            ComboResistanceRate = alchemy.ComboResistanceRate,
            StunRate = alchemy.StunRate,
            IgnoreStunRate = alchemy.IgnoreStunRate,

            // Reflection
            ReflectionRate = alchemy.ReflectionRate,
            IgnoreReflectionRate = alchemy.IgnoreReflectionRate,
            ReflectionDamageRate = alchemy.ReflectionDamageRate,
            ReflectionResistanceRate = alchemy.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = alchemy.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = alchemy.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = alchemy.DamageToSameFactionRate,
            ResistanceToSameFactionRate = alchemy.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = alchemy.NormalDamageRate,
            NormalResistanceRate = alchemy.NormalResistanceRate,
            SkillDamageRate = alchemy.SkillDamageRate,
            SkillResistanceRate = alchemy.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = alchemy.PercentAllHealth,
            PercentAllPhysicalAttack = alchemy.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = alchemy.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = alchemy.PercentAllMagicalAttack,
            PercentAllMagicalDefense = alchemy.PercentAllMagicalDefense,
            PercentAllChemicalAttack = alchemy.PercentAllChemicalAttack,
            PercentAllChemicalDefense = alchemy.PercentAllChemicalDefense,
            PercentAllAtomicAttack = alchemy.PercentAllAtomicAttack,
            PercentAllAtomicDefense = alchemy.PercentAllAtomicDefense,
            PercentAllMentalAttack = alchemy.PercentAllMentalAttack,
            PercentAllMentalDefense = alchemy.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Architectures to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Architectures architecture)
    {
        if (architecture == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = architecture.Power,
            Health = architecture.Health,
            PhysicalAttack = architecture.PhysicalAttack,
            PhysicalDefense = architecture.PhysicalDefense,
            MagicalAttack = architecture.MagicalAttack,
            MagicalDefense = architecture.MagicalDefense,
            ChemicalAttack = architecture.ChemicalAttack,
            ChemicalDefense = architecture.ChemicalDefense,
            AtomicAttack = architecture.AtomicAttack,
            AtomicDefense = architecture.AtomicDefense,
            MentalAttack = architecture.MentalAttack,
            MentalDefense = architecture.MentalDefense,

            // Rates & Combat Mechanics
            Speed = architecture.Speed,
            CriticalDamageRate = architecture.CriticalDamageRate,
            CriticalRate = architecture.CriticalRate,
            CriticalResistanceRate = architecture.CriticalResistanceRate,
            IgnoreCriticalRate = architecture.IgnoreCriticalRate,
            PenetrationRate = architecture.PenetrationRate,
            PenetrationResistanceRate = architecture.PenetrationResistanceRate,
            EvasionRate = architecture.EvasionRate,
            DamageAbsorptionRate = architecture.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = architecture.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = architecture.AbsorbedDamageRate,
            VitalityRegenerationRate = architecture.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = architecture.VitalityRegenerationResistanceRate,
            AccuracyRate = architecture.AccuracyRate,
            LifestealRate = architecture.LifestealRate,
            Mana = architecture.Mana,
            ManaRegenerationRate = architecture.ManaRegenerationRate,
            ShieldStrength = architecture.ShieldStrength,
            Tenacity = architecture.Tenacity,
            ResistanceRate = architecture.ResistanceRate,

            // Combo & Control
            ComboRate = architecture.ComboRate,
            IgnoreComboRate = architecture.IgnoreComboRate,
            ComboDamageRate = architecture.ComboDamageRate,
            ComboResistanceRate = architecture.ComboResistanceRate,
            StunRate = architecture.StunRate,
            IgnoreStunRate = architecture.IgnoreStunRate,

            // Reflection
            ReflectionRate = architecture.ReflectionRate,
            IgnoreReflectionRate = architecture.IgnoreReflectionRate,
            ReflectionDamageRate = architecture.ReflectionDamageRate,
            ReflectionResistanceRate = architecture.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = architecture.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = architecture.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = architecture.DamageToSameFactionRate,
            ResistanceToSameFactionRate = architecture.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = architecture.NormalDamageRate,
            NormalResistanceRate = architecture.NormalResistanceRate,
            SkillDamageRate = architecture.SkillDamageRate,
            SkillResistanceRate = architecture.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = architecture.PercentAllHealth,
            PercentAllPhysicalAttack = architecture.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = architecture.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = architecture.PercentAllMagicalAttack,
            PercentAllMagicalDefense = architecture.PercentAllMagicalDefense,
            PercentAllChemicalAttack = architecture.PercentAllChemicalAttack,
            PercentAllChemicalDefense = architecture.PercentAllChemicalDefense,
            PercentAllAtomicAttack = architecture.PercentAllAtomicAttack,
            PercentAllAtomicDefense = architecture.PercentAllAtomicDefense,
            PercentAllMentalAttack = architecture.PercentAllMentalAttack,
            PercentAllMentalDefense = architecture.PercentAllMentalDefense
        };
    }
    
    /// <summary>
    /// Allows implicit type casting from Artifacts to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Artifacts artifact)
    {
        if (artifact == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = artifact.Power,
            Health = artifact.Health,
            PhysicalAttack = artifact.PhysicalAttack,
            PhysicalDefense = artifact.PhysicalDefense,
            MagicalAttack = artifact.MagicalAttack,
            MagicalDefense = artifact.MagicalDefense,
            ChemicalAttack = artifact.ChemicalAttack,
            ChemicalDefense = artifact.ChemicalDefense,
            AtomicAttack = artifact.AtomicAttack,
            AtomicDefense = artifact.AtomicDefense,
            MentalAttack = artifact.MentalAttack,
            MentalDefense = artifact.MentalDefense,

            // Rates & Combat Mechanics
            Speed = artifact.Speed,
            CriticalDamageRate = artifact.CriticalDamageRate,
            CriticalRate = artifact.CriticalRate,
            CriticalResistanceRate = artifact.CriticalResistanceRate,
            IgnoreCriticalRate = artifact.IgnoreCriticalRate,
            PenetrationRate = artifact.PenetrationRate,
            PenetrationResistanceRate = artifact.PenetrationResistanceRate,
            EvasionRate = artifact.EvasionRate,
            DamageAbsorptionRate = artifact.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = artifact.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = artifact.AbsorbedDamageRate,
            VitalityRegenerationRate = artifact.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = artifact.VitalityRegenerationResistanceRate,
            AccuracyRate = artifact.AccuracyRate,
            LifestealRate = artifact.LifestealRate,
            Mana = artifact.Mana,
            ManaRegenerationRate = artifact.ManaRegenerationRate,
            ShieldStrength = artifact.ShieldStrength,
            Tenacity = artifact.Tenacity,
            ResistanceRate = artifact.ResistanceRate,

            // Combo & Control
            ComboRate = artifact.ComboRate,
            IgnoreComboRate = artifact.IgnoreComboRate,
            ComboDamageRate = artifact.ComboDamageRate,
            ComboResistanceRate = artifact.ComboResistanceRate,
            StunRate = artifact.StunRate,
            IgnoreStunRate = artifact.IgnoreStunRate,

            // Reflection
            ReflectionRate = artifact.ReflectionRate,
            IgnoreReflectionRate = artifact.IgnoreReflectionRate,
            ReflectionDamageRate = artifact.ReflectionDamageRate,
            ReflectionResistanceRate = artifact.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = artifact.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = artifact.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = artifact.DamageToSameFactionRate,
            ResistanceToSameFactionRate = artifact.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = artifact.NormalDamageRate,
            NormalResistanceRate = artifact.NormalResistanceRate,
            SkillDamageRate = artifact.SkillDamageRate,
            SkillResistanceRate = artifact.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = artifact.PercentAllHealth,
            PercentAllPhysicalAttack = artifact.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = artifact.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = artifact.PercentAllMagicalAttack,
            PercentAllMagicalDefense = artifact.PercentAllMagicalDefense,
            PercentAllChemicalAttack = artifact.PercentAllChemicalAttack,
            PercentAllChemicalDefense = artifact.PercentAllChemicalDefense,
            PercentAllAtomicAttack = artifact.PercentAllAtomicAttack,
            PercentAllAtomicDefense = artifact.PercentAllAtomicDefense,
            PercentAllMentalAttack = artifact.PercentAllMentalAttack,
            PercentAllMentalDefense = artifact.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Artworks to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Artworks artwork)
    {
        if (artwork == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = artwork.Power,
            Health = artwork.Health,
            PhysicalAttack = artwork.PhysicalAttack,
            PhysicalDefense = artwork.PhysicalDefense,
            MagicalAttack = artwork.MagicalAttack,
            MagicalDefense = artwork.MagicalDefense,
            ChemicalAttack = artwork.ChemicalAttack,
            ChemicalDefense = artwork.ChemicalDefense,
            AtomicAttack = artwork.AtomicAttack,
            AtomicDefense = artwork.AtomicDefense,
            MentalAttack = artwork.MentalAttack,
            MentalDefense = artwork.MentalDefense,

            // Rates & Combat Mechanics
            Speed = artwork.Speed,
            CriticalDamageRate = artwork.CriticalDamageRate,
            CriticalRate = artwork.CriticalRate,
            CriticalResistanceRate = artwork.CriticalResistanceRate,
            IgnoreCriticalRate = artwork.IgnoreCriticalRate,
            PenetrationRate = artwork.PenetrationRate,
            PenetrationResistanceRate = artwork.PenetrationResistanceRate,
            EvasionRate = artwork.EvasionRate,
            DamageAbsorptionRate = artwork.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = artwork.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = artwork.AbsorbedDamageRate,
            VitalityRegenerationRate = artwork.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = artwork.VitalityRegenerationResistanceRate,
            AccuracyRate = artwork.AccuracyRate,
            LifestealRate = artwork.LifestealRate,
            Mana = artwork.Mana,
            ManaRegenerationRate = artwork.ManaRegenerationRate,
            ShieldStrength = artwork.ShieldStrength,
            Tenacity = artwork.Tenacity,
            ResistanceRate = artwork.ResistanceRate,

            // Combo & Control
            ComboRate = artwork.ComboRate,
            IgnoreComboRate = artwork.IgnoreComboRate,
            ComboDamageRate = artwork.ComboDamageRate,
            ComboResistanceRate = artwork.ComboResistanceRate,
            StunRate = artwork.StunRate,
            IgnoreStunRate = artwork.IgnoreStunRate,

            // Reflection
            ReflectionRate = artwork.ReflectionRate,
            IgnoreReflectionRate = artwork.IgnoreReflectionRate,
            ReflectionDamageRate = artwork.ReflectionDamageRate,
            ReflectionResistanceRate = artwork.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = artwork.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = artwork.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = artwork.DamageToSameFactionRate,
            ResistanceToSameFactionRate = artwork.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = artwork.NormalDamageRate,
            NormalResistanceRate = artwork.NormalResistanceRate,
            SkillDamageRate = artwork.SkillDamageRate,
            SkillResistanceRate = artwork.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = artwork.PercentAllHealth,
            PercentAllPhysicalAttack = artwork.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = artwork.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = artwork.PercentAllMagicalAttack,
            PercentAllMagicalDefense = artwork.PercentAllMagicalDefense,
            PercentAllChemicalAttack = artwork.PercentAllChemicalAttack,
            PercentAllChemicalDefense = artwork.PercentAllChemicalDefense,
            PercentAllAtomicAttack = artwork.PercentAllAtomicAttack,
            PercentAllAtomicDefense = artwork.PercentAllAtomicDefense,
            PercentAllMentalAttack = artwork.PercentAllMentalAttack,
            PercentAllMentalDefense = artwork.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Avatars to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Avatars avatar)
    {
        if (avatar == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = avatar.Power,
            Health = avatar.Health,
            PhysicalAttack = avatar.PhysicalAttack,
            PhysicalDefense = avatar.PhysicalDefense,
            MagicalAttack = avatar.MagicalAttack,
            MagicalDefense = avatar.MagicalDefense,
            ChemicalAttack = avatar.ChemicalAttack,
            ChemicalDefense = avatar.ChemicalDefense,
            AtomicAttack = avatar.AtomicAttack,
            AtomicDefense = avatar.AtomicDefense,
            MentalAttack = avatar.MentalAttack,
            MentalDefense = avatar.MentalDefense,

            // Rates & Combat Mechanics
            Speed = avatar.Speed,
            CriticalDamageRate = avatar.CriticalDamageRate,
            CriticalRate = avatar.CriticalRate,
            CriticalResistanceRate = avatar.CriticalResistanceRate,
            IgnoreCriticalRate = avatar.IgnoreCriticalRate,
            PenetrationRate = avatar.PenetrationRate,
            PenetrationResistanceRate = avatar.PenetrationResistanceRate,
            EvasionRate = avatar.EvasionRate,
            DamageAbsorptionRate = avatar.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = avatar.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = avatar.AbsorbedDamageRate,
            VitalityRegenerationRate = avatar.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = avatar.VitalityRegenerationResistanceRate,
            AccuracyRate = avatar.AccuracyRate,
            LifestealRate = avatar.LifestealRate,
            Mana = avatar.Mana,
            ManaRegenerationRate = avatar.ManaRegenerationRate,
            ShieldStrength = avatar.ShieldStrength,
            Tenacity = avatar.Tenacity,
            ResistanceRate = avatar.ResistanceRate,

            // Combo & Control
            ComboRate = avatar.ComboRate,
            IgnoreComboRate = avatar.IgnoreComboRate,
            ComboDamageRate = avatar.ComboDamageRate,
            ComboResistanceRate = avatar.ComboResistanceRate,
            StunRate = avatar.StunRate,
            IgnoreStunRate = avatar.IgnoreStunRate,

            // Reflection
            ReflectionRate = avatar.ReflectionRate,
            IgnoreReflectionRate = avatar.IgnoreReflectionRate,
            ReflectionDamageRate = avatar.ReflectionDamageRate,
            ReflectionResistanceRate = avatar.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = avatar.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = avatar.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = avatar.DamageToSameFactionRate,
            ResistanceToSameFactionRate = avatar.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = avatar.NormalDamageRate,
            NormalResistanceRate = avatar.NormalResistanceRate,
            SkillDamageRate = avatar.SkillDamageRate,
            SkillResistanceRate = avatar.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = avatar.PercentAllHealth,
            PercentAllPhysicalAttack = avatar.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = avatar.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = avatar.PercentAllMagicalAttack,
            PercentAllMagicalDefense = avatar.PercentAllMagicalDefense,
            PercentAllChemicalAttack = avatar.PercentAllChemicalAttack,
            PercentAllChemicalDefense = avatar.PercentAllChemicalDefense,
            PercentAllAtomicAttack = avatar.PercentAllAtomicAttack,
            PercentAllAtomicDefense = avatar.PercentAllAtomicDefense,
            PercentAllMentalAttack = avatar.PercentAllMentalAttack,
            PercentAllMentalDefense = avatar.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Badges to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Badges badge)
    {
        if (badge == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = badge.Power,
            Health = badge.Health,
            PhysicalAttack = badge.PhysicalAttack,
            PhysicalDefense = badge.PhysicalDefense,
            MagicalAttack = badge.MagicalAttack,
            MagicalDefense = badge.MagicalDefense,
            ChemicalAttack = badge.ChemicalAttack,
            ChemicalDefense = badge.ChemicalDefense,
            AtomicAttack = badge.AtomicAttack,
            AtomicDefense = badge.AtomicDefense,
            MentalAttack = badge.MentalAttack,
            MentalDefense = badge.MentalDefense,

            // Rates & Combat Mechanics
            Speed = badge.Speed,
            CriticalDamageRate = badge.CriticalDamageRate,
            CriticalRate = badge.CriticalRate,
            CriticalResistanceRate = badge.CriticalResistanceRate,
            IgnoreCriticalRate = badge.IgnoreCriticalRate,
            PenetrationRate = badge.PenetrationRate,
            PenetrationResistanceRate = badge.PenetrationResistanceRate,
            EvasionRate = badge.EvasionRate,
            DamageAbsorptionRate = badge.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = badge.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = badge.AbsorbedDamageRate,
            VitalityRegenerationRate = badge.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = badge.VitalityRegenerationResistanceRate,
            AccuracyRate = badge.AccuracyRate,
            LifestealRate = badge.LifestealRate,
            Mana = badge.Mana,
            ManaRegenerationRate = badge.ManaRegenerationRate,
            ShieldStrength = badge.ShieldStrength,
            Tenacity = badge.Tenacity,
            ResistanceRate = badge.ResistanceRate,

            // Combo & Control
            ComboRate = badge.ComboRate,
            IgnoreComboRate = badge.IgnoreComboRate,
            ComboDamageRate = badge.ComboDamageRate,
            ComboResistanceRate = badge.ComboResistanceRate,
            StunRate = badge.StunRate,
            IgnoreStunRate = badge.IgnoreStunRate,

            // Reflection
            ReflectionRate = badge.ReflectionRate,
            IgnoreReflectionRate = badge.IgnoreReflectionRate,
            ReflectionDamageRate = badge.ReflectionDamageRate,
            ReflectionResistanceRate = badge.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = badge.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = badge.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = badge.DamageToSameFactionRate,
            ResistanceToSameFactionRate = badge.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = badge.NormalDamageRate,
            NormalResistanceRate = badge.NormalResistanceRate,
            SkillDamageRate = badge.SkillDamageRate,
            SkillResistanceRate = badge.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = badge.PercentAllHealth,
            PercentAllPhysicalAttack = badge.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = badge.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = badge.PercentAllMagicalAttack,
            PercentAllMagicalDefense = badge.PercentAllMagicalDefense,
            PercentAllChemicalAttack = badge.PercentAllChemicalAttack,
            PercentAllChemicalDefense = badge.PercentAllChemicalDefense,
            PercentAllAtomicAttack = badge.PercentAllAtomicAttack,
            PercentAllAtomicDefense = badge.PercentAllAtomicDefense,
            PercentAllMentalAttack = badge.PercentAllMentalAttack,
            PercentAllMentalDefense = badge.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Beverages to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Beverages beverage)
    {
        if (beverage == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = beverage.Power,
            Health = beverage.Health,
            PhysicalAttack = beverage.PhysicalAttack,
            PhysicalDefense = beverage.PhysicalDefense,
            MagicalAttack = beverage.MagicalAttack,
            MagicalDefense = beverage.MagicalDefense,
            ChemicalAttack = beverage.ChemicalAttack,
            ChemicalDefense = beverage.ChemicalDefense,
            AtomicAttack = beverage.AtomicAttack,
            AtomicDefense = beverage.AtomicDefense,
            MentalAttack = beverage.MentalAttack,
            MentalDefense = beverage.MentalDefense,

            // Rates & Combat Mechanics
            Speed = beverage.Speed,
            CriticalDamageRate = beverage.CriticalDamageRate,
            CriticalRate = beverage.CriticalRate,
            CriticalResistanceRate = beverage.CriticalResistanceRate,
            IgnoreCriticalRate = beverage.IgnoreCriticalRate,
            PenetrationRate = beverage.PenetrationRate,
            PenetrationResistanceRate = beverage.PenetrationResistanceRate,
            EvasionRate = beverage.EvasionRate,
            DamageAbsorptionRate = beverage.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = beverage.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = beverage.AbsorbedDamageRate,
            VitalityRegenerationRate = beverage.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = beverage.VitalityRegenerationResistanceRate,
            AccuracyRate = beverage.AccuracyRate,
            LifestealRate = beverage.LifestealRate,
            Mana = beverage.Mana,
            ManaRegenerationRate = beverage.ManaRegenerationRate,
            ShieldStrength = beverage.ShieldStrength,
            Tenacity = beverage.Tenacity,
            ResistanceRate = beverage.ResistanceRate,

            // Combo & Control
            ComboRate = beverage.ComboRate,
            IgnoreComboRate = beverage.IgnoreComboRate,
            ComboDamageRate = beverage.ComboDamageRate,
            ComboResistanceRate = beverage.ComboResistanceRate,
            StunRate = beverage.StunRate,
            IgnoreStunRate = beverage.IgnoreStunRate,

            // Reflection
            ReflectionRate = beverage.ReflectionRate,
            IgnoreReflectionRate = beverage.IgnoreReflectionRate,
            ReflectionDamageRate = beverage.ReflectionDamageRate,
            ReflectionResistanceRate = beverage.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = beverage.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = beverage.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = beverage.DamageToSameFactionRate,
            ResistanceToSameFactionRate = beverage.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = beverage.NormalDamageRate,
            NormalResistanceRate = beverage.NormalResistanceRate,
            SkillDamageRate = beverage.SkillDamageRate,
            SkillResistanceRate = beverage.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = beverage.PercentAllHealth,
            PercentAllPhysicalAttack = beverage.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = beverage.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = beverage.PercentAllMagicalAttack,
            PercentAllMagicalDefense = beverage.PercentAllMagicalDefense,
            PercentAllChemicalAttack = beverage.PercentAllChemicalAttack,
            PercentAllChemicalDefense = beverage.PercentAllChemicalDefense,
            PercentAllAtomicAttack = beverage.PercentAllAtomicAttack,
            PercentAllAtomicDefense = beverage.PercentAllAtomicDefense,
            PercentAllMentalAttack = beverage.PercentAllMentalAttack,
            PercentAllMentalDefense = beverage.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Books to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Books book)
    {
        if (book == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = book.Power,
            Health = book.Health,
            PhysicalAttack = book.PhysicalAttack,
            PhysicalDefense = book.PhysicalDefense,
            MagicalAttack = book.MagicalAttack,
            MagicalDefense = book.MagicalDefense,
            ChemicalAttack = book.ChemicalAttack,
            ChemicalDefense = book.ChemicalDefense,
            AtomicAttack = book.AtomicAttack,
            AtomicDefense = book.AtomicDefense,
            MentalAttack = book.MentalAttack,
            MentalDefense = book.MentalDefense,

            // Rates & Combat Mechanics
            Speed = book.Speed,
            CriticalDamageRate = book.CriticalDamageRate,
            CriticalRate = book.CriticalRate,
            CriticalResistanceRate = book.CriticalResistanceRate,
            IgnoreCriticalRate = book.IgnoreCriticalRate,
            PenetrationRate = book.PenetrationRate,
            PenetrationResistanceRate = book.PenetrationResistanceRate,
            EvasionRate = book.EvasionRate,
            DamageAbsorptionRate = book.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = book.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = book.AbsorbedDamageRate,
            VitalityRegenerationRate = book.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = book.VitalityRegenerationResistanceRate,
            AccuracyRate = book.AccuracyRate,
            LifestealRate = book.LifestealRate,
            Mana = book.Mana,
            ManaRegenerationRate = book.ManaRegenerationRate,
            ShieldStrength = book.ShieldStrength,
            Tenacity = book.Tenacity,
            ResistanceRate = book.ResistanceRate,

            // Combo & Control
            ComboRate = book.ComboRate,
            IgnoreComboRate = book.IgnoreComboRate,
            ComboDamageRate = book.ComboDamageRate,
            ComboResistanceRate = book.ComboResistanceRate,
            StunRate = book.StunRate,
            IgnoreStunRate = book.IgnoreStunRate,

            // Reflection
            ReflectionRate = book.ReflectionRate,
            IgnoreReflectionRate = book.IgnoreReflectionRate,
            ReflectionDamageRate = book.ReflectionDamageRate,
            ReflectionResistanceRate = book.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = book.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = book.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = book.DamageToSameFactionRate,
            ResistanceToSameFactionRate = book.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = book.NormalDamageRate,
            NormalResistanceRate = book.NormalResistanceRate,
            SkillDamageRate = book.SkillDamageRate,
            SkillResistanceRate = book.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = book.PercentAllHealth,
            PercentAllPhysicalAttack = book.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = book.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = book.PercentAllMagicalAttack,
            PercentAllMagicalDefense = book.PercentAllMagicalDefense,
            PercentAllChemicalAttack = book.PercentAllChemicalAttack,
            PercentAllChemicalDefense = book.PercentAllChemicalDefense,
            PercentAllAtomicAttack = book.PercentAllAtomicAttack,
            PercentAllAtomicDefense = book.PercentAllAtomicDefense,
            PercentAllMentalAttack = book.PercentAllMentalAttack,
            PercentAllMentalDefense = book.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Borders to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Borders border)
    {
        if (border == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = border.Power,
            Health = border.Health,
            PhysicalAttack = border.PhysicalAttack,
            PhysicalDefense = border.PhysicalDefense,
            MagicalAttack = border.MagicalAttack,
            MagicalDefense = border.MagicalDefense,
            ChemicalAttack = border.ChemicalAttack,
            ChemicalDefense = border.ChemicalDefense,
            AtomicAttack = border.AtomicAttack,
            AtomicDefense = border.AtomicDefense,
            MentalAttack = border.MentalAttack,
            MentalDefense = border.MentalDefense,

            // Rates & Combat Mechanics
            Speed = border.Speed,
            CriticalDamageRate = border.CriticalDamageRate,
            CriticalRate = border.CriticalRate,
            CriticalResistanceRate = border.CriticalResistanceRate,
            IgnoreCriticalRate = border.IgnoreCriticalRate,
            PenetrationRate = border.PenetrationRate,
            PenetrationResistanceRate = border.PenetrationResistanceRate,
            EvasionRate = border.EvasionRate,
            DamageAbsorptionRate = border.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = border.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = border.AbsorbedDamageRate,
            VitalityRegenerationRate = border.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = border.VitalityRegenerationResistanceRate,
            AccuracyRate = border.AccuracyRate,
            LifestealRate = border.LifestealRate,
            Mana = border.Mana,
            ManaRegenerationRate = border.ManaRegenerationRate,
            ShieldStrength = border.ShieldStrength,
            Tenacity = border.Tenacity,
            ResistanceRate = border.ResistanceRate,

            // Combo & Control
            ComboRate = border.ComboRate,
            IgnoreComboRate = border.IgnoreComboRate,
            ComboDamageRate = border.ComboDamageRate,
            ComboResistanceRate = border.ComboResistanceRate,
            StunRate = border.StunRate,
            IgnoreStunRate = border.IgnoreStunRate,

            // Reflection
            ReflectionRate = border.ReflectionRate,
            IgnoreReflectionRate = border.IgnoreReflectionRate,
            ReflectionDamageRate = border.ReflectionDamageRate,
            ReflectionResistanceRate = border.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = border.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = border.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = border.DamageToSameFactionRate,
            ResistanceToSameFactionRate = border.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = border.NormalDamageRate,
            NormalResistanceRate = border.NormalResistanceRate,
            SkillDamageRate = border.SkillDamageRate,
            SkillResistanceRate = border.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = border.PercentAllHealth,
            PercentAllPhysicalAttack = border.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = border.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = border.PercentAllMagicalAttack,
            PercentAllMagicalDefense = border.PercentAllMagicalDefense,
            PercentAllChemicalAttack = border.PercentAllChemicalAttack,
            PercentAllChemicalDefense = border.PercentAllChemicalDefense,
            PercentAllAtomicAttack = border.PercentAllAtomicAttack,
            PercentAllAtomicDefense = border.PercentAllAtomicDefense,
            PercentAllMentalAttack = border.PercentAllMentalAttack,
            PercentAllMentalDefense = border.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Buildings to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Buildings building)
    {
        if (building == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = building.Power,
            Health = building.Health,
            PhysicalAttack = building.PhysicalAttack,
            PhysicalDefense = building.PhysicalDefense,
            MagicalAttack = building.MagicalAttack,
            MagicalDefense = building.MagicalDefense,
            ChemicalAttack = building.ChemicalAttack,
            ChemicalDefense = building.ChemicalDefense,
            AtomicAttack = building.AtomicAttack,
            AtomicDefense = building.AtomicDefense,
            MentalAttack = building.MentalAttack,
            MentalDefense = building.MentalDefense,

            // Rates & Combat Mechanics
            Speed = building.Speed,
            CriticalDamageRate = building.CriticalDamageRate,
            CriticalRate = building.CriticalRate,
            CriticalResistanceRate = building.CriticalResistanceRate,
            IgnoreCriticalRate = building.IgnoreCriticalRate,
            PenetrationRate = building.PenetrationRate,
            PenetrationResistanceRate = building.PenetrationResistanceRate,
            EvasionRate = building.EvasionRate,
            DamageAbsorptionRate = building.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = building.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = building.AbsorbedDamageRate,
            VitalityRegenerationRate = building.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = building.VitalityRegenerationResistanceRate,
            AccuracyRate = building.AccuracyRate,
            LifestealRate = building.LifestealRate,
            Mana = building.Mana,
            ManaRegenerationRate = building.ManaRegenerationRate,
            ShieldStrength = building.ShieldStrength,
            Tenacity = building.Tenacity,
            ResistanceRate = building.ResistanceRate,

            // Combo & Control
            ComboRate = building.ComboRate,
            IgnoreComboRate = building.IgnoreComboRate,
            ComboDamageRate = building.ComboDamageRate,
            ComboResistanceRate = building.ComboResistanceRate,
            StunRate = building.StunRate,
            IgnoreStunRate = building.IgnoreStunRate,

            // Reflection
            ReflectionRate = building.ReflectionRate,
            IgnoreReflectionRate = building.IgnoreReflectionRate,
            ReflectionDamageRate = building.ReflectionDamageRate,
            ReflectionResistanceRate = building.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = building.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = building.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = building.DamageToSameFactionRate,
            ResistanceToSameFactionRate = building.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = building.NormalDamageRate,
            NormalResistanceRate = building.NormalResistanceRate,
            SkillDamageRate = building.SkillDamageRate,
            SkillResistanceRate = building.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = building.PercentAllHealth,
            PercentAllPhysicalAttack = building.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = building.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = building.PercentAllMagicalAttack,
            PercentAllMagicalDefense = building.PercentAllMagicalDefense,
            PercentAllChemicalAttack = building.PercentAllChemicalAttack,
            PercentAllChemicalDefense = building.PercentAllChemicalDefense,
            PercentAllAtomicAttack = building.PercentAllAtomicAttack,
            PercentAllAtomicDefense = building.PercentAllAtomicDefense,
            PercentAllMentalAttack = building.PercentAllMentalAttack,
            PercentAllMentalDefense = building.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from CardAdmirals to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(CardAdmirals cardAdmiral)
    {
        if (cardAdmiral == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = cardAdmiral.Power,
            Health = cardAdmiral.Health,
            PhysicalAttack = cardAdmiral.PhysicalAttack,
            PhysicalDefense = cardAdmiral.PhysicalDefense,
            MagicalAttack = cardAdmiral.MagicalAttack,
            MagicalDefense = cardAdmiral.MagicalDefense,
            ChemicalAttack = cardAdmiral.ChemicalAttack,
            ChemicalDefense = cardAdmiral.ChemicalDefense,
            AtomicAttack = cardAdmiral.AtomicAttack,
            AtomicDefense = cardAdmiral.AtomicDefense,
            MentalAttack = cardAdmiral.MentalAttack,
            MentalDefense = cardAdmiral.MentalDefense,

            // Rates & Combat Mechanics
            Speed = cardAdmiral.Speed,
            CriticalDamageRate = cardAdmiral.CriticalDamageRate,
            CriticalRate = cardAdmiral.CriticalRate,
            CriticalResistanceRate = cardAdmiral.CriticalResistanceRate,
            IgnoreCriticalRate = cardAdmiral.IgnoreCriticalRate,
            PenetrationRate = cardAdmiral.PenetrationRate,
            PenetrationResistanceRate = cardAdmiral.PenetrationResistanceRate,
            EvasionRate = cardAdmiral.EvasionRate,
            DamageAbsorptionRate = cardAdmiral.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = cardAdmiral.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = cardAdmiral.AbsorbedDamageRate,
            VitalityRegenerationRate = cardAdmiral.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = cardAdmiral.VitalityRegenerationResistanceRate,
            AccuracyRate = cardAdmiral.AccuracyRate,
            LifestealRate = cardAdmiral.LifestealRate,
            Mana = cardAdmiral.Mana,
            ManaRegenerationRate = cardAdmiral.ManaRegenerationRate,
            ShieldStrength = cardAdmiral.ShieldStrength,
            Tenacity = cardAdmiral.Tenacity,
            ResistanceRate = cardAdmiral.ResistanceRate,

            // Combo & Control
            ComboRate = cardAdmiral.ComboRate,
            IgnoreComboRate = cardAdmiral.IgnoreComboRate,
            ComboDamageRate = cardAdmiral.ComboDamageRate,
            ComboResistanceRate = cardAdmiral.ComboResistanceRate,
            StunRate = cardAdmiral.StunRate,
            IgnoreStunRate = cardAdmiral.IgnoreStunRate,

            // Reflection
            ReflectionRate = cardAdmiral.ReflectionRate,
            IgnoreReflectionRate = cardAdmiral.IgnoreReflectionRate,
            ReflectionDamageRate = cardAdmiral.ReflectionDamageRate,
            ReflectionResistanceRate = cardAdmiral.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = cardAdmiral.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = cardAdmiral.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = cardAdmiral.DamageToSameFactionRate,
            ResistanceToSameFactionRate = cardAdmiral.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = cardAdmiral.NormalDamageRate,
            NormalResistanceRate = cardAdmiral.NormalResistanceRate,
            SkillDamageRate = cardAdmiral.SkillDamageRate,
            SkillResistanceRate = cardAdmiral.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = cardAdmiral.PercentAllHealth,
            PercentAllPhysicalAttack = cardAdmiral.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = cardAdmiral.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = cardAdmiral.PercentAllMagicalAttack,
            PercentAllMagicalDefense = cardAdmiral.PercentAllMagicalDefense,
            PercentAllChemicalAttack = cardAdmiral.PercentAllChemicalAttack,
            PercentAllChemicalDefense = cardAdmiral.PercentAllChemicalDefense,
            PercentAllAtomicAttack = cardAdmiral.PercentAllAtomicAttack,
            PercentAllAtomicDefense = cardAdmiral.PercentAllAtomicDefense,
            PercentAllMentalAttack = cardAdmiral.PercentAllMentalAttack,
            PercentAllMentalDefense = cardAdmiral.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from CardCaptains to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(CardCaptains cardCaptain)
    {
        if (cardCaptain == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = cardCaptain.Power,
            Health = cardCaptain.Health,
            PhysicalAttack = cardCaptain.PhysicalAttack,
            PhysicalDefense = cardCaptain.PhysicalDefense,
            MagicalAttack = cardCaptain.MagicalAttack,
            MagicalDefense = cardCaptain.MagicalDefense,
            ChemicalAttack = cardCaptain.ChemicalAttack,
            ChemicalDefense = cardCaptain.ChemicalDefense,
            AtomicAttack = cardCaptain.AtomicAttack,
            AtomicDefense = cardCaptain.AtomicDefense,
            MentalAttack = cardCaptain.MentalAttack,
            MentalDefense = cardCaptain.MentalDefense,

            // Rates & Combat Mechanics
            Speed = cardCaptain.Speed,
            CriticalDamageRate = cardCaptain.CriticalDamageRate,
            CriticalRate = cardCaptain.CriticalRate,
            CriticalResistanceRate = cardCaptain.CriticalResistanceRate,
            IgnoreCriticalRate = cardCaptain.IgnoreCriticalRate,
            PenetrationRate = cardCaptain.PenetrationRate,
            PenetrationResistanceRate = cardCaptain.PenetrationResistanceRate,
            EvasionRate = cardCaptain.EvasionRate,
            DamageAbsorptionRate = cardCaptain.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = cardCaptain.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = cardCaptain.AbsorbedDamageRate,
            VitalityRegenerationRate = cardCaptain.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = cardCaptain.VitalityRegenerationResistanceRate,
            AccuracyRate = cardCaptain.AccuracyRate,
            LifestealRate = cardCaptain.LifestealRate,
            Mana = cardCaptain.Mana,
            ManaRegenerationRate = cardCaptain.ManaRegenerationRate,
            ShieldStrength = cardCaptain.ShieldStrength,
            Tenacity = cardCaptain.Tenacity,
            ResistanceRate = cardCaptain.ResistanceRate,

            // Combo & Control
            ComboRate = cardCaptain.ComboRate,
            IgnoreComboRate = cardCaptain.IgnoreComboRate,
            ComboDamageRate = cardCaptain.ComboDamageRate,
            ComboResistanceRate = cardCaptain.ComboResistanceRate,
            StunRate = cardCaptain.StunRate,
            IgnoreStunRate = cardCaptain.IgnoreStunRate,

            // Reflection
            ReflectionRate = cardCaptain.ReflectionRate,
            IgnoreReflectionRate = cardCaptain.IgnoreReflectionRate,
            ReflectionDamageRate = cardCaptain.ReflectionDamageRate,
            ReflectionResistanceRate = cardCaptain.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = cardCaptain.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = cardCaptain.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = cardCaptain.DamageToSameFactionRate,
            ResistanceToSameFactionRate = cardCaptain.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = cardCaptain.NormalDamageRate,
            NormalResistanceRate = cardCaptain.NormalResistanceRate,
            SkillDamageRate = cardCaptain.SkillDamageRate,
            SkillResistanceRate = cardCaptain.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = cardCaptain.PercentAllHealth,
            PercentAllPhysicalAttack = cardCaptain.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = cardCaptain.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = cardCaptain.PercentAllMagicalAttack,
            PercentAllMagicalDefense = cardCaptain.PercentAllMagicalDefense,
            PercentAllChemicalAttack = cardCaptain.PercentAllChemicalAttack,
            PercentAllChemicalDefense = cardCaptain.PercentAllChemicalDefense,
            PercentAllAtomicAttack = cardCaptain.PercentAllAtomicAttack,
            PercentAllAtomicDefense = cardCaptain.PercentAllAtomicDefense,
            PercentAllMentalAttack = cardCaptain.PercentAllMentalAttack,
            PercentAllMentalDefense = cardCaptain.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from CardColonels to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(CardColonels cardColonel)
    {
        if (cardColonel == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = cardColonel.Power,
            Health = cardColonel.Health,
            PhysicalAttack = cardColonel.PhysicalAttack,
            PhysicalDefense = cardColonel.PhysicalDefense,
            MagicalAttack = cardColonel.MagicalAttack,
            MagicalDefense = cardColonel.MagicalDefense,
            ChemicalAttack = cardColonel.ChemicalAttack,
            ChemicalDefense = cardColonel.ChemicalDefense,
            AtomicAttack = cardColonel.AtomicAttack,
            AtomicDefense = cardColonel.AtomicDefense,
            MentalAttack = cardColonel.MentalAttack,
            MentalDefense = cardColonel.MentalDefense,

            // Rates & Combat Mechanics
            Speed = cardColonel.Speed,
            CriticalDamageRate = cardColonel.CriticalDamageRate,
            CriticalRate = cardColonel.CriticalRate,
            CriticalResistanceRate = cardColonel.CriticalResistanceRate,
            IgnoreCriticalRate = cardColonel.IgnoreCriticalRate,
            PenetrationRate = cardColonel.PenetrationRate,
            PenetrationResistanceRate = cardColonel.PenetrationResistanceRate,
            EvasionRate = cardColonel.EvasionRate,
            DamageAbsorptionRate = cardColonel.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = cardColonel.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = cardColonel.AbsorbedDamageRate,
            VitalityRegenerationRate = cardColonel.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = cardColonel.VitalityRegenerationResistanceRate,
            AccuracyRate = cardColonel.AccuracyRate,
            LifestealRate = cardColonel.LifestealRate,
            Mana = cardColonel.Mana,
            ManaRegenerationRate = cardColonel.ManaRegenerationRate,
            ShieldStrength = cardColonel.ShieldStrength,
            Tenacity = cardColonel.Tenacity,
            ResistanceRate = cardColonel.ResistanceRate,

            // Combo & Control
            ComboRate = cardColonel.ComboRate,
            IgnoreComboRate = cardColonel.IgnoreComboRate,
            ComboDamageRate = cardColonel.ComboDamageRate,
            ComboResistanceRate = cardColonel.ComboResistanceRate,
            StunRate = cardColonel.StunRate,
            IgnoreStunRate = cardColonel.IgnoreStunRate,

            // Reflection
            ReflectionRate = cardColonel.ReflectionRate,
            IgnoreReflectionRate = cardColonel.IgnoreReflectionRate,
            ReflectionDamageRate = cardColonel.ReflectionDamageRate,
            ReflectionResistanceRate = cardColonel.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = cardColonel.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = cardColonel.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = cardColonel.DamageToSameFactionRate,
            ResistanceToSameFactionRate = cardColonel.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = cardColonel.NormalDamageRate,
            NormalResistanceRate = cardColonel.NormalResistanceRate,
            SkillDamageRate = cardColonel.SkillDamageRate,
            SkillResistanceRate = cardColonel.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = cardColonel.PercentAllHealth,
            PercentAllPhysicalAttack = cardColonel.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = cardColonel.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = cardColonel.PercentAllMagicalAttack,
            PercentAllMagicalDefense = cardColonel.PercentAllMagicalDefense,
            PercentAllChemicalAttack = cardColonel.PercentAllChemicalAttack,
            PercentAllChemicalDefense = cardColonel.PercentAllChemicalDefense,
            PercentAllAtomicAttack = cardColonel.PercentAllAtomicAttack,
            PercentAllAtomicDefense = cardColonel.PercentAllAtomicDefense,
            PercentAllMentalAttack = cardColonel.PercentAllMentalAttack,
            PercentAllMentalDefense = cardColonel.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from CardGenerals to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(CardGenerals cardGeneral)
    {
        if (cardGeneral == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = cardGeneral.Power,
            Health = cardGeneral.Health,
            PhysicalAttack = cardGeneral.PhysicalAttack,
            PhysicalDefense = cardGeneral.PhysicalDefense,
            MagicalAttack = cardGeneral.MagicalAttack,
            MagicalDefense = cardGeneral.MagicalDefense,
            ChemicalAttack = cardGeneral.ChemicalAttack,
            ChemicalDefense = cardGeneral.ChemicalDefense,
            AtomicAttack = cardGeneral.AtomicAttack,
            AtomicDefense = cardGeneral.AtomicDefense,
            MentalAttack = cardGeneral.MentalAttack,
            MentalDefense = cardGeneral.MentalDefense,

            // Rates & Combat Mechanics
            Speed = cardGeneral.Speed,
            CriticalDamageRate = cardGeneral.CriticalDamageRate,
            CriticalRate = cardGeneral.CriticalRate,
            CriticalResistanceRate = cardGeneral.CriticalResistanceRate,
            IgnoreCriticalRate = cardGeneral.IgnoreCriticalRate,
            PenetrationRate = cardGeneral.PenetrationRate,
            PenetrationResistanceRate = cardGeneral.PenetrationResistanceRate,
            EvasionRate = cardGeneral.EvasionRate,
            DamageAbsorptionRate = cardGeneral.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = cardGeneral.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = cardGeneral.AbsorbedDamageRate,
            VitalityRegenerationRate = cardGeneral.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = cardGeneral.VitalityRegenerationResistanceRate,
            AccuracyRate = cardGeneral.AccuracyRate,
            LifestealRate = cardGeneral.LifestealRate,
            Mana = cardGeneral.Mana,
            ManaRegenerationRate = cardGeneral.ManaRegenerationRate,
            ShieldStrength = cardGeneral.ShieldStrength,
            Tenacity = cardGeneral.Tenacity,
            ResistanceRate = cardGeneral.ResistanceRate,

            // Combo & Control
            ComboRate = cardGeneral.ComboRate,
            IgnoreComboRate = cardGeneral.IgnoreComboRate,
            ComboDamageRate = cardGeneral.ComboDamageRate,
            ComboResistanceRate = cardGeneral.ComboResistanceRate,
            StunRate = cardGeneral.StunRate,
            IgnoreStunRate = cardGeneral.IgnoreStunRate,

            // Reflection
            ReflectionRate = cardGeneral.ReflectionRate,
            IgnoreReflectionRate = cardGeneral.IgnoreReflectionRate,
            ReflectionDamageRate = cardGeneral.ReflectionDamageRate,
            ReflectionResistanceRate = cardGeneral.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = cardGeneral.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = cardGeneral.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = cardGeneral.DamageToSameFactionRate,
            ResistanceToSameFactionRate = cardGeneral.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = cardGeneral.NormalDamageRate,
            NormalResistanceRate = cardGeneral.NormalResistanceRate,
            SkillDamageRate = cardGeneral.SkillDamageRate,
            SkillResistanceRate = cardGeneral.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = cardGeneral.PercentAllHealth,
            PercentAllPhysicalAttack = cardGeneral.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = cardGeneral.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = cardGeneral.PercentAllMagicalAttack,
            PercentAllMagicalDefense = cardGeneral.PercentAllMagicalDefense,
            PercentAllChemicalAttack = cardGeneral.PercentAllChemicalAttack,
            PercentAllChemicalDefense = cardGeneral.PercentAllChemicalDefense,
            PercentAllAtomicAttack = cardGeneral.PercentAllAtomicAttack,
            PercentAllAtomicDefense = cardGeneral.PercentAllAtomicDefense,
            PercentAllMentalAttack = cardGeneral.PercentAllMentalAttack,
            PercentAllMentalDefense = cardGeneral.PercentAllMentalDefense
        };
    }
    
    /// <summary>
    /// Allows implicit type casting from CardHeroes to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(CardHeroes cardHero)
    {
        if (cardHero == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = cardHero.Power,
            Health = cardHero.Health,
            PhysicalAttack = cardHero.PhysicalAttack,
            PhysicalDefense = cardHero.PhysicalDefense,
            MagicalAttack = cardHero.MagicalAttack,
            MagicalDefense = cardHero.MagicalDefense,
            ChemicalAttack = cardHero.ChemicalAttack,
            ChemicalDefense = cardHero.ChemicalDefense,
            AtomicAttack = cardHero.AtomicAttack,
            AtomicDefense = cardHero.AtomicDefense,
            MentalAttack = cardHero.MentalAttack,
            MentalDefense = cardHero.MentalDefense,

            // Rates & Combat Mechanics
            Speed = cardHero.Speed,
            CriticalDamageRate = cardHero.CriticalDamageRate,
            CriticalRate = cardHero.CriticalRate,
            CriticalResistanceRate = cardHero.CriticalResistanceRate,
            IgnoreCriticalRate = cardHero.IgnoreCriticalRate,
            PenetrationRate = cardHero.PenetrationRate,
            PenetrationResistanceRate = cardHero.PenetrationResistanceRate,
            EvasionRate = cardHero.EvasionRate,
            DamageAbsorptionRate = cardHero.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = cardHero.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = cardHero.AbsorbedDamageRate,
            VitalityRegenerationRate = cardHero.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = cardHero.VitalityRegenerationResistanceRate,
            AccuracyRate = cardHero.AccuracyRate,
            LifestealRate = cardHero.LifestealRate,
            Mana = cardHero.Mana,
            ManaRegenerationRate = cardHero.ManaRegenerationRate,
            ShieldStrength = cardHero.ShieldStrength,
            Tenacity = cardHero.Tenacity,
            ResistanceRate = cardHero.ResistanceRate,

            // Combo & Control
            ComboRate = cardHero.ComboRate,
            IgnoreComboRate = cardHero.IgnoreComboRate,
            ComboDamageRate = cardHero.ComboDamageRate,
            ComboResistanceRate = cardHero.ComboResistanceRate,
            StunRate = cardHero.StunRate,
            IgnoreStunRate = cardHero.IgnoreStunRate,

            // Reflection
            ReflectionRate = cardHero.ReflectionRate,
            IgnoreReflectionRate = cardHero.IgnoreReflectionRate,
            ReflectionDamageRate = cardHero.ReflectionDamageRate,
            ReflectionResistanceRate = cardHero.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = cardHero.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = cardHero.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = cardHero.DamageToSameFactionRate,
            ResistanceToSameFactionRate = cardHero.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = cardHero.NormalDamageRate,
            NormalResistanceRate = cardHero.NormalResistanceRate,
            SkillDamageRate = cardHero.SkillDamageRate,
            SkillResistanceRate = cardHero.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = cardHero.PercentAllHealth,
            PercentAllPhysicalAttack = cardHero.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = cardHero.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = cardHero.PercentAllMagicalAttack,
            PercentAllMagicalDefense = cardHero.PercentAllMagicalDefense,
            PercentAllChemicalAttack = cardHero.PercentAllChemicalAttack,
            PercentAllChemicalDefense = cardHero.PercentAllChemicalDefense,
            PercentAllAtomicAttack = cardHero.PercentAllAtomicAttack,
            PercentAllAtomicDefense = cardHero.PercentAllAtomicDefense,
            PercentAllMentalAttack = cardHero.PercentAllMentalAttack,
            PercentAllMentalDefense = cardHero.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from CardLives to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(CardLives cardLife)
    {
        if (cardLife == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = cardLife.Power,
            Health = cardLife.Health,
            PhysicalAttack = cardLife.PhysicalAttack,
            PhysicalDefense = cardLife.PhysicalDefense,
            MagicalAttack = cardLife.MagicalAttack,
            MagicalDefense = cardLife.MagicalDefense,
            ChemicalAttack = cardLife.ChemicalAttack,
            ChemicalDefense = cardLife.ChemicalDefense,
            AtomicAttack = cardLife.AtomicAttack,
            AtomicDefense = cardLife.AtomicDefense,
            MentalAttack = cardLife.MentalAttack,
            MentalDefense = cardLife.MentalDefense,

            // Rates & Combat Mechanics
            Speed = cardLife.Speed,
            CriticalDamageRate = cardLife.CriticalDamageRate,
            CriticalRate = cardLife.CriticalRate,
            CriticalResistanceRate = cardLife.CriticalResistanceRate,
            IgnoreCriticalRate = cardLife.IgnoreCriticalRate,
            PenetrationRate = cardLife.PenetrationRate,
            PenetrationResistanceRate = cardLife.PenetrationResistanceRate,
            EvasionRate = cardLife.EvasionRate,
            DamageAbsorptionRate = cardLife.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = cardLife.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = cardLife.AbsorbedDamageRate,
            VitalityRegenerationRate = cardLife.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = cardLife.VitalityRegenerationResistanceRate,
            AccuracyRate = cardLife.AccuracyRate,
            LifestealRate = cardLife.LifestealRate,
            Mana = cardLife.Mana,
            ManaRegenerationRate = cardLife.ManaRegenerationRate,
            ShieldStrength = cardLife.ShieldStrength,
            Tenacity = cardLife.Tenacity,
            ResistanceRate = cardLife.ResistanceRate,

            // Combo & Control
            ComboRate = cardLife.ComboRate,
            IgnoreComboRate = cardLife.IgnoreComboRate,
            ComboDamageRate = cardLife.ComboDamageRate,
            ComboResistanceRate = cardLife.ComboResistanceRate,
            StunRate = cardLife.StunRate,
            IgnoreStunRate = cardLife.IgnoreStunRate,

            // Reflection
            ReflectionRate = cardLife.ReflectionRate,
            IgnoreReflectionRate = cardLife.IgnoreReflectionRate,
            ReflectionDamageRate = cardLife.ReflectionDamageRate,
            ReflectionResistanceRate = cardLife.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = cardLife.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = cardLife.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = cardLife.DamageToSameFactionRate,
            ResistanceToSameFactionRate = cardLife.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = cardLife.NormalDamageRate,
            NormalResistanceRate = cardLife.NormalResistanceRate,
            SkillDamageRate = cardLife.SkillDamageRate,
            SkillResistanceRate = cardLife.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = cardLife.PercentAllHealth,
            PercentAllPhysicalAttack = cardLife.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = cardLife.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = cardLife.PercentAllMagicalAttack,
            PercentAllMagicalDefense = cardLife.PercentAllMagicalDefense,
            PercentAllChemicalAttack = cardLife.PercentAllChemicalAttack,
            PercentAllChemicalDefense = cardLife.PercentAllChemicalDefense,
            PercentAllAtomicAttack = cardLife.PercentAllAtomicAttack,
            PercentAllAtomicDefense = cardLife.PercentAllAtomicDefense,
            PercentAllMentalAttack = cardLife.PercentAllMentalAttack,
            PercentAllMentalDefense = cardLife.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from CardMilitaries to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(CardMilitaries cardMilitary)
    {
        if (cardMilitary == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = cardMilitary.Power,
            Health = cardMilitary.Health,
            PhysicalAttack = cardMilitary.PhysicalAttack,
            PhysicalDefense = cardMilitary.PhysicalDefense,
            MagicalAttack = cardMilitary.MagicalAttack,
            MagicalDefense = cardMilitary.MagicalDefense,
            ChemicalAttack = cardMilitary.ChemicalAttack,
            ChemicalDefense = cardMilitary.ChemicalDefense,
            AtomicAttack = cardMilitary.AtomicAttack,
            AtomicDefense = cardMilitary.AtomicDefense,
            MentalAttack = cardMilitary.MentalAttack,
            MentalDefense = cardMilitary.MentalDefense,

            // Rates & Combat Mechanics
            Speed = cardMilitary.Speed,
            CriticalDamageRate = cardMilitary.CriticalDamageRate,
            CriticalRate = cardMilitary.CriticalRate,
            CriticalResistanceRate = cardMilitary.CriticalResistanceRate,
            IgnoreCriticalRate = cardMilitary.IgnoreCriticalRate,
            PenetrationRate = cardMilitary.PenetrationRate,
            PenetrationResistanceRate = cardMilitary.PenetrationResistanceRate,
            EvasionRate = cardMilitary.EvasionRate,
            DamageAbsorptionRate = cardMilitary.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = cardMilitary.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = cardMilitary.AbsorbedDamageRate,
            VitalityRegenerationRate = cardMilitary.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = cardMilitary.VitalityRegenerationResistanceRate,
            AccuracyRate = cardMilitary.AccuracyRate,
            LifestealRate = cardMilitary.LifestealRate,
            Mana = cardMilitary.Mana,
            ManaRegenerationRate = cardMilitary.ManaRegenerationRate,
            ShieldStrength = cardMilitary.ShieldStrength,
            Tenacity = cardMilitary.Tenacity,
            ResistanceRate = cardMilitary.ResistanceRate,

            // Combo & Control
            ComboRate = cardMilitary.ComboRate,
            IgnoreComboRate = cardMilitary.IgnoreComboRate,
            ComboDamageRate = cardMilitary.ComboDamageRate,
            ComboResistanceRate = cardMilitary.ComboResistanceRate,
            StunRate = cardMilitary.StunRate,
            IgnoreStunRate = cardMilitary.IgnoreStunRate,

            // Reflection
            ReflectionRate = cardMilitary.ReflectionRate,
            IgnoreReflectionRate = cardMilitary.IgnoreReflectionRate,
            ReflectionDamageRate = cardMilitary.ReflectionDamageRate,
            ReflectionResistanceRate = cardMilitary.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = cardMilitary.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = cardMilitary.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = cardMilitary.DamageToSameFactionRate,
            ResistanceToSameFactionRate = cardMilitary.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = cardMilitary.NormalDamageRate,
            NormalResistanceRate = cardMilitary.NormalResistanceRate,
            SkillDamageRate = cardMilitary.SkillDamageRate,
            SkillResistanceRate = cardMilitary.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = cardMilitary.PercentAllHealth,
            PercentAllPhysicalAttack = cardMilitary.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = cardMilitary.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = cardMilitary.PercentAllMagicalAttack,
            PercentAllMagicalDefense = cardMilitary.PercentAllMagicalDefense,
            PercentAllChemicalAttack = cardMilitary.PercentAllChemicalAttack,
            PercentAllChemicalDefense = cardMilitary.PercentAllChemicalDefense,
            PercentAllAtomicAttack = cardMilitary.PercentAllAtomicAttack,
            PercentAllAtomicDefense = cardMilitary.PercentAllAtomicDefense,
            PercentAllMentalAttack = cardMilitary.PercentAllMentalAttack,
            PercentAllMentalDefense = cardMilitary.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from CardMonsters to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(CardMonsters cardMonster)
    {
        if (cardMonster == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = cardMonster.Power,
            Health = cardMonster.Health,
            PhysicalAttack = cardMonster.PhysicalAttack,
            PhysicalDefense = cardMonster.PhysicalDefense,
            MagicalAttack = cardMonster.MagicalAttack,
            MagicalDefense = cardMonster.MagicalDefense,
            ChemicalAttack = cardMonster.ChemicalAttack,
            ChemicalDefense = cardMonster.ChemicalDefense,
            AtomicAttack = cardMonster.AtomicAttack,
            AtomicDefense = cardMonster.AtomicDefense,
            MentalAttack = cardMonster.MentalAttack,
            MentalDefense = cardMonster.MentalDefense,

            // Rates & Combat Mechanics
            Speed = cardMonster.Speed,
            CriticalDamageRate = cardMonster.CriticalDamageRate,
            CriticalRate = cardMonster.CriticalRate,
            CriticalResistanceRate = cardMonster.CriticalResistanceRate,
            IgnoreCriticalRate = cardMonster.IgnoreCriticalRate,
            PenetrationRate = cardMonster.PenetrationRate,
            PenetrationResistanceRate = cardMonster.PenetrationResistanceRate,
            EvasionRate = cardMonster.EvasionRate,
            DamageAbsorptionRate = cardMonster.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = cardMonster.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = cardMonster.AbsorbedDamageRate,
            VitalityRegenerationRate = cardMonster.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = cardMonster.VitalityRegenerationResistanceRate,
            AccuracyRate = cardMonster.AccuracyRate,
            LifestealRate = cardMonster.LifestealRate,
            Mana = cardMonster.Mana,
            ManaRegenerationRate = cardMonster.ManaRegenerationRate,
            ShieldStrength = cardMonster.ShieldStrength,
            Tenacity = cardMonster.Tenacity,
            ResistanceRate = cardMonster.ResistanceRate,

            // Combo & Control
            ComboRate = cardMonster.ComboRate,
            IgnoreComboRate = cardMonster.IgnoreComboRate,
            ComboDamageRate = cardMonster.ComboDamageRate,
            ComboResistanceRate = cardMonster.ComboResistanceRate,
            StunRate = cardMonster.StunRate,
            IgnoreStunRate = cardMonster.IgnoreStunRate,

            // Reflection
            ReflectionRate = cardMonster.ReflectionRate,
            IgnoreReflectionRate = cardMonster.IgnoreReflectionRate,
            ReflectionDamageRate = cardMonster.ReflectionDamageRate,
            ReflectionResistanceRate = cardMonster.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = cardMonster.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = cardMonster.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = cardMonster.DamageToSameFactionRate,
            ResistanceToSameFactionRate = cardMonster.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = cardMonster.NormalDamageRate,
            NormalResistanceRate = cardMonster.NormalResistanceRate,
            SkillDamageRate = cardMonster.SkillDamageRate,
            SkillResistanceRate = cardMonster.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = cardMonster.PercentAllHealth,
            PercentAllPhysicalAttack = cardMonster.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = cardMonster.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = cardMonster.PercentAllMagicalAttack,
            PercentAllMagicalDefense = cardMonster.PercentAllMagicalDefense,
            PercentAllChemicalAttack = cardMonster.PercentAllChemicalAttack,
            PercentAllChemicalDefense = cardMonster.PercentAllChemicalDefense,
            PercentAllAtomicAttack = cardMonster.PercentAllAtomicAttack,
            PercentAllAtomicDefense = cardMonster.PercentAllAtomicDefense,
            PercentAllMentalAttack = cardMonster.PercentAllMentalAttack,
            PercentAllMentalDefense = cardMonster.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from CardSoldiers to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(CardSoldiers cardSoldier)
    {
        if (cardSoldier == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = cardSoldier.Power,
            Health = cardSoldier.Health,
            PhysicalAttack = cardSoldier.PhysicalAttack,
            PhysicalDefense = cardSoldier.PhysicalDefense,
            MagicalAttack = cardSoldier.MagicalAttack,
            MagicalDefense = cardSoldier.MagicalDefense,
            ChemicalAttack = cardSoldier.ChemicalAttack,
            ChemicalDefense = cardSoldier.ChemicalDefense,
            AtomicAttack = cardSoldier.AtomicAttack,
            AtomicDefense = cardSoldier.AtomicDefense,
            MentalAttack = cardSoldier.MentalAttack,
            MentalDefense = cardSoldier.MentalDefense,

            // Rates & Combat Mechanics
            Speed = cardSoldier.Speed,
            CriticalDamageRate = cardSoldier.CriticalDamageRate,
            CriticalRate = cardSoldier.CriticalRate,
            CriticalResistanceRate = cardSoldier.CriticalResistanceRate,
            IgnoreCriticalRate = cardSoldier.IgnoreCriticalRate,
            PenetrationRate = cardSoldier.PenetrationRate,
            PenetrationResistanceRate = cardSoldier.PenetrationResistanceRate,
            EvasionRate = cardSoldier.EvasionRate,
            DamageAbsorptionRate = cardSoldier.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = cardSoldier.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = cardSoldier.AbsorbedDamageRate,
            VitalityRegenerationRate = cardSoldier.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = cardSoldier.VitalityRegenerationResistanceRate,
            AccuracyRate = cardSoldier.AccuracyRate,
            LifestealRate = cardSoldier.LifestealRate,
            Mana = cardSoldier.Mana,
            ManaRegenerationRate = cardSoldier.ManaRegenerationRate,
            ShieldStrength = cardSoldier.ShieldStrength,
            Tenacity = cardSoldier.Tenacity,
            ResistanceRate = cardSoldier.ResistanceRate,

            // Combo & Control
            ComboRate = cardSoldier.ComboRate,
            IgnoreComboRate = cardSoldier.IgnoreComboRate,
            ComboDamageRate = cardSoldier.ComboDamageRate,
            ComboResistanceRate = cardSoldier.ComboResistanceRate,
            StunRate = cardSoldier.StunRate,
            IgnoreStunRate = cardSoldier.IgnoreStunRate,

            // Reflection
            ReflectionRate = cardSoldier.ReflectionRate,
            IgnoreReflectionRate = cardSoldier.IgnoreReflectionRate,
            ReflectionDamageRate = cardSoldier.ReflectionDamageRate,
            ReflectionResistanceRate = cardSoldier.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = cardSoldier.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = cardSoldier.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = cardSoldier.DamageToSameFactionRate,
            ResistanceToSameFactionRate = cardSoldier.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = cardSoldier.NormalDamageRate,
            NormalResistanceRate = cardSoldier.NormalResistanceRate,
            SkillDamageRate = cardSoldier.SkillDamageRate,
            SkillResistanceRate = cardSoldier.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = cardSoldier.PercentAllHealth,
            PercentAllPhysicalAttack = cardSoldier.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = cardSoldier.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = cardSoldier.PercentAllMagicalAttack,
            PercentAllMagicalDefense = cardSoldier.PercentAllMagicalDefense,
            PercentAllChemicalAttack = cardSoldier.PercentAllChemicalAttack,
            PercentAllChemicalDefense = cardSoldier.PercentAllChemicalDefense,
            PercentAllAtomicAttack = cardSoldier.PercentAllAtomicAttack,
            PercentAllAtomicDefense = cardSoldier.PercentAllAtomicDefense,
            PercentAllMentalAttack = cardSoldier.PercentAllMentalAttack,
            PercentAllMentalDefense = cardSoldier.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from CardSpells to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(CardSpells cardSpell)
    {
        if (cardSpell == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = cardSpell.Power,
            Health = cardSpell.Health,
            PhysicalAttack = cardSpell.PhysicalAttack,
            PhysicalDefense = cardSpell.PhysicalDefense,
            MagicalAttack = cardSpell.MagicalAttack,
            MagicalDefense = cardSpell.MagicalDefense,
            ChemicalAttack = cardSpell.ChemicalAttack,
            ChemicalDefense = cardSpell.ChemicalDefense,
            AtomicAttack = cardSpell.AtomicAttack,
            AtomicDefense = cardSpell.AtomicDefense,
            MentalAttack = cardSpell.MentalAttack,
            MentalDefense = cardSpell.MentalDefense,

            // Rates & Combat Mechanics
            Speed = cardSpell.Speed,
            CriticalDamageRate = cardSpell.CriticalDamageRate,
            CriticalRate = cardSpell.CriticalRate,
            CriticalResistanceRate = cardSpell.CriticalResistanceRate,
            IgnoreCriticalRate = cardSpell.IgnoreCriticalRate,
            PenetrationRate = cardSpell.PenetrationRate,
            PenetrationResistanceRate = cardSpell.PenetrationResistanceRate,
            EvasionRate = cardSpell.EvasionRate,
            DamageAbsorptionRate = cardSpell.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = cardSpell.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = cardSpell.AbsorbedDamageRate,
            VitalityRegenerationRate = cardSpell.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = cardSpell.VitalityRegenerationResistanceRate,
            AccuracyRate = cardSpell.AccuracyRate,
            LifestealRate = cardSpell.LifestealRate,
            Mana = cardSpell.Mana,
            ManaRegenerationRate = cardSpell.ManaRegenerationRate,
            ShieldStrength = cardSpell.ShieldStrength,
            Tenacity = cardSpell.Tenacity,
            ResistanceRate = cardSpell.ResistanceRate,

            // Combo & Control
            ComboRate = cardSpell.ComboRate,
            IgnoreComboRate = cardSpell.IgnoreComboRate,
            ComboDamageRate = cardSpell.ComboDamageRate,
            ComboResistanceRate = cardSpell.ComboResistanceRate,
            StunRate = cardSpell.StunRate,
            IgnoreStunRate = cardSpell.IgnoreStunRate,

            // Reflection
            ReflectionRate = cardSpell.ReflectionRate,
            IgnoreReflectionRate = cardSpell.IgnoreReflectionRate,
            ReflectionDamageRate = cardSpell.ReflectionDamageRate,
            ReflectionResistanceRate = cardSpell.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = cardSpell.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = cardSpell.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = cardSpell.DamageToSameFactionRate,
            ResistanceToSameFactionRate = cardSpell.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = cardSpell.NormalDamageRate,
            NormalResistanceRate = cardSpell.NormalResistanceRate,
            SkillDamageRate = cardSpell.SkillDamageRate,
            SkillResistanceRate = cardSpell.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = cardSpell.PercentAllHealth,
            PercentAllPhysicalAttack = cardSpell.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = cardSpell.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = cardSpell.PercentAllMagicalAttack,
            PercentAllMagicalDefense = cardSpell.PercentAllMagicalDefense,
            PercentAllChemicalAttack = cardSpell.PercentAllChemicalAttack,
            PercentAllChemicalDefense = cardSpell.PercentAllChemicalDefense,
            PercentAllAtomicAttack = cardSpell.PercentAllAtomicAttack,
            PercentAllAtomicDefense = cardSpell.PercentAllAtomicDefense,
            PercentAllMentalAttack = cardSpell.PercentAllMentalAttack,
            PercentAllMentalDefense = cardSpell.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from CollaborationEquipments to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(CollaborationEquipments collaborationEquipment)
    {
        if (collaborationEquipment == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = collaborationEquipment.Power,
            Health = collaborationEquipment.Health,
            PhysicalAttack = collaborationEquipment.PhysicalAttack,
            PhysicalDefense = collaborationEquipment.PhysicalDefense,
            MagicalAttack = collaborationEquipment.MagicalAttack,
            MagicalDefense = collaborationEquipment.MagicalDefense,
            ChemicalAttack = collaborationEquipment.ChemicalAttack,
            ChemicalDefense = collaborationEquipment.ChemicalDefense,
            AtomicAttack = collaborationEquipment.AtomicAttack,
            AtomicDefense = collaborationEquipment.AtomicDefense,
            MentalAttack = collaborationEquipment.MentalAttack,
            MentalDefense = collaborationEquipment.MentalDefense,

            // Rates & Combat Mechanics
            Speed = collaborationEquipment.Speed,
            CriticalDamageRate = collaborationEquipment.CriticalDamageRate,
            CriticalRate = collaborationEquipment.CriticalRate,
            CriticalResistanceRate = collaborationEquipment.CriticalResistanceRate,
            IgnoreCriticalRate = collaborationEquipment.IgnoreCriticalRate,
            PenetrationRate = collaborationEquipment.PenetrationRate,
            PenetrationResistanceRate = collaborationEquipment.PenetrationResistanceRate,
            EvasionRate = collaborationEquipment.EvasionRate,
            DamageAbsorptionRate = collaborationEquipment.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = collaborationEquipment.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = collaborationEquipment.AbsorbedDamageRate,
            VitalityRegenerationRate = collaborationEquipment.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = collaborationEquipment.VitalityRegenerationResistanceRate,
            AccuracyRate = collaborationEquipment.AccuracyRate,
            LifestealRate = collaborationEquipment.LifestealRate,
            Mana = collaborationEquipment.Mana,
            ManaRegenerationRate = collaborationEquipment.ManaRegenerationRate,
            ShieldStrength = collaborationEquipment.ShieldStrength,
            Tenacity = collaborationEquipment.Tenacity,
            ResistanceRate = collaborationEquipment.ResistanceRate,

            // Combo & Control
            ComboRate = collaborationEquipment.ComboRate,
            IgnoreComboRate = collaborationEquipment.IgnoreComboRate,
            ComboDamageRate = collaborationEquipment.ComboDamageRate,
            ComboResistanceRate = collaborationEquipment.ComboResistanceRate,
            StunRate = collaborationEquipment.StunRate,
            IgnoreStunRate = collaborationEquipment.IgnoreStunRate,

            // Reflection
            ReflectionRate = collaborationEquipment.ReflectionRate,
            IgnoreReflectionRate = collaborationEquipment.IgnoreReflectionRate,
            ReflectionDamageRate = collaborationEquipment.ReflectionDamageRate,
            ReflectionResistanceRate = collaborationEquipment.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = collaborationEquipment.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = collaborationEquipment.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = collaborationEquipment.DamageToSameFactionRate,
            ResistanceToSameFactionRate = collaborationEquipment.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = collaborationEquipment.NormalDamageRate,
            NormalResistanceRate = collaborationEquipment.NormalResistanceRate,
            SkillDamageRate = collaborationEquipment.SkillDamageRate,
            SkillResistanceRate = collaborationEquipment.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = collaborationEquipment.PercentAllHealth,
            PercentAllPhysicalAttack = collaborationEquipment.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = collaborationEquipment.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = collaborationEquipment.PercentAllMagicalAttack,
            PercentAllMagicalDefense = collaborationEquipment.PercentAllMagicalDefense,
            PercentAllChemicalAttack = collaborationEquipment.PercentAllChemicalAttack,
            PercentAllChemicalDefense = collaborationEquipment.PercentAllChemicalDefense,
            PercentAllAtomicAttack = collaborationEquipment.PercentAllAtomicAttack,
            PercentAllAtomicDefense = collaborationEquipment.PercentAllAtomicDefense,
            PercentAllMentalAttack = collaborationEquipment.PercentAllMentalAttack,
            PercentAllMentalDefense = collaborationEquipment.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Collaborations to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Collaborations collaboration)
    {
        if (collaboration == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = collaboration.Power,
            Health = collaboration.Health,
            PhysicalAttack = collaboration.PhysicalAttack,
            PhysicalDefense = collaboration.PhysicalDefense,
            MagicalAttack = collaboration.MagicalAttack,
            MagicalDefense = collaboration.MagicalDefense,
            ChemicalAttack = collaboration.ChemicalAttack,
            ChemicalDefense = collaboration.ChemicalDefense,
            AtomicAttack = collaboration.AtomicAttack,
            AtomicDefense = collaboration.AtomicDefense,
            MentalAttack = collaboration.MentalAttack,
            MentalDefense = collaboration.MentalDefense,

            // Rates & Combat Mechanics
            Speed = collaboration.Speed,
            CriticalDamageRate = collaboration.CriticalDamageRate,
            CriticalRate = collaboration.CriticalRate,
            CriticalResistanceRate = collaboration.CriticalResistanceRate,
            IgnoreCriticalRate = collaboration.IgnoreCriticalRate,
            PenetrationRate = collaboration.PenetrationRate,
            PenetrationResistanceRate = collaboration.PenetrationResistanceRate,
            EvasionRate = collaboration.EvasionRate,
            DamageAbsorptionRate = collaboration.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = collaboration.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = collaboration.AbsorbedDamageRate,
            VitalityRegenerationRate = collaboration.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = collaboration.VitalityRegenerationResistanceRate,
            AccuracyRate = collaboration.AccuracyRate,
            LifestealRate = collaboration.LifestealRate,
            Mana = collaboration.Mana,
            ManaRegenerationRate = collaboration.ManaRegenerationRate,
            ShieldStrength = collaboration.ShieldStrength,
            Tenacity = collaboration.Tenacity,
            ResistanceRate = collaboration.ResistanceRate,

            // Combo & Control
            ComboRate = collaboration.ComboRate,
            IgnoreComboRate = collaboration.IgnoreComboRate,
            ComboDamageRate = collaboration.ComboDamageRate,
            ComboResistanceRate = collaboration.ComboResistanceRate,
            StunRate = collaboration.StunRate,
            IgnoreStunRate = collaboration.IgnoreStunRate,

            // Reflection
            ReflectionRate = collaboration.ReflectionRate,
            IgnoreReflectionRate = collaboration.IgnoreReflectionRate,
            ReflectionDamageRate = collaboration.ReflectionDamageRate,
            ReflectionResistanceRate = collaboration.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = collaboration.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = collaboration.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = collaboration.DamageToSameFactionRate,
            ResistanceToSameFactionRate = collaboration.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = collaboration.NormalDamageRate,
            NormalResistanceRate = collaboration.NormalResistanceRate,
            SkillDamageRate = collaboration.SkillDamageRate,
            SkillResistanceRate = collaboration.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = collaboration.PercentAllHealth,
            PercentAllPhysicalAttack = collaboration.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = collaboration.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = collaboration.PercentAllMagicalAttack,
            PercentAllMagicalDefense = collaboration.PercentAllMagicalDefense,
            PercentAllChemicalAttack = collaboration.PercentAllChemicalAttack,
            PercentAllChemicalDefense = collaboration.PercentAllChemicalDefense,
            PercentAllAtomicAttack = collaboration.PercentAllAtomicAttack,
            PercentAllAtomicDefense = collaboration.PercentAllAtomicDefense,
            PercentAllMentalAttack = collaboration.PercentAllMentalAttack,
            PercentAllMentalDefense = collaboration.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Cores to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Cores core)
    {
        if (core == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = core.Power,
            Health = core.Health,
            PhysicalAttack = core.PhysicalAttack,
            PhysicalDefense = core.PhysicalDefense,
            MagicalAttack = core.MagicalAttack,
            MagicalDefense = core.MagicalDefense,
            ChemicalAttack = core.ChemicalAttack,
            ChemicalDefense = core.ChemicalDefense,
            AtomicAttack = core.AtomicAttack,
            AtomicDefense = core.AtomicDefense,
            MentalAttack = core.MentalAttack,
            MentalDefense = core.MentalDefense,

            // Rates & Combat Mechanics
            Speed = core.Speed,
            CriticalDamageRate = core.CriticalDamageRate,
            CriticalRate = core.CriticalRate,
            CriticalResistanceRate = core.CriticalResistanceRate,
            IgnoreCriticalRate = core.IgnoreCriticalRate,
            PenetrationRate = core.PenetrationRate,
            PenetrationResistanceRate = core.PenetrationResistanceRate,
            EvasionRate = core.EvasionRate,
            DamageAbsorptionRate = core.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = core.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = core.AbsorbedDamageRate,
            VitalityRegenerationRate = core.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = core.VitalityRegenerationResistanceRate,
            AccuracyRate = core.AccuracyRate,
            LifestealRate = core.LifestealRate,
            Mana = core.Mana,
            ManaRegenerationRate = core.ManaRegenerationRate,
            ShieldStrength = core.ShieldStrength,
            Tenacity = core.Tenacity,
            ResistanceRate = core.ResistanceRate,

            // Combo & Control
            ComboRate = core.ComboRate,
            IgnoreComboRate = core.IgnoreComboRate,
            ComboDamageRate = core.ComboDamageRate,
            ComboResistanceRate = core.ComboResistanceRate,
            StunRate = core.StunRate,
            IgnoreStunRate = core.IgnoreStunRate,

            // Reflection
            ReflectionRate = core.ReflectionRate,
            IgnoreReflectionRate = core.IgnoreReflectionRate,
            ReflectionDamageRate = core.ReflectionDamageRate,
            ReflectionResistanceRate = core.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = core.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = core.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = core.DamageToSameFactionRate,
            ResistanceToSameFactionRate = core.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = core.NormalDamageRate,
            NormalResistanceRate = core.NormalResistanceRate,
            SkillDamageRate = core.SkillDamageRate,
            SkillResistanceRate = core.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = core.PercentAllHealth,
            PercentAllPhysicalAttack = core.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = core.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = core.PercentAllMagicalAttack,
            PercentAllMagicalDefense = core.PercentAllMagicalDefense,
            PercentAllChemicalAttack = core.PercentAllChemicalAttack,
            PercentAllChemicalDefense = core.PercentAllChemicalDefense,
            PercentAllAtomicAttack = core.PercentAllAtomicAttack,
            PercentAllAtomicDefense = core.PercentAllAtomicDefense,
            PercentAllMentalAttack = core.PercentAllMentalAttack,
            PercentAllMentalDefense = core.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Emojis to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Emojis emoji)
    {
        if (emoji == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = emoji.Power,
            Health = emoji.Health,
            PhysicalAttack = emoji.PhysicalAttack,
            PhysicalDefense = emoji.PhysicalDefense,
            MagicalAttack = emoji.MagicalAttack,
            MagicalDefense = emoji.MagicalDefense,
            ChemicalAttack = emoji.ChemicalAttack,
            ChemicalDefense = emoji.ChemicalDefense,
            AtomicAttack = emoji.AtomicAttack,
            AtomicDefense = emoji.AtomicDefense,
            MentalAttack = emoji.MentalAttack,
            MentalDefense = emoji.MentalDefense,

            // Rates & Combat Mechanics
            Speed = emoji.Speed,
            CriticalDamageRate = emoji.CriticalDamageRate,
            CriticalRate = emoji.CriticalRate,
            CriticalResistanceRate = emoji.CriticalResistanceRate,
            IgnoreCriticalRate = emoji.IgnoreCriticalRate,
            PenetrationRate = emoji.PenetrationRate,
            PenetrationResistanceRate = emoji.PenetrationResistanceRate,
            EvasionRate = emoji.EvasionRate,
            DamageAbsorptionRate = emoji.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = emoji.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = emoji.AbsorbedDamageRate,
            VitalityRegenerationRate = emoji.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = emoji.VitalityRegenerationResistanceRate,
            AccuracyRate = emoji.AccuracyRate,
            LifestealRate = emoji.LifestealRate,
            Mana = emoji.Mana,
            ManaRegenerationRate = emoji.ManaRegenerationRate,
            ShieldStrength = emoji.ShieldStrength,
            Tenacity = emoji.Tenacity,
            ResistanceRate = emoji.ResistanceRate,

            // Combo & Control
            ComboRate = emoji.ComboRate,
            IgnoreComboRate = emoji.IgnoreComboRate,
            ComboDamageRate = emoji.ComboDamageRate,
            ComboResistanceRate = emoji.ComboResistanceRate,
            StunRate = emoji.StunRate,
            IgnoreStunRate = emoji.IgnoreStunRate,

            // Reflection
            ReflectionRate = emoji.ReflectionRate,
            IgnoreReflectionRate = emoji.IgnoreReflectionRate,
            ReflectionDamageRate = emoji.ReflectionDamageRate,
            ReflectionResistanceRate = emoji.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = emoji.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = emoji.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = emoji.DamageToSameFactionRate,
            ResistanceToSameFactionRate = emoji.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = emoji.NormalDamageRate,
            NormalResistanceRate = emoji.NormalResistanceRate,
            SkillDamageRate = emoji.SkillDamageRate,
            SkillResistanceRate = emoji.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = emoji.PercentAllHealth,
            PercentAllPhysicalAttack = emoji.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = emoji.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = emoji.PercentAllMagicalAttack,
            PercentAllMagicalDefense = emoji.PercentAllMagicalDefense,
            PercentAllChemicalAttack = emoji.PercentAllChemicalAttack,
            PercentAllChemicalDefense = emoji.PercentAllChemicalDefense,
            PercentAllAtomicAttack = emoji.PercentAllAtomicAttack,
            PercentAllAtomicDefense = emoji.PercentAllAtomicDefense,
            PercentAllMentalAttack = emoji.PercentAllMentalAttack,
            PercentAllMentalDefense = emoji.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Equipments to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Equipments equipment)
    {
        if (equipment == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = equipment.Power,
            Health = equipment.Health,
            PhysicalAttack = equipment.PhysicalAttack,
            PhysicalDefense = equipment.PhysicalDefense,
            MagicalAttack = equipment.MagicalAttack,
            MagicalDefense = equipment.MagicalDefense,
            ChemicalAttack = equipment.ChemicalAttack,
            ChemicalDefense = equipment.ChemicalDefense,
            AtomicAttack = equipment.AtomicAttack,
            AtomicDefense = equipment.AtomicDefense,
            MentalAttack = equipment.MentalAttack,
            MentalDefense = equipment.MentalDefense,

            // Rates & Combat Mechanics
            Speed = equipment.Speed,
            CriticalDamageRate = equipment.CriticalDamageRate,
            CriticalRate = equipment.CriticalRate,
            CriticalResistanceRate = equipment.CriticalResistanceRate,
            IgnoreCriticalRate = equipment.IgnoreCriticalRate,
            PenetrationRate = equipment.PenetrationRate,
            PenetrationResistanceRate = equipment.PenetrationResistanceRate,
            EvasionRate = equipment.EvasionRate,
            DamageAbsorptionRate = equipment.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = equipment.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = equipment.AbsorbedDamageRate,
            VitalityRegenerationRate = equipment.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = equipment.VitalityRegenerationResistanceRate,
            AccuracyRate = equipment.AccuracyRate,
            LifestealRate = equipment.LifestealRate,
            Mana = equipment.Mana,
            ManaRegenerationRate = equipment.ManaRegenerationRate,
            ShieldStrength = equipment.ShieldStrength,
            Tenacity = equipment.Tenacity,
            ResistanceRate = equipment.ResistanceRate,

            // Combo & Control
            ComboRate = equipment.ComboRate,
            IgnoreComboRate = equipment.IgnoreComboRate,
            ComboDamageRate = equipment.ComboDamageRate,
            ComboResistanceRate = equipment.ComboResistanceRate,
            StunRate = equipment.StunRate,
            IgnoreStunRate = equipment.IgnoreStunRate,

            // Reflection
            ReflectionRate = equipment.ReflectionRate,
            IgnoreReflectionRate = equipment.IgnoreReflectionRate,
            ReflectionDamageRate = equipment.ReflectionDamageRate,
            ReflectionResistanceRate = equipment.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = equipment.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = equipment.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = equipment.DamageToSameFactionRate,
            ResistanceToSameFactionRate = equipment.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = equipment.NormalDamageRate,
            NormalResistanceRate = equipment.NormalResistanceRate,
            SkillDamageRate = equipment.SkillDamageRate,
            SkillResistanceRate = equipment.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = equipment.PercentAllHealth,
            PercentAllPhysicalAttack = equipment.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = equipment.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = equipment.PercentAllMagicalAttack,
            PercentAllMagicalDefense = equipment.PercentAllMagicalDefense,
            PercentAllChemicalAttack = equipment.PercentAllChemicalAttack,
            PercentAllChemicalDefense = equipment.PercentAllChemicalDefense,
            PercentAllAtomicAttack = equipment.PercentAllAtomicAttack,
            PercentAllAtomicDefense = equipment.PercentAllAtomicDefense,
            PercentAllMentalAttack = equipment.PercentAllMentalAttack,
            PercentAllMentalDefense = equipment.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Fashions to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Fashions fashion)
    {
        if (fashion == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = fashion.Power,
            Health = fashion.Health,
            PhysicalAttack = fashion.PhysicalAttack,
            PhysicalDefense = fashion.PhysicalDefense,
            MagicalAttack = fashion.MagicalAttack,
            MagicalDefense = fashion.MagicalDefense,
            ChemicalAttack = fashion.ChemicalAttack,
            ChemicalDefense = fashion.ChemicalDefense,
            AtomicAttack = fashion.AtomicAttack,
            AtomicDefense = fashion.AtomicDefense,
            MentalAttack = fashion.MentalAttack,
            MentalDefense = fashion.MentalDefense,

            // Rates & Combat Mechanics
            Speed = fashion.Speed,
            CriticalDamageRate = fashion.CriticalDamageRate,
            CriticalRate = fashion.CriticalRate,
            CriticalResistanceRate = fashion.CriticalResistanceRate,
            IgnoreCriticalRate = fashion.IgnoreCriticalRate,
            PenetrationRate = fashion.PenetrationRate,
            PenetrationResistanceRate = fashion.PenetrationResistanceRate,
            EvasionRate = fashion.EvasionRate,
            DamageAbsorptionRate = fashion.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = fashion.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = fashion.AbsorbedDamageRate,
            VitalityRegenerationRate = fashion.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = fashion.VitalityRegenerationResistanceRate,
            AccuracyRate = fashion.AccuracyRate,
            LifestealRate = fashion.LifestealRate,
            Mana = fashion.Mana,
            ManaRegenerationRate = fashion.ManaRegenerationRate,
            ShieldStrength = fashion.ShieldStrength,
            Tenacity = fashion.Tenacity,
            ResistanceRate = fashion.ResistanceRate,

            // Combo & Control
            ComboRate = fashion.ComboRate,
            IgnoreComboRate = fashion.IgnoreComboRate,
            ComboDamageRate = fashion.ComboDamageRate,
            ComboResistanceRate = fashion.ComboResistanceRate,
            StunRate = fashion.StunRate,
            IgnoreStunRate = fashion.IgnoreStunRate,

            // Reflection
            ReflectionRate = fashion.ReflectionRate,
            IgnoreReflectionRate = fashion.IgnoreReflectionRate,
            ReflectionDamageRate = fashion.ReflectionDamageRate,
            ReflectionResistanceRate = fashion.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = fashion.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = fashion.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = fashion.DamageToSameFactionRate,
            ResistanceToSameFactionRate = fashion.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = fashion.NormalDamageRate,
            NormalResistanceRate = fashion.NormalResistanceRate,
            SkillDamageRate = fashion.SkillDamageRate,
            SkillResistanceRate = fashion.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = fashion.PercentAllHealth,
            PercentAllPhysicalAttack = fashion.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = fashion.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = fashion.PercentAllMagicalAttack,
            PercentAllMagicalDefense = fashion.PercentAllMagicalDefense,
            PercentAllChemicalAttack = fashion.PercentAllChemicalAttack,
            PercentAllChemicalDefense = fashion.PercentAllChemicalDefense,
            PercentAllAtomicAttack = fashion.PercentAllAtomicAttack,
            PercentAllAtomicDefense = fashion.PercentAllAtomicDefense,
            PercentAllMentalAttack = fashion.PercentAllMentalAttack,
            PercentAllMentalDefense = fashion.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Foods to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Foods food)
    {
        if (food == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = food.Power,
            Health = food.Health,
            PhysicalAttack = food.PhysicalAttack,
            PhysicalDefense = food.PhysicalDefense,
            MagicalAttack = food.MagicalAttack,
            MagicalDefense = food.MagicalDefense,
            ChemicalAttack = food.ChemicalAttack,
            ChemicalDefense = food.ChemicalDefense,
            AtomicAttack = food.AtomicAttack,
            AtomicDefense = food.AtomicDefense,
            MentalAttack = food.MentalAttack,
            MentalDefense = food.MentalDefense,

            // Rates & Combat Mechanics
            Speed = food.Speed,
            CriticalDamageRate = food.CriticalDamageRate,
            CriticalRate = food.CriticalRate,
            CriticalResistanceRate = food.CriticalResistanceRate,
            IgnoreCriticalRate = food.IgnoreCriticalRate,
            PenetrationRate = food.PenetrationRate,
            PenetrationResistanceRate = food.PenetrationResistanceRate,
            EvasionRate = food.EvasionRate,
            DamageAbsorptionRate = food.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = food.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = food.AbsorbedDamageRate,
            VitalityRegenerationRate = food.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = food.VitalityRegenerationResistanceRate,
            AccuracyRate = food.AccuracyRate,
            LifestealRate = food.LifestealRate,
            Mana = food.Mana,
            ManaRegenerationRate = food.ManaRegenerationRate,
            ShieldStrength = food.ShieldStrength,
            Tenacity = food.Tenacity,
            ResistanceRate = food.ResistanceRate,

            // Combo & Control
            ComboRate = food.ComboRate,
            IgnoreComboRate = food.IgnoreComboRate,
            ComboDamageRate = food.ComboDamageRate,
            ComboResistanceRate = food.ComboResistanceRate,
            StunRate = food.StunRate,
            IgnoreStunRate = food.IgnoreStunRate,

            // Reflection
            ReflectionRate = food.ReflectionRate,
            IgnoreReflectionRate = food.IgnoreReflectionRate,
            ReflectionDamageRate = food.ReflectionDamageRate,
            ReflectionResistanceRate = food.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = food.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = food.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = food.DamageToSameFactionRate,
            ResistanceToSameFactionRate = food.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = food.NormalDamageRate,
            NormalResistanceRate = food.NormalResistanceRate,
            SkillDamageRate = food.SkillDamageRate,
            SkillResistanceRate = food.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = food.PercentAllHealth,
            PercentAllPhysicalAttack = food.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = food.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = food.PercentAllMagicalAttack,
            PercentAllMagicalDefense = food.PercentAllMagicalDefense,
            PercentAllChemicalAttack = food.PercentAllChemicalAttack,
            PercentAllChemicalDefense = food.PercentAllChemicalDefense,
            PercentAllAtomicAttack = food.PercentAllAtomicAttack,
            PercentAllAtomicDefense = food.PercentAllAtomicDefense,
            PercentAllMentalAttack = food.PercentAllMentalAttack,
            PercentAllMentalDefense = food.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Forges to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Forges forge)
    {
        if (forge == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = forge.Power,
            Health = forge.Health,
            PhysicalAttack = forge.PhysicalAttack,
            PhysicalDefense = forge.PhysicalDefense,
            MagicalAttack = forge.MagicalAttack,
            MagicalDefense = forge.MagicalDefense,
            ChemicalAttack = forge.ChemicalAttack,
            ChemicalDefense = forge.ChemicalDefense,
            AtomicAttack = forge.AtomicAttack,
            AtomicDefense = forge.AtomicDefense,
            MentalAttack = forge.MentalAttack,
            MentalDefense = forge.MentalDefense,

            // Rates & Combat Mechanics
            Speed = forge.Speed,
            CriticalDamageRate = forge.CriticalDamageRate,
            CriticalRate = forge.CriticalRate,
            CriticalResistanceRate = forge.CriticalResistanceRate,
            IgnoreCriticalRate = forge.IgnoreCriticalRate,
            PenetrationRate = forge.PenetrationRate,
            PenetrationResistanceRate = forge.PenetrationResistanceRate,
            EvasionRate = forge.EvasionRate,
            DamageAbsorptionRate = forge.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = forge.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = forge.AbsorbedDamageRate,
            VitalityRegenerationRate = forge.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = forge.VitalityRegenerationResistanceRate,
            AccuracyRate = forge.AccuracyRate,
            LifestealRate = forge.LifestealRate,
            Mana = forge.Mana,
            ManaRegenerationRate = forge.ManaRegenerationRate,
            ShieldStrength = forge.ShieldStrength,
            Tenacity = forge.Tenacity,
            ResistanceRate = forge.ResistanceRate,

            // Combo & Control
            ComboRate = forge.ComboRate,
            IgnoreComboRate = forge.IgnoreComboRate,
            ComboDamageRate = forge.ComboDamageRate,
            ComboResistanceRate = forge.ComboResistanceRate,
            StunRate = forge.StunRate,
            IgnoreStunRate = forge.IgnoreStunRate,

            // Reflection
            ReflectionRate = forge.ReflectionRate,
            IgnoreReflectionRate = forge.IgnoreReflectionRate,
            ReflectionDamageRate = forge.ReflectionDamageRate,
            ReflectionResistanceRate = forge.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = forge.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = forge.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = forge.DamageToSameFactionRate,
            ResistanceToSameFactionRate = forge.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = forge.NormalDamageRate,
            NormalResistanceRate = forge.NormalResistanceRate,
            SkillDamageRate = forge.SkillDamageRate,
            SkillResistanceRate = forge.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = forge.PercentAllHealth,
            PercentAllPhysicalAttack = forge.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = forge.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = forge.PercentAllMagicalAttack,
            PercentAllMagicalDefense = forge.PercentAllMagicalDefense,
            PercentAllChemicalAttack = forge.PercentAllChemicalAttack,
            PercentAllChemicalDefense = forge.PercentAllChemicalDefense,
            PercentAllAtomicAttack = forge.PercentAllAtomicAttack,
            PercentAllAtomicDefense = forge.PercentAllAtomicDefense,
            PercentAllMentalAttack = forge.PercentAllMentalAttack,
            PercentAllMentalDefense = forge.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Furnitures to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Furnitures furniture)
    {
        if (furniture == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = furniture.Power,
            Health = furniture.Health,
            PhysicalAttack = furniture.PhysicalAttack,
            PhysicalDefense = furniture.PhysicalDefense,
            MagicalAttack = furniture.MagicalAttack,
            MagicalDefense = furniture.MagicalDefense,
            ChemicalAttack = furniture.ChemicalAttack,
            ChemicalDefense = furniture.ChemicalDefense,
            AtomicAttack = furniture.AtomicAttack,
            AtomicDefense = furniture.AtomicDefense,
            MentalAttack = furniture.MentalAttack,
            MentalDefense = furniture.MentalDefense,

            // Rates & Combat Mechanics
            Speed = furniture.Speed,
            CriticalDamageRate = furniture.CriticalDamageRate,
            CriticalRate = furniture.CriticalRate,
            CriticalResistanceRate = furniture.CriticalResistanceRate,
            IgnoreCriticalRate = furniture.IgnoreCriticalRate,
            PenetrationRate = furniture.PenetrationRate,
            PenetrationResistanceRate = furniture.PenetrationResistanceRate,
            EvasionRate = furniture.EvasionRate,
            DamageAbsorptionRate = furniture.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = furniture.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = furniture.AbsorbedDamageRate,
            VitalityRegenerationRate = furniture.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = furniture.VitalityRegenerationResistanceRate,
            AccuracyRate = furniture.AccuracyRate,
            LifestealRate = furniture.LifestealRate,
            Mana = furniture.Mana,
            ManaRegenerationRate = furniture.ManaRegenerationRate,
            ShieldStrength = furniture.ShieldStrength,
            Tenacity = furniture.Tenacity,
            ResistanceRate = furniture.ResistanceRate,

            // Combo & Control
            ComboRate = furniture.ComboRate,
            IgnoreComboRate = furniture.IgnoreComboRate,
            ComboDamageRate = furniture.ComboDamageRate,
            ComboResistanceRate = furniture.ComboResistanceRate,
            StunRate = furniture.StunRate,
            IgnoreStunRate = furniture.IgnoreStunRate,

            // Reflection
            ReflectionRate = furniture.ReflectionRate,
            IgnoreReflectionRate = furniture.IgnoreReflectionRate,
            ReflectionDamageRate = furniture.ReflectionDamageRate,
            ReflectionResistanceRate = furniture.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = furniture.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = furniture.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = furniture.DamageToSameFactionRate,
            ResistanceToSameFactionRate = furniture.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = furniture.NormalDamageRate,
            NormalResistanceRate = furniture.NormalResistanceRate,
            SkillDamageRate = furniture.SkillDamageRate,
            SkillResistanceRate = furniture.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = furniture.PercentAllHealth,
            PercentAllPhysicalAttack = furniture.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = furniture.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = furniture.PercentAllMagicalAttack,
            PercentAllMagicalDefense = furniture.PercentAllMagicalDefense,
            PercentAllChemicalAttack = furniture.PercentAllChemicalAttack,
            PercentAllChemicalDefense = furniture.PercentAllChemicalDefense,
            PercentAllAtomicAttack = furniture.PercentAllAtomicAttack,
            PercentAllAtomicDefense = furniture.PercentAllAtomicDefense,
            PercentAllMentalAttack = furniture.PercentAllMentalAttack,
            PercentAllMentalDefense = furniture.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from MagicFormationCircles to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(MagicFormationCircles magicFormationCircle)
    {
        if (magicFormationCircle == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = magicFormationCircle.Power,
            Health = magicFormationCircle.Health,
            PhysicalAttack = magicFormationCircle.PhysicalAttack,
            PhysicalDefense = magicFormationCircle.PhysicalDefense,
            MagicalAttack = magicFormationCircle.MagicalAttack,
            MagicalDefense = magicFormationCircle.MagicalDefense,
            ChemicalAttack = magicFormationCircle.ChemicalAttack,
            ChemicalDefense = magicFormationCircle.ChemicalDefense,
            AtomicAttack = magicFormationCircle.AtomicAttack,
            AtomicDefense = magicFormationCircle.AtomicDefense,
            MentalAttack = magicFormationCircle.MentalAttack,
            MentalDefense = magicFormationCircle.MentalDefense,

            // Rates & Combat Mechanics
            Speed = magicFormationCircle.Speed,
            CriticalDamageRate = magicFormationCircle.CriticalDamageRate,
            CriticalRate = magicFormationCircle.CriticalRate,
            CriticalResistanceRate = magicFormationCircle.CriticalResistanceRate,
            IgnoreCriticalRate = magicFormationCircle.IgnoreCriticalRate,
            PenetrationRate = magicFormationCircle.PenetrationRate,
            PenetrationResistanceRate = magicFormationCircle.PenetrationResistanceRate,
            EvasionRate = magicFormationCircle.EvasionRate,
            DamageAbsorptionRate = magicFormationCircle.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = magicFormationCircle.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = magicFormationCircle.AbsorbedDamageRate,
            VitalityRegenerationRate = magicFormationCircle.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = magicFormationCircle.VitalityRegenerationResistanceRate,
            AccuracyRate = magicFormationCircle.AccuracyRate,
            LifestealRate = magicFormationCircle.LifestealRate,
            Mana = magicFormationCircle.Mana,
            ManaRegenerationRate = magicFormationCircle.ManaRegenerationRate,
            ShieldStrength = magicFormationCircle.ShieldStrength,
            Tenacity = magicFormationCircle.Tenacity,
            ResistanceRate = magicFormationCircle.ResistanceRate,

            // Combo & Control
            ComboRate = magicFormationCircle.ComboRate,
            IgnoreComboRate = magicFormationCircle.IgnoreComboRate,
            ComboDamageRate = magicFormationCircle.ComboDamageRate,
            ComboResistanceRate = magicFormationCircle.ComboResistanceRate,
            StunRate = magicFormationCircle.StunRate,
            IgnoreStunRate = magicFormationCircle.IgnoreStunRate,

            // Reflection
            ReflectionRate = magicFormationCircle.ReflectionRate,
            IgnoreReflectionRate = magicFormationCircle.IgnoreReflectionRate,
            ReflectionDamageRate = magicFormationCircle.ReflectionDamageRate,
            ReflectionResistanceRate = magicFormationCircle.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = magicFormationCircle.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = magicFormationCircle.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = magicFormationCircle.DamageToSameFactionRate,
            ResistanceToSameFactionRate = magicFormationCircle.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = magicFormationCircle.NormalDamageRate,
            NormalResistanceRate = magicFormationCircle.NormalResistanceRate,
            SkillDamageRate = magicFormationCircle.SkillDamageRate,
            SkillResistanceRate = magicFormationCircle.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = magicFormationCircle.PercentAllHealth,
            PercentAllPhysicalAttack = magicFormationCircle.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = magicFormationCircle.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = magicFormationCircle.PercentAllMagicalAttack,
            PercentAllMagicalDefense = magicFormationCircle.PercentAllMagicalDefense,
            PercentAllChemicalAttack = magicFormationCircle.PercentAllChemicalAttack,
            PercentAllChemicalDefense = magicFormationCircle.PercentAllChemicalDefense,
            PercentAllAtomicAttack = magicFormationCircle.PercentAllAtomicAttack,
            PercentAllAtomicDefense = magicFormationCircle.PercentAllAtomicDefense,
            PercentAllMentalAttack = magicFormationCircle.PercentAllMentalAttack,
            PercentAllMentalDefense = magicFormationCircle.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from MechaBeasts to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(MechaBeasts mechaBeast)
    {
        if (mechaBeast == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = mechaBeast.Power,
            Health = mechaBeast.Health,
            PhysicalAttack = mechaBeast.PhysicalAttack,
            PhysicalDefense = mechaBeast.PhysicalDefense,
            MagicalAttack = mechaBeast.MagicalAttack,
            MagicalDefense = mechaBeast.MagicalDefense,
            ChemicalAttack = mechaBeast.ChemicalAttack,
            ChemicalDefense = mechaBeast.ChemicalDefense,
            AtomicAttack = mechaBeast.AtomicAttack,
            AtomicDefense = mechaBeast.AtomicDefense,
            MentalAttack = mechaBeast.MentalAttack,
            MentalDefense = mechaBeast.MentalDefense,

            // Rates & Combat Mechanics
            Speed = mechaBeast.Speed,
            CriticalDamageRate = mechaBeast.CriticalDamageRate,
            CriticalRate = mechaBeast.CriticalRate,
            CriticalResistanceRate = mechaBeast.CriticalResistanceRate,
            IgnoreCriticalRate = mechaBeast.IgnoreCriticalRate,
            PenetrationRate = mechaBeast.PenetrationRate,
            PenetrationResistanceRate = mechaBeast.PenetrationResistanceRate,
            EvasionRate = mechaBeast.EvasionRate,
            DamageAbsorptionRate = mechaBeast.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = mechaBeast.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = mechaBeast.AbsorbedDamageRate,
            VitalityRegenerationRate = mechaBeast.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = mechaBeast.VitalityRegenerationResistanceRate,
            AccuracyRate = mechaBeast.AccuracyRate,
            LifestealRate = mechaBeast.LifestealRate,
            Mana = mechaBeast.Mana,
            ManaRegenerationRate = mechaBeast.ManaRegenerationRate,
            ShieldStrength = mechaBeast.ShieldStrength,
            Tenacity = mechaBeast.Tenacity,
            ResistanceRate = mechaBeast.ResistanceRate,

            // Combo & Control
            ComboRate = mechaBeast.ComboRate,
            IgnoreComboRate = mechaBeast.IgnoreComboRate,
            ComboDamageRate = mechaBeast.ComboDamageRate,
            ComboResistanceRate = mechaBeast.ComboResistanceRate,
            StunRate = mechaBeast.StunRate,
            IgnoreStunRate = mechaBeast.IgnoreStunRate,

            // Reflection
            ReflectionRate = mechaBeast.ReflectionRate,
            IgnoreReflectionRate = mechaBeast.IgnoreReflectionRate,
            ReflectionDamageRate = mechaBeast.ReflectionDamageRate,
            ReflectionResistanceRate = mechaBeast.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = mechaBeast.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = mechaBeast.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = mechaBeast.DamageToSameFactionRate,
            ResistanceToSameFactionRate = mechaBeast.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = mechaBeast.NormalDamageRate,
            NormalResistanceRate = mechaBeast.NormalResistanceRate,
            SkillDamageRate = mechaBeast.SkillDamageRate,
            SkillResistanceRate = mechaBeast.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = mechaBeast.PercentAllHealth,
            PercentAllPhysicalAttack = mechaBeast.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = mechaBeast.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = mechaBeast.PercentAllMagicalAttack,
            PercentAllMagicalDefense = mechaBeast.PercentAllMagicalDefense,
            PercentAllChemicalAttack = mechaBeast.PercentAllChemicalAttack,
            PercentAllChemicalDefense = mechaBeast.PercentAllChemicalDefense,
            PercentAllAtomicAttack = mechaBeast.PercentAllAtomicAttack,
            PercentAllAtomicDefense = mechaBeast.PercentAllAtomicDefense,
            PercentAllMentalAttack = mechaBeast.PercentAllMentalAttack,
            PercentAllMentalDefense = mechaBeast.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Medals to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Medals medal)
    {
        if (medal == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = medal.Power,
            Health = medal.Health,
            PhysicalAttack = medal.PhysicalAttack,
            PhysicalDefense = medal.PhysicalDefense,
            MagicalAttack = medal.MagicalAttack,
            MagicalDefense = medal.MagicalDefense,
            ChemicalAttack = medal.ChemicalAttack,
            ChemicalDefense = medal.ChemicalDefense,
            AtomicAttack = medal.AtomicAttack,
            AtomicDefense = medal.AtomicDefense,
            MentalAttack = medal.MentalAttack,
            MentalDefense = medal.MentalDefense,

            // Rates & Combat Mechanics
            Speed = medal.Speed,
            CriticalDamageRate = medal.CriticalDamageRate,
            CriticalRate = medal.CriticalRate,
            CriticalResistanceRate = medal.CriticalResistanceRate,
            IgnoreCriticalRate = medal.IgnoreCriticalRate,
            PenetrationRate = medal.PenetrationRate,
            PenetrationResistanceRate = medal.PenetrationResistanceRate,
            EvasionRate = medal.EvasionRate,
            DamageAbsorptionRate = medal.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = medal.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = medal.AbsorbedDamageRate,
            VitalityRegenerationRate = medal.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = medal.VitalityRegenerationResistanceRate,
            AccuracyRate = medal.AccuracyRate,
            LifestealRate = medal.LifestealRate,
            Mana = medal.Mana,
            ManaRegenerationRate = medal.ManaRegenerationRate,
            ShieldStrength = medal.ShieldStrength,
            Tenacity = medal.Tenacity,
            ResistanceRate = medal.ResistanceRate,

            // Combo & Control
            ComboRate = medal.ComboRate,
            IgnoreComboRate = medal.IgnoreComboRate,
            ComboDamageRate = medal.ComboDamageRate,
            ComboResistanceRate = medal.ComboResistanceRate,
            StunRate = medal.StunRate,
            IgnoreStunRate = medal.IgnoreStunRate,

            // Reflection
            ReflectionRate = medal.ReflectionRate,
            IgnoreReflectionRate = medal.IgnoreReflectionRate,
            ReflectionDamageRate = medal.ReflectionDamageRate,
            ReflectionResistanceRate = medal.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = medal.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = medal.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = medal.DamageToSameFactionRate,
            ResistanceToSameFactionRate = medal.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = medal.NormalDamageRate,
            NormalResistanceRate = medal.NormalResistanceRate,
            SkillDamageRate = medal.SkillDamageRate,
            SkillResistanceRate = medal.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = medal.PercentAllHealth,
            PercentAllPhysicalAttack = medal.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = medal.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = medal.PercentAllMagicalAttack,
            PercentAllMagicalDefense = medal.PercentAllMagicalDefense,
            PercentAllChemicalAttack = medal.PercentAllChemicalAttack,
            PercentAllChemicalDefense = medal.PercentAllChemicalDefense,
            PercentAllAtomicAttack = medal.PercentAllAtomicAttack,
            PercentAllAtomicDefense = medal.PercentAllAtomicDefense,
            PercentAllMentalAttack = medal.PercentAllMentalAttack,
            PercentAllMentalDefense = medal.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Outfits to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Outfits outfit)
    {
        if (outfit == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = outfit.Power,
            Health = outfit.Health,
            PhysicalAttack = outfit.PhysicalAttack,
            PhysicalDefense = outfit.PhysicalDefense,
            MagicalAttack = outfit.MagicalAttack,
            MagicalDefense = outfit.MagicalDefense,
            ChemicalAttack = outfit.ChemicalAttack,
            ChemicalDefense = outfit.ChemicalDefense,
            AtomicAttack = outfit.AtomicAttack,
            AtomicDefense = outfit.AtomicDefense,
            MentalAttack = outfit.MentalAttack,
            MentalDefense = outfit.MentalDefense,

            // Rates & Combat Mechanics
            Speed = outfit.Speed,
            CriticalDamageRate = outfit.CriticalDamageRate,
            CriticalRate = outfit.CriticalRate,
            CriticalResistanceRate = outfit.CriticalResistanceRate,
            IgnoreCriticalRate = outfit.IgnoreCriticalRate,
            PenetrationRate = outfit.PenetrationRate,
            PenetrationResistanceRate = outfit.PenetrationResistanceRate,
            EvasionRate = outfit.EvasionRate,
            DamageAbsorptionRate = outfit.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = outfit.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = outfit.AbsorbedDamageRate,
            VitalityRegenerationRate = outfit.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = outfit.VitalityRegenerationResistanceRate,
            AccuracyRate = outfit.AccuracyRate,
            LifestealRate = outfit.LifestealRate,
            Mana = outfit.Mana,
            ManaRegenerationRate = outfit.ManaRegenerationRate,
            ShieldStrength = outfit.ShieldStrength,
            Tenacity = outfit.Tenacity,
            ResistanceRate = outfit.ResistanceRate,

            // Combo & Control
            ComboRate = outfit.ComboRate,
            IgnoreComboRate = outfit.IgnoreComboRate,
            ComboDamageRate = outfit.ComboDamageRate,
            ComboResistanceRate = outfit.ComboResistanceRate,
            StunRate = outfit.StunRate,
            IgnoreStunRate = outfit.IgnoreStunRate,

            // Reflection
            ReflectionRate = outfit.ReflectionRate,
            IgnoreReflectionRate = outfit.IgnoreReflectionRate,
            ReflectionDamageRate = outfit.ReflectionDamageRate,
            ReflectionResistanceRate = outfit.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = outfit.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = outfit.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = outfit.DamageToSameFactionRate,
            ResistanceToSameFactionRate = outfit.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = outfit.NormalDamageRate,
            NormalResistanceRate = outfit.NormalResistanceRate,
            SkillDamageRate = outfit.SkillDamageRate,
            SkillResistanceRate = outfit.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = outfit.PercentAllHealth,
            PercentAllPhysicalAttack = outfit.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = outfit.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = outfit.PercentAllMagicalAttack,
            PercentAllMagicalDefense = outfit.PercentAllMagicalDefense,
            PercentAllChemicalAttack = outfit.PercentAllChemicalAttack,
            PercentAllChemicalDefense = outfit.PercentAllChemicalDefense,
            PercentAllAtomicAttack = outfit.PercentAllAtomicAttack,
            PercentAllAtomicDefense = outfit.PercentAllAtomicDefense,
            PercentAllMentalAttack = outfit.PercentAllMentalAttack,
            PercentAllMentalDefense = outfit.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Pets to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Pets pet)
    {
        if (pet == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = pet.Power,
            Health = pet.Health,
            PhysicalAttack = pet.PhysicalAttack,
            PhysicalDefense = pet.PhysicalDefense,
            MagicalAttack = pet.MagicalAttack,
            MagicalDefense = pet.MagicalDefense,
            ChemicalAttack = pet.ChemicalAttack,
            ChemicalDefense = pet.ChemicalDefense,
            AtomicAttack = pet.AtomicAttack,
            AtomicDefense = pet.AtomicDefense,
            MentalAttack = pet.MentalAttack,
            MentalDefense = pet.MentalDefense,

            // Rates & Combat Mechanics
            Speed = pet.Speed,
            CriticalDamageRate = pet.CriticalDamageRate,
            CriticalRate = pet.CriticalRate,
            CriticalResistanceRate = pet.CriticalResistanceRate,
            IgnoreCriticalRate = pet.IgnoreCriticalRate,
            PenetrationRate = pet.PenetrationRate,
            PenetrationResistanceRate = pet.PenetrationResistanceRate,
            EvasionRate = pet.EvasionRate,
            DamageAbsorptionRate = pet.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = pet.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = pet.AbsorbedDamageRate,
            VitalityRegenerationRate = pet.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = pet.VitalityRegenerationResistanceRate,
            AccuracyRate = pet.AccuracyRate,
            LifestealRate = pet.LifestealRate,
            Mana = pet.Mana,
            ManaRegenerationRate = pet.ManaRegenerationRate,
            ShieldStrength = pet.ShieldStrength,
            Tenacity = pet.Tenacity,
            ResistanceRate = pet.ResistanceRate,

            // Combo & Control
            ComboRate = pet.ComboRate,
            IgnoreComboRate = pet.IgnoreComboRate,
            ComboDamageRate = pet.ComboDamageRate,
            ComboResistanceRate = pet.ComboResistanceRate,
            StunRate = pet.StunRate,
            IgnoreStunRate = pet.IgnoreStunRate,

            // Reflection
            ReflectionRate = pet.ReflectionRate,
            IgnoreReflectionRate = pet.IgnoreReflectionRate,
            ReflectionDamageRate = pet.ReflectionDamageRate,
            ReflectionResistanceRate = pet.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = pet.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = pet.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = pet.DamageToSameFactionRate,
            ResistanceToSameFactionRate = pet.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = pet.NormalDamageRate,
            NormalResistanceRate = pet.NormalResistanceRate,
            SkillDamageRate = pet.SkillDamageRate,
            SkillResistanceRate = pet.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = pet.PercentAllHealth,
            PercentAllPhysicalAttack = pet.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = pet.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = pet.PercentAllMagicalAttack,
            PercentAllMagicalDefense = pet.PercentAllMagicalDefense,
            PercentAllChemicalAttack = pet.PercentAllChemicalAttack,
            PercentAllChemicalDefense = pet.PercentAllChemicalDefense,
            PercentAllAtomicAttack = pet.PercentAllAtomicAttack,
            PercentAllAtomicDefense = pet.PercentAllAtomicDefense,
            PercentAllMentalAttack = pet.PercentAllMentalAttack,
            PercentAllMentalDefense = pet.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Plants to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Plants plant)
    {
        if (plant == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = plant.Power,
            Health = plant.Health,
            PhysicalAttack = plant.PhysicalAttack,
            PhysicalDefense = plant.PhysicalDefense,
            MagicalAttack = plant.MagicalAttack,
            MagicalDefense = plant.MagicalDefense,
            ChemicalAttack = plant.ChemicalAttack,
            ChemicalDefense = plant.ChemicalDefense,
            AtomicAttack = plant.AtomicAttack,
            AtomicDefense = plant.AtomicDefense,
            MentalAttack = plant.MentalAttack,
            MentalDefense = plant.MentalDefense,

            // Rates & Combat Mechanics
            Speed = plant.Speed,
            CriticalDamageRate = plant.CriticalDamageRate,
            CriticalRate = plant.CriticalRate,
            CriticalResistanceRate = plant.CriticalResistanceRate,
            IgnoreCriticalRate = plant.IgnoreCriticalRate,
            PenetrationRate = plant.PenetrationRate,
            PenetrationResistanceRate = plant.PenetrationResistanceRate,
            EvasionRate = plant.EvasionRate,
            DamageAbsorptionRate = plant.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = plant.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = plant.AbsorbedDamageRate,
            VitalityRegenerationRate = plant.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = plant.VitalityRegenerationResistanceRate,
            AccuracyRate = plant.AccuracyRate,
            LifestealRate = plant.LifestealRate,
            Mana = plant.Mana,
            ManaRegenerationRate = plant.ManaRegenerationRate,
            ShieldStrength = plant.ShieldStrength,
            Tenacity = plant.Tenacity,
            ResistanceRate = plant.ResistanceRate,

            // Combo & Control
            ComboRate = plant.ComboRate,
            IgnoreComboRate = plant.IgnoreComboRate,
            ComboDamageRate = plant.ComboDamageRate,
            ComboResistanceRate = plant.ComboResistanceRate,
            StunRate = plant.StunRate,
            IgnoreStunRate = plant.IgnoreStunRate,

            // Reflection
            ReflectionRate = plant.ReflectionRate,
            IgnoreReflectionRate = plant.IgnoreReflectionRate,
            ReflectionDamageRate = plant.ReflectionDamageRate,
            ReflectionResistanceRate = plant.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = plant.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = plant.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = plant.DamageToSameFactionRate,
            ResistanceToSameFactionRate = plant.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = plant.NormalDamageRate,
            NormalResistanceRate = plant.NormalResistanceRate,
            SkillDamageRate = plant.SkillDamageRate,
            SkillResistanceRate = plant.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = plant.PercentAllHealth,
            PercentAllPhysicalAttack = plant.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = plant.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = plant.PercentAllMagicalAttack,
            PercentAllMagicalDefense = plant.PercentAllMagicalDefense,
            PercentAllChemicalAttack = plant.PercentAllChemicalAttack,
            PercentAllChemicalDefense = plant.PercentAllChemicalDefense,
            PercentAllAtomicAttack = plant.PercentAllAtomicAttack,
            PercentAllAtomicDefense = plant.PercentAllAtomicDefense,
            PercentAllMentalAttack = plant.PercentAllMentalAttack,
            PercentAllMentalDefense = plant.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Puppets to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Puppets puppet)
    {
        if (puppet == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = puppet.Power,
            Health = puppet.Health,
            PhysicalAttack = puppet.PhysicalAttack,
            PhysicalDefense = puppet.PhysicalDefense,
            MagicalAttack = puppet.MagicalAttack,
            MagicalDefense = puppet.MagicalDefense,
            ChemicalAttack = puppet.ChemicalAttack,
            ChemicalDefense = puppet.ChemicalDefense,
            AtomicAttack = puppet.AtomicAttack,
            AtomicDefense = puppet.AtomicDefense,
            MentalAttack = puppet.MentalAttack,
            MentalDefense = puppet.MentalDefense,

            // Rates & Combat Mechanics
            Speed = puppet.Speed,
            CriticalDamageRate = puppet.CriticalDamageRate,
            CriticalRate = puppet.CriticalRate,
            CriticalResistanceRate = puppet.CriticalResistanceRate,
            IgnoreCriticalRate = puppet.IgnoreCriticalRate,
            PenetrationRate = puppet.PenetrationRate,
            PenetrationResistanceRate = puppet.PenetrationResistanceRate,
            EvasionRate = puppet.EvasionRate,
            DamageAbsorptionRate = puppet.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = puppet.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = puppet.AbsorbedDamageRate,
            VitalityRegenerationRate = puppet.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = puppet.VitalityRegenerationResistanceRate,
            AccuracyRate = puppet.AccuracyRate,
            LifestealRate = puppet.LifestealRate,
            Mana = puppet.Mana,
            ManaRegenerationRate = puppet.ManaRegenerationRate,
            ShieldStrength = puppet.ShieldStrength,
            Tenacity = puppet.Tenacity,
            ResistanceRate = puppet.ResistanceRate,

            // Combo & Control
            ComboRate = puppet.ComboRate,
            IgnoreComboRate = puppet.IgnoreComboRate,
            ComboDamageRate = puppet.ComboDamageRate,
            ComboResistanceRate = puppet.ComboResistanceRate,
            StunRate = puppet.StunRate,
            IgnoreStunRate = puppet.IgnoreStunRate,

            // Reflection
            ReflectionRate = puppet.ReflectionRate,
            IgnoreReflectionRate = puppet.IgnoreReflectionRate,
            ReflectionDamageRate = puppet.ReflectionDamageRate,
            ReflectionResistanceRate = puppet.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = puppet.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = puppet.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = puppet.DamageToSameFactionRate,
            ResistanceToSameFactionRate = puppet.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = puppet.NormalDamageRate,
            NormalResistanceRate = puppet.NormalResistanceRate,
            SkillDamageRate = puppet.SkillDamageRate,
            SkillResistanceRate = puppet.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = puppet.PercentAllHealth,
            PercentAllPhysicalAttack = puppet.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = puppet.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = puppet.PercentAllMagicalAttack,
            PercentAllMagicalDefense = puppet.PercentAllMagicalDefense,
            PercentAllChemicalAttack = puppet.PercentAllChemicalAttack,
            PercentAllChemicalDefense = puppet.PercentAllChemicalDefense,
            PercentAllAtomicAttack = puppet.PercentAllAtomicAttack,
            PercentAllAtomicDefense = puppet.PercentAllAtomicDefense,
            PercentAllMentalAttack = puppet.PercentAllMentalAttack,
            PercentAllMentalDefense = puppet.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Relics to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Relics relic)
    {
        if (relic == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = relic.Power,
            Health = relic.Health,
            PhysicalAttack = relic.PhysicalAttack,
            PhysicalDefense = relic.PhysicalDefense,
            MagicalAttack = relic.MagicalAttack,
            MagicalDefense = relic.MagicalDefense,
            ChemicalAttack = relic.ChemicalAttack,
            ChemicalDefense = relic.ChemicalDefense,
            AtomicAttack = relic.AtomicAttack,
            AtomicDefense = relic.AtomicDefense,
            MentalAttack = relic.MentalAttack,
            MentalDefense = relic.MentalDefense,

            // Rates & Combat Mechanics
            Speed = relic.Speed,
            CriticalDamageRate = relic.CriticalDamageRate,
            CriticalRate = relic.CriticalRate,
            CriticalResistanceRate = relic.CriticalResistanceRate,
            IgnoreCriticalRate = relic.IgnoreCriticalRate,
            PenetrationRate = relic.PenetrationRate,
            PenetrationResistanceRate = relic.PenetrationResistanceRate,
            EvasionRate = relic.EvasionRate,
            DamageAbsorptionRate = relic.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = relic.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = relic.AbsorbedDamageRate,
            VitalityRegenerationRate = relic.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = relic.VitalityRegenerationResistanceRate,
            AccuracyRate = relic.AccuracyRate,
            LifestealRate = relic.LifestealRate,
            Mana = relic.Mana,
            ManaRegenerationRate = relic.ManaRegenerationRate,
            ShieldStrength = relic.ShieldStrength,
            Tenacity = relic.Tenacity,
            ResistanceRate = relic.ResistanceRate,

            // Combo & Control
            ComboRate = relic.ComboRate,
            IgnoreComboRate = relic.IgnoreComboRate,
            ComboDamageRate = relic.ComboDamageRate,
            ComboResistanceRate = relic.ComboResistanceRate,
            StunRate = relic.StunRate,
            IgnoreStunRate = relic.IgnoreStunRate,

            // Reflection
            ReflectionRate = relic.ReflectionRate,
            IgnoreReflectionRate = relic.IgnoreReflectionRate,
            ReflectionDamageRate = relic.ReflectionDamageRate,
            ReflectionResistanceRate = relic.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = relic.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = relic.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = relic.DamageToSameFactionRate,
            ResistanceToSameFactionRate = relic.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = relic.NormalDamageRate,
            NormalResistanceRate = relic.NormalResistanceRate,
            SkillDamageRate = relic.SkillDamageRate,
            SkillResistanceRate = relic.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = relic.PercentAllHealth,
            PercentAllPhysicalAttack = relic.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = relic.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = relic.PercentAllMagicalAttack,
            PercentAllMagicalDefense = relic.PercentAllMagicalDefense,
            PercentAllChemicalAttack = relic.PercentAllChemicalAttack,
            PercentAllChemicalDefense = relic.PercentAllChemicalDefense,
            PercentAllAtomicAttack = relic.PercentAllAtomicAttack,
            PercentAllAtomicDefense = relic.PercentAllAtomicDefense,
            PercentAllMentalAttack = relic.PercentAllMentalAttack,
            PercentAllMentalDefense = relic.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Robots to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Robots robot)
    {
        if (robot == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = robot.Power,
            Health = robot.Health,
            PhysicalAttack = robot.PhysicalAttack,
            PhysicalDefense = robot.PhysicalDefense,
            MagicalAttack = robot.MagicalAttack,
            MagicalDefense = robot.MagicalDefense,
            ChemicalAttack = robot.ChemicalAttack,
            ChemicalDefense = robot.ChemicalDefense,
            AtomicAttack = robot.AtomicAttack,
            AtomicDefense = robot.AtomicDefense,
            MentalAttack = robot.MentalAttack,
            MentalDefense = robot.MentalDefense,

            // Rates & Combat Mechanics
            Speed = robot.Speed,
            CriticalDamageRate = robot.CriticalDamageRate,
            CriticalRate = robot.CriticalRate,
            CriticalResistanceRate = robot.CriticalResistanceRate,
            IgnoreCriticalRate = robot.IgnoreCriticalRate,
            PenetrationRate = robot.PenetrationRate,
            PenetrationResistanceRate = robot.PenetrationResistanceRate,
            EvasionRate = robot.EvasionRate,
            DamageAbsorptionRate = robot.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = robot.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = robot.AbsorbedDamageRate,
            VitalityRegenerationRate = robot.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = robot.VitalityRegenerationResistanceRate,
            AccuracyRate = robot.AccuracyRate,
            LifestealRate = robot.LifestealRate,
            Mana = robot.Mana,
            ManaRegenerationRate = robot.ManaRegenerationRate,
            ShieldStrength = robot.ShieldStrength,
            Tenacity = robot.Tenacity,
            ResistanceRate = robot.ResistanceRate,

            // Combo & Control
            ComboRate = robot.ComboRate,
            IgnoreComboRate = robot.IgnoreComboRate,
            ComboDamageRate = robot.ComboDamageRate,
            ComboResistanceRate = robot.ComboResistanceRate,
            StunRate = robot.StunRate,
            IgnoreStunRate = robot.IgnoreStunRate,

            // Reflection
            ReflectionRate = robot.ReflectionRate,
            IgnoreReflectionRate = robot.IgnoreReflectionRate,
            ReflectionDamageRate = robot.ReflectionDamageRate,
            ReflectionResistanceRate = robot.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = robot.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = robot.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = robot.DamageToSameFactionRate,
            ResistanceToSameFactionRate = robot.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = robot.NormalDamageRate,
            NormalResistanceRate = robot.NormalResistanceRate,
            SkillDamageRate = robot.SkillDamageRate,
            SkillResistanceRate = robot.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = robot.PercentAllHealth,
            PercentAllPhysicalAttack = robot.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = robot.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = robot.PercentAllMagicalAttack,
            PercentAllMagicalDefense = robot.PercentAllMagicalDefense,
            PercentAllChemicalAttack = robot.PercentAllChemicalAttack,
            PercentAllChemicalDefense = robot.PercentAllChemicalDefense,
            PercentAllAtomicAttack = robot.PercentAllAtomicAttack,
            PercentAllAtomicDefense = robot.PercentAllAtomicDefense,
            PercentAllMentalAttack = robot.PercentAllMentalAttack,
            PercentAllMentalDefense = robot.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Runes to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Runes rune)
    {
        if (rune == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = rune.Power,
            Health = rune.Health,
            PhysicalAttack = rune.PhysicalAttack,
            PhysicalDefense = rune.PhysicalDefense,
            MagicalAttack = rune.MagicalAttack,
            MagicalDefense = rune.MagicalDefense,
            ChemicalAttack = rune.ChemicalAttack,
            ChemicalDefense = rune.ChemicalDefense,
            AtomicAttack = rune.AtomicAttack,
            AtomicDefense = rune.AtomicDefense,
            MentalAttack = rune.MentalAttack,
            MentalDefense = rune.MentalDefense,

            // Rates & Combat Mechanics
            Speed = rune.Speed,
            CriticalDamageRate = rune.CriticalDamageRate,
            CriticalRate = rune.CriticalRate,
            CriticalResistanceRate = rune.CriticalResistanceRate,
            IgnoreCriticalRate = rune.IgnoreCriticalRate,
            PenetrationRate = rune.PenetrationRate,
            PenetrationResistanceRate = rune.PenetrationResistanceRate,
            EvasionRate = rune.EvasionRate,
            DamageAbsorptionRate = rune.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = rune.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = rune.AbsorbedDamageRate,
            VitalityRegenerationRate = rune.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = rune.VitalityRegenerationResistanceRate,
            AccuracyRate = rune.AccuracyRate,
            LifestealRate = rune.LifestealRate,
            Mana = rune.Mana,
            ManaRegenerationRate = rune.ManaRegenerationRate,
            ShieldStrength = rune.ShieldStrength,
            Tenacity = rune.Tenacity,
            ResistanceRate = rune.ResistanceRate,

            // Combo & Control
            ComboRate = rune.ComboRate,
            IgnoreComboRate = rune.IgnoreComboRate,
            ComboDamageRate = rune.ComboDamageRate,
            ComboResistanceRate = rune.ComboResistanceRate,
            StunRate = rune.StunRate,
            IgnoreStunRate = rune.IgnoreStunRate,

            // Reflection
            ReflectionRate = rune.ReflectionRate,
            IgnoreReflectionRate = rune.IgnoreReflectionRate,
            ReflectionDamageRate = rune.ReflectionDamageRate,
            ReflectionResistanceRate = rune.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = rune.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = rune.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = rune.DamageToSameFactionRate,
            ResistanceToSameFactionRate = rune.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = rune.NormalDamageRate,
            NormalResistanceRate = rune.NormalResistanceRate,
            SkillDamageRate = rune.SkillDamageRate,
            SkillResistanceRate = rune.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = rune.PercentAllHealth,
            PercentAllPhysicalAttack = rune.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = rune.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = rune.PercentAllMagicalAttack,
            PercentAllMagicalDefense = rune.PercentAllMagicalDefense,
            PercentAllChemicalAttack = rune.PercentAllChemicalAttack,
            PercentAllChemicalDefense = rune.PercentAllChemicalDefense,
            PercentAllAtomicAttack = rune.PercentAllAtomicAttack,
            PercentAllAtomicDefense = rune.PercentAllAtomicDefense,
            PercentAllMentalAttack = rune.PercentAllMentalAttack,
            PercentAllMentalDefense = rune.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Skills to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Skills skill)
    {
        if (skill == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = skill.Power,
            Health = skill.Health,
            PhysicalAttack = skill.PhysicalAttack,
            PhysicalDefense = skill.PhysicalDefense,
            MagicalAttack = skill.MagicalAttack,
            MagicalDefense = skill.MagicalDefense,
            ChemicalAttack = skill.ChemicalAttack,
            ChemicalDefense = skill.ChemicalDefense,
            AtomicAttack = skill.AtomicAttack,
            AtomicDefense = skill.AtomicDefense,
            MentalAttack = skill.MentalAttack,
            MentalDefense = skill.MentalDefense,

            // Rates & Combat Mechanics
            Speed = skill.Speed,
            CriticalDamageRate = skill.CriticalDamageRate,
            CriticalRate = skill.CriticalRate,
            CriticalResistanceRate = skill.CriticalResistanceRate,
            IgnoreCriticalRate = skill.IgnoreCriticalRate,
            PenetrationRate = skill.PenetrationRate,
            PenetrationResistanceRate = skill.PenetrationResistanceRate,
            EvasionRate = skill.EvasionRate,
            DamageAbsorptionRate = skill.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = skill.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = skill.AbsorbedDamageRate,
            VitalityRegenerationRate = skill.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = skill.VitalityRegenerationResistanceRate,
            AccuracyRate = skill.AccuracyRate,
            LifestealRate = skill.LifestealRate,
            Mana = skill.Mana,
            ManaRegenerationRate = skill.ManaRegenerationRate,
            ShieldStrength = skill.ShieldStrength,
            Tenacity = skill.Tenacity,
            ResistanceRate = skill.ResistanceRate,

            // Combo & Control
            ComboRate = skill.ComboRate,
            IgnoreComboRate = skill.IgnoreComboRate,
            ComboDamageRate = skill.ComboDamageRate,
            ComboResistanceRate = skill.ComboResistanceRate,
            StunRate = skill.StunRate,
            IgnoreStunRate = skill.IgnoreStunRate,

            // Reflection
            ReflectionRate = skill.ReflectionRate,
            IgnoreReflectionRate = skill.IgnoreReflectionRate,
            ReflectionDamageRate = skill.ReflectionDamageRate,
            ReflectionResistanceRate = skill.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = skill.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = skill.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = skill.DamageToSameFactionRate,
            ResistanceToSameFactionRate = skill.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = skill.NormalDamageRate,
            NormalResistanceRate = skill.NormalResistanceRate,
            SkillDamageRate = skill.SkillDamageRate,
            SkillResistanceRate = skill.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = skill.PercentAllHealth,
            PercentAllPhysicalAttack = skill.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = skill.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = skill.PercentAllMagicalAttack,
            PercentAllMagicalDefense = skill.PercentAllMagicalDefense,
            PercentAllChemicalAttack = skill.PercentAllChemicalAttack,
            PercentAllChemicalDefense = skill.PercentAllChemicalDefense,
            PercentAllAtomicAttack = skill.PercentAllAtomicAttack,
            PercentAllAtomicDefense = skill.PercentAllAtomicDefense,
            PercentAllMentalAttack = skill.PercentAllMentalAttack,
            PercentAllMentalDefense = skill.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from SpiritBeasts to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(SpiritBeasts spiritBeast)
    {
        if (spiritBeast == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = spiritBeast.Power,
            Health = spiritBeast.Health,
            PhysicalAttack = spiritBeast.PhysicalAttack,
            PhysicalDefense = spiritBeast.PhysicalDefense,
            MagicalAttack = spiritBeast.MagicalAttack,
            MagicalDefense = spiritBeast.MagicalDefense,
            ChemicalAttack = spiritBeast.ChemicalAttack,
            ChemicalDefense = spiritBeast.ChemicalDefense,
            AtomicAttack = spiritBeast.AtomicAttack,
            AtomicDefense = spiritBeast.AtomicDefense,
            MentalAttack = spiritBeast.MentalAttack,
            MentalDefense = spiritBeast.MentalDefense,

            // Rates & Combat Mechanics
            Speed = spiritBeast.Speed,
            CriticalDamageRate = spiritBeast.CriticalDamageRate,
            CriticalRate = spiritBeast.CriticalRate,
            CriticalResistanceRate = spiritBeast.CriticalResistanceRate,
            IgnoreCriticalRate = spiritBeast.IgnoreCriticalRate,
            PenetrationRate = spiritBeast.PenetrationRate,
            PenetrationResistanceRate = spiritBeast.PenetrationResistanceRate,
            EvasionRate = spiritBeast.EvasionRate,
            DamageAbsorptionRate = spiritBeast.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = spiritBeast.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = spiritBeast.AbsorbedDamageRate,
            VitalityRegenerationRate = spiritBeast.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = spiritBeast.VitalityRegenerationResistanceRate,
            AccuracyRate = spiritBeast.AccuracyRate,
            LifestealRate = spiritBeast.LifestealRate,
            Mana = spiritBeast.Mana,
            ManaRegenerationRate = spiritBeast.ManaRegenerationRate,
            ShieldStrength = spiritBeast.ShieldStrength,
            Tenacity = spiritBeast.Tenacity,
            ResistanceRate = spiritBeast.ResistanceRate,

            // Combo & Control
            ComboRate = spiritBeast.ComboRate,
            IgnoreComboRate = spiritBeast.IgnoreComboRate,
            ComboDamageRate = spiritBeast.ComboDamageRate,
            ComboResistanceRate = spiritBeast.ComboResistanceRate,
            StunRate = spiritBeast.StunRate,
            IgnoreStunRate = spiritBeast.IgnoreStunRate,

            // Reflection
            ReflectionRate = spiritBeast.ReflectionRate,
            IgnoreReflectionRate = spiritBeast.IgnoreReflectionRate,
            ReflectionDamageRate = spiritBeast.ReflectionDamageRate,
            ReflectionResistanceRate = spiritBeast.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = spiritBeast.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = spiritBeast.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = spiritBeast.DamageToSameFactionRate,
            ResistanceToSameFactionRate = spiritBeast.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = spiritBeast.NormalDamageRate,
            NormalResistanceRate = spiritBeast.NormalResistanceRate,
            SkillDamageRate = spiritBeast.SkillDamageRate,
            SkillResistanceRate = spiritBeast.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = spiritBeast.PercentAllHealth,
            PercentAllPhysicalAttack = spiritBeast.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = spiritBeast.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = spiritBeast.PercentAllMagicalAttack,
            PercentAllMagicalDefense = spiritBeast.PercentAllMagicalDefense,
            PercentAllChemicalAttack = spiritBeast.PercentAllChemicalAttack,
            PercentAllChemicalDefense = spiritBeast.PercentAllChemicalDefense,
            PercentAllAtomicAttack = spiritBeast.PercentAllAtomicAttack,
            PercentAllAtomicDefense = spiritBeast.PercentAllAtomicDefense,
            PercentAllMentalAttack = spiritBeast.PercentAllMentalAttack,
            PercentAllMentalDefense = spiritBeast.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from SpiritCards to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(SpiritCards spiritCard)
    {
        if (spiritCard == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = spiritCard.Power,
            Health = spiritCard.Health,
            PhysicalAttack = spiritCard.PhysicalAttack,
            PhysicalDefense = spiritCard.PhysicalDefense,
            MagicalAttack = spiritCard.MagicalAttack,
            MagicalDefense = spiritCard.MagicalDefense,
            ChemicalAttack = spiritCard.ChemicalAttack,
            ChemicalDefense = spiritCard.ChemicalDefense,
            AtomicAttack = spiritCard.AtomicAttack,
            AtomicDefense = spiritCard.AtomicDefense,
            MentalAttack = spiritCard.MentalAttack,
            MentalDefense = spiritCard.MentalDefense,

            // Rates & Combat Mechanics
            Speed = spiritCard.Speed,
            CriticalDamageRate = spiritCard.CriticalDamageRate,
            CriticalRate = spiritCard.CriticalRate,
            CriticalResistanceRate = spiritCard.CriticalResistanceRate,
            IgnoreCriticalRate = spiritCard.IgnoreCriticalRate,
            PenetrationRate = spiritCard.PenetrationRate,
            PenetrationResistanceRate = spiritCard.PenetrationResistanceRate,
            EvasionRate = spiritCard.EvasionRate,
            DamageAbsorptionRate = spiritCard.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = spiritCard.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = spiritCard.AbsorbedDamageRate,
            VitalityRegenerationRate = spiritCard.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = spiritCard.VitalityRegenerationResistanceRate,
            AccuracyRate = spiritCard.AccuracyRate,
            LifestealRate = spiritCard.LifestealRate,
            Mana = spiritCard.Mana,
            ManaRegenerationRate = spiritCard.ManaRegenerationRate,
            ShieldStrength = spiritCard.ShieldStrength,
            Tenacity = spiritCard.Tenacity,
            ResistanceRate = spiritCard.ResistanceRate,

            // Combo & Control
            ComboRate = spiritCard.ComboRate,
            IgnoreComboRate = spiritCard.IgnoreComboRate,
            ComboDamageRate = spiritCard.ComboDamageRate,
            ComboResistanceRate = spiritCard.ComboResistanceRate,
            StunRate = spiritCard.StunRate,
            IgnoreStunRate = spiritCard.IgnoreStunRate,

            // Reflection
            ReflectionRate = spiritCard.ReflectionRate,
            IgnoreReflectionRate = spiritCard.IgnoreReflectionRate,
            ReflectionDamageRate = spiritCard.ReflectionDamageRate,
            ReflectionResistanceRate = spiritCard.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = spiritCard.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = spiritCard.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = spiritCard.DamageToSameFactionRate,
            ResistanceToSameFactionRate = spiritCard.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = spiritCard.NormalDamageRate,
            NormalResistanceRate = spiritCard.NormalResistanceRate,
            SkillDamageRate = spiritCard.SkillDamageRate,
            SkillResistanceRate = spiritCard.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = spiritCard.PercentAllHealth,
            PercentAllPhysicalAttack = spiritCard.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = spiritCard.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = spiritCard.PercentAllMagicalAttack,
            PercentAllMagicalDefense = spiritCard.PercentAllMagicalDefense,
            PercentAllChemicalAttack = spiritCard.PercentAllChemicalAttack,
            PercentAllChemicalDefense = spiritCard.PercentAllChemicalDefense,
            PercentAllAtomicAttack = spiritCard.PercentAllAtomicAttack,
            PercentAllAtomicDefense = spiritCard.PercentAllAtomicDefense,
            PercentAllMentalAttack = spiritCard.PercentAllMentalAttack,
            PercentAllMentalDefense = spiritCard.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Symbols to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Symbols symbol)
    {
        if (symbol == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = symbol.Power,
            Health = symbol.Health,
            PhysicalAttack = symbol.PhysicalAttack,
            PhysicalDefense = symbol.PhysicalDefense,
            MagicalAttack = symbol.MagicalAttack,
            MagicalDefense = symbol.MagicalDefense,
            ChemicalAttack = symbol.ChemicalAttack,
            ChemicalDefense = symbol.ChemicalDefense,
            AtomicAttack = symbol.AtomicAttack,
            AtomicDefense = symbol.AtomicDefense,
            MentalAttack = symbol.MentalAttack,
            MentalDefense = symbol.MentalDefense,

            // Rates & Combat Mechanics
            Speed = symbol.Speed,
            CriticalDamageRate = symbol.CriticalDamageRate,
            CriticalRate = symbol.CriticalRate,
            CriticalResistanceRate = symbol.CriticalResistanceRate,
            IgnoreCriticalRate = symbol.IgnoreCriticalRate,
            PenetrationRate = symbol.PenetrationRate,
            PenetrationResistanceRate = symbol.PenetrationResistanceRate,
            EvasionRate = symbol.EvasionRate,
            DamageAbsorptionRate = symbol.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = symbol.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = symbol.AbsorbedDamageRate,
            VitalityRegenerationRate = symbol.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = symbol.VitalityRegenerationResistanceRate,
            AccuracyRate = symbol.AccuracyRate,
            LifestealRate = symbol.LifestealRate,
            Mana = symbol.Mana,
            ManaRegenerationRate = symbol.ManaRegenerationRate,
            ShieldStrength = symbol.ShieldStrength,
            Tenacity = symbol.Tenacity,
            ResistanceRate = symbol.ResistanceRate,

            // Combo & Control
            ComboRate = symbol.ComboRate,
            IgnoreComboRate = symbol.IgnoreComboRate,
            ComboDamageRate = symbol.ComboDamageRate,
            ComboResistanceRate = symbol.ComboResistanceRate,
            StunRate = symbol.StunRate,
            IgnoreStunRate = symbol.IgnoreStunRate,

            // Reflection
            ReflectionRate = symbol.ReflectionRate,
            IgnoreReflectionRate = symbol.IgnoreReflectionRate,
            ReflectionDamageRate = symbol.ReflectionDamageRate,
            ReflectionResistanceRate = symbol.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = symbol.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = symbol.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = symbol.DamageToSameFactionRate,
            ResistanceToSameFactionRate = symbol.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = symbol.NormalDamageRate,
            NormalResistanceRate = symbol.NormalResistanceRate,
            SkillDamageRate = symbol.SkillDamageRate,
            SkillResistanceRate = symbol.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = symbol.PercentAllHealth,
            PercentAllPhysicalAttack = symbol.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = symbol.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = symbol.PercentAllMagicalAttack,
            PercentAllMagicalDefense = symbol.PercentAllMagicalDefense,
            PercentAllChemicalAttack = symbol.PercentAllChemicalAttack,
            PercentAllChemicalDefense = symbol.PercentAllChemicalDefense,
            PercentAllAtomicAttack = symbol.PercentAllAtomicAttack,
            PercentAllAtomicDefense = symbol.PercentAllAtomicDefense,
            PercentAllMentalAttack = symbol.PercentAllMentalAttack,
            PercentAllMentalDefense = symbol.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Talismans to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Talismans talisman)
    {
        if (talisman == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = talisman.Power,
            Health = talisman.Health,
            PhysicalAttack = talisman.PhysicalAttack,
            PhysicalDefense = talisman.PhysicalDefense,
            MagicalAttack = talisman.MagicalAttack,
            MagicalDefense = talisman.MagicalDefense,
            ChemicalAttack = talisman.ChemicalAttack,
            ChemicalDefense = talisman.ChemicalDefense,
            AtomicAttack = talisman.AtomicAttack,
            AtomicDefense = talisman.AtomicDefense,
            MentalAttack = talisman.MentalAttack,
            MentalDefense = talisman.MentalDefense,

            // Rates & Combat Mechanics
            Speed = talisman.Speed,
            CriticalDamageRate = talisman.CriticalDamageRate,
            CriticalRate = talisman.CriticalRate,
            CriticalResistanceRate = talisman.CriticalResistanceRate,
            IgnoreCriticalRate = talisman.IgnoreCriticalRate,
            PenetrationRate = talisman.PenetrationRate,
            PenetrationResistanceRate = talisman.PenetrationResistanceRate,
            EvasionRate = talisman.EvasionRate,
            DamageAbsorptionRate = talisman.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = talisman.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = talisman.AbsorbedDamageRate,
            VitalityRegenerationRate = talisman.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = talisman.VitalityRegenerationResistanceRate,
            AccuracyRate = talisman.AccuracyRate,
            LifestealRate = talisman.LifestealRate,
            Mana = talisman.Mana,
            ManaRegenerationRate = talisman.ManaRegenerationRate,
            ShieldStrength = talisman.ShieldStrength,
            Tenacity = talisman.Tenacity,
            ResistanceRate = talisman.ResistanceRate,

            // Combo & Control
            ComboRate = talisman.ComboRate,
            IgnoreComboRate = talisman.IgnoreComboRate,
            ComboDamageRate = talisman.ComboDamageRate,
            ComboResistanceRate = talisman.ComboResistanceRate,
            StunRate = talisman.StunRate,
            IgnoreStunRate = talisman.IgnoreStunRate,

            // Reflection
            ReflectionRate = talisman.ReflectionRate,
            IgnoreReflectionRate = talisman.IgnoreReflectionRate,
            ReflectionDamageRate = talisman.ReflectionDamageRate,
            ReflectionResistanceRate = talisman.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = talisman.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = talisman.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = talisman.DamageToSameFactionRate,
            ResistanceToSameFactionRate = talisman.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = talisman.NormalDamageRate,
            NormalResistanceRate = talisman.NormalResistanceRate,
            SkillDamageRate = talisman.SkillDamageRate,
            SkillResistanceRate = talisman.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = talisman.PercentAllHealth,
            PercentAllPhysicalAttack = talisman.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = talisman.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = talisman.PercentAllMagicalAttack,
            PercentAllMagicalDefense = talisman.PercentAllMagicalDefense,
            PercentAllChemicalAttack = talisman.PercentAllChemicalAttack,
            PercentAllChemicalDefense = talisman.PercentAllChemicalDefense,
            PercentAllAtomicAttack = talisman.PercentAllAtomicAttack,
            PercentAllAtomicDefense = talisman.PercentAllAtomicDefense,
            PercentAllMentalAttack = talisman.PercentAllMentalAttack,
            PercentAllMentalDefense = talisman.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Technologies to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Technologies technology)
    {
        if (technology == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = technology.Power,
            Health = technology.Health,
            PhysicalAttack = technology.PhysicalAttack,
            PhysicalDefense = technology.PhysicalDefense,
            MagicalAttack = technology.MagicalAttack,
            MagicalDefense = technology.MagicalDefense,
            ChemicalAttack = technology.ChemicalAttack,
            ChemicalDefense = technology.ChemicalDefense,
            AtomicAttack = technology.AtomicAttack,
            AtomicDefense = technology.AtomicDefense,
            MentalAttack = technology.MentalAttack,
            MentalDefense = technology.MentalDefense,

            // Rates & Combat Mechanics
            Speed = technology.Speed,
            CriticalDamageRate = technology.CriticalDamageRate,
            CriticalRate = technology.CriticalRate,
            CriticalResistanceRate = technology.CriticalResistanceRate,
            IgnoreCriticalRate = technology.IgnoreCriticalRate,
            PenetrationRate = technology.PenetrationRate,
            PenetrationResistanceRate = technology.PenetrationResistanceRate,
            EvasionRate = technology.EvasionRate,
            DamageAbsorptionRate = technology.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = technology.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = technology.AbsorbedDamageRate,
            VitalityRegenerationRate = technology.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = technology.VitalityRegenerationResistanceRate,
            AccuracyRate = technology.AccuracyRate,
            LifestealRate = technology.LifestealRate,
            Mana = technology.Mana,
            ManaRegenerationRate = technology.ManaRegenerationRate,
            ShieldStrength = technology.ShieldStrength,
            Tenacity = technology.Tenacity,
            ResistanceRate = technology.ResistanceRate,

            // Combo & Control
            ComboRate = technology.ComboRate,
            IgnoreComboRate = technology.IgnoreComboRate,
            ComboDamageRate = technology.ComboDamageRate,
            ComboResistanceRate = technology.ComboResistanceRate,
            StunRate = technology.StunRate,
            IgnoreStunRate = technology.IgnoreStunRate,

            // Reflection
            ReflectionRate = technology.ReflectionRate,
            IgnoreReflectionRate = technology.IgnoreReflectionRate,
            ReflectionDamageRate = technology.ReflectionDamageRate,
            ReflectionResistanceRate = technology.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = technology.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = technology.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = technology.DamageToSameFactionRate,
            ResistanceToSameFactionRate = technology.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = technology.NormalDamageRate,
            NormalResistanceRate = technology.NormalResistanceRate,
            SkillDamageRate = technology.SkillDamageRate,
            SkillResistanceRate = technology.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = technology.PercentAllHealth,
            PercentAllPhysicalAttack = technology.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = technology.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = technology.PercentAllMagicalAttack,
            PercentAllMagicalDefense = technology.PercentAllMagicalDefense,
            PercentAllChemicalAttack = technology.PercentAllChemicalAttack,
            PercentAllChemicalDefense = technology.PercentAllChemicalDefense,
            PercentAllAtomicAttack = technology.PercentAllAtomicAttack,
            PercentAllAtomicDefense = technology.PercentAllAtomicDefense,
            PercentAllMentalAttack = technology.PercentAllMentalAttack,
            PercentAllMentalDefense = technology.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Titles to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Titles title)
    {
        if (title == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = title.Power,
            Health = title.Health,
            PhysicalAttack = title.PhysicalAttack,
            PhysicalDefense = title.PhysicalDefense,
            MagicalAttack = title.MagicalAttack,
            MagicalDefense = title.MagicalDefense,
            ChemicalAttack = title.ChemicalAttack,
            ChemicalDefense = title.ChemicalDefense,
            AtomicAttack = title.AtomicAttack,
            AtomicDefense = title.AtomicDefense,
            MentalAttack = title.MentalAttack,
            MentalDefense = title.MentalDefense,

            // Rates & Combat Mechanics
            Speed = title.Speed,
            CriticalDamageRate = title.CriticalDamageRate,
            CriticalRate = title.CriticalRate,
            CriticalResistanceRate = title.CriticalResistanceRate,
            IgnoreCriticalRate = title.IgnoreCriticalRate,
            PenetrationRate = title.PenetrationRate,
            PenetrationResistanceRate = title.PenetrationResistanceRate,
            EvasionRate = title.EvasionRate,
            DamageAbsorptionRate = title.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = title.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = title.AbsorbedDamageRate,
            VitalityRegenerationRate = title.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = title.VitalityRegenerationResistanceRate,
            AccuracyRate = title.AccuracyRate,
            LifestealRate = title.LifestealRate,
            Mana = title.Mana,
            ManaRegenerationRate = title.ManaRegenerationRate,
            ShieldStrength = title.ShieldStrength,
            Tenacity = title.Tenacity,
            ResistanceRate = title.ResistanceRate,

            // Combo & Control
            ComboRate = title.ComboRate,
            IgnoreComboRate = title.IgnoreComboRate,
            ComboDamageRate = title.ComboDamageRate,
            ComboResistanceRate = title.ComboResistanceRate,
            StunRate = title.StunRate,
            IgnoreStunRate = title.IgnoreStunRate,

            // Reflection
            ReflectionRate = title.ReflectionRate,
            IgnoreReflectionRate = title.IgnoreReflectionRate,
            ReflectionDamageRate = title.ReflectionDamageRate,
            ReflectionResistanceRate = title.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = title.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = title.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = title.DamageToSameFactionRate,
            ResistanceToSameFactionRate = title.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = title.NormalDamageRate,
            NormalResistanceRate = title.NormalResistanceRate,
            SkillDamageRate = title.SkillDamageRate,
            SkillResistanceRate = title.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = title.PercentAllHealth,
            PercentAllPhysicalAttack = title.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = title.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = title.PercentAllMagicalAttack,
            PercentAllMagicalDefense = title.PercentAllMagicalDefense,
            PercentAllChemicalAttack = title.PercentAllChemicalAttack,
            PercentAllChemicalDefense = title.PercentAllChemicalDefense,
            PercentAllAtomicAttack = title.PercentAllAtomicAttack,
            PercentAllAtomicDefense = title.PercentAllAtomicDefense,
            PercentAllMentalAttack = title.PercentAllMentalAttack,
            PercentAllMentalDefense = title.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Vehicles to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Vehicles vehicle)
    {
        if (vehicle == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = vehicle.Power,
            Health = vehicle.Health,
            PhysicalAttack = vehicle.PhysicalAttack,
            PhysicalDefense = vehicle.PhysicalDefense,
            MagicalAttack = vehicle.MagicalAttack,
            MagicalDefense = vehicle.MagicalDefense,
            ChemicalAttack = vehicle.ChemicalAttack,
            ChemicalDefense = vehicle.ChemicalDefense,
            AtomicAttack = vehicle.AtomicAttack,
            AtomicDefense = vehicle.AtomicDefense,
            MentalAttack = vehicle.MentalAttack,
            MentalDefense = vehicle.MentalDefense,

            // Rates & Combat Mechanics
            Speed = vehicle.Speed,
            CriticalDamageRate = vehicle.CriticalDamageRate,
            CriticalRate = vehicle.CriticalRate,
            CriticalResistanceRate = vehicle.CriticalResistanceRate,
            IgnoreCriticalRate = vehicle.IgnoreCriticalRate,
            PenetrationRate = vehicle.PenetrationRate,
            PenetrationResistanceRate = vehicle.PenetrationResistanceRate,
            EvasionRate = vehicle.EvasionRate,
            DamageAbsorptionRate = vehicle.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = vehicle.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = vehicle.AbsorbedDamageRate,
            VitalityRegenerationRate = vehicle.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = vehicle.VitalityRegenerationResistanceRate,
            AccuracyRate = vehicle.AccuracyRate,
            LifestealRate = vehicle.LifestealRate,
            Mana = vehicle.Mana,
            ManaRegenerationRate = vehicle.ManaRegenerationRate,
            ShieldStrength = vehicle.ShieldStrength,
            Tenacity = vehicle.Tenacity,
            ResistanceRate = vehicle.ResistanceRate,

            // Combo & Control
            ComboRate = vehicle.ComboRate,
            IgnoreComboRate = vehicle.IgnoreComboRate,
            ComboDamageRate = vehicle.ComboDamageRate,
            ComboResistanceRate = vehicle.ComboResistanceRate,
            StunRate = vehicle.StunRate,
            IgnoreStunRate = vehicle.IgnoreStunRate,

            // Reflection
            ReflectionRate = vehicle.ReflectionRate,
            IgnoreReflectionRate = vehicle.IgnoreReflectionRate,
            ReflectionDamageRate = vehicle.ReflectionDamageRate,
            ReflectionResistanceRate = vehicle.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = vehicle.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = vehicle.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = vehicle.DamageToSameFactionRate,
            ResistanceToSameFactionRate = vehicle.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = vehicle.NormalDamageRate,
            NormalResistanceRate = vehicle.NormalResistanceRate,
            SkillDamageRate = vehicle.SkillDamageRate,
            SkillResistanceRate = vehicle.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = vehicle.PercentAllHealth,
            PercentAllPhysicalAttack = vehicle.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = vehicle.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = vehicle.PercentAllMagicalAttack,
            PercentAllMagicalDefense = vehicle.PercentAllMagicalDefense,
            PercentAllChemicalAttack = vehicle.PercentAllChemicalAttack,
            PercentAllChemicalDefense = vehicle.PercentAllChemicalDefense,
            PercentAllAtomicAttack = vehicle.PercentAllAtomicAttack,
            PercentAllAtomicDefense = vehicle.PercentAllAtomicDefense,
            PercentAllMentalAttack = vehicle.PercentAllMentalAttack,
            PercentAllMentalDefense = vehicle.PercentAllMentalDefense
        };
    }

    /// <summary>
    /// Allows implicit type casting from Weapons to PowerManager.
    /// </summary>
    public static implicit operator PowerManager(Weapons weapon)
    {
        if (weapon == null) return new PowerManager();

        return new PowerManager
        {
            // Base Stats & Primary Attributes
            Power = weapon.Power,
            Health = weapon.Health,
            PhysicalAttack = weapon.PhysicalAttack,
            PhysicalDefense = weapon.PhysicalDefense,
            MagicalAttack = weapon.MagicalAttack,
            MagicalDefense = weapon.MagicalDefense,
            ChemicalAttack = weapon.ChemicalAttack,
            ChemicalDefense = weapon.ChemicalDefense,
            AtomicAttack = weapon.AtomicAttack,
            AtomicDefense = weapon.AtomicDefense,
            MentalAttack = weapon.MentalAttack,
            MentalDefense = weapon.MentalDefense,

            // Rates & Combat Mechanics
            Speed = weapon.Speed,
            CriticalDamageRate = weapon.CriticalDamageRate,
            CriticalRate = weapon.CriticalRate,
            CriticalResistanceRate = weapon.CriticalResistanceRate,
            IgnoreCriticalRate = weapon.IgnoreCriticalRate,
            PenetrationRate = weapon.PenetrationRate,
            PenetrationResistanceRate = weapon.PenetrationResistanceRate,
            EvasionRate = weapon.EvasionRate,
            DamageAbsorptionRate = weapon.DamageAbsorptionRate,
            IgnoreDamageAbsorptionRate = weapon.IgnoreDamageAbsorptionRate,
            AbsorbedDamageRate = weapon.AbsorbedDamageRate,
            VitalityRegenerationRate = weapon.VitalityRegenerationRate,
            VitalityRegenerationResistanceRate = weapon.VitalityRegenerationResistanceRate,
            AccuracyRate = weapon.AccuracyRate,
            LifestealRate = weapon.LifestealRate,
            Mana = weapon.Mana,
            ManaRegenerationRate = weapon.ManaRegenerationRate,
            ShieldStrength = weapon.ShieldStrength,
            Tenacity = weapon.Tenacity,
            ResistanceRate = weapon.ResistanceRate,

            // Combo & Control
            ComboRate = weapon.ComboRate,
            IgnoreComboRate = weapon.IgnoreComboRate,
            ComboDamageRate = weapon.ComboDamageRate,
            ComboResistanceRate = weapon.ComboResistanceRate,
            StunRate = weapon.StunRate,
            IgnoreStunRate = weapon.IgnoreStunRate,

            // Reflection
            ReflectionRate = weapon.ReflectionRate,
            IgnoreReflectionRate = weapon.IgnoreReflectionRate,
            ReflectionDamageRate = weapon.ReflectionDamageRate,
            ReflectionResistanceRate = weapon.ReflectionResistanceRate,

            // Faction Modifiers
            DamageToDifferentFactionRate = weapon.DamageToDifferentFactionRate,
            ResistanceToDifferentFactionRate = weapon.ResistanceToDifferentFactionRate,
            DamageToSameFactionRate = weapon.DamageToSameFactionRate,
            ResistanceToSameFactionRate = weapon.ResistanceToSameFactionRate,

            // Damage Type Modifiers
            NormalDamageRate = weapon.NormalDamageRate,
            NormalResistanceRate = weapon.NormalResistanceRate,
            SkillDamageRate = weapon.SkillDamageRate,
            SkillResistanceRate = weapon.SkillResistanceRate,

            // Percent Buffs
            PercentAllHealth = weapon.PercentAllHealth,
            PercentAllPhysicalAttack = weapon.PercentAllPhysicalAttack,
            PercentAllPhysicalDefense = weapon.PercentAllPhysicalDefense,
            PercentAllMagicalAttack = weapon.PercentAllMagicalAttack,
            PercentAllMagicalDefense = weapon.PercentAllMagicalDefense,
            PercentAllChemicalAttack = weapon.PercentAllChemicalAttack,
            PercentAllChemicalDefense = weapon.PercentAllChemicalDefense,
            PercentAllAtomicAttack = weapon.PercentAllAtomicAttack,
            PercentAllAtomicDefense = weapon.PercentAllAtomicDefense,
            PercentAllMentalAttack = weapon.PercentAllMentalAttack,
            PercentAllMentalDefense = weapon.PercentAllMentalDefense
        };
    }
}
