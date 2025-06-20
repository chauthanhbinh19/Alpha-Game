using System.Collections.Generic;

public class UserCollaborationEquipmentService : IUserCollaborationEquipmentService
{
    private readonly IUserCollaborationEquipmentRepository _userCollabEquipmentsRepo;

    public UserCollaborationEquipmentService(IUserCollaborationEquipmentRepository userCollabEquipmentsRepo)
    {
        _userCollabEquipmentsRepo = userCollabEquipmentsRepo;
    }

    public static UserCollaborationEquipmentService Create()
    {
        return new UserCollaborationEquipmentService(new UserCollaborationEquipmentRepository());
    }

    public CollaborationEquipment GetNewLevelPower(CollaborationEquipment c, double coefficient)
    {
        ICollaborationEquipmentRepository _repository = new CollaborationEquipmentRepository();
        CollaborationEquipmentService _service = new CollaborationEquipmentService(_repository);
        CollaborationEquipment orginCard = _service.GetCollaborationEquipmentsById(c.id);
        CollaborationEquipment collaborationEquipment = new CollaborationEquipment
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
        collaborationEquipment.power = EvaluatePower.CalculatePower(
            collaborationEquipment.health,
            collaborationEquipment.physical_attack, collaborationEquipment.physical_defense,
            collaborationEquipment.magical_attack, collaborationEquipment.magical_defense,
            collaborationEquipment.chemical_attack, collaborationEquipment.chemical_defense,
            collaborationEquipment.atomic_attack, collaborationEquipment.atomic_defense,
            collaborationEquipment.mental_attack, collaborationEquipment.mental_defense,
            collaborationEquipment.speed,
            collaborationEquipment.critical_damage_rate, collaborationEquipment.critical_rate, collaborationEquipment.critical_resistance_rate, collaborationEquipment.ignore_critical_rate,
            collaborationEquipment.penetration_rate, collaborationEquipment.penetration_resistance_rate, collaborationEquipment.evasion_rate,
            collaborationEquipment.damage_absorption_rate, collaborationEquipment.ignore_damage_absorption_rate, collaborationEquipment.absorbed_damage_rate,
            collaborationEquipment.vitality_regeneration_rate, collaborationEquipment.vitality_regeneration_resistance_rate,
            collaborationEquipment.accuracy_rate, collaborationEquipment.lifesteal_rate,
            collaborationEquipment.shield_strength, collaborationEquipment.tenacity, collaborationEquipment.resistance_rate,
            collaborationEquipment.combo_rate, collaborationEquipment.ignore_combo_rate, collaborationEquipment.combo_damage_rate, collaborationEquipment.combo_resistance_rate,
            collaborationEquipment.stun_rate, collaborationEquipment.ignore_stun_rate,
            collaborationEquipment.reflection_rate, collaborationEquipment.ignore_reflection_rate, collaborationEquipment.reflection_damage_rate, collaborationEquipment.reflection_resistance_rate,
            collaborationEquipment.mana, collaborationEquipment.mana_regeneration_rate,
            collaborationEquipment.damage_to_different_faction_rate, collaborationEquipment.resistance_to_different_faction_rate,
            collaborationEquipment.damage_to_same_faction_rate, collaborationEquipment.resistance_to_same_faction_rate,
            collaborationEquipment.normal_damage_rate, collaborationEquipment.normal_resistance_rate,
            collaborationEquipment.skill_damage_rate, collaborationEquipment.skill_resistance_rate
        );
        return collaborationEquipment;
    }
    public CollaborationEquipment GetNewBreakthroughPower(CollaborationEquipment c, double coefficient)
    {
        ICollaborationEquipmentRepository _repository = new CollaborationEquipmentRepository();
        CollaborationEquipmentService _service = new CollaborationEquipmentService(_repository);
        CollaborationEquipment orginCard = _service.GetCollaborationEquipmentsById(c.id);
        CollaborationEquipment collaborationEquipment = new CollaborationEquipment
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
        collaborationEquipment.power = EvaluatePower.CalculatePower(
            collaborationEquipment.health,
            collaborationEquipment.physical_attack, collaborationEquipment.physical_defense,
            collaborationEquipment.magical_attack, collaborationEquipment.magical_defense,
            collaborationEquipment.chemical_attack, collaborationEquipment.chemical_defense,
            collaborationEquipment.atomic_attack, collaborationEquipment.atomic_defense,
            collaborationEquipment.mental_attack, collaborationEquipment.mental_defense,
            collaborationEquipment.speed,
            collaborationEquipment.critical_damage_rate, collaborationEquipment.critical_rate, collaborationEquipment.critical_resistance_rate, collaborationEquipment.ignore_critical_rate,
            collaborationEquipment.penetration_rate, collaborationEquipment.penetration_resistance_rate, collaborationEquipment.evasion_rate,
            collaborationEquipment.damage_absorption_rate, collaborationEquipment.ignore_damage_absorption_rate, collaborationEquipment.absorbed_damage_rate,
            collaborationEquipment.vitality_regeneration_rate, collaborationEquipment.vitality_regeneration_resistance_rate,
            collaborationEquipment.accuracy_rate, collaborationEquipment.lifesteal_rate,
            collaborationEquipment.shield_strength, collaborationEquipment.tenacity, collaborationEquipment.resistance_rate,
            collaborationEquipment.combo_rate, collaborationEquipment.ignore_combo_rate, collaborationEquipment.combo_damage_rate, collaborationEquipment.combo_resistance_rate,
            collaborationEquipment.stun_rate, collaborationEquipment.ignore_stun_rate,
            collaborationEquipment.reflection_rate, collaborationEquipment.ignore_reflection_rate, collaborationEquipment.reflection_damage_rate, collaborationEquipment.reflection_resistance_rate,
            collaborationEquipment.mana, collaborationEquipment.mana_regeneration_rate,
            collaborationEquipment.damage_to_different_faction_rate, collaborationEquipment.resistance_to_different_faction_rate,
            collaborationEquipment.damage_to_same_faction_rate, collaborationEquipment.resistance_to_same_faction_rate,
            collaborationEquipment.normal_damage_rate, collaborationEquipment.normal_resistance_rate,
            collaborationEquipment.skill_damage_rate, collaborationEquipment.skill_resistance_rate
        );
        return collaborationEquipment;
    }

