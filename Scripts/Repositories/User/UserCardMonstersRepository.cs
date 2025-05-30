using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserCardMonstersRepository : IUserCardMonstersRepository
{
    public List<CardMonsters> GetUserCardMonsters(string user_id, string type, int pageSize, int offset)
    {
        List<CardMonsters> CardMonstersList = new List<CardMonsters>();
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT um.*, m.*, fcm.*
                FROM user_card_monsters um
                LEFT JOIN card_monsters m ON um.card_monster_id = m.id
                LEFT JOIN fact_card_monsters fcm ON fcm.user_id = um.user_id AND fcm.user_card_monster_id = um.card_monster_id
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
                    CardMonsters CardMonsters = new CardMonsters
                    {
                        id = reader.GetString("card_monster_id"),
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

                    CardMonstersList.Add(CardMonsters);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardMonstersList;
    }
    public List<CardMonsters> GetUserCardMonstersTeam(string user_id, string teamId, string position)
    {
        List<CardMonsters> CardMonstersList = new List<CardMonsters>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT um.*, m.*, fcm.*
                FROM user_card_monsters um
                LEFT JOIN card_monsters m ON um.card_monster_id = m.id
                LEFT JOIN fact_card_monsters fcm ON fcm.user_id = um.user_id AND fcm.user_card_monster_id = um.card_monster_id
                WHERE um.user_id = @userId and fcm.team_id=@team_id and SUBSTRING_INDEX(fcm.position, '-', 1) = @position
                ORDER BY m.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@team_id", teamId);
                command.Parameters.AddWithValue("@position", position);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardMonsters CardMonsters = new CardMonsters
                    {
                        id = reader.GetString("monster_id"),
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

                    CardMonstersList.Add(CardMonsters);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardMonstersList;
    }
    public Dictionary<string, int> GetUniqueCardMonsterTypesTeam(string teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = @"SELECT distinct c.type, count(c.type) as number
            FROM user_card_monsters uc
            LEFT JOIN card_monsters c ON uc.card_monster_id = c.id 
            LEFT JOIN fact_card_monsters fch ON fch.user_id = uc.user_id AND fch.user_card_monster_id = uc.card_monster_id
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
    public int GetUserCardMonstersCount(string user_id, string type)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from card_monsters m, user_card_monsters um where m.id=um.card_monster_id and um.user_id=@userId and m.type= @type";
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
    public int GetUserCardMonstersTeamsPositionCount(string user_id, string team_id, string position)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*) from fact_card_monsters
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
    public bool InsertUserCardMonsters(CardMonsters CardMonsters)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_monsters
                WHERE user_id = @user_id AND card_monster_id = @card_monster_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_monster_id", CardMonsters.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_card_monsters (
                    user_id, card_monster_id, level, experiment, star, quality, block, quantity, power, health, physical_attack, 
                    physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, 
                    atomic_defense, mental_attack, mental_defense, speed, critical_damage_rate, critical_rate, 
                    penetration_rate, evasion_rate, damage_absorption_rate, vitality_regeneration_rate, accuracy_rate, 
                    lifesteal_rate, shield_strength, tenacity, resistance_rate, combo_rate, reflection_rate, 
                    mana, mana_regeneration_rate, damage_to_different_faction_rate, 
                    resistance_to_different_faction_rate, damage_to_same_faction_rate, resistance_to_same_faction_rate
                ) VALUES (
                    @user_id, @card_monster_id, @level, @experiment, @star, @quality, @block, @quantity, @power, @health, @physical_attack, 
                    @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, 
                    @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage_rate, @critical_rate, 
                    @penetration_rate, @evasion_rate, @damage_absorption_rate, @vitality_regeneration_rate, @accuracy_rate, 
                    @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate, @combo_rate, @reflection_rate, 
                    @mana, @mana_regeneration_rate, @damage_to_different_faction_rate, 
                    @resistance_to_different_faction_rate, @damage_to_same_faction_rate, @resistance_to_same_faction_rate
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@card_monster_id", CardMonsters.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(CardMonsters.rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", 0);
                    command.Parameters.AddWithValue("@power", CardMonsters.power);
                    command.Parameters.AddWithValue("@health", CardMonsters.health);
                    command.Parameters.AddWithValue("@physical_attack", CardMonsters.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", CardMonsters.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", CardMonsters.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", CardMonsters.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", CardMonsters.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", CardMonsters.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", CardMonsters.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", CardMonsters.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", CardMonsters.mental_attack);
                    command.Parameters.AddWithValue("@mental_defense", CardMonsters.mental_defense);
                    command.Parameters.AddWithValue("@speed", CardMonsters.speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", CardMonsters.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", CardMonsters.critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", CardMonsters.penetration_rate);
                    command.Parameters.AddWithValue("@evasion_rate", CardMonsters.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", CardMonsters.damage_absorption_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", CardMonsters.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", CardMonsters.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", CardMonsters.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", CardMonsters.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", CardMonsters.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", CardMonsters.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", CardMonsters.combo_rate);
                    command.Parameters.AddWithValue("@reflection_rate", CardMonsters.reflection_rate);
                    command.Parameters.AddWithValue("@mana", CardMonsters.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", CardMonsters.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", CardMonsters.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", CardMonsters.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", CardMonsters.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", CardMonsters.resistance_to_same_faction_rate);
                    MySqlDataReader reader = command.ExecuteReader();
                    InsertFactCardMonsters(CardMonsters);
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_card_monsters
                    SET quantity = quantity + 1
                    WHERE user_id = @user_id AND card_monster_id = @card_monster_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@card_monster_id", CardMonsters.id);

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
    public bool UpdateCardMonstersLevel(CardMonsters cardMonsters, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_monsters
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
                WHERE user_id = @user_id AND card_monster_id = @card_monster_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_monster_id", cardMonsters.id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", cardMonsters.power);
                command.Parameters.AddWithValue("@health", cardMonsters.health);
                command.Parameters.AddWithValue("@physical_attack", cardMonsters.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", cardMonsters.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", cardMonsters.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", cardMonsters.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", cardMonsters.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", cardMonsters.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", cardMonsters.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", cardMonsters.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", cardMonsters.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", cardMonsters.mental_defense);
                command.Parameters.AddWithValue("@speed", cardMonsters.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", cardMonsters.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", cardMonsters.critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", cardMonsters.penetration_rate);
                command.Parameters.AddWithValue("@evasion_rate", cardMonsters.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardMonsters.damage_absorption_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardMonsters.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@accuracy_rate", cardMonsters.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardMonsters.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", cardMonsters.shield_strength);
                command.Parameters.AddWithValue("@tenacity", cardMonsters.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardMonsters.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", cardMonsters.combo_rate);
                command.Parameters.AddWithValue("@reflection_rate", cardMonsters.reflection_rate);
                command.Parameters.AddWithValue("@mana", cardMonsters.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardMonsters.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardMonsters.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardMonsters.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardMonsters.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardMonsters.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateCardMonstersBreakthrough(CardMonsters cardMonsters, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_monsters
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
                WHERE user_id = @user_id AND card_monster_id = @card_monster_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_monster_id", cardMonsters.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", cardMonsters.power);
                command.Parameters.AddWithValue("@health", cardMonsters.health);
                command.Parameters.AddWithValue("@physical_attack", cardMonsters.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", cardMonsters.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", cardMonsters.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", cardMonsters.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", cardMonsters.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", cardMonsters.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", cardMonsters.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", cardMonsters.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", cardMonsters.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", cardMonsters.mental_defense);
                command.Parameters.AddWithValue("@speed", cardMonsters.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", cardMonsters.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", cardMonsters.critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", cardMonsters.penetration_rate);
                command.Parameters.AddWithValue("@evasion_rate", cardMonsters.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardMonsters.damage_absorption_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardMonsters.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@accuracy_rate", cardMonsters.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardMonsters.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", cardMonsters.shield_strength);
                command.Parameters.AddWithValue("@tenacity", cardMonsters.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardMonsters.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", cardMonsters.combo_rate);
                command.Parameters.AddWithValue("@reflection_rate", cardMonsters.reflection_rate);
                command.Parameters.AddWithValue("@mana", cardMonsters.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardMonsters.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardMonsters.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardMonsters.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardMonsters.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardMonsters.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool InsertFactCardMonsters(CardMonsters cardMonsters)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                INSERT INTO fact_card_monsters (
                user_id, user_card_monster_id, team_id, position, role, 
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
                @user_id, @user_card_monster_id, @team_id, @position, @role, 
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
                command.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.id);
                command.Parameters.AddWithValue("@team_id", null);
                command.Parameters.AddWithValue("@position", null);
                command.Parameters.AddWithValue("@role", null);
                command.Parameters.AddWithValue("@all_power", cardMonsters.power);
                command.Parameters.AddWithValue("@all_health", cardMonsters.health);
                command.Parameters.AddWithValue("@all_physical_attack", cardMonsters.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", cardMonsters.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", cardMonsters.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", cardMonsters.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", cardMonsters.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", cardMonsters.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", cardMonsters.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", cardMonsters.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", cardMonsters.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", cardMonsters.mental_defense);
                command.Parameters.AddWithValue("@all_speed", cardMonsters.speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", cardMonsters.critical_damage_rate);
                command.Parameters.AddWithValue("@all_critical_rate", cardMonsters.critical_rate);
                command.Parameters.AddWithValue("@all_penetration_rate", cardMonsters.penetration_rate);
                command.Parameters.AddWithValue("@all_evasion_rate", cardMonsters.evasion_rate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", cardMonsters.damage_absorption_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", cardMonsters.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@all_accuracy_rate", cardMonsters.accuracy_rate);
                command.Parameters.AddWithValue("@all_mana", cardMonsters.mana);
                command.Parameters.AddWithValue("@all_lifesteal_rate", cardMonsters.lifesteal_rate);
                command.Parameters.AddWithValue("@all_shield_strength", cardMonsters.shield_strength);
                command.Parameters.AddWithValue("@all_tenacity", cardMonsters.tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", cardMonsters.resistance_rate);
                command.Parameters.AddWithValue("@all_combo_rate", cardMonsters.combo_rate);
                command.Parameters.AddWithValue("@all_reflection_rate", cardMonsters.reflection_rate);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", cardMonsters.mana_regeneration_rate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", cardMonsters.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", cardMonsters.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", cardMonsters.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", cardMonsters.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateFactCardMonsters(CardMonsters cardMonsters)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE fact_card_monsters
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
                WHERE user_id = @user_id AND user_card_monster_id = @user_card_monster_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.id);
                command.Parameters.AddWithValue("@all_power", cardMonsters.power);
                command.Parameters.AddWithValue("@all_health", cardMonsters.health);
                command.Parameters.AddWithValue("@all_physical_attack", cardMonsters.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", cardMonsters.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", cardMonsters.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", cardMonsters.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", cardMonsters.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", cardMonsters.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", cardMonsters.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", cardMonsters.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", cardMonsters.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", cardMonsters.mental_defense);
                command.Parameters.AddWithValue("@all_speed", cardMonsters.speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", cardMonsters.critical_damage_rate);
                command.Parameters.AddWithValue("@all_critical_rate", cardMonsters.critical_rate);
                command.Parameters.AddWithValue("@all_penetration_rate", cardMonsters.penetration_rate);
                command.Parameters.AddWithValue("@all_evasion_rate", cardMonsters.evasion_rate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", cardMonsters.damage_absorption_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", cardMonsters.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@all_accuracy_rate", cardMonsters.accuracy_rate);
                command.Parameters.AddWithValue("@all_mana", cardMonsters.mana);
                command.Parameters.AddWithValue("@all_lifesteal_rate", cardMonsters.lifesteal_rate);
                command.Parameters.AddWithValue("@all_shield_strength", cardMonsters.shield_strength);
                command.Parameters.AddWithValue("@all_tenacity", cardMonsters.tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", cardMonsters.resistance_rate);
                command.Parameters.AddWithValue("@all_combo_rate", cardMonsters.combo_rate);
                command.Parameters.AddWithValue("@all_reflection_rate", cardMonsters.reflection_rate);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", cardMonsters.mana_regeneration_rate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", cardMonsters.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", cardMonsters.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", cardMonsters.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", cardMonsters.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateTeamFactCardMonsters(string team_id, string position, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                Update fact_card_monsters set team_id=@team_id, position=@position where user_id=@user_id 
                and user_card_monster_id=@user_card_monster_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_monster_id", card_id);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public CardMonsters GetUserCardMonstersById(string user_id, string Id)
    {
        CardMonsters card = new CardMonsters();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_card_monsters where user_card_monsters.card_monster_id=@id 
                and user_card_monsters.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new CardMonsters
                    {
                        id = reader.GetString("card_monster_id"),
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
    public List<CardMonsters> GetAllUserCardMonstersInTeam(string user_id)
    {
        List<CardMonsters> cardMonsters = new List<CardMonsters>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = @"SELECT uc.*, c.*, fch.*
                FROM user_card_monsters uc
                LEFT JOIN card_monsters c ON uc.card_monster_id = c.id 
                LEFT JOIN fact_card_monsters fch ON fch.user_id = uc.user_id AND fch.user_card_monster_id = uc.card_monster_id
                WHERE uc.user_id = @user_id and fch.team_id IS NOT null";
            MySqlCommand command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardMonsters monsters = new CardMonsters
                {
                    id = reader.GetString("card_monster_id"),
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

                cardMonsters.Add(monsters);
            }
        }
        return cardMonsters;
    }
}