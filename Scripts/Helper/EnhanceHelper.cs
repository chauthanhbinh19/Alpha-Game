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
    public static UserScienceFictions EnhanceScienceFictions(UserScienceFictions scienceFiction, int level, double multiplier = 1)
    {
        int startLevel = scienceFiction.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

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
    public static UserArchives EnhanceArchives(UserArchives archive, int level, double multiplier = 1)
    {
        int startLevel = archive.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                archive.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                archive.PhysicalAttack += 15000000 * statMultiplier;
                archive.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                archive.MagicalAttack += 15000000 * statMultiplier;
                archive.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                archive.ChemicalAttack += 15000000 * statMultiplier;
                archive.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                archive.AtomicAttack += 15000000 * statMultiplier;
                archive.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                archive.MentalAttack += 15000000 * statMultiplier;
                archive.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                archive.Speed += 15000000 * statMultiplier;
                archive.CriticalDamageRate += 0.1 * statMultiplier;
                archive.CriticalRate += 0.1 * statMultiplier;
                archive.CriticalResistanceRate += 0.1 * statMultiplier;
                archive.IgnoreCriticalRate += 0.1 * statMultiplier;
                archive.PenetrationRate += 0.1 * statMultiplier;
                archive.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                archive.EvasionRate += 0.1 * statMultiplier;
                archive.DamageAbsorptionRate += 0.1 * statMultiplier;
                archive.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                archive.AbsorbedDamageRate += 0.1 * statMultiplier;
                archive.VitalityRegenerationRate += 0.1 * statMultiplier;
                archive.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                archive.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                archive.LifestealRate += 0.1 * statMultiplier;
                archive.Mana += 15000000 * statMultiplier;
                archive.ManaRegenerationRate += 0.1 * statMultiplier;
                archive.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                archive.Tenacity += 0.5 * statMultiplier;
                archive.ResistanceRate += 0.1 * statMultiplier;
                archive.ComboRate += 0.1 * statMultiplier;
                archive.IgnoreComboRate += 0.1 * statMultiplier;
                archive.ComboDamageRate += 0.1 * statMultiplier;
                archive.ComboResistanceRate += 0.1 * statMultiplier;
                archive.StunRate += 0.1 * statMultiplier;
                archive.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                archive.ReflectionRate += 0.1 * statMultiplier;
                archive.IgnoreReflectionRate += 0.1 * statMultiplier;
                archive.ReflectionDamageRate += 0.1 * statMultiplier;
                archive.ReflectionResistanceRate += 0.1 * statMultiplier;
                archive.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                archive.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                archive.DamageToSameFactionRate += 0.1 * statMultiplier;
                archive.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                archive.NormalDamageRate += 0.1 * statMultiplier;
                archive.NormalResistanceRate += 0.1 * statMultiplier;
                archive.SkillDamageRate += 0.1 * statMultiplier;
                archive.SkillResistanceRate += 0.1 * statMultiplier;
                archive.PercentAllHealth += 5 * statMultiplier;
                archive.PercentAllPhysicalAttack += 5 * statMultiplier;
                archive.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                archive.PercentAllMagicalAttack += 5 * statMultiplier;
                archive.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                archive.PercentAllChemicalAttack += 5 * statMultiplier;
                archive.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                archive.PercentAllAtomicAttack += 5 * statMultiplier;
                archive.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                archive.PercentAllMentalAttack += 5 * statMultiplier;
                archive.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                archive.PhysicalAttack += 15000000 * statMultiplier;
                archive.MagicalAttack += 15000000 * statMultiplier;
                archive.ChemicalAttack += 15000000 * statMultiplier;
                archive.AtomicAttack += 15000000 * statMultiplier;
                archive.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                archive.PhysicalDefense += 15000000 * statMultiplier;
                archive.MagicalDefense += 15000000 * statMultiplier;
                archive.ChemicalDefense += 15000000 * statMultiplier;
                archive.AtomicDefense += 15000000 * statMultiplier;
                archive.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                archive.Speed += 15000000 * statMultiplier;
                archive.CriticalDamageRate += 0.1 * statMultiplier;
                archive.CriticalRate += 0.1 * statMultiplier;
                archive.PenetrationRate += 0.1 * statMultiplier;
                archive.EvasionRate += 0.1 * statMultiplier;
                archive.DamageAbsorptionRate += 0.1 * statMultiplier;
                archive.VitalityRegenerationRate += 0.1 * statMultiplier;
                archive.AccuracyRate += 0.1 * statMultiplier;
                archive.LifestealRate += 0.1 * statMultiplier;
                archive.Mana += 15000000 * statMultiplier;
                archive.ManaRegenerationRate += 0.1 * statMultiplier;
                archive.ShieldStrength += 15000000 * statMultiplier;
                archive.Tenacity += 0.5 * statMultiplier;
                archive.ResistanceRate += 0.1 * statMultiplier;
                archive.ComboRate += 0.1 * statMultiplier;
                archive.ReflectionRate += 0.1 * statMultiplier;
                archive.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                archive.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                archive.DamageToSameFactionRate += 0.1 * statMultiplier;
                archive.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        archive.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return archive;
    }
    public static UserResearchs EnhanceResearchs(UserResearchs research, int level, double multiplier = 1)
    {
        int startLevel = research.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

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
    public static UserUniverses EnhanceUniverses(UserUniverses universe, int level, double multiplier = 1)
    {
        int startLevel = universe.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                universe.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                universe.PhysicalAttack += 15000000 * statMultiplier;
                universe.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                universe.MagicalAttack += 15000000 * statMultiplier;
                universe.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                universe.ChemicalAttack += 15000000 * statMultiplier;
                universe.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                universe.AtomicAttack += 15000000 * statMultiplier;
                universe.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                universe.MentalAttack += 15000000 * statMultiplier;
                universe.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                universe.Speed += 15000000 * statMultiplier;
                universe.CriticalDamageRate += 0.1 * statMultiplier;
                universe.CriticalRate += 0.1 * statMultiplier;
                universe.CriticalResistanceRate += 0.1 * statMultiplier;
                universe.IgnoreCriticalRate += 0.1 * statMultiplier;
                universe.PenetrationRate += 0.1 * statMultiplier;
                universe.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                universe.EvasionRate += 0.1 * statMultiplier;
                universe.DamageAbsorptionRate += 0.1 * statMultiplier;
                universe.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                universe.AbsorbedDamageRate += 0.1 * statMultiplier;
                universe.VitalityRegenerationRate += 0.1 * statMultiplier;
                universe.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                universe.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                universe.LifestealRate += 0.1 * statMultiplier;
                universe.Mana += 15000000 * statMultiplier;
                universe.ManaRegenerationRate += 0.1 * statMultiplier;
                universe.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                universe.Tenacity += 0.5 * statMultiplier;
                universe.ResistanceRate += 0.1 * statMultiplier;
                universe.ComboRate += 0.1 * statMultiplier;
                universe.IgnoreComboRate += 0.1 * statMultiplier;
                universe.ComboDamageRate += 0.1 * statMultiplier;
                universe.ComboResistanceRate += 0.1 * statMultiplier;
                universe.StunRate += 0.1 * statMultiplier;
                universe.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                universe.ReflectionRate += 0.1 * statMultiplier;
                universe.IgnoreReflectionRate += 0.1 * statMultiplier;
                universe.ReflectionDamageRate += 0.1 * statMultiplier;
                universe.ReflectionResistanceRate += 0.1 * statMultiplier;
                universe.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                universe.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                universe.DamageToSameFactionRate += 0.1 * statMultiplier;
                universe.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                universe.NormalDamageRate += 0.1 * statMultiplier;
                universe.NormalResistanceRate += 0.1 * statMultiplier;
                universe.SkillDamageRate += 0.1 * statMultiplier;
                universe.SkillResistanceRate += 0.1 * statMultiplier;
                universe.PercentAllHealth += 5 * statMultiplier;
                universe.PercentAllPhysicalAttack += 5 * statMultiplier;
                universe.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                universe.PercentAllMagicalAttack += 5 * statMultiplier;
                universe.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                universe.PercentAllChemicalAttack += 5 * statMultiplier;
                universe.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                universe.PercentAllAtomicAttack += 5 * statMultiplier;
                universe.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                universe.PercentAllMentalAttack += 5 * statMultiplier;
                universe.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                universe.PhysicalAttack += 15000000 * statMultiplier;
                universe.MagicalAttack += 15000000 * statMultiplier;
                universe.ChemicalAttack += 15000000 * statMultiplier;
                universe.AtomicAttack += 15000000 * statMultiplier;
                universe.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                universe.PhysicalDefense += 15000000 * statMultiplier;
                universe.MagicalDefense += 15000000 * statMultiplier;
                universe.ChemicalDefense += 15000000 * statMultiplier;
                universe.AtomicDefense += 15000000 * statMultiplier;
                universe.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                universe.Speed += 15000000 * statMultiplier;
                universe.CriticalDamageRate += 0.1 * statMultiplier;
                universe.CriticalRate += 0.1 * statMultiplier;
                universe.PenetrationRate += 0.1 * statMultiplier;
                universe.EvasionRate += 0.1 * statMultiplier;
                universe.DamageAbsorptionRate += 0.1 * statMultiplier;
                universe.VitalityRegenerationRate += 0.1 * statMultiplier;
                universe.AccuracyRate += 0.1 * statMultiplier;
                universe.LifestealRate += 0.1 * statMultiplier;
                universe.Mana += 15000000 * statMultiplier;
                universe.ManaRegenerationRate += 0.1 * statMultiplier;
                universe.ShieldStrength += 15000000 * statMultiplier;
                universe.Tenacity += 0.5 * statMultiplier;
                universe.ResistanceRate += 0.1 * statMultiplier;
                universe.ComboRate += 0.1 * statMultiplier;
                universe.ReflectionRate += 0.1 * statMultiplier;
                universe.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                universe.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                universe.DamageToSameFactionRate += 0.1 * statMultiplier;
                universe.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        universe.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return universe;
    }
    public static UserHICAs EnhanceHICAs(UserHICAs hica, int level, double multiplier = 1)
    {
        int startLevel = hica.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                hica.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                hica.PhysicalAttack += 15000000 * statMultiplier;
                hica.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                hica.MagicalAttack += 15000000 * statMultiplier;
                hica.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                hica.ChemicalAttack += 15000000 * statMultiplier;
                hica.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                hica.AtomicAttack += 15000000 * statMultiplier;
                hica.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                hica.MentalAttack += 15000000 * statMultiplier;
                hica.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                hica.Speed += 15000000 * statMultiplier;
                hica.CriticalDamageRate += 0.1 * statMultiplier;
                hica.CriticalRate += 0.1 * statMultiplier;
                hica.CriticalResistanceRate += 0.1 * statMultiplier;
                hica.IgnoreCriticalRate += 0.1 * statMultiplier;
                hica.PenetrationRate += 0.1 * statMultiplier;
                hica.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                hica.EvasionRate += 0.1 * statMultiplier;
                hica.DamageAbsorptionRate += 0.1 * statMultiplier;
                hica.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                hica.AbsorbedDamageRate += 0.1 * statMultiplier;
                hica.VitalityRegenerationRate += 0.1 * statMultiplier;
                hica.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                hica.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                hica.LifestealRate += 0.1 * statMultiplier;
                hica.Mana += 15000000 * statMultiplier;
                hica.ManaRegenerationRate += 0.1 * statMultiplier;
                hica.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                hica.Tenacity += 0.5 * statMultiplier;
                hica.ResistanceRate += 0.1 * statMultiplier;
                hica.ComboRate += 0.1 * statMultiplier;
                hica.IgnoreComboRate += 0.1 * statMultiplier;
                hica.ComboDamageRate += 0.1 * statMultiplier;
                hica.ComboResistanceRate += 0.1 * statMultiplier;
                hica.StunRate += 0.1 * statMultiplier;
                hica.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                hica.ReflectionRate += 0.1 * statMultiplier;
                hica.IgnoreReflectionRate += 0.1 * statMultiplier;
                hica.ReflectionDamageRate += 0.1 * statMultiplier;
                hica.ReflectionResistanceRate += 0.1 * statMultiplier;
                hica.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                hica.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                hica.DamageToSameFactionRate += 0.1 * statMultiplier;
                hica.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                hica.NormalDamageRate += 0.1 * statMultiplier;
                hica.NormalResistanceRate += 0.1 * statMultiplier;
                hica.SkillDamageRate += 0.1 * statMultiplier;
                hica.SkillResistanceRate += 0.1 * statMultiplier;
                hica.PercentAllHealth += 5 * statMultiplier;
                hica.PercentAllPhysicalAttack += 5 * statMultiplier;
                hica.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                hica.PercentAllMagicalAttack += 5 * statMultiplier;
                hica.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                hica.PercentAllChemicalAttack += 5 * statMultiplier;
                hica.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                hica.PercentAllAtomicAttack += 5 * statMultiplier;
                hica.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                hica.PercentAllMentalAttack += 5 * statMultiplier;
                hica.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                hica.PhysicalAttack += 15000000 * statMultiplier;
                hica.MagicalAttack += 15000000 * statMultiplier;
                hica.ChemicalAttack += 15000000 * statMultiplier;
                hica.AtomicAttack += 15000000 * statMultiplier;
                hica.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                hica.PhysicalDefense += 15000000 * statMultiplier;
                hica.MagicalDefense += 15000000 * statMultiplier;
                hica.ChemicalDefense += 15000000 * statMultiplier;
                hica.AtomicDefense += 15000000 * statMultiplier;
                hica.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                hica.Speed += 15000000 * statMultiplier;
                hica.CriticalDamageRate += 0.1 * statMultiplier;
                hica.CriticalRate += 0.1 * statMultiplier;
                hica.PenetrationRate += 0.1 * statMultiplier;
                hica.EvasionRate += 0.1 * statMultiplier;
                hica.DamageAbsorptionRate += 0.1 * statMultiplier;
                hica.VitalityRegenerationRate += 0.1 * statMultiplier;
                hica.AccuracyRate += 0.1 * statMultiplier;
                hica.LifestealRate += 0.1 * statMultiplier;
                hica.Mana += 15000000 * statMultiplier;
                hica.ManaRegenerationRate += 0.1 * statMultiplier;
                hica.ShieldStrength += 15000000 * statMultiplier;
                hica.Tenacity += 0.5 * statMultiplier;
                hica.ResistanceRate += 0.1 * statMultiplier;
                hica.ComboRate += 0.1 * statMultiplier;
                hica.ReflectionRate += 0.1 * statMultiplier;
                hica.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                hica.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                hica.DamageToSameFactionRate += 0.1 * statMultiplier;
                hica.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        hica.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return hica;
    }
    public static UserHICBs EnhanceHICBs(UserHICBs hicb, int level, double multiplier = 1)
    {
        int startLevel = hicb.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                hicb.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                hicb.PhysicalAttack += 15000000 * statMultiplier;
                hicb.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                hicb.MagicalAttack += 15000000 * statMultiplier;
                hicb.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                hicb.ChemicalAttack += 15000000 * statMultiplier;
                hicb.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                hicb.AtomicAttack += 15000000 * statMultiplier;
                hicb.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                hicb.MentalAttack += 15000000 * statMultiplier;
                hicb.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                hicb.Speed += 15000000 * statMultiplier;
                hicb.CriticalDamageRate += 0.1 * statMultiplier;
                hicb.CriticalRate += 0.1 * statMultiplier;
                hicb.CriticalResistanceRate += 0.1 * statMultiplier;
                hicb.IgnoreCriticalRate += 0.1 * statMultiplier;
                hicb.PenetrationRate += 0.1 * statMultiplier;
                hicb.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                hicb.EvasionRate += 0.1 * statMultiplier;
                hicb.DamageAbsorptionRate += 0.1 * statMultiplier;
                hicb.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                hicb.AbsorbedDamageRate += 0.1 * statMultiplier;
                hicb.VitalityRegenerationRate += 0.1 * statMultiplier;
                hicb.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                hicb.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                hicb.LifestealRate += 0.1 * statMultiplier;
                hicb.Mana += 15000000 * statMultiplier;
                hicb.ManaRegenerationRate += 0.1 * statMultiplier;
                hicb.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                hicb.Tenacity += 0.5 * statMultiplier;
                hicb.ResistanceRate += 0.1 * statMultiplier;
                hicb.ComboRate += 0.1 * statMultiplier;
                hicb.IgnoreComboRate += 0.1 * statMultiplier;
                hicb.ComboDamageRate += 0.1 * statMultiplier;
                hicb.ComboResistanceRate += 0.1 * statMultiplier;
                hicb.StunRate += 0.1 * statMultiplier;
                hicb.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                hicb.ReflectionRate += 0.1 * statMultiplier;
                hicb.IgnoreReflectionRate += 0.1 * statMultiplier;
                hicb.ReflectionDamageRate += 0.1 * statMultiplier;
                hicb.ReflectionResistanceRate += 0.1 * statMultiplier;
                hicb.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                hicb.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                hicb.DamageToSameFactionRate += 0.1 * statMultiplier;
                hicb.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                hicb.NormalDamageRate += 0.1 * statMultiplier;
                hicb.NormalResistanceRate += 0.1 * statMultiplier;
                hicb.SkillDamageRate += 0.1 * statMultiplier;
                hicb.SkillResistanceRate += 0.1 * statMultiplier;
                hicb.PercentAllHealth += 5 * statMultiplier;
                hicb.PercentAllPhysicalAttack += 5 * statMultiplier;
                hicb.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                hicb.PercentAllMagicalAttack += 5 * statMultiplier;
                hicb.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                hicb.PercentAllChemicalAttack += 5 * statMultiplier;
                hicb.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                hicb.PercentAllAtomicAttack += 5 * statMultiplier;
                hicb.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                hicb.PercentAllMentalAttack += 5 * statMultiplier;
                hicb.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                hicb.PhysicalAttack += 15000000 * statMultiplier;
                hicb.MagicalAttack += 15000000 * statMultiplier;
                hicb.ChemicalAttack += 15000000 * statMultiplier;
                hicb.AtomicAttack += 15000000 * statMultiplier;
                hicb.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                hicb.PhysicalDefense += 15000000 * statMultiplier;
                hicb.MagicalDefense += 15000000 * statMultiplier;
                hicb.ChemicalDefense += 15000000 * statMultiplier;
                hicb.AtomicDefense += 15000000 * statMultiplier;
                hicb.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                hicb.Speed += 15000000 * statMultiplier;
                hicb.CriticalDamageRate += 0.1 * statMultiplier;
                hicb.CriticalRate += 0.1 * statMultiplier;
                hicb.PenetrationRate += 0.1 * statMultiplier;
                hicb.EvasionRate += 0.1 * statMultiplier;
                hicb.DamageAbsorptionRate += 0.1 * statMultiplier;
                hicb.VitalityRegenerationRate += 0.1 * statMultiplier;
                hicb.AccuracyRate += 0.1 * statMultiplier;
                hicb.LifestealRate += 0.1 * statMultiplier;
                hicb.Mana += 15000000 * statMultiplier;
                hicb.ManaRegenerationRate += 0.1 * statMultiplier;
                hicb.ShieldStrength += 15000000 * statMultiplier;
                hicb.Tenacity += 0.5 * statMultiplier;
                hicb.ResistanceRate += 0.1 * statMultiplier;
                hicb.ComboRate += 0.1 * statMultiplier;
                hicb.ReflectionRate += 0.1 * statMultiplier;
                hicb.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                hicb.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                hicb.DamageToSameFactionRate += 0.1 * statMultiplier;
                hicb.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        hicb.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return hicb;
    }
    public static UserHIDCs EnhanceHIDCs(UserHIDCs hidc, int level, double multiplier = 1)
    {
        int startLevel = hidc.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                hidc.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                hidc.PhysicalAttack += 15000000 * statMultiplier;
                hidc.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                hidc.MagicalAttack += 15000000 * statMultiplier;
                hidc.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                hidc.ChemicalAttack += 15000000 * statMultiplier;
                hidc.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                hidc.AtomicAttack += 15000000 * statMultiplier;
                hidc.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                hidc.MentalAttack += 15000000 * statMultiplier;
                hidc.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                hidc.Speed += 15000000 * statMultiplier;
                hidc.CriticalDamageRate += 0.1 * statMultiplier;
                hidc.CriticalRate += 0.1 * statMultiplier;
                hidc.CriticalResistanceRate += 0.1 * statMultiplier;
                hidc.IgnoreCriticalRate += 0.1 * statMultiplier;
                hidc.PenetrationRate += 0.1 * statMultiplier;
                hidc.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                hidc.EvasionRate += 0.1 * statMultiplier;
                hidc.DamageAbsorptionRate += 0.1 * statMultiplier;
                hidc.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                hidc.AbsorbedDamageRate += 0.1 * statMultiplier;
                hidc.VitalityRegenerationRate += 0.1 * statMultiplier;
                hidc.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                hidc.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                hidc.LifestealRate += 0.1 * statMultiplier;
                hidc.Mana += 15000000 * statMultiplier;
                hidc.ManaRegenerationRate += 0.1 * statMultiplier;
                hidc.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                hidc.Tenacity += 0.5 * statMultiplier;
                hidc.ResistanceRate += 0.1 * statMultiplier;
                hidc.ComboRate += 0.1 * statMultiplier;
                hidc.IgnoreComboRate += 0.1 * statMultiplier;
                hidc.ComboDamageRate += 0.1 * statMultiplier;
                hidc.ComboResistanceRate += 0.1 * statMultiplier;
                hidc.StunRate += 0.1 * statMultiplier;
                hidc.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                hidc.ReflectionRate += 0.1 * statMultiplier;
                hidc.IgnoreReflectionRate += 0.1 * statMultiplier;
                hidc.ReflectionDamageRate += 0.1 * statMultiplier;
                hidc.ReflectionResistanceRate += 0.1 * statMultiplier;
                hidc.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                hidc.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                hidc.DamageToSameFactionRate += 0.1 * statMultiplier;
                hidc.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                hidc.NormalDamageRate += 0.1 * statMultiplier;
                hidc.NormalResistanceRate += 0.1 * statMultiplier;
                hidc.SkillDamageRate += 0.1 * statMultiplier;
                hidc.SkillResistanceRate += 0.1 * statMultiplier;
                hidc.PercentAllHealth += 5 * statMultiplier;
                hidc.PercentAllPhysicalAttack += 5 * statMultiplier;
                hidc.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                hidc.PercentAllMagicalAttack += 5 * statMultiplier;
                hidc.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                hidc.PercentAllChemicalAttack += 5 * statMultiplier;
                hidc.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                hidc.PercentAllAtomicAttack += 5 * statMultiplier;
                hidc.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                hidc.PercentAllMentalAttack += 5 * statMultiplier;
                hidc.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                hidc.PhysicalAttack += 15000000 * statMultiplier;
                hidc.MagicalAttack += 15000000 * statMultiplier;
                hidc.ChemicalAttack += 15000000 * statMultiplier;
                hidc.AtomicAttack += 15000000 * statMultiplier;
                hidc.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                hidc.PhysicalDefense += 15000000 * statMultiplier;
                hidc.MagicalDefense += 15000000 * statMultiplier;
                hidc.ChemicalDefense += 15000000 * statMultiplier;
                hidc.AtomicDefense += 15000000 * statMultiplier;
                hidc.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                hidc.Speed += 15000000 * statMultiplier;
                hidc.CriticalDamageRate += 0.1 * statMultiplier;
                hidc.CriticalRate += 0.1 * statMultiplier;
                hidc.PenetrationRate += 0.1 * statMultiplier;
                hidc.EvasionRate += 0.1 * statMultiplier;
                hidc.DamageAbsorptionRate += 0.1 * statMultiplier;
                hidc.VitalityRegenerationRate += 0.1 * statMultiplier;
                hidc.AccuracyRate += 0.1 * statMultiplier;
                hidc.LifestealRate += 0.1 * statMultiplier;
                hidc.Mana += 15000000 * statMultiplier;
                hidc.ManaRegenerationRate += 0.1 * statMultiplier;
                hidc.ShieldStrength += 15000000 * statMultiplier;
                hidc.Tenacity += 0.5 * statMultiplier;
                hidc.ResistanceRate += 0.1 * statMultiplier;
                hidc.ComboRate += 0.1 * statMultiplier;
                hidc.ReflectionRate += 0.1 * statMultiplier;
                hidc.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                hidc.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                hidc.DamageToSameFactionRate += 0.1 * statMultiplier;
                hidc.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        hidc.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return hidc;
    }
    public static UserHIENs EnhanceHIENs(UserHIENs hien, int level, double multiplier = 1)
    {
        int startLevel = hien.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                hien.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                hien.PhysicalAttack += 15000000 * statMultiplier;
                hien.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                hien.MagicalAttack += 15000000 * statMultiplier;
                hien.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                hien.ChemicalAttack += 15000000 * statMultiplier;
                hien.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                hien.AtomicAttack += 15000000 * statMultiplier;
                hien.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                hien.MentalAttack += 15000000 * statMultiplier;
                hien.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                hien.Speed += 15000000 * statMultiplier;
                hien.CriticalDamageRate += 0.1 * statMultiplier;
                hien.CriticalRate += 0.1 * statMultiplier;
                hien.CriticalResistanceRate += 0.1 * statMultiplier;
                hien.IgnoreCriticalRate += 0.1 * statMultiplier;
                hien.PenetrationRate += 0.1 * statMultiplier;
                hien.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                hien.EvasionRate += 0.1 * statMultiplier;
                hien.DamageAbsorptionRate += 0.1 * statMultiplier;
                hien.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                hien.AbsorbedDamageRate += 0.1 * statMultiplier;
                hien.VitalityRegenerationRate += 0.1 * statMultiplier;
                hien.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                hien.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                hien.LifestealRate += 0.1 * statMultiplier;
                hien.Mana += 15000000 * statMultiplier;
                hien.ManaRegenerationRate += 0.1 * statMultiplier;
                hien.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                hien.Tenacity += 0.5 * statMultiplier;
                hien.ResistanceRate += 0.1 * statMultiplier;
                hien.ComboRate += 0.1 * statMultiplier;
                hien.IgnoreComboRate += 0.1 * statMultiplier;
                hien.ComboDamageRate += 0.1 * statMultiplier;
                hien.ComboResistanceRate += 0.1 * statMultiplier;
                hien.StunRate += 0.1 * statMultiplier;
                hien.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                hien.ReflectionRate += 0.1 * statMultiplier;
                hien.IgnoreReflectionRate += 0.1 * statMultiplier;
                hien.ReflectionDamageRate += 0.1 * statMultiplier;
                hien.ReflectionResistanceRate += 0.1 * statMultiplier;
                hien.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                hien.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                hien.DamageToSameFactionRate += 0.1 * statMultiplier;
                hien.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                hien.NormalDamageRate += 0.1 * statMultiplier;
                hien.NormalResistanceRate += 0.1 * statMultiplier;
                hien.SkillDamageRate += 0.1 * statMultiplier;
                hien.SkillResistanceRate += 0.1 * statMultiplier;
                hien.PercentAllHealth += 5 * statMultiplier;
                hien.PercentAllPhysicalAttack += 5 * statMultiplier;
                hien.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                hien.PercentAllMagicalAttack += 5 * statMultiplier;
                hien.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                hien.PercentAllChemicalAttack += 5 * statMultiplier;
                hien.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                hien.PercentAllAtomicAttack += 5 * statMultiplier;
                hien.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                hien.PercentAllMentalAttack += 5 * statMultiplier;
                hien.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                hien.PhysicalAttack += 15000000 * statMultiplier;
                hien.MagicalAttack += 15000000 * statMultiplier;
                hien.ChemicalAttack += 15000000 * statMultiplier;
                hien.AtomicAttack += 15000000 * statMultiplier;
                hien.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                hien.PhysicalDefense += 15000000 * statMultiplier;
                hien.MagicalDefense += 15000000 * statMultiplier;
                hien.ChemicalDefense += 15000000 * statMultiplier;
                hien.AtomicDefense += 15000000 * statMultiplier;
                hien.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                hien.Speed += 15000000 * statMultiplier;
                hien.CriticalDamageRate += 0.1 * statMultiplier;
                hien.CriticalRate += 0.1 * statMultiplier;
                hien.PenetrationRate += 0.1 * statMultiplier;
                hien.EvasionRate += 0.1 * statMultiplier;
                hien.DamageAbsorptionRate += 0.1 * statMultiplier;
                hien.VitalityRegenerationRate += 0.1 * statMultiplier;
                hien.AccuracyRate += 0.1 * statMultiplier;
                hien.LifestealRate += 0.1 * statMultiplier;
                hien.Mana += 15000000 * statMultiplier;
                hien.ManaRegenerationRate += 0.1 * statMultiplier;
                hien.ShieldStrength += 15000000 * statMultiplier;
                hien.Tenacity += 0.5 * statMultiplier;
                hien.ResistanceRate += 0.1 * statMultiplier;
                hien.ComboRate += 0.1 * statMultiplier;
                hien.ReflectionRate += 0.1 * statMultiplier;
                hien.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                hien.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                hien.DamageToSameFactionRate += 0.1 * statMultiplier;
                hien.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        hien.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return hien;
    }
    public static UserHIHNs EnhanceHIHNs(UserHIHNs hihn, int level, double multiplier = 1)
    {
        int startLevel = hihn.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                hihn.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                hihn.PhysicalAttack += 15000000 * statMultiplier;
                hihn.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                hihn.MagicalAttack += 15000000 * statMultiplier;
                hihn.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                hihn.ChemicalAttack += 15000000 * statMultiplier;
                hihn.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                hihn.AtomicAttack += 15000000 * statMultiplier;
                hihn.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                hihn.MentalAttack += 15000000 * statMultiplier;
                hihn.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                hihn.Speed += 15000000 * statMultiplier;
                hihn.CriticalDamageRate += 0.1 * statMultiplier;
                hihn.CriticalRate += 0.1 * statMultiplier;
                hihn.CriticalResistanceRate += 0.1 * statMultiplier;
                hihn.IgnoreCriticalRate += 0.1 * statMultiplier;
                hihn.PenetrationRate += 0.1 * statMultiplier;
                hihn.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                hihn.EvasionRate += 0.1 * statMultiplier;
                hihn.DamageAbsorptionRate += 0.1 * statMultiplier;
                hihn.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                hihn.AbsorbedDamageRate += 0.1 * statMultiplier;
                hihn.VitalityRegenerationRate += 0.1 * statMultiplier;
                hihn.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                hihn.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                hihn.LifestealRate += 0.1 * statMultiplier;
                hihn.Mana += 15000000 * statMultiplier;
                hihn.ManaRegenerationRate += 0.1 * statMultiplier;
                hihn.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                hihn.Tenacity += 0.5 * statMultiplier;
                hihn.ResistanceRate += 0.1 * statMultiplier;
                hihn.ComboRate += 0.1 * statMultiplier;
                hihn.IgnoreComboRate += 0.1 * statMultiplier;
                hihn.ComboDamageRate += 0.1 * statMultiplier;
                hihn.ComboResistanceRate += 0.1 * statMultiplier;
                hihn.StunRate += 0.1 * statMultiplier;
                hihn.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                hihn.ReflectionRate += 0.1 * statMultiplier;
                hihn.IgnoreReflectionRate += 0.1 * statMultiplier;
                hihn.ReflectionDamageRate += 0.1 * statMultiplier;
                hihn.ReflectionResistanceRate += 0.1 * statMultiplier;
                hihn.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                hihn.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                hihn.DamageToSameFactionRate += 0.1 * statMultiplier;
                hihn.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                hihn.NormalDamageRate += 0.1 * statMultiplier;
                hihn.NormalResistanceRate += 0.1 * statMultiplier;
                hihn.SkillDamageRate += 0.1 * statMultiplier;
                hihn.SkillResistanceRate += 0.1 * statMultiplier;
                hihn.PercentAllHealth += 5 * statMultiplier;
                hihn.PercentAllPhysicalAttack += 5 * statMultiplier;
                hihn.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                hihn.PercentAllMagicalAttack += 5 * statMultiplier;
                hihn.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                hihn.PercentAllChemicalAttack += 5 * statMultiplier;
                hihn.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                hihn.PercentAllAtomicAttack += 5 * statMultiplier;
                hihn.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                hihn.PercentAllMentalAttack += 5 * statMultiplier;
                hihn.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                hihn.PhysicalAttack += 15000000 * statMultiplier;
                hihn.MagicalAttack += 15000000 * statMultiplier;
                hihn.ChemicalAttack += 15000000 * statMultiplier;
                hihn.AtomicAttack += 15000000 * statMultiplier;
                hihn.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                hihn.PhysicalDefense += 15000000 * statMultiplier;
                hihn.MagicalDefense += 15000000 * statMultiplier;
                hihn.ChemicalDefense += 15000000 * statMultiplier;
                hihn.AtomicDefense += 15000000 * statMultiplier;
                hihn.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                hihn.Speed += 15000000 * statMultiplier;
                hihn.CriticalDamageRate += 0.1 * statMultiplier;
                hihn.CriticalRate += 0.1 * statMultiplier;
                hihn.PenetrationRate += 0.1 * statMultiplier;
                hihn.EvasionRate += 0.1 * statMultiplier;
                hihn.DamageAbsorptionRate += 0.1 * statMultiplier;
                hihn.VitalityRegenerationRate += 0.1 * statMultiplier;
                hihn.AccuracyRate += 0.1 * statMultiplier;
                hihn.LifestealRate += 0.1 * statMultiplier;
                hihn.Mana += 15000000 * statMultiplier;
                hihn.ManaRegenerationRate += 0.1 * statMultiplier;
                hihn.ShieldStrength += 15000000 * statMultiplier;
                hihn.Tenacity += 0.5 * statMultiplier;
                hihn.ResistanceRate += 0.1 * statMultiplier;
                hihn.ComboRate += 0.1 * statMultiplier;
                hihn.ReflectionRate += 0.1 * statMultiplier;
                hihn.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                hihn.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                hihn.DamageToSameFactionRate += 0.1 * statMultiplier;
                hihn.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        hihn.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return hihn;
    }
    public static UserHIINs EnhanceHIINs(UserHIINs hiin, int level, double multiplier = 1)
    {
        int startLevel = hiin.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                hiin.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                hiin.PhysicalAttack += 15000000 * statMultiplier;
                hiin.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                hiin.MagicalAttack += 15000000 * statMultiplier;
                hiin.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                hiin.ChemicalAttack += 15000000 * statMultiplier;
                hiin.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                hiin.AtomicAttack += 15000000 * statMultiplier;
                hiin.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                hiin.MentalAttack += 15000000 * statMultiplier;
                hiin.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                hiin.Speed += 15000000 * statMultiplier;
                hiin.CriticalDamageRate += 0.1 * statMultiplier;
                hiin.CriticalRate += 0.1 * statMultiplier;
                hiin.CriticalResistanceRate += 0.1 * statMultiplier;
                hiin.IgnoreCriticalRate += 0.1 * statMultiplier;
                hiin.PenetrationRate += 0.1 * statMultiplier;
                hiin.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                hiin.EvasionRate += 0.1 * statMultiplier;
                hiin.DamageAbsorptionRate += 0.1 * statMultiplier;
                hiin.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                hiin.AbsorbedDamageRate += 0.1 * statMultiplier;
                hiin.VitalityRegenerationRate += 0.1 * statMultiplier;
                hiin.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                hiin.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                hiin.LifestealRate += 0.1 * statMultiplier;
                hiin.Mana += 15000000 * statMultiplier;
                hiin.ManaRegenerationRate += 0.1 * statMultiplier;
                hiin.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                hiin.Tenacity += 0.5 * statMultiplier;
                hiin.ResistanceRate += 0.1 * statMultiplier;
                hiin.ComboRate += 0.1 * statMultiplier;
                hiin.IgnoreComboRate += 0.1 * statMultiplier;
                hiin.ComboDamageRate += 0.1 * statMultiplier;
                hiin.ComboResistanceRate += 0.1 * statMultiplier;
                hiin.StunRate += 0.1 * statMultiplier;
                hiin.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                hiin.ReflectionRate += 0.1 * statMultiplier;
                hiin.IgnoreReflectionRate += 0.1 * statMultiplier;
                hiin.ReflectionDamageRate += 0.1 * statMultiplier;
                hiin.ReflectionResistanceRate += 0.1 * statMultiplier;
                hiin.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                hiin.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                hiin.DamageToSameFactionRate += 0.1 * statMultiplier;
                hiin.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                hiin.NormalDamageRate += 0.1 * statMultiplier;
                hiin.NormalResistanceRate += 0.1 * statMultiplier;
                hiin.SkillDamageRate += 0.1 * statMultiplier;
                hiin.SkillResistanceRate += 0.1 * statMultiplier;
                hiin.PercentAllHealth += 5 * statMultiplier;
                hiin.PercentAllPhysicalAttack += 5 * statMultiplier;
                hiin.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                hiin.PercentAllMagicalAttack += 5 * statMultiplier;
                hiin.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                hiin.PercentAllChemicalAttack += 5 * statMultiplier;
                hiin.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                hiin.PercentAllAtomicAttack += 5 * statMultiplier;
                hiin.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                hiin.PercentAllMentalAttack += 5 * statMultiplier;
                hiin.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                hiin.PhysicalAttack += 15000000 * statMultiplier;
                hiin.MagicalAttack += 15000000 * statMultiplier;
                hiin.ChemicalAttack += 15000000 * statMultiplier;
                hiin.AtomicAttack += 15000000 * statMultiplier;
                hiin.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                hiin.PhysicalDefense += 15000000 * statMultiplier;
                hiin.MagicalDefense += 15000000 * statMultiplier;
                hiin.ChemicalDefense += 15000000 * statMultiplier;
                hiin.AtomicDefense += 15000000 * statMultiplier;
                hiin.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                hiin.Speed += 15000000 * statMultiplier;
                hiin.CriticalDamageRate += 0.1 * statMultiplier;
                hiin.CriticalRate += 0.1 * statMultiplier;
                hiin.PenetrationRate += 0.1 * statMultiplier;
                hiin.EvasionRate += 0.1 * statMultiplier;
                hiin.DamageAbsorptionRate += 0.1 * statMultiplier;
                hiin.VitalityRegenerationRate += 0.1 * statMultiplier;
                hiin.AccuracyRate += 0.1 * statMultiplier;
                hiin.LifestealRate += 0.1 * statMultiplier;
                hiin.Mana += 15000000 * statMultiplier;
                hiin.ManaRegenerationRate += 0.1 * statMultiplier;
                hiin.ShieldStrength += 15000000 * statMultiplier;
                hiin.Tenacity += 0.5 * statMultiplier;
                hiin.ResistanceRate += 0.1 * statMultiplier;
                hiin.ComboRate += 0.1 * statMultiplier;
                hiin.ReflectionRate += 0.1 * statMultiplier;
                hiin.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                hiin.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                hiin.DamageToSameFactionRate += 0.1 * statMultiplier;
                hiin.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        hiin.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return hiin;
    }
    public static UserHIRNs EnhanceHIRNs(UserHIRNs hirn, int level, double multiplier = 1)
    {
        int startLevel = hirn.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                hirn.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                hirn.PhysicalAttack += 15000000 * statMultiplier;
                hirn.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                hirn.MagicalAttack += 15000000 * statMultiplier;
                hirn.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                hirn.ChemicalAttack += 15000000 * statMultiplier;
                hirn.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                hirn.AtomicAttack += 15000000 * statMultiplier;
                hirn.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                hirn.MentalAttack += 15000000 * statMultiplier;
                hirn.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                hirn.Speed += 15000000 * statMultiplier;
                hirn.CriticalDamageRate += 0.1 * statMultiplier;
                hirn.CriticalRate += 0.1 * statMultiplier;
                hirn.CriticalResistanceRate += 0.1 * statMultiplier;
                hirn.IgnoreCriticalRate += 0.1 * statMultiplier;
                hirn.PenetrationRate += 0.1 * statMultiplier;
                hirn.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                hirn.EvasionRate += 0.1 * statMultiplier;
                hirn.DamageAbsorptionRate += 0.1 * statMultiplier;
                hirn.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                hirn.AbsorbedDamageRate += 0.1 * statMultiplier;
                hirn.VitalityRegenerationRate += 0.1 * statMultiplier;
                hirn.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                hirn.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                hirn.LifestealRate += 0.1 * statMultiplier;
                hirn.Mana += 15000000 * statMultiplier;
                hirn.ManaRegenerationRate += 0.1 * statMultiplier;
                hirn.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                hirn.Tenacity += 0.5 * statMultiplier;
                hirn.ResistanceRate += 0.1 * statMultiplier;
                hirn.ComboRate += 0.1 * statMultiplier;
                hirn.IgnoreComboRate += 0.1 * statMultiplier;
                hirn.ComboDamageRate += 0.1 * statMultiplier;
                hirn.ComboResistanceRate += 0.1 * statMultiplier;
                hirn.StunRate += 0.1 * statMultiplier;
                hirn.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                hirn.ReflectionRate += 0.1 * statMultiplier;
                hirn.IgnoreReflectionRate += 0.1 * statMultiplier;
                hirn.ReflectionDamageRate += 0.1 * statMultiplier;
                hirn.ReflectionResistanceRate += 0.1 * statMultiplier;
                hirn.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                hirn.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                hirn.DamageToSameFactionRate += 0.1 * statMultiplier;
                hirn.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                hirn.NormalDamageRate += 0.1 * statMultiplier;
                hirn.NormalResistanceRate += 0.1 * statMultiplier;
                hirn.SkillDamageRate += 0.1 * statMultiplier;
                hirn.SkillResistanceRate += 0.1 * statMultiplier;
                hirn.PercentAllHealth += 5 * statMultiplier;
                hirn.PercentAllPhysicalAttack += 5 * statMultiplier;
                hirn.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                hirn.PercentAllMagicalAttack += 5 * statMultiplier;
                hirn.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                hirn.PercentAllChemicalAttack += 5 * statMultiplier;
                hirn.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                hirn.PercentAllAtomicAttack += 5 * statMultiplier;
                hirn.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                hirn.PercentAllMentalAttack += 5 * statMultiplier;
                hirn.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                hirn.PhysicalAttack += 15000000 * statMultiplier;
                hirn.MagicalAttack += 15000000 * statMultiplier;
                hirn.ChemicalAttack += 15000000 * statMultiplier;
                hirn.AtomicAttack += 15000000 * statMultiplier;
                hirn.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                hirn.PhysicalDefense += 15000000 * statMultiplier;
                hirn.MagicalDefense += 15000000 * statMultiplier;
                hirn.ChemicalDefense += 15000000 * statMultiplier;
                hirn.AtomicDefense += 15000000 * statMultiplier;
                hirn.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                hirn.Speed += 15000000 * statMultiplier;
                hirn.CriticalDamageRate += 0.1 * statMultiplier;
                hirn.CriticalRate += 0.1 * statMultiplier;
                hirn.PenetrationRate += 0.1 * statMultiplier;
                hirn.EvasionRate += 0.1 * statMultiplier;
                hirn.DamageAbsorptionRate += 0.1 * statMultiplier;
                hirn.VitalityRegenerationRate += 0.1 * statMultiplier;
                hirn.AccuracyRate += 0.1 * statMultiplier;
                hirn.LifestealRate += 0.1 * statMultiplier;
                hirn.Mana += 15000000 * statMultiplier;
                hirn.ManaRegenerationRate += 0.1 * statMultiplier;
                hirn.ShieldStrength += 15000000 * statMultiplier;
                hirn.Tenacity += 0.5 * statMultiplier;
                hirn.ResistanceRate += 0.1 * statMultiplier;
                hirn.ComboRate += 0.1 * statMultiplier;
                hirn.ReflectionRate += 0.1 * statMultiplier;
                hirn.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                hirn.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                hirn.DamageToSameFactionRate += 0.1 * statMultiplier;
                hirn.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        hirn.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return hirn;
    }
    public static UserHISNs EnhanceHISNs(UserHISNs hisn, int level, double multiplier = 1)
    {
        int startLevel = hisn.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                hisn.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                hisn.PhysicalAttack += 15000000 * statMultiplier;
                hisn.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                hisn.MagicalAttack += 15000000 * statMultiplier;
                hisn.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                hisn.ChemicalAttack += 15000000 * statMultiplier;
                hisn.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                hisn.AtomicAttack += 15000000 * statMultiplier;
                hisn.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                hisn.MentalAttack += 15000000 * statMultiplier;
                hisn.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                hisn.Speed += 15000000 * statMultiplier;
                hisn.CriticalDamageRate += 0.1 * statMultiplier;
                hisn.CriticalRate += 0.1 * statMultiplier;
                hisn.CriticalResistanceRate += 0.1 * statMultiplier;
                hisn.IgnoreCriticalRate += 0.1 * statMultiplier;
                hisn.PenetrationRate += 0.1 * statMultiplier;
                hisn.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                hisn.EvasionRate += 0.1 * statMultiplier;
                hisn.DamageAbsorptionRate += 0.1 * statMultiplier;
                hisn.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                hisn.AbsorbedDamageRate += 0.1 * statMultiplier;
                hisn.VitalityRegenerationRate += 0.1 * statMultiplier;
                hisn.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                hisn.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                hisn.LifestealRate += 0.1 * statMultiplier;
                hisn.Mana += 15000000 * statMultiplier;
                hisn.ManaRegenerationRate += 0.1 * statMultiplier;
                hisn.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                hisn.Tenacity += 0.5 * statMultiplier;
                hisn.ResistanceRate += 0.1 * statMultiplier;
                hisn.ComboRate += 0.1 * statMultiplier;
                hisn.IgnoreComboRate += 0.1 * statMultiplier;
                hisn.ComboDamageRate += 0.1 * statMultiplier;
                hisn.ComboResistanceRate += 0.1 * statMultiplier;
                hisn.StunRate += 0.1 * statMultiplier;
                hisn.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                hisn.ReflectionRate += 0.1 * statMultiplier;
                hisn.IgnoreReflectionRate += 0.1 * statMultiplier;
                hisn.ReflectionDamageRate += 0.1 * statMultiplier;
                hisn.ReflectionResistanceRate += 0.1 * statMultiplier;
                hisn.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                hisn.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                hisn.DamageToSameFactionRate += 0.1 * statMultiplier;
                hisn.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                hisn.NormalDamageRate += 0.1 * statMultiplier;
                hisn.NormalResistanceRate += 0.1 * statMultiplier;
                hisn.SkillDamageRate += 0.1 * statMultiplier;
                hisn.SkillResistanceRate += 0.1 * statMultiplier;
                hisn.PercentAllHealth += 5 * statMultiplier;
                hisn.PercentAllPhysicalAttack += 5 * statMultiplier;
                hisn.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                hisn.PercentAllMagicalAttack += 5 * statMultiplier;
                hisn.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                hisn.PercentAllChemicalAttack += 5 * statMultiplier;
                hisn.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                hisn.PercentAllAtomicAttack += 5 * statMultiplier;
                hisn.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                hisn.PercentAllMentalAttack += 5 * statMultiplier;
                hisn.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                hisn.PhysicalAttack += 15000000 * statMultiplier;
                hisn.MagicalAttack += 15000000 * statMultiplier;
                hisn.ChemicalAttack += 15000000 * statMultiplier;
                hisn.AtomicAttack += 15000000 * statMultiplier;
                hisn.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                hisn.PhysicalDefense += 15000000 * statMultiplier;
                hisn.MagicalDefense += 15000000 * statMultiplier;
                hisn.ChemicalDefense += 15000000 * statMultiplier;
                hisn.AtomicDefense += 15000000 * statMultiplier;
                hisn.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                hisn.Speed += 15000000 * statMultiplier;
                hisn.CriticalDamageRate += 0.1 * statMultiplier;
                hisn.CriticalRate += 0.1 * statMultiplier;
                hisn.PenetrationRate += 0.1 * statMultiplier;
                hisn.EvasionRate += 0.1 * statMultiplier;
                hisn.DamageAbsorptionRate += 0.1 * statMultiplier;
                hisn.VitalityRegenerationRate += 0.1 * statMultiplier;
                hisn.AccuracyRate += 0.1 * statMultiplier;
                hisn.LifestealRate += 0.1 * statMultiplier;
                hisn.Mana += 15000000 * statMultiplier;
                hisn.ManaRegenerationRate += 0.1 * statMultiplier;
                hisn.ShieldStrength += 15000000 * statMultiplier;
                hisn.Tenacity += 0.5 * statMultiplier;
                hisn.ResistanceRate += 0.1 * statMultiplier;
                hisn.ComboRate += 0.1 * statMultiplier;
                hisn.ReflectionRate += 0.1 * statMultiplier;
                hisn.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                hisn.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                hisn.DamageToSameFactionRate += 0.1 * statMultiplier;
                hisn.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        hisn.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return hisn;
    }
    public static UserHITNs EnhanceHITNs(UserHITNs hitn, int level, double multiplier = 1)
    {
        int startLevel = hitn.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                hitn.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                hitn.PhysicalAttack += 15000000 * statMultiplier;
                hitn.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                hitn.MagicalAttack += 15000000 * statMultiplier;
                hitn.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                hitn.ChemicalAttack += 15000000 * statMultiplier;
                hitn.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                hitn.AtomicAttack += 15000000 * statMultiplier;
                hitn.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                hitn.MentalAttack += 15000000 * statMultiplier;
                hitn.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                hitn.Speed += 15000000 * statMultiplier;
                hitn.CriticalDamageRate += 0.1 * statMultiplier;
                hitn.CriticalRate += 0.1 * statMultiplier;
                hitn.CriticalResistanceRate += 0.1 * statMultiplier;
                hitn.IgnoreCriticalRate += 0.1 * statMultiplier;
                hitn.PenetrationRate += 0.1 * statMultiplier;
                hitn.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                hitn.EvasionRate += 0.1 * statMultiplier;
                hitn.DamageAbsorptionRate += 0.1 * statMultiplier;
                hitn.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                hitn.AbsorbedDamageRate += 0.1 * statMultiplier;
                hitn.VitalityRegenerationRate += 0.1 * statMultiplier;
                hitn.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                hitn.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                hitn.LifestealRate += 0.1 * statMultiplier;
                hitn.Mana += 15000000 * statMultiplier;
                hitn.ManaRegenerationRate += 0.1 * statMultiplier;
                hitn.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                hitn.Tenacity += 0.5 * statMultiplier;
                hitn.ResistanceRate += 0.1 * statMultiplier;
                hitn.ComboRate += 0.1 * statMultiplier;
                hitn.IgnoreComboRate += 0.1 * statMultiplier;
                hitn.ComboDamageRate += 0.1 * statMultiplier;
                hitn.ComboResistanceRate += 0.1 * statMultiplier;
                hitn.StunRate += 0.1 * statMultiplier;
                hitn.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                hitn.ReflectionRate += 0.1 * statMultiplier;
                hitn.IgnoreReflectionRate += 0.1 * statMultiplier;
                hitn.ReflectionDamageRate += 0.1 * statMultiplier;
                hitn.ReflectionResistanceRate += 0.1 * statMultiplier;
                hitn.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                hitn.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                hitn.DamageToSameFactionRate += 0.1 * statMultiplier;
                hitn.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                hitn.NormalDamageRate += 0.1 * statMultiplier;
                hitn.NormalResistanceRate += 0.1 * statMultiplier;
                hitn.SkillDamageRate += 0.1 * statMultiplier;
                hitn.SkillResistanceRate += 0.1 * statMultiplier;
                hitn.PercentAllHealth += 5 * statMultiplier;
                hitn.PercentAllPhysicalAttack += 5 * statMultiplier;
                hitn.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                hitn.PercentAllMagicalAttack += 5 * statMultiplier;
                hitn.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                hitn.PercentAllChemicalAttack += 5 * statMultiplier;
                hitn.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                hitn.PercentAllAtomicAttack += 5 * statMultiplier;
                hitn.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                hitn.PercentAllMentalAttack += 5 * statMultiplier;
                hitn.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                hitn.PhysicalAttack += 15000000 * statMultiplier;
                hitn.MagicalAttack += 15000000 * statMultiplier;
                hitn.ChemicalAttack += 15000000 * statMultiplier;
                hitn.AtomicAttack += 15000000 * statMultiplier;
                hitn.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                hitn.PhysicalDefense += 15000000 * statMultiplier;
                hitn.MagicalDefense += 15000000 * statMultiplier;
                hitn.ChemicalDefense += 15000000 * statMultiplier;
                hitn.AtomicDefense += 15000000 * statMultiplier;
                hitn.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                hitn.Speed += 15000000 * statMultiplier;
                hitn.CriticalDamageRate += 0.1 * statMultiplier;
                hitn.CriticalRate += 0.1 * statMultiplier;
                hitn.PenetrationRate += 0.1 * statMultiplier;
                hitn.EvasionRate += 0.1 * statMultiplier;
                hitn.DamageAbsorptionRate += 0.1 * statMultiplier;
                hitn.VitalityRegenerationRate += 0.1 * statMultiplier;
                hitn.AccuracyRate += 0.1 * statMultiplier;
                hitn.LifestealRate += 0.1 * statMultiplier;
                hitn.Mana += 15000000 * statMultiplier;
                hitn.ManaRegenerationRate += 0.1 * statMultiplier;
                hitn.ShieldStrength += 15000000 * statMultiplier;
                hitn.Tenacity += 0.5 * statMultiplier;
                hitn.ResistanceRate += 0.1 * statMultiplier;
                hitn.ComboRate += 0.1 * statMultiplier;
                hitn.ReflectionRate += 0.1 * statMultiplier;
                hitn.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                hitn.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                hitn.DamageToSameFactionRate += 0.1 * statMultiplier;
                hitn.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        hitn.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return hitn;
    }
    public static UserSSWNs EnhanceSSWNs(UserSSWNs sswn, int level, double multiplier = 1)
    {
        int startLevel = sswn.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                sswn.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                sswn.PhysicalAttack += 15000000 * statMultiplier;
                sswn.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                sswn.MagicalAttack += 15000000 * statMultiplier;
                sswn.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                sswn.ChemicalAttack += 15000000 * statMultiplier;
                sswn.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                sswn.AtomicAttack += 15000000 * statMultiplier;
                sswn.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                sswn.MentalAttack += 15000000 * statMultiplier;
                sswn.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                sswn.Speed += 15000000 * statMultiplier;
                sswn.CriticalDamageRate += 0.1 * statMultiplier;
                sswn.CriticalRate += 0.1 * statMultiplier;
                sswn.CriticalResistanceRate += 0.1 * statMultiplier;
                sswn.IgnoreCriticalRate += 0.1 * statMultiplier;
                sswn.PenetrationRate += 0.1 * statMultiplier;
                sswn.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                sswn.EvasionRate += 0.1 * statMultiplier;
                sswn.DamageAbsorptionRate += 0.1 * statMultiplier;
                sswn.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                sswn.AbsorbedDamageRate += 0.1 * statMultiplier;
                sswn.VitalityRegenerationRate += 0.1 * statMultiplier;
                sswn.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                sswn.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                sswn.LifestealRate += 0.1 * statMultiplier;
                sswn.Mana += 15000000 * statMultiplier;
                sswn.ManaRegenerationRate += 0.1 * statMultiplier;
                sswn.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                sswn.Tenacity += 0.5 * statMultiplier;
                sswn.ResistanceRate += 0.1 * statMultiplier;
                sswn.ComboRate += 0.1 * statMultiplier;
                sswn.IgnoreComboRate += 0.1 * statMultiplier;
                sswn.ComboDamageRate += 0.1 * statMultiplier;
                sswn.ComboResistanceRate += 0.1 * statMultiplier;
                sswn.StunRate += 0.1 * statMultiplier;
                sswn.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                sswn.ReflectionRate += 0.1 * statMultiplier;
                sswn.IgnoreReflectionRate += 0.1 * statMultiplier;
                sswn.ReflectionDamageRate += 0.1 * statMultiplier;
                sswn.ReflectionResistanceRate += 0.1 * statMultiplier;
                sswn.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                sswn.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                sswn.DamageToSameFactionRate += 0.1 * statMultiplier;
                sswn.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                sswn.NormalDamageRate += 0.1 * statMultiplier;
                sswn.NormalResistanceRate += 0.1 * statMultiplier;
                sswn.SkillDamageRate += 0.1 * statMultiplier;
                sswn.SkillResistanceRate += 0.1 * statMultiplier;
                sswn.PercentAllHealth += 5 * statMultiplier;
                sswn.PercentAllPhysicalAttack += 5 * statMultiplier;
                sswn.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                sswn.PercentAllMagicalAttack += 5 * statMultiplier;
                sswn.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                sswn.PercentAllChemicalAttack += 5 * statMultiplier;
                sswn.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                sswn.PercentAllAtomicAttack += 5 * statMultiplier;
                sswn.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                sswn.PercentAllMentalAttack += 5 * statMultiplier;
                sswn.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                sswn.PhysicalAttack += 15000000 * statMultiplier;
                sswn.MagicalAttack += 15000000 * statMultiplier;
                sswn.ChemicalAttack += 15000000 * statMultiplier;
                sswn.AtomicAttack += 15000000 * statMultiplier;
                sswn.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                sswn.PhysicalDefense += 15000000 * statMultiplier;
                sswn.MagicalDefense += 15000000 * statMultiplier;
                sswn.ChemicalDefense += 15000000 * statMultiplier;
                sswn.AtomicDefense += 15000000 * statMultiplier;
                sswn.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                sswn.Speed += 15000000 * statMultiplier;
                sswn.CriticalDamageRate += 0.1 * statMultiplier;
                sswn.CriticalRate += 0.1 * statMultiplier;
                sswn.PenetrationRate += 0.1 * statMultiplier;
                sswn.EvasionRate += 0.1 * statMultiplier;
                sswn.DamageAbsorptionRate += 0.1 * statMultiplier;
                sswn.VitalityRegenerationRate += 0.1 * statMultiplier;
                sswn.AccuracyRate += 0.1 * statMultiplier;
                sswn.LifestealRate += 0.1 * statMultiplier;
                sswn.Mana += 15000000 * statMultiplier;
                sswn.ManaRegenerationRate += 0.1 * statMultiplier;
                sswn.ShieldStrength += 15000000 * statMultiplier;
                sswn.Tenacity += 0.5 * statMultiplier;
                sswn.ResistanceRate += 0.1 * statMultiplier;
                sswn.ComboRate += 0.1 * statMultiplier;
                sswn.ReflectionRate += 0.1 * statMultiplier;
                sswn.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                sswn.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                sswn.DamageToSameFactionRate += 0.1 * statMultiplier;
                sswn.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        sswn.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return sswn;
    }
    public static UserModules EnhanceModules(UserModules module, int level, double multiplier = 1)
    {
        int startLevel = module.CurrentLevel;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 10)
            {
                module.CurrentMultiplier += 1 * statMultiplier;
            }
            else if (lvl > 11 && lvl <= 20)
            {
                module.CurrentMultiplier += 2 * statMultiplier;
            }
            else if (lvl > 21 && lvl <= 30)
            {
                module.CurrentMultiplier += 3 * statMultiplier;
            }
            else if (lvl > 31 && lvl <= 40)
            {
                module.CurrentMultiplier += 4 * statMultiplier;
            }
            else if (lvl > 41 && lvl <= 50)
            {
                module.CurrentMultiplier += 5 * statMultiplier;
            }
            else if (lvl > 51 && lvl <= 60)
            {
                module.CurrentMultiplier += 6 * statMultiplier;
            }
            else if (lvl > 61 && lvl <= 70)
            {
                module.CurrentMultiplier += 7 * statMultiplier;
            }
            else if (lvl > 71 && lvl <= 80)
            {
                module.CurrentMultiplier += 8 * statMultiplier;
            }
            else if (lvl > 81 && lvl <= 90)
            {
                module.CurrentMultiplier += 9 * statMultiplier;
            }
            else if (lvl > 91 && lvl <= 100)
            {
                module.CurrentMultiplier += 10 * statMultiplier;
            }
        }

        module.CurrentLevel = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return module;
    }
    public static UserUpgrades EnhanceUpgrades(UserUpgrades upgrade, int level, double multiplier = 1)
    {
        int startLevel = upgrade.CurrentLevel;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 10)
            {
                upgrade.CurrentMultiplier += 1 * statMultiplier;
            }
            else if (lvl > 11 && lvl <= 20)
            {
                upgrade.CurrentMultiplier += 2 * statMultiplier;
            }
            else if (lvl > 21 && lvl <= 30)
            {
                upgrade.CurrentMultiplier += 3 * statMultiplier;
            }
            else if (lvl > 31 && lvl <= 40)
            {
                upgrade.CurrentMultiplier += 4 * statMultiplier;
            }
            else if (lvl > 41 && lvl <= 50)
            {
                upgrade.CurrentMultiplier += 5 * statMultiplier;
            }
            else if (lvl > 51 && lvl <= 60)
            {
                upgrade.CurrentMultiplier += 6 * statMultiplier;
            }
            else if (lvl > 61 && lvl <= 70)
            {
                upgrade.CurrentMultiplier += 7 * statMultiplier;
            }
            else if (lvl > 71 && lvl <= 80)
            {
                upgrade.CurrentMultiplier += 8 * statMultiplier;
            }
            else if (lvl > 81 && lvl <= 90)
            {
                upgrade.CurrentMultiplier += 9 * statMultiplier;
            }
            else if (lvl > 91 && lvl <= 100)
            {
                upgrade.CurrentMultiplier += 10 * statMultiplier;
            }
        }

        upgrade.CurrentLevel = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return upgrade;
    }
}