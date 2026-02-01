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
        CardLives CardLife = new CardLives
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
        CardLife.Power = EvaluatePower.CalculatePower(
            CardLife.Health,
            CardLife.PhysicalAttack, CardLife.PhysicalDefense,
            CardLife.MagicalAttack, CardLife.MagicalDefense,
            CardLife.ChemicalAttack, CardLife.ChemicalDefense,
            CardLife.AtomicAttack, CardLife.AtomicDefense,
            CardLife.MentalAttack, CardLife.MentalDefense,
            CardLife.Speed,
            CardLife.CriticalDamageRate, CardLife.CriticalRate, CardLife.CriticalResistanceRate, CardLife.IgnoreCriticalRate,
            CardLife.PenetrationRate, CardLife.PenetrationResistanceRate, CardLife.EvasionRate,
            CardLife.DamageAbsorptionRate, CardLife.IgnoreDamageAbsorptionRate, CardLife.AbsorbedDamageRate,
            CardLife.VitalityRegenerationRate, CardLife.VitalityRegenerationResistanceRate,
            CardLife.AccuracyRate, CardLife.LifestealRate,
            CardLife.ShieldStrength, CardLife.Tenacity, CardLife.ResistanceRate,
            CardLife.ComboRate, CardLife.IgnoreComboRate, CardLife.ComboDamageRate, CardLife.ComboResistanceRate,
            CardLife.StunRate, CardLife.IgnoreStunRate,
            CardLife.ReflectionRate, CardLife.IgnoreReflectionRate, CardLife.ReflectionDamageRate, CardLife.ReflectionResistanceRate,
            CardLife.Mana, CardLife.ManaRegenerationRate,
            CardLife.DamageToDifferentFactionRate, CardLife.ResistanceToDifferentFactionRate,
            CardLife.DamageToSameFactionRate, CardLife.ResistanceToSameFactionRate,
            CardLife.NormalDamageRate, CardLife.NormalResistanceRate,
            CardLife.SkillDamageRate, CardLife.SkillResistanceRate
        );
        return CardLife;
    }
    public async Task<CardLives> GetNewBreakthroughPowerAsync(CardLives c, double coefficient)
    {
        ICardLivesRepository _repository = new CardLivesRepository();
        CardLivesService _service = new CardLivesService(_repository);
        CardLives orginCard = await _service.GetCardLifeByIdAsync(c.Id);
        CardLives CardLife = new CardLives
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
        CardLife.Power = EvaluatePower.CalculatePower(
            CardLife.Health,
            CardLife.PhysicalAttack, CardLife.PhysicalDefense,
            CardLife.MagicalAttack, CardLife.MagicalDefense,
            CardLife.ChemicalAttack, CardLife.ChemicalDefense,
            CardLife.AtomicAttack, CardLife.AtomicDefense,
            CardLife.MentalAttack, CardLife.MentalDefense,
            CardLife.Speed,
            CardLife.CriticalDamageRate, CardLife.CriticalRate, CardLife.CriticalResistanceRate, CardLife.IgnoreCriticalRate,
            CardLife.PenetrationRate, CardLife.PenetrationResistanceRate, CardLife.EvasionRate,
            CardLife.DamageAbsorptionRate, CardLife.IgnoreDamageAbsorptionRate, CardLife.AbsorbedDamageRate,
            CardLife.VitalityRegenerationRate, CardLife.VitalityRegenerationResistanceRate,
            CardLife.AccuracyRate, CardLife.LifestealRate,
            CardLife.ShieldStrength, CardLife.Tenacity, CardLife.ResistanceRate,
            CardLife.ComboRate, CardLife.IgnoreComboRate, CardLife.ComboDamageRate, CardLife.ComboResistanceRate,
            CardLife.StunRate, CardLife.IgnoreStunRate,
            CardLife.ReflectionRate, CardLife.IgnoreReflectionRate, CardLife.ReflectionDamageRate, CardLife.ReflectionResistanceRate,
            CardLife.Mana, CardLife.ManaRegenerationRate,
            CardLife.DamageToDifferentFactionRate, CardLife.ResistanceToDifferentFactionRate,
            CardLife.DamageToSameFactionRate, CardLife.ResistanceToSameFactionRate,
            CardLife.NormalDamageRate, CardLife.NormalResistanceRate,
            CardLife.SkillDamageRate, CardLife.SkillResistanceRate
        );
        return CardLife;
    }

    public async Task<List<CardLives>> GetUserCardLivesAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardLives> list = await _userCardLivesRepository.GetUserCardLivesAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
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
