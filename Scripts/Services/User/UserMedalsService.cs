using System.Collections.Generic;

public class UserMedalsService : IUserMedalsService
{
    private IUserMedalsRepository _userMedalsRepository;

    public UserMedalsService(IUserMedalsRepository userMedalsRepository)
    {
        _userMedalsRepository = userMedalsRepository;
    }

    public static UserMedalsService Create()
    {
        return new UserMedalsService(new UserMedalsRepository());
    }

    public Medals GetNewLevelPower(Medals c, double coefficient)
    {
        IMedalsRepository _repository = new MedalsRepository();
        MedalsService _service = new MedalsService(_repository);
        Medals orginCard = _service.GetMedalsById(c.id);
        Medals medals = new Medals
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
        medals.power = EvaluatePower.CalculatePower(
            medals.health,
            medals.physical_attack, medals.physical_defense,
            medals.magical_attack, medals.magical_defense,
            medals.chemical_attack, medals.chemical_defense,
            medals.atomic_attack, medals.atomic_defense,
            medals.mental_attack, medals.mental_defense,
            medals.speed,
            medals.critical_damage_rate, medals.critical_rate, medals.critical_resistance_rate, medals.ignore_critical_rate,
            medals.penetration_rate, medals.penetration_resistance_rate, medals.evasion_rate,
            medals.damage_absorption_rate, medals.ignore_damage_absorption_rate, medals.absorbed_damage_rate,
            medals.vitality_regeneration_rate, medals.vitality_regeneration_resistance_rate,
            medals.accuracy_rate, medals.lifesteal_rate,
            medals.shield_strength, medals.tenacity, medals.resistance_rate,
            medals.combo_rate, medals.ignore_combo_rate, medals.combo_damage_rate, medals.combo_resistance_rate,
            medals.stun_rate, medals.ignore_stun_rate,
            medals.reflection_rate, medals.ignore_reflection_rate, medals.reflection_damage_rate, medals.reflection_resistance_rate,
            medals.mana, medals.mana_regeneration_rate,
            medals.damage_to_different_faction_rate, medals.resistance_to_different_faction_rate,
            medals.damage_to_same_faction_rate, medals.resistance_to_same_faction_rate,
            medals.normal_damage_rate, medals.normal_resistance_rate,
            medals.skill_damage_rate, medals.skill_resistance_rate
        );
        return medals;
    }
    public Medals GetNewBreakthroughPower(Medals c, double coefficient)
    {
        IMedalsRepository _repository = new MedalsRepository();
        MedalsService _service = new MedalsService(_repository);
        Medals orginCard = _service.GetMedalsById(c.id);
        Medals medals = new Medals
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
        medals.power = EvaluatePower.CalculatePower(
            medals.health,
            medals.physical_attack, medals.physical_defense,
            medals.magical_attack, medals.magical_defense,
            medals.chemical_attack, medals.chemical_defense,
            medals.atomic_attack, medals.atomic_defense,
            medals.mental_attack, medals.mental_defense,
            medals.speed,
            medals.critical_damage_rate, medals.critical_rate, medals.critical_resistance_rate, medals.ignore_critical_rate,
            medals.penetration_rate, medals.penetration_resistance_rate, medals.evasion_rate,
            medals.damage_absorption_rate, medals.ignore_damage_absorption_rate, medals.absorbed_damage_rate,
            medals.vitality_regeneration_rate, medals.vitality_regeneration_resistance_rate,
            medals.accuracy_rate, medals.lifesteal_rate,
            medals.shield_strength, medals.tenacity, medals.resistance_rate,
            medals.combo_rate, medals.ignore_combo_rate, medals.combo_damage_rate, medals.combo_resistance_rate,
            medals.stun_rate, medals.ignore_stun_rate,
            medals.reflection_rate, medals.ignore_reflection_rate, medals.reflection_damage_rate, medals.reflection_resistance_rate,
            medals.mana, medals.mana_regeneration_rate,
            medals.damage_to_different_faction_rate, medals.resistance_to_different_faction_rate,
            medals.damage_to_same_faction_rate, medals.resistance_to_same_faction_rate,
            medals.normal_damage_rate, medals.normal_resistance_rate,
            medals.skill_damage_rate, medals.skill_resistance_rate
        );
        return medals;
    }

    public List<Medals> GetUserMedals(string user_id, int pageSize, int offset)
    {
        List<Medals> list = _userMedalsRepository.GetUserMedals(user_id, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserMedalsCount(string user_id)
    {
        return _userMedalsRepository.GetUserMedalsCount(user_id);
    }

    public bool InsertUserMedals(Medals medals)
    {
        return _userMedalsRepository.InsertUserMedals(medals);
    }

    public bool UpdateMedalsLevel(Medals medals, int cardLevel)
    {
        return _userMedalsRepository.UpdateMedalsLevel(medals, cardLevel);
    }

    public bool UpdateMedalsBreakthrough(Medals medals, int star, int quantity)
    {
        return _userMedalsRepository.UpdateMedalsBreakthrough(medals, star, quantity);
    }

    public Medals GetUserMedalsById(string user_id, string Id)
    {
        return _userMedalsRepository.GetUserMedalsById(user_id, Id);
    }

    public Medals SumPowerUserMedals()
    {
        return _userMedalsRepository.SumPowerUserMedals();
    }
}
