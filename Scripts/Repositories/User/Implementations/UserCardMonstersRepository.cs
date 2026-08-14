using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;

public class UserCardMonstersRepository : IUserCardMonstersRepository
{
    public async Task<List<CardMonsters>> GetUserCardMonstersAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardMonsters> cardMonsters = new List<CardMonsters>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            WITH AggregatedModules AS (
                    SELECT user_card_monster_id, SUM(current_multiplier) AS total_module_mult
                    FROM user_card_monsters_module
                    GROUP BY user_card_monster_id
                ),
                AggregatedUpgrades AS (
                    SELECT user_card_monster_id, SUM(current_multiplier) AS total_upgrade_mult
                    FROM user_card_monsters_upgrade
                    GROUP BY user_card_monster_id
                )
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
                        FROM card_monster_emblem che
                        JOIN emblems e ON che.emblem_id = e.id
                        WHERE che.card_monster_id = c.id
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
                        FROM card_monster_class chc
                        JOIN classes cl ON chc.class_id = cl.id
                        WHERE chc.card_monster_id = c.id
                    ) AS classes_json
                FROM user_card_monsters uc
                INNER JOIN card_monsters c ON uc.card_monster_id = c.id
                LEFT JOIN AggregatedModules am ON uc.card_monster_id = am.user_card_monster_id
                LEFT JOIN AggregatedUpgrades au ON uc.card_monster_id = au.user_card_monster_id
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
                CardMonsters cardMonster = new CardMonsters
                {
                    Id = reader.GetStringSafe("card_monster_id"),
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
                        cardMonster.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                        cardMonster.Emblems = new List<Emblems>();
                    }
                }

                string classesJson = reader.GetStringSafe("classes_json");

                if (!string.IsNullOrEmpty(classesJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Classes> trong C#
                        cardMonster.Class = JsonHelper.DeserializeClasses(classesJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có class, MySQL sinh ra chuỗi "[null]"
                        cardMonster.Class = new Classes();
                    }
                }
                UserModules userModule = new UserModules
                {
                    CurrentMultiplier = reader.GetDoubleSafe("module_multiplier"),
                };

                UserUpgrades userUpgrade = new UserUpgrades
                {
                    CurrentMultiplier = reader.GetDoubleSafe("upgrade_multiplier"),
                };

                cardMonster.UserModules = userModule;
                cardMonster.UserUpgrades = userUpgrade;

                cardMonsters.Add(cardMonster);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardMonsters;
    }
    public async Task<List<CardMonsters>> GetUserCardMonstersTeamAsync(string userId, string teamId, string position)
    {
        List<CardMonsters> cardMonsters = new List<CardMonsters>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            WITH AggregatedModules AS (
                    SELECT user_card_monster_id, SUM(current_multiplier) AS total_module_mult
                    FROM user_card_monsters_module
                    GROUP BY user_card_monster_id
                ),
                AggregatedUpgrades AS (
                    SELECT user_card_monster_id, SUM(current_multiplier) AS total_upgrade_mult
                    FROM user_card_monsters_upgrade
                    GROUP BY user_card_monster_id
                )
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
                        FROM card_monster_emblem che
                        JOIN emblems e ON che.emblem_id = e.id
                        WHERE che.card_monster_id = c.id
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
                        FROM card_monster_class chc
                        JOIN classes cl ON chc.class_id = cl.id
                        WHERE chc.card_monster_id = c.id
                    ) AS classes_json
                FROM user_card_monsters uc
                INNER JOIN card_monsters c ON uc.card_monster_id = c.id
                LEFT JOIN AggregatedModules am ON uc.card_monster_id = am.user_card_monster_id
                LEFT JOIN AggregatedUpgrades au ON uc.card_monster_id = au.user_card_monster_id
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
                CardMonsters cardMonster = new CardMonsters
                {
                    Id = reader.GetStringSafe("card_monster_id"),
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
                        cardMonster.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                        cardMonster.Emblems = new List<Emblems>();
                    }
                }

                string classesJson = reader.GetStringSafe("classes_json");

                if (!string.IsNullOrEmpty(classesJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Classes> trong C#
                        cardMonster.Class = JsonHelper.DeserializeClasses(classesJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có class, MySQL sinh ra chuỗi "[null]"
                        cardMonster.Class = new Classes();
                    }
                }
                UserModules userModule = new UserModules
                {
                    CurrentMultiplier = reader.GetDoubleSafe("module_multiplier"),
                };

                UserUpgrades userUpgrade = new UserUpgrades
                {
                    CurrentMultiplier = reader.GetDoubleSafe("upgrade_multiplier"),
                };

                cardMonster.UserModules = userModule;
                cardMonster.UserUpgrades = userUpgrade;

                cardMonsters.Add(cardMonster);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardMonsters;
    }
    public async Task<List<CardMonsters>> GetUserCardMonstersTeamWithoutPositionAsync(string userId, string teamId)
    {
        List<CardMonsters> cardMonsters = new List<CardMonsters>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            WITH AggregatedModules AS (
                    SELECT user_card_monster_id, SUM(current_multiplier) AS total_module_mult
                    FROM user_card_monsters_module
                    GROUP BY user_card_monster_id
                ),
                AggregatedUpgrades AS (
                    SELECT user_card_monster_id, SUM(current_multiplier) AS total_upgrade_mult
                    FROM user_card_monsters_upgrade
                    GROUP BY user_card_monster_id
                )
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
                        FROM card_monster_emblem che
                        JOIN emblems e ON che.emblem_id = e.id
                        WHERE che.card_monster_id = c.id
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
                        FROM card_monster_class chc
                        JOIN classes cl ON chc.class_id = cl.id
                        WHERE chc.card_monster_id = c.id
                    ) AS classes_json
                FROM user_card_monsters uc
                INNER JOIN card_monsters c ON uc.card_monster_id = c.id
                LEFT JOIN AggregatedModules am ON uc.card_monster_id = am.user_card_monster_id
                LEFT JOIN AggregatedUpgrades au ON uc.card_monster_id = au.user_card_monster_id
                LEFT JOIN teams t ON t.team_id = uc.team_id
            WHERE uc.user_id = @userId AND uc.team_id = @team_id
        ";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@userId", userId);
            selectCommand.Parameters.AddWithValue("@team_id", teamId);

            await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                CardMonsters cardMonster = new CardMonsters
                {
                    Id = reader.GetStringSafe("card_monster_id"),
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
                        cardMonster.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                        cardMonster.Emblems = new List<Emblems>();
                    }
                }

                string classesJson = reader.GetStringSafe("classes_json");

                if (!string.IsNullOrEmpty(classesJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Classes> trong C#
                        cardMonster.Class = JsonHelper.DeserializeClasses(classesJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có class, MySQL sinh ra chuỗi "[null]"
                        cardMonster.Class = new Classes();
                    }
                }
                UserModules userModule = new UserModules
                {
                    CurrentMultiplier = reader.GetDoubleSafe("module_multiplier"),
                };

                UserUpgrades userUpgrade = new UserUpgrades
                {
                    CurrentMultiplier = reader.GetDoubleSafe("upgrade_multiplier"),
                };

                cardMonster.UserModules = userModule;
                cardMonster.UserUpgrades = userUpgrade;

                cardMonsters.Add(cardMonster);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardMonsters;
    }
    public async Task<Dictionary<string, int>> GetUniqueCardMonstersTypesTeamAsync(string userId, string teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT c.type, COUNT(c.type) AS number
            FROM user_card_monsters uc
            LEFT JOIN card_monsters c ON uc.card_monster_id = c.id 
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
    public async Task<bool> UpdateTeamUserCardMonsterAsync(string userId, string teamId, string position, string cardId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string updateSQL = @"
            UPDATE user_card_monsters 
            SET team_id = @team_id, position = @position 
            WHERE user_id = @user_id AND card_monster_id = @card_monster_id;
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
            updateCommand.Parameters.AddWithValue("@team_id", teamId);
            updateCommand.Parameters.AddWithValue("@position", position);
            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@card_monster_id", cardId);

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
            FROM user_card_monsters 
            WHERE user_id = @user_id 
              AND card_monster_id = @card_monster_id 
              AND team_id IS NOT NULL 
              AND team_id != ''
            LIMIT 1;
        ";

            await using MySqlCommand command = new MySqlCommand(checkSQL, connection);
            command.Parameters.AddWithValue("@user_id", userId);
            command.Parameters.AddWithValue("@card_monster_id", cardId);

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
    public async Task<int> GetUserCardMonstersCountAsync(string userId, string search, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT COUNT(*) 
            FROM card_monsters c
            JOIN user_card_monsters uc ON c.id = uc.card_monster_id
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
    public async Task<int> GetUserCardMonstersTeamsPositionCountAsync(string userId, string teamId, string position)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT COUNT(*) 
            FROM user_card_monsters
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
    public async Task<int> GetUserCardMonstersTeamsCountAsync(string userId, string teamId)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT COUNT(*) 
            FROM user_card_monsters
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
    public async Task<InsertOrUpdateResult<CardMonsters>> InsertOrUpdateUserCardMonsterAsync(string userId, CardMonsters cardMonster)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Query thực hiện Insert hoặc Update nếu đã tồn tại Composite Primary Key (user_id, card_monster_id)
            string upsertSQL = @"
            INSERT INTO user_card_monsters (
                user_id, card_monster_id, rare, level, experience, star, quality, block, quantity,
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
                @user_id, @card_monster_id, @rare, 0, 0, 0, @quality, false, @quantity,
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
            command.Parameters.AddWithValue("@card_monster_id", cardMonster.Id);
            command.Parameters.AddWithValue("@rare", cardMonster.Rarity);
            command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(cardMonster.Rarity));
            command.Parameters.AddWithValue("@quantity", cardMonster.Quantity);
            command.Parameters.AddWithValue("@power", cardMonster.Power);
            command.Parameters.AddWithValue("@health", cardMonster.Health);
            command.Parameters.AddWithValue("@physical_attack", cardMonster.PhysicalAttack);
            command.Parameters.AddWithValue("@physical_defense", cardMonster.PhysicalDefense);
            command.Parameters.AddWithValue("@magical_attack", cardMonster.MagicalAttack);
            command.Parameters.AddWithValue("@magical_defense", cardMonster.MagicalDefense);
            command.Parameters.AddWithValue("@chemical_attack", cardMonster.ChemicalAttack);
            command.Parameters.AddWithValue("@chemical_defense", cardMonster.ChemicalDefense);
            command.Parameters.AddWithValue("@atomic_attack", cardMonster.AtomicAttack);
            command.Parameters.AddWithValue("@atomic_defense", cardMonster.AtomicDefense);
            command.Parameters.AddWithValue("@mental_attack", cardMonster.MentalAttack);
            command.Parameters.AddWithValue("@mental_defense", cardMonster.MentalDefense);
            command.Parameters.AddWithValue("@speed", cardMonster.Speed);
            command.Parameters.AddWithValue("@critical_damage_rate", cardMonster.CriticalDamageRate);
            command.Parameters.AddWithValue("@critical_rate", cardMonster.CriticalRate);
            command.Parameters.AddWithValue("@critical_resistance_rate", cardMonster.CriticalResistanceRate);
            command.Parameters.AddWithValue("@ignore_critical_rate", cardMonster.IgnoreCriticalRate);
            command.Parameters.AddWithValue("@penetration_rate", cardMonster.PenetrationRate);
            command.Parameters.AddWithValue("@penetration_resistance_rate", cardMonster.PenetrationResistanceRate);
            command.Parameters.AddWithValue("@evasion_rate", cardMonster.EvasionRate);
            command.Parameters.AddWithValue("@damage_absorption_rate", cardMonster.DamageAbsorptionRate);
            command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardMonster.IgnoreDamageAbsorptionRate);
            command.Parameters.AddWithValue("@absorbed_damage_rate", cardMonster.AbsorbedDamageRate);
            command.Parameters.AddWithValue("@vitality_regeneration_rate", cardMonster.VitalityRegenerationRate);
            command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardMonster.VitalityRegenerationResistanceRate);
            command.Parameters.AddWithValue("@accuracy_rate", cardMonster.AccuracyRate);
            command.Parameters.AddWithValue("@lifesteal_rate", cardMonster.LifestealRate);
            command.Parameters.AddWithValue("@shield_strength", cardMonster.ShieldStrength);
            command.Parameters.AddWithValue("@tenacity", cardMonster.Tenacity);
            command.Parameters.AddWithValue("@resistance_rate", cardMonster.ResistanceRate);
            command.Parameters.AddWithValue("@combo_rate", cardMonster.ComboRate);
            command.Parameters.AddWithValue("@ignore_combo_rate", cardMonster.IgnoreComboRate);
            command.Parameters.AddWithValue("@combo_damage_rate", cardMonster.ComboDamageRate);
            command.Parameters.AddWithValue("@combo_resistance_rate", cardMonster.ComboResistanceRate);
            command.Parameters.AddWithValue("@stun_rate", cardMonster.StunRate);
            command.Parameters.AddWithValue("@ignore_stun_rate", cardMonster.IgnoreStunRate);
            command.Parameters.AddWithValue("@reflection_rate", cardMonster.ReflectionRate);
            command.Parameters.AddWithValue("@ignore_reflection_rate", cardMonster.IgnoreReflectionRate);
            command.Parameters.AddWithValue("@reflection_damage_rate", cardMonster.ReflectionDamageRate);
            command.Parameters.AddWithValue("@reflection_resistance_rate", cardMonster.ReflectionResistanceRate);
            command.Parameters.AddWithValue("@mana", cardMonster.Mana);
            command.Parameters.AddWithValue("@mana_regeneration_rate", cardMonster.ManaRegenerationRate);
            command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardMonster.DamageToDifferentFactionRate);
            command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardMonster.ResistanceToDifferentFactionRate);
            command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardMonster.DamageToSameFactionRate);
            command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardMonster.ResistanceToSameFactionRate);
            command.Parameters.AddWithValue("@normal_damage_rate", cardMonster.NormalDamageRate);
            command.Parameters.AddWithValue("@normal_resistance_rate", cardMonster.NormalResistanceRate);
            command.Parameters.AddWithValue("@skill_damage_rate", cardMonster.SkillDamageRate);
            command.Parameters.AddWithValue("@skill_resistance_rate", cardMonster.SkillResistanceRate);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            // MySQL quy ước: Insert mới = 1, Update = 2, Không thay đổi = 0
            if (rowsAffected == 1)
            {
                return InsertOrUpdateResult<CardMonsters>.Inserted(cardMonster);
            }
            else if (rowsAffected == 2 || rowsAffected == 0)
            {
                return InsertOrUpdateResult<CardMonsters>.Updated(cardMonster);
            }

            return InsertOrUpdateResult<CardMonsters>.Failure();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Database Error: " + ex.Message);
            return InsertOrUpdateResult<CardMonsters>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<BatchOperationResultDTO<CardMonsters>>> InsertOrUpdateUserCardMonstersBatchAsync(
    string userId, List<CardMonsters> cardMonsters)
    {
        if (cardMonsters == null || cardMonsters.Count == 0)
        {
            return new InsertOrUpdateResult<BatchOperationResultDTO<CardMonsters>>
            {
                Data = new BatchOperationResultDTO<CardMonsters>(),
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        string connectionString = DatabaseConfig.ConnectionString;
        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // 1. SELECT chỉ đúng 1 cột ID duy nhất để tối ưu RAM và I/O
            var existingIds = new HashSet<string>();
            string checkSql = "SELECT card_monster_id FROM user_card_monsters WHERE user_id = @user_id;";

            await using (var checkCmd = new MySqlCommand(checkSql, connection))
            {
                checkCmd.Parameters.AddWithValue("@user_id", userId);
                await using var reader = await checkCmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    existingIds.Add(reader.GetString(0));
                }
            }

            // 2. Phân loại Card Heroes trong RAM C#
            var batchResult = new BatchOperationResultDTO<CardMonsters>();
            foreach (var card in cardMonsters)
            {
                if (existingIds.Contains(card.Id))
                    batchResult.UpdatedItems.Add(card);
                else
                    batchResult.InsertedItems.Add(card);
            }

            // 3. Thực hiện Bulk Insert/Update
            await using var transaction = await connection.BeginTransactionAsync();

            int batchSize = 200; // Mức an toàn và tối ưu nhất cho ~60 parameters/row
            int totalCards = cardMonsters.Count;

            // Bỏ Skip().Take(), dùng vòng lặp bước nhảy i += batchSize
            for (int i = 0; i < totalCards; i += batchSize)
            {
                int currentBatchCount = Math.Min(batchSize, totalCards - i);

                var stringBuilder = new System.Text.StringBuilder();
                await using var command = new MySqlCommand { Connection = connection, Transaction = (MySqlTransaction)transaction };

                stringBuilder.Append(@"
            INSERT INTO user_card_monsters (
                user_id, card_monster_id, rare, level, experience, star, quality, block, quantity,
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

                for (int j = 0; j < currentBatchCount; j++)
                {
                    // Truy cập thẳng index O(1) thay vì Skip/Take
                    var c = cardMonsters[i + j];

                    stringBuilder.Append($"(@user_id, @card_monster_id_{j}, @rare_{j}, 0, 0, 0, @quality_{j}, 0, @quantity_{j}, ")
                                 .Append($"@power_{j}, @health_{j}, @physical_attack_{j}, @physical_defense_{j}, @magical_attack_{j}, @magical_defense_{j}, ")
                                 .Append($"@chemical_attack_{j}, @chemical_defense_{j}, @atomic_attack_{j}, @atomic_defense_{j}, @mental_attack_{j}, @mental_defense_{j}, ")
                                 .Append($"@speed_{j}, @critical_damage_rate_{j}, @critical_rate_{j}, @critical_resistance_rate_{j}, @ignore_critical_rate_{j}, ")
                                 .Append($"@penetration_rate_{j}, @penetration_resistance_rate_{j}, ")
                                 .Append($"@evasion_rate_{j}, @damage_absorption_rate_{j}, @ignore_damage_absorption_rate_{j}, @absorbed_damage_rate_{j}, ")
                                 .Append($"@vitality_regeneration_rate_{j}, @vitality_regeneration_resistance_rate_{j}, ")
                                 .Append($"@accuracy_rate_{j}, @lifesteal_rate_{j}, @shield_strength_{j}, @tenacity_{j}, @resistance_rate_{j}, ")
                                 .Append($"@combo_rate_{j}, @ignore_combo_rate_{j}, @combo_damage_rate_{j}, @combo_resistance_rate_{j}, ")
                                 .Append($"@stun_rate_{j}, @ignore_stun_rate_{j}, ")
                                 .Append($"@reflection_rate_{j}, @ignore_reflection_rate_{j}, @reflection_damage_rate_{j}, @reflection_resistance_rate_{j}, ")
                                 .Append($"@mana_{j}, @mana_regeneration_rate_{j}, ")
                                 .Append($"@damage_to_different_faction_rate_{j}, @resistance_to_different_faction_rate_{j}, ")
                                 .Append($"@damage_to_same_faction_rate_{j}, @resistance_to_same_faction_rate_{j}, ")
                                 .Append($"@normal_damage_rate_{j}, @normal_resistance_rate_{j}, ")
                                 .Append($"@skill_damage_rate_{j}, @skill_resistance_rate_{j}),");

                    // Thêm trực tiếp vào Parameters collection của Command (Không tạo new[] Array phụ)
                    var p = command.Parameters;
                    p.AddWithValue($"@card_monster_id_{j}", c.Id);
                    p.AddWithValue($"@rare_{j}", c.Rarity);
                    p.AddWithValue($"@quality_{j}", QualityEvaluatorHelper.CheckQuality(c.Rarity));
                    p.AddWithValue($"@quantity_{j}", c.Quantity);
                    p.AddWithValue($"@power_{j}", c.Power);
                    p.AddWithValue($"@health_{j}", c.Health);
                    p.AddWithValue($"@physical_attack_{j}", c.PhysicalAttack);
                    p.AddWithValue($"@physical_defense_{j}", c.PhysicalDefense);
                    p.AddWithValue($"@magical_attack_{j}", c.MagicalAttack);
                    p.AddWithValue($"@magical_defense_{j}", c.MagicalDefense);
                    p.AddWithValue($"@chemical_attack_{j}", c.ChemicalAttack);
                    p.AddWithValue($"@chemical_defense_{j}", c.ChemicalDefense);
                    p.AddWithValue($"@atomic_attack_{j}", c.AtomicAttack);
                    p.AddWithValue($"@atomic_defense_{j}", c.AtomicDefense);
                    p.AddWithValue($"@mental_attack_{j}", c.MentalAttack);
                    p.AddWithValue($"@mental_defense_{j}", c.MentalDefense);
                    p.AddWithValue($"@speed_{j}", c.Speed);
                    p.AddWithValue($"@critical_damage_rate_{j}", c.CriticalDamageRate);
                    p.AddWithValue($"@critical_rate_{j}", c.CriticalRate);
                    p.AddWithValue($"@critical_resistance_rate_{j}", c.CriticalResistanceRate);
                    p.AddWithValue($"@ignore_critical_rate_{j}", c.IgnoreCriticalRate);
                    p.AddWithValue($"@penetration_rate_{j}", c.PenetrationRate);
                    p.AddWithValue($"@penetration_resistance_rate_{j}", c.PenetrationResistanceRate);
                    p.AddWithValue($"@evasion_rate_{j}", c.EvasionRate);
                    p.AddWithValue($"@damage_absorption_rate_{j}", c.DamageAbsorptionRate);
                    p.AddWithValue($"@ignore_damage_absorption_rate_{j}", c.IgnoreDamageAbsorptionRate);
                    p.AddWithValue($"@absorbed_damage_rate_{j}", c.AbsorbedDamageRate);
                    p.AddWithValue($"@vitality_regeneration_rate_{j}", c.VitalityRegenerationRate);
                    p.AddWithValue($"@vitality_regeneration_resistance_rate_{j}", c.VitalityRegenerationResistanceRate);
                    p.AddWithValue($"@accuracy_rate_{j}", c.AccuracyRate);
                    p.AddWithValue($"@lifesteal_rate_{j}", c.LifestealRate);
                    p.AddWithValue($"@shield_strength_{j}", c.ShieldStrength);
                    p.AddWithValue($"@tenacity_{j}", c.Tenacity);
                    p.AddWithValue($"@resistance_rate_{j}", c.ResistanceRate);
                    p.AddWithValue($"@combo_rate_{j}", c.ComboRate);
                    p.AddWithValue($"@ignore_combo_rate_{j}", c.IgnoreComboRate);
                    p.AddWithValue($"@combo_damage_rate_{j}", c.ComboDamageRate);
                    p.AddWithValue($"@combo_resistance_rate_{j}", c.ComboResistanceRate);
                    p.AddWithValue($"@stun_rate_{j}", c.StunRate);
                    p.AddWithValue($"@ignore_stun_rate_{j}", c.IgnoreStunRate);
                    p.AddWithValue($"@reflection_rate_{j}", c.ReflectionRate);
                    p.AddWithValue($"@ignore_reflection_rate_{j}", c.IgnoreReflectionRate);
                    p.AddWithValue($"@reflection_damage_rate_{j}", c.ReflectionDamageRate);
                    p.AddWithValue($"@reflection_resistance_rate_{j}", c.ReflectionResistanceRate);
                    p.AddWithValue($"@mana_{j}", c.Mana);
                    p.AddWithValue($"@mana_regeneration_rate_{j}", c.ManaRegenerationRate);
                    p.AddWithValue($"@damage_to_different_faction_rate_{j}", c.DamageToDifferentFactionRate);
                    p.AddWithValue($"@resistance_to_different_faction_rate_{j}", c.ResistanceToDifferentFactionRate);
                    p.AddWithValue($"@damage_to_same_faction_rate_{j}", c.DamageToSameFactionRate);
                    p.AddWithValue($"@resistance_to_same_faction_rate_{j}", c.ResistanceToSameFactionRate);
                    p.AddWithValue($"@normal_damage_rate_{j}", c.NormalDamageRate);
                    p.AddWithValue($"@normal_resistance_rate_{j}", c.NormalResistanceRate);
                    p.AddWithValue($"@skill_damage_rate_{j}", c.SkillDamageRate);
                    p.AddWithValue($"@skill_resistance_rate_{j}", c.SkillResistanceRate);
                }

                stringBuilder.Length--; // Xóa dấu phẩy thừa cuối cùng

                stringBuilder.Append(@"
            ON DUPLICATE KEY UPDATE
                quantity = COALESCE(user_card_monsters.quantity, 0) + VALUES(quantity);");

                command.CommandText = stringBuilder.ToString();
                command.Parameters.AddWithValue("@user_id", userId);

                await command.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();

            // 4. Trả về kết quả
            var operationType = DatabaseOperationType.None;
            if (batchResult.InsertedItems.Count > 0 && batchResult.UpdatedItems.Count > 0)
                operationType = DatabaseOperationType.Mixed;
            else if (batchResult.InsertedItems.Count > 0)
                operationType = DatabaseOperationType.Inserted;
            else if (batchResult.UpdatedItems.Count > 0)
                operationType = DatabaseOperationType.Updated;

            return new InsertOrUpdateResult<BatchOperationResultDTO<CardMonsters>>
            {
                Data = batchResult,
                OperationType = operationType
            };
        }
        catch (Exception ex)
        {
            Debug.LogError("Batch Error: " + ex.Message);
            return InsertOrUpdateResult<BatchOperationResultDTO<CardMonsters>>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<bool>> UpdateUserCardMonsterLevelAsync(string userId, CardMonsters cardMonster)
    {
        if (cardMonster == null)
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
            UPDATE user_card_monsters
            SET 
                level = @level, 
                experience = @experience
            WHERE user_id = @user_id 
              AND card_monster_id = @card_monster_id
              AND (level != @level OR experience != @experience);
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);

            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@card_monster_id", cardMonster.Id);
            updateCommand.Parameters.AddWithValue("@level", cardMonster.Level);
            updateCommand.Parameters.AddWithValue("@experience", cardMonster.Experience);

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
            Debug.LogError("Error UpdateUserCardMonsterLevel: " + ex.Message);
            return InsertOrUpdateResult<bool>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<bool>> UpdateUserCardMonsterStarAsync(string userId, CardMonsters cardMonster)
    {
        if (cardMonster == null)
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
            UPDATE user_card_monsters
            SET 
                star = @star, 
                quantity = @quantity
            WHERE user_id = @user_id 
              AND card_monster_id = @card_monster_id
              AND (star != @star OR quantity != @quantity);
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);

            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@card_monster_id", cardMonster.Id);
            updateCommand.Parameters.AddWithValue("@star", cardMonster.Star);
            updateCommand.Parameters.AddWithValue("@quantity", cardMonster.Quantity);

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
            Debug.LogError("Error UpdateUserCardMonsterStar: " + ex.Message);
            return InsertOrUpdateResult<bool>.Failure(ex.Message);
        }
    }
    public async Task<CardMonsters> GetUserCardMonsterByIdAsync(string userId, string Id)
    {
        CardMonsters cardMonster = new CardMonsters();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            WITH AggregatedModules AS (
                    SELECT user_card_monster_id, SUM(current_multiplier) AS total_module_mult
                    FROM user_card_monsters_module
                    GROUP BY user_card_monster_id
                ),
                AggregatedUpgrades AS (
                    SELECT user_card_monster_id, SUM(current_multiplier) AS total_upgrade_mult
                    FROM user_card_monsters_upgrade
                    GROUP BY user_card_monster_id
                )
            SELECT uc.*, c.image
            FROM user_card_monsters uc
            INNER JOIN card_monsters c ON uc.card_monster_id = c.id
                LEFT JOIN AggregatedModules am ON uc.card_monster_id = am.user_card_monster_id
                LEFT JOIN AggregatedUpgrades au ON uc.card_monster_id = au.user_card_monster_id
            WHERE uc.card_monster_id = @id AND uc.user_id = @user_id";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@id", Id);
            selectCommand.Parameters.AddWithValue("@user_id", userId);

            await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                cardMonster = new CardMonsters
                {
                    Id = reader.GetStringSafe("card_monster_id"),
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
                UserModules userModule = new UserModules
                {
                    CurrentMultiplier = reader.GetDoubleSafe("module_multiplier"),
                };

                UserUpgrades userUpgrade = new UserUpgrades
                {
                    CurrentMultiplier = reader.GetDoubleSafe("upgrade_multiplier"),
                };

                cardMonster.UserModules = userModule;
                cardMonster.UserUpgrades = userUpgrade;
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardMonster;
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
            WITH CalculatedCards AS (
                SELECT 
                    uc.*,
                    (
                        -- Quality: 0 -> 1.0, 1 -> 1.1
                        (1 + COALESCE(uc.quality, 0) / 10.0) 
                        
                        -- Star: 0 -> 1.0, 1 -> 2.0, 2 -> 3.0
                        * (1 + COALESCE(uc.star, 0)) 
                        
                        -- Level: 0 -> 1.0, 10 -> 1.1
                        * (1 + COALESCE(uc.level, 0) / 100.0) 
                        
                        -- Module: 0/NULL -> 1.0
                        * (1 + COALESCE(ubm.current_multiplier, 0) / 100.0) 
                        
                        -- Upgrade: 0/NULL -> 1.0
                        * (1 + COALESCE(ubu.current_multiplier, 0) / 100.0)
                    ) AS total_multiplier
                FROM user_card_monsters uc
                INNER JOIN teams t ON uc.team_id = t.team_id AND t.is_main = 1
                LEFT JOIN user_card_monsters_module ubm ON uc.card_monster_id = ubm.user_card_monster_id
                LEFT JOIN user_card_monsters_upgrade ubu ON uc.card_monster_id = ubu.user_card_monster_id
                WHERE uc.user_id = @user_id AND uc.team_id IS NOT NULL
            )
            SELECT 
                SUM(health * total_multiplier) AS health,
                SUM(physical_attack * total_multiplier) AS physical_attack,
                SUM(physical_defense * total_multiplier) AS physical_defense,
                SUM(magical_attack * total_multiplier) AS magical_attack,
                SUM(magical_defense * total_multiplier) AS magical_defense,
                SUM(chemical_attack * total_multiplier) AS chemical_attack,
                SUM(chemical_defense * total_multiplier) AS chemical_defense,
                SUM(atomic_attack * total_multiplier) AS atomic_attack,
                SUM(atomic_defense * total_multiplier) AS atomic_defense,
                SUM(mental_attack * total_multiplier) AS mental_attack,
                SUM(mental_defense * total_multiplier) AS mental_defense,
                SUM(speed * total_multiplier) AS speed,
                SUM(critical_damage_rate * total_multiplier) AS critical_damage_rate,
                SUM(critical_rate * total_multiplier) AS critical_rate,
                SUM(critical_resistance_rate * total_multiplier) AS critical_resistance_rate,
                SUM(ignore_critical_rate * total_multiplier) AS ignore_critical_rate,
                SUM(penetration_rate * total_multiplier) AS penetration_rate,
                SUM(penetration_resistance_rate * total_multiplier) AS penetration_resistance_rate,
                SUM(evasion_rate * total_multiplier) AS evasion_rate,
                SUM(damage_absorption_rate * total_multiplier) AS damage_absorption_rate,
                SUM(ignore_damage_absorption_rate * total_multiplier) AS ignore_damage_absorption_rate,
                SUM(absorbed_damage_rate * total_multiplier) AS absorbed_damage_rate,
                SUM(vitality_regeneration_rate * total_multiplier) AS vitality_regeneration_rate,
                SUM(vitality_regeneration_resistance_rate * total_multiplier) AS vitality_regeneration_resistance_rate,
                SUM(accuracy_rate * total_multiplier) AS accuracy_rate,
                SUM(lifesteal_rate * total_multiplier) AS lifesteal_rate,
                SUM(shield_strength * total_multiplier) AS shield_strength,
                SUM(tenacity * total_multiplier) AS tenacity,
                SUM(resistance_rate * total_multiplier) AS resistance_rate,
                SUM(combo_rate * total_multiplier) AS combo_rate,
                SUM(ignore_combo_rate * total_multiplier) AS ignore_combo_rate,
                SUM(combo_damage_rate * total_multiplier) AS combo_damage_rate,
                SUM(combo_resistance_rate * total_multiplier) AS combo_resistance_rate,
                SUM(stun_rate * total_multiplier) AS stun_rate,
                SUM(ignore_stun_rate * total_multiplier) AS ignore_stun_rate,
                SUM(reflection_rate * total_multiplier) AS reflection_rate,
                SUM(ignore_reflection_rate * total_multiplier) AS ignore_reflection_rate,
                SUM(reflection_damage_rate * total_multiplier) AS reflection_damage_rate,
                SUM(reflection_resistance_rate * total_multiplier) AS reflection_resistance_rate,
                SUM(mana * total_multiplier) AS mana,
                SUM(mana_regeneration_rate * total_multiplier) AS mana_regeneration_rate,
                SUM(damage_to_different_faction_rate * total_multiplier) AS damage_to_different_faction_rate,
                SUM(resistance_to_different_faction_rate * total_multiplier) AS resistance_to_different_faction_rate,
                SUM(damage_to_same_faction_rate * total_multiplier) AS damage_to_same_faction_rate,
                SUM(resistance_to_same_faction_rate * total_multiplier) AS resistance_to_same_faction_rate,
                SUM(normal_damage_rate * total_multiplier) AS normal_damage_rate,
                SUM(normal_resistance_rate * total_multiplier) AS normal_resistance_rate,
                SUM(skill_damage_rate * total_multiplier) AS skill_damage_rate,
                SUM(skill_resistance_rate * total_multiplier) AS skill_resistance_rate
            FROM CalculatedCards;";

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
            FROM user_card_monsters uc
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