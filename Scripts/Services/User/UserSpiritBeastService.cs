using System.Collections.Generic;

public class UserSpiritBeastService : IUserSpiritBeastService
{
    private readonly IUserSpiritBeastRepository _userSpiritBeastRepository;

    public UserSpiritBeastService(IUserSpiritBeastRepository userSpiritBeastRepository)
    {
        _userSpiritBeastRepository = userSpiritBeastRepository;
    }

    public static UserSpiritBeastService Create()
    {
        return new UserSpiritBeastService(new UserSpiritBeastRepository());
    }

    public SpiritBeasts GetNewLevelPower(SpiritBeasts c, double coefficient)
    {
        ISpiritBeastRepository _repository = new SpiritBeastRepository();
        SpiritBeastService _service = new SpiritBeastService(_repository);
        SpiritBeasts orginCard = _service.GetSpiritBeastById(c.Id);
        SpiritBeasts SpiritBeast = new SpiritBeasts
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
        SpiritBeast.Power = EvaluatePower.CalculatePower(
            SpiritBeast.Health,
            SpiritBeast.PhysicalAttack, SpiritBeast.PhysicalDefense,
            SpiritBeast.MagicalAttack, SpiritBeast.MagicalDefense,
            SpiritBeast.ChemicalAttack, SpiritBeast.ChemicalDefense,
            SpiritBeast.AtomicAttack, SpiritBeast.AtomicDefense,
            SpiritBeast.MentalAttack, SpiritBeast.MentalDefense,
            SpiritBeast.Speed,
            SpiritBeast.CriticalDamageRate, SpiritBeast.CriticalRate, SpiritBeast.CriticalResistanceRate, SpiritBeast.IgnoreCriticalRate,
            SpiritBeast.PenetrationRate, SpiritBeast.PenetrationResistanceRate, SpiritBeast.EvasionRate,
            SpiritBeast.DamageAbsorptionRate, SpiritBeast.IgnoreDamageAbsorptionRate, SpiritBeast.AbsorbedDamageRate,
            SpiritBeast.VitalityRegenerationRate, SpiritBeast.VitalityRegenerationResistanceRate,
            SpiritBeast.AccuracyRate, SpiritBeast.LifestealRate,
            SpiritBeast.ShieldStrength, SpiritBeast.Tenacity, SpiritBeast.ResistanceRate,
            SpiritBeast.ComboRate, SpiritBeast.IgnoreComboRate, SpiritBeast.ComboDamageRate, SpiritBeast.ComboResistanceRate,
            SpiritBeast.StunRate, SpiritBeast.IgnoreStunRate,
            SpiritBeast.ReflectionRate, SpiritBeast.IgnoreReflectionRate, SpiritBeast.ReflectionDamageRate, SpiritBeast.ReflectionResistanceRate,
            SpiritBeast.Mana, SpiritBeast.ManaRegenerationRate,
            SpiritBeast.DamageToDifferentFactionRate, SpiritBeast.ResistanceToDifferentFactionRate,
            SpiritBeast.DamageToSameFactionRate, SpiritBeast.ResistanceToSameFactionRate,
            SpiritBeast.NormalDamageRate, SpiritBeast.NormalResistanceRate,
            SpiritBeast.SkillDamageRate, SpiritBeast.SkillResistanceRate
        );
        return SpiritBeast;
    }
    public SpiritBeasts GetNewBreakthroughPower(SpiritBeasts c, double coefficient)
    {
        ISpiritBeastRepository _repository = new SpiritBeastRepository();
        SpiritBeastService _service = new SpiritBeastService(_repository);
        SpiritBeasts orginCard = _service.GetSpiritBeastById(c.Id);
        SpiritBeasts SpiritBeast = new SpiritBeasts
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
        SpiritBeast.Power = EvaluatePower.CalculatePower(
            SpiritBeast.Health,
            SpiritBeast.PhysicalAttack, SpiritBeast.PhysicalDefense,
            SpiritBeast.MagicalAttack, SpiritBeast.MagicalDefense,
            SpiritBeast.ChemicalAttack, SpiritBeast.ChemicalDefense,
            SpiritBeast.AtomicAttack, SpiritBeast.AtomicDefense,
            SpiritBeast.MentalAttack, SpiritBeast.MentalDefense,
            SpiritBeast.Speed,
            SpiritBeast.CriticalDamageRate, SpiritBeast.CriticalRate, SpiritBeast.CriticalResistanceRate, SpiritBeast.IgnoreCriticalRate,
            SpiritBeast.PenetrationRate, SpiritBeast.PenetrationResistanceRate, SpiritBeast.EvasionRate,
            SpiritBeast.DamageAbsorptionRate, SpiritBeast.IgnoreDamageAbsorptionRate, SpiritBeast.AbsorbedDamageRate,
            SpiritBeast.VitalityRegenerationRate, SpiritBeast.VitalityRegenerationResistanceRate,
            SpiritBeast.AccuracyRate, SpiritBeast.LifestealRate,
            SpiritBeast.ShieldStrength, SpiritBeast.Tenacity, SpiritBeast.ResistanceRate,
            SpiritBeast.ComboRate, SpiritBeast.IgnoreComboRate, SpiritBeast.ComboDamageRate, SpiritBeast.ComboResistanceRate,
            SpiritBeast.StunRate, SpiritBeast.IgnoreStunRate,
            SpiritBeast.ReflectionRate, SpiritBeast.IgnoreReflectionRate, SpiritBeast.ReflectionDamageRate, SpiritBeast.ReflectionResistanceRate,
            SpiritBeast.Mana, SpiritBeast.ManaRegenerationRate,
            SpiritBeast.DamageToDifferentFactionRate, SpiritBeast.ResistanceToDifferentFactionRate,
            SpiritBeast.DamageToSameFactionRate, SpiritBeast.ResistanceToSameFactionRate,
            SpiritBeast.NormalDamageRate, SpiritBeast.NormalResistanceRate,
            SpiritBeast.SkillDamageRate, SpiritBeast.SkillResistanceRate
        );
        return SpiritBeast;
    }

