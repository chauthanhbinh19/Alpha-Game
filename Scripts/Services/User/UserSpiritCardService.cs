using System.Collections.Generic;

public class UserSpiritCardService : IUserSpiritCardService
{
    private readonly IUserSpiritCardRepository _userSpiritCardRepository;

    public UserSpiritCardService(IUserSpiritCardRepository userSpiritCardRepository)
    {
        _userSpiritCardRepository = userSpiritCardRepository;
    }

    public static UserSpiritCardService Create()
    {
        return new UserSpiritCardService(new UserSpiritCardRepository());
    }

    public SpiritCards GetNewLevelPower(SpiritCards c, double coefficient)
    {
        ISpiritCardRepository _repository = new SpiritCardRepository();
        SpiritCardService _service = new SpiritCardService(_repository);
        SpiritCards orginCard = _service.GetSpiritCardById(c.Id);
        SpiritCards SpiritCard = new SpiritCards
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
            IgnoreReflectionRate = c.IgnoreReflectionRate + orginCard.IgnoreReflectionRate * coefficient,
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
        SpiritCard.Power = EvaluatePower.CalculatePower(
            SpiritCard.Health,
            SpiritCard.PhysicalAttack, SpiritCard.PhysicalDefense,
            SpiritCard.MagicalAttack, SpiritCard.MagicalDefense,
            SpiritCard.ChemicalAttack, SpiritCard.ChemicalDefense,
            SpiritCard.AtomicAttack, SpiritCard.AtomicDefense,
            SpiritCard.MentalAttack, SpiritCard.MentalDefense,
            SpiritCard.Speed,
            SpiritCard.CriticalDamageRate, SpiritCard.CriticalRate, SpiritCard.CriticalResistanceRate, SpiritCard.IgnoreCriticalRate,
            SpiritCard.PenetrationRate, SpiritCard.PenetrationResistanceRate, SpiritCard.EvasionRate,
            SpiritCard.DamageAbsorptionRate, SpiritCard.IgnoreDamageAbsorptionRate, SpiritCard.AbsorbedDamageRate,
            SpiritCard.VitalityRegenerationRate, SpiritCard.VitalityRegenerationResistanceRate,
            SpiritCard.AccuracyRate, SpiritCard.LifestealRate,
            SpiritCard.ShieldStrength, SpiritCard.Tenacity, SpiritCard.ResistanceRate,
            SpiritCard.ComboRate, SpiritCard.IgnoreComboRate, SpiritCard.ComboDamageRate, SpiritCard.ComboResistanceRate,
            SpiritCard.StunRate, SpiritCard.IgnoreStunRate,
            SpiritCard.ReflectionRate, SpiritCard.IgnoreReflectionRate, SpiritCard.ReflectionDamageRate, SpiritCard.ReflectionResistanceRate,
            SpiritCard.Mana, SpiritCard.ManaRegenerationRate,
            SpiritCard.DamageToDifferentFactionRate, SpiritCard.ResistanceToDifferentFactionRate,
            SpiritCard.DamageToSameFactionRate, SpiritCard.ResistanceToSameFactionRate,
            SpiritCard.NormalDamageRate, SpiritCard.NormalResistanceRate,
            SpiritCard.SkillDamageRate, SpiritCard.SkillResistanceRate
        );
        return SpiritCard;
    }
    public SpiritCards GetNewBreakthroughPower(SpiritCards c, double coefficient)
    {
        ISpiritCardRepository _repository = new SpiritCardRepository();
        SpiritCardService _service = new SpiritCardService(_repository);
        SpiritCards orginCard = _service.GetSpiritCardById(c.Id);
        SpiritCards SpiritCard = new SpiritCards
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
            IgnoreReflectionRate = c.IgnoreReflectionRate + orginCard.IgnoreReflectionRate * coefficient,
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
        SpiritCard.Power = EvaluatePower.CalculatePower(
            SpiritCard.Health,
            SpiritCard.PhysicalAttack, SpiritCard.PhysicalDefense,
            SpiritCard.MagicalAttack, SpiritCard.MagicalDefense,
            SpiritCard.ChemicalAttack, SpiritCard.ChemicalDefense,
            SpiritCard.AtomicAttack, SpiritCard.AtomicDefense,
            SpiritCard.MentalAttack, SpiritCard.MentalDefense,
            SpiritCard.Speed,
            SpiritCard.CriticalDamageRate, SpiritCard.CriticalRate, SpiritCard.CriticalResistanceRate, SpiritCard.IgnoreCriticalRate,
            SpiritCard.PenetrationRate, SpiritCard.PenetrationResistanceRate, SpiritCard.EvasionRate,
            SpiritCard.DamageAbsorptionRate, SpiritCard.IgnoreDamageAbsorptionRate, SpiritCard.AbsorbedDamageRate,
            SpiritCard.VitalityRegenerationRate, SpiritCard.VitalityRegenerationResistanceRate,
            SpiritCard.AccuracyRate, SpiritCard.LifestealRate,
            SpiritCard.ShieldStrength, SpiritCard.Tenacity, SpiritCard.ResistanceRate,
            SpiritCard.ComboRate, SpiritCard.IgnoreComboRate, SpiritCard.ComboDamageRate, SpiritCard.ComboResistanceRate,
            SpiritCard.StunRate, SpiritCard.IgnoreStunRate,
            SpiritCard.ReflectionRate, SpiritCard.IgnoreReflectionRate, SpiritCard.ReflectionDamageRate, SpiritCard.ReflectionResistanceRate,
            SpiritCard.Mana, SpiritCard.ManaRegenerationRate,
            SpiritCard.DamageToDifferentFactionRate, SpiritCard.ResistanceToDifferentFactionRate,
            SpiritCard.DamageToSameFactionRate, SpiritCard.ResistanceToSameFactionRate,
            SpiritCard.NormalDamageRate, SpiritCard.NormalResistanceRate,
            SpiritCard.SkillDamageRate, SpiritCard.SkillResistanceRate
        );
        return SpiritCard;
    }

