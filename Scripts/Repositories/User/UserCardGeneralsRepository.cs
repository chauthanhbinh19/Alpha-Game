using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserCardGeneralsRepository : IUserCardGeneralsRepository
{
    public int GetUserCardGeneralsTeamsPositionCount(string user_id, string team_id, string position)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*) from fact_card_generals
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
    public List<CardGenerals> GetUserCardGenerals(string user_id, string type, int pageSize, int offset)
    {
        List<CardGenerals> CardGeneralsList = new List<CardGenerals>();
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT uc.*, c.*, fcg.*
                FROM user_card_generals uc
                LEFT JOIN card_generals c ON c.id = uc.card_general_id 
                LEFT JOIN fact_card_generals fcg ON fcg.user_id = uc.user_id AND fcg.user_card_general_id = uc.card_general_id
                WHERE uc.user_id = @userId AND c.type = @type
                ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name
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
                    CardGenerals captain = new CardGenerals
                    {
                        id = reader.GetString("card_general_id"),
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
                        team_id = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetString("team_id"),
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
                        critical_resistance_rate = reader.GetDouble("critical_resistance_rate"),
                        ignore_critical_rate = reader.GetDouble("ignore_critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate"),
                        absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        ignore_combo_rate = reader.GetDouble("ignore_combo_rate"),
                        combo_damage_rate = reader.GetDouble("combo_damage_rate"),
                        combo_resistance_rate = reader.GetDouble("combo_resistance_rate"),
                        stun_rate = reader.GetDouble("stun_rate"),
                        ignore_stun_rate = reader.GetDouble("ignore_stun_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate"),
                        reflection_damage_rate = reader.GetDouble("reflection_damage_rate"),
                        reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        normal_damage_rate = reader.GetDouble("normal_damage_rate"),
                        normal_resistance_rate = reader.GetDouble("normal_resistance_rate"),
                        skill_damage_rate = reader.GetDouble("skill_damage_rate"),
                        skill_resistance_rate = reader.GetDouble("skill_resistance_rate"),
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
                        all_critical_resistance_rate = reader.GetDouble("all_critical_resistance_rate"),
                        all_ignore_critical_rate = reader.GetDouble("all_ignore_critical_rate"),
                        all_penetration_rate = reader.GetDouble("all_penetration_rate"),
                        all_penetration_resistance_rate = reader.GetDouble("all_penetration_resistance_rate"),
                        all_evasion_rate = reader.GetDouble("all_evasion_rate"),
                        all_damage_absorption_rate = reader.GetDouble("all_damage_absorption_rate"),
                        all_ignore_damage_absorption_rate = reader.GetDouble("all_ignore_damage_absorption_rate"),
                        all_absorbed_damage_rate = reader.GetDouble("all_absorbed_damage_rate"),
                        all_vitality_regeneration_rate = reader.GetDouble("all_vitality_regeneration_rate"),
                        all_vitality_regeneration_resistance_rate = reader.GetDouble("all_vitality_regeneration_resistance_rate"),
                        all_accuracy_rate = reader.GetDouble("all_accuracy_rate"),
                        all_lifesteal_rate = reader.GetDouble("all_lifesteal_rate"),
                        all_shield_strength = reader.GetDouble("all_shield_strength"),
                        all_tenacity = reader.GetDouble("all_tenacity"),
                        all_resistance_rate = reader.GetDouble("all_resistance_rate"),
                        all_combo_rate = reader.GetDouble("all_combo_rate"),
                        all_ignore_combo_rate = reader.GetDouble("all_ignore_combo_rate"),
                        all_combo_damage_rate = reader.GetDouble("all_combo_damage_rate"),
                        all_combo_resistance_rate = reader.GetDouble("all_combo_resistance_rate"),
                        all_stun_rate = reader.GetDouble("all_stun_rate"),
                        all_ignore_stun_rate = reader.GetDouble("all_ignore_stun_rate"),
                        all_reflection_rate = reader.GetDouble("all_reflection_rate"),
                        all_ignore_reflection_rate = reader.GetDouble("all_ignore_reflection_rate"),
                        all_reflection_damage_rate = reader.GetDouble("all_reflection_damage_rate"),
                        all_reflection_resistance_rate = reader.GetDouble("all_reflection_resistance_rate"),
                        all_mana = reader.GetFloat("all_mana"),
                        all_mana_regeneration_rate = reader.GetDouble("all_mana_regeneration_rate"),
                        all_damage_to_different_faction_rate = reader.GetDouble("all_damage_to_different_faction_rate"),
                        all_resistance_to_different_faction_rate = reader.GetDouble("all_resistance_to_different_faction_rate"),
                        all_damage_to_same_faction_rate = reader.GetDouble("all_damage_to_same_faction_rate"),
                        all_resistance_to_same_faction_rate = reader.GetDouble("all_resistance_to_same_faction_rate"),
                        all_normal_damage_rate = reader.GetDouble("all_normal_damage_rate"),
                        all_normal_resistance_rate = reader.GetDouble("all_normal_resistance_rate"),
                        all_skill_damage_rate = reader.GetDouble("all_skill_damage_rate"),
                        all_skill_resistance_rate = reader.GetDouble("all_skill_resistance_rate"),
                    };

                    CardGeneralsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardGeneralsList;
    }
    public List<CardGenerals> GetUserCardGeneralsTeam(string user_id, string teamId, string position)
    {
        List<CardGenerals> CardGeneralsList = new List<CardGenerals>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT uc.*, c.*, fcg.*
                FROM user_card_generals uc
                LEFT JOIN card_generals c ON c.id = uc.card_general_id 
                LEFT JOIN fact_card_generals fcg ON fcg.user_id = uc.user_id AND fcg.user_card_general_id = uc.card_general_id
                WHERE uc.user_id = @userId AND fcg.team_id = @team_id and SUBSTRING_INDEX(fcg.position, '-', 1) = @position
                ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@team_id", teamId);
                command.Parameters.AddWithValue("@position", position);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardGenerals captain = new CardGenerals
                    {
                        id = reader.GetString("card_general_id"),
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
                        team_id = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetString("team_id"),
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
                        critical_resistance_rate = reader.GetDouble("critical_resistance_rate"),
                        ignore_critical_rate = reader.GetDouble("ignore_critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate"),
                        absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        ignore_combo_rate = reader.GetDouble("ignore_combo_rate"),
                        combo_damage_rate = reader.GetDouble("combo_damage_rate"),
                        combo_resistance_rate = reader.GetDouble("combo_resistance_rate"),
                        stun_rate = reader.GetDouble("stun_rate"),
                        ignore_stun_rate = reader.GetDouble("ignore_stun_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate"),
                        reflection_damage_rate = reader.GetDouble("reflection_damage_rate"),
                        reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        normal_damage_rate = reader.GetDouble("normal_damage_rate"),
                        normal_resistance_rate = reader.GetDouble("normal_resistance_rate"),
                        skill_damage_rate = reader.GetDouble("skill_damage_rate"),
                        skill_resistance_rate = reader.GetDouble("skill_resistance_rate"),
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
                        all_critical_resistance_rate = reader.GetDouble("all_critical_resistance_rate"),
                        all_ignore_critical_rate = reader.GetDouble("all_ignore_critical_rate"),
                        all_penetration_rate = reader.GetDouble("all_penetration_rate"),
                        all_penetration_resistance_rate = reader.GetDouble("all_penetration_resistance_rate"),
                        all_evasion_rate = reader.GetDouble("all_evasion_rate"),
                        all_damage_absorption_rate = reader.GetDouble("all_damage_absorption_rate"),
                        all_ignore_damage_absorption_rate = reader.GetDouble("all_ignore_damage_absorption_rate"),
                        all_absorbed_damage_rate = reader.GetDouble("all_absorbed_damage_rate"),
                        all_vitality_regeneration_rate = reader.GetDouble("all_vitality_regeneration_rate"),
                        all_vitality_regeneration_resistance_rate = reader.GetDouble("all_vitality_regeneration_resistance_rate"),
                        all_accuracy_rate = reader.GetDouble("all_accuracy_rate"),
                        all_lifesteal_rate = reader.GetDouble("all_lifesteal_rate"),
                        all_shield_strength = reader.GetDouble("all_shield_strength"),
                        all_tenacity = reader.GetDouble("all_tenacity"),
                        all_resistance_rate = reader.GetDouble("all_resistance_rate"),
                        all_combo_rate = reader.GetDouble("all_combo_rate"),
                        all_ignore_combo_rate = reader.GetDouble("all_ignore_combo_rate"),
                        all_combo_damage_rate = reader.GetDouble("all_combo_damage_rate"),
                        all_combo_resistance_rate = reader.GetDouble("all_combo_resistance_rate"),
                        all_stun_rate = reader.GetDouble("all_stun_rate"),
                        all_ignore_stun_rate = reader.GetDouble("all_ignore_stun_rate"),
                        all_reflection_rate = reader.GetDouble("all_reflection_rate"),
                        all_ignore_reflection_rate = reader.GetDouble("all_ignore_reflection_rate"),
                        all_reflection_damage_rate = reader.GetDouble("all_reflection_damage_rate"),
                        all_reflection_resistance_rate = reader.GetDouble("all_reflection_resistance_rate"),
                        all_mana = reader.GetFloat("all_mana"),
                        all_mana_regeneration_rate = reader.GetDouble("all_mana_regeneration_rate"),
                        all_damage_to_different_faction_rate = reader.GetDouble("all_damage_to_different_faction_rate"),
                        all_resistance_to_different_faction_rate = reader.GetDouble("all_resistance_to_different_faction_rate"),
                        all_damage_to_same_faction_rate = reader.GetDouble("all_damage_to_same_faction_rate"),
                        all_resistance_to_same_faction_rate = reader.GetDouble("all_resistance_to_same_faction_rate"),
                        all_normal_damage_rate = reader.GetDouble("all_normal_damage_rate"),
                        all_normal_resistance_rate = reader.GetDouble("all_normal_resistance_rate"),
                        all_skill_damage_rate = reader.GetDouble("all_skill_damage_rate"),
                        all_skill_resistance_rate = reader.GetDouble("all_skill_resistance_rate"),
                    };

                    CardGeneralsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardGeneralsList;
    }
    public Dictionary<string, int> GetUniqueCardGeneralTypesTeam(string teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = @"SELECT distinct c.type, count(c.type) as number
            FROM user_card_generals uc
            LEFT JOIN card_generals c ON uc.card_general_id = c.id 
            LEFT JOIN fact_card_generals fch ON fch.user_id = uc.user_id AND fch.user_card_general_id = uc.card_general_id
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
    public bool UpdateTeamFactCardGenerals(string team_id, string position, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                Update fact_card_generals set team_id=@team_id, position=@position where user_id=@user_id 
                and user_card_general_id=@user_card_general_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_general_id", card_id);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public int GetUserCardGeneralsCount(string user_id, string type)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from card_generals c, user_card_generals uc where c.id=uc.card_general_id and uc.user_id=@userId and c.type= @type";
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
    public bool InsertUserCardGenerals(CardGenerals CardGenerals)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_generals
                WHERE user_id = @user_id AND card_general_id = @card_general_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_general_id", CardGenerals.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_card_generals (
                    user_id, card_general_id, level, experiment, star, quality, block, quantity,
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
                    @user_id, @card_general_id, @level, @experiment, @star, @quality, @block, @quantity,
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
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@card_general_id", CardGenerals.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(CardGenerals.rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", 0);
                    command.Parameters.AddWithValue("@power", CardGenerals.power);
                    command.Parameters.AddWithValue("@health", CardGenerals.health);
                    command.Parameters.AddWithValue("@physical_attack", CardGenerals.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", CardGenerals.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", CardGenerals.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", CardGenerals.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", CardGenerals.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", CardGenerals.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", CardGenerals.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", CardGenerals.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", CardGenerals.mental_attack);
                    command.Parameters.AddWithValue("@mental_defense", CardGenerals.mental_defense);
                    command.Parameters.AddWithValue("@speed", CardGenerals.speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", CardGenerals.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", CardGenerals.critical_rate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", CardGenerals.critical_resistance_rate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", CardGenerals.ignore_critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", CardGenerals.penetration_rate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", CardGenerals.penetration_resistance_rate);
                    command.Parameters.AddWithValue("@evasion_rate", CardGenerals.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", CardGenerals.damage_absorption_rate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", CardGenerals.ignore_damage_absorption_rate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", CardGenerals.absorbed_damage_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", CardGenerals.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", CardGenerals.vitality_regeneration_resistance_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", CardGenerals.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", CardGenerals.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", CardGenerals.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", CardGenerals.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", CardGenerals.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", CardGenerals.combo_rate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", CardGenerals.ignore_combo_rate);
                    command.Parameters.AddWithValue("@combo_damage_rate", CardGenerals.combo_damage_rate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", CardGenerals.combo_resistance_rate);
                    command.Parameters.AddWithValue("@stun_rate", CardGenerals.stun_rate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", CardGenerals.ignore_stun_rate);
                    command.Parameters.AddWithValue("@reflection_rate", CardGenerals.reflection_rate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", CardGenerals.ignore_reflection_rate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", CardGenerals.reflection_damage_rate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", CardGenerals.reflection_resistance_rate);
                    command.Parameters.AddWithValue("@mana", CardGenerals.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", CardGenerals.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", CardGenerals.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", CardGenerals.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", CardGenerals.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", CardGenerals.resistance_to_same_faction_rate);
                    command.Parameters.AddWithValue("@normal_damage_rate", CardGenerals.normal_damage_rate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", CardGenerals.normal_resistance_rate);
                    command.Parameters.AddWithValue("@skill_damage_rate", CardGenerals.skill_damage_rate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", CardGenerals.skill_resistance_rate);
                    MySqlDataReader reader = command.ExecuteReader();
                    InsertFactCardGenerals(CardGenerals);
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_card_generals
                    SET quantity = quantity + 1
                    WHERE user_id = @user_id AND card_general_id = @card_general_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@card_general_id", CardGenerals.id);

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
    public bool UpdateCardGeneralsLevel(CardGenerals cardGenerals, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_generals
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
                WHERE user_id = @user_id AND card_general_id = @card_general_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_general_id", cardGenerals.id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", cardGenerals.power);
                command.Parameters.AddWithValue("@health", cardGenerals.health);
                command.Parameters.AddWithValue("@physical_attack", cardGenerals.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", cardGenerals.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", cardGenerals.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", cardGenerals.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", cardGenerals.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", cardGenerals.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", cardGenerals.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", cardGenerals.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", cardGenerals.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", cardGenerals.mental_defense);
                command.Parameters.AddWithValue("@speed", cardGenerals.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", cardGenerals.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", cardGenerals.critical_rate);
                command.Parameters.AddWithValue("@critical_resistance_rate", cardGenerals.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", cardGenerals.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", cardGenerals.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", cardGenerals.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", cardGenerals.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardGenerals.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardGenerals.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", cardGenerals.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardGenerals.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardGenerals.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", cardGenerals.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardGenerals.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", cardGenerals.shield_strength);
                command.Parameters.AddWithValue("@tenacity", cardGenerals.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardGenerals.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", cardGenerals.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", cardGenerals.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", cardGenerals.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", cardGenerals.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", cardGenerals.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", cardGenerals.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", cardGenerals.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", cardGenerals.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", cardGenerals.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", cardGenerals.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", cardGenerals.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardGenerals.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardGenerals.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardGenerals.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardGenerals.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardGenerals.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", cardGenerals.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", cardGenerals.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", cardGenerals.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", cardGenerals.skill_resistance_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateCardGeneralsBreakthrough(CardGenerals cardGenerals, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_generals
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
                WHERE user_id = @user_id AND card_general_id = @card_general_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_general_id", cardGenerals.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", cardGenerals.power);
                command.Parameters.AddWithValue("@health", cardGenerals.health);
                command.Parameters.AddWithValue("@physical_attack", cardGenerals.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", cardGenerals.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", cardGenerals.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", cardGenerals.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", cardGenerals.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", cardGenerals.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", cardGenerals.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", cardGenerals.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", cardGenerals.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", cardGenerals.mental_defense);
                command.Parameters.AddWithValue("@speed", cardGenerals.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", cardGenerals.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", cardGenerals.critical_rate);
                command.Parameters.AddWithValue("@critical_resistance_rate", cardGenerals.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", cardGenerals.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", cardGenerals.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", cardGenerals.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", cardGenerals.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardGenerals.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardGenerals.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", cardGenerals.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardGenerals.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardGenerals.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", cardGenerals.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardGenerals.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", cardGenerals.shield_strength);
                command.Parameters.AddWithValue("@tenacity", cardGenerals.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardGenerals.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", cardGenerals.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", cardGenerals.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", cardGenerals.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", cardGenerals.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", cardGenerals.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", cardGenerals.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", cardGenerals.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", cardGenerals.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", cardGenerals.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", cardGenerals.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", cardGenerals.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardGenerals.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardGenerals.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardGenerals.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardGenerals.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardGenerals.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", cardGenerals.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", cardGenerals.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", cardGenerals.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", cardGenerals.skill_resistance_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool InsertFactCardGenerals(CardGenerals cardGenerals)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                INSERT INTO fact_card_generals (
                user_id, user_card_general_id, team_id, position, role, 
                all_power, all_health, 
                all_physical_attack, all_physical_defense, 
                all_magical_attack, all_magical_defense, 
                all_chemical_attack, all_chemical_defense, 
                all_atomic_attack, all_atomic_defense, 
                all_mental_attack, all_mental_defense, 
                all_speed, 
                all_critical_damage_rate, all_critical_rate, all_critical_resistance_rate, all_ignore_critical_rate,
                all_penetration_rate, all_penetration_resistance_rate, 
                all_evasion_rate, 
                all_damage_absorption_rate, all_ignore_damage_absorption_rate, all_absorbed_damage_rate,
                all_vitality_regeneration_rate, all_vitality_regeneration_resistance_rate, 
                all_accuracy_rate, all_lifesteal_rate, 
                all_shield_strength, all_tenacity, all_resistance_rate, 
                all_combo_rate, all_ignore_combo_rate, all_combo_damage_rate, all_combo_resistance_rate,
                all_stun_rate, all_ignore_stun_rate,
                all_reflection_rate, all_ignore_reflection_rate, all_reflection_damage_rate, all_reflection_resistance_rate,
                all_mana, all_mana_regeneration_rate, 
                all_damage_to_different_faction_rate, all_resistance_to_different_faction_rate, 
                all_damage_to_same_faction_rate, all_resistance_to_same_faction_rate,
                all_normal_damage_rate, all_normal_resistance_rate,
                all_skill_damage_rate, all_skill_resistance_rate
            ) VALUES (
                @user_id, @user_card_general_id, @team_id, @position, @role, 
                @all_power, @all_health, 
                @all_physical_attack, @all_physical_defense, 
                @all_magical_attack, @all_magical_defense, 
                @all_chemical_attack, @all_chemical_defense, 
                @all_atomic_attack, @all_atomic_defense, 
                @all_mental_attack, @all_mental_defense, 
                @all_speed, 
                @all_critical_damage_rate, @all_critical_rate, @all_critical_resistance_rate, @all_ignore_critical_rate,
                @all_penetration_rate, @all_penetration_resistance_rate, 
                @all_evasion_rate, 
                @all_damage_absorption_rate, @all_ignore_damage_absorption_rate, @all_absorbed_damage_rate,
                @all_vitality_regeneration_rate, @all_vitality_regeneration_resistance_rate, 
                @all_accuracy_rate, @all_lifesteal_rate, 
                @all_shield_strength, @all_tenacity, @all_resistance_rate, 
                @all_combo_rate, @all_ignore_combo_rate, @all_combo_damage_rate, @all_combo_resistance_rate,
                @all_stun_rate, @all_ignore_stun_rate,
                @all_reflection_rate, @all_ignore_reflection_rate, @all_reflection_damage_rate, @all_reflection_resistance_rate,
                @all_mana, @all_mana_regeneration_rate, 
                @all_damage_to_different_faction_rate, @all_resistance_to_different_faction_rate, 
                @all_damage_to_same_faction_rate, @all_resistance_to_same_faction_rate,
                @all_normal_damage_rate, @all_normal_resistance_rate,
                @all_skill_damage_rate, @all_skill_resistance_rate
            );";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_general_id", cardGenerals.id);
                command.Parameters.AddWithValue("@team_id", null);
                command.Parameters.AddWithValue("@position", null);
                command.Parameters.AddWithValue("@role", null);
                command.Parameters.AddWithValue("@all_power", cardGenerals.power);
                command.Parameters.AddWithValue("@all_health", cardGenerals.health);
                command.Parameters.AddWithValue("@all_physical_attack", cardGenerals.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", cardGenerals.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", cardGenerals.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", cardGenerals.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", cardGenerals.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", cardGenerals.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", cardGenerals.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", cardGenerals.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", cardGenerals.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", cardGenerals.mental_defense);
                command.Parameters.AddWithValue("@all_speed", cardGenerals.speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", cardGenerals.critical_damage_rate);
                command.Parameters.AddWithValue("@all_critical_rate", cardGenerals.critical_rate);
                command.Parameters.AddWithValue("@all_critical_resistance_rate", cardGenerals.critical_resistance_rate);
                command.Parameters.AddWithValue("@all_ignore_critical_rate", cardGenerals.ignore_critical_rate);
                command.Parameters.AddWithValue("@all_penetration_rate", cardGenerals.penetration_rate);
                command.Parameters.AddWithValue("@all_penetration_resistance_rate", cardGenerals.penetration_resistance_rate);
                command.Parameters.AddWithValue("@all_evasion_rate", cardGenerals.evasion_rate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", cardGenerals.damage_absorption_rate);
                command.Parameters.AddWithValue("@all_ignore_damage_absorption_rate", cardGenerals.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@all_absorbed_damage_rate", cardGenerals.absorbed_damage_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", cardGenerals.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_resistance_rate", cardGenerals.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@all_accuracy_rate", cardGenerals.accuracy_rate);
                command.Parameters.AddWithValue("@all_lifesteal_rate", cardGenerals.lifesteal_rate);
                command.Parameters.AddWithValue("@all_shield_strength", cardGenerals.shield_strength);
                command.Parameters.AddWithValue("@all_tenacity", cardGenerals.tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", cardGenerals.resistance_rate);
                command.Parameters.AddWithValue("@all_combo_rate", cardGenerals.combo_rate);
                command.Parameters.AddWithValue("@all_ignore_combo_rate", cardGenerals.ignore_combo_rate);
                command.Parameters.AddWithValue("@all_combo_damage_rate", cardGenerals.combo_damage_rate);
                command.Parameters.AddWithValue("@all_combo_resistance_rate", cardGenerals.combo_resistance_rate);
                command.Parameters.AddWithValue("@all_stun_rate", cardGenerals.stun_rate);
                command.Parameters.AddWithValue("@all_ignore_stun_rate", cardGenerals.ignore_stun_rate);
                command.Parameters.AddWithValue("@all_reflection_rate", cardGenerals.reflection_rate);
                command.Parameters.AddWithValue("@all_ignore_reflection_rate", cardGenerals.ignore_reflection_rate);
                command.Parameters.AddWithValue("@all_reflection_damage_rate", cardGenerals.reflection_damage_rate);
                command.Parameters.AddWithValue("@all_reflection_resistance_rate", cardGenerals.reflection_resistance_rate);
                command.Parameters.AddWithValue("@all_mana", cardGenerals.mana);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", cardGenerals.mana_regeneration_rate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", cardGenerals.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", cardGenerals.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", cardGenerals.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", cardGenerals.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_normal_damage_rate", cardGenerals.normal_damage_rate);
                command.Parameters.AddWithValue("@all_normal_resistance_rate", cardGenerals.normal_resistance_rate);
                command.Parameters.AddWithValue("@all_skill_damage_rate", cardGenerals.skill_damage_rate);
                command.Parameters.AddWithValue("@all_skill_resistance_rate", cardGenerals.skill_resistance_rate);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateFactCardGenerals(CardGenerals cardGenerals)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE fact_card_generals
                SET 
                    all_physical_attack = @all_physical_attack, all_physical_defense = @all_physical_defense, 
                    all_magical_attack = @all_magical_attack, all_magical_defense = @all_magical_defense, 
                    all_chemical_attack = @all_chemical_attack, all_chemical_defense = @all_chemical_defense, 
                    all_atomic_attack = @all_atomic_attack, all_atomic_defense = @all_atomic_defense, 
                    all_mental_attack = @all_mental_attack, all_mental_defense = @all_mental_defense, 
                    all_speed = @all_speed, all_critical_damage_rate = @all_critical_damage_rate, 
                    all_critical_rate = @all_critical_rate, all_critical_resistance_rate = @all_critical_resistance_rate, all_ignore_critical_rate = @all_ignore_critical_rate,
                    all_penetration_rate = @all_penetration_rate, all_penetration_resistance_rate = @all_penetration_resistance_rate,
                    all_evasion_rate = @all_evasion_rate, all_damage_absorption_rate = @all_damage_absorption_rate, 
                    all_ignore_damage_absorption_rate = @all_ignore_damage_absorption_rate, all_absorbed_damage_rate = @all_absorbed_damage_rate,
                    all_vitality_regeneration_rate = @all_vitality_regeneration_rate, all_vitality_regeneration_resistance_rate = @all_vitality_regeneration_resistance_rate, 
                    all_accuracy_rate = @all_accuracy_rate, all_lifesteal_rate = @all_lifesteal_rate, all_shield_strength = @all_shield_strength, 
                    all_tenacity = @all_tenacity, all_resistance_rate = @all_resistance_rate, 
                    all_combo_rate = @all_combo_rate, all_ignore_combo_rate = @all_ignore_combo_rate, all_combo_damage_rate = @all_combo_damage_rate, all_combo_resistance_rate = @all_combo_resistance_rate,
                    all_stun_rate = @all_stun_rate, all_ignore_stun_rate = @all_ignore_stun_rate,
                    all_reflection_rate = @all_reflection_rate, all_ignore_reflection_rate = @all_ignore_reflection_rate, 
                    all_reflection_damage_rate = @all_reflection_damage_rate, all_reflection_resistance_rate = @all_reflection_resistance_rate,
                    all_mana = @all_mana, all_mana_regeneration_rate = @all_mana_regeneration_rate, 
                    all_damage_to_different_faction_rate = @all_damage_to_different_faction_rate, 
                    all_resistance_to_different_faction_rate = @all_resistance_to_different_faction_rate, 
                    all_damage_to_same_faction_rate = @all_damage_to_same_faction_rate, 
                    all_resistance_to_same_faction_rate = @all_resistance_to_same_faction_rate,
                    all_normal_damage_rate = @all_normal_damage_rate, all_normal_resistance_rate = @all_normal_resistance_rate,
                    all_skill_damage_rate = @all_skill_damage_rate, all_skill_resistance_rate = @all_skill_resistance_rate
                WHERE user_id = @user_id AND user_card_general_id = @user_card_general_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_general_id", cardGenerals.id);
                command.Parameters.AddWithValue("@all_power", cardGenerals.power);
                command.Parameters.AddWithValue("@all_health", cardGenerals.health);
                command.Parameters.AddWithValue("@all_physical_attack", cardGenerals.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", cardGenerals.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", cardGenerals.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", cardGenerals.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", cardGenerals.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", cardGenerals.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", cardGenerals.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", cardGenerals.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", cardGenerals.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", cardGenerals.mental_defense);
                command.Parameters.AddWithValue("@all_speed", cardGenerals.speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", cardGenerals.critical_damage_rate);
                command.Parameters.AddWithValue("@all_critical_rate", cardGenerals.critical_rate);
                command.Parameters.AddWithValue("@all_critical_resistance_rate", cardGenerals.critical_resistance_rate);
                command.Parameters.AddWithValue("@all_ignore_critical_rate", cardGenerals.ignore_critical_rate);
                command.Parameters.AddWithValue("@all_penetration_rate", cardGenerals.penetration_rate);
                command.Parameters.AddWithValue("@all_penetration_resistance_rate", cardGenerals.penetration_resistance_rate);
                command.Parameters.AddWithValue("@all_evasion_rate", cardGenerals.evasion_rate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", cardGenerals.damage_absorption_rate);
                command.Parameters.AddWithValue("@all_ignore_damage_absorption_rate", cardGenerals.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@all_absorbed_damage_rate", cardGenerals.absorbed_damage_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", cardGenerals.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_resistance_rate", cardGenerals.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@all_accuracy_rate", cardGenerals.accuracy_rate);
                command.Parameters.AddWithValue("@all_lifesteal_rate", cardGenerals.lifesteal_rate);
                command.Parameters.AddWithValue("@all_shield_strength", cardGenerals.shield_strength);
                command.Parameters.AddWithValue("@all_tenacity", cardGenerals.tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", cardGenerals.resistance_rate);
                command.Parameters.AddWithValue("@all_combo_rate", cardGenerals.combo_rate);
                command.Parameters.AddWithValue("@all_ignore_combo_rate", cardGenerals.ignore_combo_rate);
                command.Parameters.AddWithValue("@all_combo_damage_rate", cardGenerals.combo_damage_rate);
                command.Parameters.AddWithValue("@all_combo_resistance_rate", cardGenerals.combo_resistance_rate);
                command.Parameters.AddWithValue("@all_stun_rate", cardGenerals.stun_rate);
                command.Parameters.AddWithValue("@all_ignore_stun_rate", cardGenerals.ignore_stun_rate);
                command.Parameters.AddWithValue("@all_reflection_rate", cardGenerals.reflection_rate);
                command.Parameters.AddWithValue("@all_ignore_reflection_rate", cardGenerals.ignore_reflection_rate);
                command.Parameters.AddWithValue("@all_reflection_damage_rate", cardGenerals.reflection_damage_rate);
                command.Parameters.AddWithValue("@all_reflection_resistance_rate", cardGenerals.reflection_resistance_rate);
                command.Parameters.AddWithValue("@all_mana", cardGenerals.mana);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", cardGenerals.mana_regeneration_rate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", cardGenerals.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", cardGenerals.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", cardGenerals.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", cardGenerals.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_normal_damage_rate", cardGenerals.normal_damage_rate);
                command.Parameters.AddWithValue("@all_normal_resistance_rate", cardGenerals.normal_resistance_rate);
                command.Parameters.AddWithValue("@all_skill_damage_rate", cardGenerals.skill_damage_rate);
                command.Parameters.AddWithValue("@all_skill_resistance_rate", cardGenerals.skill_resistance_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public CardGenerals GetUserCardGeneralsById(string user_id, string Id)
    {
        CardGenerals card = new CardGenerals();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_card_generals where user_card_generals.card_general_id=@id 
                and user_card_generals.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new CardGenerals
                    {
                        id = reader.GetString("card_general_id"),
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
                        critical_resistance_rate = reader.GetDouble("critical_resistance_rate"),
                        ignore_critical_rate = reader.GetDouble("ignore_critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate"),
                        absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        ignore_combo_rate = reader.GetDouble("ignore_combo_rate"),
                        combo_damage_rate = reader.GetDouble("combo_damage_rate"),
                        combo_resistance_rate = reader.GetDouble("combo_resistance_rate"),
                        stun_rate = reader.GetDouble("stun_rate"),
                        ignore_stun_rate = reader.GetDouble("ignore_stun_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate"),
                        reflection_damage_rate = reader.GetDouble("reflection_damage_rate"),
                        reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        normal_damage_rate = reader.GetDouble("normal_damage_rate"),
                        normal_resistance_rate = reader.GetDouble("normal_resistance_rate"),
                        skill_damage_rate = reader.GetDouble("skill_damage_rate"),
                        skill_resistance_rate = reader.GetDouble("skill_resistance_rate"),
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
    public List<CardGenerals> GetAllUserCardGeneralsInTeam(string user_id)
    {
        List<CardGenerals> cardGenerals = new List<CardGenerals>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = @"SELECT uc.*, c.*, fch.*
                FROM user_card_generals uc
                LEFT JOIN card_generals c ON uc.card_general_id = c.id 
                LEFT JOIN fact_card_generals fch ON fch.user_id = uc.user_id AND fch.user_card_general_id = uc.card_general_id
                WHERE uc.user_id = @user_id and fch.team_id IS NOT null";
            MySqlCommand command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardGenerals generals = new CardGenerals
                {
                    id = reader.GetString("card_general_id"),
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
                    team_id = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetString("team_id"),
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
                    critical_resistance_rate = reader.GetDouble("critical_resistance_rate"),
                    ignore_critical_rate = reader.GetDouble("ignore_critical_rate"),
                    penetration_rate = reader.GetDouble("penetration_rate"),
                    penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate"),
                    evasion_rate = reader.GetDouble("evasion_rate"),
                    damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                    ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate"),
                    absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate"),
                    vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                    vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                    accuracy_rate = reader.GetDouble("accuracy_rate"),
                    lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                    shield_strength = reader.GetDouble("shield_strength"),
                    tenacity = reader.GetDouble("tenacity"),
                    resistance_rate = reader.GetDouble("resistance_rate"),
                    combo_rate = reader.GetDouble("combo_rate"),
                    ignore_combo_rate = reader.GetDouble("ignore_combo_rate"),
                    combo_damage_rate = reader.GetDouble("combo_damage_rate"),
                    combo_resistance_rate = reader.GetDouble("combo_resistance_rate"),
                    stun_rate = reader.GetDouble("stun_rate"),
                    ignore_stun_rate = reader.GetDouble("ignore_stun_rate"),
                    reflection_rate = reader.GetDouble("reflection_rate"),
                    ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate"),
                    reflection_damage_rate = reader.GetDouble("reflection_damage_rate"),
                    reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate"),
                    mana = reader.GetFloat("mana"),
                    mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                    damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                    resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                    damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                    resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                    normal_damage_rate = reader.GetDouble("normal_damage_rate"),
                    normal_resistance_rate = reader.GetDouble("normal_resistance_rate"),
                    skill_damage_rate = reader.GetDouble("skill_damage_rate"),
                    skill_resistance_rate = reader.GetDouble("skill_resistance_rate"),
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
                    all_critical_resistance_rate = reader.GetDouble("all_critical_resistance_rate"),
                    all_ignore_critical_rate = reader.GetDouble("all_ignore_critical_rate"),
                    all_penetration_rate = reader.GetDouble("all_penetration_rate"),
                    all_penetration_resistance_rate = reader.GetDouble("all_penetration_resistance_rate"),
                    all_evasion_rate = reader.GetDouble("all_evasion_rate"),
                    all_damage_absorption_rate = reader.GetDouble("all_damage_absorption_rate"),
                    all_ignore_damage_absorption_rate = reader.GetDouble("all_ignore_damage_absorption_rate"),
                    all_absorbed_damage_rate = reader.GetDouble("all_absorbed_damage_rate"),
                    all_vitality_regeneration_rate = reader.GetDouble("all_vitality_regeneration_rate"),
                    all_vitality_regeneration_resistance_rate = reader.GetDouble("all_vitality_regeneration_resistance_rate"),
                    all_accuracy_rate = reader.GetDouble("all_accuracy_rate"),
                    all_lifesteal_rate = reader.GetDouble("all_lifesteal_rate"),
                    all_shield_strength = reader.GetDouble("all_shield_strength"),
                    all_tenacity = reader.GetDouble("all_tenacity"),
                    all_resistance_rate = reader.GetDouble("all_resistance_rate"),
                    all_combo_rate = reader.GetDouble("all_combo_rate"),
                    all_ignore_combo_rate = reader.GetDouble("all_ignore_combo_rate"),
                    all_combo_damage_rate = reader.GetDouble("all_combo_damage_rate"),
                    all_combo_resistance_rate = reader.GetDouble("all_combo_resistance_rate"),
                    all_stun_rate = reader.GetDouble("all_stun_rate"),
                    all_ignore_stun_rate = reader.GetDouble("all_ignore_stun_rate"),
                    all_reflection_rate = reader.GetDouble("all_reflection_rate"),
                    all_ignore_reflection_rate = reader.GetDouble("all_ignore_reflection_rate"),
                    all_reflection_damage_rate = reader.GetDouble("all_reflection_damage_rate"),
                    all_reflection_resistance_rate = reader.GetDouble("all_reflection_resistance_rate"),
                    all_mana = reader.GetFloat("all_mana"),
                    all_mana_regeneration_rate = reader.GetDouble("all_mana_regeneration_rate"),
                    all_damage_to_different_faction_rate = reader.GetDouble("all_damage_to_different_faction_rate"),
                    all_resistance_to_different_faction_rate = reader.GetDouble("all_resistance_to_different_faction_rate"),
                    all_damage_to_same_faction_rate = reader.GetDouble("all_damage_to_same_faction_rate"),
                    all_resistance_to_same_faction_rate = reader.GetDouble("all_resistance_to_same_faction_rate"),
                    all_normal_damage_rate = reader.GetDouble("all_normal_damage_rate"),
                    all_normal_resistance_rate = reader.GetDouble("all_normal_resistance_rate"),
                    all_skill_damage_rate = reader.GetDouble("all_skill_damage_rate"),
                    all_skill_resistance_rate = reader.GetDouble("all_skill_resistance_rate"),
                };

                cardGenerals.Add(generals);
            }
        }
        return cardGenerals;
    }
}