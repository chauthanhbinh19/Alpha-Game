using System.Collections.Generic;

public class UserPetsService : IUserPetsService
{
    private readonly IUserPetsRepository _userPetsRepository;

    public UserPetsService(IUserPetsRepository userPetsRepository)
    {
        _userPetsRepository = userPetsRepository;
    }

    public static UserPetsService Create()
    {
        return new UserPetsService(new UserPetsRepository());
    }

    public List<Pets> GetFinalPower(string user_id, List<Pets> PetsList)
    {
        // PowerManager powerManager = new PowerManager();
        IPowerManagerRepository powerManagerRepository = new PowerManagerRepository();
        PowerManagerService powerManagerService = new PowerManagerService(powerManagerRepository);
        PowerManager powerManager = powerManagerService.GetUserStats(user_id);
        foreach (var c in PetsList)
        {
            // Pets card = new Pets();
            Pets card = _userPetsRepository.GetUserPetsById(user_id, c.id);
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
            c.all_penetration_rate = c.all_penetration_rate + powerManager.penetration_rate;
            c.all_evasion_rate = c.all_evasion_rate + powerManager.evasion_rate;
            c.all_damage_absorption_rate = c.all_damage_absorption_rate + powerManager.damage_absorption_rate;
            c.all_vitality_regeneration_rate = c.all_vitality_regeneration_rate + powerManager.vitality_regeneration_rate;
            c.all_accuracy_rate = c.all_accuracy_rate + powerManager.accuracy_rate;
            c.all_lifesteal_rate = c.all_lifesteal_rate + powerManager.lifesteal_rate;
            c.all_shield_strength = c.all_shield_strength + powerManager.shield_strength;
            c.all_tenacity = c.all_tenacity + powerManager.tenacity;
            c.all_resistance_rate = c.all_resistance_rate + powerManager.resistance_rate;
            c.all_combo_rate = c.all_combo_rate + powerManager.combo_rate;
            c.all_reflection_rate = c.all_reflection_rate + powerManager.reflection_rate;
            c.all_mana = c.all_mana + powerManager.mana;
            c.all_mana_regeneration_rate = c.all_mana_regeneration_rate + powerManager.mana_regeneration_rate;
            c.all_damage_to_different_faction_rate = c.all_damage_to_different_faction_rate + powerManager.damage_to_different_faction_rate;
            c.all_resistance_to_different_faction_rate = c.all_resistance_to_different_faction_rate + powerManager.resistance_to_different_faction_rate;
            c.all_damage_to_same_faction_rate = c.all_damage_to_same_faction_rate + powerManager.damage_to_same_faction_rate;
            c.all_resistance_to_same_faction_rate = c.all_resistance_to_same_faction_rate + powerManager.resistance_to_same_faction_rate;

            c.all_power = EvaluatePower.CalculatePower(
            c.all_health,
            c.all_physical_attack, c.all_physical_defense,
            c.all_magical_attack, c.all_magical_defense,
            c.all_chemical_attack, c.all_chemical_defense,
            c.all_atomic_attack, c.all_atomic_defense,
            c.all_mental_attack, c.all_mental_defense,
            c.all_speed,
            c.all_critical_damage_rate, c.all_critical_rate,
            c.all_penetration_rate, c.all_evasion_rate,
            c.all_damage_absorption_rate, c.all_vitality_regeneration_rate,
            c.all_accuracy_rate, c.all_lifesteal_rate,
            c.all_shield_strength, c.all_tenacity, c.all_resistance_rate,
            c.all_combo_rate, c.all_reflection_rate,
            c.all_mana, c.all_mana_regeneration_rate,
            c.all_damage_to_different_faction_rate, c.all_resistance_to_different_faction_rate,
            c.all_damage_to_same_faction_rate, c.all_resistance_to_same_faction_rate
        );
        }
        return PetsList;
    }
    public List<Pets> GetAllEquipmentPower(string user_id, List<Pets> PetsList)
    {
        Equipments equipments = new Equipments();
        IUserEquipmentsRepository userEquipmentsRepository = new UserEquipmentsRepository();
        UserEquipmentsService userEquipmentsService = new UserEquipmentsService(userEquipmentsRepository);
        foreach (var c in PetsList)
        {
            equipments = userEquipmentsService.GetAllEquipmentsByPetsId(user_id, c.id);
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
            c.all_penetration_rate = c.all_penetration_rate + equipments.penetration_rate;
            c.all_evasion_rate = c.all_evasion_rate + equipments.evasion_rate;
            c.all_damage_absorption_rate = c.all_damage_absorption_rate + equipments.damage_absorption_rate;
            c.all_vitality_regeneration_rate = c.all_vitality_regeneration_rate + equipments.vitality_regeneration_rate;
            c.all_accuracy_rate = c.all_accuracy_rate + equipments.accuracy_rate;
            c.all_lifesteal_rate = c.all_lifesteal_rate + equipments.lifesteal_rate;
            c.all_shield_strength = c.all_shield_strength + equipments.shield_strength;
            c.all_tenacity = c.all_tenacity + equipments.tenacity;
            c.all_resistance_rate = c.all_resistance_rate + equipments.resistance_rate;
            c.all_combo_rate = c.all_combo_rate + equipments.combo_rate;
            c.all_reflection_rate = c.all_reflection_rate + equipments.reflection_rate;
            c.all_mana = c.all_mana + equipments.mana;
            c.all_mana_regeneration_rate = c.all_mana_regeneration_rate + equipments.mana_regeneration_rate;
            c.all_damage_to_different_faction_rate = c.all_damage_to_different_faction_rate + equipments.damage_to_different_faction_rate;
            c.all_resistance_to_different_faction_rate = c.all_resistance_to_different_faction_rate + equipments.resistance_to_different_faction_rate;
            c.all_damage_to_same_faction_rate = c.all_damage_to_same_faction_rate + equipments.damage_to_same_faction_rate;
            c.all_resistance_to_same_faction_rate = c.all_resistance_to_same_faction_rate + equipments.resistance_to_same_faction_rate;

            c.all_power = EvaluatePower.CalculatePower(
            c.all_health,
            c.all_physical_attack, c.all_physical_defense,
            c.all_magical_attack, c.all_magical_defense,
            c.all_chemical_attack, c.all_chemical_defense,
            c.all_atomic_attack, c.all_atomic_defense,
            c.all_mental_attack, c.all_mental_defense,
            c.all_speed,
            c.all_critical_damage_rate, c.all_critical_rate,
            c.all_penetration_rate, c.all_evasion_rate,
            c.all_damage_absorption_rate, c.all_vitality_regeneration_rate,
            c.all_accuracy_rate, c.all_lifesteal_rate,
            c.all_shield_strength, c.all_tenacity, c.all_resistance_rate,
            c.all_combo_rate, c.all_reflection_rate,
            c.all_mana, c.all_mana_regeneration_rate,
            c.all_damage_to_different_faction_rate, c.all_resistance_to_different_faction_rate,
            c.all_damage_to_same_faction_rate, c.all_resistance_to_same_faction_rate
        );
        }
        return PetsList;
    }
    public List<Pets> GetAllRankPower(string user_id, List<Pets> PetsList)
    {
        IUserPetsRankRepository userPetsRankRepository = new UserPetsRankRepository();
        UserPetsRankService userPetsRankService = new UserPetsRankService(userPetsRankRepository);
        foreach (var c in PetsList)
        {
            Pets card = _userPetsRepository.GetUserPetsById(user_id, c.id);
            Rank rank = userPetsRankService.GetSumPetsRank(user_id, c.id);
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
            c.all_penetration_rate = c.all_penetration_rate + rank.penetration_rate;
            c.all_evasion_rate = c.all_evasion_rate + rank.evasion_rate;
            c.all_damage_absorption_rate = c.all_damage_absorption_rate + rank.damage_absorption_rate;
            c.all_vitality_regeneration_rate = c.all_vitality_regeneration_rate + rank.vitality_regeneration_rate;
            c.all_accuracy_rate = c.all_accuracy_rate + rank.accuracy_rate;
            c.all_lifesteal_rate = c.all_lifesteal_rate + rank.lifesteal_rate;
            c.all_shield_strength = c.all_shield_strength + rank.shield_strength;
            c.all_tenacity = c.all_tenacity + rank.tenacity;
            c.all_resistance_rate = c.all_resistance_rate + rank.resistance_rate;
            c.all_combo_rate = c.all_combo_rate + rank.combo_rate;
            c.all_reflection_rate = c.all_reflection_rate + rank.reflection_rate;
            c.all_mana = c.all_mana + rank.mana;
            c.all_mana_regeneration_rate = c.all_mana_regeneration_rate + rank.mana_regeneration_rate;
            c.all_damage_to_different_faction_rate = c.all_damage_to_different_faction_rate + rank.damage_to_different_faction_rate;
            c.all_resistance_to_different_faction_rate = c.all_resistance_to_different_faction_rate + rank.resistance_to_different_faction_rate;
            c.all_damage_to_same_faction_rate = c.all_damage_to_same_faction_rate + rank.damage_to_same_faction_rate;
            c.all_resistance_to_same_faction_rate = c.all_resistance_to_same_faction_rate + rank.resistance_to_same_faction_rate;

            c.all_power = EvaluatePower.CalculatePower(
            c.all_health,
            c.all_physical_attack, c.all_physical_defense,
            c.all_magical_attack, c.all_magical_defense,
            c.all_chemical_attack, c.all_chemical_defense,
            c.all_atomic_attack, c.all_atomic_defense,
            c.all_mental_attack, c.all_mental_defense,
            c.all_speed,
            c.all_critical_damage_rate, c.all_critical_rate,
            c.all_penetration_rate, c.all_evasion_rate,
            c.all_damage_absorption_rate, c.all_vitality_regeneration_rate,
            c.all_accuracy_rate, c.all_lifesteal_rate,
            c.all_shield_strength, c.all_tenacity, c.all_resistance_rate,
            c.all_combo_rate, c.all_reflection_rate,
            c.all_mana, c.all_mana_regeneration_rate,
            c.all_damage_to_different_faction_rate, c.all_resistance_to_different_faction_rate,
            c.all_damage_to_same_faction_rate, c.all_resistance_to_same_faction_rate
        );
        }
        return PetsList;
    }
    public List<Pets> GetAllAnimeStatsPower(string user_id, List<Pets> PetsList)
    {
        IAnimeStatsRepository animeStatsRepository = new AnimeStatsRepository();
        AnimeStatsService animeStatsService = new AnimeStatsService(animeStatsRepository);
        foreach (var c in PetsList)
        {
            Pets card = _userPetsRepository.GetUserPetsById(user_id, c.id);
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
            c.all_penetration_rate = c.all_penetration_rate + animeStats.penetration_rate;
            c.all_evasion_rate = c.all_evasion_rate + animeStats.evasion_rate;
            c.all_damage_absorption_rate = c.all_damage_absorption_rate + animeStats.damage_absorption_rate;
            c.all_vitality_regeneration_rate = c.all_vitality_regeneration_rate + animeStats.vitality_regeneration_rate;
            c.all_accuracy_rate = c.all_accuracy_rate + animeStats.accuracy_rate;
            c.all_lifesteal_rate = c.all_lifesteal_rate + animeStats.lifesteal_rate;
            c.all_shield_strength = c.all_shield_strength + animeStats.shield_strength;
            c.all_tenacity = c.all_tenacity + animeStats.tenacity;
            c.all_resistance_rate = c.all_resistance_rate + animeStats.resistance_rate;
            c.all_combo_rate = c.all_combo_rate + animeStats.combo_rate;
            c.all_reflection_rate = c.all_reflection_rate + animeStats.reflection_rate;
            c.all_mana = c.all_mana + animeStats.mana;
            c.all_mana_regeneration_rate = c.all_mana_regeneration_rate + animeStats.mana_regeneration_rate;
            c.all_damage_to_different_faction_rate = c.all_damage_to_different_faction_rate + animeStats.damage_to_different_faction_rate;
            c.all_resistance_to_different_faction_rate = c.all_resistance_to_different_faction_rate + animeStats.resistance_to_different_faction_rate;
            c.all_damage_to_same_faction_rate = c.all_damage_to_same_faction_rate + animeStats.damage_to_same_faction_rate;
            c.all_resistance_to_same_faction_rate = c.all_resistance_to_same_faction_rate + animeStats.resistance_to_same_faction_rate;

            c.all_power = EvaluatePower.CalculatePower(
            c.all_health,
            c.all_physical_attack, c.all_physical_defense,
            c.all_magical_attack, c.all_magical_defense,
            c.all_chemical_attack, c.all_chemical_defense,
            c.all_atomic_attack, c.all_atomic_defense,
            c.all_mental_attack, c.all_mental_defense,
            c.all_speed,
            c.all_critical_damage_rate, c.all_critical_rate,
            c.all_penetration_rate, c.all_evasion_rate,
            c.all_damage_absorption_rate, c.all_vitality_regeneration_rate,
            c.all_accuracy_rate, c.all_lifesteal_rate,
            c.all_shield_strength, c.all_tenacity, c.all_resistance_rate,
            c.all_combo_rate, c.all_reflection_rate,
            c.all_mana, c.all_mana_regeneration_rate,
            c.all_damage_to_different_faction_rate, c.all_resistance_to_different_faction_rate,
            c.all_damage_to_same_faction_rate, c.all_resistance_to_same_faction_rate
        );
        }
        return PetsList;
    }
    public Pets GetNewLevelPower(Pets c, double coefficient)
    {
        IPetsRepository _repository = new PetsRepository();
        PetsService _service = new PetsService(_repository);
        Pets orginCard = _service.GetPetsById(c.id);
        Pets pets = new Pets
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
        pets.power = EvaluatePower.CalculatePower(
            pets.health,
            pets.physical_attack, pets.physical_defense,
            pets.magical_attack, pets.magical_defense,
            pets.chemical_attack, pets.chemical_defense,
            pets.atomic_attack, pets.atomic_defense,
            pets.mental_attack, pets.mental_defense,
            pets.speed,
            pets.critical_damage_rate, pets.critical_rate,
            pets.penetration_rate, pets.evasion_rate,
            pets.damage_absorption_rate, pets.vitality_regeneration_rate,
            pets.accuracy_rate, pets.lifesteal_rate,
            pets.shield_strength, pets.tenacity, pets.resistance_rate,
            pets.combo_rate, pets.reflection_rate,
            pets.mana, pets.mana_regeneration_rate,
            pets.damage_to_different_faction_rate, pets.resistance_to_different_faction_rate,
            pets.damage_to_same_faction_rate, pets.resistance_to_same_faction_rate
        );
        return pets;
    }
    public Pets GetNewBreakthroughPower(Pets c, double coefficient)
    {
        IPetsRepository _repository = new PetsRepository();
        PetsService _service = new PetsService(_repository);
        Pets orginCard = _service.GetPetsById(c.id);
        Pets pets = new Pets
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
        pets.power = EvaluatePower.CalculatePower(
            pets.health,
            pets.physical_attack, pets.physical_defense,
            pets.magical_attack, pets.magical_defense,
            pets.chemical_attack, pets.chemical_defense,
            pets.atomic_attack, pets.atomic_defense,
            pets.mental_attack, pets.mental_defense,
            pets.speed,
            pets.critical_damage_rate, pets.critical_rate,
            pets.penetration_rate, pets.evasion_rate,
            pets.damage_absorption_rate, pets.vitality_regeneration_rate,
            pets.accuracy_rate, pets.lifesteal_rate,
            pets.shield_strength, pets.tenacity, pets.resistance_rate,
            pets.combo_rate, pets.reflection_rate,
            pets.mana, pets.mana_regeneration_rate,
            pets.damage_to_different_faction_rate, pets.resistance_to_different_faction_rate,
            pets.damage_to_same_faction_rate, pets.resistance_to_same_faction_rate
        );
        return pets;
    }

