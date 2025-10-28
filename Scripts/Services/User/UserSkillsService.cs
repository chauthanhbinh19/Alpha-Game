using System.Collections.Generic;

public class UserSkillsService : IUserSkillsService
{
    private readonly IUserSkillsRepository _userSkillsRepository;

    public UserSkillsService(IUserSkillsRepository userSkillsRepository)
    {
        _userSkillsRepository = userSkillsRepository;
    }

    public static UserSkillsService Create()
    {
        return new UserSkillsService(new UserSkillsRepository());
    }

    public Skills GetNewLevelPower(Skills c, double coefficient)
    {
        ISkillsRepository _repository = new SkillsRepository();
        SkillsService _service = new SkillsService(_repository);
        Skills orginCard = _service.GetSkillsById(c.Id);
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
    public Skills GetNewBreakthroughPower(Skills c, double coefficient)
    {
        ISkillsRepository _repository = new SkillsRepository();
        SkillsService _service = new SkillsService(_repository);
        Skills orginCard = _service.GetSkillsById(c.Id);
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

    public List<Skills> GetUserSkills(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<Skills> list = _userSkillsRepository.GetUserSkills(user_id, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserSkillsCount(string user_id, string type, string rare)
    {
        return _userSkillsRepository.GetUserSkillsCount(user_id, type, rare);
    }

    public bool InsertUserSkills(Skills skills)
    {
        return _userSkillsRepository.InsertUserSkills(skills);
    }

    public bool UpdateSkillsLevel(Skills skills, int cardLevel)
    {
        return _userSkillsRepository.UpdateSkillsLevel(skills, cardLevel);
    }

    public bool UpdateSkillsBreakthrough(Skills skills, int star, double quantity)
    {
        return _userSkillsRepository.UpdateSkillsBreakthrough(skills, star, quantity);
    }

    public Skills GetUserSkillsById(string user_id, string Id)
    {
        return _userSkillsRepository.GetUserSkillsById(user_id, Id);
    }

    public List<Skills> GetUserCardHeroesSkills(string user_id, string cardId)
    {
        return _userSkillsRepository.GetUserCardHeroesSkills(user_id, cardId);
    }

    public List<Skills> GetUserCardCaptainsSkills(string user_id, string cardId)
    {
        return _userSkillsRepository.GetUserCardCaptainsSkills(user_id, cardId);
    }

    public List<Skills> GetUserCardColonelsSkills(string user_id, string cardId)
    {
        return _userSkillsRepository.GetUserCardColonelsSkills(user_id, cardId);
    }

    public List<Skills> GetUserCardGeneralsSkills(string user_id, string cardId)
    {
        return _userSkillsRepository.GetUserCardGeneralsSkills(user_id, cardId);
    }

    public List<Skills> GetUserCardAdmiralsSkills(string user_id, string cardId)
    {
        return _userSkillsRepository.GetUserCardAdmiralsSkills(user_id, cardId);
    }

    public List<Skills> GetUserCardMilitarySkills(string user_id, string cardId)
    {
        return _userSkillsRepository.GetUserCardMilitarySkills(user_id, cardId);
    }

    public List<Skills> GetUserCardMonstersSkills(string user_id, string cardId)
    {
        return _userSkillsRepository.GetUserCardMonstersSkills(user_id, cardId);
    }

    public List<Skills> GetUserCardSpellSkills(string user_id, string cardId)
    {
        return _userSkillsRepository.GetUserCardSpellSkills(user_id, cardId);
    }

    public bool InsertUserCardHeroesSkills(string userId, string cardId, string skillId, int position)
    {
        return _userSkillsRepository.InsertUserCardHeroesSkills(userId, cardId, skillId, position);
    }

    public bool InsertUserCardCaptainsSkills(string userId, string cardId, string skillId, int position)
    {
        return _userSkillsRepository.InsertUserCardCaptainsSkills(userId, cardId, skillId, position);
    }

    public bool InsertUserCardColonelsSkills(string userId, string cardId, string skillId, int position)
    {
        return _userSkillsRepository.InsertUserCardColonelsSkills(userId, cardId, skillId, position);
    }

    public bool InsertUserCardGeneralsSkills(string userId, string cardId, string skillId, int position)
    {
        return _userSkillsRepository.InsertUserCardGeneralsSkills(userId, cardId, skillId, position);
    }

    public bool InsertUserCardAdmiralsSkills(string userId, string cardId, string skillId, int position)
    {
        return _userSkillsRepository.InsertUserCardAdmiralsSkills(userId, cardId, skillId, position);
    }

    public bool InsertUserCardMilitarySkills(string userId, string cardId, string skillId, int position)
    {
        return _userSkillsRepository.InsertUserCardMilitarySkills(userId, cardId, skillId, position);
    }

    public bool InsertUserCardMonstersSkills(string userId, string cardId, string skillId, int position)
    {
        return _userSkillsRepository.InsertUserCardMonstersSkills(userId, cardId, skillId, position);
    }

    public bool InsertUserCardSpellSkills(string userId, string cardId, string skillId, int position)
    {
        return _userSkillsRepository.InsertUserCardSpellSkills(userId, cardId, skillId, position);
    }

    public bool DeleteUserCardHeroesSkills(string userId, string cardId, string skillId, int position)
    {
        return _userSkillsRepository.DeleteUserCardHeroesSkills(userId, cardId, skillId, position);
    }

    public bool DeleteUserCardCaptainsSkills(string userId, string cardId, string skillId, int position)
    {
        return _userSkillsRepository.DeleteUserCardCaptainsSkills(userId, cardId, skillId, position);
    }

    public bool DeleteUserCardColonelsSkills(string userId, string cardId, string skillId, int position)
    {
        return _userSkillsRepository.DeleteUserCardColonelsSkills(userId, cardId, skillId, position);
    }

    public bool DeleteUserCardGeneralsSkills(string userId, string cardId, string skillId, int position)
    {
        return _userSkillsRepository.DeleteUserCardGeneralsSkills(userId, cardId, skillId, position);
    }

    public bool DeleteUserCardAdmiralsSkills(string userId, string cardId, string skillId, int position)
    {
        return _userSkillsRepository.DeleteUserCardAdmiralsSkills(userId, cardId, skillId, position);
    }

    public bool DeleteUserCardMonstersSkills(string userId, string cardId, string skillId, int position)
    {
        return _userSkillsRepository.DeleteUserCardMonstersSkills(userId, cardId, skillId, position);
    }

    public bool DeleteUserCardMilitarySkills(string userId, string cardId, string skillId, int position)
    {
        return _userSkillsRepository.DeleteUserCardMilitarySkills(userId, cardId, skillId, position);
    }

    public bool DeleteUserCardSpellSkills(string userId, string cardId, string skillId, int position)
    {
        return _userSkillsRepository.DeleteUserCardSpellSkills(userId, cardId, skillId, position);
    }
}
