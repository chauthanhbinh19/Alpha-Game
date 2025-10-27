using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserCardMilitaryRepository : IUserCardMilitaryRepository
{
    public List<CardMilitaries> GetUserCardMilitary(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<CardMilitaries> CardMilitaryList = new List<CardMilitaries>();
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT um.*, m.name, m.image, m.type, m.description
                FROM user_card_military um
                LEFT JOIN card_military m ON um.card_military_id = m.id 
                WHERE um.user_id = @userId AND m.type = @type AND (@rare = 'All' or m.rare = @rare)
                ORDER BY m.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name
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
                    CardMilitaries CardMilitary = new CardMilitaries
                    {
                        Id = reader.GetString("card_military_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        Star = reader.GetInt32("star"),
                        Level = reader.GetInt32("level"),
                        Experiment = reader.GetInt32("experiment"),
                        Quantity = reader.GetInt32("quantity"),
                        Block = reader.GetBoolean("block"),
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

                        BaseStats = new BaseStats
                        {
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
                        }
                    };

                    CardMilitaryList.Add(CardMilitary);
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
        return CardMilitaryList;
    }
    public List<CardMilitaries> GetUserCardMilitaryTeam(string user_id, string teamId, string position)
    {
        List<CardMilitaries> CardMilitaryList = new List<CardMilitaries>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT um.*, m.name, m.image, m.type, m.description
                FROM user_card_military um
                LEFT JOIN card_military m ON um.card_military_id = m.id 
                WHERE um.user_id = @userId AND um.team_id = @team_id and SUBSTRING_INDEX(um.position, '-', 1) = @position
                ORDER BY m.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@team_id", teamId);
                command.Parameters.AddWithValue("@position", position);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardMilitaries CardMilitary = new CardMilitaries
                    {
                        Id = reader.GetString("card_military_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        Star = reader.GetInt32("star"),
                        Level = reader.GetInt32("level"),
                        Experiment = reader.GetInt32("experiment"),
                        Quantity = reader.GetInt32("quantity"),
                        Block = reader.GetBoolean("block"),
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

                        BaseStats = new BaseStats
                        {
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
                        }
                    };

                    CardMilitaryList.Add(CardMilitary);
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
        return CardMilitaryList;
    }
    public List<CardMilitaries> GetUserCardMilitaryTeamWithoutPosition(string user_id, string teamId)
    {
        List<CardMilitaries> CardMilitaryList = new List<CardMilitaries>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT um.*, m.name, m.image, m.type, m.description
                FROM user_card_military um
                LEFT JOIN card_military m ON um.card_military_id = m.id 
                WHERE um.user_id = @userId AND um.team_id = @team_id
                ORDER BY m.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@team_id", teamId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardMilitaries CardMilitary = new CardMilitaries
                    {
                        Id = reader.GetString("card_military_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        Star = reader.GetInt32("star"),
                        Level = reader.GetInt32("level"),
                        Experiment = reader.GetInt32("experiment"),
                        Quantity = reader.GetInt32("quantity"),
                        Block = reader.GetBoolean("block"),
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

                        BaseStats = new BaseStats
                        {
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
                        }
                    };

                    CardMilitaryList.Add(CardMilitary);
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
        return CardMilitaryList;
    }
    public Dictionary<string, int> GetUniqueCardMilitaryTypesTeam(string teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = @"SELECT distinct c.type, count(c.type) as number
            FROM user_card_military uc
            LEFT JOIN card_military c ON uc.card_military_id = c.id 
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
            connection.Close();
        }
        return result;
    }
    public bool UpdateTeamCardMilitary(string team_id, string position, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                Update user_card_military set team_id=@team_id, position=@position where user_id=@user_id 
                and card_military_id=@card_military_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_military_id", card_id);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        return true;
    }
    public int GetUserCardMilitaryCount(string user_id, string type, string rare)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select count(*) from card_military m, user_card_military um 
                where m.id=um.card_military_id and um.user_id=@userId and m.type= @type AND (@rare = 'All' or m.rare = @rare)";
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
            finally
            {
                connection.Close();
            }
        }
        return count;
    }
    public int GetUserCardMilitaryTeamsPositionCount(string user_id, string team_id, string position)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*) from user_card_military
                where team_id = @team_id and SUBSTRING_INDEX(position, '-', 1) = @position and user_id=@userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@position", position);
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
    public int GetUserCardMilitaryTeamsCount(string user_id, string team_id)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*) from user_card_military
                where team_id = @team_id and user_id=@userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@team_id", team_id);
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
    public bool InsertUserCardMilitary(CardMilitaries CardMilitary)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_military 
                WHERE user_id = @user_id AND card_military_id = @card_military_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_military_id", CardMilitary.Id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_card_military (
                    user_id, card_military_id, rare, level, experiment, star, quality, block, quantity,
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
                    @user_id, @card_military_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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
                    command.Parameters.AddWithValue("@card_military_id", CardMilitary.Id);
                    command.Parameters.AddWithValue("@rare", CardMilitary.Rare);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(CardMilitary.Rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", CardMilitary.Quantity);
                    command.Parameters.AddWithValue("@power", CardMilitary.Power);
                    command.Parameters.AddWithValue("@health", CardMilitary.Health);
                    command.Parameters.AddWithValue("@physical_attack", CardMilitary.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", CardMilitary.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", CardMilitary.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", CardMilitary.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", CardMilitary.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", CardMilitary.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", CardMilitary.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", CardMilitary.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", CardMilitary.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", CardMilitary.MentalDefense);
                    command.Parameters.AddWithValue("@speed", CardMilitary.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", CardMilitary.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", CardMilitary.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", CardMilitary.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", CardMilitary.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", CardMilitary.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", CardMilitary.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", CardMilitary.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", CardMilitary.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", CardMilitary.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", CardMilitary.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", CardMilitary.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", CardMilitary.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", CardMilitary.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", CardMilitary.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", CardMilitary.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", CardMilitary.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", CardMilitary.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", CardMilitary.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", CardMilitary.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", CardMilitary.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", CardMilitary.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", CardMilitary.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", CardMilitary.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", CardMilitary.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", CardMilitary.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", CardMilitary.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", CardMilitary.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", CardMilitary.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", CardMilitary.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", CardMilitary.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", CardMilitary.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", CardMilitary.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", CardMilitary.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", CardMilitary.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", CardMilitary.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", CardMilitary.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", CardMilitary.SkillResistanceRate);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_card_military
                    SET quantity = @quantity
                    WHERE user_id = @user_id AND card_military_id = @card_military_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@card_military_id", CardMilitary.Id);
                    updateCommand.Parameters.AddWithValue("@quantity", CardMilitary.Quantity);

                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }

        }
        return true;
    }
    public bool UpdateCardMilitaryLevel(CardMilitaries cardMilitary, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_military
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
                WHERE user_id = @user_id AND card_military_id = @card_military_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_military_id", cardMilitary.Id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", cardMilitary.Power);
                command.Parameters.AddWithValue("@health", cardMilitary.Health);
                command.Parameters.AddWithValue("@physical_attack", cardMilitary.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", cardMilitary.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", cardMilitary.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", cardMilitary.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", cardMilitary.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", cardMilitary.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", cardMilitary.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", cardMilitary.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", cardMilitary.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", cardMilitary.MentalDefense);
                command.Parameters.AddWithValue("@speed", cardMilitary.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", cardMilitary.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", cardMilitary.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", cardMilitary.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", cardMilitary.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", cardMilitary.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", cardMilitary.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", cardMilitary.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardMilitary.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardMilitary.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", cardMilitary.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardMilitary.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardMilitary.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", cardMilitary.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardMilitary.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", cardMilitary.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", cardMilitary.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardMilitary.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", cardMilitary.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", cardMilitary.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", cardMilitary.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", cardMilitary.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", cardMilitary.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", cardMilitary.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", cardMilitary.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", cardMilitary.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", cardMilitary.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", cardMilitary.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", cardMilitary.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardMilitary.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardMilitary.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardMilitary.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardMilitary.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardMilitary.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", cardMilitary.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", cardMilitary.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", cardMilitary.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", cardMilitary.SkillResistanceRate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        return true;
    }
    public bool UpdateCardMilitaryBreakthrough(CardMilitaries cardMilitary, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_military
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
                WHERE user_id = @user_id AND card_military_id = @card_military_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_military_id", cardMilitary.Id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", cardMilitary.Power);
                command.Parameters.AddWithValue("@health", cardMilitary.Health);
                command.Parameters.AddWithValue("@physical_attack", cardMilitary.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", cardMilitary.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", cardMilitary.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", cardMilitary.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", cardMilitary.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", cardMilitary.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", cardMilitary.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", cardMilitary.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", cardMilitary.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", cardMilitary.MentalDefense);
                command.Parameters.AddWithValue("@speed", cardMilitary.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", cardMilitary.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", cardMilitary.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", cardMilitary.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", cardMilitary.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", cardMilitary.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", cardMilitary.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", cardMilitary.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardMilitary.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardMilitary.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", cardMilitary.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardMilitary.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardMilitary.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", cardMilitary.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardMilitary.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", cardMilitary.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", cardMilitary.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardMilitary.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", cardMilitary.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", cardMilitary.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", cardMilitary.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", cardMilitary.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", cardMilitary.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", cardMilitary.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", cardMilitary.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", cardMilitary.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", cardMilitary.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", cardMilitary.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", cardMilitary.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardMilitary.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardMilitary.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardMilitary.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardMilitary.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardMilitary.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", cardMilitary.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", cardMilitary.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", cardMilitary.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", cardMilitary.SkillResistanceRate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        return true;
    }
    public CardMilitaries GetUserCardMilitaryById(string user_id, string Id)
    {
        CardMilitaries card = new CardMilitaries();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select uc.*, c.image
                from user_card_military uc, card_military c
                where uc.card_military_id=@id 
                and uc.card_military_id = c.id
                and uc.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new CardMilitaries
                    {
                        Id = reader.GetString("card_military_id"),
                        Image = reader.GetString("image"),
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

                        BaseStats = new BaseStats
                        {
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
                        }
                    };
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
        return card;
    }
    public List<CardMilitaries> GetAllUserCardMilitaryInTeam(string user_id)
    {
        List<CardMilitaries> cardMilitaries = new List<CardMilitaries>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = @"SELECT uc.*, c.name, c.image, c.type, c.description
                FROM user_card_military uc
                LEFT JOIN card_military c ON uc.card_military_id = c.id 
                WHERE uc.user_id = @user_id and uc.team_id IS NOT null";
            MySqlCommand command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardMilitaries military = new CardMilitaries
                {
                    Id = reader.GetString("card_military_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetInt32("quality"),
                    Type = reader.GetString("type"),
                    Star = reader.GetInt32("star"),
                    Level = reader.GetInt32("level"),
                    Experiment = reader.GetInt32("experiment"),
                    Quantity = reader.GetInt32("quantity"),
                    Block = reader.GetBoolean("block"),
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

                    BaseStats = new BaseStats
                    {
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
                    }
                };

                cardMilitaries.Add(military);
            }
            connection.Close();
        }
        return cardMilitaries;
    }
}