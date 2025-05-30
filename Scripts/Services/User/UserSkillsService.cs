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
        Skills orginCard = _service.GetSkillsById(c.id);
        Skills skills = new Skills
        {
            id = c.id,
            health = c.health + orginCard.health * coefficient,
            physical_attack = c.physical_attack + orginCard.physical_attack * coefficient,
            physical_defense = c.physical_defense + orginCard.physical_defense * coefficient,
            magical_attack = c.magical_attack + orginCard.magical_attack * coefficient,
            magical_defense = c.magical_defense + orginCard.magical_defense * coefficient,
            chemical_attack = c.chemical_attack + orginCard.chemical_attack * coefficient,
            chemical_defense = c.chemical_defense + orginCard.chemical_defense * coefficient,
            atomic_attack = c.atomic_attack + orginCard.atomic_attack * coefficient,
            atomic_defense = c.atomic_defense + orginCard.atomic_defense * coefficient,
            mental_attack = c.mental_attack + orginCard.mental_attack * coefficient,
            mental_defense = c.mental_defense + orginCard.mental_defense * coefficient,
            speed = c.speed + orginCard.speed * coefficient,
            critical_damage_rate = c.critical_damage_rate + orginCard.critical_damage_rate * coefficient,
            critical_rate = c.critical_rate + orginCard.critical_rate * coefficient,
            penetration_rate = c.penetration_rate + orginCard.penetration_rate * coefficient,
            evasion_rate = c.evasion_rate + orginCard.evasion_rate * coefficient,
            damage_absorption_rate = c.damage_absorption_rate + orginCard.damage_absorption_rate * coefficient,
            vitality_regeneration_rate = c.vitality_regeneration_rate + orginCard.vitality_regeneration_rate * coefficient,
            accuracy_rate = c.accuracy_rate + orginCard.accuracy_rate * coefficient,
            lifesteal_rate = c.lifesteal_rate + orginCard.lifesteal_rate * coefficient,
            shield_strength = c.shield_strength + orginCard.shield_strength * coefficient,
            tenacity = c.tenacity + orginCard.tenacity * coefficient,
            resistance_rate = c.resistance_rate + orginCard.resistance_rate * coefficient,
            combo_rate = c.combo_rate + orginCard.combo_rate * coefficient,
            reflection_rate = c.reflection_rate + orginCard.reflection_rate * coefficient,
            mana = c.mana + orginCard.mana * (float)coefficient,
            mana_regeneration_rate = c.mana_regeneration_rate + orginCard.mana_regeneration_rate * coefficient,
            damage_to_different_faction_rate = c.damage_to_different_faction_rate + orginCard.damage_to_different_faction_rate * coefficient,
            resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + orginCard.resistance_to_different_faction_rate * coefficient,
            damage_to_same_faction_rate = c.damage_to_same_faction_rate + orginCard.damage_to_same_faction_rate * coefficient,
            resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + orginCard.resistance_to_same_faction_rate * coefficient
        };
        skills.power = EvaluatePower.CalculatePower(
            skills.health,
            skills.physical_attack, skills.physical_defense,
            skills.magical_attack, skills.magical_defense,
            skills.chemical_attack, skills.chemical_defense,
            skills.atomic_attack, skills.atomic_defense,
            skills.mental_attack, skills.mental_defense,
            skills.speed,
            skills.critical_damage_rate, skills.critical_rate,
            skills.penetration_rate, skills.evasion_rate,
            skills.damage_absorption_rate, skills.vitality_regeneration_rate,
            skills.accuracy_rate, skills.lifesteal_rate,
            skills.shield_strength, skills.tenacity, skills.resistance_rate,
            skills.combo_rate, skills.reflection_rate,
            skills.mana, skills.mana_regeneration_rate,
            skills.damage_to_different_faction_rate, skills.resistance_to_different_faction_rate,
            skills.damage_to_same_faction_rate, skills.resistance_to_same_faction_rate
        );
        return skills;
    }
    public Skills GetNewBreakthroughPower(Skills c, double coefficient)
    {
        ISkillsRepository _repository = new SkillsRepository();
        SkillsService _service = new SkillsService(_repository);
        Skills orginCard = _service.GetSkillsById(c.id);
        Skills skills = new Skills
        {
            id = c.id,
            health = c.health + orginCard.health * coefficient,
            physical_attack = c.physical_attack + orginCard.physical_attack * coefficient,
            physical_defense = c.physical_defense + orginCard.physical_defense * coefficient,
            magical_attack = c.magical_attack + orginCard.magical_attack * coefficient,
            magical_defense = c.magical_defense + orginCard.magical_defense * coefficient,
            chemical_attack = c.chemical_attack + orginCard.chemical_attack * coefficient,
            chemical_defense = c.chemical_defense + orginCard.chemical_defense * coefficient,
            atomic_attack = c.atomic_attack + orginCard.atomic_attack * coefficient,
            atomic_defense = c.atomic_defense + orginCard.atomic_defense * coefficient,
            mental_attack = c.mental_attack + orginCard.mental_attack * coefficient,
            mental_defense = c.mental_defense + orginCard.mental_defense * coefficient,
            speed = c.speed + orginCard.speed * coefficient,
            critical_damage_rate = c.critical_damage_rate + orginCard.critical_damage_rate * coefficient,
            critical_rate = c.critical_rate + orginCard.critical_rate * coefficient,
            penetration_rate = c.penetration_rate + orginCard.penetration_rate * coefficient,
            evasion_rate = c.evasion_rate + orginCard.evasion_rate * coefficient,
            damage_absorption_rate = c.damage_absorption_rate + orginCard.damage_absorption_rate * coefficient,
            vitality_regeneration_rate = c.vitality_regeneration_rate + orginCard.vitality_regeneration_rate * coefficient,
            accuracy_rate = c.accuracy_rate + orginCard.accuracy_rate * coefficient,
            lifesteal_rate = c.lifesteal_rate + orginCard.lifesteal_rate * coefficient,
            shield_strength = c.shield_strength + orginCard.shield_strength * coefficient,
            tenacity = c.tenacity + orginCard.tenacity * coefficient,
            resistance_rate = c.resistance_rate + orginCard.resistance_rate * coefficient,
            combo_rate = c.combo_rate + orginCard.combo_rate * coefficient,
            reflection_rate = c.reflection_rate + orginCard.reflection_rate * coefficient,
            mana = c.mana + orginCard.mana * (float)coefficient,
            mana_regeneration_rate = c.mana_regeneration_rate + orginCard.mana_regeneration_rate * coefficient,
            damage_to_different_faction_rate = c.damage_to_different_faction_rate + orginCard.damage_to_different_faction_rate * coefficient,
            resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + orginCard.resistance_to_different_faction_rate * coefficient,
            damage_to_same_faction_rate = c.damage_to_same_faction_rate + orginCard.damage_to_same_faction_rate * coefficient,
            resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + orginCard.resistance_to_same_faction_rate * coefficient
        };
        skills.power = EvaluatePower.CalculatePower(
            skills.health,
            skills.physical_attack, skills.physical_defense,
            skills.magical_attack, skills.magical_defense,
            skills.chemical_attack, skills.chemical_defense,
            skills.atomic_attack, skills.atomic_defense,
            skills.mental_attack, skills.mental_defense,
            skills.speed,
            skills.critical_damage_rate, skills.critical_rate,
            skills.penetration_rate, skills.evasion_rate,
            skills.damage_absorption_rate, skills.vitality_regeneration_rate,
            skills.accuracy_rate, skills.lifesteal_rate,
            skills.shield_strength, skills.tenacity, skills.resistance_rate,
            skills.combo_rate, skills.reflection_rate,
            skills.mana, skills.mana_regeneration_rate,
            skills.damage_to_different_faction_rate, skills.resistance_to_different_faction_rate,
            skills.damage_to_same_faction_rate, skills.resistance_to_same_faction_rate
        );
        return skills;
    }

    public List<Skills> GetUserSkills(string user_id, string type, int pageSize, int offset)
    {
        List<Skills> list = _userSkillsRepository.GetUserSkills(user_id, type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserSkillsCount(string user_id, string type)
    {
        return _userSkillsRepository.GetUserSkillsCount(user_id, type);
    }

    public bool InsertUserSkills(Skills skills)
    {
        return _userSkillsRepository.InsertUserSkills(skills);
    }

    public bool UpdateSkillsLevel(Skills skills, int cardLevel)
    {
        return _userSkillsRepository.UpdateSkillsLevel(skills, cardLevel);
    }

    public bool UpdateSkillsBreakthrough(Skills skills, int star, int quantity)
    {
        return _userSkillsRepository.UpdateSkillsBreakthrough(skills, star, quantity);
    }

    public Skills GetUserSkillsById(string user_id, string Id)
    {
        return _userSkillsRepository.GetUserSkillsById(user_id, Id);
    }
}
