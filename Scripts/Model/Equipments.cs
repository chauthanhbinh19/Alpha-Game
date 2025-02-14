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
    public string set { get; set; }
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
    public int position { get; set; }
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
        position = -1;
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
            mana = c.mana + orginCard.mana * (float)coefficient,
            special_health = c.special_health + orginCard.special_health * coefficient,
            special_physical_attack = c.special_physical_attack + orginCard.special_physical_attack * coefficient,
            special_physical_defense = c.special_physical_defense + orginCard.special_physical_defense * coefficient,
            special_magical_attack = c.special_magical_attack + orginCard.special_magical_attack * coefficient,
            special_magical_defense = c.special_magical_defense + orginCard.special_magical_defense * coefficient,
            special_chemical_attack = c.special_chemical_attack + orginCard.special_chemical_attack * coefficient,
            special_chemical_defense = c.special_chemical_defense + orginCard.special_chemical_defense * coefficient,
            special_atomic_attack = c.special_atomic_attack + orginCard.special_atomic_attack * coefficient,
            special_atomic_defense = c.special_atomic_defense + orginCard.special_atomic_defense * coefficient,
            special_mental_attack = c.special_mental_attack + orginCard.special_mental_attack * coefficient,
            special_mental_defense = c.special_mental_defense + orginCard.special_mental_defense * coefficient,
            special_speed = c.special_speed + orginCard.special_speed * coefficient,
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
            equipments.mana +
            equipments.special_health +
            equipments.special_physical_attack +
            equipments.special_physical_defense +
            equipments.special_magical_attack +
            equipments.special_magical_defense +
            equipments.special_chemical_attack +
            equipments.special_chemical_defense +
            equipments.special_atomic_attack +
            equipments.special_atomic_defense +
            equipments.special_mental_attack +
            equipments.special_mental_defense +
            equipments.special_speed
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
            mana = c.mana + orginCard.mana * (float)coefficient,
            special_health = c.special_health + orginCard.special_health * coefficient,
            special_physical_attack = c.special_physical_attack + orginCard.special_physical_attack * coefficient,
            special_physical_defense = c.special_physical_defense + orginCard.special_physical_defense * coefficient,
            special_magical_attack = c.special_magical_attack + orginCard.special_magical_attack * coefficient,
            special_magical_defense = c.special_magical_defense + orginCard.special_magical_defense * coefficient,
            special_chemical_attack = c.special_chemical_attack + orginCard.special_chemical_attack * coefficient,
            special_chemical_defense = c.special_chemical_defense + orginCard.special_chemical_defense * coefficient,
            special_atomic_attack = c.special_atomic_attack + orginCard.special_atomic_attack * coefficient,
            special_atomic_defense = c.special_atomic_defense + orginCard.special_atomic_defense * coefficient,
            special_mental_attack = c.special_mental_attack + orginCard.special_mental_attack * coefficient,
            special_mental_defense = c.special_mental_defense + orginCard.special_mental_defense * coefficient,
            special_speed = c.special_speed + orginCard.special_speed * coefficient,
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
            equipments.mana +
            equipments.special_health +
            equipments.special_physical_attack +
            equipments.special_physical_defense +
            equipments.special_magical_attack +
            equipments.special_magical_defense +
            equipments.special_chemical_attack +
            equipments.special_chemical_defense +
            equipments.special_atomic_attack +
            equipments.special_atomic_defense +
            equipments.special_mental_attack +
            equipments.special_mental_defense +
            equipments.special_speed
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
    public static List<String> GetEquipmentsSet(string type)
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select distinct (e.equipmentSet) 
                from Equipments e 
                where e.type= @type";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    typeList.Add(reader.GetString(0));
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
        return typeList;
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
    public void UpdateStatusEquipmentsGallery(int Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "update equipments_gallery set status=@status where user_id=@user_id and equipment_id=@equipment_id";
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
    public void InsertCardHeroesEquipments(int Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_heroes_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM card_heroes_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO card_heroes_equipment (user_id, card_hero_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @card_hero_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@card_hero_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
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
    public void InsertCardCaptainsEquipments(int Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_captains_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM card_captains_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO card_captains_equipment (user_id, card_captain_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @card_captain_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@card_captain_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
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
    public void InsertCardColonelsEquipments(int Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_colonels_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM card_colonels_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO card_colonels_equipment (user_id, card_colonel_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @card_colonel_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@card_colonel_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
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
    public void InsertCardGeneralsEquipments(int Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_generals_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM card_generals_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO card_generals_equipment (user_id, card_general_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @card_general_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@card_general_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
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
    public void InsertCardAdmiralsEquipments(int Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_admirals_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM card_admirals_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO card_admirals_equipment (user_id, card_admiral_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @card_admiral_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@card_admiral_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
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
    public void InsertCardMonstersEquipments(int Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_monsters_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM card_monsters_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO card_monsters_equipment (user_id, card_monster_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @card_monster_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@card_monster_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
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
    public void InsertCardMilitaryEquipments(int Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_military_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM card_military_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO card_military_equipment (user_id, card_military_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @card_military_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@card_military_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
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
    public void InsertCardSpellEquipments(int Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_spell_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM card_spell_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO card_spell_equipment (user_id, card_spell_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @card_spell_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@card_spell_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
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
    public void InsertBooksEquipments(int Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM books_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM books_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO books_equipment (user_id, book_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @book_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@book_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
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
    public void InsertPetsEquipments(int Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM pets_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM pets_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO pets_equipment (user_id, pet_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @pet_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@pet_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
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
    public List<Equipments> GetCardHeroesEquipments(int card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_heroes_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.card_hero_id = @card_hero_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_hero_id", card_id);
                command.Parameters.AddWithValue("@type", type);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
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
    public List<Equipments> GetCardCaptainsEquipments(int card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_captains_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.card_captain_id = @card_captain_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_captain_id", card_id);
                command.Parameters.AddWithValue("@type", type);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.GetInt32("position")
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
    public List<Equipments> GetCardColonelsEquipments(int card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_colonels_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.card_colonel_id = @card_colonel_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_colonel_id", card_id);
                command.Parameters.AddWithValue("@type", type);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.GetInt32("position")
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
    public List<Equipments> GetCardGeneralsEquipments(int card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_generals_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.card_general_id = @card_general_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_general_id", card_id);
                command.Parameters.AddWithValue("@type", type);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.GetInt32("position")
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
    public List<Equipments> GetCardAdmiralsEquipments(int card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_admirals_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.card_admiral_id = @card_admiral_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_admiral_id", card_id);
                command.Parameters.AddWithValue("@type", type);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.GetInt32("position")
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
    public List<Equipments> GetCardMonstersEquipments(int card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_monsters_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.card_monster_id = @card_monster_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_monster_id", card_id);
                command.Parameters.AddWithValue("@type", type);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.GetInt32("position")
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
    public List<Equipments> GetCardMilitaryEquipments(int card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_military_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.card_military_id = @card_military_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_military_id", card_id);
                command.Parameters.AddWithValue("@type", type);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.GetInt32("position")
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
    public List<Equipments> GetCardSpellEquipments(int card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_spell_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.card_spell_id = @card_spell_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_spell_id", card_id);
                command.Parameters.AddWithValue("@type", type);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.GetInt32("position")
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
    public List<Equipments> GetBooksEquipments(int card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_books_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.book_id = @book_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@book_id", card_id);
                command.Parameters.AddWithValue("@type", type);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.GetInt32("position")
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
    public List<Equipments> GetPetsEquipments(int card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_pets_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.pet_id = @pet_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@pet_id", card_id);
                command.Parameters.AddWithValue("@type", type);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.GetInt32("position")
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
    public List<Equipments> GetAllCardHeroesEquipments(string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join card_heroes_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
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
    public List<Equipments> GetAllCardCaptainsEquipments(string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join card_captains_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
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
    public List<Equipments> GetAllCardColonelsEquipments(string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join card_colonels_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
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
    public List<Equipments> GetAllCardGeneralsEquipments(string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join card_generals_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
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
    public List<Equipments> GetAllCardAdmiralsEquipments(string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join card_admirals_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
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
    public List<Equipments> GetAllCardMonstersEquipments(string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join card_monsters_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
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
    public List<Equipments> GetAllCardMilitaryEquipments(string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join card_military_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
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
    public List<Equipments> GetAllCardSpellEquipments(string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join card_spell_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
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
    public List<Equipments> GetAllBooksEquipments(string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join books_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
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
    public List<Equipments> GetAllPetsEquipments(string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join pets_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
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
                        set = reader.GetString("equipmentSet"),
                        star = reader.GetInt32("star"),
                        sequence = reader.GetInt32("sequence"),
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
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
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
    public Equipments GetAllEquipmentsByCardHeoresId(int Id)
    {
        Equipments equipments = new Equipments();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT SUM(ue.power) AS total_power, SUM(ue.health) AS total_health,
                    SUM(ue.physical_attack) AS total_physical_attack, SUM(ue.physical_defense) AS total_physical_defense,
                    SUM(ue.magical_attack) AS total_magical_attack, SUM(ue.magical_defense) AS total_magical_defense,
                    SUM(ue.chemical_attack) AS total_chemical_attack, SUM(ue.chemical_defense) AS total_chemical_defense,
                    SUM(ue.atomic_attack) AS total_atomic_attack, SUM(ue.atomic_defense) AS total_atomic_defense,
                    SUM(ue.mental_attack) AS total_mental_attack, SUM(ue.mental_defense) AS total_mental_defense,
                    SUM(ue.speed) AS total_speed, SUM(ue.critical_damage) AS total_critical_damage,
                    SUM(ue.critical_rate) AS total_critical_rate, SUM(ue.armor_penetration) AS total_armor_penetration,
                    SUM(ue.avoid) AS total_avoid, SUM(ue.absorbs_damage) AS total_absorbs_damage,
                    SUM(ue.regenerate_vitality) AS total_regenerate_vitality, SUM(ue.accuracy) AS total_accuracy,
                    SUM(ue.mana) AS total_mana, SUM(ue.special_health) AS total_special_health,
                    SUM(ue.special_physical_attack) AS total_special_physical_attack, SUM(ue.special_physical_defense) AS total_special_physical_defense,
                    SUM(ue.special_magical_attack) AS total_special_magical_attack, SUM(ue.special_magical_defense) AS total_special_magical_defense,
                    SUM(ue.special_chemical_attack) AS total_special_chemical_attack, SUM(ue.special_chemical_defense) AS total_special_chemical_defense,
                    SUM(ue.special_atomic_attack) AS total_special_atomic_attack, SUM(ue.special_atomic_defense) AS total_special_atomic_defense,
                    SUM(ue.special_mental_attack) AS total_special_mental_attack, SUM(ue.special_mental_defense) AS total_special_mental_defense,
                    SUM(ue.special_speed) AS total_special_speed
                FROM user_card_heroes uc, card_heroes c, card_heroes_equipment che, user_equipments ue
                WHERE uc.card_hero_id = c.id AND uc.card_hero_id = che.card_hero_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_hero_id = @card_hero_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_hero_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    equipments.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                    equipments.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                    equipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                    equipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                    equipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                    equipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                    equipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                    equipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                    equipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                    equipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                    equipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                    equipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                    equipments.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                    equipments.critical_damage = reader.IsDBNull(reader.GetOrdinal("total_critical_damage")) ? 0 : reader.GetDouble("total_critical_damage");
                    equipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                    equipments.armor_penetration = reader.IsDBNull(reader.GetOrdinal("total_armor_penetration")) ? 0 : reader.GetDouble("total_armor_penetration");
                    equipments.avoid = reader.IsDBNull(reader.GetOrdinal("total_avoid")) ? 0 : reader.GetDouble("total_avoid");
                    equipments.absorbs_damage = reader.IsDBNull(reader.GetOrdinal("total_absorbs_damage")) ? 0 : reader.GetDouble("total_absorbs_damage");
                    equipments.regenerate_vitality = reader.IsDBNull(reader.GetOrdinal("total_regenerate_vitality")) ? 0 : reader.GetDouble("total_regenerate_vitality");
                    equipments.accuracy = reader.IsDBNull(reader.GetOrdinal("total_accuracy")) ? 0 : reader.GetDouble("total_accuracy");
                    equipments.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                    equipments.special_health = reader.IsDBNull(reader.GetOrdinal("total_special_health")) ? 0 : reader.GetDouble("total_special_health");
                    equipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_physical_attack")) ? 0 : reader.GetDouble("total_special_physical_attack");
                    equipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_physical_defense")) ? 0 : reader.GetDouble("total_special_physical_defense");
                    equipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_magical_attack")) ? 0 : reader.GetDouble("total_special_magical_attack");
                    equipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_magical_defense")) ? 0 : reader.GetDouble("total_special_magical_defense");
                    equipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_attack")) ? 0 : reader.GetDouble("total_special_chemical_attack");
                    equipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_defense")) ? 0 : reader.GetDouble("total_special_chemical_defense");
                    equipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_attack")) ? 0 : reader.GetDouble("total_special_atomic_attack");
                    equipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_defense")) ? 0 : reader.GetDouble("total_special_atomic_defense");
                    equipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_special_mental_attack")) ? 0 : reader.GetDouble("total_special_mental_attack");
                    equipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_special_mental_defense")) ? 0 : reader.GetDouble("total_special_mental_defense");
                    equipments.special_speed = reader.IsDBNull(reader.GetOrdinal("total_special_speed")) ? 0 : reader.GetDouble("total_special_speed");

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
        return equipments;
    }
    public Equipments GetAllEquipmentsByCardCaptainsId(int Id)
    {
        Equipments equipments = new Equipments();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT SUM(ue.power) AS total_power, SUM(ue.health) AS total_health,
                    SUM(ue.physical_attack) AS total_physical_attack, SUM(ue.physical_defense) AS total_physical_defense,
                    SUM(ue.magical_attack) AS total_magical_attack, SUM(ue.magical_defense) AS total_magical_defense,
                    SUM(ue.chemical_attack) AS total_chemical_attack, SUM(ue.chemical_defense) AS total_chemical_defense,
                    SUM(ue.atomic_attack) AS total_atomic_attack, SUM(ue.atomic_defense) AS total_atomic_defense,
                    SUM(ue.mental_attack) AS total_mental_attack, SUM(ue.mental_defense) AS total_mental_defense,
                    SUM(ue.speed) AS total_speed, SUM(ue.critical_damage) AS total_critical_damage,
                    SUM(ue.critical_rate) AS total_critical_rate, SUM(ue.armor_penetration) AS total_armor_penetration,
                    SUM(ue.avoid) AS total_avoid, SUM(ue.absorbs_damage) AS total_absorbs_damage,
                    SUM(ue.regenerate_vitality) AS total_regenerate_vitality, SUM(ue.accuracy) AS total_accuracy,
                    SUM(ue.mana) AS total_mana, SUM(ue.special_health) AS total_special_health,
                    SUM(ue.special_physical_attack) AS total_special_physical_attack, SUM(ue.special_physical_defense) AS total_special_physical_defense,
                    SUM(ue.special_magical_attack) AS total_special_magical_attack, SUM(ue.special_magical_defense) AS total_special_magical_defense,
                    SUM(ue.special_chemical_attack) AS total_special_chemical_attack, SUM(ue.special_chemical_defense) AS total_special_chemical_defense,
                    SUM(ue.special_atomic_attack) AS total_special_atomic_attack, SUM(ue.special_atomic_defense) AS total_special_atomic_defense,
                    SUM(ue.special_mental_attack) AS total_special_mental_attack, SUM(ue.special_mental_defense) AS total_special_mental_defense,
                    SUM(ue.special_speed) AS total_special_speed
                FROM user_card_captains uc, card_captains c, card_captains_equipment che, user_equipments ue
                WHERE uc.card_captain_id = c.id AND uc.card_captain_id = che.card_captain_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_captain_id = @card_captain_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_captain_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    equipments.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                    equipments.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                    equipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                    equipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                    equipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                    equipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                    equipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                    equipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                    equipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                    equipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                    equipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                    equipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                    equipments.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                    equipments.critical_damage = reader.IsDBNull(reader.GetOrdinal("total_critical_damage")) ? 0 : reader.GetDouble("total_critical_damage");
                    equipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                    equipments.armor_penetration = reader.IsDBNull(reader.GetOrdinal("total_armor_penetration")) ? 0 : reader.GetDouble("total_armor_penetration");
                    equipments.avoid = reader.IsDBNull(reader.GetOrdinal("total_avoid")) ? 0 : reader.GetDouble("total_avoid");
                    equipments.absorbs_damage = reader.IsDBNull(reader.GetOrdinal("total_absorbs_damage")) ? 0 : reader.GetDouble("total_absorbs_damage");
                    equipments.regenerate_vitality = reader.IsDBNull(reader.GetOrdinal("total_regenerate_vitality")) ? 0 : reader.GetDouble("total_regenerate_vitality");
                    equipments.accuracy = reader.IsDBNull(reader.GetOrdinal("total_accuracy")) ? 0 : reader.GetDouble("total_accuracy");
                    equipments.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                    equipments.special_health = reader.IsDBNull(reader.GetOrdinal("total_special_health")) ? 0 : reader.GetDouble("total_special_health");
                    equipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_physical_attack")) ? 0 : reader.GetDouble("total_special_physical_attack");
                    equipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_physical_defense")) ? 0 : reader.GetDouble("total_special_physical_defense");
                    equipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_magical_attack")) ? 0 : reader.GetDouble("total_special_magical_attack");
                    equipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_magical_defense")) ? 0 : reader.GetDouble("total_special_magical_defense");
                    equipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_attack")) ? 0 : reader.GetDouble("total_special_chemical_attack");
                    equipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_defense")) ? 0 : reader.GetDouble("total_special_chemical_defense");
                    equipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_attack")) ? 0 : reader.GetDouble("total_special_atomic_attack");
                    equipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_defense")) ? 0 : reader.GetDouble("total_special_atomic_defense");
                    equipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_special_mental_attack")) ? 0 : reader.GetDouble("total_special_mental_attack");
                    equipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_special_mental_defense")) ? 0 : reader.GetDouble("total_special_mental_defense");
                    equipments.special_speed = reader.IsDBNull(reader.GetOrdinal("total_special_speed")) ? 0 : reader.GetDouble("total_special_speed");

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
        return equipments;
    }
    public Equipments GetAllEquipmentsByCardColonelsId(int Id)
    {
        Equipments equipments = new Equipments();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT SUM(ue.power) AS total_power, SUM(ue.health) AS total_health,
                    SUM(ue.physical_attack) AS total_physical_attack, SUM(ue.physical_defense) AS total_physical_defense,
                    SUM(ue.magical_attack) AS total_magical_attack, SUM(ue.magical_defense) AS total_magical_defense,
                    SUM(ue.chemical_attack) AS total_chemical_attack, SUM(ue.chemical_defense) AS total_chemical_defense,
                    SUM(ue.atomic_attack) AS total_atomic_attack, SUM(ue.atomic_defense) AS total_atomic_defense,
                    SUM(ue.mental_attack) AS total_mental_attack, SUM(ue.mental_defense) AS total_mental_defense,
                    SUM(ue.speed) AS total_speed, SUM(ue.critical_damage) AS total_critical_damage,
                    SUM(ue.critical_rate) AS total_critical_rate, SUM(ue.armor_penetration) AS total_armor_penetration,
                    SUM(ue.avoid) AS total_avoid, SUM(ue.absorbs_damage) AS total_absorbs_damage,
                    SUM(ue.regenerate_vitality) AS total_regenerate_vitality, SUM(ue.accuracy) AS total_accuracy,
                    SUM(ue.mana) AS total_mana, SUM(ue.special_health) AS total_special_health,
                    SUM(ue.special_physical_attack) AS total_special_physical_attack, SUM(ue.special_physical_defense) AS total_special_physical_defense,
                    SUM(ue.special_magical_attack) AS total_special_magical_attack, SUM(ue.special_magical_defense) AS total_special_magical_defense,
                    SUM(ue.special_chemical_attack) AS total_special_chemical_attack, SUM(ue.special_chemical_defense) AS total_special_chemical_defense,
                    SUM(ue.special_atomic_attack) AS total_special_atomic_attack, SUM(ue.special_atomic_defense) AS total_special_atomic_defense,
                    SUM(ue.special_mental_attack) AS total_special_mental_attack, SUM(ue.special_mental_defense) AS total_special_mental_defense,
                    SUM(ue.special_speed) AS total_special_speed
                FROM user_card_colonels uc, card_colonels c, card_colonels_equipment che, user_equipments ue
                WHERE uc.card_colonel_id = c.id AND uc.card_colonel_id = che.card_colonel_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_colonel_id = @card_colonel_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_colonel_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    equipments.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                    equipments.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                    equipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                    equipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                    equipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                    equipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                    equipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                    equipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                    equipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                    equipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                    equipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                    equipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                    equipments.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                    equipments.critical_damage = reader.IsDBNull(reader.GetOrdinal("total_critical_damage")) ? 0 : reader.GetDouble("total_critical_damage");
                    equipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                    equipments.armor_penetration = reader.IsDBNull(reader.GetOrdinal("total_armor_penetration")) ? 0 : reader.GetDouble("total_armor_penetration");
                    equipments.avoid = reader.IsDBNull(reader.GetOrdinal("total_avoid")) ? 0 : reader.GetDouble("total_avoid");
                    equipments.absorbs_damage = reader.IsDBNull(reader.GetOrdinal("total_absorbs_damage")) ? 0 : reader.GetDouble("total_absorbs_damage");
                    equipments.regenerate_vitality = reader.IsDBNull(reader.GetOrdinal("total_regenerate_vitality")) ? 0 : reader.GetDouble("total_regenerate_vitality");
                    equipments.accuracy = reader.IsDBNull(reader.GetOrdinal("total_accuracy")) ? 0 : reader.GetDouble("total_accuracy");
                    equipments.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                    equipments.special_health = reader.IsDBNull(reader.GetOrdinal("total_special_health")) ? 0 : reader.GetDouble("total_special_health");
                    equipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_physical_attack")) ? 0 : reader.GetDouble("total_special_physical_attack");
                    equipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_physical_defense")) ? 0 : reader.GetDouble("total_special_physical_defense");
                    equipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_magical_attack")) ? 0 : reader.GetDouble("total_special_magical_attack");
                    equipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_magical_defense")) ? 0 : reader.GetDouble("total_special_magical_defense");
                    equipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_attack")) ? 0 : reader.GetDouble("total_special_chemical_attack");
                    equipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_defense")) ? 0 : reader.GetDouble("total_special_chemical_defense");
                    equipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_attack")) ? 0 : reader.GetDouble("total_special_atomic_attack");
                    equipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_defense")) ? 0 : reader.GetDouble("total_special_atomic_defense");
                    equipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_special_mental_attack")) ? 0 : reader.GetDouble("total_special_mental_attack");
                    equipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_special_mental_defense")) ? 0 : reader.GetDouble("total_special_mental_defense");
                    equipments.special_speed = reader.IsDBNull(reader.GetOrdinal("total_special_speed")) ? 0 : reader.GetDouble("total_special_speed");

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
        return equipments;
    }
    public Equipments GetAllEquipmentsByCardGeneralsId(int Id)
    {
        Equipments equipments = new Equipments();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT SUM(ue.power) AS total_power, SUM(ue.health) AS total_health,
                    SUM(ue.physical_attack) AS total_physical_attack, SUM(ue.physical_defense) AS total_physical_defense,
                    SUM(ue.magical_attack) AS total_magical_attack, SUM(ue.magical_defense) AS total_magical_defense,
                    SUM(ue.chemical_attack) AS total_chemical_attack, SUM(ue.chemical_defense) AS total_chemical_defense,
                    SUM(ue.atomic_attack) AS total_atomic_attack, SUM(ue.atomic_defense) AS total_atomic_defense,
                    SUM(ue.mental_attack) AS total_mental_attack, SUM(ue.mental_defense) AS total_mental_defense,
                    SUM(ue.speed) AS total_speed, SUM(ue.critical_damage) AS total_critical_damage,
                    SUM(ue.critical_rate) AS total_critical_rate, SUM(ue.armor_penetration) AS total_armor_penetration,
                    SUM(ue.avoid) AS total_avoid, SUM(ue.absorbs_damage) AS total_absorbs_damage,
                    SUM(ue.regenerate_vitality) AS total_regenerate_vitality, SUM(ue.accuracy) AS total_accuracy,
                    SUM(ue.mana) AS total_mana, SUM(ue.special_health) AS total_special_health,
                    SUM(ue.special_physical_attack) AS total_special_physical_attack, SUM(ue.special_physical_defense) AS total_special_physical_defense,
                    SUM(ue.special_magical_attack) AS total_special_magical_attack, SUM(ue.special_magical_defense) AS total_special_magical_defense,
                    SUM(ue.special_chemical_attack) AS total_special_chemical_attack, SUM(ue.special_chemical_defense) AS total_special_chemical_defense,
                    SUM(ue.special_atomic_attack) AS total_special_atomic_attack, SUM(ue.special_atomic_defense) AS total_special_atomic_defense,
                    SUM(ue.special_mental_attack) AS total_special_mental_attack, SUM(ue.special_mental_defense) AS total_special_mental_defense,
                    SUM(ue.special_speed) AS total_special_speed
                FROM user_card_generals uc, card_generals c, card_generals_equipment che, user_equipments ue
                WHERE uc.card_general_id = c.id AND uc.card_general_id = che.card_general_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_general_id = @card_general_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_general_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    equipments.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                    equipments.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                    equipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                    equipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                    equipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                    equipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                    equipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                    equipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                    equipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                    equipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                    equipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                    equipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                    equipments.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                    equipments.critical_damage = reader.IsDBNull(reader.GetOrdinal("total_critical_damage")) ? 0 : reader.GetDouble("total_critical_damage");
                    equipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                    equipments.armor_penetration = reader.IsDBNull(reader.GetOrdinal("total_armor_penetration")) ? 0 : reader.GetDouble("total_armor_penetration");
                    equipments.avoid = reader.IsDBNull(reader.GetOrdinal("total_avoid")) ? 0 : reader.GetDouble("total_avoid");
                    equipments.absorbs_damage = reader.IsDBNull(reader.GetOrdinal("total_absorbs_damage")) ? 0 : reader.GetDouble("total_absorbs_damage");
                    equipments.regenerate_vitality = reader.IsDBNull(reader.GetOrdinal("total_regenerate_vitality")) ? 0 : reader.GetDouble("total_regenerate_vitality");
                    equipments.accuracy = reader.IsDBNull(reader.GetOrdinal("total_accuracy")) ? 0 : reader.GetDouble("total_accuracy");
                    equipments.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                    equipments.special_health = reader.IsDBNull(reader.GetOrdinal("total_special_health")) ? 0 : reader.GetDouble("total_special_health");
                    equipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_physical_attack")) ? 0 : reader.GetDouble("total_special_physical_attack");
                    equipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_physical_defense")) ? 0 : reader.GetDouble("total_special_physical_defense");
                    equipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_magical_attack")) ? 0 : reader.GetDouble("total_special_magical_attack");
                    equipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_magical_defense")) ? 0 : reader.GetDouble("total_special_magical_defense");
                    equipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_attack")) ? 0 : reader.GetDouble("total_special_chemical_attack");
                    equipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_defense")) ? 0 : reader.GetDouble("total_special_chemical_defense");
                    equipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_attack")) ? 0 : reader.GetDouble("total_special_atomic_attack");
                    equipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_defense")) ? 0 : reader.GetDouble("total_special_atomic_defense");
                    equipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_special_mental_attack")) ? 0 : reader.GetDouble("total_special_mental_attack");
                    equipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_special_mental_defense")) ? 0 : reader.GetDouble("total_special_mental_defense");
                    equipments.special_speed = reader.IsDBNull(reader.GetOrdinal("total_special_speed")) ? 0 : reader.GetDouble("total_special_speed");

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
        return equipments;
    }
    public Equipments GetAllEquipmentsByCardAdmiralsId(int Id)
    {
        Equipments equipments = new Equipments();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT SUM(ue.power) AS total_power, SUM(ue.health) AS total_health,
                    SUM(ue.physical_attack) AS total_physical_attack, SUM(ue.physical_defense) AS total_physical_defense,
                    SUM(ue.magical_attack) AS total_magical_attack, SUM(ue.magical_defense) AS total_magical_defense,
                    SUM(ue.chemical_attack) AS total_chemical_attack, SUM(ue.chemical_defense) AS total_chemical_defense,
                    SUM(ue.atomic_attack) AS total_atomic_attack, SUM(ue.atomic_defense) AS total_atomic_defense,
                    SUM(ue.mental_attack) AS total_mental_attack, SUM(ue.mental_defense) AS total_mental_defense,
                    SUM(ue.speed) AS total_speed, SUM(ue.critical_damage) AS total_critical_damage,
                    SUM(ue.critical_rate) AS total_critical_rate, SUM(ue.armor_penetration) AS total_armor_penetration,
                    SUM(ue.avoid) AS total_avoid, SUM(ue.absorbs_damage) AS total_absorbs_damage,
                    SUM(ue.regenerate_vitality) AS total_regenerate_vitality, SUM(ue.accuracy) AS total_accuracy,
                    SUM(ue.mana) AS total_mana, SUM(ue.special_health) AS total_special_health,
                    SUM(ue.special_physical_attack) AS total_special_physical_attack, SUM(ue.special_physical_defense) AS total_special_physical_defense,
                    SUM(ue.special_magical_attack) AS total_special_magical_attack, SUM(ue.special_magical_defense) AS total_special_magical_defense,
                    SUM(ue.special_chemical_attack) AS total_special_chemical_attack, SUM(ue.special_chemical_defense) AS total_special_chemical_defense,
                    SUM(ue.special_atomic_attack) AS total_special_atomic_attack, SUM(ue.special_atomic_defense) AS total_special_atomic_defense,
                    SUM(ue.special_mental_attack) AS total_special_mental_attack, SUM(ue.special_mental_defense) AS total_special_mental_defense,
                    SUM(ue.special_speed) AS total_special_speed
                FROM user_card_admirals uc, card_admirals c, card_admirals_equipment che, user_equipments ue
                WHERE uc.card_admiral_id = c.id AND uc.card_hero_id = che.card_hero_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_admiral_id = @card_admiral_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_admiral_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    equipments.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                    equipments.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                    equipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                    equipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                    equipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                    equipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                    equipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                    equipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                    equipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                    equipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                    equipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                    equipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                    equipments.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                    equipments.critical_damage = reader.IsDBNull(reader.GetOrdinal("total_critical_damage")) ? 0 : reader.GetDouble("total_critical_damage");
                    equipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                    equipments.armor_penetration = reader.IsDBNull(reader.GetOrdinal("total_armor_penetration")) ? 0 : reader.GetDouble("total_armor_penetration");
                    equipments.avoid = reader.IsDBNull(reader.GetOrdinal("total_avoid")) ? 0 : reader.GetDouble("total_avoid");
                    equipments.absorbs_damage = reader.IsDBNull(reader.GetOrdinal("total_absorbs_damage")) ? 0 : reader.GetDouble("total_absorbs_damage");
                    equipments.regenerate_vitality = reader.IsDBNull(reader.GetOrdinal("total_regenerate_vitality")) ? 0 : reader.GetDouble("total_regenerate_vitality");
                    equipments.accuracy = reader.IsDBNull(reader.GetOrdinal("total_accuracy")) ? 0 : reader.GetDouble("total_accuracy");
                    equipments.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                    equipments.special_health = reader.IsDBNull(reader.GetOrdinal("total_special_health")) ? 0 : reader.GetDouble("total_special_health");
                    equipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_physical_attack")) ? 0 : reader.GetDouble("total_special_physical_attack");
                    equipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_physical_defense")) ? 0 : reader.GetDouble("total_special_physical_defense");
                    equipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_magical_attack")) ? 0 : reader.GetDouble("total_special_magical_attack");
                    equipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_magical_defense")) ? 0 : reader.GetDouble("total_special_magical_defense");
                    equipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_attack")) ? 0 : reader.GetDouble("total_special_chemical_attack");
                    equipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_defense")) ? 0 : reader.GetDouble("total_special_chemical_defense");
                    equipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_attack")) ? 0 : reader.GetDouble("total_special_atomic_attack");
                    equipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_defense")) ? 0 : reader.GetDouble("total_special_atomic_defense");
                    equipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_special_mental_attack")) ? 0 : reader.GetDouble("total_special_mental_attack");
                    equipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_special_mental_defense")) ? 0 : reader.GetDouble("total_special_mental_defense");
                    equipments.special_speed = reader.IsDBNull(reader.GetOrdinal("total_special_speed")) ? 0 : reader.GetDouble("total_special_speed");

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
        return equipments;
    }
    public Equipments GetAllEquipmentsByCardMonstersId(int Id)
    {
        Equipments equipments = new Equipments();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT SUM(ue.power) AS total_power, SUM(ue.health) AS total_health,
                    SUM(ue.physical_attack) AS total_physical_attack, SUM(ue.physical_defense) AS total_physical_defense,
                    SUM(ue.magical_attack) AS total_magical_attack, SUM(ue.magical_defense) AS total_magical_defense,
                    SUM(ue.chemical_attack) AS total_chemical_attack, SUM(ue.chemical_defense) AS total_chemical_defense,
                    SUM(ue.atomic_attack) AS total_atomic_attack, SUM(ue.atomic_defense) AS total_atomic_defense,
                    SUM(ue.mental_attack) AS total_mental_attack, SUM(ue.mental_defense) AS total_mental_defense,
                    SUM(ue.speed) AS total_speed, SUM(ue.critical_damage) AS total_critical_damage,
                    SUM(ue.critical_rate) AS total_critical_rate, SUM(ue.armor_penetration) AS total_armor_penetration,
                    SUM(ue.avoid) AS total_avoid, SUM(ue.absorbs_damage) AS total_absorbs_damage,
                    SUM(ue.regenerate_vitality) AS total_regenerate_vitality, SUM(ue.accuracy) AS total_accuracy,
                    SUM(ue.mana) AS total_mana, SUM(ue.special_health) AS total_special_health,
                    SUM(ue.special_physical_attack) AS total_special_physical_attack, SUM(ue.special_physical_defense) AS total_special_physical_defense,
                    SUM(ue.special_magical_attack) AS total_special_magical_attack, SUM(ue.special_magical_defense) AS total_special_magical_defense,
                    SUM(ue.special_chemical_attack) AS total_special_chemical_attack, SUM(ue.special_chemical_defense) AS total_special_chemical_defense,
                    SUM(ue.special_atomic_attack) AS total_special_atomic_attack, SUM(ue.special_atomic_defense) AS total_special_atomic_defense,
                    SUM(ue.special_mental_attack) AS total_special_mental_attack, SUM(ue.special_mental_defense) AS total_special_mental_defense,
                    SUM(ue.special_speed) AS total_special_speed
                FROM user_card_monsters uc, card_monsters c, card_monsters_equipment che, user_equipments ue
                WHERE uc.card_monster_id = c.id AND uc.card_monster_id = che.card_monster_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_monster_id = @card_monster_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_monster_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    equipments.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                    equipments.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                    equipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                    equipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                    equipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                    equipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                    equipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                    equipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                    equipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                    equipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                    equipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                    equipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                    equipments.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                    equipments.critical_damage = reader.IsDBNull(reader.GetOrdinal("total_critical_damage")) ? 0 : reader.GetDouble("total_critical_damage");
                    equipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                    equipments.armor_penetration = reader.IsDBNull(reader.GetOrdinal("total_armor_penetration")) ? 0 : reader.GetDouble("total_armor_penetration");
                    equipments.avoid = reader.IsDBNull(reader.GetOrdinal("total_avoid")) ? 0 : reader.GetDouble("total_avoid");
                    equipments.absorbs_damage = reader.IsDBNull(reader.GetOrdinal("total_absorbs_damage")) ? 0 : reader.GetDouble("total_absorbs_damage");
                    equipments.regenerate_vitality = reader.IsDBNull(reader.GetOrdinal("total_regenerate_vitality")) ? 0 : reader.GetDouble("total_regenerate_vitality");
                    equipments.accuracy = reader.IsDBNull(reader.GetOrdinal("total_accuracy")) ? 0 : reader.GetDouble("total_accuracy");
                    equipments.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                    equipments.special_health = reader.IsDBNull(reader.GetOrdinal("total_special_health")) ? 0 : reader.GetDouble("total_special_health");
                    equipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_physical_attack")) ? 0 : reader.GetDouble("total_special_physical_attack");
                    equipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_physical_defense")) ? 0 : reader.GetDouble("total_special_physical_defense");
                    equipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_magical_attack")) ? 0 : reader.GetDouble("total_special_magical_attack");
                    equipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_magical_defense")) ? 0 : reader.GetDouble("total_special_magical_defense");
                    equipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_attack")) ? 0 : reader.GetDouble("total_special_chemical_attack");
                    equipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_defense")) ? 0 : reader.GetDouble("total_special_chemical_defense");
                    equipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_attack")) ? 0 : reader.GetDouble("total_special_atomic_attack");
                    equipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_defense")) ? 0 : reader.GetDouble("total_special_atomic_defense");
                    equipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_special_mental_attack")) ? 0 : reader.GetDouble("total_special_mental_attack");
                    equipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_special_mental_defense")) ? 0 : reader.GetDouble("total_special_mental_defense");
                    equipments.special_speed = reader.IsDBNull(reader.GetOrdinal("total_special_speed")) ? 0 : reader.GetDouble("total_special_speed");

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
        return equipments;
    }
    public Equipments GetAllEquipmentsByCardMilitaryId(int Id)
    {
        Equipments equipments = new Equipments();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT SUM(ue.power) AS total_power, SUM(ue.health) AS total_health,
                    SUM(ue.physical_attack) AS total_physical_attack, SUM(ue.physical_defense) AS total_physical_defense,
                    SUM(ue.magical_attack) AS total_magical_attack, SUM(ue.magical_defense) AS total_magical_defense,
                    SUM(ue.chemical_attack) AS total_chemical_attack, SUM(ue.chemical_defense) AS total_chemical_defense,
                    SUM(ue.atomic_attack) AS total_atomic_attack, SUM(ue.atomic_defense) AS total_atomic_defense,
                    SUM(ue.mental_attack) AS total_mental_attack, SUM(ue.mental_defense) AS total_mental_defense,
                    SUM(ue.speed) AS total_speed, SUM(ue.critical_damage) AS total_critical_damage,
                    SUM(ue.critical_rate) AS total_critical_rate, SUM(ue.armor_penetration) AS total_armor_penetration,
                    SUM(ue.avoid) AS total_avoid, SUM(ue.absorbs_damage) AS total_absorbs_damage,
                    SUM(ue.regenerate_vitality) AS total_regenerate_vitality, SUM(ue.accuracy) AS total_accuracy,
                    SUM(ue.mana) AS total_mana, SUM(ue.special_health) AS total_special_health,
                    SUM(ue.special_physical_attack) AS total_special_physical_attack, SUM(ue.special_physical_defense) AS total_special_physical_defense,
                    SUM(ue.special_magical_attack) AS total_special_magical_attack, SUM(ue.special_magical_defense) AS total_special_magical_defense,
                    SUM(ue.special_chemical_attack) AS total_special_chemical_attack, SUM(ue.special_chemical_defense) AS total_special_chemical_defense,
                    SUM(ue.special_atomic_attack) AS total_special_atomic_attack, SUM(ue.special_atomic_defense) AS total_special_atomic_defense,
                    SUM(ue.special_mental_attack) AS total_special_mental_attack, SUM(ue.special_mental_defense) AS total_special_mental_defense,
                    SUM(ue.special_speed) AS total_special_speed
                FROM user_card_military uc, card_military c, card_military_equipment che, user_equipments ue
                WHERE uc.card_military_id = c.id AND uc.card_military_id = che.card_military_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_military_id = @card_military_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_military_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    equipments.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                    equipments.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                    equipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                    equipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                    equipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                    equipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                    equipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                    equipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                    equipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                    equipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                    equipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                    equipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                    equipments.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                    equipments.critical_damage = reader.IsDBNull(reader.GetOrdinal("total_critical_damage")) ? 0 : reader.GetDouble("total_critical_damage");
                    equipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                    equipments.armor_penetration = reader.IsDBNull(reader.GetOrdinal("total_armor_penetration")) ? 0 : reader.GetDouble("total_armor_penetration");
                    equipments.avoid = reader.IsDBNull(reader.GetOrdinal("total_avoid")) ? 0 : reader.GetDouble("total_avoid");
                    equipments.absorbs_damage = reader.IsDBNull(reader.GetOrdinal("total_absorbs_damage")) ? 0 : reader.GetDouble("total_absorbs_damage");
                    equipments.regenerate_vitality = reader.IsDBNull(reader.GetOrdinal("total_regenerate_vitality")) ? 0 : reader.GetDouble("total_regenerate_vitality");
                    equipments.accuracy = reader.IsDBNull(reader.GetOrdinal("total_accuracy")) ? 0 : reader.GetDouble("total_accuracy");
                    equipments.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                    equipments.special_health = reader.IsDBNull(reader.GetOrdinal("total_special_health")) ? 0 : reader.GetDouble("total_special_health");
                    equipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_physical_attack")) ? 0 : reader.GetDouble("total_special_physical_attack");
                    equipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_physical_defense")) ? 0 : reader.GetDouble("total_special_physical_defense");
                    equipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_magical_attack")) ? 0 : reader.GetDouble("total_special_magical_attack");
                    equipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_magical_defense")) ? 0 : reader.GetDouble("total_special_magical_defense");
                    equipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_attack")) ? 0 : reader.GetDouble("total_special_chemical_attack");
                    equipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_defense")) ? 0 : reader.GetDouble("total_special_chemical_defense");
                    equipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_attack")) ? 0 : reader.GetDouble("total_special_atomic_attack");
                    equipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_defense")) ? 0 : reader.GetDouble("total_special_atomic_defense");
                    equipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_special_mental_attack")) ? 0 : reader.GetDouble("total_special_mental_attack");
                    equipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_special_mental_defense")) ? 0 : reader.GetDouble("total_special_mental_defense");
                    equipments.special_speed = reader.IsDBNull(reader.GetOrdinal("total_special_speed")) ? 0 : reader.GetDouble("total_special_speed");

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
        return equipments;
    }
    public Equipments GetAllEquipmentsByCardSpellId(int Id)
    {
        Equipments equipments = new Equipments();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT SUM(ue.power) AS total_power, SUM(ue.health) AS total_health,
                    SUM(ue.physical_attack) AS total_physical_attack, SUM(ue.physical_defense) AS total_physical_defense,
                    SUM(ue.magical_attack) AS total_magical_attack, SUM(ue.magical_defense) AS total_magical_defense,
                    SUM(ue.chemical_attack) AS total_chemical_attack, SUM(ue.chemical_defense) AS total_chemical_defense,
                    SUM(ue.atomic_attack) AS total_atomic_attack, SUM(ue.atomic_defense) AS total_atomic_defense,
                    SUM(ue.mental_attack) AS total_mental_attack, SUM(ue.mental_defense) AS total_mental_defense,
                    SUM(ue.speed) AS total_speed, SUM(ue.critical_damage) AS total_critical_damage,
                    SUM(ue.critical_rate) AS total_critical_rate, SUM(ue.armor_penetration) AS total_armor_penetration,
                    SUM(ue.avoid) AS total_avoid, SUM(ue.absorbs_damage) AS total_absorbs_damage,
                    SUM(ue.regenerate_vitality) AS total_regenerate_vitality, SUM(ue.accuracy) AS total_accuracy,
                    SUM(ue.mana) AS total_mana, SUM(ue.special_health) AS total_special_health,
                    SUM(ue.special_physical_attack) AS total_special_physical_attack, SUM(ue.special_physical_defense) AS total_special_physical_defense,
                    SUM(ue.special_magical_attack) AS total_special_magical_attack, SUM(ue.special_magical_defense) AS total_special_magical_defense,
                    SUM(ue.special_chemical_attack) AS total_special_chemical_attack, SUM(ue.special_chemical_defense) AS total_special_chemical_defense,
                    SUM(ue.special_atomic_attack) AS total_special_atomic_attack, SUM(ue.special_atomic_defense) AS total_special_atomic_defense,
                    SUM(ue.special_mental_attack) AS total_special_mental_attack, SUM(ue.special_mental_defense) AS total_special_mental_defense,
                    SUM(ue.special_speed) AS total_special_speed
                FROM user_card_spell uc, card_spell c, card_spell_equipment che, user_equipments ue
                WHERE uc.card_spell_id = c.id AND uc.card_spell_id = che.card_spell_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_spell_id = @card_spell_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_spell_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    equipments.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                    equipments.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                    equipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                    equipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                    equipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                    equipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                    equipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                    equipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                    equipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                    equipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                    equipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                    equipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                    equipments.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                    equipments.critical_damage = reader.IsDBNull(reader.GetOrdinal("total_critical_damage")) ? 0 : reader.GetDouble("total_critical_damage");
                    equipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                    equipments.armor_penetration = reader.IsDBNull(reader.GetOrdinal("total_armor_penetration")) ? 0 : reader.GetDouble("total_armor_penetration");
                    equipments.avoid = reader.IsDBNull(reader.GetOrdinal("total_avoid")) ? 0 : reader.GetDouble("total_avoid");
                    equipments.absorbs_damage = reader.IsDBNull(reader.GetOrdinal("total_absorbs_damage")) ? 0 : reader.GetDouble("total_absorbs_damage");
                    equipments.regenerate_vitality = reader.IsDBNull(reader.GetOrdinal("total_regenerate_vitality")) ? 0 : reader.GetDouble("total_regenerate_vitality");
                    equipments.accuracy = reader.IsDBNull(reader.GetOrdinal("total_accuracy")) ? 0 : reader.GetDouble("total_accuracy");
                    equipments.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                    equipments.special_health = reader.IsDBNull(reader.GetOrdinal("total_special_health")) ? 0 : reader.GetDouble("total_special_health");
                    equipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_physical_attack")) ? 0 : reader.GetDouble("total_special_physical_attack");
                    equipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_physical_defense")) ? 0 : reader.GetDouble("total_special_physical_defense");
                    equipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_magical_attack")) ? 0 : reader.GetDouble("total_special_magical_attack");
                    equipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_magical_defense")) ? 0 : reader.GetDouble("total_special_magical_defense");
                    equipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_attack")) ? 0 : reader.GetDouble("total_special_chemical_attack");
                    equipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_defense")) ? 0 : reader.GetDouble("total_special_chemical_defense");
                    equipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_attack")) ? 0 : reader.GetDouble("total_special_atomic_attack");
                    equipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_defense")) ? 0 : reader.GetDouble("total_special_atomic_defense");
                    equipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_special_mental_attack")) ? 0 : reader.GetDouble("total_special_mental_attack");
                    equipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_special_mental_defense")) ? 0 : reader.GetDouble("total_special_mental_defense");
                    equipments.special_speed = reader.IsDBNull(reader.GetOrdinal("total_special_speed")) ? 0 : reader.GetDouble("total_special_speed");

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
        return equipments;
    }
    public Equipments GetAllEquipmentsByBooksId(int Id)
    {
        Equipments equipments = new Equipments();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT SUM(ue.power) AS total_power, SUM(ue.health) AS total_health,
                    SUM(ue.physical_attack) AS total_physical_attack, SUM(ue.physical_defense) AS total_physical_defense,
                    SUM(ue.magical_attack) AS total_magical_attack, SUM(ue.magical_defense) AS total_magical_defense,
                    SUM(ue.chemical_attack) AS total_chemical_attack, SUM(ue.chemical_defense) AS total_chemical_defense,
                    SUM(ue.atomic_attack) AS total_atomic_attack, SUM(ue.atomic_defense) AS total_atomic_defense,
                    SUM(ue.mental_attack) AS total_mental_attack, SUM(ue.mental_defense) AS total_mental_defense,
                    SUM(ue.speed) AS total_speed, SUM(ue.critical_damage) AS total_critical_damage,
                    SUM(ue.critical_rate) AS total_critical_rate, SUM(ue.armor_penetration) AS total_armor_penetration,
                    SUM(ue.avoid) AS total_avoid, SUM(ue.absorbs_damage) AS total_absorbs_damage,
                    SUM(ue.regenerate_vitality) AS total_regenerate_vitality, SUM(ue.accuracy) AS total_accuracy,
                    SUM(ue.mana) AS total_mana, SUM(ue.special_health) AS total_special_health,
                    SUM(ue.special_physical_attack) AS total_special_physical_attack, SUM(ue.special_physical_defense) AS total_special_physical_defense,
                    SUM(ue.special_magical_attack) AS total_special_magical_attack, SUM(ue.special_magical_defense) AS total_special_magical_defense,
                    SUM(ue.special_chemical_attack) AS total_special_chemical_attack, SUM(ue.special_chemical_defense) AS total_special_chemical_defense,
                    SUM(ue.special_atomic_attack) AS total_special_atomic_attack, SUM(ue.special_atomic_defense) AS total_special_atomic_defense,
                    SUM(ue.special_mental_attack) AS total_special_mental_attack, SUM(ue.special_mental_defense) AS total_special_mental_defense,
                    SUM(ue.special_speed) AS total_special_speed
                FROM user_books uc, books c, books_equipment che, user_equipments ue
                WHERE uc.book_id = c.id AND uc.book_id = che.book_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.book_id = @book_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@book_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    equipments.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                    equipments.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                    equipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                    equipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                    equipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                    equipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                    equipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                    equipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                    equipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                    equipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                    equipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                    equipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                    equipments.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                    equipments.critical_damage = reader.IsDBNull(reader.GetOrdinal("total_critical_damage")) ? 0 : reader.GetDouble("total_critical_damage");
                    equipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                    equipments.armor_penetration = reader.IsDBNull(reader.GetOrdinal("total_armor_penetration")) ? 0 : reader.GetDouble("total_armor_penetration");
                    equipments.avoid = reader.IsDBNull(reader.GetOrdinal("total_avoid")) ? 0 : reader.GetDouble("total_avoid");
                    equipments.absorbs_damage = reader.IsDBNull(reader.GetOrdinal("total_absorbs_damage")) ? 0 : reader.GetDouble("total_absorbs_damage");
                    equipments.regenerate_vitality = reader.IsDBNull(reader.GetOrdinal("total_regenerate_vitality")) ? 0 : reader.GetDouble("total_regenerate_vitality");
                    equipments.accuracy = reader.IsDBNull(reader.GetOrdinal("total_accuracy")) ? 0 : reader.GetDouble("total_accuracy");
                    equipments.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                    equipments.special_health = reader.IsDBNull(reader.GetOrdinal("total_special_health")) ? 0 : reader.GetDouble("total_special_health");
                    equipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_physical_attack")) ? 0 : reader.GetDouble("total_special_physical_attack");
                    equipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_physical_defense")) ? 0 : reader.GetDouble("total_special_physical_defense");
                    equipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_magical_attack")) ? 0 : reader.GetDouble("total_special_magical_attack");
                    equipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_magical_defense")) ? 0 : reader.GetDouble("total_special_magical_defense");
                    equipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_attack")) ? 0 : reader.GetDouble("total_special_chemical_attack");
                    equipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_defense")) ? 0 : reader.GetDouble("total_special_chemical_defense");
                    equipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_attack")) ? 0 : reader.GetDouble("total_special_atomic_attack");
                    equipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_defense")) ? 0 : reader.GetDouble("total_special_atomic_defense");
                    equipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_special_mental_attack")) ? 0 : reader.GetDouble("total_special_mental_attack");
                    equipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_special_mental_defense")) ? 0 : reader.GetDouble("total_special_mental_defense");
                    equipments.special_speed = reader.IsDBNull(reader.GetOrdinal("total_special_speed")) ? 0 : reader.GetDouble("total_special_speed");

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
        return equipments;
    }
    public Equipments GetAllEquipmentsByPetsId(int Id)
    {
        Equipments equipments = new Equipments();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT SUM(ue.power) AS total_power, SUM(ue.health) AS total_health,
                    SUM(ue.physical_attack) AS total_physical_attack, SUM(ue.physical_defense) AS total_physical_defense,
                    SUM(ue.magical_attack) AS total_magical_attack, SUM(ue.magical_defense) AS total_magical_defense,
                    SUM(ue.chemical_attack) AS total_chemical_attack, SUM(ue.chemical_defense) AS total_chemical_defense,
                    SUM(ue.atomic_attack) AS total_atomic_attack, SUM(ue.atomic_defense) AS total_atomic_defense,
                    SUM(ue.mental_attack) AS total_mental_attack, SUM(ue.mental_defense) AS total_mental_defense,
                    SUM(ue.speed) AS total_speed, SUM(ue.critical_damage) AS total_critical_damage,
                    SUM(ue.critical_rate) AS total_critical_rate, SUM(ue.armor_penetration) AS total_armor_penetration,
                    SUM(ue.avoid) AS total_avoid, SUM(ue.absorbs_damage) AS total_absorbs_damage,
                    SUM(ue.regenerate_vitality) AS total_regenerate_vitality, SUM(ue.accuracy) AS total_accuracy,
                    SUM(ue.mana) AS total_mana, SUM(ue.special_health) AS total_special_health,
                    SUM(ue.special_physical_attack) AS total_special_physical_attack, SUM(ue.special_physical_defense) AS total_special_physical_defense,
                    SUM(ue.special_magical_attack) AS total_special_magical_attack, SUM(ue.special_magical_defense) AS total_special_magical_defense,
                    SUM(ue.special_chemical_attack) AS total_special_chemical_attack, SUM(ue.special_chemical_defense) AS total_special_chemical_defense,
                    SUM(ue.special_atomic_attack) AS total_special_atomic_attack, SUM(ue.special_atomic_defense) AS total_special_atomic_defense,
                    SUM(ue.special_mental_attack) AS total_special_mental_attack, SUM(ue.special_mental_defense) AS total_special_mental_defense,
                    SUM(ue.special_speed) AS total_special_speed
                FROM user_pets uc, pets c, pets_equipment che, user_equipments ue
                WHERE uc.pet_id = c.id AND uc.pet_id = che.pet_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.pet_id = @pet_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@pet_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    equipments.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                    equipments.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                    equipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                    equipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                    equipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                    equipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                    equipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                    equipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                    equipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                    equipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                    equipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                    equipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                    equipments.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                    equipments.critical_damage = reader.IsDBNull(reader.GetOrdinal("total_critical_damage")) ? 0 : reader.GetDouble("total_critical_damage");
                    equipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                    equipments.armor_penetration = reader.IsDBNull(reader.GetOrdinal("total_armor_penetration")) ? 0 : reader.GetDouble("total_armor_penetration");
                    equipments.avoid = reader.IsDBNull(reader.GetOrdinal("total_avoid")) ? 0 : reader.GetDouble("total_avoid");
                    equipments.absorbs_damage = reader.IsDBNull(reader.GetOrdinal("total_absorbs_damage")) ? 0 : reader.GetDouble("total_absorbs_damage");
                    equipments.regenerate_vitality = reader.IsDBNull(reader.GetOrdinal("total_regenerate_vitality")) ? 0 : reader.GetDouble("total_regenerate_vitality");
                    equipments.accuracy = reader.IsDBNull(reader.GetOrdinal("total_accuracy")) ? 0 : reader.GetDouble("total_accuracy");
                    equipments.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                    equipments.special_health = reader.IsDBNull(reader.GetOrdinal("total_special_health")) ? 0 : reader.GetDouble("total_special_health");
                    equipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_physical_attack")) ? 0 : reader.GetDouble("total_special_physical_attack");
                    equipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_physical_defense")) ? 0 : reader.GetDouble("total_special_physical_defense");
                    equipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_magical_attack")) ? 0 : reader.GetDouble("total_special_magical_attack");
                    equipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_magical_defense")) ? 0 : reader.GetDouble("total_special_magical_defense");
                    equipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_attack")) ? 0 : reader.GetDouble("total_special_chemical_attack");
                    equipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_special_chemical_defense")) ? 0 : reader.GetDouble("total_special_chemical_defense");
                    equipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_attack")) ? 0 : reader.GetDouble("total_special_atomic_attack");
                    equipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_special_atomic_defense")) ? 0 : reader.GetDouble("total_special_atomic_defense");
                    equipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_special_mental_attack")) ? 0 : reader.GetDouble("total_special_mental_attack");
                    equipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_special_mental_defense")) ? 0 : reader.GetDouble("total_special_mental_defense");
                    equipments.special_speed = reader.IsDBNull(reader.GetOrdinal("total_special_speed")) ? 0 : reader.GetDouble("total_special_speed");

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
        return equipments;
    }
}
