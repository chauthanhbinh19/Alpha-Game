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
    public double critical_damage { get; set; }
    public double critical_rate { get; set; }
    public double armor_penetration { get; set; }
    public double avoid { get; set; }
    public double absorbs_damage { get; set; }
    public double regenerate_vitality { get; set; }
    public double accuracy { get; set; }
    public float mana { get; set; }
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
        critical_damage = 0;
        critical_rate = 0;
        armor_penetration = 0;
        avoid = 0;
        absorbs_damage = 0;
        regenerate_vitality = 0;
        accuracy = 0;
        mana = 0;
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
        GetAchievementsPower();
        GetBooksPower();
        GetBordersPower();
        GetCardHeroesPower();
        GetCardCaptainsPower();
        GetCardColonelsPower();
        GetCardGeneralsPower();
        GetCardAdmiralsPower();
        GetCardMonstersPower();
        GetCardMilitaryPower();
        GetCardSpellPower();
        GetCollaborationsPower();
        GetCollaborationEquipmentsPower();
        GetEquipmentsPower();
        GetMagicFormationCirlcePower();
        GetRelicsPower();
        GetMedalsPower();
        GetSkillsPower();
        GetSymbolsPower();
        GetPetsPower();
        GetTitlesPower();
    }
    public void InsertUserStats()
    {
        CalculatePower();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"INSERT INTO user_stats (
                        user_id, all_power, all_health, all_physical_attack, all_physical_defense,
                        all_magical_attack, all_magical_defense, all_chemical_attack, all_chemical_defense,
                        all_atomic_attack, all_atomic_defense, all_mental_attack, all_mental_defense,
                        all_speed, all_critical_damage, all_critical_rate, all_armor_penetration,
                        all_avoid, all_absorbs_damage, all_regenerate_vitality, all_accuracy, all_mana,
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense,
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack,
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense,
                        percent_all_mental_attack, percent_all_mental_defense
                    )
                    VALUES (
                        @userId, @allPower, @allHealth, @allPhysicalAttack, @allPhysicalDefense,
                        @allMagicalAttack, @allMagicalDefense, @allChemicalAttack, @allChemicalDefense,
                        @allAtomicAttack, @allAtomicDefense, @allMentalAttack, @allMentalDefense,
                        @allSpeed, @allCriticalDamage, @allCriticalRate, @allArmorPenetration,
                        @allAvoid, @allAbsorbsDamage, @allRegenerateVitality, @allAccuracy, @allMana,
                        @percentAllHealth, @percentAllPhysicalAttack, @percentAllPhysicalDefense,
                        @percentAllMagicalAttack, @percentAllMagicalDefense, @percentAllChemicalAttack,
                        @percentAllChemicalDefense, @percentAllAtomicAttack, @percentAllAtomicDefense,
                        @percentAllMentalAttack, @percentAllMentalDefense
                    );";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", User.CurrentUserId);
                command.Parameters.AddWithValue("@allPower", power);
                command.Parameters.AddWithValue("@allHealth", health);
                command.Parameters.AddWithValue("@allPhysicalAttack", physical_attack);
                command.Parameters.AddWithValue("@allPhysicalDefense", physical_defense);
                command.Parameters.AddWithValue("@allMagicalAttack", magical_attack);
                command.Parameters.AddWithValue("@allMagicalDefense", magical_defense);
                command.Parameters.AddWithValue("@allChemicalAttack", chemical_attack);
                command.Parameters.AddWithValue("@allChemicalDefense", chemical_defense);
                command.Parameters.AddWithValue("@allAtomicAttack", atomic_attack);
                command.Parameters.AddWithValue("@allAtomicDefense", atomic_defense);
                command.Parameters.AddWithValue("@allMentalAttack", mental_attack);
                command.Parameters.AddWithValue("@allMentalDefense", mental_defense);
                command.Parameters.AddWithValue("@allSpeed", speed);
                command.Parameters.AddWithValue("@allCriticalDamage", critical_damage);
                command.Parameters.AddWithValue("@allCriticalRate", critical_rate);
                command.Parameters.AddWithValue("@allArmorPenetration", armor_penetration);
                command.Parameters.AddWithValue("@allAvoid", avoid);
                command.Parameters.AddWithValue("@allAbsorbsDamage", absorbs_damage);
                command.Parameters.AddWithValue("@allRegenerateVitality", regenerate_vitality);
                command.Parameters.AddWithValue("@allAccuracy", accuracy);
                command.Parameters.AddWithValue("@allMana", mana);
                command.Parameters.AddWithValue("@percentAllHealth", percent_all_health);
                command.Parameters.AddWithValue("@percentAllPhysicalAttack", percent_all_physical_attack);
                command.Parameters.AddWithValue("@percentAllPhysicalDefense", percent_all_physical_defense);
                command.Parameters.AddWithValue("@percentAllMagicalAttack", percent_all_magical_attack);
                command.Parameters.AddWithValue("@percentAllMagicalDefense", percent_all_magical_defense);
                command.Parameters.AddWithValue("@percentAllChemicalAttack", percent_all_chemical_attack);
                command.Parameters.AddWithValue("@percentAllChemicalDefense", percent_all_chemical_defense);
                command.Parameters.AddWithValue("@percentAllAtomicAttack", percent_all_atomic_attack);
                command.Parameters.AddWithValue("@percentAllAtomicDefense", percent_all_atomic_defense);
                command.Parameters.AddWithValue("@percentAllMentalAttack", percent_all_mental_attack);
                command.Parameters.AddWithValue("@percentAllMentalDefense", percent_all_mental_defense);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public void UpdateUserStats()
    {
        CalculatePower();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"UPDATE user_stats
                SET 
                    all_power = @allPower, all_health = @allHealth, 
                    all_physical_attack = @allPhysicalAttack, all_physical_defense = @allPhysicalDefense,
                    all_magical_attack = @allMagicalAttack, all_magical_defense = @allMagicalDefense, 
                    all_chemical_attack = @allChemicalAttack, all_chemical_defense = @allChemicalDefense,
                    all_atomic_attack = @allAtomicAttack, all_atomic_defense = @allAtomicDefense, 
                    all_mental_attack = @allMentalAttack, all_mental_defense = @allMentalDefense,
                    all_speed = @allSpeed, all_critical_damage = @allCriticalDamage, 
                    all_critical_rate = @allCriticalRate, all_armor_penetration = @allArmorPenetration,
                    all_avoid = @allAvoid, all_absorbs_damage = @allAbsorbsDamage, 
                    all_regenerate_vitality = @allRegenerateVitality, all_accuracy = @allAccuracy, 
                    all_mana = @allMana, percent_all_health = @percentAllHealth, 
                    percent_all_physical_attack = @percentAllPhysicalAttack, percent_all_physical_defense = @percentAllPhysicalDefense,
                    percent_all_magical_attack = @percentAllMagicalAttack, percent_all_magical_defense = @percentAllMagicalDefense, 
                    percent_all_chemical_attack = @percentAllChemicalAttack, percent_all_chemical_defense = @percentAllChemicalDefense, 
                    percent_all_atomic_attack = @percentAllAtomicAttack, percent_all_atomic_defense = @percentAllAtomicDefense,
                    percent_all_mental_attack = @percentAllMentalAttack, percent_all_mental_defense = @percentAllMentalDefense
                WHERE user_id = @userId;
                ;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", User.CurrentUserId);
                command.Parameters.AddWithValue("@allPower", power);
                command.Parameters.AddWithValue("@allHealth", health);
                command.Parameters.AddWithValue("@allPhysicalAttack", physical_attack);
                command.Parameters.AddWithValue("@allPhysicalDefense", physical_defense);
                command.Parameters.AddWithValue("@allMagicalAttack", magical_attack);
                command.Parameters.AddWithValue("@allMagicalDefense", magical_defense);
                command.Parameters.AddWithValue("@allChemicalAttack", chemical_attack);
                command.Parameters.AddWithValue("@allChemicalDefense", chemical_defense);
                command.Parameters.AddWithValue("@allAtomicAttack", atomic_attack);
                command.Parameters.AddWithValue("@allAtomicDefense", atomic_defense);
                command.Parameters.AddWithValue("@allMentalAttack", mental_attack);
                command.Parameters.AddWithValue("@allMentalDefense", mental_defense);
                command.Parameters.AddWithValue("@allSpeed", speed);
                command.Parameters.AddWithValue("@allCriticalDamage", critical_damage);
                command.Parameters.AddWithValue("@allCriticalRate", critical_rate);
                command.Parameters.AddWithValue("@allArmorPenetration", armor_penetration);
                command.Parameters.AddWithValue("@allAvoid", avoid);
                command.Parameters.AddWithValue("@allAbsorbsDamage", absorbs_damage);
                command.Parameters.AddWithValue("@allRegenerateVitality", regenerate_vitality);
                command.Parameters.AddWithValue("@allAccuracy", accuracy);
                command.Parameters.AddWithValue("@allMana", mana);
                command.Parameters.AddWithValue("@percentAllHealth", percent_all_health);
                command.Parameters.AddWithValue("@percentAllPhysicalAttack", percent_all_physical_attack);
                command.Parameters.AddWithValue("@percentAllPhysicalDefense", percent_all_physical_defense);
                command.Parameters.AddWithValue("@percentAllMagicalAttack", percent_all_magical_attack);
                command.Parameters.AddWithValue("@percentAllMagicalDefense", percent_all_magical_defense);
                command.Parameters.AddWithValue("@percentAllChemicalAttack", percent_all_chemical_attack);
                command.Parameters.AddWithValue("@percentAllChemicalDefense", percent_all_chemical_defense);
                command.Parameters.AddWithValue("@percentAllAtomicAttack", percent_all_atomic_attack);
                command.Parameters.AddWithValue("@percentAllAtomicDefense", percent_all_atomic_defense);
                command.Parameters.AddWithValue("@percentAllMentalAttack", percent_all_mental_attack);
                command.Parameters.AddWithValue("@percentAllMentalDefense", percent_all_mental_defense);

                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public PowerManager GetUserStats()
    {
        PowerManager powerManager = new PowerManager();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                SELECT * FROM USER_STATS WHERE USER_ID=@user_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        powerManager.power = reader.IsDBNull(reader.GetOrdinal("all_power")) ? 0 : reader.GetDouble("all_power");
                        powerManager.health = reader.IsDBNull(reader.GetOrdinal("all_health")) ? 0 : reader.GetDouble("all_health");
                        powerManager.physical_attack = reader.IsDBNull(reader.GetOrdinal("all_physical_attack")) ? 0 : reader.GetDouble("all_physical_attack");
                        powerManager.physical_defense = reader.IsDBNull(reader.GetOrdinal("all_physical_defense")) ? 0 : reader.GetDouble("all_physical_defense");
                        powerManager.magical_attack = reader.IsDBNull(reader.GetOrdinal("all_magical_attack")) ? 0 : reader.GetDouble("all_magical_attack");
                        powerManager.magical_defense = reader.IsDBNull(reader.GetOrdinal("all_magical_defense")) ? 0 : reader.GetDouble("all_magical_defense");
                        powerManager.chemical_attack = reader.IsDBNull(reader.GetOrdinal("all_chemical_attack")) ? 0 : reader.GetDouble("all_chemical_attack");
                        powerManager.chemical_defense = reader.IsDBNull(reader.GetOrdinal("all_chemical_defense")) ? 0 : reader.GetDouble("all_chemical_defense");
                        powerManager.atomic_attack = reader.IsDBNull(reader.GetOrdinal("all_atomic_attack")) ? 0 : reader.GetDouble("all_atomic_attack");
                        powerManager.atomic_defense = reader.IsDBNull(reader.GetOrdinal("all_atomic_defense")) ? 0 : reader.GetDouble("all_atomic_defense");
                        powerManager.mental_attack = reader.IsDBNull(reader.GetOrdinal("all_mental_attack")) ? 0 : reader.GetDouble("all_mental_attack");
                        powerManager.mental_defense = reader.IsDBNull(reader.GetOrdinal("all_mental_defense")) ? 0 : reader.GetDouble("all_mental_defense");
                        powerManager.speed = reader.IsDBNull(reader.GetOrdinal("all_speed")) ? 0 : reader.GetDouble("all_speed");
                        powerManager.critical_damage = reader.IsDBNull(reader.GetOrdinal("all_critical_damage")) ? 0 : reader.GetDouble("all_critical_damage");
                        powerManager.critical_rate = reader.IsDBNull(reader.GetOrdinal("all_critical_rate")) ? 0 : reader.GetDouble("all_critical_rate");
                        powerManager.armor_penetration = reader.IsDBNull(reader.GetOrdinal("all_armor_penetration")) ? 0 : reader.GetDouble("all_armor_penetration");
                        powerManager.avoid = reader.IsDBNull(reader.GetOrdinal("all_avoid")) ? 0 : reader.GetDouble("all_avoid");
                        powerManager.absorbs_damage = reader.IsDBNull(reader.GetOrdinal("all_absorbs_damage")) ? 0 : reader.GetDouble("all_absorbs_damage");
                        powerManager.regenerate_vitality = reader.IsDBNull(reader.GetOrdinal("all_regenerate_vitality")) ? 0 : reader.GetDouble("all_regenerate_vitality");
                        powerManager.accuracy = reader.IsDBNull(reader.GetOrdinal("all_accuracy")) ? 0 : reader.GetDouble("all_accuracy");
                        powerManager.mana = reader.IsDBNull(reader.GetOrdinal("all_mana")) ? 0 : (float)reader.GetDouble("all_mana");
                        powerManager.percent_all_health = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDouble("percent_all_health");
                        powerManager.percent_all_physical_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDouble("percent_all_physical_attack");
                        powerManager.percent_all_physical_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDouble("percent_all_physical_defense");
                        powerManager.percent_all_magical_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDouble("percent_all_magical_attack");
                        powerManager.percent_all_magical_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDouble("percent_all_magical_defense");
                        powerManager.percent_all_chemical_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDouble("percent_all_chemical_attack");
                        powerManager.percent_all_chemical_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDouble("percent_all_chemical_defense");
                        powerManager.percent_all_atomic_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDouble("percent_all_atomic_attack");
                        powerManager.percent_all_atomic_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDouble("percent_all_atomic_defense");
                        powerManager.percent_all_mental_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDouble("percent_all_mental_attack");
                        powerManager.percent_all_mental_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDouble("percent_all_mental_defense");
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return powerManager;
    }
    public double GetFinalCardHeroesPower(CardHeroes c)
    {
        PowerManager powerManager = new PowerManager();
        powerManager = powerManager.GetUserStats();
        return power = Math.Floor( (c.all_health + health + c.health * powerManager.percent_all_health/100) * coefficient +
            (c.all_physical_attack + powerManager.physical_attack + c.physical_attack * powerManager.percent_all_physical_attack/100) * coefficient +
            (c.all_physical_defense + powerManager.physical_defense + c.physical_defense * powerManager.percent_all_physical_defense/100) * coefficient +
            (c.all_magical_attack + powerManager.magical_attack + c.magical_attack * powerManager.percent_all_magical_attack/100) * coefficient +
            (c.all_magical_defense + powerManager.magical_defense + c.magical_defense * powerManager.percent_all_magical_defense/100) * coefficient +
            (c.all_chemical_attack + powerManager.chemical_attack + c.chemical_attack * powerManager.percent_all_chemical_attack/100) * coefficient +
            (c.all_chemical_defense + powerManager.chemical_defense + c.chemical_defense * powerManager.percent_all_chemical_defense/100) * coefficient +
            (c.all_atomic_attack + powerManager.atomic_attack + c.atomic_attack * powerManager.percent_all_atomic_attack/100) * coefficient +
            (c.all_atomic_defense + powerManager.atomic_defense + c.atomic_defense * powerManager.percent_all_atomic_defense/100) * coefficient +
            (c.all_mental_attack + powerManager.mental_attack + c.mental_attack * powerManager.percent_all_mental_attack/100) * coefficient +
            (c.all_mental_defense + powerManager.mental_defense + c.mental_defense * powerManager.percent_all_mental_defense/100) * coefficient +
            (c.all_speed + powerManager.speed) * coefficient +
            (c.all_critical_damage + powerManager.critical_damage) * coefficient +
            (c.all_critical_rate + powerManager.critical_rate) * coefficient +
            (c.all_armor_penetration + powerManager.armor_penetration) * coefficient +
            (c.all_avoid + powerManager.avoid) * coefficient +
            (c.all_absorbs_damage + powerManager.absorbs_damage) * coefficient +
            (c.all_regenerate_vitality + powerManager.regenerate_vitality) * coefficient +
            (c.all_accuracy + powerManager.accuracy) * coefficient +
            (c.all_mana + powerManager.mana) * coefficient);
    }
    public double GetFinalCardCaptainsPower(CardCaptains c)
    {
        PowerManager powerManager = new PowerManager();
        powerManager = powerManager.GetUserStats();
        return power = Math.Floor( (c.all_health + health + c.health * powerManager.percent_all_health/100) * coefficient +
            (c.all_physical_attack + powerManager.physical_attack + c.physical_attack * powerManager.percent_all_physical_attack/100) * coefficient +
            (c.all_physical_defense + powerManager.physical_defense + c.physical_defense * powerManager.percent_all_physical_defense/100) * coefficient +
            (c.all_magical_attack + powerManager.magical_attack + c.magical_attack * powerManager.percent_all_magical_attack/100) * coefficient +
            (c.all_magical_defense + powerManager.magical_defense + c.magical_defense * powerManager.percent_all_magical_defense/100) * coefficient +
            (c.all_chemical_attack + powerManager.chemical_attack + c.chemical_attack * powerManager.percent_all_chemical_attack/100) * coefficient +
            (c.all_chemical_defense + powerManager.chemical_defense + c.chemical_defense * powerManager.percent_all_chemical_defense/100) * coefficient +
            (c.all_atomic_attack + powerManager.atomic_attack + c.atomic_attack * powerManager.percent_all_atomic_attack/100) * coefficient +
            (c.all_atomic_defense + powerManager.atomic_defense + c.atomic_defense * powerManager.percent_all_atomic_defense/100) * coefficient +
            (c.all_mental_attack + powerManager.mental_attack + c.mental_attack * powerManager.percent_all_mental_attack/100) * coefficient +
            (c.all_mental_defense + powerManager.mental_defense + c.mental_defense * powerManager.percent_all_mental_defense/100) * coefficient +
            (c.all_speed + powerManager.speed) * coefficient +
            (c.all_critical_damage + powerManager.critical_damage) * coefficient +
            (c.all_critical_rate + powerManager.critical_rate) * coefficient +
            (c.all_armor_penetration + powerManager.armor_penetration) * coefficient +
            (c.all_avoid + powerManager.avoid) * coefficient +
            (c.all_absorbs_damage + powerManager.absorbs_damage) * coefficient +
            (c.all_regenerate_vitality + powerManager.regenerate_vitality) * coefficient +
            (c.all_accuracy + powerManager.accuracy) * coefficient +
            (c.all_mana + powerManager.mana) * coefficient);
    }
    public double GetFinalCardColonelsPower(CardColonels c)
    {
        PowerManager powerManager = new PowerManager();
        powerManager = powerManager.GetUserStats();
        return power = Math.Floor( (c.all_health + health + c.health * powerManager.percent_all_health/100) * coefficient +
            (c.all_physical_attack + powerManager.physical_attack + c.physical_attack * powerManager.percent_all_physical_attack/100) * coefficient +
            (c.all_physical_defense + powerManager.physical_defense + c.physical_defense * powerManager.percent_all_physical_defense/100) * coefficient +
            (c.all_magical_attack + powerManager.magical_attack + c.magical_attack * powerManager.percent_all_magical_attack/100) * coefficient +
            (c.all_magical_defense + powerManager.magical_defense + c.magical_defense * powerManager.percent_all_magical_defense/100) * coefficient +
            (c.all_chemical_attack + powerManager.chemical_attack + c.chemical_attack * powerManager.percent_all_chemical_attack/100) * coefficient +
            (c.all_chemical_defense + powerManager.chemical_defense + c.chemical_defense * powerManager.percent_all_chemical_defense/100) * coefficient +
            (c.all_atomic_attack + powerManager.atomic_attack + c.atomic_attack * powerManager.percent_all_atomic_attack/100) * coefficient +
            (c.all_atomic_defense + powerManager.atomic_defense + c.atomic_defense * powerManager.percent_all_atomic_defense/100) * coefficient +
            (c.all_mental_attack + powerManager.mental_attack + c.mental_attack * powerManager.percent_all_mental_attack/100) * coefficient +
            (c.all_mental_defense + powerManager.mental_defense + c.mental_defense * powerManager.percent_all_mental_defense/100) * coefficient +
            (c.all_speed + powerManager.speed) * coefficient +
            (c.all_critical_damage + powerManager.critical_damage) * coefficient +
            (c.all_critical_rate + powerManager.critical_rate) * coefficient +
            (c.all_armor_penetration + powerManager.armor_penetration) * coefficient +
            (c.all_avoid + powerManager.avoid) * coefficient +
            (c.all_absorbs_damage + powerManager.absorbs_damage) * coefficient +
            (c.all_regenerate_vitality + powerManager.regenerate_vitality) * coefficient +
            (c.all_accuracy + powerManager.accuracy) * coefficient +
            (c.all_mana + powerManager.mana) * coefficient);
    }
    public double GetFinalCardGeneralsPower(CardGenerals c)
    {
        PowerManager powerManager = new PowerManager();
        powerManager = powerManager.GetUserStats();
        return power = Math.Floor( (c.all_health + health + c.health * powerManager.percent_all_health/100) * coefficient +
            (c.all_physical_attack + powerManager.physical_attack + c.physical_attack * powerManager.percent_all_physical_attack/100) * coefficient +
            (c.all_physical_defense + powerManager.physical_defense + c.physical_defense * powerManager.percent_all_physical_defense/100) * coefficient +
            (c.all_magical_attack + powerManager.magical_attack + c.magical_attack * powerManager.percent_all_magical_attack/100) * coefficient +
            (c.all_magical_defense + powerManager.magical_defense + c.magical_defense * powerManager.percent_all_magical_defense/100) * coefficient +
            (c.all_chemical_attack + powerManager.chemical_attack + c.chemical_attack * powerManager.percent_all_chemical_attack/100) * coefficient +
            (c.all_chemical_defense + powerManager.chemical_defense + c.chemical_defense * powerManager.percent_all_chemical_defense/100) * coefficient +
            (c.all_atomic_attack + powerManager.atomic_attack + c.atomic_attack * powerManager.percent_all_atomic_attack/100) * coefficient +
            (c.all_atomic_defense + powerManager.atomic_defense + c.atomic_defense * powerManager.percent_all_atomic_defense/100) * coefficient +
            (c.all_mental_attack + powerManager.mental_attack + c.mental_attack * powerManager.percent_all_mental_attack/100) * coefficient +
            (c.all_mental_defense + powerManager.mental_defense + c.mental_defense * powerManager.percent_all_mental_defense/100) * coefficient +
            (c.all_speed + powerManager.speed) * coefficient +
            (c.all_critical_damage + powerManager.critical_damage) * coefficient +
            (c.all_critical_rate + powerManager.critical_rate) * coefficient +
            (c.all_armor_penetration + powerManager.armor_penetration) * coefficient +
            (c.all_avoid + powerManager.avoid) * coefficient +
            (c.all_absorbs_damage + powerManager.absorbs_damage) * coefficient +
            (c.all_regenerate_vitality + powerManager.regenerate_vitality) * coefficient +
            (c.all_accuracy + powerManager.accuracy) * coefficient +
            (c.all_mana + powerManager.mana) * coefficient);
    }
    public double GetFinalCardAdmiralsPower(CardAdmirals c)
    {
        PowerManager powerManager = new PowerManager();
        powerManager = powerManager.GetUserStats();
        return power = Math.Floor( (c.all_health + health + c.health * powerManager.percent_all_health/100) * coefficient +
            (c.all_physical_attack + powerManager.physical_attack + c.physical_attack * powerManager.percent_all_physical_attack/100) * coefficient +
            (c.all_physical_defense + powerManager.physical_defense + c.physical_defense * powerManager.percent_all_physical_defense/100) * coefficient +
            (c.all_magical_attack + powerManager.magical_attack + c.magical_attack * powerManager.percent_all_magical_attack/100) * coefficient +
            (c.all_magical_defense + powerManager.magical_defense + c.magical_defense * powerManager.percent_all_magical_defense/100) * coefficient +
            (c.all_chemical_attack + powerManager.chemical_attack + c.chemical_attack * powerManager.percent_all_chemical_attack/100) * coefficient +
            (c.all_chemical_defense + powerManager.chemical_defense + c.chemical_defense * powerManager.percent_all_chemical_defense/100) * coefficient +
            (c.all_atomic_attack + powerManager.atomic_attack + c.atomic_attack * powerManager.percent_all_atomic_attack/100) * coefficient +
            (c.all_atomic_defense + powerManager.atomic_defense + c.atomic_defense * powerManager.percent_all_atomic_defense/100) * coefficient +
            (c.all_mental_attack + powerManager.mental_attack + c.mental_attack * powerManager.percent_all_mental_attack/100) * coefficient +
            (c.all_mental_defense + powerManager.mental_defense + c.mental_defense * powerManager.percent_all_mental_defense/100) * coefficient +
            (c.all_speed + powerManager.speed) * coefficient +
            (c.all_critical_damage + powerManager.critical_damage) * coefficient +
            (c.all_critical_rate + powerManager.critical_rate) * coefficient +
            (c.all_armor_penetration + powerManager.armor_penetration) * coefficient +
            (c.all_avoid + powerManager.avoid) * coefficient +
            (c.all_absorbs_damage + powerManager.absorbs_damage) * coefficient +
            (c.all_regenerate_vitality + powerManager.regenerate_vitality) * coefficient +
            (c.all_accuracy + powerManager.accuracy) * coefficient +
            (c.all_mana + powerManager.mana) * coefficient);
    }
    public double GetFinalCardMonstersPower(CardMonsters c)
    {
        PowerManager powerManager = new PowerManager();
        powerManager = powerManager.GetUserStats();
        return power = Math.Floor( (c.all_health + health + c.health * powerManager.percent_all_health/100) * coefficient +
            (c.all_physical_attack + powerManager.physical_attack + c.physical_attack * powerManager.percent_all_physical_attack/100) * coefficient +
            (c.all_physical_defense + powerManager.physical_defense + c.physical_defense * powerManager.percent_all_physical_defense/100) * coefficient +
            (c.all_magical_attack + powerManager.magical_attack + c.magical_attack * powerManager.percent_all_magical_attack/100) * coefficient +
            (c.all_magical_defense + powerManager.magical_defense + c.magical_defense * powerManager.percent_all_magical_defense/100) * coefficient +
            (c.all_chemical_attack + powerManager.chemical_attack + c.chemical_attack * powerManager.percent_all_chemical_attack/100) * coefficient +
            (c.all_chemical_defense + powerManager.chemical_defense + c.chemical_defense * powerManager.percent_all_chemical_defense/100) * coefficient +
            (c.all_atomic_attack + powerManager.atomic_attack + c.atomic_attack * powerManager.percent_all_atomic_attack/100) * coefficient +
            (c.all_atomic_defense + powerManager.atomic_defense + c.atomic_defense * powerManager.percent_all_atomic_defense/100) * coefficient +
            (c.all_mental_attack + powerManager.mental_attack + c.mental_attack * powerManager.percent_all_mental_attack/100) * coefficient +
            (c.all_mental_defense + powerManager.mental_defense + c.mental_defense * powerManager.percent_all_mental_defense/100) * coefficient +
            (c.all_speed + powerManager.speed) * coefficient +
            (c.all_critical_damage + powerManager.critical_damage) * coefficient +
            (c.all_critical_rate + powerManager.critical_rate) * coefficient +
            (c.all_armor_penetration + powerManager.armor_penetration) * coefficient +
            (c.all_avoid + powerManager.avoid) * coefficient +
            (c.all_absorbs_damage + powerManager.absorbs_damage) * coefficient +
            (c.all_regenerate_vitality + powerManager.regenerate_vitality) * coefficient +
            (c.all_accuracy + powerManager.accuracy) * coefficient +
            (c.all_mana + powerManager.mana) * coefficient);
    }
    public double GetFinalCardMilitaryPower(CardMilitary c)
    {
        PowerManager powerManager = new PowerManager();
        powerManager = powerManager.GetUserStats();
        return power = Math.Floor( (c.all_health + health + c.health * powerManager.percent_all_health/100) * coefficient +
            (c.all_physical_attack + powerManager.physical_attack + c.physical_attack * powerManager.percent_all_physical_attack/100) * coefficient +
            (c.all_physical_defense + powerManager.physical_defense + c.physical_defense * powerManager.percent_all_physical_defense/100) * coefficient +
            (c.all_magical_attack + powerManager.magical_attack + c.magical_attack * powerManager.percent_all_magical_attack/100) * coefficient +
            (c.all_magical_defense + powerManager.magical_defense + c.magical_defense * powerManager.percent_all_magical_defense/100) * coefficient +
            (c.all_chemical_attack + powerManager.chemical_attack + c.chemical_attack * powerManager.percent_all_chemical_attack/100) * coefficient +
            (c.all_chemical_defense + powerManager.chemical_defense + c.chemical_defense * powerManager.percent_all_chemical_defense/100) * coefficient +
            (c.all_atomic_attack + powerManager.atomic_attack + c.atomic_attack * powerManager.percent_all_atomic_attack/100) * coefficient +
            (c.all_atomic_defense + powerManager.atomic_defense + c.atomic_defense * powerManager.percent_all_atomic_defense/100) * coefficient +
            (c.all_mental_attack + powerManager.mental_attack + c.mental_attack * powerManager.percent_all_mental_attack/100) * coefficient +
            (c.all_mental_defense + powerManager.mental_defense + c.mental_defense * powerManager.percent_all_mental_defense/100) * coefficient +
            (c.all_speed + powerManager.speed) * coefficient +
            (c.all_critical_damage + powerManager.critical_damage) * coefficient +
            (c.all_critical_rate + powerManager.critical_rate) * coefficient +
            (c.all_armor_penetration + powerManager.armor_penetration) * coefficient +
            (c.all_avoid + powerManager.avoid) * coefficient +
            (c.all_absorbs_damage + powerManager.absorbs_damage) * coefficient +
            (c.all_regenerate_vitality + powerManager.regenerate_vitality) * coefficient +
            (c.all_accuracy + powerManager.accuracy) * coefficient +
            (c.all_mana + powerManager.mana) * coefficient);
    }
    public double GetFinalBooksPower(Books c)
    {
        PowerManager powerManager = new PowerManager();
        powerManager = powerManager.GetUserStats();
        return power = Math.Floor( (c.all_health + health + c.health * powerManager.percent_all_health/100) * coefficient +
            (c.all_physical_attack + powerManager.physical_attack + c.physical_attack * powerManager.percent_all_physical_attack/100) * coefficient +
            (c.all_physical_defense + powerManager.physical_defense + c.physical_defense * powerManager.percent_all_physical_defense/100) * coefficient +
            (c.all_magical_attack + powerManager.magical_attack + c.magical_attack * powerManager.percent_all_magical_attack/100) * coefficient +
            (c.all_magical_defense + powerManager.magical_defense + c.magical_defense * powerManager.percent_all_magical_defense/100) * coefficient +
            (c.all_chemical_attack + powerManager.chemical_attack + c.chemical_attack * powerManager.percent_all_chemical_attack/100) * coefficient +
            (c.all_chemical_defense + powerManager.chemical_defense + c.chemical_defense * powerManager.percent_all_chemical_defense/100) * coefficient +
            (c.all_atomic_attack + powerManager.atomic_attack + c.atomic_attack * powerManager.percent_all_atomic_attack/100) * coefficient +
            (c.all_atomic_defense + powerManager.atomic_defense + c.atomic_defense * powerManager.percent_all_atomic_defense/100) * coefficient +
            (c.all_mental_attack + powerManager.mental_attack + c.mental_attack * powerManager.percent_all_mental_attack/100) * coefficient +
            (c.all_mental_defense + powerManager.mental_defense + c.mental_defense * powerManager.percent_all_mental_defense/100) * coefficient +
            (c.all_speed + powerManager.speed) * coefficient +
            (c.all_critical_damage + powerManager.critical_damage) * coefficient +
            (c.all_critical_rate + powerManager.critical_rate) * coefficient +
            (c.all_armor_penetration + powerManager.armor_penetration) * coefficient +
            (c.all_avoid + powerManager.avoid) * coefficient +
            (c.all_absorbs_damage + powerManager.absorbs_damage) * coefficient +
            (c.all_regenerate_vitality + powerManager.regenerate_vitality) * coefficient +
            (c.all_accuracy + powerManager.accuracy) * coefficient +
            (c.all_mana + powerManager.mana) * coefficient);
    }
    public double GetFinalPetsPower(Pets c)
    {
        PowerManager powerManager = new PowerManager();
        powerManager = powerManager.GetUserStats();
        return power = Math.Floor( (c.all_health + health + c.health * powerManager.percent_all_health/100) * coefficient +
            (c.all_physical_attack + powerManager.physical_attack + c.physical_attack * powerManager.percent_all_physical_attack/100) * coefficient +
            (c.all_physical_defense + powerManager.physical_defense + c.physical_defense * powerManager.percent_all_physical_defense/100) * coefficient +
            (c.all_magical_attack + powerManager.magical_attack + c.magical_attack * powerManager.percent_all_magical_attack/100) * coefficient +
            (c.all_magical_defense + powerManager.magical_defense + c.magical_defense * powerManager.percent_all_magical_defense/100) * coefficient +
            (c.all_chemical_attack + powerManager.chemical_attack + c.chemical_attack * powerManager.percent_all_chemical_attack/100) * coefficient +
            (c.all_chemical_defense + powerManager.chemical_defense + c.chemical_defense * powerManager.percent_all_chemical_defense/100) * coefficient +
            (c.all_atomic_attack + powerManager.atomic_attack + c.atomic_attack * powerManager.percent_all_atomic_attack/100) * coefficient +
            (c.all_atomic_defense + powerManager.atomic_defense + c.atomic_defense * powerManager.percent_all_atomic_defense/100) * coefficient +
            (c.all_mental_attack + powerManager.mental_attack + c.mental_attack * powerManager.percent_all_mental_attack/100) * coefficient +
            (c.all_mental_defense + powerManager.mental_defense + c.mental_defense * powerManager.percent_all_mental_defense/100) * coefficient +
            (c.all_speed + powerManager.speed) * coefficient +
            (c.all_critical_damage + powerManager.critical_damage) * coefficient +
            (c.all_critical_rate + powerManager.critical_rate) * coefficient +
            (c.all_armor_penetration + powerManager.armor_penetration) * coefficient +
            (c.all_avoid + powerManager.avoid) * coefficient +
            (c.all_absorbs_damage + powerManager.absorbs_damage) * coefficient +
            (c.all_regenerate_vitality + powerManager.regenerate_vitality) * coefficient +
            (c.all_accuracy + powerManager.accuracy) * coefficient +
            (c.all_mana + powerManager.mana) * coefficient);
    }
    public void GetAchievementsPower()
    {
        Achievements achievements = new Achievements();
        //User
        achievements = achievements.SumPowerUserAchievements();
        power = power + achievements.power;
        health = health + achievements.health;
        physical_attack = physical_attack + achievements.physical_attack;
        physical_defense = physical_defense + achievements.physical_defense;
        magical_attack = magical_attack + achievements.magical_attack;
        magical_defense = magical_defense + achievements.magical_defense;
        chemical_attack = chemical_attack + achievements.chemical_attack;
        chemical_defense = chemical_defense + achievements.chemical_defense;
        atomic_attack = atomic_attack + achievements.atomic_attack;
        atomic_defense = atomic_defense + achievements.atomic_defense;
        mental_attack = mental_attack + achievements.mental_attack;
        mental_defense = mental_defense + achievements.mental_defense;
        speed = speed + achievements.speed;
        critical_damage = critical_damage + achievements.critical_damage;
        critical_rate = critical_rate + achievements.critical_rate;
        armor_penetration = armor_penetration + achievements.armor_penetration;
        avoid = avoid + achievements.avoid;
        absorbs_damage = absorbs_damage + achievements.absorbs_damage;
        regenerate_vitality = regenerate_vitality + achievements.regenerate_vitality;
        accuracy = accuracy + achievements.accuracy;
        mana = mana + achievements.mana;

    }
    public void GetBooksPower()
    {
        Books books = new Books();
        //Gallery
        books = books.SumPowerBooksGallery();
        power = power + books.power;
        health = health + books.health;
        physical_attack = physical_attack + books.physical_attack;
        physical_defense = physical_defense + books.physical_defense;
        magical_attack = magical_attack + books.magical_attack;
        magical_defense = magical_defense + books.magical_defense;
        chemical_attack = chemical_attack + books.chemical_attack;
        chemical_defense = chemical_defense + books.chemical_defense;
        atomic_attack = atomic_attack + books.atomic_attack;
        atomic_defense = atomic_defense + books.atomic_defense;
        mental_attack = mental_attack + books.mental_attack;
        mental_defense = mental_defense + books.mental_defense;
        speed = speed + books.speed;
        critical_damage = critical_damage + books.critical_damage;
        critical_rate = critical_rate + books.critical_rate;
        armor_penetration = armor_penetration + books.armor_penetration;
        avoid = avoid + books.avoid;
        absorbs_damage = absorbs_damage + books.absorbs_damage;
        regenerate_vitality = regenerate_vitality + books.regenerate_vitality;
        accuracy = accuracy + books.accuracy;
        mana = mana + books.mana;
        percent_all_health = percent_all_health + books.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + books.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + books.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + books.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + books.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + books.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + books.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + books.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + books.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + books.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + books.percent_all_mental_defense;

    }
    public void GetBordersPower()
    {
        Borders borders = new Borders();
        //Gallery
        borders = borders.SumPowerBordersGallery();
        power = power + borders.power;
        health = health + borders.health;
        physical_attack = physical_attack + borders.physical_attack;
        physical_defense = physical_defense + borders.physical_defense;
        magical_attack = magical_attack + borders.magical_attack;
        magical_defense = magical_defense + borders.magical_defense;
        chemical_attack = chemical_attack + borders.chemical_attack;
        chemical_defense = chemical_defense + borders.chemical_defense;
        atomic_attack = atomic_attack + borders.atomic_attack;
        atomic_defense = atomic_defense + borders.atomic_defense;
        mental_attack = mental_attack + borders.mental_attack;
        mental_defense = mental_defense + borders.mental_defense;
        speed = speed + borders.speed;
        critical_damage = critical_damage + borders.critical_damage;
        critical_rate = critical_rate + borders.critical_rate;
        armor_penetration = armor_penetration + borders.armor_penetration;
        avoid = avoid + borders.avoid;
        absorbs_damage = absorbs_damage + borders.absorbs_damage;
        regenerate_vitality = regenerate_vitality + borders.regenerate_vitality;
        accuracy = accuracy + borders.accuracy;
        mana = mana + borders.mana;
        percent_all_health = percent_all_health + borders.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + borders.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + borders.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + borders.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + borders.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + borders.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + borders.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + borders.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + borders.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + borders.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + borders.percent_all_mental_defense;

        //Gallery
        borders = borders.SumPowerUserBorders();
        power = power + borders.power;
        health = health + borders.health;
        physical_attack = physical_attack + borders.physical_attack;
        physical_defense = physical_defense + borders.physical_defense;
        magical_attack = magical_attack + borders.magical_attack;
        magical_defense = magical_defense + borders.magical_defense;
        chemical_attack = chemical_attack + borders.chemical_attack;
        chemical_defense = chemical_defense + borders.chemical_defense;
        atomic_attack = atomic_attack + borders.atomic_attack;
        atomic_defense = atomic_defense + borders.atomic_defense;
        mental_attack = mental_attack + borders.mental_attack;
        mental_defense = mental_defense + borders.mental_defense;
        speed = speed + borders.speed;
        critical_damage = critical_damage + borders.critical_damage;
        critical_rate = critical_rate + borders.critical_rate;
        armor_penetration = armor_penetration + borders.armor_penetration;
        avoid = avoid + borders.avoid;
        absorbs_damage = absorbs_damage + borders.absorbs_damage;
        regenerate_vitality = regenerate_vitality + borders.regenerate_vitality;
        accuracy = accuracy + borders.accuracy;
        mana = mana + borders.mana;
    }
    public void GetCardHeroesPower()
    {
        CardHeroes cardHeroes = new CardHeroes();
        //Gallery
        cardHeroes = cardHeroes.SumPowerCardHeroesGallery();
        power = power + cardHeroes.power;
        health = health + cardHeroes.health;
        physical_attack = physical_attack + cardHeroes.physical_attack;
        physical_defense = physical_defense + cardHeroes.physical_defense;
        magical_attack = magical_attack + cardHeroes.magical_attack;
        magical_defense = magical_defense + cardHeroes.magical_defense;
        chemical_attack = chemical_attack + cardHeroes.chemical_attack;
        chemical_defense = chemical_defense + cardHeroes.chemical_defense;
        atomic_attack = atomic_attack + cardHeroes.atomic_attack;
        atomic_defense = atomic_defense + cardHeroes.atomic_defense;
        mental_attack = mental_attack + cardHeroes.mental_attack;
        mental_defense = mental_defense + cardHeroes.mental_defense;
        speed = speed + cardHeroes.speed;
        critical_damage = critical_damage + cardHeroes.critical_damage;
        critical_rate = critical_rate + cardHeroes.critical_rate;
        armor_penetration = armor_penetration + cardHeroes.armor_penetration;
        avoid = avoid + cardHeroes.avoid;
        absorbs_damage = absorbs_damage + cardHeroes.absorbs_damage;
        regenerate_vitality = regenerate_vitality + cardHeroes.regenerate_vitality;
        accuracy = accuracy + cardHeroes.accuracy;
        mana = mana + cardHeroes.mana;
        percent_all_health = percent_all_health + cardHeroes.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + cardHeroes.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + cardHeroes.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + cardHeroes.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + cardHeroes.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + cardHeroes.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + cardHeroes.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + cardHeroes.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + cardHeroes.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + cardHeroes.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + cardHeroes.percent_all_mental_defense;
    }
    public void GetCardCaptainsPower()
    {
        CardCaptains cardCaptains = new CardCaptains();
        //Gallery
        cardCaptains = cardCaptains.SumPowerCardCaptainsGallery();
        power = power + cardCaptains.power;
        health = health + cardCaptains.health;
        physical_attack = physical_attack + cardCaptains.physical_attack;
        physical_defense = physical_defense + cardCaptains.physical_defense;
        magical_attack = magical_attack + cardCaptains.magical_attack;
        magical_defense = magical_defense + cardCaptains.magical_defense;
        chemical_attack = chemical_attack + cardCaptains.chemical_attack;
        chemical_defense = chemical_defense + cardCaptains.chemical_defense;
        atomic_attack = atomic_attack + cardCaptains.atomic_attack;
        atomic_defense = atomic_defense + cardCaptains.atomic_defense;
        mental_attack = mental_attack + cardCaptains.mental_attack;
        mental_defense = mental_defense + cardCaptains.mental_defense;
        speed = speed + cardCaptains.speed;
        critical_damage = critical_damage + cardCaptains.critical_damage;
        critical_rate = critical_rate + cardCaptains.critical_rate;
        armor_penetration = armor_penetration + cardCaptains.armor_penetration;
        avoid = avoid + cardCaptains.avoid;
        absorbs_damage = absorbs_damage + cardCaptains.absorbs_damage;
        regenerate_vitality = regenerate_vitality + cardCaptains.regenerate_vitality;
        accuracy = accuracy + cardCaptains.accuracy;
        mana = mana + cardCaptains.mana;
        percent_all_health = percent_all_health + cardCaptains.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + cardCaptains.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + cardCaptains.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + cardCaptains.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + cardCaptains.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + cardCaptains.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + cardCaptains.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + cardCaptains.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + cardCaptains.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + cardCaptains.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + cardCaptains.percent_all_mental_defense;
    }
    public void GetCardColonelsPower()
    {
        CardColonels cardColonels = new CardColonels();
        //Gallery
        cardColonels = cardColonels.SumPowerCardColonelsGallery();
        power = power + cardColonels.power;
        health = health + cardColonels.health;
        physical_attack = physical_attack + cardColonels.physical_attack;
        physical_defense = physical_defense + cardColonels.physical_defense;
        magical_attack = magical_attack + cardColonels.magical_attack;
        magical_defense = magical_defense + cardColonels.magical_defense;
        chemical_attack = chemical_attack + cardColonels.chemical_attack;
        chemical_defense = chemical_defense + cardColonels.chemical_defense;
        atomic_attack = atomic_attack + cardColonels.atomic_attack;
        atomic_defense = atomic_defense + cardColonels.atomic_defense;
        mental_attack = mental_attack + cardColonels.mental_attack;
        mental_defense = mental_defense + cardColonels.mental_defense;
        speed = speed + cardColonels.speed;
        critical_damage = critical_damage + cardColonels.critical_damage;
        critical_rate = critical_rate + cardColonels.critical_rate;
        armor_penetration = armor_penetration + cardColonels.armor_penetration;
        avoid = avoid + cardColonels.avoid;
        absorbs_damage = absorbs_damage + cardColonels.absorbs_damage;
        regenerate_vitality = regenerate_vitality + cardColonels.regenerate_vitality;
        accuracy = accuracy + cardColonels.accuracy;
        mana = mana + cardColonels.mana;
        percent_all_health = percent_all_health + cardColonels.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + cardColonels.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + cardColonels.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + cardColonels.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + cardColonels.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + cardColonels.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + cardColonels.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + cardColonels.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + cardColonels.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + cardColonels.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + cardColonels.percent_all_mental_defense;
    }
    public void GetCardGeneralsPower()
    {
        CardGenerals cardGenerals = new CardGenerals();
        //Gallery
        cardGenerals = cardGenerals.SumPowerCardGeneralsGallery();
        power = power + cardGenerals.power;
        health = health + cardGenerals.health;
        physical_attack = physical_attack + cardGenerals.physical_attack;
        physical_defense = physical_defense + cardGenerals.physical_defense;
        magical_attack = magical_attack + cardGenerals.magical_attack;
        magical_defense = magical_defense + cardGenerals.magical_defense;
        chemical_attack = chemical_attack + cardGenerals.chemical_attack;
        chemical_defense = chemical_defense + cardGenerals.chemical_defense;
        atomic_attack = atomic_attack + cardGenerals.atomic_attack;
        atomic_defense = atomic_defense + cardGenerals.atomic_defense;
        mental_attack = mental_attack + cardGenerals.mental_attack;
        mental_defense = mental_defense + cardGenerals.mental_defense;
        speed = speed + cardGenerals.speed;
        critical_damage = critical_damage + cardGenerals.critical_damage;
        critical_rate = critical_rate + cardGenerals.critical_rate;
        armor_penetration = armor_penetration + cardGenerals.armor_penetration;
        avoid = avoid + cardGenerals.avoid;
        absorbs_damage = absorbs_damage + cardGenerals.absorbs_damage;
        regenerate_vitality = regenerate_vitality + cardGenerals.regenerate_vitality;
        accuracy = accuracy + cardGenerals.accuracy;
        mana = mana + cardGenerals.mana;
        percent_all_health = percent_all_health + cardGenerals.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + cardGenerals.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + cardGenerals.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + cardGenerals.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + cardGenerals.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + cardGenerals.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + cardGenerals.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + cardGenerals.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + cardGenerals.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + cardGenerals.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + cardGenerals.percent_all_mental_defense;
    }
    public void GetCardAdmiralsPower()
    {
        CardAdmirals cardAdmirals = new CardAdmirals();
        //Gallery
        cardAdmirals = cardAdmirals.SumPowerCardCaptainsGallery();
        power = power + cardAdmirals.power;
        health = health + cardAdmirals.health;
        physical_attack = physical_attack + cardAdmirals.physical_attack;
        physical_defense = physical_defense + cardAdmirals.physical_defense;
        magical_attack = magical_attack + cardAdmirals.magical_attack;
        magical_defense = magical_defense + cardAdmirals.magical_defense;
        chemical_attack = chemical_attack + cardAdmirals.chemical_attack;
        chemical_defense = chemical_defense + cardAdmirals.chemical_defense;
        atomic_attack = atomic_attack + cardAdmirals.atomic_attack;
        atomic_defense = atomic_defense + cardAdmirals.atomic_defense;
        mental_attack = mental_attack + cardAdmirals.mental_attack;
        mental_defense = mental_defense + cardAdmirals.mental_defense;
        speed = speed + cardAdmirals.speed;
        critical_damage = critical_damage + cardAdmirals.critical_damage;
        critical_rate = critical_rate + cardAdmirals.critical_rate;
        armor_penetration = armor_penetration + cardAdmirals.armor_penetration;
        avoid = avoid + cardAdmirals.avoid;
        absorbs_damage = absorbs_damage + cardAdmirals.absorbs_damage;
        regenerate_vitality = regenerate_vitality + cardAdmirals.regenerate_vitality;
        accuracy = accuracy + cardAdmirals.accuracy;
        mana = mana + cardAdmirals.mana;
        percent_all_health = percent_all_health + cardAdmirals.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + cardAdmirals.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + cardAdmirals.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + cardAdmirals.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + cardAdmirals.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + cardAdmirals.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + cardAdmirals.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + cardAdmirals.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + cardAdmirals.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + cardAdmirals.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + cardAdmirals.percent_all_mental_defense;
    }
    public void GetCardMonstersPower()
    {
        CardMonsters cardMonsters = new CardMonsters();
        //Gallery
        cardMonsters = cardMonsters.SumPowerCardMonstersGallery();
        power = power + cardMonsters.power;
        health = health + cardMonsters.health;
        physical_attack = physical_attack + cardMonsters.physical_attack;
        physical_defense = physical_defense + cardMonsters.physical_defense;
        magical_attack = magical_attack + cardMonsters.magical_attack;
        magical_defense = magical_defense + cardMonsters.magical_defense;
        chemical_attack = chemical_attack + cardMonsters.chemical_attack;
        chemical_defense = chemical_defense + cardMonsters.chemical_defense;
        atomic_attack = atomic_attack + cardMonsters.atomic_attack;
        atomic_defense = atomic_defense + cardMonsters.atomic_defense;
        mental_attack = mental_attack + cardMonsters.mental_attack;
        mental_defense = mental_defense + cardMonsters.mental_defense;
        speed = speed + cardMonsters.speed;
        critical_damage = critical_damage + cardMonsters.critical_damage;
        critical_rate = critical_rate + cardMonsters.critical_rate;
        armor_penetration = armor_penetration + cardMonsters.armor_penetration;
        avoid = avoid + cardMonsters.avoid;
        absorbs_damage = absorbs_damage + cardMonsters.absorbs_damage;
        regenerate_vitality = regenerate_vitality + cardMonsters.regenerate_vitality;
        accuracy = accuracy + cardMonsters.accuracy;
        mana = mana + cardMonsters.mana;
        percent_all_health = percent_all_health + cardMonsters.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + cardMonsters.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + cardMonsters.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + cardMonsters.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + cardMonsters.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + cardMonsters.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + cardMonsters.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + cardMonsters.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + cardMonsters.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + cardMonsters.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + cardMonsters.percent_all_mental_defense;
    }
    public void GetCardMilitaryPower()
    {
        CardMilitary cardMilitary = new CardMilitary();
        //Gallery
        cardMilitary = cardMilitary.SumPowerCardMilitaryGallery();
        power = power + cardMilitary.power;
        health = health + cardMilitary.health;
        physical_attack = physical_attack + cardMilitary.physical_attack;
        physical_defense = physical_defense + cardMilitary.physical_defense;
        magical_attack = magical_attack + cardMilitary.magical_attack;
        magical_defense = magical_defense + cardMilitary.magical_defense;
        chemical_attack = chemical_attack + cardMilitary.chemical_attack;
        chemical_defense = chemical_defense + cardMilitary.chemical_defense;
        atomic_attack = atomic_attack + cardMilitary.atomic_attack;
        atomic_defense = atomic_defense + cardMilitary.atomic_defense;
        mental_attack = mental_attack + cardMilitary.mental_attack;
        mental_defense = mental_defense + cardMilitary.mental_defense;
        speed = speed + cardMilitary.speed;
        critical_damage = critical_damage + cardMilitary.critical_damage;
        critical_rate = critical_rate + cardMilitary.critical_rate;
        armor_penetration = armor_penetration + cardMilitary.armor_penetration;
        avoid = avoid + cardMilitary.avoid;
        absorbs_damage = absorbs_damage + cardMilitary.absorbs_damage;
        regenerate_vitality = regenerate_vitality + cardMilitary.regenerate_vitality;
        accuracy = accuracy + cardMilitary.accuracy;
        mana = mana + cardMilitary.mana;
        percent_all_health = percent_all_health + cardMilitary.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + cardMilitary.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + cardMilitary.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + cardMilitary.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + cardMilitary.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + cardMilitary.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + cardMilitary.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + cardMilitary.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + cardMilitary.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + cardMilitary.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + cardMilitary.percent_all_mental_defense;
    }
    public void GetCardSpellPower()
    {
        CardSpell cardSpell = new CardSpell();
        //Gallery
        cardSpell = cardSpell.SumPowerCardSpellGallery();
        power = power + cardSpell.power;
        health = health + cardSpell.health;
        physical_attack = physical_attack + cardSpell.physical_attack;
        physical_defense = physical_defense + cardSpell.physical_defense;
        magical_attack = magical_attack + cardSpell.magical_attack;
        magical_defense = magical_defense + cardSpell.magical_defense;
        chemical_attack = chemical_attack + cardSpell.chemical_attack;
        chemical_defense = chemical_defense + cardSpell.chemical_defense;
        atomic_attack = atomic_attack + cardSpell.atomic_attack;
        atomic_defense = atomic_defense + cardSpell.atomic_defense;
        mental_attack = mental_attack + cardSpell.mental_attack;
        mental_defense = mental_defense + cardSpell.mental_defense;
        speed = speed + cardSpell.speed;
        critical_damage = critical_damage + cardSpell.critical_damage;
        critical_rate = critical_rate + cardSpell.critical_rate;
        armor_penetration = armor_penetration + cardSpell.armor_penetration;
        avoid = avoid + cardSpell.avoid;
        absorbs_damage = absorbs_damage + cardSpell.absorbs_damage;
        regenerate_vitality = regenerate_vitality + cardSpell.regenerate_vitality;
        accuracy = accuracy + cardSpell.accuracy;
        mana = mana + cardSpell.mana;
        percent_all_health = percent_all_health + cardSpell.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + cardSpell.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + cardSpell.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + cardSpell.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + cardSpell.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + cardSpell.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + cardSpell.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + cardSpell.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + cardSpell.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + cardSpell.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + cardSpell.percent_all_mental_defense;
    }
    public void GetCollaborationsPower()
    {
        Collaboration collaboration = new Collaboration();
        //Gallery
        collaboration = collaboration.SumPowerCollaborationsGallery();
        power = power + collaboration.power;
        health = health + collaboration.health;
        physical_attack = physical_attack + collaboration.physical_attack;
        physical_defense = physical_defense + collaboration.physical_defense;
        magical_attack = magical_attack + collaboration.magical_attack;
        magical_defense = magical_defense + collaboration.magical_defense;
        chemical_attack = chemical_attack + collaboration.chemical_attack;
        chemical_defense = chemical_defense + collaboration.chemical_defense;
        atomic_attack = atomic_attack + collaboration.atomic_attack;
        atomic_defense = atomic_defense + collaboration.atomic_defense;
        mental_attack = mental_attack + collaboration.mental_attack;
        mental_defense = mental_defense + collaboration.mental_defense;
        speed = speed + collaboration.speed;
        critical_damage = critical_damage + collaboration.critical_damage;
        critical_rate = critical_rate + collaboration.critical_rate;
        armor_penetration = armor_penetration + collaboration.armor_penetration;
        avoid = avoid + collaboration.avoid;
        absorbs_damage = absorbs_damage + collaboration.absorbs_damage;
        regenerate_vitality = regenerate_vitality + collaboration.regenerate_vitality;
        accuracy = accuracy + collaboration.accuracy;
        mana = mana + collaboration.mana;
        percent_all_health = percent_all_health + collaboration.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + collaboration.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + collaboration.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + collaboration.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + collaboration.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + collaboration.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + collaboration.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + collaboration.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + collaboration.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + collaboration.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + collaboration.percent_all_mental_defense;

        //Gallery
        collaboration = collaboration.SumPowerUserCollaborations();
        power = power + collaboration.power;
        health = health + collaboration.health;
        physical_attack = physical_attack + collaboration.physical_attack;
        physical_defense = physical_defense + collaboration.physical_defense;
        magical_attack = magical_attack + collaboration.magical_attack;
        magical_defense = magical_defense + collaboration.magical_defense;
        chemical_attack = chemical_attack + collaboration.chemical_attack;
        chemical_defense = chemical_defense + collaboration.chemical_defense;
        atomic_attack = atomic_attack + collaboration.atomic_attack;
        atomic_defense = atomic_defense + collaboration.atomic_defense;
        mental_attack = mental_attack + collaboration.mental_attack;
        mental_defense = mental_defense + collaboration.mental_defense;
        speed = speed + collaboration.speed;
        critical_damage = critical_damage + collaboration.critical_damage;
        critical_rate = critical_rate + collaboration.critical_rate;
        armor_penetration = armor_penetration + collaboration.armor_penetration;
        avoid = avoid + collaboration.avoid;
        absorbs_damage = absorbs_damage + collaboration.absorbs_damage;
        regenerate_vitality = regenerate_vitality + collaboration.regenerate_vitality;
        accuracy = accuracy + collaboration.accuracy;
        mana = mana + collaboration.mana;
    }
    public void GetCollaborationEquipmentsPower()
    {
        CollaborationEquipment collaborationEquipment = new CollaborationEquipment();
        //Gallery
        collaborationEquipment = collaborationEquipment.SumPowerCollaborationEquipmentsGallery();
        power = power + collaborationEquipment.power;
        health = health + collaborationEquipment.health;
        physical_attack = physical_attack + collaborationEquipment.physical_attack;
        physical_defense = physical_defense + collaborationEquipment.physical_defense;
        magical_attack = magical_attack + collaborationEquipment.magical_attack;
        magical_defense = magical_defense + collaborationEquipment.magical_defense;
        chemical_attack = chemical_attack + collaborationEquipment.chemical_attack;
        chemical_defense = chemical_defense + collaborationEquipment.chemical_defense;
        atomic_attack = atomic_attack + collaborationEquipment.atomic_attack;
        atomic_defense = atomic_defense + collaborationEquipment.atomic_defense;
        mental_attack = mental_attack + collaborationEquipment.mental_attack;
        mental_defense = mental_defense + collaborationEquipment.mental_defense;
        speed = speed + collaborationEquipment.speed;
        critical_damage = critical_damage + collaborationEquipment.critical_damage;
        critical_rate = critical_rate + collaborationEquipment.critical_rate;
        armor_penetration = armor_penetration + collaborationEquipment.armor_penetration;
        avoid = avoid + collaborationEquipment.avoid;
        absorbs_damage = absorbs_damage + collaborationEquipment.absorbs_damage;
        regenerate_vitality = regenerate_vitality + collaborationEquipment.regenerate_vitality;
        accuracy = accuracy + collaborationEquipment.accuracy;
        mana = mana + collaborationEquipment.mana;
        percent_all_health = percent_all_health + collaborationEquipment.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + collaborationEquipment.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + collaborationEquipment.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + collaborationEquipment.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + collaborationEquipment.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + collaborationEquipment.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + collaborationEquipment.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + collaborationEquipment.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + collaborationEquipment.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + collaborationEquipment.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + collaborationEquipment.percent_all_mental_defense;

        //Gallery
        collaborationEquipment = collaborationEquipment.SumPowerUserCollaborationEquipments();
        power = power + collaborationEquipment.power;
        health = health + collaborationEquipment.health;
        physical_attack = physical_attack + collaborationEquipment.physical_attack;
        physical_defense = physical_defense + collaborationEquipment.physical_defense;
        magical_attack = magical_attack + collaborationEquipment.magical_attack;
        magical_defense = magical_defense + collaborationEquipment.magical_defense;
        chemical_attack = chemical_attack + collaborationEquipment.chemical_attack;
        chemical_defense = chemical_defense + collaborationEquipment.chemical_defense;
        atomic_attack = atomic_attack + collaborationEquipment.atomic_attack;
        atomic_defense = atomic_defense + collaborationEquipment.atomic_defense;
        mental_attack = mental_attack + collaborationEquipment.mental_attack;
        mental_defense = mental_defense + collaborationEquipment.mental_defense;
        speed = speed + collaborationEquipment.speed;
        critical_damage = critical_damage + collaborationEquipment.critical_damage;
        critical_rate = critical_rate + collaborationEquipment.critical_rate;
        armor_penetration = armor_penetration + collaborationEquipment.armor_penetration;
        avoid = avoid + collaborationEquipment.avoid;
        absorbs_damage = absorbs_damage + collaborationEquipment.absorbs_damage;
        regenerate_vitality = regenerate_vitality + collaborationEquipment.regenerate_vitality;
        accuracy = accuracy + collaborationEquipment.accuracy;
        mana = mana + collaborationEquipment.mana;
    }
    public void GetEquipmentsPower()
    {
        Equipments equipments = new Equipments();
        //Gallery
        equipments = equipments.SumPowerEquipmentsGallery();
        power = power + equipments.power;
        health = health + equipments.health;
        physical_attack = physical_attack + equipments.physical_attack;
        physical_defense = physical_defense + equipments.physical_defense;
        magical_attack = magical_attack + equipments.magical_attack;
        magical_defense = magical_defense + equipments.magical_defense;
        chemical_attack = chemical_attack + equipments.chemical_attack;
        chemical_defense = chemical_defense + equipments.chemical_defense;
        atomic_attack = atomic_attack + equipments.atomic_attack;
        atomic_defense = atomic_defense + equipments.atomic_defense;
        mental_attack = mental_attack + equipments.mental_attack;
        mental_defense = mental_defense + equipments.mental_defense;
        speed = speed + equipments.speed;
        critical_damage = critical_damage + equipments.critical_damage;
        critical_rate = critical_rate + equipments.critical_rate;
        armor_penetration = armor_penetration + equipments.armor_penetration;
        avoid = avoid + equipments.avoid;
        absorbs_damage = absorbs_damage + equipments.absorbs_damage;
        regenerate_vitality = regenerate_vitality + equipments.regenerate_vitality;
        accuracy = accuracy + equipments.accuracy;
        mana = mana + equipments.mana;
        percent_all_health = percent_all_health + equipments.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + equipments.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + equipments.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + equipments.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + equipments.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + equipments.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + equipments.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + equipments.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + equipments.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + equipments.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + equipments.percent_all_mental_defense;
    }
    public void GetMagicFormationCirlcePower()
    {
        MagicFormationCircle magicFormationCircle = new MagicFormationCircle();
        //Gallery
        magicFormationCircle = magicFormationCircle.SumPowerMagicFormationCircleGallery();
        power = power + magicFormationCircle.power;
        health = health + magicFormationCircle.health;
        physical_attack = physical_attack + magicFormationCircle.physical_attack;
        physical_defense = physical_defense + magicFormationCircle.physical_defense;
        magical_attack = magical_attack + magicFormationCircle.magical_attack;
        magical_defense = magical_defense + magicFormationCircle.magical_defense;
        chemical_attack = chemical_attack + magicFormationCircle.chemical_attack;
        chemical_defense = chemical_defense + magicFormationCircle.chemical_defense;
        atomic_attack = atomic_attack + magicFormationCircle.atomic_attack;
        atomic_defense = atomic_defense + magicFormationCircle.atomic_defense;
        mental_attack = mental_attack + magicFormationCircle.mental_attack;
        mental_defense = mental_defense + magicFormationCircle.mental_defense;
        speed = speed + magicFormationCircle.speed;
        critical_damage = critical_damage + magicFormationCircle.critical_damage;
        critical_rate = critical_rate + magicFormationCircle.critical_rate;
        armor_penetration = armor_penetration + magicFormationCircle.armor_penetration;
        avoid = avoid + magicFormationCircle.avoid;
        absorbs_damage = absorbs_damage + magicFormationCircle.absorbs_damage;
        regenerate_vitality = regenerate_vitality + magicFormationCircle.regenerate_vitality;
        accuracy = accuracy + magicFormationCircle.accuracy;
        mana = mana + magicFormationCircle.mana;
        percent_all_health = percent_all_health + magicFormationCircle.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + magicFormationCircle.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + magicFormationCircle.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + magicFormationCircle.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + magicFormationCircle.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + magicFormationCircle.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + magicFormationCircle.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + magicFormationCircle.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + magicFormationCircle.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + magicFormationCircle.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + magicFormationCircle.percent_all_mental_defense;
    }
    public void GetRelicsPower()
    {
        Relics relics = new Relics();
        //Gallery
        relics = relics.SumPowerRelicsGallery();
        power = power + relics.power;
        health = health + relics.health;
        physical_attack = physical_attack + relics.physical_attack;
        physical_defense = physical_defense + relics.physical_defense;
        magical_attack = magical_attack + relics.magical_attack;
        magical_defense = magical_defense + relics.magical_defense;
        chemical_attack = chemical_attack + relics.chemical_attack;
        chemical_defense = chemical_defense + relics.chemical_defense;
        atomic_attack = atomic_attack + relics.atomic_attack;
        atomic_defense = atomic_defense + relics.atomic_defense;
        mental_attack = mental_attack + relics.mental_attack;
        mental_defense = mental_defense + relics.mental_defense;
        speed = speed + relics.speed;
        critical_damage = critical_damage + relics.critical_damage;
        critical_rate = critical_rate + relics.critical_rate;
        armor_penetration = armor_penetration + relics.armor_penetration;
        avoid = avoid + relics.avoid;
        absorbs_damage = absorbs_damage + relics.absorbs_damage;
        regenerate_vitality = regenerate_vitality + relics.regenerate_vitality;
        accuracy = accuracy + relics.accuracy;
        mana = mana + relics.mana;
        percent_all_health = percent_all_health + relics.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + relics.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + relics.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + relics.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + relics.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + relics.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + relics.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + relics.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + relics.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + relics.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + relics.percent_all_mental_defense;
    }
    public void GetMedalsPower()
    {
        Medals medals = new Medals();
        //Gallery
        medals = medals.SumPowerMedalsGallery();
        power = power + medals.power;
        health = health + medals.health;
        physical_attack = physical_attack + medals.physical_attack;
        physical_defense = physical_defense + medals.physical_defense;
        magical_attack = magical_attack + medals.magical_attack;
        magical_defense = magical_defense + medals.magical_defense;
        chemical_attack = chemical_attack + medals.chemical_attack;
        chemical_defense = chemical_defense + medals.chemical_defense;
        atomic_attack = atomic_attack + medals.atomic_attack;
        atomic_defense = atomic_defense + medals.atomic_defense;
        mental_attack = mental_attack + medals.mental_attack;
        mental_defense = mental_defense + medals.mental_defense;
        speed = speed + medals.speed;
        critical_damage = critical_damage + medals.critical_damage;
        critical_rate = critical_rate + medals.critical_rate;
        armor_penetration = armor_penetration + medals.armor_penetration;
        avoid = avoid + medals.avoid;
        absorbs_damage = absorbs_damage + medals.absorbs_damage;
        regenerate_vitality = regenerate_vitality + medals.regenerate_vitality;
        accuracy = accuracy + medals.accuracy;
        mana = mana + medals.mana;
        percent_all_health = percent_all_health + medals.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + medals.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + medals.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + medals.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + medals.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + medals.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + medals.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + medals.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + medals.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + medals.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + medals.percent_all_mental_defense;

        //Gallery
        medals = medals.SumPowerUserMedals();
        power = power + medals.power;
        health = health + medals.health;
        physical_attack = physical_attack + medals.physical_attack;
        physical_defense = physical_defense + medals.physical_defense;
        magical_attack = magical_attack + medals.magical_attack;
        magical_defense = magical_defense + medals.magical_defense;
        chemical_attack = chemical_attack + medals.chemical_attack;
        chemical_defense = chemical_defense + medals.chemical_defense;
        atomic_attack = atomic_attack + medals.atomic_attack;
        atomic_defense = atomic_defense + medals.atomic_defense;
        mental_attack = mental_attack + medals.mental_attack;
        mental_defense = mental_defense + medals.mental_defense;
        speed = speed + medals.speed;
        critical_damage = critical_damage + medals.critical_damage;
        critical_rate = critical_rate + medals.critical_rate;
        armor_penetration = armor_penetration + medals.armor_penetration;
        avoid = avoid + medals.avoid;
        absorbs_damage = absorbs_damage + medals.absorbs_damage;
        regenerate_vitality = regenerate_vitality + medals.regenerate_vitality;
        accuracy = accuracy + medals.accuracy;
        mana = mana + medals.mana;
    }
    public void GetPetsPower()
    {
        Pets pets = new Pets();
        //Gallery
        pets = pets.SumPowerPetsGallery();
        power = power + pets.power;
        health = health + pets.health;
        physical_attack = physical_attack + pets.physical_attack;
        physical_defense = physical_defense + pets.physical_defense;
        magical_attack = magical_attack + pets.magical_attack;
        magical_defense = magical_defense + pets.magical_defense;
        chemical_attack = chemical_attack + pets.chemical_attack;
        chemical_defense = chemical_defense + pets.chemical_defense;
        atomic_attack = atomic_attack + pets.atomic_attack;
        atomic_defense = atomic_defense + pets.atomic_defense;
        mental_attack = mental_attack + pets.mental_attack;
        mental_defense = mental_defense + pets.mental_defense;
        speed = speed + pets.speed;
        critical_damage = critical_damage + pets.critical_damage;
        critical_rate = critical_rate + pets.critical_rate;
        armor_penetration = armor_penetration + pets.armor_penetration;
        avoid = avoid + pets.avoid;
        absorbs_damage = absorbs_damage + pets.absorbs_damage;
        regenerate_vitality = regenerate_vitality + pets.regenerate_vitality;
        accuracy = accuracy + pets.accuracy;
        mana = mana + pets.mana;
        percent_all_health = percent_all_health + pets.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + pets.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + pets.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + pets.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + pets.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + pets.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + pets.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + pets.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + pets.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + pets.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + pets.percent_all_mental_defense;
    }
    public void GetSymbolsPower()
    {
        Symbols symbols = new Symbols();
        //Gallery
        symbols = symbols.SumPowerSymbolsGallery();
        power = power + symbols.power;
        health = health + symbols.health;
        physical_attack = physical_attack + symbols.physical_attack;
        physical_defense = physical_defense + symbols.physical_defense;
        magical_attack = magical_attack + symbols.magical_attack;
        magical_defense = magical_defense + symbols.magical_defense;
        chemical_attack = chemical_attack + symbols.chemical_attack;
        chemical_defense = chemical_defense + symbols.chemical_defense;
        atomic_attack = atomic_attack + symbols.atomic_attack;
        atomic_defense = atomic_defense + symbols.atomic_defense;
        mental_attack = mental_attack + symbols.mental_attack;
        mental_defense = mental_defense + symbols.mental_defense;
        speed = speed + symbols.speed;
        critical_damage = critical_damage + symbols.critical_damage;
        critical_rate = critical_rate + symbols.critical_rate;
        armor_penetration = armor_penetration + symbols.armor_penetration;
        avoid = avoid + symbols.avoid;
        absorbs_damage = absorbs_damage + symbols.absorbs_damage;
        regenerate_vitality = regenerate_vitality + symbols.regenerate_vitality;
        accuracy = accuracy + symbols.accuracy;
        mana = mana + symbols.mana;
        percent_all_health = percent_all_health + symbols.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + symbols.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + symbols.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + symbols.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + symbols.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + symbols.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + symbols.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + symbols.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + symbols.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + symbols.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + symbols.percent_all_mental_defense;

        //Gallery
        symbols = symbols.SumPowerUserSymbols();
        power = power + symbols.power;
        health = health + symbols.health;
        physical_attack = physical_attack + symbols.physical_attack;
        physical_defense = physical_defense + symbols.physical_defense;
        magical_attack = magical_attack + symbols.magical_attack;
        magical_defense = magical_defense + symbols.magical_defense;
        chemical_attack = chemical_attack + symbols.chemical_attack;
        chemical_defense = chemical_defense + symbols.chemical_defense;
        atomic_attack = atomic_attack + symbols.atomic_attack;
        atomic_defense = atomic_defense + symbols.atomic_defense;
        mental_attack = mental_attack + symbols.mental_attack;
        mental_defense = mental_defense + symbols.mental_defense;
        speed = speed + symbols.speed;
        critical_damage = critical_damage + symbols.critical_damage;
        critical_rate = critical_rate + symbols.critical_rate;
        armor_penetration = armor_penetration + symbols.armor_penetration;
        avoid = avoid + symbols.avoid;
        absorbs_damage = absorbs_damage + symbols.absorbs_damage;
        regenerate_vitality = regenerate_vitality + symbols.regenerate_vitality;
        accuracy = accuracy + symbols.accuracy;
        mana = mana + symbols.mana;
    }
    public void GetSkillsPower()
    {
        Skills skills = new Skills();
        //Gallery
        skills = skills.SumPowerSkillsGallery();
        power = power + skills.power;
        health = health + skills.health;
        physical_attack = physical_attack + skills.physical_attack;
        physical_defense = physical_defense + skills.physical_defense;
        magical_attack = magical_attack + skills.magical_attack;
        magical_defense = magical_defense + skills.magical_defense;
        chemical_attack = chemical_attack + skills.chemical_attack;
        chemical_defense = chemical_defense + skills.chemical_defense;
        atomic_attack = atomic_attack + skills.atomic_attack;
        atomic_defense = atomic_defense + skills.atomic_defense;
        mental_attack = mental_attack + skills.mental_attack;
        mental_defense = mental_defense + skills.mental_defense;
        speed = speed + skills.speed;
        critical_damage = critical_damage + skills.critical_damage;
        critical_rate = critical_rate + skills.critical_rate;
        armor_penetration = armor_penetration + skills.armor_penetration;
        avoid = avoid + skills.avoid;
        absorbs_damage = absorbs_damage + skills.absorbs_damage;
        regenerate_vitality = regenerate_vitality + skills.regenerate_vitality;
        accuracy = accuracy + skills.accuracy;
        mana = mana + skills.mana;
        percent_all_health = percent_all_health + skills.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + skills.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + skills.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + skills.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + skills.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + skills.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + skills.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + skills.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + skills.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + skills.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + skills.percent_all_mental_defense;
    }
    public void GetTitlesPower()
    {
        Titles titles = new Titles();
        //Gallery
        titles = titles.SumPowerTitlesGallery();
        power = power + titles.power;
        health = health + titles.health;
        physical_attack = physical_attack + titles.physical_attack;
        physical_defense = physical_defense + titles.physical_defense;
        magical_attack = magical_attack + titles.magical_attack;
        magical_defense = magical_defense + titles.magical_defense;
        chemical_attack = chemical_attack + titles.chemical_attack;
        chemical_defense = chemical_defense + titles.chemical_defense;
        atomic_attack = atomic_attack + titles.atomic_attack;
        atomic_defense = atomic_defense + titles.atomic_defense;
        mental_attack = mental_attack + titles.mental_attack;
        mental_defense = mental_defense + titles.mental_defense;
        speed = speed + titles.speed;
        critical_damage = critical_damage + titles.critical_damage;
        critical_rate = critical_rate + titles.critical_rate;
        armor_penetration = armor_penetration + titles.armor_penetration;
        avoid = avoid + titles.avoid;
        absorbs_damage = absorbs_damage + titles.absorbs_damage;
        regenerate_vitality = regenerate_vitality + titles.regenerate_vitality;
        accuracy = accuracy + titles.accuracy;
        mana = mana + titles.mana;
        percent_all_health = percent_all_health + titles.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + titles.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + titles.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + titles.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + titles.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + titles.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + titles.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + titles.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + titles.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + titles.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + titles.percent_all_mental_defense;

        //Gallery
        titles = titles.SumPowerUserTitles();
        power = power + titles.power;
        health = health + titles.health;
        physical_attack = physical_attack + titles.physical_attack;
        physical_defense = physical_defense + titles.physical_defense;
        magical_attack = magical_attack + titles.magical_attack;
        magical_defense = magical_defense + titles.magical_defense;
        chemical_attack = chemical_attack + titles.chemical_attack;
        chemical_defense = chemical_defense + titles.chemical_defense;
        atomic_attack = atomic_attack + titles.atomic_attack;
        atomic_defense = atomic_defense + titles.atomic_defense;
        mental_attack = mental_attack + titles.mental_attack;
        mental_defense = mental_defense + titles.mental_defense;
        speed = speed + titles.speed;
        critical_damage = critical_damage + titles.critical_damage;
        critical_rate = critical_rate + titles.critical_rate;
        armor_penetration = armor_penetration + titles.armor_penetration;
        avoid = avoid + titles.avoid;
        absorbs_damage = absorbs_damage + titles.absorbs_damage;
        regenerate_vitality = regenerate_vitality + titles.regenerate_vitality;
        accuracy = accuracy + titles.accuracy;
        mana = mana + titles.mana;
    }
}
