using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class ArtworksRepository : IArtworksRepository
{
    public async Task<List<string>> GetUniqueArtworksTypesAsync()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            string query = "SELECT DISTINCT type FROM Artworks";
            await using var command = new MySqlCommand(query, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                typeList.Add(reader.GetString(0));
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return typeList;
    }
    public async Task<List<string>> GetUniqueArtworksIdAsync()
    {
        List<string> idList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            string query = "SELECT DISTINCT id FROM Artworks";
            await using var command = new MySqlCommand(query, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                idList.Add(reader.GetString(0));
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return idList;
    }
    public async Task<List<Artworks>> GetArtworksAsync(string type, int pageSize, int offset, string rare)
    {
        List<Artworks> artworks = new List<Artworks>();
        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            string query = @"
            SELECT * FROM Artworks 
            WHERE type = @type AND (@rare = 'All' OR rare = @rare)
            ORDER BY Artworks.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(Artworks.name, '[0-9]+$') AS UNSIGNED), Artworks.name
            LIMIT @limit OFFSET @offset";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@rare", rare);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Artworks artwork = new Artworks
                {
                    Id = reader.GetString("id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetDouble("quality"),
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
                    PercentAllHealth = reader.GetDouble("percent_all_health"),
                    PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack"),
                    PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense"),
                    PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack"),
                    PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense"),
                    PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack"),
                    PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense"),
                    PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack"),
                    PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense"),
                    PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack"),
                    PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense"),
                    Description = reader.GetString("description")
                };

                artworks.Add(artwork);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return artworks;
    }
    public async Task<int> GetArtworksCountAsync(string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            string query = "SELECT COUNT(*) FROM Artworks WHERE type = @type AND (@rare = 'All' OR rare = @rare)";
            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@rare", rare);

            object result = await command.ExecuteScalarAsync();
            if (result != null && result != DBNull.Value)
            {
                count = Convert.ToInt32(result);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return count;
    }
    public async Task<List<Artworks>> GetArtworksWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Artworks> artworks = new List<Artworks>();
        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            string query = @"
            SELECT m.*, mt.price, cu.image AS currency_image, cu.id AS currency_id
            FROM Artworks m
            JOIN Artwork_trade mt ON m.id = mt.Artwork_id
            JOIN currencies cu ON mt.currency_id = cu.id
            WHERE m.type = @type
            ORDER BY m.name REGEXP '[0-9]+$',
                     CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED),
                     m.name
            LIMIT @limit OFFSET @offset;";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Artworks artwork = new Artworks
                {
                    Id = reader.GetString("id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetDouble("quality"),
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
                    PercentAllHealth = reader.GetDouble("percent_all_health"),
                    PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack"),
                    PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense"),
                    PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack"),
                    PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense"),
                    PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack"),
                    PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense"),
                    PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack"),
                    PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense"),
                    PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack"),
                    PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense"),
                    Description = reader.GetString("description"),
                    Currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetInt32("price")
                    }
                };

                artworks.Add(artwork);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return artworks;
    }
    public async Task<int> GetArtworksWithPriceCountAsync(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            string query = @"
            SELECT COUNT(*)
            FROM Artworks m
            JOIN Artwork_trade mt ON m.id = mt.Artwork_id
            JOIN currencies cu ON mt.currency_id = cu.id
            WHERE m.type = @type;";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@type", type);

            object result = await command.ExecuteScalarAsync();
            count = Convert.ToInt32(result);
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return count;
    }
    public async Task<Artworks> GetArtworkByIdAsync(string id)
    {
        Artworks artwork = new Artworks();
        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            string query = "SELECT * FROM Artworks WHERE id = @id";
            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                artwork = new Artworks
                {
                    Id = reader.GetString("id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetDouble("quality"),
                    Type = reader.GetString("type"),
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
                    Description = reader.GetString("description")
                };
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return artwork;
    }
    public async Task<Artworks> SumPowerArtworksPercentAsync()
    {
        Artworks sumArtworks = new Artworks();
        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            string query = @"
            SELECT 
                SUM(a.percent_all_health) AS total_percent_all_health,
                SUM(a.percent_all_physical_attack) AS total_percent_all_physical_attack,
                SUM(a.percent_all_physical_defense) AS total_percent_all_physical_defense,
                SUM(a.percent_all_magical_attack) AS total_percent_all_magical_attack,
                SUM(a.percent_all_magical_defense) AS total_percent_all_magical_defense,
                SUM(a.percent_all_chemical_attack) AS total_percent_all_chemical_attack,
                SUM(a.percent_all_chemical_defense) AS total_percent_all_chemical_defense,
                SUM(a.percent_all_atomic_attack) AS total_percent_all_atomic_attack,
                SUM(a.percent_all_atomic_defense) AS total_percent_all_atomic_defense,
                SUM(a.percent_all_mental_attack) AS total_percent_all_mental_attack,
                SUM(a.percent_all_mental_defense) AS total_percent_all_mental_defense
            FROM Artworks a
            INNER JOIN user_Artworks ua ON a.id = ua.Artwork_id
            WHERE ua.user_id = @user_id;
        ";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                sumArtworks.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDouble("total_percent_all_health");
                sumArtworks.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDouble("total_percent_all_physical_attack");
                sumArtworks.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDouble("total_percent_all_physical_defense");
                sumArtworks.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDouble("total_percent_all_magical_attack");
                sumArtworks.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDouble("total_percent_all_magical_defense");
                sumArtworks.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDouble("total_percent_all_chemical_attack");
                sumArtworks.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDouble("total_percent_all_chemical_defense");
                sumArtworks.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDouble("total_percent_all_atomic_attack");
                sumArtworks.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDouble("total_percent_all_atomic_defense");
                sumArtworks.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDouble("total_percent_all_mental_attack");
                sumArtworks.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDouble("total_percent_all_mental_defense");
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return sumArtworks;
    }
}