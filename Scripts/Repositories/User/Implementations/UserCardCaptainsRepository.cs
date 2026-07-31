using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;

public class UserCardCaptainsRepository : IUserCardCaptainsRepository
{
    public async Task<List<CardCaptains>> GetUserCardCaptainsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardCaptains> cardCaptains = new List<CardCaptains>();
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
                        FROM card_captain_emblem che
                        JOIN emblems e ON che.emblem_id = e.id
                        WHERE che.card_captain_id = c.id
                    ) AS emblems_json,
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'id', cl.id,
                                'sub_type', cl.sub_type,
                                'sub_image', cl.sub_image,
                                'main_type', cl.main_type,
                                'main_image', cl.main_image,
                                'movement_range', cl.movement_range,
                                'movement_point', cl.movement_point,
                                'attack_range', cl.attack_range
                            )
                        )
                        FROM card_captain_class chc
                        JOIN classes cl ON chc.class_id = cl.id
                        WHERE chc.card_captain_id = c.id
                    ) AS classes_json
                FROM user_card_captains uc
                LEFT JOIN card_captains c ON c.id = uc.card_captain_id 
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

            selectSQL += " LIMIT @limit OFFSET @offset";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@userId", userId);
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
                CardCaptains cardCaptain = new CardCaptains
                {
                    Id = reader.GetStringSafe("card_captain_id"),
                    Name = reader.GetStringSafe("name"),
                    Image = reader.GetStringSafe("image"),
                    Rarity = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Type = reader.GetStringSafe("type"),
                    Star = reader.GetIntSafe("star"),
                    Level = reader.GetIntSafe("level"),
                    Experience = reader.GetDoubleSafe("experience"),
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
                        cardCaptain.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                        cardCaptain.Emblems = new List<Emblems>();
                    }
                }

                string classesJson = reader.GetStringSafe("classes_json");

                if (!string.IsNullOrEmpty(classesJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Classes> trong C#
                        cardCaptain.Class = JsonHelper.DeserializeClasses(classesJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có class, MySQL sinh ra chuỗi "[null]"
                        cardCaptain.Class = new Classes();
                    }
                }

                cardCaptains.Add(cardCaptain);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardCaptains;
    }
    public async Task<List<CardCaptains>> GetUserCardCaptainsTeamAsync(string userId, string teamId, string position)
    {
        List<CardCaptains> cardCaptains = new List<CardCaptains>();
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
                        FROM card_captain_emblem che
                        JOIN emblems e ON che.emblem_id = e.id
                        WHERE che.card_captain_id = c.id
                    ) AS emblems_json,
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'id', cl.id,
                                'sub_type', cl.sub_type,
                                'sub_image', cl.sub_image,
                                'main_type', cl.main_type,
                                'main_image', cl.main_image,
                                'movement_range', cl.movement_range,
                                'movement_point', cl.movement_point,
                                'attack_range', cl.attack_range
                            )
                        )
                        FROM card_captain_class chc
                        JOIN classes cl ON chc.class_id = cl.id
                        WHERE chc.card_captain_id = c.id
                    ) AS classes_json
                FROM user_card_captains uc
                LEFT JOIN card_captains c ON c.id = uc.card_captain_id 
                LEFT JOIN teams t ON t.team_id = uc.team_id
            WHERE uc.user_id = @userId AND uc.team_id = @team_id AND SUBSTRING_INDEX(uc.position, '-', 1) = @position
        ";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@userId", userId);
            selectCommand.Parameters.AddWithValue("@team_id", teamId);
            selectCommand.Parameters.AddWithValue("@position", position);

            await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                CardCaptains cardCaptain = new CardCaptains
                {
                    Id = reader.GetStringSafe("card_captain_id"),
                    Name = reader.GetStringSafe("name"),
                    Image = reader.GetStringSafe("image"),
                    Rarity = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Type = reader.GetStringSafe("type"),
                    Star = reader.GetIntSafe("star"),
                    Level = reader.GetIntSafe("level"),
                    Experience = reader.GetDoubleSafe("experience"),
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
                        cardCaptain.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                        cardCaptain.Emblems = new List<Emblems>();
                    }
                }

                string classesJson = reader.GetStringSafe("classes_json");

                if (!string.IsNullOrEmpty(classesJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Classes> trong C#
                        cardCaptain.Class = JsonHelper.DeserializeClasses(classesJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có class, MySQL sinh ra chuỗi "[null]"
                        cardCaptain.Class = new Classes();
                    }
                }

                cardCaptains.Add(cardCaptain);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardCaptains;
    }
    public async Task<List<CardCaptains>> GetUserCardCaptainsTeamWithoutPositionAsync(string userId, string teamId)
    {
        List<CardCaptains> cardCaptains = new List<CardCaptains>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT  distinct
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
                        FROM card_captain_emblem che
                        JOIN emblems e ON che.emblem_id = e.id
                        WHERE che.card_captain_id = c.id
                    ) AS emblems_json,
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'id', cl.id,
                                'sub_type', cl.sub_type,
                                'sub_image', cl.sub_image,
                                'main_type', cl.main_type,
                                'main_image', cl.main_image,
                                'movement_range', cl.movement_range,
                                'movement_point', cl.movement_point,
                                'attack_range', cl.attack_range
                            )
                        )
                        FROM card_captain_class chc
                        JOIN classes cl ON chc.class_id = cl.id
                        WHERE chc.card_captain_id = c.id
                    ) AS classes_json
                FROM user_card_captains uc
                LEFT JOIN card_captains c ON c.id = uc.card_captain_id 
                LEFT JOIN teams t ON t.team_id = uc.team_id
            WHERE uc.user_id = @userId AND uc.team_id = @team_id
        ";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@userId", userId);
            selectCommand.Parameters.AddWithValue("@team_id", teamId);

            await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                CardCaptains cardCaptain = new CardCaptains
                {
                    Id = reader.GetStringSafe("card_captain_id"),
                    Name = reader.GetStringSafe("name"),
                    Image = reader.GetStringSafe("image"),
                    Rarity = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Type = reader.GetStringSafe("type"),
                    Star = reader.GetIntSafe("star"),
                    Level = reader.GetIntSafe("level"),
                    Experience = reader.GetDoubleSafe("experience"),
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
                        cardCaptain.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                        cardCaptain.Emblems = new List<Emblems>();
                    }
                }

                string classesJson = reader.GetStringSafe("classes_json");

                if (!string.IsNullOrEmpty(classesJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Classes> trong C#
                        cardCaptain.Class = JsonHelper.DeserializeClasses(classesJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có class, MySQL sinh ra chuỗi "[null]"
                        cardCaptain.Class = new Classes();
                    }
                }

                cardCaptains.Add(cardCaptain);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardCaptains;
    }
    public async Task<Dictionary<string, int>> GetUniqueUserCardCaptainsTypesTeamAsync(string userId, string teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT c.type, COUNT(c.type) AS number
            FROM user_card_captains uc
            LEFT JOIN card_captains c ON uc.card_captain_id = c.id 
            WHERE uc.user_id = @userId AND uc.team_id = @team_id
            GROUP BY c.type;
        ";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@userId", userId);
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
    public async Task<bool> UpdateTeamUserCardCaptainAsync(string userId, string teamId, string position, string cardId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string updateSQL = @"
            UPDATE user_card_captains 
            SET team_id = @team_id, position = @position 
            WHERE user_id = @user_id AND card_captain_id = @card_captain_id;
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
            updateCommand.Parameters.AddWithValue("@team_id", teamId);
            updateCommand.Parameters.AddWithValue("@position", position);
            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@card_captain_id", cardId);

            await updateCommand.ExecuteNonQueryAsync();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return false;
        }

        return true;
    }
    public async Task<bool> IsCardInTeamAsync(string userId, string cardId)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(cardId))
            return false;

        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra team_id vừa không NULL vừa khác chuỗi rỗng
            string checkSQL = @"
            SELECT 1 
            FROM user_card_captains 
            WHERE user_id = @user_id 
              AND card_captain_id = @card_captain_id 
              AND team_id IS NOT NULL 
              AND team_id != ''
            LIMIT 1;
        ";

            await using MySqlCommand command = new MySqlCommand(checkSQL, connection);
            command.Parameters.AddWithValue("@user_id", userId);
            command.Parameters.AddWithValue("@card_captain_id", cardId);

            var result = await command.ExecuteScalarAsync();

            // Nếu result khác null tức là card đã được xếp vào một team nào đó
            return result != null && result != DBNull.Value;
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error IsCardInTeam: " + ex.Message);
            return false;
        }
    }
    public async Task<int> GetUserCardCaptainsCountAsync(string userId, string search, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT COUNT(*) 
            FROM card_captains c
            JOIN user_card_captains uc ON c.id = uc.card_captain_id
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
            selectCommand.Parameters.AddWithValue("@userId", userId);
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
    public async Task<int> GetUserCardCaptainsTeamsPositionCountAsync(string userId, string teamId, string position)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT COUNT(*) 
            FROM user_card_captains
            WHERE team_id = @team_id 
              AND SUBSTRING_INDEX(position, '-', 1) = @position 
              AND user_id = @userId;
        ";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@userId", userId);
            selectCommand.Parameters.AddWithValue("@team_id", teamId);
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
    public async Task<int> GetUserCardCaptainsTeamsCountAsync(string userId, string teamId)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT COUNT(*) 
            FROM user_card_captains
            WHERE team_id = @team_id 
              AND user_id = @userId;
        ";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@userId", userId);
            selectCommand.Parameters.AddWithValue("@team_id", teamId);

            object result = await selectCommand.ExecuteScalarAsync();
            count = Convert.ToInt32(result);
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return count;
    }
    public async Task<InsertOrUpdateResult<CardCaptains>> InsertOrUpdateUserCardCaptainAsync(string userId, CardCaptains cardCaptain)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Query thực hiện Insert hoặc Update nếu đã tồn tại Composite Primary Key (user_id, card_captain_id)
            string upsertSQL = @"
            INSERT INTO user_card_captains (
                user_id, card_captain_id, rare, level, experience, star, quality, block, quantity,
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
                @user_id, @card_captain_id, @rare, 0, 0, 0, @quality, false, @quantity,
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
            )
            ON DUPLICATE KEY UPDATE 
                quantity = VALUES(quantity);";

            await using MySqlCommand command = new MySqlCommand(upsertSQL, connection);

            // Add Parameters
            command.Parameters.AddWithValue("@user_id", userId);
            command.Parameters.AddWithValue("@card_captain_id", cardCaptain.Id);
            command.Parameters.AddWithValue("@rare", cardCaptain.Rarity);
            command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(cardCaptain.Rarity));
            command.Parameters.AddWithValue("@quantity", cardCaptain.Quantity);
            command.Parameters.AddWithValue("@power", cardCaptain.Power);
            command.Parameters.AddWithValue("@health", cardCaptain.Health);
            command.Parameters.AddWithValue("@physical_attack", cardCaptain.PhysicalAttack);
            command.Parameters.AddWithValue("@physical_defense", cardCaptain.PhysicalDefense);
            command.Parameters.AddWithValue("@magical_attack", cardCaptain.MagicalAttack);
            command.Parameters.AddWithValue("@magical_defense", cardCaptain.MagicalDefense);
            command.Parameters.AddWithValue("@chemical_attack", cardCaptain.ChemicalAttack);
            command.Parameters.AddWithValue("@chemical_defense", cardCaptain.ChemicalDefense);
            command.Parameters.AddWithValue("@atomic_attack", cardCaptain.AtomicAttack);
            command.Parameters.AddWithValue("@atomic_defense", cardCaptain.AtomicDefense);
            command.Parameters.AddWithValue("@mental_attack", cardCaptain.MentalAttack);
            command.Parameters.AddWithValue("@mental_defense", cardCaptain.MentalDefense);
            command.Parameters.AddWithValue("@speed", cardCaptain.Speed);
            command.Parameters.AddWithValue("@critical_damage_rate", cardCaptain.CriticalDamageRate);
            command.Parameters.AddWithValue("@critical_rate", cardCaptain.CriticalRate);
            command.Parameters.AddWithValue("@critical_resistance_rate", cardCaptain.CriticalResistanceRate);
            command.Parameters.AddWithValue("@ignore_critical_rate", cardCaptain.IgnoreCriticalRate);
            command.Parameters.AddWithValue("@penetration_rate", cardCaptain.PenetrationRate);
            command.Parameters.AddWithValue("@penetration_resistance_rate", cardCaptain.PenetrationResistanceRate);
            command.Parameters.AddWithValue("@evasion_rate", cardCaptain.EvasionRate);
            command.Parameters.AddWithValue("@damage_absorption_rate", cardCaptain.DamageAbsorptionRate);
            command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardCaptain.IgnoreDamageAbsorptionRate);
            command.Parameters.AddWithValue("@absorbed_damage_rate", cardCaptain.AbsorbedDamageRate);
            command.Parameters.AddWithValue("@vitality_regeneration_rate", cardCaptain.VitalityRegenerationRate);
            command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardCaptain.VitalityRegenerationResistanceRate);
            command.Parameters.AddWithValue("@accuracy_rate", cardCaptain.AccuracyRate);
            command.Parameters.AddWithValue("@lifesteal_rate", cardCaptain.LifestealRate);
            command.Parameters.AddWithValue("@shield_strength", cardCaptain.ShieldStrength);
            command.Parameters.AddWithValue("@tenacity", cardCaptain.Tenacity);
            command.Parameters.AddWithValue("@resistance_rate", cardCaptain.ResistanceRate);
            command.Parameters.AddWithValue("@combo_rate", cardCaptain.ComboRate);
            command.Parameters.AddWithValue("@ignore_combo_rate", cardCaptain.IgnoreComboRate);
            command.Parameters.AddWithValue("@combo_damage_rate", cardCaptain.ComboDamageRate);
            command.Parameters.AddWithValue("@combo_resistance_rate", cardCaptain.ComboResistanceRate);
            command.Parameters.AddWithValue("@stun_rate", cardCaptain.StunRate);
            command.Parameters.AddWithValue("@ignore_stun_rate", cardCaptain.IgnoreStunRate);
            command.Parameters.AddWithValue("@reflection_rate", cardCaptain.ReflectionRate);
            command.Parameters.AddWithValue("@ignore_reflection_rate", cardCaptain.IgnoreReflectionRate);
            command.Parameters.AddWithValue("@reflection_damage_rate", cardCaptain.ReflectionDamageRate);
            command.Parameters.AddWithValue("@reflection_resistance_rate", cardCaptain.ReflectionResistanceRate);
            command.Parameters.AddWithValue("@mana", cardCaptain.Mana);
            command.Parameters.AddWithValue("@mana_regeneration_rate", cardCaptain.ManaRegenerationRate);
            command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardCaptain.DamageToDifferentFactionRate);
            command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardCaptain.ResistanceToDifferentFactionRate);
            command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardCaptain.DamageToSameFactionRate);
            command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardCaptain.ResistanceToSameFactionRate);
            command.Parameters.AddWithValue("@normal_damage_rate", cardCaptain.NormalDamageRate);
            command.Parameters.AddWithValue("@normal_resistance_rate", cardCaptain.NormalResistanceRate);
            command.Parameters.AddWithValue("@skill_damage_rate", cardCaptain.SkillDamageRate);
            command.Parameters.AddWithValue("@skill_resistance_rate", cardCaptain.SkillResistanceRate);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            // MySQL quy ước: Insert mới = 1, Update = 2, Không thay đổi = 0
            if (rowsAffected == 1)
            {
                return InsertOrUpdateResult<CardCaptains>.Inserted(cardCaptain);
            }
            else if (rowsAffected == 2 || rowsAffected == 0)
            {
                return InsertOrUpdateResult<CardCaptains>.Updated(cardCaptain);
            }

            return InsertOrUpdateResult<CardCaptains>.Failure();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Database Error: " + ex.Message);
            return InsertOrUpdateResult<CardCaptains>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<BatchOperationResultDTO<CardCaptains>>> InsertOrUpdateUserCardCaptainsBatchAsync(
    string userId, List<CardCaptains> cardCaptains)
    {
        if (cardCaptains == null || cardCaptains.Count == 0)
        {
            return new InsertOrUpdateResult<BatchOperationResultDTO<CardCaptains>>
            {
                Data = new BatchOperationResultDTO<CardCaptains>(),
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // 1. Query lấy TOÀN BỘ card_captain_id hiện có của User (Cực nhanh nhờ Index user_id)
            var existingIds = new HashSet<string>();
            string checkSql = "SELECT card_captain_id FROM user_card_captains WHERE user_id = @user_id;";

            await using (var checkCmd = new MySqlCommand(checkSql, connection))
            {
                checkCmd.Parameters.AddWithValue("@user_id", userId);
                await using var reader = await checkCmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    existingIds.Add(reader.GetString(0));
                }
            }

            // 2. Phân loại CardCaptains giữ NGUYÊN VẸN OBJECT thuộc tính trong RAM C#
            var batchResult = new BatchOperationResultDTO<CardCaptains>();
            foreach (var card in cardCaptains)
            {
                if (existingIds.Contains(card.Id))
                {
                    batchResult.UpdatedItems.Add(card); // Trả về full object card
                }
                else
                {
                    batchResult.InsertedItems.Add(card); // Trả về full object card để dùng truyền sang Gallery
                }
            }

            // 3. Thực hiện Bulk Insert/Update
            await using var transaction = await connection.BeginTransactionAsync();

            int batchSize = 500; // Giảm batchSize vì câu lệnh có nhiều cột

            for (int i = 0; i < cardCaptains.Count; i += batchSize)
            {
                var batch = cardCaptains.Skip(i).Take(batchSize).ToList();

                var stringBuilder = new System.Text.StringBuilder();
                var parameters = new List<MySqlParameter>();

                stringBuilder.Append(@"
            INSERT INTO user_card_captains (
                user_id, card_captain_id, rare, level, experience, star, quality, block, quantity,
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
                (@user_id, @card_captain_id_{j}, @rare_{j}, 0, 0, 0, @quality_{j}, 0, @quantity_{j},
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
                    new MySqlParameter($"@card_captain_id_{j}", c.Id),
                    new MySqlParameter($"@rare_{j}", c.Rarity),
                    new MySqlParameter($"@quality_{j}", QualityEvaluatorHelper.CheckQuality(c.Rarity)),
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

                stringBuilder.Length--; // remove dấu phẩy thừa

                stringBuilder.Append(@"
            ON DUPLICATE KEY UPDATE
                quantity = COALESCE(user_card_captains.quantity, 0) + VALUES(quantity);
            ");

                await using var command = new MySqlCommand(stringBuilder.ToString(), connection, (MySqlTransaction)transaction);

                command.Parameters.AddWithValue("@user_id", userId);
                command.Parameters.AddRange(parameters.ToArray());

                await command.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();

            // 4. Trả về kết quả
            var operationType = DatabaseOperationType.None;

            if (batchResult.InsertedItems.Count > 0 && batchResult.UpdatedItems.Count > 0)
            {
                operationType = DatabaseOperationType.Mixed;
            }
            else if (batchResult.InsertedItems.Count > 0)
            {
                operationType = DatabaseOperationType.Inserted;
            }
            else if (batchResult.UpdatedItems.Count > 0)
            {
                operationType = DatabaseOperationType.Updated;
            }

            return new InsertOrUpdateResult<BatchOperationResultDTO<CardCaptains>>
            {
                Data = batchResult,
                OperationType = operationType
            };
        }
        catch (Exception ex)
        {
            Debug.LogError("Batch Error: " + ex.Message);
            return InsertOrUpdateResult<BatchOperationResultDTO<CardCaptains>>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<bool>> UpdateUserCardCaptainLevelAsync(string userId, CardCaptains cardCaptain)
    {
        if (cardCaptain == null)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Thêm điều kiện (level != @level OR experience != @experience) để tránh update thừa khi dữ liệu trùng khớp
            string updateSQL = @"
            UPDATE user_card_captains
            SET 
                level = @level, 
                experience = @experience
            WHERE user_id = @user_id 
              AND card_captain_id = @card_captain_id
              AND (level != @level OR experience != @experience);
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);

            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@card_captain_id", cardCaptain.Id);
            updateCommand.Parameters.AddWithValue("@level", cardCaptain.Level);
            updateCommand.Parameters.AddWithValue("@experience", cardCaptain.Experience);

            int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

            if (rowsAffected > 0)
            {
                return InsertOrUpdateResult<bool>.Updated(true);
            }
            else
            {
                return new InsertOrUpdateResult<bool>
                {
                    Data = false,
                    OperationType = DatabaseOperationType.None,
                    Message = MessageConstants.NOTHING_WAS_UPDATED
                };
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error UpdateUserCardCaptainLevel: " + ex.Message);
            return InsertOrUpdateResult<bool>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<bool>> UpdateUserCardCaptainStarAsync(string userId, CardCaptains cardCaptain)
    {
        if (cardCaptain == null)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra (star != @star OR quantity != @quantity) để không tốn I/O nếu dữ liệu không đổi
            string updateSQL = @"
            UPDATE user_card_captains
            SET 
                star = @star, 
                quantity = @quantity
            WHERE user_id = @user_id 
              AND card_captain_id = @card_captain_id
              AND (star != @star OR quantity != @quantity);
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);

            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@card_captain_id", cardCaptain.Id);
            updateCommand.Parameters.AddWithValue("@star", cardCaptain.Star);
            updateCommand.Parameters.AddWithValue("@quantity", cardCaptain.Quantity);

            int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

            if (rowsAffected > 0)
            {
                return InsertOrUpdateResult<bool>.Updated(true);
            }
            else
            {
                return new InsertOrUpdateResult<bool>
                {
                    Data = false,
                    OperationType = DatabaseOperationType.None,
                    Message = MessageConstants.NOTHING_WAS_UPDATED
                };
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error UpdateUserCardCaptainStar: " + ex.Message);
            return InsertOrUpdateResult<bool>.Failure(ex.Message);
        }
    }
    public async Task<CardCaptains> GetUserCardCaptainByIdAsync(string userId, string Id)
    {
        CardCaptains cardCaptain = new CardCaptains();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT uc.*, c.image
            FROM user_card_captains uc
            JOIN card_captains c ON uc.card_captain_id = c.id
            WHERE uc.card_captain_id = @id AND uc.user_id = @user_id";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@id", Id);
            selectCommand.Parameters.AddWithValue("@user_id", userId);

            await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                cardCaptain = new CardCaptains
                {
                    Id = reader.GetStringSafe("card_captain_id"),
                    Image = reader.GetStringSafe("image"),
                    Level = reader.GetIntSafe("level"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Experience = reader.GetDoubleSafe("experience"),
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

        return cardCaptain;
    }
    public async Task<BaseStats> GetTeamTotalStatsAsync(string userId)
    {
        BaseStats totalStats = new BaseStats();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT 
                -- Tính SUM trực tiếp đã áp dụng Quality cho từng Stat
                SUM(uc.health * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS health,
                SUM(uc.physical_attack * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS physical_attack,
                SUM(uc.physical_defense * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS physical_defense,
                SUM(uc.magical_attack * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS magical_attack,
                SUM(uc.magical_defense * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS magical_defense,
                SUM(uc.chemical_attack * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS chemical_attack,
                SUM(uc.chemical_defense * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS chemical_defense,
                SUM(uc.atomic_attack * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS atomic_attack,
                SUM(uc.atomic_defense * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS atomic_defense,
                SUM(uc.mental_attack * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS mental_attack,
                SUM(uc.mental_defense * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS mental_defense,
                SUM(uc.speed * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS speed,
                SUM(uc.critical_damage_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS critical_damage_rate,
                SUM(uc.critical_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS critical_rate,
                SUM(uc.critical_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS critical_resistance_rate,
                SUM(uc.ignore_critical_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS ignore_critical_rate,
                SUM(uc.penetration_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS penetration_rate,
                SUM(uc.penetration_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS penetration_resistance_rate,
                SUM(uc.evasion_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS evasion_rate,
                SUM(uc.damage_absorption_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS damage_absorption_rate,
                SUM(uc.ignore_damage_absorption_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS ignore_damage_absorption_rate,
                SUM(uc.absorbed_damage_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS absorbed_damage_rate,
                SUM(uc.vitality_regeneration_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS vitality_regeneration_rate,
                SUM(uc.vitality_regeneration_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS vitality_regeneration_resistance_rate,
                SUM(uc.accuracy_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS accuracy_rate,
                SUM(uc.lifesteal_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS lifesteal_rate,
                SUM(uc.shield_strength * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS shield_strength,
                SUM(uc.tenacity * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS tenacity,
                SUM(uc.resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS resistance_rate,
                SUM(uc.combo_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS combo_rate,
                SUM(uc.ignore_combo_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS ignore_combo_rate,
                SUM(uc.combo_damage_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS combo_damage_rate,
                SUM(uc.combo_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS combo_resistance_rate,
                SUM(uc.stun_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS stun_rate,
                SUM(uc.ignore_stun_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS ignore_stun_rate,
                SUM(uc.reflection_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS reflection_rate,
                SUM(uc.ignore_reflection_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS ignore_reflection_rate,
                SUM(uc.reflection_damage_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS reflection_damage_rate,
                SUM(uc.reflection_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS reflection_resistance_rate,
                SUM(uc.mana * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS mana,
                SUM(uc.mana_regeneration_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS mana_regeneration_rate,
                SUM(uc.damage_to_different_faction_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS damage_to_different_faction_rate,
                SUM(uc.resistance_to_different_faction_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS resistance_to_different_faction_rate,
                SUM(uc.damage_to_same_faction_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS damage_to_same_faction_rate,
                SUM(uc.resistance_to_same_faction_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS resistance_to_same_faction_rate,
                SUM(uc.normal_damage_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS normal_damage_rate,
                SUM(uc.normal_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS normal_resistance_rate,
                SUM(uc.skill_damage_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS skill_damage_rate,
                SUM(uc.skill_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS skill_resistance_rate
            FROM user_card_captains uc
            WHERE uc.user_id = @user_id AND uc.team_id IS NOT NULL;";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@user_id", userId);

            await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();
            // Chỉ có đúng 1 dòng trả về
            if (await reader.ReadAsync())
            {
                totalStats.Health = reader.GetDoubleSafe("health");
                totalStats.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                totalStats.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                totalStats.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                totalStats.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                totalStats.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                totalStats.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                totalStats.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                totalStats.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                totalStats.MentalAttack = reader.GetDoubleSafe("mental_attack");
                totalStats.MentalDefense = reader.GetDoubleSafe("mental_defense");
                totalStats.Speed = reader.GetDoubleSafe("speed");
                totalStats.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                totalStats.CriticalRate = reader.GetDoubleSafe("critical_rate");
                totalStats.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                totalStats.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                totalStats.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                totalStats.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                totalStats.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                totalStats.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                totalStats.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                totalStats.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                totalStats.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                totalStats.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                totalStats.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                totalStats.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                totalStats.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                totalStats.Tenacity = reader.GetDoubleSafe("tenacity");
                totalStats.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                totalStats.ComboRate = reader.GetDoubleSafe("combo_rate");
                totalStats.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                totalStats.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                totalStats.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                totalStats.StunRate = reader.GetDoubleSafe("stun_rate");
                totalStats.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                totalStats.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                totalStats.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                totalStats.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                totalStats.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                totalStats.Mana = reader.GetDoubleSafe("mana");
                totalStats.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                totalStats.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                totalStats.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                totalStats.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                totalStats.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                totalStats.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                totalStats.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                totalStats.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                totalStats.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");

            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return totalStats;
    }
    public async Task<BaseStats> GetTeamTotalStatsWithoutQualityAsync(string userId)
    {
        BaseStats totalStats = new BaseStats();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
           SELECT 
                SUM(uc.health) AS health,
                SUM(uc.physical_attack) AS physical_attack,
                SUM(uc.physical_defense) AS physical_defense,
                SUM(uc.magical_attack) AS magical_attack,
                SUM(uc.magical_defense) AS magical_defense,
                SUM(uc.chemical_attack) AS chemical_attack,
                SUM(uc.chemical_defense) AS chemical_defense,
                SUM(uc.atomic_attack) AS atomic_attack,
                SUM(uc.atomic_defense) AS atomic_defense,
                SUM(uc.mental_attack) AS mental_attack,
                SUM(uc.mental_defense) AS mental_defense,
                SUM(uc.speed) AS speed,
                SUM(uc.critical_damage_rate) AS critical_damage_rate,
                SUM(uc.critical_rate) AS critical_rate,
                SUM(uc.critical_resistance_rate) AS critical_resistance_rate,
                SUM(uc.ignore_critical_rate) AS ignore_critical_rate,
                SUM(uc.penetration_rate) AS penetration_rate,
                SUM(uc.penetration_resistance_rate) AS penetration_resistance_rate,
                SUM(uc.evasion_rate) AS evasion_rate,
                SUM(uc.damage_absorption_rate) AS damage_absorption_rate,
                SUM(uc.ignore_damage_absorption_rate) AS ignore_damage_absorption_rate,
                SUM(uc.absorbed_damage_rate) AS absorbed_damage_rate,
                SUM(uc.vitality_regeneration_rate) AS vitality_regeneration_rate,
                SUM(uc.vitality_regeneration_resistance_rate) AS vitality_regeneration_resistance_rate,
                SUM(uc.accuracy_rate) AS accuracy_rate,
                SUM(uc.lifesteal_rate) AS lifesteal_rate,
                SUM(uc.shield_strength) AS shield_strength,
                SUM(uc.tenacity) AS tenacity,
                SUM(uc.resistance_rate) AS resistance_rate,
                SUM(uc.combo_rate) AS combo_rate,
                SUM(uc.ignore_combo_rate) AS ignore_combo_rate,
                SUM(uc.combo_damage_rate) AS combo_damage_rate,
                SUM(uc.combo_resistance_rate) AS combo_resistance_rate,
                SUM(uc.stun_rate) AS stun_rate,
                SUM(uc.ignore_stun_rate) AS ignore_stun_rate,
                SUM(uc.reflection_rate) AS reflection_rate,
                SUM(uc.ignore_reflection_rate) AS ignore_reflection_rate,
                SUM(uc.reflection_damage_rate) AS reflection_damage_rate,
                SUM(uc.reflection_resistance_rate) AS reflection_resistance_rate,
                SUM(uc.mana) AS mana,
                SUM(uc.mana_regeneration_rate) AS mana_regeneration_rate,
                SUM(uc.damage_to_different_faction_rate) AS damage_to_different_faction_rate,
                SUM(uc.resistance_to_different_faction_rate) AS resistance_to_different_faction_rate,
                SUM(uc.damage_to_same_faction_rate) AS damage_to_same_faction_rate,
                SUM(uc.resistance_to_same_faction_rate) AS resistance_to_same_faction_rate,
                SUM(uc.normal_damage_rate) AS normal_damage_rate,
                SUM(uc.normal_resistance_rate) AS normal_resistance_rate,
                SUM(uc.skill_damage_rate) AS skill_damage_rate,
                SUM(uc.skill_resistance_rate) AS skill_resistance_rate
            FROM user_card_captains uc
            WHERE uc.user_id = @user_id AND uc.team_id IS NOT NULL;";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@user_id", userId);

            await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();
            // Chỉ có đúng 1 dòng trả về
            if (await reader.ReadAsync())
            {
                totalStats.Health = reader.GetDoubleSafe("health");
                totalStats.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                totalStats.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                totalStats.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                totalStats.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                totalStats.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                totalStats.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                totalStats.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                totalStats.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                totalStats.MentalAttack = reader.GetDoubleSafe("mental_attack");
                totalStats.MentalDefense = reader.GetDoubleSafe("mental_defense");
                totalStats.Speed = reader.GetDoubleSafe("speed");
                totalStats.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                totalStats.CriticalRate = reader.GetDoubleSafe("critical_rate");
                totalStats.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                totalStats.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                totalStats.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                totalStats.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                totalStats.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                totalStats.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                totalStats.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                totalStats.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                totalStats.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                totalStats.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                totalStats.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                totalStats.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                totalStats.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                totalStats.Tenacity = reader.GetDoubleSafe("tenacity");
                totalStats.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                totalStats.ComboRate = reader.GetDoubleSafe("combo_rate");
                totalStats.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                totalStats.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                totalStats.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                totalStats.StunRate = reader.GetDoubleSafe("stun_rate");
                totalStats.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                totalStats.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                totalStats.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                totalStats.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                totalStats.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                totalStats.Mana = reader.GetDoubleSafe("mana");
                totalStats.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                totalStats.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                totalStats.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                totalStats.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                totalStats.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                totalStats.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                totalStats.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                totalStats.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                totalStats.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");

            }

        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return totalStats;
    }
}