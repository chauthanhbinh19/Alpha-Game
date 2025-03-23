using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class CardMilitary
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

    public int id { get => id1; set => id1 = value; }
    public string name { get => name1; set => name1 = value; }
    public string image { get => image1; set => image1 = value; }
    public string rare { get => rare1; set => rare1 = value; }
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
    public CardMilitary()
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

        team_id=-1;
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
    public List<CardMilitary> GetFinalPower(List<CardMilitary> CardMilitaryList)
    {
        PowerManager powerManager = new PowerManager();
        powerManager = powerManager.GetUserStats();
        foreach (var c in CardMilitaryList)
        {
            CardMilitary card = new CardMilitary();
            card = card.GetUserCardMilitaryById(c.id);
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
        return CardMilitaryList;
    }
    public List<CardMilitary> GetAllEquipmentPower(List<CardMilitary> CardMilitaryList)
    {
        Equipments equipments = new Equipments();
        foreach (var c in CardMilitaryList)
        {
            equipments = equipments.GetAllEquipmentsByCardMilitaryId(c.id);
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
        return CardMilitaryList;
    }
    public List<CardMilitary> GetAllRankPower(List<CardMilitary> CardMilitaryList)
    {
        Rank rank = new Rank();
        foreach (var c in CardMilitaryList)
        {
            CardMilitary card = new CardMilitary();
            card = card.GetUserCardMilitaryById(c.id);
            rank = rank.GetSumCardMilitaryRank(c.id);
            c.all_health = c.all_health + rank.health + card.health * rank.percent_all_health/100;
            c.all_physical_attack = c.all_physical_attack + rank.physical_attack + card.physical_attack * rank.percent_all_physical_attack/100;
            c.all_physical_defense = c.all_physical_defense + rank.physical_defense + card.physical_defense * rank.percent_all_physical_defense/100;
            c.all_magical_attack = c.all_magical_attack + rank.magical_attack + card.magical_attack * rank.percent_all_magical_attack/100;
            c.all_magical_defense = c.all_magical_defense + rank.magical_defense + card.magical_defense * rank.percent_all_magical_defense/100;
            c.all_chemical_attack = c.all_chemical_attack + rank.chemical_attack + card.chemical_attack * rank.percent_all_chemical_attack/100;
            c.all_chemical_defense = c.all_chemical_defense + rank.chemical_defense + card.chemical_defense * rank.percent_all_chemical_defense/100;
            c.all_atomic_attack = c.all_atomic_attack + rank.atomic_attack + card.atomic_attack * rank.percent_all_atomic_attack/100;
            c.all_atomic_defense = c.all_atomic_defense + rank.atomic_defense + card.atomic_defense * rank.percent_all_atomic_defense/100;
            c.all_mental_attack = c.all_mental_attack + rank.mental_attack + card.mental_attack * rank.percent_all_mental_attack/100;
            c.all_mental_defense = c.all_mental_defense + rank.mental_defense + card.mental_defense * rank.percent_all_mental_defense/100;
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
        return CardMilitaryList;
    }
    public CardMilitary GetNewLevelPower(CardMilitary c, double coefficient)
    {
        CardMilitary orginCard = new CardMilitary();
        orginCard = orginCard.GetCardMilitaryById(c.id);
        CardMilitary cardMilitary = new CardMilitary
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
        cardMilitary.power = PowerManager.CalculatePower(
            cardMilitary.health,
            cardMilitary.physical_attack, cardMilitary.physical_defense,
            cardMilitary.magical_attack, cardMilitary.magical_defense,
            cardMilitary.chemical_attack, cardMilitary.chemical_defense,
            cardMilitary.atomic_attack, cardMilitary.atomic_defense,
            cardMilitary.mental_attack, cardMilitary.mental_defense,
            cardMilitary.speed,
            cardMilitary.critical_damage_rate, cardMilitary.critical_rate,
            cardMilitary.penetration_rate, cardMilitary.evasion_rate,
            cardMilitary.damage_absorption_rate, cardMilitary.vitality_regeneration_rate,
            cardMilitary.accuracy_rate, cardMilitary.lifesteal_rate,
            cardMilitary.shield_strength, cardMilitary.tenacity, cardMilitary.resistance_rate,
            cardMilitary.combo_rate, cardMilitary.reflection_rate,
            cardMilitary.mana, cardMilitary.mana_regeneration_rate,
            cardMilitary.damage_to_different_faction_rate, cardMilitary.resistance_to_different_faction_rate,
            cardMilitary.damage_to_same_faction_rate, cardMilitary.resistance_to_same_faction_rate
        );
        return cardMilitary;
    }
    public CardMilitary GetNewBreakthroughPower(CardMilitary c, double coefficient)
    {
        CardMilitary orginCard = new CardMilitary();
        orginCard = orginCard.GetCardMilitaryById(c.id);
        CardMilitary cardMilitary = new CardMilitary
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
        cardMilitary.power = PowerManager.CalculatePower(
            cardMilitary.health,
            cardMilitary.physical_attack, cardMilitary.physical_defense,
            cardMilitary.magical_attack, cardMilitary.magical_defense,
            cardMilitary.chemical_attack, cardMilitary.chemical_defense,
            cardMilitary.atomic_attack, cardMilitary.atomic_defense,
            cardMilitary.mental_attack, cardMilitary.mental_defense,
            cardMilitary.speed,
            cardMilitary.critical_damage_rate, cardMilitary.critical_rate,
            cardMilitary.penetration_rate, cardMilitary.evasion_rate,
            cardMilitary.damage_absorption_rate, cardMilitary.vitality_regeneration_rate,
            cardMilitary.accuracy_rate, cardMilitary.lifesteal_rate,
            cardMilitary.shield_strength, cardMilitary.tenacity, cardMilitary.resistance_rate,
            cardMilitary.combo_rate, cardMilitary.reflection_rate,
            cardMilitary.mana, cardMilitary.mana_regeneration_rate,
            cardMilitary.damage_to_different_faction_rate, cardMilitary.resistance_to_different_faction_rate,
            cardMilitary.damage_to_same_faction_rate, cardMilitary.resistance_to_same_faction_rate
        );
        return cardMilitary;
    }
    public static List<string> GetUniqueCardMilitaryTypes()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "Select distinct type from card_military";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                typeList.Add(reader.GetString(0));
            }
        }
        return typeList;
    }
    public List<CardMilitary> GetCardMilitary(string type,int pageSize, int offset)
    {
        List<CardMilitary> CardMilitaryList = new List<CardMilitary>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from card_military where type= @type 
                ORDER BY card_military.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(card_military.name, '[0-9]+$') AS UNSIGNED), card_military.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardMilitary CardMilitary = new CardMilitary
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

                    CardMilitaryList.Add(CardMilitary);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardMilitaryList;
    }
    public int GetCardMilitaryCount(string type){
        int count =0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from card_military where type= @type";
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
    public List<CardMilitary> GetCardMilitaryCollection(string type,int pageSize, int offset)
    {
        List<CardMilitary> CardMilitaryList = new List<CardMilitary>();
        int user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT m.*, CASE WHEN mg.card_military_id IS NULL THEN 'block' WHEN mg.status = 'pending' THEN 'pending' WHEN mg.status = 'available' THEN 'available' END AS status 
                FROM card_military m LEFT JOIN card_military_gallery mg ON m.id = mg.card_military_id and mg.user_id = @userId where m.type=@type 
                ORDER BY m.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardMilitary CardMilitary = new CardMilitary
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
                        status=reader.GetString("status"),
                    };

                    CardMilitaryList.Add(CardMilitary);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardMilitaryList;
    }
    public List<CardMilitary> GetUserCardMilitary(string type,int pageSize, int offset)
    {
        List<CardMilitary> CardMilitaryList = new List<CardMilitary>();
        int user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT um.*, m.*, fcm.*
                FROM user_card_military um
                LEFT JOIN card_military m ON um.card_military_id = m.id 
                LEFT JOIN fact_card_military fcm ON fcm.user_id = um.user_id AND fcm.user_card_military_id = um.card_military_id
                WHERE um.user_id = @userId AND m.type = @type
                ORDER BY m.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name
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
                    CardMilitary CardMilitary = new CardMilitary
                    {
                        id = reader.GetInt32("card_military_id"),
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

                    CardMilitaryList.Add(CardMilitary);
                }
                CardMilitaryList = GetFinalPower(CardMilitaryList);
                CardMilitaryList = GetAllEquipmentPower(CardMilitaryList);
                CardMilitaryList = GetAllRankPower(CardMilitaryList);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardMilitaryList;
    }
    public List<CardMilitary> GetUserCardMilitaryTeam(int teamId)
    {
        List<CardMilitary> CardMilitaryList = new List<CardMilitary>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT um.*, m.*, fcm.*
                FROM user_card_military um
                LEFT JOIN card_military m ON um.card_military_id = m.id 
                LEFT JOIN fact_card_military fcm ON fcm.user_id = um.user_id AND fcm.user_card_military_id = um.card_military_id
                WHERE um.user_id = @userId AND fcm.team_id = @team_id
                ORDER BY m.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", User.CurrentUserId);
                command.Parameters.AddWithValue("@team_id", teamId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardMilitary CardMilitary = new CardMilitary
                    {
                        id = reader.GetInt32("card_military_id"),
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

                    CardMilitaryList.Add(CardMilitary);
                }
                CardMilitaryList = GetFinalPower(CardMilitaryList);
                CardMilitaryList = GetAllEquipmentPower(CardMilitaryList);
                CardMilitaryList = GetAllRankPower(CardMilitaryList);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardMilitaryList;
    }
    public Dictionary<string, int> GetUniqueCardMilitaryTypesTeam(int teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = @"SELECT distinct c.type, count(c.type) as number
            FROM user_card_military uc
            LEFT JOIN card_military c ON uc.card_military_id = c.id 
            LEFT JOIN fact_card_military fch ON fch.user_id = uc.user_id AND fch.user_card_military_id = uc.card_military_id
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
    public bool UpdateTeamFactCardMilitary(int? team_id,string position, int card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                Update fact_card_military set team_id=@team_id, position=@position where user_id=@user_id 
                and user_card_military_id=@user_card_military_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_military_id", card_id);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public int GetUserCardMilitaryCount(string type){
        int count =0;
        int user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from card_military m, user_card_military um where m.id=um.card_military_id and um.user_id=@userId and m.type= @type";
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
    public List<CardMilitary> GetCardMilitaryRandom(string type,int pageSize)
    {
        List<CardMilitary> CardMilitaryList = new List<CardMilitary>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from card_military where type= @type ORDER BY RAND() limit @limit";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardMilitary CardMilitary = new CardMilitary
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

                    CardMilitaryList.Add(CardMilitary);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardMilitaryList;
    }
    public List<CardMilitary> GetAllCardMilitary(string type)
    {
        List<CardMilitary> CardMilitaryList = new List<CardMilitary>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from card_military where type= @type";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardMilitary CardMilitary = new CardMilitary
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

                    CardMilitaryList.Add(CardMilitary);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardMilitaryList;
    }
    public bool InsertUserCardMilitary(CardMilitary CardMilitary)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_military 
                WHERE user_id = @user_id AND card_military_id = @card_military_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_military_id", CardMilitary.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_card_military (
                    user_id, card_military_id, level, experiment, star, block, quantity, power, health, physical_attack, 
                    physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, 
                    atomic_defense, mental_attack, mental_defense, speed, critical_damage_rate, critical_rate, 
                    penetration_rate, evasion_rate, damage_absorption_rate, vitality_regeneration_rate, accuracy_rate, 
                    lifesteal_rate, shield_strength, tenacity, resistance_rate, combo_rate, reflection_rate, 
                    mana, mana_regeneration_rate, damage_to_different_faction_rate, 
                    resistance_to_different_faction_rate, damage_to_same_faction_rate, resistance_to_same_faction_rate
                ) VALUES (
                    @user_id, @card_military_id, @level, @experiment, @star, @block, @quantity, @power, @health, @physical_attack, 
                    @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, 
                    @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage_rate, @critical_rate, 
                    @penetration_rate, @evasion_rate, @damage_absorption_rate, @vitality_regeneration_rate, @accuracy_rate, 
                    @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate, @combo_rate, @reflection_rate, 
                    @mana, @mana_regeneration_rate, @damage_to_different_faction_rate, 
                    @resistance_to_different_faction_rate, @damage_to_same_faction_rate, @resistance_to_same_faction_rate
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@card_military_id", CardMilitary.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", 0);
                    command.Parameters.AddWithValue("@power", CardMilitary.power);
                    command.Parameters.AddWithValue("@health", CardMilitary.health);
                    command.Parameters.AddWithValue("@physical_attack", CardMilitary.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", CardMilitary.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", CardMilitary.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", CardMilitary.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", CardMilitary.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", CardMilitary.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", CardMilitary.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", CardMilitary.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", CardMilitary.mental_attack);
                    command.Parameters.AddWithValue("@mental_defense", CardMilitary.mental_defense);
                    command.Parameters.AddWithValue("@speed", CardMilitary.speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", CardMilitary.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", CardMilitary.critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", CardMilitary.penetration_rate);
                    command.Parameters.AddWithValue("@evasion_rate", CardMilitary.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", CardMilitary.damage_absorption_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", CardMilitary.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", CardMilitary.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", CardMilitary.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", CardMilitary.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", CardMilitary.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", CardMilitary.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", CardMilitary.combo_rate);
                    command.Parameters.AddWithValue("@reflection_rate", CardMilitary.reflection_rate);
                    command.Parameters.AddWithValue("@mana", CardMilitary.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", CardMilitary.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", CardMilitary.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", CardMilitary.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", CardMilitary.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", CardMilitary.resistance_to_same_faction_rate);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_card_military
                    SET quantity = quantity + 1
                    WHERE user_id = @user_id AND card_military_id = @card_military_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@card_military_id", CardMilitary.id);

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
    public bool UpdateCardMilitaryLevel(CardMilitary cardMilitary, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_military
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
                WHERE user_id = @user_id AND card_military_id = @card_military_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_military_id", cardMilitary.id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", cardMilitary.power);
                command.Parameters.AddWithValue("@health", cardMilitary.health);
                command.Parameters.AddWithValue("@physical_attack", cardMilitary.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", cardMilitary.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", cardMilitary.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", cardMilitary.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", cardMilitary.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", cardMilitary.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", cardMilitary.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", cardMilitary.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", cardMilitary.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", cardMilitary.mental_defense);
                command.Parameters.AddWithValue("@speed", cardMilitary.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", cardMilitary.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", cardMilitary.critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", cardMilitary.penetration_rate);
                command.Parameters.AddWithValue("@evasion_rate", cardMilitary.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardMilitary.damage_absorption_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardMilitary.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@accuracy_rate", cardMilitary.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardMilitary.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", cardMilitary.shield_strength);
                command.Parameters.AddWithValue("@tenacity", cardMilitary.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardMilitary.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", cardMilitary.combo_rate);
                command.Parameters.AddWithValue("@reflection_rate", cardMilitary.reflection_rate);
                command.Parameters.AddWithValue("@mana", cardMilitary.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardMilitary.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardMilitary.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardMilitary.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardMilitary.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardMilitary.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateCardMilitaryBreakthrough(CardMilitary cardMilitary, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_military
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
                WHERE user_id = @user_id AND card_military_id = @card_military_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_military_id", cardMilitary.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", cardMilitary.power);
                command.Parameters.AddWithValue("@health", cardMilitary.health);
                command.Parameters.AddWithValue("@physical_attack", cardMilitary.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", cardMilitary.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", cardMilitary.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", cardMilitary.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", cardMilitary.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", cardMilitary.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", cardMilitary.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", cardMilitary.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", cardMilitary.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", cardMilitary.mental_defense);
                command.Parameters.AddWithValue("@speed", cardMilitary.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", cardMilitary.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", cardMilitary.critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", cardMilitary.penetration_rate);
                command.Parameters.AddWithValue("@evasion_rate", cardMilitary.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardMilitary.damage_absorption_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardMilitary.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@accuracy_rate", cardMilitary.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardMilitary.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", cardMilitary.shield_strength);
                command.Parameters.AddWithValue("@tenacity", cardMilitary.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardMilitary.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", cardMilitary.combo_rate);
                command.Parameters.AddWithValue("@reflection_rate", cardMilitary.reflection_rate);
                command.Parameters.AddWithValue("@mana", cardMilitary.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardMilitary.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardMilitary.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardMilitary.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardMilitary.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardMilitary.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool InsertFactCardMilitary(CardMilitary cardMilitary)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                INSERT INTO fact_card_military (
                user_id, user_card_military_id, team_id, position, role, 
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
                @user_id, @user_card_military_id, @team_id, @position, @role, 
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
                command.Parameters.AddWithValue("@user_card_military_id", cardMilitary.id);
                command.Parameters.AddWithValue("@team_id", null);
                command.Parameters.AddWithValue("@position", null);
                command.Parameters.AddWithValue("@role", null);
                command.Parameters.AddWithValue("@all_power", cardMilitary.power);
                command.Parameters.AddWithValue("@all_health", cardMilitary.health);
                command.Parameters.AddWithValue("@all_physical_attack", cardMilitary.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", cardMilitary.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", cardMilitary.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", cardMilitary.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", cardMilitary.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", cardMilitary.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", cardMilitary.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", cardMilitary.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", cardMilitary.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", cardMilitary.mental_defense);
                command.Parameters.AddWithValue("@all_speed", cardMilitary.speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", cardMilitary.critical_damage_rate);
                command.Parameters.AddWithValue("@all_critical_rate", cardMilitary.critical_rate);
                command.Parameters.AddWithValue("@all_penetration_rate", cardMilitary.penetration_rate);
                command.Parameters.AddWithValue("@all_evasion_rate", cardMilitary.evasion_rate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", cardMilitary.damage_absorption_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", cardMilitary.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@all_accuracy_rate", cardMilitary.accuracy_rate);
                command.Parameters.AddWithValue("@all_mana", cardMilitary.mana);
                command.Parameters.AddWithValue("@all_lifesteal_rate", cardMilitary.lifesteal_rate);
                command.Parameters.AddWithValue("@all_shield_strength", cardMilitary.shield_strength);
                command.Parameters.AddWithValue("@all_tenacity", cardMilitary.tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", cardMilitary.resistance_rate);
                command.Parameters.AddWithValue("@all_combo_rate", cardMilitary.combo_rate);
                command.Parameters.AddWithValue("@all_reflection_rate", cardMilitary.reflection_rate);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", cardMilitary.mana_regeneration_rate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", cardMilitary.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", cardMilitary.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", cardMilitary.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", cardMilitary.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateFactCardMilitary(CardMilitary cardMilitary)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE fact_card_military
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
                WHERE user_id = @user_id AND user_card_military_id = @user_card_military_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_military_id", cardMilitary.id);
                command.Parameters.AddWithValue("@all_power", cardMilitary.power);
                command.Parameters.AddWithValue("@all_health", cardMilitary.health);
                command.Parameters.AddWithValue("@all_physical_attack", cardMilitary.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", cardMilitary.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", cardMilitary.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", cardMilitary.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", cardMilitary.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", cardMilitary.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", cardMilitary.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", cardMilitary.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", cardMilitary.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", cardMilitary.mental_defense);
                command.Parameters.AddWithValue("@all_speed", cardMilitary.speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", cardMilitary.critical_damage_rate);
                command.Parameters.AddWithValue("@all_critical_rate", cardMilitary.critical_rate);
                command.Parameters.AddWithValue("@all_penetration_rate", cardMilitary.penetration_rate);
                command.Parameters.AddWithValue("@all_evasion_rate", cardMilitary.evasion_rate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", cardMilitary.damage_absorption_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", cardMilitary.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@all_accuracy_rate", cardMilitary.accuracy_rate);
                command.Parameters.AddWithValue("@all_mana", cardMilitary.mana);
                command.Parameters.AddWithValue("@all_lifesteal_rate", cardMilitary.lifesteal_rate);
                command.Parameters.AddWithValue("@all_shield_strength", cardMilitary.shield_strength);
                command.Parameters.AddWithValue("@all_tenacity", cardMilitary.tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", cardMilitary.resistance_rate);
                command.Parameters.AddWithValue("@all_combo_rate", cardMilitary.combo_rate);
                command.Parameters.AddWithValue("@all_reflection_rate", cardMilitary.reflection_rate);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", cardMilitary.mana_regeneration_rate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", cardMilitary.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", cardMilitary.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", cardMilitary.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", cardMilitary.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public CardMilitary GetCardMilitaryById(int Id)
    {
        CardMilitary CardMilitary = new CardMilitary();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from card_military where card_military.id=@id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardMilitary = new CardMilitary
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
        return CardMilitary;
    }
    public CardMilitary GetUserCardMilitaryById(int Id)
    {
        CardMilitary card = new CardMilitary();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_card_military where user_card_military.card_military_id=@id 
                and user_card_military.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new CardMilitary
                    {
                        id = reader.GetInt32("card_military_id"),
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
    public void InsertCardMilitaryGallery(int Id)
    {
        CardMilitary CardMilitaryFromDB = GetCardMilitaryById(Id);
        int percent = 0;
        if (CardMilitaryFromDB.rare.Equals("LG"))
        {
            percent = 20;
        }
        else if (CardMilitaryFromDB.rare.Equals("UR"))
        {
            percent = 10;
        }
        else if (CardMilitaryFromDB.rare.Equals("SSR"))
        {
            percent = 5;
        }
        else if (CardMilitaryFromDB.rare.Equals("SR"))
        {
            percent = 2;
        }
        else if (CardMilitaryFromDB.rare.Equals("MR"))
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
                FROM card_military_gallery 
                WHERE user_id = @user_id AND card_military_id = @card_military_id;
                ";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_military_id", Id);

                int recordCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (recordCount == 0)
                {
                    string query = @"
                    INSERT INTO card_military_gallery (
                        user_id, card_military_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
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
                        @user_id, @card_military_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
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
                    command.Parameters.AddWithValue("@card_military_id", Id);
                    command.Parameters.AddWithValue("@status", "pending");
                    command.Parameters.AddWithValue("@current_star", 0);
                    command.Parameters.AddWithValue("@temp_star", 0);
                    command.Parameters.AddWithValue("@power", CardMilitaryFromDB.power);
                    command.Parameters.AddWithValue("@health", CardMilitaryFromDB.health);
                    command.Parameters.AddWithValue("@physical_attack", CardMilitaryFromDB.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", CardMilitaryFromDB.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", CardMilitaryFromDB.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", CardMilitaryFromDB.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", CardMilitaryFromDB.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", CardMilitaryFromDB.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", CardMilitaryFromDB.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", CardMilitaryFromDB.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", CardMilitaryFromDB.magical_attack);
                    command.Parameters.AddWithValue("@mental_defense", CardMilitaryFromDB.magical_defense);
                    command.Parameters.AddWithValue("@speed", CardMilitaryFromDB.speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", CardMilitaryFromDB.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", CardMilitaryFromDB.critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", CardMilitaryFromDB.penetration_rate);
                    command.Parameters.AddWithValue("@evasion_rate", CardMilitaryFromDB.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", CardMilitaryFromDB.damage_absorption_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", CardMilitaryFromDB.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", CardMilitaryFromDB.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", CardMilitaryFromDB.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", CardMilitaryFromDB.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", CardMilitaryFromDB.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", CardMilitaryFromDB.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", CardMilitaryFromDB.combo_rate);
                    command.Parameters.AddWithValue("@reflection_rate", CardMilitaryFromDB.reflection_rate);
                    command.Parameters.AddWithValue("@mana", CardMilitaryFromDB.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", CardMilitaryFromDB.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", CardMilitaryFromDB.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", CardMilitaryFromDB.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", CardMilitaryFromDB.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", CardMilitaryFromDB.resistance_to_same_faction_rate);
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
    public void UpdateStatusCardMilitaryGallery(int Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "update card_military_gallery set status=@status where user_id=@user_id and card_military_id=@card_military_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_military_id", Id);
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
    public List<CardMilitary> GetCardMilitaryWithPrice(string type,int pageSize, int offset)
    {
        List<CardMilitary> CardMilitaryList = new List<CardMilitary>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select m.*, mt.price, cu.image as currency_image, cu.id as currency_id
                from card_military m, card_military_trade mt, currency cu
                where m.id=mt.card_military_id and mt.currency_id = cu.id and m.type =@type
                ORDER BY m.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardMilitary CardMilitary = new CardMilitary
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
                    CardMilitary.currency = new Currency{
                        id = reader.GetInt32("currency_id"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("price")
                    };

                    CardMilitaryList.Add(CardMilitary);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardMilitaryList;
    }
    public int GetCardMilitaryWithPriceCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*)
                from card_military m, card_military_trade mt, currency cu
                where m.id=mt.card_military_id and mt.currency_id = cu.id and m.type =@type;";
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
    public CardMilitary SumPowerCardMilitaryGallery()
    {
        CardMilitary sumCardMilitary = new CardMilitary();
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
            FROM card_military_gallery 
            WHERE user_id = @user_id AND status = 'available';";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumCardMilitary.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumCardMilitary.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumCardMilitary.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumCardMilitary.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumCardMilitary.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumCardMilitary.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumCardMilitary.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumCardMilitary.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumCardMilitary.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumCardMilitary.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumCardMilitary.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumCardMilitary.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumCardMilitary.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumCardMilitary.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                        sumCardMilitary.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumCardMilitary.penetration_rate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                        sumCardMilitary.evasion_rate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                        sumCardMilitary.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                        sumCardMilitary.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                        sumCardMilitary.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                        sumCardMilitary.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                        sumCardMilitary.shield_strength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                        sumCardMilitary.tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                        sumCardMilitary.resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                        sumCardMilitary.combo_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                        sumCardMilitary.reflection_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                        sumCardMilitary.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                        sumCardMilitary.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                        sumCardMilitary.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                        sumCardMilitary.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                        sumCardMilitary.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                        sumCardMilitary.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                        sumCardMilitary.percent_all_health = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDouble("total_percent_all_health");
                        sumCardMilitary.percent_all_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDouble("total_percent_all_physical_attack");
                        sumCardMilitary.percent_all_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDouble("total_percent_all_physical_defense");
                        sumCardMilitary.percent_all_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDouble("total_percent_all_magical_attack");
                        sumCardMilitary.percent_all_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDouble("total_percent_all_magical_defense");
                        sumCardMilitary.percent_all_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDouble("total_percent_all_chemical_attack");
                        sumCardMilitary.percent_all_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDouble("total_percent_all_chemical_defense");
                        sumCardMilitary.percent_all_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDouble("total_percent_all_atomic_attack");
                        sumCardMilitary.percent_all_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDouble("total_percent_all_atomic_defense");
                        sumCardMilitary.percent_all_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDouble("total_percent_all_mental_attack");
                        sumCardMilitary.percent_all_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDouble("total_percent_all_mental_defense");
                    }
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return sumCardMilitary;
    }
}
