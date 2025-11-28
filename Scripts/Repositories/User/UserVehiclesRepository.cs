using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserVehicleRepository : IUserVehicleRepository
{
    public List<Vehicles> GetUserVehicle(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<Vehicles> Vehicles = new List<Vehicles>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select um.*, m.id, m.name, m.image, m.rare, m.description from Vehicles m, user_Vehicles um 
                where m.id=um.Vehicle_id and um.user_id=@userId and m.type=@type AND (@rare = 'All' or m.rare = @rare)
                ORDER BY m.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@rare", rare);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Vehicles Vehicle = new Vehicles
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Star = reader.GetInt32("star"),
                        Level = reader.GetInt32("level"),
                        Experiment = reader.GetDouble("experiment"),
                        Quantity = reader.GetDouble("quantity"),
                        Power = reader.GetDouble("power"),
                        Health = reader.GetDouble("health"),
                        PhysicalAttack = reader.GetDouble("physical_attack"),
                        PhysicalDefense = reader.GetDouble("physical_defense"),
                        MagicalAttack = reader.GetDouble("magical_attack"),
                        MagicalDefense = reader.GetDouble("magical_defense"),
                        ChemicalAttack = reader.GetDouble("chemical_attack"),
                        ChemicalDefense = reader.GetDouble("chemical_defense"),
                        AtomicAttack = reader.GetDouble("atomic_attack"),
                        AtomicDefense = reader.GetDouble("atomic_defense"),
                        MentalAttack = reader.GetDouble("mental_attack"),
                        MentalDefense = reader.GetDouble("mental_defense"),
                        Speed = reader.GetDouble("speed"),
                        CriticalDamageRate = reader.GetDouble("critical_damage_rate"),
                        CriticalRate = reader.GetDouble("critical_rate"),
                        CriticalResistanceRate = reader.GetDouble("critical_resistance_rate"),
                        IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate"),
                        PenetrationRate = reader.GetDouble("penetration_rate"),
                        PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate"),
                        EvasionRate = reader.GetDouble("evasion_rate"),
                        DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate"),
                        IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate"),
                        AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate"),
                        VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate"),
                        VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        AccuracyRate = reader.GetDouble("accuracy_rate"),
                        LifestealRate = reader.GetDouble("lifesteal_rate"),
                        ShieldStrength = reader.GetDouble("shield_strength"),
                        Tenacity = reader.GetDouble("tenacity"),
                        ResistanceRate = reader.GetDouble("resistance_rate"),
                        ComboRate = reader.GetDouble("combo_rate"),
                        IgnoreComboRate = reader.GetDouble("ignore_combo_rate"),
                        ComboDamageRate = reader.GetDouble("combo_damage_rate"),
                        ComboResistanceRate = reader.GetDouble("combo_resistance_rate"),
                        StunRate = reader.GetDouble("stun_rate"),
                        IgnoreStunRate = reader.GetDouble("ignore_stun_rate"),
                        ReflectionRate = reader.GetDouble("reflection_rate"),
                        IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate"),
                        ReflectionDamageRate = reader.GetDouble("reflection_damage_rate"),
                        ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate"),
                        Mana = reader.GetFloat("mana"),
                        ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                        DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                        ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                        DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                        ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                        NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                        NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                        SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                        SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                        // percent_all_health = reader.GetDouble("percent_all_health"),
                        // percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        // percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        // percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        // percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        // percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        // percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        // percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        // percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        // percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        // percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                        Description = reader.GetString("description")
                    };

                    Vehicles.Add(Vehicle);
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
        return Vehicles;
    }
    public int GetUserVehicleCount(string user_id, string type, string rare)
    {
        int count = 0;
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select count(*) from Vehicles m, user_Vehicles um 
                where m.id=um.Vehicle_id and um.user_id=@userId and m.type= @type AND (@rare = 'All' or m.rare = @rare)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@rare", rare);
                count = Convert.ToInt32(command.ExecuteScalar());

                return count;
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
        return count;
    }
    public bool InsertUserVehicle(Vehicles Vehicle, string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_Vehicles 
                WHERE user_id = @user_id AND Vehicle_id = @Vehicle_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@Vehicle_id", Vehicle.Id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                    INSERT INTO user_Vehicles (
                    user_id, Vehicle_id, rare, level, experiment, star, quality, block, quantity,
                    power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                    chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                    speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                    penetration_rate, penetration_resistance_rate,
                    evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                    vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                    accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                    combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                    stun_rate, ignore_stun_rate,
                    reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                    mana, mana_regeneration_rate,
                    damage_to_different_faction_rate, resistance_to_different_faction_rate,
                    damage_to_same_faction_rate, resistance_to_same_faction_rate,
                    normal_damage_rate, normal_resistance_rate,
                    skill_damage_rate, skill_resistance_rate
                ) VALUES (
                    @user_id, @Vehicle_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
                    @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                    @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                    @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                    @penetration_rate, @penetration_resistance_rate,
                    @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                    @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                    @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                    @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                    @stun_rate, @ignore_stun_rate,
                    @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                    @mana, @mana_regeneration_rate,
                    @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                    @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                    @normal_damage_rate, @normal_resistance_rate,
                    @skill_damage_rate, @skill_resistance_rate
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@Vehicle_id", Vehicle.Id);
                    command.Parameters.AddWithValue("@rare", Vehicle.Rare);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(Vehicle.Rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", Vehicle.Quantity);
                    command.Parameters.AddWithValue("@power", Vehicle.Power);
                    command.Parameters.AddWithValue("@health", Vehicle.Health);
                    command.Parameters.AddWithValue("@physical_attack", Vehicle.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", Vehicle.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", Vehicle.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", Vehicle.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", Vehicle.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", Vehicle.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", Vehicle.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", Vehicle.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", Vehicle.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", Vehicle.MentalDefense);
                    command.Parameters.AddWithValue("@speed", Vehicle.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", Vehicle.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", Vehicle.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", Vehicle.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", Vehicle.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", Vehicle.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", Vehicle.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", Vehicle.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", Vehicle.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", Vehicle.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", Vehicle.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", Vehicle.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", Vehicle.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", Vehicle.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", Vehicle.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", Vehicle.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", Vehicle.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", Vehicle.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", Vehicle.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", Vehicle.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", Vehicle.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", Vehicle.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", Vehicle.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", Vehicle.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", Vehicle.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", Vehicle.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", Vehicle.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", Vehicle.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", Vehicle.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", Vehicle.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", Vehicle.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", Vehicle.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", Vehicle.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", Vehicle.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", Vehicle.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", Vehicle.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", Vehicle.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", Vehicle.SkillResistanceRate);

                    // command.Parameters.AddWithValue("@percent_all_health", Vehicle.percent_all_health);
                    // command.Parameters.AddWithValue("@percent_all_physical_attack", Vehicle.percent_all_physical_attack);
                    // command.Parameters.AddWithValue("@percent_all_physical_defense", Vehicle.percent_all_physical_defense);
                    // command.Parameters.AddWithValue("@percent_all_magical_attack", Vehicle.percent_all_magical_attack);
                    // command.Parameters.AddWithValue("@percent_all_magical_defense", Vehicle.percent_all_magical_defense);
                    // command.Parameters.AddWithValue("@percent_all_chemical_attack", Vehicle.percent_all_chemical_attack);
                    // command.Parameters.AddWithValue("@percent_all_chemical_defense", Vehicle.percent_all_chemical_defense);
                    // command.Parameters.AddWithValue("@percent_all_atomic_attack", Vehicle.percent_all_atomic_attack);
                    // command.Parameters.AddWithValue("@percent_all_atomic_defense", Vehicle.percent_all_atomic_defense);
                    // command.Parameters.AddWithValue("@percent_all_mental_attack", Vehicle.percent_all_mental_attack);
                    // command.Parameters.AddWithValue("@percent_all_mental_defense", Vehicle.percent_all_mental_defense);
                    // command.Parameters.AddWithValue("@percent_all_speed", 20);
                    // command.Parameters.AddWithValue("@percent_all_critical_damage", 20);
                    // command.Parameters.AddWithValue("@percent_all_critical_rate", 20);
                    // command.Parameters.AddWithValue("@percent_all_armor_penetration", 20);
                    // command.Parameters.AddWithValue("@percent_all_avoid", 20);
                    // command.Parameters.AddWithValue("@percent_all_absorbs_damage", 20);
                    // command.Parameters.AddWithValue("@percent_all_regenerate_vitality", 20);
                    // command.Parameters.AddWithValue("@percent_all_accuracy", 20);
                    // command.Parameters.AddWithValue("@percent_all_mana", 20);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_Vehicles
                    SET quantity = @quantity
                    WHERE user_id = @user_id AND Vehicle_id = @Vehicle_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@Vehicle_id", Vehicle.Id);
                    updateCommand.Parameters.AddWithValue("@quantity", Vehicle.Quantity);

                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }

        }
        return true;
    }
    public bool UpdateVehicleLevel(Vehicles Vehicle, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_Vehicles
                SET 
                    level = @level, power = @power, health = @health, 
                    physical_attack = @physical_attack, physical_defense = @physical_defense, 
                    magical_attack = @magical_attack, magical_defense = @magical_defense, 
                    chemical_attack = @chemical_attack, chemical_defense = @chemical_defense, 
                    atomic_attack = @atomic_attack, atomic_defense = @atomic_defense, 
                    mental_attack = @mental_attack, mental_defense = @mental_defense, 
                    speed = @speed, critical_damage_rate = @critical_damage_rate, 
                    critical_rate = @critical_rate, critical_resistance_rate = @critical_resistance_rate, ignore_critical_rate = @ignore_critical_rate,
                    penetration_rate = @penetration_rate, penetration_resistance_rate = @penetration_resistance_rate,
                    evasion_rate = @evasion_rate, damage_absorption_rate = @damage_absorption_rate, 
                    ignore_damage_absorption_rate = @ignore_damage_absorption_rate, absorbed_damage_rate = @absorbed_damage_rate,
                    vitality_regeneration_rate = @vitality_regeneration_rate, vitality_regeneration_resistance_rate = @vitality_regeneration_resistance_rate, 
                    accuracy_rate = @accuracy_rate, lifesteal_rate = @lifesteal_rate, shield_strength = @shield_strength, 
                    tenacity = @tenacity, resistance_rate = @resistance_rate, 
                    combo_rate = @comboRate, ignore_combo_rate = @ignore_combo_rate, combo_damage_rate = @combo_damage_rate, combo_resistance_rate = @combo_resistance_rate,
                    stun_rate = @stun_rate, ignore_stun_rate = @ignore_stun_rate,
                    reflection_rate = @reflection_rate, ignore_reflection_rate = @ignore_reflection_rate, 
                    reflection_damage_rate = @reflection_damage_rate, reflection_resistance_rate = @reflection_resistance_rate,
                    mana = @mana, mana_regeneration_rate = @mana_regeneration_rate, 
                    damage_to_different_faction_rate = @damage_to_different_faction_rate, 
                    resistance_to_different_faction_rate = @resistance_to_different_faction_rate, 
                    damage_to_same_faction_rate = @damage_to_same_faction_rate, 
                    resistance_to_same_faction_rate = @resistance_to_same_faction_rate,
                    normal_damage_rate = @normal_damage_rate, normal_resistance_rate = @normal_resistance_rate,
                    skill_damage_rate = @skill_damage_rate, skill_resistance_rate = @skill_resistance_rate
                WHERE user_id = @user_id AND Vehicle_id = @Vehicle_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@Vehicle_id", Vehicle.Id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", Vehicle.Power);
                command.Parameters.AddWithValue("@health", Vehicle.Health);
                command.Parameters.AddWithValue("@physical_attack", Vehicle.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", Vehicle.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", Vehicle.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", Vehicle.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", Vehicle.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", Vehicle.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", Vehicle.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", Vehicle.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", Vehicle.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", Vehicle.MentalDefense);
                command.Parameters.AddWithValue("@speed", Vehicle.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", Vehicle.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", Vehicle.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", Vehicle.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", Vehicle.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", Vehicle.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", Vehicle.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", Vehicle.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", Vehicle.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", Vehicle.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", Vehicle.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", Vehicle.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", Vehicle.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", Vehicle.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", Vehicle.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", Vehicle.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", Vehicle.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", Vehicle.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", Vehicle.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", Vehicle.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", Vehicle.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", Vehicle.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", Vehicle.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", Vehicle.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", Vehicle.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", Vehicle.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", Vehicle.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", Vehicle.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", Vehicle.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", Vehicle.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", Vehicle.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", Vehicle.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", Vehicle.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", Vehicle.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", Vehicle.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", Vehicle.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", Vehicle.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", Vehicle.SkillResistanceRate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        return true;
    }
    public bool UpdateVehicleBreakthrough(Vehicles Vehicle, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_Vehicles
                SET 
                    star = @star, quantity = @quantity, power=@power, health = @health, 
                    physical_attack = @physical_attack, physical_defense = @physical_defense, 
                    magical_attack = @magical_attack, magical_defense = @magical_defense, 
                    chemical_attack = @chemical_attack, chemical_defense = @chemical_defense, 
                    atomic_attack = @atomic_attack, atomic_defense = @atomic_defense, 
                    mental_attack = @mental_attack, mental_defense = @mental_defense, 
                    speed = @speed, critical_damage_rate = @critical_damage_rate, 
                    critical_rate = @critical_rate, critical_resistance_rate = @critical_resistance_rate, ignore_critical_rate = @ignore_critical_rate,
                    penetration_rate = @penetration_rate, penetration_resistance_rate = @penetration_resistance_rate,
                    evasion_rate = @evasion_rate, damage_absorption_rate = @damage_absorption_rate, 
                    ignore_damage_absorption_rate = @ignore_damage_absorption_rate, absorbed_damage_rate = @absorbed_damage_rate,
                    vitality_regeneration_rate = @vitality_regeneration_rate, vitality_regeneration_resistance_rate = @vitality_regeneration_resistance_rate, 
                    accuracy_rate = @accuracy_rate, lifesteal_rate = @lifesteal_rate, shield_strength = @shield_strength, 
                    tenacity = @tenacity, resistance_rate = @resistance_rate, 
                    combo_rate = @comboRate, ignore_combo_rate = @ignore_combo_rate, combo_damage_rate = @combo_damage_rate, combo_resistance_rate = @combo_resistance_rate,
                    stun_rate = @stun_rate, ignore_stun_rate = @ignore_stun_rate,
                    reflection_rate = @reflection_rate, ignore_reflection_rate = @ignore_reflection_rate, 
                    reflection_damage_rate = @reflection_damage_rate, reflection_resistance_rate = @reflection_resistance_rate,
                    mana = @mana, mana_regeneration_rate = @mana_regeneration_rate, 
                    damage_to_different_faction_rate = @damage_to_different_faction_rate, 
                    resistance_to_different_faction_rate = @resistance_to_different_faction_rate, 
                    damage_to_same_faction_rate = @damage_to_same_faction_rate, 
                    resistance_to_same_faction_rate = @resistance_to_same_faction_rate,
                    normal_damage_rate = @normal_damage_rate, normal_resistance_rate = @normal_resistance_rate,
                    skill_damage_rate = @skill_damage_rate, skill_resistance_rate = @skill_resistance_rate
                WHERE user_id = @user_id AND Vehicle_id = @Vehicle_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@Vehicle_id", Vehicle.Id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", Vehicle.Power);
                command.Parameters.AddWithValue("@health", Vehicle.Health);
                command.Parameters.AddWithValue("@physical_attack", Vehicle.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", Vehicle.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", Vehicle.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", Vehicle.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", Vehicle.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", Vehicle.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", Vehicle.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", Vehicle.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", Vehicle.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", Vehicle.MentalDefense);
                command.Parameters.AddWithValue("@speed", Vehicle.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", Vehicle.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", Vehicle.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", Vehicle.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", Vehicle.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", Vehicle.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", Vehicle.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", Vehicle.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", Vehicle.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", Vehicle.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", Vehicle.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", Vehicle.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", Vehicle.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", Vehicle.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", Vehicle.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", Vehicle.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", Vehicle.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", Vehicle.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", Vehicle.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", Vehicle.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", Vehicle.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", Vehicle.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", Vehicle.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", Vehicle.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", Vehicle.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", Vehicle.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", Vehicle.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", Vehicle.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", Vehicle.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", Vehicle.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", Vehicle.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", Vehicle.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", Vehicle.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", Vehicle.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", Vehicle.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", Vehicle.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", Vehicle.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", Vehicle.SkillResistanceRate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        return true;
    }
    public Vehicles GetUserVehicleById(string user_id, string Id)
    {
        Vehicles card = new Vehicles();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_Vehicles where Vehicle_id=@id 
                and user_Vehicles.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new Vehicles
                    {
                        Id = reader.GetString("Vehicle_id"),
                        Level = reader.GetInt32("level"),
                        Quality = reader.GetInt32("quality"),
                        Experiment = reader.GetDouble("experiment"),
                        Star = reader.GetInt32("star"),
                        Power = reader.GetDouble("power"),
                        Health = reader.GetDouble("health"),
                        PhysicalAttack = reader.GetDouble("physical_attack"),
                        PhysicalDefense = reader.GetDouble("physical_defense"),
                        MagicalAttack = reader.GetDouble("magical_attack"),
                        MagicalDefense = reader.GetDouble("magical_defense"),
                        ChemicalAttack = reader.GetDouble("chemical_attack"),
                        ChemicalDefense = reader.GetDouble("chemical_defense"),
                        AtomicAttack = reader.GetDouble("atomic_attack"),
                        AtomicDefense = reader.GetDouble("atomic_defense"),
                        MentalAttack = reader.GetDouble("mental_attack"),
                        MentalDefense = reader.GetDouble("mental_defense"),
                        Speed = reader.GetDouble("speed"),
                        CriticalDamageRate = reader.GetDouble("critical_damage_rate"),
                        CriticalRate = reader.GetDouble("critical_rate"),
                        CriticalResistanceRate = reader.GetDouble("critical_resistance_rate"),
                        IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate"),
                        PenetrationRate = reader.GetDouble("penetration_rate"),
                        PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate"),
                        EvasionRate = reader.GetDouble("evasion_rate"),
                        DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate"),
                        IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate"),
                        AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate"),
                        VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate"),
                        VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        AccuracyRate = reader.GetDouble("accuracy_rate"),
                        LifestealRate = reader.GetDouble("lifesteal_rate"),
                        ShieldStrength = reader.GetDouble("shield_strength"),
                        Tenacity = reader.GetDouble("tenacity"),
                        ResistanceRate = reader.GetDouble("resistance_rate"),
                        ComboRate = reader.GetDouble("combo_rate"),
                        IgnoreComboRate = reader.GetDouble("ignore_combo_rate"),
                        ComboDamageRate = reader.GetDouble("combo_damage_rate"),
                        ComboResistanceRate = reader.GetDouble("combo_resistance_rate"),
                        StunRate = reader.GetDouble("stun_rate"),
                        IgnoreStunRate = reader.GetDouble("ignore_stun_rate"),
                        ReflectionRate = reader.GetDouble("reflection_rate"),
                        IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate"),
                        ReflectionDamageRate = reader.GetDouble("reflection_damage_rate"),
                        ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate"),
                        Mana = reader.GetFloat("mana"),
                        ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                        DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                        ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                        DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                        ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                        NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                        NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                        SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                        SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                    };
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
        return card;
    }
    public Vehicles SumPowerUserVehicle()
    {
        Vehicles sumVehicle = new Vehicles();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                SUM(power * (1 + quality / 10.0)) AS total_power,
                SUM(health * (1 + quality / 10.0)) AS total_health,
                SUM(mana * (1 + quality / 10.0)) AS total_mana,
                SUM(physical_attack * (1 + quality / 10.0)) AS total_physical_attack,
                SUM(physical_defense * (1 + quality / 10.0)) AS total_physical_defense,
                SUM(magical_attack * (1 + quality / 10.0)) AS total_magical_attack,
                SUM(magical_defense * (1 + quality / 10.0)) AS total_magical_defense,
                SUM(chemical_attack * (1 + quality / 10.0)) AS total_chemical_attack,
                SUM(chemical_defense * (1 + quality / 10.0)) AS total_chemical_defense,
                SUM(atomic_attack * (1 + quality / 10.0)) AS total_atomic_attack,
                SUM(atomic_defense * (1 + quality / 10.0)) AS total_atomic_defense,
                SUM(mental_attack * (1 + quality / 10.0)) AS total_mental_attack,
                SUM(mental_defense * (1 + quality / 10.0)) AS total_mental_defense,
                SUM(speed * (1 + quality / 10.0)) AS total_speed,
                SUM(critical_damage_rate * (1 + quality / 10.0)) AS total_critical_damage_rate,
                SUM(critical_rate * (1 + quality / 10.0)) AS total_critical_rate,
                SUM(critical_resistance_rate * (1 + quality / 10.0)) AS total_critical_resistance_rate,
                SUM(ignore_critical_rate * (1 + quality / 10.0)) AS total_ignore_critical_rate,
                SUM(penetration_rate * (1 + quality / 10.0)) AS total_penetration_rate,
                SUM(penetration_resistance_rate * (1 + quality / 10.0)) AS total_penetration_resistance_rate,
                SUM(evasion_rate * (1 + quality / 10.0)) AS total_evasion_rate,
                SUM(damage_absorption_rate * (1 + quality / 10.0)) AS total_damage_absorption_rate,
                SUM(ignore_damage_absorption_rate * (1 + quality / 10.0)) AS total_ignore_damage_absorption_rate,
                SUM(absorbed_damage_rate * (1 + quality / 10.0)) AS total_absorbed_damage_rate,
                SUM(vitality_regeneration_rate * (1 + quality / 10.0)) AS total_vitality_regeneration_rate,
                SUM(vitality_regeneration_resistance_rate * (1 + quality / 10.0)) AS total_vitality_regeneration_resistance_rate,
                SUM(accuracy_rate * (1 + quality / 10.0)) AS total_accuracy_rate,
                SUM(lifesteal_rate * (1 + quality / 10.0)) AS total_lifesteal_rate,
                SUM(shield_strength * (1 + quality / 10.0)) AS total_shield_strength,
                SUM(tenacity * (1 + quality / 10.0)) AS total_tenacity,
                SUM(resistance_rate * (1 + quality / 10.0)) AS total_resistance_rate,
                SUM(combo_rate * (1 + quality / 10.0)) AS total_combo_rate,
                SUM(ignore_combo_rate * (1 + quality / 10.0)) AS total_ignore_combo_rate,
                SUM(combo_damage_rate * (1 + quality / 10.0)) AS total_combo_damage_rate,
                SUM(combo_resistance_rate * (1 + quality / 10.0)) AS total_combo_resistance_rate,
                SUM(stun_rate * (1 + quality / 10.0)) AS total_stun_rate,
                SUM(ignore_stun_rate * (1 + quality / 10.0)) AS total_ignore_stun_rate,
                SUM(reflection_rate * (1 + quality / 10.0)) AS total_reflection_rate,
                SUM(ignore_reflection_rate * (1 + quality / 10.0)) AS total_ignore_reflection_rate,
                SUM(reflection_damage_rate * (1 + quality / 10.0)) AS total_reflection_damage_rate,
                SUM(reflection_resistance_rate * (1 + quality / 10.0)) AS total_reflection_resistance_rate,
                SUM(mana_regeneration_rate * (1 + quality / 10.0)) AS total_mana_regeneration_rate,
                SUM(damage_to_different_faction_rate * (1 + quality / 10.0)) AS total_damage_to_different_faction_rate,
                SUM(resistance_to_different_faction_rate * (1 + quality / 10.0)) AS total_resistance_to_different_faction_rate,
                SUM(damage_to_same_faction_rate * (1 + quality / 10.0)) AS total_damage_to_same_faction_rate,
                SUM(resistance_to_same_faction_rate * (1 + quality / 10.0)) AS total_resistance_to_same_faction_rate,
                SUM(normal_damage_rate * (1 + quality / 10.0)) AS total_normal_damage_rate,
                SUM(normal_resistance_rate * (1 + quality / 10.0)) AS total_normal_resistance_rate,
                SUM(skill_damage_rate * (1 + quality / 10.0)) AS total_skill_damage_rate,
                SUM(skill_resistance_rate * (1 + quality / 10.0)) AS total_skill_resistance_rate
            FROM user_Vehicles
            WHERE user_id = @user_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumVehicle.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumVehicle.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumVehicle.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumVehicle.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumVehicle.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumVehicle.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumVehicle.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumVehicle.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumVehicle.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumVehicle.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumVehicle.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumVehicle.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumVehicle.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumVehicle.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                        sumVehicle.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumVehicle.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                        sumVehicle.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                        sumVehicle.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                        sumVehicle.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                        sumVehicle.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                        sumVehicle.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                        sumVehicle.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                        sumVehicle.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                        sumVehicle.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                        sumVehicle.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                        sumVehicle.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                        sumVehicle.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                        sumVehicle.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                        sumVehicle.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                        sumVehicle.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                        sumVehicle.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                        sumVehicle.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                        sumVehicle.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                        sumVehicle.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                        sumVehicle.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                        sumVehicle.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                        sumVehicle.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                        sumVehicle.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                        sumVehicle.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                        sumVehicle.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                        sumVehicle.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                        sumVehicle.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                        sumVehicle.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                        sumVehicle.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                        sumVehicle.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                        sumVehicle.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                        sumVehicle.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                        sumVehicle.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                        sumVehicle.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                        sumVehicle.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
                    }
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
        return sumVehicle;
    }
}