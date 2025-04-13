using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class CollaborationEquipment
{
    private int id1;
    private string name1;
    private string image1;
    private string rare1;
    private string type1;
    private int star1;
    private int level1;
    private int experiment1;
    private int quantity1;
    private bool block1;
    private double power1;
    private double health1;
    private double physical_attack1;
    private double physical_defense1;
    private double magical_attack1;
    private double magical_defense1;
    private double chemical_attack1;
    private double chemical_defense1;
    private double atomic_attack1;
    private double atomic_defense1;
    private double mental_attack1;
    private double mental_defense1;
    private double speed1;
    private double critical_damage_rate1;
    private double critical_rate1;
    private double penetration_rate1;
    private double evasion_rate1;
    private double damage_absorption_rate1;
    private double vitality_regeneration_rate1;
    private double accuracy_rate1;
    private double lifesteal_rate1;
    private float mana1;
    private double mana_regeneration_rate1;
    private double shield_strength1;
    private double tenacity1;
    private double resistance_rate1;
    private double combo_rate1;
    private double reflection_rate1;
    private double damage_to_different_faction_rate1;
    private double resistance_to_different_faction_rate1;
    private double damage_to_same_faction_rate1;
    private double resistance_to_same_faction_rate1;
    private string description1;
    private string status1;
    private double percent_all_health1;
    private double percent_all_physical_attack1;
    private double percent_all_physical_defense1;
    private double percent_all_magical_attack1;
    private double percent_all_magical_defense1;
    private double percent_all_chemical_attack1;
    private double percent_all_chemical_defense1;
    private double percent_all_atomic_attack1;
    private double percent_all_atomic_defense1;
    private double percent_all_mental_attack1;
    private double percent_all_mental_defense1;
    private int quality1;

    public int id { get => id1; set => id1 = value; }
    public string name { get => name1; set => name1 = value; }
    public string image { get => image1; set => image1 = value; }
    public string rare { get => rare1; set => rare1 = value; }
    public int quality { get => quality1; set => quality1 = value; }
    public string type { get => type1; set => type1 = value; }
    public int star { get => star1; set => star1 = value; }
    public int level { get => level1; set => level1 = value; }
    public int experiment { get => experiment1; set => experiment1 = value; }
    public int quantity { get => quantity1; set => quantity1 = value; }
    public bool block { get => block1; set => block1 = value; }
    public double power { get => power1; set => power1 = value; }
    public double health { get => health1; set => health1 = value; }
    public double physical_attack { get => physical_attack1; set => physical_attack1 = value; }
    public double physical_defense { get => physical_defense1; set => physical_defense1 = value; }
    public double magical_attack { get => magical_attack1; set => magical_attack1 = value; }
    public double magical_defense { get => magical_defense1; set => magical_defense1 = value; }
    public double chemical_attack { get => chemical_attack1; set => chemical_attack1 = value; }
    public double chemical_defense { get => chemical_defense1; set => chemical_defense1 = value; }
    public double atomic_attack { get => atomic_attack1; set => atomic_attack1 = value; }
    public double atomic_defense { get => atomic_defense1; set => atomic_defense1 = value; }
    public double mental_attack { get => mental_attack1; set => mental_attack1 = value; }
    public double mental_defense { get => mental_defense1; set => mental_defense1 = value; }
    public double speed { get => speed1; set => speed1 = value; }
    public double critical_damage_rate { get => critical_damage_rate1; set => critical_damage_rate1 = value; }
    public double critical_rate { get => critical_rate1; set => critical_rate1 = value; }
    public double penetration_rate { get => penetration_rate1; set => penetration_rate1 = value; }
    public double evasion_rate { get => evasion_rate1; set => evasion_rate1 = value; }
    public double damage_absorption_rate { get => damage_absorption_rate1; set => damage_absorption_rate1 = value; }
    public double vitality_regeneration_rate { get => vitality_regeneration_rate1; set => vitality_regeneration_rate1 = value; }
    public double accuracy_rate { get => accuracy_rate1; set => accuracy_rate1 = value; }
    public double lifesteal_rate { get => lifesteal_rate1; set => lifesteal_rate1 = value; }
    public float mana { get => mana1; set => mana1 = value; }
    public double mana_regeneration_rate { get => mana_regeneration_rate1; set => mana_regeneration_rate1 = value; }
    public double shield_strength { get => shield_strength1; set => shield_strength1 = value; }
    public double tenacity { get => tenacity1; set => tenacity1 = value; }
    public double resistance_rate { get => resistance_rate1; set => resistance_rate1 = value; }
    public double combo_rate { get => combo_rate1; set => combo_rate1 = value; }
    public double reflection_rate { get => reflection_rate1; set => reflection_rate1 = value; }
    public double damage_to_different_faction_rate { get => damage_to_different_faction_rate1; set => damage_to_different_faction_rate1 = value; }
    public double resistance_to_different_faction_rate { get => resistance_to_different_faction_rate1; set => resistance_to_different_faction_rate1 = value; }
    public double damage_to_same_faction_rate { get => damage_to_same_faction_rate1; set => damage_to_same_faction_rate1 = value; }
    public double resistance_to_same_faction_rate { get => resistance_to_same_faction_rate1; set => resistance_to_same_faction_rate1 = value; }
    public string description { get => description1; set => description1 = value; }
    public string status { get => status1; set => status1 = value; }
    public double percent_all_health { get => percent_all_health1; set => percent_all_health1 = value; }
    public double percent_all_physical_attack { get => percent_all_physical_attack1; set => percent_all_physical_attack1 = value; }
    public double percent_all_physical_defense { get => percent_all_physical_defense1; set => percent_all_physical_defense1 = value; }
    public double percent_all_magical_attack { get => percent_all_magical_attack1; set => percent_all_magical_attack1 = value; }
    public double percent_all_magical_defense { get => percent_all_magical_defense1; set => percent_all_magical_defense1 = value; }
    public double percent_all_chemical_attack { get => percent_all_chemical_attack1; set => percent_all_chemical_attack1 = value; }
    public double percent_all_chemical_defense { get => percent_all_chemical_defense1; set => percent_all_chemical_defense1 = value; }
    public double percent_all_atomic_attack { get => percent_all_atomic_attack1; set => percent_all_atomic_attack1 = value; }
    public double percent_all_atomic_defense { get => percent_all_atomic_defense1; set => percent_all_atomic_defense1 = value; }
    public double percent_all_mental_attack { get => percent_all_mental_attack1; set => percent_all_mental_attack1 = value; }
    public double percent_all_mental_defense { get => percent_all_mental_defense1; set => percent_all_mental_defense1 = value; }
    public Currency currency { get; set; }
    public CollaborationEquipment()
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
    public CollaborationEquipment GetNewLevelPower(CollaborationEquipment c, double coefficient)
    {
        CollaborationEquipment orginCard = new CollaborationEquipment();
        orginCard = orginCard.GetCollaborationEquipmentsById(c.id);
        CollaborationEquipment collaborationEquipment = new CollaborationEquipment
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
            critical_damage_rate = c.critical_damage_rate + orginCard.critical_damage_rate * coefficient,
            critical_rate = c.critical_rate + orginCard.critical_rate * coefficient,
            penetration_rate = c.penetration_rate + orginCard.penetration_rate * coefficient,
            evasion_rate = c.evasion_rate + orginCard.evasion_rate * coefficient,
            damage_absorption_rate = c.damage_absorption_rate + orginCard.damage_absorption_rate * coefficient,
            vitality_regeneration_rate = c.vitality_regeneration_rate + orginCard.vitality_regeneration_rate * coefficient,
            accuracy_rate = c.accuracy_rate + orginCard.accuracy_rate * coefficient,
            lifesteal_rate = c.lifesteal_rate + orginCard.lifesteal_rate * coefficient,
            shield_strength = c.shield_strength + orginCard.shield_strength * coefficient,
            tenacity = c.tenacity + orginCard.tenacity * coefficient,
            resistance_rate = c.resistance_rate + orginCard.resistance_rate * coefficient,
            combo_rate = c.combo_rate + orginCard.combo_rate * coefficient,
            reflection_rate = c.reflection_rate + orginCard.reflection_rate * coefficient,
            mana = c.mana + orginCard.mana * (float)coefficient,
            mana_regeneration_rate = c.mana_regeneration_rate + orginCard.mana_regeneration_rate * coefficient,
            damage_to_different_faction_rate = c.damage_to_different_faction_rate + orginCard.damage_to_different_faction_rate * coefficient,
            resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + orginCard.resistance_to_different_faction_rate * coefficient,
            damage_to_same_faction_rate = c.damage_to_same_faction_rate + orginCard.damage_to_same_faction_rate * coefficient,
            resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + orginCard.resistance_to_same_faction_rate * coefficient
        };
        collaborationEquipment.power = PowerManager.CalculatePower(
            collaborationEquipment.health,
            collaborationEquipment.physical_attack, collaborationEquipment.physical_defense,
            collaborationEquipment.magical_attack, collaborationEquipment.magical_defense,
            collaborationEquipment.chemical_attack, collaborationEquipment.chemical_defense,
            collaborationEquipment.atomic_attack, collaborationEquipment.atomic_defense,
            collaborationEquipment.mental_attack, collaborationEquipment.mental_defense,
            collaborationEquipment.speed,
            collaborationEquipment.critical_damage_rate, collaborationEquipment.critical_rate,
            collaborationEquipment.penetration_rate, collaborationEquipment.evasion_rate,
            collaborationEquipment.damage_absorption_rate, collaborationEquipment.vitality_regeneration_rate,
            collaborationEquipment.accuracy_rate, collaborationEquipment.lifesteal_rate,
            collaborationEquipment.shield_strength, collaborationEquipment.tenacity, collaborationEquipment.resistance_rate,
            collaborationEquipment.combo_rate, collaborationEquipment.reflection_rate,
            collaborationEquipment.mana, collaborationEquipment.mana_regeneration_rate,
            collaborationEquipment.damage_to_different_faction_rate, collaborationEquipment.resistance_to_different_faction_rate,
            collaborationEquipment.damage_to_same_faction_rate, collaborationEquipment.resistance_to_same_faction_rate
        );
        return collaborationEquipment;
    }
    public CollaborationEquipment GetNewBreakthroughPower(CollaborationEquipment c, double coefficient)
    {
        CollaborationEquipment orginCard = new CollaborationEquipment();
        orginCard = orginCard.GetCollaborationEquipmentsById(c.id);
        CollaborationEquipment collaborationEquipment = new CollaborationEquipment
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
            critical_damage_rate = c.critical_damage_rate + orginCard.critical_damage_rate * coefficient,
            critical_rate = c.critical_rate + orginCard.critical_rate * coefficient,
            penetration_rate = c.penetration_rate + orginCard.penetration_rate * coefficient,
            evasion_rate = c.evasion_rate + orginCard.evasion_rate * coefficient,
            damage_absorption_rate = c.damage_absorption_rate + orginCard.damage_absorption_rate * coefficient,
            vitality_regeneration_rate = c.vitality_regeneration_rate + orginCard.vitality_regeneration_rate * coefficient,
            accuracy_rate = c.accuracy_rate + orginCard.accuracy_rate * coefficient,
            lifesteal_rate = c.lifesteal_rate + orginCard.lifesteal_rate * coefficient,
            shield_strength = c.shield_strength + orginCard.shield_strength * coefficient,
            tenacity = c.tenacity + orginCard.tenacity * coefficient,
            resistance_rate = c.resistance_rate + orginCard.resistance_rate * coefficient,
            combo_rate = c.combo_rate + orginCard.combo_rate * coefficient,
            reflection_rate = c.reflection_rate + orginCard.reflection_rate * coefficient,
            mana = c.mana + orginCard.mana * (float)coefficient,
            mana_regeneration_rate = c.mana_regeneration_rate + orginCard.mana_regeneration_rate * coefficient,
            damage_to_different_faction_rate = c.damage_to_different_faction_rate + orginCard.damage_to_different_faction_rate * coefficient,
            resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + orginCard.resistance_to_different_faction_rate * coefficient,
            damage_to_same_faction_rate = c.damage_to_same_faction_rate + orginCard.damage_to_same_faction_rate * coefficient,
            resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + orginCard.resistance_to_same_faction_rate * coefficient
        };
        collaborationEquipment.power = PowerManager.CalculatePower(
            collaborationEquipment.health,
            collaborationEquipment.physical_attack, collaborationEquipment.physical_defense,
            collaborationEquipment.magical_attack, collaborationEquipment.magical_defense,
            collaborationEquipment.chemical_attack, collaborationEquipment.chemical_defense,
            collaborationEquipment.atomic_attack, collaborationEquipment.atomic_defense,
            collaborationEquipment.mental_attack, collaborationEquipment.mental_defense,
            collaborationEquipment.speed,
            collaborationEquipment.critical_damage_rate, collaborationEquipment.critical_rate,
            collaborationEquipment.penetration_rate, collaborationEquipment.evasion_rate,
            collaborationEquipment.damage_absorption_rate, collaborationEquipment.vitality_regeneration_rate,
            collaborationEquipment.accuracy_rate, collaborationEquipment.lifesteal_rate,
            collaborationEquipment.shield_strength, collaborationEquipment.tenacity, collaborationEquipment.resistance_rate,
            collaborationEquipment.combo_rate, collaborationEquipment.reflection_rate,
            collaborationEquipment.mana, collaborationEquipment.mana_regeneration_rate,
            collaborationEquipment.damage_to_different_faction_rate, collaborationEquipment.resistance_to_different_faction_rate,
            collaborationEquipment.damage_to_same_faction_rate, collaborationEquipment.resistance_to_same_faction_rate
        );
        return collaborationEquipment;
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
    public List<CollaborationEquipment> GetCollaborationEquipments(string type, int pageSize, int offset)
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
                        quality = reader.GetInt32("quality"),
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
    public int GetCollaborationEquipmentCount(string type)
    {
        int count = 0;
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
    public List<CollaborationEquipment> GetCollaborationEquipmentsCollection(string type, int pageSize, int offset)
    {
        List<CollaborationEquipment> collaborationEquipmentList = new List<CollaborationEquipment>();
        int user_id = User.CurrentUserId;
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
                        quality = reader.GetInt32("quality"),
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
                        status = reader.GetString("status"),
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
    public List<CollaborationEquipment> GetUserCollaborationEquipments(string type, int pageSize, int offset)
    {
        List<CollaborationEquipment> collaborationEquipmentList = new List<CollaborationEquipment>();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select uce.*, ce.image, ce.rare, ce.type, ce.name, ce.description from collaboration_equipments ce, user_collaboration_equipments uce where ce.id=uce.collaboration_equipment_id and uce.user_id=@userId and ce.type= @type 
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
                        id = reader.GetInt32("collaboration_equipment_id"),
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
    public int GetUserCollaborationEquipmentCount(string type)
    {
        int count = 0;
        int user_id = User.CurrentUserId;
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
                string checkQuery = @"
                SELECT COUNT(*) FROM user_collaboration_equipments 
                WHERE user_id = @user_id AND collaboration_equipment_id = @collaboration_equipment_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@collaboration_equipment_id", collaborationEquipment.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_collaboration_equipments (
                    user_id, collaboration_equipment_id, level, experiment, star, quality, block, quantity, power, health, physical_attack, 
                    physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, 
                    atomic_defense, mental_attack, mental_defense, speed, critical_damage_rate, critical_rate, 
                    penetration_rate, evasion_rate, damage_absorption_rate, vitality_regeneration_rate, accuracy_rate, 
                    lifesteal_rate, shield_strength, tenacity, resistance_rate, combo_rate, reflection_rate, 
                    mana, mana_regeneration_rate, damage_to_different_faction_rate, 
                    resistance_to_different_faction_rate, damage_to_same_faction_rate, resistance_to_same_faction_rate
                ) VALUES (
                    @user_id, @collaboration_equipment_id, @level, @experiment, @star, @quality, @block, @quantity, @power, @health, @physical_attack, 
                    @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, 
                    @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage_rate, @critical_rate, 
                    @penetration_rate, @evasion_rate, @damage_absorption_rate, @vitality_regeneration_rate, @accuracy_rate, 
                    @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate, @combo_rate, @reflection_rate, 
                    @mana, @mana_regeneration_rate, @damage_to_different_faction_rate, 
                    @resistance_to_different_faction_rate, @damage_to_same_faction_rate, @resistance_to_same_faction_rate
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@collaboration_equipment_id", collaborationEquipment.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", PowerManager.CheckQuality(collaborationEquipment.rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", 0);
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
                    command.Parameters.AddWithValue("@critical_damage_rate", collaborationEquipment.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", collaborationEquipment.critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", collaborationEquipment.penetration_rate);
                    command.Parameters.AddWithValue("@evasion_rate", collaborationEquipment.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", collaborationEquipment.damage_absorption_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", collaborationEquipment.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", collaborationEquipment.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", collaborationEquipment.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", collaborationEquipment.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", collaborationEquipment.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", collaborationEquipment.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", collaborationEquipment.combo_rate);
                    command.Parameters.AddWithValue("@reflection_rate", collaborationEquipment.reflection_rate);
                    command.Parameters.AddWithValue("@mana", collaborationEquipment.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", collaborationEquipment.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", collaborationEquipment.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", collaborationEquipment.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", collaborationEquipment.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", collaborationEquipment.resistance_to_same_faction_rate);
                    command.ExecuteNonQuery();
                }
                else{
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_collaboration_equipments
                    SET quantity = quantity + 1
                    WHERE user_id = @user_id AND collaboration_equipment_id = @collaboration_equipment_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@collaboration_equipment_id", collaborationEquipment.id);

                    updateCommand.ExecuteNonQuery();
                }

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
    public bool UpdateCollaborationEquipmentsLevel(CollaborationEquipment collaborationEquipment, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_collaboration_equipments
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
                WHERE user_id = @user_id AND collaboration_equipment_id = @collaboration_equipment_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@collaboration_equipment_id", collaborationEquipment.id);
                command.Parameters.AddWithValue("@level", cardLevel);
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
                command.Parameters.AddWithValue("@mental_attack", collaborationEquipment.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", collaborationEquipment.mental_defense);
                command.Parameters.AddWithValue("@speed", collaborationEquipment.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", collaborationEquipment.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", collaborationEquipment.critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", collaborationEquipment.penetration_rate);
                command.Parameters.AddWithValue("@evasion_rate", collaborationEquipment.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", collaborationEquipment.damage_absorption_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", collaborationEquipment.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@accuracy_rate", collaborationEquipment.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", collaborationEquipment.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", collaborationEquipment.shield_strength);
                command.Parameters.AddWithValue("@tenacity", collaborationEquipment.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", collaborationEquipment.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", collaborationEquipment.combo_rate);
                command.Parameters.AddWithValue("@reflection_rate", collaborationEquipment.reflection_rate);
                command.Parameters.AddWithValue("@mana", collaborationEquipment.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", collaborationEquipment.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", collaborationEquipment.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", collaborationEquipment.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", collaborationEquipment.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", collaborationEquipment.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateCollaborationEquipmentsBreakthrough(CollaborationEquipment collaborationEquipment, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_collaboration_equipments
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
                WHERE user_id = @user_id AND collaboration_equipment_id = @collaboration_equipment_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@collaboration_equipment_id", collaborationEquipment.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
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
                command.Parameters.AddWithValue("@mental_attack", collaborationEquipment.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", collaborationEquipment.mental_defense);
                command.Parameters.AddWithValue("@speed", collaborationEquipment.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", collaborationEquipment.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", collaborationEquipment.critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", collaborationEquipment.penetration_rate);
                command.Parameters.AddWithValue("@evasion_rate", collaborationEquipment.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", collaborationEquipment.damage_absorption_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", collaborationEquipment.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@accuracy_rate", collaborationEquipment.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", collaborationEquipment.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", collaborationEquipment.shield_strength);
                command.Parameters.AddWithValue("@tenacity", collaborationEquipment.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", collaborationEquipment.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", collaborationEquipment.combo_rate);
                command.Parameters.AddWithValue("@reflection_rate", collaborationEquipment.reflection_rate);
                command.Parameters.AddWithValue("@mana", collaborationEquipment.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", collaborationEquipment.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", collaborationEquipment.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", collaborationEquipment.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", collaborationEquipment.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", collaborationEquipment.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public List<CollaborationEquipment> GetCollaborationEquipmentsWithPrice(string type, int pageSize, int offset)
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
                        quality = reader.GetInt32("quality"),
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
                        description = reader.GetString("description")
                    };
                    collaborationEquipment.currency = new Currency
                    {
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
                        quality = reader.GetInt32("quality"),
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
    public CollaborationEquipment GetUserCollaborationEquipmentsById(int Id)
    {
        CollaborationEquipment card = new CollaborationEquipment();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_collaboration_equipments where user_collaboration_equipments.collaboration_equipment_id=@id 
                and user_collaboration_equipments.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new CollaborationEquipment
                    {
                        id = reader.GetInt32("collaboration_equipment_id"),
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
                FROM collaboration_equipments_gallery 
                WHERE user_id = @user_id AND collaboration_equipment_id = @collaboration_equipment_id;
                ";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@collaboration_equipment_id", Id);

                int recordCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (recordCount == 0)
                {
                    string query = @"
                    INSERT INTO collaboration_equipments_gallery (
                        user_id, collaboration_equipment_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
                        magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                        mental_attack, mental_defense, speed, critical_damage_rate, critical_rate, penetration_rate, evasion_rate, 
                        damage_absorption_rate, vitality_regeneration_rate, accuracy_rate, lifesteal_rate, shield_strength, tenacity, 
                        resistance_rate, combo_rate, reflection_rate, mana, mana_regeneration_rate, 
                        damage_to_different_faction_rate, resistance_to_different_faction_rate, 
                        damage_to_same_faction_rate, resistance_to_same_faction_rate, 
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                        percent_all_mental_attack, percent_all_mental_defense
                    ) VALUES (
                        @user_id, @collaboration_equipment_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
                        @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, 
                        @mental_attack, @mental_defense, @speed, @critical_damage_rate, @critical_rate, @penetration_rate, @evasion_rate, 
                        @damage_absorption_rate, @vitality_regeneration_rate, @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, 
                        @resistance_rate, @combo_rate, @reflection_rate, @mana, @mana_regeneration_rate, 
                        @damage_to_different_faction_rate, @resistance_to_different_faction_rate, 
                        @damage_to_same_faction_rate, @resistance_to_same_faction_rate, 
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, 
                        @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, 
                        @percent_all_mental_attack, @percent_all_mental_defense
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
                    command.Parameters.AddWithValue("@critical_damage_rate", collaborationEquipmentFromDB.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", collaborationEquipmentFromDB.critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", collaborationEquipmentFromDB.penetration_rate);
                    command.Parameters.AddWithValue("@evasion_rate", collaborationEquipmentFromDB.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", collaborationEquipmentFromDB.damage_absorption_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", collaborationEquipmentFromDB.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", collaborationEquipmentFromDB.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", collaborationEquipmentFromDB.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", collaborationEquipmentFromDB.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", collaborationEquipmentFromDB.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", collaborationEquipmentFromDB.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", collaborationEquipmentFromDB.combo_rate);
                    command.Parameters.AddWithValue("@reflection_rate", collaborationEquipmentFromDB.reflection_rate);
                    command.Parameters.AddWithValue("@mana", collaborationEquipmentFromDB.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", collaborationEquipmentFromDB.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", collaborationEquipmentFromDB.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", collaborationEquipmentFromDB.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", collaborationEquipmentFromDB.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", collaborationEquipmentFromDB.resistance_to_same_faction_rate);
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
    public CollaborationEquipment SumPowerCollaborationEquipmentsGallery()
    {
        CollaborationEquipment sumCollaborationEquipments = new CollaborationEquipment();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                SUM(power) AS total_power, SUM(health) AS total_health, SUM(mana) AS total_mana, 
                SUM(physical_attack) AS total_physical_attack, SUM(physical_defense) AS total_physical_defense, 
                SUM(magical_attack) AS total_magical_attack, SUM(magical_defense) AS total_magical_defense, 
                SUM(chemical_attack) AS total_chemical_attack, SUM(chemical_defense) AS total_chemical_defense, 
                SUM(atomic_attack) AS total_atomic_attack, SUM(atomic_defense) AS total_atomic_defense, 
                SUM(mental_attack) AS total_mental_attack, SUM(mental_defense) AS total_mental_defense, 
                SUM(speed) AS total_speed, SUM(critical_damage_rate) AS total_critical_damage_rate, 
                SUM(critical_rate) AS total_critical_rate, SUM(penetration_rate) AS total_penetration_rate, 
                SUM(evasion_rate) AS total_evasion_rate, SUM(damage_absorption_rate) AS total_damage_absorption_rate, 
                SUM(vitality_regeneration_rate) AS total_vitality_regeneration_rate, SUM(accuracy_rate) AS total_accuracy_rate, 
                SUM(lifesteal_rate) AS total_lifesteal_rate, SUM(shield_strength) AS total_shield_strength, 
                SUM(tenacity) AS total_tenacity, SUM(resistance_rate) AS total_resistance_rate, 
                SUM(combo_rate) AS total_combo_rate, SUM(reflection_rate) AS total_reflection_rate, 
                SUM(mana_regeneration_rate) AS total_mana_regeneration_rate, 
                SUM(damage_to_different_faction_rate) AS total_damage_to_different_faction_rate, 
                SUM(resistance_to_different_faction_rate) AS total_resistance_to_different_faction_rate, 
                SUM(damage_to_same_faction_rate) AS total_damage_to_same_faction_rate, 
                SUM(resistance_to_same_faction_rate) AS total_resistance_to_same_faction_rate, 
                SUM(percent_all_health) AS total_percent_all_health, 
                SUM(percent_all_physical_attack) AS total_percent_all_physical_attack, 
                SUM(percent_all_physical_defense) AS total_percent_all_physical_defense, 
                SUM(percent_all_magical_attack) AS total_percent_all_magical_attack, 
                SUM(percent_all_magical_defense) AS total_percent_all_magical_defense, 
                SUM(percent_all_chemical_attack) AS total_percent_all_chemical_attack, 
                SUM(percent_all_chemical_defense) AS total_percent_all_chemical_defense, 
                SUM(percent_all_atomic_attack) AS total_percent_all_atomic_attack, 
                SUM(percent_all_atomic_defense) AS total_percent_all_atomic_defense, 
                SUM(percent_all_mental_attack) AS total_percent_all_mental_attack, 
                SUM(percent_all_mental_defense) AS total_percent_all_mental_defense 
            FROM collaboration_equipments_gallery 
            WHERE user_id = @user_id AND status = 'available';";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumCollaborationEquipments.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumCollaborationEquipments.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumCollaborationEquipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumCollaborationEquipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumCollaborationEquipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumCollaborationEquipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumCollaborationEquipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumCollaborationEquipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumCollaborationEquipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumCollaborationEquipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumCollaborationEquipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumCollaborationEquipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumCollaborationEquipments.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumCollaborationEquipments.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                        sumCollaborationEquipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumCollaborationEquipments.penetration_rate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                        sumCollaborationEquipments.evasion_rate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                        sumCollaborationEquipments.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                        sumCollaborationEquipments.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                        sumCollaborationEquipments.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                        sumCollaborationEquipments.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                        sumCollaborationEquipments.shield_strength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                        sumCollaborationEquipments.tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                        sumCollaborationEquipments.resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                        sumCollaborationEquipments.combo_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                        sumCollaborationEquipments.reflection_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                        sumCollaborationEquipments.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                        sumCollaborationEquipments.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                        sumCollaborationEquipments.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                        sumCollaborationEquipments.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                        sumCollaborationEquipments.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                        sumCollaborationEquipments.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                        sumCollaborationEquipments.percent_all_health = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDouble("total_percent_all_health");
                        sumCollaborationEquipments.percent_all_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDouble("total_percent_all_physical_attack");
                        sumCollaborationEquipments.percent_all_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDouble("total_percent_all_physical_defense");
                        sumCollaborationEquipments.percent_all_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDouble("total_percent_all_magical_attack");
                        sumCollaborationEquipments.percent_all_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDouble("total_percent_all_magical_defense");
                        sumCollaborationEquipments.percent_all_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDouble("total_percent_all_chemical_attack");
                        sumCollaborationEquipments.percent_all_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDouble("total_percent_all_chemical_defense");
                        sumCollaborationEquipments.percent_all_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDouble("total_percent_all_atomic_attack");
                        sumCollaborationEquipments.percent_all_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDouble("total_percent_all_atomic_defense");
                        sumCollaborationEquipments.percent_all_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDouble("total_percent_all_mental_attack");
                        sumCollaborationEquipments.percent_all_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDouble("total_percent_all_mental_defense");
                    }
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return sumCollaborationEquipments;
    }
    public CollaborationEquipment SumPowerUserCollaborationEquipments()
    {
        CollaborationEquipment sumCollaborationEquipments = new CollaborationEquipment();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                SUM(power) AS total_power, SUM(health) AS total_health, SUM(mana) AS total_mana, 
                SUM(physical_attack) AS total_physical_attack, SUM(physical_defense) AS total_physical_defense, 
                SUM(magical_attack) AS total_magical_attack, SUM(magical_defense) AS total_magical_defense, 
                SUM(chemical_attack) AS total_chemical_attack, SUM(chemical_defense) AS total_chemical_defense, 
                SUM(atomic_attack) AS total_atomic_attack, SUM(atomic_defense) AS total_atomic_defense, 
                SUM(mental_attack) AS total_mental_attack, SUM(mental_defense) AS total_mental_defense, 
                SUM(speed) AS total_speed, SUM(critical_damage_rate) AS total_critical_damage_rate, 
                SUM(critical_rate) AS total_critical_rate, SUM(penetration_rate) AS total_penetration_rate, 
                SUM(evasion_rate) AS total_evasion_rate, SUM(damage_absorption_rate) AS total_damage_absorption_rate, 
                SUM(vitality_regeneration_rate) AS total_vitality_regeneration_rate, SUM(accuracy_rate) AS total_accuracy_rate, 
                SUM(lifesteal_rate) AS total_lifesteal_rate, SUM(shield_strength) AS total_shield_strength, 
                SUM(tenacity) AS total_tenacity, SUM(resistance_rate) AS total_resistance_rate, 
                SUM(combo_rate) AS total_combo_rate, SUM(reflection_rate) AS total_reflection_rate, 
                SUM(mana_regeneration_rate) AS total_mana_regeneration_rate, 
                SUM(damage_to_different_faction_rate) AS total_damage_to_different_faction_rate, 
                SUM(resistance_to_different_faction_rate) AS total_resistance_to_different_faction_rate, 
                SUM(damage_to_same_faction_rate) AS total_damage_to_same_faction_rate, 
                SUM(resistance_to_same_faction_rate) AS total_resistance_to_same_faction_rate
            FROM user_collaboration_equipments 
            WHERE user_id = @user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumCollaborationEquipments.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumCollaborationEquipments.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumCollaborationEquipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumCollaborationEquipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumCollaborationEquipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumCollaborationEquipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumCollaborationEquipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumCollaborationEquipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumCollaborationEquipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumCollaborationEquipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumCollaborationEquipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumCollaborationEquipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumCollaborationEquipments.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumCollaborationEquipments.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                        sumCollaborationEquipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumCollaborationEquipments.penetration_rate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                        sumCollaborationEquipments.evasion_rate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                        sumCollaborationEquipments.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                        sumCollaborationEquipments.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                        sumCollaborationEquipments.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                        sumCollaborationEquipments.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                        sumCollaborationEquipments.shield_strength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                        sumCollaborationEquipments.tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                        sumCollaborationEquipments.resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                        sumCollaborationEquipments.combo_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                        sumCollaborationEquipments.reflection_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                        sumCollaborationEquipments.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                        sumCollaborationEquipments.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                        sumCollaborationEquipments.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                        sumCollaborationEquipments.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                        sumCollaborationEquipments.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                        sumCollaborationEquipments.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                    }
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return sumCollaborationEquipments;
    }
}
