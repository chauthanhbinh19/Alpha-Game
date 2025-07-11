using System.Collections.Generic;

public class UserMagicFormationCircleService : IUserMagicFormationCircleService
{
    private IUserMagicFormationCircleRepository _userMagicFormationCircleRepository;

    public UserMagicFormationCircleService(IUserMagicFormationCircleRepository userMagicFormationCircleRepository)
    {
        _userMagicFormationCircleRepository = userMagicFormationCircleRepository;
    }

    public static UserMagicFormationCircleService Create()
    {
        return new UserMagicFormationCircleService(new UserMagicFormationCirlceRepository());
    }

    public MagicFormationCircle GetNewLevelPower(MagicFormationCircle c, double coefficient)
    {
        IMagicFormationCircleRepository _repository = new MagicFormationCircleRepository();
        MagicFormationCircleService _service = new MagicFormationCircleService(_repository);
        MagicFormationCircle orginCard = _service.GetMagicFormationCircleById(c.id);
        MagicFormationCircle magicFormationCircle = new MagicFormationCircle
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
        magicFormationCircle.power = EvaluatePower.CalculatePower(
            magicFormationCircle.health,
            magicFormationCircle.physical_attack, magicFormationCircle.physical_defense,
            magicFormationCircle.magical_attack, magicFormationCircle.magical_defense,
            magicFormationCircle.chemical_attack, magicFormationCircle.chemical_defense,
            magicFormationCircle.atomic_attack, magicFormationCircle.atomic_defense,
            magicFormationCircle.mental_attack, magicFormationCircle.mental_defense,
            magicFormationCircle.speed,
            magicFormationCircle.critical_damage_rate, magicFormationCircle.critical_rate, magicFormationCircle.critical_resistance_rate, magicFormationCircle.ignore_critical_rate,
            magicFormationCircle.penetration_rate, magicFormationCircle.penetration_resistance_rate, magicFormationCircle.evasion_rate,
            magicFormationCircle.damage_absorption_rate, magicFormationCircle.ignore_damage_absorption_rate, magicFormationCircle.absorbed_damage_rate,
            magicFormationCircle.vitality_regeneration_rate, magicFormationCircle.vitality_regeneration_resistance_rate,
            magicFormationCircle.accuracy_rate, magicFormationCircle.lifesteal_rate,
            magicFormationCircle.shield_strength, magicFormationCircle.tenacity, magicFormationCircle.resistance_rate,
            magicFormationCircle.combo_rate, magicFormationCircle.ignore_combo_rate, magicFormationCircle.combo_damage_rate, magicFormationCircle.combo_resistance_rate,
            magicFormationCircle.stun_rate, magicFormationCircle.ignore_stun_rate,
            magicFormationCircle.reflection_rate, magicFormationCircle.ignore_reflection_rate, magicFormationCircle.reflection_damage_rate, magicFormationCircle.reflection_resistance_rate,
            magicFormationCircle.mana, magicFormationCircle.mana_regeneration_rate,
            magicFormationCircle.damage_to_different_faction_rate, magicFormationCircle.resistance_to_different_faction_rate,
            magicFormationCircle.damage_to_same_faction_rate, magicFormationCircle.resistance_to_same_faction_rate,
            magicFormationCircle.normal_damage_rate, magicFormationCircle.normal_resistance_rate,
            magicFormationCircle.skill_damage_rate, magicFormationCircle.skill_resistance_rate
        );
        return magicFormationCircle;
    }
    public MagicFormationCircle GetNewBreakthroughPower(MagicFormationCircle c, double coefficient)
    {
        IMagicFormationCircleRepository _repository = new MagicFormationCircleRepository();
        MagicFormationCircleService _service = new MagicFormationCircleService(_repository);
        MagicFormationCircle orginCard = _service.GetMagicFormationCircleById(c.id);
        MagicFormationCircle magicFormationCircle = new MagicFormationCircle
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
        magicFormationCircle.power = EvaluatePower.CalculatePower(
            magicFormationCircle.health,
            magicFormationCircle.physical_attack, magicFormationCircle.physical_defense,
            magicFormationCircle.magical_attack, magicFormationCircle.magical_defense,
            magicFormationCircle.chemical_attack, magicFormationCircle.chemical_defense,
            magicFormationCircle.atomic_attack, magicFormationCircle.atomic_defense,
            magicFormationCircle.mental_attack, magicFormationCircle.mental_defense,
            magicFormationCircle.speed,
            magicFormationCircle.critical_damage_rate, magicFormationCircle.critical_rate, magicFormationCircle.critical_resistance_rate, magicFormationCircle.ignore_critical_rate,
            magicFormationCircle.penetration_rate, magicFormationCircle.penetration_resistance_rate, magicFormationCircle.evasion_rate,
            magicFormationCircle.damage_absorption_rate, magicFormationCircle.ignore_damage_absorption_rate, magicFormationCircle.absorbed_damage_rate,
            magicFormationCircle.vitality_regeneration_rate, magicFormationCircle.vitality_regeneration_resistance_rate,
            magicFormationCircle.accuracy_rate, magicFormationCircle.lifesteal_rate,
            magicFormationCircle.shield_strength, magicFormationCircle.tenacity, magicFormationCircle.resistance_rate,
            magicFormationCircle.combo_rate, magicFormationCircle.ignore_combo_rate, magicFormationCircle.combo_damage_rate, magicFormationCircle.combo_resistance_rate,
            magicFormationCircle.stun_rate, magicFormationCircle.ignore_stun_rate,
            magicFormationCircle.reflection_rate, magicFormationCircle.ignore_reflection_rate, magicFormationCircle.reflection_damage_rate, magicFormationCircle.reflection_resistance_rate,
            magicFormationCircle.mana, magicFormationCircle.mana_regeneration_rate,
            magicFormationCircle.damage_to_different_faction_rate, magicFormationCircle.resistance_to_different_faction_rate,
            magicFormationCircle.damage_to_same_faction_rate, magicFormationCircle.resistance_to_same_faction_rate,
            magicFormationCircle.normal_damage_rate, magicFormationCircle.normal_resistance_rate,
            magicFormationCircle.skill_damage_rate, magicFormationCircle.skill_resistance_rate
        );
        return magicFormationCircle;
    }

