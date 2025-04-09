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
        GetAchievementsPower();
        GetBooksPower();
        GetBordersPower();
        GetAvatarsPower();
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
        GetTalismanPower();
        GetPuppetPower();
        GetAlchemyPower();
        GetForgePower();
        GetCardLifePower();
    }
    public static double CalculatePower(
    double health, double physicalAttack, double physicalDefense, double magicalAttack, double magicalDefense,
    double chemicalAttack, double chemicalDefense, double atomicAttack, double atomicDefense, double mentalAttack, double mentalDefense,
    double speed, double criticalDamageRate, double criticalRate, double penetrationRate, double evasionRate,
    double damageAbsorptionRate, double vitalityRegenerationRate, double accuracyRate, double lifestealRate,
    double shieldStrength, double tenacity, double resistanceRate, double comboRate, double reflectionRate,
    double mana, double manaRegenerationRate,
    double damageToDifferentFactionRate, double resistanceToDifferentFactionRate,
    double damageToSameFactionRate, double resistanceToSameFactionRate
)
    {
        double weight = 0.5;

        double totalAttack = (physicalAttack + magicalAttack + chemicalAttack + atomicAttack + mentalAttack) * weight;
        double totalDefense = (physicalDefense + magicalDefense + chemicalDefense + atomicDefense + mentalDefense) * weight + shieldStrength * weight;

        // Điều chỉnh các chỉ số tỷ lệ
        double adjustedCriticalRate = (criticalRate / 100) * totalAttack;
        double adjustedCriticalDamage = (criticalDamageRate / 100) * totalAttack;
        double adjustedPenetration = (penetrationRate / 100) * totalAttack;
        double adjustedEvasion = (evasionRate / 100) * totalDefense;
        double adjustedAbsorption = (damageAbsorptionRate / 100) * (totalDefense + health * 0.5);
        double adjustedRegeneration = (vitalityRegenerationRate / 100) * health;
        double adjustedAccuracy = (accuracyRate / 100) * totalAttack;
        double adjustedLifesteal = (lifestealRate / 100) * totalAttack;
        double adjustedTenacity = (tenacity / 100) * totalDefense;
        double adjustedResistance = (resistanceRate / 100) * (totalDefense + health * 0.5);
        double adjustedCombo = (comboRate / 100) * totalAttack;
        double adjustedReflection = (reflectionRate / 100) * (totalDefense + health * 0.5);

        // Điều chỉnh thuộc tính faction
        double adjustedDamageToDifferentFaction = (damageToDifferentFactionRate / 100) * totalAttack;
        double adjustedResistanceToDifferentFaction = (resistanceToDifferentFactionRate / 100) * totalDefense;
        double adjustedDamageToSameFaction = (damageToSameFactionRate / 100) * totalAttack;
        double adjustedResistanceToSameFaction = (resistanceToSameFactionRate / 100) * totalDefense;

        // Điều chỉnh mana
        double adjustedMana = mana * 0.5;
        double adjustedManaRegeneration = (manaRegenerationRate / 100) * mana;

        // Tổng sức mạnh
        return health * weight + totalAttack + totalDefense + speed * weight +
               adjustedCriticalRate + adjustedCriticalDamage + adjustedPenetration +
               adjustedEvasion + adjustedAbsorption + adjustedRegeneration + adjustedAccuracy +
               adjustedLifesteal + adjustedTenacity + adjustedResistance + adjustedCombo + adjustedReflection +
               adjustedDamageToDifferentFaction + adjustedResistanceToDifferentFaction +
               adjustedDamageToSameFaction + adjustedResistanceToSameFaction +
               adjustedMana + adjustedManaRegeneration;
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
                        all_speed, all_critical_damage_rate, all_critical_rate, 
                        all_penetration_rate, all_evasion_rate, all_damage_absorption_rate, all_vitality_regeneration_rate, all_accuracy_rate, 
                        all_lifesteal_rate, all_shield_strength, all_tenacity, all_resistance_rate, all_combo_rate, all_reflection_rate, 
                        all_mana, all_mana_regeneration_rate, all_damage_to_different_faction_rate, 
                        all_resistance_to_different_faction_rate, all_damage_to_same_faction_rate, all_resistance_to_same_faction_rate,
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense,
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack,
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense,
                        percent_all_mental_attack, percent_all_mental_defense
                    )
                    VALUES (
                        @userId, @allPower, @allHealth, @allPhysicalAttack, @allPhysicalDefense,
                        @allMagicalAttack, @allMagicalDefense, @allChemicalAttack, @allChemicalDefense,
                        @allAtomicAttack, @allAtomicDefense, @allMentalAttack, @allMentalDefense,
                        @all_speed, @all_critical_damage_rate, @all_critical_rate, 
                        @all_penetration_rate, @all_evasion_rate, @all_damage_absorption_rate, @all_vitality_regeneration_rate, @all_accuracy_rate, 
                        @all_lifesteal_rate, @all_shield_strength, @all_tenacity, @all_resistance_rate, @all_combo_rate, @all_reflection_rate, 
                        @all_mana, @all_mana_regeneration_rate, @all_damage_to_different_faction_rate, 
                        @all_resistance_to_different_faction_rate, @all_damage_to_same_faction_rate, @all_resistance_to_same_faction_rate,
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
                command.Parameters.AddWithValue("@all_speed", speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", critical_damage_rate);
                command.Parameters.AddWithValue("@all_critical_rate", critical_rate);
                command.Parameters.AddWithValue("@all_penetration_rate", penetration_rate);
                command.Parameters.AddWithValue("@all_evasion_rate", evasion_rate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", damage_absorption_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", vitality_regeneration_rate);
                command.Parameters.AddWithValue("@all_accuracy_rate", accuracy_rate);
                command.Parameters.AddWithValue("@all_lifesteal_rate", lifesteal_rate);
                command.Parameters.AddWithValue("@all_shield_strength", shield_strength);
                command.Parameters.AddWithValue("@all_tenacity", tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", resistance_rate);
                command.Parameters.AddWithValue("@all_combo_rate", combo_rate);
                command.Parameters.AddWithValue("@all_reflection_rate", reflection_rate);
                command.Parameters.AddWithValue("@all_mana", mana);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", mana_regeneration_rate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", resistance_to_same_faction_rate);
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
                    all_speed = @all_speed, all_critical_damage_rate = @all_critical_damage_rate, 
                    all_critical_rate = @all_critical_rate, all_penetration_rate = @all_penetration_rate, 
                    all_evasion_rate = @all_evasion_rate, all_damage_absorption_rate = @all_damage_absorption_rate, 
                    all_vitality_regeneration_rate = @all_vitality_regeneration_rate, all_accuracy_rate = @all_accuracy_rate, 
                    all_lifesteal_rate = @all_lifesteal_rate, all_shield_strength = @all_shield_strength, 
                    all_tenacity = @all_tenacity, all_resistance_rate = @all_resistance_rate, all_combo_rate = @all_combo_rate, 
                    all_reflection_rate = @all_reflection_rate, all_mana = @all_mana, all_mana_regeneration_rate = @all_mana_regeneration_rate, 
                    all_damage_to_different_faction_rate = @all_damage_to_different_faction_rate, 
                    all_resistance_to_different_faction_rate = @all_resistance_to_different_faction_rate, 
                    all_damage_to_same_faction_rate = @all_damage_to_same_faction_rate, 
                    all_resistance_to_same_faction_rate = @all_resistance_to_same_faction_rate,
                    percent_all_health = @percentAllHealth, 
                    percent_all_physical_attack = @percentAllPhysicalAttack, percent_all_physical_defense = @percentAllPhysicalDefense,
                    percent_all_magical_attack = @percentAllMagicalAttack, percent_all_magical_defense = @percentAllMagicalDefense, 
                    percent_all_chemical_attack = @percentAllChemicalAttack, percent_all_chemical_defense = @percentAllChemicalDefense, 
                    percent_all_atomic_attack = @percentAllAtomicAttack, percent_all_atomic_defense = @percentAllAtomicDefense,
                    percent_all_mental_attack = @percentAllMentalAttack, percent_all_mental_defense = @percentAllMentalDefense
                WHERE user_id = @userId;";
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
                command.Parameters.AddWithValue("@all_speed", speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", critical_damage_rate);
                command.Parameters.AddWithValue("@all_critical_rate", critical_rate);
                command.Parameters.AddWithValue("@all_penetration_rate", penetration_rate);
                command.Parameters.AddWithValue("@all_evasion_rate", evasion_rate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", damage_absorption_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", vitality_regeneration_rate);
                command.Parameters.AddWithValue("@all_accuracy_rate", accuracy_rate);
                command.Parameters.AddWithValue("@all_lifesteal_rate", lifesteal_rate);
                command.Parameters.AddWithValue("@all_shield_strength", shield_strength);
                command.Parameters.AddWithValue("@all_tenacity", tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", resistance_rate);
                command.Parameters.AddWithValue("@all_combo_rate", combo_rate);
                command.Parameters.AddWithValue("@all_reflection_rate", reflection_rate);
                command.Parameters.AddWithValue("@all_mana", mana);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", mana_regeneration_rate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", resistance_to_same_faction_rate);
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
                Debug.LogError("Error Message: " + ex.Message);
                Debug.LogError("Error Code: " + ex.Number); // Mã lỗi MySQL (rất hữu ích)
                Debug.LogError("SQLState: " + ex.SqlState); // Chuẩn SQL state code
                Debug.LogError("Stack Trace: " + ex.StackTrace); // Xem lỗi nằm dòng nào
                Debug.LogError("Inner Exception: " + ex.InnerException); // Nếu có exception lồng nhau
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
                        powerManager.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("all_critical_damage_rate")) ? 0 : reader.GetDouble("all_critical_damage_rate");
                        powerManager.critical_rate = reader.IsDBNull(reader.GetOrdinal("all_critical_rate")) ? 0 : reader.GetDouble("all_critical_rate");
                        powerManager.penetration_rate = reader.IsDBNull(reader.GetOrdinal("all_penetration_rate")) ? 0 : reader.GetDouble("all_penetration_rate");
                        powerManager.evasion_rate = reader.IsDBNull(reader.GetOrdinal("all_evasion_rate")) ? 0 : reader.GetDouble("all_evasion_rate");
                        powerManager.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("all_damage_absorption_rate")) ? 0 : reader.GetDouble("all_damage_absorption_rate");
                        powerManager.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("all_vitality_regeneration_rate")) ? 0 : reader.GetDouble("all_vitality_regeneration_rate");
                        powerManager.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("all_accuracy_rate")) ? 0 : reader.GetDouble("all_accuracy_rate");
                        powerManager.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("all_lifesteal_rate")) ? 0 : reader.GetDouble("all_lifesteal_rate");
                        powerManager.shield_strength = reader.IsDBNull(reader.GetOrdinal("all_shield_strength")) ? 0 : reader.GetDouble("all_shield_strength");
                        powerManager.tenacity = reader.IsDBNull(reader.GetOrdinal("all_tenacity")) ? 0 : reader.GetDouble("all_tenacity");
                        powerManager.resistance_rate = reader.IsDBNull(reader.GetOrdinal("all_resistance_rate")) ? 0 : reader.GetDouble("all_resistance_rate");
                        powerManager.combo_rate = reader.IsDBNull(reader.GetOrdinal("all_combo_rate")) ? 0 : reader.GetDouble("all_combo_rate");
                        powerManager.reflection_rate = reader.IsDBNull(reader.GetOrdinal("all_reflection_rate")) ? 0 : reader.GetDouble("all_reflection_rate");
                        powerManager.mana = reader.IsDBNull(reader.GetOrdinal("all_mana")) ? 0 : reader.GetFloat("all_mana");
                        powerManager.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("all_mana_regeneration_rate")) ? 0 : reader.GetDouble("all_mana_regeneration_rate");
                        powerManager.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("all_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("all_damage_to_different_faction_rate");
                        powerManager.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("all_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("all_resistance_to_different_faction_rate");
                        powerManager.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("all_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("all_damage_to_same_faction_rate");
                        powerManager.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("all_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("all_resistance_to_same_faction_rate");
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
        critical_damage_rate = critical_damage_rate + achievements.critical_damage_rate;
        critical_rate = critical_rate + achievements.critical_rate;
        penetration_rate = penetration_rate + achievements.penetration_rate;
        evasion_rate = evasion_rate + achievements.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + achievements.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + achievements.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + achievements.accuracy_rate;
        lifesteal_rate = lifesteal_rate + achievements.lifesteal_rate;
        shield_strength = shield_strength + achievements.shield_strength;
        tenacity = tenacity + achievements.tenacity;
        resistance_rate = resistance_rate + achievements.resistance_rate;
        combo_rate = combo_rate + achievements.combo_rate;
        reflection_rate = reflection_rate + achievements.reflection_rate;
        mana = mana + achievements.mana;
        mana_regeneration_rate = mana_regeneration_rate + achievements.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + achievements.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + achievements.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + achievements.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + achievements.resistance_to_same_faction_rate;

        //Percent
        achievements = achievements.SumPowerAchievementsPercent();
        percent_all_health = percent_all_health + achievements.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + achievements.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + achievements.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + achievements.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + achievements.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + achievements.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + achievements.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + achievements.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + achievements.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + achievements.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + achievements.percent_all_mental_defense;
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
        critical_damage_rate = critical_damage_rate + books.critical_damage_rate;
        critical_rate = critical_rate + books.critical_rate;
        penetration_rate = penetration_rate + books.penetration_rate;
        evasion_rate = evasion_rate + books.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + books.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + books.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + books.accuracy_rate;
        lifesteal_rate = lifesteal_rate + books.lifesteal_rate;
        shield_strength = shield_strength + books.shield_strength;
        tenacity = tenacity + books.tenacity;
        resistance_rate = resistance_rate + books.resistance_rate;
        combo_rate = combo_rate + books.combo_rate;
        reflection_rate = reflection_rate + books.reflection_rate;
        mana = mana + books.mana;
        mana_regeneration_rate = mana_regeneration_rate + books.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + books.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + books.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + books.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + books.resistance_to_same_faction_rate;

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
        critical_damage_rate = critical_damage_rate + borders.critical_damage_rate;
        critical_rate = critical_rate + borders.critical_rate;
        penetration_rate = penetration_rate + borders.penetration_rate;
        evasion_rate = evasion_rate + borders.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + borders.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + borders.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + borders.accuracy_rate;
        lifesteal_rate = lifesteal_rate + borders.lifesteal_rate;
        shield_strength = shield_strength + borders.shield_strength;
        tenacity = tenacity + borders.tenacity;
        resistance_rate = resistance_rate + borders.resistance_rate;
        combo_rate = combo_rate + borders.combo_rate;
        reflection_rate = reflection_rate + borders.reflection_rate;
        mana = mana + borders.mana;
        mana_regeneration_rate = mana_regeneration_rate + borders.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + borders.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + borders.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + borders.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + borders.resistance_to_same_faction_rate;

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
        critical_damage_rate = critical_damage_rate + borders.critical_damage_rate;
        critical_rate = critical_rate + borders.critical_rate;
        penetration_rate = penetration_rate + borders.penetration_rate;
        evasion_rate = evasion_rate + borders.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + borders.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + borders.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + borders.accuracy_rate;
        lifesteal_rate = lifesteal_rate + borders.lifesteal_rate;
        shield_strength = shield_strength + borders.shield_strength;
        tenacity = tenacity + borders.tenacity;
        resistance_rate = resistance_rate + borders.resistance_rate;
        combo_rate = combo_rate + borders.combo_rate;
        reflection_rate = reflection_rate + borders.reflection_rate;
        mana = mana + borders.mana;
        mana_regeneration_rate = mana_regeneration_rate + borders.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + borders.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + borders.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + borders.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + borders.resistance_to_same_faction_rate;

        //Percent
        borders = borders.SumPowerBordersPercent();
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
    }
    public void GetAvatarsPower()
    {
        Avatars avatars = new Avatars();
        //Gallery
        avatars = avatars.SumPowerAvatarsGallery();
        power = power + avatars.power;
        health = health + avatars.health;
        physical_attack = physical_attack + avatars.physical_attack;
        physical_defense = physical_defense + avatars.physical_defense;
        magical_attack = magical_attack + avatars.magical_attack;
        magical_defense = magical_defense + avatars.magical_defense;
        chemical_attack = chemical_attack + avatars.chemical_attack;
        chemical_defense = chemical_defense + avatars.chemical_defense;
        atomic_attack = atomic_attack + avatars.atomic_attack;
        atomic_defense = atomic_defense + avatars.atomic_defense;
        mental_attack = mental_attack + avatars.mental_attack;
        mental_defense = mental_defense + avatars.mental_defense;
        speed = speed + avatars.speed;
        critical_damage_rate = critical_damage_rate + avatars.critical_damage_rate;
        critical_rate = critical_rate + avatars.critical_rate;
        penetration_rate = penetration_rate + avatars.penetration_rate;
        evasion_rate = evasion_rate + avatars.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + avatars.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + avatars.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + avatars.accuracy_rate;
        lifesteal_rate = lifesteal_rate + avatars.lifesteal_rate;
        shield_strength = shield_strength + avatars.shield_strength;
        tenacity = tenacity + avatars.tenacity;
        resistance_rate = resistance_rate + avatars.resistance_rate;
        combo_rate = combo_rate + avatars.combo_rate;
        reflection_rate = reflection_rate + avatars.reflection_rate;
        mana = mana + avatars.mana;
        mana_regeneration_rate = mana_regeneration_rate + avatars.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + avatars.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + avatars.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + avatars.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + avatars.resistance_to_same_faction_rate;

        percent_all_health = percent_all_health + avatars.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + avatars.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + avatars.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + avatars.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + avatars.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + avatars.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + avatars.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + avatars.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + avatars.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + avatars.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + avatars.percent_all_mental_defense;

        //User
        avatars = avatars.SumPowerUserAvatars();
        power = power + avatars.power;
        health = health + avatars.health;
        physical_attack = physical_attack + avatars.physical_attack;
        physical_defense = physical_defense + avatars.physical_defense;
        magical_attack = magical_attack + avatars.magical_attack;
        magical_defense = magical_defense + avatars.magical_defense;
        chemical_attack = chemical_attack + avatars.chemical_attack;
        chemical_defense = chemical_defense + avatars.chemical_defense;
        atomic_attack = atomic_attack + avatars.atomic_attack;
        atomic_defense = atomic_defense + avatars.atomic_defense;
        mental_attack = mental_attack + avatars.mental_attack;
        mental_defense = mental_defense + avatars.mental_defense;
        speed = speed + avatars.speed;
        critical_damage_rate = critical_damage_rate + avatars.critical_damage_rate;
        critical_rate = critical_rate + avatars.critical_rate;
        penetration_rate = penetration_rate + avatars.penetration_rate;
        evasion_rate = evasion_rate + avatars.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + avatars.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + avatars.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + avatars.accuracy_rate;
        lifesteal_rate = lifesteal_rate + avatars.lifesteal_rate;
        shield_strength = shield_strength + avatars.shield_strength;
        tenacity = tenacity + avatars.tenacity;
        resistance_rate = resistance_rate + avatars.resistance_rate;
        combo_rate = combo_rate + avatars.combo_rate;
        reflection_rate = reflection_rate + avatars.reflection_rate;
        mana = mana + avatars.mana;
        mana_regeneration_rate = mana_regeneration_rate + avatars.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + avatars.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + avatars.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + avatars.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + avatars.resistance_to_same_faction_rate;

        //Percent
        avatars = avatars.SumPowerAvatarsPercent();
        percent_all_health = percent_all_health + avatars.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + avatars.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + avatars.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + avatars.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + avatars.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + avatars.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + avatars.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + avatars.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + avatars.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + avatars.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + avatars.percent_all_mental_defense;
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
        critical_damage_rate = critical_damage_rate + cardHeroes.critical_damage_rate;
        critical_rate = critical_rate + cardHeroes.critical_rate;
        penetration_rate = penetration_rate + cardHeroes.penetration_rate;
        evasion_rate = evasion_rate + cardHeroes.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + cardHeroes.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + cardHeroes.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + cardHeroes.accuracy_rate;
        lifesteal_rate = lifesteal_rate + cardHeroes.lifesteal_rate;
        shield_strength = shield_strength + cardHeroes.shield_strength;
        tenacity = tenacity + cardHeroes.tenacity;
        resistance_rate = resistance_rate + cardHeroes.resistance_rate;
        combo_rate = combo_rate + cardHeroes.combo_rate;
        reflection_rate = reflection_rate + cardHeroes.reflection_rate;
        mana = mana + cardHeroes.mana;
        mana_regeneration_rate = mana_regeneration_rate + cardHeroes.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + cardHeroes.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + cardHeroes.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + cardHeroes.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + cardHeroes.resistance_to_same_faction_rate;

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
        critical_damage_rate = critical_damage_rate + cardCaptains.critical_damage_rate;
        critical_rate = critical_rate + cardCaptains.critical_rate;
        penetration_rate = penetration_rate + cardCaptains.penetration_rate;
        evasion_rate = evasion_rate + cardCaptains.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + cardCaptains.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + cardCaptains.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + cardCaptains.accuracy_rate;
        lifesteal_rate = lifesteal_rate + cardCaptains.lifesteal_rate;
        shield_strength = shield_strength + cardCaptains.shield_strength;
        tenacity = tenacity + cardCaptains.tenacity;
        resistance_rate = resistance_rate + cardCaptains.resistance_rate;
        combo_rate = combo_rate + cardCaptains.combo_rate;
        reflection_rate = reflection_rate + cardCaptains.reflection_rate;
        mana = mana + cardCaptains.mana;
        mana_regeneration_rate = mana_regeneration_rate + cardCaptains.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + cardCaptains.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + cardCaptains.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + cardCaptains.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + cardCaptains.resistance_to_same_faction_rate;

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
        critical_damage_rate = critical_damage_rate + cardColonels.critical_damage_rate;
        critical_rate = critical_rate + cardColonels.critical_rate;
        penetration_rate = penetration_rate + cardColonels.penetration_rate;
        evasion_rate = evasion_rate + cardColonels.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + cardColonels.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + cardColonels.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + cardColonels.accuracy_rate;
        lifesteal_rate = lifesteal_rate + cardColonels.lifesteal_rate;
        shield_strength = shield_strength + cardColonels.shield_strength;
        tenacity = tenacity + cardColonels.tenacity;
        resistance_rate = resistance_rate + cardColonels.resistance_rate;
        combo_rate = combo_rate + cardColonels.combo_rate;
        reflection_rate = reflection_rate + cardColonels.reflection_rate;
        mana = mana + cardColonels.mana;
        mana_regeneration_rate = mana_regeneration_rate + cardColonels.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + cardColonels.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + cardColonels.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + cardColonels.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + cardColonels.resistance_to_same_faction_rate;

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
        critical_damage_rate = critical_damage_rate + cardGenerals.critical_damage_rate;
        critical_rate = critical_rate + cardGenerals.critical_rate;
        penetration_rate = penetration_rate + cardGenerals.penetration_rate;
        evasion_rate = evasion_rate + cardGenerals.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + cardGenerals.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + cardGenerals.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + cardGenerals.accuracy_rate;
        lifesteal_rate = lifesteal_rate + cardGenerals.lifesteal_rate;
        shield_strength = shield_strength + cardGenerals.shield_strength;
        tenacity = tenacity + cardGenerals.tenacity;
        resistance_rate = resistance_rate + cardGenerals.resistance_rate;
        combo_rate = combo_rate + cardGenerals.combo_rate;
        reflection_rate = reflection_rate + cardGenerals.reflection_rate;
        mana = mana + cardGenerals.mana;
        mana_regeneration_rate = mana_regeneration_rate + cardGenerals.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + cardGenerals.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + cardGenerals.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + cardGenerals.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + cardGenerals.resistance_to_same_faction_rate;

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
        critical_damage_rate = critical_damage_rate + cardAdmirals.critical_damage_rate;
        critical_rate = critical_rate + cardAdmirals.critical_rate;
        penetration_rate = penetration_rate + cardAdmirals.penetration_rate;
        evasion_rate = evasion_rate + cardAdmirals.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + cardAdmirals.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + cardAdmirals.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + cardAdmirals.accuracy_rate;
        lifesteal_rate = lifesteal_rate + cardAdmirals.lifesteal_rate;
        shield_strength = shield_strength + cardAdmirals.shield_strength;
        tenacity = tenacity + cardAdmirals.tenacity;
        resistance_rate = resistance_rate + cardAdmirals.resistance_rate;
        combo_rate = combo_rate + cardAdmirals.combo_rate;
        reflection_rate = reflection_rate + cardAdmirals.reflection_rate;
        mana = mana + cardAdmirals.mana;
        mana_regeneration_rate = mana_regeneration_rate + cardAdmirals.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + cardAdmirals.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + cardAdmirals.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + cardAdmirals.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + cardAdmirals.resistance_to_same_faction_rate;

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
        critical_damage_rate = critical_damage_rate + cardMonsters.critical_damage_rate;
        critical_rate = critical_rate + cardMonsters.critical_rate;
        penetration_rate = penetration_rate + cardMonsters.penetration_rate;
        evasion_rate = evasion_rate + cardMonsters.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + cardMonsters.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + cardMonsters.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + cardMonsters.accuracy_rate;
        lifesteal_rate = lifesteal_rate + cardMonsters.lifesteal_rate;
        shield_strength = shield_strength + cardMonsters.shield_strength;
        tenacity = tenacity + cardMonsters.tenacity;
        resistance_rate = resistance_rate + cardMonsters.resistance_rate;
        combo_rate = combo_rate + cardMonsters.combo_rate;
        reflection_rate = reflection_rate + cardMonsters.reflection_rate;
        mana = mana + cardMonsters.mana;
        mana_regeneration_rate = mana_regeneration_rate + cardMonsters.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + cardMonsters.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + cardMonsters.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + cardMonsters.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + cardMonsters.resistance_to_same_faction_rate;

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
        critical_damage_rate = critical_damage_rate + cardMilitary.critical_damage_rate;
        critical_rate = critical_rate + cardMilitary.critical_rate;
        penetration_rate = penetration_rate + cardMilitary.penetration_rate;
        evasion_rate = evasion_rate + cardMilitary.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + cardMilitary.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + cardMilitary.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + cardMilitary.accuracy_rate;
        lifesteal_rate = lifesteal_rate + cardMilitary.lifesteal_rate;
        shield_strength = shield_strength + cardMilitary.shield_strength;
        tenacity = tenacity + cardMilitary.tenacity;
        resistance_rate = resistance_rate + cardMilitary.resistance_rate;
        combo_rate = combo_rate + cardMilitary.combo_rate;
        reflection_rate = reflection_rate + cardMilitary.reflection_rate;
        mana = mana + cardMilitary.mana;
        mana_regeneration_rate = mana_regeneration_rate + cardMilitary.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + cardMilitary.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + cardMilitary.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + cardMilitary.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + cardMilitary.resistance_to_same_faction_rate;

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
        critical_damage_rate = critical_damage_rate + cardSpell.critical_damage_rate;
        critical_rate = critical_rate + cardSpell.critical_rate;
        penetration_rate = penetration_rate + cardSpell.penetration_rate;
        evasion_rate = evasion_rate + cardSpell.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + cardSpell.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + cardSpell.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + cardSpell.accuracy_rate;
        lifesteal_rate = lifesteal_rate + cardSpell.lifesteal_rate;
        shield_strength = shield_strength + cardSpell.shield_strength;
        tenacity = tenacity + cardSpell.tenacity;
        resistance_rate = resistance_rate + cardSpell.resistance_rate;
        combo_rate = combo_rate + cardSpell.combo_rate;
        reflection_rate = reflection_rate + cardSpell.reflection_rate;
        mana = mana + cardSpell.mana;
        mana_regeneration_rate = mana_regeneration_rate + cardSpell.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + cardSpell.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + cardSpell.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + cardSpell.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + cardSpell.resistance_to_same_faction_rate;

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
        critical_damage_rate = critical_damage_rate + collaboration.critical_damage_rate;
        critical_rate = critical_rate + collaboration.critical_rate;
        penetration_rate = penetration_rate + collaboration.penetration_rate;
        evasion_rate = evasion_rate + collaboration.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + collaboration.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + collaboration.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + collaboration.accuracy_rate;
        lifesteal_rate = lifesteal_rate + collaboration.lifesteal_rate;
        shield_strength = shield_strength + collaboration.shield_strength;
        tenacity = tenacity + collaboration.tenacity;
        resistance_rate = resistance_rate + collaboration.resistance_rate;
        combo_rate = combo_rate + collaboration.combo_rate;
        reflection_rate = reflection_rate + collaboration.reflection_rate;
        mana = mana + collaboration.mana;
        mana_regeneration_rate = mana_regeneration_rate + collaboration.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + collaboration.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + collaboration.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + collaboration.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + collaboration.resistance_to_same_faction_rate;

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
        critical_damage_rate = critical_damage_rate + collaboration.critical_damage_rate;
        critical_rate = critical_rate + collaboration.critical_rate;
        penetration_rate = penetration_rate + collaboration.penetration_rate;
        evasion_rate = evasion_rate + collaboration.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + collaboration.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + collaboration.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + collaboration.accuracy_rate;
        lifesteal_rate = lifesteal_rate + collaboration.lifesteal_rate;
        shield_strength = shield_strength + collaboration.shield_strength;
        tenacity = tenacity + collaboration.tenacity;
        resistance_rate = resistance_rate + collaboration.resistance_rate;
        combo_rate = combo_rate + collaboration.combo_rate;
        reflection_rate = reflection_rate + collaboration.reflection_rate;
        mana = mana + collaboration.mana;
        mana_regeneration_rate = mana_regeneration_rate + collaboration.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + collaboration.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + collaboration.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + collaboration.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + collaboration.resistance_to_same_faction_rate;

        //Percent
        collaboration = collaboration.SumPowerCollaborationsPercent();
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
        critical_damage_rate = critical_damage_rate + collaborationEquipment.critical_damage_rate;
        critical_rate = critical_rate + collaborationEquipment.critical_rate;
        penetration_rate = penetration_rate + collaborationEquipment.penetration_rate;
        evasion_rate = evasion_rate + collaborationEquipment.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + collaborationEquipment.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + collaborationEquipment.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + collaborationEquipment.accuracy_rate;
        lifesteal_rate = lifesteal_rate + collaborationEquipment.lifesteal_rate;
        shield_strength = shield_strength + collaborationEquipment.shield_strength;
        tenacity = tenacity + collaborationEquipment.tenacity;
        resistance_rate = resistance_rate + collaborationEquipment.resistance_rate;
        combo_rate = combo_rate + collaborationEquipment.combo_rate;
        reflection_rate = reflection_rate + collaborationEquipment.reflection_rate;
        mana = mana + collaborationEquipment.mana;
        mana_regeneration_rate = mana_regeneration_rate + collaborationEquipment.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + collaborationEquipment.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + collaborationEquipment.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + collaborationEquipment.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + collaborationEquipment.resistance_to_same_faction_rate;

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
        critical_damage_rate = critical_damage_rate + collaborationEquipment.critical_damage_rate;
        critical_rate = critical_rate + collaborationEquipment.critical_rate;
        penetration_rate = penetration_rate + collaborationEquipment.penetration_rate;
        evasion_rate = evasion_rate + collaborationEquipment.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + collaborationEquipment.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + collaborationEquipment.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + collaborationEquipment.accuracy_rate;
        lifesteal_rate = lifesteal_rate + collaborationEquipment.lifesteal_rate;
        shield_strength = shield_strength + collaborationEquipment.shield_strength;
        tenacity = tenacity + collaborationEquipment.tenacity;
        resistance_rate = resistance_rate + collaborationEquipment.resistance_rate;
        combo_rate = combo_rate + collaborationEquipment.combo_rate;
        reflection_rate = reflection_rate + collaborationEquipment.reflection_rate;
        mana = mana + collaborationEquipment.mana;
        mana_regeneration_rate = mana_regeneration_rate + collaborationEquipment.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + collaborationEquipment.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + collaborationEquipment.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + collaborationEquipment.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + collaborationEquipment.resistance_to_same_faction_rate;
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
        critical_damage_rate = critical_damage_rate + equipments.critical_damage_rate;
        critical_rate = critical_rate + equipments.critical_rate;
        penetration_rate = penetration_rate + equipments.penetration_rate;
        evasion_rate = evasion_rate + equipments.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + equipments.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + equipments.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + equipments.accuracy_rate;
        lifesteal_rate = lifesteal_rate + equipments.lifesteal_rate;
        shield_strength = shield_strength + equipments.shield_strength;
        tenacity = tenacity + equipments.tenacity;
        resistance_rate = resistance_rate + equipments.resistance_rate;
        combo_rate = combo_rate + equipments.combo_rate;
        reflection_rate = reflection_rate + equipments.reflection_rate;
        mana = mana + equipments.mana;
        mana_regeneration_rate = mana_regeneration_rate + equipments.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + equipments.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + equipments.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + equipments.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + equipments.resistance_to_same_faction_rate;

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
        critical_damage_rate = critical_damage_rate + magicFormationCircle.critical_damage_rate;
        critical_rate = critical_rate + magicFormationCircle.critical_rate;
        penetration_rate = penetration_rate + magicFormationCircle.penetration_rate;
        evasion_rate = evasion_rate + magicFormationCircle.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + magicFormationCircle.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + magicFormationCircle.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + magicFormationCircle.accuracy_rate;
        lifesteal_rate = lifesteal_rate + magicFormationCircle.lifesteal_rate;
        shield_strength = shield_strength + magicFormationCircle.shield_strength;
        tenacity = tenacity + magicFormationCircle.tenacity;
        resistance_rate = resistance_rate + magicFormationCircle.resistance_rate;
        combo_rate = combo_rate + magicFormationCircle.combo_rate;
        reflection_rate = reflection_rate + magicFormationCircle.reflection_rate;
        mana = mana + magicFormationCircle.mana;
        mana_regeneration_rate = mana_regeneration_rate + magicFormationCircle.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + magicFormationCircle.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + magicFormationCircle.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + magicFormationCircle.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + magicFormationCircle.resistance_to_same_faction_rate;

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

        //User
        magicFormationCircle = magicFormationCircle.SumPowerUserMagicFormationCircle();
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
        critical_damage_rate = critical_damage_rate + magicFormationCircle.critical_damage_rate;
        critical_rate = critical_rate + magicFormationCircle.critical_rate;
        penetration_rate = penetration_rate + magicFormationCircle.penetration_rate;
        evasion_rate = evasion_rate + magicFormationCircle.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + magicFormationCircle.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + magicFormationCircle.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + magicFormationCircle.accuracy_rate;
        lifesteal_rate = lifesteal_rate + magicFormationCircle.lifesteal_rate;
        shield_strength = shield_strength + magicFormationCircle.shield_strength;
        tenacity = tenacity + magicFormationCircle.tenacity;
        resistance_rate = resistance_rate + magicFormationCircle.resistance_rate;
        combo_rate = combo_rate + magicFormationCircle.combo_rate;
        reflection_rate = reflection_rate + magicFormationCircle.reflection_rate;
        mana = mana + magicFormationCircle.mana;
        mana_regeneration_rate = mana_regeneration_rate + magicFormationCircle.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + magicFormationCircle.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + magicFormationCircle.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + magicFormationCircle.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + magicFormationCircle.resistance_to_same_faction_rate;

        //Percent
        magicFormationCircle = magicFormationCircle.SumPowerMagicFormationCirclePercent();
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
        critical_damage_rate = critical_damage_rate + relics.critical_damage_rate;
        critical_rate = critical_rate + relics.critical_rate;
        penetration_rate = penetration_rate + relics.penetration_rate;
        evasion_rate = evasion_rate + relics.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + relics.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + relics.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + relics.accuracy_rate;
        lifesteal_rate = lifesteal_rate + relics.lifesteal_rate;
        shield_strength = shield_strength + relics.shield_strength;
        tenacity = tenacity + relics.tenacity;
        resistance_rate = resistance_rate + relics.resistance_rate;
        combo_rate = combo_rate + relics.combo_rate;
        reflection_rate = reflection_rate + relics.reflection_rate;
        mana = mana + relics.mana;
        mana_regeneration_rate = mana_regeneration_rate + relics.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + relics.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + relics.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + relics.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + relics.resistance_to_same_faction_rate;

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

        //User
        relics = relics.SumPowerUserRelics();
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
        critical_damage_rate = critical_damage_rate + relics.critical_damage_rate;
        critical_rate = critical_rate + relics.critical_rate;
        penetration_rate = penetration_rate + relics.penetration_rate;
        evasion_rate = evasion_rate + relics.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + relics.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + relics.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + relics.accuracy_rate;
        lifesteal_rate = lifesteal_rate + relics.lifesteal_rate;
        shield_strength = shield_strength + relics.shield_strength;
        tenacity = tenacity + relics.tenacity;
        resistance_rate = resistance_rate + relics.resistance_rate;
        combo_rate = combo_rate + relics.combo_rate;
        reflection_rate = reflection_rate + relics.reflection_rate;
        mana = mana + relics.mana;
        mana_regeneration_rate = mana_regeneration_rate + relics.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + relics.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + relics.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + relics.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + relics.resistance_to_same_faction_rate;

        //Percent
        relics = relics.SumPowerRelicsPercent();
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
        critical_damage_rate = critical_damage_rate + medals.critical_damage_rate;
        critical_rate = critical_rate + medals.critical_rate;
        penetration_rate = penetration_rate + medals.penetration_rate;
        evasion_rate = evasion_rate + medals.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + medals.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + medals.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + medals.accuracy_rate;
        lifesteal_rate = lifesteal_rate + medals.lifesteal_rate;
        shield_strength = shield_strength + medals.shield_strength;
        tenacity = tenacity + medals.tenacity;
        resistance_rate = resistance_rate + medals.resistance_rate;
        combo_rate = combo_rate + medals.combo_rate;
        reflection_rate = reflection_rate + medals.reflection_rate;
        mana = mana + medals.mana;
        mana_regeneration_rate = mana_regeneration_rate + medals.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + medals.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + medals.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + medals.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + medals.resistance_to_same_faction_rate;

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
        critical_damage_rate = critical_damage_rate + medals.critical_damage_rate;
        critical_rate = critical_rate + medals.critical_rate;
        penetration_rate = penetration_rate + medals.penetration_rate;
        evasion_rate = evasion_rate + medals.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + medals.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + medals.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + medals.accuracy_rate;
        lifesteal_rate = lifesteal_rate + medals.lifesteal_rate;
        shield_strength = shield_strength + medals.shield_strength;
        tenacity = tenacity + medals.tenacity;
        resistance_rate = resistance_rate + medals.resistance_rate;
        combo_rate = combo_rate + medals.combo_rate;
        reflection_rate = reflection_rate + medals.reflection_rate;
        mana = mana + medals.mana;
        mana_regeneration_rate = mana_regeneration_rate + medals.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + medals.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + medals.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + medals.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + medals.resistance_to_same_faction_rate;

        //Percent
        medals = medals.SumPowerMedalsPercent();
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
        critical_damage_rate = critical_damage_rate + pets.critical_damage_rate;
        critical_rate = critical_rate + pets.critical_rate;
        penetration_rate = penetration_rate + pets.penetration_rate;
        evasion_rate = evasion_rate + pets.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + pets.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + pets.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + pets.accuracy_rate;
        lifesteal_rate = lifesteal_rate + pets.lifesteal_rate;
        shield_strength = shield_strength + pets.shield_strength;
        tenacity = tenacity + pets.tenacity;
        resistance_rate = resistance_rate + pets.resistance_rate;
        combo_rate = combo_rate + pets.combo_rate;
        reflection_rate = reflection_rate + pets.reflection_rate;
        mana = mana + pets.mana;
        mana_regeneration_rate = mana_regeneration_rate + pets.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + pets.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + pets.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + pets.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + pets.resistance_to_same_faction_rate;

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
        critical_damage_rate = critical_damage_rate + symbols.critical_damage_rate;
        critical_rate = critical_rate + symbols.critical_rate;
        penetration_rate = penetration_rate + symbols.penetration_rate;
        evasion_rate = evasion_rate + symbols.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + symbols.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + symbols.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + symbols.accuracy_rate;
        lifesteal_rate = lifesteal_rate + symbols.lifesteal_rate;
        shield_strength = shield_strength + symbols.shield_strength;
        tenacity = tenacity + symbols.tenacity;
        resistance_rate = resistance_rate + symbols.resistance_rate;
        combo_rate = combo_rate + symbols.combo_rate;
        reflection_rate = reflection_rate + symbols.reflection_rate;
        mana = mana + symbols.mana;
        mana_regeneration_rate = mana_regeneration_rate + symbols.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + symbols.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + symbols.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + symbols.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + symbols.resistance_to_same_faction_rate;

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
        critical_damage_rate = critical_damage_rate + symbols.critical_damage_rate;
        critical_rate = critical_rate + symbols.critical_rate;
        penetration_rate = penetration_rate + symbols.penetration_rate;
        evasion_rate = evasion_rate + symbols.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + symbols.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + symbols.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + symbols.accuracy_rate;
        lifesteal_rate = lifesteal_rate + symbols.lifesteal_rate;
        shield_strength = shield_strength + symbols.shield_strength;
        tenacity = tenacity + symbols.tenacity;
        resistance_rate = resistance_rate + symbols.resistance_rate;
        combo_rate = combo_rate + symbols.combo_rate;
        reflection_rate = reflection_rate + symbols.reflection_rate;
        mana = mana + symbols.mana;
        mana_regeneration_rate = mana_regeneration_rate + symbols.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + symbols.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + symbols.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + symbols.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + symbols.resistance_to_same_faction_rate;

        //Percent
        symbols = symbols.SumPowerSymbolsPercent();
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
        critical_damage_rate = critical_damage_rate + skills.critical_damage_rate;
        critical_rate = critical_rate + skills.critical_rate;
        penetration_rate = penetration_rate + skills.penetration_rate;
        evasion_rate = evasion_rate + skills.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + skills.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + skills.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + skills.accuracy_rate;
        lifesteal_rate = lifesteal_rate + skills.lifesteal_rate;
        shield_strength = shield_strength + skills.shield_strength;
        tenacity = tenacity + skills.tenacity;
        resistance_rate = resistance_rate + skills.resistance_rate;
        combo_rate = combo_rate + skills.combo_rate;
        reflection_rate = reflection_rate + skills.reflection_rate;
        mana = mana + skills.mana;
        mana_regeneration_rate = mana_regeneration_rate + skills.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + skills.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + skills.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + skills.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + skills.resistance_to_same_faction_rate;

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
        critical_damage_rate = critical_damage_rate + titles.critical_damage_rate;
        critical_rate = critical_rate + titles.critical_rate;
        penetration_rate = penetration_rate + titles.penetration_rate;
        evasion_rate = evasion_rate + titles.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + titles.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + titles.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + titles.accuracy_rate;
        lifesteal_rate = lifesteal_rate + titles.lifesteal_rate;
        shield_strength = shield_strength + titles.shield_strength;
        tenacity = tenacity + titles.tenacity;
        resistance_rate = resistance_rate + titles.resistance_rate;
        combo_rate = combo_rate + titles.combo_rate;
        reflection_rate = reflection_rate + titles.reflection_rate;
        mana = mana + titles.mana;
        mana_regeneration_rate = mana_regeneration_rate + titles.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + titles.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + titles.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + titles.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + titles.resistance_to_same_faction_rate;

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
        critical_damage_rate = critical_damage_rate + titles.critical_damage_rate;
        critical_rate = critical_rate + titles.critical_rate;
        penetration_rate = penetration_rate + titles.penetration_rate;
        evasion_rate = evasion_rate + titles.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + titles.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + titles.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + titles.accuracy_rate;
        lifesteal_rate = lifesteal_rate + titles.lifesteal_rate;
        shield_strength = shield_strength + titles.shield_strength;
        tenacity = tenacity + titles.tenacity;
        resistance_rate = resistance_rate + titles.resistance_rate;
        combo_rate = combo_rate + titles.combo_rate;
        reflection_rate = reflection_rate + titles.reflection_rate;
        mana = mana + titles.mana;
        mana_regeneration_rate = mana_regeneration_rate + titles.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + titles.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + titles.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + titles.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + titles.resistance_to_same_faction_rate;

        //Percent
        titles = titles.SumPowerTitlesPercent();
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
    }
    public void GetTalismanPower()
    {
        Talisman talisman = new Talisman();
        //Gallery
        talisman = talisman.SumPowerTalismanGallery();
        power = power + talisman.power;
        health = health + talisman.health;
        physical_attack = physical_attack + talisman.physical_attack;
        physical_defense = physical_defense + talisman.physical_defense;
        magical_attack = magical_attack + talisman.magical_attack;
        magical_defense = magical_defense + talisman.magical_defense;
        chemical_attack = chemical_attack + talisman.chemical_attack;
        chemical_defense = chemical_defense + talisman.chemical_defense;
        atomic_attack = atomic_attack + talisman.atomic_attack;
        atomic_defense = atomic_defense + talisman.atomic_defense;
        mental_attack = mental_attack + talisman.mental_attack;
        mental_defense = mental_defense + talisman.mental_defense;
        speed = speed + talisman.speed;
        critical_damage_rate = critical_damage_rate + talisman.critical_damage_rate;
        critical_rate = critical_rate + talisman.critical_rate;
        penetration_rate = penetration_rate + talisman.penetration_rate;
        evasion_rate = evasion_rate + talisman.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + talisman.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + talisman.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + talisman.accuracy_rate;
        lifesteal_rate = lifesteal_rate + talisman.lifesteal_rate;
        shield_strength = shield_strength + talisman.shield_strength;
        tenacity = tenacity + talisman.tenacity;
        resistance_rate = resistance_rate + talisman.resistance_rate;
        combo_rate = combo_rate + talisman.combo_rate;
        reflection_rate = reflection_rate + talisman.reflection_rate;
        mana = mana + talisman.mana;
        mana_regeneration_rate = mana_regeneration_rate + talisman.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + talisman.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + talisman.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + talisman.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + talisman.resistance_to_same_faction_rate;

        percent_all_health = percent_all_health + talisman.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + talisman.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + talisman.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + talisman.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + talisman.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + talisman.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + talisman.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + talisman.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + talisman.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + talisman.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + talisman.percent_all_mental_defense;

        //User
        talisman = talisman.SumPowerUserTalisman();
        power = power + talisman.power;
        health = health + talisman.health;
        physical_attack = physical_attack + talisman.physical_attack;
        physical_defense = physical_defense + talisman.physical_defense;
        magical_attack = magical_attack + talisman.magical_attack;
        magical_defense = magical_defense + talisman.magical_defense;
        chemical_attack = chemical_attack + talisman.chemical_attack;
        chemical_defense = chemical_defense + talisman.chemical_defense;
        atomic_attack = atomic_attack + talisman.atomic_attack;
        atomic_defense = atomic_defense + talisman.atomic_defense;
        mental_attack = mental_attack + talisman.mental_attack;
        mental_defense = mental_defense + talisman.mental_defense;
        speed = speed + talisman.speed;
        critical_damage_rate = critical_damage_rate + talisman.critical_damage_rate;
        critical_rate = critical_rate + talisman.critical_rate;
        penetration_rate = penetration_rate + talisman.penetration_rate;
        evasion_rate = evasion_rate + talisman.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + talisman.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + talisman.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + talisman.accuracy_rate;
        lifesteal_rate = lifesteal_rate + talisman.lifesteal_rate;
        shield_strength = shield_strength + talisman.shield_strength;
        tenacity = tenacity + talisman.tenacity;
        resistance_rate = resistance_rate + talisman.resistance_rate;
        combo_rate = combo_rate + talisman.combo_rate;
        reflection_rate = reflection_rate + talisman.reflection_rate;
        mana = mana + talisman.mana;
        mana_regeneration_rate = mana_regeneration_rate + talisman.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + talisman.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + talisman.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + talisman.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + talisman.resistance_to_same_faction_rate;

        //Percent
        talisman = talisman.SumPowerTalismanPercent();
        percent_all_health = percent_all_health + talisman.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + talisman.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + talisman.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + talisman.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + talisman.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + talisman.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + talisman.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + talisman.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + talisman.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + talisman.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + talisman.percent_all_mental_defense;
    }
    public void GetPuppetPower()
    {
        Puppet puppet = new Puppet();
        //Gallery
        puppet = puppet.SumPowerPuppetGallery();
        power = power + puppet.power;
        health = health + puppet.health;
        physical_attack = physical_attack + puppet.physical_attack;
        physical_defense = physical_defense + puppet.physical_defense;
        magical_attack = magical_attack + puppet.magical_attack;
        magical_defense = magical_defense + puppet.magical_defense;
        chemical_attack = chemical_attack + puppet.chemical_attack;
        chemical_defense = chemical_defense + puppet.chemical_defense;
        atomic_attack = atomic_attack + puppet.atomic_attack;
        atomic_defense = atomic_defense + puppet.atomic_defense;
        mental_attack = mental_attack + puppet.mental_attack;
        mental_defense = mental_defense + puppet.mental_defense;
        speed = speed + puppet.speed;
        critical_damage_rate = critical_damage_rate + puppet.critical_damage_rate;
        critical_rate = critical_rate + puppet.critical_rate;
        penetration_rate = penetration_rate + puppet.penetration_rate;
        evasion_rate = evasion_rate + puppet.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + puppet.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + puppet.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + puppet.accuracy_rate;
        lifesteal_rate = lifesteal_rate + puppet.lifesteal_rate;
        shield_strength = shield_strength + puppet.shield_strength;
        tenacity = tenacity + puppet.tenacity;
        resistance_rate = resistance_rate + puppet.resistance_rate;
        combo_rate = combo_rate + puppet.combo_rate;
        reflection_rate = reflection_rate + puppet.reflection_rate;
        mana = mana + puppet.mana;
        mana_regeneration_rate = mana_regeneration_rate + puppet.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + puppet.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + puppet.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + puppet.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + puppet.resistance_to_same_faction_rate;

        percent_all_health = percent_all_health + puppet.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + puppet.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + puppet.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + puppet.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + puppet.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + puppet.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + puppet.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + puppet.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + puppet.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + puppet.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + puppet.percent_all_mental_defense;

        //User
        puppet = puppet.SumPowerUserPuppet();
        power = power + puppet.power;
        health = health + puppet.health;
        physical_attack = physical_attack + puppet.physical_attack;
        physical_defense = physical_defense + puppet.physical_defense;
        magical_attack = magical_attack + puppet.magical_attack;
        magical_defense = magical_defense + puppet.magical_defense;
        chemical_attack = chemical_attack + puppet.chemical_attack;
        chemical_defense = chemical_defense + puppet.chemical_defense;
        atomic_attack = atomic_attack + puppet.atomic_attack;
        atomic_defense = atomic_defense + puppet.atomic_defense;
        mental_attack = mental_attack + puppet.mental_attack;
        mental_defense = mental_defense + puppet.mental_defense;
        speed = speed + puppet.speed;
        critical_damage_rate = critical_damage_rate + puppet.critical_damage_rate;
        critical_rate = critical_rate + puppet.critical_rate;
        penetration_rate = penetration_rate + puppet.penetration_rate;
        evasion_rate = evasion_rate + puppet.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + puppet.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + puppet.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + puppet.accuracy_rate;
        lifesteal_rate = lifesteal_rate + puppet.lifesteal_rate;
        shield_strength = shield_strength + puppet.shield_strength;
        tenacity = tenacity + puppet.tenacity;
        resistance_rate = resistance_rate + puppet.resistance_rate;
        combo_rate = combo_rate + puppet.combo_rate;
        reflection_rate = reflection_rate + puppet.reflection_rate;
        mana = mana + puppet.mana;
        mana_regeneration_rate = mana_regeneration_rate + puppet.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + puppet.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + puppet.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + puppet.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + puppet.resistance_to_same_faction_rate;

        //Percent
        puppet = puppet.SumPowerPuppetPercent();
        percent_all_health = percent_all_health + puppet.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + puppet.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + puppet.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + puppet.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + puppet.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + puppet.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + puppet.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + puppet.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + puppet.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + puppet.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + puppet.percent_all_mental_defense;
    }
    public void GetAlchemyPower()
    {
        Alchemy alchemy = new Alchemy();
        //Gallery
        alchemy = alchemy.SumPowerAlchemyGallery();
        power = power + alchemy.power;
        health = health + alchemy.health;
        physical_attack = physical_attack + alchemy.physical_attack;
        physical_defense = physical_defense + alchemy.physical_defense;
        magical_attack = magical_attack + alchemy.magical_attack;
        magical_defense = magical_defense + alchemy.magical_defense;
        chemical_attack = chemical_attack + alchemy.chemical_attack;
        chemical_defense = chemical_defense + alchemy.chemical_defense;
        atomic_attack = atomic_attack + alchemy.atomic_attack;
        atomic_defense = atomic_defense + alchemy.atomic_defense;
        mental_attack = mental_attack + alchemy.mental_attack;
        mental_defense = mental_defense + alchemy.mental_defense;
        speed = speed + alchemy.speed;
        critical_damage_rate = critical_damage_rate + alchemy.critical_damage_rate;
        critical_rate = critical_rate + alchemy.critical_rate;
        penetration_rate = penetration_rate + alchemy.penetration_rate;
        evasion_rate = evasion_rate + alchemy.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + alchemy.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + alchemy.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + alchemy.accuracy_rate;
        lifesteal_rate = lifesteal_rate + alchemy.lifesteal_rate;
        shield_strength = shield_strength + alchemy.shield_strength;
        tenacity = tenacity + alchemy.tenacity;
        resistance_rate = resistance_rate + alchemy.resistance_rate;
        combo_rate = combo_rate + alchemy.combo_rate;
        reflection_rate = reflection_rate + alchemy.reflection_rate;
        mana = mana + alchemy.mana;
        mana_regeneration_rate = mana_regeneration_rate + alchemy.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + alchemy.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + alchemy.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + alchemy.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + alchemy.resistance_to_same_faction_rate;

        percent_all_health = percent_all_health + alchemy.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + alchemy.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + alchemy.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + alchemy.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + alchemy.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + alchemy.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + alchemy.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + alchemy.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + alchemy.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + alchemy.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + alchemy.percent_all_mental_defense;

        //User
        alchemy = alchemy.SumPowerUserAlchemy();
        power = power + alchemy.power;
        health = health + alchemy.health;
        physical_attack = physical_attack + alchemy.physical_attack;
        physical_defense = physical_defense + alchemy.physical_defense;
        magical_attack = magical_attack + alchemy.magical_attack;
        magical_defense = magical_defense + alchemy.magical_defense;
        chemical_attack = chemical_attack + alchemy.chemical_attack;
        chemical_defense = chemical_defense + alchemy.chemical_defense;
        atomic_attack = atomic_attack + alchemy.atomic_attack;
        atomic_defense = atomic_defense + alchemy.atomic_defense;
        mental_attack = mental_attack + alchemy.mental_attack;
        mental_defense = mental_defense + alchemy.mental_defense;
        speed = speed + alchemy.speed;
        critical_damage_rate = critical_damage_rate + alchemy.critical_damage_rate;
        critical_rate = critical_rate + alchemy.critical_rate;
        penetration_rate = penetration_rate + alchemy.penetration_rate;
        evasion_rate = evasion_rate + alchemy.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + alchemy.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + alchemy.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + alchemy.accuracy_rate;
        lifesteal_rate = lifesteal_rate + alchemy.lifesteal_rate;
        shield_strength = shield_strength + alchemy.shield_strength;
        tenacity = tenacity + alchemy.tenacity;
        resistance_rate = resistance_rate + alchemy.resistance_rate;
        combo_rate = combo_rate + alchemy.combo_rate;
        reflection_rate = reflection_rate + alchemy.reflection_rate;
        mana = mana + alchemy.mana;
        mana_regeneration_rate = mana_regeneration_rate + alchemy.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + alchemy.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + alchemy.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + alchemy.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + alchemy.resistance_to_same_faction_rate;

        //Percent
        alchemy = alchemy.SumPowerAlchemyPercent();
        percent_all_health = percent_all_health + alchemy.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + alchemy.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + alchemy.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + alchemy.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + alchemy.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + alchemy.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + alchemy.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + alchemy.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + alchemy.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + alchemy.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + alchemy.percent_all_mental_defense;
    }
    public void GetForgePower()
    {
        Forge forge = new Forge();
        //Gallery
        forge = forge.SumPowerForgeGallery();
        power = power + forge.power;
        health = health + forge.health;
        physical_attack = physical_attack + forge.physical_attack;
        physical_defense = physical_defense + forge.physical_defense;
        magical_attack = magical_attack + forge.magical_attack;
        magical_defense = magical_defense + forge.magical_defense;
        chemical_attack = chemical_attack + forge.chemical_attack;
        chemical_defense = chemical_defense + forge.chemical_defense;
        atomic_attack = atomic_attack + forge.atomic_attack;
        atomic_defense = atomic_defense + forge.atomic_defense;
        mental_attack = mental_attack + forge.mental_attack;
        mental_defense = mental_defense + forge.mental_defense;
        speed = speed + forge.speed;
        critical_damage_rate = critical_damage_rate + forge.critical_damage_rate;
        critical_rate = critical_rate + forge.critical_rate;
        penetration_rate = penetration_rate + forge.penetration_rate;
        evasion_rate = evasion_rate + forge.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + forge.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + forge.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + forge.accuracy_rate;
        lifesteal_rate = lifesteal_rate + forge.lifesteal_rate;
        shield_strength = shield_strength + forge.shield_strength;
        tenacity = tenacity + forge.tenacity;
        resistance_rate = resistance_rate + forge.resistance_rate;
        combo_rate = combo_rate + forge.combo_rate;
        reflection_rate = reflection_rate + forge.reflection_rate;
        mana = mana + forge.mana;
        mana_regeneration_rate = mana_regeneration_rate + forge.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + forge.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + forge.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + forge.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + forge.resistance_to_same_faction_rate;

        percent_all_health = percent_all_health + forge.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + forge.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + forge.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + forge.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + forge.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + forge.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + forge.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + forge.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + forge.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + forge.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + forge.percent_all_mental_defense;

        //User
        forge = forge.SumPowerUserForge();
        power = power + forge.power;
        health = health + forge.health;
        physical_attack = physical_attack + forge.physical_attack;
        physical_defense = physical_defense + forge.physical_defense;
        magical_attack = magical_attack + forge.magical_attack;
        magical_defense = magical_defense + forge.magical_defense;
        chemical_attack = chemical_attack + forge.chemical_attack;
        chemical_defense = chemical_defense + forge.chemical_defense;
        atomic_attack = atomic_attack + forge.atomic_attack;
        atomic_defense = atomic_defense + forge.atomic_defense;
        mental_attack = mental_attack + forge.mental_attack;
        mental_defense = mental_defense + forge.mental_defense;
        speed = speed + forge.speed;
        critical_damage_rate = critical_damage_rate + forge.critical_damage_rate;
        critical_rate = critical_rate + forge.critical_rate;
        penetration_rate = penetration_rate + forge.penetration_rate;
        evasion_rate = evasion_rate + forge.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + forge.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + forge.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + forge.accuracy_rate;
        lifesteal_rate = lifesteal_rate + forge.lifesteal_rate;
        shield_strength = shield_strength + forge.shield_strength;
        tenacity = tenacity + forge.tenacity;
        resistance_rate = resistance_rate + forge.resistance_rate;
        combo_rate = combo_rate + forge.combo_rate;
        reflection_rate = reflection_rate + forge.reflection_rate;
        mana = mana + forge.mana;
        mana_regeneration_rate = mana_regeneration_rate + forge.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + forge.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + forge.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + forge.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + forge.resistance_to_same_faction_rate;

        //Percent
        forge = forge.SumPowerForgePercent();
        percent_all_health = percent_all_health + forge.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + forge.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + forge.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + forge.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + forge.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + forge.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + forge.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + forge.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + forge.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + forge.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + forge.percent_all_mental_defense;
    }
    public void GetCardLifePower()
    {
        CardLife cardLife = new CardLife();
        //Gallery
        cardLife = cardLife.SumPowerCardLifeGallery();
        power = power + cardLife.power;
        health = health + cardLife.health;
        physical_attack = physical_attack + cardLife.physical_attack;
        physical_defense = physical_defense + cardLife.physical_defense;
        magical_attack = magical_attack + cardLife.magical_attack;
        magical_defense = magical_defense + cardLife.magical_defense;
        chemical_attack = chemical_attack + cardLife.chemical_attack;
        chemical_defense = chemical_defense + cardLife.chemical_defense;
        atomic_attack = atomic_attack + cardLife.atomic_attack;
        atomic_defense = atomic_defense + cardLife.atomic_defense;
        mental_attack = mental_attack + cardLife.mental_attack;
        mental_defense = mental_defense + cardLife.mental_defense;
        speed = speed + cardLife.speed;
        critical_damage_rate = critical_damage_rate + cardLife.critical_damage_rate;
        critical_rate = critical_rate + cardLife.critical_rate;
        penetration_rate = penetration_rate + cardLife.penetration_rate;
        evasion_rate = evasion_rate + cardLife.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + cardLife.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + cardLife.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + cardLife.accuracy_rate;
        lifesteal_rate = lifesteal_rate + cardLife.lifesteal_rate;
        shield_strength = shield_strength + cardLife.shield_strength;
        tenacity = tenacity + cardLife.tenacity;
        resistance_rate = resistance_rate + cardLife.resistance_rate;
        combo_rate = combo_rate + cardLife.combo_rate;
        reflection_rate = reflection_rate + cardLife.reflection_rate;
        mana = mana + cardLife.mana;
        mana_regeneration_rate = mana_regeneration_rate + cardLife.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + cardLife.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + cardLife.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + cardLife.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + cardLife.resistance_to_same_faction_rate;

        percent_all_health = percent_all_health + cardLife.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + cardLife.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + cardLife.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + cardLife.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + cardLife.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + cardLife.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + cardLife.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + cardLife.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + cardLife.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + cardLife.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + cardLife.percent_all_mental_defense;

        //User
        cardLife = cardLife.SumPowerUserCardLife();
        power = power + cardLife.power;
        health = health + cardLife.health;
        physical_attack = physical_attack + cardLife.physical_attack;
        physical_defense = physical_defense + cardLife.physical_defense;
        magical_attack = magical_attack + cardLife.magical_attack;
        magical_defense = magical_defense + cardLife.magical_defense;
        chemical_attack = chemical_attack + cardLife.chemical_attack;
        chemical_defense = chemical_defense + cardLife.chemical_defense;
        atomic_attack = atomic_attack + cardLife.atomic_attack;
        atomic_defense = atomic_defense + cardLife.atomic_defense;
        mental_attack = mental_attack + cardLife.mental_attack;
        mental_defense = mental_defense + cardLife.mental_defense;
        speed = speed + cardLife.speed;
        critical_damage_rate = critical_damage_rate + cardLife.critical_damage_rate;
        critical_rate = critical_rate + cardLife.critical_rate;
        penetration_rate = penetration_rate + cardLife.penetration_rate;
        evasion_rate = evasion_rate + cardLife.evasion_rate;
        damage_absorption_rate = damage_absorption_rate + cardLife.damage_absorption_rate;
        vitality_regeneration_rate = vitality_regeneration_rate + cardLife.vitality_regeneration_rate;
        accuracy_rate = accuracy_rate + cardLife.accuracy_rate;
        lifesteal_rate = lifesteal_rate + cardLife.lifesteal_rate;
        shield_strength = shield_strength + cardLife.shield_strength;
        tenacity = tenacity + cardLife.tenacity;
        resistance_rate = resistance_rate + cardLife.resistance_rate;
        combo_rate = combo_rate + cardLife.combo_rate;
        reflection_rate = reflection_rate + cardLife.reflection_rate;
        mana = mana + cardLife.mana;
        mana_regeneration_rate = mana_regeneration_rate + cardLife.mana_regeneration_rate;
        damage_to_different_faction_rate = damage_to_different_faction_rate + cardLife.damage_to_different_faction_rate;
        resistance_to_different_faction_rate = resistance_to_different_faction_rate + cardLife.resistance_to_different_faction_rate;
        damage_to_same_faction_rate = damage_to_same_faction_rate + cardLife.damage_to_same_faction_rate;
        resistance_to_same_faction_rate = resistance_to_same_faction_rate + cardLife.resistance_to_same_faction_rate;

        //Percent
        cardLife = cardLife.SumPowerCardLifePercent();
        percent_all_health = percent_all_health + cardLife.percent_all_health;
        percent_all_physical_attack = percent_all_physical_attack + cardLife.percent_all_physical_attack;
        percent_all_physical_defense = percent_all_physical_defense + cardLife.percent_all_physical_defense;
        percent_all_magical_attack = percent_all_magical_attack + cardLife.percent_all_magical_attack;
        percent_all_magical_defense = percent_all_magical_defense + cardLife.percent_all_magical_defense;
        percent_all_chemical_attack = percent_all_chemical_attack + cardLife.percent_all_chemical_attack;
        percent_all_chemical_defense = percent_all_chemical_defense + cardLife.percent_all_chemical_defense;
        percent_all_atomic_attack = percent_all_atomic_attack + cardLife.percent_all_atomic_attack;
        percent_all_atomic_defense = percent_all_atomic_defense + cardLife.percent_all_atomic_defense;
        percent_all_mental_attack = percent_all_mental_attack + cardLife.percent_all_mental_attack;
        percent_all_mental_defense = percent_all_mental_defense + cardLife.percent_all_mental_defense;
    }
}