    public List<SpiritCards> GetUserSpiritCard(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<SpiritCards> list = _userSpiritCardRepository.GetUserSpiritCard(user_id, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<SpiritCards> GetAllUserSpiritCard(string user_id, int pageSize, int offset)
    {
        List<SpiritCards> list = _userSpiritCardRepository.GetAllUserSpiritCard(user_id, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserSpiritCardCount(string user_id, string type, string rare)
    {
        return _userSpiritCardRepository.GetUserSpiritCardCount(user_id, type, rare);
    }

    public bool InsertUserSpiritCard(SpiritCards SpiritCard)
    {
        return _userSpiritCardRepository.InsertUserSpiritCard(SpiritCard);
    }

    public bool UpdateSpiritCardLevel(SpiritCards SpiritCard, int cardLevel)
    {
        return _userSpiritCardRepository.UpdateSpiritCardLevel(SpiritCard, cardLevel);
    }

    public bool UpdateSpiritCardBreakthrough(SpiritCards SpiritCard, int star, double quantity)
    {
        return _userSpiritCardRepository.UpdateSpiritCardBreakthrough(SpiritCard, star, quantity);
    }

    public SpiritCards GetUserSpiritCardById(string user_id, string Id)
    {
        return _userSpiritCardRepository.GetUserSpiritCardById(user_id, Id);
    }

    public SpiritCards SumPowerUserSpiritCard()
    {
        return _userSpiritCardRepository.SumPowerUserSpiritCard();
    }

    public bool InsertOrUpdateUserCardHeroesSpiritCard(string userId, CardHeroes cardHeroes, SpiritCards spiritBeast)
    {
        return _userSpiritCardRepository.InsertOrUpdateUserCardHeroesSpiritCard(userId, cardHeroes, spiritBeast);
    }

    public bool InsertOrUpdateUserCardCaptainsSpiritCard(string userId, CardCaptains cardCaptains, SpiritCards spiritBeast)
    {
        return _userSpiritCardRepository.InsertOrUpdateUserCardCaptainsSpiritCard(userId, cardCaptains, spiritBeast);
    }

    public bool InsertOrUpdateUserCardColonelsSpiritCard(string userId, CardColonels cardColonels, SpiritCards spiritBeast)
    {
        return _userSpiritCardRepository.InsertOrUpdateUserCardColonelsSpiritCard(userId, cardColonels, spiritBeast);
    }

    public bool InsertOrUpdateUserCardGeneralsSpiritCard(string userId, CardGenerals cardGenerals, SpiritCards spiritBeast)
    {
        return _userSpiritCardRepository.InsertOrUpdateUserCardGeneralsSpiritCard(userId, cardGenerals, spiritBeast);
    }

    public bool InsertOrUpdateUserCardAdmiralsSpiritCard(string userId, CardAdmirals cardAdmirals, SpiritCards spiritBeast)
    {
        return _userSpiritCardRepository.InsertOrUpdateUserCardAdmiralsSpiritCard(userId, cardAdmirals, spiritBeast);
    }

    public bool InsertOrUpdateUserCardMilitarySpiritCard(string userId, CardMilitaries cardMilitary, SpiritCards spiritBeast)
    {
        return _userSpiritCardRepository.InsertOrUpdateUserCardMilitarySpiritCard(userId, cardMilitary, spiritBeast);
    }

    public bool InsertOrUpdateUserCardMonstersSpiritCard(string userId, CardMonsters cardMonsters, SpiritCards spiritBeast)
    {
        return _userSpiritCardRepository.InsertOrUpdateUserCardMonstersSpiritCard(userId, cardMonsters, spiritBeast);
    }

    public bool InsertOrUpdateUserCardSpellSpiritCard(string userId, CardSpells cardSpell, SpiritCards spiritBeast)
    {
        return _userSpiritCardRepository.InsertOrUpdateUserCardSpellSpiritCard(userId, cardSpell, spiritBeast);
    }

    public List<SpiritCards> GetAllUserCardHeroesSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritCardRepository.GetAllUserCardHeroesSpiritCard(user_id, pageSize, offset, status);
    }

    public List<SpiritCards> GetAllUserCardCaptainsSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritCardRepository.GetAllUserCardCaptainsSpiritCard(user_id, pageSize, offset, status);
    }

    public List<SpiritCards> GetAllUserCardColonelsSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritCardRepository.GetAllUserCardColonelsSpiritCard(user_id, pageSize, offset, status);
    }

    public List<SpiritCards> GetAllUserCardGeneralsSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritCardRepository.GetAllUserCardGeneralsSpiritCard(user_id, pageSize, offset, status);
    }