    public List<MagicFormationCircle> GetUserMagicFormationCircle(string user_id, string type, int pageSize, int offset)
    {
        List<MagicFormationCircle> list = _userMagicFormationCircleRepository.GetUserMagicFormationCircle(user_id, type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserMagicFormationCircleCount(string user_id, string type)
    {
        return _userMagicFormationCircleRepository.GetUserMagicFormationCircleCount(user_id, type);
    }

    public bool InsertUserMagicFormationCircle(MagicFormationCircle magicFormationCircle)
    {
        return _userMagicFormationCircleRepository.InsertUserMagicFormationCircle(magicFormationCircle);
    }

    public bool UpdateMagicFormationCircleLevel(MagicFormationCircle magicFormationCircle, int cardLevel)
    {
        return _userMagicFormationCircleRepository.UpdateMagicFormationCircleLevel(magicFormationCircle, cardLevel);
    }

    public bool UpdateMagicFormationCircleBreakthrough(MagicFormationCircle magicFormationCircle, int star, int quantity)
    {
        return _userMagicFormationCircleRepository.UpdateMagicFormationCircleBreakthrough(magicFormationCircle, star, quantity);
    }

    public MagicFormationCircle GetUserMagicFormationCircleById(string user_id, string Id)
    {
        return _userMagicFormationCircleRepository.GetUserMagicFormationCircleById(user_id, Id);
    }

    public MagicFormationCircle SumPowerUserMagicFormationCircle()
    {
        return _userMagicFormationCircleRepository.SumPowerUserMagicFormationCircle();
    }
}
