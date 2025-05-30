using System.Collections.Generic;

public class UserTalismanService : IUserTalismanService
{
    private readonly IUserTalismanRepository _userTalismanRepository;

    public UserTalismanService(IUserTalismanRepository userTalismanRepository)
    {
        _userTalismanRepository = userTalismanRepository;
    }

    public static UserTalismanService Create()
    {
        return new UserTalismanService(new UserTalismanRepository());
    }

    public Talisman GetNewLevelPower(Talisman c, double coefficient)
    {
        ITalismanRepository _repository = new TalismanRepository();
        TalismanService _service = new TalismanService(_repository);
        Talisman orginCard = _service.GetTalismanById(c.id);
        Talisman talisman = new Talisman
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
        talisman.power = EvaluatePower.CalculatePower(
            talisman.health,
            talisman.physical_attack, talisman.physical_defense,
            talisman.magical_attack, talisman.magical_defense,
            talisman.chemical_attack, talisman.chemical_defense,
            talisman.atomic_attack, talisman.atomic_defense,
            talisman.mental_attack, talisman.mental_defense,
            talisman.speed,
            talisman.critical_damage_rate, talisman.critical_rate,
            talisman.penetration_rate, talisman.evasion_rate,
            talisman.damage_absorption_rate, talisman.vitality_regeneration_rate,
            talisman.accuracy_rate, talisman.lifesteal_rate,
            talisman.shield_strength, talisman.tenacity, talisman.resistance_rate,
            talisman.combo_rate, talisman.reflection_rate,
            talisman.mana, talisman.mana_regeneration_rate,
            talisman.damage_to_different_faction_rate, talisman.resistance_to_different_faction_rate,
            talisman.damage_to_same_faction_rate, talisman.resistance_to_same_faction_rate
        );
        return talisman;
    }
    public Talisman GetNewBreakthroughPower(Talisman c, double coefficient)
    {
        ITalismanRepository _repository = new TalismanRepository();
        TalismanService _service = new TalismanService(_repository);
        Talisman orginCard = _service.GetTalismanById(c.id);
        Talisman magicFormationCircle = new Talisman
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
        magicFormationCircle.power = EvaluatePower.CalculatePower(
            magicFormationCircle.health,
            magicFormationCircle.physical_attack, magicFormationCircle.physical_defense,
            magicFormationCircle.magical_attack, magicFormationCircle.magical_defense,
            magicFormationCircle.chemical_attack, magicFormationCircle.chemical_defense,
            magicFormationCircle.atomic_attack, magicFormationCircle.atomic_defense,
            magicFormationCircle.mental_attack, magicFormationCircle.mental_defense,
            magicFormationCircle.speed,
            magicFormationCircle.critical_damage_rate, magicFormationCircle.critical_rate,
            magicFormationCircle.penetration_rate, magicFormationCircle.evasion_rate,
            magicFormationCircle.damage_absorption_rate, magicFormationCircle.vitality_regeneration_rate,
            magicFormationCircle.accuracy_rate, magicFormationCircle.lifesteal_rate,
            magicFormationCircle.shield_strength, magicFormationCircle.tenacity, magicFormationCircle.resistance_rate,
            magicFormationCircle.combo_rate, magicFormationCircle.reflection_rate,
            magicFormationCircle.mana, magicFormationCircle.mana_regeneration_rate,
            magicFormationCircle.damage_to_different_faction_rate, magicFormationCircle.resistance_to_different_faction_rate,
            magicFormationCircle.damage_to_same_faction_rate, magicFormationCircle.resistance_to_same_faction_rate
        );
        return magicFormationCircle;
    }

    public List<Talisman> GetUserTalisman(string user_id, string type, int pageSize, int offset)
    {
        List<Talisman> list = _userTalismanRepository.GetUserTalisman(user_id, type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserTalismanCount(string user_id, string type)
    {
        return _userTalismanRepository.GetUserTalismanCount(user_id, type);
    }

    public bool InsertUserTalisman(Talisman talisman)
    {
        return _userTalismanRepository.InsertUserTalisman(talisman);
    }

    public bool UpdateTalismanLevel(Talisman talisman, int level)
    {
        return _userTalismanRepository.UpdateTalismanLevel(talisman, level);
    }

    public bool UpdateTalismanBreakthrough(Talisman talisman, int star, int quantity)
    {
        return _userTalismanRepository.UpdateTalismanBreakthrough(talisman, star, quantity);
    }

    public Talisman GetUserTalismanById(string user_id, string Id)
    {
        return _userTalismanRepository.GetUserTalismanById(user_id, Id);
    }

    public Talisman SumPowerUserTalisman()
    {
        return _userTalismanRepository.SumPowerUserTalisman();
    }
}
