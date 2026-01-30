using System.Collections.Generic;
using System.Threading.Tasks;

public class UserSpiritCardsService : IUserSpiritCardsService
{
    private readonly IUserSpiritCardsRepository _userSpiritCardRepository;

    public UserSpiritCardsService(IUserSpiritCardsRepository userSpiritCardRepository)
    {
        _userSpiritCardRepository = userSpiritCardRepository;
    }

    public static UserSpiritCardsService Create()
    {
        return new UserSpiritCardsService(new UserSpiritCardsRepository());
    }

    public async Task<SpiritCards> GetNewLevelPowerAsync(SpiritCards c, double coefficient)
    {
        ISpiritCardsRepository _repository = new SpiritCardsRepository();
        SpiritCardsService _service = new SpiritCardsService(_repository);
        SpiritCards orginCard = await _service.GetSpiritCardByIdAsync(c.Id);
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
    public async Task<SpiritCards> GetNewBreakthroughPowerAsync(SpiritCards c, double coefficient)
    {
        ISpiritCardsRepository _repository = new SpiritCardsRepository();
        SpiritCardsService _service = new SpiritCardsService(_repository);
        SpiritCards orginCard = await _service.GetSpiritCardByIdAsync(c.Id);
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

    public async Task<List<SpiritCards>> GetUserSpiritCardAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<SpiritCards> list = await _userSpiritCardRepository.GetUserSpiritCardsAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<SpiritCards>> GetAllUserSpiritCardAsync(string user_id, int pageSize, int offset)
    {
        List<SpiritCards> list = await _userSpiritCardRepository.GetAllUserSpiritCardsAsync(user_id, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetUserSpiritCardCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userSpiritCardRepository.GetUserSpiritCardsCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserSpiritCardAsync(SpiritCards SpiritCard)
    {
        return await _userSpiritCardRepository.InsertUserSpiritCardAsync(SpiritCard);
    }

    public async Task<bool> UpdateSpiritCardLevelAsync(SpiritCards SpiritCard, int cardLevel)
    {
        return await _userSpiritCardRepository.UpdateSpiritCardLevelAsync(SpiritCard, cardLevel);
    }

    public async Task<bool> UpdateSpiritCardBreakthroughAsync(SpiritCards SpiritCard, int star, double quantity)
    {
        return await _userSpiritCardRepository.UpdateSpiritCardBreakthroughAsync(SpiritCard, star, quantity);
    }

    public async Task<SpiritCards> GetUserSpiritCardByIdAsync(string user_id, string Id)
    {
        return await _userSpiritCardRepository.GetUserSpiritCardByIdAsync(user_id, Id);
    }

    public async Task<SpiritCards> SumPowerUserSpiritCardsAsync()
    {
        return await _userSpiritCardRepository.SumPowerUserSpiritCardsAsync();
    }

    public async Task<bool> InsertOrUpdateUserCardHeroSpiritCardAsync(string userId, CardHeroes cardHeroes, SpiritCards spiritBeast)
    {
        return await _userSpiritCardRepository.InsertOrUpdateUserCardHeroSpiritCardAsync(userId, cardHeroes, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardCaptainSpiritCardAsync(string userId, CardCaptains cardCaptains, SpiritCards spiritBeast)
    {
        return await _userSpiritCardRepository.InsertOrUpdateUserCardCaptainSpiritCardAsync(userId, cardCaptains, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardColonelSpiritCardAsync(string userId, CardColonels cardColonels, SpiritCards spiritBeast)
    {
        return await _userSpiritCardRepository.InsertOrUpdateUserCardColonelSpiritCardAsync(userId, cardColonels, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardGeneralSpiritCardAsync(string userId, CardGenerals cardGenerals, SpiritCards spiritBeast)
    {
        return await _userSpiritCardRepository.InsertOrUpdateUserCardGeneralSpiritCardAsync(userId, cardGenerals, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardAdmiralSpiritCardAsync(string userId, CardAdmirals cardAdmirals, SpiritCards spiritBeast)
    {
        return await _userSpiritCardRepository.InsertOrUpdateUserCardAdmiralSpiritCardAsync(userId, cardAdmirals, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardMilitarySpiritCardAsync(string userId, CardMilitaries cardMilitary, SpiritCards spiritBeast)
    {
        return await _userSpiritCardRepository.InsertOrUpdateUserCardMilitarySpiritCardAsync(userId, cardMilitary, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardMonsterSpiritCardAsync(string userId, CardMonsters cardMonsters, SpiritCards spiritBeast)
    {
        return await _userSpiritCardRepository.InsertOrUpdateUserCardMonsterSpiritCardAsync(userId, cardMonsters, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardSpellSpiritCardAsync(string userId, CardSpells cardSpell, SpiritCards spiritBeast)
    {
        return await _userSpiritCardRepository.InsertOrUpdateUserCardSpellSpiritCardAsync(userId, cardSpell, spiritBeast);
    }

    public async Task<List<SpiritCards>> GetAllUserCardHeroesSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritCardRepository.GetAllUserCardHeroesSpiritCardAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritCards>> GetAllUserCardCaptainsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritCardRepository.GetAllUserCardCaptainsSpiritCardAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritCards>> GetAllUserCardColonelsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritCardRepository.GetAllUserCardColonelsSpiritCardAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritCards>> GetAllUserCardGeneralsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritCardRepository.GetAllUserCardGeneralsSpiritCardAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritCards>> GetAllUserCardAdmiralsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritCardRepository.GetAllUserCardAdmiralsSpiritCardAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritCards>> GetAllUserCardMilitariesSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritCardRepository.GetAllUserCardMilitariesSpiritCardAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritCards>> GetAllUserCardMonstersSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritCardRepository.GetAllUserCardMonstersSpiritCardAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritCards>> GetAllUserCardSpellsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritCardRepository.GetAllUserCardSpellsSpiritCardAsync(user_id, pageSize, offset, status);
    }

    public async Task<SpiritCards> GetUserCardHeroSpiritCardAsync(string userId, CardHeroes cardHeroes)
    {
        return await _userSpiritCardRepository.GetUserCardHeroSpiritCardAsync(userId, cardHeroes);
    }

    public async Task<SpiritCards> GetUserCardCaptainSpiritCardAsync(string userId, CardCaptains cardCaptains)
    {
        return await _userSpiritCardRepository.GetUserCardCaptainSpiritCardAsync(userId, cardCaptains);
    }

    public async Task<SpiritCards> GetUserCardColonelSpiritCardAsync(string userId, CardColonels cardColonels)
    {
        return await _userSpiritCardRepository.GetUserCardColonelSpiritCardAsync(userId, cardColonels);
    }

    public async Task<SpiritCards> GetUserCardGeneralSpiritCardAsync(string userId, CardGenerals cardGenerals)
    {
        return await _userSpiritCardRepository.GetUserCardGeneralSpiritCardAsync(userId, cardGenerals);
    }

    public async Task<SpiritCards> GetUserCardAdmiralSpiritCardAsync(string userId, CardAdmirals cardAdmirals)
    {
        return await _userSpiritCardRepository.GetUserCardAdmiralSpiritCardAsync(userId, cardAdmirals);
    }

    public async Task<SpiritCards> GetUserCardMilitarySpiritCardAsync(string userId, CardMilitaries cardMilitary)
    {
        return await _userSpiritCardRepository.GetUserCardMilitarySpiritCardAsync(userId, cardMilitary);
    }

    public async Task<SpiritCards> GetUserCardMonsterSpiritCardAsync(string userId, CardMonsters cardMonsters)
    {
        return await _userSpiritCardRepository.GetUserCardMonsterSpiritCardAsync(userId, cardMonsters);
    }

    public async Task<SpiritCards> GetUserCardSpellSpiritCardAsync(string userId, CardSpells cardSpell)
    {
        return await _userSpiritCardRepository.GetUserCardSpellSpiritCardAsync(userId, cardSpell);
    }

    public async Task<bool> DeleteUserCardHeroSpiritCardAsync(string userId, CardHeroes cardHeroes, SpiritCards spiritBeast)
    {
        return await _userSpiritCardRepository.DeleteUserCardHeroSpiritCardAsync(userId, cardHeroes, spiritBeast);
    }

    public async Task<bool> DeleteUserCardCaptainSpiritCardAsync(string userId, CardCaptains cardCaptains, SpiritCards spiritBeast)
    {
        return await _userSpiritCardRepository.DeleteUserCardCaptainSpiritCardAsync(userId, cardCaptains, spiritBeast);
    }

    public async Task<bool> DeleteUserCardColonelSpiritCardAsync(string userId, CardColonels cardColonels, SpiritCards spiritBeast)
    {
        return await _userSpiritCardRepository.DeleteUserCardColonelSpiritCardAsync(userId, cardColonels, spiritBeast);
    }

    public async Task<bool> DeleteUserCardGeneralSpiritCardAsync(string userId, CardGenerals cardGenerals, SpiritCards spiritBeast)
    {
        return await _userSpiritCardRepository.DeleteUserCardGeneralSpiritCardAsync(userId, cardGenerals, spiritBeast);
    }

    public async Task<bool> DeleteUserCardAdmiralSpiritCardAsync(string userId, CardAdmirals cardAdmirals, SpiritCards spiritBeast)
    {
        return await _userSpiritCardRepository.DeleteUserCardAdmiralSpiritCardAsync(userId, cardAdmirals, spiritBeast);
    }

    public async Task<bool> DeleteUserCardMilitarySpiritCardAsync(string userId, CardMilitaries cardMilitary, SpiritCards spiritBeast)
    {
        return await _userSpiritCardRepository.DeleteUserCardMilitarySpiritCardAsync(userId, cardMilitary, spiritBeast);
    }

    public async Task<bool> DeleteUserCardMonsterSpiritCardAsync(string userId, CardMonsters cardMonsters, SpiritCards spiritBeast)
    {
        return await _userSpiritCardRepository.DeleteUserCardMonsterSpiritCardAsync(userId, cardMonsters, spiritBeast);
    }
    
    public async Task<bool> DeleteUserCardSpellSpiritCardAsync(string userId, CardSpells cardSpell, SpiritCards spiritBeast)
    {
        return await _userSpiritCardRepository.DeleteUserCardSpellSpiritCardAsync(userId, cardSpell, spiritBeast);
    }
}
