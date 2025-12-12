using System.Collections.Generic;
using System.Threading.Tasks;

public class UserBeveragesService : IUserBeveragesService
{
    private readonly IUserBeveragesRepository _userBeveragesRepository;

    public UserBeveragesService(IUserBeveragesRepository userBeveragesRepository)
    {
        _userBeveragesRepository = userBeveragesRepository;
    }

    public static UserBeveragesService Create()
    {
        return new UserBeveragesService(new UserBeveragesRepository());
    }

    public async Task<Beverages> GetNewLevelPowerAsync(Beverages c, double coefficient)
    {
        IBeveragesRepository _repository = new BeveragesRepository();
        BeveragesService _service = new BeveragesService(_repository);
        Beverages orginCard = await _service.GetBeverageByIdAsync(c.Id);
        Beverages Beverages = new Beverages
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
        Beverages.Power = EvaluatePower.CalculatePower(
            Beverages.Health,
            Beverages.PhysicalAttack, Beverages.PhysicalDefense,
            Beverages.MagicalAttack, Beverages.MagicalDefense,
            Beverages.ChemicalAttack, Beverages.ChemicalDefense,
            Beverages.AtomicAttack, Beverages.AtomicDefense,
            Beverages.MentalAttack, Beverages.MentalDefense,
            Beverages.Speed,
            Beverages.CriticalDamageRate, Beverages.CriticalRate, Beverages.CriticalResistanceRate, Beverages.IgnoreCriticalRate,
            Beverages.PenetrationRate, Beverages.PenetrationResistanceRate, Beverages.EvasionRate,
            Beverages.DamageAbsorptionRate, Beverages.IgnoreDamageAbsorptionRate, Beverages.AbsorbedDamageRate,
            Beverages.VitalityRegenerationRate, Beverages.VitalityRegenerationResistanceRate,
            Beverages.AccuracyRate, Beverages.LifestealRate,
            Beverages.ShieldStrength, Beverages.Tenacity, Beverages.ResistanceRate,
            Beverages.ComboRate, Beverages.IgnoreComboRate, Beverages.ComboDamageRate, Beverages.ComboResistanceRate,
            Beverages.StunRate, Beverages.IgnoreStunRate,
            Beverages.ReflectionRate, Beverages.IgnoreReflectionRate, Beverages.ReflectionDamageRate, Beverages.ReflectionResistanceRate,
            Beverages.Mana, Beverages.ManaRegenerationRate,
            Beverages.DamageToDifferentFactionRate, Beverages.ResistanceToDifferentFactionRate,
            Beverages.DamageToSameFactionRate, Beverages.ResistanceToSameFactionRate,
            Beverages.NormalDamageRate, Beverages.NormalResistanceRate,
            Beverages.SkillDamageRate, Beverages.SkillResistanceRate
        );
        return Beverages;
    }
    public async Task<Beverages> GetNewBreakthroughPowerAsync(Beverages c, double coefficient)
    {
        IBeveragesRepository _repository = new BeveragesRepository();
        BeveragesService _service = new BeveragesService(_repository);
        Beverages orginCard = await _service.GetBeverageByIdAsync(c.Id);
        Beverages Beverages = new Beverages
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
        Beverages.Power = EvaluatePower.CalculatePower(
            Beverages.Health,
            Beverages.PhysicalAttack, Beverages.PhysicalDefense,
            Beverages.MagicalAttack, Beverages.MagicalDefense,
            Beverages.ChemicalAttack, Beverages.ChemicalDefense,
            Beverages.AtomicAttack, Beverages.AtomicDefense,
            Beverages.MentalAttack, Beverages.MentalDefense,
            Beverages.Speed,
            Beverages.CriticalDamageRate, Beverages.CriticalRate, Beverages.CriticalResistanceRate, Beverages.IgnoreCriticalRate,
            Beverages.PenetrationRate, Beverages.PenetrationResistanceRate, Beverages.EvasionRate,
            Beverages.DamageAbsorptionRate, Beverages.IgnoreDamageAbsorptionRate, Beverages.AbsorbedDamageRate,
            Beverages.VitalityRegenerationRate, Beverages.VitalityRegenerationResistanceRate,
            Beverages.AccuracyRate, Beverages.LifestealRate,
            Beverages.ShieldStrength, Beverages.Tenacity, Beverages.ResistanceRate,
            Beverages.ComboRate, Beverages.IgnoreComboRate, Beverages.ComboDamageRate, Beverages.ComboResistanceRate,
            Beverages.StunRate, Beverages.IgnoreStunRate,
            Beverages.ReflectionRate, Beverages.IgnoreReflectionRate, Beverages.ReflectionDamageRate, Beverages.ReflectionResistanceRate,
            Beverages.Mana, Beverages.ManaRegenerationRate,
            Beverages.DamageToDifferentFactionRate, Beverages.ResistanceToDifferentFactionRate,
            Beverages.DamageToSameFactionRate, Beverages.ResistanceToSameFactionRate,
            Beverages.NormalDamageRate, Beverages.NormalResistanceRate,
            Beverages.SkillDamageRate, Beverages.SkillResistanceRate
        );
        return Beverages;
    }

    public async Task<List<Beverages>> GetUserBeveragesAsync(string user_id, int pageSize, int offset, string rare)
    {
        List<Beverages> list = await _userBeveragesRepository.GetUserBeveragesAsync(user_id, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetUserBeveragesCountAsync(string user_id, string rare)
    {
        return await _userBeveragesRepository.GetUserBeveragesCountAsync(user_id, rare);
    }

    public async Task<bool> InsertUserBeverageAsync(Beverages Beverages, string userId)
    {
        return await _userBeveragesRepository.InsertUserBeverageAsync(Beverages, userId);
    }

    public async Task<bool> UpdateBeverageLevelAsync(Beverages Beverages, int cardLevel)
    {
        return await _userBeveragesRepository.UpdateBeverageLevelAsync(Beverages, cardLevel);
    }

    public async Task<bool> UpdateBeverageBreakthroughAsync(Beverages Beverages, int star, double quantity)
    {
        return await _userBeveragesRepository.UpdateBeverageBreakthroughAsync(Beverages, star, quantity);
    }

    public async Task<Beverages> GetUserBeverageByIdAsync(string user_id, string Id)
    {
        return await _userBeveragesRepository.GetUserBeverageByIdAsync(user_id, Id);
    }

    public async Task<Beverages> SumPowerUserBeveragesAsync()
    {
        return await _userBeveragesRepository.SumPowerUserBeveragesAsync();
    }
}
