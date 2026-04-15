using System.Collections.Generic;
using System.Threading.Tasks;

public class UserPlantsService : IUserPlantsService
{
     private static UserPlantsService _instance;
    private readonly IUserPlantsRepository _userPlantsRepository;

    public UserPlantsService(IUserPlantsRepository userPlantsRepository)
    {
        _userPlantsRepository = userPlantsRepository;
    }

    public static UserPlantsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserPlantsService(new UserPlantsRepository());
        }
        return _instance;
    }

    public async Task<Plants> GetNewLevelPowerAsync(Plants c, double coefficient)
    {
        IPlantsRepository _repository = new PlantsRepository();
        PlantsService _service = new PlantsService(_repository);
        Plants orginCard = await _service.GetPlantByIdAsync(c.Id);
        Plants plant = new Plants
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
        plant.Power = PowerHelper.CalculatePower(
            plant.Health,
            plant.PhysicalAttack, plant.PhysicalDefense,
            plant.MagicalAttack, plant.MagicalDefense,
            plant.ChemicalAttack, plant.ChemicalDefense,
            plant.AtomicAttack, plant.AtomicDefense,
            plant.MentalAttack, plant.MentalDefense,
            plant.Speed,
            plant.CriticalDamageRate, plant.CriticalRate, plant.CriticalResistanceRate, plant.IgnoreCriticalRate,
            plant.PenetrationRate, plant.PenetrationResistanceRate, plant.EvasionRate,
            plant.DamageAbsorptionRate, plant.IgnoreDamageAbsorptionRate, plant.AbsorbedDamageRate,
            plant.VitalityRegenerationRate, plant.VitalityRegenerationResistanceRate,
            plant.AccuracyRate, plant.LifestealRate,
            plant.ShieldStrength, plant.Tenacity, plant.ResistanceRate,
            plant.ComboRate, plant.IgnoreComboRate, plant.ComboDamageRate, plant.ComboResistanceRate,
            plant.StunRate, plant.IgnoreStunRate,
            plant.ReflectionRate, plant.IgnoreReflectionRate, plant.ReflectionDamageRate, plant.ReflectionResistanceRate,
            plant.Mana, plant.ManaRegenerationRate,
            plant.DamageToDifferentFactionRate, plant.ResistanceToDifferentFactionRate,
            plant.DamageToSameFactionRate, plant.ResistanceToSameFactionRate,
            plant.NormalDamageRate, plant.NormalResistanceRate,
            plant.SkillDamageRate, plant.SkillResistanceRate
        );
        return plant;
    }
    public async Task<Plants> GetNewBreakthroughPowerAsync(Plants c, double coefficient)
    {
        IPlantsRepository _repository = new PlantsRepository();
        PlantsService _service = new PlantsService(_repository);
        Plants orginCard = await _service.GetPlantByIdAsync(c.Id);
        Plants plant = new Plants
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
        plant.Power = PowerHelper.CalculatePower(
            plant.Health,
            plant.PhysicalAttack, plant.PhysicalDefense,
            plant.MagicalAttack, plant.MagicalDefense,
            plant.ChemicalAttack, plant.ChemicalDefense,
            plant.AtomicAttack, plant.AtomicDefense,
            plant.MentalAttack, plant.MentalDefense,
            plant.Speed,
            plant.CriticalDamageRate, plant.CriticalRate, plant.CriticalResistanceRate, plant.IgnoreCriticalRate,
            plant.PenetrationRate, plant.PenetrationResistanceRate, plant.EvasionRate,
            plant.DamageAbsorptionRate, plant.IgnoreDamageAbsorptionRate, plant.AbsorbedDamageRate,
            plant.VitalityRegenerationRate, plant.VitalityRegenerationResistanceRate,
            plant.AccuracyRate, plant.LifestealRate,
            plant.ShieldStrength, plant.Tenacity, plant.ResistanceRate,
            plant.ComboRate, plant.IgnoreComboRate, plant.ComboDamageRate, plant.ComboResistanceRate,
            plant.StunRate, plant.IgnoreStunRate,
            plant.ReflectionRate, plant.IgnoreReflectionRate, plant.ReflectionDamageRate, plant.ReflectionResistanceRate,
            plant.Mana, plant.ManaRegenerationRate,
            plant.DamageToDifferentFactionRate, plant.ResistanceToDifferentFactionRate,
            plant.DamageToSameFactionRate, plant.ResistanceToSameFactionRate,
            plant.NormalDamageRate, plant.NormalResistanceRate,
            plant.SkillDamageRate, plant.SkillResistanceRate
        );
        return plant;
    }

    public async Task<List<Plants>> GetUserPlantsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Plants> list = await _userPlantsRepository.GetUserPlantsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserPlantsCountAsync(string user_id, string search, string rare)
    {
        return await _userPlantsRepository.GetUserPlantsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserPlantAsync(Plants Plants, string userId)
    {
        return await _userPlantsRepository.InsertUserPlantAsync(Plants, userId);
    }

    public async Task<bool> UpdatePlantLevelAsync(Plants Plants, int cardLevel)
    {
        return await _userPlantsRepository.UpdatePlantLevelAsync(Plants, cardLevel);
    }

    public async Task<bool> UpdatePlantBreakthroughAsync(Plants Plants, int star, double quantity)
    {
        return await _userPlantsRepository.UpdatePlantBreakthroughAsync(Plants, star, quantity);
    }

    public async Task<Plants> GetUserPlantByIdAsync(string user_id, string Id)
    {
        return await _userPlantsRepository.GetUserPlantByIdAsync(user_id, Id);
    }

    public async Task<Plants> SumPowerUserPlantsAsync()
    {
        return await _userPlantsRepository.SumPowerUserPlantsAsync();
    }
}
