using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserCardMonstersRepository : IUserCardMonstersRepository
{
    public List<CardMonsters> GetUserCardMonsters(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<CardMonsters> CardMonstersList = new List<CardMonsters>();
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT um.*, m.name, m.image, m.type, m.description
                FROM user_card_monsters um
                LEFT JOIN card_monsters m ON um.card_monster_id = m.id
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
                    CardMonsters CardMonsters = new CardMonsters
                    {
                        Id = reader.GetString("card_monster_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        Star = reader.GetInt32("star"),
                        Level = reader.GetInt32("level"),
                        Experiment = reader.GetDouble("experiment"),
                        Quantity = reader.GetDouble("quantity"),
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

                    CardMonstersList.Add(CardMonsters);
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
        return CardMonstersList;
    }
    public List<CardMonsters> GetUserCardMonstersTeam(string user_id, string teamId, string position)
    {
        List<CardMonsters> CardMonstersList = new List<CardMonsters>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT um.*, m.name, m.image, m.type, m.description
                FROM user_card_monsters um
                LEFT JOIN card_monsters m ON um.card_monster_id = m.id
                WHERE um.user_id = @userId and um.team_id=@team_id and SUBSTRING_INDEX(um.position, '-', 1) = @position
                ORDER BY m.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@team_id", teamId);
                command.Parameters.AddWithValue("@position", position);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardMonsters CardMonsters = new CardMonsters
                    {
                        Id = reader.GetString("monster_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        Star = reader.GetInt32("star"),
                        Level = reader.GetInt32("level"),
                        Experiment = reader.GetDouble("experiment"),
                        Quantity = reader.GetDouble("quantity"),
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

                    CardMonstersList.Add(CardMonsters);
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
        return CardMonstersList;
    }
    public List<CardMonsters> GetUserCardMonstersTeamWithoutPosition(string user_id, string teamId)
    {
        List<CardMonsters> CardMonstersList = new List<CardMonsters>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT um.*, m.name, m.image, m.type, m.description
                FROM user_card_monsters um
                LEFT JOIN card_monsters m ON um.card_monster_id = m.id
                WHERE um.user_id = @userId and um.team_id=@team_id
                ORDER BY m.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@team_id", teamId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardMonsters CardMonsters = new CardMonsters
                    {
                        Id = reader.GetString("monster_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        Star = reader.GetInt32("star"),
                        Level = reader.GetInt32("level"),
                        Experiment = reader.GetDouble("experiment"),
                        Quantity = reader.GetDouble("quantity"),
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

                    CardMonstersList.Add(CardMonsters);
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
        return CardMonstersList;
    }
    public Dictionary<string, int> GetUniqueCardMonsterTypesTeam(string teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = @"SELECT distinct c.type, count(c.type) as number
            FROM user_card_monsters uc
            LEFT JOIN card_monsters c ON uc.card_monster_id = c.id 
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
    public int GetUserCardMonstersCount(string user_id, string type, string rare)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select count(*) from card_monsters m, user_card_monsters um 
                where m.id=um.card_monster_id and um.user_id=@userId and m.type= @type AND (@rare = 'All' or m.rare = @rare)";
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
    public int GetUserCardMonstersTeamsPositionCount(string user_id, string team_id, string position)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*) from user_card_monsters
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
    public int GetUserCardMonstersTeamsCount(string user_id, string team_id)
    {
        int count = 0;
        // string user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*) from user_card_monsters
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
    public bool InsertUserCardMonsters(CardMonsters CardMonsters)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_monsters
                WHERE user_id = @user_id AND card_monster_id = @card_monster_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_monster_id", CardMonsters.Id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_card_monsters(
                    user_id, card_monster_id, rare, level, experiment, star, quality, block, quantity,
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
                    @user_id, @card_monster_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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
                    command.Parameters.AddWithValue("@card_monster_id", CardMonsters.Id);
                    command.Parameters.AddWithValue("@rare", CardMonsters.Rare);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(CardMonsters.Rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", CardMonsters.Quantity);
                    command.Parameters.AddWithValue("@power", CardMonsters.Power);
                    command.Parameters.AddWithValue("@health", CardMonsters.Health);
                    command.Parameters.AddWithValue("@physical_attack", CardMonsters.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", CardMonsters.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", CardMonsters.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", CardMonsters.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", CardMonsters.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", CardMonsters.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", CardMonsters.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", CardMonsters.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", CardMonsters.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", CardMonsters.MentalDefense);
                    command.Parameters.AddWithValue("@speed", CardMonsters.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", CardMonsters.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", CardMonsters.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", CardMonsters.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", CardMonsters.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", CardMonsters.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", CardMonsters.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", CardMonsters.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", CardMonsters.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", CardMonsters.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", CardMonsters.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", CardMonsters.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", CardMonsters.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", CardMonsters.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", CardMonsters.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", CardMonsters.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", CardMonsters.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", CardMonsters.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", CardMonsters.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", CardMonsters.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", CardMonsters.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", CardMonsters.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", CardMonsters.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", CardMonsters.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", CardMonsters.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", CardMonsters.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", CardMonsters.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", CardMonsters.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", CardMonsters.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", CardMonsters.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", CardMonsters.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", CardMonsters.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", CardMonsters.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", CardMonsters.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", CardMonsters.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", CardMonsters.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", CardMonsters.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", CardMonsters.SkillResistanceRate);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_card_monsters
                    SET quantity = @quantity
                    WHERE user_id = @user_id AND card_monster_id = @card_monster_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@card_monster_id", CardMonsters.Id);
                    updateCommand.Parameters.AddWithValue("@quantity", CardMonsters.Quantity);

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
    public bool UpdateCardMonstersLevel(CardMonsters cardMonsters, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_monsters
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
                WHERE user_id = @user_id AND card_monster_id = @card_monster_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_monster_id", cardMonsters.Id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", cardMonsters.Power);
                command.Parameters.AddWithValue("@health", cardMonsters.Health);
                command.Parameters.AddWithValue("@physical_attack", cardMonsters.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", cardMonsters.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", cardMonsters.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", cardMonsters.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", cardMonsters.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", cardMonsters.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", cardMonsters.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", cardMonsters.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", cardMonsters.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", cardMonsters.MentalDefense);
                command.Parameters.AddWithValue("@speed", cardMonsters.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", cardMonsters.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", cardMonsters.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", cardMonsters.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", cardMonsters.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", cardMonsters.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", cardMonsters.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", cardMonsters.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardMonsters.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardMonsters.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", cardMonsters.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardMonsters.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardMonsters.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", cardMonsters.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardMonsters.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", cardMonsters.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", cardMonsters.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardMonsters.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", cardMonsters.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", cardMonsters.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", cardMonsters.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", cardMonsters.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", cardMonsters.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", cardMonsters.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", cardMonsters.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", cardMonsters.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", cardMonsters.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", cardMonsters.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", cardMonsters.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardMonsters.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardMonsters.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardMonsters.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardMonsters.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardMonsters.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", cardMonsters.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", cardMonsters.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", cardMonsters.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", cardMonsters.SkillResistanceRate);
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
    public bool UpdateCardMonstersBreakthrough(CardMonsters cardMonsters, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_monsters
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
                WHERE user_id = @user_id AND card_monster_id = @card_monster_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_monster_id", cardMonsters.Id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", cardMonsters.Power);
                command.Parameters.AddWithValue("@health", cardMonsters.Health);
                command.Parameters.AddWithValue("@physical_attack", cardMonsters.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", cardMonsters.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", cardMonsters.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", cardMonsters.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", cardMonsters.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", cardMonsters.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", cardMonsters.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", cardMonsters.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", cardMonsters.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", cardMonsters.MentalDefense);
                command.Parameters.AddWithValue("@speed", cardMonsters.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", cardMonsters.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", cardMonsters.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", cardMonsters.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", cardMonsters.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", cardMonsters.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", cardMonsters.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", cardMonsters.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardMonsters.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardMonsters.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", cardMonsters.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardMonsters.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardMonsters.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", cardMonsters.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardMonsters.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", cardMonsters.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", cardMonsters.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardMonsters.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", cardMonsters.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", cardMonsters.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", cardMonsters.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", cardMonsters.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", cardMonsters.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", cardMonsters.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", cardMonsters.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", cardMonsters.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", cardMonsters.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", cardMonsters.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", cardMonsters.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardMonsters.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardMonsters.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardMonsters.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardMonsters.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardMonsters.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", cardMonsters.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", cardMonsters.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", cardMonsters.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", cardMonsters.SkillResistanceRate);
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
    public bool UpdateTeamCardMonsters(string team_id, string position, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                Update user_card_monsters set team_id=@team_id, position=@position where user_id=@user_id 
                and card_monster_id=@card_monster_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_monster_id", card_id);
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
    public CardMonsters GetUserCardMonstersById(string user_id, string Id)
    {
        CardMonsters card = new CardMonsters();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select uc.*, c.image
                from user_card_monsters uc, card_monsters c
                where uc.card_monster_id=@id 
                and uc.card_monster_id = c.id
                and uc.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new CardMonsters
                    {
                        Id = reader.GetString("card_monster_id"),
                        Image = reader.GetString("image"),
                        Level = reader.GetInt32("level"),
                        Quality = reader.GetInt32("quality"),
                        Experiment = reader.GetDouble("experiment"),
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
    public List<CardMonsters> GetAllUserCardMonstersInTeam(string user_id)
    {
        List<CardMonsters> cardMonsters = new List<CardMonsters>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = @"SELECT uc.*, c.name, c.image, c.type, c.description
                FROM user_card_monsters uc
                LEFT JOIN card_monsters c ON uc.card_monster_id = c.id 
                WHERE uc.user_id = @user_id and uc.team_id IS NOT null";
            MySqlCommand command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardMonsters monsters = new CardMonsters
                {
                    Id = reader.GetString("card_monster_id"),
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

                cardMonsters.Add(monsters);
            }
            connection.Close();
        }
        return cardMonsters;
    }
}