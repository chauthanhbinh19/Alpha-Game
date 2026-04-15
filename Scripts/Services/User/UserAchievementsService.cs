using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class UserAchievementsService : IUserAchievementsService
{
     private static UserAchievementsService _instance;
    private IUserAchievementsRepository _userAchievementsRepository;

    public UserAchievementsService(IUserAchievementsRepository userAchievementsService)
    {
        _userAchievementsRepository = userAchievementsService;
    }

    public static UserAchievementsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserAchievementsService(new UserAchievementsRepository());
        }
        return _instance;
    }

    public async Task<Achievements> GetNewLevelPowerAsync(Achievements c, double coefficient)
    {
        // Achievements orginCard = new Achievements();
        IAchievementsRepository _repository = new AchievementsRepository();
        AchievementsService _service = new AchievementsService(_repository);
        Achievements orginCard = await _service.GetAchievementByIdAsync(c.Id);
        Achievements achievement = new Achievements
        {
            Id = c.Id,
            Health = c.Health + orginCard.Health * coefficient,
            PhysicalAttack = c.PhysicalAttack + orginCard.PhysicalAttack * coefficient,
            PhysicalDefense = c.PhysicalDefense + orginCard.PhysicalDefense * coefficient,
            MagicalAttack = c.MagicalAttack + orginCard.MagicalAttack * coefficient,
            MagicalDefense = c.MagicalDefense + orginCard.MagicalDefense * coefficient,
            ChemicalAttack = c.ChemicalAttack + orginCard.ChemicalAttack * coefficient,
            ChemicalDefense = c.ChemicalDefense + orginCard.ChemicalDefense * coefficient,
            AtomicAttack = c.AtomicAttack + orginCard.AtomicAttack * coefficient,
            AtomicDefense = c.AtomicDefense + orginCard.AtomicDefense * coefficient,
            MentalAttack = c.MentalAttack + orginCard.MentalAttack * coefficient,
            MentalDefense = c.MentalDefense + orginCard.MentalDefense * coefficient,
            Speed = c.Speed + orginCard.Speed * coefficient,
            CriticalDamageRate = c.CriticalDamageRate + orginCard.CriticalDamageRate * coefficient,
            CriticalRate = c.CriticalRate + orginCard.CriticalRate * coefficient,
            CriticalResistanceRate = c.CriticalResistanceRate + orginCard.CriticalResistanceRate * coefficient,
            IgnoreCriticalRate = c.IgnoreCriticalRate + orginCard.IgnoreCriticalRate * coefficient,
            PenetrationRate = c.PenetrationRate + orginCard.PenetrationRate * coefficient,
            PenetrationResistanceRate = c.PenetrationResistanceRate + orginCard.PenetrationResistanceRate * coefficient,
            EvasionRate = c.EvasionRate + orginCard.EvasionRate * coefficient,
            DamageAbsorptionRate = c.DamageAbsorptionRate + orginCard.DamageAbsorptionRate * coefficient,
            IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + orginCard.IgnoreDamageAbsorptionRate * coefficient,
            AbsorbedDamageRate = c.AbsorbedDamageRate + orginCard.AbsorbedDamageRate * coefficient,
            VitalityRegenerationRate = c.VitalityRegenerationRate + orginCard.VitalityRegenerationRate * coefficient,
            VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + orginCard.VitalityRegenerationResistanceRate * coefficient,
            AccuracyRate = c.AccuracyRate + orginCard.AccuracyRate * coefficient,
            LifestealRate = c.LifestealRate + orginCard.LifestealRate * coefficient,
            ShieldStrength = c.ShieldStrength + orginCard.ShieldStrength * coefficient,
            Tenacity = c.Tenacity + orginCard.Tenacity * coefficient,
            ResistanceRate = c.ResistanceRate + orginCard.ResistanceRate * coefficient,
            ComboRate = c.ComboRate + orginCard.ComboRate * coefficient,
            IgnoreComboRate = c.IgnoreComboRate + orginCard.IgnoreComboRate * coefficient,
            ComboDamageRate = c.ComboDamageRate + orginCard.ComboDamageRate * coefficient,
            ComboResistanceRate = c.ComboResistanceRate + orginCard.ComboResistanceRate * coefficient,
            StunRate = c.StunRate + orginCard.StunRate * coefficient,
            IgnoreStunRate = c.IgnoreStunRate + orginCard.IgnoreStunRate * coefficient,
            ReflectionRate = c.ReflectionRate + orginCard.ReflectionRate * coefficient,
            IgnoreReflectionRate  = c.IgnoreReflectionRate + orginCard.IgnoreReflectionRate * coefficient,
            ReflectionDamageRate = c.ReflectionDamageRate + orginCard.ReflectionDamageRate * coefficient,
            ReflectionResistanceRate = c.ReflectionResistanceRate + orginCard.ReflectionResistanceRate * coefficient,
            Mana = c.Mana + orginCard.Mana * (float)coefficient,
            ManaRegenerationRate = c.ManaRegenerationRate + orginCard.ManaRegenerationRate * coefficient,
            DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + orginCard.DamageToDifferentFactionRate * coefficient,
            ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + orginCard.ResistanceToDifferentFactionRate * coefficient,
            DamageToSameFactionRate = c.DamageToSameFactionRate + orginCard.DamageToSameFactionRate * coefficient,
            ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + orginCard.ResistanceToSameFactionRate * coefficient,
            NormalDamageRate = c.NormalDamageRate + orginCard.NormalDamageRate * coefficient,
            NormalResistanceRate = c.NormalResistanceRate + orginCard.NormalResistanceRate * coefficient,
            SkillDamageRate = c.SkillDamageRate + orginCard.SkillDamageRate * coefficient,
            SkillResistanceRate = c.SkillResistanceRate + orginCard.SkillResistanceRate * coefficient
        };
        achievement.Power = PowerHelper.CalculatePower(
            achievement.Health,
            achievement.PhysicalAttack, achievement.PhysicalDefense,
            achievement.MagicalAttack, achievement.MagicalDefense,
            achievement.ChemicalAttack, achievement.ChemicalDefense,
            achievement.AtomicAttack, achievement.AtomicDefense,
            achievement.MentalAttack, achievement.MentalDefense,
            achievement.Speed,
            achievement.CriticalDamageRate, achievement.CriticalRate, achievement.CriticalResistanceRate, achievement.IgnoreCriticalRate,
            achievement.PenetrationRate, achievement.PenetrationResistanceRate, achievement.EvasionRate,
            achievement.DamageAbsorptionRate, achievement.IgnoreDamageAbsorptionRate, achievement.AbsorbedDamageRate,
            achievement.VitalityRegenerationRate, achievement.VitalityRegenerationResistanceRate,
            achievement.AccuracyRate, achievement.LifestealRate,
            achievement.ShieldStrength, achievement.Tenacity, achievement.ResistanceRate,
            achievement.ComboRate, achievement.IgnoreComboRate, achievement.ComboDamageRate, achievement.ComboResistanceRate,
            achievement.StunRate, achievement.IgnoreStunRate,
            achievement.ReflectionRate, achievement.IgnoreReflectionRate, achievement.ReflectionDamageRate, achievement.ReflectionResistanceRate,
            achievement.Mana, achievement.ManaRegenerationRate,
            achievement.DamageToDifferentFactionRate, achievement.ResistanceToDifferentFactionRate,
            achievement.DamageToSameFactionRate, achievement.ResistanceToSameFactionRate,
            achievement.NormalDamageRate, achievement.NormalResistanceRate,
            achievement.SkillDamageRate, achievement.SkillResistanceRate
        );
        return achievement;
    }
    public async Task<Achievements> GetNewBreakthroughPowerAsync(Achievements c, double coefficient)
    {
        IAchievementsRepository _repository = new AchievementsRepository();
        AchievementsService _service = new AchievementsService(_repository);
        Achievements orginCard = await _service.GetAchievementByIdAsync(c.Id);
        Achievements achievement = new Achievements
        {
            Id = c.Id,
            Health = c.Health + orginCard.Health * coefficient,
            PhysicalAttack = c.PhysicalAttack + orginCard.PhysicalAttack * coefficient,
            PhysicalDefense = c.PhysicalDefense + orginCard.PhysicalDefense * coefficient,
            MagicalAttack = c.MagicalAttack + orginCard.MagicalAttack * coefficient,
            MagicalDefense = c.MagicalDefense + orginCard.MagicalDefense * coefficient,
            ChemicalAttack = c.ChemicalAttack + orginCard.ChemicalAttack * coefficient,
            ChemicalDefense = c.ChemicalDefense + orginCard.ChemicalDefense * coefficient,
            AtomicAttack = c.AtomicAttack + orginCard.AtomicAttack * coefficient,
            AtomicDefense = c.AtomicDefense + orginCard.AtomicDefense * coefficient,
            MentalAttack = c.MentalAttack + orginCard.MentalAttack * coefficient,
            MentalDefense = c.MentalDefense + orginCard.MentalDefense * coefficient,
            Speed = c.Speed + orginCard.Speed * coefficient,
            CriticalDamageRate = c.CriticalDamageRate + orginCard.CriticalDamageRate * coefficient,
            CriticalRate = c.CriticalRate + orginCard.CriticalRate * coefficient,
            CriticalResistanceRate = c.CriticalResistanceRate + orginCard.CriticalResistanceRate * coefficient,
            IgnoreCriticalRate = c.IgnoreCriticalRate + orginCard.IgnoreCriticalRate * coefficient,
            PenetrationRate = c.PenetrationRate + orginCard.PenetrationRate * coefficient,
            PenetrationResistanceRate = c.PenetrationResistanceRate + orginCard.PenetrationResistanceRate * coefficient,
            EvasionRate = c.EvasionRate + orginCard.EvasionRate * coefficient,
            DamageAbsorptionRate = c.DamageAbsorptionRate + orginCard.DamageAbsorptionRate * coefficient,
            IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + orginCard.IgnoreDamageAbsorptionRate * coefficient,
            AbsorbedDamageRate = c.AbsorbedDamageRate + orginCard.AbsorbedDamageRate * coefficient,
            VitalityRegenerationRate = c.VitalityRegenerationRate + orginCard.VitalityRegenerationRate * coefficient,
            VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + orginCard.VitalityRegenerationResistanceRate * coefficient,
            AccuracyRate = c.AccuracyRate + orginCard.AccuracyRate * coefficient,
            LifestealRate = c.LifestealRate + orginCard.LifestealRate * coefficient,
            ShieldStrength = c.ShieldStrength + orginCard.ShieldStrength * coefficient,
            Tenacity = c.Tenacity + orginCard.Tenacity * coefficient,
            ResistanceRate = c.ResistanceRate + orginCard.ResistanceRate * coefficient,
            ComboRate = c.ComboRate + orginCard.ComboRate * coefficient,
            IgnoreComboRate = c.IgnoreComboRate + orginCard.IgnoreComboRate * coefficient,
            ComboDamageRate = c.ComboDamageRate + orginCard.ComboDamageRate * coefficient,
            ComboResistanceRate = c.ComboResistanceRate + orginCard.ComboResistanceRate * coefficient,
            StunRate = c.StunRate + orginCard.StunRate * coefficient,
            IgnoreStunRate = c.IgnoreStunRate + orginCard.IgnoreStunRate * coefficient,
            ReflectionRate = c.ReflectionRate + orginCard.ReflectionRate * coefficient,
            IgnoreReflectionRate  = c.IgnoreReflectionRate + orginCard.IgnoreReflectionRate * coefficient,
            ReflectionDamageRate = c.ReflectionDamageRate + orginCard.ReflectionDamageRate * coefficient,
            ReflectionResistanceRate = c.ReflectionResistanceRate + orginCard.ReflectionResistanceRate * coefficient,
            Mana = c.Mana + orginCard.Mana * (float)coefficient,
            ManaRegenerationRate = c.ManaRegenerationRate + orginCard.ManaRegenerationRate * coefficient,
            DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + orginCard.DamageToDifferentFactionRate * coefficient,
            ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + orginCard.ResistanceToDifferentFactionRate * coefficient,
            DamageToSameFactionRate = c.DamageToSameFactionRate + orginCard.DamageToSameFactionRate * coefficient,
            ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + orginCard.ResistanceToSameFactionRate * coefficient,
            NormalDamageRate = c.NormalDamageRate + orginCard.NormalDamageRate * coefficient,
            NormalResistanceRate = c.NormalResistanceRate + orginCard.NormalResistanceRate * coefficient,
            SkillDamageRate = c.SkillDamageRate + orginCard.SkillDamageRate * coefficient,
            SkillResistanceRate = c.SkillResistanceRate + orginCard.SkillResistanceRate * coefficient
        };
        achievement.Power = PowerHelper.CalculatePower(
            achievement.Health,
            achievement.PhysicalAttack, achievement.PhysicalDefense,
            achievement.MagicalAttack, achievement.MagicalDefense,
            achievement.ChemicalAttack, achievement.ChemicalDefense,
            achievement.AtomicAttack, achievement.AtomicDefense,
            achievement.MentalAttack, achievement.MentalDefense,
            achievement.Speed,
            achievement.CriticalDamageRate, achievement.CriticalRate, achievement.CriticalResistanceRate, achievement.IgnoreCriticalRate,
            achievement.PenetrationRate, achievement.PenetrationResistanceRate, achievement.EvasionRate,
            achievement.DamageAbsorptionRate, achievement.IgnoreDamageAbsorptionRate, achievement.AbsorbedDamageRate,
            achievement.VitalityRegenerationRate, achievement.VitalityRegenerationResistanceRate,
            achievement.AccuracyRate, achievement.LifestealRate,
            achievement.ShieldStrength, achievement.Tenacity, achievement.ResistanceRate,
            achievement.ComboRate, achievement.IgnoreComboRate, achievement.ComboDamageRate, achievement.ComboResistanceRate,
            achievement.StunRate, achievement.IgnoreStunRate,
            achievement.ReflectionRate, achievement.IgnoreReflectionRate, achievement.ReflectionDamageRate, achievement.ReflectionResistanceRate,
            achievement.Mana, achievement.ManaRegenerationRate,
            achievement.DamageToDifferentFactionRate, achievement.ResistanceToDifferentFactionRate,
            achievement.DamageToSameFactionRate, achievement.ResistanceToSameFactionRate,
            achievement.NormalDamageRate, achievement.NormalResistanceRate,
            achievement.SkillDamageRate, achievement.SkillResistanceRate
        );
        return achievement;
    }

    public async Task<List<Achievements>> GetUserAchievementsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Achievements> list = await _userAchievementsRepository.GetUserAchievementsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserAchievementsCountAsync(string user_id, string search, string rare)
    {
        return await _userAchievementsRepository.GetUserArchievementsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserAchievementAsync(Achievements Achievements, string userId)
    {
        return await _userAchievementsRepository.InsertUserAchievementsAsync(Achievements, userId);
    }

    public async Task<bool> UpdateAchievementLevelAsync(Achievements achievements, int cardLevel)
    {
        return await _userAchievementsRepository.UpdateAchievementLevelAsync(achievements, cardLevel);
    }

    public async Task<bool> UpdateAchievementBreakthroughAsync(Achievements achievements, int star, double quantity)
    {
        return await _userAchievementsRepository.UpdateAchievementBreakthroughAsync(achievements, star, quantity);
    }

    public async Task<Achievements> GetUserAchievementByIdAsync(string user_id, string Id)
    {
        return await _userAchievementsRepository.GetUserAchievementByIdAsync(user_id, Id);
    }

    public async Task<Achievements> SumPowerUserAchievementsAsync()
    {
        return await _userAchievementsRepository.SumPowerUserAchievementsAsync();
    }
}