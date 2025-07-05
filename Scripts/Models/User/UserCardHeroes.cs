using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserCardHeroes : BaseEntity
{
    public string id { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public string rare { get; set; }
    public int quality { get; set; }
    public string type { get; set; }
    public int star { get; set; }
    public int level { get; set; }
    public int experiment { get; set; }
    public int quantity { get; set; }
    public bool block { get; set; }
    public string position { get; set; }
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
    public double all_critical_resistance_rate { get; set; }
    public double all_ignore_critical_rate { get; set; }
    public double all_penetration_rate { get; set; }
    public double all_penetration_resistance_rate { get; set; }
    public double all_evasion_rate { get; set; }
    public double all_damage_absorption_rate { get; set; }
    public double all_ignore_damage_absorption_rate { get; set; }
    public double all_absorbed_damage_rate { get; set; }
    public double all_vitality_regeneration_rate { get; set; }
    public double all_vitality_regeneration_resistance_rate { get; set; }
    public double all_accuracy_rate { get; set; }
    public double all_lifesteal_rate { get; set; }
    public float all_mana { get; set; }
    public double all_mana_regeneration_rate { get; set; }
    public double all_shield_strength { get; set; }
    public double all_tenacity { get; set; }
    public double all_resistance_rate { get; set; }
    public double all_combo_rate { get; set; }
    public double all_ignore_combo_rate { get; set; }
    public double all_combo_damage_rate { get; set; }
    public double all_combo_resistance_rate { get; set; }
    public double all_stun_rate { get; set; }
    public double all_ignore_stun_rate { get; set; }
    public double all_reflection_rate { get; set; }
    public double all_ignore_reflection_rate { get; set; }
    public double all_reflection_damage_rate { get; set; }
    public double all_reflection_resistance_rate { get; set; }
    public double all_damage_to_different_faction_rate { get; set; }
    public double all_resistance_to_different_faction_rate { get; set; }
    public double all_damage_to_same_faction_rate { get; set; }
    public double all_resistance_to_same_faction_rate { get; set; }
    public double all_normal_damage_rate { get; set; }
    public double all_normal_resistance_rate { get; set; }
    public double all_skill_damage_rate { get; set; }
    public double all_skill_resistance_rate { get; set; }
    public string description { get; set; }
    public string status { get; set; }
    public int team_id { get; set; }
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
    public UserCardHeroes()
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
}
