using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
public class UserBooksRepository : IUserBooksRepository
{
    public List<Books> GetUserBooks(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<Books> bookslist = new List<Books>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT ub.*, b.name, b.image, b.type, b.description
                FROM user_books ub
                LEFT JOIN books b ON ub.book_id = b.id 
                WHERE ub.user_id = @userId AND b.type = @type AND (@rare = 'All' or b.rare = @rare)
                ORDER BY b.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(b.name, '[0-9]+$') AS UNSIGNED), b.name
                LIMIT @limit OFFSET @offset;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@rare", rare);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Books book = new Books
                    {
                        id = reader.GetString("book_id"),
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

                    bookslist.Add(book);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return bookslist;
    }
    public List<Books> GetUserBooksTeam(string teamId)
    {
        List<Books> bookslist = new List<Books>();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT ub.*, b.name, b.image, b.type, b.description
                FROM user_books ub
                LEFT JOIN books b ON ub.book_id = b.id 
                WHERE ub.user_id = @userId AND ub.team_id=@team_id
                ORDER BY b.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(b.name, '[0-9]+$') AS UNSIGNED), b.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", User.CurrentUserId);
                command.Parameters.AddWithValue("@team_id", teamId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Books book = new Books
                    {
                        id = reader.GetString("book_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
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

                    bookslist.Add(book);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return bookslist;
    }
    public Dictionary<string, int> GetUniqueBookTypesTeam(string teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = @"SELECT distinct c.type, count(c.type) as number
            FROM user_card_books uc
            LEFT JOIN books c ON uc.book_id = c.id 
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
    public int GetUserBooksCount(string user_id, string type, string rare)
    {
        int count = 0;
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select count(*) from books b, user_books ub 
                where b.id=ub.book_id and ub.user_id=@userId and type= @type AND (@rare = 'All' or b.rare = @rare)";
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
    public bool InsertUserBooks(Books books)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_books 
                WHERE user_id = @user_id AND book_id = @book_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@book_id", books.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_books (
                    user_id, book_id, rare, level, experiment, star, quality, block, quantity,
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
                    @user_id, @book_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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
                    command.Parameters.AddWithValue("@book_id", books.id);
                    command.Parameters.AddWithValue("@rare", books.rare);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(books.rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", books.quantity);
                    command.Parameters.AddWithValue("@power", books.power);
                    command.Parameters.AddWithValue("@health", books.health);
                    command.Parameters.AddWithValue("@physical_attack", books.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", books.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", books.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", books.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", books.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", books.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", books.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", books.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", books.mental_attack);
                    command.Parameters.AddWithValue("@mental_defense", books.mental_defense);
                    command.Parameters.AddWithValue("@speed", books.speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", books.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", books.critical_rate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", books.critical_resistance_rate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", books.ignore_critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", books.penetration_rate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", books.penetration_resistance_rate);
                    command.Parameters.AddWithValue("@evasion_rate", books.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", books.damage_absorption_rate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", books.ignore_damage_absorption_rate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", books.absorbed_damage_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", books.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", books.vitality_regeneration_resistance_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", books.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", books.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", books.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", books.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", books.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", books.combo_rate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", books.ignore_combo_rate);
                    command.Parameters.AddWithValue("@combo_damage_rate", books.combo_damage_rate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", books.combo_resistance_rate);
                    command.Parameters.AddWithValue("@stun_rate", books.stun_rate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", books.ignore_stun_rate);
                    command.Parameters.AddWithValue("@reflection_rate", books.reflection_rate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", books.ignore_reflection_rate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", books.reflection_damage_rate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", books.reflection_resistance_rate);
                    command.Parameters.AddWithValue("@mana", books.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", books.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", books.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", books.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", books.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", books.resistance_to_same_faction_rate);
                    command.Parameters.AddWithValue("@normal_damage_rate", books.normal_damage_rate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", books.normal_resistance_rate);
                    command.Parameters.AddWithValue("@skill_damage_rate", books.skill_damage_rate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", books.skill_resistance_rate);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_books
                    SET quantity = @quantity
                    WHERE user_id = @user_id AND book_id = @book_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@book_id", books.id);
                    updateCommand.Parameters.AddWithValue("@quantity", books.quantity);

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
    public bool UpdateBooksLevel(Books books, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_books
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
                WHERE user_id = @user_id AND book_id = @book_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@book_id", books.id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", books.power);
                command.Parameters.AddWithValue("@health", books.health);
                command.Parameters.AddWithValue("@physical_attack", books.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", books.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", books.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", books.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", books.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", books.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", books.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", books.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", books.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", books.mental_defense);
                command.Parameters.AddWithValue("@speed", books.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", books.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", books.critical_rate);
                command.Parameters.AddWithValue("@critical_resistance_rate", books.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", books.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", books.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", books.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", books.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", books.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", books.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", books.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", books.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", books.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", books.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", books.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", books.shield_strength);
                command.Parameters.AddWithValue("@tenacity", books.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", books.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", books.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", books.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", books.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", books.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", books.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", books.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", books.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", books.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", books.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", books.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", books.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", books.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", books.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", books.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", books.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", books.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", books.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", books.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", books.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", books.skill_resistance_rate);
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
    public bool UpdateBooksBreakthrough(Books books, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_books
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
                WHERE user_id = @user_id AND book_id = @book_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@book_id", books.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", books.power);
                command.Parameters.AddWithValue("@health", books.health);
                command.Parameters.AddWithValue("@physical_attack", books.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", books.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", books.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", books.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", books.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", books.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", books.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", books.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", books.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", books.mental_defense);
                command.Parameters.AddWithValue("@speed", books.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", books.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", books.critical_rate);
                command.Parameters.AddWithValue("@critical_resistance_rate", books.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", books.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", books.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", books.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", books.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", books.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", books.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", books.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", books.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", books.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", books.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", books.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", books.shield_strength);
                command.Parameters.AddWithValue("@tenacity", books.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", books.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", books.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", books.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", books.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", books.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", books.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", books.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", books.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", books.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", books.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", books.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", books.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", books.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", books.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", books.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", books.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", books.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", books.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", books.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", books.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", books.skill_resistance_rate);
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
    public bool UpdateTeamBooks(string team_id, string position, string book_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                Update user_books set team_id=@team_id, position=@position where user_id=@user_id 
                and book_id=@book_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@book_id", book_id);
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
    public Books GetUserBooksById(string user_id, string Id)
    {
        Books card = new Books();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_books where user_books.book_id=@id 
                and user_books.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new Books
                    {
                        id = reader.GetString("book_id"),
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
    public List<Books> GetAllUserBooksInTeam(string user_id)
    {
        List<Books> books = new List<Books>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = @"SELECT uc.*, c.name, c.image, c.type, c.description
                FROM user_books uc
                LEFT JOIN books c ON uc.book_id = c.id 
                WHERE uc.user_id = @user_id and uc.team_id IS NOT null";
            MySqlCommand command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Books book = new Books
                {
                    id = reader.GetString("book_id"),
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

                books.Add(book);
            }
        }
        return books;
    }
}