    public List<SpiritBeasts> GetUserSpiritBeast(string user_id, int pageSize, int offset, string rare)
    {
        List<SpiritBeasts> list = _userSpiritBeastRepository.GetUserSpiritBeast(user_id, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<SpiritBeasts> GetAllUserSpiritBeast(string user_id, int pageSize, int offset)
    {
        List<SpiritBeasts> list = _userSpiritBeastRepository.GetAllUserSpiritBeast(user_id, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserSpiritBeastCount(string user_id, string rare)
    {
        return _userSpiritBeastRepository.GetUserSpiritBeastCount(user_id, rare);
    }

    public bool InsertUserSpiritBeast(SpiritBeasts SpiritBeast)
    {
        return _userSpiritBeastRepository.InsertUserSpiritBeast(SpiritBeast);
    }

    public bool UpdateSpiritBeastLevel(SpiritBeasts SpiritBeast, int cardLevel)
    {
        return _userSpiritBeastRepository.UpdateSpiritBeastLevel(SpiritBeast, cardLevel);
    }

    public bool UpdateSpiritBeastBreakthrough(SpiritBeasts SpiritBeast, int star, int quantity)
    {
        return _userSpiritBeastRepository.UpdateSpiritBeastBreakthrough(SpiritBeast, star, quantity);
    }

    public SpiritBeasts GetUserSpiritBeastById(string user_id, string Id)
    {
        return _userSpiritBeastRepository.GetUserSpiritBeastById(user_id, Id);
    }

    public SpiritBeasts SumPowerUserSpiritBeast()
    {
        return _userSpiritBeastRepository.SumPowerUserSpiritBeast();
    }

    public bool InsertOrUpdateUserCardHeroesSpiritBeast(string userId, CardHeroes cardHeroes, SpiritBeasts spiritBeast)
    {
        return _userSpiritBeastRepository.InsertOrUpdateUserCardHeroesSpiritBeast(userId, cardHeroes, spiritBeast);
    }

    public bool InsertOrUpdateUserCardCaptainsSpiritBeast(string userId, CardCaptains cardCaptains, SpiritBeasts spiritBeast)
    {
        return _userSpiritBeastRepository.InsertOrUpdateUserCardCaptainsSpiritBeast(userId, cardCaptains, spiritBeast);
    }

    public bool InsertOrUpdateUserCardColonelsSpiritBeast(string userId, CardColonels cardColonels, SpiritBeasts spiritBeast)
    {
        return _userSpiritBeastRepository.InsertOrUpdateUserCardColonelsSpiritBeast(userId, cardColonels, spiritBeast);
    }

    public bool InsertOrUpdateUserCardGeneralsSpiritBeast(string userId, CardGenerals cardGenerals, SpiritBeasts spiritBeast)
    {
        return _userSpiritBeastRepository.InsertOrUpdateUserCardGeneralsSpiritBeast(userId, cardGenerals, spiritBeast);
    }

    public bool InsertOrUpdateUserCardAdmiralsSpiritBeast(string userId, CardAdmirals cardAdmirals, SpiritBeasts spiritBeast)
    {
        return _userSpiritBeastRepository.InsertOrUpdateUserCardAdmiralsSpiritBeast(userId, cardAdmirals, spiritBeast);
    }

    public bool InsertOrUpdateUserCardMilitarySpiritBeast(string userId, CardMilitaries cardMilitary, SpiritBeasts spiritBeast)
    {
        return _userSpiritBeastRepository.InsertOrUpdateUserCardMilitarySpiritBeast(userId, cardMilitary, spiritBeast);
    }

    public bool InsertOrUpdateUserCardMonstersSpiritBeast(string userId, CardMonsters cardMonsters, SpiritBeasts spiritBeast)
    {
        return _userSpiritBeastRepository.InsertOrUpdateUserCardMonstersSpiritBeast(userId, cardMonsters, spiritBeast);
    }

    public bool InsertOrUpdateUserCardSpellSpiritBeast(string userId, CardSpells cardSpell, SpiritBeasts spiritBeast)
    {
        return _userSpiritBeastRepository.InsertOrUpdateUserCardSpellSpiritBeast(userId, cardSpell, spiritBeast);
    }

    public List<SpiritBeasts> GetAllUserCardHeroesSpiritBeast(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritBeastRepository.GetAllUserCardHeroesSpiritBeast(user_id, pageSize, offset, status);
    }

    public List<SpiritBeasts> GetAllUserCardCaptainsSpiritBeast(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritBeastRepository.GetAllUserCardCaptainsSpiritBeast(user_id, pageSize, offset, status);
    }

    public List<SpiritBeasts> GetAllUserCardColonelsSpiritBeast(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritBeastRepository.GetAllUserCardColonelsSpiritBeast(user_id, pageSize, offset, status);
    }

    public List<SpiritBeasts> GetAllUserCardGeneralsSpiritBeast(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritBeastRepository.GetAllUserCardGeneralsSpiritBeast(user_id, pageSize, offset, status);
    }

    public List<SpiritBeasts> GetAllUserCardAdmiralsSpiritBeast(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritBeastRepository.GetAllUserCardAdmiralsSpiritBeast(user_id, pageSize, offset, status);
    }

    public List<SpiritBeasts> GetAllUserCardMilitarySpiritBeast(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritBeastRepository.GetAllUserCardMilitarySpiritBeast(user_id, pageSize, offset, status);
    }

    public List<SpiritBeasts> GetAllUserCardMonstersSpiritBeast(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritBeastRepository.GetAllUserCardMonstersSpiritBeast(user_id, pageSize, offset, status);
    }

    public List<SpiritBeasts> GetAllUserCardSpellSpiritBeast(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritBeastRepository.GetAllUserCardSpellSpiritBeast(user_id, pageSize, offset, status);
    }

    public SpiritBeasts GetUserCardHeroesSpiritBeast(string userId, CardHeroes cardHeroes)
    {
        return _userSpiritBeastRepository.GetUserCardHeroesSpiritBeast(userId, cardHeroes);
    }

    public SpiritBeasts GetUserCardCaptainsSpiritBeast(string userId, CardCaptains cardCaptains)
    {
        return _userSpiritBeastRepository.GetUserCardCaptainsSpiritBeast(userId, cardCaptains);
    }

    public SpiritBeasts GetUserCardColonelsSpiritBeast(string userId, CardColonels cardColonels)
    {
        return _userSpiritBeastRepository.GetUserCardColonelsSpiritBeast(userId, cardColonels);
    }

    public SpiritBeasts GetUserCardGeneralsSpiritBeast(string userId, CardGenerals cardGenerals)
    {
        return _userSpiritBeastRepository.GetUserCardGeneralsSpiritBeast(userId, cardGenerals);
    }

    public SpiritBeasts GetUserCardAdmiralsSpiritBeast(string userId, CardAdmirals cardAdmirals)
    {
        return _userSpiritBeastRepository.GetUserCardAdmiralsSpiritBeast(userId, cardAdmirals);
    }

    public SpiritBeasts GetUserCardMilitarySpiritBeast(string userId, CardMilitaries cardMilitary)
    {
        return _userSpiritBeastRepository.GetUserCardMilitarySpiritBeast(userId, cardMilitary);
    }

    public SpiritBeasts GetUserCardMonstersSpiritBeast(string userId, CardMonsters cardMonsters)
    {
        return _userSpiritBeastRepository.GetUserCardMonstersSpiritBeast(userId, cardMonsters);
    }

    public SpiritBeasts GetUserCardSpellSpiritBeast(string userId, CardSpells cardSpell)
    {
        return _userSpiritBeastRepository.GetUserCardSpellSpiritBeast(userId, cardSpell);
    }

    public bool DeleteUserCardHeroesSpiritBeast(string userId, CardHeroes cardHeroes, SpiritBeasts spiritBeast)
    {
        return _userSpiritBeastRepository.DeleteUserCardHeroesSpiritBeast(userId, cardHeroes, spiritBeast);
    }

    public bool DeleteUserCardCaptainsSpiritBeast(string userId, CardCaptains cardCaptains, SpiritBeasts spiritBeast)
    {
        return _userSpiritBeastRepository.DeleteUserCardCaptainsSpiritBeast(userId, cardCaptains, spiritBeast);
    }

    public bool DeleteUserCardColonelsSpiritBeast(string userId, CardColonels cardColonels, SpiritBeasts spiritBeast)
    {
        return _userSpiritBeastRepository.DeleteUserCardColonelsSpiritBeast(userId, cardColonels, spiritBeast);
    }

    public bool DeleteUserCardGeneralsSpiritBeast(string userId, CardGenerals cardGenerals, SpiritBeasts spiritBeast)
    {
        return _userSpiritBeastRepository.DeleteUserCardGeneralsSpiritBeast(userId, cardGenerals, spiritBeast);
    }

    public bool DeleteUserCardAdmiralsSpiritBeast(string userId, CardAdmirals cardAdmirals, SpiritBeasts spiritBeast)
    {
        return _userSpiritBeastRepository.DeleteUserCardAdmiralsSpiritBeast(userId, cardAdmirals, spiritBeast);
    }

    public bool DeleteUserCardMilitarySpiritBeast(string userId, CardMilitaries cardMilitary, SpiritBeasts spiritBeast)
    {
        return _userSpiritBeastRepository.DeleteUserCardMilitarySpiritBeast(userId, cardMilitary, spiritBeast);
    }

    public bool DeleteUserCardMonstersSpiritBeast(string userId, CardMonsters cardMonsters, SpiritBeasts spiritBeast)
    {
        return _userSpiritBeastRepository.DeleteUserCardMonstersSpiritBeast(userId, cardMonsters, spiritBeast);
    }
    
    public bool DeleteUserCardSpellSpiritBeast(string userId, CardSpells cardSpell, SpiritBeasts spiritBeast)
    {
        return _userSpiritBeastRepository.DeleteUserCardSpellSpiritBeast(userId, cardSpell, spiritBeast);
    }
}
