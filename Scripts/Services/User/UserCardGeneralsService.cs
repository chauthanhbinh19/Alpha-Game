using System.Collections.Generic;

public class UserCardGeneralsService : IUserCardGeneralsService
{
    private readonly IUserCardGeneralsRepository _userCardGeneralsRepository;

    public UserCardGeneralsService(IUserCardGeneralsRepository userCardGeneralsRepository)
    {
        _userCardGeneralsRepository = userCardGeneralsRepository;
    }

    public static UserCardGeneralsService Create()
    {
        return new UserCardGeneralsService(new UserCardGeneralsRepository());
    }

    public List<CardGenerals> GetFinalPower(string user_id, List<CardGenerals> CardGeneralsList)
    {
        IPowerManagerRepository powerManagerRepository = new PowerManagerRepository();
        PowerManagerService powerManagerService = new PowerManagerService(powerManagerRepository);
        PowerManager powerManager = powerManagerService.GetUserStats(user_id);
        foreach (var c in CardGeneralsList)
        {
            CardGenerals card = _userCardGeneralsRepository.GetUserCardGeneralsById(user_id, c.id);
            c.all_health = c.all_health + powerManager.health + card.health * powerManager.percent_all_health / 100;
            c.all_physical_attack = c.all_physical_attack + powerManager.physical_attack + card.physical_attack * powerManager.percent_all_physical_attack / 100;
            c.all_physical_defense = c.all_physical_defense + powerManager.physical_defense + card.physical_defense * powerManager.percent_all_physical_defense / 100;
            c.all_magical_attack = c.all_magical_attack + powerManager.magical_attack + card.magical_attack * powerManager.percent_all_magical_attack / 100;
            c.all_magical_defense = c.all_magical_defense + powerManager.magical_defense + card.magical_defense * powerManager.percent_all_magical_defense / 100;
            c.all_chemical_attack = c.all_chemical_attack + powerManager.chemical_attack + card.chemical_attack * powerManager.percent_all_chemical_attack / 100;
            c.all_chemical_defense = c.all_chemical_defense + powerManager.chemical_defense + card.chemical_defense * powerManager.percent_all_chemical_defense / 100;
            c.all_atomic_attack = c.all_atomic_attack + powerManager.atomic_attack + card.atomic_attack * powerManager.percent_all_atomic_attack / 100;
            c.all_atomic_defense = c.all_atomic_defense + powerManager.atomic_defense + card.atomic_defense * powerManager.percent_all_atomic_defense / 100;
            c.all_mental_attack = c.all_mental_attack + powerManager.mental_attack + card.mental_attack * powerManager.percent_all_mental_attack / 100;
            c.all_mental_defense = c.all_mental_defense + powerManager.mental_defense + card.mental_defense * powerManager.percent_all_mental_defense / 100;
            c.all_speed = c.all_speed + powerManager.speed;
            c.all_critical_damage_rate = c.all_critical_damage_rate + powerManager.critical_damage_rate;
            c.all_critical_rate = c.all_critical_rate + powerManager.critical_rate;
            c.all_critical_resistance_rate = c.all_critical_resistance_rate + powerManager.critical_resistance_rate;
            c.all_ignore_critical_rate = c.all_ignore_critical_rate + powerManager.ignore_critical_rate;
            c.all_penetration_rate = c.all_penetration_rate + powerManager.penetration_rate;
            c.all_penetration_resistance_rate = c.all_penetration_resistance_rate + powerManager.penetration_resistance_rate;
            c.all_evasion_rate = c.all_evasion_rate + powerManager.evasion_rate;
            c.all_damage_absorption_rate = c.all_damage_absorption_rate + powerManager.damage_absorption_rate;
            c.all_ignore_damage_absorption_rate = c.all_ignore_damage_absorption_rate + powerManager.ignore_damage_absorption_rate;
            c.all_absorbed_damage_rate = c.all_absorbed_damage_rate + powerManager.absorbed_damage_rate;
            c.all_vitality_regeneration_rate = c.all_vitality_regeneration_rate + powerManager.vitality_regeneration_rate;
            c.all_vitality_regeneration_resistance_rate = c.all_vitality_regeneration_resistance_rate + powerManager.vitality_regeneration_resistance_rate;
            c.all_accuracy_rate = c.all_accuracy_rate + powerManager.accuracy_rate;
            c.all_lifesteal_rate = c.all_lifesteal_rate + powerManager.lifesteal_rate;
            c.all_shield_strength = c.all_shield_strength + powerManager.shield_strength;
            c.all_tenacity = c.all_tenacity + powerManager.tenacity;
            c.all_resistance_rate = c.all_resistance_rate + powerManager.resistance_rate;
            c.all_combo_rate = c.all_combo_rate + powerManager.combo_rate;
            c.all_ignore_combo_rate = c.all_ignore_combo_rate + powerManager.ignore_combo_rate;
            c.all_combo_damage_rate = c.all_combo_damage_rate + powerManager.combo_damage_rate;
            c.all_combo_resistance_rate = c.all_combo_resistance_rate + powerManager.combo_resistance_rate;
            c.all_stun_rate = c.all_stun_rate + powerManager.stun_rate;
            c.all_ignore_stun_rate = c.all_ignore_stun_rate + powerManager.ignore_stun_rate;
            c.all_reflection_rate = c.all_reflection_rate + powerManager.reflection_rate;
            c.all_ignore_reflection_rate  = c.all_ignore_reflection_rate + powerManager.ignore_reflection_rate;
            c.all_reflection_damage_rate = c.all_reflection_damage_rate + powerManager.reflection_damage_rate;
            c.all_reflection_resistance_rate = c.all_reflection_resistance_rate + powerManager.reflection_resistance_rate;
            c.all_mana = c.all_mana + powerManager.mana;
            c.all_mana_regeneration_rate = c.all_mana_regeneration_rate + powerManager.mana_regeneration_rate;
            c.all_damage_to_different_faction_rate = c.all_damage_to_different_faction_rate + powerManager.damage_to_different_faction_rate;
            c.all_resistance_to_different_faction_rate = c.all_resistance_to_different_faction_rate + powerManager.resistance_to_different_faction_rate;
            c.all_damage_to_same_faction_rate = c.all_damage_to_same_faction_rate + powerManager.damage_to_same_faction_rate;
            c.all_resistance_to_same_faction_rate = c.all_resistance_to_same_faction_rate + powerManager.resistance_to_same_faction_rate;
            c.all_normal_damage_rate = c.all_normal_damage_rate + powerManager.normal_damage_rate;
            c.all_normal_resistance_rate = c.all_normal_resistance_rate + powerManager.normal_resistance_rate;
            c.all_skill_damage_rate = c.all_skill_damage_rate + powerManager.skill_damage_rate;
            c.all_skill_resistance_rate = c.all_skill_resistance_rate + powerManager.skill_resistance_rate;

            c.all_power = EvaluatePower.CalculatePower(
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
        return CardGeneralsList;
    }
    public List<CardGenerals> GetAllEquipmentPower(string user_id, List<CardGenerals> CardGeneralsList)
    {
        Equipments equipments = new Equipments();
        IUserEquipmentsRepository userEquipmentsRepository = new UserEquipmentsRepository();
        UserEquipmentsService userEquipmentsService = new UserEquipmentsService(userEquipmentsRepository);
        foreach (var c in CardGeneralsList)
        {
            equipments = userEquipmentsService.GetAllEquipmentsByCardGeneralsId(user_id, c.id);
            c.all_health = c.all_health + equipments.health + equipments.special_health;
            c.all_physical_attack = c.all_physical_attack + equipments.physical_attack + equipments.special_physical_attack;
            c.all_physical_defense = c.all_physical_defense + equipments.physical_defense + equipments.special_physical_defense;
            c.all_magical_attack = c.all_magical_attack + equipments.magical_attack + equipments.special_magical_attack;
            c.all_magical_defense = c.all_magical_defense + equipments.magical_defense + equipments.special_magical_defense;
            c.all_chemical_attack = c.all_chemical_attack + equipments.chemical_attack + equipments.special_chemical_attack;
            c.all_chemical_defense = c.all_chemical_defense + equipments.chemical_defense + equipments.special_chemical_defense;
            c.all_atomic_attack = c.all_atomic_attack + equipments.atomic_attack + equipments.special_atomic_attack;
            c.all_atomic_defense = c.all_atomic_defense + equipments.atomic_defense + equipments.special_atomic_defense;
            c.all_mental_attack = c.all_mental_attack + equipments.mental_attack + equipments.special_mental_attack;
            c.all_mental_defense = c.all_mental_defense + equipments.mental_defense + equipments.special_mental_defense;
            c.all_speed = c.all_speed + equipments.speed;
            c.all_critical_damage_rate = c.all_critical_damage_rate + equipments.critical_damage_rate;
            c.all_critical_rate = c.all_critical_rate + equipments.critical_rate;
            c.all_critical_resistance_rate = c.all_critical_resistance_rate + equipments.critical_resistance_rate;
            c.all_ignore_critical_rate = c.all_ignore_critical_rate + equipments.ignore_critical_rate;
            c.all_penetration_rate = c.all_penetration_rate + equipments.penetration_rate;
            c.all_penetration_resistance_rate = c.all_penetration_resistance_rate + equipments.penetration_resistance_rate;
            c.all_evasion_rate = c.all_evasion_rate + equipments.evasion_rate;
            c.all_damage_absorption_rate = c.all_damage_absorption_rate + equipments.damage_absorption_rate;
            c.all_ignore_damage_absorption_rate = c.all_ignore_damage_absorption_rate + equipments.ignore_damage_absorption_rate;
            c.all_absorbed_damage_rate = c.all_absorbed_damage_rate + equipments.absorbed_damage_rate;
            c.all_vitality_regeneration_rate = c.all_vitality_regeneration_rate + equipments.vitality_regeneration_rate;
            c.all_vitality_regeneration_resistance_rate = c.all_vitality_regeneration_resistance_rate + equipments.vitality_regeneration_resistance_rate;
            c.all_accuracy_rate = c.all_accuracy_rate + equipments.accuracy_rate;
            c.all_lifesteal_rate = c.all_lifesteal_rate + equipments.lifesteal_rate;
            c.all_shield_strength = c.all_shield_strength + equipments.shield_strength;
            c.all_tenacity = c.all_tenacity + equipments.tenacity;
            c.all_resistance_rate = c.all_resistance_rate + equipments.resistance_rate;
            c.all_combo_rate = c.all_combo_rate + equipments.combo_rate;
            c.all_ignore_combo_rate = c.all_ignore_combo_rate + equipments.ignore_combo_rate;
            c.all_combo_damage_rate = c.all_combo_damage_rate + equipments.combo_damage_rate;
            c.all_combo_resistance_rate = c.all_combo_resistance_rate + equipments.combo_resistance_rate;
            c.all_stun_rate = c.all_stun_rate + equipments.stun_rate;
            c.all_ignore_stun_rate = c.all_ignore_stun_rate + equipments.ignore_stun_rate;
            c.all_reflection_rate = c.all_reflection_rate + equipments.reflection_rate;
            c.all_ignore_reflection_rate  = c.all_ignore_reflection_rate + equipments.ignore_reflection_rate;
            c.all_reflection_damage_rate = c.all_reflection_damage_rate + equipments.reflection_damage_rate;
            c.all_reflection_resistance_rate = c.all_reflection_resistance_rate + equipments.reflection_resistance_rate;
            c.all_mana = c.all_mana + equipments.mana;
            c.all_mana_regeneration_rate = c.all_mana_regeneration_rate + equipments.mana_regeneration_rate;
            c.all_damage_to_different_faction_rate = c.all_damage_to_different_faction_rate + equipments.damage_to_different_faction_rate;
            c.all_resistance_to_different_faction_rate = c.all_resistance_to_different_faction_rate + equipments.resistance_to_different_faction_rate;
            c.all_damage_to_same_faction_rate = c.all_damage_to_same_faction_rate + equipments.damage_to_same_faction_rate;
            c.all_resistance_to_same_faction_rate = c.all_resistance_to_same_faction_rate + equipments.resistance_to_same_faction_rate;
            c.all_normal_damage_rate = c.all_normal_damage_rate + equipments.normal_damage_rate;
            c.all_normal_resistance_rate = c.all_normal_resistance_rate + equipments.normal_resistance_rate;
            c.all_skill_damage_rate = c.all_skill_damage_rate + equipments.skill_damage_rate;
            c.all_skill_resistance_rate = c.all_skill_resistance_rate + equipments.skill_resistance_rate;

            c.all_power = EvaluatePower.CalculatePower(
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
        return CardGeneralsList;
    }
    public List<CardGenerals> GetAllRankPower(string user_id, List<CardGenerals> CardGeneralsList)
    {
        IUserCardGeneralsRankRepository userCardGeneralsRankRepository = new UserCardGeneralsRankRepository();
        UserCardGeneralsRankService userCardGeneralsRankService = new UserCardGeneralsRankService(userCardGeneralsRankRepository);
        foreach (var c in CardGeneralsList)
        {
            CardGenerals card = _userCardGeneralsRepository.GetUserCardGeneralsById(user_id, c.id);
            Rank rank = userCardGeneralsRankService.GetSumCardGeneralsRank(user_id, c.id);
            c.all_health = c.all_health + rank.health + card.health * rank.percent_all_health / 100;
            c.all_physical_attack = c.all_physical_attack + rank.physical_attack + card.physical_attack * rank.percent_all_physical_attack / 100;
            c.all_physical_defense = c.all_physical_defense + rank.physical_defense + card.physical_defense * rank.percent_all_physical_defense / 100;
            c.all_magical_attack = c.all_magical_attack + rank.magical_attack + card.magical_attack * rank.percent_all_magical_attack / 100;
            c.all_magical_defense = c.all_magical_defense + rank.magical_defense + card.magical_defense * rank.percent_all_magical_defense / 100;
            c.all_chemical_attack = c.all_chemical_attack + rank.chemical_attack + card.chemical_attack * rank.percent_all_chemical_attack / 100;
            c.all_chemical_defense = c.all_chemical_defense + rank.chemical_defense + card.chemical_defense * rank.percent_all_chemical_defense / 100;
            c.all_atomic_attack = c.all_atomic_attack + rank.atomic_attack + card.atomic_attack * rank.percent_all_atomic_attack / 100;
            c.all_atomic_defense = c.all_atomic_defense + rank.atomic_defense + card.atomic_defense * rank.percent_all_atomic_defense / 100;
            c.all_mental_attack = c.all_mental_attack + rank.mental_attack + card.mental_attack * rank.percent_all_mental_attack / 100;
            c.all_mental_defense = c.all_mental_defense + rank.mental_defense + card.mental_defense * rank.percent_all_mental_defense / 100;
            c.all_speed = c.all_speed + rank.speed;
            c.all_critical_damage_rate = c.all_critical_damage_rate + rank.critical_damage_rate;
            c.all_critical_rate = c.all_critical_rate + rank.critical_rate;
            c.all_critical_resistance_rate = c.all_critical_resistance_rate + rank.critical_resistance_rate;
            c.all_ignore_critical_rate = c.all_ignore_critical_rate + rank.ignore_critical_rate;
            c.all_penetration_rate = c.all_penetration_rate + rank.penetration_rate;
            c.all_penetration_resistance_rate = c.all_penetration_resistance_rate + rank.penetration_resistance_rate;
            c.all_evasion_rate = c.all_evasion_rate + rank.evasion_rate;
            c.all_damage_absorption_rate = c.all_damage_absorption_rate + rank.damage_absorption_rate;
            c.all_ignore_damage_absorption_rate = c.all_ignore_damage_absorption_rate + rank.ignore_damage_absorption_rate;
            c.all_absorbed_damage_rate = c.all_absorbed_damage_rate + rank.absorbed_damage_rate;
            c.all_vitality_regeneration_rate = c.all_vitality_regeneration_rate + rank.vitality_regeneration_rate;
            c.all_vitality_regeneration_resistance_rate = c.all_vitality_regeneration_resistance_rate + rank.vitality_regeneration_resistance_rate;
            c.all_accuracy_rate = c.all_accuracy_rate + rank.accuracy_rate;
            c.all_lifesteal_rate = c.all_lifesteal_rate + rank.lifesteal_rate;
            c.all_shield_strength = c.all_shield_strength + rank.shield_strength;
            c.all_tenacity = c.all_tenacity + rank.tenacity;
            c.all_resistance_rate = c.all_resistance_rate + rank.resistance_rate;
            c.all_combo_rate = c.all_combo_rate + rank.combo_rate;
            c.all_ignore_combo_rate = c.all_ignore_combo_rate + rank.ignore_combo_rate;
            c.all_combo_damage_rate = c.all_combo_damage_rate + rank.combo_damage_rate;
            c.all_combo_resistance_rate = c.all_combo_resistance_rate + rank.combo_resistance_rate;
            c.all_stun_rate = c.all_stun_rate + rank.stun_rate;
            c.all_ignore_stun_rate = c.all_ignore_stun_rate + rank.ignore_stun_rate;
            c.all_reflection_rate = c.all_reflection_rate + rank.reflection_rate;
            c.all_ignore_reflection_rate  = c.all_ignore_reflection_rate + rank.ignore_reflection_rate;
            c.all_reflection_damage_rate = c.all_reflection_damage_rate + rank.reflection_damage_rate;
            c.all_reflection_resistance_rate = c.all_reflection_resistance_rate + rank.reflection_resistance_rate;
            c.all_mana = c.all_mana + rank.mana;
            c.all_mana_regeneration_rate = c.all_mana_regeneration_rate + rank.mana_regeneration_rate;
            c.all_damage_to_different_faction_rate = c.all_damage_to_different_faction_rate + rank.damage_to_different_faction_rate;
            c.all_resistance_to_different_faction_rate = c.all_resistance_to_different_faction_rate + rank.resistance_to_different_faction_rate;
            c.all_damage_to_same_faction_rate = c.all_damage_to_same_faction_rate + rank.damage_to_same_faction_rate;
            c.all_resistance_to_same_faction_rate = c.all_resistance_to_same_faction_rate + rank.resistance_to_same_faction_rate;
            c.all_normal_damage_rate = c.all_normal_damage_rate + rank.normal_damage_rate;
            c.all_normal_resistance_rate = c.all_normal_resistance_rate + rank.normal_resistance_rate;
            c.all_skill_damage_rate = c.all_skill_damage_rate + rank.skill_damage_rate;
            c.all_skill_resistance_rate = c.all_skill_resistance_rate + rank.skill_resistance_rate;

            c.all_power = EvaluatePower.CalculatePower(
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
        return CardGeneralsList;
    }
    public List<CardGenerals> GetAllAnimeStatsPower(string user_id, List<CardGenerals> CardGeneralsList)
    {
        IAnimeStatsRepository animeStatsRepository = new AnimeStatsRepository();
        AnimeStatsService animeStatsService = new AnimeStatsService(animeStatsRepository);
        foreach (var c in CardGeneralsList)
        {
            CardGenerals card = _userCardGeneralsRepository.GetUserCardGeneralsById(user_id, c.id);
            AnimeStats animeStats = animeStatsService.GetSumAnimeStats(user_id);
            c.all_health = c.all_health + animeStats.health + card.health * animeStats.percent_all_health / 100;
            c.all_physical_attack = c.all_physical_attack + animeStats.physical_attack + card.physical_attack * animeStats.percent_all_physical_attack / 100;
            c.all_physical_defense = c.all_physical_defense + animeStats.physical_defense + card.physical_defense * animeStats.percent_all_physical_defense / 100;
            c.all_magical_attack = c.all_magical_attack + animeStats.magical_attack + card.magical_attack * animeStats.percent_all_magical_attack / 100;
            c.all_magical_defense = c.all_magical_defense + animeStats.magical_defense + card.magical_defense * animeStats.percent_all_magical_defense / 100;
            c.all_chemical_attack = c.all_chemical_attack + animeStats.chemical_attack + card.chemical_attack * animeStats.percent_all_chemical_attack / 100;
            c.all_chemical_defense = c.all_chemical_defense + animeStats.chemical_defense + card.chemical_defense * animeStats.percent_all_chemical_defense / 100;
            c.all_atomic_attack = c.all_atomic_attack + animeStats.atomic_attack + card.atomic_attack * animeStats.percent_all_atomic_attack / 100;
            c.all_atomic_defense = c.all_atomic_defense + animeStats.atomic_defense + card.atomic_defense * animeStats.percent_all_atomic_defense / 100;
            c.all_mental_attack = c.all_mental_attack + animeStats.mental_attack + card.mental_attack * animeStats.percent_all_mental_attack / 100;
            c.all_mental_defense = c.all_mental_defense + animeStats.mental_defense + card.mental_defense * animeStats.percent_all_mental_defense / 100;
            c.all_speed = c.all_speed + animeStats.speed;
            c.all_critical_damage_rate = c.all_critical_damage_rate + animeStats.critical_damage_rate;
            c.all_critical_rate = c.all_critical_rate + animeStats.critical_rate;
            c.all_critical_resistance_rate = c.all_critical_resistance_rate + animeStats.critical_resistance_rate;
            c.all_ignore_critical_rate = c.all_ignore_critical_rate + animeStats.ignore_critical_rate;
            c.all_penetration_rate = c.all_penetration_rate + animeStats.penetration_rate;
            c.all_penetration_resistance_rate = c.all_penetration_resistance_rate + animeStats.penetration_resistance_rate;
            c.all_evasion_rate = c.all_evasion_rate + animeStats.evasion_rate;
            c.all_damage_absorption_rate = c.all_damage_absorption_rate + animeStats.damage_absorption_rate;
            c.all_ignore_damage_absorption_rate = c.all_ignore_damage_absorption_rate + animeStats.ignore_damage_absorption_rate;
            c.all_absorbed_damage_rate = c.all_absorbed_damage_rate + animeStats.absorbed_damage_rate;
            c.all_vitality_regeneration_rate = c.all_vitality_regeneration_rate + animeStats.vitality_regeneration_rate;
            c.all_vitality_regeneration_resistance_rate = c.all_vitality_regeneration_resistance_rate + animeStats.vitality_regeneration_resistance_rate;
            c.all_accuracy_rate = c.all_accuracy_rate + animeStats.accuracy_rate;
            c.all_lifesteal_rate = c.all_lifesteal_rate + animeStats.lifesteal_rate;
            c.all_shield_strength = c.all_shield_strength + animeStats.shield_strength;
            c.all_tenacity = c.all_tenacity + animeStats.tenacity;
            c.all_resistance_rate = c.all_resistance_rate + animeStats.resistance_rate;
            c.all_combo_rate = c.all_combo_rate + animeStats.combo_rate;
            c.all_ignore_combo_rate = c.all_ignore_combo_rate + animeStats.ignore_combo_rate;
            c.all_combo_damage_rate = c.all_combo_damage_rate + animeStats.combo_damage_rate;
            c.all_combo_resistance_rate = c.all_combo_resistance_rate + animeStats.combo_resistance_rate;
            c.all_stun_rate = c.all_stun_rate + animeStats.stun_rate;
            c.all_ignore_stun_rate = c.all_ignore_stun_rate + animeStats.ignore_stun_rate;
            c.all_reflection_rate = c.all_reflection_rate + animeStats.reflection_rate;
            c.all_ignore_reflection_rate  = c.all_ignore_reflection_rate + animeStats.ignore_reflection_rate;
            c.all_reflection_damage_rate = c.all_reflection_damage_rate + animeStats.reflection_damage_rate;
            c.all_reflection_resistance_rate = c.all_reflection_resistance_rate + animeStats.reflection_resistance_rate;
            c.all_mana = c.all_mana + animeStats.mana;
            c.all_mana_regeneration_rate = c.all_mana_regeneration_rate + animeStats.mana_regeneration_rate;
            c.all_damage_to_different_faction_rate = c.all_damage_to_different_faction_rate + animeStats.damage_to_different_faction_rate;
            c.all_resistance_to_different_faction_rate = c.all_resistance_to_different_faction_rate + animeStats.resistance_to_different_faction_rate;
            c.all_damage_to_same_faction_rate = c.all_damage_to_same_faction_rate + animeStats.damage_to_same_faction_rate;
            c.all_resistance_to_same_faction_rate = c.all_resistance_to_same_faction_rate + animeStats.resistance_to_same_faction_rate;
            c.all_normal_damage_rate = c.all_normal_damage_rate + animeStats.normal_damage_rate;
            c.all_normal_resistance_rate = c.all_normal_resistance_rate + animeStats.normal_resistance_rate;
            c.all_skill_damage_rate = c.all_skill_damage_rate + animeStats.skill_damage_rate;
            c.all_skill_resistance_rate = c.all_skill_resistance_rate + animeStats.skill_resistance_rate;

            c.all_power = EvaluatePower.CalculatePower(
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
        return CardGeneralsList;
    }
    public CardGenerals GetNewLevelPower(CardGenerals c, double coefficient)
    {
        ICardGeneralsRepository _repository = new CardGeneralsRepository();
        CardGeneralsService _service = new CardGeneralsService(_repository);
        CardGenerals orginCard = _service.GetCardGeneralsById(c.id);
        CardGenerals cardGenerals = new CardGenerals
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
        cardGenerals.power = EvaluatePower.CalculatePower(
            cardGenerals.health,
            cardGenerals.physical_attack, cardGenerals.physical_defense,
            cardGenerals.magical_attack, cardGenerals.magical_defense,
            cardGenerals.chemical_attack, cardGenerals.chemical_defense,
            cardGenerals.atomic_attack, cardGenerals.atomic_defense,
            cardGenerals.mental_attack, cardGenerals.mental_defense,
            cardGenerals.speed,
            cardGenerals.critical_damage_rate, cardGenerals.critical_rate, cardGenerals.critical_resistance_rate, cardGenerals.ignore_critical_rate,
            cardGenerals.penetration_rate, cardGenerals.penetration_resistance_rate, cardGenerals.evasion_rate,
            cardGenerals.damage_absorption_rate, cardGenerals.ignore_damage_absorption_rate, cardGenerals.absorbed_damage_rate,
            cardGenerals.vitality_regeneration_rate, cardGenerals.vitality_regeneration_resistance_rate,
            cardGenerals.accuracy_rate, cardGenerals.lifesteal_rate,
            cardGenerals.shield_strength, cardGenerals.tenacity, cardGenerals.resistance_rate,
            cardGenerals.combo_rate, cardGenerals.ignore_combo_rate, cardGenerals.combo_damage_rate, cardGenerals.combo_resistance_rate,
            cardGenerals.stun_rate, cardGenerals.ignore_stun_rate,
            cardGenerals.reflection_rate, cardGenerals.ignore_reflection_rate, cardGenerals.reflection_damage_rate, cardGenerals.reflection_resistance_rate,
            cardGenerals.mana, cardGenerals.mana_regeneration_rate,
            cardGenerals.damage_to_different_faction_rate, cardGenerals.resistance_to_different_faction_rate,
            cardGenerals.damage_to_same_faction_rate, cardGenerals.resistance_to_same_faction_rate,
            cardGenerals.normal_damage_rate, cardGenerals.normal_resistance_rate,
            cardGenerals.skill_damage_rate, cardGenerals.skill_resistance_rate
        );
        return cardGenerals;
    }
    public CardGenerals GetNewBreakthroughPower(CardGenerals c, double coefficient)
    {
        ICardGeneralsRepository _repository = new CardGeneralsRepository();
        CardGeneralsService _service = new CardGeneralsService(_repository);
        CardGenerals orginCard = _service.GetCardGeneralsById(c.id);
        CardGenerals cardGenerals = new CardGenerals
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
        cardGenerals.power = EvaluatePower.CalculatePower(
            cardGenerals.health,
            cardGenerals.physical_attack, cardGenerals.physical_defense,
            cardGenerals.magical_attack, cardGenerals.magical_defense,
            cardGenerals.chemical_attack, cardGenerals.chemical_defense,
            cardGenerals.atomic_attack, cardGenerals.atomic_defense,
            cardGenerals.mental_attack, cardGenerals.mental_defense,
            cardGenerals.speed,
            cardGenerals.critical_damage_rate, cardGenerals.critical_rate, cardGenerals.critical_resistance_rate, cardGenerals.ignore_critical_rate,
            cardGenerals.penetration_rate, cardGenerals.penetration_resistance_rate, cardGenerals.evasion_rate,
            cardGenerals.damage_absorption_rate, cardGenerals.ignore_damage_absorption_rate, cardGenerals.absorbed_damage_rate,
            cardGenerals.vitality_regeneration_rate, cardGenerals.vitality_regeneration_resistance_rate,
            cardGenerals.accuracy_rate, cardGenerals.lifesteal_rate,
            cardGenerals.shield_strength, cardGenerals.tenacity, cardGenerals.resistance_rate,
            cardGenerals.combo_rate, cardGenerals.ignore_combo_rate, cardGenerals.combo_damage_rate, cardGenerals.combo_resistance_rate,
            cardGenerals.stun_rate, cardGenerals.ignore_stun_rate,
            cardGenerals.reflection_rate, cardGenerals.ignore_reflection_rate, cardGenerals.reflection_damage_rate, cardGenerals.reflection_resistance_rate,
            cardGenerals.mana, cardGenerals.mana_regeneration_rate,
            cardGenerals.damage_to_different_faction_rate, cardGenerals.resistance_to_different_faction_rate,
            cardGenerals.damage_to_same_faction_rate, cardGenerals.resistance_to_same_faction_rate,
            cardGenerals.normal_damage_rate, cardGenerals.normal_resistance_rate,
            cardGenerals.skill_damage_rate, cardGenerals.skill_resistance_rate
        );
        return cardGenerals;
    }

    public List<CardGenerals> GetUserCardGenerals(string user_id, string type, int pageSize, int offset)
    {
        List<CardGenerals> list = _userCardGeneralsRepository.GetUserCardGenerals(user_id, type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        return list;
    }

    public List<CardGenerals> GetUserCardGeneralsTeam(string user_id, string teamId, string position)
    {
        List<CardGenerals> list = _userCardGeneralsRepository.GetUserCardGeneralsTeam(user_id, teamId, position);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        return list;
    }

    public int GetUserCardGeneralsTeamsPositionCount(string user_id, string team_id, string position)
    {
        return _userCardGeneralsRepository.GetUserCardGeneralsTeamsPositionCount(user_id, team_id, position);
    }

    public Dictionary<string, int> GetUniqueCardGeneralTypesTeam(string teamId)
    {
        return _userCardGeneralsRepository.GetUniqueCardGeneralTypesTeam(teamId);
    }

    public bool UpdateTeamFactCardGenerals(string team_id, string position, string card_id)
    {
        return _userCardGeneralsRepository.UpdateTeamFactCardGenerals(team_id, position, card_id);
    }

    public int GetUserCardGeneralsCount(string user_id, string type)
    {
        return _userCardGeneralsRepository.GetUserCardGeneralsCount(user_id, type);
    }

    public bool InsertUserCardGenerals(CardGenerals CardGenerals)
    {
        return _userCardGeneralsRepository.InsertUserCardGenerals(CardGenerals);
    }

    public bool UpdateCardGeneralsLevel(CardGenerals cardGenerals, int cardLevel)
    {
        return _userCardGeneralsRepository.UpdateCardGeneralsLevel(cardGenerals, cardLevel);
    }

    public bool UpdateCardGeneralsBreakthrough(CardGenerals cardGenerals, int star, int quantity)
    {
        return _userCardGeneralsRepository.UpdateCardGeneralsBreakthrough(cardGenerals, star, quantity);
    }

    public bool InsertFactCardGenerals(CardGenerals cardGenerals)
    {
        return _userCardGeneralsRepository.InsertFactCardGenerals(cardGenerals);
    }

    public bool UpdateFactCardGenerals(CardGenerals cardGenerals)
    {
        return _userCardGeneralsRepository.UpdateFactCardGenerals(cardGenerals);
    }

    public CardGenerals GetUserCardGeneralsById(string user_id, string Id)
    {
        return _userCardGeneralsRepository.GetUserCardGeneralsById(user_id, Id);
    }

    public List<CardGenerals> GetAllUserCardGeneralsInTeam(string user_id)
    {
        List<CardGenerals> list = _userCardGeneralsRepository.GetAllUserCardGeneralsInTeam(user_id);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        return list;
    }
}
