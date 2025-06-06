using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserCardMilitaryRepository : IUserCardMilitaryRepository
{
    public List<CardMilitary> GetUserCardMilitary(string user_id, string type, int pageSize, int offset)
    {
        List<CardMilitary> CardMilitaryList = new List<CardMilitary>();
        // string user_id=User.CurrentUserId;
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
                        id = reader.GetString("card_military_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        quality = reader.GetInt32("quality"),
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
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardMilitaryList;
    }
    public List<CardMilitary> GetUserCardMilitaryTeam(string user_id, string teamId, string position)
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
                WHERE um.user_id = @userId AND fcm.team_id = @team_id and SUBSTRING_INDEX(fcm.position, '-', 1) = @position
                ORDER BY m.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@team_id", teamId);
                command.Parameters.AddWithValue("@position", position);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardMilitary CardMilitary = new CardMilitary
                    {
                        id = reader.GetString("card_military_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        quality = reader.GetInt32("quality"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
                        block = reader.GetBoolean("block"),
                        team_id = reader.IsDBNull(reader.GetOrdinal("team_id")) ? -1 : reader.GetInt32("team_id"),
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString("position"),
                        // power = reader.GetDouble("power"),
                        // health = reader.GetDouble("health"),
                        // physical_attack = reader.GetDouble("physical_attack"),
                        // physical_defense = reader.GetDouble("physical_defense"),
                        // magical_attack = reader.GetDouble("magical_attack"),
                        // magical_defense = reader.GetDouble("magical_defense"),
                        // chemical_attack = reader.GetDouble("chemical_attack"),
                        // chemical_defense = reader.GetDouble("chemical_defense"),
                        // atomic_attack = reader.GetDouble("atomic_attack"),
                        // atomic_defense = reader.GetDouble("atomic_defense"),
                        // mental_attack = reader.GetDouble("mental_attack"),
                        // mental_defense = reader.GetDouble("mental_defense"),
                        // speed = reader.GetDouble("speed"),
                        // critical_damage_rate = reader.GetDouble("critical_damage_rate"),
                        // critical_rate = reader.GetDouble("critical_rate"),
                        // penetration_rate = reader.GetDouble("penetration_rate"),
                        // evasion_rate = reader.GetDouble("evasion_rate"),
                        // damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        // vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        // accuracy_rate = reader.GetDouble("accuracy_rate"),
                        // lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        // shield_strength = reader.GetDouble("shield_strength"),
                        // tenacity = reader.GetDouble("tenacity"),
                        // resistance_rate = reader.GetDouble("resistance_rate"),
                        // combo_rate = reader.GetDouble("combo_rate"),
                        // reflection_rate = reader.GetDouble("reflection_rate"),
                        // mana = reader.GetFloat("mana"),
                        // mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        // damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        // resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        // damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        // resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
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
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardMilitaryList;
    }
    public Dictionary<string, int> GetUniqueCardMilitaryTypesTeam(string teamId)
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
    public bool UpdateTeamFactCardMilitary(string team_id, string position, string card_id)
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
    public int GetUserCardMilitaryCount(string user_id, string type)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
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
    public int GetUserCardMilitaryTeamsPositionCount(string user_id, string team_id, string position)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*) from fact_card_military
                where team_id = @team_id and SUBSTRING_INDEX(position, '-', 1) = @position and user_id=@userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@position", position);
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
                    user_id, card_military_id, level, experiment, star, quality, block, quantity, power, health, physical_attack, 
                    physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, 
                    atomic_defense, mental_attack, mental_defense, speed, critical_damage_rate, critical_rate, 
                    penetration_rate, evasion_rate, damage_absorption_rate, vitality_regeneration_rate, accuracy_rate, 
                    lifesteal_rate, shield_strength, tenacity, resistance_rate, combo_rate, reflection_rate, 
                    mana, mana_regeneration_rate, damage_to_different_faction_rate, 
                    resistance_to_different_faction_rate, damage_to_same_faction_rate, resistance_to_same_faction_rate
                ) VALUES (
                    @user_id, @card_military_id, @level, @experiment, @star, @quality, @block, @quantity, @power, @health, @physical_attack, 
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
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(CardMilitary.rare));
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
                    InsertFactCardMilitary(CardMilitary);
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
    public CardMilitary GetUserCardMilitaryById(string user_id, string Id)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new CardMilitary
                    {
                        id = reader.GetString("card_military_id"),
                        level = reader.GetInt32("level"),
                        quality = reader.GetInt32("quality"),
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
    public List<CardMilitary> GetAllUserCardMilitaryInTeam(string user_id)
    {
        List<CardMilitary> cardMilitaries = new List<CardMilitary>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = @"SELECT uc.*, c.*, fch.*
                FROM user_card_military uc
                LEFT JOIN card_military c ON uc.card_military_id = c.id 
                LEFT JOIN fact_card_military fch ON fch.user_id = uc.user_id AND fch.user_card_military_id = uc.card_military_id
                WHERE uc.user_id = @user_id and fch.team_id IS NOT null";
            MySqlCommand command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardMilitary military = new CardMilitary
                {
                    id = reader.GetString("card_military_id"),
                    name = reader.GetString("name"),
                    image = reader.GetString("image"),
                    rare = reader.GetString("rare"),
                    quality = reader.GetInt32("quality"),
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

                cardMilitaries.Add(military);
            }
        }
        return cardMilitaries;
    }
}