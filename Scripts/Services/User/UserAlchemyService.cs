
using System.Collections.Generic;

public class UserAlchemyService : IUserAlchemyService
{
    private IUserAlchemyRepository _userAlchemyRepository;

    public UserAlchemyService(IUserAlchemyRepository userAlchemyRepository)
    {
        _userAlchemyRepository = userAlchemyRepository;
    }

    public static UserAlchemyService Create()
    {
        return new UserAlchemyService(new UserAlchemyRepository());
    }

    public Alchemy GetNewLevelPower(Alchemy c, double coefficient)
    {
        IAlchemyRepository _repository = new AlchemyRepository();
        AlchemyService _service = new AlchemyService(_repository);
        Alchemy orginCard = _service.GetAlchemyById(c.id);
        Alchemy Alchemy = new Alchemy
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
        Alchemy.power = EvaluatePower.CalculatePower(
            Alchemy.health,
            Alchemy.physical_attack, Alchemy.physical_defense,
            Alchemy.magical_attack, Alchemy.magical_defense,
            Alchemy.chemical_attack, Alchemy.chemical_defense,
            Alchemy.atomic_attack, Alchemy.atomic_defense,
            Alchemy.mental_attack, Alchemy.mental_defense,
            Alchemy.speed,
            Alchemy.critical_damage_rate, Alchemy.critical_rate, Alchemy.critical_resistance_rate, Alchemy.ignore_critical_rate,
            Alchemy.penetration_rate, Alchemy.penetration_resistance_rate, Alchemy.evasion_rate,
            Alchemy.damage_absorption_rate, Alchemy.ignore_damage_absorption_rate, Alchemy.absorbed_damage_rate,
            Alchemy.vitality_regeneration_rate, Alchemy.vitality_regeneration_resistance_rate,
            Alchemy.accuracy_rate, Alchemy.lifesteal_rate,
            Alchemy.shield_strength, Alchemy.tenacity, Alchemy.resistance_rate,
            Alchemy.combo_rate, Alchemy.ignore_combo_rate, Alchemy.combo_damage_rate, Alchemy.combo_resistance_rate,
            Alchemy.stun_rate, Alchemy.ignore_stun_rate,
            Alchemy.reflection_rate, Alchemy.ignore_reflection_rate, Alchemy.reflection_damage_rate, Alchemy.reflection_resistance_rate,
            Alchemy.mana, Alchemy.mana_regeneration_rate,
            Alchemy.damage_to_different_faction_rate, Alchemy.resistance_to_different_faction_rate,
            Alchemy.damage_to_same_faction_rate, Alchemy.resistance_to_same_faction_rate,
            Alchemy.normal_damage_rate, Alchemy.normal_resistance_rate,
            Alchemy.skill_damage_rate, Alchemy.skill_resistance_rate
        );
        return Alchemy;
    }
    public Alchemy GetNewBreakthroughPower(Alchemy c, double coefficient)
    {
        IAlchemyRepository _repository = new AlchemyRepository();
        AlchemyService _service = new AlchemyService(_repository);
        Alchemy orginCard = _service.GetAlchemyById(c.id);
        Alchemy Alchemy = new Alchemy
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
        Alchemy.power = EvaluatePower.CalculatePower(
            Alchemy.health,
            Alchemy.physical_attack, Alchemy.physical_defense,
            Alchemy.magical_attack, Alchemy.magical_defense,
            Alchemy.chemical_attack, Alchemy.chemical_defense,
            Alchemy.atomic_attack, Alchemy.atomic_defense,
            Alchemy.mental_attack, Alchemy.mental_defense,
            Alchemy.speed,
            Alchemy.critical_damage_rate, Alchemy.critical_rate, Alchemy.critical_resistance_rate, Alchemy.ignore_critical_rate,
            Alchemy.penetration_rate, Alchemy.penetration_resistance_rate, Alchemy.evasion_rate,
            Alchemy.damage_absorption_rate, Alchemy.ignore_damage_absorption_rate, Alchemy.absorbed_damage_rate,
            Alchemy.vitality_regeneration_rate, Alchemy.vitality_regeneration_resistance_rate,
            Alchemy.accuracy_rate, Alchemy.lifesteal_rate,
            Alchemy.shield_strength, Alchemy.tenacity, Alchemy.resistance_rate,
            Alchemy.combo_rate, Alchemy.ignore_combo_rate, Alchemy.combo_damage_rate, Alchemy.combo_resistance_rate,
            Alchemy.stun_rate, Alchemy.ignore_stun_rate,
            Alchemy.reflection_rate, Alchemy.ignore_reflection_rate, Alchemy.reflection_damage_rate, Alchemy.reflection_resistance_rate,
            Alchemy.mana, Alchemy.mana_regeneration_rate,
            Alchemy.damage_to_different_faction_rate, Alchemy.resistance_to_different_faction_rate,
            Alchemy.damage_to_same_faction_rate, Alchemy.resistance_to_same_faction_rate,
            Alchemy.normal_damage_rate, Alchemy.normal_resistance_rate,
            Alchemy.skill_damage_rate, Alchemy.skill_resistance_rate
        );
        return Alchemy;
    }

    public List<Alchemy> GetUserAlchemy(string user_id, string type, int pageSize, int offset)
    {
        List<Alchemy> list = _userAlchemyRepository.GetUserAlchemy(user_id, type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserAlchemyCount(string user_id, string type)
    {
        return _userAlchemyRepository.GetUserAlchemyCount(user_id, type);
    }

    public bool InsertUserAlchemy(Alchemy alchemy)
    {
        return _userAlchemyRepository.InsertUserAlchemy(alchemy);
    }

    public bool UpdateAlchemyLevel(Alchemy alchemy, int cardLevel)
    {
        return _userAlchemyRepository.UpdateAlchemyLevel(alchemy, cardLevel);
    }

    public bool UpdateAlchemyBreakthrough(Alchemy alchemy, int star, int quantity)
    {
        return _userAlchemyRepository.UpdateAlchemyBreakthrough(alchemy, star, quantity);
    }

    public Alchemy GetUserAlchemyById(string user_id, string Id)
    {
        return _userAlchemyRepository.GetUserAlchemyById(user_id, Id);
    }

    public Alchemy SumPowerUserAlchemy()
    {
        return _userAlchemyRepository.SumPowerUserAlchemy();
    }
}
