using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class Skills
{
    public int id { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public string rare { get; set; }
    public string type { get; set; }
    public int star { get; set; }
    public int quantity { get; set; }
    public int level { get; set; }
    public int experiment { get; set; }
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
    public Currency currency { get; set; }
    public Skills()
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
    public Skills GetNewLevelPower(Skills c, double coefficient)
    {
        Skills orginCard = new Skills();
        orginCard = orginCard.GetSkillsById(c.id);
        Skills skills = new Skills
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
        skills.power = 0.5 * (
            skills.health +
            skills.physical_attack +
            skills.physical_defense +
            skills.magical_attack +
            skills.magical_defense +
            skills.chemical_attack +
            skills.chemical_defense +
            skills.atomic_attack +
            skills.atomic_defense +
            skills.mental_attack +
            skills.mental_defense +
            skills.speed +
            skills.critical_damage +
            skills.critical_rate +
            skills.armor_penetration +
            skills.avoid +
            skills.absorbs_damage +
            skills.regenerate_vitality +
            skills.accuracy +
            skills.mana
        );
        return skills;
    }
    public Skills GetNewBreakthroughPower(Skills c, double coefficient)
    {
        Skills orginCard = new Skills();
        orginCard = orginCard.GetSkillsById(c.id);
        Skills skills = new Skills
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
        skills.power = 0.5 * (
            skills.health +
            skills.physical_attack +
            skills.physical_defense +
            skills.magical_attack +
            skills.magical_defense +
            skills.chemical_attack +
            skills.chemical_defense +
            skills.atomic_attack +
            skills.atomic_defense +
            skills.mental_attack +
            skills.mental_defense +
            skills.speed +
            skills.critical_damage +
            skills.critical_rate +
            skills.armor_penetration +
            skills.avoid +
            skills.absorbs_damage +
            skills.regenerate_vitality +
            skills.accuracy +
            skills.mana
        );
        return skills;
    }
    public static List<string> GetUniqueSkillsTypes()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "Select distinct type from Skills";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                typeList.Add(reader.GetString(0));
            }
        }
        return typeList;
    }
    public List<Skills> GetSkills(string type,int pageSize, int offset)
    {
        List<Skills> skillsList = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from Skills where type= @type 
                ORDER BY Skills.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(Skills.name, '[0-9]+$') AS UNSIGNED), Skills.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Skills skill = new Skills
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
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
    public int GetSkillsCount(string type){
        int count =0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from skills where type= @type";
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
    public List<Skills> GetSkillsCollection(string type,int pageSize, int offset)
    {
        List<Skills> skillsList = new List<Skills>();
        int user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT s.*, CASE WHEN sg.skill_id IS NULL THEN 'block' WHEN sg.status = 'pending' THEN 'pending' WHEN sg.status = 'available' THEN 'available' END AS status 
                FROM skills s LEFT JOIN skills_gallery sg ON s.id = sg.skill_id and sg.user_id = @userId where s.type=@type 
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
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
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
    public List<Skills> GetUserSkills(string type,int pageSize, int offset)
    {
        List<Skills> skillsList = new List<Skills>();
        int user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select us.*, s.image, s.rare, s.type from Skills s,user_skills us where s.id=us.skill_id and us.user_id=@userId and s.type= @type 
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
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
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
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        mana = reader.GetFloat("mana"),
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
    public int GetUserSkillsCount(string type){
        int count =0;
        int user_id=User.CurrentUserId;
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
                    user_id, skill_id, level, experiment, star, quantity, power, health, physical_attack, 
                    physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, 
                    atomic_defense, mental_attack, mental_defense, speed, critical_damage, critical_rate, 
                    armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana
                ) VALUES (
                    @user_id, @skill_id, @level, @experiment, @star, @quantity, @power, @health, @physical_attack, 
                    @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, 
                    @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage, @critical_rate, 
                    @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@skill_id", skills.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
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
                    command.Parameters.AddWithValue("@critical_damage", skills.critical_damage);
                    command.Parameters.AddWithValue("@critical_rate", skills.critical_rate);
                    command.Parameters.AddWithValue("@armor_penetration", skills.armor_penetration);
                    command.Parameters.AddWithValue("@avoid", skills.avoid);
                    command.Parameters.AddWithValue("@absorbs_damage", skills.absorbs_damage);
                    command.Parameters.AddWithValue("@regenerate_vitality", skills.regenerate_vitality);
                    command.Parameters.AddWithValue("@accuracy", skills.accuracy);
                    command.Parameters.AddWithValue("@mana", skills.mana);
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
                    user_id = @user_id AND skill_id = @skill_id;;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@skill_id", skills.id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", skills.power);
                command.Parameters.AddWithValue("@health", skills.health);
                command.Parameters.AddWithValue("@physicalAttack", skills.physical_attack);
                command.Parameters.AddWithValue("@physicalDefense", skills.physical_defense);
                command.Parameters.AddWithValue("@magicalAttack", skills.magical_attack);
                command.Parameters.AddWithValue("@magicalDefense", skills.magical_defense);
                command.Parameters.AddWithValue("@chemicalAttack", skills.chemical_attack);
                command.Parameters.AddWithValue("@chemicalDefense", skills.chemical_defense);
                command.Parameters.AddWithValue("@atomicAttack", skills.atomic_attack);
                command.Parameters.AddWithValue("@atomicDefense", skills.atomic_defense);
                command.Parameters.AddWithValue("@mentalAttack", skills.mental_attack);
                command.Parameters.AddWithValue("@mentalDefense", skills.mental_defense);
                command.Parameters.AddWithValue("@speed", skills.speed);
                command.Parameters.AddWithValue("@criticalDamage", skills.critical_damage);
                command.Parameters.AddWithValue("@criticalRate", skills.critical_rate);
                command.Parameters.AddWithValue("@armorPenetration", skills.armor_penetration);
                command.Parameters.AddWithValue("@avoid", skills.avoid);
                command.Parameters.AddWithValue("@absorbsDamage", skills.absorbs_damage);
                command.Parameters.AddWithValue("@regenerateVitality", skills.regenerate_vitality);
                command.Parameters.AddWithValue("@accuracy", skills.accuracy);
                command.Parameters.AddWithValue("@mana", skills.mana);
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
                    user_id = @user_id AND skill_id = @skill_id;;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@skill_id", skills.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", skills.power);
                command.Parameters.AddWithValue("@health", skills.health);
                command.Parameters.AddWithValue("@physicalAttack", skills.physical_attack);
                command.Parameters.AddWithValue("@physicalDefense", skills.physical_defense);
                command.Parameters.AddWithValue("@magicalAttack", skills.magical_attack);
                command.Parameters.AddWithValue("@magicalDefense", skills.magical_defense);
                command.Parameters.AddWithValue("@chemicalAttack", skills.chemical_attack);
                command.Parameters.AddWithValue("@chemicalDefense", skills.chemical_defense);
                command.Parameters.AddWithValue("@atomicAttack", skills.atomic_attack);
                command.Parameters.AddWithValue("@atomicDefense", skills.atomic_defense);
                command.Parameters.AddWithValue("@mentalAttack", skills.mental_attack);
                command.Parameters.AddWithValue("@mentalDefense", skills.mental_defense);
                command.Parameters.AddWithValue("@speed", skills.speed);
                command.Parameters.AddWithValue("@criticalDamage", skills.critical_damage);
                command.Parameters.AddWithValue("@criticalRate", skills.critical_rate);
                command.Parameters.AddWithValue("@armorPenetration", skills.armor_penetration);
                command.Parameters.AddWithValue("@avoid", skills.avoid);
                command.Parameters.AddWithValue("@absorbsDamage", skills.absorbs_damage);
                command.Parameters.AddWithValue("@regenerateVitality", skills.regenerate_vitality);
                command.Parameters.AddWithValue("@accuracy", skills.accuracy);
                command.Parameters.AddWithValue("@mana", skills.mana);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public List<Skills> GetSkillsWithPrice(string type,int pageSize, int offset)
    {
        List<Skills> skillsList = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select s.*, st.price, cu.image as currency_image, cu.id as currency_id
                from skills s, skill_trade st, currency cu
                where s.id=st.skill_id and st.currency_id = cu.id and s.type =@type
                ORDER BY s.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Skills skill = new Skills
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
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
                    skill.currency = new Currency{
                        id = reader.GetInt32("currency_id"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("price")
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
    public int GetSkillsWithPriceCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*)
                from skills s, skill_trade st, currency cu
                where s.id=st.skill_id and st.currency_id = cu.id and s.type =@type;";
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
    public Skills GetSkillsById(int Id)
    {
        Skills skill = new Skills();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from skills where id=@id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    skill = new Skills
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
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
        return skill;
    }
    public Skills GetUserSkillsById(int Id)
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
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new Skills
                    {
                        id = reader.GetInt32("skills_id"),
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
    public void InsertSkillsGallery(int Id)
    {
        Skills skillFromDB = GetSkillsById(Id);
        int percent = 0;
        if (skillFromDB.rare.Equals("LG"))
        {
            percent = 20;
        }
        else if (skillFromDB.rare.Equals("UR"))
        {
            percent = 10;
        }
        else if (skillFromDB.rare.Equals("SSR"))
        {
            percent = 5;
        }
        else if (skillFromDB.rare.Equals("SR"))
        {
            percent = 2;
        }
        else if (skillFromDB.rare.Equals("MR"))
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
                FROM skills_gallery 
                WHERE user_id = @user_id AND skill_id = @skill_id;
                ";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@skill_id", Id);

                int recordCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (recordCount == 0)
                {
                    string query = @"
                    INSERT INTO skills_gallery (
                        user_id, skill_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
                        magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                        mental_attack, mental_defense, speed, critical_damage, critical_rate, armor_penetration, avoid, 
                        absorbs_damage, regenerate_vitality, accuracy, mana, percent_all_health, percent_all_physical_attack, 
                        percent_all_physical_defense, percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, percent_all_mental_attack, 
                        percent_all_mental_defense
                    ) VALUES (
                        @user_id, @skill_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
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
                    command.Parameters.AddWithValue("@skill_id", Id);
                    command.Parameters.AddWithValue("@status", "pending");
                    command.Parameters.AddWithValue("@current_star", 0);
                    command.Parameters.AddWithValue("@temp_star", 0);
                    command.Parameters.AddWithValue("@power", skillFromDB.power);
                    command.Parameters.AddWithValue("@health", skillFromDB.health);
                    command.Parameters.AddWithValue("@physical_attack", skillFromDB.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", skillFromDB.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", skillFromDB.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", skillFromDB.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", skillFromDB.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", skillFromDB.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", skillFromDB.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", skillFromDB.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", skillFromDB.magical_attack);
                    command.Parameters.AddWithValue("@mental_defense", skillFromDB.magical_defense);
                    command.Parameters.AddWithValue("@speed", skillFromDB.speed);
                    command.Parameters.AddWithValue("@critical_damage", skillFromDB.critical_damage);
                    command.Parameters.AddWithValue("@critical_rate", skillFromDB.critical_rate);
                    command.Parameters.AddWithValue("@armor_penetration", skillFromDB.armor_penetration);
                    command.Parameters.AddWithValue("@avoid", skillFromDB.avoid);
                    command.Parameters.AddWithValue("@absorbs_damage", skillFromDB.absorbs_damage);
                    command.Parameters.AddWithValue("@regenerate_vitality", skillFromDB.regenerate_vitality);
                    command.Parameters.AddWithValue("@accuracy", skillFromDB.accuracy);
                    command.Parameters.AddWithValue("@mana", skillFromDB.mana);
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
    public void UpdateStatusSkillsGallery(int Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "update skills_gallery set status=@status where user_id=@user_id and skill_id=@skill_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@skill_id", Id);
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
    public Skills SumPowerSkillsGallery()
    {
        Skills sumSkills = new Skills();
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
                FROM skills_gallery where user_id=@user_id and status = 'available';";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumSkills.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumSkills.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumSkills.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumSkills.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumSkills.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumSkills.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumSkills.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumSkills.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumSkills.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumSkills.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumSkills.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumSkills.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumSkills.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumSkills.critical_damage = reader.IsDBNull(reader.GetOrdinal("total_critical_damage")) ? 0 : reader.GetDouble("total_critical_damage");
                        sumSkills.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumSkills.armor_penetration = reader.IsDBNull(reader.GetOrdinal("total_armor_penetration")) ? 0 : reader.GetDouble("total_armor_penetration");
                        sumSkills.avoid = reader.IsDBNull(reader.GetOrdinal("total_avoid")) ? 0 : reader.GetDouble("total_avoid");
                        sumSkills.absorbs_damage = reader.IsDBNull(reader.GetOrdinal("total_absorbs_damage")) ? 0 : reader.GetDouble("total_absorbs_damage");
                        sumSkills.regenerate_vitality = reader.IsDBNull(reader.GetOrdinal("total_regenerate_vitality")) ? 0 : reader.GetDouble("total_regenerate_vitality");
                        sumSkills.accuracy = reader.IsDBNull(reader.GetOrdinal("total_accuracy")) ? 0 : reader.GetDouble("total_accuracy");
                        sumSkills.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetInt32("total_mana");
                        sumSkills.percent_all_health = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDouble("total_percent_all_health");
                        sumSkills.percent_all_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDouble("total_percent_all_physical_attack");
                        sumSkills.percent_all_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDouble("total_percent_all_physical_defense");
                        sumSkills.percent_all_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDouble("total_percent_all_magical_attack");
                        sumSkills.percent_all_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDouble("total_percent_all_magical_defense");
                        sumSkills.percent_all_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDouble("total_percent_all_chemical_attack");
                        sumSkills.percent_all_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDouble("total_percent_all_chemical_defense");
                        sumSkills.percent_all_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDouble("total_percent_all_atomic_attack");
                        sumSkills.percent_all_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDouble("total_percent_all_atomic_defense");
                        sumSkills.percent_all_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDouble("total_percent_all_mental_attack");
                        sumSkills.percent_all_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDouble("total_percent_all_mental_defense");
                    }
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return sumSkills;
    }
}
