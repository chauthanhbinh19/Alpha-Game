using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UserCardMilitariesRepository : IUserCardMilitariesRepository
{
    public async Task<List<CardMilitaries>> GetUserCardMilitariesAsync(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<CardMilitaries> CardMilitariesList = new List<CardMilitaries>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT uc.*, c.name, c.image, c.type, c.description, COALESCE(t.team_number, 0) AS team_number
            FROM user_card_militaries uc
            LEFT JOIN card_militaries c ON c.id = uc.card_military_id 
            LEFT JOIN teams t on t.team_id = uc.team_id
            WHERE uc.user_id = @userId AND c.type = @type AND (@rare = 'All' or c.rare = @rare)
            ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name
            LIMIT @limit OFFSET @offset;
        ";

            await using MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", user_id);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@rare", rare);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            await using MySqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                CardMilitaries captain = new CardMilitaries
                {
                    Id = reader.GetString("card_military_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetDouble("quality"),
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
                    Mana = reader.GetDouble("mana"),
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

                    Team = new Teams
                    {
                        TeamNumber = reader.GetInt32("team_number")
                    },

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
                        Mana = reader.GetDouble("mana"),
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

                CardMilitariesList.Add(captain);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return CardMilitariesList;
    }
    public async Task<List<CardMilitaries>> GetUserCardMilitariesTeamAsync(string user_id, string teamId, string position)
    {
        List<CardMilitaries> CardMilitariesList = new List<CardMilitaries>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT uc.*, c.name, c.image, c.type, c.description
            FROM user_card_militaries uc
            LEFT JOIN card_militaries c ON c.id = uc.card_military_id 
            WHERE uc.user_id = @userId AND uc.team_id = @team_id AND SUBSTRING_INDEX(uc.position, '-', 1) = @position
            ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name;
        ";

            await using MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", user_id);
            command.Parameters.AddWithValue("@team_id", teamId);
            command.Parameters.AddWithValue("@position", position);

            await using MySqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                CardMilitaries captain = new CardMilitaries
                {
                    Id = reader.GetString("card_military_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetDouble("quality"),
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
                    Mana = reader.GetDouble("mana"),
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
                        Mana = reader.GetDouble("mana"),
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

                CardMilitariesList.Add(captain);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return CardMilitariesList;
    }
    public async Task<List<CardMilitaries>> GetUserCardMilitariesTeamWithoutPositionAsync(string user_id, string teamId)
    {
        List<CardMilitaries> CardMilitariesList = new List<CardMilitaries>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT uc.*, c.name, c.image, c.type, c.description
            FROM user_card_militaries uc
            LEFT JOIN card_militaries c ON c.id = uc.card_military_id 
            WHERE uc.user_id = @userId AND uc.team_id = @team_id
            ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name;
        ";

            await using MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", user_id);
            command.Parameters.AddWithValue("@team_id", teamId);

            await using MySqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                CardMilitaries captain = new CardMilitaries
                {
                    Id = reader.GetString("card_military_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetDouble("quality"),
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
                    Mana = reader.GetDouble("mana"),
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
                        Mana = reader.GetDouble("mana"),
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

                CardMilitariesList.Add(captain);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return CardMilitariesList;
    }
    public async Task<Dictionary<string, int>> GetUniqueCardMilitariesTypesTeamAsync(string teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT c.type, COUNT(c.type) AS number
            FROM user_card_militaries uc
            LEFT JOIN card_militaries c ON uc.card_military_id = c.id 
            WHERE uc.user_id = @userId AND uc.team_id = @team_id
            GROUP BY c.type;
        ";

            await using MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", User.CurrentUserId);
            command.Parameters.AddWithValue("@team_id", teamId);

            await using MySqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                string type = reader["type"].ToString();
                int number = Convert.ToInt32(reader["number"]);

                result[type] = number;
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return result;
    }
    public async Task<bool> UpdateTeamCardMilitaryAsync(string team_id, string position, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            UPDATE user_card_militaries 
            SET team_id = @team_id, position = @position 
            WHERE user_id = @user_id AND card_military_id = @card_military_id;
        ";

            await using MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@team_id", team_id);
            command.Parameters.AddWithValue("@position", position);
            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            command.Parameters.AddWithValue("@card_military_id", card_id);

            await command.ExecuteNonQueryAsync();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return false;
        }

        return true;
    }
    public async Task<int> GetUserCardMilitariesCountAsync(string user_id, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT COUNT(*) 
            FROM card_militaries c
            JOIN user_card_militaries uc ON c.id = uc.card_military_id
            WHERE uc.user_id = @userId AND c.type = @type AND (@rare = 'All' OR c.rare = @rare);
        ";

            await using MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", user_id);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@rare", rare);

            object result = await command.ExecuteScalarAsync();
            count = Convert.ToInt32(result);
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return count;
    }
    public async Task<int> GetUserCardMilitariesTeamsPositionCountAsync(string user_id, string team_id, string position)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT COUNT(*) 
            FROM user_card_militaries
            WHERE team_id = @team_id 
              AND SUBSTRING_INDEX(position, '-', 1) = @position 
              AND user_id = @userId;
        ";

            await using MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", user_id);
            command.Parameters.AddWithValue("@team_id", team_id);
            command.Parameters.AddWithValue("@position", position);

            object result = await command.ExecuteScalarAsync();
            count = Convert.ToInt32(result);
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return count;
    }
    public async Task<int> GetUserCardMilitariesTeamsCountAsync(string user_id, string team_id)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT COUNT(*) 
            FROM user_card_militaries
            WHERE team_id = @team_id 
              AND user_id = @userId;
        ";

            await using MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", user_id);
            command.Parameters.AddWithValue("@team_id", team_id);

            object result = await command.ExecuteScalarAsync();
            count = Convert.ToInt32(result);
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return count;
    }
    public async Task<bool> InsertUserCardMilitaryAsync(CardMilitaries CardMilitaries)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkQuery = @"
            SELECT COUNT(*) 
            FROM user_card_militaries
            WHERE user_id = @user_id AND card_military_id = @card_military_id;
        ";

            await using MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
            checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            checkCommand.Parameters.AddWithValue("@card_military_id", CardMilitaries.Id);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertQuery = @"
                INSERT INTO user_card_militaries (
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
                );
            ";

                await using MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);

                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@card_military_id", CardMilitaries.Id);
                insertCommand.Parameters.AddWithValue("@rare", CardMilitaries.Rare);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@experiment", 0);
                insertCommand.Parameters.AddWithValue("@star", 0);
                insertCommand.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(CardMilitaries.Rare));
                insertCommand.Parameters.AddWithValue("@block", false);
                insertCommand.Parameters.AddWithValue("@quantity", CardMilitaries.Quantity);
                insertCommand.Parameters.AddWithValue("@power", CardMilitaries.Power);
                insertCommand.Parameters.AddWithValue("@health", CardMilitaries.Health);
                insertCommand.Parameters.AddWithValue("@physical_attack", CardMilitaries.PhysicalAttack);
                insertCommand.Parameters.AddWithValue("@physical_defense", CardMilitaries.PhysicalDefense);
                insertCommand.Parameters.AddWithValue("@magical_attack", CardMilitaries.MagicalAttack);
                insertCommand.Parameters.AddWithValue("@magical_defense", CardMilitaries.MagicalDefense);
                insertCommand.Parameters.AddWithValue("@chemical_attack", CardMilitaries.ChemicalAttack);
                insertCommand.Parameters.AddWithValue("@chemical_defense", CardMilitaries.ChemicalDefense);
                insertCommand.Parameters.AddWithValue("@atomic_attack", CardMilitaries.AtomicAttack);
                insertCommand.Parameters.AddWithValue("@atomic_defense", CardMilitaries.AtomicDefense);
                insertCommand.Parameters.AddWithValue("@mental_attack", CardMilitaries.MentalAttack);
                insertCommand.Parameters.AddWithValue("@mental_defense", CardMilitaries.MentalDefense);
                insertCommand.Parameters.AddWithValue("@speed", CardMilitaries.Speed);
                insertCommand.Parameters.AddWithValue("@critical_damage_rate", CardMilitaries.CriticalDamageRate);
                insertCommand.Parameters.AddWithValue("@critical_rate", CardMilitaries.CriticalRate);
                insertCommand.Parameters.AddWithValue("@critical_resistance_rate", CardMilitaries.CriticalResistanceRate);
                insertCommand.Parameters.AddWithValue("@ignore_critical_rate", CardMilitaries.IgnoreCriticalRate);
                insertCommand.Parameters.AddWithValue("@penetration_rate", CardMilitaries.PenetrationRate);
                insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", CardMilitaries.PenetrationResistanceRate);
                insertCommand.Parameters.AddWithValue("@evasion_rate", CardMilitaries.EvasionRate);
                insertCommand.Parameters.AddWithValue("@damage_absorption_rate", CardMilitaries.DamageAbsorptionRate);
                insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", CardMilitaries.IgnoreDamageAbsorptionRate);
                insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", CardMilitaries.AbsorbedDamageRate);
                insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", CardMilitaries.VitalityRegenerationRate);
                insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", CardMilitaries.VitalityRegenerationResistanceRate);
                insertCommand.Parameters.AddWithValue("@accuracy_rate", CardMilitaries.AccuracyRate);
                insertCommand.Parameters.AddWithValue("@lifesteal_rate", CardMilitaries.LifestealRate);
                insertCommand.Parameters.AddWithValue("@shield_strength", CardMilitaries.ShieldStrength);
                insertCommand.Parameters.AddWithValue("@tenacity", CardMilitaries.Tenacity);
                insertCommand.Parameters.AddWithValue("@resistance_rate", CardMilitaries.ResistanceRate);
                insertCommand.Parameters.AddWithValue("@combo_rate", CardMilitaries.ComboRate);
                insertCommand.Parameters.AddWithValue("@ignore_combo_rate", CardMilitaries.IgnoreComboRate);
                insertCommand.Parameters.AddWithValue("@combo_damage_rate", CardMilitaries.ComboDamageRate);
                insertCommand.Parameters.AddWithValue("@combo_resistance_rate", CardMilitaries.ComboResistanceRate);
                insertCommand.Parameters.AddWithValue("@stun_rate", CardMilitaries.StunRate);
                insertCommand.Parameters.AddWithValue("@ignore_stun_rate", CardMilitaries.IgnoreStunRate);
                insertCommand.Parameters.AddWithValue("@reflection_rate", CardMilitaries.ReflectionRate);
                insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", CardMilitaries.IgnoreReflectionRate);
                insertCommand.Parameters.AddWithValue("@reflection_damage_rate", CardMilitaries.ReflectionDamageRate);
                insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", CardMilitaries.ReflectionResistanceRate);
                insertCommand.Parameters.AddWithValue("@mana", CardMilitaries.Mana);
                insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", CardMilitaries.ManaRegenerationRate);
                insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", CardMilitaries.DamageToDifferentFactionRate);
                insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", CardMilitaries.ResistanceToDifferentFactionRate);
                insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", CardMilitaries.DamageToSameFactionRate);
                insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", CardMilitaries.ResistanceToSameFactionRate);
                insertCommand.Parameters.AddWithValue("@normal_damage_rate", CardMilitaries.NormalDamageRate);
                insertCommand.Parameters.AddWithValue("@normal_resistance_rate", CardMilitaries.NormalResistanceRate);
                insertCommand.Parameters.AddWithValue("@skill_damage_rate", CardMilitaries.SkillDamageRate);
                insertCommand.Parameters.AddWithValue("@skill_resistance_rate", CardMilitaries.SkillResistanceRate);

                await insertCommand.ExecuteNonQueryAsync();
            }
            else
            {
                // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                string updateQuery = @"
                UPDATE user_card_militaries
                SET quantity = @quantity
                WHERE user_id = @user_id AND card_military_id = @card_military_id;
            ";

                await using MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                updateCommand.Parameters.AddWithValue("@card_military_id", CardMilitaries.Id);
                updateCommand.Parameters.AddWithValue("@quantity", CardMilitaries.Quantity);

                await updateCommand.ExecuteNonQueryAsync();
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return false;
        }

        return true;
    }
    public async Task<bool> UpdateCardMilitaryLevelAsync(CardMilitaries CardMilitaries, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            UPDATE user_card_militaries
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
                combo_rate = @combo_rate, ignore_combo_rate = @ignore_combo_rate, combo_damage_rate = @combo_damage_rate, combo_resistance_rate = @combo_resistance_rate,
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
            WHERE user_id = @user_id AND card_military_id = @card_military_id;
        ";

            await using MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            command.Parameters.AddWithValue("@card_military_id", CardMilitaries.Id);
            command.Parameters.AddWithValue("@level", cardLevel);
            command.Parameters.AddWithValue("@power", CardMilitaries.Power);
            command.Parameters.AddWithValue("@health", CardMilitaries.Health);
            command.Parameters.AddWithValue("@physical_attack", CardMilitaries.PhysicalAttack);
            command.Parameters.AddWithValue("@physical_defense", CardMilitaries.PhysicalDefense);
            command.Parameters.AddWithValue("@magical_attack", CardMilitaries.MagicalAttack);
            command.Parameters.AddWithValue("@magical_defense", CardMilitaries.MagicalDefense);
            command.Parameters.AddWithValue("@chemical_attack", CardMilitaries.ChemicalAttack);
            command.Parameters.AddWithValue("@chemical_defense", CardMilitaries.ChemicalDefense);
            command.Parameters.AddWithValue("@atomic_attack", CardMilitaries.AtomicAttack);
            command.Parameters.AddWithValue("@atomic_defense", CardMilitaries.AtomicDefense);
            command.Parameters.AddWithValue("@mental_attack", CardMilitaries.MentalAttack);
            command.Parameters.AddWithValue("@mental_defense", CardMilitaries.MentalDefense);
            command.Parameters.AddWithValue("@speed", CardMilitaries.Speed);
            command.Parameters.AddWithValue("@critical_damage_rate", CardMilitaries.CriticalDamageRate);
            command.Parameters.AddWithValue("@critical_rate", CardMilitaries.CriticalRate);
            command.Parameters.AddWithValue("@critical_resistance_rate", CardMilitaries.CriticalResistanceRate);
            command.Parameters.AddWithValue("@ignore_critical_rate", CardMilitaries.IgnoreCriticalRate);
            command.Parameters.AddWithValue("@penetration_rate", CardMilitaries.PenetrationRate);
            command.Parameters.AddWithValue("@penetration_resistance_rate", CardMilitaries.PenetrationResistanceRate);
            command.Parameters.AddWithValue("@evasion_rate", CardMilitaries.EvasionRate);
            command.Parameters.AddWithValue("@damage_absorption_rate", CardMilitaries.DamageAbsorptionRate);
            command.Parameters.AddWithValue("@ignore_damage_absorption_rate", CardMilitaries.IgnoreDamageAbsorptionRate);
            command.Parameters.AddWithValue("@absorbed_damage_rate", CardMilitaries.AbsorbedDamageRate);
            command.Parameters.AddWithValue("@vitality_regeneration_rate", CardMilitaries.VitalityRegenerationRate);
            command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", CardMilitaries.VitalityRegenerationResistanceRate);
            command.Parameters.AddWithValue("@accuracy_rate", CardMilitaries.AccuracyRate);
            command.Parameters.AddWithValue("@lifesteal_rate", CardMilitaries.LifestealRate);
            command.Parameters.AddWithValue("@shield_strength", CardMilitaries.ShieldStrength);
            command.Parameters.AddWithValue("@tenacity", CardMilitaries.Tenacity);
            command.Parameters.AddWithValue("@resistance_rate", CardMilitaries.ResistanceRate);
            command.Parameters.AddWithValue("@combo_rate", CardMilitaries.ComboRate);
            command.Parameters.AddWithValue("@ignore_combo_rate", CardMilitaries.IgnoreComboRate);
            command.Parameters.AddWithValue("@combo_damage_rate", CardMilitaries.ComboDamageRate);
            command.Parameters.AddWithValue("@combo_resistance_rate", CardMilitaries.ComboResistanceRate);
            command.Parameters.AddWithValue("@stun_rate", CardMilitaries.StunRate);
            command.Parameters.AddWithValue("@ignore_stun_rate", CardMilitaries.IgnoreStunRate);
            command.Parameters.AddWithValue("@reflection_rate", CardMilitaries.ReflectionRate);
            command.Parameters.AddWithValue("@ignore_reflection_rate", CardMilitaries.IgnoreReflectionRate);
            command.Parameters.AddWithValue("@reflection_damage_rate", CardMilitaries.ReflectionDamageRate);
            command.Parameters.AddWithValue("@reflection_resistance_rate", CardMilitaries.ReflectionResistanceRate);
            command.Parameters.AddWithValue("@mana", CardMilitaries.Mana);
            command.Parameters.AddWithValue("@mana_regeneration_rate", CardMilitaries.ManaRegenerationRate);
            command.Parameters.AddWithValue("@damage_to_different_faction_rate", CardMilitaries.DamageToDifferentFactionRate);
            command.Parameters.AddWithValue("@resistance_to_different_faction_rate", CardMilitaries.ResistanceToDifferentFactionRate);
            command.Parameters.AddWithValue("@damage_to_same_faction_rate", CardMilitaries.DamageToSameFactionRate);
            command.Parameters.AddWithValue("@resistance_to_same_faction_rate", CardMilitaries.ResistanceToSameFactionRate);
            command.Parameters.AddWithValue("@normal_damage_rate", CardMilitaries.NormalDamageRate);
            command.Parameters.AddWithValue("@normal_resistance_rate", CardMilitaries.NormalResistanceRate);
            command.Parameters.AddWithValue("@skill_damage_rate", CardMilitaries.SkillDamageRate);
            command.Parameters.AddWithValue("@skill_resistance_rate", CardMilitaries.SkillResistanceRate);

            await command.ExecuteNonQueryAsync();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return false;
        }

        return true;
    }
    public async Task<bool> UpdateCardMilitaryBreakthroughAsync(CardMilitaries CardMilitaries, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            UPDATE user_card_militaries
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
                combo_rate = @combo_rate, ignore_combo_rate = @ignore_combo_rate, combo_damage_rate = @combo_damage_rate, combo_resistance_rate = @combo_resistance_rate,
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
            WHERE user_id = @user_id AND card_military_id = @card_military_id;
        ";

            await using MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            command.Parameters.AddWithValue("@card_military_id", CardMilitaries.Id);
            command.Parameters.AddWithValue("@star", star);
            command.Parameters.AddWithValue("@quantity", quantity);
            command.Parameters.AddWithValue("@power", CardMilitaries.Power);
            command.Parameters.AddWithValue("@health", CardMilitaries.Health);
            command.Parameters.AddWithValue("@physical_attack", CardMilitaries.PhysicalAttack);
            command.Parameters.AddWithValue("@physical_defense", CardMilitaries.PhysicalDefense);
            command.Parameters.AddWithValue("@magical_attack", CardMilitaries.MagicalAttack);
            command.Parameters.AddWithValue("@magical_defense", CardMilitaries.MagicalDefense);
            command.Parameters.AddWithValue("@chemical_attack", CardMilitaries.ChemicalAttack);
            command.Parameters.AddWithValue("@chemical_defense", CardMilitaries.ChemicalDefense);
            command.Parameters.AddWithValue("@atomic_attack", CardMilitaries.AtomicAttack);
            command.Parameters.AddWithValue("@atomic_defense", CardMilitaries.AtomicDefense);
            command.Parameters.AddWithValue("@mental_attack", CardMilitaries.MentalAttack);
            command.Parameters.AddWithValue("@mental_defense", CardMilitaries.MentalDefense);
            command.Parameters.AddWithValue("@speed", CardMilitaries.Speed);
            command.Parameters.AddWithValue("@critical_damage_rate", CardMilitaries.CriticalDamageRate);
            command.Parameters.AddWithValue("@critical_rate", CardMilitaries.CriticalRate);
            command.Parameters.AddWithValue("@critical_resistance_rate", CardMilitaries.CriticalResistanceRate);
            command.Parameters.AddWithValue("@ignore_critical_rate", CardMilitaries.IgnoreCriticalRate);
            command.Parameters.AddWithValue("@penetration_rate", CardMilitaries.PenetrationRate);
            command.Parameters.AddWithValue("@penetration_resistance_rate", CardMilitaries.PenetrationResistanceRate);
            command.Parameters.AddWithValue("@evasion_rate", CardMilitaries.EvasionRate);
            command.Parameters.AddWithValue("@damage_absorption_rate", CardMilitaries.DamageAbsorptionRate);
            command.Parameters.AddWithValue("@ignore_damage_absorption_rate", CardMilitaries.IgnoreDamageAbsorptionRate);
            command.Parameters.AddWithValue("@absorbed_damage_rate", CardMilitaries.AbsorbedDamageRate);
            command.Parameters.AddWithValue("@vitality_regeneration_rate", CardMilitaries.VitalityRegenerationRate);
            command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", CardMilitaries.VitalityRegenerationResistanceRate);
            command.Parameters.AddWithValue("@accuracy_rate", CardMilitaries.AccuracyRate);
            command.Parameters.AddWithValue("@lifesteal_rate", CardMilitaries.LifestealRate);
            command.Parameters.AddWithValue("@shield_strength", CardMilitaries.ShieldStrength);
            command.Parameters.AddWithValue("@tenacity", CardMilitaries.Tenacity);
            command.Parameters.AddWithValue("@resistance_rate", CardMilitaries.ResistanceRate);
            command.Parameters.AddWithValue("@combo_rate", CardMilitaries.ComboRate);
            command.Parameters.AddWithValue("@ignore_combo_rate", CardMilitaries.IgnoreComboRate);
            command.Parameters.AddWithValue("@combo_damage_rate", CardMilitaries.ComboDamageRate);
            command.Parameters.AddWithValue("@combo_resistance_rate", CardMilitaries.ComboResistanceRate);
            command.Parameters.AddWithValue("@stun_rate", CardMilitaries.StunRate);
            command.Parameters.AddWithValue("@ignore_stun_rate", CardMilitaries.IgnoreStunRate);
            command.Parameters.AddWithValue("@reflection_rate", CardMilitaries.ReflectionRate);
            command.Parameters.AddWithValue("@ignore_reflection_rate", CardMilitaries.IgnoreReflectionRate);
            command.Parameters.AddWithValue("@reflection_damage_rate", CardMilitaries.ReflectionDamageRate);
            command.Parameters.AddWithValue("@reflection_resistance_rate", CardMilitaries.ReflectionResistanceRate);
            command.Parameters.AddWithValue("@mana", CardMilitaries.Mana);
            command.Parameters.AddWithValue("@mana_regeneration_rate", CardMilitaries.ManaRegenerationRate);
            command.Parameters.AddWithValue("@damage_to_different_faction_rate", CardMilitaries.DamageToDifferentFactionRate);
            command.Parameters.AddWithValue("@resistance_to_different_faction_rate", CardMilitaries.ResistanceToDifferentFactionRate);
            command.Parameters.AddWithValue("@damage_to_same_faction_rate", CardMilitaries.DamageToSameFactionRate);
            command.Parameters.AddWithValue("@resistance_to_same_faction_rate", CardMilitaries.ResistanceToSameFactionRate);
            command.Parameters.AddWithValue("@normal_damage_rate", CardMilitaries.NormalDamageRate);
            command.Parameters.AddWithValue("@normal_resistance_rate", CardMilitaries.NormalResistanceRate);
            command.Parameters.AddWithValue("@skill_damage_rate", CardMilitaries.SkillDamageRate);
            command.Parameters.AddWithValue("@skill_resistance_rate", CardMilitaries.SkillResistanceRate);

            await command.ExecuteNonQueryAsync();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return false;
        }

        return true;
    }
    public async Task<CardMilitaries> GetUserCardMilitaryByIdAsync(string user_id, string Id)
    {
        CardMilitaries card = new CardMilitaries();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT uc.*, c.image
            FROM user_card_militaries uc
            JOIN card_militaries c ON uc.card_military_id = c.id
            WHERE uc.card_military_id = @id AND uc.user_id = @user_id";

            await using MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", Id);
            command.Parameters.AddWithValue("@user_id", user_id);

            await using MySqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                card = new CardMilitaries
                {
                    Id = reader.GetString("card_military_id"),
                    Image = reader.GetString("image"),
                    Level = reader.GetInt32("level"),
                    Quality = reader.GetDouble("quality"),
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
                    Mana = reader.GetDouble("mana"),
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
                        Mana = reader.GetDouble("mana"),
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

        return card;
    }
    public async Task<List<CardMilitaries>> GetAllUserCardMilitariesInTeamAsync(string user_id)
    {
        List<CardMilitaries> CardMilitaries = new List<CardMilitaries>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string userQuery = @"
            SELECT uc.*, c.name, c.image, c.type, c.description
            FROM user_card_militaries uc
            LEFT JOIN card_militaries c ON uc.card_military_id = c.id 
            WHERE uc.user_id = @user_id AND uc.team_id IS NOT NULL";

            await using MySqlCommand command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", user_id);

            await using MySqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                CardMilitaries admirals = new CardMilitaries
                {
                    Id = reader.GetString("card_military_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetDouble("quality"),
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
                    Mana = reader.GetDouble("mana"),
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
                        Mana = reader.GetDouble("mana"),
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

                CardMilitaries.Add(admirals);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return CardMilitaries;
    }
}