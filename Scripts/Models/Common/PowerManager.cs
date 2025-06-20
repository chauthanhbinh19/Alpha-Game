using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;


public class PowerManager
{
    public double power { get; set; } = 0;
    public double health { get; set; } = 0;
    public double physical_attack { get; set; } = 0;
    public double physical_defense { get; set; } = 0;
    public double magical_attack { get; set; } = 0;
    public double magical_defense { get; set; } = 0;
    public double chemical_attack { get; set; } = 0;
    public double chemical_defense { get; set; } = 0;
    public double atomic_attack { get; set; } = 0;
    public double atomic_defense { get; set; } = 0;
    public double mental_attack { get; set; } = 0;
    public double mental_defense { get; set; } = 0;
    public double speed { get; set; } = 0;
    public double critical_damage_rate { get; set; } = 0;
    public double critical_rate { get; set; } = 0;
    public double critical_resistance_rate { get; set; } = 0;
    public double ignore_critical_rate { get; set; } = 0;
    public double penetration_rate { get; set; } = 0;
    public double penetration_resistance_rate { get; set; } = 0;
    public double evasion_rate { get; set; } = 0;
    public double damage_absorption_rate { get; set; } = 0;
    public double ignore_damage_absorption_rate { get; set; } = 0;
    public double absorbed_damage_rate { get; set; } = 0;
    public double vitality_regeneration_rate { get; set; } = 0;
    public double vitality_regeneration_resistance_rate { get; set; } = 0;
    public double accuracy_rate { get; set; } = 0;
    public double lifesteal_rate { get; set; } = 0;
    public float mana { get; set; } = 0;
    public double mana_regeneration_rate { get; set; } = 0;
    public double shield_strength { get; set; } = 0;
    public double tenacity { get; set; } = 0;
    public double resistance_rate { get; set; } = 0;
    public double combo_rate { get; set; } = 0;
    public double ignore_combo_rate { get; set; } = 0;
    public double combo_damage_rate { get; set; } = 0;
    public double combo_resistance_rate { get; set; } = 0;
    public double stun_rate { get; set; } = 0;
    public double ignore_stun_rate { get; set; } = 0;
    public double reflection_rate { get; set; } = 0;
    public double ignore_reflection_rate { get; set; } = 0;
    public double reflection_damage_rate { get; set; } = 0;
    public double reflection_resistance_rate { get; set; } = 0;
    public double damage_to_different_faction_rate { get; set; } = 0;
    public double resistance_to_different_faction_rate { get; set; } = 0;
    public double damage_to_same_faction_rate { get; set; } = 0;
    public double resistance_to_same_faction_rate { get; set; } = 0;
    public double normal_damage_rate { get; set; } = 0;
    public double normal_resistance_rate { get; set; } = 0;
    public double skill_damage_rate { get; set; } = 0;
    public double skill_resistance_rate { get; set; } = 0;
    public double percent_all_health { get; set; } = 0;
    public double percent_all_physical_attack { get; set; } = 0;
    public double percent_all_physical_defense { get; set; } = 0;
    public double percent_all_magical_attack { get; set; } = 0;
    public double percent_all_magical_defense { get; set; } = 0;
    public double percent_all_chemical_attack { get; set; } = 0;
    public double percent_all_chemical_defense { get; set; } = 0;
    public double percent_all_atomic_attack { get; set; } = 0;
    public double percent_all_atomic_defense { get; set; } = 0;
    public double percent_all_mental_attack { get; set; } = 0;
    public double percent_all_mental_defense { get; set; } = 0;
    public const double coefficient = 0.5;

    // Start is called before the first frame update
    public PowerManager()
    {

    }
    
    
}
