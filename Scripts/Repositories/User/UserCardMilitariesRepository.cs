using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UserCardMilitariesRepository : IUserCardMilitariesRepository
{
    public async Task<List<CardMilitaries>> GetUserCardMilitariesAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
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
            WHERE uc.user_id = @userId 
                AND (@type = 'All' OR c.type = @type)
                AND (@rare = 'All' or c.rare = @rare)
                AND (@search = '' OR c.name LIKE CONCAT('%', @search, '%'))
            ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name
            LIMIT @limit OFFSET @offset;
        ";

            await using MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", user_id);
            command.Parameters.AddWithValue("@search", search);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@rare", rare);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            await using MySqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                CardMilitaries captain = new CardMilitaries
                {
                    Id = reader.GetStringSafe("card_military_id"),
                    Name = reader.GetStringSafe("name"),
                    Image = reader.GetStringSafe("image"),
                    Rare = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Type = reader.GetStringSafe("type"),
                    Star = reader.GetIntSafe("star"),
                    Level = reader.GetIntSafe("level"),
                    Experiment = reader.GetDoubleSafe("experiment"),
                    Quantity = reader.GetDoubleSafe("quantity"),
                    Block = reader.GetBoolean("block"),
                    TeamId = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetStringSafe("team_id"),
                    Position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetStringSafe("position"),

                    Power = reader.GetDoubleSafe("power"),
                    Health = reader.GetDoubleSafe("health"),
                    PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                    PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                    MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                    MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                    ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                    ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                    AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                    AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                    MentalAttack = reader.GetDoubleSafe("mental_attack"),
                    MentalDefense = reader.GetDoubleSafe("mental_defense"),
                    Speed = reader.GetDoubleSafe("speed"),
                    CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                    CriticalRate = reader.GetDoubleSafe("critical_rate"),
                    CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                    PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                    EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                    LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                    ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                    Tenacity = reader.GetDoubleSafe("tenacity"),
                    ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                    ComboRate = reader.GetDoubleSafe("combo_rate"),
                    IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                    StunRate = reader.GetDoubleSafe("stun_rate"),
                    IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                    ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                    Mana = reader.GetDoubleSafe("mana"),
                    ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                    Description = reader.GetStringSafe("description"),

                    Team = new Teams
                    {
                        TeamNumber = reader.GetIntSafe("team_number")
                    },

                    BaseStats = new BaseStats
                    {
                        Power = reader.GetDoubleSafe("power"),
                        Health = reader.GetDoubleSafe("health"),
                        PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                        PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                        MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                        MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                        ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                        ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                        AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                        AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                        MentalAttack = reader.GetDoubleSafe("mental_attack"),
                        MentalDefense = reader.GetDoubleSafe("mental_defense"),
                        Speed = reader.GetDoubleSafe("speed"),
                        CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                        CriticalRate = reader.GetDoubleSafe("critical_rate"),
                        CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                        IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                        PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                        PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                        EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                        DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                        IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                        AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                        VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                        VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                        AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                        LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                        ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                        Tenacity = reader.GetDoubleSafe("tenacity"),
                        ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                        ComboRate = reader.GetDoubleSafe("combo_rate"),
                        IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                        ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                        ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                        StunRate = reader.GetDoubleSafe("stun_rate"),
                        IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                        ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                        IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                        ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                        ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                        Mana = reader.GetDoubleSafe("mana"),
                        ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                        DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                        ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                        DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                        ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                        NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                        NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                        SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                        SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
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
                    Id = reader.GetStringSafe("card_military_id"),
                    Name = reader.GetStringSafe("name"),
                    Image = reader.GetStringSafe("image"),
                    Rare = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Type = reader.GetStringSafe("type"),
                    Star = reader.GetIntSafe("star"),
                    Level = reader.GetIntSafe("level"),
                    Experiment = reader.GetDoubleSafe("experiment"),
                    Quantity = reader.GetDoubleSafe("quantity"),
                    Block = reader.GetBoolean("block"),
                    TeamId = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetStringSafe("team_id"),
                    Position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetStringSafe("position"),

                    Power = reader.GetDoubleSafe("power"),
                    Health = reader.GetDoubleSafe("health"),
                    PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                    PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                    MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                    MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                    ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                    ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                    AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                    AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                    MentalAttack = reader.GetDoubleSafe("mental_attack"),
                    MentalDefense = reader.GetDoubleSafe("mental_defense"),
                    Speed = reader.GetDoubleSafe("speed"),
                    CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                    CriticalRate = reader.GetDoubleSafe("critical_rate"),
                    CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                    PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                    EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                    LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                    ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                    Tenacity = reader.GetDoubleSafe("tenacity"),
                    ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                    ComboRate = reader.GetDoubleSafe("combo_rate"),
                    IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                    StunRate = reader.GetDoubleSafe("stun_rate"),
                    IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                    ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                    Mana = reader.GetDoubleSafe("mana"),
                    ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                    Description = reader.GetStringSafe("description"),

                    BaseStats = new BaseStats
                    {
                        Power = reader.GetDoubleSafe("power"),
                        Health = reader.GetDoubleSafe("health"),
                        PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                        PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                        MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                        MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                        ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                        ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                        AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                        AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                        MentalAttack = reader.GetDoubleSafe("mental_attack"),
                        MentalDefense = reader.GetDoubleSafe("mental_defense"),
                        Speed = reader.GetDoubleSafe("speed"),
                        CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                        CriticalRate = reader.GetDoubleSafe("critical_rate"),
                        CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                        IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                        PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                        PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                        EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                        DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                        IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                        AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                        VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                        VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                        AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                        LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                        ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                        Tenacity = reader.GetDoubleSafe("tenacity"),
                        ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                        ComboRate = reader.GetDoubleSafe("combo_rate"),
                        IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                        ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                        ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                        StunRate = reader.GetDoubleSafe("stun_rate"),
                        IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                        ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                        IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                        ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                        ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                        Mana = reader.GetDoubleSafe("mana"),
                        ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                        DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                        ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                        DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                        ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                        NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                        NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                        SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                        SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
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
                    Id = reader.GetStringSafe("card_military_id"),
                    Name = reader.GetStringSafe("name"),
                    Image = reader.GetStringSafe("image"),
                    Rare = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Type = reader.GetStringSafe("type"),
                    Star = reader.GetIntSafe("star"),
                    Level = reader.GetIntSafe("level"),
                    Experiment = reader.GetDoubleSafe("experiment"),
                    Quantity = reader.GetDoubleSafe("quantity"),
                    Block = reader.GetBoolean("block"),
                    TeamId = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetStringSafe("team_id"),
                    Position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetStringSafe("position"),

                    Power = reader.GetDoubleSafe("power"),
                    Health = reader.GetDoubleSafe("health"),
                    PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                    PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                    MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                    MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                    ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                    ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                    AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                    AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                    MentalAttack = reader.GetDoubleSafe("mental_attack"),
                    MentalDefense = reader.GetDoubleSafe("mental_defense"),
                    Speed = reader.GetDoubleSafe("speed"),
                    CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                    CriticalRate = reader.GetDoubleSafe("critical_rate"),
                    CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                    PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                    EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                    LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                    ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                    Tenacity = reader.GetDoubleSafe("tenacity"),
                    ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                    ComboRate = reader.GetDoubleSafe("combo_rate"),
                    IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                    StunRate = reader.GetDoubleSafe("stun_rate"),
                    IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                    ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                    Mana = reader.GetDoubleSafe("mana"),
                    ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                    Description = reader.GetStringSafe("description"),

                    BaseStats = new BaseStats
                    {
                        Power = reader.GetDoubleSafe("power"),
                        Health = reader.GetDoubleSafe("health"),
                        PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                        PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                        MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                        MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                        ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                        ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                        AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                        AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                        MentalAttack = reader.GetDoubleSafe("mental_attack"),
                        MentalDefense = reader.GetDoubleSafe("mental_defense"),
                        Speed = reader.GetDoubleSafe("speed"),
                        CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                        CriticalRate = reader.GetDoubleSafe("critical_rate"),
                        CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                        IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                        PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                        PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                        EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                        DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                        IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                        AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                        VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                        VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                        AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                        LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                        ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                        Tenacity = reader.GetDoubleSafe("tenacity"),
                        ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                        ComboRate = reader.GetDoubleSafe("combo_rate"),
                        IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                        ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                        ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                        StunRate = reader.GetDoubleSafe("stun_rate"),
                        IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                        ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                        IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                        ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                        ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                        Mana = reader.GetDoubleSafe("mana"),
                        ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                        DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                        ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                        DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                        ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                        NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                        NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                        SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                        SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
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
    public async Task<int> GetUserCardMilitariesCountAsync(string user_id, string search, string type, string rare)
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
            WHERE uc.user_id = @userId 
                AND (@type = 'All' OR c.type = @type)
                AND (@rare = 'All' OR c.rare = @rare)
                AND (@search = '' OR c.name LIKE CONCAT('%', @search, '%'));
        ";

            await using MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", user_id);
            command.Parameters.AddWithValue("@search", search);
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
    public async Task<bool> InsertUserCardMilitaryAsync(CardMilitaries cardMilitary)
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
            checkCommand.Parameters.AddWithValue("@card_military_id", cardMilitary.Id);

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
                insertCommand.Parameters.AddWithValue("@card_military_id", cardMilitary.Id);
                insertCommand.Parameters.AddWithValue("@rare", cardMilitary.Rare);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@experiment", 0);
                insertCommand.Parameters.AddWithValue("@star", 0);
                insertCommand.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(cardMilitary.Rare));
                insertCommand.Parameters.AddWithValue("@block", false);
                insertCommand.Parameters.AddWithValue("@quantity", cardMilitary.Quantity);
                insertCommand.Parameters.AddWithValue("@power", cardMilitary.Power);
                insertCommand.Parameters.AddWithValue("@health", cardMilitary.Health);
                insertCommand.Parameters.AddWithValue("@physical_attack", cardMilitary.PhysicalAttack);
                insertCommand.Parameters.AddWithValue("@physical_defense", cardMilitary.PhysicalDefense);
                insertCommand.Parameters.AddWithValue("@magical_attack", cardMilitary.MagicalAttack);
                insertCommand.Parameters.AddWithValue("@magical_defense", cardMilitary.MagicalDefense);
                insertCommand.Parameters.AddWithValue("@chemical_attack", cardMilitary.ChemicalAttack);
                insertCommand.Parameters.AddWithValue("@chemical_defense", cardMilitary.ChemicalDefense);
                insertCommand.Parameters.AddWithValue("@atomic_attack", cardMilitary.AtomicAttack);
                insertCommand.Parameters.AddWithValue("@atomic_defense", cardMilitary.AtomicDefense);
                insertCommand.Parameters.AddWithValue("@mental_attack", cardMilitary.MentalAttack);
                insertCommand.Parameters.AddWithValue("@mental_defense", cardMilitary.MentalDefense);
                insertCommand.Parameters.AddWithValue("@speed", cardMilitary.Speed);
                insertCommand.Parameters.AddWithValue("@critical_damage_rate", cardMilitary.CriticalDamageRate);
                insertCommand.Parameters.AddWithValue("@critical_rate", cardMilitary.CriticalRate);
                insertCommand.Parameters.AddWithValue("@critical_resistance_rate", cardMilitary.CriticalResistanceRate);
                insertCommand.Parameters.AddWithValue("@ignore_critical_rate", cardMilitary.IgnoreCriticalRate);
                insertCommand.Parameters.AddWithValue("@penetration_rate", cardMilitary.PenetrationRate);
                insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", cardMilitary.PenetrationResistanceRate);
                insertCommand.Parameters.AddWithValue("@evasion_rate", cardMilitary.EvasionRate);
                insertCommand.Parameters.AddWithValue("@damage_absorption_rate", cardMilitary.DamageAbsorptionRate);
                insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardMilitary.IgnoreDamageAbsorptionRate);
                insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", cardMilitary.AbsorbedDamageRate);
                insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", cardMilitary.VitalityRegenerationRate);
                insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardMilitary.VitalityRegenerationResistanceRate);
                insertCommand.Parameters.AddWithValue("@accuracy_rate", cardMilitary.AccuracyRate);
                insertCommand.Parameters.AddWithValue("@lifesteal_rate", cardMilitary.LifestealRate);
                insertCommand.Parameters.AddWithValue("@shield_strength", cardMilitary.ShieldStrength);
                insertCommand.Parameters.AddWithValue("@tenacity", cardMilitary.Tenacity);
                insertCommand.Parameters.AddWithValue("@resistance_rate", cardMilitary.ResistanceRate);
                insertCommand.Parameters.AddWithValue("@combo_rate", cardMilitary.ComboRate);
                insertCommand.Parameters.AddWithValue("@ignore_combo_rate", cardMilitary.IgnoreComboRate);
                insertCommand.Parameters.AddWithValue("@combo_damage_rate", cardMilitary.ComboDamageRate);
                insertCommand.Parameters.AddWithValue("@combo_resistance_rate", cardMilitary.ComboResistanceRate);
                insertCommand.Parameters.AddWithValue("@stun_rate", cardMilitary.StunRate);
                insertCommand.Parameters.AddWithValue("@ignore_stun_rate", cardMilitary.IgnoreStunRate);
                insertCommand.Parameters.AddWithValue("@reflection_rate", cardMilitary.ReflectionRate);
                insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", cardMilitary.IgnoreReflectionRate);
                insertCommand.Parameters.AddWithValue("@reflection_damage_rate", cardMilitary.ReflectionDamageRate);
                insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", cardMilitary.ReflectionResistanceRate);
                insertCommand.Parameters.AddWithValue("@mana", cardMilitary.Mana);
                insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", cardMilitary.ManaRegenerationRate);
                insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", cardMilitary.DamageToDifferentFactionRate);
                insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardMilitary.ResistanceToDifferentFactionRate);
                insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", cardMilitary.DamageToSameFactionRate);
                insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardMilitary.ResistanceToSameFactionRate);
                insertCommand.Parameters.AddWithValue("@normal_damage_rate", cardMilitary.NormalDamageRate);
                insertCommand.Parameters.AddWithValue("@normal_resistance_rate", cardMilitary.NormalResistanceRate);
                insertCommand.Parameters.AddWithValue("@skill_damage_rate", cardMilitary.SkillDamageRate);
                insertCommand.Parameters.AddWithValue("@skill_resistance_rate", cardMilitary.SkillResistanceRate);

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
                updateCommand.Parameters.AddWithValue("@card_military_id", cardMilitary.Id);
                updateCommand.Parameters.AddWithValue("@quantity", cardMilitary.Quantity);

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
    public async Task<bool> UpdateCardMilitaryLevelAsync(CardMilitaries cardMilitary, int cardLevel)
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

            await command.ExecuteNonQueryAsync();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return false;
        }

        return true;
    }
    public async Task<bool> UpdateCardMilitaryBreakthroughAsync(CardMilitaries cardMilitary, int star, double quantity)
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
                    Id = reader.GetStringSafe("card_military_id"),
                    Image = reader.GetStringSafe("image"),
                    Level = reader.GetIntSafe("level"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Experiment = reader.GetDoubleSafe("experiment"),
                    Star = reader.GetIntSafe("star"),
                    Power = reader.GetDoubleSafe("power"),
                    Health = reader.GetDoubleSafe("health"),
                    PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                    PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                    MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                    MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                    ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                    ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                    AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                    AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                    MentalAttack = reader.GetDoubleSafe("mental_attack"),
                    MentalDefense = reader.GetDoubleSafe("mental_defense"),
                    Speed = reader.GetDoubleSafe("speed"),
                    CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                    CriticalRate = reader.GetDoubleSafe("critical_rate"),
                    CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                    PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                    EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                    LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                    ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                    Tenacity = reader.GetDoubleSafe("tenacity"),
                    ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                    ComboRate = reader.GetDoubleSafe("combo_rate"),
                    IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                    StunRate = reader.GetDoubleSafe("stun_rate"),
                    IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                    ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                    Mana = reader.GetDoubleSafe("mana"),
                    ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),

                    BaseStats = new BaseStats
                    {
                        Power = reader.GetDoubleSafe("power"),
                        Health = reader.GetDoubleSafe("health"),
                        PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                        PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                        MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                        MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                        ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                        ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                        AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                        AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                        MentalAttack = reader.GetDoubleSafe("mental_attack"),
                        MentalDefense = reader.GetDoubleSafe("mental_defense"),
                        Speed = reader.GetDoubleSafe("speed"),
                        CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                        CriticalRate = reader.GetDoubleSafe("critical_rate"),
                        CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                        IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                        PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                        PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                        EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                        DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                        IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                        AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                        VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                        VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                        AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                        LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                        ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                        Tenacity = reader.GetDoubleSafe("tenacity"),
                        ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                        ComboRate = reader.GetDoubleSafe("combo_rate"),
                        IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                        ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                        ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                        StunRate = reader.GetDoubleSafe("stun_rate"),
                        IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                        ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                        IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                        ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                        ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                        Mana = reader.GetDoubleSafe("mana"),
                        ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                        DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                        ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                        DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                        ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                        NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                        NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                        SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                        SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
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
                    Id = reader.GetStringSafe("card_military_id"),
                    Name = reader.GetStringSafe("name"),
                    Image = reader.GetStringSafe("image"),
                    Rare = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Type = reader.GetStringSafe("type"),
                    Star = reader.GetIntSafe("star"),
                    Level = reader.GetIntSafe("level"),
                    Experiment = reader.GetIntSafe("experiment"),
                    Quantity = reader.GetIntSafe("quantity"),
                    Block = reader.GetBoolean("block"),
                    TeamId = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetStringSafe("team_id"),
                    Position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetStringSafe("position"),
                    Power = reader.GetDoubleSafe("power"),
                    Health = reader.GetDoubleSafe("health"),
                    PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                    PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                    MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                    MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                    ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                    ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                    AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                    AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                    MentalAttack = reader.GetDoubleSafe("mental_attack"),
                    MentalDefense = reader.GetDoubleSafe("mental_defense"),
                    Speed = reader.GetDoubleSafe("speed"),
                    CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                    CriticalRate = reader.GetDoubleSafe("critical_rate"),
                    CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                    PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                    EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                    LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                    ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                    Tenacity = reader.GetDoubleSafe("tenacity"),
                    ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                    ComboRate = reader.GetDoubleSafe("combo_rate"),
                    IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                    StunRate = reader.GetDoubleSafe("stun_rate"),
                    IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                    ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                    Mana = reader.GetDoubleSafe("mana"),
                    ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                    Description = reader.GetStringSafe("description"),

                    BaseStats = new BaseStats
                    {
                        Power = reader.GetDoubleSafe("power"),
                        Health = reader.GetDoubleSafe("health"),
                        PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                        PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                        MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                        MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                        ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                        ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                        AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                        AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                        MentalAttack = reader.GetDoubleSafe("mental_attack"),
                        MentalDefense = reader.GetDoubleSafe("mental_defense"),
                        Speed = reader.GetDoubleSafe("speed"),
                        CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                        CriticalRate = reader.GetDoubleSafe("critical_rate"),
                        CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                        IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                        PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                        PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                        EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                        DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                        IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                        AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                        VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                        VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                        AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                        LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                        ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                        Tenacity = reader.GetDoubleSafe("tenacity"),
                        ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                        ComboRate = reader.GetDoubleSafe("combo_rate"),
                        IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                        ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                        ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                        StunRate = reader.GetDoubleSafe("stun_rate"),
                        IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                        ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                        IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                        ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                        ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                        Mana = reader.GetDoubleSafe("mana"),
                        ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                        DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                        ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                        DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                        ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                        NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                        NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                        SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                        SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
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