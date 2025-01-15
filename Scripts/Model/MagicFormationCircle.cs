using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class MagicFormationCircle
{
    public int id { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public string rare { get; set; }
    public string type { get; set; }
    public int star { get; set; }
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
    public string description { get; set; }
    public string status { get; set; }
    public Currency currency { get; set; }
    public MagicFormationCircle()
    {

    }
    public static List<string> GetUniqueMagicFormationCircleTypes()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "Select distinct type from magic_formation_circle";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                typeList.Add(reader.GetString(0));
            }
        }
        return typeList;
    }
    public List<MagicFormationCircle> GetMagicFormationCircle(string type, int pageSize, int offset)
    {
        List<MagicFormationCircle> magicFormationCircles = new List<MagicFormationCircle>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from magic_formation_circle where type =@type 
                ORDER BY magic_formation_circle.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(magic_formation_circle.name, '[0-9]+$') AS UNSIGNED), magic_formation_circle.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MagicFormationCircle magicFormationCircle = new MagicFormationCircle
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
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
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        mana = reader.GetFloat("mana"),
                        percent_all_health = reader.GetDouble("percent_all_health"),
                        percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                        description = reader.GetString("description")
                    };

                    magicFormationCircles.Add(magicFormationCircle);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return magicFormationCircles;
    }
    public int GetMagicFormationCircleCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from magic_formation_circle where type =@type";
                MySqlCommand command = new MySqlCommand(query, connection);
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
    public List<MagicFormationCircle> GetMagicFormationCircleCollection(string type, int pageSize, int offset)
    {
        List<MagicFormationCircle> magicFormationCircles = new List<MagicFormationCircle>();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT m.*, CASE WHEN mg.mfc_id IS NULL THEN 'block' WHEN mg.status = 'pending' THEN 'pending' WHEN mg.status = 'available' THEN 'available' END AS status 
                FROM magic_formation_circle m LEFT JOIN magic_formation_circle_gallery mg ON m.id = mg.mfc_id and mg.user_id = @userId where m.type=@type 
                ORDER BY m.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MagicFormationCircle magicFormationCircle = new MagicFormationCircle
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
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
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        mana = reader.GetFloat("mana"),
                        percent_all_health = reader.GetDouble("percent_all_health"),
                        percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                        description = reader.GetString("description"),
                        status = reader.GetString("status"),
                    };

                    magicFormationCircles.Add(magicFormationCircle);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return magicFormationCircles;
    }
    public List<MagicFormationCircle> GetUserMagicFormationCircle(string type, int pageSize, int offset)
    {
        List<MagicFormationCircle> magicFormationCircles = new List<MagicFormationCircle>();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select m.* from magic_formation_circle m, user_magic_formation_circle um where m.id=um.mfc_id and um.user_id=@userId and m.type=@type 
                ORDER BY m.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MagicFormationCircle magicFormationCircle = new MagicFormationCircle
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
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
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        mana = reader.GetFloat("mana"),
                        percent_all_health = reader.GetDouble("percent_all_health"),
                        percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                        description = reader.GetString("description")
                    };

                    magicFormationCircles.Add(magicFormationCircle);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return magicFormationCircles;
    }
    public int GetUserMagicFormationCircleCount(string type)
    {
        int count = 0;
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from magic_formation_circle m, user_magic_formation_circle um where m.id=um.mfc_id and um.user_id=@userId and m.type= @type";
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
    public bool InsertUserMacgicFormationCircle(MagicFormationCircle magicFormationCircle)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_magic_formation_circle 
                WHERE user_id = @user_id AND mfc_id = @mfc_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@mfc_id", magicFormationCircle.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                    INSERT INTO user_magic_formation_circle (
                        user_id, mfc_id, level, experiment, star, block, quantity, power, health, physical_attack, 
                        physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, 
                        atomic_defense, mental_attack, mental_defense, speed, critical_damage, critical_rate, 
                        armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana,
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense, percent_all_magical_attack,
                        percent_all_magical_defense, percent_all_chemical_attack, percent_all_chemical_defense,
                        percent_all_atomic_attack, percent_all_atomic_defense, percent_all_mental_attack, percent_all_mental_defense,
                        percent_all_speed, percent_all_critical_damage, percent_all_critical_rate, percent_all_armor_penetration,
                        percent_all_avoid, percent_all_absorbs_damage, percent_all_regenerate_vitality, percent_all_accuracy, percent_all_mana
                    ) VALUES (
                        @user_id, @mfc_id, @level, @experiment, @star, @block, @quantity, @power, @health, @physical_attack, 
                        @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, 
                        @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage, @critical_rate, 
                        @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana,
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, @percent_all_magical_attack,
                        @percent_all_magical_defense, @percent_all_chemical_attack, @percent_all_chemical_defense,
                        @percent_all_atomic_attack, @percent_all_atomic_defense, @percent_all_mental_attack, @percent_all_mental_defense,
                        @percent_all_speed, @percent_all_critical_damage, @percent_all_critical_rate, @percent_all_armor_penetration,
                        @percent_all_avoid, @percent_all_absorbs_damage, @percent_all_regenerate_vitality, @percent_all_accuracy, @percent_all_mana
                    );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@mfc_id", magicFormationCircle.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", 1);
                    command.Parameters.AddWithValue("@power", magicFormationCircle.power);
                    command.Parameters.AddWithValue("@health", magicFormationCircle.health);
                    command.Parameters.AddWithValue("@physical_attack", magicFormationCircle.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", magicFormationCircle.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", magicFormationCircle.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", magicFormationCircle.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", magicFormationCircle.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", magicFormationCircle.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", magicFormationCircle.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", magicFormationCircle.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", magicFormationCircle.mental_attack);
                    command.Parameters.AddWithValue("@mental_defense", magicFormationCircle.mental_defense);
                    command.Parameters.AddWithValue("@speed", magicFormationCircle.speed);
                    command.Parameters.AddWithValue("@critical_damage", magicFormationCircle.critical_damage);
                    command.Parameters.AddWithValue("@critical_rate", magicFormationCircle.critical_rate);
                    command.Parameters.AddWithValue("@armor_penetration", magicFormationCircle.armor_penetration);
                    command.Parameters.AddWithValue("@avoid", magicFormationCircle.avoid);
                    command.Parameters.AddWithValue("@absorbs_damage", magicFormationCircle.absorbs_damage);
                    command.Parameters.AddWithValue("@regenerate_vitality", magicFormationCircle.regenerate_vitality);
                    command.Parameters.AddWithValue("@accuracy", magicFormationCircle.accuracy);
                    command.Parameters.AddWithValue("@mana", magicFormationCircle.mana);

                    command.Parameters.AddWithValue("@percent_all_health", magicFormationCircle.percent_all_health);
                    command.Parameters.AddWithValue("@percent_all_physical_attack", magicFormationCircle.percent_all_physical_attack);
                    command.Parameters.AddWithValue("@percent_all_physical_defense", magicFormationCircle.percent_all_physical_defense);
                    command.Parameters.AddWithValue("@percent_all_magical_attack", magicFormationCircle.percent_all_magical_attack);
                    command.Parameters.AddWithValue("@percent_all_magical_defense", magicFormationCircle.percent_all_magical_defense);
                    command.Parameters.AddWithValue("@percent_all_chemical_attack", magicFormationCircle.percent_all_chemical_attack);
                    command.Parameters.AddWithValue("@percent_all_chemical_defense", magicFormationCircle.percent_all_chemical_defense);
                    command.Parameters.AddWithValue("@percent_all_atomic_attack", magicFormationCircle.percent_all_atomic_attack);
                    command.Parameters.AddWithValue("@percent_all_atomic_defense", magicFormationCircle.percent_all_atomic_defense);
                    command.Parameters.AddWithValue("@percent_all_mental_attack", magicFormationCircle.percent_all_mental_attack);
                    command.Parameters.AddWithValue("@percent_all_mental_defense", magicFormationCircle.percent_all_mental_defense);
                    command.Parameters.AddWithValue("@percent_all_speed", 20);
                    command.Parameters.AddWithValue("@percent_all_critical_damage", 20);
                    command.Parameters.AddWithValue("@percent_all_critical_rate", 20);
                    command.Parameters.AddWithValue("@percent_all_armor_penetration", 20);
                    command.Parameters.AddWithValue("@percent_all_avoid", 20);
                    command.Parameters.AddWithValue("@percent_all_absorbs_damage", 20);
                    command.Parameters.AddWithValue("@percent_all_regenerate_vitality", 20);
                    command.Parameters.AddWithValue("@percent_all_accuracy", 20);
                    command.Parameters.AddWithValue("@percent_all_mana", 20);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_magic_formation_circle
                    SET quantity = quantity + 1
                    WHERE user_id = @user_id AND mfc_id = @mfc_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@mfc_id", magicFormationCircle.id);

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
    public List<MagicFormationCircle> GetMagicFormationCircleWithPrice(string type, int pageSize, int offset)
    {
        List<MagicFormationCircle> magicFormationCircles = new List<MagicFormationCircle>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select m.*, mt.price, cu.image as currency_image, cu.id as currency_id
                from magic_formation_circle m, magic_formation_circle_trade mt, currency cu
                where m.id=mt.mfc_id and mt.currency_id = cu.id and m.type =@type
                ORDER BY m.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MagicFormationCircle magicFormationCircle = new MagicFormationCircle
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
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
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        mana = reader.GetFloat("mana"),
                        percent_all_health = reader.GetDouble("percent_all_health"),
                        percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                        description = reader.GetString("description")
                    };
                    magicFormationCircle.currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("price")
                    };

                    magicFormationCircles.Add(magicFormationCircle);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return magicFormationCircles;
    }
    public int GetMagicFormationCircleWithPriceCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*)
                from magic_formation_circle m, magic_formation_circle_trade mt, currency cu
                where m.id=mt.mfc_id and mt.currency_id = cu.id and m.type =@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
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
    public MagicFormationCircle GetMagicFormationCircleById(int Id)
    {
        MagicFormationCircle magicFormationCircle = new MagicFormationCircle();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from magic_formation_circle where id=@id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    magicFormationCircle = new MagicFormationCircle
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
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
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description")
                    };
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return magicFormationCircle;
    }
    public void InsertCollaborationsGallery(int Id)
    {
        MagicFormationCircle magicFormationCircleFromDB = GetMagicFormationCircleById(Id);
        int percent = 0;
        if (magicFormationCircleFromDB.rare.Equals("LG"))
        {
            percent = 20;
        }
        else if (magicFormationCircleFromDB.rare.Equals("UR"))
        {
            percent = 10;
        }
        else if (magicFormationCircleFromDB.rare.Equals("SSR"))
        {
            percent = 5;
        }
        else if (magicFormationCircleFromDB.rare.Equals("SR"))
        {
            percent = 2;
        }
        else if (magicFormationCircleFromDB.rare.Equals("MR"))
        {
            percent = 30;
        }
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra bản ghi đã tồn tại
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM magic_formation_circle_gallery 
                WHERE user_id = @user_id AND mfc_id = @mfc_id;
                ";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@mfc_id", Id);

                int recordCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (recordCount == 0)
                {
                    string query = @"
                    INSERT INTO magic_formation_circle_gallery (
                        user_id, mfc_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
                        magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                        mental_attack, mental_defense, speed, critical_damage, critical_rate, armor_penetration, avoid, 
                        absorbs_damage, regenerate_vitality, accuracy, mana, percent_all_health, percent_all_physical_attack, 
                        percent_all_physical_defense, percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, percent_all_mental_attack, 
                        percent_all_mental_defense
                    ) VALUES (
                        @user_id, @mfc_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
                        @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, 
                        @mental_attack, @mental_defense, @speed, @critical_damage, @critical_rate, @armor_penetration, @avoid, 
                        @absorbs_damage, @regenerate_vitality, @accuracy, @mana, @percent_all_health, @percent_all_physical_attack, 
                        @percent_all_physical_defense, @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, @percent_all_mental_attack, 
                        @percent_all_mental_defense
                    );
                    ";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@mfc_id", Id);
                    command.Parameters.AddWithValue("@status", "pending");
                    command.Parameters.AddWithValue("@current_star", 0);
                    command.Parameters.AddWithValue("@temp_star", 0);
                    command.Parameters.AddWithValue("@power", magicFormationCircleFromDB.power);
                    command.Parameters.AddWithValue("@health", magicFormationCircleFromDB.health);
                    command.Parameters.AddWithValue("@physical_attack", magicFormationCircleFromDB.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", magicFormationCircleFromDB.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", magicFormationCircleFromDB.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", magicFormationCircleFromDB.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", magicFormationCircleFromDB.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", magicFormationCircleFromDB.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", magicFormationCircleFromDB.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", magicFormationCircleFromDB.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", magicFormationCircleFromDB.magical_attack);
                    command.Parameters.AddWithValue("@mental_defense", magicFormationCircleFromDB.magical_defense);
                    command.Parameters.AddWithValue("@speed", magicFormationCircleFromDB.speed);
                    command.Parameters.AddWithValue("@critical_damage", magicFormationCircleFromDB.critical_damage);
                    command.Parameters.AddWithValue("@critical_rate", magicFormationCircleFromDB.critical_rate);
                    command.Parameters.AddWithValue("@armor_penetration", magicFormationCircleFromDB.armor_penetration);
                    command.Parameters.AddWithValue("@avoid", magicFormationCircleFromDB.avoid);
                    command.Parameters.AddWithValue("@absorbs_damage", magicFormationCircleFromDB.absorbs_damage);
                    command.Parameters.AddWithValue("@regenerate_vitality", magicFormationCircleFromDB.regenerate_vitality);
                    command.Parameters.AddWithValue("@accuracy", magicFormationCircleFromDB.accuracy);
                    command.Parameters.AddWithValue("@mana", magicFormationCircleFromDB.mana);
                    command.Parameters.AddWithValue("@percent_all_health", percent);
                    command.Parameters.AddWithValue("@percent_all_physical_attack", percent);
                    command.Parameters.AddWithValue("@percent_all_physical_defense", percent);
                    command.Parameters.AddWithValue("@percent_all_magical_attack", percent);
                    command.Parameters.AddWithValue("@percent_all_magical_defense", percent);
                    command.Parameters.AddWithValue("@percent_all_chemical_attack", percent);
                    command.Parameters.AddWithValue("@percent_all_chemical_defense", percent);
                    command.Parameters.AddWithValue("@percent_all_atomic_attack", percent);
                    command.Parameters.AddWithValue("@percent_all_atomic_defense", percent);
                    command.Parameters.AddWithValue("@percent_all_mental_attack", percent);
                    command.Parameters.AddWithValue("@percent_all_mental_defense", percent);
                    command.ExecuteNonQuery();
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
    }
    public void UpdateStatusMagicFormationCircleGallery(int Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "update magic_formation_circle_gallery set status=@status where user_id=@user_id and mfc_id=@mfc_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@mfc_id", Id);
                command.Parameters.AddWithValue("@status", "available");
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
}
