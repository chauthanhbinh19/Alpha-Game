using System.Collections.Generic;

public class UserCardsService : IUserCardsService
{
    private readonly IUserCardsRepository _userCardsRepository;

    public UserCardsService(IUserCardsRepository userCardsRepository)
    {
        _userCardsRepository = userCardsRepository;
    }

    public static UserCardsService Create()
    {
        return new UserCardsService(new UserCardsRepository());
    }

    public Cards GetNewLevelPower(Cards c, double coefficient)
    {
        ICardsRepository _repository = new CardsRepository();
        CardsService _service = new CardsService(_repository);
        Cards orginCard = _service.GetCardsById(c.Id);
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
    public Cards GetNewBreakthroughPower(Cards c, double coefficient)
    {
        ICardsRepository _repository = new CardsRepository();
        CardsService _service = new CardsService(_repository);
        Cards orginCard = _service.GetCardsById(c.Id);
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

    public List<Cards> GetUserCards(string user_id, int pageSize, int offset, string rare)
    {
        List<Cards> list = _userCardsRepository.GetUserCards(user_id, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserCardsCount(string user_id, string rare)
    {
        return _userCardsRepository.GetUserCardsCount(user_id, rare);
    }

    public bool InsertUserCards(Cards Cards)
    {
        return _userCardsRepository.InsertUserCards(Cards);
    }

    public bool UpdateCardsLevel(Cards Cards, int cardLevel)
    {
        return _userCardsRepository.UpdateCardsLevel(Cards, cardLevel);
    }

    public bool UpdateCardsBreakthrough(Cards Cards, int star, double quantity)
    {
        return _userCardsRepository.UpdateCardsBreakthrough(Cards, star, quantity);
    }

    public Cards GetUserCardsById(string user_id, string Id)
    {
        return _userCardsRepository.GetUserCardsById(user_id, Id);
    }

    public Cards SumPowerUserCards()
    {
        return _userCardsRepository.SumPowerUserCards();
    }
}
