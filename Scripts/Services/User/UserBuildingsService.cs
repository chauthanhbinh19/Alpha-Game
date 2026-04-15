using System.Collections.Generic;
using System.Threading.Tasks;

public class UserBuildingsService : IUserBuildingsService
{
     private static UserBuildingsService _instance;
    private readonly IUserBuildingsRepository _userBuildingsRepository;

    public UserBuildingsService(IUserBuildingsRepository userBuildingsRepository)
    {
        _userBuildingsRepository = userBuildingsRepository;
    }

    public static UserBuildingsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserBuildingsService(new UserBuildingsRepository());
        }
        return _instance;
    }

    public async Task<Buildings> GetNewLevelPowerAsync(Buildings c, double coefficient)
    {
        IBuildingsRepository _repository = new BuildingsRepository();
        BuildingsService _service = new BuildingsService(_repository);
        Buildings orginCard = await _service.GetBuildingByIdAsync(c.Id);
        Buildings building = new Buildings
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
        building.Power = PowerHelper.CalculatePower(
            building.Health,
            building.PhysicalAttack, building.PhysicalDefense,
            building.MagicalAttack, building.MagicalDefense,
            building.ChemicalAttack, building.ChemicalDefense,
            building.AtomicAttack, building.AtomicDefense,
            building.MentalAttack, building.MentalDefense,
            building.Speed,
            building.CriticalDamageRate, building.CriticalRate, building.CriticalResistanceRate, building.IgnoreCriticalRate,
            building.PenetrationRate, building.PenetrationResistanceRate, building.EvasionRate,
            building.DamageAbsorptionRate, building.IgnoreDamageAbsorptionRate, building.AbsorbedDamageRate,
            building.VitalityRegenerationRate, building.VitalityRegenerationResistanceRate,
            building.AccuracyRate, building.LifestealRate,
            building.ShieldStrength, building.Tenacity, building.ResistanceRate,
            building.ComboRate, building.IgnoreComboRate, building.ComboDamageRate, building.ComboResistanceRate,
            building.StunRate, building.IgnoreStunRate,
            building.ReflectionRate, building.IgnoreReflectionRate, building.ReflectionDamageRate, building.ReflectionResistanceRate,
            building.Mana, building.ManaRegenerationRate,
            building.DamageToDifferentFactionRate, building.ResistanceToDifferentFactionRate,
            building.DamageToSameFactionRate, building.ResistanceToSameFactionRate,
            building.NormalDamageRate, building.NormalResistanceRate,
            building.SkillDamageRate, building.SkillResistanceRate
        );
        return building;
    }
    public async Task<Buildings> GetNewBreakthroughPowerAsync(Buildings c, double coefficient)
    {
        IBuildingsRepository _repository = new BuildingsRepository();
        BuildingsService _service = new BuildingsService(_repository);
        Buildings orginCard = await _service.GetBuildingByIdAsync(c.Id);
        Buildings building = new Buildings
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
        building.Power = PowerHelper.CalculatePower(
            building.Health,
            building.PhysicalAttack, building.PhysicalDefense,
            building.MagicalAttack, building.MagicalDefense,
            building.ChemicalAttack, building.ChemicalDefense,
            building.AtomicAttack, building.AtomicDefense,
            building.MentalAttack, building.MentalDefense,
            building.Speed,
            building.CriticalDamageRate, building.CriticalRate, building.CriticalResistanceRate, building.IgnoreCriticalRate,
            building.PenetrationRate, building.PenetrationResistanceRate, building.EvasionRate,
            building.DamageAbsorptionRate, building.IgnoreDamageAbsorptionRate, building.AbsorbedDamageRate,
            building.VitalityRegenerationRate, building.VitalityRegenerationResistanceRate,
            building.AccuracyRate, building.LifestealRate,
            building.ShieldStrength, building.Tenacity, building.ResistanceRate,
            building.ComboRate, building.IgnoreComboRate, building.ComboDamageRate, building.ComboResistanceRate,
            building.StunRate, building.IgnoreStunRate,
            building.ReflectionRate, building.IgnoreReflectionRate, building.ReflectionDamageRate, building.ReflectionResistanceRate,
            building.Mana, building.ManaRegenerationRate,
            building.DamageToDifferentFactionRate, building.ResistanceToDifferentFactionRate,
            building.DamageToSameFactionRate, building.ResistanceToSameFactionRate,
            building.NormalDamageRate, building.NormalResistanceRate,
            building.SkillDamageRate, building.SkillResistanceRate
        );
        return building;
    }

    public async Task<List<Buildings>> GetUserBuildingsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Buildings> list = await _userBuildingsRepository.GetUserBuildingsAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserBuildingsCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userBuildingsRepository.GetUserBuildingsCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserBuildingAsync(Buildings Building, string userId)
    {
        return await _userBuildingsRepository.InsertUserBuildingAsync(Building, userId);
    }

    public async Task<bool> UpdateBuildingLevelAsync(Buildings Building, int cardLevel)
    {
        return await _userBuildingsRepository.UpdateBuildingLevelAsync(Building, cardLevel);
    }

    public async Task<bool> UpdateBuildingBreakthroughAsync(Buildings Building, int star, double quantity)
    {
        return await _userBuildingsRepository.UpdateBuildingBreakthroughAsync(Building, star, quantity);
    }

    public async Task<Buildings> GetUserBuildingByIdAsync(string user_id, string Id)
    {
        return await _userBuildingsRepository.GetUserBuildingByIdAsync(user_id, Id);
    }

    public async Task<Buildings> SumPowerUserBuildingsAsync()
    {
        return await _userBuildingsRepository.SumPowerUserBuildingsAsync();
    }
}
