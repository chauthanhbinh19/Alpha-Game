using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
using System.Linq;

public class UserSkillsRepository : IUserSkillsRepository
{
    public List<Skills> GetUserSkills(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<Skills> skillsList = new List<Skills>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description 
                from Skills s, user_skills us
                where s.id=us.skill_id and us.user_id=@userId and s.type= @type AND (@rare = 'All' or s.rare = @rare)
                ORDER BY s.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@rare", rare);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetString("skill_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        Star = reader.GetInt32("star"),
                        Level = reader.GetInt32("level"),
                        SkillType = reader.GetString("skill_type"),
                        Experiment = reader.GetInt32("experiment"),
                        Quantity = reader.GetInt32("quantity"),
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
                        Description = reader.GetString("description")
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
    public int GetUserSkillsCount(string user_id, string type, string rare)
    {
        int count = 0;
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select count(*) from skills s, user_skills us 
                where s.id=us.skill_id and us.user_id=@userId and s.type= @type AND (@rare = 'All' or s.rare = @rare)";
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
                checkCommand.Parameters.AddWithValue("@skill_id", skills.Id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_skills (
                    user_id, skill_id, rare, level, experiment, star, quality, block, quantity,
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
                    @user_id, @skill_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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
                    command.Parameters.AddWithValue("@skill_id", skills.Id);
                    command.Parameters.AddWithValue("@rare", skills.Rare);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(skills.Rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", skills.Quantity);
                    command.Parameters.AddWithValue("@power", skills.Power);
                    command.Parameters.AddWithValue("@health", skills.Health);
                    command.Parameters.AddWithValue("@physical_attack", skills.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", skills.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", skills.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", skills.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", skills.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", skills.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", skills.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", skills.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", skills.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", skills.MentalDefense);
                    command.Parameters.AddWithValue("@speed", skills.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", skills.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", skills.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", skills.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", skills.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", skills.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", skills.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", skills.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", skills.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", skills.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", skills.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", skills.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", skills.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", skills.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", skills.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", skills.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", skills.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", skills.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", skills.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", skills.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", skills.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", skills.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", skills.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", skills.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", skills.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", skills.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", skills.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", skills.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", skills.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", skills.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", skills.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", skills.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", skills.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", skills.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", skills.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", skills.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", skills.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", skills.SkillResistanceRate);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_skills
                    SET quantity = @quantity
                    WHERE user_id = @user_id AND skill_id = @skill_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@skill_id", skills.Id);
                    updateCommand.Parameters.AddWithValue("@quantity", skills.Quantity);

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
                WHERE user_id = @user_id AND skill_id = @skill_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@skill_id", skills.Id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", skills.Power);
                command.Parameters.AddWithValue("@health", skills.Health);
                command.Parameters.AddWithValue("@physical_attack", skills.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", skills.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", skills.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", skills.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", skills.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", skills.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", skills.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", skills.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", skills.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", skills.MentalDefense);
                command.Parameters.AddWithValue("@speed", skills.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", skills.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", skills.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", skills.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", skills.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", skills.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", skills.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", skills.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", skills.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", skills.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", skills.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", skills.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", skills.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", skills.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", skills.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", skills.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", skills.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", skills.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", skills.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", skills.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", skills.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", skills.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", skills.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", skills.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", skills.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", skills.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", skills.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", skills.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", skills.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", skills.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", skills.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", skills.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", skills.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", skills.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", skills.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", skills.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", skills.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", skills.SkillResistanceRate);
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
                WHERE user_id = @user_id AND skill_id = @skill_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@skill_id", skills.Id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", skills.Power);
                command.Parameters.AddWithValue("@health", skills.Health);
                command.Parameters.AddWithValue("@physical_attack", skills.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", skills.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", skills.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", skills.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", skills.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", skills.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", skills.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", skills.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", skills.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", skills.MentalDefense);
                command.Parameters.AddWithValue("@speed", skills.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", skills.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", skills.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", skills.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", skills.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", skills.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", skills.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", skills.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", skills.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", skills.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", skills.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", skills.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", skills.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", skills.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", skills.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", skills.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", skills.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", skills.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", skills.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", skills.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", skills.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", skills.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", skills.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", skills.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", skills.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", skills.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", skills.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", skills.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", skills.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", skills.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", skills.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", skills.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", skills.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", skills.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", skills.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", skills.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", skills.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", skills.SkillResistanceRate);
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
                        Id = reader.GetString("skills_id"),
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
    public List<Skills> GetUserCardHeroesSkills(string user_id, string cardId)
    {
        List<Skills> skillsList = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, IFNULL(chs.position, 0) AS position
                from Skills s, user_skills us left join card_heroes_skills chs
                on chs.card_hero_id = @card_hero_id AND chs.skill_id = us.skill_id
                where s.id=us.skill_id AND us.user_id=@userId
                ORDER BY s.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@card_hero_id", cardId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetString("skill_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        Star = reader.GetInt32("star"),
                        Level = reader.GetInt32("level"),
                        Position = reader.GetInt32("position"),
                        SkillType = reader.GetString("skill_type"),
                        Experiment = reader.GetInt32("experiment"),
                        Quantity = reader.GetInt32("quantity"),
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
                        Description = reader.GetString("description")
                    };

                    skillsList.Add(skill);
                }
                skillsList = LoadSkillsWithEffects(user_id,skillsList, connection);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return skillsList;
    }
    public List<Skills> GetUserCardCaptainsSkills(string user_id, string cardId)
    {
        List<Skills> skillsList = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, IFNULL(chs.position, 0) AS position
                from Skills s, user_skills us left join card_captains_skills chs
                on chs.card_captain_id = @card_captain_id AND chs.skill_id = us.skill_id
                where s.id=us.skill_id AND us.user_id=@userId
                ORDER BY s.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@card_captain_id", cardId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetString("skill_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        Star = reader.GetInt32("star"),
                        Level = reader.GetInt32("level"),
                        Position = reader.GetInt32("position"),
                        SkillType = reader.GetString("skill_type"),
                        Experiment = reader.GetInt32("experiment"),
                        Quantity = reader.GetInt32("quantity"),
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
                        Description = reader.GetString("description")
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
    public List<Skills> GetUserCardColonelsSkills(string user_id, string cardId)
    {
        List<Skills> skillsList = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, IFNULL(chs.position, 0) AS position
                from Skills s, user_skills us left join card_colonels_skills chs
                on chs.card_colonel_id = @card_colonel_id AND chs.skill_id = us.skill_id
                where s.id=us.skill_id AND us.user_id=@userId
                ORDER BY s.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@card_colonel_id", cardId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetString("skill_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        Star = reader.GetInt32("star"),
                        Level = reader.GetInt32("level"),
                        Position = reader.GetInt32("position"),
                        SkillType = reader.GetString("skill_type"),
                        Experiment = reader.GetInt32("experiment"),
                        Quantity = reader.GetInt32("quantity"),
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
                        Description = reader.GetString("description")
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
    public List<Skills> GetUserCardGeneralsSkills(string user_id, string cardId)
    {
        List<Skills> skillsList = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, IFNULL(chs.position, 0) AS position
                from Skills s, user_skills us left join card_generals_skills chs
                on chs.card_general_id = @card_general_id AND chs.skill_id = us.skill_id
                where s.id=us.skill_id AND us.user_id=@userId
                ORDER BY s.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@card_general_id", cardId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetString("skill_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        Star = reader.GetInt32("star"),
                        Level = reader.GetInt32("level"),
                        Position = reader.GetInt32("position"),
                        SkillType = reader.GetString("skill_type"),
                        Experiment = reader.GetInt32("experiment"),
                        Quantity = reader.GetInt32("quantity"),
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
                        Description = reader.GetString("description")
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
    public List<Skills> GetUserCardAdmiralsSkills(string user_id, string cardId)
    {
        List<Skills> skillsList = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, IFNULL(chs.position, 0) AS position
                from Skills s, user_skills us left join card_admirals_skills chs
                on chs.card_admiral_id = @card_admiral_id AND chs.skill_id = us.skill_id
                where s.id=us.skill_id AND us.user_id=@userId
                ORDER BY s.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@card_admiral_id", cardId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetString("skill_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        Star = reader.GetInt32("star"),
                        Level = reader.GetInt32("level"),
                        Position = reader.GetInt32("position"),
                        SkillType = reader.GetString("skill_type"),
                        Experiment = reader.GetInt32("experiment"),
                        Quantity = reader.GetInt32("quantity"),
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
                        Description = reader.GetString("description")
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
    public List<Skills> GetUserCardMilitarySkills(string user_id, string cardId)
    {
        List<Skills> skillsList = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, IFNULL(chs.position, 0) AS position
                from Skills s, user_skills us left join card_military_skills chs
                on chs.card_military_id = @card_military_id AND chs.skill_id = us.skill_id
                where s.id=us.skill_id AND us.user_id=@userId
                ORDER BY s.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@card_military_id", cardId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetString("skill_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        Star = reader.GetInt32("star"),
                        Level = reader.GetInt32("level"),
                        Position = reader.GetInt32("position"),
                        SkillType = reader.GetString("skill_type"),
                        Experiment = reader.GetInt32("experiment"),
                        Quantity = reader.GetInt32("quantity"),
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
                        Description = reader.GetString("description")
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
    public List<Skills> GetUserCardMonstersSkills(string user_id, string cardId)
    {
        List<Skills> skillsList = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, IFNULL(chs.position, 0) AS position
                from Skills s, user_skills us left join card_monsters_skills chs
                on chs.card_monster_id = @card_monster_id AND chs.skill_id = us.skill_id
                where s.id=us.skill_id AND us.user_id=@userId
                ORDER BY s.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@card_monster_id", cardId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetString("skill_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        Star = reader.GetInt32("star"),
                        Level = reader.GetInt32("level"),
                        Position = reader.GetInt32("position"),
                        SkillType = reader.GetString("skill_type"),
                        Experiment = reader.GetInt32("experiment"),
                        Quantity = reader.GetInt32("quantity"),
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
                        Description = reader.GetString("description")
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
    public List<Skills> GetUserCardSpellSkills(string user_id, string cardId)
    {
        List<Skills> skillsList = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, IFNULL(chs.position, 0) AS position
                from Skills s, user_skills us left join card_spell_skills chs 
                on chs.card_spell_id = @card_spell_id AND chs.skill_id = us.skill_id
                where s.id=us.skill_id AND us.user_id=@userId
                ORDER BY s.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@card_spell_id", cardId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetString("skill_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        Star = reader.GetInt32("star"),
                        Level = reader.GetInt32("level"),
                        Position = reader.GetInt32("position"),
                        SkillType = reader.GetString("skill_type"),
                        Experiment = reader.GetInt32("experiment"),
                        Quantity = reader.GetInt32("quantity"),
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
                        Description = reader.GetString("description")
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
    public List<Skills> LoadSkillsWithEffects(string userId, List<Skills> skillsList, MySqlConnection connection)
    {
        // Giả định đây là hàm bạn đang làm việc.

        // ------------------------------------------------------------------------
        // BƯỚC 2: Tối ưu hóa - Load Effects cho TẤT CẢ Skills trong một truy vấn
        // ------------------------------------------------------------------------

        // Lấy tất cả Skill IDs vào một danh sách để tạo mệnh đề WHERE IN
        var skillIds = skillsList.Select(s => s.Id).ToList();
        if (!skillIds.Any()) return skillsList; // Thoát nếu không có kỹ năng

        // Chuyển danh sách ID sang chuỗi: ('id1', 'id2', 'id3', ...)
        string skillIdInClause = string.Join(",", skillIds.Select(id => $"'{id}'"));

        string combinedQuery = $@"
        SELECT 
            s.id AS Skill_Id, -- Chọn Skill ID để biết Effect thuộc về Skill nào
            e.*, 
            ep.*, 
            ea.*
        FROM skills s
        JOIN skill_effect se ON s.id = se.skill_id
        JOIN effects e ON se.effect_id = e.id
        JOIN effect_property_action epa ON e.id = epa.effect_id
        JOIN effect_property ep ON epa.property_id = ep.property_id
        JOIN effect_action ea ON epa.action_id = ea.action_id
        WHERE s.id IN ({skillIdInClause});";

        // Tạo Dictionary để ánh xạ Skill ID tới đối tượng Skill tương ứng
        var skillDict = skillsList.ToDictionary(s => s.Id);

        // Sử dụng 'using' để đảm bảo Command và Reader được đóng
        using (MySqlCommand command = new MySqlCommand(combinedQuery, connection))
        using (MySqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                // 1. Tìm Skill gốc
                string currentSkillId = reader.GetString("Skill_Id");
                if (!skillDict.TryGetValue(currentSkillId, out Skills currentSkill)) continue;

                // 2. Tạo đối tượng Effect và các đối tượng liên quan
                Effects newEffect = new Effects
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    EffectType = reader.GetString("effect_type"),
                    // Xử lý Duration có thể là NULL
                    Duration = reader.IsDBNull(reader.GetOrdinal("duration")) ? 0 : reader.GetInt32("duration"),
                    Description = reader.GetString("description"),

                    EffectProperty = new EffectProperty
                    {
                        PropertyId = reader.GetInt32("property_id"),
                        PropertyCode = reader.GetString("property_code"),
                        PropertyName = reader.GetString("property_name"),
                        // LƯU Ý: Nếu có nhiều cột Description, bạn phải đặt alias trong SQL để phân biệt
                        // Ví dụ: reader.GetString("Effect_Property_Description") 
                    },
                    EffectAction = new EffectAction
                    {
                        ActionId = reader.GetInt32("action_id"),
                        ActionCode = reader.GetString("action_code"),
                        ActionName = reader.GetString("action_name"),
                        // Tương tự, nếu có Description trùng tên, phải dùng alias
                    }
                };

                // 3. Gán Effect vào Skill
                currentSkill.Effects.Add(newEffect);
            }
        } // Reader và Command tự động được Dispose/Close ở đây

        return skillsList;
    }
    public bool InsertUserCardHeroesSkills(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_heroes_skills 
                WHERE user_id = @user_id AND card_hero_id = @card_hero_id AND skill_id=@skill_id AND position=@position;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@card_hero_id", cardId);
                checkCommand.Parameters.AddWithValue("@skill_id", skillId);
                checkCommand.Parameters.AddWithValue("@position", position);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO card_heroes_skills (
                    user_id, card_hero_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_hero_id, @skill_id, @level, @position
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@card_hero_id", cardId);
                    command.Parameters.AddWithValue("@skill_id", skillId);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@position", position);
                    MySqlDataReader reader = command.ExecuteReader();
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
    public bool InsertUserCardCaptainsSkills(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_captains_skills 
                WHERE user_id = @user_id AND card_captain_id = @card_captain_id AND skill_id=@skill_id AND position=@position;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@card_captain_id", cardId);
                checkCommand.Parameters.AddWithValue("@skill_id", skillId);
                checkCommand.Parameters.AddWithValue("@position", position);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO card_captains_skills (
                    user_id, card_captain_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_captain_id, @skill_id, @level, @position
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@card_captain_id", cardId);
                    command.Parameters.AddWithValue("@skill_id", skillId);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@position", position);
                    MySqlDataReader reader = command.ExecuteReader();
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
    public bool InsertUserCardColonelsSkills(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_colonels_skills 
                WHERE user_id = @user_id AND card_colonel_id = @card_colonel_id AND skill_id=@skill_id AND position=@position;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@card_colonel_id", cardId);
                checkCommand.Parameters.AddWithValue("@skill_id", skillId);
                checkCommand.Parameters.AddWithValue("@position", position);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO card_colonels_skills (
                    user_id, card_colonel_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_colonel_id, @skill_id, @level, @position
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@card_colonel_id", cardId);
                    command.Parameters.AddWithValue("@skill_id", skillId);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@position", position);
                    MySqlDataReader reader = command.ExecuteReader();
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
    public bool InsertUserCardGeneralsSkills(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_generals_skills 
                WHERE user_id = @user_id AND card_general_id = @card_general_id AND skill_id=@skill_id AND position=@position;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@card_general_id", cardId);
                checkCommand.Parameters.AddWithValue("@skill_id", skillId);
                checkCommand.Parameters.AddWithValue("@position", position);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO card_generals_skills (
                    user_id, card_general_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_general_id, @skill_id, @level, @position
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@card_general_id", cardId);
                    command.Parameters.AddWithValue("@skill_id", skillId);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@position", position);
                    MySqlDataReader reader = command.ExecuteReader();
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
    public bool InsertUserCardAdmiralsSkills(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_admirals_skills 
                WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id AND skill_id=@skill_id AND position=@position;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@card_admiral_id", cardId);
                checkCommand.Parameters.AddWithValue("@skill_id", skillId);
                checkCommand.Parameters.AddWithValue("@position", position);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO card_admirals_skills (
                    user_id, card_admiral_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_admiral_id, @skill_id, @level, @position
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@card_admiral_id", cardId);
                    command.Parameters.AddWithValue("@skill_id", skillId);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@position", position);
                    MySqlDataReader reader = command.ExecuteReader();
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
    public bool InsertUserCardMilitarySkills(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_military_skills 
                WHERE user_id = @user_id AND card_military_id = @card_military_id AND skill_id=@skill_id AND position=@position;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@card_military_id", cardId);
                checkCommand.Parameters.AddWithValue("@skill_id", skillId);
                checkCommand.Parameters.AddWithValue("@position", position);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO card_military_skills (
                    user_id, card_military_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_military_id, @skill_id, @level, @position
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@card_military_id", cardId);
                    command.Parameters.AddWithValue("@skill_id", skillId);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@position", position);
                    MySqlDataReader reader = command.ExecuteReader();
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
    public bool InsertUserCardMonstersSkills(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_monsters_skills 
                WHERE user_id = @user_id AND card_monster_id = @card_monster_id AND skill_id=@skill_id AND position=@position;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@card_monster_id", cardId);
                checkCommand.Parameters.AddWithValue("@skill_id", skillId);
                checkCommand.Parameters.AddWithValue("@position", position);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO card_monsters_skills (
                    user_id, card_monster_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_monster_id, @skill_id, @level, @position
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@card_monster_id", cardId);
                    command.Parameters.AddWithValue("@skill_id", skillId);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@position", position);
                    MySqlDataReader reader = command.ExecuteReader();
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
    public bool InsertUserCardSpellSkills(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_spell_skills 
                WHERE user_id = @user_id AND card_spell_id = @card_spell_id AND skill_id=@skill_id AND position=@position;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@card_spell_id", cardId);
                checkCommand.Parameters.AddWithValue("@skill_id", skillId);
                checkCommand.Parameters.AddWithValue("@position", position);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO card_spell_skills (
                    user_id, card_spell_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_spell_id, @skill_id, @level, @position
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@card_spell_id", cardId);
                    command.Parameters.AddWithValue("@skill_id", skillId);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@position", position);
                    MySqlDataReader reader = command.ExecuteReader();
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
    public bool DeleteUserCardHeroesSkills(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string deleteQuery = @"
                DELETE FROM card_heroes_skills 
                WHERE user_id = @user_id AND card_hero_id = @card_hero_id AND skill_id=@skill_id AND position=@position;";

                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@user_id", userId);
                deleteCommand.Parameters.AddWithValue("@card_hero_id", cardId);
                deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
                deleteCommand.Parameters.AddWithValue("@position", position);
                deleteCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool DeleteUserCardCaptainsSkills(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string deleteQuery = @"
                DELETE FROM card_captains_skills 
                WHERE user_id = @user_id AND card_captain_id = @card_captain_id AND skill_id=@skill_id AND position=@position;";

                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@user_id", userId);
                deleteCommand.Parameters.AddWithValue("@card_captain_id", cardId);
                deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
                deleteCommand.Parameters.AddWithValue("@position", position);
                deleteCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool DeleteUserCardColonelsSkills(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string deleteQuery = @"
                DELETE FROM card_colonels_skills 
                WHERE user_id = @user_id AND card_colonel_id = @card_colonel_id AND skill_id=@skill_id AND position=@position;";

                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@user_id", userId);
                deleteCommand.Parameters.AddWithValue("@card_colonel_id", cardId);
                deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
                deleteCommand.Parameters.AddWithValue("@position", position);
                deleteCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool DeleteUserCardGeneralsSkills(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string deleteQuery = @"
                DELETE FROM card_generals_skills 
                WHERE user_id = @user_id AND card_general_id = @card_general_id AND skill_id=@skill_id AND position=@position;";

                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@user_id", userId);
                deleteCommand.Parameters.AddWithValue("@card_general_id", cardId);
                deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
                deleteCommand.Parameters.AddWithValue("@position", position);
                deleteCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool DeleteUserCardAdmiralsSkills(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string deleteQuery = @"
                DELETE FROM card_admirals_skills 
                WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id AND skill_id=@skill_id AND position=@position;";

                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@user_id", userId);
                deleteCommand.Parameters.AddWithValue("@card_admiral_id", cardId);
                deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
                deleteCommand.Parameters.AddWithValue("@position", position);
                deleteCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool DeleteUserCardMonstersSkills(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string deleteQuery = @"
                DELETE FROM card_monsters_skills 
                WHERE user_id = @user_id AND card_monster_id = @card_monster_id AND skill_id=@skill_id AND position=@position;";

                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@user_id", userId);
                deleteCommand.Parameters.AddWithValue("@card_monster_id", cardId);
                deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
                deleteCommand.Parameters.AddWithValue("@position", position);
                deleteCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool DeleteUserCardMilitarySkills(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string deleteQuery = @"
                DELETE FROM card_military_skills 
                WHERE user_id = @user_id AND card_military_id = @card_military_id AND skill_id=@skill_id AND position=@position;";

                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@user_id", userId);
                deleteCommand.Parameters.AddWithValue("@card_military_id", cardId);
                deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
                deleteCommand.Parameters.AddWithValue("@position", position);
                deleteCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool DeleteUserCardSpellSkills(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string deleteQuery = @"
                DELETE FROM card_spell_skills 
                WHERE user_id = @user_id AND card_spell_id = @card_spell_id AND skill_id=@skill_id AND position=@position;";

                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@user_id", userId);
                deleteCommand.Parameters.AddWithValue("@card_spell_id", cardId);
                deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
                deleteCommand.Parameters.AddWithValue("@position", position);
                deleteCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
}