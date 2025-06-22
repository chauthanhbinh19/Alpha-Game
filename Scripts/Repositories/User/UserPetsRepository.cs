using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserPetsRepository : IUserPetsRepository
{
    public List<Pets> GetUserPets(string user_id, string type, int pageSize, int offset)
    {
        List<Pets> petsList = new List<Pets>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT up.*, p.image, p.rare, p.type, fp.*
                FROM user_pets up
                LEFT JOIN Pets p ON p.id = up.pet_id
                LEFT JOIN fact_pets fp ON fp.user_id = up.user_id AND fp.user_pet_id = up.pet_id
                WHERE up.user_id = @userId AND p.type = @type
                ORDER BY p.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(p.name, '[0-9]+$') AS UNSIGNED), p.name
                LIMIT @limit OFFSET @offset;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Pets pet = new Pets
                    {
                        id = reader.GetString("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        quality = reader.GetInt32("quality"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        team_id = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetString("team_id"),
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString("position"),

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
                        description = reader.GetString("description"),

                        all_power = reader.GetDouble("all_power"),
                        all_health = reader.GetDouble("all_health"),
                        all_physical_attack = reader.GetDouble("all_physical_attack"),
                        all_physical_defense = reader.GetDouble("all_physical_defense"),
                        all_magical_attack = reader.GetDouble("all_magical_attack"),
                        all_magical_defense = reader.GetDouble("all_magical_defense"),
                        all_chemical_attack = reader.GetDouble("all_chemical_attack"),
                        all_chemical_defense = reader.GetDouble("all_chemical_defense"),
                        all_atomic_attack = reader.GetDouble("all_atomic_attack"),
                        all_atomic_defense = reader.GetDouble("all_atomic_defense"),
                        all_mental_attack = reader.GetDouble("all_mental_attack"),
                        all_mental_defense = reader.GetDouble("all_mental_defense"),
                        all_speed = reader.GetDouble("all_speed"),
                        all_critical_damage_rate = reader.GetDouble("all_critical_damage_rate"),
                        all_critical_rate = reader.GetDouble("all_critical_rate"),
                        all_critical_resistance_rate = reader.GetDouble("all_critical_resistance_rate"),
                        all_ignore_critical_rate = reader.GetDouble("all_ignore_critical_rate"),
                        all_penetration_rate = reader.GetDouble("all_penetration_rate"),
                        all_penetration_resistance_rate = reader.GetDouble("all_penetration_resistance_rate"),
                        all_evasion_rate = reader.GetDouble("all_evasion_rate"),
                        all_damage_absorption_rate = reader.GetDouble("all_damage_absorption_rate"),
                        all_ignore_damage_absorption_rate = reader.GetDouble("all_ignore_damage_absorption_rate"),
                        all_absorbed_damage_rate = reader.GetDouble("all_absorbed_damage_rate"),
                        all_vitality_regeneration_rate = reader.GetDouble("all_vitality_regeneration_rate"),
                        all_vitality_regeneration_resistance_rate = reader.GetDouble("all_vitality_regeneration_resistance_rate"),
                        all_accuracy_rate = reader.GetDouble("all_accuracy_rate"),
                        all_lifesteal_rate = reader.GetDouble("all_lifesteal_rate"),
                        all_shield_strength = reader.GetDouble("all_shield_strength"),
                        all_tenacity = reader.GetDouble("all_tenacity"),
                        all_resistance_rate = reader.GetDouble("all_resistance_rate"),
                        all_combo_rate = reader.GetDouble("all_combo_rate"),
                        all_ignore_combo_rate = reader.GetDouble("all_ignore_combo_rate"),
                        all_combo_damage_rate = reader.GetDouble("all_combo_damage_rate"),
                        all_combo_resistance_rate = reader.GetDouble("all_combo_resistance_rate"),
                        all_stun_rate = reader.GetDouble("all_stun_rate"),
                        all_ignore_stun_rate = reader.GetDouble("all_ignore_stun_rate"),
                        all_reflection_rate = reader.GetDouble("all_reflection_rate"),
                        all_ignore_reflection_rate = reader.GetDouble("all_ignore_reflection_rate"),
                        all_reflection_damage_rate = reader.GetDouble("all_reflection_damage_rate"),
                        all_reflection_resistance_rate = reader.GetDouble("all_reflection_resistance_rate"),
                        all_mana = reader.GetFloat("all_mana"),
                        all_mana_regeneration_rate = reader.GetDouble("all_mana_regeneration_rate"),
                        all_damage_to_different_faction_rate = reader.GetDouble("all_damage_to_different_faction_rate"),
                        all_resistance_to_different_faction_rate = reader.GetDouble("all_resistance_to_different_faction_rate"),
                        all_damage_to_same_faction_rate = reader.GetDouble("all_damage_to_same_faction_rate"),
                        all_resistance_to_same_faction_rate = reader.GetDouble("all_resistance_to_same_faction_rate"),
                        all_normal_damage_rate = reader.GetDouble("all_normal_damage_rate"),
                        all_normal_resistance_rate = reader.GetDouble("all_normal_resistance_rate"),
                        all_skill_damage_rate = reader.GetDouble("all_skill_damage_rate"),
                        all_skill_resistance_rate = reader.GetDouble("all_skill_resistance_rate"),
                    };

                    petsList.Add(pet);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return petsList;
    }
    public List<Pets> GetUserPetsTeam(string user_id, string teamId)
    {
        List<Pets> petsList = new List<Pets>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT up.*, p.image, p.rare, p.type, fp.*
                FROM user_pets up
                LEFT JOIN Pets p ON p.id = up.pet_id
                LEFT JOIN fact_pets fp ON fp.user_id = up.user_id AND fp.user_pet_id = up.pet_id
                WHERE up.user_id = @userId AND fp.team_id = @team_id
                ORDER BY p.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(p.name, '[0-9]+$') AS UNSIGNED), p.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@team_id", teamId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Pets pet = new Pets
                    {
                        id = reader.GetString("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        quality = reader.GetInt32("quality"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        team_id = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetString("team_id"),
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString("position"),
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
                        description = reader.GetString("description"),

                        all_power = reader.GetDouble("all_power"),
                        all_health = reader.GetDouble("all_health"),
                        all_physical_attack = reader.GetDouble("all_physical_attack"),
                        all_physical_defense = reader.GetDouble("all_physical_defense"),
                        all_magical_attack = reader.GetDouble("all_magical_attack"),
                        all_magical_defense = reader.GetDouble("all_magical_defense"),
                        all_chemical_attack = reader.GetDouble("all_chemical_attack"),
                        all_chemical_defense = reader.GetDouble("all_chemical_defense"),
                        all_atomic_attack = reader.GetDouble("all_atomic_attack"),
                        all_atomic_defense = reader.GetDouble("all_atomic_defense"),
                        all_mental_attack = reader.GetDouble("all_mental_attack"),
                        all_mental_defense = reader.GetDouble("all_mental_defense"),
                        all_speed = reader.GetDouble("all_speed"),
                        all_critical_damage_rate = reader.GetDouble("all_critical_damage_rate"),
                        all_critical_rate = reader.GetDouble("all_critical_rate"),
                        all_critical_resistance_rate = reader.GetDouble("all_critical_resistance_rate"),
                        all_ignore_critical_rate = reader.GetDouble("all_ignore_critical_rate"),
                        all_penetration_rate = reader.GetDouble("all_penetration_rate"),
                        all_penetration_resistance_rate = reader.GetDouble("all_penetration_resistance_rate"),
                        all_evasion_rate = reader.GetDouble("all_evasion_rate"),
                        all_damage_absorption_rate = reader.GetDouble("all_damage_absorption_rate"),
                        all_ignore_damage_absorption_rate = reader.GetDouble("all_ignore_damage_absorption_rate"),
                        all_absorbed_damage_rate = reader.GetDouble("all_absorbed_damage_rate"),
                        all_vitality_regeneration_rate = reader.GetDouble("all_vitality_regeneration_rate"),
                        all_vitality_regeneration_resistance_rate = reader.GetDouble("all_vitality_regeneration_resistance_rate"),
                        all_accuracy_rate = reader.GetDouble("all_accuracy_rate"),
                        all_lifesteal_rate = reader.GetDouble("all_lifesteal_rate"),
                        all_shield_strength = reader.GetDouble("all_shield_strength"),
                        all_tenacity = reader.GetDouble("all_tenacity"),
                        all_resistance_rate = reader.GetDouble("all_resistance_rate"),
                        all_combo_rate = reader.GetDouble("all_combo_rate"),
                        all_ignore_combo_rate = reader.GetDouble("all_ignore_combo_rate"),
                        all_combo_damage_rate = reader.GetDouble("all_combo_damage_rate"),
                        all_combo_resistance_rate = reader.GetDouble("all_combo_resistance_rate"),
                        all_stun_rate = reader.GetDouble("all_stun_rate"),
                        all_ignore_stun_rate = reader.GetDouble("all_ignore_stun_rate"),
                        all_reflection_rate = reader.GetDouble("all_reflection_rate"),
                        all_ignore_reflection_rate = reader.GetDouble("all_ignore_reflection_rate"),
                        all_reflection_damage_rate = reader.GetDouble("all_reflection_damage_rate"),
                        all_reflection_resistance_rate = reader.GetDouble("all_reflection_resistance_rate"),
                        all_mana = reader.GetFloat("all_mana"),
                        all_mana_regeneration_rate = reader.GetDouble("all_mana_regeneration_rate"),
                        all_damage_to_different_faction_rate = reader.GetDouble("all_damage_to_different_faction_rate"),
                        all_resistance_to_different_faction_rate = reader.GetDouble("all_resistance_to_different_faction_rate"),
                        all_damage_to_same_faction_rate = reader.GetDouble("all_damage_to_same_faction_rate"),
                        all_resistance_to_same_faction_rate = reader.GetDouble("all_resistance_to_same_faction_rate"),
                        all_normal_damage_rate = reader.GetDouble("all_normal_damage_rate"),
                        all_normal_resistance_rate = reader.GetDouble("all_normal_resistance_rate"),
                        all_skill_damage_rate = reader.GetDouble("all_skill_damage_rate"),
                        all_skill_resistance_rate = reader.GetDouble("all_skill_resistance_rate"),
                    };

                    petsList.Add(pet);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return petsList;
    }
    public Dictionary<string, int> GetUniquePetTypesTeam(string teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = @"SELECT distinct c.type, count(c.type) as number
            FROM user_pets uc
            LEFT JOIN pets c ON uc.pet_id = c.id 
            LEFT JOIN fact_pets fch ON fch.user_id = uc.user_id AND fch.user_pet_id = uc.pet_id
            WHERE uc.user_id =@userId and fch.team_id=@team_id
            group by c.type, c.type";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", User.CurrentUserId);
            command.Parameters.AddWithValue("@team_id", teamId);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string type = reader["type"].ToString();
                int number = Convert.ToInt32(reader["number"]);

                result[type] = number;
            }
        }
        return result;
    }
    public int GetUserPetsCount(string user_id, string type)
    {
        int count = 0;
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from Pets p, user_pets up where p.id=up.pet_id and up.user_id=@userId and p.type= @type";
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
    public bool InsertUserPets(Pets pets)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                INSERT INTO user_pets (
                    user_id, pet_id, level, experiment, star, quality, block, quantity,
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
                    @user_id, @pet_id, @level, @experiment, @star, @quality, @block, @quantity,
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
                command.Parameters.AddWithValue("@pet_id", pets.id);
                command.Parameters.AddWithValue("@level", 0);
                command.Parameters.AddWithValue("@experiment", 0);
                command.Parameters.AddWithValue("@star", 0);
                command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(pets.rare));
                command.Parameters.AddWithValue("@block", false);
                command.Parameters.AddWithValue("@power", pets.power);
                command.Parameters.AddWithValue("@health", pets.health);
                command.Parameters.AddWithValue("@physical_attack", pets.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", pets.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", pets.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", pets.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", pets.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", pets.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", pets.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", pets.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", pets.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", pets.mental_defense);
                command.Parameters.AddWithValue("@speed", pets.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", pets.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", pets.critical_rate);
                command.Parameters.AddWithValue("@critical_resistance_rate", pets.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", pets.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", pets.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", pets.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", pets.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", pets.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", pets.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", pets.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", pets.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", pets.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", pets.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", pets.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", pets.shield_strength);
                command.Parameters.AddWithValue("@tenacity", pets.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", pets.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", pets.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", pets.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", pets.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", pets.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", pets.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", pets.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", pets.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", pets.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", pets.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", pets.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", pets.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", pets.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", pets.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", pets.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", pets.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", pets.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", pets.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", pets.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", pets.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", pets.skill_resistance_rate);
                command.ExecuteNonQuery();
                InsertFactPets(pets);
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
    public bool UpdatePetsLevel(Pets pets, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_pets
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
                WHERE user_id = @user_id AND pet_id = @pet_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@pet_id", pets.id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", pets.power);
                command.Parameters.AddWithValue("@health", pets.health);
                command.Parameters.AddWithValue("@physical_attack", pets.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", pets.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", pets.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", pets.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", pets.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", pets.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", pets.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", pets.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", pets.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", pets.mental_defense);
                command.Parameters.AddWithValue("@speed", pets.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", pets.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", pets.critical_rate);
                command.Parameters.AddWithValue("@critical_resistance_rate", pets.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", pets.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", pets.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", pets.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", pets.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", pets.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", pets.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", pets.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", pets.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", pets.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", pets.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", pets.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", pets.shield_strength);
                command.Parameters.AddWithValue("@tenacity", pets.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", pets.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", pets.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", pets.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", pets.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", pets.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", pets.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", pets.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", pets.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", pets.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", pets.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", pets.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", pets.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", pets.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", pets.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", pets.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", pets.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", pets.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", pets.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", pets.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", pets.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", pets.skill_resistance_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdatePetsBreakthrough(Pets pets, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_pets
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
                WHERE user_id = @user_id AND pet_id = @pet_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@pet_id", pets.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", pets.power);
                command.Parameters.AddWithValue("@health", pets.health);
                command.Parameters.AddWithValue("@physical_attack", pets.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", pets.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", pets.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", pets.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", pets.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", pets.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", pets.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", pets.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", pets.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", pets.mental_defense);
                command.Parameters.AddWithValue("@speed", pets.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", pets.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", pets.critical_rate);
                command.Parameters.AddWithValue("@critical_resistance_rate", pets.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", pets.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", pets.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", pets.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", pets.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", pets.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", pets.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", pets.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", pets.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", pets.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", pets.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", pets.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", pets.shield_strength);
                command.Parameters.AddWithValue("@tenacity", pets.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", pets.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", pets.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", pets.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", pets.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", pets.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", pets.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", pets.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", pets.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", pets.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", pets.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", pets.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", pets.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", pets.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", pets.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", pets.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", pets.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", pets.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", pets.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", pets.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", pets.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", pets.skill_resistance_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool InsertFactPets(Pets pets)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                INSERT INTO fact_pets (
                user_id, user_pet_id, team_id, position, role, 
                all_power, all_health, 
                all_physical_attack, all_physical_defense, 
                all_magical_attack, all_magical_defense, 
                all_chemical_attack, all_chemical_defense, 
                all_atomic_attack, all_atomic_defense, 
                all_mental_attack, all_mental_defense, 
                all_speed, 
                all_critical_damage_rate, all_critical_rate, all_critical_resistance_rate, all_ignore_critical_rate,
                all_penetration_rate, all_penetration_resistance_rate, 
                all_evasion_rate, 
                all_damage_absorption_rate, all_ignore_damage_absorption_rate, all_absorbed_damage_rate,
                all_vitality_regeneration_rate, all_vitality_regeneration_resistance_rate, 
                all_accuracy_rate, all_lifesteal_rate, 
                all_shield_strength, all_tenacity, all_resistance_rate, 
                all_combo_rate, all_ignore_combo_rate, all_combo_damage_rate, all_combo_resistance_rate,
                all_stun_rate, all_ignore_stun_rate,
                all_reflection_rate, all_ignore_reflection_rate, all_reflection_damage_rate, all_reflection_resistance_rate,
                all_mana, all_mana_regeneration_rate, 
                all_damage_to_different_faction_rate, all_resistance_to_different_faction_rate, 
                all_damage_to_same_faction_rate, all_resistance_to_same_faction_rate,
                all_normal_damage_rate, all_normal_resistance_rate,
                all_skill_damage_rate, all_skill_resistance_rate
            ) VALUES (
                @user_id, @user_pet_id, @team_id, @position, @role, 
                @all_power, @all_health, 
                @all_physical_attack, @all_physical_defense, 
                @all_magical_attack, @all_magical_defense, 
                @all_chemical_attack, @all_chemical_defense, 
                @all_atomic_attack, @all_atomic_defense, 
                @all_mental_attack, @all_mental_defense, 
                @all_speed, 
                @all_critical_damage_rate, @all_critical_rate, @all_critical_resistance_rate, @all_ignore_critical_rate,
                @all_penetration_rate, @all_penetration_resistance_rate, 
                @all_evasion_rate, 
                @all_damage_absorption_rate, @all_ignore_damage_absorption_rate, @all_absorbed_damage_rate,
                @all_vitality_regeneration_rate, @all_vitality_regeneration_resistance_rate, 
                @all_accuracy_rate, @all_lifesteal_rate, 
                @all_shield_strength, @all_tenacity, @all_resistance_rate, 
                @all_combo_rate, @all_ignore_combo_rate, @all_combo_damage_rate, @all_combo_resistance_rate,
                @all_stun_rate, @all_ignore_stun_rate,
                @all_reflection_rate, @all_ignore_reflection_rate, @all_reflection_damage_rate, @all_reflection_resistance_rate,
                @all_mana, @all_mana_regeneration_rate, 
                @all_damage_to_different_faction_rate, @all_resistance_to_different_faction_rate, 
                @all_damage_to_same_faction_rate, @all_resistance_to_same_faction_rate,
                @all_normal_damage_rate, @all_normal_resistance_rate,
                @all_skill_damage_rate, @all_skill_resistance_rate
            );";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_pet_id", pets.id);
                command.Parameters.AddWithValue("@team_id", null);
                command.Parameters.AddWithValue("@position", null);
                command.Parameters.AddWithValue("@role", null);
                command.Parameters.AddWithValue("@all_power", pets.power);
                command.Parameters.AddWithValue("@all_health", pets.health);
                command.Parameters.AddWithValue("@all_physical_attack", pets.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", pets.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", pets.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", pets.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", pets.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", pets.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", pets.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", pets.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", pets.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", pets.mental_defense);
                command.Parameters.AddWithValue("@all_speed", pets.speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", pets.critical_damage_rate);
                command.Parameters.AddWithValue("@all_critical_rate", pets.critical_rate);
                command.Parameters.AddWithValue("@all_critical_resistance_rate", pets.critical_resistance_rate);
                command.Parameters.AddWithValue("@all_ignore_critical_rate", pets.ignore_critical_rate);
                command.Parameters.AddWithValue("@all_penetration_rate", pets.penetration_rate);
                command.Parameters.AddWithValue("@all_penetration_resistance_rate", pets.penetration_resistance_rate);
                command.Parameters.AddWithValue("@all_evasion_rate", pets.evasion_rate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", pets.damage_absorption_rate);
                command.Parameters.AddWithValue("@all_ignore_damage_absorption_rate", pets.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@all_absorbed_damage_rate", pets.absorbed_damage_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", pets.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_resistance_rate", pets.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@all_accuracy_rate", pets.accuracy_rate);
                command.Parameters.AddWithValue("@all_lifesteal_rate", pets.lifesteal_rate);
                command.Parameters.AddWithValue("@all_shield_strength", pets.shield_strength);
                command.Parameters.AddWithValue("@all_tenacity", pets.tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", pets.resistance_rate);
                command.Parameters.AddWithValue("@all_combo_rate", pets.combo_rate);
                command.Parameters.AddWithValue("@all_ignore_combo_rate", pets.ignore_combo_rate);
                command.Parameters.AddWithValue("@all_combo_damage_rate", pets.combo_damage_rate);
                command.Parameters.AddWithValue("@all_combo_resistance_rate", pets.combo_resistance_rate);
                command.Parameters.AddWithValue("@all_stun_rate", pets.stun_rate);
                command.Parameters.AddWithValue("@all_ignore_stun_rate", pets.ignore_stun_rate);
                command.Parameters.AddWithValue("@all_reflection_rate", pets.reflection_rate);
                command.Parameters.AddWithValue("@all_ignore_reflection_rate", pets.ignore_reflection_rate);
                command.Parameters.AddWithValue("@all_reflection_damage_rate", pets.reflection_damage_rate);
                command.Parameters.AddWithValue("@all_reflection_resistance_rate", pets.reflection_resistance_rate);
                command.Parameters.AddWithValue("@all_mana", pets.mana);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", pets.mana_regeneration_rate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", pets.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", pets.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", pets.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", pets.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_normal_damage_rate", pets.normal_damage_rate);
                command.Parameters.AddWithValue("@all_normal_resistance_rate", pets.normal_resistance_rate);
                command.Parameters.AddWithValue("@all_skill_damage_rate", pets.skill_damage_rate);
                command.Parameters.AddWithValue("@all_skill_resistance_rate", pets.skill_resistance_rate);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateFactPets(Pets pets)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE fact_pets
                SET 
                    all_physical_attack = @all_physical_attack, all_physical_defense = @all_physical_defense, 
                    all_magical_attack = @all_magical_attack, all_magical_defense = @all_magical_defense, 
                    all_chemical_attack = @all_chemical_attack, all_chemical_defense = @all_chemical_defense, 
                    all_atomic_attack = @all_atomic_attack, all_atomic_defense = @all_atomic_defense, 
                    all_mental_attack = @all_mental_attack, all_mental_defense = @all_mental_defense, 
                    all_speed = @all_speed, all_critical_damage_rate = @all_critical_damage_rate, 
                    all_critical_rate = @all_critical_rate, all_critical_resistance_rate = @all_critical_resistance_rate, all_ignore_critical_rate = @all_ignore_critical_rate,
                    all_penetration_rate = @all_penetration_rate, all_penetration_resistance_rate = @all_penetration_resistance_rate,
                    all_evasion_rate = @all_evasion_rate, all_damage_absorption_rate = @all_damage_absorption_rate, 
                    all_ignore_damage_absorption_rate = @all_ignore_damage_absorption_rate, all_absorbed_damage_rate = @all_absorbed_damage_rate,
                    all_vitality_regeneration_rate = @all_vitality_regeneration_rate, all_vitality_regeneration_resistance_rate = @all_vitality_regeneration_resistance_rate, 
                    all_accuracy_rate = @all_accuracy_rate, all_lifesteal_rate = @all_lifesteal_rate, all_shield_strength = @all_shield_strength, 
                    all_tenacity = @all_tenacity, all_resistance_rate = @all_resistance_rate, 
                    all_combo_rate = @all_combo_rate, all_ignore_combo_rate = @all_ignore_combo_rate, all_combo_damage_rate = @all_combo_damage_rate, all_combo_resistance_rate = @all_combo_resistance_rate,
                    all_stun_rate = @all_stun_rate, all_ignore_stun_rate = @all_ignore_stun_rate,
                    all_reflection_rate = @all_reflection_rate, all_ignore_reflection_rate = @all_ignore_reflection_rate, 
                    all_reflection_damage_rate = @all_reflection_damage_rate, all_reflection_resistance_rate = @all_reflection_resistance_rate,
                    all_mana = @all_mana, all_mana_regeneration_rate = @all_mana_regeneration_rate, 
                    all_damage_to_different_faction_rate = @all_damage_to_different_faction_rate, 
                    all_resistance_to_different_faction_rate = @all_resistance_to_different_faction_rate, 
                    all_damage_to_same_faction_rate = @all_damage_to_same_faction_rate, 
                    all_resistance_to_same_faction_rate = @all_resistance_to_same_faction_rate,
                    all_normal_damage_rate = @all_normal_damage_rate, all_normal_resistance_rate = @all_normal_resistance_rate,
                    all_skill_damage_rate = @all_skill_damage_rate, all_skill_resistance_rate = @all_skill_resistance_rate
                WHERE user_id = @user_id AND user_pet_id = @user_pet_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_pet_id", pets.id);
                command.Parameters.AddWithValue("@all_power", pets.power);
                command.Parameters.AddWithValue("@all_health", pets.health);
                command.Parameters.AddWithValue("@all_physical_attack", pets.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", pets.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", pets.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", pets.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", pets.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", pets.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", pets.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", pets.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", pets.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", pets.mental_defense);
                command.Parameters.AddWithValue("@all_speed", pets.speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", pets.critical_damage_rate);
                command.Parameters.AddWithValue("@all_critical_rate", pets.critical_rate);
                command.Parameters.AddWithValue("@all_critical_resistance_rate", pets.critical_resistance_rate);
                command.Parameters.AddWithValue("@all_ignore_critical_rate", pets.ignore_critical_rate);
                command.Parameters.AddWithValue("@all_penetration_rate", pets.penetration_rate);
                command.Parameters.AddWithValue("@all_penetration_resistance_rate", pets.penetration_resistance_rate);
                command.Parameters.AddWithValue("@all_evasion_rate", pets.evasion_rate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", pets.damage_absorption_rate);
                command.Parameters.AddWithValue("@all_ignore_damage_absorption_rate", pets.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@all_absorbed_damage_rate", pets.absorbed_damage_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", pets.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_resistance_rate", pets.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@all_accuracy_rate", pets.accuracy_rate);
                command.Parameters.AddWithValue("@all_lifesteal_rate", pets.lifesteal_rate);
                command.Parameters.AddWithValue("@all_shield_strength", pets.shield_strength);
                command.Parameters.AddWithValue("@all_tenacity", pets.tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", pets.resistance_rate);
                command.Parameters.AddWithValue("@all_combo_rate", pets.combo_rate);
                command.Parameters.AddWithValue("@all_ignore_combo_rate", pets.ignore_combo_rate);
                command.Parameters.AddWithValue("@all_combo_damage_rate", pets.combo_damage_rate);
                command.Parameters.AddWithValue("@all_combo_resistance_rate", pets.combo_resistance_rate);
                command.Parameters.AddWithValue("@all_stun_rate", pets.stun_rate);
                command.Parameters.AddWithValue("@all_ignore_stun_rate", pets.ignore_stun_rate);
                command.Parameters.AddWithValue("@all_reflection_rate", pets.reflection_rate);
                command.Parameters.AddWithValue("@all_ignore_reflection_rate", pets.ignore_reflection_rate);
                command.Parameters.AddWithValue("@all_reflection_damage_rate", pets.reflection_damage_rate);
                command.Parameters.AddWithValue("@all_reflection_resistance_rate", pets.reflection_resistance_rate);
                command.Parameters.AddWithValue("@all_mana", pets.mana);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", pets.mana_regeneration_rate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", pets.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", pets.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", pets.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", pets.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_normal_damage_rate", pets.normal_damage_rate);
                command.Parameters.AddWithValue("@all_normal_resistance_rate", pets.normal_resistance_rate);
                command.Parameters.AddWithValue("@all_skill_damage_rate", pets.skill_damage_rate);
                command.Parameters.AddWithValue("@all_skill_resistance_rate", pets.skill_resistance_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateTeamFactCardPets(string team_id, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                Update fact_card_pets set team_id=@team_id where user_id=@user_id 
                and user_card_pet_id=@user_card_pet_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_pet_id", card_id);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public Pets GetUserPetsById(string user_id, string Id)
    {
        Pets card = new Pets();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_pets where user_pets.pet_id=@id 
                and user_pets.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new Pets
                    {
                        id = reader.GetString("pet_id"),
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
    public List<Pets> GetAllUserPetsInTeam(string user_id)
    {
        List<Pets> pets = new List<Pets>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = @"SELECT uc.*, c.*, fch.*
                FROM user_pets uc
                LEFT JOIN pets c ON uc.pet_id = c.id 
                LEFT JOIN fact_pets fch ON fch.user_id = uc.user_id AND fch.user_pet_id = uc.pet_id
                WHERE uc.user_id = @user_id and fch.team_id IS NOT null";
            MySqlCommand command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Pets pet = new Pets
                {
                    id = reader.GetString("pet_id"),
                    name = reader.GetString("name"),
                    image = reader.GetString("image"),
                    rare = reader.GetString("rare"),
                    quality = reader.GetInt32("quality"),
                    type = reader.GetString("type"),
                    star = reader.GetInt32("star"),
                    level = reader.GetInt32("level"),
                    experiment = reader.GetInt32("experiment"),
                    quantity = reader.GetInt32("quantity"),
                    team_id = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetString("team_id"),
                    position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString("position"),
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
                    description = reader.GetString("description"),

                    all_power = reader.GetDouble("all_power"),
                    all_health = reader.GetDouble("all_health"),
                    all_physical_attack = reader.GetDouble("all_physical_attack"),
                    all_physical_defense = reader.GetDouble("all_physical_defense"),
                    all_magical_attack = reader.GetDouble("all_magical_attack"),
                    all_magical_defense = reader.GetDouble("all_magical_defense"),
                    all_chemical_attack = reader.GetDouble("all_chemical_attack"),
                    all_chemical_defense = reader.GetDouble("all_chemical_defense"),
                    all_atomic_attack = reader.GetDouble("all_atomic_attack"),
                    all_atomic_defense = reader.GetDouble("all_atomic_defense"),
                    all_mental_attack = reader.GetDouble("all_mental_attack"),
                    all_mental_defense = reader.GetDouble("all_mental_defense"),
                    all_speed = reader.GetDouble("all_speed"),
                    all_critical_damage_rate = reader.GetDouble("all_critical_damage_rate"),
                    all_critical_rate = reader.GetDouble("all_critical_rate"),
                    all_critical_resistance_rate = reader.GetDouble("all_critical_resistance_rate"),
                    all_ignore_critical_rate = reader.GetDouble("all_ignore_critical_rate"),
                    all_penetration_rate = reader.GetDouble("all_penetration_rate"),
                    all_penetration_resistance_rate = reader.GetDouble("all_penetration_resistance_rate"),
                    all_evasion_rate = reader.GetDouble("all_evasion_rate"),
                    all_damage_absorption_rate = reader.GetDouble("all_damage_absorption_rate"),
                    all_ignore_damage_absorption_rate = reader.GetDouble("all_ignore_damage_absorption_rate"),
                    all_absorbed_damage_rate = reader.GetDouble("all_absorbed_damage_rate"),
                    all_vitality_regeneration_rate = reader.GetDouble("all_vitality_regeneration_rate"),
                    all_vitality_regeneration_resistance_rate = reader.GetDouble("all_vitality_regeneration_resistance_rate"),
                    all_accuracy_rate = reader.GetDouble("all_accuracy_rate"),
                    all_lifesteal_rate = reader.GetDouble("all_lifesteal_rate"),
                    all_shield_strength = reader.GetDouble("all_shield_strength"),
                    all_tenacity = reader.GetDouble("all_tenacity"),
                    all_resistance_rate = reader.GetDouble("all_resistance_rate"),
                    all_combo_rate = reader.GetDouble("all_combo_rate"),
                    all_ignore_combo_rate = reader.GetDouble("all_ignore_combo_rate"),
                    all_combo_damage_rate = reader.GetDouble("all_combo_damage_rate"),
                    all_combo_resistance_rate = reader.GetDouble("all_combo_resistance_rate"),
                    all_stun_rate = reader.GetDouble("all_stun_rate"),
                    all_ignore_stun_rate = reader.GetDouble("all_ignore_stun_rate"),
                    all_reflection_rate = reader.GetDouble("all_reflection_rate"),
                    all_ignore_reflection_rate = reader.GetDouble("all_ignore_reflection_rate"),
                    all_reflection_damage_rate = reader.GetDouble("all_reflection_damage_rate"),
                    all_reflection_resistance_rate = reader.GetDouble("all_reflection_resistance_rate"),
                    all_mana = reader.GetFloat("all_mana"),
                    all_mana_regeneration_rate = reader.GetDouble("all_mana_regeneration_rate"),
                    all_damage_to_different_faction_rate = reader.GetDouble("all_damage_to_different_faction_rate"),
                    all_resistance_to_different_faction_rate = reader.GetDouble("all_resistance_to_different_faction_rate"),
                    all_damage_to_same_faction_rate = reader.GetDouble("all_damage_to_same_faction_rate"),
                    all_resistance_to_same_faction_rate = reader.GetDouble("all_resistance_to_same_faction_rate"),
                    all_normal_damage_rate = reader.GetDouble("all_normal_damage_rate"),
                    all_normal_resistance_rate = reader.GetDouble("all_normal_resistance_rate"),
                    all_skill_damage_rate = reader.GetDouble("all_skill_damage_rate"),
                    all_skill_resistance_rate = reader.GetDouble("all_skill_resistance_rate"),
                };

                pets.Add(pet);
            }
        }
        return pets;
    }
}