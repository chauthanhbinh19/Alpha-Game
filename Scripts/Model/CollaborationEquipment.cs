using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class CollaborationEquipment
{
    public int id { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public string rare { get; set; }
    public string type { get; set; }
    public int star { get; set; }
    public int sequence { get; set; }
    public int level { get; set; }
    public int experiment { get; set; }
    public int quantity { get; set; }
    public bool block { get; set; }
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
    public string description { get; set; }
    public string status { get; set; }
    public Currency currency { get; set; }
    public CollaborationEquipment()
    {

    }
    public static List<string> GetUniqueCollaborationEquipmentTypes()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "Select distinct type from collaboration_equipments";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                typeList.Add(reader.GetString(0));
            }
        }
        return typeList;
    }
    public List<CollaborationEquipment> GetCollaborationEquipments(string type,int pageSize, int offset)
    {
        List<CollaborationEquipment> collaborationEquipmentList = new List<CollaborationEquipment>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from collaboration_equipments where type= @type 
                ORDER BY collaboration_equipments.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(collaboration_equipments.name, '[0-9]+$') AS UNSIGNED), collaboration_equipments.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CollaborationEquipment collaborationEquipment = new CollaborationEquipment
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
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description")
                    };

                    collaborationEquipmentList.Add(collaborationEquipment);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return collaborationEquipmentList;
    }
    public int GetCollaborationEquipmentCount(string type){
        int count =0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from collaboration_equipments where type= @type";
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
    public List<CollaborationEquipment> GetCollaborationEquipmentsCollection(string type,int pageSize, int offset)
    {
        List<CollaborationEquipment> collaborationEquipmentList = new List<CollaborationEquipment>();
        int user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT ce.*, CASE WHEN ceg.collaboration_equipment_id IS NULL THEN 'block' WHEN ceg.status = 'pending' THEN 'pending' WHEN ceg.status = 'available' THEN 'available' END AS status 
                FROM collaboration_equipments ce LEFT JOIN collaboration_equipments_gallery ceg ON ce.id = ceg.collaboration_equipment_id and ceg.user_id = @userId where ce.type=@type 
                ORDER BY ce.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(ce.name, '[0-9]+$') AS UNSIGNED), ce.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CollaborationEquipment collaborationEquipment = new CollaborationEquipment
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
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description"),
                        status=reader.GetString("status"),
                    };

                    collaborationEquipmentList.Add(collaborationEquipment);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return collaborationEquipmentList;
    }
    public List<CollaborationEquipment> GetUserCollaborationEquipments(string type,int pageSize, int offset)
    {
        List<CollaborationEquipment> collaborationEquipmentList = new List<CollaborationEquipment>();
        int user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select uce.*, ce.image, ce.rare, ce.type from collaboration_equipments ce, user_collaboration_equipments uce where ce.id=uce.collaboration_equipment_id and uce.user_id=@userId and ce.type= @type 
                ORDER BY ce.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(ce.name, '[0-9]+$') AS UNSIGNED), ce.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CollaborationEquipment collaborationEquipment = new CollaborationEquipment
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
                        block = reader.GetBoolean("block"),
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
                        description = reader.GetString("description")
                    };

                    collaborationEquipmentList.Add(collaborationEquipment);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return collaborationEquipmentList;
    }
    public int GetUserCollaborationEquipmentCount(string type){
        int count =0;
        int user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from collaboration_equipments ce, user_collaboration_equipments uce where ce.id=uce.collaboration_equipment_id and uce.user_id=@userId and type= @type";
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
    private int GetMaxSequence(MySqlConnection connection, int id)
    {
        string query = "SELECT MAX(sequence) FROM user_collaboration_equipments ue where ue.collaboration_equipment_id=@collaboration_equipment_id and ue.user_id=@user_id";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@collaboration_equipment_id", id);
        command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
        object result = command.ExecuteScalar();

        if (result != DBNull.Value)
        {
            return Convert.ToInt32(result);
        }
        return 0; // Nếu bảng rỗng, trả về 0
    }
    public bool InsertUserCollaborationEquipments(CollaborationEquipment collaborationEquipment)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                INSERT INTO user_collaboration_equipments (
                    user_id, collaboration_equipment_id, sequence, level, experiment, star, block, power,
                    health, physical_attack, physical_defense, magical_attack, magical_defense, 
                    chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                    mental_attack, mental_defense, speed, critical_damage, critical_rate, 
                    armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana
                ) VALUES (
                    @user_id, @collaboration_equipment_id, @sequence, @level, @experiment, @star, @block, @power, 
                    @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense, 
                    @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, 
                    @mental_attack, @mental_defense, @speed, @critical_damage, @critical_rate, 
                    @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana
                )";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@collaboration_equipment_id", collaborationEquipment.id);
                command.Parameters.AddWithValue("@sequence", GetMaxSequence(connection, collaborationEquipment.id) + 1);
                command.Parameters.AddWithValue("@level", 0);
                command.Parameters.AddWithValue("@experiment", 0);
                command.Parameters.AddWithValue("@star", 0);
                command.Parameters.AddWithValue("@block", false);
                command.Parameters.AddWithValue("@power", collaborationEquipment.power);
                command.Parameters.AddWithValue("@health", collaborationEquipment.health);
                command.Parameters.AddWithValue("@physical_attack", collaborationEquipment.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", collaborationEquipment.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", collaborationEquipment.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", collaborationEquipment.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", collaborationEquipment.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", collaborationEquipment.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", collaborationEquipment.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", collaborationEquipment.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", collaborationEquipment.magical_attack);
                command.Parameters.AddWithValue("@mental_defense", collaborationEquipment.magical_defense);
                command.Parameters.AddWithValue("@speed", collaborationEquipment.speed);
                command.Parameters.AddWithValue("@critical_damage", collaborationEquipment.critical_damage);
                command.Parameters.AddWithValue("@critical_rate", collaborationEquipment.critical_rate);
                command.Parameters.AddWithValue("@armor_penetration", collaborationEquipment.armor_penetration);
                command.Parameters.AddWithValue("@avoid", collaborationEquipment.avoid);
                command.Parameters.AddWithValue("@absorbs_damage", collaborationEquipment.absorbs_damage);
                command.Parameters.AddWithValue("@regenerate_vitality", collaborationEquipment.regenerate_vitality);
                command.Parameters.AddWithValue("@accuracy", collaborationEquipment.accuracy);
                command.Parameters.AddWithValue("@mana", collaborationEquipment.mana);
                command.ExecuteNonQuery();
                return true;
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
        return false;
    }
    public List<CollaborationEquipment> GetCollaborationEquipmentsWithPrice(string type,int pageSize, int offset)
    {
        List<CollaborationEquipment> collaborationEquipmentList = new List<CollaborationEquipment>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select c.*, ct.price, cu.image as currency_image, cu.id as currency_id
                from collaboration_equipments c, collaboration_equipment_trade ct, currency cu
                where c.id=ct.collaboration_equipment_id and ct.currency_id = cu.id and c.type =@type
                ORDER BY c.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name limit @limit offset @offset;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CollaborationEquipment collaborationEquipment = new CollaborationEquipment
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
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description")
                    };
                    collaborationEquipment.currency = new Currency{
                        id = reader.GetInt32("currency_id"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("price")
                    };

                    collaborationEquipmentList.Add(collaborationEquipment);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return collaborationEquipmentList;
    }
    public int GetCollaborationEquipmentsWithPriceCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*)
                from collaboration_equipments c, collaboration_equipment_trade ct, currency cu
                where c.id=ct.collaboration_equipment_id and ct.currency_id = cu.id and c.type =@type;";
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
    public CollaborationEquipment GetCollaborationEquipmentsById(int Id)
    {
        CollaborationEquipment collaborationEquipment = new CollaborationEquipment();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from collaboration_equipments where id=@id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    collaborationEquipment = new CollaborationEquipment
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
        return collaborationEquipment;
    }
    public void InsertCollaborationEquipmentsGallery(int Id)
    {
        CollaborationEquipment collaborationEquipmentFromDB = GetCollaborationEquipmentsById(Id);
        int percent = 0;
        if (collaborationEquipmentFromDB.rare.Equals("LG"))
        {
            percent = 20;
        }
        else if (collaborationEquipmentFromDB.rare.Equals("UR"))
        {
            percent = 10;
        }
        else if (collaborationEquipmentFromDB.rare.Equals("SSR"))
        {
            percent = 5;
        }
        else if (collaborationEquipmentFromDB.rare.Equals("SR"))
        {
            percent = 2;
        }
        else if (collaborationEquipmentFromDB.rare.Equals("MR"))
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
                FROM collaborations_gallery 
                WHERE user_id = @user_id AND collaboration_id = @collaboration_id;
                ";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@collaboration_id", Id);

                int recordCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (recordCount == 0)
                {
                    string query = @"
                    INSERT INTO collaboration_equipments_gallery (
                        user_id, collaboration_equipment_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
                        magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                        mental_attack, mental_defense, speed, critical_damage, critical_rate, armor_penetration, avoid, 
                        absorbs_damage, regenerate_vitality, accuracy, mana, percent_all_health, percent_all_physical_attack, 
                        percent_all_physical_defense, percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, percent_all_mental_attack, 
                        percent_all_mental_defense
                    ) VALUES (
                        @user_id, @collaboration_equipment_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
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
                    command.Parameters.AddWithValue("@collaboration_equipment_id", Id);
                    command.Parameters.AddWithValue("@status", "pending");
                    command.Parameters.AddWithValue("@current_star", 0);
                    command.Parameters.AddWithValue("@temp_star", 0);
                    command.Parameters.AddWithValue("@power", collaborationEquipmentFromDB.power);
                    command.Parameters.AddWithValue("@health", collaborationEquipmentFromDB.health);
                    command.Parameters.AddWithValue("@physical_attack", collaborationEquipmentFromDB.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", collaborationEquipmentFromDB.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", collaborationEquipmentFromDB.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", collaborationEquipmentFromDB.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", collaborationEquipmentFromDB.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", collaborationEquipmentFromDB.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", collaborationEquipmentFromDB.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", collaborationEquipmentFromDB.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", collaborationEquipmentFromDB.magical_attack);
                    command.Parameters.AddWithValue("@mental_defense", collaborationEquipmentFromDB.magical_defense);
                    command.Parameters.AddWithValue("@speed", collaborationEquipmentFromDB.speed);
                    command.Parameters.AddWithValue("@critical_damage", collaborationEquipmentFromDB.critical_damage);
                    command.Parameters.AddWithValue("@critical_rate", collaborationEquipmentFromDB.critical_rate);
                    command.Parameters.AddWithValue("@armor_penetration", collaborationEquipmentFromDB.armor_penetration);
                    command.Parameters.AddWithValue("@avoid", collaborationEquipmentFromDB.avoid);
                    command.Parameters.AddWithValue("@absorbs_damage", collaborationEquipmentFromDB.absorbs_damage);
                    command.Parameters.AddWithValue("@regenerate_vitality", collaborationEquipmentFromDB.regenerate_vitality);
                    command.Parameters.AddWithValue("@accuracy", collaborationEquipmentFromDB.accuracy);
                    command.Parameters.AddWithValue("@mana", collaborationEquipmentFromDB.mana);
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
    public void UpdateStatusCollaborationEquipmentsGallery(int Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "update collaboration_equipments_gallery set status=@status where user_id=@user_id and collaboration_equipment_id=@collaboration_equipment_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@collaboration_equipment_id", Id);
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
