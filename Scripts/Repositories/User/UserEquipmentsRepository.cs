using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserEquipmentsRepository : IUserEquipmentsRepository
{
    public List<Equipments> GetUserEquipments(string user_id, string type, int pageSize, int offset)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        // string user_id = User.CurrentUserId;
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
                        id = reader.GetString("equipment_id"),
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
    public int GetUserEquipmentsCount(string user_id, string type)
    {
        int count = 0;
        // string user_id = User.CurrentUserId;
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
    public Equipments GetUserEquipmentsById(string user_id, string Id)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
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
    public bool BuyEquipment(string Id, Equipments EquipmentFromDB)
    {
        // Equipments EquipmentFromDB = GetEquipmentById(Id);
        // Debug.Log(EquipmentFromDB);
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                INSERT INTO user_equipments (
                    user_id, equipment_id, sequence, level, experiment, star, quality, block, power,
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
                    special_health, special_physical_attack, special_physical_defense, special_magical_attack,
                    special_magical_defense, special_chemical_attack, special_chemical_defense, special_atomic_attack,
                    special_atomic_defense, special_mental_attack, special_mental_defense, special_speed
                ) VALUES (
                    @user_id, @equipment_id, @sequence, @level, @experiment, @star, @quality, @block, @power, 
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
                command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(EquipmentFromDB.rare));
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
                command.Parameters.AddWithValue("@mental_attack", EquipmentFromDB.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", EquipmentFromDB.mental_defense);
                command.Parameters.AddWithValue("@speed", EquipmentFromDB.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", EquipmentFromDB.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", EquipmentFromDB.critical_rate);
                command.Parameters.AddWithValue("@critical_resistance_rate", EquipmentFromDB.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", EquipmentFromDB.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", EquipmentFromDB.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", EquipmentFromDB.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", EquipmentFromDB.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", EquipmentFromDB.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", EquipmentFromDB.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", EquipmentFromDB.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", EquipmentFromDB.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", EquipmentFromDB.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", EquipmentFromDB.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", EquipmentFromDB.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", EquipmentFromDB.shield_strength);
                command.Parameters.AddWithValue("@tenacity", EquipmentFromDB.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", EquipmentFromDB.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", EquipmentFromDB.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", EquipmentFromDB.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", EquipmentFromDB.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", EquipmentFromDB.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", EquipmentFromDB.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", EquipmentFromDB.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", EquipmentFromDB.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", EquipmentFromDB.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", EquipmentFromDB.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", EquipmentFromDB.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", EquipmentFromDB.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", EquipmentFromDB.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", EquipmentFromDB.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", EquipmentFromDB.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", EquipmentFromDB.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", EquipmentFromDB.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", EquipmentFromDB.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", EquipmentFromDB.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", EquipmentFromDB.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", EquipmentFromDB.skill_resistance_rate);
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
                WHERE user_id = @user_id AND equipment_id = @equipment_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@equipment_id", equipments.id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", equipments.power);
                command.Parameters.AddWithValue("@health", equipments.health);
                command.Parameters.AddWithValue("@physical_attack", equipments.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", equipments.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", equipments.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", equipments.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", equipments.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", equipments.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", equipments.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", equipments.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", equipments.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", equipments.mental_defense);
                command.Parameters.AddWithValue("@speed", equipments.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", equipments.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", equipments.critical_rate);
                command.Parameters.AddWithValue("@critical_resistance_rate", equipments.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", equipments.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", equipments.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", equipments.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", equipments.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", equipments.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", equipments.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", equipments.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", equipments.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", equipments.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", equipments.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", equipments.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", equipments.shield_strength);
                command.Parameters.AddWithValue("@tenacity", equipments.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", equipments.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", equipments.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", equipments.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", equipments.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", equipments.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", equipments.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", equipments.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", equipments.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", equipments.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", equipments.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", equipments.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", equipments.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", equipments.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", equipments.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", equipments.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", equipments.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", equipments.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", equipments.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", equipments.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", equipments.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", equipments.skill_resistance_rate);
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
                WHERE user_id = @user_id AND equipment_id = @equipment_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@equipment_id", equipments.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", equipments.power);
                command.Parameters.AddWithValue("@health", equipments.health);
                command.Parameters.AddWithValue("@physical_attack", equipments.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", equipments.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", equipments.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", equipments.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", equipments.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", equipments.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", equipments.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", equipments.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", equipments.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", equipments.mental_defense);
                command.Parameters.AddWithValue("@speed", equipments.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", equipments.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", equipments.critical_rate);
                command.Parameters.AddWithValue("@critical_resistance_rate", equipments.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", equipments.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", equipments.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", equipments.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", equipments.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", equipments.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", equipments.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", equipments.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", equipments.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", equipments.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", equipments.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", equipments.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", equipments.shield_strength);
                command.Parameters.AddWithValue("@tenacity", equipments.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", equipments.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", equipments.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", equipments.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", equipments.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", equipments.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", equipments.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", equipments.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", equipments.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", equipments.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", equipments.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", equipments.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", equipments.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", equipments.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", equipments.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", equipments.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", equipments.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", equipments.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", equipments.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", equipments.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", equipments.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", equipments.skill_resistance_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    private int GetMaxSequence(MySqlConnection connection, string equipment_id)
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
    public void UpdateUserCurrency(string Id)
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
    public void InsertCardHeroesEquipments(string Id, Equipments equipments, int position)
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
    public void InsertCardCaptainsEquipments(string Id, Equipments equipments, int position)
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
    public void InsertCardColonelsEquipments(string Id, Equipments equipments, int position)
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
    public void InsertCardGeneralsEquipments(string Id, Equipments equipments, int position)
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
    public void InsertCardAdmiralsEquipments(string Id, Equipments equipments, int position)
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
    public void InsertCardMonstersEquipments(string Id, Equipments equipments, int position)
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
    public void InsertCardMilitaryEquipments(string Id, Equipments equipments, int position)
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
    public void InsertCardSpellEquipments(string Id, Equipments equipments, int position)
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
    public void InsertBooksEquipments(string Id, Equipments equipments, int position)
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
    public void InsertPetsEquipments(string Id, Equipments equipments, int position)
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
    public List<Equipments> GetCardHeroesEquipments(string user_id, string card_id, string type)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_hero_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetCardCaptainsEquipments(string user_id, string card_id, string type)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_captain_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetCardColonelsEquipments(string user_id, string card_id, string type)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_colonel_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetCardGeneralsEquipments(string user_id, string card_id, string type)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_general_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetCardAdmiralsEquipments(string user_id, string card_id, string type)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_admiral_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetCardMonstersEquipments(string user_id, string card_id, string type)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_monster_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetCardMilitaryEquipments(string user_id, string card_id, string type)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_military_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetCardSpellEquipments(string user_id, string card_id, string type)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_spell_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetBooksEquipments(string user_id, string card_id, string type)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@book_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetPetsEquipments(string user_id, string card_id, string type)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@pet_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetAllCardHeroesEquipments(string user_id, string type, int limit, int offset, string status)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetAllCardCaptainsEquipments(string user_id, string type, int limit, int offset, string status)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetAllCardColonelsEquipments(string user_id, string type, int limit, int offset, string status)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetAllCardGeneralsEquipments(string user_id, string type, int limit, int offset, string status)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetAllCardAdmiralsEquipments(string user_id, string type, int limit, int offset, string status)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetAllCardMonstersEquipments(string user_id, string type, int limit, int offset, string status)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetAllCardMilitaryEquipments(string user_id, string type, int limit, int offset, string status)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetAllCardSpellEquipments(string user_id, string type, int limit, int offset, string status)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetAllBooksEquipments(string user_id, string type, int limit, int offset, string status)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public List<Equipments> GetAllPetsEquipments(string user_id, string type, int limit, int offset, string status)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        id = reader.GetString("equipment_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        set = reader.GetString("equipmentSet"),
                        level = reader.GetInt32("level"),
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
    public Equipments ChangeValueToZero(Equipments equipments)
    {
        equipments.power = 0;
        equipments.health = 0;
        equipments.physical_attack = 0;
        equipments.physical_defense = 0;
        equipments.magical_attack = 0;
        equipments.magical_defense = 0;
        equipments.chemical_attack = 0;
        equipments.chemical_defense = 0;
        equipments.atomic_attack = 0;
        equipments.atomic_defense = 0;
        equipments.mental_attack = 0;
        equipments.mental_defense = 0;
        equipments.speed = 0;
        equipments.critical_damage_rate = 0;
        equipments.critical_rate = 0;
        equipments.penetration_rate = 0;
        equipments.evasion_rate = 0;
        equipments.damage_absorption_rate = 0;
        equipments.vitality_regeneration_rate = 0;
        equipments.accuracy_rate = 0;
        equipments.lifesteal_rate = 0;
        equipments.shield_strength = 0;
        equipments.tenacity = 0;
        equipments.resistance_rate = 0;
        equipments.combo_rate = 0;
        equipments.reflection_rate = 0;
        equipments.mana = 0;
        equipments.mana_regeneration_rate = 0;
        equipments.damage_to_different_faction_rate = 0;
        equipments.resistance_to_different_faction_rate = 0;
        equipments.damage_to_same_faction_rate = 0;
        equipments.resistance_to_same_faction_rate = 0;
        equipments.special_health = 0;
        equipments.special_physical_attack = 0;
        equipments.special_physical_defense = 0;
        equipments.special_magical_attack = 0;
        equipments.special_magical_defense = 0;
        equipments.special_chemical_attack = 0;
        equipments.special_chemical_defense = 0;
        equipments.special_atomic_attack = 0;
        equipments.special_atomic_defense = 0;
        equipments.special_mental_attack = 0;
        equipments.special_mental_defense = 0;
        equipments.special_speed = 0;
        return equipments;
    }
    public Equipments GetAllEquipmentsByCardHeoresId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT ue.*
                FROM user_card_heroes uc, card_heroes c, card_heroes_equipment che, user_equipments ue
                WHERE uc.card_hero_id = c.id AND uc.card_hero_id = che.card_hero_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_hero_id = @card_hero_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_hero_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.critical_resistance_rate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.ignore_critical_rate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.penetration_rate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.penetration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.evasion_rate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.ignore_damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.absorbed_damage_rate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.vitality_regeneration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.shield_strength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.resistance_rate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.combo_rate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.ignore_combo_rate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.combo_damage_rate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.combo_resistance_rate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.stun_rate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.ignore_stun_rate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.reflection_rate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.ignore_reflection_rate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.reflection_damage_rate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.reflection_resistance_rate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.normal_damage_rate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.normal_resistance_rate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.skill_damage_rate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.skill_resistance_rate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.special_health = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.special_speed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);
                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.power += e.power;
                    equipments.health += e.health;
                    equipments.physical_attack += e.physical_attack;
                    equipments.physical_defense += e.physical_defense;
                    equipments.magical_attack += e.magical_attack;
                    equipments.magical_defense += e.magical_defense;
                    equipments.chemical_attack += e.chemical_attack;
                    equipments.chemical_defense += e.chemical_defense;
                    equipments.atomic_attack += e.atomic_attack;
                    equipments.atomic_defense += e.atomic_defense;
                    equipments.mental_attack += e.mental_attack;
                    equipments.mental_defense += e.mental_defense;
                    equipments.speed += e.speed;
                    equipments.critical_damage_rate += e.critical_damage_rate;
                    equipments.critical_rate += e.critical_rate;
                    equipments.critical_resistance_rate += e.critical_resistance_rate;
                    equipments.ignore_critical_rate += e.ignore_critical_rate;
                    equipments.penetration_rate += e.penetration_rate;
                    equipments.penetration_resistance_rate += e.penetration_resistance_rate;
                    equipments.evasion_rate += e.evasion_rate;
                    equipments.damage_absorption_rate += e.damage_absorption_rate;
                    equipments.ignore_damage_absorption_rate += e.ignore_damage_absorption_rate;
                    equipments.absorbed_damage_rate += e.absorbed_damage_rate;
                    equipments.vitality_regeneration_rate += e.vitality_regeneration_rate;
                    equipments.vitality_regeneration_resistance_rate += e.vitality_regeneration_resistance_rate;
                    equipments.accuracy_rate += e.accuracy_rate;
                    equipments.lifesteal_rate += e.lifesteal_rate;
                    equipments.shield_strength += e.shield_strength;
                    equipments.tenacity += e.tenacity;
                    equipments.resistance_rate += e.resistance_rate;
                    equipments.combo_rate += e.combo_rate;
                    equipments.ignore_combo_rate += e.ignore_combo_rate;
                    equipments.combo_damage_rate += e.combo_damage_rate;
                    equipments.combo_resistance_rate += e.combo_resistance_rate;
                    equipments.stun_rate += e.stun_rate;
                    equipments.ignore_stun_rate += e.ignore_stun_rate;
                    equipments.reflection_rate += e.reflection_rate;
                    equipments.ignore_reflection_rate += e.ignore_reflection_rate;
                    equipments.reflection_damage_rate += e.reflection_damage_rate;
                    equipments.reflection_resistance_rate += e.reflection_resistance_rate;
                    equipments.mana += e.mana;
                    equipments.mana_regeneration_rate += e.mana_regeneration_rate;
                    equipments.damage_to_different_faction_rate += e.damage_to_different_faction_rate;
                    equipments.resistance_to_different_faction_rate += e.resistance_to_different_faction_rate;
                    equipments.damage_to_same_faction_rate += e.damage_to_same_faction_rate;
                    equipments.resistance_to_same_faction_rate += e.resistance_to_same_faction_rate;
                    equipments.normal_damage_rate += e.normal_damage_rate;
                    equipments.normal_resistance_rate += e.normal_resistance_rate;
                    equipments.skill_damage_rate += e.skill_damage_rate;
                    equipments.skill_resistance_rate += e.skill_resistance_rate;
                    equipments.special_health += e.special_health;
                    equipments.special_physical_attack += e.special_physical_attack;
                    equipments.special_physical_defense += e.special_physical_defense;
                    equipments.special_magical_attack += e.special_magical_attack;
                    equipments.special_magical_defense += e.special_magical_defense;
                    equipments.special_chemical_attack += e.special_chemical_attack;
                    equipments.special_chemical_defense += e.special_chemical_defense;
                    equipments.special_atomic_attack += e.special_atomic_attack;
                    equipments.special_atomic_defense += e.special_atomic_defense;
                    equipments.special_mental_attack += e.special_mental_attack;
                    equipments.special_mental_defense += e.special_mental_defense;
                    equipments.special_speed += e.speed;
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
    public Equipments GetAllEquipmentsByCardCaptainsId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT *
                FROM user_card_captains uc, card_captains c, card_captains_equipment che, user_equipments ue
                WHERE uc.card_captain_id = c.id AND uc.card_captain_id = che.card_captain_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_captain_id = @card_captain_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_captain_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.critical_resistance_rate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.ignore_critical_rate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.penetration_rate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.penetration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.evasion_rate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.ignore_damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.absorbed_damage_rate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.vitality_regeneration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.shield_strength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.resistance_rate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.combo_rate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.ignore_combo_rate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.combo_damage_rate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.combo_resistance_rate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.stun_rate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.ignore_stun_rate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.reflection_rate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.ignore_reflection_rate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.reflection_damage_rate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.reflection_resistance_rate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.normal_damage_rate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.normal_resistance_rate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.skill_damage_rate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.skill_resistance_rate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.special_health = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.special_speed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);

                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.power += e.power;
                    equipments.health += e.health;
                    equipments.physical_attack += e.physical_attack;
                    equipments.physical_defense += e.physical_defense;
                    equipments.magical_attack += e.magical_attack;
                    equipments.magical_defense += e.magical_defense;
                    equipments.chemical_attack += e.chemical_attack;
                    equipments.chemical_defense += e.chemical_defense;
                    equipments.atomic_attack += e.atomic_attack;
                    equipments.atomic_defense += e.atomic_defense;
                    equipments.mental_attack += e.mental_attack;
                    equipments.mental_defense += e.mental_defense;
                    equipments.speed += e.speed;
                    equipments.critical_damage_rate += e.critical_damage_rate;
                    equipments.critical_rate += e.critical_rate;
                    equipments.critical_resistance_rate += e.critical_resistance_rate;
                    equipments.ignore_critical_rate += e.ignore_critical_rate;
                    equipments.penetration_rate += e.penetration_rate;
                    equipments.penetration_resistance_rate += e.penetration_resistance_rate;
                    equipments.evasion_rate += e.evasion_rate;
                    equipments.damage_absorption_rate += e.damage_absorption_rate;
                    equipments.ignore_damage_absorption_rate += e.ignore_damage_absorption_rate;
                    equipments.absorbed_damage_rate += e.absorbed_damage_rate;
                    equipments.vitality_regeneration_rate += e.vitality_regeneration_rate;
                    equipments.vitality_regeneration_resistance_rate += e.vitality_regeneration_resistance_rate;
                    equipments.accuracy_rate += e.accuracy_rate;
                    equipments.lifesteal_rate += e.lifesteal_rate;
                    equipments.shield_strength += e.shield_strength;
                    equipments.tenacity += e.tenacity;
                    equipments.resistance_rate += e.resistance_rate;
                    equipments.combo_rate += e.combo_rate;
                    equipments.ignore_combo_rate += e.ignore_combo_rate;
                    equipments.combo_damage_rate += e.combo_damage_rate;
                    equipments.combo_resistance_rate += e.combo_resistance_rate;
                    equipments.stun_rate += e.stun_rate;
                    equipments.ignore_stun_rate += e.ignore_stun_rate;
                    equipments.reflection_rate += e.reflection_rate;
                    equipments.ignore_reflection_rate += e.ignore_reflection_rate;
                    equipments.reflection_damage_rate += e.reflection_damage_rate;
                    equipments.reflection_resistance_rate += e.reflection_resistance_rate;
                    equipments.mana += e.mana;
                    equipments.mana_regeneration_rate += e.mana_regeneration_rate;
                    equipments.damage_to_different_faction_rate += e.damage_to_different_faction_rate;
                    equipments.resistance_to_different_faction_rate += e.resistance_to_different_faction_rate;
                    equipments.damage_to_same_faction_rate += e.damage_to_same_faction_rate;
                    equipments.resistance_to_same_faction_rate += e.resistance_to_same_faction_rate;
                    equipments.normal_damage_rate += e.normal_damage_rate;
                    equipments.normal_resistance_rate += e.normal_resistance_rate;
                    equipments.skill_damage_rate += e.skill_damage_rate;
                    equipments.skill_resistance_rate += e.skill_resistance_rate;
                    equipments.special_health += e.special_health;
                    equipments.special_physical_attack += e.special_physical_attack;
                    equipments.special_physical_defense += e.special_physical_defense;
                    equipments.special_magical_attack += e.special_magical_attack;
                    equipments.special_magical_defense += e.special_magical_defense;
                    equipments.special_chemical_attack += e.special_chemical_attack;
                    equipments.special_chemical_defense += e.special_chemical_defense;
                    equipments.special_atomic_attack += e.special_atomic_attack;
                    equipments.special_atomic_defense += e.special_atomic_defense;
                    equipments.special_mental_attack += e.special_mental_attack;
                    equipments.special_mental_defense += e.special_mental_defense;
                    equipments.special_speed += e.speed;
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
    public Equipments GetAllEquipmentsByCardColonelsId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT *
                FROM user_card_colonels uc, card_colonels c, card_colonels_equipment che, user_equipments ue
                WHERE uc.card_colonel_id = c.id AND uc.card_colonel_id = che.card_colonel_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_colonel_id = @card_colonel_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_colonel_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.critical_resistance_rate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.ignore_critical_rate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.penetration_rate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.penetration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.evasion_rate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.ignore_damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.absorbed_damage_rate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.vitality_regeneration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.shield_strength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.resistance_rate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.combo_rate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.ignore_combo_rate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.combo_damage_rate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.combo_resistance_rate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.stun_rate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.ignore_stun_rate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.reflection_rate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.ignore_reflection_rate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.reflection_damage_rate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.reflection_resistance_rate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.normal_damage_rate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.normal_resistance_rate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.skill_damage_rate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.skill_resistance_rate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.special_health = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.special_speed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);

                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.power += e.power;
                    equipments.health += e.health;
                    equipments.physical_attack += e.physical_attack;
                    equipments.physical_defense += e.physical_defense;
                    equipments.magical_attack += e.magical_attack;
                    equipments.magical_defense += e.magical_defense;
                    equipments.chemical_attack += e.chemical_attack;
                    equipments.chemical_defense += e.chemical_defense;
                    equipments.atomic_attack += e.atomic_attack;
                    equipments.atomic_defense += e.atomic_defense;
                    equipments.mental_attack += e.mental_attack;
                    equipments.mental_defense += e.mental_defense;
                    equipments.speed += e.speed;
                    equipments.critical_damage_rate += e.critical_damage_rate;
                    equipments.critical_rate += e.critical_rate;
                    equipments.critical_resistance_rate += e.critical_resistance_rate;
                    equipments.ignore_critical_rate += e.ignore_critical_rate;
                    equipments.penetration_rate += e.penetration_rate;
                    equipments.penetration_resistance_rate += e.penetration_resistance_rate;
                    equipments.evasion_rate += e.evasion_rate;
                    equipments.damage_absorption_rate += e.damage_absorption_rate;
                    equipments.ignore_damage_absorption_rate += e.ignore_damage_absorption_rate;
                    equipments.absorbed_damage_rate += e.absorbed_damage_rate;
                    equipments.vitality_regeneration_rate += e.vitality_regeneration_rate;
                    equipments.vitality_regeneration_resistance_rate += e.vitality_regeneration_resistance_rate;
                    equipments.accuracy_rate += e.accuracy_rate;
                    equipments.lifesteal_rate += e.lifesteal_rate;
                    equipments.shield_strength += e.shield_strength;
                    equipments.tenacity += e.tenacity;
                    equipments.resistance_rate += e.resistance_rate;
                    equipments.combo_rate += e.combo_rate;
                    equipments.ignore_combo_rate += e.ignore_combo_rate;
                    equipments.combo_damage_rate += e.combo_damage_rate;
                    equipments.combo_resistance_rate += e.combo_resistance_rate;
                    equipments.stun_rate += e.stun_rate;
                    equipments.ignore_stun_rate += e.ignore_stun_rate;
                    equipments.reflection_rate += e.reflection_rate;
                    equipments.ignore_reflection_rate += e.ignore_reflection_rate;
                    equipments.reflection_damage_rate += e.reflection_damage_rate;
                    equipments.reflection_resistance_rate += e.reflection_resistance_rate;
                    equipments.mana += e.mana;
                    equipments.mana_regeneration_rate += e.mana_regeneration_rate;
                    equipments.damage_to_different_faction_rate += e.damage_to_different_faction_rate;
                    equipments.resistance_to_different_faction_rate += e.resistance_to_different_faction_rate;
                    equipments.damage_to_same_faction_rate += e.damage_to_same_faction_rate;
                    equipments.resistance_to_same_faction_rate += e.resistance_to_same_faction_rate;
                    equipments.normal_damage_rate += e.normal_damage_rate;
                    equipments.normal_resistance_rate += e.normal_resistance_rate;
                    equipments.skill_damage_rate += e.skill_damage_rate;
                    equipments.skill_resistance_rate += e.skill_resistance_rate;
                    equipments.special_health += e.special_health;
                    equipments.special_physical_attack += e.special_physical_attack;
                    equipments.special_physical_defense += e.special_physical_defense;
                    equipments.special_magical_attack += e.special_magical_attack;
                    equipments.special_magical_defense += e.special_magical_defense;
                    equipments.special_chemical_attack += e.special_chemical_attack;
                    equipments.special_chemical_defense += e.special_chemical_defense;
                    equipments.special_atomic_attack += e.special_atomic_attack;
                    equipments.special_atomic_defense += e.special_atomic_defense;
                    equipments.special_mental_attack += e.special_mental_attack;
                    equipments.special_mental_defense += e.special_mental_defense;
                    equipments.special_speed += e.speed;
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
    public Equipments GetAllEquipmentsByCardGeneralsId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT *
                FROM user_card_generals uc, card_generals c, card_generals_equipment che, user_equipments ue
                WHERE uc.card_general_id = c.id AND uc.card_general_id = che.card_general_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_general_id = @card_general_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_general_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.critical_resistance_rate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.ignore_critical_rate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.penetration_rate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.penetration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.evasion_rate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.ignore_damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.absorbed_damage_rate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.vitality_regeneration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.shield_strength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.resistance_rate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.combo_rate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.ignore_combo_rate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.combo_damage_rate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.combo_resistance_rate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.stun_rate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.ignore_stun_rate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.reflection_rate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.ignore_reflection_rate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.reflection_damage_rate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.reflection_resistance_rate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.normal_damage_rate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.normal_resistance_rate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.skill_damage_rate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.skill_resistance_rate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.special_health = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.special_speed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);

                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.power += e.power;
                    equipments.health += e.health;
                    equipments.physical_attack += e.physical_attack;
                    equipments.physical_defense += e.physical_defense;
                    equipments.magical_attack += e.magical_attack;
                    equipments.magical_defense += e.magical_defense;
                    equipments.chemical_attack += e.chemical_attack;
                    equipments.chemical_defense += e.chemical_defense;
                    equipments.atomic_attack += e.atomic_attack;
                    equipments.atomic_defense += e.atomic_defense;
                    equipments.mental_attack += e.mental_attack;
                    equipments.mental_defense += e.mental_defense;
                    equipments.speed += e.speed;
                    equipments.critical_damage_rate += e.critical_damage_rate;
                    equipments.critical_rate += e.critical_rate;
                    equipments.critical_resistance_rate += e.critical_resistance_rate;
                    equipments.ignore_critical_rate += e.ignore_critical_rate;
                    equipments.penetration_rate += e.penetration_rate;
                    equipments.penetration_resistance_rate += e.penetration_resistance_rate;
                    equipments.evasion_rate += e.evasion_rate;
                    equipments.damage_absorption_rate += e.damage_absorption_rate;
                    equipments.ignore_damage_absorption_rate += e.ignore_damage_absorption_rate;
                    equipments.absorbed_damage_rate += e.absorbed_damage_rate;
                    equipments.vitality_regeneration_rate += e.vitality_regeneration_rate;
                    equipments.vitality_regeneration_resistance_rate += e.vitality_regeneration_resistance_rate;
                    equipments.accuracy_rate += e.accuracy_rate;
                    equipments.lifesteal_rate += e.lifesteal_rate;
                    equipments.shield_strength += e.shield_strength;
                    equipments.tenacity += e.tenacity;
                    equipments.resistance_rate += e.resistance_rate;
                    equipments.combo_rate += e.combo_rate;
                    equipments.ignore_combo_rate += e.ignore_combo_rate;
                    equipments.combo_damage_rate += e.combo_damage_rate;
                    equipments.combo_resistance_rate += e.combo_resistance_rate;
                    equipments.stun_rate += e.stun_rate;
                    equipments.ignore_stun_rate += e.ignore_stun_rate;
                    equipments.reflection_rate += e.reflection_rate;
                    equipments.ignore_reflection_rate += e.ignore_reflection_rate;
                    equipments.reflection_damage_rate += e.reflection_damage_rate;
                    equipments.reflection_resistance_rate += e.reflection_resistance_rate;
                    equipments.mana += e.mana;
                    equipments.mana_regeneration_rate += e.mana_regeneration_rate;
                    equipments.damage_to_different_faction_rate += e.damage_to_different_faction_rate;
                    equipments.resistance_to_different_faction_rate += e.resistance_to_different_faction_rate;
                    equipments.damage_to_same_faction_rate += e.damage_to_same_faction_rate;
                    equipments.resistance_to_same_faction_rate += e.resistance_to_same_faction_rate;
                    equipments.normal_damage_rate += e.normal_damage_rate;
                    equipments.normal_resistance_rate += e.normal_resistance_rate;
                    equipments.skill_damage_rate += e.skill_damage_rate;
                    equipments.skill_resistance_rate += e.skill_resistance_rate;
                    equipments.special_health += e.special_health;
                    equipments.special_physical_attack += e.special_physical_attack;
                    equipments.special_physical_defense += e.special_physical_defense;
                    equipments.special_magical_attack += e.special_magical_attack;
                    equipments.special_magical_defense += e.special_magical_defense;
                    equipments.special_chemical_attack += e.special_chemical_attack;
                    equipments.special_chemical_defense += e.special_chemical_defense;
                    equipments.special_atomic_attack += e.special_atomic_attack;
                    equipments.special_atomic_defense += e.special_atomic_defense;
                    equipments.special_mental_attack += e.special_mental_attack;
                    equipments.special_mental_defense += e.special_mental_defense;
                    equipments.special_speed += e.speed;
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
    public Equipments GetAllEquipmentsByCardAdmiralsId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT *
                FROM user_card_admirals uc, card_admirals c, card_admirals_equipment che, user_equipments ue
                WHERE uc.card_admiral_id = c.id AND uc.card_admiral_id = che.card_admiral_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_admiral_id = @card_admiral_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_admiral_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.critical_resistance_rate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.ignore_critical_rate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.penetration_rate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.penetration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.evasion_rate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.ignore_damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.absorbed_damage_rate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.vitality_regeneration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.shield_strength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.resistance_rate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.combo_rate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.ignore_combo_rate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.combo_damage_rate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.combo_resistance_rate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.stun_rate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.ignore_stun_rate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.reflection_rate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.ignore_reflection_rate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.reflection_damage_rate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.reflection_resistance_rate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.normal_damage_rate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.normal_resistance_rate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.skill_damage_rate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.skill_resistance_rate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.special_health = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.special_speed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);

                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.power += e.power;
                    equipments.health += e.health;
                    equipments.physical_attack += e.physical_attack;
                    equipments.physical_defense += e.physical_defense;
                    equipments.magical_attack += e.magical_attack;
                    equipments.magical_defense += e.magical_defense;
                    equipments.chemical_attack += e.chemical_attack;
                    equipments.chemical_defense += e.chemical_defense;
                    equipments.atomic_attack += e.atomic_attack;
                    equipments.atomic_defense += e.atomic_defense;
                    equipments.mental_attack += e.mental_attack;
                    equipments.mental_defense += e.mental_defense;
                    equipments.speed += e.speed;
                    equipments.critical_damage_rate += e.critical_damage_rate;
                    equipments.critical_rate += e.critical_rate;
                    equipments.critical_resistance_rate += e.critical_resistance_rate;
                    equipments.ignore_critical_rate += e.ignore_critical_rate;
                    equipments.penetration_rate += e.penetration_rate;
                    equipments.penetration_resistance_rate += e.penetration_resistance_rate;
                    equipments.evasion_rate += e.evasion_rate;
                    equipments.damage_absorption_rate += e.damage_absorption_rate;
                    equipments.ignore_damage_absorption_rate += e.ignore_damage_absorption_rate;
                    equipments.absorbed_damage_rate += e.absorbed_damage_rate;
                    equipments.vitality_regeneration_rate += e.vitality_regeneration_rate;
                    equipments.vitality_regeneration_resistance_rate += e.vitality_regeneration_resistance_rate;
                    equipments.accuracy_rate += e.accuracy_rate;
                    equipments.lifesteal_rate += e.lifesteal_rate;
                    equipments.shield_strength += e.shield_strength;
                    equipments.tenacity += e.tenacity;
                    equipments.resistance_rate += e.resistance_rate;
                    equipments.combo_rate += e.combo_rate;
                    equipments.ignore_combo_rate += e.ignore_combo_rate;
                    equipments.combo_damage_rate += e.combo_damage_rate;
                    equipments.combo_resistance_rate += e.combo_resistance_rate;
                    equipments.stun_rate += e.stun_rate;
                    equipments.ignore_stun_rate += e.ignore_stun_rate;
                    equipments.reflection_rate += e.reflection_rate;
                    equipments.ignore_reflection_rate += e.ignore_reflection_rate;
                    equipments.reflection_damage_rate += e.reflection_damage_rate;
                    equipments.reflection_resistance_rate += e.reflection_resistance_rate;
                    equipments.mana += e.mana;
                    equipments.mana_regeneration_rate += e.mana_regeneration_rate;
                    equipments.damage_to_different_faction_rate += e.damage_to_different_faction_rate;
                    equipments.resistance_to_different_faction_rate += e.resistance_to_different_faction_rate;
                    equipments.damage_to_same_faction_rate += e.damage_to_same_faction_rate;
                    equipments.resistance_to_same_faction_rate += e.resistance_to_same_faction_rate;
                    equipments.normal_damage_rate += e.normal_damage_rate;
                    equipments.normal_resistance_rate += e.normal_resistance_rate;
                    equipments.skill_damage_rate += e.skill_damage_rate;
                    equipments.skill_resistance_rate += e.skill_resistance_rate;
                    equipments.special_health += e.special_health;
                    equipments.special_physical_attack += e.special_physical_attack;
                    equipments.special_physical_defense += e.special_physical_defense;
                    equipments.special_magical_attack += e.special_magical_attack;
                    equipments.special_magical_defense += e.special_magical_defense;
                    equipments.special_chemical_attack += e.special_chemical_attack;
                    equipments.special_chemical_defense += e.special_chemical_defense;
                    equipments.special_atomic_attack += e.special_atomic_attack;
                    equipments.special_atomic_defense += e.special_atomic_defense;
                    equipments.special_mental_attack += e.special_mental_attack;
                    equipments.special_mental_defense += e.special_mental_defense;
                    equipments.special_speed += e.speed;
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
    public Equipments GetAllEquipmentsByCardMonstersId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT *
                FROM user_card_monsters uc, card_monsters c, card_monsters_equipment che, user_equipments ue
                WHERE uc.card_monster_id = c.id AND uc.card_monster_id = che.card_monster_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_monster_id = @card_monster_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_monster_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.critical_resistance_rate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.ignore_critical_rate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.penetration_rate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.penetration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.evasion_rate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.ignore_damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.absorbed_damage_rate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.vitality_regeneration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.shield_strength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.resistance_rate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.combo_rate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.ignore_combo_rate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.combo_damage_rate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.combo_resistance_rate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.stun_rate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.ignore_stun_rate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.reflection_rate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.ignore_reflection_rate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.reflection_damage_rate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.reflection_resistance_rate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.normal_damage_rate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.normal_resistance_rate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.skill_damage_rate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.skill_resistance_rate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.special_health = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.special_speed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);

                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.power += e.power;
                    equipments.health += e.health;
                    equipments.physical_attack += e.physical_attack;
                    equipments.physical_defense += e.physical_defense;
                    equipments.magical_attack += e.magical_attack;
                    equipments.magical_defense += e.magical_defense;
                    equipments.chemical_attack += e.chemical_attack;
                    equipments.chemical_defense += e.chemical_defense;
                    equipments.atomic_attack += e.atomic_attack;
                    equipments.atomic_defense += e.atomic_defense;
                    equipments.mental_attack += e.mental_attack;
                    equipments.mental_defense += e.mental_defense;
                    equipments.speed += e.speed;
                    equipments.critical_damage_rate += e.critical_damage_rate;
                    equipments.critical_rate += e.critical_rate;
                    equipments.critical_resistance_rate += e.critical_resistance_rate;
                    equipments.ignore_critical_rate += e.ignore_critical_rate;
                    equipments.penetration_rate += e.penetration_rate;
                    equipments.penetration_resistance_rate += e.penetration_resistance_rate;
                    equipments.evasion_rate += e.evasion_rate;
                    equipments.damage_absorption_rate += e.damage_absorption_rate;
                    equipments.ignore_damage_absorption_rate += e.ignore_damage_absorption_rate;
                    equipments.absorbed_damage_rate += e.absorbed_damage_rate;
                    equipments.vitality_regeneration_rate += e.vitality_regeneration_rate;
                    equipments.vitality_regeneration_resistance_rate += e.vitality_regeneration_resistance_rate;
                    equipments.accuracy_rate += e.accuracy_rate;
                    equipments.lifesteal_rate += e.lifesteal_rate;
                    equipments.shield_strength += e.shield_strength;
                    equipments.tenacity += e.tenacity;
                    equipments.resistance_rate += e.resistance_rate;
                    equipments.combo_rate += e.combo_rate;
                    equipments.ignore_combo_rate += e.ignore_combo_rate;
                    equipments.combo_damage_rate += e.combo_damage_rate;
                    equipments.combo_resistance_rate += e.combo_resistance_rate;
                    equipments.stun_rate += e.stun_rate;
                    equipments.ignore_stun_rate += e.ignore_stun_rate;
                    equipments.reflection_rate += e.reflection_rate;
                    equipments.ignore_reflection_rate += e.ignore_reflection_rate;
                    equipments.reflection_damage_rate += e.reflection_damage_rate;
                    equipments.reflection_resistance_rate += e.reflection_resistance_rate;
                    equipments.mana += e.mana;
                    equipments.mana_regeneration_rate += e.mana_regeneration_rate;
                    equipments.damage_to_different_faction_rate += e.damage_to_different_faction_rate;
                    equipments.resistance_to_different_faction_rate += e.resistance_to_different_faction_rate;
                    equipments.damage_to_same_faction_rate += e.damage_to_same_faction_rate;
                    equipments.resistance_to_same_faction_rate += e.resistance_to_same_faction_rate;
                    equipments.normal_damage_rate += e.normal_damage_rate;
                    equipments.normal_resistance_rate += e.normal_resistance_rate;
                    equipments.skill_damage_rate += e.skill_damage_rate;
                    equipments.skill_resistance_rate += e.skill_resistance_rate;
                    equipments.special_health += e.special_health;
                    equipments.special_physical_attack += e.special_physical_attack;
                    equipments.special_physical_defense += e.special_physical_defense;
                    equipments.special_magical_attack += e.special_magical_attack;
                    equipments.special_magical_defense += e.special_magical_defense;
                    equipments.special_chemical_attack += e.special_chemical_attack;
                    equipments.special_chemical_defense += e.special_chemical_defense;
                    equipments.special_atomic_attack += e.special_atomic_attack;
                    equipments.special_atomic_defense += e.special_atomic_defense;
                    equipments.special_mental_attack += e.special_mental_attack;
                    equipments.special_mental_defense += e.special_mental_defense;
                    equipments.special_speed += e.speed;
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
    public Equipments GetAllEquipmentsByCardMilitaryId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT *
                FROM user_card_military uc, card_military c, card_military_equipment che, user_equipments ue
                WHERE uc.card_military_id = c.id AND uc.card_military_id = che.card_military_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_military_id = @card_military_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_military_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.critical_resistance_rate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.ignore_critical_rate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.penetration_rate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.penetration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.evasion_rate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.ignore_damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.absorbed_damage_rate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.vitality_regeneration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.shield_strength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.resistance_rate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.combo_rate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.ignore_combo_rate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.combo_damage_rate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.combo_resistance_rate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.stun_rate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.ignore_stun_rate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.reflection_rate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.ignore_reflection_rate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.reflection_damage_rate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.reflection_resistance_rate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.normal_damage_rate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.normal_resistance_rate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.skill_damage_rate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.skill_resistance_rate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.special_health = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.special_speed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);

                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.power += e.power;
                    equipments.health += e.health;
                    equipments.physical_attack += e.physical_attack;
                    equipments.physical_defense += e.physical_defense;
                    equipments.magical_attack += e.magical_attack;
                    equipments.magical_defense += e.magical_defense;
                    equipments.chemical_attack += e.chemical_attack;
                    equipments.chemical_defense += e.chemical_defense;
                    equipments.atomic_attack += e.atomic_attack;
                    equipments.atomic_defense += e.atomic_defense;
                    equipments.mental_attack += e.mental_attack;
                    equipments.mental_defense += e.mental_defense;
                    equipments.speed += e.speed;
                    equipments.critical_damage_rate += e.critical_damage_rate;
                    equipments.critical_rate += e.critical_rate;
                    equipments.critical_resistance_rate += e.critical_resistance_rate;
                    equipments.ignore_critical_rate += e.ignore_critical_rate;
                    equipments.penetration_rate += e.penetration_rate;
                    equipments.penetration_resistance_rate += e.penetration_resistance_rate;
                    equipments.evasion_rate += e.evasion_rate;
                    equipments.damage_absorption_rate += e.damage_absorption_rate;
                    equipments.ignore_damage_absorption_rate += e.ignore_damage_absorption_rate;
                    equipments.absorbed_damage_rate += e.absorbed_damage_rate;
                    equipments.vitality_regeneration_rate += e.vitality_regeneration_rate;
                    equipments.vitality_regeneration_resistance_rate += e.vitality_regeneration_resistance_rate;
                    equipments.accuracy_rate += e.accuracy_rate;
                    equipments.lifesteal_rate += e.lifesteal_rate;
                    equipments.shield_strength += e.shield_strength;
                    equipments.tenacity += e.tenacity;
                    equipments.resistance_rate += e.resistance_rate;
                    equipments.combo_rate += e.combo_rate;
                    equipments.ignore_combo_rate += e.ignore_combo_rate;
                    equipments.combo_damage_rate += e.combo_damage_rate;
                    equipments.combo_resistance_rate += e.combo_resistance_rate;
                    equipments.stun_rate += e.stun_rate;
                    equipments.ignore_stun_rate += e.ignore_stun_rate;
                    equipments.reflection_rate += e.reflection_rate;
                    equipments.ignore_reflection_rate += e.ignore_reflection_rate;
                    equipments.reflection_damage_rate += e.reflection_damage_rate;
                    equipments.reflection_resistance_rate += e.reflection_resistance_rate;
                    equipments.mana += e.mana;
                    equipments.mana_regeneration_rate += e.mana_regeneration_rate;
                    equipments.damage_to_different_faction_rate += e.damage_to_different_faction_rate;
                    equipments.resistance_to_different_faction_rate += e.resistance_to_different_faction_rate;
                    equipments.damage_to_same_faction_rate += e.damage_to_same_faction_rate;
                    equipments.resistance_to_same_faction_rate += e.resistance_to_same_faction_rate;
                    equipments.normal_damage_rate += e.normal_damage_rate;
                    equipments.normal_resistance_rate += e.normal_resistance_rate;
                    equipments.skill_damage_rate += e.skill_damage_rate;
                    equipments.skill_resistance_rate += e.skill_resistance_rate;
                    equipments.special_health += e.special_health;
                    equipments.special_physical_attack += e.special_physical_attack;
                    equipments.special_physical_defense += e.special_physical_defense;
                    equipments.special_magical_attack += e.special_magical_attack;
                    equipments.special_magical_defense += e.special_magical_defense;
                    equipments.special_chemical_attack += e.special_chemical_attack;
                    equipments.special_chemical_defense += e.special_chemical_defense;
                    equipments.special_atomic_attack += e.special_atomic_attack;
                    equipments.special_atomic_defense += e.special_atomic_defense;
                    equipments.special_mental_attack += e.special_mental_attack;
                    equipments.special_mental_defense += e.special_mental_defense;
                    equipments.special_speed += e.speed;
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
    public Equipments GetAllEquipmentsByCardSpellId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT *
                FROM user_card_spell uc, card_spell c, card_spell_equipment che, user_equipments ue
                WHERE uc.card_spell_id = c.id AND uc.card_spell_id = che.card_spell_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_spell_id = @card_spell_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_spell_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.critical_resistance_rate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.ignore_critical_rate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.penetration_rate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.penetration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.evasion_rate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.ignore_damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.absorbed_damage_rate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.vitality_regeneration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.shield_strength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.resistance_rate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.combo_rate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.ignore_combo_rate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.combo_damage_rate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.combo_resistance_rate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.stun_rate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.ignore_stun_rate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.reflection_rate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.ignore_reflection_rate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.reflection_damage_rate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.reflection_resistance_rate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.normal_damage_rate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.normal_resistance_rate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.skill_damage_rate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.skill_resistance_rate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.special_health = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.special_speed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);

                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.power += e.power;
                    equipments.health += e.health;
                    equipments.physical_attack += e.physical_attack;
                    equipments.physical_defense += e.physical_defense;
                    equipments.magical_attack += e.magical_attack;
                    equipments.magical_defense += e.magical_defense;
                    equipments.chemical_attack += e.chemical_attack;
                    equipments.chemical_defense += e.chemical_defense;
                    equipments.atomic_attack += e.atomic_attack;
                    equipments.atomic_defense += e.atomic_defense;
                    equipments.mental_attack += e.mental_attack;
                    equipments.mental_defense += e.mental_defense;
                    equipments.speed += e.speed;
                    equipments.critical_damage_rate += e.critical_damage_rate;
                    equipments.critical_rate += e.critical_rate;
                    equipments.critical_resistance_rate += e.critical_resistance_rate;
                    equipments.ignore_critical_rate += e.ignore_critical_rate;
                    equipments.penetration_rate += e.penetration_rate;
                    equipments.penetration_resistance_rate += e.penetration_resistance_rate;
                    equipments.evasion_rate += e.evasion_rate;
                    equipments.damage_absorption_rate += e.damage_absorption_rate;
                    equipments.ignore_damage_absorption_rate += e.ignore_damage_absorption_rate;
                    equipments.absorbed_damage_rate += e.absorbed_damage_rate;
                    equipments.vitality_regeneration_rate += e.vitality_regeneration_rate;
                    equipments.vitality_regeneration_resistance_rate += e.vitality_regeneration_resistance_rate;
                    equipments.accuracy_rate += e.accuracy_rate;
                    equipments.lifesteal_rate += e.lifesteal_rate;
                    equipments.shield_strength += e.shield_strength;
                    equipments.tenacity += e.tenacity;
                    equipments.resistance_rate += e.resistance_rate;
                    equipments.combo_rate += e.combo_rate;
                    equipments.ignore_combo_rate += e.ignore_combo_rate;
                    equipments.combo_damage_rate += e.combo_damage_rate;
                    equipments.combo_resistance_rate += e.combo_resistance_rate;
                    equipments.stun_rate += e.stun_rate;
                    equipments.ignore_stun_rate += e.ignore_stun_rate;
                    equipments.reflection_rate += e.reflection_rate;
                    equipments.ignore_reflection_rate += e.ignore_reflection_rate;
                    equipments.reflection_damage_rate += e.reflection_damage_rate;
                    equipments.reflection_resistance_rate += e.reflection_resistance_rate;
                    equipments.mana += e.mana;
                    equipments.mana_regeneration_rate += e.mana_regeneration_rate;
                    equipments.damage_to_different_faction_rate += e.damage_to_different_faction_rate;
                    equipments.resistance_to_different_faction_rate += e.resistance_to_different_faction_rate;
                    equipments.damage_to_same_faction_rate += e.damage_to_same_faction_rate;
                    equipments.resistance_to_same_faction_rate += e.resistance_to_same_faction_rate;
                    equipments.normal_damage_rate += e.normal_damage_rate;
                    equipments.normal_resistance_rate += e.normal_resistance_rate;
                    equipments.skill_damage_rate += e.skill_damage_rate;
                    equipments.skill_resistance_rate += e.skill_resistance_rate;
                    equipments.special_health += e.special_health;
                    equipments.special_physical_attack += e.special_physical_attack;
                    equipments.special_physical_defense += e.special_physical_defense;
                    equipments.special_magical_attack += e.special_magical_attack;
                    equipments.special_magical_defense += e.special_magical_defense;
                    equipments.special_chemical_attack += e.special_chemical_attack;
                    equipments.special_chemical_defense += e.special_chemical_defense;
                    equipments.special_atomic_attack += e.special_atomic_attack;
                    equipments.special_atomic_defense += e.special_atomic_defense;
                    equipments.special_mental_attack += e.special_mental_attack;
                    equipments.special_mental_defense += e.special_mental_defense;
                    equipments.special_speed += e.speed;
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
    public Equipments GetAllEquipmentsByBooksId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT *
                FROM user_books uc, books c, books_equipment che, user_equipments ue
                WHERE uc.book_id = c.id AND uc.book_id = che.book_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.book_id = @book_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@book_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.critical_resistance_rate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.ignore_critical_rate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.penetration_rate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.penetration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.evasion_rate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.ignore_damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.absorbed_damage_rate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.vitality_regeneration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.shield_strength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.resistance_rate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.combo_rate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.ignore_combo_rate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.combo_damage_rate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.combo_resistance_rate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.stun_rate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.ignore_stun_rate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.reflection_rate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.ignore_reflection_rate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.reflection_damage_rate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.reflection_resistance_rate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.normal_damage_rate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.normal_resistance_rate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.skill_damage_rate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.skill_resistance_rate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.special_health = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.special_speed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);

                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.power += e.power;
                    equipments.health += e.health;
                    equipments.physical_attack += e.physical_attack;
                    equipments.physical_defense += e.physical_defense;
                    equipments.magical_attack += e.magical_attack;
                    equipments.magical_defense += e.magical_defense;
                    equipments.chemical_attack += e.chemical_attack;
                    equipments.chemical_defense += e.chemical_defense;
                    equipments.atomic_attack += e.atomic_attack;
                    equipments.atomic_defense += e.atomic_defense;
                    equipments.mental_attack += e.mental_attack;
                    equipments.mental_defense += e.mental_defense;
                    equipments.speed += e.speed;
                    equipments.critical_damage_rate += e.critical_damage_rate;
                    equipments.critical_rate += e.critical_rate;
                    equipments.critical_resistance_rate += e.critical_resistance_rate;
                    equipments.ignore_critical_rate += e.ignore_critical_rate;
                    equipments.penetration_rate += e.penetration_rate;
                    equipments.penetration_resistance_rate += e.penetration_resistance_rate;
                    equipments.evasion_rate += e.evasion_rate;
                    equipments.damage_absorption_rate += e.damage_absorption_rate;
                    equipments.ignore_damage_absorption_rate += e.ignore_damage_absorption_rate;
                    equipments.absorbed_damage_rate += e.absorbed_damage_rate;
                    equipments.vitality_regeneration_rate += e.vitality_regeneration_rate;
                    equipments.vitality_regeneration_resistance_rate += e.vitality_regeneration_resistance_rate;
                    equipments.accuracy_rate += e.accuracy_rate;
                    equipments.lifesteal_rate += e.lifesteal_rate;
                    equipments.shield_strength += e.shield_strength;
                    equipments.tenacity += e.tenacity;
                    equipments.resistance_rate += e.resistance_rate;
                    equipments.combo_rate += e.combo_rate;
                    equipments.ignore_combo_rate += e.ignore_combo_rate;
                    equipments.combo_damage_rate += e.combo_damage_rate;
                    equipments.combo_resistance_rate += e.combo_resistance_rate;
                    equipments.stun_rate += e.stun_rate;
                    equipments.ignore_stun_rate += e.ignore_stun_rate;
                    equipments.reflection_rate += e.reflection_rate;
                    equipments.ignore_reflection_rate += e.ignore_reflection_rate;
                    equipments.reflection_damage_rate += e.reflection_damage_rate;
                    equipments.reflection_resistance_rate += e.reflection_resistance_rate;
                    equipments.mana += e.mana;
                    equipments.mana_regeneration_rate += e.mana_regeneration_rate;
                    equipments.damage_to_different_faction_rate += e.damage_to_different_faction_rate;
                    equipments.resistance_to_different_faction_rate += e.resistance_to_different_faction_rate;
                    equipments.damage_to_same_faction_rate += e.damage_to_same_faction_rate;
                    equipments.resistance_to_same_faction_rate += e.resistance_to_same_faction_rate;
                    equipments.normal_damage_rate += e.normal_damage_rate;
                    equipments.normal_resistance_rate += e.normal_resistance_rate;
                    equipments.skill_damage_rate += e.skill_damage_rate;
                    equipments.skill_resistance_rate += e.skill_resistance_rate;
                    equipments.special_health += e.special_health;
                    equipments.special_physical_attack += e.special_physical_attack;
                    equipments.special_physical_defense += e.special_physical_defense;
                    equipments.special_magical_attack += e.special_magical_attack;
                    equipments.special_magical_defense += e.special_magical_defense;
                    equipments.special_chemical_attack += e.special_chemical_attack;
                    equipments.special_chemical_defense += e.special_chemical_defense;
                    equipments.special_atomic_attack += e.special_atomic_attack;
                    equipments.special_atomic_defense += e.special_atomic_defense;
                    equipments.special_mental_attack += e.special_mental_attack;
                    equipments.special_mental_defense += e.special_mental_defense;
                    equipments.special_speed += e.speed;
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
    public Equipments GetAllEquipmentsByPetsId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT *
                FROM user_pets uc, pets c, pets_equipment che, user_equipments ue
                WHERE uc.pet_id = c.id AND uc.pet_id = che.pet_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.pet_id = @pet_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@pet_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.physical_attack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.physical_defense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.magical_attack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.magical_defense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.chemical_attack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.chemical_defense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.atomic_attack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.atomic_defense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.mental_attack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.mental_defense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.critical_rate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.critical_resistance_rate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.ignore_critical_rate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.penetration_rate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.penetration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.evasion_rate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.ignore_damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.absorbed_damage_rate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.vitality_regeneration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.shield_strength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.resistance_rate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.combo_rate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.ignore_combo_rate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.combo_damage_rate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.combo_resistance_rate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.stun_rate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.ignore_stun_rate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.reflection_rate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.ignore_reflection_rate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.reflection_damage_rate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.reflection_resistance_rate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.normal_damage_rate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.normal_resistance_rate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.skill_damage_rate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.skill_resistance_rate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.special_health = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.special_physical_attack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.special_physical_defense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.special_magical_attack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.special_magical_defense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.special_chemical_attack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.special_chemical_defense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.special_atomic_attack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.special_atomic_defense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.special_mental_attack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.special_mental_defense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.special_speed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);

                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.power += e.power;
                    equipments.health += e.health;
                    equipments.physical_attack += e.physical_attack;
                    equipments.physical_defense += e.physical_defense;
                    equipments.magical_attack += e.magical_attack;
                    equipments.magical_defense += e.magical_defense;
                    equipments.chemical_attack += e.chemical_attack;
                    equipments.chemical_defense += e.chemical_defense;
                    equipments.atomic_attack += e.atomic_attack;
                    equipments.atomic_defense += e.atomic_defense;
                    equipments.mental_attack += e.mental_attack;
                    equipments.mental_defense += e.mental_defense;
                    equipments.speed += e.speed;
                    equipments.critical_damage_rate += e.critical_damage_rate;
                    equipments.critical_rate += e.critical_rate;
                    equipments.critical_resistance_rate += e.critical_resistance_rate;
                    equipments.ignore_critical_rate += e.ignore_critical_rate;
                    equipments.penetration_rate += e.penetration_rate;
                    equipments.penetration_resistance_rate += e.penetration_resistance_rate;
                    equipments.evasion_rate += e.evasion_rate;
                    equipments.damage_absorption_rate += e.damage_absorption_rate;
                    equipments.ignore_damage_absorption_rate += e.ignore_damage_absorption_rate;
                    equipments.absorbed_damage_rate += e.absorbed_damage_rate;
                    equipments.vitality_regeneration_rate += e.vitality_regeneration_rate;
                    equipments.vitality_regeneration_resistance_rate += e.vitality_regeneration_resistance_rate;
                    equipments.accuracy_rate += e.accuracy_rate;
                    equipments.lifesteal_rate += e.lifesteal_rate;
                    equipments.shield_strength += e.shield_strength;
                    equipments.tenacity += e.tenacity;
                    equipments.resistance_rate += e.resistance_rate;
                    equipments.combo_rate += e.combo_rate;
                    equipments.ignore_combo_rate += e.ignore_combo_rate;
                    equipments.combo_damage_rate += e.combo_damage_rate;
                    equipments.combo_resistance_rate += e.combo_resistance_rate;
                    equipments.stun_rate += e.stun_rate;
                    equipments.ignore_stun_rate += e.ignore_stun_rate;
                    equipments.reflection_rate += e.reflection_rate;
                    equipments.ignore_reflection_rate += e.ignore_reflection_rate;
                    equipments.reflection_damage_rate += e.reflection_damage_rate;
                    equipments.reflection_resistance_rate += e.reflection_resistance_rate;
                    equipments.mana += e.mana;
                    equipments.mana_regeneration_rate += e.mana_regeneration_rate;
                    equipments.damage_to_different_faction_rate += e.damage_to_different_faction_rate;
                    equipments.resistance_to_different_faction_rate += e.resistance_to_different_faction_rate;
                    equipments.damage_to_same_faction_rate += e.damage_to_same_faction_rate;
                    equipments.resistance_to_same_faction_rate += e.resistance_to_same_faction_rate;
                    equipments.normal_damage_rate += e.normal_damage_rate;
                    equipments.normal_resistance_rate += e.normal_resistance_rate;
                    equipments.skill_damage_rate += e.skill_damage_rate;
                    equipments.skill_resistance_rate += e.skill_resistance_rate;
                    equipments.special_health += e.special_health;
                    equipments.special_physical_attack += e.special_physical_attack;
                    equipments.special_physical_defense += e.special_physical_defense;
                    equipments.special_magical_attack += e.special_magical_attack;
                    equipments.special_magical_defense += e.special_magical_defense;
                    equipments.special_chemical_attack += e.special_chemical_attack;
                    equipments.special_chemical_defense += e.special_chemical_defense;
                    equipments.special_atomic_attack += e.special_atomic_attack;
                    equipments.special_atomic_defense += e.special_atomic_defense;
                    equipments.special_mental_attack += e.special_mental_attack;
                    equipments.special_mental_defense += e.special_mental_defense;
                    equipments.special_speed += e.speed;
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