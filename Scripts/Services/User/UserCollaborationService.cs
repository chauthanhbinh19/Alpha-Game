using System.Collections.Generic;

public class UserCollaborationService : IUserCollaborationService
{
    private readonly IUserCollaborationRepository _userCollaborationRepository;

    public UserCollaborationService(IUserCollaborationRepository userCollaborationRepository)
    {
        _userCollaborationRepository = userCollaborationRepository;
    }

    public static UserCollaborationService Create()
    {
        return new UserCollaborationService(new UserCollaborationRepository());
    }

    public Collaboration GetNewLevelPower(Collaboration c, double coefficient)
    {
        ICollaborationRepository _repository = new CollaborationRepository();
        CollaborationService _service = new CollaborationService(_repository);
        Collaboration orginCard = _service.GetCollaborationsById(c.id);
        Collaboration collaboration = new Collaboration
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
            critical_resistance_rate = c.critical_resistance_rate + orginCard.critical_resistance_rate * coefficient,
            ignore_critical_rate = c.ignore_critical_rate + orginCard.ignore_critical_rate * coefficient,
            penetration_rate = c.penetration_rate + orginCard.penetration_rate * coefficient,
            penetration_resistance_rate = c.penetration_resistance_rate + orginCard.penetration_resistance_rate * coefficient,
            evasion_rate = c.evasion_rate + orginCard.evasion_rate * coefficient,
            damage_absorption_rate = c.damage_absorption_rate + orginCard.damage_absorption_rate * coefficient,
            ignore_damage_absorption_rate = c.ignore_damage_absorption_rate + orginCard.ignore_damage_absorption_rate * coefficient,
            absorbed_damage_rate = c.absorbed_damage_rate + orginCard.absorbed_damage_rate * coefficient,
            vitality_regeneration_rate = c.vitality_regeneration_rate + orginCard.vitality_regeneration_rate * coefficient,
            vitality_regeneration_resistance_rate = c.vitality_regeneration_resistance_rate + orginCard.vitality_regeneration_resistance_rate * coefficient,
            accuracy_rate = c.accuracy_rate + orginCard.accuracy_rate * coefficient,
            lifesteal_rate = c.lifesteal_rate + orginCard.lifesteal_rate * coefficient,
            shield_strength = c.shield_strength + orginCard.shield_strength * coefficient,
            tenacity = c.tenacity + orginCard.tenacity * coefficient,
            resistance_rate = c.resistance_rate + orginCard.resistance_rate * coefficient,
            combo_rate = c.combo_rate + orginCard.combo_rate * coefficient,
            ignore_combo_rate = c.ignore_combo_rate + orginCard.ignore_combo_rate * coefficient,
            combo_damage_rate = c.combo_damage_rate + orginCard.combo_damage_rate * coefficient,
            combo_resistance_rate = c.combo_resistance_rate + orginCard.combo_resistance_rate * coefficient,
            stun_rate = c.stun_rate + orginCard.stun_rate * coefficient,
            ignore_stun_rate = c.ignore_stun_rate + orginCard.ignore_stun_rate * coefficient,
            reflection_rate = c.reflection_rate + orginCard.reflection_rate * coefficient,
            ignore_reflection_rate  = c.ignore_reflection_rate + orginCard.ignore_reflection_rate * coefficient,
            reflection_damage_rate = c.reflection_damage_rate + orginCard.reflection_damage_rate * coefficient,
            reflection_resistance_rate = c.reflection_resistance_rate + orginCard.reflection_resistance_rate * coefficient,
            mana = c.mana + orginCard.mana * (float)coefficient,
            mana_regeneration_rate = c.mana_regeneration_rate + orginCard.mana_regeneration_rate * coefficient,
            damage_to_different_faction_rate = c.damage_to_different_faction_rate + orginCard.damage_to_different_faction_rate * coefficient,
            resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + orginCard.resistance_to_different_faction_rate * coefficient,
            damage_to_same_faction_rate = c.damage_to_same_faction_rate + orginCard.damage_to_same_faction_rate * coefficient,
            resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + orginCard.resistance_to_same_faction_rate * coefficient,
            normal_damage_rate = c.normal_damage_rate + orginCard.normal_damage_rate * coefficient,
            normal_resistance_rate = c.normal_resistance_rate + orginCard.normal_resistance_rate * coefficient,
            skill_damage_rate = c.skill_damage_rate + orginCard.skill_damage_rate * coefficient,
            skill_resistance_rate = c.skill_resistance_rate + orginCard.skill_resistance_rate * coefficient
        };
        collaboration.power = EvaluatePower.CalculatePower(
            collaboration.health,
            collaboration.physical_attack, collaboration.physical_defense,
            collaboration.magical_attack, collaboration.magical_defense,
            collaboration.chemical_attack, collaboration.chemical_defense,
            collaboration.atomic_attack, collaboration.atomic_defense,
            collaboration.mental_attack, collaboration.mental_defense,
            collaboration.speed,
            collaboration.critical_damage_rate, collaboration.critical_rate, collaboration.critical_resistance_rate, collaboration.ignore_critical_rate,
            collaboration.penetration_rate, collaboration.penetration_resistance_rate, collaboration.evasion_rate,
            collaboration.damage_absorption_rate, collaboration.ignore_damage_absorption_rate, collaboration.absorbed_damage_rate,
            collaboration.vitality_regeneration_rate, collaboration.vitality_regeneration_resistance_rate,
            collaboration.accuracy_rate, collaboration.lifesteal_rate,
            collaboration.shield_strength, collaboration.tenacity, collaboration.resistance_rate,
            collaboration.combo_rate, collaboration.ignore_combo_rate, collaboration.combo_damage_rate, collaboration.combo_resistance_rate,
            collaboration.stun_rate, collaboration.ignore_stun_rate,
            collaboration.reflection_rate, collaboration.ignore_reflection_rate, collaboration.reflection_damage_rate, collaboration.reflection_resistance_rate,
            collaboration.mana, collaboration.mana_regeneration_rate,
            collaboration.damage_to_different_faction_rate, collaboration.resistance_to_different_faction_rate,
            collaboration.damage_to_same_faction_rate, collaboration.resistance_to_same_faction_rate,
            collaboration.normal_damage_rate, collaboration.normal_resistance_rate,
            collaboration.skill_damage_rate, collaboration.skill_resistance_rate
        );
        return collaboration;
    }
    public Collaboration GetNewBreakthroughPower(Collaboration c, double coefficient)
    {
        ICollaborationRepository _repository = new CollaborationRepository();
        CollaborationService _service = new CollaborationService(_repository);
        Collaboration orginCard = _service.GetCollaborationsById(c.id);
        Collaboration collaboration = new Collaboration
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
            critical_resistance_rate = c.critical_resistance_rate + orginCard.critical_resistance_rate * coefficient,
            ignore_critical_rate = c.ignore_critical_rate + orginCard.ignore_critical_rate * coefficient,
            penetration_rate = c.penetration_rate + orginCard.penetration_rate * coefficient,
            penetration_resistance_rate = c.penetration_resistance_rate + orginCard.penetration_resistance_rate * coefficient,
            evasion_rate = c.evasion_rate + orginCard.evasion_rate * coefficient,
            damage_absorption_rate = c.damage_absorption_rate + orginCard.damage_absorption_rate * coefficient,
            ignore_damage_absorption_rate = c.ignore_damage_absorption_rate + orginCard.ignore_damage_absorption_rate * coefficient,
            absorbed_damage_rate = c.absorbed_damage_rate + orginCard.absorbed_damage_rate * coefficient,
            vitality_regeneration_rate = c.vitality_regeneration_rate + orginCard.vitality_regeneration_rate * coefficient,
            vitality_regeneration_resistance_rate = c.vitality_regeneration_resistance_rate + orginCard.vitality_regeneration_resistance_rate * coefficient,
            accuracy_rate = c.accuracy_rate + orginCard.accuracy_rate * coefficient,
            lifesteal_rate = c.lifesteal_rate + orginCard.lifesteal_rate * coefficient,
            shield_strength = c.shield_strength + orginCard.shield_strength * coefficient,
            tenacity = c.tenacity + orginCard.tenacity * coefficient,
            resistance_rate = c.resistance_rate + orginCard.resistance_rate * coefficient,
            combo_rate = c.combo_rate + orginCard.combo_rate * coefficient,
            ignore_combo_rate = c.ignore_combo_rate + orginCard.ignore_combo_rate * coefficient,
            combo_damage_rate = c.combo_damage_rate + orginCard.combo_damage_rate * coefficient,
            combo_resistance_rate = c.combo_resistance_rate + orginCard.combo_resistance_rate * coefficient,
            stun_rate = c.stun_rate + orginCard.stun_rate * coefficient,
            ignore_stun_rate = c.ignore_stun_rate + orginCard.ignore_stun_rate * coefficient,
            reflection_rate = c.reflection_rate + orginCard.reflection_rate * coefficient,
            ignore_reflection_rate  = c.ignore_reflection_rate + orginCard.ignore_reflection_rate * coefficient,
            reflection_damage_rate = c.reflection_damage_rate + orginCard.reflection_damage_rate * coefficient,
            reflection_resistance_rate = c.reflection_resistance_rate + orginCard.reflection_resistance_rate * coefficient,
            mana = c.mana + orginCard.mana * (float)coefficient,
            mana_regeneration_rate = c.mana_regeneration_rate + orginCard.mana_regeneration_rate * coefficient,
            damage_to_different_faction_rate = c.damage_to_different_faction_rate + orginCard.damage_to_different_faction_rate * coefficient,
            resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + orginCard.resistance_to_different_faction_rate * coefficient,
            damage_to_same_faction_rate = c.damage_to_same_faction_rate + orginCard.damage_to_same_faction_rate * coefficient,
            resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + orginCard.resistance_to_same_faction_rate * coefficient,
            normal_damage_rate = c.normal_damage_rate + orginCard.normal_damage_rate * coefficient,
            normal_resistance_rate = c.normal_resistance_rate + orginCard.normal_resistance_rate * coefficient,
            skill_damage_rate = c.skill_damage_rate + orginCard.skill_damage_rate * coefficient,
            skill_resistance_rate = c.skill_resistance_rate + orginCard.skill_resistance_rate * coefficient
        };
        collaboration.power = EvaluatePower.CalculatePower(
            collaboration.health,
            collaboration.physical_attack, collaboration.physical_defense,
            collaboration.magical_attack, collaboration.magical_defense,
            collaboration.chemical_attack, collaboration.chemical_defense,
            collaboration.atomic_attack, collaboration.atomic_defense,
            collaboration.mental_attack, collaboration.mental_defense,
            collaboration.speed,
            collaboration.critical_damage_rate, collaboration.critical_rate, collaboration.critical_resistance_rate, collaboration.ignore_critical_rate,
            collaboration.penetration_rate, collaboration.penetration_resistance_rate, collaboration.evasion_rate,
            collaboration.damage_absorption_rate, collaboration.ignore_damage_absorption_rate, collaboration.absorbed_damage_rate,
            collaboration.vitality_regeneration_rate, collaboration.vitality_regeneration_resistance_rate,
            collaboration.accuracy_rate, collaboration.lifesteal_rate,
            collaboration.shield_strength, collaboration.tenacity, collaboration.resistance_rate,
            collaboration.combo_rate, collaboration.ignore_combo_rate, collaboration.combo_damage_rate, collaboration.combo_resistance_rate,
            collaboration.stun_rate, collaboration.ignore_stun_rate,
            collaboration.reflection_rate, collaboration.ignore_reflection_rate, collaboration.reflection_damage_rate, collaboration.reflection_resistance_rate,
            collaboration.mana, collaboration.mana_regeneration_rate,
            collaboration.damage_to_different_faction_rate, collaboration.resistance_to_different_faction_rate,
            collaboration.damage_to_same_faction_rate, collaboration.resistance_to_same_faction_rate,
            collaboration.normal_damage_rate, collaboration.normal_resistance_rate,
            collaboration.skill_damage_rate, collaboration.skill_resistance_rate
        );
        return collaboration;
    }

    public List<Collaboration> GetUserCollaboration(string user_id, int pageSize, int offset)
    {
        List<Collaboration> list = _userCollaborationRepository.GetUserCollaboration(user_id, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserCollaborationCount(string user_id)
    {
        return _userCollaborationRepository.GetUserCollaborationCount(user_id);
    }

    public bool InsertUserCollaborations(Collaboration collaboration)
    {
        return _userCollaborationRepository.InsertUserCollaborations(collaboration);
    }

    public bool UpdateCollaborationsLevel(Collaboration collaboration, int cardLevel)
    {
        return _userCollaborationRepository.UpdateCollaborationsLevel(collaboration, cardLevel);
    }

    public bool UpdateCollaborationsBreakthrough(Collaboration collaboration, int star, int quantity)
    {
        return _userCollaborationRepository.UpdateCollaborationsBreakthrough(collaboration, star, quantity);
    }

    public Collaboration GetUserCollaborationsById(string user_id, string Id)
    {
        return _userCollaborationRepository.GetUserCollaborationsById(user_id, Id);
    }

    public Collaboration SumPowerUserCollaborations()
    {
        return _userCollaborationRepository.SumPowerUserCollaborations();
    }
}
