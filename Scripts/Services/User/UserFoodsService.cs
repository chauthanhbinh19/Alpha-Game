using System.Collections.Generic;
using System.Threading.Tasks;

public class UserFoodsService : IUserFoodsService
{
    private readonly IUserFoodsRepository _userFoodsRepository;

    public UserFoodsService(IUserFoodsRepository userFoodsRepository)
    {
        _userFoodsRepository = userFoodsRepository;
    }

    public static UserFoodsService Create()
    {
        return new UserFoodsService(new UserFoodsRepository());
    }

    public async Task<Foods> GetNewLevelPowerAsync(Foods c, double coefficient)
    {
        IFoodsRepository _repository = new FoodsRepository();
        FoodsService _service = new FoodsService(_repository);
        Foods orginCard = await _service.GetFoodByIdAsync(c.Id);
        Foods Foods = new Foods
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
        Foods.Power = EvaluatePower.CalculatePower(
            Foods.Health,
            Foods.PhysicalAttack, Foods.PhysicalDefense,
            Foods.MagicalAttack, Foods.MagicalDefense,
            Foods.ChemicalAttack, Foods.ChemicalDefense,
            Foods.AtomicAttack, Foods.AtomicDefense,
            Foods.MentalAttack, Foods.MentalDefense,
            Foods.Speed,
            Foods.CriticalDamageRate, Foods.CriticalRate, Foods.CriticalResistanceRate, Foods.IgnoreCriticalRate,
            Foods.PenetrationRate, Foods.PenetrationResistanceRate, Foods.EvasionRate,
            Foods.DamageAbsorptionRate, Foods.IgnoreDamageAbsorptionRate, Foods.AbsorbedDamageRate,
            Foods.VitalityRegenerationRate, Foods.VitalityRegenerationResistanceRate,
            Foods.AccuracyRate, Foods.LifestealRate,
            Foods.ShieldStrength, Foods.Tenacity, Foods.ResistanceRate,
            Foods.ComboRate, Foods.IgnoreComboRate, Foods.ComboDamageRate, Foods.ComboResistanceRate,
            Foods.StunRate, Foods.IgnoreStunRate,
            Foods.ReflectionRate, Foods.IgnoreReflectionRate, Foods.ReflectionDamageRate, Foods.ReflectionResistanceRate,
            Foods.Mana, Foods.ManaRegenerationRate,
            Foods.DamageToDifferentFactionRate, Foods.ResistanceToDifferentFactionRate,
            Foods.DamageToSameFactionRate, Foods.ResistanceToSameFactionRate,
            Foods.NormalDamageRate, Foods.NormalResistanceRate,
            Foods.SkillDamageRate, Foods.SkillResistanceRate
        );
        return Foods;
    }
    public async Task<Foods> GetNewBreakthroughPowerAsync(Foods c, double coefficient)
    {
        IFoodsRepository _repository = new FoodsRepository();
        FoodsService _service = new FoodsService(_repository);
        Foods orginCard = await _service.GetFoodByIdAsync(c.Id);
        Foods Foods = new Foods
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
        Foods.Power = EvaluatePower.CalculatePower(
            Foods.Health,
            Foods.PhysicalAttack, Foods.PhysicalDefense,
            Foods.MagicalAttack, Foods.MagicalDefense,
            Foods.ChemicalAttack, Foods.ChemicalDefense,
            Foods.AtomicAttack, Foods.AtomicDefense,
            Foods.MentalAttack, Foods.MentalDefense,
            Foods.Speed,
            Foods.CriticalDamageRate, Foods.CriticalRate, Foods.CriticalResistanceRate, Foods.IgnoreCriticalRate,
            Foods.PenetrationRate, Foods.PenetrationResistanceRate, Foods.EvasionRate,
            Foods.DamageAbsorptionRate, Foods.IgnoreDamageAbsorptionRate, Foods.AbsorbedDamageRate,
            Foods.VitalityRegenerationRate, Foods.VitalityRegenerationResistanceRate,
            Foods.AccuracyRate, Foods.LifestealRate,
            Foods.ShieldStrength, Foods.Tenacity, Foods.ResistanceRate,
            Foods.ComboRate, Foods.IgnoreComboRate, Foods.ComboDamageRate, Foods.ComboResistanceRate,
            Foods.StunRate, Foods.IgnoreStunRate,
            Foods.ReflectionRate, Foods.IgnoreReflectionRate, Foods.ReflectionDamageRate, Foods.ReflectionResistanceRate,
            Foods.Mana, Foods.ManaRegenerationRate,
            Foods.DamageToDifferentFactionRate, Foods.ResistanceToDifferentFactionRate,
            Foods.DamageToSameFactionRate, Foods.ResistanceToSameFactionRate,
            Foods.NormalDamageRate, Foods.NormalResistanceRate,
            Foods.SkillDamageRate, Foods.SkillResistanceRate
        );
        return Foods;
    }

    public async Task<List<Foods>> GetUserFoodsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Foods> list = await _userFoodsRepository.GetUserFoodsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetUserFoodsCountAsync(string user_id, string search, string rare)
    {
        return await _userFoodsRepository.GetUserFoodsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserFoodAsync(Foods Foods, string userId)
    {
        return await _userFoodsRepository.InsertUserFoodAsync(Foods, userId);
    }

    public async Task<bool> UpdateFoodLevelAsync(Foods Foods, int cardLevel)
    {
        return await _userFoodsRepository.UpdateFoodLevelAsync(Foods, cardLevel);
    }

    public async Task<bool> UpdateFoodBreakthroughAsync(Foods Foods, int star, double quantity)
    {
        return await _userFoodsRepository.UpdateFoodBreakthroughAsync(Foods, star, quantity);
    }

    public async Task<Foods> GetUserFoodByIdAsync(string user_id, string Id)
    {
        return await _userFoodsRepository.GetUserFoodByIdAsync(user_id, Id);
    }

    public async Task<Foods> SumPowerUserFoodsAsync()
    {
        return await _userFoodsRepository.SumPowerUserFoodsAsync();
    }
}
