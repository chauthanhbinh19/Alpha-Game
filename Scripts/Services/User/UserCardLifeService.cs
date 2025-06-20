using System.Collections.Generic;

public class UserCardLifeService : IUserCardLifeService
{
    private readonly IUserCardLifeRepository _userCardLifeRepository;

    public UserCardLifeService(IUserCardLifeRepository userCardLifeRepository)
    {
        _userCardLifeRepository = userCardLifeRepository;
    }

    public static UserCardLifeService Create()
    {
        return new UserCardLifeService(new UserCardLifeRepository());
    }

    public CardLife GetNewLevelPower(CardLife c, double coefficient)
    {
        ICardLifeRepository _repository = new CardLifeRepository();
        CardLifeService _service = new CardLifeService(_repository);
        CardLife orginCard = _service.GetCardLifeById(c.id);
        CardLife CardLife = new CardLife
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
        CardLife.power = EvaluatePower.CalculatePower(
            CardLife.health,
            CardLife.physical_attack, CardLife.physical_defense,
            CardLife.magical_attack, CardLife.magical_defense,
            CardLife.chemical_attack, CardLife.chemical_defense,
            CardLife.atomic_attack, CardLife.atomic_defense,
            CardLife.mental_attack, CardLife.mental_defense,
            CardLife.speed,
            CardLife.critical_damage_rate, CardLife.critical_rate, CardLife.critical_resistance_rate, CardLife.ignore_critical_rate,
            CardLife.penetration_rate, CardLife.penetration_resistance_rate, CardLife.evasion_rate,
            CardLife.damage_absorption_rate, CardLife.ignore_damage_absorption_rate, CardLife.absorbed_damage_rate,
            CardLife.vitality_regeneration_rate, CardLife.vitality_regeneration_resistance_rate,
            CardLife.accuracy_rate, CardLife.lifesteal_rate,
            CardLife.shield_strength, CardLife.tenacity, CardLife.resistance_rate,
            CardLife.combo_rate, CardLife.ignore_combo_rate, CardLife.combo_damage_rate, CardLife.combo_resistance_rate,
            CardLife.stun_rate, CardLife.ignore_stun_rate,
            CardLife.reflection_rate, CardLife.ignore_reflection_rate, CardLife.reflection_damage_rate, CardLife.reflection_resistance_rate,
            CardLife.mana, CardLife.mana_regeneration_rate,
            CardLife.damage_to_different_faction_rate, CardLife.resistance_to_different_faction_rate,
            CardLife.damage_to_same_faction_rate, CardLife.resistance_to_same_faction_rate,
            CardLife.normal_damage_rate, CardLife.normal_resistance_rate,
            CardLife.skill_damage_rate, CardLife.skill_resistance_rate
        );
        return CardLife;
    }
    public CardLife GetNewBreakthroughPower(CardLife c, double coefficient)
    {
        ICardLifeRepository _repository = new CardLifeRepository();
        CardLifeService _service = new CardLifeService(_repository);
        CardLife orginCard = _service.GetCardLifeById(c.id);
        CardLife CardLife = new CardLife
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
        CardLife.power = EvaluatePower.CalculatePower(
            CardLife.health,
            CardLife.physical_attack, CardLife.physical_defense,
            CardLife.magical_attack, CardLife.magical_defense,
            CardLife.chemical_attack, CardLife.chemical_defense,
            CardLife.atomic_attack, CardLife.atomic_defense,
            CardLife.mental_attack, CardLife.mental_defense,
            CardLife.speed,
            CardLife.critical_damage_rate, CardLife.critical_rate, CardLife.critical_resistance_rate, CardLife.ignore_critical_rate,
            CardLife.penetration_rate, CardLife.penetration_resistance_rate, CardLife.evasion_rate,
            CardLife.damage_absorption_rate, CardLife.ignore_damage_absorption_rate, CardLife.absorbed_damage_rate,
            CardLife.vitality_regeneration_rate, CardLife.vitality_regeneration_resistance_rate,
            CardLife.accuracy_rate, CardLife.lifesteal_rate,
            CardLife.shield_strength, CardLife.tenacity, CardLife.resistance_rate,
            CardLife.combo_rate, CardLife.ignore_combo_rate, CardLife.combo_damage_rate, CardLife.combo_resistance_rate,
            CardLife.stun_rate, CardLife.ignore_stun_rate,
            CardLife.reflection_rate, CardLife.ignore_reflection_rate, CardLife.reflection_damage_rate, CardLife.reflection_resistance_rate,
            CardLife.mana, CardLife.mana_regeneration_rate,
            CardLife.damage_to_different_faction_rate, CardLife.resistance_to_different_faction_rate,
            CardLife.damage_to_same_faction_rate, CardLife.resistance_to_same_faction_rate,
            CardLife.normal_damage_rate, CardLife.normal_resistance_rate,
            CardLife.skill_damage_rate, CardLife.skill_resistance_rate
        );
        return CardLife;
    }

    public List<CardLife> GetUserCardLife(string user_id, string type, int pageSize, int offset)
    {
        List<CardLife> list = _userCardLifeRepository.GetUserCardLife(user_id, type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserCardLifeCount(string user_id, string type)
    {
        return _userCardLifeRepository.GetUserCardLifeCount(user_id, type);
    }

    public bool InsertUserCardLife(CardLife cardLife)
    {
        return _userCardLifeRepository.InsertUserCardLife(cardLife);
    }

    public bool UpdateCardLifeLevel(CardLife cardLife, int cardLevel)
    {
        return _userCardLifeRepository.UpdateCardLifeLevel(cardLife, cardLevel);
    }

    public bool UpdateCardLifeBreakthrough(CardLife cardLife, int star, int quantity)
    {
        return _userCardLifeRepository.UpdateCardLifeBreakthrough(cardLife, star, quantity);
    }

    public CardLife GetUserCardLifeById(string user_id, string Id)
    {
        return _userCardLifeRepository.GetUserCardLifeById(user_id, Id);
    }

    public CardLife SumPowerUserCardLife()
    {
        return _userCardLifeRepository.SumPowerUserCardLife();
    }
}