    public List<CollaborationEquipment> GetUserCollaborationEquipments(string user_id, string type, int pageSize, int offset)
    {
        List<CollaborationEquipment> list = _userCollabEquipmentsRepo.GetUserCollaborationEquipments(user_id, type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserCollaborationEquipmentCount(string user_id, string type)
    {
        return _userCollabEquipmentsRepo.GetUserCollaborationEquipmentCount(user_id, type);
    }

    public bool InsertUserCollaborationEquipments(CollaborationEquipment collaborationEquipment)
    {
        return _userCollabEquipmentsRepo.InsertUserCollaborationEquipments(collaborationEquipment);
    }

    public bool UpdateCollaborationEquipmentsLevel(CollaborationEquipment collaborationEquipment, int cardLevel)
    {
        return _userCollabEquipmentsRepo.UpdateCollaborationEquipmentsLevel(collaborationEquipment, cardLevel);
    }

    public bool UpdateCollaborationEquipmentsBreakthrough(CollaborationEquipment collaborationEquipment, int star, int quantity)
    {
        return _userCollabEquipmentsRepo.UpdateCollaborationEquipmentsBreakthrough(collaborationEquipment, star, quantity);
    }

    public CollaborationEquipment GetUserCollaborationEquipmentsById(string user_id, string Id)
    {
        return _userCollabEquipmentsRepo.GetUserCollaborationEquipmentsById(user_id, Id);
    }

    public CollaborationEquipment SumPowerUserCollaborationEquipments()
    {
        return _userCollabEquipmentsRepo.SumPowerUserCollaborationEquipments();
    }
}
