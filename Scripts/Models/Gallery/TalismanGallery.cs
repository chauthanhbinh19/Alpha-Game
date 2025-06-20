using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class TalismanGallery
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
    private string description1;
    private string status1;
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
    public double critical_resistance_rate { get; set; }
    public double ignore_critical_rate { get; set; }
    public double penetration_rate { get; set; }
    public double penetration_resistance_rate { get; set; }
    public double evasion_rate { get; set; }
    public double damage_absorption_rate { get; set; }
    public double ignore_damage_absorption_rate { get; set; }
    public double absorbed_damage_rate { get; set; }
    public double vitality_regeneration_rate { get; set; }
    public double vitality_regeneration_resistance_rate { get; set; }
    public double accuracy_rate { get; set; }
    public double lifesteal_rate { get; set; }
    public float mana { get; set; }
    public double mana_regeneration_rate { get; set; }
    public double shield_strength { get; set; }
    public double tenacity { get; set; }
    public double resistance_rate { get; set; }
    public double combo_rate { get; set; }
    public double ignore_combo_rate { get; set; }
    public double combo_damage_rate { get; set; }
    public double combo_resistance_rate { get; set; }
    public double stun_rate { get; set; }
    public double ignore_stun_rate { get; set; }
    public double reflection_rate { get; set; }
    public double ignore_reflection_rate { get; set; }
    public double reflection_damage_rate { get; set; }
    public double reflection_resistance_rate { get; set; }
    public double damage_to_different_faction_rate { get; set; }
    public double resistance_to_different_faction_rate { get; set; }
    public double damage_to_same_faction_rate { get; set; }
    public double resistance_to_same_faction_rate { get; set; }
    public double normal_damage_rate { get; set; }
    public double normal_resistance_rate { get; set; }
    public double skill_damage_rate { get; set; }
    public double skill_resistance_rate { get; set; }
    public string description { get => description1; set => description1 = value; }
    public string status { get => status1; set => status1 = value; }
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
    public TalismanGallery()
    {
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
