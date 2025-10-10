using System.Collections.Generic;
using System.Linq;

public class UserCardSpellService : IUserCardSpellService
{
    private readonly IUserCardSpellRepository _userCardSpellRepository;

    public UserCardSpellService(IUserCardSpellRepository userCardSpellRepository)
    {
        _userCardSpellRepository = userCardSpellRepository;
    }
    
    public static UserCardSpellService Create()
    {
        return new UserCardSpellService(new UserCardSpellRepository());
    }

    public List<CardSpell> GetFinalPower(string user_id, List<CardSpell> CardSpellList)
    {
        IPowerManagerRepository powerManagerRepository = new PowerManagerRepository();
        PowerManagerService powerManagerService = new PowerManagerService(powerManagerRepository);
        PowerManager powerManager = powerManagerService.GetUserStats(user_id);
        foreach (var c in CardSpellList)
        {
            CardSpell card = _userCardSpellRepository.GetUserCardSpellById(user_id, c.id);
            c.health = c.health + powerManager.health + card.health * powerManager.percent_all_health / 100;
            c.physical_attack = c.physical_attack + powerManager.physical_attack + card.physical_attack * powerManager.percent_all_physical_attack / 100;
            c.physical_defense = c.physical_defense + powerManager.physical_defense + card.physical_defense * powerManager.percent_all_physical_defense / 100;
            c.magical_attack = c.magical_attack + powerManager.magical_attack + card.magical_attack * powerManager.percent_all_magical_attack / 100;
            c.magical_defense = c.magical_defense + powerManager.magical_defense + card.magical_defense * powerManager.percent_all_magical_defense / 100;
            c.chemical_attack = c.chemical_attack + powerManager.chemical_attack + card.chemical_attack * powerManager.percent_all_chemical_attack / 100;
            c.chemical_defense = c.chemical_defense + powerManager.chemical_defense + card.chemical_defense * powerManager.percent_all_chemical_defense / 100;
            c.atomic_attack = c.atomic_attack + powerManager.atomic_attack + card.atomic_attack * powerManager.percent_all_atomic_attack / 100;
            c.atomic_defense = c.atomic_defense + powerManager.atomic_defense + card.atomic_defense * powerManager.percent_all_atomic_defense / 100;
            c.mental_attack = c.mental_attack + powerManager.mental_attack + card.mental_attack * powerManager.percent_all_mental_attack / 100;
            c.mental_defense = c.mental_defense + powerManager.mental_defense + card.mental_defense * powerManager.percent_all_mental_defense / 100;
            c.speed = c.speed + powerManager.speed;
            c.critical_damage_rate = c.critical_damage_rate + powerManager.critical_damage_rate;
            c.critical_rate = c.critical_rate + powerManager.critical_rate;
            c.critical_resistance_rate = c.critical_resistance_rate + powerManager.critical_resistance_rate;
            c.ignore_critical_rate = c.ignore_critical_rate + powerManager.ignore_critical_rate;
            c.penetration_rate = c.penetration_rate + powerManager.penetration_rate;
            c.penetration_resistance_rate = c.penetration_resistance_rate + powerManager.penetration_resistance_rate;
            c.evasion_rate = c.evasion_rate + powerManager.evasion_rate;
            c.damage_absorption_rate = c.damage_absorption_rate + powerManager.damage_absorption_rate;
            c.ignore_damage_absorption_rate = c.ignore_damage_absorption_rate + powerManager.ignore_damage_absorption_rate;
            c.absorbed_damage_rate = c.absorbed_damage_rate + powerManager.absorbed_damage_rate;
            c.vitality_regeneration_rate = c.vitality_regeneration_rate + powerManager.vitality_regeneration_rate;
            c.vitality_regeneration_resistance_rate = c.vitality_regeneration_resistance_rate + powerManager.vitality_regeneration_resistance_rate;
            c.accuracy_rate = c.accuracy_rate + powerManager.accuracy_rate;
            c.lifesteal_rate = c.lifesteal_rate + powerManager.lifesteal_rate;
            c.shield_strength = c.shield_strength + powerManager.shield_strength;
            c.tenacity = c.tenacity + powerManager.tenacity;
            c.resistance_rate = c.resistance_rate + powerManager.resistance_rate;
            c.combo_rate = c.combo_rate + powerManager.combo_rate;
            c.ignore_combo_rate = c.ignore_combo_rate + powerManager.ignore_combo_rate;
            c.combo_damage_rate = c.combo_damage_rate + powerManager.combo_damage_rate;
            c.combo_resistance_rate = c.combo_resistance_rate + powerManager.combo_resistance_rate;
            c.stun_rate = c.stun_rate + powerManager.stun_rate;
            c.ignore_stun_rate = c.ignore_stun_rate + powerManager.ignore_stun_rate;
            c.reflection_rate = c.reflection_rate + powerManager.reflection_rate;
            c.ignore_reflection_rate  = c.ignore_reflection_rate + powerManager.ignore_reflection_rate;
            c.reflection_damage_rate = c.reflection_damage_rate + powerManager.reflection_damage_rate;
            c.reflection_resistance_rate = c.reflection_resistance_rate + powerManager.reflection_resistance_rate;
            c.mana = c.mana + powerManager.mana;
            c.mana_regeneration_rate = c.mana_regeneration_rate + powerManager.mana_regeneration_rate;
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate + powerManager.damage_to_different_faction_rate;
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + powerManager.resistance_to_different_faction_rate;
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate + powerManager.damage_to_same_faction_rate;
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + powerManager.resistance_to_same_faction_rate;
            c.normal_damage_rate = c.normal_damage_rate + powerManager.normal_damage_rate;
            c.normal_resistance_rate = c.normal_resistance_rate + powerManager.normal_resistance_rate;
            c.skill_damage_rate = c.skill_damage_rate + powerManager.skill_damage_rate;
            c.skill_resistance_rate = c.skill_resistance_rate + powerManager.skill_resistance_rate;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return CardSpellList;
    }
    public List<CardSpell> GetScienceFictionPower(string user_id, List<CardSpell> CardSpellList)
    {
        ScienceFiction scienceFiction = ScienceFictionService.Create().GetSumScienceFiction(user_id);
        foreach (var c in CardSpellList)
        {
            CardSpell card = _userCardSpellRepository.GetUserCardSpellById(user_id, c.id);
            c.health = c.health + scienceFiction.health + card.health * scienceFiction.percent_all_health / 100;
            c.physical_attack = c.physical_attack + scienceFiction.physical_attack + card.physical_attack * scienceFiction.percent_all_physical_attack / 100;
            c.physical_defense = c.physical_defense + scienceFiction.physical_defense + card.physical_defense * scienceFiction.percent_all_physical_defense / 100;
            c.magical_attack = c.magical_attack + scienceFiction.magical_attack + card.magical_attack * scienceFiction.percent_all_magical_attack / 100;
            c.magical_defense = c.magical_defense + scienceFiction.magical_defense + card.magical_defense * scienceFiction.percent_all_magical_defense / 100;
            c.chemical_attack = c.chemical_attack + scienceFiction.chemical_attack + card.chemical_attack * scienceFiction.percent_all_chemical_attack / 100;
            c.chemical_defense = c.chemical_defense + scienceFiction.chemical_defense + card.chemical_defense * scienceFiction.percent_all_chemical_defense / 100;
            c.atomic_attack = c.atomic_attack + scienceFiction.atomic_attack + card.atomic_attack * scienceFiction.percent_all_atomic_attack / 100;
            c.atomic_defense = c.atomic_defense + scienceFiction.atomic_defense + card.atomic_defense * scienceFiction.percent_all_atomic_defense / 100;
            c.mental_attack = c.mental_attack + scienceFiction.mental_attack + card.mental_attack * scienceFiction.percent_all_mental_attack / 100;
            c.mental_defense = c.mental_defense + scienceFiction.mental_defense + card.mental_defense * scienceFiction.percent_all_mental_defense / 100;
            c.speed = c.speed + scienceFiction.speed;
            c.critical_damage_rate = c.critical_damage_rate + scienceFiction.critical_damage_rate;
            c.critical_rate = c.critical_rate + scienceFiction.critical_rate;
            c.critical_resistance_rate = c.critical_resistance_rate + scienceFiction.critical_resistance_rate;
            c.ignore_critical_rate = c.ignore_critical_rate + scienceFiction.ignore_critical_rate;
            c.penetration_rate = c.penetration_rate + scienceFiction.penetration_rate;
            c.penetration_resistance_rate = c.penetration_resistance_rate + scienceFiction.penetration_resistance_rate;
            c.evasion_rate = c.evasion_rate + scienceFiction.evasion_rate;
            c.damage_absorption_rate = c.damage_absorption_rate + scienceFiction.damage_absorption_rate;
            c.ignore_damage_absorption_rate = c.ignore_damage_absorption_rate + scienceFiction.ignore_damage_absorption_rate;
            c.absorbed_damage_rate = c.absorbed_damage_rate + scienceFiction.absorbed_damage_rate;
            c.vitality_regeneration_rate = c.vitality_regeneration_rate + scienceFiction.vitality_regeneration_rate;
            c.vitality_regeneration_resistance_rate = c.vitality_regeneration_resistance_rate + scienceFiction.vitality_regeneration_resistance_rate;
            c.accuracy_rate = c.accuracy_rate + scienceFiction.accuracy_rate;
            c.lifesteal_rate = c.lifesteal_rate + scienceFiction.lifesteal_rate;
            c.shield_strength = c.shield_strength + scienceFiction.shield_strength;
            c.tenacity = c.tenacity + scienceFiction.tenacity;
            c.resistance_rate = c.resistance_rate + scienceFiction.resistance_rate;
            c.combo_rate = c.combo_rate + scienceFiction.combo_rate;
            c.ignore_combo_rate = c.ignore_combo_rate + scienceFiction.ignore_combo_rate;
            c.combo_damage_rate = c.combo_damage_rate + scienceFiction.combo_damage_rate;
            c.combo_resistance_rate = c.combo_resistance_rate + scienceFiction.combo_resistance_rate;
            c.stun_rate = c.stun_rate + scienceFiction.stun_rate;
            c.ignore_stun_rate = c.ignore_stun_rate + scienceFiction.ignore_stun_rate;
            c.reflection_rate = c.reflection_rate + scienceFiction.reflection_rate;
            c.ignore_reflection_rate  = c.ignore_reflection_rate + scienceFiction.ignore_reflection_rate;
            c.reflection_damage_rate = c.reflection_damage_rate + scienceFiction.reflection_damage_rate;
            c.reflection_resistance_rate = c.reflection_resistance_rate + scienceFiction.reflection_resistance_rate;
            c.mana = c.mana + scienceFiction.mana;
            c.mana_regeneration_rate = c.mana_regeneration_rate + scienceFiction.mana_regeneration_rate;
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate + scienceFiction.damage_to_different_faction_rate;
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + scienceFiction.resistance_to_different_faction_rate;
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate + scienceFiction.damage_to_same_faction_rate;
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + scienceFiction.resistance_to_same_faction_rate;
            c.normal_damage_rate = c.normal_damage_rate + scienceFiction.normal_damage_rate;
            c.normal_resistance_rate = c.normal_resistance_rate + scienceFiction.normal_resistance_rate;
            c.skill_damage_rate = c.skill_damage_rate + scienceFiction.skill_damage_rate;
            c.skill_resistance_rate = c.skill_resistance_rate + scienceFiction.skill_resistance_rate;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return CardSpellList;
    }
    public List<CardSpell> GetAllEquipmentPower(string user_id, List<CardSpell> CardSpellList)
    {
        Equipments equipments = new Equipments();
        IUserEquipmentsRepository userEquipmentsRepository = new UserEquipmentsRepository();
        UserEquipmentsService userEquipmentsService = new UserEquipmentsService(userEquipmentsRepository);
        foreach (var c in CardSpellList)
        {
            equipments = userEquipmentsService.GetAllEquipmentsByCardSpellId(user_id, c.id);
            c.health = c.health + equipments.health + equipments.special_health;
            c.physical_attack = c.physical_attack + equipments.physical_attack + equipments.special_physical_attack;
            c.physical_defense = c.physical_defense + equipments.physical_defense + equipments.special_physical_defense;
            c.magical_attack = c.magical_attack + equipments.magical_attack + equipments.special_magical_attack;
            c.magical_defense = c.magical_defense + equipments.magical_defense + equipments.special_magical_defense;
            c.chemical_attack = c.chemical_attack + equipments.chemical_attack + equipments.special_chemical_attack;
            c.chemical_defense = c.chemical_defense + equipments.chemical_defense + equipments.special_chemical_defense;
            c.atomic_attack = c.atomic_attack + equipments.atomic_attack + equipments.special_atomic_attack;
            c.atomic_defense = c.atomic_defense + equipments.atomic_defense + equipments.special_atomic_defense;
            c.mental_attack = c.mental_attack + equipments.mental_attack + equipments.special_mental_attack;
            c.mental_defense = c.mental_defense + equipments.mental_defense + equipments.special_mental_defense;
            c.speed = c.speed + equipments.speed;
            c.critical_damage_rate = c.critical_damage_rate + equipments.critical_damage_rate;
            c.critical_rate = c.critical_rate + equipments.critical_rate;
            c.critical_resistance_rate = c.critical_resistance_rate + equipments.critical_resistance_rate;
            c.ignore_critical_rate = c.ignore_critical_rate + equipments.ignore_critical_rate;
            c.penetration_rate = c.penetration_rate + equipments.penetration_rate;
            c.penetration_resistance_rate = c.penetration_resistance_rate + equipments.penetration_resistance_rate;
            c.evasion_rate = c.evasion_rate + equipments.evasion_rate;
            c.damage_absorption_rate = c.damage_absorption_rate + equipments.damage_absorption_rate;
            c.ignore_damage_absorption_rate = c.ignore_damage_absorption_rate + equipments.ignore_damage_absorption_rate;
            c.absorbed_damage_rate = c.absorbed_damage_rate + equipments.absorbed_damage_rate;
            c.vitality_regeneration_rate = c.vitality_regeneration_rate + equipments.vitality_regeneration_rate;
            c.vitality_regeneration_resistance_rate = c.vitality_regeneration_resistance_rate + equipments.vitality_regeneration_resistance_rate;
            c.accuracy_rate = c.accuracy_rate + equipments.accuracy_rate;
            c.lifesteal_rate = c.lifesteal_rate + equipments.lifesteal_rate;
            c.shield_strength = c.shield_strength + equipments.shield_strength;
            c.tenacity = c.tenacity + equipments.tenacity;
            c.resistance_rate = c.resistance_rate + equipments.resistance_rate;
            c.combo_rate = c.combo_rate + equipments.combo_rate;
            c.ignore_combo_rate = c.ignore_combo_rate + equipments.ignore_combo_rate;
            c.combo_damage_rate = c.combo_damage_rate + equipments.combo_damage_rate;
            c.combo_resistance_rate = c.combo_resistance_rate + equipments.combo_resistance_rate;
            c.stun_rate = c.stun_rate + equipments.stun_rate;
            c.ignore_stun_rate = c.ignore_stun_rate + equipments.ignore_stun_rate;
            c.reflection_rate = c.reflection_rate + equipments.reflection_rate;
            c.ignore_reflection_rate = c.ignore_reflection_rate + equipments.ignore_reflection_rate;
            c.reflection_damage_rate = c.reflection_damage_rate + equipments.reflection_damage_rate;
            c.reflection_resistance_rate = c.reflection_resistance_rate + equipments.reflection_resistance_rate;
            c.mana = c.mana + equipments.mana;
            c.mana_regeneration_rate = c.mana_regeneration_rate + equipments.mana_regeneration_rate;
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate + equipments.damage_to_different_faction_rate;
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + equipments.resistance_to_different_faction_rate;
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate + equipments.damage_to_same_faction_rate;
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + equipments.resistance_to_same_faction_rate;
            c.normal_damage_rate = c.normal_damage_rate + equipments.normal_damage_rate;
            c.normal_resistance_rate = c.normal_resistance_rate + equipments.normal_resistance_rate;
            c.skill_damage_rate = c.skill_damage_rate + equipments.skill_damage_rate;
            c.skill_resistance_rate = c.skill_resistance_rate + equipments.skill_resistance_rate;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return CardSpellList;
    }
    public List<CardSpell> GetAllRankPower(string user_id, List<CardSpell> CardSpellList)
    {
        IUserCardSpellRankRepository userCardSpellRankRepository = new UserCardSpellRankRepository();
        UserCardSpellRankService userCardSpellRankService = new UserCardSpellRankService(userCardSpellRankRepository);
        foreach (var c in CardSpellList)
        {
            CardSpell card = _userCardSpellRepository.GetUserCardSpellById(user_id, c.id);
            Rank rank = userCardSpellRankService.GetSumCardSpellRank(user_id, c.id);
            c.health = c.health + rank.health + card.health * rank.percent_all_health / 100;
            c.physical_attack = c.physical_attack + rank.physical_attack + card.physical_attack * rank.percent_all_physical_attack / 100;
            c.physical_defense = c.physical_defense + rank.physical_defense + card.physical_defense * rank.percent_all_physical_defense / 100;
            c.magical_attack = c.magical_attack + rank.magical_attack + card.magical_attack * rank.percent_all_magical_attack / 100;
            c.magical_defense = c.magical_defense + rank.magical_defense + card.magical_defense * rank.percent_all_magical_defense / 100;
            c.chemical_attack = c.chemical_attack + rank.chemical_attack + card.chemical_attack * rank.percent_all_chemical_attack / 100;
            c.chemical_defense = c.chemical_defense + rank.chemical_defense + card.chemical_defense * rank.percent_all_chemical_defense / 100;
            c.atomic_attack = c.atomic_attack + rank.atomic_attack + card.atomic_attack * rank.percent_all_atomic_attack / 100;
            c.atomic_defense = c.atomic_defense + rank.atomic_defense + card.atomic_defense * rank.percent_all_atomic_defense / 100;
            c.mental_attack = c.mental_attack + rank.mental_attack + card.mental_attack * rank.percent_all_mental_attack / 100;
            c.mental_defense = c.mental_defense + rank.mental_defense + card.mental_defense * rank.percent_all_mental_defense / 100;
            c.speed = c.speed + rank.speed;
            c.critical_damage_rate = c.critical_damage_rate + rank.critical_damage_rate;
            c.critical_rate = c.critical_rate + rank.critical_rate;
            c.critical_resistance_rate = c.critical_resistance_rate + rank.critical_resistance_rate;
            c.ignore_critical_rate = c.ignore_critical_rate + rank.ignore_critical_rate;
            c.penetration_rate = c.penetration_rate + rank.penetration_rate;
            c.penetration_resistance_rate = c.penetration_resistance_rate + rank.penetration_resistance_rate;
            c.evasion_rate = c.evasion_rate + rank.evasion_rate;
            c.damage_absorption_rate = c.damage_absorption_rate + rank.damage_absorption_rate;
            c.ignore_damage_absorption_rate = c.ignore_damage_absorption_rate + rank.ignore_damage_absorption_rate;
            c.absorbed_damage_rate = c.absorbed_damage_rate + rank.absorbed_damage_rate;
            c.vitality_regeneration_rate = c.vitality_regeneration_rate + rank.vitality_regeneration_rate;
            c.vitality_regeneration_resistance_rate = c.vitality_regeneration_resistance_rate + rank.vitality_regeneration_resistance_rate;
            c.accuracy_rate = c.accuracy_rate + rank.accuracy_rate;
            c.lifesteal_rate = c.lifesteal_rate + rank.lifesteal_rate;
            c.shield_strength = c.shield_strength + rank.shield_strength;
            c.tenacity = c.tenacity + rank.tenacity;
            c.resistance_rate = c.resistance_rate + rank.resistance_rate;
            c.combo_rate = c.combo_rate + rank.combo_rate;
            c.ignore_combo_rate = c.ignore_combo_rate + rank.ignore_combo_rate;
            c.combo_damage_rate = c.combo_damage_rate + rank.combo_damage_rate;
            c.combo_resistance_rate = c.combo_resistance_rate + rank.combo_resistance_rate;
            c.stun_rate = c.stun_rate + rank.stun_rate;
            c.ignore_stun_rate = c.ignore_stun_rate + rank.ignore_stun_rate;
            c.reflection_rate = c.reflection_rate + rank.reflection_rate;
            c.ignore_reflection_rate  = c.ignore_reflection_rate + rank.ignore_reflection_rate;
            c.reflection_damage_rate = c.reflection_damage_rate + rank.reflection_damage_rate;
            c.reflection_resistance_rate = c.reflection_resistance_rate + rank.reflection_resistance_rate;
            c.mana = c.mana + rank.mana;
            c.mana_regeneration_rate = c.mana_regeneration_rate + rank.mana_regeneration_rate;
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate + rank.damage_to_different_faction_rate;
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + rank.resistance_to_different_faction_rate;
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate + rank.damage_to_same_faction_rate;
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + rank.resistance_to_same_faction_rate;
            c.normal_damage_rate = c.normal_damage_rate + rank.normal_damage_rate;
            c.normal_resistance_rate = c.normal_resistance_rate + rank.normal_resistance_rate;
            c.skill_damage_rate = c.skill_damage_rate + rank.skill_damage_rate;
            c.skill_resistance_rate = c.skill_resistance_rate + rank.skill_resistance_rate;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return CardSpellList;
    }
    public List<CardSpell> GetAllMasterPower(string user_id, List<CardSpell> CardSpellList)
    {
        IUserCardSpellMasterRepository userCardSpellMasterRepository = new UserCardSpellMasterRepository();
        UserCardSpellMasterService userCardSpellMasterService = new UserCardSpellMasterService(userCardSpellMasterRepository);
        foreach (var c in CardSpellList)
        {
            CardSpell card = _userCardSpellRepository.GetUserCardSpellById(user_id, c.id);
            Master master = userCardSpellMasterService.GetSumCardSpellMaster(user_id, c.id);
            c.health = c.health + master.health + card.health * master.percent_all_health / 100;
            c.physical_attack = c.physical_attack + master.physical_attack + card.physical_attack * master.percent_all_physical_attack / 100;
            c.physical_defense = c.physical_defense + master.physical_defense + card.physical_defense * master.percent_all_physical_defense / 100;
            c.magical_attack = c.magical_attack + master.magical_attack + card.magical_attack * master.percent_all_magical_attack / 100;
            c.magical_defense = c.magical_defense + master.magical_defense + card.magical_defense * master.percent_all_magical_defense / 100;
            c.chemical_attack = c.chemical_attack + master.chemical_attack + card.chemical_attack * master.percent_all_chemical_attack / 100;
            c.chemical_defense = c.chemical_defense + master.chemical_defense + card.chemical_defense * master.percent_all_chemical_defense / 100;
            c.atomic_attack = c.atomic_attack + master.atomic_attack + card.atomic_attack * master.percent_all_atomic_attack / 100;
            c.atomic_defense = c.atomic_defense + master.atomic_defense + card.atomic_defense * master.percent_all_atomic_defense / 100;
            c.mental_attack = c.mental_attack + master.mental_attack + card.mental_attack * master.percent_all_mental_attack / 100;
            c.mental_defense = c.mental_defense + master.mental_defense + card.mental_defense * master.percent_all_mental_defense / 100;
            c.speed = c.speed + master.speed;
            c.critical_damage_rate = c.critical_damage_rate + master.critical_damage_rate;
            c.critical_rate = c.critical_rate + master.critical_rate;
            c.critical_resistance_rate = c.critical_resistance_rate + master.critical_resistance_rate;
            c.ignore_critical_rate = c.ignore_critical_rate + master.ignore_critical_rate;
            c.penetration_rate = c.penetration_rate + master.penetration_rate;
            c.penetration_resistance_rate = c.penetration_resistance_rate + master.penetration_resistance_rate;
            c.evasion_rate = c.evasion_rate + master.evasion_rate;
            c.damage_absorption_rate = c.damage_absorption_rate + master.damage_absorption_rate;
            c.ignore_damage_absorption_rate = c.ignore_damage_absorption_rate + master.ignore_damage_absorption_rate;
            c.absorbed_damage_rate = c.absorbed_damage_rate + master.absorbed_damage_rate;
            c.vitality_regeneration_rate = c.vitality_regeneration_rate + master.vitality_regeneration_rate;
            c.vitality_regeneration_resistance_rate = c.vitality_regeneration_resistance_rate + master.vitality_regeneration_resistance_rate;
            c.accuracy_rate = c.accuracy_rate + master.accuracy_rate;
            c.lifesteal_rate = c.lifesteal_rate + master.lifesteal_rate;
            c.shield_strength = c.shield_strength + master.shield_strength;
            c.tenacity = c.tenacity + master.tenacity;
            c.resistance_rate = c.resistance_rate + master.resistance_rate;
            c.combo_rate = c.combo_rate + master.combo_rate;
            c.ignore_combo_rate = c.ignore_combo_rate + master.ignore_combo_rate;
            c.combo_damage_rate = c.combo_damage_rate + master.combo_damage_rate;
            c.combo_resistance_rate = c.combo_resistance_rate + master.combo_resistance_rate;
            c.stun_rate = c.stun_rate + master.stun_rate;
            c.ignore_stun_rate = c.ignore_stun_rate + master.ignore_stun_rate;
            c.reflection_rate = c.reflection_rate + master.reflection_rate;
            c.ignore_reflection_rate  = c.ignore_reflection_rate + master.ignore_reflection_rate;
            c.reflection_damage_rate = c.reflection_damage_rate + master.reflection_damage_rate;
            c.reflection_resistance_rate = c.reflection_resistance_rate + master.reflection_resistance_rate;
            c.mana = c.mana + master.mana;
            c.mana_regeneration_rate = c.mana_regeneration_rate + master.mana_regeneration_rate;
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate + master.damage_to_different_faction_rate;
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + master.resistance_to_different_faction_rate;
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate + master.damage_to_same_faction_rate;
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + master.resistance_to_same_faction_rate;
            c.normal_damage_rate = c.normal_damage_rate + master.normal_damage_rate;
            c.normal_resistance_rate = c.normal_resistance_rate + master.normal_resistance_rate;
            c.skill_damage_rate = c.skill_damage_rate + master.skill_damage_rate;
            c.skill_resistance_rate = c.skill_resistance_rate + master.skill_resistance_rate;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return CardSpellList;
    }
    public List<CardSpell> GetAllAnimeStatsPower(string user_id, List<CardSpell> CardSpellList)
    {
        IAnimeStatsRepository animeStatsRepository = new AnimeStatsRepository();
        AnimeStatsService animeStatsService = new AnimeStatsService(animeStatsRepository);
        foreach (var c in CardSpellList)
        {
            CardSpell card = _userCardSpellRepository.GetUserCardSpellById(user_id, c.id);
            AnimeStats animeStats = animeStatsService.GetSumAnimeStats(user_id);
            c.health = c.health + animeStats.health + card.health * animeStats.percent_all_health / 100;
            c.physical_attack = c.physical_attack + animeStats.physical_attack + card.physical_attack * animeStats.percent_all_physical_attack / 100;
            c.physical_defense = c.physical_defense + animeStats.physical_defense + card.physical_defense * animeStats.percent_all_physical_defense / 100;
            c.magical_attack = c.magical_attack + animeStats.magical_attack + card.magical_attack * animeStats.percent_all_magical_attack / 100;
            c.magical_defense = c.magical_defense + animeStats.magical_defense + card.magical_defense * animeStats.percent_all_magical_defense / 100;
            c.chemical_attack = c.chemical_attack + animeStats.chemical_attack + card.chemical_attack * animeStats.percent_all_chemical_attack / 100;
            c.chemical_defense = c.chemical_defense + animeStats.chemical_defense + card.chemical_defense * animeStats.percent_all_chemical_defense / 100;
            c.atomic_attack = c.atomic_attack + animeStats.atomic_attack + card.atomic_attack * animeStats.percent_all_atomic_attack / 100;
            c.atomic_defense = c.atomic_defense + animeStats.atomic_defense + card.atomic_defense * animeStats.percent_all_atomic_defense / 100;
            c.mental_attack = c.mental_attack + animeStats.mental_attack + card.mental_attack * animeStats.percent_all_mental_attack / 100;
            c.mental_defense = c.mental_defense + animeStats.mental_defense + card.mental_defense * animeStats.percent_all_mental_defense / 100;
            c.speed = c.speed + animeStats.speed;
            c.critical_damage_rate = c.critical_damage_rate + animeStats.critical_damage_rate;
            c.critical_rate = c.critical_rate + animeStats.critical_rate;
            c.critical_resistance_rate = c.critical_resistance_rate + animeStats.critical_resistance_rate;
            c.ignore_critical_rate = c.ignore_critical_rate + animeStats.ignore_critical_rate;
            c.penetration_rate = c.penetration_rate + animeStats.penetration_rate;
            c.penetration_resistance_rate = c.penetration_resistance_rate + animeStats.penetration_resistance_rate;
            c.evasion_rate = c.evasion_rate + animeStats.evasion_rate;
            c.damage_absorption_rate = c.damage_absorption_rate + animeStats.damage_absorption_rate;
            c.ignore_damage_absorption_rate = c.ignore_damage_absorption_rate + animeStats.ignore_damage_absorption_rate;
            c.absorbed_damage_rate = c.absorbed_damage_rate + animeStats.absorbed_damage_rate;
            c.vitality_regeneration_rate = c.vitality_regeneration_rate + animeStats.vitality_regeneration_rate;
            c.vitality_regeneration_resistance_rate = c.vitality_regeneration_resistance_rate + animeStats.vitality_regeneration_resistance_rate;
            c.accuracy_rate = c.accuracy_rate + animeStats.accuracy_rate;
            c.lifesteal_rate = c.lifesteal_rate + animeStats.lifesteal_rate;
            c.shield_strength = c.shield_strength + animeStats.shield_strength;
            c.tenacity = c.tenacity + animeStats.tenacity;
            c.resistance_rate = c.resistance_rate + animeStats.resistance_rate;
            c.combo_rate = c.combo_rate + animeStats.combo_rate;
            c.ignore_combo_rate = c.ignore_combo_rate + animeStats.ignore_combo_rate;
            c.combo_damage_rate = c.combo_damage_rate + animeStats.combo_damage_rate;
            c.combo_resistance_rate = c.combo_resistance_rate + animeStats.combo_resistance_rate;
            c.stun_rate = c.stun_rate + animeStats.stun_rate;
            c.ignore_stun_rate = c.ignore_stun_rate + animeStats.ignore_stun_rate;
            c.reflection_rate = c.reflection_rate + animeStats.reflection_rate;
            c.ignore_reflection_rate = c.ignore_reflection_rate + animeStats.ignore_reflection_rate;
            c.reflection_damage_rate = c.reflection_damage_rate + animeStats.reflection_damage_rate;
            c.reflection_resistance_rate = c.reflection_resistance_rate + animeStats.reflection_resistance_rate;
            c.mana = c.mana + animeStats.mana;
            c.mana_regeneration_rate = c.mana_regeneration_rate + animeStats.mana_regeneration_rate;
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate + animeStats.damage_to_different_faction_rate;
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + animeStats.resistance_to_different_faction_rate;
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate + animeStats.damage_to_same_faction_rate;
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + animeStats.resistance_to_same_faction_rate;
            c.normal_damage_rate = c.normal_damage_rate + animeStats.normal_damage_rate;
            c.normal_resistance_rate = c.normal_resistance_rate + animeStats.normal_resistance_rate;
            c.skill_damage_rate = c.skill_damage_rate + animeStats.skill_damage_rate;
            c.skill_resistance_rate = c.skill_resistance_rate + animeStats.skill_resistance_rate;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return CardSpellList;
    }
    public List<CardSpell> GetAllSpiritBeastPower(string user_id, List<CardSpell> cardSpells)
    {
        IUserSpiritBeastRepository userSpiritBeastRepository = new UserSpiritBeastRepository();
        UserSpiritBeastService userSpiritBeastService = new UserSpiritBeastService(userSpiritBeastRepository);
        foreach (var c in cardSpells)
        {
            CardSpell card = _userCardSpellRepository.GetUserCardSpellById(user_id, c.id);
            SpiritBeast spiritBeast = userSpiritBeastService.GetUserCardSpellSpiritBeast(user_id, c);
            if (spiritBeast != null)
            {
                c.health = c.health + spiritBeast.health + card.health * spiritBeast.percent_all_health / 100;
                c.physical_attack = c.physical_attack + spiritBeast.physical_attack + card.physical_attack * spiritBeast.percent_all_physical_attack / 100;
                c.physical_defense = c.physical_defense + spiritBeast.physical_defense + card.physical_defense * spiritBeast.percent_all_physical_defense / 100;
                c.magical_attack = c.magical_attack + spiritBeast.magical_attack + card.magical_attack * spiritBeast.percent_all_magical_attack / 100;
                c.magical_defense = c.magical_defense + spiritBeast.magical_defense + card.magical_defense * spiritBeast.percent_all_magical_defense / 100;
                c.chemical_attack = c.chemical_attack + spiritBeast.chemical_attack + card.chemical_attack * spiritBeast.percent_all_chemical_attack / 100;
                c.chemical_defense = c.chemical_defense + spiritBeast.chemical_defense + card.chemical_defense * spiritBeast.percent_all_chemical_defense / 100;
                c.atomic_attack = c.atomic_attack + spiritBeast.atomic_attack + card.atomic_attack * spiritBeast.percent_all_atomic_attack / 100;
                c.atomic_defense = c.atomic_defense + spiritBeast.atomic_defense + card.atomic_defense * spiritBeast.percent_all_atomic_defense / 100;
                c.mental_attack = c.mental_attack + spiritBeast.mental_attack + card.mental_attack * spiritBeast.percent_all_mental_attack / 100;
                c.mental_defense = c.mental_defense + spiritBeast.mental_defense + card.mental_defense * spiritBeast.percent_all_mental_defense / 100;
                c.speed = c.speed + spiritBeast.speed;
                c.critical_damage_rate = c.critical_damage_rate + spiritBeast.critical_damage_rate;
                c.critical_rate = c.critical_rate + spiritBeast.critical_rate;
                c.critical_resistance_rate = c.critical_resistance_rate + spiritBeast.critical_resistance_rate;
                c.ignore_critical_rate = c.ignore_critical_rate + spiritBeast.ignore_critical_rate;
                c.penetration_rate = c.penetration_rate + spiritBeast.penetration_rate;
                c.penetration_resistance_rate = c.penetration_resistance_rate + spiritBeast.penetration_resistance_rate;
                c.evasion_rate = c.evasion_rate + spiritBeast.evasion_rate;
                c.damage_absorption_rate = c.damage_absorption_rate + spiritBeast.damage_absorption_rate;
                c.ignore_damage_absorption_rate = c.ignore_damage_absorption_rate + spiritBeast.ignore_damage_absorption_rate;
                c.absorbed_damage_rate = c.absorbed_damage_rate + spiritBeast.absorbed_damage_rate;
                c.vitality_regeneration_rate = c.vitality_regeneration_rate + spiritBeast.vitality_regeneration_rate;
                c.vitality_regeneration_resistance_rate = c.vitality_regeneration_resistance_rate + spiritBeast.vitality_regeneration_resistance_rate;
                c.accuracy_rate = c.accuracy_rate + spiritBeast.accuracy_rate;
                c.lifesteal_rate = c.lifesteal_rate + spiritBeast.lifesteal_rate;
                c.shield_strength = c.shield_strength + spiritBeast.shield_strength;
                c.tenacity = c.tenacity + spiritBeast.tenacity;
                c.resistance_rate = c.resistance_rate + spiritBeast.resistance_rate;
                c.combo_rate = c.combo_rate + spiritBeast.combo_rate;
                c.ignore_combo_rate = c.ignore_combo_rate + spiritBeast.ignore_combo_rate;
                c.combo_damage_rate = c.combo_damage_rate + spiritBeast.combo_damage_rate;
                c.combo_resistance_rate = c.combo_resistance_rate + spiritBeast.combo_resistance_rate;
                c.stun_rate = c.stun_rate + spiritBeast.stun_rate;
                c.ignore_stun_rate = c.ignore_stun_rate + spiritBeast.ignore_stun_rate;
                c.reflection_rate = c.reflection_rate + spiritBeast.reflection_rate;
                c.ignore_reflection_rate = c.ignore_reflection_rate + spiritBeast.ignore_reflection_rate;
                c.reflection_damage_rate = c.reflection_damage_rate + spiritBeast.reflection_damage_rate;
                c.reflection_resistance_rate = c.reflection_resistance_rate + spiritBeast.reflection_resistance_rate;
                c.mana = c.mana + spiritBeast.mana;
                c.mana_regeneration_rate = c.mana_regeneration_rate + spiritBeast.mana_regeneration_rate;
                c.damage_to_different_faction_rate = c.damage_to_different_faction_rate + spiritBeast.damage_to_different_faction_rate;
                c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + spiritBeast.resistance_to_different_faction_rate;
                c.damage_to_same_faction_rate = c.damage_to_same_faction_rate + spiritBeast.damage_to_same_faction_rate;
                c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + spiritBeast.resistance_to_same_faction_rate;
                c.normal_damage_rate = c.normal_damage_rate + spiritBeast.normal_damage_rate;
                c.normal_resistance_rate = c.normal_resistance_rate + spiritBeast.normal_resistance_rate;
                c.skill_damage_rate = c.skill_damage_rate + spiritBeast.skill_damage_rate;
                c.skill_resistance_rate = c.skill_resistance_rate + spiritBeast.skill_resistance_rate;
            }

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
            c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
            c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
            c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
            c.stun_rate, c.ignore_stun_rate,
            c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
            c.normal_damage_rate, c.normal_resistance_rate,
            c.skill_damage_rate, c.skill_resistance_rate
        );
        }
        return cardSpells;
    }
    public CardSpell GetNewLevelPower(CardSpell c, double coefficient)
    {
        ICardSpellRepository _repository = new CardSpellRepository();
        CardSpellService _service = new CardSpellService(_repository);
        CardSpell orginCard = _service.GetCardSpellById(c.id);
        CardSpell cardSpell = new CardSpell
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
            ignore_reflection_rate = c.ignore_reflection_rate + orginCard.ignore_reflection_rate * coefficient,
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
        cardSpell.power = EvaluatePower.CalculatePower(
            cardSpell.health,
            cardSpell.physical_attack, cardSpell.physical_defense,
            cardSpell.magical_attack, cardSpell.magical_defense,
            cardSpell.chemical_attack, cardSpell.chemical_defense,
            cardSpell.atomic_attack, cardSpell.atomic_defense,
            cardSpell.mental_attack, cardSpell.mental_defense,
            cardSpell.speed,
            cardSpell.critical_damage_rate, cardSpell.critical_rate, cardSpell.critical_resistance_rate, cardSpell.ignore_critical_rate,
            cardSpell.penetration_rate, cardSpell.penetration_resistance_rate, cardSpell.evasion_rate,
            cardSpell.damage_absorption_rate, cardSpell.ignore_damage_absorption_rate, cardSpell.absorbed_damage_rate,
            cardSpell.vitality_regeneration_rate, cardSpell.vitality_regeneration_resistance_rate,
            cardSpell.accuracy_rate, cardSpell.lifesteal_rate,
            cardSpell.shield_strength, cardSpell.tenacity, cardSpell.resistance_rate,
            cardSpell.combo_rate, cardSpell.ignore_combo_rate, cardSpell.combo_damage_rate, cardSpell.combo_resistance_rate,
            cardSpell.stun_rate, cardSpell.ignore_stun_rate,
            cardSpell.reflection_rate, cardSpell.ignore_reflection_rate, cardSpell.reflection_damage_rate, cardSpell.reflection_resistance_rate,
            cardSpell.mana, cardSpell.mana_regeneration_rate,
            cardSpell.damage_to_different_faction_rate, cardSpell.resistance_to_different_faction_rate,
            cardSpell.damage_to_same_faction_rate, cardSpell.resistance_to_same_faction_rate,
            cardSpell.normal_damage_rate, cardSpell.normal_resistance_rate,
            cardSpell.skill_damage_rate, cardSpell.skill_resistance_rate
        );
        return cardSpell;
    }
    public CardSpell GetNewBreakthroughPower(CardSpell c, double coefficient)
    {
        ICardSpellRepository _repository = new CardSpellRepository();
        CardSpellService _service = new CardSpellService(_repository);
        CardSpell orginCard = _service.GetCardSpellById(c.id);
        CardSpell cardSpell = new CardSpell
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
        cardSpell.power = EvaluatePower.CalculatePower(
            cardSpell.health,
            cardSpell.physical_attack, cardSpell.physical_defense,
            cardSpell.magical_attack, cardSpell.magical_defense,
            cardSpell.chemical_attack, cardSpell.chemical_defense,
            cardSpell.atomic_attack, cardSpell.atomic_defense,
            cardSpell.mental_attack, cardSpell.mental_defense,
            cardSpell.speed,
            cardSpell.critical_damage_rate, cardSpell.critical_rate, cardSpell.critical_resistance_rate, cardSpell.ignore_critical_rate,
            cardSpell.penetration_rate, cardSpell.penetration_resistance_rate, cardSpell.evasion_rate,
            cardSpell.damage_absorption_rate, cardSpell.ignore_damage_absorption_rate, cardSpell.absorbed_damage_rate,
            cardSpell.vitality_regeneration_rate, cardSpell.vitality_regeneration_resistance_rate,
            cardSpell.accuracy_rate, cardSpell.lifesteal_rate,
            cardSpell.shield_strength, cardSpell.tenacity, cardSpell.resistance_rate,
            cardSpell.combo_rate, cardSpell.ignore_combo_rate, cardSpell.combo_damage_rate, cardSpell.combo_resistance_rate,
            cardSpell.stun_rate, cardSpell.ignore_stun_rate,
            cardSpell.reflection_rate, cardSpell.ignore_reflection_rate, cardSpell.reflection_damage_rate, cardSpell.reflection_resistance_rate,
            cardSpell.mana, cardSpell.mana_regeneration_rate,
            cardSpell.damage_to_different_faction_rate, cardSpell.resistance_to_different_faction_rate,
            cardSpell.damage_to_same_faction_rate, cardSpell.resistance_to_same_faction_rate,
            cardSpell.normal_damage_rate, cardSpell.normal_resistance_rate,
            cardSpell.skill_damage_rate, cardSpell.skill_resistance_rate
        );
        return cardSpell;
    }

    public List<CardSpell> GetUserCardSpell(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<CardSpell> list = _userCardSpellRepository.GetUserCardSpell(user_id, type, pageSize, offset, rare);
        list = GetAllSpiritBeastPower(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllMasterPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        list = GetScienceFictionPower(user_id, list);
        return list;
    }

    public List<CardSpell> GetUserCardSpellTeam(string user_id, string teamId, string position)
    {
        List<CardSpell> list = _userCardSpellRepository.GetUserCardSpellTeam(user_id, teamId, position);
        list = GetAllSpiritBeastPower(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllMasterPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        list = GetScienceFictionPower(user_id, list);
        return list;
    }

    public Dictionary<string, int> GetUniqueCardSpellTypesTeam(string teamId)
    {
        return _userCardSpellRepository.GetUniqueCardSpellTypesTeam(teamId);
    }

    public bool UpdateTeamCardSpell(string team_id, string position, string card_id)
    {
        return _userCardSpellRepository.UpdateTeamCardSpell(team_id, position, card_id);
    }

    public int GetUserCardSpellCount(string user_id, string type, string rare)
    {
        return _userCardSpellRepository.GetUserCardSpellCount(user_id, type, rare);
    }

    public int GetUserCardSpellTeamsPositionCount(string user_id, string team_id, string position)
    {
        return _userCardSpellRepository.GetUserCardSpellTeamsPositionCount(user_id, team_id, position);
    }

    public int GetUserCardSpellTeamsCount(string user_id, string team_id)
    {
        return _userCardSpellRepository.GetUserCardSpellTeamsCount(user_id, team_id);
    }

    public bool InsertUserCardSpell(CardSpell CardSpell)
    {
        return _userCardSpellRepository.InsertUserCardSpell(CardSpell);
    }

    public bool UpdateCardSpellLevel(CardSpell cardSpell, int cardLevel)
    {
        return _userCardSpellRepository.UpdateCardSpellLevel(cardSpell, cardLevel);
    }

    public bool UpdateCardSpellBreakthrough(CardSpell cardSpell, int star, int quantity)
    {
        return _userCardSpellRepository.UpdateCardSpellBreakthrough(cardSpell, star, quantity);
    }

    public CardSpell GetUserCardSpellById(string user_id, string Id)
    {
        CardSpell cardSpell = _userCardSpellRepository.GetUserCardSpellById(user_id, Id);
        if (cardSpell == null) return null;

        // Bọc vào list để tái sử dụng logic
        List<CardSpell> list = new List<CardSpell> { cardSpell };

        list = GetAllSpiritBeastPower(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllMasterPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        list = GetScienceFictionPower(user_id, list);
        return list.FirstOrDefault();
    }

    public List<CardSpell> GetAllUserCardSpellInTeam(string user_id)
    {
        List<CardSpell> list = _userCardSpellRepository.GetAllUserCardSpellInTeam(user_id);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllMasterPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        list = GetScienceFictionPower(user_id, list);
        return list;
    }
}
