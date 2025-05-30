using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;


public class PowerManager
{
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
    public const double coefficient = 0.5;

    // Start is called before the first frame update
    public PowerManager()
    {
        power = 0;
        health = 0;
        physical_attack = 0;
        physical_defense = 0;
        magical_attack = 0;
        magical_defense = 0;
        chemical_attack = 0;
        chemical_defense = 0;
        atomic_attack = 0;
        atomic_defense = 0;
        mental_attack = 0;
        mental_defense = 0;
        speed = 0;
        critical_damage_rate = 0;
        critical_rate = 0;
        penetration_rate = 0;
        evasion_rate = 0;
        damage_absorption_rate = 0;
        vitality_regeneration_rate = 0;
        accuracy_rate = 0;
        lifesteal_rate = 0;
        mana = 0;
        mana_regeneration_rate = 0;
        shield_strength = 0;
        tenacity = 0;
        resistance_rate = 0;
        combo_rate = 0;
        reflection_rate = 0;
        damage_to_different_faction_rate = 0;
        resistance_to_different_faction_rate = 0;
        damage_to_same_faction_rate = 0;
        resistance_to_same_faction_rate = 0;
        percent_all_health = 0;
        percent_all_physical_attack = 0;
        percent_all_physical_defense = 0;
        percent_all_magical_attack = 0;
        percent_all_magical_defense = 0;
        percent_all_chemical_attack = 0;
        percent_all_chemical_defense = 0;
        percent_all_atomic_attack = 0;
        percent_all_atomic_defense = 0;
        percent_all_mental_attack = 0;
        percent_all_mental_defense = 0;
    }

    public void CalculatePower()
    {
        // GetAchievementsPower();
        // GetBooksPower();
        // GetBordersPower();
        // GetAvatarsPower();
        // GetCardHeroesPower();
        // GetCardCaptainsPower();
        // GetCardColonelsPower();
        // GetCardGeneralsPower();
        // GetCardAdmiralsPower();
        // GetCardMonstersPower();
        // GetCardMilitaryPower();
        // GetCardSpellPower();
        // GetCollaborationsPower();
        // GetCollaborationEquipmentsPower();
        // GetEquipmentsPower();
        // GetMagicFormationCirlcePower();
        // GetRelicsPower();
        // GetMedalsPower();
        // GetSkillsPower();
        // GetSymbolsPower();
        // GetPetsPower();
        // GetTitlesPower();
        // GetTalismanPower();
        // GetPuppetPower();
        // GetAlchemyPower();
        // GetForgePower();
        // GetCardLifePower();
    }

    
    
}
