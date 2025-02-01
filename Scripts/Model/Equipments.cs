using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class Equipments
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
    public int block { get; set; }
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
    public double special_health { get; set; }
    public double special_physical_attack { get; set; }
    public double special_physical_defense { get; set; }
    public double special_magical_attack { get; set; }
    public double special_magical_defense { get; set; }
    public double special_chemical_attack { get; set; }
    public double special_chemical_defense { get; set; }
    public double special_atomic_attack { get; set; }
    public double special_atomic_defense { get; set; }
    public double special_mental_attack { get; set; }
    public double special_mental_defense { get; set; }
    public double special_speed { get; set; }
    public string description { get; set; }
    public string status { get; set; }
    public string currency_image { get; set; }
    public double price { get; set; }
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
    public Equipments()
    {
        percent_all_health = -1;
        percent_all_physical_attack = -1;
        percent_all_physical_defense = -1;
        percent_all_magical_attack = -1;
        percent_all_magical_defense = -1;
        percent_all_chemical_attack = -1;
        percent_all_chemical_defense = -1;
        percent_all_atomic_attack = -1;
        percent_all_atomic_defense = -1;
        percent_all_mental_attack = -1;
        percent_all_mental_defense = -1;
    }
    public Equipments GetNewLevelPower(Equipments c, double coefficient)
    {
        Equipments orginCard = new Equipments();
        orginCard = orginCard.GetEquipmentById(c.id);
        Equipments equipments = new Equipments
        {
            id = c.id,
            health = c.health + orginCard.health * coefficient,
            physical_attack = c.physical_attack + orginCard.physical_attack * coefficient,
            physical_defense = c.physical_defense + orginCard.physical_defense * coefficient,
            magical_attack = c.magical_attack + orginCard.magical_attack * coefficient,
            magical_defense = c.magical_defense + orginCard.magical_defense * coefficient,
            chemical_attack = c.chemical_attack + orginCard.chemical_attack * coefficient,
            chemical_defense = c.chemical_defense + orginCard.chemical_defense * coefficient,
            atomic_attack = c.atomic_attack + orginCard.atomic_attack * coefficient,
            atomic_defense = c.atomic_defense + orginCard.atomic_defense * coefficient,
            mental_attack = c.mental_attack + orginCard.mental_attack * coefficient,
            mental_defense = c.mental_defense + orginCard.mental_defense * coefficient,
            speed = c.speed + orginCard.speed * coefficient,
            critical_damage = c.critical_damage + orginCard.critical_damage * coefficient,
            critical_rate = c.critical_rate + orginCard.critical_rate * coefficient,
            armor_penetration = c.armor_penetration + orginCard.armor_penetration * coefficient,
            avoid = c.avoid + orginCard.avoid * coefficient,
            absorbs_damage = c.absorbs_damage + orginCard.absorbs_damage * coefficient,
            regenerate_vitality = c.regenerate_vitality + orginCard.regenerate_vitality * coefficient,
            accuracy = c.accuracy + orginCard.accuracy * coefficient,
            mana = c.mana + orginCard.mana * (float)coefficient
        };
        equipments.power = 0.5 * (
            equipments.health +
            equipments.physical_attack +
            equipments.physical_defense +
            equipments.magical_attack +
            equipments.magical_defense +
            equipments.chemical_attack +
            equipments.chemical_defense +
            equipments.atomic_attack +
            equipments.atomic_defense +
            equipments.mental_attack +
            equipments.mental_defense +
            equipments.speed +
            equipments.critical_damage +
            equipments.critical_rate +
            equipments.armor_penetration +
            equipments.avoid +
            equipments.absorbs_damage +
            equipments.regenerate_vitality +
            equipments.accuracy +
            equipments.mana
        );
        return equipments;
    }
    public Equipments GetNewBreakthroughPower(Equipments c, double coefficient)
    {
        Equipments orginCard = new Equipments();
        orginCard = orginCard.GetEquipmentById(c.id);
        Equipments equipments = new Equipments
        {
            id = c.id,
            health = c.health + orginCard.health * coefficient,
            physical_attack = c.physical_attack + orginCard.physical_attack * coefficient,
            physical_defense = c.physical_defense + orginCard.physical_defense * coefficient,
            magical_attack = c.magical_attack + orginCard.magical_attack * coefficient,
            magical_defense = c.magical_defense + orginCard.magical_defense * coefficient,
            chemical_attack = c.chemical_attack + orginCard.chemical_attack * coefficient,
            chemical_defense = c.chemical_defense + orginCard.chemical_defense * coefficient,
            atomic_attack = c.atomic_attack + orginCard.atomic_attack * coefficient,
            atomic_defense = c.atomic_defense + orginCard.atomic_defense * coefficient,
            mental_attack = c.mental_attack + orginCard.mental_attack * coefficient,
            mental_defense = c.mental_defense + orginCard.mental_defense * coefficient,
            speed = c.speed + orginCard.speed * coefficient,
            critical_damage = c.critical_damage + orginCard.critical_damage * coefficient,
            critical_rate = c.critical_rate + orginCard.critical_rate * coefficient,
            armor_penetration = c.armor_penetration + orginCard.armor_penetration * coefficient,
            avoid = c.avoid + orginCard.avoid * coefficient,
            absorbs_damage = c.absorbs_damage + orginCard.absorbs_damage * coefficient,
            regenerate_vitality = c.regenerate_vitality + orginCard.regenerate_vitality * coefficient,
            accuracy = c.accuracy + orginCard.accuracy * coefficient,
            mana = c.mana + orginCard.mana * (float)coefficient
        };
        equipments.power = 0.5 * (
            equipments.health +
            equipments.physical_attack +
            equipments.physical_defense +
            equipments.magical_attack +
            equipments.magical_defense +
            equipments.chemical_attack +
            equipments.chemical_defense +
            equipments.atomic_attack +
            equipments.atomic_defense +
            equipments.mental_attack +
            equipments.mental_defense +
            equipments.speed +
            equipments.critical_damage +
            equipments.critical_rate +
            equipments.armor_penetration +
            equipments.avoid +
            equipments.absorbs_damage +
            equipments.regenerate_vitality +
            equipments.accuracy +
            equipments.mana
        );
        return equipments;
    }
    public static List<string> GetUniqueEquipmentsTypes()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "Select distinct type from Equipments";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                typeList.Add(reader.GetString(0));
            }
        }
        return typeList;
    }
    public List<Equipments> GetEquipments(string type, int pageSize, int offset)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from Equipments where type= @type limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
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
                        special_health = reader.GetDouble("special_health"),
                        special_physical_attack = reader.GetDouble("special_physical_attack"),
                        special_physical_defense = reader.GetDouble("special_physical_defense"),
                        special_magical_attack = reader.GetDouble("special_magical_attack"),
                        special_magical_defense = reader.GetDouble("special_magical_defense"),
                        special_chemical_attack = reader.GetDouble("special_chemical_attack"),
                        special_chemical_defense = reader.GetDouble("special_chemical_defense"),
                        special_atomic_attack = reader.GetDouble("special_atomic_attack"),
                        special_atomic_defense = reader.GetDouble("special_atomic_defense"),
                        special_mental_attack = reader.GetDouble("special_mental_attack"),
                        special_mental_defense = reader.GetDouble("special_mental_defense"),
                        special_speed = reader.GetDouble("special_speed"),
                        description = reader.GetString("description")
                    };

                    equipmentList.Add(equipments);
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
        return equipmentList;
    }
    public int GetEquipmentsCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from Equipments where type= @type";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
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
    public List<Equipments> GetEquipmentsCollection(string type, int pageSize, int offset)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "SELECT e.*, CASE WHEN eg.equipment_id IS NULL THEN 'block' WHEN eg.status = 'pending' THEN 'pending' WHEN eg.status = 'available' THEN 'available' END AS status "
                + "FROM equipments e LEFT JOIN equipments_gallery eg ON e.id = eg.equipment_id and eg.user_id = @userId where e.type=@type limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
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
                        special_health = reader.GetDouble("special_health"),
                        special_physical_attack = reader.GetDouble("special_physical_attack"),
                        special_physical_defense = reader.GetDouble("special_physical_defense"),
                        special_magical_attack = reader.GetDouble("special_magical_attack"),
                        special_magical_defense = reader.GetDouble("special_magical_defense"),
                        special_chemical_attack = reader.GetDouble("special_chemical_attack"),
                        special_chemical_defense = reader.GetDouble("special_chemical_defense"),
                        special_atomic_attack = reader.GetDouble("special_atomic_attack"),
                        special_atomic_defense = reader.GetDouble("special_atomic_defense"),
                        special_mental_attack = reader.GetDouble("special_mental_attack"),
                        special_mental_defense = reader.GetDouble("special_mental_defense"),
                        special_speed = reader.GetDouble("special_speed"),
                        description = reader.GetString("description"),
                        status = reader.GetString("status"),
                    };

                    equipmentList.Add(equipments);
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
        return equipmentList;
    }
    public List<Equipments> GetUserEquipments(string type, int pageSize, int offset)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select e.id, e.name, ue.*, e.image, e.rare, e.type from Equipments e, user_equipments ue where e.id=ue.equipment_id and ue.user_id=@userId and e.type= @type limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetInt32("equipment_id"),
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
                        special_health = reader.GetDouble("special_health"),
                        special_physical_attack = reader.GetDouble("special_physical_attack"),
                        special_physical_defense = reader.GetDouble("special_physical_defense"),
                        special_magical_attack = reader.GetDouble("special_magical_attack"),
                        special_magical_defense = reader.GetDouble("special_magical_defense"),
                        special_chemical_attack = reader.GetDouble("special_chemical_attack"),
                        special_chemical_defense = reader.GetDouble("special_chemical_defense"),
                        special_atomic_attack = reader.GetDouble("special_atomic_attack"),
                        special_atomic_defense = reader.GetDouble("special_atomic_defense"),
                        special_mental_attack = reader.GetDouble("special_mental_attack"),
                        special_mental_defense = reader.GetDouble("special_mental_defense"),
                        special_speed = reader.GetDouble("special_speed")
                    };

                    equipmentList.Add(equipments);
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
        return equipmentList;
    }
    public int GetUserEquipmentsCount(string type)
    {
        int count = 0;
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from Equipments e, user_equipments ue where e.id=ue.equipment_id and ue.user_id=@userId and e.type= @type";
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
            finally
            {
                connection.Close();
            }
        }
        return count;
    }
    public List<Equipments> GetEquipmentsWithCurrency(string type, int pageSize, int offset)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "select e.*, c.image as currency_image, et.price from equipments e, currency c , equipment_trade et where e.id=et.equipment_id and c.id=et.currency_id and e.type=@type limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
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
                        special_health = reader.GetDouble("special_health"),
                        special_physical_attack = reader.GetDouble("special_physical_attack"),
                        special_physical_defense = reader.GetDouble("special_physical_defense"),
                        special_magical_attack = reader.GetDouble("special_magical_attack"),
                        special_magical_defense = reader.GetDouble("special_magical_defense"),
                        special_chemical_attack = reader.GetDouble("special_chemical_attack"),
                        special_chemical_defense = reader.GetDouble("special_chemical_defense"),
                        special_atomic_attack = reader.GetDouble("special_atomic_attack"),
                        special_atomic_defense = reader.GetDouble("special_atomic_defense"),
                        special_mental_attack = reader.GetDouble("special_mental_attack"),
                        special_mental_defense = reader.GetDouble("special_mental_defense"),
                        special_speed = reader.GetDouble("special_speed"),
                        description = reader.GetString("description"),
                        currency_image = reader.GetString("currency_image"),
                        price = reader.GetDouble("price"),
                    };

                    equipmentList.Add(equipments);
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
        return equipmentList;
    }
    private int GetMaxSequence(MySqlConnection connection, int equipment_id)
    {
        string query = "SELECT MAX(sequence) FROM user_equipments ue where ue.equipment_id=@equipment_id and ue.user_id=@user_id";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@equipment_id", equipment_id);
        command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
        object result = command.ExecuteScalar();

        if (result != DBNull.Value)
        {
            return Convert.ToInt32(result);
        }
        return 0; // Nếu bảng rỗng, trả về 0
    }
    public Equipments GetEquipmentById(int Id)
    {
        Equipments equipments = null;
        // Debug.Log(Id);
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "select * from equipments where equipments.id=@id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    equipments = new Equipments
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
                        special_health = reader.GetDouble("special_health"),
                        special_physical_attack = reader.GetDouble("special_physical_attack"),
                        special_physical_defense = reader.GetDouble("special_physical_defense"),
                        special_magical_attack = reader.GetDouble("special_magical_attack"),
                        special_magical_defense = reader.GetDouble("special_magical_defense"),
                        special_chemical_attack = reader.GetDouble("special_chemical_attack"),
                        special_chemical_defense = reader.GetDouble("special_chemical_defense"),
                        special_atomic_attack = reader.GetDouble("special_atomic_attack"),
                        special_atomic_defense = reader.GetDouble("special_atomic_defense"),
                        special_mental_attack = reader.GetDouble("special_mental_attack"),
                        special_mental_defense = reader.GetDouble("special_mental_defense"),
                        special_speed = reader.GetDouble("special_speed"),
                        description = reader.GetString("description"),
                    };
                }
                return equipments;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return equipments;
    }
    public Equipments GetUserEquipmentsById(int Id)
    {
        Equipments card = new Equipments();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_equipments where equipment_id=@id 
                and user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new Equipments
                    {
                        id = reader.GetInt32("equipment_id"),
                        level = reader.GetInt32("level"),
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
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana")
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
    public bool BuyEquipment(int Id)
    {
        Equipments EquipmentFromDB = GetEquipmentById(Id);
        // Debug.Log(EquipmentFromDB);
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                INSERT INTO user_equipments (
                    user_id, equipment_id, sequence, level, experiment, star, block, power,
                    health, physical_attack, physical_defense, magical_attack, magical_defense, 
                    chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                    mental_attack, mental_defense, speed, critical_damage, critical_rate, 
                    armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana,
                    special_health, special_physical_attack, special_physical_defense, special_magical_attack,
                    special_magical_defense, special_chemical_attack, special_chemical_defense, special_atomic_attack,
                    special_atomic_defense, special_mental_attack, special_mental_defense, special_speed
                ) VALUES (
                    @user_id, @equipment_id, @sequence, @level, @experiment, @star, @block, @power, 
                    @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense, 
                    @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, 
                    @mental_attack, @mental_defense, @speed, @critical_damage, @critical_rate, 
                    @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana,
                    @special_health, @special_physical_attack, @special_physical_defense, @special_magical_attack,
                    @special_magical_defense, @special_chemical_attack, @special_chemical_defense, @special_atomic_attack,
                    @special_atomic_defense, @special_mental_attack, @special_mental_defense, @special_speed
                )";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@equipment_id", Id);
                command.Parameters.AddWithValue("@sequence", GetMaxSequence(connection, Id) + 1);
                command.Parameters.AddWithValue("@level", 0);
                command.Parameters.AddWithValue("@experiment", 0);
                command.Parameters.AddWithValue("@star", 0);
                command.Parameters.AddWithValue("@block", false);
                command.Parameters.AddWithValue("@power", EquipmentFromDB.power);
                command.Parameters.AddWithValue("@health", EquipmentFromDB.health);
                command.Parameters.AddWithValue("@physical_attack", EquipmentFromDB.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", EquipmentFromDB.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", EquipmentFromDB.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", EquipmentFromDB.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", EquipmentFromDB.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", EquipmentFromDB.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", EquipmentFromDB.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", EquipmentFromDB.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", EquipmentFromDB.magical_attack);
                command.Parameters.AddWithValue("@mental_defense", EquipmentFromDB.magical_defense);
                command.Parameters.AddWithValue("@speed", EquipmentFromDB.speed);
                command.Parameters.AddWithValue("@critical_damage", EquipmentFromDB.critical_damage);
                command.Parameters.AddWithValue("@critical_rate", EquipmentFromDB.critical_rate);
                command.Parameters.AddWithValue("@armor_penetration", EquipmentFromDB.armor_penetration);
                command.Parameters.AddWithValue("@avoid", EquipmentFromDB.avoid);
                command.Parameters.AddWithValue("@absorbs_damage", EquipmentFromDB.absorbs_damage);
                command.Parameters.AddWithValue("@regenerate_vitality", EquipmentFromDB.regenerate_vitality);
                command.Parameters.AddWithValue("@accuracy", EquipmentFromDB.accuracy);
                command.Parameters.AddWithValue("@mana", EquipmentFromDB.mana);
                command.Parameters.AddWithValue("@special_health", EquipmentFromDB.special_health);
                command.Parameters.AddWithValue("@special_physical_attack", EquipmentFromDB.special_physical_attack);
                command.Parameters.AddWithValue("@special_physical_defense", EquipmentFromDB.special_physical_defense);
                command.Parameters.AddWithValue("@special_magical_attack", EquipmentFromDB.special_magical_attack);
                command.Parameters.AddWithValue("@special_magical_defense", EquipmentFromDB.special_magical_defense);
                command.Parameters.AddWithValue("@special_chemical_attack", EquipmentFromDB.special_chemical_attack);
                command.Parameters.AddWithValue("@special_chemical_defense", EquipmentFromDB.special_chemical_defense);
                command.Parameters.AddWithValue("@special_atomic_attack", EquipmentFromDB.special_atomic_attack);
                command.Parameters.AddWithValue("@special_atomic_defense", EquipmentFromDB.special_atomic_defense);
                command.Parameters.AddWithValue("@special_mental_attack", EquipmentFromDB.special_mental_attack);
                command.Parameters.AddWithValue("@special_mental_defense", EquipmentFromDB.special_mental_defense);
                command.Parameters.AddWithValue("@special_speed", EquipmentFromDB.special_speed);
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
    public bool UpdateEquipmentsLevel(Equipments equipments, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_equipments
                SET level = @level,
                    power = @power, health = @health, physical_attack = @physicalAttack,
                    physical_defense = @physicalDefense, magical_attack = @magicalAttack,
                    magical_defense = @magicalDefense, chemical_attack = @chemicalAttack,
                    chemical_defense = @chemicalDefense, atomic_attack = @atomicAttack,
                    atomic_defense = @atomicDefense, mental_attack = @mentalAttack,
                    mental_defense = @mentalDefense, speed = @speed, critical_damage = @criticalDamage,
                    critical_rate = @criticalRate, armor_penetration = @armorPenetration,
                    avoid = @avoid, absorbs_damage = @absorbsDamage, regenerate_vitality = @regenerateVitality, 
                    accuracy = @accuracy, mana = @mana
                WHERE 
                    user_id = @user_id AND equipment_id = @equipment_id;;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@equipment_id", equipments.id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", equipments.power);
                command.Parameters.AddWithValue("@health", equipments.health);
                command.Parameters.AddWithValue("@physicalAttack", equipments.physical_attack);
                command.Parameters.AddWithValue("@physicalDefense", equipments.physical_defense);
                command.Parameters.AddWithValue("@magicalAttack", equipments.magical_attack);
                command.Parameters.AddWithValue("@magicalDefense", equipments.magical_defense);
                command.Parameters.AddWithValue("@chemicalAttack", equipments.chemical_attack);
                command.Parameters.AddWithValue("@chemicalDefense", equipments.chemical_defense);
                command.Parameters.AddWithValue("@atomicAttack", equipments.atomic_attack);
                command.Parameters.AddWithValue("@atomicDefense", equipments.atomic_defense);
                command.Parameters.AddWithValue("@mentalAttack", equipments.mental_attack);
                command.Parameters.AddWithValue("@mentalDefense", equipments.mental_defense);
                command.Parameters.AddWithValue("@speed", equipments.speed);
                command.Parameters.AddWithValue("@criticalDamage", equipments.critical_damage);
                command.Parameters.AddWithValue("@criticalRate", equipments.critical_rate);
                command.Parameters.AddWithValue("@armorPenetration", equipments.armor_penetration);
                command.Parameters.AddWithValue("@avoid", equipments.avoid);
                command.Parameters.AddWithValue("@absorbsDamage", equipments.absorbs_damage);
                command.Parameters.AddWithValue("@regenerateVitality", equipments.regenerate_vitality);
                command.Parameters.AddWithValue("@accuracy", equipments.accuracy);
                command.Parameters.AddWithValue("@mana", equipments.mana);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateEquipmentsBreakthrough(Equipments equipments, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_equipments
                SET star = @star, quantity=@quantity,
                    power = @power, health = @health, physical_attack = @physicalAttack,
                    physical_defense = @physicalDefense, magical_attack = @magicalAttack,
                    magical_defense = @magicalDefense, chemical_attack = @chemicalAttack,
                    chemical_defense = @chemicalDefense, atomic_attack = @atomicAttack,
                    atomic_defense = @atomicDefense, mental_attack = @mentalAttack,
                    mental_defense = @mentalDefense, speed = @speed, critical_damage = @criticalDamage,
                    critical_rate = @criticalRate, armor_penetration = @armorPenetration,
                    avoid = @avoid, absorbs_damage = @absorbsDamage, regenerate_vitality = @regenerateVitality, 
                    accuracy = @accuracy, mana = @mana
                WHERE 
                    user_id = @user_id AND equipment_id = @equipment_id;;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@equipment_id", equipments.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", equipments.power);
                command.Parameters.AddWithValue("@health", equipments.health);
                command.Parameters.AddWithValue("@physicalAttack", equipments.physical_attack);
                command.Parameters.AddWithValue("@physicalDefense", equipments.physical_defense);
                command.Parameters.AddWithValue("@magicalAttack", equipments.magical_attack);
                command.Parameters.AddWithValue("@magicalDefense", equipments.magical_defense);
                command.Parameters.AddWithValue("@chemicalAttack", equipments.chemical_attack);
                command.Parameters.AddWithValue("@chemicalDefense", equipments.chemical_defense);
                command.Parameters.AddWithValue("@atomicAttack", equipments.atomic_attack);
                command.Parameters.AddWithValue("@atomicDefense", equipments.atomic_defense);
                command.Parameters.AddWithValue("@mentalAttack", equipments.mental_attack);
                command.Parameters.AddWithValue("@mentalDefense", equipments.mental_defense);
                command.Parameters.AddWithValue("@speed", equipments.speed);
                command.Parameters.AddWithValue("@criticalDamage", equipments.critical_damage);
                command.Parameters.AddWithValue("@criticalRate", equipments.critical_rate);
                command.Parameters.AddWithValue("@armorPenetration", equipments.armor_penetration);
                command.Parameters.AddWithValue("@avoid", equipments.avoid);
                command.Parameters.AddWithValue("@absorbsDamage", equipments.absorbs_damage);
                command.Parameters.AddWithValue("@regenerateVitality", equipments.regenerate_vitality);
                command.Parameters.AddWithValue("@accuracy", equipments.accuracy);
                command.Parameters.AddWithValue("@mana", equipments.mana);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public void UpdateUserCurrency(int Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "select et.currency_id, et.price from equipments e, equipment_trade et where e.id=et.equipment_id and e.id=@id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                int currencyId = 0;
                double price = 0;

                if (reader.Read())
                {
                    currencyId = reader.GetInt32("currency_id");
                    price = reader.GetDouble("price");
                }
                reader.Close();

                // Lấy quantity hiện tại
                query = "SELECT quantity FROM user_currency WHERE user_id = @user_id AND currency_id = @currency_id";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@currency_id", currencyId);
                double currentQuantity = Convert.ToDouble(command.ExecuteScalar());
                double newQuantity = currentQuantity - price;

                query = "update user_currency set quantity=@quantity where user_id=@user_id and currency_id=@currency_id";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@quantity", newQuantity);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@currency_id", currencyId);
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
    public void InsertEquipmentsGallery(int Id)
    {
        Equipments EquipmentFromDB = GetEquipmentById(Id);
        int percent = 0;
        if (EquipmentFromDB.rare.Equals("LG"))
        {
            percent = 20;
        }
        else if (EquipmentFromDB.rare.Equals("UR"))
        {
            percent = 10;
        }
        else if (EquipmentFromDB.rare.Equals("SSR"))
        {
            percent = 5;
        }
        else if (EquipmentFromDB.rare.Equals("SR"))
        {
            percent = 2;
        }
        else if (EquipmentFromDB.rare.Equals("MR"))
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
                FROM equipments_gallery 
                WHERE user_id = @user_id AND equipment_id = @equipment_id;
                ";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@equipment_id", Id);

                int recordCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (recordCount == 0)
                {
                    string query = @"
                    INSERT INTO equipments_gallery (
                        user_id, equipment_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
                        magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                        mental_attack, mental_defense, speed, critical_damage, critical_rate, armor_penetration, avoid, 
                        absorbs_damage, regenerate_vitality, accuracy, mana, percent_all_health, percent_all_physical_attack, 
                        percent_all_physical_defense, percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, percent_all_mental_attack, 
                        percent_all_mental_defense
                    ) VALUES (
                        @user_id, @equipment_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
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
                    command.Parameters.AddWithValue("@equipment_id", Id);
                    command.Parameters.AddWithValue("@status", "pending");
                    command.Parameters.AddWithValue("@current_star", 0);
                    command.Parameters.AddWithValue("@temp_star", 0);
                    command.Parameters.AddWithValue("@power", EquipmentFromDB.power);
                    command.Parameters.AddWithValue("@health", EquipmentFromDB.health);
                    command.Parameters.AddWithValue("@physical_attack", EquipmentFromDB.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", EquipmentFromDB.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", EquipmentFromDB.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", EquipmentFromDB.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", EquipmentFromDB.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", EquipmentFromDB.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", EquipmentFromDB.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", EquipmentFromDB.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", EquipmentFromDB.magical_attack);
                    command.Parameters.AddWithValue("@mental_defense", EquipmentFromDB.magical_defense);
                    command.Parameters.AddWithValue("@speed", EquipmentFromDB.speed);
                    command.Parameters.AddWithValue("@critical_damage", EquipmentFromDB.critical_damage);
                    command.Parameters.AddWithValue("@critical_rate", EquipmentFromDB.critical_rate);
                    command.Parameters.AddWithValue("@armor_penetration", EquipmentFromDB.armor_penetration);
                    command.Parameters.AddWithValue("@avoid", EquipmentFromDB.avoid);
                    command.Parameters.AddWithValue("@absorbs_damage", EquipmentFromDB.absorbs_damage);
                    command.Parameters.AddWithValue("@regenerate_vitality", EquipmentFromDB.regenerate_vitality);
                    command.Parameters.AddWithValue("@accuracy", EquipmentFromDB.accuracy);
                    command.Parameters.AddWithValue("@mana", EquipmentFromDB.mana);
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
    public void UpdateStatusEquipmentsGallery(int Id){
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query="update equipments_gallery set status=@status where user_id=@user_id and equipment_id=@equipment_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@equipment_id", Id);
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
    public Equipments SumPowerEquipmentsGallery()
    {
        Equipments sumEquipments = new Equipments();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                SUM(power) AS total_power, SUM(health) AS total_health, SUM(physical_attack) AS total_physical_attack,
                SUM(physical_defense) AS total_physical_defense, SUM(magical_attack) AS total_magical_attack, SUM(magical_defense) AS total_magical_defense,
                SUM(chemical_attack) AS total_chemical_attack, SUM(chemical_defense) AS total_chemical_defense, SUM(atomic_attack) AS total_atomic_attack,
                SUM(atomic_defense) AS total_atomic_defense, SUM(mental_attack) AS total_mental_attack, SUM(mental_defense) AS total_mental_defense,
                SUM(speed) AS total_speed, SUM(critical_damage) AS total_critical_damage, SUM(critical_rate) AS total_critical_rate,
                SUM(armor_penetration) AS total_armor_penetration, SUM(avoid) AS total_avoid, SUM(absorbs_damage) AS total_absorbs_damage,
                SUM(regenerate_vitality) AS total_regenerate_vitality, SUM(accuracy) AS total_accuracy, SUM(mana) AS total_mana,    
                SUM(percent_all_health) AS total_percent_all_health, SUM(percent_all_physical_attack) AS total_percent_all_physical_attack,
                SUM(percent_all_physical_defense) AS total_percent_all_physical_defense, SUM(percent_all_magical_attack) AS total_percent_all_magical_attack,
                SUM(percent_all_magical_defense) AS total_percent_all_magical_defense, SUM(percent_all_chemical_attack) AS total_percent_all_chemical_attack,
                SUM(percent_all_chemical_defense) AS total_percent_all_chemical_defense, SUM(percent_all_atomic_attack) AS total_percent_all_atomic_attack,
                SUM(percent_all_atomic_defense) AS total_percent_all_atomic_defense, SUM(percent_all_mental_attack) AS total_percent_all_mental_attack,
                SUM(percent_all_mental_defense) AS total_percent_all_mental_defense
                FROM equipments_gallery where user_id=@user_id and status = 'available';";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumEquipments.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumEquipments.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumEquipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumEquipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumEquipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumEquipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumEquipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumEquipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumEquipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumEquipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumEquipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumEquipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumEquipments.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumEquipments.critical_damage = reader.IsDBNull(reader.GetOrdinal("total_critical_damage")) ? 0 : reader.GetDouble("total_critical_damage");
                        sumEquipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumEquipments.armor_penetration = reader.IsDBNull(reader.GetOrdinal("total_armor_penetration")) ? 0 : reader.GetDouble("total_armor_penetration");
                        sumEquipments.avoid = reader.IsDBNull(reader.GetOrdinal("total_avoid")) ? 0 : reader.GetDouble("total_avoid");
                        sumEquipments.absorbs_damage = reader.IsDBNull(reader.GetOrdinal("total_absorbs_damage")) ? 0 : reader.GetDouble("total_absorbs_damage");
                        sumEquipments.regenerate_vitality = reader.IsDBNull(reader.GetOrdinal("total_regenerate_vitality")) ? 0 : reader.GetDouble("total_regenerate_vitality");
                        sumEquipments.accuracy = reader.IsDBNull(reader.GetOrdinal("total_accuracy")) ? 0 : reader.GetDouble("total_accuracy");
                        sumEquipments.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetInt32("total_mana");
                        sumEquipments.percent_all_health = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDouble("total_percent_all_health");
                        sumEquipments.percent_all_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDouble("total_percent_all_physical_attack");
                        sumEquipments.percent_all_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDouble("total_percent_all_physical_defense");
                        sumEquipments.percent_all_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDouble("total_percent_all_magical_attack");
                        sumEquipments.percent_all_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDouble("total_percent_all_magical_defense");
                        sumEquipments.percent_all_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDouble("total_percent_all_chemical_attack");
                        sumEquipments.percent_all_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDouble("total_percent_all_chemical_defense");
                        sumEquipments.percent_all_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDouble("total_percent_all_atomic_attack");
                        sumEquipments.percent_all_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDouble("total_percent_all_atomic_defense");
                        sumEquipments.percent_all_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDouble("total_percent_all_mental_attack");
                        sumEquipments.percent_all_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDouble("total_percent_all_mental_defense");
                    }
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return sumEquipments;
    }
}
