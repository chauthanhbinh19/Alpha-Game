using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class Books
{
    private int id1;
    private string name1;
    private string image1;
    private string rare1;
    private string type1;
    private int star1;
    private int level1;
    private int experiment1;
    private int quantity1;
    private bool block1;
    private string position1;
    private double power1;
    private double health1;
    private double physical_attack1;
    private double physical_defense1;
    private double magical_attack1;
    private double magical_defense1;
    private double chemical_attack1;
    private double chemical_defense1;
    private double atomic_attack1;
    private double atomic_defense1;
    private double mental_attack1;
    private double mental_defense1;
    private double speed1;
    private double critical_damage_rate1;
    private double critical_rate1;
    private double penetration_rate1;
    private double evasion_rate1;
    private double damage_absorption_rate1;
    private double vitality_regeneration_rate1;
    private double accuracy_rate1;
    private double lifesteal_rate1;
    private float mana1;
    private double mana_regeneration_rate1;
    private double shield_strength1;
    private double tenacity1;
    private double resistance_rate1;
    private double combo_rate1;
    private double reflection_rate1;
    private double damage_to_different_faction_rate1;
    private double resistance_to_different_faction_rate1;
    private double damage_to_same_faction_rate1;
    private double resistance_to_same_faction_rate1;
    private double all_power1;
    private double all_health1;
    private double all_physical_attack1;
    private double all_physical_defense1;
    private double all_magical_attack1;
    private double all_magical_defense1;
    private double all_chemical_attack1;
    private double all_chemical_defense1;
    private double all_atomic_attack1;
    private double all_atomic_defense1;
    private double all_mental_attack1;
    private double all_mental_defense1;
    private double all_speed1;
    private double all_critical_damage_rate1;
    private double all_critical_rate1;
    private double all_penetration_rate1;
    private double all_evasion_rate1;
    private double all_damage_absorption_rate1;
    private double all_vitality_regeneration_rate1;
    private double all_accuracy_rate1;
    private double all_lifesteal_rate1;
    private float all_mana1;
    private double all_mana_regeneration_rate1;
    private double all_shield_strength1;
    private double all_tenacity1;
    private double all_resistance_rate1;
    private double all_combo_rate1;
    private double all_reflection_rate1;
    private double all_damage_to_different_faction_rate1;
    private double all_resistance_to_different_faction_rate1;
    private double all_damage_to_same_faction_rate1;
    private double all_resistance_to_same_faction_rate1;
    private string description1;
    private string status1;
    private int team_id1;
    private double percent_all_health1;
    private double percent_all_physical_attack1;
    private double percent_all_physical_defense1;
    private double percent_all_magical_attack1;
    private double percent_all_magical_defense1;
    private double percent_all_chemical_attack1;
    private double percent_all_chemical_defense1;
    private double percent_all_atomic_attack1;
    private double percent_all_atomic_defense1;
    private double percent_all_mental_attack1;
    private double percent_all_mental_defense1;
    private int quality1;

    public int id { get => id1; set => id1 = value; }
    public string name { get => name1; set => name1 = value; }
    public string image { get => image1; set => image1 = value; }
    public string rare { get => rare1; set => rare1 = value; }
    public int quality { get => quality1; set => quality1 = value; }
    public string type { get => type1; set => type1 = value; }
    public int star { get => star1; set => star1 = value; }
    public int level { get => level1; set => level1 = value; }
    public int experiment { get => experiment1; set => experiment1 = value; }
    public int quantity { get => quantity1; set => quantity1 = value; }
    public bool block { get => block1; set => block1 = value; }
    public string position { get => position1; set => position1 = value; }
    public double power { get => power1; set => power1 = value; }
    public double health { get => health1; set => health1 = value; }
    public double physical_attack { get => physical_attack1; set => physical_attack1 = value; }
    public double physical_defense { get => physical_defense1; set => physical_defense1 = value; }
    public double magical_attack { get => magical_attack1; set => magical_attack1 = value; }
    public double magical_defense { get => magical_defense1; set => magical_defense1 = value; }
    public double chemical_attack { get => chemical_attack1; set => chemical_attack1 = value; }
    public double chemical_defense { get => chemical_defense1; set => chemical_defense1 = value; }
    public double atomic_attack { get => atomic_attack1; set => atomic_attack1 = value; }
    public double atomic_defense { get => atomic_defense1; set => atomic_defense1 = value; }
    public double mental_attack { get => mental_attack1; set => mental_attack1 = value; }
    public double mental_defense { get => mental_defense1; set => mental_defense1 = value; }
    public double speed { get => speed1; set => speed1 = value; }
    public double critical_damage_rate { get => critical_damage_rate1; set => critical_damage_rate1 = value; }
    public double critical_rate { get => critical_rate1; set => critical_rate1 = value; }
    public double penetration_rate { get => penetration_rate1; set => penetration_rate1 = value; }
    public double evasion_rate { get => evasion_rate1; set => evasion_rate1 = value; }
    public double damage_absorption_rate { get => damage_absorption_rate1; set => damage_absorption_rate1 = value; }
    public double vitality_regeneration_rate { get => vitality_regeneration_rate1; set => vitality_regeneration_rate1 = value; }
    public double accuracy_rate { get => accuracy_rate1; set => accuracy_rate1 = value; }
    public double lifesteal_rate { get => lifesteal_rate1; set => lifesteal_rate1 = value; }
    public float mana { get => mana1; set => mana1 = value; }
    public double mana_regeneration_rate { get => mana_regeneration_rate1; set => mana_regeneration_rate1 = value; }
    public double shield_strength { get => shield_strength1; set => shield_strength1 = value; }
    public double tenacity { get => tenacity1; set => tenacity1 = value; }
    public double resistance_rate { get => resistance_rate1; set => resistance_rate1 = value; }
    public double combo_rate { get => combo_rate1; set => combo_rate1 = value; }
    public double reflection_rate { get => reflection_rate1; set => reflection_rate1 = value; }
    public double damage_to_different_faction_rate { get => damage_to_different_faction_rate1; set => damage_to_different_faction_rate1 = value; }
    public double resistance_to_different_faction_rate { get => resistance_to_different_faction_rate1; set => resistance_to_different_faction_rate1 = value; }
    public double damage_to_same_faction_rate { get => damage_to_same_faction_rate1; set => damage_to_same_faction_rate1 = value; }
    public double resistance_to_same_faction_rate { get => resistance_to_same_faction_rate1; set => resistance_to_same_faction_rate1 = value; }
    public double all_power { get => all_power1; set => all_power1 = value; }
    public double all_health { get => all_health1; set => all_health1 = value; }
    public double all_physical_attack { get => all_physical_attack1; set => all_physical_attack1 = value; }
    public double all_physical_defense { get => all_physical_defense1; set => all_physical_defense1 = value; }
    public double all_magical_attack { get => all_magical_attack1; set => all_magical_attack1 = value; }
    public double all_magical_defense { get => all_magical_defense1; set => all_magical_defense1 = value; }
    public double all_chemical_attack { get => all_chemical_attack1; set => all_chemical_attack1 = value; }
    public double all_chemical_defense { get => all_chemical_defense1; set => all_chemical_defense1 = value; }
    public double all_atomic_attack { get => all_atomic_attack1; set => all_atomic_attack1 = value; }
    public double all_atomic_defense { get => all_atomic_defense1; set => all_atomic_defense1 = value; }
    public double all_mental_attack { get => all_mental_attack1; set => all_mental_attack1 = value; }
    public double all_mental_defense { get => all_mental_defense1; set => all_mental_defense1 = value; }
    public double all_speed { get => all_speed1; set => all_speed1 = value; }
    public double all_critical_damage_rate { get => all_critical_damage_rate1; set => all_critical_damage_rate1 = value; }
    public double all_critical_rate { get => all_critical_rate1; set => all_critical_rate1 = value; }
    public double all_penetration_rate { get => all_penetration_rate1; set => all_penetration_rate1 = value; }
    public double all_evasion_rate { get => all_evasion_rate1; set => all_evasion_rate1 = value; }
    public double all_damage_absorption_rate { get => all_damage_absorption_rate1; set => all_damage_absorption_rate1 = value; }
    public double all_vitality_regeneration_rate { get => all_vitality_regeneration_rate1; set => all_vitality_regeneration_rate1 = value; }
    public double all_accuracy_rate { get => all_accuracy_rate1; set => all_accuracy_rate1 = value; }
    public double all_lifesteal_rate { get => all_lifesteal_rate1; set => all_lifesteal_rate1 = value; }
    public float all_mana { get => all_mana1; set => all_mana1 = value; }
    public double all_mana_regeneration_rate { get => all_mana_regeneration_rate1; set => all_mana_regeneration_rate1 = value; }
    public double all_shield_strength { get => all_shield_strength1; set => all_shield_strength1 = value; }
    public double all_tenacity { get => all_tenacity1; set => all_tenacity1 = value; }
    public double all_resistance_rate { get => all_resistance_rate1; set => all_resistance_rate1 = value; }
    public double all_combo_rate { get => all_combo_rate1; set => all_combo_rate1 = value; }
    public double all_reflection_rate { get => all_reflection_rate1; set => all_reflection_rate1 = value; }
    public double all_damage_to_different_faction_rate { get => all_damage_to_different_faction_rate1; set => all_damage_to_different_faction_rate1 = value; }
    public double all_resistance_to_different_faction_rate { get => all_resistance_to_different_faction_rate1; set => all_resistance_to_different_faction_rate1 = value; }
    public double all_damage_to_same_faction_rate { get => all_damage_to_same_faction_rate1; set => all_damage_to_same_faction_rate1 = value; }
    public double all_resistance_to_same_faction_rate { get => all_resistance_to_same_faction_rate1; set => all_resistance_to_same_faction_rate1 = value; }
    public string description { get => description1; set => description1 = value; }
    public string status { get => status1; set => status1 = value; }
    public int team_id { get => team_id1; set => team_id1 = value; }
    public double percent_all_health { get => percent_all_health1; set => percent_all_health1 = value; }
    public double percent_all_physical_attack { get => percent_all_physical_attack1; set => percent_all_physical_attack1 = value; }
    public double percent_all_physical_defense { get => percent_all_physical_defense1; set => percent_all_physical_defense1 = value; }
    public double percent_all_magical_attack { get => percent_all_magical_attack1; set => percent_all_magical_attack1 = value; }
    public double percent_all_magical_defense { get => percent_all_magical_defense1; set => percent_all_magical_defense1 = value; }
    public double percent_all_chemical_attack { get => percent_all_chemical_attack1; set => percent_all_chemical_attack1 = value; }
    public double percent_all_chemical_defense { get => percent_all_chemical_defense1; set => percent_all_chemical_defense1 = value; }
    public double percent_all_atomic_attack { get => percent_all_atomic_attack1; set => percent_all_atomic_attack1 = value; }
    public double percent_all_atomic_defense { get => percent_all_atomic_defense1; set => percent_all_atomic_defense1 = value; }
    public double percent_all_mental_attack { get => percent_all_mental_attack1; set => percent_all_mental_attack1 = value; }
    public double percent_all_mental_defense { get => percent_all_mental_defense1; set => percent_all_mental_defense1 = value; }
    public Currency currency { get; set; }
    public Books()
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
    public List<Books> GetFinalPower(List<Books> BooksList)
    {
        PowerManager powerManager = new PowerManager();
        powerManager = powerManager.GetUserStats();
        foreach (var c in BooksList)
        {
            Books books = new Books();
            books = books.GetUserBooksById(c.id);
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
        return BooksList;
    }
    public List<Books> GetAllEquipmentPower(List<Books> BooksList)
    {
        Equipments equipments = new Equipments();
        foreach (var c in BooksList)
        {
            equipments = equipments.GetAllEquipmentsByBooksId(c.id);
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
        return BooksList;
    }
    public List<Books> GetAllRankPower(List<Books> BooksList)
    {
        Rank rank = new Rank();
        foreach (var c in BooksList)
        {
            Books books = new Books();
            books = books.GetUserBooksById(c.id);
            rank = rank.GetSumBooksRank(c.id);
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
        return BooksList;
    }
    public Books GetNewLevelPower(Books c, double coefficient)
    {
        Books orginCard = new Books();
        orginCard = orginCard.GetBooksById(c.id);
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
        books.power = PowerManager.CalculatePower(
            books.health,
            books.physical_attack, books.physical_defense,
            books.magical_attack, books.magical_defense,
            books.chemical_attack, books.chemical_defense,
            books.atomic_attack, books.atomic_defense,
            books.mental_attack, books.mental_defense,
            books.speed,
            books.critical_damage_rate, books.critical_rate,
            books.penetration_rate, books.evasion_rate,
            books.damage_absorption_rate, books.vitality_regeneration_rate,
            books.accuracy_rate, books.lifesteal_rate,
            books.shield_strength, books.tenacity, books.resistance_rate,
            books.combo_rate, books.reflection_rate,
            books.mana, books.mana_regeneration_rate,
            books.damage_to_different_faction_rate, books.resistance_to_different_faction_rate,
            books.damage_to_same_faction_rate, books.resistance_to_same_faction_rate
        );
        return books;
    }
    public Books GetNewBreakthroughPower(Books c, double coefficient)
    {
        Books orginCard = new Books();
        orginCard = orginCard.GetBooksById(c.id);
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
        books.power = PowerManager.CalculatePower(
            books.health,
            books.physical_attack, books.physical_defense,
            books.magical_attack, books.magical_defense,
            books.chemical_attack, books.chemical_defense,
            books.atomic_attack, books.atomic_defense,
            books.mental_attack, books.mental_defense,
            books.speed,
            books.critical_damage_rate, books.critical_rate,
            books.penetration_rate, books.evasion_rate,
            books.damage_absorption_rate, books.vitality_regeneration_rate,
            books.accuracy_rate, books.lifesteal_rate,
            books.shield_strength, books.tenacity, books.resistance_rate,
            books.combo_rate, books.reflection_rate,
            books.mana, books.mana_regeneration_rate,
            books.damage_to_different_faction_rate, books.resistance_to_different_faction_rate,
            books.damage_to_same_faction_rate, books.resistance_to_same_faction_rate
        );
        return books;
    }
    public static List<string> GetUniqueBookTypes()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "Select distinct type from books";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                typeList.Add(reader.GetString(0));
            }
        }
        return typeList;
    }
    public List<Books> GetBooks(string type, int pageSize, int offset)
    {
        List<Books> bookslist = new List<Books>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from books where type= @type 
                ORDER BY books.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(books.name, '[0-9]+$') AS UNSIGNED), books.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Books book = new Books
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        quality = reader.GetInt32("quality"),
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

                    bookslist.Add(book);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return bookslist;
    }
    public int GetBooksCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from books where type= @type";
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
    public List<Books> GetBooksCollection(string type, int pageSize, int offset)
    {
        List<Books> bookslist = new List<Books>();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT b.*, CASE WHEN bg.book_id IS NULL THEN 'block' WHEN bg.status = 'pending' THEN 'pending' WHEN bg.status = 'available' THEN 'available' END AS status
                FROM books b LEFT JOIN books_gallery bg ON b.id = bg.book_id and bg.user_id = @userId where b.type=@type 
                ORDER BY b.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(b.name, '[0-9]+$') AS UNSIGNED), b.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Books book = new Books
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        quality = reader.GetInt32("quality"),
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

                    bookslist.Add(book);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return bookslist;
    }
    public List<Books> GetUserBooks(string type, int pageSize, int offset)
    {
        List<Books> bookslist = new List<Books>();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT ub.*, b.*, fb.*
                FROM user_books ub
                LEFT JOIN books b ON ub.book_id = b.id 
                LEFT JOIN fact_books fb ON fb.user_id = ub.user_id AND fb.user_book_id = ub.book_id
                WHERE ub.user_id = @userId AND b.type = @type
                ORDER BY b.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(b.name, '[0-9]+$') AS UNSIGNED), b.name
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
                    Books book = new Books
                    {
                        id = reader.GetInt32("book_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        quality = reader.GetInt32("quality"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
                        block = reader.GetBoolean("block"),
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

                    bookslist.Add(book);
                }
                bookslist = GetFinalPower(bookslist);
                bookslist = GetAllEquipmentPower(bookslist);
                bookslist = GetAllRankPower(bookslist);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return bookslist;
    }
    public List<Books> GetUserBooksTeam(int teamId)
    {
        List<Books> bookslist = new List<Books>();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT ub.*, b.*, fb.*
                FROM user_books ub
                LEFT JOIN books b ON ub.book_id = b.id 
                LEFT JOIN fact_books fb ON fb.user_id = ub.user_id AND fb.user_book_id = ub.book_id
                WHERE ub.user_id = @userId AND fb.team_id=@team_id
                ORDER BY b.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(b.name, '[0-9]+$') AS UNSIGNED), b.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", User.CurrentUserId);
                command.Parameters.AddWithValue("@team_id", teamId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Books book = new Books
                    {
                        id = reader.GetInt32("book_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
                        block = reader.GetBoolean("block"),
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

                    bookslist.Add(book);
                }
                bookslist = GetFinalPower(bookslist);
                bookslist = GetAllEquipmentPower(bookslist);
                bookslist = GetAllRankPower(bookslist);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return bookslist;
    }
    public Dictionary<string, int> GetUniqueBookTypesTeam(int teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = @"SELECT distinct c.type, count(c.type) as number
            FROM user_card_books uc
            LEFT JOIN books c ON uc.book_id = c.id 
            LEFT JOIN fact_books fch ON fch.user_id = uc.user_id AND fch.user_book_id = uc.book_id
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
    public int GetUserBooksCount(string type)
    {
        int count = 0;
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from books b, user_books ub where b.id=ub.book_id and ub.user_id=@userId and type= @type";
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
    public List<Books> GetBooksRandom(string type, int pageSize)
    {
        List<Books> bookslist = new List<Books>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from books where type= @type ORDER BY RAND() limit @limit";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Books book = new Books
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        quality = reader.GetInt32("quality"),
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

                    bookslist.Add(book);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return bookslist;
    }
    public List<Books> GetAllBooks(string type)
    {
        List<Books> bookslist = new List<Books>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from books where type= @type";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Books book = new Books
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        quality = reader.GetInt32("quality"),
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

                    bookslist.Add(book);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return bookslist;
    }
    public bool InsertUserBooks(Books books)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_books 
                WHERE user_id = @user_id AND book_id = @book_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@book_id", books.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_books (
                    user_id, book_id, level, experiment, star, quality, block, quantity, power, health, physical_attack, 
                    physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, 
                    atomic_defense, mental_attack, mental_defense, speed, critical_damage_rate, critical_rate, 
                    penetration_rate, evasion_rate, damage_absorption_rate, vitality_regeneration_rate, accuracy_rate, 
                    lifesteal_rate, shield_strength, tenacity, resistance_rate, combo_rate, reflection_rate, 
                    mana, mana_regeneration_rate, damage_to_different_faction_rate, 
                    resistance_to_different_faction_rate, damage_to_same_faction_rate, resistance_to_same_faction_rate
                ) VALUES (
                    @user_id, @book_id, @level, @experiment, @star, @quality, @block, @quantity, @power, @health, @physical_attack, 
                    @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, 
                    @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage_rate, @critical_rate, 
                    @penetration_rate, @evasion_rate, @damage_absorption_rate, @vitality_regeneration_rate, @accuracy_rate, 
                    @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate, @combo_rate, @reflection_rate, 
                    @mana, @mana_regeneration_rate, @damage_to_different_faction_rate, 
                    @resistance_to_different_faction_rate, @damage_to_same_faction_rate, @resistance_to_same_faction_rate
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@book_id", books.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", PowerManager.CheckQuality(books.rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", 0);
                    command.Parameters.AddWithValue("@power", books.power);
                    command.Parameters.AddWithValue("@health", books.health);
                    command.Parameters.AddWithValue("@physical_attack", books.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", books.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", books.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", books.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", books.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", books.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", books.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", books.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", books.mental_attack);
                    command.Parameters.AddWithValue("@mental_defense", books.mental_defense);
                    command.Parameters.AddWithValue("@speed", books.speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", books.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", books.critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", books.penetration_rate);
                    command.Parameters.AddWithValue("@evasion_rate", books.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", books.damage_absorption_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", books.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", books.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", books.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", books.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", books.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", books.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", books.combo_rate);
                    command.Parameters.AddWithValue("@reflection_rate", books.reflection_rate);
                    command.Parameters.AddWithValue("@mana", books.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", books.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", books.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", books.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", books.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", books.resistance_to_same_faction_rate);
                    MySqlDataReader reader = command.ExecuteReader();
                    InsertFactBooks(books);
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_books
                    SET quantity = quantity + 1
                    WHERE user_id = @user_id AND book_id = @book_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@book_id", books.id);

                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return true;
    }
    public bool UpdateBooksLevel(Books books, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_books
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
                WHERE user_id = @user_id AND book_id = @book_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@book_id", books.id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", books.power);
                command.Parameters.AddWithValue("@health", books.health);
                command.Parameters.AddWithValue("@physical_attack", books.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", books.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", books.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", books.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", books.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", books.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", books.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", books.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", books.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", books.mental_defense);
                command.Parameters.AddWithValue("@speed", books.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", books.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", books.critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", books.penetration_rate);
                command.Parameters.AddWithValue("@evasion_rate", books.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", books.damage_absorption_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", books.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@accuracy_rate", books.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", books.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", books.shield_strength);
                command.Parameters.AddWithValue("@tenacity", books.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", books.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", books.combo_rate);
                command.Parameters.AddWithValue("@reflection_rate", books.reflection_rate);
                command.Parameters.AddWithValue("@mana", books.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", books.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", books.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", books.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", books.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", books.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateBooksBreakthrough(Books books, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_books
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
                WHERE user_id = @user_id AND book_id = @book_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@book_id", books.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", books.power);
                command.Parameters.AddWithValue("@health", books.health);
                command.Parameters.AddWithValue("@physical_attack", books.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", books.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", books.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", books.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", books.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", books.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", books.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", books.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", books.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", books.mental_defense);
                command.Parameters.AddWithValue("@speed", books.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", books.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", books.critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", books.penetration_rate);
                command.Parameters.AddWithValue("@evasion_rate", books.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", books.damage_absorption_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", books.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@accuracy_rate", books.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", books.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", books.shield_strength);
                command.Parameters.AddWithValue("@tenacity", books.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", books.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", books.combo_rate);
                command.Parameters.AddWithValue("@reflection_rate", books.reflection_rate);
                command.Parameters.AddWithValue("@mana", books.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", books.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", books.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", books.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", books.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", books.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool InsertFactBooks(Books books)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                INSERT INTO fact_books (
                user_id, user_book_id, team_id, position, role, 
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
                @user_id, @user_book_id, @team_id, @position, @role, 
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
            );
            ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_book_id", books.id);
                command.Parameters.AddWithValue("@team_id", null);
                command.Parameters.AddWithValue("@position", null);
                command.Parameters.AddWithValue("@role", null);
                command.Parameters.AddWithValue("@all_power", books.power);
                command.Parameters.AddWithValue("@all_health", books.health);
                command.Parameters.AddWithValue("@all_physical_attack", books.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", books.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", books.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", books.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", books.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", books.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", books.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", books.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", books.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", books.mental_defense);
                command.Parameters.AddWithValue("@all_speed", books.speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", books.critical_damage_rate);
                command.Parameters.AddWithValue("@all_critical_rate", books.critical_rate);
                command.Parameters.AddWithValue("@all_penetration_rate", books.penetration_rate);
                command.Parameters.AddWithValue("@all_evasion_rate", books.evasion_rate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", books.damage_absorption_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", books.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@all_accuracy_rate", books.accuracy_rate);
                command.Parameters.AddWithValue("@all_mana", books.mana);
                command.Parameters.AddWithValue("@all_lifesteal_rate", books.lifesteal_rate);
                command.Parameters.AddWithValue("@all_shield_strength", books.shield_strength);
                command.Parameters.AddWithValue("@all_tenacity", books.tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", books.resistance_rate);
                command.Parameters.AddWithValue("@all_combo_rate", books.combo_rate);
                command.Parameters.AddWithValue("@all_reflection_rate", books.reflection_rate);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", books.mana_regeneration_rate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", books.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", books.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", books.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", books.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateFactBooks(Books books)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE fact_books
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
                WHERE user_id = @user_id AND user_book_id = @user_book_id;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_book_id", books.id);
                command.Parameters.AddWithValue("@all_power", books.power);
                command.Parameters.AddWithValue("@all_health", books.health);
                command.Parameters.AddWithValue("@all_physical_attack", books.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", books.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", books.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", books.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", books.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", books.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", books.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", books.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", books.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", books.mental_defense);
                command.Parameters.AddWithValue("@all_speed", books.speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", books.critical_damage_rate);
                command.Parameters.AddWithValue("@all_critical_rate", books.critical_rate);
                command.Parameters.AddWithValue("@all_penetration_rate", books.penetration_rate);
                command.Parameters.AddWithValue("@all_evasion_rate", books.evasion_rate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", books.damage_absorption_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", books.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@all_accuracy_rate", books.accuracy_rate);
                command.Parameters.AddWithValue("@all_mana", books.mana);
                command.Parameters.AddWithValue("@all_lifesteal_rate", books.lifesteal_rate);
                command.Parameters.AddWithValue("@all_shield_strength", books.shield_strength);
                command.Parameters.AddWithValue("@all_tenacity", books.tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", books.resistance_rate);
                command.Parameters.AddWithValue("@all_combo_rate", books.combo_rate);
                command.Parameters.AddWithValue("@all_reflection_rate", books.reflection_rate);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", books.mana_regeneration_rate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", books.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", books.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", books.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", books.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateTeamFactBooks(int? team_id, string position, int book_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                Update fact_books set team_id=@team_id, position=@position where user_id=@user_id 
                and user_book_id=@user_book_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_book_id", book_id);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public Books GetBooksById(int Id)
    {
        Books book = new Books();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from books where books.id=@id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    book = new Books
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        quality = reader.GetInt32("quality"),
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
        return book;
    }
    public Books GetUserBooksById(int Id)
    {
        Books card = new Books();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_books where user_books.book_id=@id 
                and user_books.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new Books
                    {
                        id = reader.GetInt32("book_id"),
                        level = reader.GetInt32("level"),
                        quality = reader.GetInt32("quality"),
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
    public void InsertBooksGallery(int Id)
    {
        Books BookFromDB = GetBooksById(Id);
        int percent = 0;
        if (BookFromDB.rare.Equals("LG"))
        {
            percent = 20;
        }
        else if (BookFromDB.rare.Equals("UR"))
        {
            percent = 10;
        }
        else if (BookFromDB.rare.Equals("SSR"))
        {
            percent = 5;
        }
        else if (BookFromDB.rare.Equals("SR"))
        {
            percent = 2;
        }
        else if (BookFromDB.rare.Equals("MR"))
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
                FROM books_gallery 
                WHERE user_id = @user_id AND book_id = @book_id;
                ";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@book_id", Id);

                int recordCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (recordCount == 0)
                {
                    string query = @"
                    INSERT INTO books_gallery (
                        user_id, book_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
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
                        @user_id, @book_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
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
                    command.Parameters.AddWithValue("@book_id", Id);
                    command.Parameters.AddWithValue("@status", "pending");
                    command.Parameters.AddWithValue("@current_star", 0);
                    command.Parameters.AddWithValue("@temp_star", 0);
                    command.Parameters.AddWithValue("@power", BookFromDB.power);
                    command.Parameters.AddWithValue("@health", BookFromDB.health);
                    command.Parameters.AddWithValue("@physical_attack", BookFromDB.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", BookFromDB.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", BookFromDB.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", BookFromDB.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", BookFromDB.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", BookFromDB.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", BookFromDB.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", BookFromDB.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", BookFromDB.magical_attack);
                    command.Parameters.AddWithValue("@mental_defense", BookFromDB.magical_defense);
                    command.Parameters.AddWithValue("@speed", BookFromDB.speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", BookFromDB.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", BookFromDB.critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", BookFromDB.penetration_rate);
                    command.Parameters.AddWithValue("@evasion_rate", BookFromDB.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", BookFromDB.damage_absorption_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", BookFromDB.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", BookFromDB.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", BookFromDB.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", BookFromDB.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", BookFromDB.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", BookFromDB.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", BookFromDB.combo_rate);
                    command.Parameters.AddWithValue("@reflection_rate", BookFromDB.reflection_rate);
                    command.Parameters.AddWithValue("@mana", BookFromDB.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", BookFromDB.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", BookFromDB.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", BookFromDB.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", BookFromDB.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", BookFromDB.resistance_to_same_faction_rate);
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
    public void UpdateStatusBooksGallery(int Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "update books_gallery set status=@status where user_id=@user_id and book_id=@book_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@book_id", Id);
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
    public List<Books> GetBooksWithPrice(string type, int pageSize, int offset)
    {
        List<Books> bookslist = new List<Books>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select b.*, bt.price, cu.image as currency_image, cu.id as currency_id
                from books b, book_trade bt, currency cu
                where b.id=bt.book_id and bt.currency_id = cu.id and b.type =@type
                ORDER BY b.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(b.name, '[0-9]+$') AS UNSIGNED), b.name limit @limit offset @offset;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Books book = new Books
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        quality = reader.GetInt32("quality"),
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
                    book.currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("price")
                    };

                    bookslist.Add(book);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return bookslist;
    }
    public int GetBookssWithPriceCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*)
                from books b, book_trade bt, currency cu
                where b.id=bt.book_id and bt.currency_id = cu.id and b.type =@type;";
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
    public Books SumPowerBooksGallery()
    {
        Books sumBooks = new Books();
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
            FROM books_gallery 
            WHERE user_id = @user_id AND status = 'available';";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumBooks.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumBooks.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumBooks.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumBooks.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumBooks.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumBooks.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumBooks.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumBooks.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumBooks.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumBooks.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumBooks.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumBooks.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumBooks.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumBooks.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                        sumBooks.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumBooks.penetration_rate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                        sumBooks.evasion_rate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                        sumBooks.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                        sumBooks.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                        sumBooks.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                        sumBooks.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                        sumBooks.shield_strength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                        sumBooks.tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                        sumBooks.resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                        sumBooks.combo_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                        sumBooks.reflection_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                        sumBooks.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                        sumBooks.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                        sumBooks.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                        sumBooks.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                        sumBooks.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                        sumBooks.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                        sumBooks.percent_all_health = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDouble("total_percent_all_health");
                        sumBooks.percent_all_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDouble("total_percent_all_physical_attack");
                        sumBooks.percent_all_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDouble("total_percent_all_physical_defense");
                        sumBooks.percent_all_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDouble("total_percent_all_magical_attack");
                        sumBooks.percent_all_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDouble("total_percent_all_magical_defense");
                        sumBooks.percent_all_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDouble("total_percent_all_chemical_attack");
                        sumBooks.percent_all_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDouble("total_percent_all_chemical_defense");
                        sumBooks.percent_all_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDouble("total_percent_all_atomic_attack");
                        sumBooks.percent_all_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDouble("total_percent_all_atomic_defense");
                        sumBooks.percent_all_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDouble("total_percent_all_mental_attack");
                        sumBooks.percent_all_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDouble("total_percent_all_mental_defense");
                    }
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return sumBooks;
    }
}
