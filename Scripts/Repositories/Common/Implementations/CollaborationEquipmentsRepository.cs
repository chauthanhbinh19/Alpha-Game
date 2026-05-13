using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class CollaborationEquipmentsRepository : ICollaborationEquipmentsRepository
{
    public async Task<List<string>> GetUniqueCollaborationEquipmentsTypesAsync()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string selectSQL = "SELECT DISTINCT type FROM collaboration_equipments";
            await using var selectCommand = new MySqlCommand(selectSQL, connection);
            await using var reader = await selectCommand.ExecuteReaderAsync();

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
    public async Task<List<string>> GetUniqueCollaborationEquipmentsIdAsync()
    {
        List<string> idList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string selectSQL = "SELECT DISTINCT id FROM collaboration_equipments";
            await using var selectCommand = new MySqlCommand(selectSQL, connection);
            await using var reader = await selectCommand.ExecuteReaderAsync();

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
    public async Task<List<CollaborationEquipments>> GetCollaborationEquipmentsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<CollaborationEquipments> collaborationEquipments = new List<CollaborationEquipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT *
            FROM collaboration_equipments
            WHERE 1=1";

            if (!string.IsNullOrEmpty(type) && type != "All")
            {
                selectSQL += " AND type = @type";
            }

            if (!string.IsNullOrEmpty(rare) && rare != "All")
            {
                selectSQL += " AND rare = @rare";
            }

            if (!string.IsNullOrEmpty(search))
            {
                selectSQL += " AND name LIKE CONCAT('%', @search, '%')";
            }

            selectSQL += " ORDER BY name";
            selectSQL += " LIMIT @limit OFFSET @offset";

            await using var selectCommand = new MySqlCommand(selectSQL, connection);
            
            if (!string.IsNullOrEmpty(type) && type != "All")
            {
                selectCommand.Parameters.AddWithValue("@type", type);
            }

            if (!string.IsNullOrEmpty(rare) && rare != "All")
            {
                selectCommand.Parameters.AddWithValue("@rare", rare);
            }

            if (!string.IsNullOrEmpty(search))
            {
                selectCommand.Parameters.AddWithValue("@search", search);
            }

            selectCommand.Parameters.AddWithValue("@limit", pageSize);
            selectCommand.Parameters.AddWithValue("@offset", offset);

            await using var reader = await selectCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var collaborationEquipment = new CollaborationEquipments
                {
                    Id = reader.GetStringSafe("id"),
                    Name = reader.GetStringSafe("name"),
                    Image = reader.GetStringSafe("image"),
                    Rare = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Type = reader.GetStringSafe("type"),
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
                    Description = reader.GetStringSafe("description")
                };

                collaborationEquipments.Add(collaborationEquipment);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return collaborationEquipments;
    }
    public async Task<List<CollaborationEquipments>> GetCollaborationEquipmentsWithoutLimitAsync()
    {
        List<CollaborationEquipments> collaborationEquipments = new List<CollaborationEquipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"SELECT * FROM collaboration_equipments";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            CollaborationEquipments collaborationEquipment = new CollaborationEquipments
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Quality = reader.GetDoubleSafe("quality"),
                            };

                            collaborationEquipments.Add(collaborationEquipment);
                        }
                    }
                }
            }
            catch (MySqlConnector.MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return collaborationEquipments;
    }
    public async Task<int> GetCollaborationEquipmentsCountAsync(string search, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string selectSQL = @"SELECT COUNT(*) FROM collaboration_equipments WHERE 1=1";
            
            if (!string.IsNullOrEmpty(type) && type != "All")
            {
                selectSQL += " AND type = @type";
            }

            if (!string.IsNullOrEmpty(rare) && rare != "All")
            {
                selectSQL += " AND rare = @rare";
            }

            if (!string.IsNullOrEmpty(search))
            {
                selectSQL += " AND name LIKE CONCAT('%', @search, '%')";
            }

            await using var selectCommand = new MySqlCommand(selectSQL, connection);
            if (!string.IsNullOrEmpty(type) && type != "All")
            {
                selectCommand.Parameters.AddWithValue("@type", type);
            }

            if (!string.IsNullOrEmpty(rare) && rare != "All")
            {
                selectCommand.Parameters.AddWithValue("@rare", rare);
            }

            if (!string.IsNullOrEmpty(search))
            {
                selectCommand.Parameters.AddWithValue("@search", search);
            }

            object result = await selectCommand.ExecuteScalarAsync();
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
    public async Task<List<CollaborationEquipments>> GetCollaborationEquipmentsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CollaborationEquipments> collaborationEquipments = new List<CollaborationEquipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT c.*, ct.price, cu.image AS currency_image, cu.id AS currency_id
            FROM collaboration_equipments c
            JOIN collaboration_equipment_trade ct ON c.id = ct.collaboration_equipment_id
            JOIN currencies cu ON ct.currency_id = cu.id
            WHERE c.type = @type
            ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name
            LIMIT @limit OFFSET @offset;";

            await using var selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@type", type);
            selectCommand.Parameters.AddWithValue("@limit", pageSize);
            selectCommand.Parameters.AddWithValue("@offset", offset);

            await using var reader = await selectCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var collaborationEquipment = new CollaborationEquipments
                {
                    Id = reader.GetStringSafe("id"),
                    Name = reader.GetStringSafe("name"),
                    Image = reader.GetStringSafe("image"),
                    Rare = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Type = reader.GetStringSafe("type"),
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
                    Description = reader.GetStringSafe("description")
                };

                collaborationEquipment.Currency = new Currencies
                {
                    Id = reader.GetStringSafe("currency_id"),
                    Image = reader.GetStringSafe("currency_image"),
                    Quantity = reader.GetIntSafe("price")
                };

                collaborationEquipments.Add(collaborationEquipment);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return collaborationEquipments;
    }
    public async Task<int> GetCollaborationEquipmentsWithPriceCountAsync(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT COUNT(*)
            FROM collaboration_equipments c
            JOIN collaboration_equipment_trade ct ON c.id = ct.collaboration_equipment_id
            JOIN currencies cu ON ct.currency_id = cu.id
            WHERE c.type = @type;";

            await using var selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@type", type);

            var result = await selectCommand.ExecuteScalarAsync();
            count = Convert.ToInt32(result);
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return count;
    }
    public async Task<CollaborationEquipments> GetCollaborationEquipmentByIdAsync(string id)
    {
        CollaborationEquipments collaborationEquipment = new CollaborationEquipments();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string selectSQL = "SELECT * FROM collaboration_equipments WHERE id = @id";
            await using var selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@id", id);

            await using var reader = await selectCommand.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                collaborationEquipment = new CollaborationEquipments
                {
                    Id = reader.GetStringSafe("id"),
                    Name = reader.GetStringSafe("name"),
                    Image = reader.GetStringSafe("image"),
                    Rare = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Type = reader.GetStringSafe("type"),
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
                    Description = reader.GetStringSafe("description")
                };
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return collaborationEquipment;
    }
}