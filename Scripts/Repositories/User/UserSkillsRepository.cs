using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserSkillsRepository : IUserSkillsRepository
{
    public List<Skills> GetUserSkills(string user_id, string type, int pageSize, int offset)
    {
        List<Skills> skillsList = new List<Skills>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select us.*, s.name, s.image, s.rare, s.type, s.description from Skills s,user_skills us where s.id=us.skill_id and us.user_id=@userId and s.type= @type 
                ORDER BY s.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Skills skill = new Skills
                    {
                        id = reader.GetString("skill_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        quality = reader.GetInt32("quality"),
                        type = reader.GetString("type"),
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

                    skillsList.Add(skill);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return skillsList;
    }
    public int GetUserSkillsCount(string user_id, string type)
    {
        int count = 0;
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from skills s, user_skills us where s.id=us.skill_id and us.user_id=@userId and s.type= @type";
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
    public bool InsertUserSkills(Skills skills)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_skills 
                WHERE user_id = @user_id AND skill_id = @skill_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@skill_id", skills.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_skills (
                    user_id, skill_id, level, experiment, star, quality, block, quantity, power, health, physical_attack, 
                    physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, 
                    atomic_defense, mental_attack, mental_defense, speed, critical_damage_rate, critical_rate, 
                    penetration_rate, evasion_rate, damage_absorption_rate, vitality_regeneration_rate, accuracy_rate, 
                    lifesteal_rate, shield_strength, tenacity, resistance_rate, combo_rate, reflection_rate, 
                    mana, mana_regeneration_rate, damage_to_different_faction_rate, 
                    resistance_to_different_faction_rate, damage_to_same_faction_rate, resistance_to_same_faction_rate
                ) VALUES (
                    @user_id, @skill_id, @level, @experiment, @star, quality, @block, @quantity, @power, @health, @physical_attack, 
                    @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, 
                    @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage_rate, @critical_rate, 
                    @penetration_rate, @evasion_rate, @damage_absorption_rate, @vitality_regeneration_rate, @accuracy_rate, 
                    @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate, @combo_rate, @reflection_rate, 
                    @mana, @mana_regeneration_rate, @damage_to_different_faction_rate, 
                    @resistance_to_different_faction_rate, @damage_to_same_faction_rate, @resistance_to_same_faction_rate
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@skill_id", skills.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(skills.rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", 1);
                    command.Parameters.AddWithValue("@power", skills.power);
                    command.Parameters.AddWithValue("@health", skills.health);
                    command.Parameters.AddWithValue("@physical_attack", skills.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", skills.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", skills.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", skills.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", skills.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", skills.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", skills.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", skills.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", skills.mental_attack);
                    command.Parameters.AddWithValue("@mental_defense", skills.mental_defense);
                    command.Parameters.AddWithValue("@speed", skills.speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", skills.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", skills.critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", skills.penetration_rate);
                    command.Parameters.AddWithValue("@evasion_rate", skills.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", skills.damage_absorption_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", skills.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", skills.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", skills.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", skills.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", skills.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", skills.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", skills.combo_rate);
                    command.Parameters.AddWithValue("@reflection_rate", skills.reflection_rate);
                    command.Parameters.AddWithValue("@mana", skills.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", skills.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", skills.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", skills.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", skills.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", skills.resistance_to_same_faction_rate);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_skills
                    SET quantity = quantity + 1
                    WHERE user_id = @user_id AND skill_id = @skill_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@skill_id", skills.id);

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
    public bool UpdateSkillsLevel(Skills skills, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_skills
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
                WHERE user_id = @user_id AND skill_id = @skill_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@skill_id", skills.id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", skills.power);
                command.Parameters.AddWithValue("@health", skills.health);
                command.Parameters.AddWithValue("@physical_attack", skills.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", skills.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", skills.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", skills.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", skills.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", skills.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", skills.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", skills.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", skills.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", skills.mental_defense);
                command.Parameters.AddWithValue("@speed", skills.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", skills.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", skills.critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", skills.penetration_rate);
                command.Parameters.AddWithValue("@evasion_rate", skills.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", skills.damage_absorption_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", skills.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@accuracy_rate", skills.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", skills.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", skills.shield_strength);
                command.Parameters.AddWithValue("@tenacity", skills.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", skills.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", skills.combo_rate);
                command.Parameters.AddWithValue("@reflection_rate", skills.reflection_rate);
                command.Parameters.AddWithValue("@mana", skills.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", skills.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", skills.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", skills.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", skills.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", skills.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateSkillsBreakthrough(Skills skills, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_skills
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
                WHERE user_id = @user_id AND skill_id = @skill_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@skill_id", skills.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", skills.power);
                command.Parameters.AddWithValue("@health", skills.health);
                command.Parameters.AddWithValue("@physical_attack", skills.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", skills.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", skills.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", skills.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", skills.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", skills.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", skills.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", skills.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", skills.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", skills.mental_defense);
                command.Parameters.AddWithValue("@speed", skills.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", skills.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", skills.critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", skills.penetration_rate);
                command.Parameters.AddWithValue("@evasion_rate", skills.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", skills.damage_absorption_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", skills.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@accuracy_rate", skills.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", skills.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", skills.shield_strength);
                command.Parameters.AddWithValue("@tenacity", skills.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", skills.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", skills.combo_rate);
                command.Parameters.AddWithValue("@reflection_rate", skills.reflection_rate);
                command.Parameters.AddWithValue("@mana", skills.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", skills.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", skills.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", skills.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", skills.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", skills.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public Skills GetUserSkillsById(string user_id, string Id)
    {
        Skills card = new Skills();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_skills where skill_id=@id 
                and user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new Skills
                    {
                        id = reader.GetString("skills_id"),
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
}