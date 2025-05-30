using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserCardAdmirals
{
    private string id1;
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

    public string id { get => id1; set => id1 = value; }
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
    public UserCardAdmirals()
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
}
