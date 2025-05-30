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
        relics.power = EvaluatePower.CalculatePower(
            relics.health,
            relics.physical_attack, relics.physical_defense,
            relics.magical_attack, relics.magical_defense,
            relics.chemical_attack, relics.chemical_defense,
            relics.atomic_attack, relics.atomic_defense,
            relics.mental_attack, relics.mental_defense,
            relics.speed,
            relics.critical_damage_rate, relics.critical_rate,
            relics.penetration_rate, relics.evasion_rate,
            relics.damage_absorption_rate, relics.vitality_regeneration_rate,
            relics.accuracy_rate, relics.lifesteal_rate,
            relics.shield_strength, relics.tenacity, relics.resistance_rate,
            relics.combo_rate, relics.reflection_rate,
            relics.mana, relics.mana_regeneration_rate,
            relics.damage_to_different_faction_rate, relics.resistance_to_different_faction_rate,
            relics.damage_to_same_faction_rate, relics.resistance_to_same_faction_rate
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
        relics.power = EvaluatePower.CalculatePower(
            relics.health,
            relics.physical_attack, relics.physical_defense,
            relics.magical_attack, relics.magical_defense,
            relics.chemical_attack, relics.chemical_defense,
            relics.atomic_attack, relics.atomic_defense,
            relics.mental_attack, relics.mental_defense,
            relics.speed,
            relics.critical_damage_rate, relics.critical_rate,
            relics.penetration_rate, relics.evasion_rate,
            relics.damage_absorption_rate, relics.vitality_regeneration_rate,
            relics.accuracy_rate, relics.lifesteal_rate,
            relics.shield_strength, relics.tenacity, relics.resistance_rate,
            relics.combo_rate, relics.reflection_rate,
            relics.mana, relics.mana_regeneration_rate,
            relics.damage_to_different_faction_rate, relics.resistance_to_different_faction_rate,
            relics.damage_to_same_faction_rate, relics.resistance_to_same_faction_rate
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
