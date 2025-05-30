using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserAchievementsService : IUserAchievementsService
{
    private IUserAchievementsRepository _userAchievementsRepository;

    public UserAchievementsService(IUserAchievementsRepository achievementsService)
    {
        _userAchievementsRepository = achievementsService;
    }

    public static UserAchievementsService Create()
    {
        return new UserAchievementsService(new UserAchievementsRepository());
    }

    public Achievements GetNewLevelPower(Achievements c, double coefficient)
    {
        // Achievements orginCard = new Achievements();
        IAchievementsRepository _repository = new AchievementsRepository();
        AchievementsService _service = new AchievementsService(_repository);
        Achievements orginCard = _service.GetAchievementsById(c.id);
        Achievements achievements = new Achievements
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
        achievements.power = EvaluatePower.CalculatePower(
            achievements.health,
            achievements.physical_attack, achievements.physical_defense,
            achievements.magical_attack, achievements.magical_defense,
            achievements.chemical_attack, achievements.chemical_defense,
            achievements.atomic_attack, achievements.atomic_defense,
            achievements.mental_attack, achievements.mental_defense,
            achievements.speed,
            achievements.critical_damage_rate, achievements.critical_rate,
            achievements.penetration_rate, achievements.evasion_rate,
            achievements.damage_absorption_rate, achievements.vitality_regeneration_rate,
            achievements.accuracy_rate, achievements.lifesteal_rate,
            achievements.shield_strength, achievements.tenacity, achievements.resistance_rate,
            achievements.combo_rate, achievements.reflection_rate,
            achievements.mana, achievements.mana_regeneration_rate,
            achievements.damage_to_different_faction_rate, achievements.resistance_to_different_faction_rate,
            achievements.damage_to_same_faction_rate, achievements.resistance_to_same_faction_rate
        );
        return achievements;
    }
    public Achievements GetNewBreakthroughPower(Achievements c, double coefficient)
    {
        IAchievementsRepository _repository = new AchievementsRepository();
        AchievementsService _service = new AchievementsService(_repository);
        Achievements orginCard = _service.GetAchievementsById(c.id);
        Achievements achievements = new Achievements
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
        achievements.power = EvaluatePower.CalculatePower(
            achievements.health,
            achievements.physical_attack, achievements.physical_defense,
            achievements.magical_attack, achievements.magical_defense,
            achievements.chemical_attack, achievements.chemical_defense,
            achievements.atomic_attack, achievements.atomic_defense,
            achievements.mental_attack, achievements.mental_defense,
            achievements.speed,
            achievements.critical_damage_rate, achievements.critical_rate,
            achievements.penetration_rate, achievements.evasion_rate,
            achievements.damage_absorption_rate, achievements.vitality_regeneration_rate,
            achievements.accuracy_rate, achievements.lifesteal_rate,
            achievements.shield_strength, achievements.tenacity, achievements.resistance_rate,
            achievements.combo_rate, achievements.reflection_rate,
            achievements.mana, achievements.mana_regeneration_rate,
            achievements.damage_to_different_faction_rate, achievements.resistance_to_different_faction_rate,
            achievements.damage_to_same_faction_rate, achievements.resistance_to_same_faction_rate
        );
        return achievements;
    }

    public List<Achievements> GetUserAchievements(string user_id, int pageSize, int offset)
    {
        List<Achievements> list = _userAchievementsRepository.GetUserAchievements(user_id, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserCollaborationCount(string user_id)
    {
        return _userAchievementsRepository.GetUserCollaborationCount(user_id);
    }

    public bool InsertUserAchievements(Achievements Achievements)
    {
        return _userAchievementsRepository.InsertUserAchievements(Achievements);
    }

    public bool UpdateAchievementLevel(Achievements achievements, int cardLevel)
    {
        return _userAchievementsRepository.UpdateAchievementLevel(achievements, cardLevel);
    }

    public bool UpdateAchievementsBreakthrough(Achievements achievements, int star, int quantity)
    {
        return _userAchievementsRepository.UpdateAchievementsBreakthrough(achievements, star, quantity);
    }

    public Achievements GetUserAchievementsById(string user_id, string Id)
    {
        return _userAchievementsRepository.GetUserAchievementsById(user_id, Id);
    }

    public Achievements SumPowerUserAchievements()
    {
        return _userAchievementsRepository.SumPowerUserAchievements();
    }
}