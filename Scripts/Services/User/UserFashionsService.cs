using System.Collections.Generic;
using System.Threading.Tasks;

public class UserFashionsService : IUserFashionsService
{
     private static UserFashionsService _instance;
    private readonly IUserFashionsRepository _userFashionsRepository;

    public UserFashionsService(IUserFashionsRepository userFashionsRepository)
    {
        _userFashionsRepository = userFashionsRepository;
    }

    public static UserFashionsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserFashionsService(new UserFashionsRepository());
        }
        return _instance;
    }

    public async Task<Fashions> GetNewLevelPowerAsync(Fashions c, double coefficient)
    {
        IFashionsRepository _repository = new FashionsRepository();
        FashionsService _service = new FashionsService(_repository);
        Fashions orginCard = await _service.GetFashionByIdAsync(c.Id);
        Fashions Fashion = new Fashions
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
        Fashion.Power = EvaluatePower.CalculatePower(
            Fashion.Health,
            Fashion.PhysicalAttack, Fashion.PhysicalDefense,
            Fashion.MagicalAttack, Fashion.MagicalDefense,
            Fashion.ChemicalAttack, Fashion.ChemicalDefense,
            Fashion.AtomicAttack, Fashion.AtomicDefense,
            Fashion.MentalAttack, Fashion.MentalDefense,
            Fashion.Speed,
            Fashion.CriticalDamageRate, Fashion.CriticalRate, Fashion.CriticalResistanceRate, Fashion.IgnoreCriticalRate,
            Fashion.PenetrationRate, Fashion.PenetrationResistanceRate, Fashion.EvasionRate,
            Fashion.DamageAbsorptionRate, Fashion.IgnoreDamageAbsorptionRate, Fashion.AbsorbedDamageRate,
            Fashion.VitalityRegenerationRate, Fashion.VitalityRegenerationResistanceRate,
            Fashion.AccuracyRate, Fashion.LifestealRate,
            Fashion.ShieldStrength, Fashion.Tenacity, Fashion.ResistanceRate,
            Fashion.ComboRate, Fashion.IgnoreComboRate, Fashion.ComboDamageRate, Fashion.ComboResistanceRate,
            Fashion.StunRate, Fashion.IgnoreStunRate,
            Fashion.ReflectionRate, Fashion.IgnoreReflectionRate, Fashion.ReflectionDamageRate, Fashion.ReflectionResistanceRate,
            Fashion.Mana, Fashion.ManaRegenerationRate,
            Fashion.DamageToDifferentFactionRate, Fashion.ResistanceToDifferentFactionRate,
            Fashion.DamageToSameFactionRate, Fashion.ResistanceToSameFactionRate,
            Fashion.NormalDamageRate, Fashion.NormalResistanceRate,
            Fashion.SkillDamageRate, Fashion.SkillResistanceRate
        );
        return Fashion;
    }
    public async Task<Fashions> GetNewBreakthroughPowerAsync(Fashions c, double coefficient)
    {
        IFashionsRepository _repository = new FashionsRepository();
        FashionsService _service = new FashionsService(_repository);
        Fashions orginCard = await _service.GetFashionByIdAsync(c.Id);
        Fashions Fashion = new Fashions
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
        Fashion.Power = EvaluatePower.CalculatePower(
            Fashion.Health,
            Fashion.PhysicalAttack, Fashion.PhysicalDefense,
            Fashion.MagicalAttack, Fashion.MagicalDefense,
            Fashion.ChemicalAttack, Fashion.ChemicalDefense,
            Fashion.AtomicAttack, Fashion.AtomicDefense,
            Fashion.MentalAttack, Fashion.MentalDefense,
            Fashion.Speed,
            Fashion.CriticalDamageRate, Fashion.CriticalRate, Fashion.CriticalResistanceRate, Fashion.IgnoreCriticalRate,
            Fashion.PenetrationRate, Fashion.PenetrationResistanceRate, Fashion.EvasionRate,
            Fashion.DamageAbsorptionRate, Fashion.IgnoreDamageAbsorptionRate, Fashion.AbsorbedDamageRate,
            Fashion.VitalityRegenerationRate, Fashion.VitalityRegenerationResistanceRate,
            Fashion.AccuracyRate, Fashion.LifestealRate,
            Fashion.ShieldStrength, Fashion.Tenacity, Fashion.ResistanceRate,
            Fashion.ComboRate, Fashion.IgnoreComboRate, Fashion.ComboDamageRate, Fashion.ComboResistanceRate,
            Fashion.StunRate, Fashion.IgnoreStunRate,
            Fashion.ReflectionRate, Fashion.IgnoreReflectionRate, Fashion.ReflectionDamageRate, Fashion.ReflectionResistanceRate,
            Fashion.Mana, Fashion.ManaRegenerationRate,
            Fashion.DamageToDifferentFactionRate, Fashion.ResistanceToDifferentFactionRate,
            Fashion.DamageToSameFactionRate, Fashion.ResistanceToSameFactionRate,
            Fashion.NormalDamageRate, Fashion.NormalResistanceRate,
            Fashion.SkillDamageRate, Fashion.SkillResistanceRate
        );
        return Fashion;
    }

    public async Task<List<Fashions>> GetUserFashionsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Fashions> list = await _userFashionsRepository.GetUserFashionsAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetUserFashionsCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userFashionsRepository.GetUserFashionsCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserFashionAsync(Fashions Fashion, string userId)
    {
        return await _userFashionsRepository.InsertUserFashionAsync(Fashion, userId);
    }

    public async Task<bool> UpdateFashionLevelAsync(Fashions Fashion, int cardLevel)
    {
        return await _userFashionsRepository.UpdateFashionLevelAsync(Fashion, cardLevel);
    }

    public async Task<bool> UpdateFashionBreakthroughAsync(Fashions Fashion, int star, double quantity)
    {
        return await _userFashionsRepository.UpdateFashionBreakthroughAsync(Fashion, star, quantity);
    }

    public async Task<Fashions> GetUserFashionByIdAsync(string user_id, string Id)
    {
        return await _userFashionsRepository.GetUserFashionByIdAsync(user_id, Id);
    }

    public async Task<Fashions> SumPowerUserFashionsAsync()
    {
        return await _userFashionsRepository.SumPowerUserFashionsAsync();
    }
}
