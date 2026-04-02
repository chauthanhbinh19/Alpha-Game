using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCardLivesService : IUserCardLivesService
{
     private static UserCardLivesService _instance;
    private readonly IUserCardLivesRepository _userCardLivesRepository;

    public UserCardLivesService(IUserCardLivesRepository userCardLivesRepository)
    {
        _userCardLivesRepository = userCardLivesRepository;
    }

    public static UserCardLivesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardLivesService(new UserCardLivesRepository());
        }
        return _instance;
    }

    public async Task<CardLives> GetNewLevelPowerAsync(CardLives c, double coefficient)
    {
        ICardLivesRepository _repository = new CardLivesRepository();
        CardLivesService _service = new CardLivesService(_repository);
        CardLives orginCard = await _service.GetCardLifeByIdAsync(c.Id);
        CardLives cardLife = new CardLives
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
        cardLife.Power = EvaluatePower.CalculatePower(
            cardLife.Health,
            cardLife.PhysicalAttack, cardLife.PhysicalDefense,
            cardLife.MagicalAttack, cardLife.MagicalDefense,
            cardLife.ChemicalAttack, cardLife.ChemicalDefense,
            cardLife.AtomicAttack, cardLife.AtomicDefense,
            cardLife.MentalAttack, cardLife.MentalDefense,
            cardLife.Speed,
            cardLife.CriticalDamageRate, cardLife.CriticalRate, cardLife.CriticalResistanceRate, cardLife.IgnoreCriticalRate,
            cardLife.PenetrationRate, cardLife.PenetrationResistanceRate, cardLife.EvasionRate,
            cardLife.DamageAbsorptionRate, cardLife.IgnoreDamageAbsorptionRate, cardLife.AbsorbedDamageRate,
            cardLife.VitalityRegenerationRate, cardLife.VitalityRegenerationResistanceRate,
            cardLife.AccuracyRate, cardLife.LifestealRate,
            cardLife.ShieldStrength, cardLife.Tenacity, cardLife.ResistanceRate,
            cardLife.ComboRate, cardLife.IgnoreComboRate, cardLife.ComboDamageRate, cardLife.ComboResistanceRate,
            cardLife.StunRate, cardLife.IgnoreStunRate,
            cardLife.ReflectionRate, cardLife.IgnoreReflectionRate, cardLife.ReflectionDamageRate, cardLife.ReflectionResistanceRate,
            cardLife.Mana, cardLife.ManaRegenerationRate,
            cardLife.DamageToDifferentFactionRate, cardLife.ResistanceToDifferentFactionRate,
            cardLife.DamageToSameFactionRate, cardLife.ResistanceToSameFactionRate,
            cardLife.NormalDamageRate, cardLife.NormalResistanceRate,
            cardLife.SkillDamageRate, cardLife.SkillResistanceRate
        );
        return cardLife;
    }
    public async Task<CardLives> GetNewBreakthroughPowerAsync(CardLives c, double coefficient)
    {
        ICardLivesRepository _repository = new CardLivesRepository();
        CardLivesService _service = new CardLivesService(_repository);
        CardLives orginCard = await _service.GetCardLifeByIdAsync(c.Id);
        CardLives cardLife = new CardLives
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
        cardLife.Power = EvaluatePower.CalculatePower(
            cardLife.Health,
            cardLife.PhysicalAttack, cardLife.PhysicalDefense,
            cardLife.MagicalAttack, cardLife.MagicalDefense,
            cardLife.ChemicalAttack, cardLife.ChemicalDefense,
            cardLife.AtomicAttack, cardLife.AtomicDefense,
            cardLife.MentalAttack, cardLife.MentalDefense,
            cardLife.Speed,
            cardLife.CriticalDamageRate, cardLife.CriticalRate, cardLife.CriticalResistanceRate, cardLife.IgnoreCriticalRate,
            cardLife.PenetrationRate, cardLife.PenetrationResistanceRate, cardLife.EvasionRate,
            cardLife.DamageAbsorptionRate, cardLife.IgnoreDamageAbsorptionRate, cardLife.AbsorbedDamageRate,
            cardLife.VitalityRegenerationRate, cardLife.VitalityRegenerationResistanceRate,
            cardLife.AccuracyRate, cardLife.LifestealRate,
            cardLife.ShieldStrength, cardLife.Tenacity, cardLife.ResistanceRate,
            cardLife.ComboRate, cardLife.IgnoreComboRate, cardLife.ComboDamageRate, cardLife.ComboResistanceRate,
            cardLife.StunRate, cardLife.IgnoreStunRate,
            cardLife.ReflectionRate, cardLife.IgnoreReflectionRate, cardLife.ReflectionDamageRate, cardLife.ReflectionResistanceRate,
            cardLife.Mana, cardLife.ManaRegenerationRate,
            cardLife.DamageToDifferentFactionRate, cardLife.ResistanceToDifferentFactionRate,
            cardLife.DamageToSameFactionRate, cardLife.ResistanceToSameFactionRate,
            cardLife.NormalDamageRate, cardLife.NormalResistanceRate,
            cardLife.SkillDamageRate, cardLife.SkillResistanceRate
        );
        return cardLife;
    }

    public async Task<List<CardLives>> GetUserCardLivesAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardLives> list = await _userCardLivesRepository.GetUserCardLivesAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserCardLivesCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userCardLivesRepository.GetUserCardLivesCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserCardLifeAsync(CardLives cardLife, string userId)
    {
        return await _userCardLivesRepository.InsertUserCardLifeAsync(cardLife, userId);
    }

    public async Task<bool> UpdateCardLifeLevelAsync(CardLives cardLife, int cardLevel)
    {
        return await _userCardLivesRepository.UpdateCardLifeLevelAsync(cardLife, cardLevel);
    }

    public async Task<bool> UpdateCardLifeBreakthroughAsync(CardLives cardLife, int star, double quantity)
    {
        return await _userCardLivesRepository.UpdateCardLifeBreakthroughAsync(cardLife, star, quantity);
    }

    public async Task<CardLives> GetUserCardLifeByIdAsync(string user_id, string Id)
    {
        return await _userCardLivesRepository.GetUserCardLifeByIdAsync(user_id, Id);
    }

    public async Task<CardLives> SumPowerUserCardLivesAsync()
    {
        return await _userCardLivesRepository.SumPowerUserCardLivesAsync();
    }
}
