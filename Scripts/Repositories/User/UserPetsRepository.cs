using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserPetsRepository : IUserPetsRepository
{
    public List<Pets> GetUserPets(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<Pets> petsList = new List<Pets>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT up.*, p.image, p.rare, p.type, p.description
                FROM user_pets up
                LEFT JOIN Pets p ON p.id = up.pet_id
                WHERE up.user_id = @userId AND p.type = @type AND (@rare = 'All' or p.rare = @rare)
                ORDER BY p.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(p.name, '[0-9]+$') AS UNSIGNED), p.name
                LIMIT @limit OFFSET @offset;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@rare", rare);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Pets pet = new Pets
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        Star = reader.GetInt32("star"),
                        TeamId = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetString("team_id"),
                        Position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString("position"),

                        Power = reader.GetDouble("power"),
                        Health = reader.GetDouble("health"),
                        PhysicalAttack = reader.GetDouble("physical_attack"),
                        PhysicalDefense = reader.GetDouble("physical_defense"),
                        MagicalAttack = reader.GetDouble("magical_attack"),
                        MagicalDefense = reader.GetDouble("magical_defense"),
                        ChemicalAttack = reader.GetDouble("chemical_attack"),
                        ChemicalDefense = reader.GetDouble("chemical_defense"),
                        AtomicAttack = reader.GetDouble("atomic_attack"),
                        AtomicDefense = reader.GetDouble("atomic_defense"),
                        MentalAttack = reader.GetDouble("mental_attack"),
                        MentalDefense = reader.GetDouble("mental_defense"),
                        Speed = reader.GetDouble("speed"),
                        CriticalDamageRate = reader.GetDouble("critical_damage_rate"),
                        CriticalRate = reader.GetDouble("critical_rate"),
                        CriticalResistanceRate = reader.GetDouble("critical_resistance_rate"),
                        IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate"),
                        PenetrationRate = reader.GetDouble("penetration_rate"),
                        PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate"),
                        EvasionRate = reader.GetDouble("evasion_rate"),
                        DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate"),
                        IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate"),
                        AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate"),
                        VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate"),
                        VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        AccuracyRate = reader.GetDouble("accuracy_rate"),
                        LifestealRate = reader.GetDouble("lifesteal_rate"),
                        ShieldStrength = reader.GetDouble("shield_strength"),
                        Tenacity = reader.GetDouble("tenacity"),
                        ResistanceRate = reader.GetDouble("resistance_rate"),
                        ComboRate = reader.GetDouble("combo_rate"),
                        IgnoreComboRate = reader.GetDouble("ignore_combo_rate"),
                        ComboDamageRate = reader.GetDouble("combo_damage_rate"),
                        ComboResistanceRate = reader.GetDouble("combo_resistance_rate"),
                        StunRate = reader.GetDouble("stun_rate"),
                        IgnoreStunRate = reader.GetDouble("ignore_stun_rate"),
                        ReflectionRate = reader.GetDouble("reflection_rate"),
                        IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate"),
                        ReflectionDamageRate = reader.GetDouble("reflection_damage_rate"),
                        ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate"),
                        Mana = reader.GetFloat("mana"),
                        ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                        DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                        ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                        DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                        ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                        NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                        NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                        SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                        SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                        Description = reader.GetString("description"),
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
                string query = @"SELECT up.*, p.image, p.rare, p.type, p.description
                FROM user_pets up
                LEFT JOIN Pets p ON p.id = up.pet_id
                WHERE up.user_id = @userId AND up.team_id = @team_id
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
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        Star = reader.GetInt32("star"),
                        TeamId = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetString("team_id"),
                        Position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString("position"),
                        Power = reader.GetDouble("power"),
                        Health = reader.GetDouble("health"),
                        PhysicalAttack = reader.GetDouble("physical_attack"),
                        PhysicalDefense = reader.GetDouble("physical_defense"),
                        MagicalAttack = reader.GetDouble("magical_attack"),
                        MagicalDefense = reader.GetDouble("magical_defense"),
                        ChemicalAttack = reader.GetDouble("chemical_attack"),
                        ChemicalDefense = reader.GetDouble("chemical_defense"),
                        AtomicAttack = reader.GetDouble("atomic_attack"),
                        AtomicDefense = reader.GetDouble("atomic_defense"),
                        MentalAttack = reader.GetDouble("mental_attack"),
                        MentalDefense = reader.GetDouble("mental_defense"),
                        Speed = reader.GetDouble("speed"),
                        CriticalDamageRate = reader.GetDouble("critical_damage_rate"),
                        CriticalRate = reader.GetDouble("critical_rate"),
                        CriticalResistanceRate = reader.GetDouble("critical_resistance_rate"),
                        IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate"),
                        PenetrationRate = reader.GetDouble("penetration_rate"),
                        PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate"),
                        EvasionRate = reader.GetDouble("evasion_rate"),
                        DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate"),
                        IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate"),
                        AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate"),
                        VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate"),
                        VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        AccuracyRate = reader.GetDouble("accuracy_rate"),
                        LifestealRate = reader.GetDouble("lifesteal_rate"),
                        ShieldStrength = reader.GetDouble("shield_strength"),
                        Tenacity = reader.GetDouble("tenacity"),
                        ResistanceRate = reader.GetDouble("resistance_rate"),
                        ComboRate = reader.GetDouble("combo_rate"),
                        IgnoreComboRate = reader.GetDouble("ignore_combo_rate"),
                        ComboDamageRate = reader.GetDouble("combo_damage_rate"),
                        ComboResistanceRate = reader.GetDouble("combo_resistance_rate"),
                        StunRate = reader.GetDouble("stun_rate"),
                        IgnoreStunRate = reader.GetDouble("ignore_stun_rate"),
                        ReflectionRate = reader.GetDouble("reflection_rate"),
                        IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate"),
                        ReflectionDamageRate = reader.GetDouble("reflection_damage_rate"),
                        ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate"),
                        Mana = reader.GetFloat("mana"),
                        ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                        DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                        ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                        DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                        ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                        NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                        NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                        SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                        SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                        Description = reader.GetString("description"),
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
            WHERE uc.user_id =@userId and uc.team_id=@team_id
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
    public int GetUserPetsCount(string user_id, string type, string rare)
    {
        int count = 0;
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select count(*) from Pets p, user_pets up 
                where p.id=up.pet_id and up.user_id=@userId and p.type= @type AND (@rare = 'All' or p.rare = @rare)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@rare", rare);
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

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_pets
                WHERE user_id = @user_id AND pet_id = @pet_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@pet_id", pets.Id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                    INSERT INTO user_pets(
                    user_id, pet_id, rare, level, experiment, star, quality, block, quantity,
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
                    @user_id, @pet_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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
                    command.Parameters.AddWithValue("@pet_id", pets.Id);
                    command.Parameters.AddWithValue("@rare", pets.Rare);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(pets.Rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", pets.Quantity);
                    command.Parameters.AddWithValue("@power", pets.Power);
                    command.Parameters.AddWithValue("@health", pets.Health);
                    command.Parameters.AddWithValue("@physical_attack", pets.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", pets.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", pets.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", pets.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", pets.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", pets.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", pets.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", pets.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", pets.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", pets.MentalDefense);
                    command.Parameters.AddWithValue("@speed", pets.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", pets.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", pets.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", pets.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", pets.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", pets.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", pets.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", pets.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", pets.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", pets.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", pets.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", pets.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", pets.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", pets.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", pets.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", pets.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", pets.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", pets.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", pets.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", pets.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", pets.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", pets.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", pets.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", pets.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", pets.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", pets.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", pets.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", pets.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", pets.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", pets.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", pets.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", pets.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", pets.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", pets.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", pets.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", pets.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", pets.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", pets.SkillResistanceRate);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_pets
                    SET quantity = @quantity
                    WHERE user_id = @user_id AND pet_id = @pet_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@pet_id", pets.Id);
                    updateCommand.Parameters.AddWithValue("@quantity", pets.Quantity);

                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
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
                command.Parameters.AddWithValue("@pet_id", pets.Id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", pets.Power);
                command.Parameters.AddWithValue("@health", pets.Health);
                command.Parameters.AddWithValue("@physical_attack", pets.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", pets.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", pets.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", pets.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", pets.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", pets.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", pets.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", pets.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", pets.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", pets.MentalDefense);
                command.Parameters.AddWithValue("@speed", pets.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", pets.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", pets.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", pets.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", pets.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", pets.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", pets.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", pets.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", pets.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", pets.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", pets.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", pets.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", pets.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", pets.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", pets.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", pets.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", pets.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", pets.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", pets.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", pets.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", pets.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", pets.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", pets.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", pets.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", pets.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", pets.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", pets.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", pets.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", pets.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", pets.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", pets.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", pets.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", pets.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", pets.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", pets.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", pets.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", pets.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", pets.SkillResistanceRate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
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
                command.Parameters.AddWithValue("@pet_id", pets.Id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", pets.Power);
                command.Parameters.AddWithValue("@health", pets.Health);
                command.Parameters.AddWithValue("@physical_attack", pets.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", pets.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", pets.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", pets.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", pets.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", pets.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", pets.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", pets.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", pets.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", pets.MentalDefense);
                command.Parameters.AddWithValue("@speed", pets.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", pets.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", pets.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", pets.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", pets.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", pets.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", pets.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", pets.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", pets.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", pets.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", pets.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", pets.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", pets.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", pets.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", pets.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", pets.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", pets.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", pets.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", pets.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", pets.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", pets.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", pets.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", pets.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", pets.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", pets.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", pets.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", pets.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", pets.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", pets.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", pets.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", pets.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", pets.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", pets.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", pets.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", pets.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", pets.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", pets.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", pets.SkillResistanceRate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
        }
        return true;
    }
    public bool UpdateTeamCardPets(string team_id, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                Update user_card_pets set team_id=@team_id where user_id=@user_id 
                and card_pet_id=@card_pet_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_pet_id", card_id);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
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
                        Id = reader.GetString("pet_id"),
                        Level = reader.GetInt32("level"),
                        Quality = reader.GetInt32("quality"),
                        Experiment = reader.GetInt32("experiment"),
                        Star = reader.GetInt32("star"),
                        Power = reader.GetDouble("power"),
                        Health = reader.GetDouble("health"),
                        PhysicalAttack = reader.GetDouble("physical_attack"),
                        PhysicalDefense = reader.GetDouble("physical_defense"),
                        MagicalAttack = reader.GetDouble("magical_attack"),
                        MagicalDefense = reader.GetDouble("magical_defense"),
                        ChemicalAttack = reader.GetDouble("chemical_attack"),
                        ChemicalDefense = reader.GetDouble("chemical_defense"),
                        AtomicAttack = reader.GetDouble("atomic_attack"),
                        AtomicDefense = reader.GetDouble("atomic_defense"),
                        MentalAttack = reader.GetDouble("mental_attack"),
                        MentalDefense = reader.GetDouble("mental_defense"),
                        Speed = reader.GetDouble("speed"),
                        CriticalDamageRate = reader.GetDouble("critical_damage_rate"),
                        CriticalRate = reader.GetDouble("critical_rate"),
                        CriticalResistanceRate = reader.GetDouble("critical_resistance_rate"),
                        IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate"),
                        PenetrationRate = reader.GetDouble("penetration_rate"),
                        PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate"),
                        EvasionRate = reader.GetDouble("evasion_rate"),
                        DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate"),
                        IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate"),
                        AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate"),
                        VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate"),
                        VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        AccuracyRate = reader.GetDouble("accuracy_rate"),
                        LifestealRate = reader.GetDouble("lifesteal_rate"),
                        ShieldStrength = reader.GetDouble("shield_strength"),
                        Tenacity = reader.GetDouble("tenacity"),
                        ResistanceRate = reader.GetDouble("resistance_rate"),
                        ComboRate = reader.GetDouble("combo_rate"),
                        IgnoreComboRate = reader.GetDouble("ignore_combo_rate"),
                        ComboDamageRate = reader.GetDouble("combo_damage_rate"),
                        ComboResistanceRate = reader.GetDouble("combo_resistance_rate"),
                        StunRate = reader.GetDouble("stun_rate"),
                        IgnoreStunRate = reader.GetDouble("ignore_stun_rate"),
                        ReflectionRate = reader.GetDouble("reflection_rate"),
                        IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate"),
                        ReflectionDamageRate = reader.GetDouble("reflection_damage_rate"),
                        ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate"),
                        Mana = reader.GetFloat("mana"),
                        ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                        DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                        ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                        DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                        ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                        NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                        NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                        SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                        SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
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
            string userQuery = @"SELECT uc.*, c.*
                FROM user_pets uc
                LEFT JOIN pets c ON uc.pet_id = c.id 
                WHERE uc.user_id = @user_id and uc.team_id IS NOT null";
            MySqlCommand command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Pets pet = new Pets
                {
                    Id = reader.GetString("pet_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetInt32("quality"),
                    Type = reader.GetString("type"),
                    Star = reader.GetInt32("star"),
                    Level = reader.GetInt32("level"),
                    Experiment = reader.GetInt32("experiment"),
                    Quantity = reader.GetInt32("quantity"),
                    TeamId = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetString("team_id"),
                    Position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString("position"),
                    Power = reader.GetDouble("power"),
                    Health = reader.GetDouble("health"),
                    PhysicalAttack = reader.GetDouble("physical_attack"),
                    PhysicalDefense = reader.GetDouble("physical_defense"),
                    MagicalAttack = reader.GetDouble("magical_attack"),
                    MagicalDefense = reader.GetDouble("magical_defense"),
                    ChemicalAttack = reader.GetDouble("chemical_attack"),
                    ChemicalDefense = reader.GetDouble("chemical_defense"),
                    AtomicAttack = reader.GetDouble("atomic_attack"),
                    AtomicDefense = reader.GetDouble("atomic_defense"),
                    MentalAttack = reader.GetDouble("mental_attack"),
                    MentalDefense = reader.GetDouble("mental_defense"),
                    Speed = reader.GetDouble("speed"),
                    CriticalDamageRate = reader.GetDouble("critical_damage_rate"),
                    CriticalRate = reader.GetDouble("critical_rate"),
                    CriticalResistanceRate = reader.GetDouble("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate"),
                    PenetrationRate = reader.GetDouble("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate"),
                    EvasionRate = reader.GetDouble("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDouble("accuracy_rate"),
                    LifestealRate = reader.GetDouble("lifesteal_rate"),
                    ShieldStrength = reader.GetDouble("shield_strength"),
                    Tenacity = reader.GetDouble("tenacity"),
                    ResistanceRate = reader.GetDouble("resistance_rate"),
                    ComboRate = reader.GetDouble("combo_rate"),
                    IgnoreComboRate = reader.GetDouble("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDouble("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDouble("combo_resistance_rate"),
                    StunRate = reader.GetDouble("stun_rate"),
                    IgnoreStunRate = reader.GetDouble("ignore_stun_rate"),
                    ReflectionRate = reader.GetDouble("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDouble("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate"),
                    Mana = reader.GetFloat("mana"),
                    ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                    Description = reader.GetString("description"),
                };

                pets.Add(pet);
            }
        }
        return pets;
    }
}