using System.Collections.Generic;

public class UserCardLifeService : IUserCardLifeService
{
    private readonly IUserCardLifeRepository _userCardLifeRepository;

    public UserCardLifeService(IUserCardLifeRepository userCardLifeRepository)
    {
        _userCardLifeRepository = userCardLifeRepository;
    }

    public static UserCardLifeService Create()
    {
        return new UserCardLifeService(new UserCardLifeRepository());
    }

    public CardLives GetNewLevelPower(CardLives c, double coefficient)
    {
        ICardLifeRepository _repository = new CardLifeRepository();
        CardLifeService _service = new CardLifeService(_repository);
        CardLives orginCard = _service.GetCardLifeById(c.Id);
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
    public CardLives GetNewBreakthroughPower(CardLives c, double coefficient)
    {
        ICardLifeRepository _repository = new CardLifeRepository();
        CardLifeService _service = new CardLifeService(_repository);
        CardLives orginCard = _service.GetCardLifeById(c.Id);
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

    public List<CardLives> GetUserCardLife(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<CardLives> list = _userCardLifeRepository.GetUserCardLife(user_id, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserCardLifeCount(string user_id, string type, string rare)
    {
        return _userCardLifeRepository.GetUserCardLifeCount(user_id, type, rare);
    }

    public bool InsertUserCardLife(CardLives cardLife, string userId)
    {
        return _userCardLifeRepository.InsertUserCardLife(cardLife, userId);
    }

    public bool UpdateCardLifeLevel(CardLives cardLife, int cardLevel)
    {
        return _userCardLifeRepository.UpdateCardLifeLevel(cardLife, cardLevel);
    }

    public bool UpdateCardLifeBreakthrough(CardLives cardLife, int star, double quantity)
    {
        return _userCardLifeRepository.UpdateCardLifeBreakthrough(cardLife, star, quantity);
    }

    public CardLives GetUserCardLifeById(string user_id, string Id)
    {
        return _userCardLifeRepository.GetUserCardLifeById(user_id, Id);
    }

    public CardLives SumPowerUserCardLife()
    {
        return _userCardLifeRepository.SumPowerUserCardLife();
    }
}
