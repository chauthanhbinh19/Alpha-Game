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
    public static UserAnimes EnhanceAnimes(UserAnimes userAnime, int level, double multiplier = 1)
    {
        int startLevel = userAnime.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                userAnime.Health += 10000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                userAnime.PhysicalAttack += 1500000 * statMultiplier;
                userAnime.PhysicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                userAnime.MagicalAttack += 1500000 * statMultiplier;
                userAnime.MagicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                userAnime.ChemicalAttack += 1500000 * statMultiplier;
                userAnime.ChemicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                userAnime.AtomicAttack += 1500000 * statMultiplier;
                userAnime.AtomicDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                userAnime.MentalAttack += 1500000 * statMultiplier;
                userAnime.MentalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                userAnime.Speed += 1500000 * statMultiplier;
                userAnime.CriticalDamageRate += 0.1 * statMultiplier;
                userAnime.CriticalRate += 0.1 * statMultiplier;
                userAnime.CriticalResistanceRate += 0.1 * statMultiplier;
                userAnime.IgnoreCriticalRate += 0.1 * statMultiplier;
                userAnime.PenetrationRate += 0.1 * statMultiplier;
                userAnime.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                userAnime.EvasionRate += 0.1 * statMultiplier;
                userAnime.DamageAbsorptionRate += 0.1 * statMultiplier;
                userAnime.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                userAnime.AbsorbedDamageRate += 0.1 * statMultiplier;
                userAnime.VitalityRegenerationRate += 0.1 * statMultiplier;
                userAnime.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                userAnime.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                userAnime.LifestealRate += 0.1 * statMultiplier;
                userAnime.Mana += 1500000 * statMultiplier;
                userAnime.ManaRegenerationRate += 0.1 * statMultiplier;
                userAnime.ShieldStrength += 1500000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                userAnime.Tenacity += 0.5 * statMultiplier;
                userAnime.ResistanceRate += 0.1 * statMultiplier;
                userAnime.ComboRate += 0.1 * statMultiplier;
                userAnime.IgnoreComboRate += 0.1 * statMultiplier;
                userAnime.ComboDamageRate += 0.1 * statMultiplier;
                userAnime.ComboResistanceRate += 0.1 * statMultiplier;
                userAnime.StunRate += 0.1 * statMultiplier;
                userAnime.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                userAnime.ReflectionRate += 0.1 * statMultiplier;
                userAnime.IgnoreReflectionRate += 0.1 * statMultiplier;
                userAnime.ReflectionDamageRate += 0.1 * statMultiplier;
                userAnime.ReflectionResistanceRate += 0.1 * statMultiplier;
                userAnime.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userAnime.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userAnime.DamageToSameFactionRate += 0.1 * statMultiplier;
                userAnime.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                userAnime.NormalDamageRate += 0.1 * statMultiplier;
                userAnime.NormalResistanceRate += 0.1 * statMultiplier;
                userAnime.SkillDamageRate += 0.1 * statMultiplier;
                userAnime.SkillResistanceRate += 0.1 * statMultiplier;
                userAnime.PercentAllHealth += 5 * statMultiplier;
                userAnime.PercentAllPhysicalAttack += 5 * statMultiplier;
                userAnime.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                userAnime.PercentAllMagicalAttack += 5 * statMultiplier;
                userAnime.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                userAnime.PercentAllChemicalAttack += 5 * statMultiplier;
                userAnime.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                userAnime.PercentAllAtomicAttack += 5 * statMultiplier;
                userAnime.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                userAnime.PercentAllMentalAttack += 5 * statMultiplier;
                userAnime.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                userAnime.PhysicalAttack += 1500000 * statMultiplier;
                userAnime.MagicalAttack += 1500000 * statMultiplier;
                userAnime.ChemicalAttack += 1500000 * statMultiplier;
                userAnime.AtomicAttack += 1500000 * statMultiplier;
                userAnime.MentalAttack += 1500000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                userAnime.PhysicalDefense += 1500000 * statMultiplier;
                userAnime.MagicalDefense += 1500000 * statMultiplier;
                userAnime.ChemicalDefense += 1500000 * statMultiplier;
                userAnime.AtomicDefense += 1500000 * statMultiplier;
                userAnime.MentalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                userAnime.Speed += 1500000 * statMultiplier;
                userAnime.CriticalDamageRate += 0.1 * statMultiplier;
                userAnime.CriticalRate += 0.1 * statMultiplier;
                userAnime.PenetrationRate += 0.1 * statMultiplier;
                userAnime.EvasionRate += 0.1 * statMultiplier;
                userAnime.DamageAbsorptionRate += 0.1 * statMultiplier;
                userAnime.VitalityRegenerationRate += 0.1 * statMultiplier;
                userAnime.AccuracyRate += 0.1 * statMultiplier;
                userAnime.LifestealRate += 0.1 * statMultiplier;
                userAnime.Mana += 1500000 * statMultiplier;
                userAnime.ManaRegenerationRate += 0.1 * statMultiplier;
                userAnime.ShieldStrength += 1500000 * statMultiplier;
                userAnime.Tenacity += 0.5 * statMultiplier;
                userAnime.ResistanceRate += 0.1 * statMultiplier;
                userAnime.ComboRate += 0.1 * statMultiplier;
                userAnime.ReflectionRate += 0.1 * statMultiplier;
                userAnime.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userAnime.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userAnime.DamageToSameFactionRate += 0.1 * statMultiplier;
                userAnime.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        userAnime.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return userAnime;
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
    public static UserScienceFictions EnhanceScienceFictions(UserScienceFictions userScienceFiction, int level, double multiplier = 1)
    {
        int startLevel = userScienceFiction.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                userScienceFiction.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                userScienceFiction.PhysicalAttack += 15000000 * statMultiplier;
                userScienceFiction.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                userScienceFiction.MagicalAttack += 15000000 * statMultiplier;
                userScienceFiction.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                userScienceFiction.ChemicalAttack += 15000000 * statMultiplier;
                userScienceFiction.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                userScienceFiction.AtomicAttack += 15000000 * statMultiplier;
                userScienceFiction.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                userScienceFiction.MentalAttack += 15000000 * statMultiplier;
                userScienceFiction.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                userScienceFiction.Speed += 15000000 * statMultiplier;
                userScienceFiction.CriticalDamageRate += 0.1 * statMultiplier;
                userScienceFiction.CriticalRate += 0.1 * statMultiplier;
                userScienceFiction.CriticalResistanceRate += 0.1 * statMultiplier;
                userScienceFiction.IgnoreCriticalRate += 0.1 * statMultiplier;
                userScienceFiction.PenetrationRate += 0.1 * statMultiplier;
                userScienceFiction.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                userScienceFiction.EvasionRate += 0.1 * statMultiplier;
                userScienceFiction.DamageAbsorptionRate += 0.1 * statMultiplier;
                userScienceFiction.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                userScienceFiction.AbsorbedDamageRate += 0.1 * statMultiplier;
                userScienceFiction.VitalityRegenerationRate += 0.1 * statMultiplier;
                userScienceFiction.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                userScienceFiction.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                userScienceFiction.LifestealRate += 0.1 * statMultiplier;
                userScienceFiction.Mana += 15000000 * statMultiplier;
                userScienceFiction.ManaRegenerationRate += 0.1 * statMultiplier;
                userScienceFiction.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                userScienceFiction.Tenacity += 0.5 * statMultiplier;
                userScienceFiction.ResistanceRate += 0.1 * statMultiplier;
                userScienceFiction.ComboRate += 0.1 * statMultiplier;
                userScienceFiction.IgnoreComboRate += 0.1 * statMultiplier;
                userScienceFiction.ComboDamageRate += 0.1 * statMultiplier;
                userScienceFiction.ComboResistanceRate += 0.1 * statMultiplier;
                userScienceFiction.StunRate += 0.1 * statMultiplier;
                userScienceFiction.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                userScienceFiction.ReflectionRate += 0.1 * statMultiplier;
                userScienceFiction.IgnoreReflectionRate += 0.1 * statMultiplier;
                userScienceFiction.ReflectionDamageRate += 0.1 * statMultiplier;
                userScienceFiction.ReflectionResistanceRate += 0.1 * statMultiplier;
                userScienceFiction.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userScienceFiction.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userScienceFiction.DamageToSameFactionRate += 0.1 * statMultiplier;
                userScienceFiction.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                userScienceFiction.NormalDamageRate += 0.1 * statMultiplier;
                userScienceFiction.NormalResistanceRate += 0.1 * statMultiplier;
                userScienceFiction.SkillDamageRate += 0.1 * statMultiplier;
                userScienceFiction.SkillResistanceRate += 0.1 * statMultiplier;
                userScienceFiction.PercentAllHealth += 5 * statMultiplier;
                userScienceFiction.PercentAllPhysicalAttack += 5 * statMultiplier;
                userScienceFiction.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                userScienceFiction.PercentAllMagicalAttack += 5 * statMultiplier;
                userScienceFiction.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                userScienceFiction.PercentAllChemicalAttack += 5 * statMultiplier;
                userScienceFiction.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                userScienceFiction.PercentAllAtomicAttack += 5 * statMultiplier;
                userScienceFiction.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                userScienceFiction.PercentAllMentalAttack += 5 * statMultiplier;
                userScienceFiction.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                userScienceFiction.PhysicalAttack += 15000000 * statMultiplier;
                userScienceFiction.MagicalAttack += 15000000 * statMultiplier;
                userScienceFiction.ChemicalAttack += 15000000 * statMultiplier;
                userScienceFiction.AtomicAttack += 15000000 * statMultiplier;
                userScienceFiction.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                userScienceFiction.PhysicalDefense += 15000000 * statMultiplier;
                userScienceFiction.MagicalDefense += 15000000 * statMultiplier;
                userScienceFiction.ChemicalDefense += 15000000 * statMultiplier;
                userScienceFiction.AtomicDefense += 15000000 * statMultiplier;
                userScienceFiction.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                userScienceFiction.Speed += 15000000 * statMultiplier;
                userScienceFiction.CriticalDamageRate += 0.1 * statMultiplier;
                userScienceFiction.CriticalRate += 0.1 * statMultiplier;
                userScienceFiction.PenetrationRate += 0.1 * statMultiplier;
                userScienceFiction.EvasionRate += 0.1 * statMultiplier;
                userScienceFiction.DamageAbsorptionRate += 0.1 * statMultiplier;
                userScienceFiction.VitalityRegenerationRate += 0.1 * statMultiplier;
                userScienceFiction.AccuracyRate += 0.1 * statMultiplier;
                userScienceFiction.LifestealRate += 0.1 * statMultiplier;
                userScienceFiction.Mana += 15000000 * statMultiplier;
                userScienceFiction.ManaRegenerationRate += 0.1 * statMultiplier;
                userScienceFiction.ShieldStrength += 15000000 * statMultiplier;
                userScienceFiction.Tenacity += 0.5 * statMultiplier;
                userScienceFiction.ResistanceRate += 0.1 * statMultiplier;
                userScienceFiction.ComboRate += 0.1 * statMultiplier;
                userScienceFiction.ReflectionRate += 0.1 * statMultiplier;
                userScienceFiction.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userScienceFiction.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userScienceFiction.DamageToSameFactionRate += 0.1 * statMultiplier;
                userScienceFiction.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        userScienceFiction.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return userScienceFiction;
    }
    public static UserArchives EnhanceArchives(UserArchives userArchive, int level, double multiplier = 1)
    {
        int startLevel = userArchive.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                userArchive.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                userArchive.PhysicalAttack += 15000000 * statMultiplier;
                userArchive.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                userArchive.MagicalAttack += 15000000 * statMultiplier;
                userArchive.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                userArchive.ChemicalAttack += 15000000 * statMultiplier;
                userArchive.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                userArchive.AtomicAttack += 15000000 * statMultiplier;
                userArchive.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                userArchive.MentalAttack += 15000000 * statMultiplier;
                userArchive.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                userArchive.Speed += 15000000 * statMultiplier;
                userArchive.CriticalDamageRate += 0.1 * statMultiplier;
                userArchive.CriticalRate += 0.1 * statMultiplier;
                userArchive.CriticalResistanceRate += 0.1 * statMultiplier;
                userArchive.IgnoreCriticalRate += 0.1 * statMultiplier;
                userArchive.PenetrationRate += 0.1 * statMultiplier;
                userArchive.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                userArchive.EvasionRate += 0.1 * statMultiplier;
                userArchive.DamageAbsorptionRate += 0.1 * statMultiplier;
                userArchive.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                userArchive.AbsorbedDamageRate += 0.1 * statMultiplier;
                userArchive.VitalityRegenerationRate += 0.1 * statMultiplier;
                userArchive.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                userArchive.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                userArchive.LifestealRate += 0.1 * statMultiplier;
                userArchive.Mana += 15000000 * statMultiplier;
                userArchive.ManaRegenerationRate += 0.1 * statMultiplier;
                userArchive.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                userArchive.Tenacity += 0.5 * statMultiplier;
                userArchive.ResistanceRate += 0.1 * statMultiplier;
                userArchive.ComboRate += 0.1 * statMultiplier;
                userArchive.IgnoreComboRate += 0.1 * statMultiplier;
                userArchive.ComboDamageRate += 0.1 * statMultiplier;
                userArchive.ComboResistanceRate += 0.1 * statMultiplier;
                userArchive.StunRate += 0.1 * statMultiplier;
                userArchive.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                userArchive.ReflectionRate += 0.1 * statMultiplier;
                userArchive.IgnoreReflectionRate += 0.1 * statMultiplier;
                userArchive.ReflectionDamageRate += 0.1 * statMultiplier;
                userArchive.ReflectionResistanceRate += 0.1 * statMultiplier;
                userArchive.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userArchive.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userArchive.DamageToSameFactionRate += 0.1 * statMultiplier;
                userArchive.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                userArchive.NormalDamageRate += 0.1 * statMultiplier;
                userArchive.NormalResistanceRate += 0.1 * statMultiplier;
                userArchive.SkillDamageRate += 0.1 * statMultiplier;
                userArchive.SkillResistanceRate += 0.1 * statMultiplier;
                userArchive.PercentAllHealth += 5 * statMultiplier;
                userArchive.PercentAllPhysicalAttack += 5 * statMultiplier;
                userArchive.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                userArchive.PercentAllMagicalAttack += 5 * statMultiplier;
                userArchive.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                userArchive.PercentAllChemicalAttack += 5 * statMultiplier;
                userArchive.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                userArchive.PercentAllAtomicAttack += 5 * statMultiplier;
                userArchive.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                userArchive.PercentAllMentalAttack += 5 * statMultiplier;
                userArchive.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                userArchive.PhysicalAttack += 15000000 * statMultiplier;
                userArchive.MagicalAttack += 15000000 * statMultiplier;
                userArchive.ChemicalAttack += 15000000 * statMultiplier;
                userArchive.AtomicAttack += 15000000 * statMultiplier;
                userArchive.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                userArchive.PhysicalDefense += 15000000 * statMultiplier;
                userArchive.MagicalDefense += 15000000 * statMultiplier;
                userArchive.ChemicalDefense += 15000000 * statMultiplier;
                userArchive.AtomicDefense += 15000000 * statMultiplier;
                userArchive.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                userArchive.Speed += 15000000 * statMultiplier;
                userArchive.CriticalDamageRate += 0.1 * statMultiplier;
                userArchive.CriticalRate += 0.1 * statMultiplier;
                userArchive.PenetrationRate += 0.1 * statMultiplier;
                userArchive.EvasionRate += 0.1 * statMultiplier;
                userArchive.DamageAbsorptionRate += 0.1 * statMultiplier;
                userArchive.VitalityRegenerationRate += 0.1 * statMultiplier;
                userArchive.AccuracyRate += 0.1 * statMultiplier;
                userArchive.LifestealRate += 0.1 * statMultiplier;
                userArchive.Mana += 15000000 * statMultiplier;
                userArchive.ManaRegenerationRate += 0.1 * statMultiplier;
                userArchive.ShieldStrength += 15000000 * statMultiplier;
                userArchive.Tenacity += 0.5 * statMultiplier;
                userArchive.ResistanceRate += 0.1 * statMultiplier;
                userArchive.ComboRate += 0.1 * statMultiplier;
                userArchive.ReflectionRate += 0.1 * statMultiplier;
                userArchive.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userArchive.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userArchive.DamageToSameFactionRate += 0.1 * statMultiplier;
                userArchive.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        userArchive.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return userArchive;
    }
    public static UserResearchs EnhanceResearchs(UserResearchs userResearch, int level, double multiplier = 1)
    {
        int startLevel = userResearch.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                userResearch.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                userResearch.PhysicalAttack += 15000000 * statMultiplier;
                userResearch.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                userResearch.MagicalAttack += 15000000 * statMultiplier;
                userResearch.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                userResearch.ChemicalAttack += 15000000 * statMultiplier;
                userResearch.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                userResearch.AtomicAttack += 15000000 * statMultiplier;
                userResearch.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                userResearch.MentalAttack += 15000000 * statMultiplier;
                userResearch.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                userResearch.Speed += 15000000 * statMultiplier;
                userResearch.CriticalDamageRate += 0.1 * statMultiplier;
                userResearch.CriticalRate += 0.1 * statMultiplier;
                userResearch.CriticalResistanceRate += 0.1 * statMultiplier;
                userResearch.IgnoreCriticalRate += 0.1 * statMultiplier;
                userResearch.PenetrationRate += 0.1 * statMultiplier;
                userResearch.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                userResearch.EvasionRate += 0.1 * statMultiplier;
                userResearch.DamageAbsorptionRate += 0.1 * statMultiplier;
                userResearch.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                userResearch.AbsorbedDamageRate += 0.1 * statMultiplier;
                userResearch.VitalityRegenerationRate += 0.1 * statMultiplier;
                userResearch.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                userResearch.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                userResearch.LifestealRate += 0.1 * statMultiplier;
                userResearch.Mana += 15000000 * statMultiplier;
                userResearch.ManaRegenerationRate += 0.1 * statMultiplier;
                userResearch.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                userResearch.Tenacity += 0.5 * statMultiplier;
                userResearch.ResistanceRate += 0.1 * statMultiplier;
                userResearch.ComboRate += 0.1 * statMultiplier;
                userResearch.IgnoreComboRate += 0.1 * statMultiplier;
                userResearch.ComboDamageRate += 0.1 * statMultiplier;
                userResearch.ComboResistanceRate += 0.1 * statMultiplier;
                userResearch.StunRate += 0.1 * statMultiplier;
                userResearch.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                userResearch.ReflectionRate += 0.1 * statMultiplier;
                userResearch.IgnoreReflectionRate += 0.1 * statMultiplier;
                userResearch.ReflectionDamageRate += 0.1 * statMultiplier;
                userResearch.ReflectionResistanceRate += 0.1 * statMultiplier;
                userResearch.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userResearch.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userResearch.DamageToSameFactionRate += 0.1 * statMultiplier;
                userResearch.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                userResearch.NormalDamageRate += 0.1 * statMultiplier;
                userResearch.NormalResistanceRate += 0.1 * statMultiplier;
                userResearch.SkillDamageRate += 0.1 * statMultiplier;
                userResearch.SkillResistanceRate += 0.1 * statMultiplier;
                userResearch.PercentAllHealth += 5 * statMultiplier;
                userResearch.PercentAllPhysicalAttack += 5 * statMultiplier;
                userResearch.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                userResearch.PercentAllMagicalAttack += 5 * statMultiplier;
                userResearch.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                userResearch.PercentAllChemicalAttack += 5 * statMultiplier;
                userResearch.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                userResearch.PercentAllAtomicAttack += 5 * statMultiplier;
                userResearch.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                userResearch.PercentAllMentalAttack += 5 * statMultiplier;
                userResearch.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                userResearch.PhysicalAttack += 15000000 * statMultiplier;
                userResearch.MagicalAttack += 15000000 * statMultiplier;
                userResearch.ChemicalAttack += 15000000 * statMultiplier;
                userResearch.AtomicAttack += 15000000 * statMultiplier;
                userResearch.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                userResearch.PhysicalDefense += 15000000 * statMultiplier;
                userResearch.MagicalDefense += 15000000 * statMultiplier;
                userResearch.ChemicalDefense += 15000000 * statMultiplier;
                userResearch.AtomicDefense += 15000000 * statMultiplier;
                userResearch.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                userResearch.Speed += 15000000 * statMultiplier;
                userResearch.CriticalDamageRate += 0.1 * statMultiplier;
                userResearch.CriticalRate += 0.1 * statMultiplier;
                userResearch.PenetrationRate += 0.1 * statMultiplier;
                userResearch.EvasionRate += 0.1 * statMultiplier;
                userResearch.DamageAbsorptionRate += 0.1 * statMultiplier;
                userResearch.VitalityRegenerationRate += 0.1 * statMultiplier;
                userResearch.AccuracyRate += 0.1 * statMultiplier;
                userResearch.LifestealRate += 0.1 * statMultiplier;
                userResearch.Mana += 15000000 * statMultiplier;
                userResearch.ManaRegenerationRate += 0.1 * statMultiplier;
                userResearch.ShieldStrength += 15000000 * statMultiplier;
                userResearch.Tenacity += 0.5 * statMultiplier;
                userResearch.ResistanceRate += 0.1 * statMultiplier;
                userResearch.ComboRate += 0.1 * statMultiplier;
                userResearch.ReflectionRate += 0.1 * statMultiplier;
                userResearch.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userResearch.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userResearch.DamageToSameFactionRate += 0.1 * statMultiplier;
                userResearch.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        userResearch.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return userResearch;
    }
    public static UserUniverses EnhanceUniverses(UserUniverses userUniverse, int level, double multiplier = 1)
    {
        int startLevel = userUniverse.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                userUniverse.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                userUniverse.PhysicalAttack += 15000000 * statMultiplier;
                userUniverse.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                userUniverse.MagicalAttack += 15000000 * statMultiplier;
                userUniverse.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                userUniverse.ChemicalAttack += 15000000 * statMultiplier;
                userUniverse.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                userUniverse.AtomicAttack += 15000000 * statMultiplier;
                userUniverse.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                userUniverse.MentalAttack += 15000000 * statMultiplier;
                userUniverse.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                userUniverse.Speed += 15000000 * statMultiplier;
                userUniverse.CriticalDamageRate += 0.1 * statMultiplier;
                userUniverse.CriticalRate += 0.1 * statMultiplier;
                userUniverse.CriticalResistanceRate += 0.1 * statMultiplier;
                userUniverse.IgnoreCriticalRate += 0.1 * statMultiplier;
                userUniverse.PenetrationRate += 0.1 * statMultiplier;
                userUniverse.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                userUniverse.EvasionRate += 0.1 * statMultiplier;
                userUniverse.DamageAbsorptionRate += 0.1 * statMultiplier;
                userUniverse.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                userUniverse.AbsorbedDamageRate += 0.1 * statMultiplier;
                userUniverse.VitalityRegenerationRate += 0.1 * statMultiplier;
                userUniverse.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                userUniverse.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                userUniverse.LifestealRate += 0.1 * statMultiplier;
                userUniverse.Mana += 15000000 * statMultiplier;
                userUniverse.ManaRegenerationRate += 0.1 * statMultiplier;
                userUniverse.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                userUniverse.Tenacity += 0.5 * statMultiplier;
                userUniverse.ResistanceRate += 0.1 * statMultiplier;
                userUniverse.ComboRate += 0.1 * statMultiplier;
                userUniverse.IgnoreComboRate += 0.1 * statMultiplier;
                userUniverse.ComboDamageRate += 0.1 * statMultiplier;
                userUniverse.ComboResistanceRate += 0.1 * statMultiplier;
                userUniverse.StunRate += 0.1 * statMultiplier;
                userUniverse.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                userUniverse.ReflectionRate += 0.1 * statMultiplier;
                userUniverse.IgnoreReflectionRate += 0.1 * statMultiplier;
                userUniverse.ReflectionDamageRate += 0.1 * statMultiplier;
                userUniverse.ReflectionResistanceRate += 0.1 * statMultiplier;
                userUniverse.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userUniverse.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userUniverse.DamageToSameFactionRate += 0.1 * statMultiplier;
                userUniverse.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                userUniverse.NormalDamageRate += 0.1 * statMultiplier;
                userUniverse.NormalResistanceRate += 0.1 * statMultiplier;
                userUniverse.SkillDamageRate += 0.1 * statMultiplier;
                userUniverse.SkillResistanceRate += 0.1 * statMultiplier;
                userUniverse.PercentAllHealth += 5 * statMultiplier;
                userUniverse.PercentAllPhysicalAttack += 5 * statMultiplier;
                userUniverse.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                userUniverse.PercentAllMagicalAttack += 5 * statMultiplier;
                userUniverse.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                userUniverse.PercentAllChemicalAttack += 5 * statMultiplier;
                userUniverse.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                userUniverse.PercentAllAtomicAttack += 5 * statMultiplier;
                userUniverse.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                userUniverse.PercentAllMentalAttack += 5 * statMultiplier;
                userUniverse.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                userUniverse.PhysicalAttack += 15000000 * statMultiplier;
                userUniverse.MagicalAttack += 15000000 * statMultiplier;
                userUniverse.ChemicalAttack += 15000000 * statMultiplier;
                userUniverse.AtomicAttack += 15000000 * statMultiplier;
                userUniverse.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                userUniverse.PhysicalDefense += 15000000 * statMultiplier;
                userUniverse.MagicalDefense += 15000000 * statMultiplier;
                userUniverse.ChemicalDefense += 15000000 * statMultiplier;
                userUniverse.AtomicDefense += 15000000 * statMultiplier;
                userUniverse.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                userUniverse.Speed += 15000000 * statMultiplier;
                userUniverse.CriticalDamageRate += 0.1 * statMultiplier;
                userUniverse.CriticalRate += 0.1 * statMultiplier;
                userUniverse.PenetrationRate += 0.1 * statMultiplier;
                userUniverse.EvasionRate += 0.1 * statMultiplier;
                userUniverse.DamageAbsorptionRate += 0.1 * statMultiplier;
                userUniverse.VitalityRegenerationRate += 0.1 * statMultiplier;
                userUniverse.AccuracyRate += 0.1 * statMultiplier;
                userUniverse.LifestealRate += 0.1 * statMultiplier;
                userUniverse.Mana += 15000000 * statMultiplier;
                userUniverse.ManaRegenerationRate += 0.1 * statMultiplier;
                userUniverse.ShieldStrength += 15000000 * statMultiplier;
                userUniverse.Tenacity += 0.5 * statMultiplier;
                userUniverse.ResistanceRate += 0.1 * statMultiplier;
                userUniverse.ComboRate += 0.1 * statMultiplier;
                userUniverse.ReflectionRate += 0.1 * statMultiplier;
                userUniverse.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userUniverse.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userUniverse.DamageToSameFactionRate += 0.1 * statMultiplier;
                userUniverse.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        userUniverse.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return userUniverse;
    }
    public static UserHICAs EnhanceHICAs(UserHICAs userHICA, int level, double multiplier = 1)
    {
        int startLevel = userHICA.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                userHICA.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                userHICA.PhysicalAttack += 15000000 * statMultiplier;
                userHICA.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                userHICA.MagicalAttack += 15000000 * statMultiplier;
                userHICA.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                userHICA.ChemicalAttack += 15000000 * statMultiplier;
                userHICA.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                userHICA.AtomicAttack += 15000000 * statMultiplier;
                userHICA.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                userHICA.MentalAttack += 15000000 * statMultiplier;
                userHICA.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                userHICA.Speed += 15000000 * statMultiplier;
                userHICA.CriticalDamageRate += 0.1 * statMultiplier;
                userHICA.CriticalRate += 0.1 * statMultiplier;
                userHICA.CriticalResistanceRate += 0.1 * statMultiplier;
                userHICA.IgnoreCriticalRate += 0.1 * statMultiplier;
                userHICA.PenetrationRate += 0.1 * statMultiplier;
                userHICA.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                userHICA.EvasionRate += 0.1 * statMultiplier;
                userHICA.DamageAbsorptionRate += 0.1 * statMultiplier;
                userHICA.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                userHICA.AbsorbedDamageRate += 0.1 * statMultiplier;
                userHICA.VitalityRegenerationRate += 0.1 * statMultiplier;
                userHICA.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                userHICA.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                userHICA.LifestealRate += 0.1 * statMultiplier;
                userHICA.Mana += 15000000 * statMultiplier;
                userHICA.ManaRegenerationRate += 0.1 * statMultiplier;
                userHICA.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                userHICA.Tenacity += 0.5 * statMultiplier;
                userHICA.ResistanceRate += 0.1 * statMultiplier;
                userHICA.ComboRate += 0.1 * statMultiplier;
                userHICA.IgnoreComboRate += 0.1 * statMultiplier;
                userHICA.ComboDamageRate += 0.1 * statMultiplier;
                userHICA.ComboResistanceRate += 0.1 * statMultiplier;
                userHICA.StunRate += 0.1 * statMultiplier;
                userHICA.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                userHICA.ReflectionRate += 0.1 * statMultiplier;
                userHICA.IgnoreReflectionRate += 0.1 * statMultiplier;
                userHICA.ReflectionDamageRate += 0.1 * statMultiplier;
                userHICA.ReflectionResistanceRate += 0.1 * statMultiplier;
                userHICA.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userHICA.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userHICA.DamageToSameFactionRate += 0.1 * statMultiplier;
                userHICA.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                userHICA.NormalDamageRate += 0.1 * statMultiplier;
                userHICA.NormalResistanceRate += 0.1 * statMultiplier;
                userHICA.SkillDamageRate += 0.1 * statMultiplier;
                userHICA.SkillResistanceRate += 0.1 * statMultiplier;
                userHICA.PercentAllHealth += 5 * statMultiplier;
                userHICA.PercentAllPhysicalAttack += 5 * statMultiplier;
                userHICA.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                userHICA.PercentAllMagicalAttack += 5 * statMultiplier;
                userHICA.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                userHICA.PercentAllChemicalAttack += 5 * statMultiplier;
                userHICA.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                userHICA.PercentAllAtomicAttack += 5 * statMultiplier;
                userHICA.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                userHICA.PercentAllMentalAttack += 5 * statMultiplier;
                userHICA.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                userHICA.PhysicalAttack += 15000000 * statMultiplier;
                userHICA.MagicalAttack += 15000000 * statMultiplier;
                userHICA.ChemicalAttack += 15000000 * statMultiplier;
                userHICA.AtomicAttack += 15000000 * statMultiplier;
                userHICA.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                userHICA.PhysicalDefense += 15000000 * statMultiplier;
                userHICA.MagicalDefense += 15000000 * statMultiplier;
                userHICA.ChemicalDefense += 15000000 * statMultiplier;
                userHICA.AtomicDefense += 15000000 * statMultiplier;
                userHICA.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                userHICA.Speed += 15000000 * statMultiplier;
                userHICA.CriticalDamageRate += 0.1 * statMultiplier;
                userHICA.CriticalRate += 0.1 * statMultiplier;
                userHICA.PenetrationRate += 0.1 * statMultiplier;
                userHICA.EvasionRate += 0.1 * statMultiplier;
                userHICA.DamageAbsorptionRate += 0.1 * statMultiplier;
                userHICA.VitalityRegenerationRate += 0.1 * statMultiplier;
                userHICA.AccuracyRate += 0.1 * statMultiplier;
                userHICA.LifestealRate += 0.1 * statMultiplier;
                userHICA.Mana += 15000000 * statMultiplier;
                userHICA.ManaRegenerationRate += 0.1 * statMultiplier;
                userHICA.ShieldStrength += 15000000 * statMultiplier;
                userHICA.Tenacity += 0.5 * statMultiplier;
                userHICA.ResistanceRate += 0.1 * statMultiplier;
                userHICA.ComboRate += 0.1 * statMultiplier;
                userHICA.ReflectionRate += 0.1 * statMultiplier;
                userHICA.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userHICA.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userHICA.DamageToSameFactionRate += 0.1 * statMultiplier;
                userHICA.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        userHICA.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return userHICA;
    }
    public static UserHICBs EnhanceHICBs(UserHICBs userHICB, int level, double multiplier = 1)
    {
        int startLevel = userHICB.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                userHICB.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                userHICB.PhysicalAttack += 15000000 * statMultiplier;
                userHICB.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                userHICB.MagicalAttack += 15000000 * statMultiplier;
                userHICB.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                userHICB.ChemicalAttack += 15000000 * statMultiplier;
                userHICB.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                userHICB.AtomicAttack += 15000000 * statMultiplier;
                userHICB.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                userHICB.MentalAttack += 15000000 * statMultiplier;
                userHICB.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                userHICB.Speed += 15000000 * statMultiplier;
                userHICB.CriticalDamageRate += 0.1 * statMultiplier;
                userHICB.CriticalRate += 0.1 * statMultiplier;
                userHICB.CriticalResistanceRate += 0.1 * statMultiplier;
                userHICB.IgnoreCriticalRate += 0.1 * statMultiplier;
                userHICB.PenetrationRate += 0.1 * statMultiplier;
                userHICB.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                userHICB.EvasionRate += 0.1 * statMultiplier;
                userHICB.DamageAbsorptionRate += 0.1 * statMultiplier;
                userHICB.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                userHICB.AbsorbedDamageRate += 0.1 * statMultiplier;
                userHICB.VitalityRegenerationRate += 0.1 * statMultiplier;
                userHICB.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                userHICB.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                userHICB.LifestealRate += 0.1 * statMultiplier;
                userHICB.Mana += 15000000 * statMultiplier;
                userHICB.ManaRegenerationRate += 0.1 * statMultiplier;
                userHICB.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                userHICB.Tenacity += 0.5 * statMultiplier;
                userHICB.ResistanceRate += 0.1 * statMultiplier;
                userHICB.ComboRate += 0.1 * statMultiplier;
                userHICB.IgnoreComboRate += 0.1 * statMultiplier;
                userHICB.ComboDamageRate += 0.1 * statMultiplier;
                userHICB.ComboResistanceRate += 0.1 * statMultiplier;
                userHICB.StunRate += 0.1 * statMultiplier;
                userHICB.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                userHICB.ReflectionRate += 0.1 * statMultiplier;
                userHICB.IgnoreReflectionRate += 0.1 * statMultiplier;
                userHICB.ReflectionDamageRate += 0.1 * statMultiplier;
                userHICB.ReflectionResistanceRate += 0.1 * statMultiplier;
                userHICB.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userHICB.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userHICB.DamageToSameFactionRate += 0.1 * statMultiplier;
                userHICB.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                userHICB.NormalDamageRate += 0.1 * statMultiplier;
                userHICB.NormalResistanceRate += 0.1 * statMultiplier;
                userHICB.SkillDamageRate += 0.1 * statMultiplier;
                userHICB.SkillResistanceRate += 0.1 * statMultiplier;
                userHICB.PercentAllHealth += 5 * statMultiplier;
                userHICB.PercentAllPhysicalAttack += 5 * statMultiplier;
                userHICB.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                userHICB.PercentAllMagicalAttack += 5 * statMultiplier;
                userHICB.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                userHICB.PercentAllChemicalAttack += 5 * statMultiplier;
                userHICB.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                userHICB.PercentAllAtomicAttack += 5 * statMultiplier;
                userHICB.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                userHICB.PercentAllMentalAttack += 5 * statMultiplier;
                userHICB.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                userHICB.PhysicalAttack += 15000000 * statMultiplier;
                userHICB.MagicalAttack += 15000000 * statMultiplier;
                userHICB.ChemicalAttack += 15000000 * statMultiplier;
                userHICB.AtomicAttack += 15000000 * statMultiplier;
                userHICB.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                userHICB.PhysicalDefense += 15000000 * statMultiplier;
                userHICB.MagicalDefense += 15000000 * statMultiplier;
                userHICB.ChemicalDefense += 15000000 * statMultiplier;
                userHICB.AtomicDefense += 15000000 * statMultiplier;
                userHICB.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                userHICB.Speed += 15000000 * statMultiplier;
                userHICB.CriticalDamageRate += 0.1 * statMultiplier;
                userHICB.CriticalRate += 0.1 * statMultiplier;
                userHICB.PenetrationRate += 0.1 * statMultiplier;
                userHICB.EvasionRate += 0.1 * statMultiplier;
                userHICB.DamageAbsorptionRate += 0.1 * statMultiplier;
                userHICB.VitalityRegenerationRate += 0.1 * statMultiplier;
                userHICB.AccuracyRate += 0.1 * statMultiplier;
                userHICB.LifestealRate += 0.1 * statMultiplier;
                userHICB.Mana += 15000000 * statMultiplier;
                userHICB.ManaRegenerationRate += 0.1 * statMultiplier;
                userHICB.ShieldStrength += 15000000 * statMultiplier;
                userHICB.Tenacity += 0.5 * statMultiplier;
                userHICB.ResistanceRate += 0.1 * statMultiplier;
                userHICB.ComboRate += 0.1 * statMultiplier;
                userHICB.ReflectionRate += 0.1 * statMultiplier;
                userHICB.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userHICB.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userHICB.DamageToSameFactionRate += 0.1 * statMultiplier;
                userHICB.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        userHICB.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return userHICB;
    }
    public static UserHIDCs EnhanceHIDCs(UserHIDCs userHIDC, int level, double multiplier = 1)
    {
        int startLevel = userHIDC.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                userHIDC.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                userHIDC.PhysicalAttack += 15000000 * statMultiplier;
                userHIDC.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                userHIDC.MagicalAttack += 15000000 * statMultiplier;
                userHIDC.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                userHIDC.ChemicalAttack += 15000000 * statMultiplier;
                userHIDC.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                userHIDC.AtomicAttack += 15000000 * statMultiplier;
                userHIDC.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                userHIDC.MentalAttack += 15000000 * statMultiplier;
                userHIDC.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                userHIDC.Speed += 15000000 * statMultiplier;
                userHIDC.CriticalDamageRate += 0.1 * statMultiplier;
                userHIDC.CriticalRate += 0.1 * statMultiplier;
                userHIDC.CriticalResistanceRate += 0.1 * statMultiplier;
                userHIDC.IgnoreCriticalRate += 0.1 * statMultiplier;
                userHIDC.PenetrationRate += 0.1 * statMultiplier;
                userHIDC.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                userHIDC.EvasionRate += 0.1 * statMultiplier;
                userHIDC.DamageAbsorptionRate += 0.1 * statMultiplier;
                userHIDC.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                userHIDC.AbsorbedDamageRate += 0.1 * statMultiplier;
                userHIDC.VitalityRegenerationRate += 0.1 * statMultiplier;
                userHIDC.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                userHIDC.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                userHIDC.LifestealRate += 0.1 * statMultiplier;
                userHIDC.Mana += 15000000 * statMultiplier;
                userHIDC.ManaRegenerationRate += 0.1 * statMultiplier;
                userHIDC.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                userHIDC.Tenacity += 0.5 * statMultiplier;
                userHIDC.ResistanceRate += 0.1 * statMultiplier;
                userHIDC.ComboRate += 0.1 * statMultiplier;
                userHIDC.IgnoreComboRate += 0.1 * statMultiplier;
                userHIDC.ComboDamageRate += 0.1 * statMultiplier;
                userHIDC.ComboResistanceRate += 0.1 * statMultiplier;
                userHIDC.StunRate += 0.1 * statMultiplier;
                userHIDC.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                userHIDC.ReflectionRate += 0.1 * statMultiplier;
                userHIDC.IgnoreReflectionRate += 0.1 * statMultiplier;
                userHIDC.ReflectionDamageRate += 0.1 * statMultiplier;
                userHIDC.ReflectionResistanceRate += 0.1 * statMultiplier;
                userHIDC.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userHIDC.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userHIDC.DamageToSameFactionRate += 0.1 * statMultiplier;
                userHIDC.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                userHIDC.NormalDamageRate += 0.1 * statMultiplier;
                userHIDC.NormalResistanceRate += 0.1 * statMultiplier;
                userHIDC.SkillDamageRate += 0.1 * statMultiplier;
                userHIDC.SkillResistanceRate += 0.1 * statMultiplier;
                userHIDC.PercentAllHealth += 5 * statMultiplier;
                userHIDC.PercentAllPhysicalAttack += 5 * statMultiplier;
                userHIDC.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                userHIDC.PercentAllMagicalAttack += 5 * statMultiplier;
                userHIDC.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                userHIDC.PercentAllChemicalAttack += 5 * statMultiplier;
                userHIDC.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                userHIDC.PercentAllAtomicAttack += 5 * statMultiplier;
                userHIDC.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                userHIDC.PercentAllMentalAttack += 5 * statMultiplier;
                userHIDC.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                userHIDC.PhysicalAttack += 15000000 * statMultiplier;
                userHIDC.MagicalAttack += 15000000 * statMultiplier;
                userHIDC.ChemicalAttack += 15000000 * statMultiplier;
                userHIDC.AtomicAttack += 15000000 * statMultiplier;
                userHIDC.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                userHIDC.PhysicalDefense += 15000000 * statMultiplier;
                userHIDC.MagicalDefense += 15000000 * statMultiplier;
                userHIDC.ChemicalDefense += 15000000 * statMultiplier;
                userHIDC.AtomicDefense += 15000000 * statMultiplier;
                userHIDC.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                userHIDC.Speed += 15000000 * statMultiplier;
                userHIDC.CriticalDamageRate += 0.1 * statMultiplier;
                userHIDC.CriticalRate += 0.1 * statMultiplier;
                userHIDC.PenetrationRate += 0.1 * statMultiplier;
                userHIDC.EvasionRate += 0.1 * statMultiplier;
                userHIDC.DamageAbsorptionRate += 0.1 * statMultiplier;
                userHIDC.VitalityRegenerationRate += 0.1 * statMultiplier;
                userHIDC.AccuracyRate += 0.1 * statMultiplier;
                userHIDC.LifestealRate += 0.1 * statMultiplier;
                userHIDC.Mana += 15000000 * statMultiplier;
                userHIDC.ManaRegenerationRate += 0.1 * statMultiplier;
                userHIDC.ShieldStrength += 15000000 * statMultiplier;
                userHIDC.Tenacity += 0.5 * statMultiplier;
                userHIDC.ResistanceRate += 0.1 * statMultiplier;
                userHIDC.ComboRate += 0.1 * statMultiplier;
                userHIDC.ReflectionRate += 0.1 * statMultiplier;
                userHIDC.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userHIDC.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userHIDC.DamageToSameFactionRate += 0.1 * statMultiplier;
                userHIDC.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        userHIDC.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return userHIDC;
    }
    public static UserHIENs EnhanceHIENs(UserHIENs userHIEN, int level, double multiplier = 1)
    {
        int startLevel = userHIEN.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                userHIEN.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                userHIEN.PhysicalAttack += 15000000 * statMultiplier;
                userHIEN.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                userHIEN.MagicalAttack += 15000000 * statMultiplier;
                userHIEN.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                userHIEN.ChemicalAttack += 15000000 * statMultiplier;
                userHIEN.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                userHIEN.AtomicAttack += 15000000 * statMultiplier;
                userHIEN.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                userHIEN.MentalAttack += 15000000 * statMultiplier;
                userHIEN.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                userHIEN.Speed += 15000000 * statMultiplier;
                userHIEN.CriticalDamageRate += 0.1 * statMultiplier;
                userHIEN.CriticalRate += 0.1 * statMultiplier;
                userHIEN.CriticalResistanceRate += 0.1 * statMultiplier;
                userHIEN.IgnoreCriticalRate += 0.1 * statMultiplier;
                userHIEN.PenetrationRate += 0.1 * statMultiplier;
                userHIEN.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                userHIEN.EvasionRate += 0.1 * statMultiplier;
                userHIEN.DamageAbsorptionRate += 0.1 * statMultiplier;
                userHIEN.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                userHIEN.AbsorbedDamageRate += 0.1 * statMultiplier;
                userHIEN.VitalityRegenerationRate += 0.1 * statMultiplier;
                userHIEN.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                userHIEN.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                userHIEN.LifestealRate += 0.1 * statMultiplier;
                userHIEN.Mana += 15000000 * statMultiplier;
                userHIEN.ManaRegenerationRate += 0.1 * statMultiplier;
                userHIEN.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                userHIEN.Tenacity += 0.5 * statMultiplier;
                userHIEN.ResistanceRate += 0.1 * statMultiplier;
                userHIEN.ComboRate += 0.1 * statMultiplier;
                userHIEN.IgnoreComboRate += 0.1 * statMultiplier;
                userHIEN.ComboDamageRate += 0.1 * statMultiplier;
                userHIEN.ComboResistanceRate += 0.1 * statMultiplier;
                userHIEN.StunRate += 0.1 * statMultiplier;
                userHIEN.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                userHIEN.ReflectionRate += 0.1 * statMultiplier;
                userHIEN.IgnoreReflectionRate += 0.1 * statMultiplier;
                userHIEN.ReflectionDamageRate += 0.1 * statMultiplier;
                userHIEN.ReflectionResistanceRate += 0.1 * statMultiplier;
                userHIEN.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userHIEN.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userHIEN.DamageToSameFactionRate += 0.1 * statMultiplier;
                userHIEN.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                userHIEN.NormalDamageRate += 0.1 * statMultiplier;
                userHIEN.NormalResistanceRate += 0.1 * statMultiplier;
                userHIEN.SkillDamageRate += 0.1 * statMultiplier;
                userHIEN.SkillResistanceRate += 0.1 * statMultiplier;
                userHIEN.PercentAllHealth += 5 * statMultiplier;
                userHIEN.PercentAllPhysicalAttack += 5 * statMultiplier;
                userHIEN.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                userHIEN.PercentAllMagicalAttack += 5 * statMultiplier;
                userHIEN.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                userHIEN.PercentAllChemicalAttack += 5 * statMultiplier;
                userHIEN.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                userHIEN.PercentAllAtomicAttack += 5 * statMultiplier;
                userHIEN.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                userHIEN.PercentAllMentalAttack += 5 * statMultiplier;
                userHIEN.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                userHIEN.PhysicalAttack += 15000000 * statMultiplier;
                userHIEN.MagicalAttack += 15000000 * statMultiplier;
                userHIEN.ChemicalAttack += 15000000 * statMultiplier;
                userHIEN.AtomicAttack += 15000000 * statMultiplier;
                userHIEN.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                userHIEN.PhysicalDefense += 15000000 * statMultiplier;
                userHIEN.MagicalDefense += 15000000 * statMultiplier;
                userHIEN.ChemicalDefense += 15000000 * statMultiplier;
                userHIEN.AtomicDefense += 15000000 * statMultiplier;
                userHIEN.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                userHIEN.Speed += 15000000 * statMultiplier;
                userHIEN.CriticalDamageRate += 0.1 * statMultiplier;
                userHIEN.CriticalRate += 0.1 * statMultiplier;
                userHIEN.PenetrationRate += 0.1 * statMultiplier;
                userHIEN.EvasionRate += 0.1 * statMultiplier;
                userHIEN.DamageAbsorptionRate += 0.1 * statMultiplier;
                userHIEN.VitalityRegenerationRate += 0.1 * statMultiplier;
                userHIEN.AccuracyRate += 0.1 * statMultiplier;
                userHIEN.LifestealRate += 0.1 * statMultiplier;
                userHIEN.Mana += 15000000 * statMultiplier;
                userHIEN.ManaRegenerationRate += 0.1 * statMultiplier;
                userHIEN.ShieldStrength += 15000000 * statMultiplier;
                userHIEN.Tenacity += 0.5 * statMultiplier;
                userHIEN.ResistanceRate += 0.1 * statMultiplier;
                userHIEN.ComboRate += 0.1 * statMultiplier;
                userHIEN.ReflectionRate += 0.1 * statMultiplier;
                userHIEN.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userHIEN.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userHIEN.DamageToSameFactionRate += 0.1 * statMultiplier;
                userHIEN.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        userHIEN.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return userHIEN;
    }
    public static UserHIHNs EnhanceHIHNs(UserHIHNs userHIHN, int level, double multiplier = 1)
    {
        int startLevel = userHIHN.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                userHIHN.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                userHIHN.PhysicalAttack += 15000000 * statMultiplier;
                userHIHN.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                userHIHN.MagicalAttack += 15000000 * statMultiplier;
                userHIHN.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                userHIHN.ChemicalAttack += 15000000 * statMultiplier;
                userHIHN.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                userHIHN.AtomicAttack += 15000000 * statMultiplier;
                userHIHN.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                userHIHN.MentalAttack += 15000000 * statMultiplier;
                userHIHN.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                userHIHN.Speed += 15000000 * statMultiplier;
                userHIHN.CriticalDamageRate += 0.1 * statMultiplier;
                userHIHN.CriticalRate += 0.1 * statMultiplier;
                userHIHN.CriticalResistanceRate += 0.1 * statMultiplier;
                userHIHN.IgnoreCriticalRate += 0.1 * statMultiplier;
                userHIHN.PenetrationRate += 0.1 * statMultiplier;
                userHIHN.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                userHIHN.EvasionRate += 0.1 * statMultiplier;
                userHIHN.DamageAbsorptionRate += 0.1 * statMultiplier;
                userHIHN.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                userHIHN.AbsorbedDamageRate += 0.1 * statMultiplier;
                userHIHN.VitalityRegenerationRate += 0.1 * statMultiplier;
                userHIHN.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                userHIHN.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                userHIHN.LifestealRate += 0.1 * statMultiplier;
                userHIHN.Mana += 15000000 * statMultiplier;
                userHIHN.ManaRegenerationRate += 0.1 * statMultiplier;
                userHIHN.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                userHIHN.Tenacity += 0.5 * statMultiplier;
                userHIHN.ResistanceRate += 0.1 * statMultiplier;
                userHIHN.ComboRate += 0.1 * statMultiplier;
                userHIHN.IgnoreComboRate += 0.1 * statMultiplier;
                userHIHN.ComboDamageRate += 0.1 * statMultiplier;
                userHIHN.ComboResistanceRate += 0.1 * statMultiplier;
                userHIHN.StunRate += 0.1 * statMultiplier;
                userHIHN.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                userHIHN.ReflectionRate += 0.1 * statMultiplier;
                userHIHN.IgnoreReflectionRate += 0.1 * statMultiplier;
                userHIHN.ReflectionDamageRate += 0.1 * statMultiplier;
                userHIHN.ReflectionResistanceRate += 0.1 * statMultiplier;
                userHIHN.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userHIHN.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userHIHN.DamageToSameFactionRate += 0.1 * statMultiplier;
                userHIHN.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                userHIHN.NormalDamageRate += 0.1 * statMultiplier;
                userHIHN.NormalResistanceRate += 0.1 * statMultiplier;
                userHIHN.SkillDamageRate += 0.1 * statMultiplier;
                userHIHN.SkillResistanceRate += 0.1 * statMultiplier;
                userHIHN.PercentAllHealth += 5 * statMultiplier;
                userHIHN.PercentAllPhysicalAttack += 5 * statMultiplier;
                userHIHN.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                userHIHN.PercentAllMagicalAttack += 5 * statMultiplier;
                userHIHN.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                userHIHN.PercentAllChemicalAttack += 5 * statMultiplier;
                userHIHN.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                userHIHN.PercentAllAtomicAttack += 5 * statMultiplier;
                userHIHN.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                userHIHN.PercentAllMentalAttack += 5 * statMultiplier;
                userHIHN.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                userHIHN.PhysicalAttack += 15000000 * statMultiplier;
                userHIHN.MagicalAttack += 15000000 * statMultiplier;
                userHIHN.ChemicalAttack += 15000000 * statMultiplier;
                userHIHN.AtomicAttack += 15000000 * statMultiplier;
                userHIHN.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                userHIHN.PhysicalDefense += 15000000 * statMultiplier;
                userHIHN.MagicalDefense += 15000000 * statMultiplier;
                userHIHN.ChemicalDefense += 15000000 * statMultiplier;
                userHIHN.AtomicDefense += 15000000 * statMultiplier;
                userHIHN.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                userHIHN.Speed += 15000000 * statMultiplier;
                userHIHN.CriticalDamageRate += 0.1 * statMultiplier;
                userHIHN.CriticalRate += 0.1 * statMultiplier;
                userHIHN.PenetrationRate += 0.1 * statMultiplier;
                userHIHN.EvasionRate += 0.1 * statMultiplier;
                userHIHN.DamageAbsorptionRate += 0.1 * statMultiplier;
                userHIHN.VitalityRegenerationRate += 0.1 * statMultiplier;
                userHIHN.AccuracyRate += 0.1 * statMultiplier;
                userHIHN.LifestealRate += 0.1 * statMultiplier;
                userHIHN.Mana += 15000000 * statMultiplier;
                userHIHN.ManaRegenerationRate += 0.1 * statMultiplier;
                userHIHN.ShieldStrength += 15000000 * statMultiplier;
                userHIHN.Tenacity += 0.5 * statMultiplier;
                userHIHN.ResistanceRate += 0.1 * statMultiplier;
                userHIHN.ComboRate += 0.1 * statMultiplier;
                userHIHN.ReflectionRate += 0.1 * statMultiplier;
                userHIHN.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userHIHN.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userHIHN.DamageToSameFactionRate += 0.1 * statMultiplier;
                userHIHN.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        userHIHN.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return userHIHN;
    }
    public static UserHIINs EnhanceHIINs(UserHIINs userHIIN, int level, double multiplier = 1)
    {
        int startLevel = userHIIN.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                userHIIN.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                userHIIN.PhysicalAttack += 15000000 * statMultiplier;
                userHIIN.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                userHIIN.MagicalAttack += 15000000 * statMultiplier;
                userHIIN.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                userHIIN.ChemicalAttack += 15000000 * statMultiplier;
                userHIIN.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                userHIIN.AtomicAttack += 15000000 * statMultiplier;
                userHIIN.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                userHIIN.MentalAttack += 15000000 * statMultiplier;
                userHIIN.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                userHIIN.Speed += 15000000 * statMultiplier;
                userHIIN.CriticalDamageRate += 0.1 * statMultiplier;
                userHIIN.CriticalRate += 0.1 * statMultiplier;
                userHIIN.CriticalResistanceRate += 0.1 * statMultiplier;
                userHIIN.IgnoreCriticalRate += 0.1 * statMultiplier;
                userHIIN.PenetrationRate += 0.1 * statMultiplier;
                userHIIN.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                userHIIN.EvasionRate += 0.1 * statMultiplier;
                userHIIN.DamageAbsorptionRate += 0.1 * statMultiplier;
                userHIIN.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                userHIIN.AbsorbedDamageRate += 0.1 * statMultiplier;
                userHIIN.VitalityRegenerationRate += 0.1 * statMultiplier;
                userHIIN.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                userHIIN.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                userHIIN.LifestealRate += 0.1 * statMultiplier;
                userHIIN.Mana += 15000000 * statMultiplier;
                userHIIN.ManaRegenerationRate += 0.1 * statMultiplier;
                userHIIN.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                userHIIN.Tenacity += 0.5 * statMultiplier;
                userHIIN.ResistanceRate += 0.1 * statMultiplier;
                userHIIN.ComboRate += 0.1 * statMultiplier;
                userHIIN.IgnoreComboRate += 0.1 * statMultiplier;
                userHIIN.ComboDamageRate += 0.1 * statMultiplier;
                userHIIN.ComboResistanceRate += 0.1 * statMultiplier;
                userHIIN.StunRate += 0.1 * statMultiplier;
                userHIIN.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                userHIIN.ReflectionRate += 0.1 * statMultiplier;
                userHIIN.IgnoreReflectionRate += 0.1 * statMultiplier;
                userHIIN.ReflectionDamageRate += 0.1 * statMultiplier;
                userHIIN.ReflectionResistanceRate += 0.1 * statMultiplier;
                userHIIN.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userHIIN.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userHIIN.DamageToSameFactionRate += 0.1 * statMultiplier;
                userHIIN.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                userHIIN.NormalDamageRate += 0.1 * statMultiplier;
                userHIIN.NormalResistanceRate += 0.1 * statMultiplier;
                userHIIN.SkillDamageRate += 0.1 * statMultiplier;
                userHIIN.SkillResistanceRate += 0.1 * statMultiplier;
                userHIIN.PercentAllHealth += 5 * statMultiplier;
                userHIIN.PercentAllPhysicalAttack += 5 * statMultiplier;
                userHIIN.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                userHIIN.PercentAllMagicalAttack += 5 * statMultiplier;
                userHIIN.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                userHIIN.PercentAllChemicalAttack += 5 * statMultiplier;
                userHIIN.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                userHIIN.PercentAllAtomicAttack += 5 * statMultiplier;
                userHIIN.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                userHIIN.PercentAllMentalAttack += 5 * statMultiplier;
                userHIIN.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                userHIIN.PhysicalAttack += 15000000 * statMultiplier;
                userHIIN.MagicalAttack += 15000000 * statMultiplier;
                userHIIN.ChemicalAttack += 15000000 * statMultiplier;
                userHIIN.AtomicAttack += 15000000 * statMultiplier;
                userHIIN.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                userHIIN.PhysicalDefense += 15000000 * statMultiplier;
                userHIIN.MagicalDefense += 15000000 * statMultiplier;
                userHIIN.ChemicalDefense += 15000000 * statMultiplier;
                userHIIN.AtomicDefense += 15000000 * statMultiplier;
                userHIIN.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                userHIIN.Speed += 15000000 * statMultiplier;
                userHIIN.CriticalDamageRate += 0.1 * statMultiplier;
                userHIIN.CriticalRate += 0.1 * statMultiplier;
                userHIIN.PenetrationRate += 0.1 * statMultiplier;
                userHIIN.EvasionRate += 0.1 * statMultiplier;
                userHIIN.DamageAbsorptionRate += 0.1 * statMultiplier;
                userHIIN.VitalityRegenerationRate += 0.1 * statMultiplier;
                userHIIN.AccuracyRate += 0.1 * statMultiplier;
                userHIIN.LifestealRate += 0.1 * statMultiplier;
                userHIIN.Mana += 15000000 * statMultiplier;
                userHIIN.ManaRegenerationRate += 0.1 * statMultiplier;
                userHIIN.ShieldStrength += 15000000 * statMultiplier;
                userHIIN.Tenacity += 0.5 * statMultiplier;
                userHIIN.ResistanceRate += 0.1 * statMultiplier;
                userHIIN.ComboRate += 0.1 * statMultiplier;
                userHIIN.ReflectionRate += 0.1 * statMultiplier;
                userHIIN.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userHIIN.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userHIIN.DamageToSameFactionRate += 0.1 * statMultiplier;
                userHIIN.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        userHIIN.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return userHIIN;
    }
    public static UserHIRNs EnhanceHIRNs(UserHIRNs userHIRN, int level, double multiplier = 1)
    {
        int startLevel = userHIRN.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                userHIRN.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                userHIRN.PhysicalAttack += 15000000 * statMultiplier;
                userHIRN.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                userHIRN.MagicalAttack += 15000000 * statMultiplier;
                userHIRN.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                userHIRN.ChemicalAttack += 15000000 * statMultiplier;
                userHIRN.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                userHIRN.AtomicAttack += 15000000 * statMultiplier;
                userHIRN.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                userHIRN.MentalAttack += 15000000 * statMultiplier;
                userHIRN.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                userHIRN.Speed += 15000000 * statMultiplier;
                userHIRN.CriticalDamageRate += 0.1 * statMultiplier;
                userHIRN.CriticalRate += 0.1 * statMultiplier;
                userHIRN.CriticalResistanceRate += 0.1 * statMultiplier;
                userHIRN.IgnoreCriticalRate += 0.1 * statMultiplier;
                userHIRN.PenetrationRate += 0.1 * statMultiplier;
                userHIRN.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                userHIRN.EvasionRate += 0.1 * statMultiplier;
                userHIRN.DamageAbsorptionRate += 0.1 * statMultiplier;
                userHIRN.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                userHIRN.AbsorbedDamageRate += 0.1 * statMultiplier;
                userHIRN.VitalityRegenerationRate += 0.1 * statMultiplier;
                userHIRN.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                userHIRN.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                userHIRN.LifestealRate += 0.1 * statMultiplier;
                userHIRN.Mana += 15000000 * statMultiplier;
                userHIRN.ManaRegenerationRate += 0.1 * statMultiplier;
                userHIRN.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                userHIRN.Tenacity += 0.5 * statMultiplier;
                userHIRN.ResistanceRate += 0.1 * statMultiplier;
                userHIRN.ComboRate += 0.1 * statMultiplier;
                userHIRN.IgnoreComboRate += 0.1 * statMultiplier;
                userHIRN.ComboDamageRate += 0.1 * statMultiplier;
                userHIRN.ComboResistanceRate += 0.1 * statMultiplier;
                userHIRN.StunRate += 0.1 * statMultiplier;
                userHIRN.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                userHIRN.ReflectionRate += 0.1 * statMultiplier;
                userHIRN.IgnoreReflectionRate += 0.1 * statMultiplier;
                userHIRN.ReflectionDamageRate += 0.1 * statMultiplier;
                userHIRN.ReflectionResistanceRate += 0.1 * statMultiplier;
                userHIRN.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userHIRN.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userHIRN.DamageToSameFactionRate += 0.1 * statMultiplier;
                userHIRN.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                userHIRN.NormalDamageRate += 0.1 * statMultiplier;
                userHIRN.NormalResistanceRate += 0.1 * statMultiplier;
                userHIRN.SkillDamageRate += 0.1 * statMultiplier;
                userHIRN.SkillResistanceRate += 0.1 * statMultiplier;
                userHIRN.PercentAllHealth += 5 * statMultiplier;
                userHIRN.PercentAllPhysicalAttack += 5 * statMultiplier;
                userHIRN.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                userHIRN.PercentAllMagicalAttack += 5 * statMultiplier;
                userHIRN.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                userHIRN.PercentAllChemicalAttack += 5 * statMultiplier;
                userHIRN.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                userHIRN.PercentAllAtomicAttack += 5 * statMultiplier;
                userHIRN.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                userHIRN.PercentAllMentalAttack += 5 * statMultiplier;
                userHIRN.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                userHIRN.PhysicalAttack += 15000000 * statMultiplier;
                userHIRN.MagicalAttack += 15000000 * statMultiplier;
                userHIRN.ChemicalAttack += 15000000 * statMultiplier;
                userHIRN.AtomicAttack += 15000000 * statMultiplier;
                userHIRN.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                userHIRN.PhysicalDefense += 15000000 * statMultiplier;
                userHIRN.MagicalDefense += 15000000 * statMultiplier;
                userHIRN.ChemicalDefense += 15000000 * statMultiplier;
                userHIRN.AtomicDefense += 15000000 * statMultiplier;
                userHIRN.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                userHIRN.Speed += 15000000 * statMultiplier;
                userHIRN.CriticalDamageRate += 0.1 * statMultiplier;
                userHIRN.CriticalRate += 0.1 * statMultiplier;
                userHIRN.PenetrationRate += 0.1 * statMultiplier;
                userHIRN.EvasionRate += 0.1 * statMultiplier;
                userHIRN.DamageAbsorptionRate += 0.1 * statMultiplier;
                userHIRN.VitalityRegenerationRate += 0.1 * statMultiplier;
                userHIRN.AccuracyRate += 0.1 * statMultiplier;
                userHIRN.LifestealRate += 0.1 * statMultiplier;
                userHIRN.Mana += 15000000 * statMultiplier;
                userHIRN.ManaRegenerationRate += 0.1 * statMultiplier;
                userHIRN.ShieldStrength += 15000000 * statMultiplier;
                userHIRN.Tenacity += 0.5 * statMultiplier;
                userHIRN.ResistanceRate += 0.1 * statMultiplier;
                userHIRN.ComboRate += 0.1 * statMultiplier;
                userHIRN.ReflectionRate += 0.1 * statMultiplier;
                userHIRN.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userHIRN.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userHIRN.DamageToSameFactionRate += 0.1 * statMultiplier;
                userHIRN.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        userHIRN.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return userHIRN;
    }
    public static UserHISNs EnhanceHISNs(UserHISNs userHISN, int level, double multiplier = 1)
    {
        int startLevel = userHISN.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                userHISN.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                userHISN.PhysicalAttack += 15000000 * statMultiplier;
                userHISN.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                userHISN.MagicalAttack += 15000000 * statMultiplier;
                userHISN.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                userHISN.ChemicalAttack += 15000000 * statMultiplier;
                userHISN.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                userHISN.AtomicAttack += 15000000 * statMultiplier;
                userHISN.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                userHISN.MentalAttack += 15000000 * statMultiplier;
                userHISN.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                userHISN.Speed += 15000000 * statMultiplier;
                userHISN.CriticalDamageRate += 0.1 * statMultiplier;
                userHISN.CriticalRate += 0.1 * statMultiplier;
                userHISN.CriticalResistanceRate += 0.1 * statMultiplier;
                userHISN.IgnoreCriticalRate += 0.1 * statMultiplier;
                userHISN.PenetrationRate += 0.1 * statMultiplier;
                userHISN.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                userHISN.EvasionRate += 0.1 * statMultiplier;
                userHISN.DamageAbsorptionRate += 0.1 * statMultiplier;
                userHISN.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                userHISN.AbsorbedDamageRate += 0.1 * statMultiplier;
                userHISN.VitalityRegenerationRate += 0.1 * statMultiplier;
                userHISN.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                userHISN.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                userHISN.LifestealRate += 0.1 * statMultiplier;
                userHISN.Mana += 15000000 * statMultiplier;
                userHISN.ManaRegenerationRate += 0.1 * statMultiplier;
                userHISN.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                userHISN.Tenacity += 0.5 * statMultiplier;
                userHISN.ResistanceRate += 0.1 * statMultiplier;
                userHISN.ComboRate += 0.1 * statMultiplier;
                userHISN.IgnoreComboRate += 0.1 * statMultiplier;
                userHISN.ComboDamageRate += 0.1 * statMultiplier;
                userHISN.ComboResistanceRate += 0.1 * statMultiplier;
                userHISN.StunRate += 0.1 * statMultiplier;
                userHISN.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                userHISN.ReflectionRate += 0.1 * statMultiplier;
                userHISN.IgnoreReflectionRate += 0.1 * statMultiplier;
                userHISN.ReflectionDamageRate += 0.1 * statMultiplier;
                userHISN.ReflectionResistanceRate += 0.1 * statMultiplier;
                userHISN.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userHISN.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userHISN.DamageToSameFactionRate += 0.1 * statMultiplier;
                userHISN.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                userHISN.NormalDamageRate += 0.1 * statMultiplier;
                userHISN.NormalResistanceRate += 0.1 * statMultiplier;
                userHISN.SkillDamageRate += 0.1 * statMultiplier;
                userHISN.SkillResistanceRate += 0.1 * statMultiplier;
                userHISN.PercentAllHealth += 5 * statMultiplier;
                userHISN.PercentAllPhysicalAttack += 5 * statMultiplier;
                userHISN.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                userHISN.PercentAllMagicalAttack += 5 * statMultiplier;
                userHISN.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                userHISN.PercentAllChemicalAttack += 5 * statMultiplier;
                userHISN.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                userHISN.PercentAllAtomicAttack += 5 * statMultiplier;
                userHISN.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                userHISN.PercentAllMentalAttack += 5 * statMultiplier;
                userHISN.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                userHISN.PhysicalAttack += 15000000 * statMultiplier;
                userHISN.MagicalAttack += 15000000 * statMultiplier;
                userHISN.ChemicalAttack += 15000000 * statMultiplier;
                userHISN.AtomicAttack += 15000000 * statMultiplier;
                userHISN.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                userHISN.PhysicalDefense += 15000000 * statMultiplier;
                userHISN.MagicalDefense += 15000000 * statMultiplier;
                userHISN.ChemicalDefense += 15000000 * statMultiplier;
                userHISN.AtomicDefense += 15000000 * statMultiplier;
                userHISN.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                userHISN.Speed += 15000000 * statMultiplier;
                userHISN.CriticalDamageRate += 0.1 * statMultiplier;
                userHISN.CriticalRate += 0.1 * statMultiplier;
                userHISN.PenetrationRate += 0.1 * statMultiplier;
                userHISN.EvasionRate += 0.1 * statMultiplier;
                userHISN.DamageAbsorptionRate += 0.1 * statMultiplier;
                userHISN.VitalityRegenerationRate += 0.1 * statMultiplier;
                userHISN.AccuracyRate += 0.1 * statMultiplier;
                userHISN.LifestealRate += 0.1 * statMultiplier;
                userHISN.Mana += 15000000 * statMultiplier;
                userHISN.ManaRegenerationRate += 0.1 * statMultiplier;
                userHISN.ShieldStrength += 15000000 * statMultiplier;
                userHISN.Tenacity += 0.5 * statMultiplier;
                userHISN.ResistanceRate += 0.1 * statMultiplier;
                userHISN.ComboRate += 0.1 * statMultiplier;
                userHISN.ReflectionRate += 0.1 * statMultiplier;
                userHISN.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userHISN.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userHISN.DamageToSameFactionRate += 0.1 * statMultiplier;
                userHISN.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        userHISN.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return userHISN;
    }
    public static UserHITNs EnhanceHITNs(UserHITNs userHITN, int level, double multiplier = 1)
    {
        int startLevel = userHITN.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                userHITN.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                userHITN.PhysicalAttack += 15000000 * statMultiplier;
                userHITN.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                userHITN.MagicalAttack += 15000000 * statMultiplier;
                userHITN.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                userHITN.ChemicalAttack += 15000000 * statMultiplier;
                userHITN.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                userHITN.AtomicAttack += 15000000 * statMultiplier;
                userHITN.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                userHITN.MentalAttack += 15000000 * statMultiplier;
                userHITN.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                userHITN.Speed += 15000000 * statMultiplier;
                userHITN.CriticalDamageRate += 0.1 * statMultiplier;
                userHITN.CriticalRate += 0.1 * statMultiplier;
                userHITN.CriticalResistanceRate += 0.1 * statMultiplier;
                userHITN.IgnoreCriticalRate += 0.1 * statMultiplier;
                userHITN.PenetrationRate += 0.1 * statMultiplier;
                userHITN.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                userHITN.EvasionRate += 0.1 * statMultiplier;
                userHITN.DamageAbsorptionRate += 0.1 * statMultiplier;
                userHITN.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                userHITN.AbsorbedDamageRate += 0.1 * statMultiplier;
                userHITN.VitalityRegenerationRate += 0.1 * statMultiplier;
                userHITN.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                userHITN.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                userHITN.LifestealRate += 0.1 * statMultiplier;
                userHITN.Mana += 15000000 * statMultiplier;
                userHITN.ManaRegenerationRate += 0.1 * statMultiplier;
                userHITN.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                userHITN.Tenacity += 0.5 * statMultiplier;
                userHITN.ResistanceRate += 0.1 * statMultiplier;
                userHITN.ComboRate += 0.1 * statMultiplier;
                userHITN.IgnoreComboRate += 0.1 * statMultiplier;
                userHITN.ComboDamageRate += 0.1 * statMultiplier;
                userHITN.ComboResistanceRate += 0.1 * statMultiplier;
                userHITN.StunRate += 0.1 * statMultiplier;
                userHITN.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                userHITN.ReflectionRate += 0.1 * statMultiplier;
                userHITN.IgnoreReflectionRate += 0.1 * statMultiplier;
                userHITN.ReflectionDamageRate += 0.1 * statMultiplier;
                userHITN.ReflectionResistanceRate += 0.1 * statMultiplier;
                userHITN.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userHITN.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userHITN.DamageToSameFactionRate += 0.1 * statMultiplier;
                userHITN.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                userHITN.NormalDamageRate += 0.1 * statMultiplier;
                userHITN.NormalResistanceRate += 0.1 * statMultiplier;
                userHITN.SkillDamageRate += 0.1 * statMultiplier;
                userHITN.SkillResistanceRate += 0.1 * statMultiplier;
                userHITN.PercentAllHealth += 5 * statMultiplier;
                userHITN.PercentAllPhysicalAttack += 5 * statMultiplier;
                userHITN.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                userHITN.PercentAllMagicalAttack += 5 * statMultiplier;
                userHITN.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                userHITN.PercentAllChemicalAttack += 5 * statMultiplier;
                userHITN.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                userHITN.PercentAllAtomicAttack += 5 * statMultiplier;
                userHITN.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                userHITN.PercentAllMentalAttack += 5 * statMultiplier;
                userHITN.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                userHITN.PhysicalAttack += 15000000 * statMultiplier;
                userHITN.MagicalAttack += 15000000 * statMultiplier;
                userHITN.ChemicalAttack += 15000000 * statMultiplier;
                userHITN.AtomicAttack += 15000000 * statMultiplier;
                userHITN.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                userHITN.PhysicalDefense += 15000000 * statMultiplier;
                userHITN.MagicalDefense += 15000000 * statMultiplier;
                userHITN.ChemicalDefense += 15000000 * statMultiplier;
                userHITN.AtomicDefense += 15000000 * statMultiplier;
                userHITN.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                userHITN.Speed += 15000000 * statMultiplier;
                userHITN.CriticalDamageRate += 0.1 * statMultiplier;
                userHITN.CriticalRate += 0.1 * statMultiplier;
                userHITN.PenetrationRate += 0.1 * statMultiplier;
                userHITN.EvasionRate += 0.1 * statMultiplier;
                userHITN.DamageAbsorptionRate += 0.1 * statMultiplier;
                userHITN.VitalityRegenerationRate += 0.1 * statMultiplier;
                userHITN.AccuracyRate += 0.1 * statMultiplier;
                userHITN.LifestealRate += 0.1 * statMultiplier;
                userHITN.Mana += 15000000 * statMultiplier;
                userHITN.ManaRegenerationRate += 0.1 * statMultiplier;
                userHITN.ShieldStrength += 15000000 * statMultiplier;
                userHITN.Tenacity += 0.5 * statMultiplier;
                userHITN.ResistanceRate += 0.1 * statMultiplier;
                userHITN.ComboRate += 0.1 * statMultiplier;
                userHITN.ReflectionRate += 0.1 * statMultiplier;
                userHITN.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userHITN.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userHITN.DamageToSameFactionRate += 0.1 * statMultiplier;
                userHITN.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        userHITN.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return userHITN;
    }
    public static UserSSWNs EnhanceSSWNs(UserSSWNs userSSWN, int level, double multiplier = 1)
    {
        int startLevel = userSSWN.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                userSSWN.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                userSSWN.PhysicalAttack += 15000000 * statMultiplier;
                userSSWN.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                userSSWN.MagicalAttack += 15000000 * statMultiplier;
                userSSWN.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                userSSWN.ChemicalAttack += 15000000 * statMultiplier;
                userSSWN.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                userSSWN.AtomicAttack += 15000000 * statMultiplier;
                userSSWN.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                userSSWN.MentalAttack += 15000000 * statMultiplier;
                userSSWN.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                userSSWN.Speed += 15000000 * statMultiplier;
                userSSWN.CriticalDamageRate += 0.1 * statMultiplier;
                userSSWN.CriticalRate += 0.1 * statMultiplier;
                userSSWN.CriticalResistanceRate += 0.1 * statMultiplier;
                userSSWN.IgnoreCriticalRate += 0.1 * statMultiplier;
                userSSWN.PenetrationRate += 0.1 * statMultiplier;
                userSSWN.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                userSSWN.EvasionRate += 0.1 * statMultiplier;
                userSSWN.DamageAbsorptionRate += 0.1 * statMultiplier;
                userSSWN.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                userSSWN.AbsorbedDamageRate += 0.1 * statMultiplier;
                userSSWN.VitalityRegenerationRate += 0.1 * statMultiplier;
                userSSWN.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                userSSWN.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                userSSWN.LifestealRate += 0.1 * statMultiplier;
                userSSWN.Mana += 15000000 * statMultiplier;
                userSSWN.ManaRegenerationRate += 0.1 * statMultiplier;
                userSSWN.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                userSSWN.Tenacity += 0.5 * statMultiplier;
                userSSWN.ResistanceRate += 0.1 * statMultiplier;
                userSSWN.ComboRate += 0.1 * statMultiplier;
                userSSWN.IgnoreComboRate += 0.1 * statMultiplier;
                userSSWN.ComboDamageRate += 0.1 * statMultiplier;
                userSSWN.ComboResistanceRate += 0.1 * statMultiplier;
                userSSWN.StunRate += 0.1 * statMultiplier;
                userSSWN.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                userSSWN.ReflectionRate += 0.1 * statMultiplier;
                userSSWN.IgnoreReflectionRate += 0.1 * statMultiplier;
                userSSWN.ReflectionDamageRate += 0.1 * statMultiplier;
                userSSWN.ReflectionResistanceRate += 0.1 * statMultiplier;
                userSSWN.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userSSWN.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userSSWN.DamageToSameFactionRate += 0.1 * statMultiplier;
                userSSWN.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                userSSWN.NormalDamageRate += 0.1 * statMultiplier;
                userSSWN.NormalResistanceRate += 0.1 * statMultiplier;
                userSSWN.SkillDamageRate += 0.1 * statMultiplier;
                userSSWN.SkillResistanceRate += 0.1 * statMultiplier;
                userSSWN.PercentAllHealth += 5 * statMultiplier;
                userSSWN.PercentAllPhysicalAttack += 5 * statMultiplier;
                userSSWN.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                userSSWN.PercentAllMagicalAttack += 5 * statMultiplier;
                userSSWN.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                userSSWN.PercentAllChemicalAttack += 5 * statMultiplier;
                userSSWN.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                userSSWN.PercentAllAtomicAttack += 5 * statMultiplier;
                userSSWN.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                userSSWN.PercentAllMentalAttack += 5 * statMultiplier;
                userSSWN.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                userSSWN.PhysicalAttack += 15000000 * statMultiplier;
                userSSWN.MagicalAttack += 15000000 * statMultiplier;
                userSSWN.ChemicalAttack += 15000000 * statMultiplier;
                userSSWN.AtomicAttack += 15000000 * statMultiplier;
                userSSWN.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                userSSWN.PhysicalDefense += 15000000 * statMultiplier;
                userSSWN.MagicalDefense += 15000000 * statMultiplier;
                userSSWN.ChemicalDefense += 15000000 * statMultiplier;
                userSSWN.AtomicDefense += 15000000 * statMultiplier;
                userSSWN.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                userSSWN.Speed += 15000000 * statMultiplier;
                userSSWN.CriticalDamageRate += 0.1 * statMultiplier;
                userSSWN.CriticalRate += 0.1 * statMultiplier;
                userSSWN.PenetrationRate += 0.1 * statMultiplier;
                userSSWN.EvasionRate += 0.1 * statMultiplier;
                userSSWN.DamageAbsorptionRate += 0.1 * statMultiplier;
                userSSWN.VitalityRegenerationRate += 0.1 * statMultiplier;
                userSSWN.AccuracyRate += 0.1 * statMultiplier;
                userSSWN.LifestealRate += 0.1 * statMultiplier;
                userSSWN.Mana += 15000000 * statMultiplier;
                userSSWN.ManaRegenerationRate += 0.1 * statMultiplier;
                userSSWN.ShieldStrength += 15000000 * statMultiplier;
                userSSWN.Tenacity += 0.5 * statMultiplier;
                userSSWN.ResistanceRate += 0.1 * statMultiplier;
                userSSWN.ComboRate += 0.1 * statMultiplier;
                userSSWN.ReflectionRate += 0.1 * statMultiplier;
                userSSWN.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                userSSWN.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                userSSWN.DamageToSameFactionRate += 0.1 * statMultiplier;
                userSSWN.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        userSSWN.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return userSSWN;
    }
    public static UserModules EnhanceModules(UserModules userModule, int level, double multiplier = 1)
    {
        int startLevel = userModule.CurrentLevel;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 10)
            {
                userModule.CurrentMultiplier += 1 * statMultiplier;
            }
            else if (lvl > 11 && lvl <= 20)
            {
                userModule.CurrentMultiplier += 2 * statMultiplier;
            }
            else if (lvl > 21 && lvl <= 30)
            {
                userModule.CurrentMultiplier += 3 * statMultiplier;
            }
            else if (lvl > 31 && lvl <= 40)
            {
                userModule.CurrentMultiplier += 4 * statMultiplier;
            }
            else if (lvl > 41 && lvl <= 50)
            {
                userModule.CurrentMultiplier += 5 * statMultiplier;
            }
            else if (lvl > 51 && lvl <= 60)
            {
                userModule.CurrentMultiplier += 6 * statMultiplier;
            }
            else if (lvl > 61 && lvl <= 70)
            {
                userModule.CurrentMultiplier += 7 * statMultiplier;
            }
            else if (lvl > 71 && lvl <= 80)
            {
                userModule.CurrentMultiplier += 8 * statMultiplier;
            }
            else if (lvl > 81 && lvl <= 90)
            {
                userModule.CurrentMultiplier += 9 * statMultiplier;
            }
            else if (lvl > 91 && lvl <= 100)
            {
                userModule.CurrentMultiplier += 10 * statMultiplier;
            }
        }

        userModule.CurrentLevel = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return userModule;
    }
    public static UserUpgrades EnhanceUpgrades(UserUpgrades userUpgrade, int level, double multiplier = 1)
    {
        int startLevel = userUpgrade.CurrentLevel;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            double statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 10)
            {
                userUpgrade.CurrentMultiplier += 1 * statMultiplier;
            }
            else if (lvl > 11 && lvl <= 20)
            {
                userUpgrade.CurrentMultiplier += 2 * statMultiplier;
            }
            else if (lvl > 21 && lvl <= 30)
            {
                userUpgrade.CurrentMultiplier += 3 * statMultiplier;
            }
            else if (lvl > 31 && lvl <= 40)
            {
                userUpgrade.CurrentMultiplier += 4 * statMultiplier;
            }
            else if (lvl > 41 && lvl <= 50)
            {
                userUpgrade.CurrentMultiplier += 5 * statMultiplier;
            }
            else if (lvl > 51 && lvl <= 60)
            {
                userUpgrade.CurrentMultiplier += 6 * statMultiplier;
            }
            else if (lvl > 61 && lvl <= 70)
            {
                userUpgrade.CurrentMultiplier += 7 * statMultiplier;
            }
            else if (lvl > 71 && lvl <= 80)
            {
                userUpgrade.CurrentMultiplier += 8 * statMultiplier;
            }
            else if (lvl > 81 && lvl <= 90)
            {
                userUpgrade.CurrentMultiplier += 9 * statMultiplier;
            }
            else if (lvl > 91 && lvl <= 100)
            {
                userUpgrade.CurrentMultiplier += 10 * statMultiplier;
            }
        }

        userUpgrade.CurrentLevel = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return userUpgrade;
    }
}