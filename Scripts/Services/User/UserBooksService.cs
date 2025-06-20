using System.Collections.Generic;

public class UserBooksService : IUserBooksService
{
    private readonly IUserBooksRepository _userBooksRepository;

    public UserBooksService(IUserBooksRepository userBooksRepository)
    {
        _userBooksRepository = userBooksRepository;
    }

    public static UserBooksService Create()
    {
        return new UserBooksService(new UserBooksRepository());
    }

    public List<Books> GetFinalPower(string user_id, List<Books> BooksList)
    {
        IPowerManagerRepository powerManagerRepository = new PowerManagerRepository();
        PowerManagerService powerManagerService = new PowerManagerService(powerManagerRepository);
        PowerManager powerManager = powerManagerService.GetUserStats(user_id);
        foreach (var c in BooksList)
        {
            Books books = _userBooksRepository.GetUserBooksById(user_id, c.id);
            c.all_health = c.all_health + powerManager.health + books.health * powerManager.percent_all_health / 100;
            c.all_physical_attack = c.all_physical_attack + powerManager.physical_attack + books.physical_attack * powerManager.percent_all_physical_attack / 100;
            c.all_physical_defense = c.all_physical_defense + powerManager.physical_defense + books.physical_defense * powerManager.percent_all_physical_defense / 100;
            c.all_magical_attack = c.all_magical_attack + powerManager.magical_attack + books.magical_attack * powerManager.percent_all_magical_attack / 100;
            c.all_magical_defense = c.all_magical_defense + powerManager.magical_defense + books.magical_defense * powerManager.percent_all_magical_defense / 100;
            c.all_chemical_attack = c.all_chemical_attack + powerManager.chemical_attack + books.chemical_attack * powerManager.percent_all_chemical_attack / 100;
            c.all_chemical_defense = c.all_chemical_defense + powerManager.chemical_defense + books.chemical_defense * powerManager.percent_all_chemical_defense / 100;
            c.all_atomic_attack = c.all_atomic_attack + powerManager.atomic_attack + books.atomic_attack * powerManager.percent_all_atomic_attack / 100;
            c.all_atomic_defense = c.all_atomic_defense + powerManager.atomic_defense + books.atomic_defense * powerManager.percent_all_atomic_defense / 100;
            c.all_mental_attack = c.all_mental_attack + powerManager.mental_attack + books.mental_attack * powerManager.percent_all_mental_attack / 100;
            c.all_mental_defense = c.all_mental_defense + powerManager.mental_defense + books.mental_defense * powerManager.percent_all_mental_defense / 100;
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
        return BooksList;
    }
    public List<Books> GetAllEquipmentPower(string user_id, List<Books> BooksList)
    {
        IUserEquipmentsRepository userEquipmentsRepository = new UserEquipmentsRepository();
        UserEquipmentsService userEquipmentsService = new UserEquipmentsService(userEquipmentsRepository);
        foreach (var c in BooksList)
        {
            Equipments equipments = userEquipmentsService.GetAllEquipmentsByBooksId(user_id, c.id);
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
        return BooksList;
    }
    public List<Books> GetAllRankPower(string user_id, List<Books> BooksList)
    {
        IUserBooksRankRepository userBooksRankRepository = new UserBooksRankRepository();
        UserBooksRankService userBooksRankService = new UserBooksRankService(userBooksRankRepository);
        foreach (var c in BooksList)
        {
            Books books = _userBooksRepository.GetUserBooksById(user_id, c.id);
            Rank rank = userBooksRankService.GetSumBooksRank(user_id, c.id);
            c.all_health = c.all_health + rank.health + books.health * rank.percent_all_health / 100;
            c.all_physical_attack = c.all_physical_attack + rank.physical_attack + books.physical_attack * rank.percent_all_physical_attack / 100;
            c.all_physical_defense = c.all_physical_defense + rank.physical_defense + books.physical_defense * rank.percent_all_physical_defense / 100;
            c.all_magical_attack = c.all_magical_attack + rank.magical_attack + books.magical_attack * rank.percent_all_magical_attack / 100;
            c.all_magical_defense = c.all_magical_defense + rank.magical_defense + books.magical_defense * rank.percent_all_magical_defense / 100;
            c.all_chemical_attack = c.all_chemical_attack + rank.chemical_attack + books.chemical_attack * rank.percent_all_chemical_attack / 100;
            c.all_chemical_defense = c.all_chemical_defense + rank.chemical_defense + books.chemical_defense * rank.percent_all_chemical_defense / 100;
            c.all_atomic_attack = c.all_atomic_attack + rank.atomic_attack + books.atomic_attack * rank.percent_all_atomic_attack / 100;
            c.all_atomic_defense = c.all_atomic_defense + rank.atomic_defense + books.atomic_defense * rank.percent_all_atomic_defense / 100;
            c.all_mental_attack = c.all_mental_attack + rank.mental_attack + books.mental_attack * rank.percent_all_mental_attack / 100;
            c.all_mental_defense = c.all_mental_defense + rank.mental_defense + books.mental_defense * rank.percent_all_mental_defense / 100;
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
        return BooksList;
    }
    public List<Books> GetAllAnimeStatsPower(string user_id, List<Books> BooksList)
    {
        IAnimeStatsRepository animeStatsRepository = new AnimeStatsRepository();
        AnimeStatsService animeStatsService = new AnimeStatsService(animeStatsRepository);
        foreach (var c in BooksList)
        {
            Books books = _userBooksRepository.GetUserBooksById(user_id, c.id);
            AnimeStats animeStats = animeStatsService.GetSumAnimeStats(user_id);
            c.all_health = c.all_health + animeStats.health + books.health * animeStats.percent_all_health / 100;
            c.all_physical_attack = c.all_physical_attack + animeStats.physical_attack + books.physical_attack * animeStats.percent_all_physical_attack / 100;
            c.all_physical_defense = c.all_physical_defense + animeStats.physical_defense + books.physical_defense * animeStats.percent_all_physical_defense / 100;
            c.all_magical_attack = c.all_magical_attack + animeStats.magical_attack + books.magical_attack * animeStats.percent_all_magical_attack / 100;
            c.all_magical_defense = c.all_magical_defense + animeStats.magical_defense + books.magical_defense * animeStats.percent_all_magical_defense / 100;
            c.all_chemical_attack = c.all_chemical_attack + animeStats.chemical_attack + books.chemical_attack * animeStats.percent_all_chemical_attack / 100;
            c.all_chemical_defense = c.all_chemical_defense + animeStats.chemical_defense + books.chemical_defense * animeStats.percent_all_chemical_defense / 100;
            c.all_atomic_attack = c.all_atomic_attack + animeStats.atomic_attack + books.atomic_attack * animeStats.percent_all_atomic_attack / 100;
            c.all_atomic_defense = c.all_atomic_defense + animeStats.atomic_defense + books.atomic_defense * animeStats.percent_all_atomic_defense / 100;
            c.all_mental_attack = c.all_mental_attack + animeStats.mental_attack + books.mental_attack * animeStats.percent_all_mental_attack / 100;
            c.all_mental_defense = c.all_mental_defense + animeStats.mental_defense + books.mental_defense * animeStats.percent_all_mental_defense / 100;
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
        return BooksList;
    }
    public Books GetNewLevelPower(Books c, double coefficient)
    {
        IBooksRepository _repository = new BooksRepository();
        BooksService _service = new BooksService(_repository);
        Books orginCard = _service.GetBooksById(c.id);
        Books books = new Books
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
        books.power = EvaluatePower.CalculatePower(
            books.health,
            books.physical_attack, books.physical_defense,
            books.magical_attack, books.magical_defense,
            books.chemical_attack, books.chemical_defense,
            books.atomic_attack, books.atomic_defense,
            books.mental_attack, books.mental_defense,
            books.speed,
            books.critical_damage_rate, books.critical_rate, books.critical_resistance_rate, books.ignore_critical_rate,
            books.penetration_rate, books.penetration_resistance_rate, books.evasion_rate,
            books.damage_absorption_rate, books.ignore_damage_absorption_rate, books.absorbed_damage_rate,
            books.vitality_regeneration_rate, books.vitality_regeneration_resistance_rate,
            books.accuracy_rate, books.lifesteal_rate,
            books.shield_strength, books.tenacity, books.resistance_rate,
            books.combo_rate, books.ignore_combo_rate, books.combo_damage_rate, books.combo_resistance_rate,
            books.stun_rate, books.ignore_stun_rate,
            books.reflection_rate, books.ignore_reflection_rate, books.reflection_damage_rate, books.reflection_resistance_rate,
            books.mana, books.mana_regeneration_rate,
            books.damage_to_different_faction_rate, books.resistance_to_different_faction_rate,
            books.damage_to_same_faction_rate, books.resistance_to_same_faction_rate,
            books.normal_damage_rate, books.normal_resistance_rate,
            books.skill_damage_rate, books.skill_resistance_rate
        );
        return books;
    }
    public Books GetNewBreakthroughPower(Books c, double coefficient)
    {
        IBooksRepository _repository = new BooksRepository();
        BooksService _service = new BooksService(_repository);
        Books orginCard = _service.GetBooksById(c.id);
        Books books = new Books
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
        books.power = EvaluatePower.CalculatePower(
            books.health,
            books.physical_attack, books.physical_defense,
            books.magical_attack, books.magical_defense,
            books.chemical_attack, books.chemical_defense,
            books.atomic_attack, books.atomic_defense,
            books.mental_attack, books.mental_defense,
            books.speed,
            books.critical_damage_rate, books.critical_rate, books.critical_resistance_rate, books.ignore_critical_rate,
            books.penetration_rate, books.penetration_resistance_rate, books.evasion_rate,
            books.damage_absorption_rate, books.ignore_damage_absorption_rate, books.absorbed_damage_rate,
            books.vitality_regeneration_rate, books.vitality_regeneration_resistance_rate,
            books.accuracy_rate, books.lifesteal_rate,
            books.shield_strength, books.tenacity, books.resistance_rate,
            books.combo_rate, books.ignore_combo_rate, books.combo_damage_rate, books.combo_resistance_rate,
            books.stun_rate, books.ignore_stun_rate,
            books.reflection_rate, books.ignore_reflection_rate, books.reflection_damage_rate, books.reflection_resistance_rate,
            books.mana, books.mana_regeneration_rate,
            books.damage_to_different_faction_rate, books.resistance_to_different_faction_rate,
            books.damage_to_same_faction_rate, books.resistance_to_same_faction_rate,
            books.normal_damage_rate, books.normal_resistance_rate,
            books.skill_damage_rate, books.skill_resistance_rate
        );
        return books;
    }

    public List<Books> GetUserBooks(string user_id, string type, int pageSize, int offset)
    {
        List<Books> list = _userBooksRepository.GetUserBooks(user_id, type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        return list;
    }

    public List<Books> GetUserBooksTeam(string user_id, string teamId, string position)
    {
        List<Books> list = _userBooksRepository.GetUserBooksTeam(teamId);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        return list;
    }

    public Dictionary<string, int> GetUniqueBookTypesTeam(string teamId)
    {
        return _userBooksRepository.GetUniqueBookTypesTeam(teamId);
    }

    public int GetUserBooksCount(string user_id, string type)
    {
        return _userBooksRepository.GetUserBooksCount(user_id, type);
    }

    public bool InsertUserBooks(Books books)
    {
        return _userBooksRepository.InsertUserBooks(books);
    }

    public bool UpdateBooksLevel(Books books, int cardLevel)
    {
        return _userBooksRepository.UpdateBooksLevel(books, cardLevel);
    }

    public bool UpdateBooksBreakthrough(Books books, int star, int quantity)
    {
        return _userBooksRepository.UpdateBooksBreakthrough(books, star, quantity);
    }

    public bool InsertFactBooks(Books books)
    {
        return _userBooksRepository.InsertFactBooks(books);
    }

    public bool UpdateFactBooks(Books books)
    {
        return _userBooksRepository.UpdateFactBooks(books);
    }

    public bool UpdateTeamFactBooks(string team_id, string position, string book_id)
    {
        return _userBooksRepository.UpdateTeamFactBooks(team_id, position, book_id);
    }

    public Books GetUserBooksById(string user_id, string Id)
    {
        return _userBooksRepository.GetUserBooksById(user_id, Id);
    }

    public List<Books> GetAllUserBooksInTeam(string user_id)
    {
        List<Books> list = _userBooksRepository.GetAllUserBooksInTeam(user_id);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        return list;
    }
}