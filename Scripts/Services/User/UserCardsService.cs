using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCardsService : IUserCardsService
{
     private static UserCardsService _instance;
    private readonly IUserCardsRepository _userCardsRepository;

    public UserCardsService(IUserCardsRepository userCardsRepository)
    {
        _userCardsRepository = userCardsRepository;
    }

    public static UserCardsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardsService(new UserCardsRepository());
        }
        return _instance;
    }

    public async Task<Cards> GetNewLevelPowerAsync(Cards c, double coefficient)
    {
        ICardsRepository _repository = new CardsRepository();
        CardsService _service = new CardsService(_repository);
        Cards orginCard = await _service.GetCardByIdAsync(c.Id);
        Cards Cards = new Cards
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
        Cards.Power = EvaluatePower.CalculatePower(
            Cards.Health,
            Cards.PhysicalAttack, Cards.PhysicalDefense,
            Cards.MagicalAttack, Cards.MagicalDefense,
            Cards.ChemicalAttack, Cards.ChemicalDefense,
            Cards.AtomicAttack, Cards.AtomicDefense,
            Cards.MentalAttack, Cards.MentalDefense,
            Cards.Speed,
            Cards.CriticalDamageRate, Cards.CriticalRate, Cards.CriticalResistanceRate, Cards.IgnoreCriticalRate,
            Cards.PenetrationRate, Cards.PenetrationResistanceRate, Cards.EvasionRate,
            Cards.DamageAbsorptionRate, Cards.IgnoreDamageAbsorptionRate, Cards.AbsorbedDamageRate,
            Cards.VitalityRegenerationRate, Cards.VitalityRegenerationResistanceRate,
            Cards.AccuracyRate, Cards.LifestealRate,
            Cards.ShieldStrength, Cards.Tenacity, Cards.ResistanceRate,
            Cards.ComboRate, Cards.IgnoreComboRate, Cards.ComboDamageRate, Cards.ComboResistanceRate,
            Cards.StunRate, Cards.IgnoreStunRate,
            Cards.ReflectionRate, Cards.IgnoreReflectionRate, Cards.ReflectionDamageRate, Cards.ReflectionResistanceRate,
            Cards.Mana, Cards.ManaRegenerationRate,
            Cards.DamageToDifferentFactionRate, Cards.ResistanceToDifferentFactionRate,
            Cards.DamageToSameFactionRate, Cards.ResistanceToSameFactionRate,
            Cards.NormalDamageRate, Cards.NormalResistanceRate,
            Cards.SkillDamageRate, Cards.SkillResistanceRate
        );
        return Cards;
    }
    public async Task<Cards> GetNewBreakthroughPowerAsync(Cards c, double coefficient)
    {
        ICardsRepository _repository = new CardsRepository();
        CardsService _service = new CardsService(_repository);
        Cards orginCard = await _service.GetCardByIdAsync(c.Id);
        Cards Cards = new Cards
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
        Cards.Power = EvaluatePower.CalculatePower(
            Cards.Health,
            Cards.PhysicalAttack, Cards.PhysicalDefense,
            Cards.MagicalAttack, Cards.MagicalDefense,
            Cards.ChemicalAttack, Cards.ChemicalDefense,
            Cards.AtomicAttack, Cards.AtomicDefense,
            Cards.MentalAttack, Cards.MentalDefense,
            Cards.Speed,
            Cards.CriticalDamageRate, Cards.CriticalRate, Cards.CriticalResistanceRate, Cards.IgnoreCriticalRate,
            Cards.PenetrationRate, Cards.PenetrationResistanceRate, Cards.EvasionRate,
            Cards.DamageAbsorptionRate, Cards.IgnoreDamageAbsorptionRate, Cards.AbsorbedDamageRate,
            Cards.VitalityRegenerationRate, Cards.VitalityRegenerationResistanceRate,
            Cards.AccuracyRate, Cards.LifestealRate,
            Cards.ShieldStrength, Cards.Tenacity, Cards.ResistanceRate,
            Cards.ComboRate, Cards.IgnoreComboRate, Cards.ComboDamageRate, Cards.ComboResistanceRate,
            Cards.StunRate, Cards.IgnoreStunRate,
            Cards.ReflectionRate, Cards.IgnoreReflectionRate, Cards.ReflectionDamageRate, Cards.ReflectionResistanceRate,
            Cards.Mana, Cards.ManaRegenerationRate,
            Cards.DamageToDifferentFactionRate, Cards.ResistanceToDifferentFactionRate,
            Cards.DamageToSameFactionRate, Cards.ResistanceToSameFactionRate,
            Cards.NormalDamageRate, Cards.NormalResistanceRate,
            Cards.SkillDamageRate, Cards.SkillResistanceRate
        );
        return Cards;
    }

    public async Task<List<Cards>> GetUserCardsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Cards> list = await _userCardsRepository.GetUserCardsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetUserCardsCountAsync(string user_id, string search, string rare)
    {
        return await _userCardsRepository.GetUserCardsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserCardAsync(Cards Cards, string userId)
    {
        return await _userCardsRepository.InsertUserCardAsync(Cards, userId);
    }

    public async Task<bool> UpdateCardLevelAsync(Cards Cards, int cardLevel)
    {
        return await _userCardsRepository.UpdateCardLevelAsync(Cards, cardLevel);
    }

    public async Task<bool> UpdateCardBreakthroughAsync(Cards Cards, int star, double quantity)
    {
        return await _userCardsRepository.UpdateCardBreakthroughAsync(Cards, star, quantity);
    }

    public async Task<Cards> GetUserCardByIdAsync(string user_id, string Id)
    {
        return await _userCardsRepository.GetUserCardByIdAsync(user_id, Id);
    }

    public async Task<Cards> SumPowerUserCardsAsync()
    {
        return await _userCardsRepository.SumPowerUserCardsAsync();
    }
}
