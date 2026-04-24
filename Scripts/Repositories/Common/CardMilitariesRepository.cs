using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class CardMilitariesRepository : ICardMilitariesRepository
{
    public async Task<List<string>> GetUniqueCardMilitariesTypesAsync()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string query = "SELECT DISTINCT type FROM card_militaries";
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
    public async Task<List<string>> GetUniqueCardMilitariesIdAsync()
    {
        List<string> idList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string query = "SELECT DISTINCT id FROM card_militaries";
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
    public async Task<List<CardMilitaries>> GetCardMilitariesAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<CardMilitaries> cardMilitaries = new List<CardMilitaries>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT 
                    ch.*, 
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'id', e.id,
                                'name', e.name,
                                'image', e.image,
                                'type', e.type
                            )
                        )
                        FROM card_military_emblem che
                        JOIN emblems e ON che.emblem_id = e.id
                        WHERE che.card_military_id = ch.id
                    ) AS emblems_json,
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'id', cl.id,
                                'sub_type', cl.sub_type,
                                'sub_image', cl.sub_image,
                                'main_type', cl.main_type,
                                'main_image', cl.main_image
                            )
                        )
                        FROM card_military_class chc
                        JOIN classes cl ON chc.class_id = cl.id
                        WHERE chc.card_military_id = ch.id
                    ) AS classes_json
                FROM card_militaries ch
            WHERE 1=1";

            if (!string.IsNullOrEmpty(type) && type != "All")
            {
                query += " AND ch.type = @type";
            }

            if (!string.IsNullOrEmpty(rare) && rare != "All")
            {
                query += " AND ch.rare = @rare";
            }

            if (!string.IsNullOrEmpty(search))
            {
                query += " AND ch.name LIKE CONCAT('%', @search, '%')";
            }

            query += " ORDER BY ch.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(ch.name, '[0-9]+$') AS UNSIGNED), ch.name";
            query += " LIMIT @limit OFFSET @offset";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@search", search);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@rare", rare);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                CardMilitaries cardMilitary = new CardMilitaries
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

                // Đọc chuỗi JSON từ Database
                string emblemsJson = reader.GetStringSafe("emblems_json");

                if (!string.IsNullOrEmpty(emblemsJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Emblem> trong C#
                        cardMilitary.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Military không có emblem, MySQL sinh ra chuỗi "[null]"
                        cardMilitary.Emblems = new List<Emblems>();
                    }
                }

                string classesJson = reader.GetStringSafe("classes_json");

                if (!string.IsNullOrEmpty(classesJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Classes> trong C#
                        cardMilitary.Classes = JsonHelper.DeserializeClasses(classesJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có class, MySQL sinh ra chuỗi "[null]"
                        cardMilitary.Classes = new List<Classes>();
                    }
                }

                cardMilitaries.Add(cardMilitary);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardMilitaries;
    }
    public async Task<int> GetCardMilitariesCountAsync(string search, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string query = @"SELECT COUNT(*) FROM card_militaries WHERE 1=1";

            if (!string.IsNullOrEmpty(type) && type != "All")
            {
                query += " AND type = @type";
            }

            if (!string.IsNullOrEmpty(rare) && rare != "All")
            {
                query += " AND rare = @rare";
            }

            if (!string.IsNullOrEmpty(search))
            {
                query += " AND name LIKE CONCAT('%', @search, '%')";
            }

            await using var command = new MySqlCommand(query, connection);
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
    public async Task<List<CardMilitaries>> GetCardMilitariesRandomAsync(string type, int pageSize)
    {
        List<CardMilitaries> cardMilitaries = new List<CardMilitaries>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string query = "SELECT * FROM card_militaries WHERE type = @type ORDER BY RAND() LIMIT @limit";
            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@limit", pageSize);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                CardMilitaries cardMilitary = new CardMilitaries
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

                cardMilitaries.Add(cardMilitary);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardMilitaries;
    }
    public async Task<List<CardMilitaries>> GetAllCardMilitariesAsync(string type)
    {
        List<CardMilitaries> cardMilitaries = new List<CardMilitaries>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string query = "SELECT * FROM card_militaries WHERE type = @type";
            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@type", type);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                CardMilitaries CardMilitary = new CardMilitaries
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

                cardMilitaries.Add(CardMilitary);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardMilitaries;
    }
    public async Task<CardMilitaries> GetCardMilitaryByIdAsync(string Id)
    {
        CardMilitaries cardMilitary = new CardMilitaries();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string query = "SELECT * FROM card_militaries WHERE id = @id";
            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", Id);

            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                cardMilitary = new CardMilitaries
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

        return cardMilitary;
    }
    public async Task<List<CardMilitaries>> GetCardMilitariesWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CardMilitaries> cardMilitaries = new List<CardMilitaries>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT m.*, mt.price, cu.image AS currency_image, cu.id AS currency_id
            FROM card_militaries m
            JOIN card_military_trade mt ON m.id = mt.card_military_id
            JOIN currencies cu ON mt.currency_id = cu.id
            WHERE m.type = @type
            ORDER BY m.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name
            LIMIT @limit OFFSET @offset";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var cardMilitary = new CardMilitaries
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
                    Description = reader.GetStringSafe("description"),
                    Currency = new Currencies
                    {
                        Id = reader.GetStringSafe("currency_id"),
                        Image = reader.GetStringSafe("currency_image"),
                        Quantity = reader.GetIntSafe("price")
                    }
                };

                cardMilitaries.Add(cardMilitary);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardMilitaries;
    }
    public async Task<int> GetCardMilitariesWithPriceCountAsync(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT COUNT(*)
            FROM card_militaries m
            JOIN card_military_trade mt ON m.id = mt.card_military_id
            JOIN currencies cu ON mt.currency_id = cu.id
            WHERE m.type = @type;";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@type", type);

            var result = await command.ExecuteScalarAsync();
            count = Convert.ToInt32(result);
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return count;
    }
}