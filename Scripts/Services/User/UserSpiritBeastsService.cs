using System.Collections.Generic;
using System.Threading.Tasks;

public class UserSpiritBeastsService : IUserSpiritBeastsService
{
     private static UserSpiritBeastsService _instance;
    private readonly IUserSpiritBeastsRepository _userSpiritBeastsRepository;

    public UserSpiritBeastsService(IUserSpiritBeastsRepository userSpiritBeastsRepository)
    {
        _userSpiritBeastsRepository = userSpiritBeastsRepository;
    }

    public static UserSpiritBeastsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserSpiritBeastsService(new UserSpiritBeastsRepository());
        }
        return _instance;
    }

    public async Task<SpiritBeasts> GetNewLevelPowerAsync(SpiritBeasts c, double coefficient)
    {
        ISpiritBeastsRepository _repository = new SpiritBeastsRepository();
        SpiritBeastsService _service = new SpiritBeastsService(_repository);
        SpiritBeasts orginCard = await _service.GetSpiritBeastByIdAsync(c.Id);
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
    public async Task<SpiritBeasts> GetNewBreakthroughPowerAsync(SpiritBeasts c, double coefficient)
    {
        ISpiritBeastsRepository _repository = new SpiritBeastsRepository();
        SpiritBeastsService _service = new SpiritBeastsService(_repository);
        SpiritBeasts orginCard = await _service.GetSpiritBeastByIdAsync(c.Id);
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

    public async Task<List<SpiritBeasts>> GetUserSpiritBeastsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<SpiritBeasts> list = await _userSpiritBeastsRepository.GetUserSpiritBeastsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<SpiritBeasts>> GetAllUserSpiritBeastsAsync(string user_id, int pageSize, int offset)
    {
        List<SpiritBeasts> list = await _userSpiritBeastsRepository.GetAllUserSpiritBeastsAsync(user_id, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserSpiritBeastsCountAsync(string user_id, string search, string rare)
    {
        return await _userSpiritBeastsRepository.GetUserSpiritBeastsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserSpiritBeastAsync(SpiritBeasts SpiritBeast)
    {
        return await _userSpiritBeastsRepository.InsertUserSpiritBeastAsync(SpiritBeast);
    }

    public async Task<bool> UpdateSpiritBeastLevelAsync(SpiritBeasts SpiritBeast, int cardLevel)
    {
        return await _userSpiritBeastsRepository.UpdateSpiritBeastLevelAsync(SpiritBeast, cardLevel);
    }

    public async Task<bool> UpdateSpiritBeastBreakthroughAsync(SpiritBeasts SpiritBeast, int star, double quantity)
    {
        return await _userSpiritBeastsRepository.UpdateSpiritBeastBreakthroughAsync(SpiritBeast, star, quantity);
    }

    public async Task<SpiritBeasts> GetUserSpiritBeastByIdAsync(string user_id, string Id)
    {
        return await _userSpiritBeastsRepository.GetUserSpiritBeastByIdAsync(user_id, Id);
    }

    public async Task<SpiritBeasts> SumPowerUserSpiritBeastsAsync()
    {
        return await _userSpiritBeastsRepository.SumPowerUserSpiritBeastsAsync();
    }

    public async Task<bool> InsertOrUpdateUserCardHeroSpiritBeastAsync(string userId, CardHeroes cardHeroes, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.InsertOrUpdateUserCardHeroSpiritBeastAsync(userId, cardHeroes, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardCaptainSpiritBeastAsync(string userId, CardCaptains cardCaptains, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.InsertOrUpdateUserCardCaptainSpiritBeastAsync(userId, cardCaptains, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardColonelSpiritBeastAsync(string userId, CardColonels cardColonels, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.InsertOrUpdateUserCardColonelSpiritBeastAsync(userId, cardColonels, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardGeneralSpiritBeastAsync(string userId, CardGenerals cardGenerals, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.InsertOrUpdateUserCardGeneralSpiritBeastAsync(userId, cardGenerals, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardAdmiralSpiritBeastAsync(string userId, CardAdmirals cardAdmirals, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.InsertOrUpdateUserCardAdmiralSpiritBeastAsync(userId, cardAdmirals, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardMilitarySpiritBeastAsync(string userId, CardMilitaries cardMilitary, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.InsertOrUpdateUserCardMilitarySpiritBeastAsync(userId, cardMilitary, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardMonsterSpiritBeastAsync(string userId, CardMonsters cardMonsters, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.InsertOrUpdateUserCardMonsterSpiritBeastAsync(userId, cardMonsters, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardSpellSpiritBeastAsync(string userId, CardSpells cardSpell, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.InsertOrUpdateUserCardSpellSpiritBeastAsync(userId, cardSpell, spiritBeast);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardHeroesSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardHeroesSpiritBeastAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardCaptainsSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardCaptainsSpiritBeastAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardColonelsSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardColonelsSpiritBeastAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardGeneralsSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardGeneralsSpiritBeastAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardAdmiralsSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardAdmiralsSpiritBeastAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardMilitariesSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardMilitariesSpiritBeastAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardMonstersSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardMonstersSpiritBeastAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardSpellsSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardSpellsSpiritBeastAsync(user_id, pageSize, offset, status);
    }

    public async Task<SpiritBeasts> GetUserCardHeroSpiritBeastAsync(string userId, CardHeroes cardHeroes)
    {
        return await _userSpiritBeastsRepository.GetUserCardHeroSpiritBeastAsync(userId, cardHeroes);
    }

    public async Task<SpiritBeasts> GetUserCardCaptainSpiritBeastAsync(string userId, CardCaptains cardCaptains)
    {
        return await _userSpiritBeastsRepository.GetUserCardCaptainSpiritBeastAsync(userId, cardCaptains);
    }

    public async Task<SpiritBeasts> GetUserCardColonelSpiritBeastAsync(string userId, CardColonels cardColonels)
    {
        return await _userSpiritBeastsRepository.GetUserCardColonelSpiritBeastAsync(userId, cardColonels);
    }

    public async Task<SpiritBeasts> GetUserCardGeneralSpiritBeastAsync(string userId, CardGenerals cardGenerals)
    {
        return await _userSpiritBeastsRepository.GetUserCardGeneralSpiritBeastAsync(userId, cardGenerals);
    }

    public async Task<SpiritBeasts> GetUserCardAdmiralSpiritBeastAsync(string userId, CardAdmirals cardAdmirals)
    {
        return await _userSpiritBeastsRepository.GetUserCardAdmiralSpiritBeastAsync(userId, cardAdmirals);
    }

    public async Task<SpiritBeasts> GetUserCardMilitarySpiritBeastAsync(string userId, CardMilitaries cardMilitary)
    {
        return await _userSpiritBeastsRepository.GetUserCardMilitarySpiritBeastAsync(userId, cardMilitary);
    }

    public async Task<SpiritBeasts> GetUserCardMonsterSpiritBeastAsync(string userId, CardMonsters cardMonsters)
    {
        return await _userSpiritBeastsRepository.GetUserCardMonsterSpiritBeastAsync(userId, cardMonsters);
    }

    public async Task<SpiritBeasts> GetUserCardSpellSpiritBeastAsync(string userId, CardSpells cardSpell)
    {
        return await _userSpiritBeastsRepository.GetUserCardSpellSpiritBeastAsync(userId, cardSpell);
    }

    public async Task<bool> DeleteUserCardHeroSpiritBeastAsync(string userId, CardHeroes cardHeroes, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.DeleteUserCardHeroSpiritBeastAsync(userId, cardHeroes, spiritBeast);
    }

    public async Task<bool> DeleteUserCardCaptainSpiritBeastAsync(string userId, CardCaptains cardCaptains, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.DeleteUserCardCaptainSpiritBeastAsync(userId, cardCaptains, spiritBeast);
    }

    public async Task<bool> DeleteUserCardColonelSpiritBeastAsync(string userId, CardColonels cardColonels, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.DeleteUserCardColonelSpiritBeastAsync(userId, cardColonels, spiritBeast);
    }

    public async Task<bool> DeleteUserCardGeneralSpiritBeastAsync(string userId, CardGenerals cardGenerals, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.DeleteUserCardGeneralSpiritBeastAsync(userId, cardGenerals, spiritBeast);
    }

    public async Task<bool> DeleteUserCardAdmiralSpiritBeastAsync(string userId, CardAdmirals cardAdmirals, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.DeleteUserCardAdmiralSpiritBeastAsync(userId, cardAdmirals, spiritBeast);
    }

    public async Task<bool> DeleteUserCardMilitarySpiritBeastAsync(string userId, CardMilitaries cardMilitary, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.DeleteUserCardMilitarySpiritBeastAsync(userId, cardMilitary, spiritBeast);
    }

    public async Task<bool> DeleteUserCardMonsterSpiritBeastAsync(string userId, CardMonsters cardMonsters, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.DeleteUserCardMonsterSpiritBeastAsync(userId, cardMonsters, spiritBeast);
    }
    
    public async Task<bool> DeleteUserCardSpellSpiritBeastAsync(string userId, CardSpells cardSpell, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.DeleteUserCardSpellSpiritBeastAsync(userId, cardSpell, spiritBeast);
    }
}
