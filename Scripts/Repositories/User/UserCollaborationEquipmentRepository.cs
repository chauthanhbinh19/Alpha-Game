using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserCollaborationEquipmentRepository : IUserCollaborationEquipmentRepository
{
    public List<CollaborationEquipment> GetUserCollaborationEquipments(string user_id, string type, int pageSize, int offset)
    {
        List<CollaborationEquipment> collaborationEquipmentList = new List<CollaborationEquipment>();
        // string user_id = User.CurrentUserId;
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
                        id = reader.GetString("collaboration_equipment_id"),
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
    public int GetUserCollaborationEquipmentCount(string user_id, string type)
    {
        int count = 0;
        // string user_id = User.CurrentUserId;
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
                    user_id, collaboration_equipment_id, level, experiment, star, quality, block, quantity,
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
                    @user_id, @collaboration_equipment_id, @level, @experiment, @star, @quality, @block, @quantity,
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
                    command.Parameters.AddWithValue("@collaboration_equipment_id", collaborationEquipment.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(collaborationEquipment.rare));
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
                    command.Parameters.AddWithValue("@mental_attack", collaborationEquipment.mental_attack);
                    command.Parameters.AddWithValue("@mental_defense", collaborationEquipment.mental_defense);
                    command.Parameters.AddWithValue("@speed", collaborationEquipment.speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", collaborationEquipment.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", collaborationEquipment.critical_rate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", collaborationEquipment.critical_resistance_rate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", collaborationEquipment.ignore_critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", collaborationEquipment.penetration_rate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", collaborationEquipment.penetration_resistance_rate);
                    command.Parameters.AddWithValue("@evasion_rate", collaborationEquipment.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", collaborationEquipment.damage_absorption_rate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", collaborationEquipment.ignore_damage_absorption_rate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", collaborationEquipment.absorbed_damage_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", collaborationEquipment.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", collaborationEquipment.vitality_regeneration_resistance_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", collaborationEquipment.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", collaborationEquipment.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", collaborationEquipment.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", collaborationEquipment.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", collaborationEquipment.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", collaborationEquipment.combo_rate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", collaborationEquipment.ignore_combo_rate);
                    command.Parameters.AddWithValue("@combo_damage_rate", collaborationEquipment.combo_damage_rate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", collaborationEquipment.combo_resistance_rate);
                    command.Parameters.AddWithValue("@stun_rate", collaborationEquipment.stun_rate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", collaborationEquipment.ignore_stun_rate);
                    command.Parameters.AddWithValue("@reflection_rate", collaborationEquipment.reflection_rate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", collaborationEquipment.ignore_reflection_rate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", collaborationEquipment.reflection_damage_rate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", collaborationEquipment.reflection_resistance_rate);
                    command.Parameters.AddWithValue("@mana", collaborationEquipment.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", collaborationEquipment.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", collaborationEquipment.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", collaborationEquipment.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", collaborationEquipment.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", collaborationEquipment.resistance_to_same_faction_rate);
                    command.Parameters.AddWithValue("@normal_damage_rate", collaborationEquipment.normal_damage_rate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", collaborationEquipment.normal_resistance_rate);
                    command.Parameters.AddWithValue("@skill_damage_rate", collaborationEquipment.skill_damage_rate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", collaborationEquipment.skill_resistance_rate);
                    command.ExecuteNonQuery();
                }
                else
                {
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
                command.Parameters.AddWithValue("@critical_resistance_rate", collaborationEquipment.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", collaborationEquipment.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", collaborationEquipment.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", collaborationEquipment.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", collaborationEquipment.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", collaborationEquipment.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", collaborationEquipment.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", collaborationEquipment.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", collaborationEquipment.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", collaborationEquipment.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", collaborationEquipment.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", collaborationEquipment.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", collaborationEquipment.shield_strength);
                command.Parameters.AddWithValue("@tenacity", collaborationEquipment.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", collaborationEquipment.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", collaborationEquipment.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", collaborationEquipment.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", collaborationEquipment.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", collaborationEquipment.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", collaborationEquipment.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", collaborationEquipment.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", collaborationEquipment.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", collaborationEquipment.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", collaborationEquipment.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", collaborationEquipment.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", collaborationEquipment.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", collaborationEquipment.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", collaborationEquipment.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", collaborationEquipment.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", collaborationEquipment.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", collaborationEquipment.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", collaborationEquipment.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", collaborationEquipment.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", collaborationEquipment.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", collaborationEquipment.skill_resistance_rate);
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
                command.Parameters.AddWithValue("@critical_resistance_rate", collaborationEquipment.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", collaborationEquipment.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", collaborationEquipment.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", collaborationEquipment.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", collaborationEquipment.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", collaborationEquipment.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", collaborationEquipment.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", collaborationEquipment.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", collaborationEquipment.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", collaborationEquipment.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", collaborationEquipment.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", collaborationEquipment.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", collaborationEquipment.shield_strength);
                command.Parameters.AddWithValue("@tenacity", collaborationEquipment.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", collaborationEquipment.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", collaborationEquipment.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", collaborationEquipment.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", collaborationEquipment.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", collaborationEquipment.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", collaborationEquipment.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", collaborationEquipment.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", collaborationEquipment.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", collaborationEquipment.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", collaborationEquipment.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", collaborationEquipment.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", collaborationEquipment.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", collaborationEquipment.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", collaborationEquipment.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", collaborationEquipment.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", collaborationEquipment.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", collaborationEquipment.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", collaborationEquipment.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", collaborationEquipment.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", collaborationEquipment.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", collaborationEquipment.skill_resistance_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public CollaborationEquipment GetUserCollaborationEquipmentsById(string user_id, string Id)
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
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new CollaborationEquipment
                    {
                        id = reader.GetString("collaboration_equipment_id"),
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
                SUM(power * (1 + quality / 10.0)) AS total_power,
                SUM(health * (1 + quality / 10.0)) AS total_health,
                SUM(mana * (1 + quality / 10.0)) AS total_mana,
                SUM(physical_attack * (1 + quality / 10.0)) AS total_physical_attack,
                SUM(physical_defense * (1 + quality / 10.0)) AS total_physical_defense,
                SUM(magical_attack * (1 + quality / 10.0)) AS total_magical_attack,
                SUM(magical_defense * (1 + quality / 10.0)) AS total_magical_defense,
                SUM(chemical_attack * (1 + quality / 10.0)) AS total_chemical_attack,
                SUM(chemical_defense * (1 + quality / 10.0)) AS total_chemical_defense,
                SUM(atomic_attack * (1 + quality / 10.0)) AS total_atomic_attack,
                SUM(atomic_defense * (1 + quality / 10.0)) AS total_atomic_defense,
                SUM(mental_attack * (1 + quality / 10.0)) AS total_mental_attack,
                SUM(mental_defense * (1 + quality / 10.0)) AS total_mental_defense,
                SUM(speed * (1 + quality / 10.0)) AS total_speed,
                SUM(critical_damage_rate * (1 + quality / 10.0)) AS total_critical_damage_rate,
                SUM(critical_rate * (1 + quality / 10.0)) AS total_critical_rate,
                SUM(critical_resistance_rate * (1 + quality / 10.0)) AS total_critical_resistance_rate,
                SUM(ignore_critical_rate * (1 + quality / 10.0)) AS total_ignore_critical_rate,
                SUM(penetration_rate * (1 + quality / 10.0)) AS total_penetration_rate,
                SUM(penetration_resistance_rate * (1 + quality / 10.0)) AS total_penetration_resistance_rate,
                SUM(evasion_rate * (1 + quality / 10.0)) AS total_evasion_rate,
                SUM(damage_absorption_rate * (1 + quality / 10.0)) AS total_damage_absorption_rate,
                SUM(ignore_damage_absorption_rate * (1 + quality / 10.0)) AS total_ignore_damage_absorption_rate,
                SUM(absorbed_damage_rate * (1 + quality / 10.0)) AS total_absorbed_damage_rate,
                SUM(vitality_regeneration_rate * (1 + quality / 10.0)) AS total_vitality_regeneration_rate,
                SUM(vitality_regeneration_resistance_rate * (1 + quality / 10.0)) AS total_vitality_regeneration_resistance_rate,
                SUM(accuracy_rate * (1 + quality / 10.0)) AS total_accuracy_rate,
                SUM(lifesteal_rate * (1 + quality / 10.0)) AS total_lifesteal_rate,
                SUM(shield_strength * (1 + quality / 10.0)) AS total_shield_strength,
                SUM(tenacity * (1 + quality / 10.0)) AS total_tenacity,
                SUM(resistance_rate * (1 + quality / 10.0)) AS total_resistance_rate,
                SUM(combo_rate * (1 + quality / 10.0)) AS total_combo_rate,
                SUM(ignore_combo_rate * (1 + quality / 10.0)) AS total_ignore_combo_rate,
                SUM(combo_damage_rate * (1 + quality / 10.0)) AS total_combo_damage_rate,
                SUM(combo_resistance_rate * (1 + quality / 10.0)) AS total_combo_resistance_rate,
                SUM(stun_rate * (1 + quality / 10.0)) AS total_stun_rate,
                SUM(ignore_stun_rate * (1 + quality / 10.0)) AS total_ignore_stun_rate,
                SUM(reflection_rate * (1 + quality / 10.0)) AS total_reflection_rate,
                SUM(ignore_reflection_rate * (1 + quality / 10.0)) AS total_ignore_reflection_rate,
                SUM(reflection_damage_rate * (1 + quality / 10.0)) AS total_reflection_damage_rate,
                SUM(reflection_resistance_rate * (1 + quality / 10.0)) AS total_reflection_resistance_rate,
                SUM(mana_regeneration_rate * (1 + quality / 10.0)) AS total_mana_regeneration_rate,
                SUM(damage_to_different_faction_rate * (1 + quality / 10.0)) AS total_damage_to_different_faction_rate,
                SUM(resistance_to_different_faction_rate * (1 + quality / 10.0)) AS total_resistance_to_different_faction_rate,
                SUM(damage_to_same_faction_rate * (1 + quality / 10.0)) AS total_damage_to_same_faction_rate,
                SUM(resistance_to_same_faction_rate * (1 + quality / 10.0)) AS total_resistance_to_same_faction_rate,
                SUM(normal_damage_rate * (1 + quality / 10.0)) AS total_normal_damage_rate,
                SUM(normal_resistance_rate * (1 + quality / 10.0)) AS total_normal_resistance_rate,
                SUM(skill_damage_rate * (1 + quality / 10.0)) AS total_skill_damage_rate,
                SUM(skill_resistance_rate * (1 + quality / 10.0)) AS total_skill_resistance_rate
            FROM user_collaboration_equipments
            WHERE user_id = @user_id;";
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
                        sumCollaborationEquipments.critical_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                        sumCollaborationEquipments.ignore_critical_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                        sumCollaborationEquipments.penetration_rate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                        sumCollaborationEquipments.penetration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                        sumCollaborationEquipments.evasion_rate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                        sumCollaborationEquipments.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                        sumCollaborationEquipments.ignore_damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                        sumCollaborationEquipments.absorbed_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                        sumCollaborationEquipments.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                        sumCollaborationEquipments.vitality_regeneration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                        sumCollaborationEquipments.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                        sumCollaborationEquipments.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                        sumCollaborationEquipments.shield_strength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                        sumCollaborationEquipments.tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                        sumCollaborationEquipments.resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                        sumCollaborationEquipments.combo_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                        sumCollaborationEquipments.ignore_combo_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                        sumCollaborationEquipments.combo_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                        sumCollaborationEquipments.combo_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                        sumCollaborationEquipments.stun_rate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                        sumCollaborationEquipments.ignore_stun_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                        sumCollaborationEquipments.reflection_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                        sumCollaborationEquipments.ignore_reflection_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                        sumCollaborationEquipments.reflection_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                        sumCollaborationEquipments.reflection_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                        sumCollaborationEquipments.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                        sumCollaborationEquipments.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                        sumCollaborationEquipments.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                        sumCollaborationEquipments.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                        sumCollaborationEquipments.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                        sumCollaborationEquipments.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                        sumCollaborationEquipments.normal_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                        sumCollaborationEquipments.normal_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                        sumCollaborationEquipments.skill_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                        sumCollaborationEquipments.skill_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
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