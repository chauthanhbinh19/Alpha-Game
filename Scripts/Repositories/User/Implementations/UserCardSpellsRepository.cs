using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;

public class UserCardSpellsRepository : IUserCardSpellsRepository
{
    public async Task<List<CardSpells>> GetUserCardSpellsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardSpells> cardSpells = new List<CardSpells>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT 
                    uc.*, 
                    c.name, 
                    c.image, 
                    c.type, 
                    c.description, 
                    COALESCE(t.team_number, 0) AS team_number,
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'id', e.id,
                                'name', e.name,
                                'image', e.image,
                                'type', e.type
                            )
                        )
                        FROM card_spell_emblem che
                        JOIN emblems e ON che.emblem_id = e.id
                        WHERE che.card_spell_id = c.id
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
                        FROM card_spell_class chc
                        JOIN classes cl ON chc.class_id = cl.id
                        WHERE chc.card_spell_id = c.id
                    ) AS classes_json
                FROM user_card_spells uc
                LEFT JOIN card_spells c ON c.id = uc.card_spell_id 
                LEFT JOIN teams t ON t.team_id = uc.team_id
            WHERE uc.user_id = @userId 
        ";
            if (!string.IsNullOrEmpty(type) && type != "All")
            {
                selectSQL += " AND c.type = @type";
            }

            if (!string.IsNullOrEmpty(rare) && rare != "All")
            {
                selectSQL += " AND c.rare = @rare";
            }

            if (!string.IsNullOrEmpty(search))
            {
                selectSQL += " AND c.name LIKE CONCAT('%', @search, '%')";
            }

            selectSQL += " ORDER BY c.name";
            selectSQL += " LIMIT @limit OFFSET @offset";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@userId", user_id);
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

            await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                CardSpells cardSpell = new CardSpells
                {
                    Id = reader.GetStringSafe("card_spell_id"),
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

                // Đọc chuỗi JSON từ Database
                string emblemsJson = reader.GetStringSafe("emblems_json");

                if (!string.IsNullOrEmpty(emblemsJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Emblem> trong C#
                        cardSpell.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                        cardSpell.Emblems = new List<Emblems>();
                    }
                }

                string classesJson = reader.GetStringSafe("classes_json");

                if (!string.IsNullOrEmpty(classesJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Classes> trong C#
                        cardSpell.Classes = JsonHelper.DeserializeClasses(classesJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có class, MySQL sinh ra chuỗi "[null]"
                        cardSpell.Classes = new List<Classes>();
                    }
                }

                cardSpells.Add(cardSpell);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardSpells;
    }
    public async Task<List<CardSpells>> GetUserCardSpellsTeamAsync(string user_id, string teamId, string position)
    {
        List<CardSpells> cardSpells = new List<CardSpells>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT 
                    uc.*, 
                    c.name, 
                    c.image, 
                    c.type, 
                    c.description, 
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'id', e.id,
                                'name', e.name,
                                'image', e.image,
                                'type', e.type
                            )
                        )
                        FROM card_spell_emblem che
                        JOIN emblems e ON che.emblem_id = e.id
                        WHERE che.card_spell_id = c.id
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
                        FROM card_spell_class chc
                        JOIN classes cl ON chc.class_id = cl.id
                        WHERE chc.card_spell_id = c.id
                    ) AS classes_json
                FROM user_card_spells uc
                LEFT JOIN card_spells c ON c.id = uc.card_spell_id 
                LEFT JOIN teams t ON t.team_id = uc.team_id
            WHERE uc.user_id = @userId AND uc.team_id = @team_id AND SUBSTRING_INDEX(uc.position, '-', 1) = @position
            ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name;
        ";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@userId", user_id);
            selectCommand.Parameters.AddWithValue("@team_id", teamId);
            selectCommand.Parameters.AddWithValue("@position", position);

            await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                CardSpells cardSpell = new CardSpells
                {
                    Id = reader.GetStringSafe("card_spell_id"),
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

                // Đọc chuỗi JSON từ Database
                string emblemsJson = reader.GetStringSafe("emblems_json");

                if (!string.IsNullOrEmpty(emblemsJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Emblem> trong C#
                        cardSpell.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                        cardSpell.Emblems = new List<Emblems>();
                    }
                }

                string classesJson = reader.GetStringSafe("classes_json");

                if (!string.IsNullOrEmpty(classesJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Classes> trong C#
                        cardSpell.Classes = JsonHelper.DeserializeClasses(classesJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có class, MySQL sinh ra chuỗi "[null]"
                        cardSpell.Classes = new List<Classes>();
                    }
                }

                cardSpells.Add(cardSpell);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardSpells;
    }
    public async Task<List<CardSpells>> GetUserCardSpellsTeamWithoutPositionAsync(string user_id, string teamId)
    {
        List<CardSpells> cardSpells = new List<CardSpells>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT 
                    uc.*, 
                    c.name, 
                    c.image, 
                    c.type, 
                    c.description, 
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'id', e.id,
                                'name', e.name,
                                'image', e.image,
                                'type', e.type
                            )
                        )
                        FROM card_spell_emblem che
                        JOIN emblems e ON che.emblem_id = e.id
                        WHERE che.card_spell_id = c.id
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
                        FROM card_spell_class chc
                        JOIN classes cl ON chc.class_id = cl.id
                        WHERE chc.card_spell_id = c.id
                    ) AS classes_json
                FROM user_card_spells uc
                LEFT JOIN card_spells c ON c.id = uc.card_spell_id 
                LEFT JOIN teams t ON t.team_id = uc.team_id
            WHERE uc.user_id = @userId AND uc.team_id = @team_id
            ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name;
        ";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@userId", user_id);
            selectCommand.Parameters.AddWithValue("@team_id", teamId);

            await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                CardSpells cardSpell = new CardSpells
                {
                    Id = reader.GetStringSafe("card_spell_id"),
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

                // Đọc chuỗi JSON từ Database
                string emblemsJson = reader.GetStringSafe("emblems_json");

                if (!string.IsNullOrEmpty(emblemsJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Emblem> trong C#
                        cardSpell.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                        cardSpell.Emblems = new List<Emblems>();
                    }
                }

                string classesJson = reader.GetStringSafe("classes_json");

                if (!string.IsNullOrEmpty(classesJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Classes> trong C#
                        cardSpell.Classes = JsonHelper.DeserializeClasses(classesJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có class, MySQL sinh ra chuỗi "[null]"
                        cardSpell.Classes = new List<Classes>();
                    }
                }

                cardSpells.Add(cardSpell);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardSpells;
    }
    public async Task<Dictionary<string, int>> GetUniqueCardSpellsTypesTeamAsync(string teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT c.type, COUNT(c.type) AS number
            FROM user_card_spells uc
            LEFT JOIN card_spells c ON uc.card_spell_id = c.id 
            WHERE uc.user_id = @userId AND uc.team_id = @team_id
            GROUP BY c.type;
        ";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@userId", User.CurrentUserId);
            selectCommand.Parameters.AddWithValue("@team_id", teamId);

            await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();

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
    public async Task<bool> UpdateTeamCardSpellAsync(string team_id, string position, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string updateSQL = @"
            UPDATE user_card_spells 
            SET team_id = @team_id, position = @position 
            WHERE user_id = @user_id AND card_spell_id = @card_spell_id;
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
            updateCommand.Parameters.AddWithValue("@team_id", team_id);
            updateCommand.Parameters.AddWithValue("@position", position);
            updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            updateCommand.Parameters.AddWithValue("@card_spell_id", card_id);

            await updateCommand.ExecuteNonQueryAsync();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return false;
        }

        return true;
    }
    public async Task<int> GetUserCardSpellsCountAsync(string user_id, string search, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT COUNT(*) 
            FROM card_spells c
            JOIN user_card_spells uc ON c.id = uc.card_spell_id
            WHERE uc.user_id = @userId 
        ";
            if (!string.IsNullOrEmpty(type) && type != "All")
            {
                selectSQL += " AND c.type = @type";
            }

            if (!string.IsNullOrEmpty(rare) && rare != "All")
            {
                selectSQL += " AND c.rare = @rare";
            }

            if (!string.IsNullOrEmpty(search))
            {
                selectSQL += " AND c.name LIKE CONCAT('%', @search, '%')";
            }

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@userId", user_id);
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
            count = Convert.ToInt32(result);
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return count;
    }
    public async Task<int> GetUserCardSpellsTeamsPositionCountAsync(string user_id, string team_id, string position)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT COUNT(*) 
            FROM user_card_spells
            WHERE team_id = @team_id 
              AND SUBSTRING_INDEX(position, '-', 1) = @position 
              AND user_id = @userId;
        ";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@userId", user_id);
            selectCommand.Parameters.AddWithValue("@team_id", team_id);
            selectCommand.Parameters.AddWithValue("@position", position);

            object result = await selectCommand.ExecuteScalarAsync();
            count = Convert.ToInt32(result);
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return count;
    }
    public async Task<int> GetUserCardSpellsTeamsCountAsync(string user_id, string team_id)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT COUNT(*) 
            FROM user_card_spells
            WHERE team_id = @team_id 
              AND user_id = @userId;
        ";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@userId", user_id);
            selectCommand.Parameters.AddWithValue("@team_id", team_id);

            object result = await selectCommand.ExecuteScalarAsync();
            count = Convert.ToInt32(result);
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return count;
    }
    public async Task<bool> InsertUserCardSpellAsync(CardSpells cardSpell)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM user_card_spells
            WHERE user_id = @user_id AND card_spell_id = @card_spell_id;
        ";

            await using MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            checkCommand.Parameters.AddWithValue("@card_spell_id", cardSpell.Id);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO user_card_spells (
                    user_id, card_spell_id, rare, level, experiment, star, quality, block, quantity,
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
                    @user_id, @card_spell_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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

                await using MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection);

                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@card_spell_id", cardSpell.Id);
                insertCommand.Parameters.AddWithValue("@rare", cardSpell.Rare);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@experiment", 0);
                insertCommand.Parameters.AddWithValue("@star", 0);
                insertCommand.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(cardSpell.Rare));
                insertCommand.Parameters.AddWithValue("@block", false);
                insertCommand.Parameters.AddWithValue("@quantity", cardSpell.Quantity);
                insertCommand.Parameters.AddWithValue("@power", cardSpell.Power);
                insertCommand.Parameters.AddWithValue("@health", cardSpell.Health);
                insertCommand.Parameters.AddWithValue("@physical_attack", cardSpell.PhysicalAttack);
                insertCommand.Parameters.AddWithValue("@physical_defense", cardSpell.PhysicalDefense);
                insertCommand.Parameters.AddWithValue("@magical_attack", cardSpell.MagicalAttack);
                insertCommand.Parameters.AddWithValue("@magical_defense", cardSpell.MagicalDefense);
                insertCommand.Parameters.AddWithValue("@chemical_attack", cardSpell.ChemicalAttack);
                insertCommand.Parameters.AddWithValue("@chemical_defense", cardSpell.ChemicalDefense);
                insertCommand.Parameters.AddWithValue("@atomic_attack", cardSpell.AtomicAttack);
                insertCommand.Parameters.AddWithValue("@atomic_defense", cardSpell.AtomicDefense);
                insertCommand.Parameters.AddWithValue("@mental_attack", cardSpell.MentalAttack);
                insertCommand.Parameters.AddWithValue("@mental_defense", cardSpell.MentalDefense);
                insertCommand.Parameters.AddWithValue("@speed", cardSpell.Speed);
                insertCommand.Parameters.AddWithValue("@critical_damage_rate", cardSpell.CriticalDamageRate);
                insertCommand.Parameters.AddWithValue("@critical_rate", cardSpell.CriticalRate);
                insertCommand.Parameters.AddWithValue("@critical_resistance_rate", cardSpell.CriticalResistanceRate);
                insertCommand.Parameters.AddWithValue("@ignore_critical_rate", cardSpell.IgnoreCriticalRate);
                insertCommand.Parameters.AddWithValue("@penetration_rate", cardSpell.PenetrationRate);
                insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", cardSpell.PenetrationResistanceRate);
                insertCommand.Parameters.AddWithValue("@evasion_rate", cardSpell.EvasionRate);
                insertCommand.Parameters.AddWithValue("@damage_absorption_rate", cardSpell.DamageAbsorptionRate);
                insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardSpell.IgnoreDamageAbsorptionRate);
                insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", cardSpell.AbsorbedDamageRate);
                insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", cardSpell.VitalityRegenerationRate);
                insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardSpell.VitalityRegenerationResistanceRate);
                insertCommand.Parameters.AddWithValue("@accuracy_rate", cardSpell.AccuracyRate);
                insertCommand.Parameters.AddWithValue("@lifesteal_rate", cardSpell.LifestealRate);
                insertCommand.Parameters.AddWithValue("@shield_strength", cardSpell.ShieldStrength);
                insertCommand.Parameters.AddWithValue("@tenacity", cardSpell.Tenacity);
                insertCommand.Parameters.AddWithValue("@resistance_rate", cardSpell.ResistanceRate);
                insertCommand.Parameters.AddWithValue("@combo_rate", cardSpell.ComboRate);
                insertCommand.Parameters.AddWithValue("@ignore_combo_rate", cardSpell.IgnoreComboRate);
                insertCommand.Parameters.AddWithValue("@combo_damage_rate", cardSpell.ComboDamageRate);
                insertCommand.Parameters.AddWithValue("@combo_resistance_rate", cardSpell.ComboResistanceRate);
                insertCommand.Parameters.AddWithValue("@stun_rate", cardSpell.StunRate);
                insertCommand.Parameters.AddWithValue("@ignore_stun_rate", cardSpell.IgnoreStunRate);
                insertCommand.Parameters.AddWithValue("@reflection_rate", cardSpell.ReflectionRate);
                insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", cardSpell.IgnoreReflectionRate);
                insertCommand.Parameters.AddWithValue("@reflection_damage_rate", cardSpell.ReflectionDamageRate);
                insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", cardSpell.ReflectionResistanceRate);
                insertCommand.Parameters.AddWithValue("@mana", cardSpell.Mana);
                insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", cardSpell.ManaRegenerationRate);
                insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", cardSpell.DamageToDifferentFactionRate);
                insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardSpell.ResistanceToDifferentFactionRate);
                insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", cardSpell.DamageToSameFactionRate);
                insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardSpell.ResistanceToSameFactionRate);
                insertCommand.Parameters.AddWithValue("@normal_damage_rate", cardSpell.NormalDamageRate);
                insertCommand.Parameters.AddWithValue("@normal_resistance_rate", cardSpell.NormalResistanceRate);
                insertCommand.Parameters.AddWithValue("@skill_damage_rate", cardSpell.SkillDamageRate);
                insertCommand.Parameters.AddWithValue("@skill_resistance_rate", cardSpell.SkillResistanceRate);

                await insertCommand.ExecuteNonQueryAsync();
            }
            else
            {
                // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                string updateSQL = @"
                UPDATE user_card_spells
                SET quantity = @quantity
                WHERE user_id = @user_id AND card_spell_id = @card_spell_id;
            ";

                await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
                updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                updateCommand.Parameters.AddWithValue("@card_spell_id", cardSpell.Id);
                updateCommand.Parameters.AddWithValue("@quantity", cardSpell.Quantity);

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
    public async Task<bool> InsertOrUpdateUserCardSpellsBatchAsync(List<CardSpells> cardSpells)
    {
        if (cardSpells == null || cardSpells.Count == 0)
            return true;

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            int batchSize = 500; // vì nhiều column → giảm size

            for (int i = 0; i < cardSpells.Count; i += batchSize)
            {
                var batch = cardSpells.Skip(i).Take(batchSize).ToList();

                var stringBuilder = new System.Text.StringBuilder();
                var parameters = new List<MySqlParameter>();

                stringBuilder.Append(@"
                INSERT INTO user_card_spells (
                    user_id, card_spell_id, rare, level, experiment, star, quality, block, quantity,
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
                ) VALUES ");

                for (int j = 0; j < batch.Count; j++)
                {
                    var c = batch[j];

                    stringBuilder.Append($@"
                    (@user_id, @card_spell_id_{j}, @rare_{j}, 0, 0, 0, @quality_{j}, 0, @quantity_{j},
                    @power_{j}, @health_{j}, @physical_attack_{j}, @physical_defense_{j}, @magical_attack_{j}, @magical_defense_{j},
                    @chemical_attack_{j}, @chemical_defense_{j}, @atomic_attack_{j}, @atomic_defense_{j}, @mental_attack_{j}, @mental_defense_{j},
                    @speed_{j}, @critical_damage_rate_{j}, @critical_rate_{j}, @critical_resistance_rate_{j}, @ignore_critical_rate_{j},
                    @penetration_rate_{j}, @penetration_resistance_rate_{j},
                    @evasion_rate_{j}, @damage_absorption_rate_{j}, @ignore_damage_absorption_rate_{j}, @absorbed_damage_rate_{j},
                    @vitality_regeneration_rate_{j}, @vitality_regeneration_resistance_rate_{j},
                    @accuracy_rate_{j}, @lifesteal_rate_{j}, @shield_strength_{j}, @tenacity_{j}, @resistance_rate_{j},
                    @combo_rate_{j}, @ignore_combo_rate_{j}, @combo_damage_rate_{j}, @combo_resistance_rate_{j},
                    @stun_rate_{j}, @ignore_stun_rate_{j},
                    @reflection_rate_{j}, @ignore_reflection_rate_{j}, @reflection_damage_rate_{j}, @reflection_resistance_rate_{j},
                    @mana_{j}, @mana_regeneration_rate_{j},
                    @damage_to_different_faction_rate_{j}, @resistance_to_different_faction_rate_{j},
                    @damage_to_same_faction_rate_{j}, @resistance_to_same_faction_rate_{j},
                    @normal_damage_rate_{j}, @normal_resistance_rate_{j},
                    @skill_damage_rate_{j}, @skill_resistance_rate_{j}
                    ),");

                    parameters.AddRange(new[]
                    {
                        new MySqlParameter($"@card_spell_id_{j}", c.Id),
                        new MySqlParameter($"@rare_{j}", c.Rare),
                        new MySqlParameter($"@quality_{j}", QualityEvaluatorHelper.CheckQuality(c.Rare)),
                        new MySqlParameter($"@quantity_{j}", c.Quantity),
                        new MySqlParameter($"@power_{j}", c.Power),
                        new MySqlParameter($"@health_{j}", c.Health),
                        new MySqlParameter($"@physical_attack_{j}", c.PhysicalAttack),
                        new MySqlParameter($"@physical_defense_{j}", c.PhysicalDefense),
                        new MySqlParameter($"@magical_attack_{j}", c.MagicalAttack),
                        new MySqlParameter($"@magical_defense_{j}", c.MagicalDefense),
                        new MySqlParameter($"@chemical_attack_{j}", c.ChemicalAttack),
                        new MySqlParameter($"@chemical_defense_{j}", c.ChemicalDefense),
                        new MySqlParameter($"@atomic_attack_{j}", c.AtomicAttack),
                        new MySqlParameter($"@atomic_defense_{j}", c.AtomicDefense),
                        new MySqlParameter($"@mental_attack_{j}", c.MentalAttack),
                        new MySqlParameter($"@mental_defense_{j}", c.MentalDefense),
                        new MySqlParameter($"@speed_{j}", c.Speed),
                        new MySqlParameter($"@critical_damage_rate_{j}", c.CriticalDamageRate),
                        new MySqlParameter($"@critical_rate_{j}", c.CriticalRate),
                        new MySqlParameter($"@critical_resistance_rate_{j}", c.CriticalResistanceRate),
                        new MySqlParameter($"@ignore_critical_rate_{j}", c.IgnoreCriticalRate),
                        new MySqlParameter($"@penetration_rate_{j}", c.PenetrationRate),
                        new MySqlParameter($"@penetration_resistance_rate_{j}", c.PenetrationResistanceRate),
                        new MySqlParameter($"@evasion_rate_{j}", c.EvasionRate),
                        new MySqlParameter($"@damage_absorption_rate_{j}", c.DamageAbsorptionRate),
                        new MySqlParameter($"@ignore_damage_absorption_rate_{j}", c.IgnoreDamageAbsorptionRate),
                        new MySqlParameter($"@absorbed_damage_rate_{j}", c.AbsorbedDamageRate),
                        new MySqlParameter($"@vitality_regeneration_rate_{j}", c.VitalityRegenerationRate),
                        new MySqlParameter($"@vitality_regeneration_resistance_rate_{j}", c.VitalityRegenerationResistanceRate),
                        new MySqlParameter($"@accuracy_rate_{j}", c.AccuracyRate),
                        new MySqlParameter($"@lifesteal_rate_{j}", c.LifestealRate),
                        new MySqlParameter($"@shield_strength_{j}", c.ShieldStrength),
                        new MySqlParameter($"@tenacity_{j}", c.Tenacity),
                        new MySqlParameter($"@resistance_rate_{j}", c.ResistanceRate),
                        new MySqlParameter($"@combo_rate_{j}", c.ComboRate),
                        new MySqlParameter($"@ignore_combo_rate_{j}", c.IgnoreComboRate),
                        new MySqlParameter($"@combo_damage_rate_{j}", c.ComboDamageRate),
                        new MySqlParameter($"@combo_resistance_rate_{j}", c.ComboResistanceRate),
                        new MySqlParameter($"@stun_rate_{j}", c.StunRate),
                        new MySqlParameter($"@ignore_stun_rate_{j}", c.IgnoreStunRate),
                        new MySqlParameter($"@reflection_rate_{j}", c.ReflectionRate),
                        new MySqlParameter($"@ignore_reflection_rate_{j}", c.IgnoreReflectionRate),
                        new MySqlParameter($"@reflection_damage_rate_{j}", c.ReflectionDamageRate),
                        new MySqlParameter($"@reflection_resistance_rate_{j}", c.ReflectionResistanceRate),
                        new MySqlParameter($"@mana_{j}", c.Mana),
                        new MySqlParameter($"@mana_regeneration_rate_{j}", c.ManaRegenerationRate),
                        new MySqlParameter($"@damage_to_different_faction_rate_{j}", c.DamageToDifferentFactionRate),
                        new MySqlParameter($"@resistance_to_different_faction_rate_{j}", c.ResistanceToDifferentFactionRate),
                        new MySqlParameter($"@damage_to_same_faction_rate_{j}", c.DamageToSameFactionRate),
                        new MySqlParameter($"@resistance_to_same_faction_rate_{j}", c.ResistanceToSameFactionRate),
                        new MySqlParameter($"@normal_damage_rate_{j}", c.NormalDamageRate),
                        new MySqlParameter($"@normal_resistance_rate_{j}", c.NormalResistanceRate),
                        new MySqlParameter($"@skill_damage_rate_{j}", c.SkillDamageRate),
                        new MySqlParameter($"@skill_resistance_rate_{j}", c.SkillResistanceRate),
                });
                }

                stringBuilder.Length--; // remove dấu ,

                stringBuilder.Append(@"
                ON DUPLICATE KEY UPDATE
                    quantity = user_card_spells.quantity + VALUES(quantity);
                ");

                await using var command = new MySqlCommand(stringBuilder.ToString(), connection, (MySqlTransaction)transaction);

                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddRange(parameters.ToArray());

                await command.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            Debug.LogError("Batch Error: " + ex.Message);
            return false;
        }

        return true;
    }
    public async Task<bool> UpdateCardSpellLevelAsync(CardSpells cardSpell, int level)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string updateSQL = @"
            UPDATE user_card_spells
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
            WHERE user_id = @user_id AND card_spell_id = @card_spell_id;
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);

            updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            updateCommand.Parameters.AddWithValue("@card_spell_id", cardSpell.Id);
            updateCommand.Parameters.AddWithValue("@level", level);
            updateCommand.Parameters.AddWithValue("@power", cardSpell.Power);
            updateCommand.Parameters.AddWithValue("@health", cardSpell.Health);
            updateCommand.Parameters.AddWithValue("@physical_attack", cardSpell.PhysicalAttack);
            updateCommand.Parameters.AddWithValue("@physical_defense", cardSpell.PhysicalDefense);
            updateCommand.Parameters.AddWithValue("@magical_attack", cardSpell.MagicalAttack);
            updateCommand.Parameters.AddWithValue("@magical_defense", cardSpell.MagicalDefense);
            updateCommand.Parameters.AddWithValue("@chemical_attack", cardSpell.ChemicalAttack);
            updateCommand.Parameters.AddWithValue("@chemical_defense", cardSpell.ChemicalDefense);
            updateCommand.Parameters.AddWithValue("@atomic_attack", cardSpell.AtomicAttack);
            updateCommand.Parameters.AddWithValue("@atomic_defense", cardSpell.AtomicDefense);
            updateCommand.Parameters.AddWithValue("@mental_attack", cardSpell.MentalAttack);
            updateCommand.Parameters.AddWithValue("@mental_defense", cardSpell.MentalDefense);
            updateCommand.Parameters.AddWithValue("@speed", cardSpell.Speed);
            updateCommand.Parameters.AddWithValue("@critical_damage_rate", cardSpell.CriticalDamageRate);
            updateCommand.Parameters.AddWithValue("@critical_rate", cardSpell.CriticalRate);
            updateCommand.Parameters.AddWithValue("@critical_resistance_rate", cardSpell.CriticalResistanceRate);
            updateCommand.Parameters.AddWithValue("@ignore_critical_rate", cardSpell.IgnoreCriticalRate);
            updateCommand.Parameters.AddWithValue("@penetration_rate", cardSpell.PenetrationRate);
            updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", cardSpell.PenetrationResistanceRate);
            updateCommand.Parameters.AddWithValue("@evasion_rate", cardSpell.EvasionRate);
            updateCommand.Parameters.AddWithValue("@damage_absorption_rate", cardSpell.DamageAbsorptionRate);
            updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardSpell.IgnoreDamageAbsorptionRate);
            updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", cardSpell.AbsorbedDamageRate);
            updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", cardSpell.VitalityRegenerationRate);
            updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardSpell.VitalityRegenerationResistanceRate);
            updateCommand.Parameters.AddWithValue("@accuracy_rate", cardSpell.AccuracyRate);
            updateCommand.Parameters.AddWithValue("@lifesteal_rate", cardSpell.LifestealRate);
            updateCommand.Parameters.AddWithValue("@shield_strength", cardSpell.ShieldStrength);
            updateCommand.Parameters.AddWithValue("@tenacity", cardSpell.Tenacity);
            updateCommand.Parameters.AddWithValue("@resistance_rate", cardSpell.ResistanceRate);
            updateCommand.Parameters.AddWithValue("@combo_rate", cardSpell.ComboRate);
            updateCommand.Parameters.AddWithValue("@ignore_combo_rate", cardSpell.IgnoreComboRate);
            updateCommand.Parameters.AddWithValue("@combo_damage_rate", cardSpell.ComboDamageRate);
            updateCommand.Parameters.AddWithValue("@combo_resistance_rate", cardSpell.ComboResistanceRate);
            updateCommand.Parameters.AddWithValue("@stun_rate", cardSpell.StunRate);
            updateCommand.Parameters.AddWithValue("@ignore_stun_rate", cardSpell.IgnoreStunRate);
            updateCommand.Parameters.AddWithValue("@reflection_rate", cardSpell.ReflectionRate);
            updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", cardSpell.IgnoreReflectionRate);
            updateCommand.Parameters.AddWithValue("@reflection_damage_rate", cardSpell.ReflectionDamageRate);
            updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", cardSpell.ReflectionResistanceRate);
            updateCommand.Parameters.AddWithValue("@mana", cardSpell.Mana);
            updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", cardSpell.ManaRegenerationRate);
            updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", cardSpell.DamageToDifferentFactionRate);
            updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardSpell.ResistanceToDifferentFactionRate);
            updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", cardSpell.DamageToSameFactionRate);
            updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardSpell.ResistanceToSameFactionRate);
            updateCommand.Parameters.AddWithValue("@normal_damage_rate", cardSpell.NormalDamageRate);
            updateCommand.Parameters.AddWithValue("@normal_resistance_rate", cardSpell.NormalResistanceRate);
            updateCommand.Parameters.AddWithValue("@skill_damage_rate", cardSpell.SkillDamageRate);
            updateCommand.Parameters.AddWithValue("@skill_resistance_rate", cardSpell.SkillResistanceRate);

            await updateCommand.ExecuteNonQueryAsync();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return false;
        }

        return true;
    }
    public async Task<bool> UpdateCardSpellBreakthroughAsync(CardSpells cardSpell, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string updateSQL = @"
            UPDATE user_card_spells
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
            WHERE user_id = @user_id AND card_spell_id = @card_spell_id;
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);

            updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            updateCommand.Parameters.AddWithValue("@card_spell_id", cardSpell.Id);
            updateCommand.Parameters.AddWithValue("@star", star);
            updateCommand.Parameters.AddWithValue("@quantity", quantity);
            updateCommand.Parameters.AddWithValue("@power", cardSpell.Power);
            updateCommand.Parameters.AddWithValue("@health", cardSpell.Health);
            updateCommand.Parameters.AddWithValue("@physical_attack", cardSpell.PhysicalAttack);
            updateCommand.Parameters.AddWithValue("@physical_defense", cardSpell.PhysicalDefense);
            updateCommand.Parameters.AddWithValue("@magical_attack", cardSpell.MagicalAttack);
            updateCommand.Parameters.AddWithValue("@magical_defense", cardSpell.MagicalDefense);
            updateCommand.Parameters.AddWithValue("@chemical_attack", cardSpell.ChemicalAttack);
            updateCommand.Parameters.AddWithValue("@chemical_defense", cardSpell.ChemicalDefense);
            updateCommand.Parameters.AddWithValue("@atomic_attack", cardSpell.AtomicAttack);
            updateCommand.Parameters.AddWithValue("@atomic_defense", cardSpell.AtomicDefense);
            updateCommand.Parameters.AddWithValue("@mental_attack", cardSpell.MentalAttack);
            updateCommand.Parameters.AddWithValue("@mental_defense", cardSpell.MentalDefense);
            updateCommand.Parameters.AddWithValue("@speed", cardSpell.Speed);
            updateCommand.Parameters.AddWithValue("@critical_damage_rate", cardSpell.CriticalDamageRate);
            updateCommand.Parameters.AddWithValue("@critical_rate", cardSpell.CriticalRate);
            updateCommand.Parameters.AddWithValue("@critical_resistance_rate", cardSpell.CriticalResistanceRate);
            updateCommand.Parameters.AddWithValue("@ignore_critical_rate", cardSpell.IgnoreCriticalRate);
            updateCommand.Parameters.AddWithValue("@penetration_rate", cardSpell.PenetrationRate);
            updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", cardSpell.PenetrationResistanceRate);
            updateCommand.Parameters.AddWithValue("@evasion_rate", cardSpell.EvasionRate);
            updateCommand.Parameters.AddWithValue("@damage_absorption_rate", cardSpell.DamageAbsorptionRate);
            updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardSpell.IgnoreDamageAbsorptionRate);
            updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", cardSpell.AbsorbedDamageRate);
            updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", cardSpell.VitalityRegenerationRate);
            updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardSpell.VitalityRegenerationResistanceRate);
            updateCommand.Parameters.AddWithValue("@accuracy_rate", cardSpell.AccuracyRate);
            updateCommand.Parameters.AddWithValue("@lifesteal_rate", cardSpell.LifestealRate);
            updateCommand.Parameters.AddWithValue("@shield_strength", cardSpell.ShieldStrength);
            updateCommand.Parameters.AddWithValue("@tenacity", cardSpell.Tenacity);
            updateCommand.Parameters.AddWithValue("@resistance_rate", cardSpell.ResistanceRate);
            updateCommand.Parameters.AddWithValue("@combo_rate", cardSpell.ComboRate);
            updateCommand.Parameters.AddWithValue("@ignore_combo_rate", cardSpell.IgnoreComboRate);
            updateCommand.Parameters.AddWithValue("@combo_damage_rate", cardSpell.ComboDamageRate);
            updateCommand.Parameters.AddWithValue("@combo_resistance_rate", cardSpell.ComboResistanceRate);
            updateCommand.Parameters.AddWithValue("@stun_rate", cardSpell.StunRate);
            updateCommand.Parameters.AddWithValue("@ignore_stun_rate", cardSpell.IgnoreStunRate);
            updateCommand.Parameters.AddWithValue("@reflection_rate", cardSpell.ReflectionRate);
            updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", cardSpell.IgnoreReflectionRate);
            updateCommand.Parameters.AddWithValue("@reflection_damage_rate", cardSpell.ReflectionDamageRate);
            updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", cardSpell.ReflectionResistanceRate);
            updateCommand.Parameters.AddWithValue("@mana", cardSpell.Mana);
            updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", cardSpell.ManaRegenerationRate);
            updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", cardSpell.DamageToDifferentFactionRate);
            updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardSpell.ResistanceToDifferentFactionRate);
            updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", cardSpell.DamageToSameFactionRate);
            updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardSpell.ResistanceToSameFactionRate);
            updateCommand.Parameters.AddWithValue("@normal_damage_rate", cardSpell.NormalDamageRate);
            updateCommand.Parameters.AddWithValue("@normal_resistance_rate", cardSpell.NormalResistanceRate);
            updateCommand.Parameters.AddWithValue("@skill_damage_rate", cardSpell.SkillDamageRate);
            updateCommand.Parameters.AddWithValue("@skill_resistance_rate", cardSpell.SkillResistanceRate);

            await updateCommand.ExecuteNonQueryAsync();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return false;
        }

        return true;
    }
    public async Task<CardSpells> GetUserCardSpellByIdAsync(string user_id, string Id)
    {
        CardSpells cardSpell = new CardSpells();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT uc.*, c.image
            FROM user_card_spells uc
            JOIN card_spells c ON uc.card_spell_id = c.id
            WHERE uc.card_spell_id = @id AND uc.user_id = @user_id";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@id", Id);
            selectCommand.Parameters.AddWithValue("@user_id", user_id);

            await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                cardSpell = new CardSpells
                {
                    Id = reader.GetStringSafe("card_spell_id"),
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

        return cardSpell;
    }
    public async Task<List<CardSpells>> GetAllUserCardSpellsInTeamAsync(string user_id)
    {
        List<CardSpells> cardSpells = new List<CardSpells>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT 
                    uc.*, 
                    c.name, 
                    c.image, 
                    c.type, 
                    c.description, 
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'id', e.id,
                                'name', e.name,
                                'image', e.image,
                                'type', e.type
                            )
                        )
                        FROM card_spell_emblem che
                        JOIN emblems e ON che.emblem_id = e.id
                        WHERE che.card_spell_id = c.id
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
                        FROM card_spell_class chc
                        JOIN classes cl ON chc.class_id = cl.id
                        WHERE chc.card_spell_id = c.id
                    ) AS classes_json
                FROM user_card_spells uc
                LEFT JOIN card_spells c ON c.id = uc.card_spell_id 
                LEFT JOIN teams t ON t.team_id = uc.team_id
            WHERE uc.user_id = @user_id AND uc.team_id IS NOT NULL";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@user_id", user_id);

            await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                CardSpells cardSpell = new CardSpells
                {
                    Id = reader.GetStringSafe("card_spell_id"),
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

                // Đọc chuỗi JSON từ Database
                string emblemsJson = reader.GetStringSafe("emblems_json");

                if (!string.IsNullOrEmpty(emblemsJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Emblem> trong C#
                        cardSpell.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                        cardSpell.Emblems = new List<Emblems>();
                    }
                }

                string classesJson = reader.GetStringSafe("classes_json");

                if (!string.IsNullOrEmpty(classesJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Classes> trong C#
                        cardSpell.Classes = JsonHelper.DeserializeClasses(classesJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có class, MySQL sinh ra chuỗi "[null]"
                        cardSpell.Classes = new List<Classes>();
                    }
                }

                cardSpells.Add(cardSpell);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardSpells;
    }
}