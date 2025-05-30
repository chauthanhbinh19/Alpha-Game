using System.Collections.Generic;

public class UserSymbolsService : IUserSymbolsService
{
    private readonly IUserSymbolsRepository _userSymbolsRepository;

    public UserSymbolsService(IUserSymbolsRepository userSymbolsRepository)
    {
        _userSymbolsRepository = userSymbolsRepository;
    }

    public static UserSymbolsService Create()
    {
        return new UserSymbolsService(new UserSymbolsRepository());
    }

    public Symbols GetNewLevelPower(Symbols c, double coefficient)
    {
        ISymbolsRepository _repository = new SymbolsRepository();
        SymbolsService _service = new SymbolsService(_repository);
        Symbols orginCard = _service.GetSymbolsById(c.id);
        Symbols symbols = new Symbols
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
        symbols.power = EvaluatePower.CalculatePower(
            symbols.health,
            symbols.physical_attack, symbols.physical_defense,
            symbols.magical_attack, symbols.magical_defense,
            symbols.chemical_attack, symbols.chemical_defense,
            symbols.atomic_attack, symbols.atomic_defense,
            symbols.mental_attack, symbols.mental_defense,
            symbols.speed,
            symbols.critical_damage_rate, symbols.critical_rate,
            symbols.penetration_rate, symbols.evasion_rate,
            symbols.damage_absorption_rate, symbols.vitality_regeneration_rate,
            symbols.accuracy_rate, symbols.lifesteal_rate,
            symbols.shield_strength, symbols.tenacity, symbols.resistance_rate,
            symbols.combo_rate, symbols.reflection_rate,
            symbols.mana, symbols.mana_regeneration_rate,
            symbols.damage_to_different_faction_rate, symbols.resistance_to_different_faction_rate,
            symbols.damage_to_same_faction_rate, symbols.resistance_to_same_faction_rate
        );
        return symbols;
    }
    public Symbols GetNewBreakthroughPower(Symbols c, double coefficient)
    {
        ISymbolsRepository _repository = new SymbolsRepository();
        SymbolsService _service = new SymbolsService(_repository);
        Symbols orginCard = _service.GetSymbolsById(c.id);
        Symbols symbols = new Symbols
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
        symbols.power = EvaluatePower.CalculatePower(
            symbols.health,
            symbols.physical_attack, symbols.physical_defense,
            symbols.magical_attack, symbols.magical_defense,
            symbols.chemical_attack, symbols.chemical_defense,
            symbols.atomic_attack, symbols.atomic_defense,
            symbols.mental_attack, symbols.mental_defense,
            symbols.speed,
            symbols.critical_damage_rate, symbols.critical_rate,
            symbols.penetration_rate, symbols.evasion_rate,
            symbols.damage_absorption_rate, symbols.vitality_regeneration_rate,
            symbols.accuracy_rate, symbols.lifesteal_rate,
            symbols.shield_strength, symbols.tenacity, symbols.resistance_rate,
            symbols.combo_rate, symbols.reflection_rate,
            symbols.mana, symbols.mana_regeneration_rate,
            symbols.damage_to_different_faction_rate, symbols.resistance_to_different_faction_rate,
            symbols.damage_to_same_faction_rate, symbols.resistance_to_same_faction_rate
        );
        return symbols;
    }

    public List<Symbols> GetUserSymbols(string user_id, string type, int pageSize, int offset)
    {
        List<Symbols> list = _userSymbolsRepository.GetUserSymbols(user_id, type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserSymbolsCount(string user_id, string type)
    {
        return _userSymbolsRepository.GetUserSymbolsCount(user_id, type);
    }

    public bool InsertUserSymbols(Symbols symbols)
    {
        return _userSymbolsRepository.InsertUserSymbols(symbols);
    }

    public bool UpdateSymbolsLevel(Symbols symbols, int cardLevel)
    {
        return _userSymbolsRepository.UpdateSymbolsLevel(symbols, cardLevel);
    }

    public bool UpdateSymbolsBreakthrough(Symbols symbols, int star, int quantity)
    {
        return _userSymbolsRepository.UpdateSymbolsBreakthrough(symbols, star, quantity);
    }

    public Symbols GetUserSymbolsById(string user_id, string Id)
    {
        return _userSymbolsRepository.GetUserSymbolsById(user_id, Id);
    }

    public Symbols SumPowerUserSymbols()
    {
        return _userSymbolsRepository.SumPowerUserSymbols();
    }
}
