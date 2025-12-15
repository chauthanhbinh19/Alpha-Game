using System.Collections.Generic;
using System.Threading.Tasks;

public class UserFurnituresService : IUserFurnituresService
{
    private readonly IUserFurnituresRepository _userFurnitureRepository;

    public UserFurnituresService(IUserFurnituresRepository userFurnitureRepository)
    {
        _userFurnitureRepository = userFurnitureRepository;
    }

    public static UserFurnituresService Create()
    {
        return new UserFurnituresService(new UserFurnituresRepository());
    }

    public async Task<Furnitures> GetNewLevelPowerAsync(Furnitures c, double coefficient)
    {
        IFurnituresRepository _repository = new FurnituresRepository();
        FurnituresService _service = new FurnituresService(_repository);
        Furnitures orginCard = await _service.GetFurnitureByIdAsync(c.Id);
        Furnitures Furniture = new Furnitures
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
        Furniture.Power = EvaluatePower.CalculatePower(
            Furniture.Health,
            Furniture.PhysicalAttack, Furniture.PhysicalDefense,
            Furniture.MagicalAttack, Furniture.MagicalDefense,
            Furniture.ChemicalAttack, Furniture.ChemicalDefense,
            Furniture.AtomicAttack, Furniture.AtomicDefense,
            Furniture.MentalAttack, Furniture.MentalDefense,
            Furniture.Speed,
            Furniture.CriticalDamageRate, Furniture.CriticalRate, Furniture.CriticalResistanceRate, Furniture.IgnoreCriticalRate,
            Furniture.PenetrationRate, Furniture.PenetrationResistanceRate, Furniture.EvasionRate,
            Furniture.DamageAbsorptionRate, Furniture.IgnoreDamageAbsorptionRate, Furniture.AbsorbedDamageRate,
            Furniture.VitalityRegenerationRate, Furniture.VitalityRegenerationResistanceRate,
            Furniture.AccuracyRate, Furniture.LifestealRate,
            Furniture.ShieldStrength, Furniture.Tenacity, Furniture.ResistanceRate,
            Furniture.ComboRate, Furniture.IgnoreComboRate, Furniture.ComboDamageRate, Furniture.ComboResistanceRate,
            Furniture.StunRate, Furniture.IgnoreStunRate,
            Furniture.ReflectionRate, Furniture.IgnoreReflectionRate, Furniture.ReflectionDamageRate, Furniture.ReflectionResistanceRate,
            Furniture.Mana, Furniture.ManaRegenerationRate,
            Furniture.DamageToDifferentFactionRate, Furniture.ResistanceToDifferentFactionRate,
            Furniture.DamageToSameFactionRate, Furniture.ResistanceToSameFactionRate,
            Furniture.NormalDamageRate, Furniture.NormalResistanceRate,
            Furniture.SkillDamageRate, Furniture.SkillResistanceRate
        );
        return Furniture;
    }
    public async Task<Furnitures> GetNewBreakthroughPowerAsync(Furnitures c, double coefficient)
    {
        IFurnituresRepository _repository = new FurnituresRepository();
        FurnituresService _service = new FurnituresService(_repository);
        Furnitures orginCard = await _service.GetFurnitureByIdAsync(c.Id);
        Furnitures Furniture = new Furnitures
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
        Furniture.Power = EvaluatePower.CalculatePower(
            Furniture.Health,
            Furniture.PhysicalAttack, Furniture.PhysicalDefense,
            Furniture.MagicalAttack, Furniture.MagicalDefense,
            Furniture.ChemicalAttack, Furniture.ChemicalDefense,
            Furniture.AtomicAttack, Furniture.AtomicDefense,
            Furniture.MentalAttack, Furniture.MentalDefense,
            Furniture.Speed,
            Furniture.CriticalDamageRate, Furniture.CriticalRate, Furniture.CriticalResistanceRate, Furniture.IgnoreCriticalRate,
            Furniture.PenetrationRate, Furniture.PenetrationResistanceRate, Furniture.EvasionRate,
            Furniture.DamageAbsorptionRate, Furniture.IgnoreDamageAbsorptionRate, Furniture.AbsorbedDamageRate,
            Furniture.VitalityRegenerationRate, Furniture.VitalityRegenerationResistanceRate,
            Furniture.AccuracyRate, Furniture.LifestealRate,
            Furniture.ShieldStrength, Furniture.Tenacity, Furniture.ResistanceRate,
            Furniture.ComboRate, Furniture.IgnoreComboRate, Furniture.ComboDamageRate, Furniture.ComboResistanceRate,
            Furniture.StunRate, Furniture.IgnoreStunRate,
            Furniture.ReflectionRate, Furniture.IgnoreReflectionRate, Furniture.ReflectionDamageRate, Furniture.ReflectionResistanceRate,
            Furniture.Mana, Furniture.ManaRegenerationRate,
            Furniture.DamageToDifferentFactionRate, Furniture.ResistanceToDifferentFactionRate,
            Furniture.DamageToSameFactionRate, Furniture.ResistanceToSameFactionRate,
            Furniture.NormalDamageRate, Furniture.NormalResistanceRate,
            Furniture.SkillDamageRate, Furniture.SkillResistanceRate
        );
        return Furniture;
    }

    public async Task<List<Furnitures>> GetUserFurnituresAsync(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<Furnitures> list = await _userFurnitureRepository.GetUserFurnituresAsync(user_id, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetUserFurnituresCountAsync(string user_id, string type, string rare)
    {
        return await _userFurnitureRepository.GetUserFurnituresCountAsync(user_id, type, rare);
    }

    public async Task<bool> InsertUserFurnitureAsync(Furnitures Furniture, string userId)
    {
        return await _userFurnitureRepository.InsertUserFurnitureAsync(Furniture, userId);
    }

    public async Task<bool> UpdateFurnitureLevelAsync(Furnitures Furniture, int cardLevel)
    {
        return await _userFurnitureRepository.UpdateFurnitureLevelAsync(Furniture, cardLevel);
    }

    public async Task<bool> UpdateFurnitureBreakthroughAsync(Furnitures Furniture, int star, double quantity)
    {
        return await _userFurnitureRepository.UpdateFurnitureBreakthroughAsync(Furniture, star, quantity);
    }

    public async Task<Furnitures> GetUserFurnitureByIdAsync(string user_id, string Id)
    {
        return await _userFurnitureRepository.GetUserFurnitureByIdAsync(user_id, Id);
    }

    public async Task<Furnitures> SumPowerUserFurnituresAsync()
    {
        return await _userFurnitureRepository.SumPowerUserFurnituresAsync();
    }
}
