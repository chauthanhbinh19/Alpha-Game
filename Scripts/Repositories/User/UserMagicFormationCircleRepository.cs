using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserMagicFormationCirlceRepository : IUserMagicFormationCircleRepository
{
    public List<MagicFormationCircle> GetUserMagicFormationCircle(string user_id, string type, int pageSize, int offset)
    {
        List<MagicFormationCircle> magicFormationCircles = new List<MagicFormationCircle>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select um.*, m.id, m.name, m.image, m.rare, m.description from magic_formation_circle m, user_magic_formation_circle um where m.id=um.mfc_id and um.user_id=@userId and m.type=@type 
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
                        id = reader.GetString("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        quality = reader.GetInt32("quality"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
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
                        // percent_all_health = reader.GetDouble("percent_all_health"),
                        // percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        // percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        // percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        // percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        // percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        // percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        // percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        // percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        // percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        // percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
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
    public int GetUserMagicFormationCircleCount(string user_id, string type)
    {
        int count = 0;
        // string user_id = User.CurrentUserId;
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
    public bool InsertUserMagicFormationCircle(MagicFormationCircle magicFormationCircle)
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
                    user_id, mfc_id, level, experiment, star, quality, block, quantity,
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
                    @user_id, @mfc_id, @level, @experiment, @star, @quality, @block, @quantity,
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
                    command.Parameters.AddWithValue("@mfc_id", magicFormationCircle.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(magicFormationCircle.rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", magicFormationCircle.quantity);
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
                    command.Parameters.AddWithValue("@critical_damage_rate", magicFormationCircle.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", magicFormationCircle.critical_rate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", magicFormationCircle.critical_resistance_rate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", magicFormationCircle.ignore_critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", magicFormationCircle.penetration_rate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", magicFormationCircle.penetration_resistance_rate);
                    command.Parameters.AddWithValue("@evasion_rate", magicFormationCircle.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", magicFormationCircle.damage_absorption_rate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", magicFormationCircle.ignore_damage_absorption_rate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", magicFormationCircle.absorbed_damage_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", magicFormationCircle.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", magicFormationCircle.vitality_regeneration_resistance_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", magicFormationCircle.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", magicFormationCircle.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", magicFormationCircle.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", magicFormationCircle.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", magicFormationCircle.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", magicFormationCircle.combo_rate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", magicFormationCircle.ignore_combo_rate);
                    command.Parameters.AddWithValue("@combo_damage_rate", magicFormationCircle.combo_damage_rate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", magicFormationCircle.combo_resistance_rate);
                    command.Parameters.AddWithValue("@stun_rate", magicFormationCircle.stun_rate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", magicFormationCircle.ignore_stun_rate);
                    command.Parameters.AddWithValue("@reflection_rate", magicFormationCircle.reflection_rate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", magicFormationCircle.ignore_reflection_rate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", magicFormationCircle.reflection_damage_rate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", magicFormationCircle.reflection_resistance_rate);
                    command.Parameters.AddWithValue("@mana", magicFormationCircle.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", magicFormationCircle.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", magicFormationCircle.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", magicFormationCircle.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", magicFormationCircle.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", magicFormationCircle.resistance_to_same_faction_rate);
                    command.Parameters.AddWithValue("@normal_damage_rate", magicFormationCircle.normal_damage_rate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", magicFormationCircle.normal_resistance_rate);
                    command.Parameters.AddWithValue("@skill_damage_rate", magicFormationCircle.skill_damage_rate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", magicFormationCircle.skill_resistance_rate);

                    // command.Parameters.AddWithValue("@percent_all_health", magicFormationCircle.percent_all_health);
                    // command.Parameters.AddWithValue("@percent_all_physical_attack", magicFormationCircle.percent_all_physical_attack);
                    // command.Parameters.AddWithValue("@percent_all_physical_defense", magicFormationCircle.percent_all_physical_defense);
                    // command.Parameters.AddWithValue("@percent_all_magical_attack", magicFormationCircle.percent_all_magical_attack);
                    // command.Parameters.AddWithValue("@percent_all_magical_defense", magicFormationCircle.percent_all_magical_defense);
                    // command.Parameters.AddWithValue("@percent_all_chemical_attack", magicFormationCircle.percent_all_chemical_attack);
                    // command.Parameters.AddWithValue("@percent_all_chemical_defense", magicFormationCircle.percent_all_chemical_defense);
                    // command.Parameters.AddWithValue("@percent_all_atomic_attack", magicFormationCircle.percent_all_atomic_attack);
                    // command.Parameters.AddWithValue("@percent_all_atomic_defense", magicFormationCircle.percent_all_atomic_defense);
                    // command.Parameters.AddWithValue("@percent_all_mental_attack", magicFormationCircle.percent_all_mental_attack);
                    // command.Parameters.AddWithValue("@percent_all_mental_defense", magicFormationCircle.percent_all_mental_defense);
                    // command.Parameters.AddWithValue("@percent_all_speed", 20);
                    // command.Parameters.AddWithValue("@percent_all_critical_damage", 20);
                    // command.Parameters.AddWithValue("@percent_all_critical_rate", 20);
                    // command.Parameters.AddWithValue("@percent_all_armor_penetration", 20);
                    // command.Parameters.AddWithValue("@percent_all_avoid", 20);
                    // command.Parameters.AddWithValue("@percent_all_absorbs_damage", 20);
                    // command.Parameters.AddWithValue("@percent_all_regenerate_vitality", 20);
                    // command.Parameters.AddWithValue("@percent_all_accuracy", 20);
                    // command.Parameters.AddWithValue("@percent_all_mana", 20);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_magic_formation_circle
                    SET quantity = @quantity
                    WHERE user_id = @user_id AND mfc_id = @mfc_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@mfc_id", magicFormationCircle.id);
                    updateCommand.Parameters.AddWithValue("@quantity", magicFormationCircle.quantity);

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
    public bool UpdateMagicFormationCircleLevel(MagicFormationCircle magicFormationCircle, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_magic_formation_circle
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
                WHERE user_id = @user_id AND mfc_id = @mfc_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@mfc_id", magicFormationCircle.id);
                command.Parameters.AddWithValue("@level", cardLevel);
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
                command.Parameters.AddWithValue("@critical_damage_rate", magicFormationCircle.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", magicFormationCircle.critical_rate);
                command.Parameters.AddWithValue("@critical_resistance_rate", magicFormationCircle.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", magicFormationCircle.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", magicFormationCircle.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", magicFormationCircle.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", magicFormationCircle.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", magicFormationCircle.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", magicFormationCircle.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", magicFormationCircle.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", magicFormationCircle.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", magicFormationCircle.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", magicFormationCircle.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", magicFormationCircle.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", magicFormationCircle.shield_strength);
                command.Parameters.AddWithValue("@tenacity", magicFormationCircle.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", magicFormationCircle.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", magicFormationCircle.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", magicFormationCircle.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", magicFormationCircle.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", magicFormationCircle.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", magicFormationCircle.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", magicFormationCircle.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", magicFormationCircle.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", magicFormationCircle.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", magicFormationCircle.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", magicFormationCircle.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", magicFormationCircle.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", magicFormationCircle.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", magicFormationCircle.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", magicFormationCircle.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", magicFormationCircle.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", magicFormationCircle.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", magicFormationCircle.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", magicFormationCircle.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", magicFormationCircle.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", magicFormationCircle.skill_resistance_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateMagicFormationCircleBreakthrough(MagicFormationCircle magicFormationCircle, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_magic_formation_circle
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
                WHERE user_id = @user_id AND mfc_id = @mfc_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@mfc_id", magicFormationCircle.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
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
                command.Parameters.AddWithValue("@critical_damage_rate", magicFormationCircle.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", magicFormationCircle.critical_rate);
                command.Parameters.AddWithValue("@critical_resistance_rate", magicFormationCircle.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", magicFormationCircle.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", magicFormationCircle.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", magicFormationCircle.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", magicFormationCircle.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", magicFormationCircle.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", magicFormationCircle.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", magicFormationCircle.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", magicFormationCircle.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", magicFormationCircle.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", magicFormationCircle.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", magicFormationCircle.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", magicFormationCircle.shield_strength);
                command.Parameters.AddWithValue("@tenacity", magicFormationCircle.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", magicFormationCircle.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", magicFormationCircle.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", magicFormationCircle.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", magicFormationCircle.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", magicFormationCircle.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", magicFormationCircle.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", magicFormationCircle.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", magicFormationCircle.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", magicFormationCircle.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", magicFormationCircle.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", magicFormationCircle.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", magicFormationCircle.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", magicFormationCircle.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", magicFormationCircle.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", magicFormationCircle.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", magicFormationCircle.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", magicFormationCircle.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", magicFormationCircle.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", magicFormationCircle.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", magicFormationCircle.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", magicFormationCircle.skill_resistance_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public MagicFormationCircle GetUserMagicFormationCircleById(string user_id, string Id)
    {
        MagicFormationCircle card = new MagicFormationCircle();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_magic_formation_circle where mfc_id=@id 
                and user_magic_formation_circle.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new MagicFormationCircle
                    {
                        id = reader.GetString("mfc_id"),
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
    public MagicFormationCircle SumPowerUserMagicFormationCircle()
    {
        MagicFormationCircle sumMagicFormationCircle = new MagicFormationCircle();
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
            FROM user_magic_formation_circle
            WHERE user_id = @user_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumMagicFormationCircle.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumMagicFormationCircle.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumMagicFormationCircle.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumMagicFormationCircle.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumMagicFormationCircle.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumMagicFormationCircle.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumMagicFormationCircle.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumMagicFormationCircle.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumMagicFormationCircle.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumMagicFormationCircle.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumMagicFormationCircle.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumMagicFormationCircle.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumMagicFormationCircle.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumMagicFormationCircle.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                        sumMagicFormationCircle.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumMagicFormationCircle.critical_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                        sumMagicFormationCircle.ignore_critical_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                        sumMagicFormationCircle.penetration_rate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                        sumMagicFormationCircle.penetration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                        sumMagicFormationCircle.evasion_rate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                        sumMagicFormationCircle.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                        sumMagicFormationCircle.ignore_damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                        sumMagicFormationCircle.absorbed_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                        sumMagicFormationCircle.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                        sumMagicFormationCircle.vitality_regeneration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                        sumMagicFormationCircle.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                        sumMagicFormationCircle.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                        sumMagicFormationCircle.shield_strength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                        sumMagicFormationCircle.tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                        sumMagicFormationCircle.resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                        sumMagicFormationCircle.combo_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                        sumMagicFormationCircle.ignore_combo_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                        sumMagicFormationCircle.combo_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                        sumMagicFormationCircle.combo_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                        sumMagicFormationCircle.stun_rate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                        sumMagicFormationCircle.ignore_stun_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                        sumMagicFormationCircle.reflection_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                        sumMagicFormationCircle.ignore_reflection_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                        sumMagicFormationCircle.reflection_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                        sumMagicFormationCircle.reflection_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                        sumMagicFormationCircle.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                        sumMagicFormationCircle.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                        sumMagicFormationCircle.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                        sumMagicFormationCircle.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                        sumMagicFormationCircle.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                        sumMagicFormationCircle.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                        sumMagicFormationCircle.normal_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                        sumMagicFormationCircle.normal_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                        sumMagicFormationCircle.skill_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                        sumMagicFormationCircle.skill_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
                    }
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return sumMagicFormationCircle;
    }
}