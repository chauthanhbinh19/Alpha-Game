using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
public class UserCardAdmiralsRepository : IUserCardAdmiralsRepository
{
    public List<CardAdmirals> GetUserCardAdmirals(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<CardAdmirals> CardAdmiralsList = new List<CardAdmirals>();
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT uc.*, c.name, c.image, c.type, c.description
                FROM user_card_admirals uc
                LEFT JOIN card_admirals c ON c.id = uc.card_admiral_id 
                WHERE uc.user_id = @userId AND c.type = @type AND (@rare = 'All' or c.rare = @rare)
                ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name
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
                    CardAdmirals captain = new CardAdmirals
                    {
                        Id = reader.GetString("card_admiral_id"),
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

                    CardAdmiralsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardAdmiralsList;
    }
    public List<CardAdmirals> GetUserCardAdmiralsTeam(string user_id, string teamId, string position)
    {
        List<CardAdmirals> CardAdmiralsList = new List<CardAdmirals>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT uc.*, c.name, c.image, c.type, c.description
                FROM user_card_admirals uc
                LEFT JOIN card_admirals c ON c.id = uc.card_admiral_id 
                WHERE uc.user_id = @userId AND uc.team_id=@team_id and SUBSTRING_INDEX(uc.position, '-', 1) = @position
                ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@team_id", teamId);
                command.Parameters.AddWithValue("@position", position);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardAdmirals captain = new CardAdmirals
                    {
                        Id = reader.GetString("card_admiral_id"),
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

                    CardAdmiralsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardAdmiralsList;
    }
    public List<CardAdmirals> GetUserCardAdmiralsTeamWithoutPosition(string user_id, string teamId)
    {
        List<CardAdmirals> CardAdmiralsList = new List<CardAdmirals>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT uc.*, c.name, c.image, c.type, c.description
                FROM user_card_admirals uc
                LEFT JOIN card_admirals c ON c.id = uc.card_admiral_id 
                WHERE uc.user_id = @userId AND uc.team_id=@team_id
                ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@team_id", teamId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardAdmirals captain = new CardAdmirals
                    {
                        Id = reader.GetString("card_admiral_id"),
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

                    CardAdmiralsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardAdmiralsList;
    }
    public Dictionary<string, int> GetUniqueCardAdmiralTypesTeam(string teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = @"SELECT distinct c.type, count(c.type) as number
            FROM user_card_admirals uc
            LEFT JOIN card_admirals c ON uc.card_admiral_id = c.id 
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
    public bool UpdateTeamCardAdmirals(string team_id, string position, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                Update user_card_admirals set team_id=@team_id, position=@position where user_id=@user_id 
                and card_admiral_id=@card_admiral_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_admiral_id", card_id);
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
    public int GetUserCardAdmiralsCount(string user_id, string type, string rare)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select count(*) from card_admirals c, user_card_admirals uc 
                where c.id=uc.card_admiral_id and uc.user_id=@userId and c.type= @type AND (@rare = 'All' or c.rare = @rare)";
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
    public int GetUserCardAdmiralsTeamsPositionCount(string user_id, string team_id, string position)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*) from user_card_admirals
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
        }
        return count;
    }
    public int GetUserCardAdmiralsTeamsCount(string user_id, string team_id)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*) from user_card_admirals
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
        }
        return count;
    }
    public bool InsertUserCardAdmirals(CardAdmirals CardAdmirals)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_admirals
                WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_admiral_id", CardAdmirals.Id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_card_admirals (
                    user_id, card_admiral_id, rare, level, experiment, star, quality, block, quantity,
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
                    @user_id, @card_admiral_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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
                    command.Parameters.AddWithValue("@card_admiral_id", CardAdmirals.Id);
                    command.Parameters.AddWithValue("@rare", CardAdmirals.Rare);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(CardAdmirals.Rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", CardAdmirals.Quantity);
                    command.Parameters.AddWithValue("@power", CardAdmirals.Power);
                    command.Parameters.AddWithValue("@health", CardAdmirals.Health);
                    command.Parameters.AddWithValue("@physical_attack", CardAdmirals.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", CardAdmirals.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", CardAdmirals.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", CardAdmirals.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", CardAdmirals.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", CardAdmirals.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", CardAdmirals.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", CardAdmirals.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", CardAdmirals.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", CardAdmirals.MentalDefense);
                    command.Parameters.AddWithValue("@speed", CardAdmirals.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", CardAdmirals.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", CardAdmirals.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", CardAdmirals.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", CardAdmirals.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", CardAdmirals.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", CardAdmirals.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", CardAdmirals.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", CardAdmirals.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", CardAdmirals.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", CardAdmirals.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", CardAdmirals.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", CardAdmirals.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", CardAdmirals.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", CardAdmirals.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", CardAdmirals.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", CardAdmirals.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", CardAdmirals.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", CardAdmirals.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", CardAdmirals.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", CardAdmirals.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", CardAdmirals.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", CardAdmirals.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", CardAdmirals.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", CardAdmirals.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", CardAdmirals.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", CardAdmirals.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", CardAdmirals.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", CardAdmirals.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", CardAdmirals.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", CardAdmirals.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", CardAdmirals.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", CardAdmirals.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", CardAdmirals.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", CardAdmirals.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", CardAdmirals.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", CardAdmirals.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", CardAdmirals.SkillResistanceRate);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_card_admirals
                    SET quantity = @quantity
                    WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@card_admiral_id", CardAdmirals.Id);
                    updateCommand.Parameters.AddWithValue("@quantity", CardAdmirals.Quantity);

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
    public bool UpdateCardAdmiralsLevel(CardAdmirals cardAdmirals, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_admirals
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
                WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_admiral_id", cardAdmirals.Id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", cardAdmirals.Power);
                command.Parameters.AddWithValue("@health", cardAdmirals.Health);
                command.Parameters.AddWithValue("@physical_attack", cardAdmirals.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", cardAdmirals.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", cardAdmirals.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", cardAdmirals.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", cardAdmirals.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", cardAdmirals.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", cardAdmirals.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", cardAdmirals.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", cardAdmirals.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", cardAdmirals.MentalDefense);
                command.Parameters.AddWithValue("@speed", cardAdmirals.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", cardAdmirals.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", cardAdmirals.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", cardAdmirals.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", cardAdmirals.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", cardAdmirals.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", cardAdmirals.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", cardAdmirals.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardAdmirals.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardAdmirals.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", cardAdmirals.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardAdmirals.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardAdmirals.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", cardAdmirals.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardAdmirals.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", cardAdmirals.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", cardAdmirals.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardAdmirals.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", cardAdmirals.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", cardAdmirals.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", cardAdmirals.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", cardAdmirals.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", cardAdmirals.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", cardAdmirals.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", cardAdmirals.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", cardAdmirals.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", cardAdmirals.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", cardAdmirals.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", cardAdmirals.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardAdmirals.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardAdmirals.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardAdmirals.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardAdmirals.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardAdmirals.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", cardAdmirals.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", cardAdmirals.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", cardAdmirals.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", cardAdmirals.SkillResistanceRate);
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
    public bool UpdateCardAdmiralsBreakthrough(CardAdmirals cardAdmirals, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_admirals
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
                WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_admiral_id", cardAdmirals.Id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", cardAdmirals.Power);
                command.Parameters.AddWithValue("@health", cardAdmirals.Health);
                command.Parameters.AddWithValue("@physical_attack", cardAdmirals.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", cardAdmirals.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", cardAdmirals.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", cardAdmirals.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", cardAdmirals.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", cardAdmirals.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", cardAdmirals.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", cardAdmirals.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", cardAdmirals.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", cardAdmirals.MentalDefense);
                command.Parameters.AddWithValue("@speed", cardAdmirals.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", cardAdmirals.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", cardAdmirals.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", cardAdmirals.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", cardAdmirals.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", cardAdmirals.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", cardAdmirals.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", cardAdmirals.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardAdmirals.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardAdmirals.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", cardAdmirals.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardAdmirals.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardAdmirals.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", cardAdmirals.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardAdmirals.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", cardAdmirals.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", cardAdmirals.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardAdmirals.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", cardAdmirals.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", cardAdmirals.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", cardAdmirals.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", cardAdmirals.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", cardAdmirals.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", cardAdmirals.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", cardAdmirals.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", cardAdmirals.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", cardAdmirals.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", cardAdmirals.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", cardAdmirals.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardAdmirals.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardAdmirals.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardAdmirals.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardAdmirals.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardAdmirals.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", cardAdmirals.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", cardAdmirals.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", cardAdmirals.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", cardAdmirals.SkillResistanceRate);
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
    public CardAdmirals GetUserCardAdmiralsById(string user_id, string Id)
    {
        CardAdmirals card = new CardAdmirals();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select uc.*, c.image
                from user_card_admirals uc, card_admirals c
                where uc.card_admiral_id=@id 
                and uc.card_admiral_id = c.id
                and uc.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new CardAdmirals
                    {
                        Id = reader.GetString("card_admiral_id"),
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

        }
        return card;
    }
    public List<CardAdmirals> GetAllUserCardAdmiralsInTeam(string user_id)
    {
        List<CardAdmirals> cardAdmirals = new List<CardAdmirals>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = @"SELECT uc.*, c.name, c.image, c.type, c.description
                FROM user_card_admirals uc
                LEFT JOIN card_admirals c ON uc.card_admiral_id = c.id 
                WHERE uc.user_id = @user_id and uc.team_id IS NOT null";
            MySqlCommand command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardAdmirals admirals = new CardAdmirals
                {
                    Id = reader.GetString("card_admiral_id"),
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

                cardAdmirals.Add(admirals);
            }
        }
        return cardAdmirals;
    }
}