    public List<Pets> GetUserPets(string user_id, string type, int pageSize, int offset)
    {
        List<Pets> list = _userPetsRepository.GetUserPets(user_id, type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        return list;
    }

    public List<Pets> GetUserPetsTeam(string user_id, string teamId)
    {
        List<Pets> list = _userPetsRepository.GetUserPetsTeam(user_id, teamId);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        return list;
    }

    public Dictionary<string, int> GetUniquePetTypesTeam(string teamId)
    {
        return _userPetsRepository.GetUniquePetTypesTeam(teamId);
    }

    public int GetUserPetsCount(string user_id, string type)
    {
        return _userPetsRepository.GetUserPetsCount(user_id, type);
    }

    public bool InsertUserPets(Pets pets)
    {
        return _userPetsRepository.InsertUserPets(pets);
    }

    public bool UpdatePetsLevel(Pets pets, int cardLevel)
    {
        return _userPetsRepository.UpdatePetsLevel(pets, cardLevel);
    }

    public bool UpdatePetsBreakthrough(Pets pets, int star, int quantity)
    {
        return _userPetsRepository.UpdatePetsBreakthrough(pets, star, quantity);
    }

    public bool InsertFactPets(Pets pets)
    {
        return _userPetsRepository.InsertFactPets(pets);
    }

    public bool UpdateFactPets(Pets pets)
    {
        return _userPetsRepository.UpdateFactPets(pets);
    }

    public bool UpdateTeamFactCardPets(string team_id, string card_id)
    {
        return _userPetsRepository.UpdateTeamFactCardPets(team_id, card_id);
    }

    public Pets GetUserPetsById(string user_id, string Id)
    {
        return _userPetsRepository.GetUserPetsById(user_id, Id);
    }

    public List<Pets> GetAllUserPetsInTeam(string user_id)
    {
        List<Pets> list = _userPetsRepository.GetAllUserPetsInTeam(user_id);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        return list;
    }
}
