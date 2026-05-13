public static class EnhanceHelper
{
    public static Master EnhanceMaster(Master master, int level, int multiplier = 1)
    {
        int startLevel = master.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                master.Health += 10000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                master.PhysicalAttack += 1500000 * statMultiplier;
                master.PhysicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                master.MagicalAttack += 1500000 * statMultiplier;
                master.MagicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                master.ChemicalAttack += 1500000 * statMultiplier;
                master.ChemicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                master.AtomicAttack += 1500000 * statMultiplier;
                master.AtomicDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                master.MentalAttack += 1500000 * statMultiplier;
                master.MentalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                master.Speed += 1500000 * statMultiplier;
                master.CriticalDamageRate += 0.1 * statMultiplier;
                master.CriticalRate += 0.1 * statMultiplier;
                master.CriticalResistanceRate += 0.1 * statMultiplier;
                master.IgnoreCriticalRate += 0.1 * statMultiplier;
                master.PenetrationRate += 0.1 * statMultiplier;
                master.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                master.EvasionRate += 0.1 * statMultiplier;
                master.DamageAbsorptionRate += 0.1 * statMultiplier;
                master.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                master.AbsorbedDamageRate += 0.1 * statMultiplier;
                master.VitalityRegenerationRate += 0.1 * statMultiplier;
                master.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                master.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                master.LifestealRate += 0.1 * statMultiplier;
                master.Mana += 1500000 * statMultiplier;
                master.ManaRegenerationRate += 0.1 * statMultiplier;
                master.ShieldStrength += 1500000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                master.Tenacity += 0.5 * statMultiplier;
                master.ResistanceRate += 0.1 * statMultiplier;
                master.ComboRate += 0.1 * statMultiplier;
                master.IgnoreComboRate += 0.1 * statMultiplier;
                master.ComboDamageRate += 0.1 * statMultiplier;
                master.ComboResistanceRate += 0.1 * statMultiplier;
                master.StunRate += 0.1 * statMultiplier;
                master.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                master.ReflectionRate += 0.1 * statMultiplier;
                master.IgnoreReflectionRate += 0.1 * statMultiplier;
                master.ReflectionDamageRate += 0.1 * statMultiplier;
                master.ReflectionResistanceRate += 0.1 * statMultiplier;
                master.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                master.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                master.DamageToSameFactionRate += 0.1 * statMultiplier;
                master.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                master.NormalDamageRate += 0.1 * statMultiplier;
                master.NormalResistanceRate += 0.1 * statMultiplier;
                master.SkillDamageRate += 0.1 * statMultiplier;
                master.SkillResistanceRate += 0.1 * statMultiplier;
                master.PercentAllHealth += 5 * statMultiplier;
                master.PercentAllPhysicalAttack += 5 * statMultiplier;
                master.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                master.PercentAllMagicalAttack += 5 * statMultiplier;
                master.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                master.PercentAllChemicalAttack += 5 * statMultiplier;
                master.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                master.PercentAllAtomicAttack += 5 * statMultiplier;
                master.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                master.PercentAllMentalAttack += 5 * statMultiplier;
                master.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                master.PhysicalAttack += 1500000 * statMultiplier;
                master.MagicalAttack += 1500000 * statMultiplier;
                master.ChemicalAttack += 1500000 * statMultiplier;
                master.AtomicAttack += 1500000 * statMultiplier;
                master.MentalAttack += 1500000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                master.PhysicalDefense += 1500000 * statMultiplier;
                master.MagicalDefense += 1500000 * statMultiplier;
                master.ChemicalDefense += 1500000 * statMultiplier;
                master.AtomicDefense += 1500000 * statMultiplier;
                master.MentalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                master.Speed += 1500000 * statMultiplier;
                master.CriticalDamageRate += 0.1 * statMultiplier;
                master.CriticalRate += 0.1 * statMultiplier;
                master.PenetrationRate += 0.1 * statMultiplier;
                master.EvasionRate += 0.1 * statMultiplier;
                master.DamageAbsorptionRate += 0.1 * statMultiplier;
                master.VitalityRegenerationRate += 0.1 * statMultiplier;
                master.AccuracyRate += 0.1 * statMultiplier;
                master.LifestealRate += 0.1 * statMultiplier;
                master.Mana += 1500000 * statMultiplier;
                master.ManaRegenerationRate += 0.1 * statMultiplier;
                master.ShieldStrength += 1500000 * statMultiplier;
                master.Tenacity += 0.5 * statMultiplier;
                master.ResistanceRate += 0.1 * statMultiplier;
                master.ComboRate += 0.1 * statMultiplier;
                master.ReflectionRate += 0.1 * statMultiplier;
                master.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                master.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                master.DamageToSameFactionRate += 0.1 * statMultiplier;
                master.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        master.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return master;
    }
    public static Rank EnhanceRank(Rank rank, int level, int multiplier = 1)
    {
        int startLevel = rank.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                rank.Health += 10000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                rank.PhysicalAttack += 1500000 * statMultiplier;
                rank.PhysicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                rank.MagicalAttack += 1500000 * statMultiplier;
                rank.MagicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                rank.ChemicalAttack += 1500000 * statMultiplier;
                rank.ChemicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                rank.AtomicAttack += 1500000 * statMultiplier;
                rank.AtomicDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                rank.MentalAttack += 1500000 * statMultiplier;
                rank.MentalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                rank.Speed += 1500000 * statMultiplier;
                rank.CriticalDamageRate += 0.1 * statMultiplier;
                rank.CriticalRate += 0.1 * statMultiplier;
                rank.CriticalResistanceRate += 0.1 * statMultiplier;
                rank.IgnoreCriticalRate += 0.1 * statMultiplier;
                rank.PenetrationRate += 0.1 * statMultiplier;
                rank.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                rank.EvasionRate += 0.1 * statMultiplier;
                rank.DamageAbsorptionRate += 0.1 * statMultiplier;
                rank.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                rank.AbsorbedDamageRate += 0.1 * statMultiplier;
                rank.VitalityRegenerationRate += 0.1 * statMultiplier;
                rank.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                rank.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                rank.LifestealRate += 0.1 * statMultiplier;
                rank.Mana += 1500000 * statMultiplier;
                rank.ManaRegenerationRate += 0.1 * statMultiplier;
                rank.ShieldStrength += 1500000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                rank.Tenacity += 0.5 * statMultiplier;
                rank.ResistanceRate += 0.1 * statMultiplier;
                rank.ComboRate += 0.1 * statMultiplier;
                rank.IgnoreComboRate += 0.1 * statMultiplier;
                rank.ComboDamageRate += 0.1 * statMultiplier;
                rank.ComboResistanceRate += 0.1 * statMultiplier;
                rank.StunRate += 0.1 * statMultiplier;
                rank.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                rank.ReflectionRate += 0.1 * statMultiplier;
                rank.IgnoreReflectionRate += 0.1 * statMultiplier;
                rank.ReflectionDamageRate += 0.1 * statMultiplier;
                rank.ReflectionResistanceRate += 0.1 * statMultiplier;
                rank.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                rank.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                rank.DamageToSameFactionRate += 0.1 * statMultiplier;
                rank.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                rank.NormalDamageRate += 0.1 * statMultiplier;
                rank.NormalResistanceRate += 0.1 * statMultiplier;
                rank.SkillDamageRate += 0.1 * statMultiplier;
                rank.SkillResistanceRate += 0.1 * statMultiplier;
                rank.PercentAllHealth += 5 * statMultiplier;
                rank.PercentAllPhysicalAttack += 5 * statMultiplier;
                rank.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                rank.PercentAllMagicalAttack += 5 * statMultiplier;
                rank.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                rank.PercentAllChemicalAttack += 5 * statMultiplier;
                rank.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                rank.PercentAllAtomicAttack += 5 * statMultiplier;
                rank.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                rank.PercentAllMentalAttack += 5 * statMultiplier;
                rank.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                rank.PhysicalAttack += 1500000 * statMultiplier;
                rank.MagicalAttack += 1500000 * statMultiplier;
                rank.ChemicalAttack += 1500000 * statMultiplier;
                rank.AtomicAttack += 1500000 * statMultiplier;
                rank.MentalAttack += 1500000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                rank.PhysicalDefense += 1500000 * statMultiplier;
                rank.MagicalDefense += 1500000 * statMultiplier;
                rank.ChemicalDefense += 1500000 * statMultiplier;
                rank.AtomicDefense += 1500000 * statMultiplier;
                rank.MentalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                rank.Speed += 1500000 * statMultiplier;
                rank.CriticalDamageRate += 0.1 * statMultiplier;
                rank.CriticalRate += 0.1 * statMultiplier;
                rank.PenetrationRate += 0.1 * statMultiplier;
                rank.EvasionRate += 0.1 * statMultiplier;
                rank.DamageAbsorptionRate += 0.1 * statMultiplier;
                rank.VitalityRegenerationRate += 0.1 * statMultiplier;
                rank.AccuracyRate += 0.1 * statMultiplier;
                rank.LifestealRate += 0.1 * statMultiplier;
                rank.Mana += 1500000 * statMultiplier;
                rank.ManaRegenerationRate += 0.1 * statMultiplier;
                rank.ShieldStrength += 1500000 * statMultiplier;
                rank.Tenacity += 0.5 * statMultiplier;
                rank.ResistanceRate += 0.1 * statMultiplier;
                rank.ComboRate += 0.1 * statMultiplier;
                rank.ReflectionRate += 0.1 * statMultiplier;
                rank.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                rank.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                rank.DamageToSameFactionRate += 0.1 * statMultiplier;
                rank.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        rank.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return rank;
    }
    public static ScienceFiction EnhanceScienceFiction(ScienceFiction scienceFiction, int level, int multiplier = 1)
    {
        int startLevel = scienceFiction.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                scienceFiction.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                scienceFiction.PhysicalAttack += 15000000 * statMultiplier;
                scienceFiction.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                scienceFiction.MagicalAttack += 15000000 * statMultiplier;
                scienceFiction.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                scienceFiction.ChemicalAttack += 15000000 * statMultiplier;
                scienceFiction.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                scienceFiction.AtomicAttack += 15000000 * statMultiplier;
                scienceFiction.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                scienceFiction.MentalAttack += 15000000 * statMultiplier;
                scienceFiction.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                scienceFiction.Speed += 15000000 * statMultiplier;
                scienceFiction.CriticalDamageRate += 0.1 * statMultiplier;
                scienceFiction.CriticalRate += 0.1 * statMultiplier;
                scienceFiction.CriticalResistanceRate += 0.1 * statMultiplier;
                scienceFiction.IgnoreCriticalRate += 0.1 * statMultiplier;
                scienceFiction.PenetrationRate += 0.1 * statMultiplier;
                scienceFiction.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                scienceFiction.EvasionRate += 0.1 * statMultiplier;
                scienceFiction.DamageAbsorptionRate += 0.1 * statMultiplier;
                scienceFiction.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                scienceFiction.AbsorbedDamageRate += 0.1 * statMultiplier;
                scienceFiction.VitalityRegenerationRate += 0.1 * statMultiplier;
                scienceFiction.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                scienceFiction.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                scienceFiction.LifestealRate += 0.1 * statMultiplier;
                scienceFiction.Mana += 15000000 * statMultiplier;
                scienceFiction.ManaRegenerationRate += 0.1 * statMultiplier;
                scienceFiction.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                scienceFiction.Tenacity += 0.5 * statMultiplier;
                scienceFiction.ResistanceRate += 0.1 * statMultiplier;
                scienceFiction.ComboRate += 0.1 * statMultiplier;
                scienceFiction.IgnoreComboRate += 0.1 * statMultiplier;
                scienceFiction.ComboDamageRate += 0.1 * statMultiplier;
                scienceFiction.ComboResistanceRate += 0.1 * statMultiplier;
                scienceFiction.StunRate += 0.1 * statMultiplier;
                scienceFiction.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                scienceFiction.ReflectionRate += 0.1 * statMultiplier;
                scienceFiction.IgnoreReflectionRate += 0.1 * statMultiplier;
                scienceFiction.ReflectionDamageRate += 0.1 * statMultiplier;
                scienceFiction.ReflectionResistanceRate += 0.1 * statMultiplier;
                scienceFiction.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                scienceFiction.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                scienceFiction.DamageToSameFactionRate += 0.1 * statMultiplier;
                scienceFiction.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                scienceFiction.NormalDamageRate += 0.1 * statMultiplier;
                scienceFiction.NormalResistanceRate += 0.1 * statMultiplier;
                scienceFiction.SkillDamageRate += 0.1 * statMultiplier;
                scienceFiction.SkillResistanceRate += 0.1 * statMultiplier;
                scienceFiction.PercentAllHealth += 5 * statMultiplier;
                scienceFiction.PercentAllPhysicalAttack += 5 * statMultiplier;
                scienceFiction.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                scienceFiction.PercentAllMagicalAttack += 5 * statMultiplier;
                scienceFiction.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                scienceFiction.PercentAllChemicalAttack += 5 * statMultiplier;
                scienceFiction.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                scienceFiction.PercentAllAtomicAttack += 5 * statMultiplier;
                scienceFiction.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                scienceFiction.PercentAllMentalAttack += 5 * statMultiplier;
                scienceFiction.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                scienceFiction.PhysicalAttack += 15000000 * statMultiplier;
                scienceFiction.MagicalAttack += 15000000 * statMultiplier;
                scienceFiction.ChemicalAttack += 15000000 * statMultiplier;
                scienceFiction.AtomicAttack += 15000000 * statMultiplier;
                scienceFiction.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                scienceFiction.PhysicalDefense += 15000000 * statMultiplier;
                scienceFiction.MagicalDefense += 15000000 * statMultiplier;
                scienceFiction.ChemicalDefense += 15000000 * statMultiplier;
                scienceFiction.AtomicDefense += 15000000 * statMultiplier;
                scienceFiction.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                scienceFiction.Speed += 15000000 * statMultiplier;
                scienceFiction.CriticalDamageRate += 0.1 * statMultiplier;
                scienceFiction.CriticalRate += 0.1 * statMultiplier;
                scienceFiction.PenetrationRate += 0.1 * statMultiplier;
                scienceFiction.EvasionRate += 0.1 * statMultiplier;
                scienceFiction.DamageAbsorptionRate += 0.1 * statMultiplier;
                scienceFiction.VitalityRegenerationRate += 0.1 * statMultiplier;
                scienceFiction.AccuracyRate += 0.1 * statMultiplier;
                scienceFiction.LifestealRate += 0.1 * statMultiplier;
                scienceFiction.Mana += 15000000 * statMultiplier;
                scienceFiction.ManaRegenerationRate += 0.1 * statMultiplier;
                scienceFiction.ShieldStrength += 15000000 * statMultiplier;
                scienceFiction.Tenacity += 0.5 * statMultiplier;
                scienceFiction.ResistanceRate += 0.1 * statMultiplier;
                scienceFiction.ComboRate += 0.1 * statMultiplier;
                scienceFiction.ReflectionRate += 0.1 * statMultiplier;
                scienceFiction.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                scienceFiction.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                scienceFiction.DamageToSameFactionRate += 0.1 * statMultiplier;
                scienceFiction.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        scienceFiction.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return scienceFiction;
    }
    public static Archives EnhanceArchives(Archives research, int level, int multiplier = 1)
    {
        int startLevel = research.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                research.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                research.MagicalAttack += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                research.AtomicAttack += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                research.MentalAttack += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.CriticalResistanceRate += 0.1 * statMultiplier;
                research.IgnoreCriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                research.AbsorbedDamageRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.IgnoreComboRate += 0.1 * statMultiplier;
                research.ComboDamageRate += 0.1 * statMultiplier;
                research.ComboResistanceRate += 0.1 * statMultiplier;
                research.StunRate += 0.1 * statMultiplier;
                research.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                research.ReflectionRate += 0.1 * statMultiplier;
                research.IgnoreReflectionRate += 0.1 * statMultiplier;
                research.ReflectionDamageRate += 0.1 * statMultiplier;
                research.ReflectionResistanceRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                research.NormalDamageRate += 0.1 * statMultiplier;
                research.NormalResistanceRate += 0.1 * statMultiplier;
                research.SkillDamageRate += 0.1 * statMultiplier;
                research.SkillResistanceRate += 0.1 * statMultiplier;
                research.PercentAllHealth += 5 * statMultiplier;
                research.PercentAllPhysicalAttack += 5 * statMultiplier;
                research.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                research.PercentAllMagicalAttack += 5 * statMultiplier;
                research.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                research.PercentAllChemicalAttack += 5 * statMultiplier;
                research.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                research.PercentAllAtomicAttack += 5 * statMultiplier;
                research.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                research.PercentAllMentalAttack += 5 * statMultiplier;
                research.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.MagicalAttack += 15000000 * statMultiplier;
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.AtomicAttack += 15000000 * statMultiplier;
                research.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                research.PhysicalDefense += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.ReflectionRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        research.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return research;
    }
    public static Researchs EnhanceResearchs(Researchs research, int level, int multiplier = 1)
    {
        int startLevel = research.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                research.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                research.MagicalAttack += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                research.AtomicAttack += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                research.MentalAttack += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.CriticalResistanceRate += 0.1 * statMultiplier;
                research.IgnoreCriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                research.AbsorbedDamageRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.IgnoreComboRate += 0.1 * statMultiplier;
                research.ComboDamageRate += 0.1 * statMultiplier;
                research.ComboResistanceRate += 0.1 * statMultiplier;
                research.StunRate += 0.1 * statMultiplier;
                research.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                research.ReflectionRate += 0.1 * statMultiplier;
                research.IgnoreReflectionRate += 0.1 * statMultiplier;
                research.ReflectionDamageRate += 0.1 * statMultiplier;
                research.ReflectionResistanceRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                research.NormalDamageRate += 0.1 * statMultiplier;
                research.NormalResistanceRate += 0.1 * statMultiplier;
                research.SkillDamageRate += 0.1 * statMultiplier;
                research.SkillResistanceRate += 0.1 * statMultiplier;
                research.PercentAllHealth += 5 * statMultiplier;
                research.PercentAllPhysicalAttack += 5 * statMultiplier;
                research.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                research.PercentAllMagicalAttack += 5 * statMultiplier;
                research.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                research.PercentAllChemicalAttack += 5 * statMultiplier;
                research.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                research.PercentAllAtomicAttack += 5 * statMultiplier;
                research.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                research.PercentAllMentalAttack += 5 * statMultiplier;
                research.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.MagicalAttack += 15000000 * statMultiplier;
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.AtomicAttack += 15000000 * statMultiplier;
                research.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                research.PhysicalDefense += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.ReflectionRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        research.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return research;
    }
    public static Universes EnhanceUniverses(Universes research, int level, int multiplier = 1)
    {
        int startLevel = research.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                research.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                research.MagicalAttack += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                research.AtomicAttack += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                research.MentalAttack += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.CriticalResistanceRate += 0.1 * statMultiplier;
                research.IgnoreCriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                research.AbsorbedDamageRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.IgnoreComboRate += 0.1 * statMultiplier;
                research.ComboDamageRate += 0.1 * statMultiplier;
                research.ComboResistanceRate += 0.1 * statMultiplier;
                research.StunRate += 0.1 * statMultiplier;
                research.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                research.ReflectionRate += 0.1 * statMultiplier;
                research.IgnoreReflectionRate += 0.1 * statMultiplier;
                research.ReflectionDamageRate += 0.1 * statMultiplier;
                research.ReflectionResistanceRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                research.NormalDamageRate += 0.1 * statMultiplier;
                research.NormalResistanceRate += 0.1 * statMultiplier;
                research.SkillDamageRate += 0.1 * statMultiplier;
                research.SkillResistanceRate += 0.1 * statMultiplier;
                research.PercentAllHealth += 5 * statMultiplier;
                research.PercentAllPhysicalAttack += 5 * statMultiplier;
                research.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                research.PercentAllMagicalAttack += 5 * statMultiplier;
                research.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                research.PercentAllChemicalAttack += 5 * statMultiplier;
                research.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                research.PercentAllAtomicAttack += 5 * statMultiplier;
                research.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                research.PercentAllMentalAttack += 5 * statMultiplier;
                research.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.MagicalAttack += 15000000 * statMultiplier;
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.AtomicAttack += 15000000 * statMultiplier;
                research.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                research.PhysicalDefense += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.ReflectionRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        research.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return research;
    }
    public static HICAs EnhanceHICAs(HICAs research, int level, int multiplier = 1)
    {
        int startLevel = research.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                research.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                research.MagicalAttack += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                research.AtomicAttack += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                research.MentalAttack += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.CriticalResistanceRate += 0.1 * statMultiplier;
                research.IgnoreCriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                research.AbsorbedDamageRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.IgnoreComboRate += 0.1 * statMultiplier;
                research.ComboDamageRate += 0.1 * statMultiplier;
                research.ComboResistanceRate += 0.1 * statMultiplier;
                research.StunRate += 0.1 * statMultiplier;
                research.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                research.ReflectionRate += 0.1 * statMultiplier;
                research.IgnoreReflectionRate += 0.1 * statMultiplier;
                research.ReflectionDamageRate += 0.1 * statMultiplier;
                research.ReflectionResistanceRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                research.NormalDamageRate += 0.1 * statMultiplier;
                research.NormalResistanceRate += 0.1 * statMultiplier;
                research.SkillDamageRate += 0.1 * statMultiplier;
                research.SkillResistanceRate += 0.1 * statMultiplier;
                research.PercentAllHealth += 5 * statMultiplier;
                research.PercentAllPhysicalAttack += 5 * statMultiplier;
                research.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                research.PercentAllMagicalAttack += 5 * statMultiplier;
                research.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                research.PercentAllChemicalAttack += 5 * statMultiplier;
                research.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                research.PercentAllAtomicAttack += 5 * statMultiplier;
                research.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                research.PercentAllMentalAttack += 5 * statMultiplier;
                research.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.MagicalAttack += 15000000 * statMultiplier;
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.AtomicAttack += 15000000 * statMultiplier;
                research.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                research.PhysicalDefense += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.ReflectionRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        research.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return research;
    }
    public static HICBs EnhanceHICBs(HICBs research, int level, int multiplier = 1)
    {
        int startLevel = research.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                research.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                research.MagicalAttack += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                research.AtomicAttack += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                research.MentalAttack += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.CriticalResistanceRate += 0.1 * statMultiplier;
                research.IgnoreCriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                research.AbsorbedDamageRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.IgnoreComboRate += 0.1 * statMultiplier;
                research.ComboDamageRate += 0.1 * statMultiplier;
                research.ComboResistanceRate += 0.1 * statMultiplier;
                research.StunRate += 0.1 * statMultiplier;
                research.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                research.ReflectionRate += 0.1 * statMultiplier;
                research.IgnoreReflectionRate += 0.1 * statMultiplier;
                research.ReflectionDamageRate += 0.1 * statMultiplier;
                research.ReflectionResistanceRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                research.NormalDamageRate += 0.1 * statMultiplier;
                research.NormalResistanceRate += 0.1 * statMultiplier;
                research.SkillDamageRate += 0.1 * statMultiplier;
                research.SkillResistanceRate += 0.1 * statMultiplier;
                research.PercentAllHealth += 5 * statMultiplier;
                research.PercentAllPhysicalAttack += 5 * statMultiplier;
                research.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                research.PercentAllMagicalAttack += 5 * statMultiplier;
                research.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                research.PercentAllChemicalAttack += 5 * statMultiplier;
                research.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                research.PercentAllAtomicAttack += 5 * statMultiplier;
                research.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                research.PercentAllMentalAttack += 5 * statMultiplier;
                research.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.MagicalAttack += 15000000 * statMultiplier;
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.AtomicAttack += 15000000 * statMultiplier;
                research.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                research.PhysicalDefense += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.ReflectionRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        research.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return research;
    }
    public static HIDCs EnhanceHIDCs(HIDCs research, int level, int multiplier = 1)
    {
        int startLevel = research.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                research.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                research.MagicalAttack += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                research.AtomicAttack += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                research.MentalAttack += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.CriticalResistanceRate += 0.1 * statMultiplier;
                research.IgnoreCriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                research.AbsorbedDamageRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.IgnoreComboRate += 0.1 * statMultiplier;
                research.ComboDamageRate += 0.1 * statMultiplier;
                research.ComboResistanceRate += 0.1 * statMultiplier;
                research.StunRate += 0.1 * statMultiplier;
                research.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                research.ReflectionRate += 0.1 * statMultiplier;
                research.IgnoreReflectionRate += 0.1 * statMultiplier;
                research.ReflectionDamageRate += 0.1 * statMultiplier;
                research.ReflectionResistanceRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                research.NormalDamageRate += 0.1 * statMultiplier;
                research.NormalResistanceRate += 0.1 * statMultiplier;
                research.SkillDamageRate += 0.1 * statMultiplier;
                research.SkillResistanceRate += 0.1 * statMultiplier;
                research.PercentAllHealth += 5 * statMultiplier;
                research.PercentAllPhysicalAttack += 5 * statMultiplier;
                research.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                research.PercentAllMagicalAttack += 5 * statMultiplier;
                research.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                research.PercentAllChemicalAttack += 5 * statMultiplier;
                research.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                research.PercentAllAtomicAttack += 5 * statMultiplier;
                research.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                research.PercentAllMentalAttack += 5 * statMultiplier;
                research.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.MagicalAttack += 15000000 * statMultiplier;
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.AtomicAttack += 15000000 * statMultiplier;
                research.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                research.PhysicalDefense += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.ReflectionRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        research.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return research;
    }
    public static HIENs EnhanceHIENs(HIENs research, int level, int multiplier = 1)
    {
        int startLevel = research.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                research.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                research.MagicalAttack += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                research.AtomicAttack += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                research.MentalAttack += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.CriticalResistanceRate += 0.1 * statMultiplier;
                research.IgnoreCriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                research.AbsorbedDamageRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.IgnoreComboRate += 0.1 * statMultiplier;
                research.ComboDamageRate += 0.1 * statMultiplier;
                research.ComboResistanceRate += 0.1 * statMultiplier;
                research.StunRate += 0.1 * statMultiplier;
                research.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                research.ReflectionRate += 0.1 * statMultiplier;
                research.IgnoreReflectionRate += 0.1 * statMultiplier;
                research.ReflectionDamageRate += 0.1 * statMultiplier;
                research.ReflectionResistanceRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                research.NormalDamageRate += 0.1 * statMultiplier;
                research.NormalResistanceRate += 0.1 * statMultiplier;
                research.SkillDamageRate += 0.1 * statMultiplier;
                research.SkillResistanceRate += 0.1 * statMultiplier;
                research.PercentAllHealth += 5 * statMultiplier;
                research.PercentAllPhysicalAttack += 5 * statMultiplier;
                research.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                research.PercentAllMagicalAttack += 5 * statMultiplier;
                research.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                research.PercentAllChemicalAttack += 5 * statMultiplier;
                research.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                research.PercentAllAtomicAttack += 5 * statMultiplier;
                research.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                research.PercentAllMentalAttack += 5 * statMultiplier;
                research.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.MagicalAttack += 15000000 * statMultiplier;
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.AtomicAttack += 15000000 * statMultiplier;
                research.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                research.PhysicalDefense += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.ReflectionRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        research.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return research;
    }
    public static HIHNs EnhanceHIHNs(HIHNs research, int level, int multiplier = 1)
    {
        int startLevel = research.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                research.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                research.MagicalAttack += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                research.AtomicAttack += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                research.MentalAttack += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.CriticalResistanceRate += 0.1 * statMultiplier;
                research.IgnoreCriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                research.AbsorbedDamageRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.IgnoreComboRate += 0.1 * statMultiplier;
                research.ComboDamageRate += 0.1 * statMultiplier;
                research.ComboResistanceRate += 0.1 * statMultiplier;
                research.StunRate += 0.1 * statMultiplier;
                research.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                research.ReflectionRate += 0.1 * statMultiplier;
                research.IgnoreReflectionRate += 0.1 * statMultiplier;
                research.ReflectionDamageRate += 0.1 * statMultiplier;
                research.ReflectionResistanceRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                research.NormalDamageRate += 0.1 * statMultiplier;
                research.NormalResistanceRate += 0.1 * statMultiplier;
                research.SkillDamageRate += 0.1 * statMultiplier;
                research.SkillResistanceRate += 0.1 * statMultiplier;
                research.PercentAllHealth += 5 * statMultiplier;
                research.PercentAllPhysicalAttack += 5 * statMultiplier;
                research.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                research.PercentAllMagicalAttack += 5 * statMultiplier;
                research.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                research.PercentAllChemicalAttack += 5 * statMultiplier;
                research.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                research.PercentAllAtomicAttack += 5 * statMultiplier;
                research.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                research.PercentAllMentalAttack += 5 * statMultiplier;
                research.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.MagicalAttack += 15000000 * statMultiplier;
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.AtomicAttack += 15000000 * statMultiplier;
                research.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                research.PhysicalDefense += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.ReflectionRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        research.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return research;
    }
    public static HIINs EnhanceHIINs(HIINs research, int level, int multiplier = 1)
    {
        int startLevel = research.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                research.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                research.MagicalAttack += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                research.AtomicAttack += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                research.MentalAttack += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.CriticalResistanceRate += 0.1 * statMultiplier;
                research.IgnoreCriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                research.AbsorbedDamageRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.IgnoreComboRate += 0.1 * statMultiplier;
                research.ComboDamageRate += 0.1 * statMultiplier;
                research.ComboResistanceRate += 0.1 * statMultiplier;
                research.StunRate += 0.1 * statMultiplier;
                research.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                research.ReflectionRate += 0.1 * statMultiplier;
                research.IgnoreReflectionRate += 0.1 * statMultiplier;
                research.ReflectionDamageRate += 0.1 * statMultiplier;
                research.ReflectionResistanceRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                research.NormalDamageRate += 0.1 * statMultiplier;
                research.NormalResistanceRate += 0.1 * statMultiplier;
                research.SkillDamageRate += 0.1 * statMultiplier;
                research.SkillResistanceRate += 0.1 * statMultiplier;
                research.PercentAllHealth += 5 * statMultiplier;
                research.PercentAllPhysicalAttack += 5 * statMultiplier;
                research.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                research.PercentAllMagicalAttack += 5 * statMultiplier;
                research.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                research.PercentAllChemicalAttack += 5 * statMultiplier;
                research.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                research.PercentAllAtomicAttack += 5 * statMultiplier;
                research.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                research.PercentAllMentalAttack += 5 * statMultiplier;
                research.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.MagicalAttack += 15000000 * statMultiplier;
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.AtomicAttack += 15000000 * statMultiplier;
                research.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                research.PhysicalDefense += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.ReflectionRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        research.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return research;
    }
    public static HIRNs EnhanceHIRNs(HIRNs research, int level, int multiplier = 1)
    {
        int startLevel = research.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                research.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                research.MagicalAttack += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                research.AtomicAttack += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                research.MentalAttack += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.CriticalResistanceRate += 0.1 * statMultiplier;
                research.IgnoreCriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                research.AbsorbedDamageRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.IgnoreComboRate += 0.1 * statMultiplier;
                research.ComboDamageRate += 0.1 * statMultiplier;
                research.ComboResistanceRate += 0.1 * statMultiplier;
                research.StunRate += 0.1 * statMultiplier;
                research.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                research.ReflectionRate += 0.1 * statMultiplier;
                research.IgnoreReflectionRate += 0.1 * statMultiplier;
                research.ReflectionDamageRate += 0.1 * statMultiplier;
                research.ReflectionResistanceRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                research.NormalDamageRate += 0.1 * statMultiplier;
                research.NormalResistanceRate += 0.1 * statMultiplier;
                research.SkillDamageRate += 0.1 * statMultiplier;
                research.SkillResistanceRate += 0.1 * statMultiplier;
                research.PercentAllHealth += 5 * statMultiplier;
                research.PercentAllPhysicalAttack += 5 * statMultiplier;
                research.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                research.PercentAllMagicalAttack += 5 * statMultiplier;
                research.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                research.PercentAllChemicalAttack += 5 * statMultiplier;
                research.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                research.PercentAllAtomicAttack += 5 * statMultiplier;
                research.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                research.PercentAllMentalAttack += 5 * statMultiplier;
                research.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.MagicalAttack += 15000000 * statMultiplier;
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.AtomicAttack += 15000000 * statMultiplier;
                research.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                research.PhysicalDefense += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.ReflectionRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        research.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return research;
    }
    public static HISNs EnhanceHISNs(HISNs research, int level, int multiplier = 1)
    {
        int startLevel = research.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                research.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                research.MagicalAttack += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                research.AtomicAttack += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                research.MentalAttack += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.CriticalResistanceRate += 0.1 * statMultiplier;
                research.IgnoreCriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                research.AbsorbedDamageRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.IgnoreComboRate += 0.1 * statMultiplier;
                research.ComboDamageRate += 0.1 * statMultiplier;
                research.ComboResistanceRate += 0.1 * statMultiplier;
                research.StunRate += 0.1 * statMultiplier;
                research.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                research.ReflectionRate += 0.1 * statMultiplier;
                research.IgnoreReflectionRate += 0.1 * statMultiplier;
                research.ReflectionDamageRate += 0.1 * statMultiplier;
                research.ReflectionResistanceRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                research.NormalDamageRate += 0.1 * statMultiplier;
                research.NormalResistanceRate += 0.1 * statMultiplier;
                research.SkillDamageRate += 0.1 * statMultiplier;
                research.SkillResistanceRate += 0.1 * statMultiplier;
                research.PercentAllHealth += 5 * statMultiplier;
                research.PercentAllPhysicalAttack += 5 * statMultiplier;
                research.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                research.PercentAllMagicalAttack += 5 * statMultiplier;
                research.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                research.PercentAllChemicalAttack += 5 * statMultiplier;
                research.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                research.PercentAllAtomicAttack += 5 * statMultiplier;
                research.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                research.PercentAllMentalAttack += 5 * statMultiplier;
                research.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.MagicalAttack += 15000000 * statMultiplier;
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.AtomicAttack += 15000000 * statMultiplier;
                research.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                research.PhysicalDefense += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.ReflectionRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        research.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return research;
    }
    public static HITNs EnhanceHITNs(HITNs research, int level, int multiplier = 1)
    {
        int startLevel = research.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                research.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                research.MagicalAttack += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                research.AtomicAttack += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                research.MentalAttack += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.CriticalResistanceRate += 0.1 * statMultiplier;
                research.IgnoreCriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                research.AbsorbedDamageRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.IgnoreComboRate += 0.1 * statMultiplier;
                research.ComboDamageRate += 0.1 * statMultiplier;
                research.ComboResistanceRate += 0.1 * statMultiplier;
                research.StunRate += 0.1 * statMultiplier;
                research.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                research.ReflectionRate += 0.1 * statMultiplier;
                research.IgnoreReflectionRate += 0.1 * statMultiplier;
                research.ReflectionDamageRate += 0.1 * statMultiplier;
                research.ReflectionResistanceRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                research.NormalDamageRate += 0.1 * statMultiplier;
                research.NormalResistanceRate += 0.1 * statMultiplier;
                research.SkillDamageRate += 0.1 * statMultiplier;
                research.SkillResistanceRate += 0.1 * statMultiplier;
                research.PercentAllHealth += 5 * statMultiplier;
                research.PercentAllPhysicalAttack += 5 * statMultiplier;
                research.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                research.PercentAllMagicalAttack += 5 * statMultiplier;
                research.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                research.PercentAllChemicalAttack += 5 * statMultiplier;
                research.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                research.PercentAllAtomicAttack += 5 * statMultiplier;
                research.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                research.PercentAllMentalAttack += 5 * statMultiplier;
                research.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.MagicalAttack += 15000000 * statMultiplier;
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.AtomicAttack += 15000000 * statMultiplier;
                research.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                research.PhysicalDefense += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.ReflectionRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        research.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return research;
    }
    public static SSWNs EnhanceSSWNs(SSWNs research, int level, int multiplier = 1)
    {
        int startLevel = research.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                research.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                research.MagicalAttack += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                research.AtomicAttack += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                research.MentalAttack += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.CriticalResistanceRate += 0.1 * statMultiplier;
                research.IgnoreCriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                research.AbsorbedDamageRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.IgnoreComboRate += 0.1 * statMultiplier;
                research.ComboDamageRate += 0.1 * statMultiplier;
                research.ComboResistanceRate += 0.1 * statMultiplier;
                research.StunRate += 0.1 * statMultiplier;
                research.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                research.ReflectionRate += 0.1 * statMultiplier;
                research.IgnoreReflectionRate += 0.1 * statMultiplier;
                research.ReflectionDamageRate += 0.1 * statMultiplier;
                research.ReflectionResistanceRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                research.NormalDamageRate += 0.1 * statMultiplier;
                research.NormalResistanceRate += 0.1 * statMultiplier;
                research.SkillDamageRate += 0.1 * statMultiplier;
                research.SkillResistanceRate += 0.1 * statMultiplier;
                research.PercentAllHealth += 5 * statMultiplier;
                research.PercentAllPhysicalAttack += 5 * statMultiplier;
                research.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                research.PercentAllMagicalAttack += 5 * statMultiplier;
                research.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                research.PercentAllChemicalAttack += 5 * statMultiplier;
                research.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                research.PercentAllAtomicAttack += 5 * statMultiplier;
                research.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                research.PercentAllMentalAttack += 5 * statMultiplier;
                research.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.MagicalAttack += 15000000 * statMultiplier;
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.AtomicAttack += 15000000 * statMultiplier;
                research.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                research.PhysicalDefense += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.ReflectionRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        research.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return research;
    }
}