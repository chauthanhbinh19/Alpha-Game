using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserCardMonstersRepository : IUserCardMonstersRepository
{
    public List<CardMonsters> GetUserCardMonsters(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<CardMonsters> CardMonstersList = new List<CardMonsters>();
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT um.*, m.name, m.image, m.type, m.description
                FROM user_card_monsters um
                LEFT JOIN card_monsters m ON um.card_monster_id = m.id
                WHERE um.user_id = @userId AND m.type = @type AND (@rare = 'All' or m.rare = @rare)
                ORDER BY m.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name
                LIMIT @limit OFFSET @offset;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@rare", rare);
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
                string query = @"SELECT um.*, m.name, m.image, m.type, m.description
                FROM user_card_monsters um
                LEFT JOIN card_monsters m ON um.card_monster_id = m.id
                WHERE um.user_id = @userId and um.team_id=@team_id and SUBSTRING_INDEX(um.position, '-', 1) = @position
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
            WHERE uc.user_id =@userId and uc.team_id=@team_id
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
    public int GetUserCardMonstersCount(string user_id, string type, string rare)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select count(*) from card_monsters m, user_card_monsters um 
                where m.id=um.card_monster_id and um.user_id=@userId and m.type= @type AND (@rare = 'All' or m.rare = @rare)";
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
                string query = @"select count(*) from user_card_monsters
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
    public int GetUserCardMonstersTeamsCount(string user_id, string team_id)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*) from user_card_monsters
                where team_id = @team_id and user_id=@userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@team_id", team_id);
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
                INSERT INTO user_card_monsters(
                    user_id, card_monster_id, rare, level, experiment, star, quality, block, quantity,
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
                    @user_id, @card_monster_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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
                    command.Parameters.AddWithValue("@card_monster_id", CardMonsters.id);
                    command.Parameters.AddWithValue("@rare", CardMonsters.rare);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(CardMonsters.rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", CardMonsters.quantity);
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
                    command.Parameters.AddWithValue("@critical_resistance_rate", CardMonsters.critical_resistance_rate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", CardMonsters.ignore_critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", CardMonsters.penetration_rate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", CardMonsters.penetration_resistance_rate);
                    command.Parameters.AddWithValue("@evasion_rate", CardMonsters.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", CardMonsters.damage_absorption_rate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", CardMonsters.ignore_damage_absorption_rate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", CardMonsters.absorbed_damage_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", CardMonsters.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", CardMonsters.vitality_regeneration_resistance_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", CardMonsters.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", CardMonsters.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", CardMonsters.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", CardMonsters.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", CardMonsters.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", CardMonsters.combo_rate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", CardMonsters.ignore_combo_rate);
                    command.Parameters.AddWithValue("@combo_damage_rate", CardMonsters.combo_damage_rate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", CardMonsters.combo_resistance_rate);
                    command.Parameters.AddWithValue("@stun_rate", CardMonsters.stun_rate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", CardMonsters.ignore_stun_rate);
                    command.Parameters.AddWithValue("@reflection_rate", CardMonsters.reflection_rate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", CardMonsters.ignore_reflection_rate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", CardMonsters.reflection_damage_rate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", CardMonsters.reflection_resistance_rate);
                    command.Parameters.AddWithValue("@mana", CardMonsters.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", CardMonsters.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", CardMonsters.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", CardMonsters.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", CardMonsters.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", CardMonsters.resistance_to_same_faction_rate);
                    command.Parameters.AddWithValue("@normal_damage_rate", CardMonsters.normal_damage_rate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", CardMonsters.normal_resistance_rate);
                    command.Parameters.AddWithValue("@skill_damage_rate", CardMonsters.skill_damage_rate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", CardMonsters.skill_resistance_rate);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_card_monsters
                    SET quantity = @quantity
                    WHERE user_id = @user_id AND card_monster_id = @card_monster_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@card_monster_id", CardMonsters.id);
                    updateCommand.Parameters.AddWithValue("@quantity", CardMonsters.quantity);

                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
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
                command.Parameters.AddWithValue("@critical_resistance_rate", cardMonsters.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", cardMonsters.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", cardMonsters.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", cardMonsters.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", cardMonsters.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardMonsters.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardMonsters.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", cardMonsters.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardMonsters.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardMonsters.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", cardMonsters.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardMonsters.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", cardMonsters.shield_strength);
                command.Parameters.AddWithValue("@tenacity", cardMonsters.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardMonsters.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", cardMonsters.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", cardMonsters.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", cardMonsters.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", cardMonsters.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", cardMonsters.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", cardMonsters.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", cardMonsters.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", cardMonsters.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", cardMonsters.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", cardMonsters.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", cardMonsters.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardMonsters.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardMonsters.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardMonsters.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardMonsters.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardMonsters.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", cardMonsters.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", cardMonsters.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", cardMonsters.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", cardMonsters.skill_resistance_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
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
                command.Parameters.AddWithValue("@critical_resistance_rate", cardMonsters.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", cardMonsters.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", cardMonsters.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", cardMonsters.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", cardMonsters.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardMonsters.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardMonsters.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", cardMonsters.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardMonsters.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardMonsters.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", cardMonsters.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardMonsters.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", cardMonsters.shield_strength);
                command.Parameters.AddWithValue("@tenacity", cardMonsters.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardMonsters.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", cardMonsters.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", cardMonsters.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", cardMonsters.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", cardMonsters.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", cardMonsters.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", cardMonsters.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", cardMonsters.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", cardMonsters.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", cardMonsters.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", cardMonsters.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", cardMonsters.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardMonsters.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardMonsters.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardMonsters.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardMonsters.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardMonsters.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", cardMonsters.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", cardMonsters.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", cardMonsters.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", cardMonsters.skill_resistance_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
        }
        return true;
    }
    public bool UpdateTeamCardMonsters(string team_id, string position, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                Update user_card_monsters set team_id=@team_id, position=@position where user_id=@user_id 
                and card_monster_id=@card_monster_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_monster_id", card_id);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
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
                string query = @"Select uc.*, c.image
                from user_card_monsters uc, card_monsters c
                where uc.card_monster_id=@id 
                and uc.card_monster_id = c.id
                and uc.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new CardMonsters
                    {
                        id = reader.GetString("card_monster_id"),
                        image = reader.GetString("image"),
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
    public List<CardMonsters> GetAllUserCardMonstersInTeam(string user_id)
    {
        List<CardMonsters> cardMonsters = new List<CardMonsters>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = @"SELECT uc.*, c.name, c.image, c.type, c.description
                FROM user_card_monsters uc
                LEFT JOIN card_monsters c ON uc.card_monster_id = c.id 
                WHERE uc.user_id = @user_id and uc.team_id IS NOT null";
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
                };

                cardMonsters.Add(monsters);
            }
        }
        return cardMonsters;
    }
}