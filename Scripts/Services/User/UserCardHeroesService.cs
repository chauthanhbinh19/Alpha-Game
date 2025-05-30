using System.Collections.Generic;

public class UserCardHeroesService : IUserCardHeroesService
{
    private readonly IUserCardHeroesRepository _userCardHeroesRepository;

    public UserCardHeroesService(IUserCardHeroesRepository userCardHeroesRepository)
    {
        _userCardHeroesRepository = userCardHeroesRepository;
    }

    public static UserCardHeroesService Create()
    {
        return new UserCardHeroesService(new UserCardHeroesRepository());
    }

    public List<CardHeroes> GetFinalPower(string user_id, List<CardHeroes> CardHeroesList)
    {
        IPowerManagerRepository powerManagerRepository = new PowerManagerRepository();
        PowerManagerService powerManagerService = new PowerManagerService(powerManagerRepository);
        PowerManager powerManager = powerManagerService.GetUserStats(user_id);
        foreach (var c in CardHeroesList)
        {
            CardHeroes card = new CardHeroes();
            card = _userCardHeroesRepository.GetUserCardHeroesById(user_id, c.id);
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
        return CardHeroesList;
    }
    public List<CardHeroes> GetAllEquipmentPower(string user_id, List<CardHeroes> CardHeroesList)
    {
        Equipments equipments = new Equipments();
        IUserEquipmentsRepository userEquipmentsRepository = new UserEquipmentsRepository();
        UserEquipmentsService userEquipmentsService = new UserEquipmentsService(userEquipmentsRepository);
        foreach (var c in CardHeroesList)
        {
            equipments = userEquipmentsService.GetAllEquipmentsByCardHeoresId(user_id, c.id);
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
        return CardHeroesList;
    }
    public List<CardHeroes> GetAllRankPower(string user_id, List<CardHeroes> CardHeroesList)
    {
        IUserCardHeroesRankRepository userCardHeroesRankRepository = new UserCardHeroesRankRepository();
        UserCardHeroesRankService userCardHeroesRankService = new UserCardHeroesRankService(userCardHeroesRankRepository);
        foreach (var c in CardHeroesList)
        {
            CardHeroes card = _userCardHeroesRepository.GetUserCardHeroesById(user_id, c.id);
            Rank rank = userCardHeroesRankService.GetSumCardHeroesRank(user_id, c.id);
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
        return CardHeroesList;
    }
    public List<CardHeroes> GetAllAnimeStatsPower(string user_id, List<CardHeroes> CardHeroesList)
    {
        IAnimeStatsRepository animeStatsRepository = new AnimeStatsRepository();
        AnimeStatsService animeStatsService = new AnimeStatsService(animeStatsRepository);
        foreach (var c in CardHeroesList)
        {
            CardHeroes card = _userCardHeroesRepository.GetUserCardHeroesById(user_id, c.id);
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
        return CardHeroesList;
    }
    public CardHeroes GetNewLevelPower(CardHeroes c, double coefficient)
    {
        ICardHeroesRepository _repository = new CardHeroesRepository();
        CardHeroesService _service = new CardHeroesService(_repository);
        CardHeroes orginCard = _service.GetCardHeroesById(c.id);
        CardHeroes cardHeroes = new CardHeroes
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
        cardHeroes.power = EvaluatePower.CalculatePower(
            cardHeroes.health,
            cardHeroes.physical_attack, cardHeroes.physical_defense,
            cardHeroes.magical_attack, cardHeroes.magical_defense,
            cardHeroes.chemical_attack, cardHeroes.chemical_defense,
            cardHeroes.atomic_attack, cardHeroes.atomic_defense,
            cardHeroes.mental_attack, cardHeroes.mental_defense,
            cardHeroes.speed,
            cardHeroes.critical_damage_rate, cardHeroes.critical_rate,
            cardHeroes.penetration_rate, cardHeroes.evasion_rate,
            cardHeroes.damage_absorption_rate, cardHeroes.vitality_regeneration_rate,
            cardHeroes.accuracy_rate, cardHeroes.lifesteal_rate,
            cardHeroes.shield_strength, cardHeroes.tenacity, cardHeroes.resistance_rate,
            cardHeroes.combo_rate, cardHeroes.reflection_rate,
            cardHeroes.mana, cardHeroes.mana_regeneration_rate,
            cardHeroes.damage_to_different_faction_rate, cardHeroes.resistance_to_different_faction_rate,
            cardHeroes.damage_to_same_faction_rate, cardHeroes.resistance_to_same_faction_rate
        );
        return cardHeroes;
    }
    public CardHeroes GetNewBreakthroughPower(CardHeroes c, double coefficient)
    {
        ICardHeroesRepository _repository = new CardHeroesRepository();
        CardHeroesService _service = new CardHeroesService(_repository);
        CardHeroes orginCard = _service.GetCardHeroesById(c.id);
        CardHeroes cardHeroes = new CardHeroes
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
        cardHeroes.power = EvaluatePower.CalculatePower(
            cardHeroes.health,
            cardHeroes.physical_attack, cardHeroes.physical_defense,
            cardHeroes.magical_attack, cardHeroes.magical_defense,
            cardHeroes.chemical_attack, cardHeroes.chemical_defense,
            cardHeroes.atomic_attack, cardHeroes.atomic_defense,
            cardHeroes.mental_attack, cardHeroes.mental_defense,
            cardHeroes.speed,
            cardHeroes.critical_damage_rate, cardHeroes.critical_rate,
            cardHeroes.penetration_rate, cardHeroes.evasion_rate,
            cardHeroes.damage_absorption_rate, cardHeroes.vitality_regeneration_rate,
            cardHeroes.accuracy_rate, cardHeroes.lifesteal_rate,
            cardHeroes.shield_strength, cardHeroes.tenacity, cardHeroes.resistance_rate,
            cardHeroes.combo_rate, cardHeroes.reflection_rate,
            cardHeroes.mana, cardHeroes.mana_regeneration_rate,
            cardHeroes.damage_to_different_faction_rate, cardHeroes.resistance_to_different_faction_rate,
            cardHeroes.damage_to_same_faction_rate, cardHeroes.resistance_to_same_faction_rate
        );
        return cardHeroes;
    }

    public List<CardHeroes> GetUserCardHeroes(string user_id, string type, int pageSize, int offset)
    {
        List<CardHeroes> list = _userCardHeroesRepository.GetUserCardHeroes(user_id, type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        return list;
    }

    public List<CardHeroes> GetUserCardHeroesTeam(string user_id, string teamId, string position)
    {
        List<CardHeroes> list = _userCardHeroesRepository.GetUserCardHeroesTeam(user_id, teamId, position);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        return list;
    }

    public Dictionary<string, int> GetUniqueCardHeroTypesTeam(string teamId)
    {
        return _userCardHeroesRepository.GetUniqueCardHeroTypesTeam(teamId);
    }

    public int GetUserCardHeroesCount(string user_id, string type)
    {
        return _userCardHeroesRepository.GetUserCardHeroesCount(user_id, type);
    }

    public int GetUserCardHeroesTeamsPositionCount(string user_id, string team_id, string position)
    {
        return _userCardHeroesRepository.GetUserCardHeroesTeamsPositionCount(user_id, team_id, position);
    }

    public bool InsertUserCardHeroes(CardHeroes CardHeroes)
    {
        return _userCardHeroesRepository.InsertUserCardHeroes(CardHeroes);
    }

    public bool UpdateCardHeroesLevel(CardHeroes cardHeroes, int cardLevel)
    {
        return _userCardHeroesRepository.UpdateCardHeroesLevel(cardHeroes, cardLevel);
    }

    public bool UpdateCardHeroesBreakthrough(CardHeroes cardHeroes, int star, int quantity)
    {
        return _userCardHeroesRepository.UpdateCardHeroesBreakthrough(cardHeroes, star, quantity);
    }

    public bool InsertFactCardHeroes(CardHeroes cardHeroes)
    {
        return _userCardHeroesRepository.InsertFactCardHeroes(cardHeroes);
    }

    public bool UpdateFactCardHeroes(CardHeroes cardHeroes)
    {
        return _userCardHeroesRepository.UpdateFactCardHeroes(cardHeroes);
    }

    public bool UpdateTeamFactCardHeroes(string team_id, string position, string card_id)
    {
        return _userCardHeroesRepository.UpdateTeamFactCardHeroes(team_id, position, card_id);
    }

    public CardHeroes GetUserCardHeroesById(string user_id, string Id)
    {
        return _userCardHeroesRepository.GetUserCardHeroesById(user_id, Id);
    }

    public List<CardHeroes> GetAllUserCardHeroesInTeam(string user_id)
    {
        List<CardHeroes> list = _userCardHeroesRepository.GetAllUserCardHeroesInTeam(user_id);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        return list;
    }
}
