using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class Pets
{
    public int id { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public string rare { get; set; }
    public string type { get; set; }
    public int star { get; set; }
    public int level { get; set; }
    public int experiment { get; set; }
    public int quantity { get; set; }
    public int block { get; set; }
    public string position { get; set; }
    public double power { get; set; }
    public double health { get; set; }
    public double physical_attack { get; set; }
    public double physical_defense { get; set; }
    public double magical_attack { get; set; }
    public double magical_defense { get; set; }
    public double chemical_attack { get; set; }
    public double chemical_defense { get; set; }
    public double atomic_attack { get; set; }
    public double atomic_defense { get; set; }
    public double mental_attack { get; set; }
    public double mental_defense { get; set; }
    public double speed { get; set; }
    public double critical_damage_rate { get; set; }
    public double critical_rate { get; set; }
    public double penetration_rate { get; set; }
    public double evasion_rate { get; set; }
    public double damage_absorption_rate { get; set; }
    public double vitality_regeneration_rate { get; set; }
    public double accuracy_rate { get; set; }
    public double lifesteal_rate { get; set; }
    public float mana { get; set; }
    public double mana_regeneration_rate { get; set; }
    public double shield_strength { get; set; }
    public double tenacity { get; set; }
    public double resistance_rate { get; set; }
    public double combo_rate { get; set; }
    public double reflection_rate { get; set; }
    public double damage_to_different_faction_rate { get; set; }
    public double resistance_to_different_faction_rate { get; set; }
    public double damage_to_same_faction_rate { get; set; }
    public double resistance_to_same_faction_rate { get; set; }
    public double all_power { get; set; }
    public double all_health { get; set; }
    public double all_physical_attack { get; set; }
    public double all_physical_defense { get; set; }
    public double all_magical_attack { get; set; }
    public double all_magical_defense { get; set; }
    public double all_chemical_attack { get; set; }
    public double all_chemical_defense { get; set; }
    public double all_atomic_attack { get; set; }
    public double all_atomic_defense { get; set; }
    public double all_mental_attack { get; set; }
    public double all_mental_defense { get; set; }
    public double all_speed { get; set; }
    public double all_critical_damage_rate { get; set; }
    public double all_critical_rate { get; set; }
    public double all_penetration_rate { get; set; }
    public double all_evasion_rate { get; set; }
    public double all_damage_absorption_rate { get; set; }
    public double all_vitality_regeneration_rate { get; set; }
    public double all_accuracy_rate { get; set; }
    public double all_lifesteal_rate { get; set; }
    public float all_mana { get; set; }
    public double all_mana_regeneration_rate { get; set; }
    public double all_shield_strength { get; set; }
    public double all_tenacity { get; set; }
    public double all_resistance_rate { get; set; }
    public double all_combo_rate { get; set; }
    public double all_reflection_rate { get; set; }
    public double all_damage_to_different_faction_rate { get; set; }
    public double all_resistance_to_different_faction_rate { get; set; }
    public double all_damage_to_same_faction_rate { get; set; }
    public double all_resistance_to_same_faction_rate { get; set; }
    public string description { get; set; }
    public int team_id { get; set; }
    public string status { get; set; }
    public double percent_all_health { get; set; }
    public double percent_all_physical_attack { get; set; }
    public double percent_all_physical_defense { get; set; }
    public double percent_all_magical_attack { get; set; }
    public double percent_all_magical_defense { get; set; }
    public double percent_all_chemical_attack { get; set; }
    public double percent_all_chemical_defense { get; set; }
    public double percent_all_atomic_attack { get; set; }
    public double percent_all_atomic_defense { get; set; }
    public double percent_all_mental_attack { get; set; }
    public double percent_all_mental_defense { get; set; }
    public Currency currency { get; set; }
    public Pets()
    {
        power = -1;
        health = -1;
        physical_attack = -1;
        physical_defense = -1;
        magical_attack = -1;
        magical_defense = -1;
        chemical_attack = -1;
        chemical_defense = -1;
        atomic_attack = -1;
        atomic_defense = -1;
        mental_attack = -1;
        mental_defense = -1;
        speed = -1;
        critical_damage_rate = -1;
        critical_rate = -1;
        penetration_rate = -1;
        evasion_rate = -1;
        damage_absorption_rate = -1;
        vitality_regeneration_rate = -1;
        accuracy_rate = -1;
        lifesteal_rate = -1;
        mana = -1;
        mana_regeneration_rate = -1;
        shield_strength = -1;
        tenacity = -1;
        resistance_rate = -1;
        combo_rate = -1;
        reflection_rate = -1;
        damage_to_different_faction_rate = -1;
        resistance_to_different_faction_rate = -1;
        damage_to_same_faction_rate = -1;
        resistance_to_same_faction_rate = -1;

        all_power = -1;
        all_health = -1;
        all_physical_attack = -1;
        all_physical_defense = -1;
        all_magical_attack = -1;
        all_magical_defense = -1;
        all_chemical_attack = -1;
        all_chemical_defense = -1;
        all_atomic_attack = -1;
        all_atomic_defense = -1;
        all_mental_attack = -1;
        all_mental_defense = -1;
        all_speed = -1;
        all_critical_damage_rate = -1;
        all_critical_rate = -1;
        all_penetration_rate = -1;
        all_evasion_rate = -1;
        all_damage_absorption_rate = -1;
        all_vitality_regeneration_rate = -1;
        all_accuracy_rate = -1;
        all_lifesteal_rate = -1;
        all_mana = -1;
        all_mana_regeneration_rate = -1;
        all_shield_strength = -1;
        all_tenacity = -1;
        all_resistance_rate = -1;
        all_combo_rate = -1;
        all_reflection_rate = -1;
        all_damage_to_different_faction_rate = -1;
        all_resistance_to_different_faction_rate = -1;
        all_damage_to_same_faction_rate = -1;
        all_resistance_to_same_faction_rate = -1;

        team_id = -1;
        percent_all_health = -1;
        percent_all_physical_attack = -1;
        percent_all_physical_defense = -1;
        percent_all_magical_attack = -1;
        percent_all_magical_defense = -1;
        percent_all_chemical_attack = -1;
        percent_all_chemical_defense = -1;
        percent_all_atomic_attack = -1;
        percent_all_atomic_defense = -1;
        percent_all_mental_attack = -1;
        percent_all_mental_defense = -1;
    }
    public List<Pets> GetFinalPower(List<Pets> PetsList)
    {
        PowerManager powerManager = new PowerManager();
        powerManager = powerManager.GetUserStats();
        foreach (var c in PetsList)
        {
            Pets card = new Pets();
            card = card.GetUserPetsById(c.id);
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

            c.power = PowerManager.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return PetsList;
    }
    public List<Pets> GetAllEquipmentPower(List<Pets> PetsList)
    {
        Equipments equipments = new Equipments();
        foreach (var c in PetsList)
        {
            equipments = equipments.GetAllEquipmentsByPetsId(c.id);
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

            c.power = PowerManager.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return PetsList;
    }
    public List<Pets> GetAllRankPower(List<Pets> PetsList)
    {
        Rank rank = new Rank();
        foreach (var c in PetsList)
        {
            Pets card = new Pets();
            card = card.GetUserPetsById(c.id);
            rank = rank.GetSumPetsRank(c.id);
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

            c.power = PowerManager.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return PetsList;
    }
    public Pets GetNewLevelPower(Pets c, double coefficient)
    {
        Pets orginCard = new Pets();
        orginCard = orginCard.GetPetsById(c.id);
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
        pets.power = PowerManager.CalculatePower(
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
        Pets orginCard = new Pets();
        orginCard = orginCard.GetPetsById(c.id);
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
        pets.power = PowerManager.CalculatePower(
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
    public static List<string> GetUniquePetsTypes()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "Select distinct type from Pets";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                typeList.Add(reader.GetString(0));
            }
        }
        return typeList;
    }
    public List<Pets> GetPets(string type, int pageSize, int offset)
    {
        List<Pets> petsList = new List<Pets>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from Pets where type= @type 
                ORDER BY Pets.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(Pets.name, '[0-9]+$') AS UNSIGNED), pets.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Pets pet = new Pets
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        power = reader.GetDouble("power"),
                        health = reader.GetDouble("health"),
                        physical_attack = reader.GetDouble("physical_attack"),
                        physical_defense = reader.GetDouble("physical_defense"),
                        magical_attack = reader.GetDouble("magical_attack"),
                        magical_defense = reader.GetDouble("magical_defense"),
                        chemical_attack = reader.GetDouble("chemical_attack"),
                        chemical_defense = reader.GetDouble("chemical_defense"),
                        atomic_attack = reader.GetDouble("atomic_attack"),
                        atomic_defense = reader.GetDouble("atomic_defense"),
                        mental_attack = reader.GetDouble("mental_attack"),
                        mental_defense = reader.GetDouble("mental_defense"),
                        speed = reader.GetDouble("speed"),
                        critical_damage_rate = reader.GetDouble("critical_damage_rate"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        description = reader.GetString("description")
                    };

                    petsList.Add(pet);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return petsList;
    }
    public int GetPetsCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from Pets where type= @type";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                count = Convert.ToInt32(command.ExecuteScalar());

                return count;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return count;
    }
    public List<Pets> GetPetsCollection(string type, int pageSize, int offset)
    {
        List<Pets> petsList = new List<Pets>();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT p.*, CASE WHEN pg.pet_id IS NULL THEN 'block' WHEN pg.status = 'pending' THEN 'pending' WHEN pg.status = 'available' THEN 'available' END AS status 
                FROM pets p LEFT JOIN pets_gallery pg ON p.id = pg.pet_id and pg.user_id = @userId where p.type=@type 
                ORDER BY p.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(p.name, '[0-9]+$') AS UNSIGNED), p.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Pets pet = new Pets
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        power = reader.GetDouble("power"),
                        health = reader.GetDouble("health"),
                        physical_attack = reader.GetDouble("physical_attack"),
                        physical_defense = reader.GetDouble("physical_defense"),
                        magical_attack = reader.GetDouble("magical_attack"),
                        magical_defense = reader.GetDouble("magical_defense"),
                        chemical_attack = reader.GetDouble("chemical_attack"),
                        chemical_defense = reader.GetDouble("chemical_defense"),
                        atomic_attack = reader.GetDouble("atomic_attack"),
                        atomic_defense = reader.GetDouble("atomic_defense"),
                        mental_attack = reader.GetDouble("mental_attack"),
                        mental_defense = reader.GetDouble("mental_defense"),
                        speed = reader.GetDouble("speed"),
                        critical_damage_rate = reader.GetDouble("critical_damage_rate"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        description = reader.GetString("description"),
                        status = reader.GetString("status"),
                    };

                    petsList.Add(pet);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return petsList;
    }
    public List<Pets> GetUserPets(string type, int pageSize, int offset)
    {
        List<Pets> petsList = new List<Pets>();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT up.*, p.image, p.rare, p.type, fp.*
                FROM user_pets up
                LEFT JOIN Pets p ON p.id = up.pet_id
                LEFT JOIN fact_pets fp ON fp.user_id = up.user_id AND fp.user_pet_id = up.pet_id
                WHERE up.user_id = @userId AND p.type = @type
                ORDER BY p.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(p.name, '[0-9]+$') AS UNSIGNED), p.name
                LIMIT @limit OFFSET @offset;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Pets pet = new Pets
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        team_id = reader.IsDBNull(reader.GetOrdinal("team_id")) ? -1 : reader.GetInt32("team_id"),
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString("position"),
                        power = reader.GetDouble("power"),
                        health = reader.GetDouble("health"),
                        physical_attack = reader.GetDouble("physical_attack"),
                        physical_defense = reader.GetDouble("physical_defense"),
                        magical_attack = reader.GetDouble("magical_attack"),
                        magical_defense = reader.GetDouble("magical_defense"),
                        chemical_attack = reader.GetDouble("chemical_attack"),
                        chemical_defense = reader.GetDouble("chemical_defense"),
                        atomic_attack = reader.GetDouble("atomic_attack"),
                        atomic_defense = reader.GetDouble("atomic_defense"),
                        mental_attack = reader.GetDouble("mental_attack"),
                        mental_defense = reader.GetDouble("mental_defense"),
                        speed = reader.GetDouble("speed"),
                        critical_damage_rate = reader.GetDouble("critical_damage_rate"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        description = reader.GetString("description"),

                        all_power = reader.GetDouble("all_power"),
                        all_health = reader.GetDouble("all_health"),
                        all_physical_attack = reader.GetDouble("all_physical_attack"),
                        all_physical_defense = reader.GetDouble("all_physical_defense"),
                        all_magical_attack = reader.GetDouble("all_magical_attack"),
                        all_magical_defense = reader.GetDouble("all_magical_defense"),
                        all_chemical_attack = reader.GetDouble("all_chemical_attack"),
                        all_chemical_defense = reader.GetDouble("all_chemical_defense"),
                        all_atomic_attack = reader.GetDouble("all_atomic_attack"),
                        all_atomic_defense = reader.GetDouble("all_atomic_defense"),
                        all_mental_attack = reader.GetDouble("all_mental_attack"),
                        all_mental_defense = reader.GetDouble("all_mental_defense"),
                        all_speed = reader.GetDouble("all_speed"),
                        all_critical_damage_rate = reader.GetDouble("all_critical_damage_rate"),
                        all_critical_rate = reader.GetDouble("all_critical_rate"),
                        all_penetration_rate = reader.GetDouble("all_penetration_rate"),
                        all_evasion_rate = reader.GetDouble("all_evasion_rate"),
                        all_damage_absorption_rate = reader.GetDouble("all_damage_absorption_rate"),
                        all_vitality_regeneration_rate = reader.GetDouble("all_vitality_regeneration_rate"),
                        all_accuracy_rate = reader.GetDouble("all_accuracy_rate"),
                        all_lifesteal_rate = reader.GetDouble("all_lifesteal_rate"),
                        all_shield_strength = reader.GetDouble("all_shield_strength"),
                        all_tenacity = reader.GetDouble("all_tenacity"),
                        all_resistance_rate = reader.GetDouble("all_resistance_rate"),
                        all_combo_rate = reader.GetDouble("all_combo_rate"),
                        all_reflection_rate = reader.GetDouble("all_reflection_rate"),
                        all_mana = reader.GetFloat("all_mana"),
                        all_mana_regeneration_rate = reader.GetDouble("all_mana_regeneration_rate"),
                        all_damage_to_different_faction_rate = reader.GetDouble("all_damage_to_different_faction_rate"),
                        all_resistance_to_different_faction_rate = reader.GetDouble("all_resistance_to_different_faction_rate"),
                        all_damage_to_same_faction_rate = reader.GetDouble("all_damage_to_same_faction_rate"),
                        all_resistance_to_same_faction_rate = reader.GetDouble("all_resistance_to_same_faction_rate"),
                    };

                    petsList.Add(pet);
                }
                petsList = GetFinalPower(petsList);
                petsList = GetAllEquipmentPower(petsList);
                petsList = GetAllRankPower(petsList);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return petsList;
    }
    public List<Pets> GetUserPetsTeam(int teamId)
    {
        List<Pets> petsList = new List<Pets>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT up.*, p.image, p.rare, p.type, fp.*
                FROM user_pets up
                LEFT JOIN Pets p ON p.id = up.pet_id
                LEFT JOIN fact_pets fp ON fp.user_id = up.user_id AND fp.user_pet_id = up.pet_id
                WHERE up.user_id = @userId AND fp.team_id = @team_id
                ORDER BY p.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(p.name, '[0-9]+$') AS UNSIGNED), p.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", User.CurrentUserId);
                command.Parameters.AddWithValue("@team_id", teamId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Pets pet = new Pets
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        team_id = reader.IsDBNull(reader.GetOrdinal("team_id")) ? -1 : reader.GetInt32("team_id"),
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString("position"),
                        power = reader.GetDouble("power"),
                        health = reader.GetDouble("health"),
                        physical_attack = reader.GetDouble("physical_attack"),
                        physical_defense = reader.GetDouble("physical_defense"),
                        magical_attack = reader.GetDouble("magical_attack"),
                        magical_defense = reader.GetDouble("magical_defense"),
                        chemical_attack = reader.GetDouble("chemical_attack"),
                        chemical_defense = reader.GetDouble("chemical_defense"),
                        atomic_attack = reader.GetDouble("atomic_attack"),
                        atomic_defense = reader.GetDouble("atomic_defense"),
                        mental_attack = reader.GetDouble("mental_attack"),
                        mental_defense = reader.GetDouble("mental_defense"),
                        speed = reader.GetDouble("speed"),
                        critical_damage_rate = reader.GetDouble("critical_damage_rate"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        description = reader.GetString("description"),

                        all_power = reader.GetDouble("all_power"),
                        all_health = reader.GetDouble("all_health"),
                        all_physical_attack = reader.GetDouble("all_physical_attack"),
                        all_physical_defense = reader.GetDouble("all_physical_defense"),
                        all_magical_attack = reader.GetDouble("all_magical_attack"),
                        all_magical_defense = reader.GetDouble("all_magical_defense"),
                        all_chemical_attack = reader.GetDouble("all_chemical_attack"),
                        all_chemical_defense = reader.GetDouble("all_chemical_defense"),
                        all_atomic_attack = reader.GetDouble("all_atomic_attack"),
                        all_atomic_defense = reader.GetDouble("all_atomic_defense"),
                        all_mental_attack = reader.GetDouble("all_mental_attack"),
                        all_mental_defense = reader.GetDouble("all_mental_defense"),
                        all_speed = reader.GetDouble("all_speed"),
                        all_critical_damage_rate = reader.GetDouble("all_critical_damage_rate"),
                        all_critical_rate = reader.GetDouble("all_critical_rate"),
                        all_penetration_rate = reader.GetDouble("all_penetration_rate"),
                        all_evasion_rate = reader.GetDouble("all_evasion_rate"),
                        all_damage_absorption_rate = reader.GetDouble("all_damage_absorption_rate"),
                        all_vitality_regeneration_rate = reader.GetDouble("all_vitality_regeneration_rate"),
                        all_accuracy_rate = reader.GetDouble("all_accuracy_rate"),
                        all_lifesteal_rate = reader.GetDouble("all_lifesteal_rate"),
                        all_shield_strength = reader.GetDouble("all_shield_strength"),
                        all_tenacity = reader.GetDouble("all_tenacity"),
                        all_resistance_rate = reader.GetDouble("all_resistance_rate"),
                        all_combo_rate = reader.GetDouble("all_combo_rate"),
                        all_reflection_rate = reader.GetDouble("all_reflection_rate"),
                        all_mana = reader.GetFloat("all_mana"),
                        all_mana_regeneration_rate = reader.GetDouble("all_mana_regeneration_rate"),
                        all_damage_to_different_faction_rate = reader.GetDouble("all_damage_to_different_faction_rate"),
                        all_resistance_to_different_faction_rate = reader.GetDouble("all_resistance_to_different_faction_rate"),
                        all_damage_to_same_faction_rate = reader.GetDouble("all_damage_to_same_faction_rate"),
                        all_resistance_to_same_faction_rate = reader.GetDouble("all_resistance_to_same_faction_rate"),
                    };

                    petsList.Add(pet);
                }
                petsList = GetFinalPower(petsList);
                petsList = GetAllEquipmentPower(petsList);
                petsList = GetAllRankPower(petsList);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return petsList;
    }
    public Dictionary<string, int> GetUniquePetTypesTeam(int teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = @"SELECT distinct c.type, count(c.type) as number
            FROM user_pets uc
            LEFT JOIN pets c ON uc.pet_id = c.id 
            LEFT JOIN fact_pets fch ON fch.user_id = uc.user_id AND fch.user_pet_id = uc.pet_id
            WHERE uc.user_id =@userId and fch.team_id=@team_id
            group by c.type, c.type";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", User.CurrentUserId);
            command.Parameters.AddWithValue("@team_id", teamId);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string type = reader["type"].ToString();
                int number = Convert.ToInt32(reader["number"]);

                result[type] = number;
            }
        }
        return result;
    }
    public int GetUserPetsCount(string type)
    {
        int count = 0;
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from Pets p, user_pets up where p.id=up.pet_id and up.user_id=@userId and p.type= @type";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                count = Convert.ToInt32(command.ExecuteScalar());

                return count;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return count;
    }
    public bool InsertUserPets(Pets pets)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                INSERT INTO user_pets (
                    user_id, pet_id, level, experiment, star, block, quantity, power, health, physical_attack, 
                    physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, 
                    atomic_defense, mental_attack, mental_defense, speed, critical_damage_rate, critical_rate, 
                    penetration_rate, evasion_rate, damage_absorption_rate, vitality_regeneration_rate, accuracy_rate, 
                    lifesteal_rate, shield_strength, tenacity, resistance_rate, combo_rate, reflection_rate, 
                    mana, mana_regeneration_rate, damage_to_different_faction_rate, 
                    resistance_to_different_faction_rate, damage_to_same_faction_rate, resistance_to_same_faction_rate
                ) VALUES (
                    @user_id, @pet_id, @level, @experiment, @star, @block, @quantity, @power, @health, @physical_attack, 
                    @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, 
                    @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage_rate, @critical_rate, 
                    @penetration_rate, @evasion_rate, @damage_absorption_rate, @vitality_regeneration_rate, @accuracy_rate, 
                    @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate, @combo_rate, @reflection_rate, 
                    @mana, @mana_regeneration_rate, @damage_to_different_faction_rate, 
                    @resistance_to_different_faction_rate, @damage_to_same_faction_rate, @resistance_to_same_faction_rate
                );";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@pet_id", pets.id);
                command.Parameters.AddWithValue("@level", 0);
                command.Parameters.AddWithValue("@experiment", 0);
                command.Parameters.AddWithValue("@star", 0);
                command.Parameters.AddWithValue("@block", false);
                command.Parameters.AddWithValue("@power", pets.power);
                command.Parameters.AddWithValue("@health", pets.health);
                command.Parameters.AddWithValue("@physical_attack", pets.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", pets.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", pets.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", pets.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", pets.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", pets.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", pets.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", pets.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", pets.magical_attack);
                command.Parameters.AddWithValue("@mental_defense", pets.magical_defense);
                command.Parameters.AddWithValue("@speed", pets.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", pets.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", pets.critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", pets.penetration_rate);
                command.Parameters.AddWithValue("@evasion_rate", pets.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", pets.damage_absorption_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", pets.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@accuracy_rate", pets.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", pets.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", pets.shield_strength);
                command.Parameters.AddWithValue("@tenacity", pets.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", pets.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", pets.combo_rate);
                command.Parameters.AddWithValue("@reflection_rate", pets.reflection_rate);
                command.Parameters.AddWithValue("@mana", pets.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", pets.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", pets.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", pets.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", pets.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", pets.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
                InsertFactPets(pets);
                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return false;
    }
    public bool UpdatePetsLevel(Pets pets, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_pets
                SET 
                    level = @level, power = @power, health = @health, 
                    physical_attack = @physical_attack, physical_defense = @physical_defense, 
                    magical_attack = @magical_attack, magical_defense = @magical_defense, 
                    chemical_attack = @chemical_attack, chemical_defense = @chemical_defense, 
                    atomic_attack = @atomic_attack, atomic_defense = @atomic_defense, 
                    mental_attack = @mental_attack, mental_defense = @mental_defense, 
                    speed = @speed, critical_damage_rate = @critical_damage_rate, 
                    critical_rate = @critical_rate, penetration_rate = @penetration_rate, 
                    evasion_rate = @evasion_rate, damage_absorption_rate = @damage_absorption_rate, 
                    vitality_regeneration_rate = @vitality_regeneration_rate, accuracy_rate = @accuracy_rate, 
                    lifesteal_rate = @lifesteal_rate, shield_strength = @shield_strength, 
                    tenacity = @tenacity, resistance_rate = @resistance_rate, combo_rate = @combo_rate, 
                    reflection_rate = @reflection_rate, mana = @mana, mana_regeneration_rate = @mana_regeneration_rate, 
                    damage_to_different_faction_rate = @damage_to_different_faction_rate, 
                    resistance_to_different_faction_rate = @resistance_to_different_faction_rate, 
                    damage_to_same_faction_rate = @damage_to_same_faction_rate, 
                    resistance_to_same_faction_rate = @resistance_to_same_faction_rate
                WHERE user_id = @user_id AND pet_id = @pet_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@pet_id", pets.id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", pets.power);
                command.Parameters.AddWithValue("@health", pets.health);
                command.Parameters.AddWithValue("@physical_attack", pets.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", pets.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", pets.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", pets.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", pets.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", pets.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", pets.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", pets.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", pets.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", pets.mental_defense);
                command.Parameters.AddWithValue("@speed", pets.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", pets.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", pets.critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", pets.penetration_rate);
                command.Parameters.AddWithValue("@evasion_rate", pets.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", pets.damage_absorption_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", pets.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@accuracy_rate", pets.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", pets.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", pets.shield_strength);
                command.Parameters.AddWithValue("@tenacity", pets.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", pets.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", pets.combo_rate);
                command.Parameters.AddWithValue("@reflection_rate", pets.reflection_rate);
                command.Parameters.AddWithValue("@mana", pets.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", pets.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", pets.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", pets.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", pets.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", pets.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdatePetsBreakthrough(Pets pets, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_pets
                SET 
                    star = @star, quantity = @quantity, power=@power, health = @health, 
                    physical_attack = @physical_attack, physical_defense = @physical_defense, 
                    magical_attack = @magical_attack, magical_defense = @magical_defense, 
                    chemical_attack = @chemical_attack, chemical_defense = @chemical_defense, 
                    atomic_attack = @atomic_attack, atomic_defense = @atomic_defense, 
                    mental_attack = @mental_attack, mental_defense = @mental_defense, 
                    speed = @speed, critical_damage_rate = @critical_damage_rate, 
                    critical_rate = @critical_rate, penetration_rate = @penetration_rate, 
                    evasion_rate = @evasion_rate, damage_absorption_rate = @damage_absorption_rate, 
                    vitality_regeneration_rate = @vitality_regeneration_rate, accuracy_rate = @accuracy_rate, 
                    lifesteal_rate = @lifesteal_rate, shield_strength = @shield_strength, 
                    tenacity = @tenacity, resistance_rate = @resistance_rate, combo_rate = @combo_rate, 
                    reflection_rate = @reflection_rate, mana = @mana, mana_regeneration_rate = @mana_regeneration_rate, 
                    damage_to_different_faction_rate = @damage_to_different_faction_rate, 
                    resistance_to_different_faction_rate = @resistance_to_different_faction_rate, 
                    damage_to_same_faction_rate = @damage_to_same_faction_rate, 
                    resistance_to_same_faction_rate = @resistance_to_same_faction_rate
                WHERE user_id = @user_id AND pet_id = @pet_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@pet_id", pets.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", pets.power);
                command.Parameters.AddWithValue("@health", pets.health);
                command.Parameters.AddWithValue("@physical_attack", pets.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", pets.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", pets.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", pets.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", pets.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", pets.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", pets.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", pets.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", pets.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", pets.mental_defense);
                command.Parameters.AddWithValue("@speed", pets.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", pets.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", pets.critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", pets.penetration_rate);
                command.Parameters.AddWithValue("@evasion_rate", pets.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", pets.damage_absorption_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", pets.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@accuracy_rate", pets.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", pets.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", pets.shield_strength);
                command.Parameters.AddWithValue("@tenacity", pets.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", pets.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", pets.combo_rate);
                command.Parameters.AddWithValue("@reflection_rate", pets.reflection_rate);
                command.Parameters.AddWithValue("@mana", pets.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", pets.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", pets.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", pets.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", pets.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", pets.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool InsertFactPets(Pets pets)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                INSERT INTO fact_pets (
                user_id, user_pet_id, team_id, position, role, 
                all_power, all_health, all_physical_attack, all_physical_defense, 
                all_magical_attack, all_magical_defense, all_chemical_attack, all_chemical_defense, 
                all_atomic_attack, all_atomic_defense, all_mental_attack, all_mental_defense, 
                all_speed, all_critical_damage_rate, all_critical_rate, all_penetration_rate, 
                all_evasion_rate, all_damage_absorption_rate, all_vitality_regeneration_rate, 
                all_accuracy_rate, all_lifesteal_rate, all_shield_strength, all_tenacity, 
                all_resistance_rate, all_combo_rate, all_reflection_rate, all_mana, 
                all_mana_regeneration_rate, all_damage_to_different_faction_rate, 
                all_resistance_to_different_faction_rate, all_damage_to_same_faction_rate, 
                all_resistance_to_same_faction_rate
            ) VALUES (
                @user_id, @user_pet_id, @team_id, @position, @role, 
                @all_power, @all_health, @all_physical_attack, @all_physical_defense, 
                @all_magical_attack, @all_magical_defense, @all_chemical_attack, @all_chemical_defense, 
                @all_atomic_attack, @all_atomic_defense, @all_mental_attack, @all_mental_defense, 
                @all_speed, @all_critical_damage_rate, @all_critical_rate, @all_penetration_rate, 
                @all_evasion_rate, @all_damage_absorption_rate, @all_vitality_regeneration_rate, 
                @all_accuracy_rate, @all_lifesteal_rate, @all_shield_strength, @all_tenacity, 
                @all_resistance_rate, @all_combo_rate, @all_reflection_rate, @all_mana, 
                @all_mana_regeneration_rate, @all_damage_to_different_faction_rate, 
                @all_resistance_to_different_faction_rate, @all_damage_to_same_faction_rate, 
                @all_resistance_to_same_faction_rate
            );";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_pet_id", pets.id);
                command.Parameters.AddWithValue("@team_id", null);
                command.Parameters.AddWithValue("@position", null);
                command.Parameters.AddWithValue("@role", null);
                command.Parameters.AddWithValue("@all_power", pets.power);
                command.Parameters.AddWithValue("@all_health", pets.health);
                command.Parameters.AddWithValue("@all_physical_attack", pets.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", pets.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", pets.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", pets.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", pets.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", pets.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", pets.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", pets.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", pets.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", pets.mental_defense);
                command.Parameters.AddWithValue("@all_speed", pets.speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", pets.critical_damage_rate);
                command.Parameters.AddWithValue("@all_critical_rate", pets.critical_rate);
                command.Parameters.AddWithValue("@all_penetration_rate", pets.penetration_rate);
                command.Parameters.AddWithValue("@all_evasion_rate", pets.evasion_rate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", pets.damage_absorption_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", pets.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@all_accuracy_rate", pets.accuracy_rate);
                command.Parameters.AddWithValue("@all_mana", pets.mana);
                command.Parameters.AddWithValue("@all_lifesteal_rate", pets.lifesteal_rate);
                command.Parameters.AddWithValue("@all_shield_strength", pets.shield_strength);
                command.Parameters.AddWithValue("@all_tenacity", pets.tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", pets.resistance_rate);
                command.Parameters.AddWithValue("@all_combo_rate", pets.combo_rate);
                command.Parameters.AddWithValue("@all_reflection_rate", pets.reflection_rate);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", pets.mana_regeneration_rate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", pets.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", pets.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", pets.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", pets.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateFactPets(Pets pets)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE fact_pets
                SET 
                    all_power = @all_power, all_health = @all_health, all_physical_attack = @all_physical_attack,
                    all_physical_defense = @all_physical_defense, all_magical_attack = @all_magical_attack,
                    all_magical_defense = @all_magical_defense, all_chemical_attack = @all_chemical_attack,
                    all_chemical_defense = @all_chemical_defense, all_atomic_attack = @all_atomic_attack,
                    all_atomic_defense = @all_atomic_defense, all_mental_attack = @all_mental_attack,
                    all_mental_defense = @all_mental_defense, all_speed = @all_speed, 
                    all_critical_damage_rate = @all_critical_damage, all_critical_rate = @all_critical_rate, 
                    all_penetration_rate = @all_armor_penetration, all_evasion_rate = @all_avoid, 
                    all_damage_absorption_rate = @all_absorbs_damage, all_vitality_regeneration_rate = @all_regenerate_vitality, 
                    all_accuracy_rate = @all_accuracy, all_mana = @all_mana, 
                    all_lifesteal_rate = @all_lifesteal, all_shield_strength = @all_shield_strength,
                    all_tenacity = @all_tenacity, all_resistance_rate = @all_resistance,
                    all_combo_rate = @all_combo_rate, all_reflection_rate = @all_reflection_rate,
                    all_mana_regeneration_rate = @all_mana_regeneration, 
                    all_damage_to_different_faction_rate = @all_damage_to_different_faction,
                    all_resistance_to_different_faction_rate = @all_resistance_to_different_faction,
                    all_damage_to_same_faction_rate = @all_damage_to_same_faction,
                    all_resistance_to_same_faction_rate = @all_resistance_to_same_faction
                WHERE user_id = @user_id AND user_pet_id = @user_pet_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_pet_id", pets.id);
                command.Parameters.AddWithValue("@all_power", pets.power);
                command.Parameters.AddWithValue("@all_health", pets.health);
                command.Parameters.AddWithValue("@all_physical_attack", pets.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", pets.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", pets.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", pets.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", pets.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", pets.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", pets.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", pets.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", pets.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", pets.mental_defense);
                command.Parameters.AddWithValue("@all_speed", pets.speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", pets.critical_damage_rate);
                command.Parameters.AddWithValue("@all_critical_rate", pets.critical_rate);
                command.Parameters.AddWithValue("@all_penetration_rate", pets.penetration_rate);
                command.Parameters.AddWithValue("@all_evasion_rate", pets.evasion_rate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", pets.damage_absorption_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", pets.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@all_accuracy_rate", pets.accuracy_rate);
                command.Parameters.AddWithValue("@all_mana", pets.mana);
                command.Parameters.AddWithValue("@all_lifesteal_rate", pets.lifesteal_rate);
                command.Parameters.AddWithValue("@all_shield_strength", pets.shield_strength);
                command.Parameters.AddWithValue("@all_tenacity", pets.tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", pets.resistance_rate);
                command.Parameters.AddWithValue("@all_combo_rate", pets.combo_rate);
                command.Parameters.AddWithValue("@all_reflection_rate", pets.reflection_rate);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", pets.mana_regeneration_rate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", pets.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", pets.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", pets.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", pets.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateTeamFactCardPets(int? team_id, int card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                Update fact_card_pets set team_id=@team_id where user_id=@user_id 
                and user_card_pet_id=@user_card_pet_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_pet_id", card_id);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public List<Pets> GetPetsWithPrice(string type, int pageSize, int offset)
    {
        List<Pets> petsList = new List<Pets>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select p.*, pt.price, cu.image as currency_image, cu.id as currency_id
                from pets p, pet_trade pt, currency cu
                where p.id=pt.pet_id and pt.currency_id = cu.id and p.type =@type
                ORDER BY p.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(p.name, '[0-9]+$') AS UNSIGNED), p.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Pets pet = new Pets
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        power = reader.GetDouble("power"),
                        health = reader.GetDouble("health"),
                        physical_attack = reader.GetDouble("physical_attack"),
                        physical_defense = reader.GetDouble("physical_defense"),
                        magical_attack = reader.GetDouble("magical_attack"),
                        magical_defense = reader.GetDouble("magical_defense"),
                        chemical_attack = reader.GetDouble("chemical_attack"),
                        chemical_defense = reader.GetDouble("chemical_defense"),
                        atomic_attack = reader.GetDouble("atomic_attack"),
                        atomic_defense = reader.GetDouble("atomic_defense"),
                        mental_attack = reader.GetDouble("mental_attack"),
                        mental_defense = reader.GetDouble("mental_defense"),
                        speed = reader.GetDouble("speed"),
                        critical_damage_rate = reader.GetDouble("critical_damage_rate"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        description = reader.GetString("description")
                    };
                    pet.currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("price")
                    };

                    petsList.Add(pet);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return petsList;
    }
    public int GetPetsWithPriceCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*)
                from pets p, pet_trade pt, currency cu
                where p.id=pt.pet_id and pt.currency_id = cu.id and p.type =@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                count = Convert.ToInt32(command.ExecuteScalar());

                return count;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return count;
    }
    public Pets GetPetsById(int Id)
    {
        Pets pets = new Pets();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from pets where id=@id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    pets = new Pets
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        power = reader.GetDouble("power"),
                        health = reader.GetDouble("health"),
                        physical_attack = reader.GetDouble("physical_attack"),
                        physical_defense = reader.GetDouble("physical_defense"),
                        magical_attack = reader.GetDouble("magical_attack"),
                        magical_defense = reader.GetDouble("magical_defense"),
                        chemical_attack = reader.GetDouble("chemical_attack"),
                        chemical_defense = reader.GetDouble("chemical_defense"),
                        atomic_attack = reader.GetDouble("atomic_attack"),
                        atomic_defense = reader.GetDouble("atomic_defense"),
                        mental_attack = reader.GetDouble("mental_attack"),
                        mental_defense = reader.GetDouble("mental_defense"),
                        speed = reader.GetDouble("speed"),
                        critical_damage_rate = reader.GetDouble("critical_damage_rate"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        description = reader.GetString("description")
                    };
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return pets;
    }
    public Pets GetUserPetsById(int Id)
    {
        Pets card = new Pets();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_pets where user_pets.pet_id=@id 
                and user_pets.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new Pets
                    {
                        id = reader.GetInt32("pet_id"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        star = reader.GetInt32("star"),
                        power = reader.GetDouble("power"),
                        health = reader.GetDouble("health"),
                        physical_attack = reader.GetDouble("physical_attack"),
                        physical_defense = reader.GetDouble("physical_defense"),
                        magical_attack = reader.GetDouble("magical_attack"),
                        magical_defense = reader.GetDouble("magical_defense"),
                        chemical_attack = reader.GetDouble("chemical_attack"),
                        chemical_defense = reader.GetDouble("chemical_defense"),
                        atomic_attack = reader.GetDouble("atomic_attack"),
                        atomic_defense = reader.GetDouble("atomic_defense"),
                        mental_attack = reader.GetDouble("mental_attack"),
                        mental_defense = reader.GetDouble("mental_defense"),
                        speed = reader.GetDouble("speed"),
                        critical_damage_rate = reader.GetDouble("critical_damage_rate"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                    };
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return card;
    }
    public void InsertPetsGallery(int Id)
    {
        Pets petFromDB = GetPetsById(Id);
        int percent = 0;
        if (petFromDB.rare.Equals("LG"))
        {
            percent = 20;
        }
        else if (petFromDB.rare.Equals("UR"))
        {
            percent = 10;
        }
        else if (petFromDB.rare.Equals("SSR"))
        {
            percent = 5;
        }
        else if (petFromDB.rare.Equals("SR"))
        {
            percent = 2;
        }
        else if (petFromDB.rare.Equals("MR"))
        {
            percent = 30;
        }
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra bản ghi đã tồn tại
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM pets_gallery 
                WHERE user_id = @user_id AND pet_id = @pet_id;
                ";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@pet_id", Id);

                int recordCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (recordCount == 0)
                {
                    string query = @"
                    INSERT INTO pets_gallery (
                        user_id, pet_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
                        magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                        mental_attack, mental_defense, speed, critical_damage_rate, critical_rate, penetration_rate, evasion_rate, 
                        damage_absorption_rate, vitality_regeneration_rate, accuracy_rate, lifesteal_rate, shield_strength, tenacity, 
                        resistance_rate, combo_rate, reflection_rate, mana, mana_regeneration_rate, 
                        damage_to_different_faction_rate, resistance_to_different_faction_rate, 
                        damage_to_same_faction_rate, resistance_to_same_faction_rate, 
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                        percent_all_mental_attack, percent_all_mental_defense
                    ) VALUES (
                        @user_id, @pet_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
                        @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, 
                        @mental_attack, @mental_defense, @speed, @critical_damage_rate, @critical_rate, @penetration_rate, @evasion_rate, 
                        @damage_absorption_rate, @vitality_regeneration_rate, @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, 
                        @resistance_rate, @combo_rate, @reflection_rate, @mana, @mana_regeneration_rate, 
                        @damage_to_different_faction_rate, @resistance_to_different_faction_rate, 
                        @damage_to_same_faction_rate, @resistance_to_same_faction_rate, 
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, 
                        @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, 
                        @percent_all_mental_attack, @percent_all_mental_defense
                    );
                    ";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@pet_id", Id);
                    command.Parameters.AddWithValue("@status", "pending");
                    command.Parameters.AddWithValue("@current_star", 0);
                    command.Parameters.AddWithValue("@temp_star", 0);
                    command.Parameters.AddWithValue("@power", petFromDB.power);
                    command.Parameters.AddWithValue("@health", petFromDB.health);
                    command.Parameters.AddWithValue("@physical_attack", petFromDB.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", petFromDB.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", petFromDB.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", petFromDB.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", petFromDB.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", petFromDB.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", petFromDB.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", petFromDB.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", petFromDB.magical_attack);
                    command.Parameters.AddWithValue("@mental_defense", petFromDB.magical_defense);
                    command.Parameters.AddWithValue("@speed", petFromDB.speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", petFromDB.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", petFromDB.critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", petFromDB.penetration_rate);
                    command.Parameters.AddWithValue("@evasion_rate", petFromDB.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", petFromDB.damage_absorption_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", petFromDB.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", petFromDB.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", petFromDB.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", petFromDB.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", petFromDB.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", petFromDB.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", petFromDB.combo_rate);
                    command.Parameters.AddWithValue("@reflection_rate", petFromDB.reflection_rate);
                    command.Parameters.AddWithValue("@mana", petFromDB.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", petFromDB.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", petFromDB.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", petFromDB.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", petFromDB.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", petFromDB.resistance_to_same_faction_rate);
                    command.Parameters.AddWithValue("@percent_all_health", percent);
                    command.Parameters.AddWithValue("@percent_all_physical_attack", percent);
                    command.Parameters.AddWithValue("@percent_all_physical_defense", percent);
                    command.Parameters.AddWithValue("@percent_all_magical_attack", percent);
                    command.Parameters.AddWithValue("@percent_all_magical_defense", percent);
                    command.Parameters.AddWithValue("@percent_all_chemical_attack", percent);
                    command.Parameters.AddWithValue("@percent_all_chemical_defense", percent);
                    command.Parameters.AddWithValue("@percent_all_atomic_attack", percent);
                    command.Parameters.AddWithValue("@percent_all_atomic_defense", percent);
                    command.Parameters.AddWithValue("@percent_all_mental_attack", percent);
                    command.Parameters.AddWithValue("@percent_all_mental_defense", percent);
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
    public void UpdateStatusPetsGallery(int Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "update pets_gallery set status=@status where user_id=@user_id and pet_id=@pet_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@pet_id", Id);
                command.Parameters.AddWithValue("@status", "available");
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
    public Pets SumPowerPetsGallery()
    {
        Pets sumPets = new Pets();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                SUM(power) AS total_power, SUM(health) AS total_health, SUM(mana) AS total_mana, 
                SUM(physical_attack) AS total_physical_attack, SUM(physical_defense) AS total_physical_defense, 
                SUM(magical_attack) AS total_magical_attack, SUM(magical_defense) AS total_magical_defense, 
                SUM(chemical_attack) AS total_chemical_attack, SUM(chemical_defense) AS total_chemical_defense, 
                SUM(atomic_attack) AS total_atomic_attack, SUM(atomic_defense) AS total_atomic_defense, 
                SUM(mental_attack) AS total_mental_attack, SUM(mental_defense) AS total_mental_defense, 
                SUM(speed) AS total_speed, SUM(critical_damage_rate) AS total_critical_damage_rate, 
                SUM(critical_rate) AS total_critical_rate, SUM(penetration_rate) AS total_penetration_rate, 
                SUM(evasion_rate) AS total_evasion_rate, SUM(damage_absorption_rate) AS total_damage_absorption_rate, 
                SUM(vitality_regeneration_rate) AS total_vitality_regeneration_rate, SUM(accuracy_rate) AS total_accuracy_rate, 
                SUM(lifesteal_rate) AS total_lifesteal_rate, SUM(shield_strength) AS total_shield_strength, 
                SUM(tenacity) AS total_tenacity, SUM(resistance_rate) AS total_resistance_rate, 
                SUM(combo_rate) AS total_combo_rate, SUM(reflection_rate) AS total_reflection_rate, 
                SUM(mana_regeneration_rate) AS total_mana_regeneration_rate, 
                SUM(damage_to_different_faction_rate) AS total_damage_to_different_faction_rate, 
                SUM(resistance_to_different_faction_rate) AS total_resistance_to_different_faction_rate, 
                SUM(damage_to_same_faction_rate) AS total_damage_to_same_faction_rate, 
                SUM(resistance_to_same_faction_rate) AS total_resistance_to_same_faction_rate, 
                SUM(percent_all_health) AS total_percent_all_health, 
                SUM(percent_all_physical_attack) AS total_percent_all_physical_attack, 
                SUM(percent_all_physical_defense) AS total_percent_all_physical_defense, 
                SUM(percent_all_magical_attack) AS total_percent_all_magical_attack, 
                SUM(percent_all_magical_defense) AS total_percent_all_magical_defense, 
                SUM(percent_all_chemical_attack) AS total_percent_all_chemical_attack, 
                SUM(percent_all_chemical_defense) AS total_percent_all_chemical_defense, 
                SUM(percent_all_atomic_attack) AS total_percent_all_atomic_attack, 
                SUM(percent_all_atomic_defense) AS total_percent_all_atomic_defense, 
                SUM(percent_all_mental_attack) AS total_percent_all_mental_attack, 
                SUM(percent_all_mental_defense) AS total_percent_all_mental_defense 
            FROM pets_gallery 
            WHERE user_id = @user_id AND status = 'available';";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumPets.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumPets.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumPets.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumPets.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumPets.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumPets.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumPets.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumPets.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumPets.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumPets.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumPets.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumPets.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumPets.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumPets.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                        sumPets.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumPets.penetration_rate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                        sumPets.evasion_rate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                        sumPets.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                        sumPets.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                        sumPets.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                        sumPets.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                        sumPets.shield_strength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                        sumPets.tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                        sumPets.resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                        sumPets.combo_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                        sumPets.reflection_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                        sumPets.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                        sumPets.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                        sumPets.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                        sumPets.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                        sumPets.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                        sumPets.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                        sumPets.percent_all_health = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDouble("total_percent_all_health");
                        sumPets.percent_all_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDouble("total_percent_all_physical_attack");
                        sumPets.percent_all_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDouble("total_percent_all_physical_defense");
                        sumPets.percent_all_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDouble("total_percent_all_magical_attack");
                        sumPets.percent_all_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDouble("total_percent_all_magical_defense");
                        sumPets.percent_all_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDouble("total_percent_all_chemical_attack");
                        sumPets.percent_all_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDouble("total_percent_all_chemical_defense");
                        sumPets.percent_all_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDouble("total_percent_all_atomic_attack");
                        sumPets.percent_all_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDouble("total_percent_all_atomic_defense");
                        sumPets.percent_all_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDouble("total_percent_all_mental_attack");
                        sumPets.percent_all_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDouble("total_percent_all_mental_defense");
                    }
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return sumPets;
    }
}
