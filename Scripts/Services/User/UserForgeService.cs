using System.Collections.Generic;

public class UserForgeService : IUserForgeService
{
    private IUserForgeRepository _userForgeRepository;

    public UserForgeService(IUserForgeRepository userForgeRepository)
    {
        _userForgeRepository = userForgeRepository;
    }

    public static UserForgeService Create()
    {
        return new UserForgeService(new UserForgeRepository());
    }

    public Forge GetNewLevelPower(Forge c, double coefficient)
    {
        IForgeRepository _repository = new ForgeRepository();
        ForgeService _service = new ForgeService(_repository);
        Forge orginCard = _service.GetForgeById(c.id);
        Forge Forge = new Forge
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
        Forge.power = EvaluatePower.CalculatePower(
            Forge.health,
            Forge.physical_attack, Forge.physical_defense,
            Forge.magical_attack, Forge.magical_defense,
            Forge.chemical_attack, Forge.chemical_defense,
            Forge.atomic_attack, Forge.atomic_defense,
            Forge.mental_attack, Forge.mental_defense,
            Forge.speed,
            Forge.critical_damage_rate, Forge.critical_rate, Forge.critical_resistance_rate, Forge.ignore_critical_rate,
            Forge.penetration_rate, Forge.penetration_resistance_rate, Forge.evasion_rate,
            Forge.damage_absorption_rate, Forge.ignore_damage_absorption_rate, Forge.absorbed_damage_rate,
            Forge.vitality_regeneration_rate, Forge.vitality_regeneration_resistance_rate,
            Forge.accuracy_rate, Forge.lifesteal_rate,
            Forge.shield_strength, Forge.tenacity, Forge.resistance_rate,
            Forge.combo_rate, Forge.ignore_combo_rate, Forge.combo_damage_rate, Forge.combo_resistance_rate,
            Forge.stun_rate, Forge.ignore_stun_rate,
            Forge.reflection_rate, Forge.ignore_reflection_rate, Forge.reflection_damage_rate, Forge.reflection_resistance_rate,
            Forge.mana, Forge.mana_regeneration_rate,
            Forge.damage_to_different_faction_rate, Forge.resistance_to_different_faction_rate,
            Forge.damage_to_same_faction_rate, Forge.resistance_to_same_faction_rate,
            Forge.normal_damage_rate, Forge.normal_resistance_rate,
            Forge.skill_damage_rate, Forge.skill_resistance_rate
        );
        return Forge;
    }
    public Forge GetNewBreakthroughPower(Forge c, double coefficient)
    {
        IForgeRepository _repository = new ForgeRepository();
        ForgeService _service = new ForgeService(_repository);
        Forge orginCard = _service.GetForgeById(c.id);
        Forge Forge = new Forge
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
        Forge.power = EvaluatePower.CalculatePower(
            Forge.health,
            Forge.physical_attack, Forge.physical_defense,
            Forge.magical_attack, Forge.magical_defense,
            Forge.chemical_attack, Forge.chemical_defense,
            Forge.atomic_attack, Forge.atomic_defense,
            Forge.mental_attack, Forge.mental_defense,
            Forge.speed,
            Forge.critical_damage_rate, Forge.critical_rate, Forge.critical_resistance_rate, Forge.ignore_critical_rate,
            Forge.penetration_rate, Forge.penetration_resistance_rate, Forge.evasion_rate,
            Forge.damage_absorption_rate, Forge.ignore_damage_absorption_rate, Forge.absorbed_damage_rate,
            Forge.vitality_regeneration_rate, Forge.vitality_regeneration_resistance_rate,
            Forge.accuracy_rate, Forge.lifesteal_rate,
            Forge.shield_strength, Forge.tenacity, Forge.resistance_rate,
            Forge.combo_rate, Forge.ignore_combo_rate, Forge.combo_damage_rate, Forge.combo_resistance_rate,
            Forge.stun_rate, Forge.ignore_stun_rate,
            Forge.reflection_rate, Forge.ignore_reflection_rate, Forge.reflection_damage_rate, Forge.reflection_resistance_rate,
            Forge.mana, Forge.mana_regeneration_rate,
            Forge.damage_to_different_faction_rate, Forge.resistance_to_different_faction_rate,
            Forge.damage_to_same_faction_rate, Forge.resistance_to_same_faction_rate,
            Forge.normal_damage_rate, Forge.normal_resistance_rate,
            Forge.skill_damage_rate, Forge.skill_resistance_rate
        );
        return Forge;
    }

    public List<Forge> GetUserForge(string user_id, string type, int pageSize, int offset)
    {
        List<Forge> list = _userForgeRepository.GetUserForge(user_id, type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserForgeCount(string user_id, string type)
    {
        return _userForgeRepository.GetUserForgeCount(user_id, type);
    }

    public bool InsertUserForge(Forge forge)
    {
        return _userForgeRepository.InsertUserForge(forge);
    }

    public bool UpdateForgeLevel(Forge forge, int cardLevel)
    {
        return _userForgeRepository.UpdateForgeLevel(forge, cardLevel);
    }

    public bool UpdateForgeBreakthrough(Forge forge, int star, int quantity)
    {
        return _userForgeRepository.UpdateForgeBreakthrough(forge, star, quantity);
    }

    public Forge GetUserForgeById(string user_id, string Id)
    {
        return _userForgeRepository.GetUserForgeById(user_id, Id);
    }

    public Forge SumPowerUserForge()
    {
        return _userForgeRepository.SumPowerUserForge();
    }
}
