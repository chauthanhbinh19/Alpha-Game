using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserCardCaptainsRepository : IUserCardCaptainsRepository
{
    public List<CardCaptains> GetUserCardCaptains(string user_id, string type, int pageSize, int offset)
    {
        List<CardCaptains> CardCaptainsList = new List<CardCaptains>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT uc.*, c.*, fcc.*
                FROM user_card_captains uc
                LEFT JOIN card_captains c ON c.id = uc.card_captain_id 
                LEFT JOIN fact_card_captains fcc ON fcc.user_id = uc.user_id AND fcc.user_card_captain_id = uc.card_captain_id
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
                    CardCaptains captain = new CardCaptains
                    {
                        id = reader.GetString("card_captain_id"),
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

                    CardCaptainsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardCaptainsList;
    }
    public List<CardCaptains> GetUserCardCaptainsTeam(string user_id, string teamId, string position)
    {
        List<CardCaptains> CardCaptainsList = new List<CardCaptains>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT uc.*, c.*, fcc.*
                FROM user_card_captains uc
                LEFT JOIN card_captains c ON c.id = uc.card_captain_id 
                LEFT JOIN fact_card_captains fcc ON fcc.user_id = uc.user_id AND fcc.user_card_captain_id = uc.card_captain_id
                WHERE uc.user_id = @userId AND fcc.team_id=@team_id AND SUBSTRING_INDEX(fcc.position, '-', 1) = @position
                ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@team_id", teamId);
                command.Parameters.AddWithValue("@position", position);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardCaptains captain = new CardCaptains
                    {
                        id = reader.GetString("card_captain_id"),
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

                    CardCaptainsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardCaptainsList;
    }
    public Dictionary<string, int> GetUniqueCardCaptainTypesTeam(string teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = @"SELECT distinct c.type, count(c.type) as number
            FROM user_card_captains uc
            LEFT JOIN card_captains c ON uc.card_captain_id = c.id 
            LEFT JOIN fact_card_captains fch ON fch.user_id = uc.user_id AND fch.user_card_captain_id = uc.card_captain_id
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
    public bool UpdateTeamFactCardCaptains(string team_id, string position, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                Update fact_card_captains set team_id=@team_id, position=@position where user_id=@user_id 
                and user_card_captain_id=@user_card_captain_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_captain_id", card_id);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public int GetUserCardCaptainsCount(string user_id, string type)
    {
        int count = 0;
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from card_captains c, user_card_captains uc where c.id=uc.card_captain_id and uc.user_id=@userId and c.type= @type";
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
    public int GetUserCardCaptainsTeamsPositionCount(string user_id, string team_id, string position)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*) from fact_card_captains
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
    public bool InsertUserCardCaptains(CardCaptains CardCaptains)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_captains
                WHERE user_id = @user_id AND card_captain_id = @card_captain_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_captain_id", CardCaptains.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_card_captains (
                    user_id, card_captain_id, level, experiment, star, quality, block, quantity,
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
                    @user_id, @card_captain_id, @level, @experiment, @star, @quality, @block, @quantity,
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
                    command.Parameters.AddWithValue("@card_captain_id", CardCaptains.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(CardCaptains.rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", 0);
                    command.Parameters.AddWithValue("@power", CardCaptains.power);
                    command.Parameters.AddWithValue("@health", CardCaptains.health);
                    command.Parameters.AddWithValue("@physical_attack", CardCaptains.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", CardCaptains.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", CardCaptains.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", CardCaptains.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", CardCaptains.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", CardCaptains.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", CardCaptains.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", CardCaptains.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", CardCaptains.mental_attack);
                    command.Parameters.AddWithValue("@mental_defense", CardCaptains.mental_defense);
                    command.Parameters.AddWithValue("@speed", CardCaptains.speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", CardCaptains.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", CardCaptains.critical_rate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", CardCaptains.critical_resistance_rate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", CardCaptains.ignore_critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", CardCaptains.penetration_rate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", CardCaptains.penetration_resistance_rate);
                    command.Parameters.AddWithValue("@evasion_rate", CardCaptains.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", CardCaptains.damage_absorption_rate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", CardCaptains.ignore_damage_absorption_rate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", CardCaptains.absorbed_damage_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", CardCaptains.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", CardCaptains.vitality_regeneration_resistance_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", CardCaptains.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", CardCaptains.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", CardCaptains.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", CardCaptains.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", CardCaptains.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", CardCaptains.combo_rate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", CardCaptains.ignore_combo_rate);
                    command.Parameters.AddWithValue("@combo_damage_rate", CardCaptains.combo_damage_rate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", CardCaptains.combo_resistance_rate);
                    command.Parameters.AddWithValue("@stun_rate", CardCaptains.stun_rate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", CardCaptains.ignore_stun_rate);
                    command.Parameters.AddWithValue("@reflection_rate", CardCaptains.reflection_rate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", CardCaptains.ignore_reflection_rate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", CardCaptains.reflection_damage_rate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", CardCaptains.reflection_resistance_rate);
                    command.Parameters.AddWithValue("@mana", CardCaptains.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", CardCaptains.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", CardCaptains.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", CardCaptains.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", CardCaptains.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", CardCaptains.resistance_to_same_faction_rate);
                    command.Parameters.AddWithValue("@normal_damage_rate", CardCaptains.normal_damage_rate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", CardCaptains.normal_resistance_rate);
                    command.Parameters.AddWithValue("@skill_damage_rate", CardCaptains.skill_damage_rate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", CardCaptains.skill_resistance_rate);
                    MySqlDataReader reader = command.ExecuteReader();
                    InsertFactCardCaptains(CardCaptains);
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_card_captains
                    SET quantity = quantity + 1
                    WHERE user_id = @user_id AND card_captain_id = @card_captain_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@card_captain_id", CardCaptains.id);

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
    public bool UpdateCardCaptainsLevel(CardCaptains cardCaptains, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_captains
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
                WHERE user_id = @user_id AND card_captain_id = @card_captain_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_captain_id", cardCaptains.id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", cardCaptains.power);
                command.Parameters.AddWithValue("@health", cardCaptains.health);
                command.Parameters.AddWithValue("@physical_attack", cardCaptains.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", cardCaptains.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", cardCaptains.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", cardCaptains.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", cardCaptains.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", cardCaptains.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", cardCaptains.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", cardCaptains.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", cardCaptains.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", cardCaptains.mental_defense);
                command.Parameters.AddWithValue("@speed", cardCaptains.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", cardCaptains.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", cardCaptains.critical_rate);
                command.Parameters.AddWithValue("@critical_resistance_rate", cardCaptains.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", cardCaptains.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", cardCaptains.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", cardCaptains.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", cardCaptains.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardCaptains.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardCaptains.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", cardCaptains.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardCaptains.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardCaptains.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", cardCaptains.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardCaptains.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", cardCaptains.shield_strength);
                command.Parameters.AddWithValue("@tenacity", cardCaptains.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardCaptains.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", cardCaptains.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", cardCaptains.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", cardCaptains.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", cardCaptains.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", cardCaptains.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", cardCaptains.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", cardCaptains.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", cardCaptains.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", cardCaptains.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", cardCaptains.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", cardCaptains.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardCaptains.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardCaptains.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardCaptains.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardCaptains.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardCaptains.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", cardCaptains.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", cardCaptains.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", cardCaptains.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", cardCaptains.skill_resistance_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateCardCaptainsBreakthrough(CardCaptains cardCaptains, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_captains
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
                WHERE user_id = @user_id AND card_captain_id = @card_captain_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_captain_id", cardCaptains.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", cardCaptains.power);
                command.Parameters.AddWithValue("@health", cardCaptains.health);
                command.Parameters.AddWithValue("@physical_attack", cardCaptains.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", cardCaptains.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", cardCaptains.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", cardCaptains.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", cardCaptains.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", cardCaptains.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", cardCaptains.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", cardCaptains.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", cardCaptains.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", cardCaptains.mental_defense);
                command.Parameters.AddWithValue("@speed", cardCaptains.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", cardCaptains.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", cardCaptains.critical_rate);
                command.Parameters.AddWithValue("@critical_resistance_rate", cardCaptains.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", cardCaptains.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", cardCaptains.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", cardCaptains.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", cardCaptains.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardCaptains.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardCaptains.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", cardCaptains.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardCaptains.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardCaptains.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", cardCaptains.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardCaptains.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", cardCaptains.shield_strength);
                command.Parameters.AddWithValue("@tenacity", cardCaptains.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardCaptains.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", cardCaptains.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", cardCaptains.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", cardCaptains.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", cardCaptains.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", cardCaptains.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", cardCaptains.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", cardCaptains.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", cardCaptains.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", cardCaptains.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", cardCaptains.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", cardCaptains.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardCaptains.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardCaptains.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardCaptains.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardCaptains.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardCaptains.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", cardCaptains.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", cardCaptains.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", cardCaptains.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", cardCaptains.skill_resistance_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool InsertFactCardCaptains(CardCaptains cardCaptains)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                INSERT INTO fact_card_captains (
                user_id, user_card_captain_id, team_id, position, role, 
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
                @user_id, @user_card_captain_id, @team_id, @position, @role, 
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
                command.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.id);
                command.Parameters.AddWithValue("@team_id", null);
                command.Parameters.AddWithValue("@position", null);
                command.Parameters.AddWithValue("@role", null);
                command.Parameters.AddWithValue("@all_power", cardCaptains.power);
                command.Parameters.AddWithValue("@all_health", cardCaptains.health);
                command.Parameters.AddWithValue("@all_physical_attack", cardCaptains.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", cardCaptains.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", cardCaptains.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", cardCaptains.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", cardCaptains.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", cardCaptains.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", cardCaptains.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", cardCaptains.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", cardCaptains.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", cardCaptains.mental_defense);
                command.Parameters.AddWithValue("@all_speed", cardCaptains.speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", cardCaptains.critical_damage_rate);
                command.Parameters.AddWithValue("@all_critical_rate", cardCaptains.critical_rate);
                command.Parameters.AddWithValue("@all_critical_resistance_rate", cardCaptains.critical_resistance_rate);
                command.Parameters.AddWithValue("@all_ignore_critical_rate", cardCaptains.ignore_critical_rate);
                command.Parameters.AddWithValue("@all_penetration_rate", cardCaptains.penetration_rate);
                command.Parameters.AddWithValue("@all_penetration_resistance_rate", cardCaptains.penetration_resistance_rate);
                command.Parameters.AddWithValue("@all_evasion_rate", cardCaptains.evasion_rate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", cardCaptains.damage_absorption_rate);
                command.Parameters.AddWithValue("@all_ignore_damage_absorption_rate", cardCaptains.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@all_absorbed_damage_rate", cardCaptains.absorbed_damage_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", cardCaptains.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_resistance_rate", cardCaptains.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@all_accuracy_rate", cardCaptains.accuracy_rate);
                command.Parameters.AddWithValue("@all_lifesteal_rate", cardCaptains.lifesteal_rate);
                command.Parameters.AddWithValue("@all_shield_strength", cardCaptains.shield_strength);
                command.Parameters.AddWithValue("@all_tenacity", cardCaptains.tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", cardCaptains.resistance_rate);
                command.Parameters.AddWithValue("@all_combo_rate", cardCaptains.combo_rate);
                command.Parameters.AddWithValue("@all_ignore_combo_rate", cardCaptains.ignore_combo_rate);
                command.Parameters.AddWithValue("@all_combo_damage_rate", cardCaptains.combo_damage_rate);
                command.Parameters.AddWithValue("@all_combo_resistance_rate", cardCaptains.combo_resistance_rate);
                command.Parameters.AddWithValue("@all_stun_rate", cardCaptains.stun_rate);
                command.Parameters.AddWithValue("@all_ignore_stun_rate", cardCaptains.ignore_stun_rate);
                command.Parameters.AddWithValue("@all_reflection_rate", cardCaptains.reflection_rate);
                command.Parameters.AddWithValue("@all_ignore_reflection_rate", cardCaptains.ignore_reflection_rate);
                command.Parameters.AddWithValue("@all_reflection_damage_rate", cardCaptains.reflection_damage_rate);
                command.Parameters.AddWithValue("@all_reflection_resistance_rate", cardCaptains.reflection_resistance_rate);
                command.Parameters.AddWithValue("@all_mana", cardCaptains.mana);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", cardCaptains.mana_regeneration_rate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", cardCaptains.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", cardCaptains.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", cardCaptains.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", cardCaptains.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_normal_damage_rate", cardCaptains.normal_damage_rate);
                command.Parameters.AddWithValue("@all_normal_resistance_rate", cardCaptains.normal_resistance_rate);
                command.Parameters.AddWithValue("@all_skill_damage_rate", cardCaptains.skill_damage_rate);
                command.Parameters.AddWithValue("@all_skill_resistance_rate", cardCaptains.skill_resistance_rate);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateFactCardCaptains(CardCaptains cardCaptains)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE fact_card_captains
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
                WHERE user_id = @user_id AND user_card_captain_id = @user_card_captain_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.id);
                command.Parameters.AddWithValue("@all_power", cardCaptains.power);
                command.Parameters.AddWithValue("@all_health", cardCaptains.health);
                command.Parameters.AddWithValue("@all_physical_attack", cardCaptains.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", cardCaptains.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", cardCaptains.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", cardCaptains.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", cardCaptains.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", cardCaptains.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", cardCaptains.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", cardCaptains.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", cardCaptains.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", cardCaptains.mental_defense);
                command.Parameters.AddWithValue("@all_speed", cardCaptains.speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", cardCaptains.critical_damage_rate);
                command.Parameters.AddWithValue("@all_critical_rate", cardCaptains.critical_rate);
                command.Parameters.AddWithValue("@all_critical_resistance_rate", cardCaptains.critical_resistance_rate);
                command.Parameters.AddWithValue("@all_ignore_critical_rate", cardCaptains.ignore_critical_rate);
                command.Parameters.AddWithValue("@all_penetration_rate", cardCaptains.penetration_rate);
                command.Parameters.AddWithValue("@all_penetration_resistance_rate", cardCaptains.penetration_resistance_rate);
                command.Parameters.AddWithValue("@all_evasion_rate", cardCaptains.evasion_rate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", cardCaptains.damage_absorption_rate);
                command.Parameters.AddWithValue("@all_ignore_damage_absorption_rate", cardCaptains.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@all_absorbed_damage_rate", cardCaptains.absorbed_damage_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", cardCaptains.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_resistance_rate", cardCaptains.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@all_accuracy_rate", cardCaptains.accuracy_rate);
                command.Parameters.AddWithValue("@all_lifesteal_rate", cardCaptains.lifesteal_rate);
                command.Parameters.AddWithValue("@all_shield_strength", cardCaptains.shield_strength);
                command.Parameters.AddWithValue("@all_tenacity", cardCaptains.tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", cardCaptains.resistance_rate);
                command.Parameters.AddWithValue("@all_combo_rate", cardCaptains.combo_rate);
                command.Parameters.AddWithValue("@all_ignore_combo_rate", cardCaptains.ignore_combo_rate);
                command.Parameters.AddWithValue("@all_combo_damage_rate", cardCaptains.combo_damage_rate);
                command.Parameters.AddWithValue("@all_combo_resistance_rate", cardCaptains.combo_resistance_rate);
                command.Parameters.AddWithValue("@all_stun_rate", cardCaptains.stun_rate);
                command.Parameters.AddWithValue("@all_ignore_stun_rate", cardCaptains.ignore_stun_rate);
                command.Parameters.AddWithValue("@all_reflection_rate", cardCaptains.reflection_rate);
                command.Parameters.AddWithValue("@all_ignore_reflection_rate", cardCaptains.ignore_reflection_rate);
                command.Parameters.AddWithValue("@all_reflection_damage_rate", cardCaptains.reflection_damage_rate);
                command.Parameters.AddWithValue("@all_reflection_resistance_rate", cardCaptains.reflection_resistance_rate);
                command.Parameters.AddWithValue("@all_mana", cardCaptains.mana);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", cardCaptains.mana_regeneration_rate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", cardCaptains.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", cardCaptains.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", cardCaptains.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", cardCaptains.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_normal_damage_rate", cardCaptains.normal_damage_rate);
                command.Parameters.AddWithValue("@all_normal_resistance_rate", cardCaptains.normal_resistance_rate);
                command.Parameters.AddWithValue("@all_skill_damage_rate", cardCaptains.skill_damage_rate);
                command.Parameters.AddWithValue("@all_skill_resistance_rate", cardCaptains.skill_resistance_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public CardCaptains GetUserCardCaptainsById(string user_id, string Id)
    {
        CardCaptains card = new CardCaptains();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_card_captains where user_card_captains.card_captain_id=@id 
                and user_card_captains.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new CardCaptains
                    {
                        id = reader.GetString("card_captain_id"),
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
    public List<CardCaptains> GetAllUserCardCaptainsInTeam(string user_id)
    {
        List<CardCaptains> cardCaptains = new List<CardCaptains>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = @"SELECT uc.*, c.*, fch.*
                FROM user_card_captains uc
                LEFT JOIN card_captains c ON uc.card_captain_id = c.id 
                LEFT JOIN fact_card_captains fch ON fch.user_id = uc.user_id AND fch.user_card_captain_id = uc.card_captain_id
                WHERE uc.user_id = @user_id and fch.team_id IS NOT null";
            MySqlCommand command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardCaptains captain = new CardCaptains
                {
                    id = reader.GetString("card_captain_id"),
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

                cardCaptains.Add(captain);
            }
        }
        return cardCaptains;
    }
}