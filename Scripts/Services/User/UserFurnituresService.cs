using System.Collections.Generic;
using System.Threading.Tasks;

public class UserFurnituresService : IUserFurnituresService
{
     private static UserFurnituresService _instance;
    private readonly IUserFurnituresRepository _userFurnituresRepository;

    public UserFurnituresService(IUserFurnituresRepository userFurnituresRepository)
    {
        _userFurnituresRepository = userFurnituresRepository;
    }

    public static UserFurnituresService Create()
    {
        if (_instance == null)
        {
            _instance = new UserFurnituresService(new UserFurnituresRepository());
        }
        return _instance;
    }

    public async Task<Furnitures> GetNewLevelPowerAsync(Furnitures c, double coefficient)
    {
        IFurnituresRepository _repository = new FurnituresRepository();
        FurnituresService _service = new FurnituresService(_repository);
        Furnitures orginCard = await _service.GetFurnitureByIdAsync(c.Id);
        Furnitures furniture = new Furnitures
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
        furniture.Power = PowerHelper.CalculatePower(
            furniture.Health,
            furniture.PhysicalAttack, furniture.PhysicalDefense,
            furniture.MagicalAttack, furniture.MagicalDefense,
            furniture.ChemicalAttack, furniture.ChemicalDefense,
            furniture.AtomicAttack, furniture.AtomicDefense,
            furniture.MentalAttack, furniture.MentalDefense,
            furniture.Speed,
            furniture.CriticalDamageRate, furniture.CriticalRate, furniture.CriticalResistanceRate, furniture.IgnoreCriticalRate,
            furniture.PenetrationRate, furniture.PenetrationResistanceRate, furniture.EvasionRate,
            furniture.DamageAbsorptionRate, furniture.IgnoreDamageAbsorptionRate, furniture.AbsorbedDamageRate,
            furniture.VitalityRegenerationRate, furniture.VitalityRegenerationResistanceRate,
            furniture.AccuracyRate, furniture.LifestealRate,
            furniture.ShieldStrength, furniture.Tenacity, furniture.ResistanceRate,
            furniture.ComboRate, furniture.IgnoreComboRate, furniture.ComboDamageRate, furniture.ComboResistanceRate,
            furniture.StunRate, furniture.IgnoreStunRate,
            furniture.ReflectionRate, furniture.IgnoreReflectionRate, furniture.ReflectionDamageRate, furniture.ReflectionResistanceRate,
            furniture.Mana, furniture.ManaRegenerationRate,
            furniture.DamageToDifferentFactionRate, furniture.ResistanceToDifferentFactionRate,
            furniture.DamageToSameFactionRate, furniture.ResistanceToSameFactionRate,
            furniture.NormalDamageRate, furniture.NormalResistanceRate,
            furniture.SkillDamageRate, furniture.SkillResistanceRate
        );
        return furniture;
    }
    public async Task<Furnitures> GetNewBreakthroughPowerAsync(Furnitures c, double coefficient)
    {
        IFurnituresRepository _repository = new FurnituresRepository();
        FurnituresService _service = new FurnituresService(_repository);
        Furnitures orginCard = await _service.GetFurnitureByIdAsync(c.Id);
        Furnitures furniture = new Furnitures
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
        furniture.Power = PowerHelper.CalculatePower(
            furniture.Health,
            furniture.PhysicalAttack, furniture.PhysicalDefense,
            furniture.MagicalAttack, furniture.MagicalDefense,
            furniture.ChemicalAttack, furniture.ChemicalDefense,
            furniture.AtomicAttack, furniture.AtomicDefense,
            furniture.MentalAttack, furniture.MentalDefense,
            furniture.Speed,
            furniture.CriticalDamageRate, furniture.CriticalRate, furniture.CriticalResistanceRate, furniture.IgnoreCriticalRate,
            furniture.PenetrationRate, furniture.PenetrationResistanceRate, furniture.EvasionRate,
            furniture.DamageAbsorptionRate, furniture.IgnoreDamageAbsorptionRate, furniture.AbsorbedDamageRate,
            furniture.VitalityRegenerationRate, furniture.VitalityRegenerationResistanceRate,
            furniture.AccuracyRate, furniture.LifestealRate,
            furniture.ShieldStrength, furniture.Tenacity, furniture.ResistanceRate,
            furniture.ComboRate, furniture.IgnoreComboRate, furniture.ComboDamageRate, furniture.ComboResistanceRate,
            furniture.StunRate, furniture.IgnoreStunRate,
            furniture.ReflectionRate, furniture.IgnoreReflectionRate, furniture.ReflectionDamageRate, furniture.ReflectionResistanceRate,
            furniture.Mana, furniture.ManaRegenerationRate,
            furniture.DamageToDifferentFactionRate, furniture.ResistanceToDifferentFactionRate,
            furniture.DamageToSameFactionRate, furniture.ResistanceToSameFactionRate,
            furniture.NormalDamageRate, furniture.NormalResistanceRate,
            furniture.SkillDamageRate, furniture.SkillResistanceRate
        );
        return furniture;
    }

    public async Task<List<Furnitures>> GetUserFurnituresAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Furnitures> list = await _userFurnituresRepository.GetUserFurnituresAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserFurnituresCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userFurnituresRepository.GetUserFurnituresCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserFurnitureAsync(Furnitures Furniture, string userId)
    {
        return await _userFurnituresRepository.InsertUserFurnitureAsync(Furniture, userId);
    }

    public async Task<bool> UpdateFurnitureLevelAsync(Furnitures Furniture, int cardLevel)
    {
        return await _userFurnituresRepository.UpdateFurnitureLevelAsync(Furniture, cardLevel);
    }

    public async Task<bool> UpdateFurnitureBreakthroughAsync(Furnitures Furniture, int star, double quantity)
    {
        return await _userFurnituresRepository.UpdateFurnitureBreakthroughAsync(Furniture, star, quantity);
    }

    public async Task<Furnitures> GetUserFurnitureByIdAsync(string user_id, string Id)
    {
        return await _userFurnituresRepository.GetUserFurnitureByIdAsync(user_id, Id);
    }

    public async Task<Furnitures> SumPowerUserFurnituresAsync()
    {
        return await _userFurnituresRepository.SumPowerUserFurnituresAsync();
    }
}
