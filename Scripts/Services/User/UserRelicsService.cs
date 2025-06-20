using System.Collections.Generic;

public class UserRelicsService : IUserRelicsService
{
    private readonly IUserRelicsRepository _userRelicsRepository;

    public UserRelicsService(IUserRelicsRepository userRelicsRepository)
    {
        _userRelicsRepository = userRelicsRepository;
    }

    public static UserRelicsService Create()
    {
        return new UserRelicsService(new UserRelicsRepository());
    }

    public Relics GetNewLevelPower(Relics c, double coefficient)
    {
        IRelicsRepository _repository = new RelicsRepository();
        RelicsService _service = new RelicsService(_repository);
        Relics orginCard = _service.GetRelicsById(c.id);
        Relics relics = new Relics
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
        relics.power = EvaluatePower.CalculatePower(
            relics.health,
            relics.physical_attack, relics.physical_defense,
            relics.magical_attack, relics.magical_defense,
            relics.chemical_attack, relics.chemical_defense,
            relics.atomic_attack, relics.atomic_defense,
            relics.mental_attack, relics.mental_defense,
            relics.speed,
            relics.critical_damage_rate, relics.critical_rate, relics.critical_resistance_rate, relics.ignore_critical_rate,
            relics.penetration_rate, relics.penetration_resistance_rate, relics.evasion_rate,
            relics.damage_absorption_rate, relics.ignore_damage_absorption_rate, relics.absorbed_damage_rate,
            relics.vitality_regeneration_rate, relics.vitality_regeneration_resistance_rate,
            relics.accuracy_rate, relics.lifesteal_rate,
            relics.shield_strength, relics.tenacity, relics.resistance_rate,
            relics.combo_rate, relics.ignore_combo_rate, relics.combo_damage_rate, relics.combo_resistance_rate,
            relics.stun_rate, relics.ignore_stun_rate,
            relics.reflection_rate, relics.ignore_reflection_rate, relics.reflection_damage_rate, relics.reflection_resistance_rate,
            relics.mana, relics.mana_regeneration_rate,
            relics.damage_to_different_faction_rate, relics.resistance_to_different_faction_rate,
            relics.damage_to_same_faction_rate, relics.resistance_to_same_faction_rate,
            relics.normal_damage_rate, relics.normal_resistance_rate,
            relics.skill_damage_rate, relics.skill_resistance_rate
        );
        return relics;
    }
    public Relics GetNewBreakthroughPower(Relics c, double coefficient)
    {
        IRelicsRepository _repository = new RelicsRepository();
        RelicsService _service = new RelicsService(_repository);
        Relics orginCard = _service.GetRelicsById(c.id);
        Relics relics = new Relics
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
        relics.power = EvaluatePower.CalculatePower(
            relics.health,
            relics.physical_attack, relics.physical_defense,
            relics.magical_attack, relics.magical_defense,
            relics.chemical_attack, relics.chemical_defense,
            relics.atomic_attack, relics.atomic_defense,
            relics.mental_attack, relics.mental_defense,
            relics.speed,
            relics.critical_damage_rate, relics.critical_rate, relics.critical_resistance_rate, relics.ignore_critical_rate,
            relics.penetration_rate, relics.penetration_resistance_rate, relics.evasion_rate,
            relics.damage_absorption_rate, relics.ignore_damage_absorption_rate, relics.absorbed_damage_rate,
            relics.vitality_regeneration_rate, relics.vitality_regeneration_resistance_rate,
            relics.accuracy_rate, relics.lifesteal_rate,
            relics.shield_strength, relics.tenacity, relics.resistance_rate,
            relics.combo_rate, relics.ignore_combo_rate, relics.combo_damage_rate, relics.combo_resistance_rate,
            relics.stun_rate, relics.ignore_stun_rate,
            relics.reflection_rate, relics.ignore_reflection_rate, relics.reflection_damage_rate, relics.reflection_resistance_rate,
            relics.mana, relics.mana_regeneration_rate,
            relics.damage_to_different_faction_rate, relics.resistance_to_different_faction_rate,
            relics.damage_to_same_faction_rate, relics.resistance_to_same_faction_rate,
            relics.normal_damage_rate, relics.normal_resistance_rate,
            relics.skill_damage_rate, relics.skill_resistance_rate
        );
        return relics;
    }

    public List<Relics> GetUserRelics(string user_id, string type, int pageSize, int offset)
    {
        List<Relics> list = _userRelicsRepository.GetUserRelics(user_id, type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserRelicsCount(string user_id, string type)
    {
        return _userRelicsRepository.GetUserRelicsCount(user_id, type);
    }

    public bool InsertUserReclis(Relics relics)
    {
        return _userRelicsRepository.InsertUserReclis(relics);
    }

    public bool UpdateRelicsLevel(Relics relics, int cardLevel)
    {
        return _userRelicsRepository.UpdateRelicsLevel(relics, cardLevel);
    }

    public bool UpdateRelicsBreakthrough(Relics relics, int star, int quantity)
    {
        return _userRelicsRepository.UpdateRelicsBreakthrough(relics, star, quantity);
    }

    public Relics GetUserRelicsById(string user_id, string Id)
    {
        return _userRelicsRepository.GetUserRelicsById(user_id, Id);
    }

    public Relics SumPowerUserRelics()
    {
        return _userRelicsRepository.SumPowerUserRelics();
    }
}
