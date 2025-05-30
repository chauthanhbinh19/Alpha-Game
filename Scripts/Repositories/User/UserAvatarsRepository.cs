using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserAvatarsRepository : IUserAvatarsRepository
{
    public List<Avatars> GetUserAvatars(string user_id, int pageSize, int offset)
    {
        List<Avatars> avatars = new List<Avatars>();
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select um.*, m.name, m.image, m.rare, m.description from avatars m, user_avatars um where m.id=um.avatar_id and um.user_id=@userId 
                ORDER BY m.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Avatars avatar = new Avatars
                    {
                        id = reader.GetString("avatar_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        quality = reader.GetInt32("quality"),
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
                        description = reader.GetString("description")
                    };

                    avatars.Add(avatar);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return avatars;
    }
    public int GetUserMedalsCount(string user_id)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from Medals m, user_medals um where m.id=um.medal_id and um.user_id=@userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
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
    public bool InsertUserAvatars(Avatars avatars)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_avatars
                WHERE user_id = @user_id AND avatar_id = @avatar_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@avatar_id", avatars.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_avatars (
                    user_id, avatar_id, level, experiment, star, quality, block, is_used, quantity, power, health, physical_attack, 
                    physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, 
                    atomic_defense, mental_attack, mental_defense, speed, critical_damage_rate, critical_rate, 
                    penetration_rate, evasion_rate, damage_absorption_rate, vitality_regeneration_rate, accuracy_rate, 
                    lifesteal_rate, shield_strength, tenacity, resistance_rate, combo_rate, reflection_rate, 
                    mana, mana_regeneration_rate, damage_to_different_faction_rate, 
                    resistance_to_different_faction_rate, damage_to_same_faction_rate, resistance_to_same_faction_rate
                ) VALUES (
                    @user_id, @avatar_id, @level, @experiment, @star, @quality, @block, @is_used, @quantity, @power, @health, @physical_attack, 
                    @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, 
                    @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage_rate, @critical_rate, 
                    @penetration_rate, @evasion_rate, @damage_absorption_rate, @vitality_regeneration_rate, @accuracy_rate, 
                    @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate, @combo_rate, @reflection_rate, 
                    @mana, @mana_regeneration_rate, @damage_to_different_faction_rate, 
                    @resistance_to_different_faction_rate, @damage_to_same_faction_rate, @resistance_to_same_faction_rate
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@avatar_id", avatars.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(avatars.rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@is_used", false);
                    command.Parameters.AddWithValue("@quantity", 0);
                    command.Parameters.AddWithValue("@power", avatars.power);
                    command.Parameters.AddWithValue("@health", avatars.health);
                    command.Parameters.AddWithValue("@physical_attack", avatars.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", avatars.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", avatars.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", avatars.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", avatars.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", avatars.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", avatars.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", avatars.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", avatars.mental_attack);
                    command.Parameters.AddWithValue("@mental_defense", avatars.mental_defense);
                    command.Parameters.AddWithValue("@speed", avatars.speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", avatars.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", avatars.critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", avatars.penetration_rate);
                    command.Parameters.AddWithValue("@evasion_rate", avatars.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", avatars.damage_absorption_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", avatars.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", avatars.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", avatars.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", avatars.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", avatars.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", avatars.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", avatars.combo_rate);
                    command.Parameters.AddWithValue("@reflection_rate", avatars.reflection_rate);
                    command.Parameters.AddWithValue("@mana", avatars.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", avatars.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", avatars.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", avatars.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", avatars.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", avatars.resistance_to_same_faction_rate);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_avatars
                    SET quantity = quantity + 1
                    WHERE user_id = @user_id AND avatar_id = @avatar_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@avatar_id", avatars.id);

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
    public bool InsertUserAvatarsById(string Id, Avatars Avatars)
    {
        // Avatars Avatars = new Avatars();
        // Avatars = GetAvatarsById(Id);
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_Avatars 
                WHERE user_id = @user_id AND avatar_id = @avatar_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@avatar_id", Avatars.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_avatars (
                    user_id, avatar_id, level, experiment, star, quality, block, is_used, quantity, power, health, physical_attack, 
                    physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, 
                    atomic_defense, mental_attack, mental_defense, speed, critical_damage_rate, critical_rate, 
                    penetration_rate, evasion_rate, damage_absorption_rate, vitality_regeneration_rate, accuracy_rate, 
                    lifesteal_rate, shield_strength, tenacity, resistance_rate, combo_rate, reflection_rate, 
                    mana, mana_regeneration_rate, damage_to_different_faction_rate, 
                    resistance_to_different_faction_rate, damage_to_same_faction_rate, resistance_to_same_faction_rate
                ) VALUES (
                    @user_id, @avatar_id, @level, @experiment, @star, @quality, @block, @is_used, @quantity, @power, @health, @physical_attack, 
                    @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, 
                    @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage_rate, @critical_rate, 
                    @penetration_rate, @evasion_rate, @damage_absorption_rate, @vitality_regeneration_rate, @accuracy_rate, 
                    @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate, @combo_rate, @reflection_rate, 
                    @mana, @mana_regeneration_rate, @damage_to_different_faction_rate, 
                    @resistance_to_different_faction_rate, @damage_to_same_faction_rate, @resistance_to_same_faction_rate
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@avatar_id", Avatars.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(Avatars.rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@is_used", false);
                    command.Parameters.AddWithValue("@quantity", 1);
                    command.Parameters.AddWithValue("@power", Avatars.power);
                    command.Parameters.AddWithValue("@health", Avatars.health);
                    command.Parameters.AddWithValue("@physical_attack", Avatars.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", Avatars.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", Avatars.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", Avatars.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", Avatars.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", Avatars.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", Avatars.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", Avatars.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", Avatars.mental_attack);
                    command.Parameters.AddWithValue("@mental_defense", Avatars.mental_defense);
                    command.Parameters.AddWithValue("@speed", Avatars.speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", Avatars.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", Avatars.critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", Avatars.penetration_rate);
                    command.Parameters.AddWithValue("@evasion_rate", Avatars.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", Avatars.damage_absorption_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", Avatars.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", Avatars.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", Avatars.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", Avatars.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", Avatars.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", Avatars.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", Avatars.combo_rate);
                    command.Parameters.AddWithValue("@reflection_rate", Avatars.reflection_rate);
                    command.Parameters.AddWithValue("@mana", Avatars.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", Avatars.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", Avatars.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", Avatars.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", Avatars.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", Avatars.resistance_to_same_faction_rate);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_avatars
                    SET quantity = quantity + 1
                    WHERE user_id = @user_id AND avatar_id = @avatar_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@avatar_id", Avatars.id);

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
    public Avatars GetAvatarsByUsed(string user_id)
    {
        Avatars Avatars = new Avatars();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select ub.*, b.image, b.rare from user_avatars ub, avatars b 
                where ub.avatar_id=b.id and ub.is_used=true and ub.user_id = @user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Avatars = new Avatars
                    {
                        id = reader.GetString("avatar_id"),
                        // name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        quality = reader.GetInt32("quality"),
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
                        // description = reader.GetString("description")
                    };
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return Avatars;
    }
    public void UpdateIsUsedAvatars(string Id, bool is_used)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "update user_avatars set is_used=@is_used where user_id=@user_id and avatar_id=@avatar_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@avatar_id", Id);
                command.Parameters.AddWithValue("@is_used", is_used);
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
    public Avatars SumPowerUserAvatars()
    {
        Avatars sumAvatars = new Avatars();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                SUM(power * (1 + quality / 10.0)) AS total_power, SUM(health * (1 + quality / 10.0)) AS total_health, SUM(mana * (1 + quality / 10.0)) AS total_mana, 
                SUM(physical_attack * (1 + quality / 10.0)) AS total_physical_attack, SUM(physical_defense * (1 + quality / 10.0)) AS total_physical_defense, 
                SUM(magical_attack * (1 + quality / 10.0)) AS total_magical_attack, SUM(magical_defense * (1 + quality / 10.0)) AS total_magical_defense, 
                SUM(chemical_attack * (1 + quality / 10.0)) AS total_chemical_attack, SUM(chemical_defense * (1 + quality / 10.0)) AS total_chemical_defense, 
                SUM(atomic_attack * (1 + quality / 10.0)) AS total_atomic_attack, SUM(atomic_defense * (1 + quality / 10.0)) AS total_atomic_defense, 
                SUM(mental_attack * (1 + quality / 10.0)) AS total_mental_attack, SUM(mental_defense * (1 + quality / 10.0)) AS total_mental_defense, 
                SUM(speed * (1 + quality / 10.0)) AS total_speed, SUM(critical_damage_rate * (1 + quality / 10.0)) AS total_critical_damage_rate, 
                SUM(critical_rate * (1 + quality / 10.0)) AS total_critical_rate, SUM(penetration_rate * (1 + quality / 10.0)) AS total_penetration_rate, 
                SUM(evasion_rate * (1 + quality / 10.0)) AS total_evasion_rate, SUM(damage_absorption_rate * (1 + quality / 10.0)) AS total_damage_absorption_rate, 
                SUM(vitality_regeneration_rate * (1 + quality / 10.0)) AS total_vitality_regeneration_rate, SUM(accuracy_rate * (1 + quality / 10.0)) AS total_accuracy_rate, 
                SUM(lifesteal_rate * (1 + quality / 10.0)) AS total_lifesteal_rate, SUM(shield_strength * (1 + quality / 10.0)) AS total_shield_strength, 
                SUM(tenacity * (1 + quality / 10.0)) AS total_tenacity, SUM(resistance_rate * (1 + quality / 10.0)) AS total_resistance_rate, 
                SUM(combo_rate * (1 + quality / 10.0)) AS total_combo_rate, SUM(reflection_rate * (1 + quality / 10.0)) AS total_reflection_rate, 
                SUM(mana_regeneration_rate * (1 + quality / 10.0)) AS total_mana_regeneration_rate, 
                SUM(damage_to_different_faction_rate * (1 + quality / 10.0)) AS total_damage_to_different_faction_rate, 
                SUM(resistance_to_different_faction_rate * (1 + quality / 10.0)) AS total_resistance_to_different_faction_rate, 
                SUM(damage_to_same_faction_rate * (1 + quality / 10.0)) AS total_damage_to_same_faction_rate, 
                SUM(resistance_to_same_faction_rate * (1 + quality / 10.0)) AS total_resistance_to_same_faction_rate
            FROM user_avatars
            WHERE user_id = @user_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumAvatars.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumAvatars.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumAvatars.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumAvatars.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumAvatars.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumAvatars.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumAvatars.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumAvatars.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumAvatars.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumAvatars.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumAvatars.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumAvatars.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumAvatars.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumAvatars.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                        sumAvatars.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumAvatars.penetration_rate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                        sumAvatars.evasion_rate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                        sumAvatars.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                        sumAvatars.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                        sumAvatars.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                        sumAvatars.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                        sumAvatars.shield_strength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                        sumAvatars.tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                        sumAvatars.resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                        sumAvatars.combo_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                        sumAvatars.reflection_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                        sumAvatars.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                        sumAvatars.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                        sumAvatars.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                        sumAvatars.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                        sumAvatars.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                        sumAvatars.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                    }
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return sumAvatars;
    }
}