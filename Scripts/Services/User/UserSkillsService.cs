using System.Collections.Generic;
using System.Threading.Tasks;

public class UserSkillsService : IUserSkillsService
{
     private static UserSkillsService _instance;
    private readonly IUserSkillsRepository _userSkillsRepository;

    public UserSkillsService(IUserSkillsRepository userSkillsRepository)
    {
        _userSkillsRepository = userSkillsRepository;
    }

    public static UserSkillsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserSkillsService(new UserSkillsRepository());
        }
        return _instance;
    }

    public async Task<Skills> GetNewLevelPowerAsync(Skills c, double coefficient)
    {
        ISkillsRepository _repository = new SkillsRepository();
        SkillsService _service = new SkillsService(_repository);
        Skills orginCard = await _service.GetSkillByIdAsync(c.Id);
        Skills skills = new Skills
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
        skills.Power = EvaluatePower.CalculatePower(
            skills.Health,
            skills.PhysicalAttack, skills.PhysicalDefense,
            skills.MagicalAttack, skills.MagicalDefense,
            skills.ChemicalAttack, skills.ChemicalDefense,
            skills.AtomicAttack, skills.AtomicDefense,
            skills.MentalAttack, skills.MentalDefense,
            skills.Speed,
            skills.CriticalDamageRate, skills.CriticalRate, skills.CriticalResistanceRate, skills.IgnoreCriticalRate,
            skills.PenetrationRate, skills.PenetrationResistanceRate, skills.EvasionRate,
            skills.DamageAbsorptionRate, skills.IgnoreDamageAbsorptionRate, skills.AbsorbedDamageRate,
            skills.VitalityRegenerationRate, skills.VitalityRegenerationResistanceRate,
            skills.AccuracyRate, skills.LifestealRate,
            skills.ShieldStrength, skills.Tenacity, skills.ResistanceRate,
            skills.ComboRate, skills.IgnoreComboRate, skills.ComboDamageRate, skills.ComboResistanceRate,
            skills.StunRate, skills.IgnoreStunRate,
            skills.ReflectionRate, skills.IgnoreReflectionRate, skills.ReflectionDamageRate, skills.ReflectionResistanceRate,
            skills.Mana, skills.ManaRegenerationRate,
            skills.DamageToDifferentFactionRate, skills.ResistanceToDifferentFactionRate,
            skills.DamageToSameFactionRate, skills.ResistanceToSameFactionRate,
            skills.NormalDamageRate, skills.NormalResistanceRate,
            skills.SkillDamageRate, skills.SkillResistanceRate
        );
        return skills;
    }
    public async Task<Skills> GetNewBreakthroughPowerAsync(Skills c, double coefficient)
    {
        ISkillsRepository _repository = new SkillsRepository();
        SkillsService _service = new SkillsService(_repository);
        Skills orginCard = await _service.GetSkillByIdAsync(c.Id);
        Skills skills = new Skills
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
        skills.Power = EvaluatePower.CalculatePower(
            skills.Health,
            skills.PhysicalAttack, skills.PhysicalDefense,
            skills.MagicalAttack, skills.MagicalDefense,
            skills.ChemicalAttack, skills.ChemicalDefense,
            skills.AtomicAttack, skills.AtomicDefense,
            skills.MentalAttack, skills.MentalDefense,
            skills.Speed,
            skills.CriticalDamageRate, skills.CriticalRate, skills.CriticalResistanceRate, skills.IgnoreCriticalRate,
            skills.PenetrationRate, skills.PenetrationResistanceRate, skills.EvasionRate,
            skills.DamageAbsorptionRate, skills.IgnoreDamageAbsorptionRate, skills.AbsorbedDamageRate,
            skills.VitalityRegenerationRate, skills.VitalityRegenerationResistanceRate,
            skills.AccuracyRate, skills.LifestealRate,
            skills.ShieldStrength, skills.Tenacity, skills.ResistanceRate,
            skills.ComboRate, skills.IgnoreComboRate, skills.ComboDamageRate, skills.ComboResistanceRate,
            skills.StunRate, skills.IgnoreStunRate,
            skills.ReflectionRate, skills.IgnoreReflectionRate, skills.ReflectionDamageRate, skills.ReflectionResistanceRate,
            skills.Mana, skills.ManaRegenerationRate,
            skills.DamageToDifferentFactionRate, skills.ResistanceToDifferentFactionRate,
            skills.DamageToSameFactionRate, skills.ResistanceToSameFactionRate,
            skills.NormalDamageRate, skills.NormalResistanceRate,
            skills.SkillDamageRate, skills.SkillResistanceRate
        );
        return skills;
    }

    public async Task<List<Skills>> GetUserSkillsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Skills> list = await _userSkillsRepository.GetUserSkillsAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserSkillsCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userSkillsRepository.GetUserSkillsCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserSkillsAsync(Skills skills)
    {
        return await _userSkillsRepository.InsertUserSkillAsync(skills);
    }

    public async Task<bool> UpdateSkillsLevelAsync(Skills skills, int cardLevel)
    {
        return await _userSkillsRepository.UpdateSkillLevelAsync(skills, cardLevel);
    }

    public async Task<bool> UpdateSkillsBreakthroughAsync(Skills skills, int star, double quantity)
    {
        return await _userSkillsRepository.UpdateSkillBreakthroughAsync(skills, star, quantity);
    }

    public async Task<Skills> GetUserSkillsByIdAsync(string user_id, string Id)
    {
        return await _userSkillsRepository.GetUserSkillsByIdAsync(user_id, Id);
    }

    public async Task<List<Skills>> GetUserCardHeroesSkillsAsync(string user_id, string cardId)
    {
        return await _userSkillsRepository.GetUserCardHeroesSkillsAsync(user_id, cardId);
    }

    public async Task<List<Skills>> GetUserCardCaptainsSkillsAsync(string user_id, string cardId)
    {
        return await _userSkillsRepository.GetUserCardCaptainsSkillsAsync(user_id, cardId);
    }

    public async Task<List<Skills>> GetUserCardColonelsSkillsAsync(string user_id, string cardId)
    {
        return await _userSkillsRepository.GetUserCardColonelsSkillsAsync(user_id, cardId);
    }

    public async Task<List<Skills>> GetUserCardGeneralsSkillsAsync(string user_id, string cardId)
    {
        return await _userSkillsRepository.GetUserCardGeneralsSkillsAsync(user_id, cardId);
    }

    public async Task<List<Skills>> GetUserCardAdmiralsSkillsAsync(string user_id, string cardId)
    {
        return await _userSkillsRepository.GetUserCardAdmiralsSkillsAsync(user_id, cardId);
    }

    public async Task<List<Skills>> GetUserCardMilitariesSkillsAsync(string user_id, string cardId)
    {
        return await _userSkillsRepository.GetUserCardMilitariesSkillsAsync(user_id, cardId);
    }

    public async Task<List<Skills>> GetUserCardMonstersSkillsAsync(string user_id, string cardId)
    {
        return await _userSkillsRepository.GetUserCardMonstersSkillsAsync(user_id, cardId);
    }

    public async Task<List<Skills>> GetUserCardSpellsSkillsAsync(string user_id, string cardId)
    {
        return await _userSkillsRepository.GetUserCardSpellsSkillsAsync(user_id, cardId);
    }

    public async Task<bool> InsertUserCardHeroSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.InsertUserCardHeroSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> InsertUserCardCaptainSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.InsertUserCardCaptainSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> InsertUserCardColonelSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.InsertUserCardColonelSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> InsertUserCardGeneralSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.InsertUserCardGeneralSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> InsertUserCardAdmiralSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.InsertUserCardAdmiralSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> InsertUserCardMilitarySkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.InsertUserCardMilitarySkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> InsertUserCardMonsterSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.InsertUserCardMonsterSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> InsertUserCardSpellSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.InsertUserCardSpellSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> DeleteUserCardHeroSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.DeleteUserCardHeroSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> DeleteUserCardCaptainSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.DeleteUserCardCaptainSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> DeleteUserCardColonelSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.DeleteUserCardColonelSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> DeleteUserCardGeneralSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.DeleteUserCardGeneralSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> DeleteUserCardAdmiralSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.DeleteUserCardAdmiralSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> DeleteUserCardMonsterSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.DeleteUserCardMonsterSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> DeleteUserCardMilitarySkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.DeleteUserCardMilitarySkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> DeleteUserCardSpellSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.DeleteUserCardSpellSkillsAsync(userId, cardId, skillId, position);
    }
}
