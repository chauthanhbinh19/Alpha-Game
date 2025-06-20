using System.Collections.Generic;

public class UserPuppetService : IUserPuppetService
{
    private readonly IUserPuppetRepository _userPuppetRepository;

    public UserPuppetService(IUserPuppetRepository userPuppetRepository)
    {
        _userPuppetRepository = userPuppetRepository;
    }

    public static UserPuppetService Create()
    {
        return new UserPuppetService(new UserPuppetRepository());
    }

    public Puppet GetNewLevelPower(Puppet c, double coefficient)
    {
        IPuppetRepository _repository = new PuppetRepository();
        PuppetService _service = new PuppetService(_repository);
        Puppet orginCard = _service.GetPuppetById(c.id);
        Puppet Puppet = new Puppet
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
        Puppet.power = EvaluatePower.CalculatePower(
            Puppet.health,
            Puppet.physical_attack, Puppet.physical_defense,
            Puppet.magical_attack, Puppet.magical_defense,
            Puppet.chemical_attack, Puppet.chemical_defense,
            Puppet.atomic_attack, Puppet.atomic_defense,
            Puppet.mental_attack, Puppet.mental_defense,
            Puppet.speed,
            Puppet.critical_damage_rate, Puppet.critical_rate, Puppet.critical_resistance_rate, Puppet.ignore_critical_rate,
            Puppet.penetration_rate, Puppet.penetration_resistance_rate, Puppet.evasion_rate,
            Puppet.damage_absorption_rate, Puppet.ignore_damage_absorption_rate, Puppet.absorbed_damage_rate,
            Puppet.vitality_regeneration_rate, Puppet.vitality_regeneration_resistance_rate,
            Puppet.accuracy_rate, Puppet.lifesteal_rate,
            Puppet.shield_strength, Puppet.tenacity, Puppet.resistance_rate,
            Puppet.combo_rate, Puppet.ignore_combo_rate, Puppet.combo_damage_rate, Puppet.combo_resistance_rate,
            Puppet.stun_rate, Puppet.ignore_stun_rate,
            Puppet.reflection_rate, Puppet.ignore_reflection_rate, Puppet.reflection_damage_rate, Puppet.reflection_resistance_rate,
            Puppet.mana, Puppet.mana_regeneration_rate,
            Puppet.damage_to_different_faction_rate, Puppet.resistance_to_different_faction_rate,
            Puppet.damage_to_same_faction_rate, Puppet.resistance_to_same_faction_rate,
            Puppet.normal_damage_rate, Puppet.normal_resistance_rate,
            Puppet.skill_damage_rate, Puppet.skill_resistance_rate
        );
        return Puppet;
    }
    public Puppet GetNewBreakthroughPower(Puppet c, double coefficient)
    {
        IPuppetRepository _repository = new PuppetRepository();
        PuppetService _service = new PuppetService(_repository);
        Puppet orginCard = _service.GetPuppetById(c.id);
        Puppet Puppet = new Puppet
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
        Puppet.power = EvaluatePower.CalculatePower(
            Puppet.health,
            Puppet.physical_attack, Puppet.physical_defense,
            Puppet.magical_attack, Puppet.magical_defense,
            Puppet.chemical_attack, Puppet.chemical_defense,
            Puppet.atomic_attack, Puppet.atomic_defense,
            Puppet.mental_attack, Puppet.mental_defense,
            Puppet.speed,
            Puppet.critical_damage_rate, Puppet.critical_rate, Puppet.critical_resistance_rate, Puppet.ignore_critical_rate,
            Puppet.penetration_rate, Puppet.penetration_resistance_rate, Puppet.evasion_rate,
            Puppet.damage_absorption_rate, Puppet.ignore_damage_absorption_rate, Puppet.absorbed_damage_rate,
            Puppet.vitality_regeneration_rate, Puppet.vitality_regeneration_resistance_rate,
            Puppet.accuracy_rate, Puppet.lifesteal_rate,
            Puppet.shield_strength, Puppet.tenacity, Puppet.resistance_rate,
            Puppet.combo_rate, Puppet.ignore_combo_rate, Puppet.combo_damage_rate, Puppet.combo_resistance_rate,
            Puppet.stun_rate, Puppet.ignore_stun_rate,
            Puppet.reflection_rate, Puppet.ignore_reflection_rate, Puppet.reflection_damage_rate, Puppet.reflection_resistance_rate,
            Puppet.mana, Puppet.mana_regeneration_rate,
            Puppet.damage_to_different_faction_rate, Puppet.resistance_to_different_faction_rate,
            Puppet.damage_to_same_faction_rate, Puppet.resistance_to_same_faction_rate,
            Puppet.normal_damage_rate, Puppet.normal_resistance_rate,
            Puppet.skill_damage_rate, Puppet.skill_resistance_rate
        );
        return Puppet;
    }

    public List<Puppet> GetUserPuppet(string user_id, string type, int pageSize, int offset)
    {
        List<Puppet> list = _userPuppetRepository.GetUserPuppet(user_id, type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserPuppetCount(string user_id, string type)
    {
        return _userPuppetRepository.GetUserPuppetCount(user_id, type);
    }

    public bool InsertUserPuppet(Puppet puppet)
    {
        return _userPuppetRepository.InsertUserPuppet(puppet);
    }

    public bool UpdatePuppetLevel(Puppet puppet, int cardLevel)
    {
        return _userPuppetRepository.UpdatePuppetLevel(puppet, cardLevel);
    }

    public bool UpdatePuppetBreakthrough(Puppet puppet, int star, int quantity)
    {
        return _userPuppetRepository.UpdatePuppetBreakthrough(puppet, star, quantity);
    }

    public Puppet GetUserPuppetById(string user_id, string Id)
    {
        return _userPuppetRepository.GetUserPuppetById(user_id, Id);
    }

    public Puppet SumPowerUserPuppet()
    {
        return _userPuppetRepository.SumPowerUserPuppet();
    }
}