    public List<SpiritCards> GetAllUserCardAdmiralsSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritCardRepository.GetAllUserCardAdmiralsSpiritCard(user_id, pageSize, offset, status);
    }

    public List<SpiritCards> GetAllUserCardMilitarySpiritCard(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritCardRepository.GetAllUserCardMilitarySpiritCard(user_id, pageSize, offset, status);
    }

    public List<SpiritCards> GetAllUserCardMonstersSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritCardRepository.GetAllUserCardMonstersSpiritCard(user_id, pageSize, offset, status);
    }

    public List<SpiritCards> GetAllUserCardSpellSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritCardRepository.GetAllUserCardSpellSpiritCard(user_id, pageSize, offset, status);
    }

    public SpiritCards GetUserCardHeroesSpiritCard(string userId, CardHeroes cardHeroes)
    {
        return _userSpiritCardRepository.GetUserCardHeroesSpiritCard(userId, cardHeroes);
    }

    public SpiritCards GetUserCardCaptainsSpiritCard(string userId, CardCaptains cardCaptains)
    {
        return _userSpiritCardRepository.GetUserCardCaptainsSpiritCard(userId, cardCaptains);
    }

    public SpiritCards GetUserCardColonelsSpiritCard(string userId, CardColonels cardColonels)
    {
        return _userSpiritCardRepository.GetUserCardColonelsSpiritCard(userId, cardColonels);
    }

    public SpiritCards GetUserCardGeneralsSpiritCard(string userId, CardGenerals cardGenerals)
    {
        return _userSpiritCardRepository.GetUserCardGeneralsSpiritCard(userId, cardGenerals);
    }

    public SpiritCards GetUserCardAdmiralsSpiritCard(string userId, CardAdmirals cardAdmirals)
    {
        return _userSpiritCardRepository.GetUserCardAdmiralsSpiritCard(userId, cardAdmirals);
    }

    public SpiritCards GetUserCardMilitarySpiritCard(string userId, CardMilitaries cardMilitary)
    {
        return _userSpiritCardRepository.GetUserCardMilitarySpiritCard(userId, cardMilitary);
    }

    public SpiritCards GetUserCardMonstersSpiritCard(string userId, CardMonsters cardMonsters)
    {
        return _userSpiritCardRepository.GetUserCardMonstersSpiritCard(userId, cardMonsters);
    }

    public SpiritCards GetUserCardSpellSpiritCard(string userId, CardSpells cardSpell)
    {
        return _userSpiritCardRepository.GetUserCardSpellSpiritCard(userId, cardSpell);
    }

    public bool DeleteUserCardHeroesSpiritCard(string userId, CardHeroes cardHeroes, SpiritCards spiritBeast)
    {
        return _userSpiritCardRepository.DeleteUserCardHeroesSpiritCard(userId, cardHeroes, spiritBeast);
    }

    public bool DeleteUserCardCaptainsSpiritCard(string userId, CardCaptains cardCaptains, SpiritCards spiritBeast)
    {
        return _userSpiritCardRepository.DeleteUserCardCaptainsSpiritCard(userId, cardCaptains, spiritBeast);
    }

    public bool DeleteUserCardColonelsSpiritCard(string userId, CardColonels cardColonels, SpiritCards spiritBeast)
    {
        return _userSpiritCardRepository.DeleteUserCardColonelsSpiritCard(userId, cardColonels, spiritBeast);
    }

    public bool DeleteUserCardGeneralsSpiritCard(string userId, CardGenerals cardGenerals, SpiritCards spiritBeast)
    {
        return _userSpiritCardRepository.DeleteUserCardGeneralsSpiritCard(userId, cardGenerals, spiritBeast);
    }

    public bool DeleteUserCardAdmiralsSpiritCard(string userId, CardAdmirals cardAdmirals, SpiritCards spiritBeast)
    {
        return _userSpiritCardRepository.DeleteUserCardAdmiralsSpiritCard(userId, cardAdmirals, spiritBeast);
    }

    public bool DeleteUserCardMilitarySpiritCard(string userId, CardMilitaries cardMilitary, SpiritCards spiritBeast)
    {
        return _userSpiritCardRepository.DeleteUserCardMilitarySpiritCard(userId, cardMilitary, spiritBeast);
    }

    public bool DeleteUserCardMonstersSpiritCard(string userId, CardMonsters cardMonsters, SpiritCards spiritBeast)
    {
        return _userSpiritCardRepository.DeleteUserCardMonstersSpiritCard(userId, cardMonsters, spiritBeast);
    }
    
    public bool DeleteUserCardSpellSpiritCard(string userId, CardSpells cardSpell, SpiritCards spiritBeast)
    {
        return _userSpiritCardRepository.DeleteUserCardSpellSpiritCard(userId, cardSpell, spiritBeast);
    }
}
