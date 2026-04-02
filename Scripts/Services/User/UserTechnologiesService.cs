using System.Collections.Generic;
using System.Threading.Tasks;

public class UserTechnologiesService : IUserTechnologiesService
{
     private static UserTechnologiesService _instance;
    private readonly IUserTechnologiesRepository _userTechnologiesRepository;

    public UserTechnologiesService(IUserTechnologiesRepository userTechnologiesRepository)
    {
        _userTechnologiesRepository = userTechnologiesRepository;
    }

    public static UserTechnologiesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserTechnologiesService(new UserTechnologiesRepository());
        }
        return _instance;
    }

    public async Task<Technologies> GetNewLevelPowerAsync(Technologies c, double coefficient)
    {
        ITechnologiesRepository _repository = new TechnologiesRepository();
        TechnologiesService _service = new TechnologiesService(_repository);
        Technologies orginCard = await _service.GetTechnologyByIdAsync(c.Id);
        Technologies technology = new Technologies
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
        technology.Power = EvaluatePower.CalculatePower(
            technology.Health,
            technology.PhysicalAttack, technology.PhysicalDefense,
            technology.MagicalAttack, technology.MagicalDefense,
            technology.ChemicalAttack, technology.ChemicalDefense,
            technology.AtomicAttack, technology.AtomicDefense,
            technology.MentalAttack, technology.MentalDefense,
            technology.Speed,
            technology.CriticalDamageRate, technology.CriticalRate, technology.CriticalResistanceRate, technology.IgnoreCriticalRate,
            technology.PenetrationRate, technology.PenetrationResistanceRate, technology.EvasionRate,
            technology.DamageAbsorptionRate, technology.IgnoreDamageAbsorptionRate, technology.AbsorbedDamageRate,
            technology.VitalityRegenerationRate, technology.VitalityRegenerationResistanceRate,
            technology.AccuracyRate, technology.LifestealRate,
            technology.ShieldStrength, technology.Tenacity, technology.ResistanceRate,
            technology.ComboRate, technology.IgnoreComboRate, technology.ComboDamageRate, technology.ComboResistanceRate,
            technology.StunRate, technology.IgnoreStunRate,
            technology.ReflectionRate, technology.IgnoreReflectionRate, technology.ReflectionDamageRate, technology.ReflectionResistanceRate,
            technology.Mana, technology.ManaRegenerationRate,
            technology.DamageToDifferentFactionRate, technology.ResistanceToDifferentFactionRate,
            technology.DamageToSameFactionRate, technology.ResistanceToSameFactionRate,
            technology.NormalDamageRate, technology.NormalResistanceRate,
            technology.SkillDamageRate, technology.SkillResistanceRate
        );
        return technology;
    }
    public async Task<Technologies> GetNewBreakthroughPowerAsync(Technologies c, double coefficient)
    {
        ITechnologiesRepository _repository = new TechnologiesRepository();
        TechnologiesService _service = new TechnologiesService(_repository);
        Technologies orginCard = await _service.GetTechnologyByIdAsync(c.Id);
        Technologies technology = new Technologies
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
        technology.Power = EvaluatePower.CalculatePower(
            technology.Health,
            technology.PhysicalAttack, technology.PhysicalDefense,
            technology.MagicalAttack, technology.MagicalDefense,
            technology.ChemicalAttack, technology.ChemicalDefense,
            technology.AtomicAttack, technology.AtomicDefense,
            technology.MentalAttack, technology.MentalDefense,
            technology.Speed,
            technology.CriticalDamageRate, technology.CriticalRate, technology.CriticalResistanceRate, technology.IgnoreCriticalRate,
            technology.PenetrationRate, technology.PenetrationResistanceRate, technology.EvasionRate,
            technology.DamageAbsorptionRate, technology.IgnoreDamageAbsorptionRate, technology.AbsorbedDamageRate,
            technology.VitalityRegenerationRate, technology.VitalityRegenerationResistanceRate,
            technology.AccuracyRate, technology.LifestealRate,
            technology.ShieldStrength, technology.Tenacity, technology.ResistanceRate,
            technology.ComboRate, technology.IgnoreComboRate, technology.ComboDamageRate, technology.ComboResistanceRate,
            technology.StunRate, technology.IgnoreStunRate,
            technology.ReflectionRate, technology.IgnoreReflectionRate, technology.ReflectionDamageRate, technology.ReflectionResistanceRate,
            technology.Mana, technology.ManaRegenerationRate,
            technology.DamageToDifferentFactionRate, technology.ResistanceToDifferentFactionRate,
            technology.DamageToSameFactionRate, technology.ResistanceToSameFactionRate,
            technology.NormalDamageRate, technology.NormalResistanceRate,
            technology.SkillDamageRate, technology.SkillResistanceRate
        );
        return technology;
    }

    public async Task<List<Technologies>> GetUserTechnologiesAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Technologies> list = await _userTechnologiesRepository.GetUserTechnologiesAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserTechnologiesCountAsync(string user_id, string search, string rare)
    {
        return await _userTechnologiesRepository.GetUserTechnologiesCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserTechnologyAsync(Technologies Technologies, string userId)
    {
        return await _userTechnologiesRepository.InsertUserTechnologyAsync(Technologies, userId);
    }

    public async Task<bool> UpdateTechnologyLevelAsync(Technologies Technologies, int cardLevel)
    {
        return await _userTechnologiesRepository.UpdateTechnologyLevelAsync(Technologies, cardLevel);
    }

    public async Task<bool> UpdateTechnologyBreakthroughAsync(Technologies Technologies, int star, double quantity)
    {
        return await _userTechnologiesRepository.UpdateTechnologyBreakthroughAsync(Technologies, star, quantity);
    }

    public async Task<Technologies> GetUserTechnologyByIdAsync(string user_id, string Id)
    {
        return await _userTechnologiesRepository.GetUserTechnologyByIdAsync(user_id, Id);
    }

    public async Task<Technologies> SumPowerUserTechnologiesAsync()
    {
        return await _userTechnologiesRepository.SumPowerUserTechnologiesAsync();
    }
}
