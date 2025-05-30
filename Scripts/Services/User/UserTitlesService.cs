using System.Collections.Generic;

public class UserTitlesService : IUserTitlesService
{
    private readonly IUserTitlesRepository _userTitlesRepository;

    public UserTitlesService(IUserTitlesRepository userTitlesRepository)
    {
        _userTitlesRepository = userTitlesRepository;
    }

    public static UserTitlesService Create()
    {
        return new UserTitlesService(new UserTitlesRepository());
    }

    public Titles GetNewLevelPower(Titles c, double coefficient)
    {
        ITitlesRepository _repository = new TitlesRepository();
        TitlesService _service = new TitlesService(_repository);
        Titles orginCard = _service.GetTitlesById(c.id);
        Titles titles = new Titles
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
        titles.power = EvaluatePower.CalculatePower(
            titles.health,
            titles.physical_attack, titles.physical_defense,
            titles.magical_attack, titles.magical_defense,
            titles.chemical_attack, titles.chemical_defense,
            titles.atomic_attack, titles.atomic_defense,
            titles.mental_attack, titles.mental_defense,
            titles.speed,
            titles.critical_damage_rate, titles.critical_rate,
            titles.penetration_rate, titles.evasion_rate,
            titles.damage_absorption_rate, titles.vitality_regeneration_rate,
            titles.accuracy_rate, titles.lifesteal_rate,
            titles.shield_strength, titles.tenacity, titles.resistance_rate,
            titles.combo_rate, titles.reflection_rate,
            titles.mana, titles.mana_regeneration_rate,
            titles.damage_to_different_faction_rate, titles.resistance_to_different_faction_rate,
            titles.damage_to_same_faction_rate, titles.resistance_to_same_faction_rate
        );
        return titles;
    }
    public Titles GetNewBreakthroughPower(Titles c, double coefficient)
    {
        ITitlesRepository _repository = new TitlesRepository();
        TitlesService _service = new TitlesService(_repository);
        Titles orginCard = _service.GetTitlesById(c.id);
        Titles titles = new Titles
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
        titles.power = EvaluatePower.CalculatePower(
            titles.health,
            titles.physical_attack, titles.physical_defense,
            titles.magical_attack, titles.magical_defense,
            titles.chemical_attack, titles.chemical_defense,
            titles.atomic_attack, titles.atomic_defense,
            titles.mental_attack, titles.mental_defense,
            titles.speed,
            titles.critical_damage_rate, titles.critical_rate,
            titles.penetration_rate, titles.evasion_rate,
            titles.damage_absorption_rate, titles.vitality_regeneration_rate,
            titles.accuracy_rate, titles.lifesteal_rate,
            titles.shield_strength, titles.tenacity, titles.resistance_rate,
            titles.combo_rate, titles.reflection_rate,
            titles.mana, titles.mana_regeneration_rate,
            titles.damage_to_different_faction_rate, titles.resistance_to_different_faction_rate,
            titles.damage_to_same_faction_rate, titles.resistance_to_same_faction_rate
        );
        return titles;
    }

    public List<Titles> GetUserTitles(string user_id, int pageSize, int offset)
    {
        List<Titles> list = _userTitlesRepository.GetUserTitles(user_id, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserTitlesCount(string user_id)
    {
        return _userTitlesRepository.GetUserTitlesCount(user_id);
    }

    public bool InsertUserTitles(Titles titles)
    {
        return _userTitlesRepository.InsertUserTitles(titles);
    }

    public bool UpdateTitlesLevel(Titles titles, int cardLevel)
    {
        return _userTitlesRepository.UpdateTitlesLevel(titles, cardLevel);
    }

    public bool UpdateTitlesBreakthrough(Titles titles, int star, int quantity)
    {
        return _userTitlesRepository.UpdateTitlesBreakthrough(titles, star, quantity);
    }

    public Titles GetUserTitlesById(string user_id, string Id)
    {
        return _userTitlesRepository.GetUserTitlesById(user_id, Id);
    }

    public Titles SumPowerUserTitles()
    {
        return _userTitlesRepository.SumPowerUserTitles();
    }
}
