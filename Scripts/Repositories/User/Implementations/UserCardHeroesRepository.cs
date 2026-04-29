using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;

public class UserCardHeroesRepository : IUserCardHeroesRepository
{
    public async Task<List<CardHeroes>> GetUserCardHeroesAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardHeroes> cardHeroes = new List<CardHeroes>();
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
                        FROM card_hero_emblem che
                        JOIN emblems e ON che.emblem_id = e.id
                        WHERE che.card_hero_id = c.id
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
                        FROM card_hero_class chc
                        JOIN classes cl ON chc.class_id = cl.id
                        WHERE chc.card_hero_id = c.id
                    ) AS classes_json
                FROM user_card_heroes uc
                LEFT JOIN card_heroes c ON c.id = uc.card_hero_id 
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
                CardHeroes cardHero = new CardHeroes
                {
                    Id = reader.GetStringSafe("card_hero_id"),
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
                        cardHero.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                        cardHero.Emblems = new List<Emblems>();
                    }
                }

                string classesJson = reader.GetStringSafe("classes_json");

                if (!string.IsNullOrEmpty(classesJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Classes> trong C#
                        cardHero.Classes = JsonHelper.DeserializeClasses(classesJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có class, MySQL sinh ra chuỗi "[null]"
                        cardHero.Classes = new List<Classes>();
                    }
                }

                cardHeroes.Add(cardHero);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardHeroes;
    }
    public async Task<List<CardHeroes>> GetUserCardHeroesTeamAsync(string user_id, string teamId, string position)
    {
        List<CardHeroes> cardHeroes = new List<CardHeroes>();
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
                        FROM card_hero_emblem che
                        JOIN emblems e ON che.emblem_id = e.id
                        WHERE che.card_hero_id = c.id
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
                        FROM card_hero_class chc
                        JOIN classes cl ON chc.class_id = cl.id
                        WHERE chc.card_hero_id = c.id
                    ) AS classes_json
                FROM user_card_heroes uc
                LEFT JOIN card_heroes c ON c.id = uc.card_hero_id 
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
                CardHeroes cardHero = new CardHeroes
                {
                    Id = reader.GetStringSafe("card_hero_id"),
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
                        cardHero.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                        cardHero.Emblems = new List<Emblems>();
                    }
                }

                string classesJson = reader.GetStringSafe("classes_json");

                if (!string.IsNullOrEmpty(classesJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Classes> trong C#
                        cardHero.Classes = JsonHelper.DeserializeClasses(classesJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có class, MySQL sinh ra chuỗi "[null]"
                        cardHero.Classes = new List<Classes>();
                    }
                }

                cardHeroes.Add(cardHero);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardHeroes;
    }
    public async Task<List<CardHeroes>> GetUserCardHeroesTeamWithoutPositionAsync(string user_id, string teamId)
    {
        List<CardHeroes> cardHeroes = new List<CardHeroes>();
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
                        FROM card_hero_emblem che
                        JOIN emblems e ON che.emblem_id = e.id
                        WHERE che.card_hero_id = c.id
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
                        FROM card_hero_class chc
                        JOIN classes cl ON chc.class_id = cl.id
                        WHERE chc.card_hero_id = c.id
                    ) AS classes_json
                FROM user_card_heroes uc
                LEFT JOIN card_heroes c ON c.id = uc.card_hero_id 
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
                CardHeroes cardHero = new CardHeroes
                {
                    Id = reader.GetStringSafe("card_hero_id"),
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
                        cardHero.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                        cardHero.Emblems = new List<Emblems>();
                    }
                }

                string classesJson = reader.GetStringSafe("classes_json");

                if (!string.IsNullOrEmpty(classesJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Classes> trong C#
                        cardHero.Classes = JsonHelper.DeserializeClasses(classesJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có class, MySQL sinh ra chuỗi "[null]"
                        cardHero.Classes = new List<Classes>();
                    }
                }

                cardHeroes.Add(cardHero);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardHeroes;
    }
    public async Task<Dictionary<string, int>> GetUniqueCardHeroesTypesTeamAsync(string teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT c.type, COUNT(c.type) AS number
            FROM user_card_heroes uc
            LEFT JOIN card_heroes c ON uc.card_hero_id = c.id 
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
    public async Task<bool> UpdateTeamCardHeroAsync(string team_id, string position, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string updateSQL = @"
            UPDATE user_card_heroes 
            SET team_id = @team_id, position = @position 
            WHERE user_id = @user_id AND card_hero_id = @card_hero_id;
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
            updateCommand.Parameters.AddWithValue("@team_id", team_id);
            updateCommand.Parameters.AddWithValue("@position", position);
            updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            updateCommand.Parameters.AddWithValue("@card_hero_id", card_id);

            await updateCommand.ExecuteNonQueryAsync();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return false;
        }

        return true;
    }
    public async Task<int> GetUserCardHeroesCountAsync(string user_id, string search, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT COUNT(*) 
            FROM card_heroes c
            JOIN user_card_heroes uc ON c.id = uc.card_hero_id
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
    public async Task<int> GetUserCardHeroesTeamsPositionCountAsync(string user_id, string team_id, string position)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT COUNT(*) 
            FROM user_card_heroes
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
    public async Task<int> GetUserCardHeroesTeamsCountAsync(string user_id, string team_id)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT COUNT(*) 
            FROM user_card_heroes
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
    public async Task<bool> InsertUserCardHeroAsync(CardHeroes cardHero)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM user_card_heroes
            WHERE user_id = @user_id AND card_hero_id = @card_hero_id;
        ";

            await using MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            checkCommand.Parameters.AddWithValue("@card_hero_id", cardHero.Id);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO user_card_heroes (
                    user_id, card_hero_id, rare, level, experiment, star, quality, block, quantity,
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
                    @user_id, @card_hero_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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
                insertCommand.Parameters.AddWithValue("@card_hero_id", cardHero.Id);
                insertCommand.Parameters.AddWithValue("@rare", cardHero.Rare);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@experiment", 0);
                insertCommand.Parameters.AddWithValue("@star", 0);
                insertCommand.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(cardHero.Rare));
                insertCommand.Parameters.AddWithValue("@block", false);
                insertCommand.Parameters.AddWithValue("@quantity", cardHero.Quantity);
                insertCommand.Parameters.AddWithValue("@power", cardHero.Power);
                insertCommand.Parameters.AddWithValue("@health", cardHero.Health);
                insertCommand.Parameters.AddWithValue("@physical_attack", cardHero.PhysicalAttack);
                insertCommand.Parameters.AddWithValue("@physical_defense", cardHero.PhysicalDefense);
                insertCommand.Parameters.AddWithValue("@magical_attack", cardHero.MagicalAttack);
                insertCommand.Parameters.AddWithValue("@magical_defense", cardHero.MagicalDefense);
                insertCommand.Parameters.AddWithValue("@chemical_attack", cardHero.ChemicalAttack);
                insertCommand.Parameters.AddWithValue("@chemical_defense", cardHero.ChemicalDefense);
                insertCommand.Parameters.AddWithValue("@atomic_attack", cardHero.AtomicAttack);
                insertCommand.Parameters.AddWithValue("@atomic_defense", cardHero.AtomicDefense);
                insertCommand.Parameters.AddWithValue("@mental_attack", cardHero.MentalAttack);
                insertCommand.Parameters.AddWithValue("@mental_defense", cardHero.MentalDefense);
                insertCommand.Parameters.AddWithValue("@speed", cardHero.Speed);
                insertCommand.Parameters.AddWithValue("@critical_damage_rate", cardHero.CriticalDamageRate);
                insertCommand.Parameters.AddWithValue("@critical_rate", cardHero.CriticalRate);
                insertCommand.Parameters.AddWithValue("@critical_resistance_rate", cardHero.CriticalResistanceRate);
                insertCommand.Parameters.AddWithValue("@ignore_critical_rate", cardHero.IgnoreCriticalRate);
                insertCommand.Parameters.AddWithValue("@penetration_rate", cardHero.PenetrationRate);
                insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", cardHero.PenetrationResistanceRate);
                insertCommand.Parameters.AddWithValue("@evasion_rate", cardHero.EvasionRate);
                insertCommand.Parameters.AddWithValue("@damage_absorption_rate", cardHero.DamageAbsorptionRate);
                insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardHero.IgnoreDamageAbsorptionRate);
                insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", cardHero.AbsorbedDamageRate);
                insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", cardHero.VitalityRegenerationRate);
                insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardHero.VitalityRegenerationResistanceRate);
                insertCommand.Parameters.AddWithValue("@accuracy_rate", cardHero.AccuracyRate);
                insertCommand.Parameters.AddWithValue("@lifesteal_rate", cardHero.LifestealRate);
                insertCommand.Parameters.AddWithValue("@shield_strength", cardHero.ShieldStrength);
                insertCommand.Parameters.AddWithValue("@tenacity", cardHero.Tenacity);
                insertCommand.Parameters.AddWithValue("@resistance_rate", cardHero.ResistanceRate);
                insertCommand.Parameters.AddWithValue("@combo_rate", cardHero.ComboRate);
                insertCommand.Parameters.AddWithValue("@ignore_combo_rate", cardHero.IgnoreComboRate);
                insertCommand.Parameters.AddWithValue("@combo_damage_rate", cardHero.ComboDamageRate);
                insertCommand.Parameters.AddWithValue("@combo_resistance_rate", cardHero.ComboResistanceRate);
                insertCommand.Parameters.AddWithValue("@stun_rate", cardHero.StunRate);
                insertCommand.Parameters.AddWithValue("@ignore_stun_rate", cardHero.IgnoreStunRate);
                insertCommand.Parameters.AddWithValue("@reflection_rate", cardHero.ReflectionRate);
                insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", cardHero.IgnoreReflectionRate);
                insertCommand.Parameters.AddWithValue("@reflection_damage_rate", cardHero.ReflectionDamageRate);
                insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", cardHero.ReflectionResistanceRate);
                insertCommand.Parameters.AddWithValue("@mana", cardHero.Mana);
                insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", cardHero.ManaRegenerationRate);
                insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", cardHero.DamageToDifferentFactionRate);
                insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardHero.ResistanceToDifferentFactionRate);
                insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", cardHero.DamageToSameFactionRate);
                insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardHero.ResistanceToSameFactionRate);
                insertCommand.Parameters.AddWithValue("@normal_damage_rate", cardHero.NormalDamageRate);
                insertCommand.Parameters.AddWithValue("@normal_resistance_rate", cardHero.NormalResistanceRate);
                insertCommand.Parameters.AddWithValue("@skill_damage_rate", cardHero.SkillDamageRate);
                insertCommand.Parameters.AddWithValue("@skill_resistance_rate", cardHero.SkillResistanceRate);

                await insertCommand.ExecuteNonQueryAsync();
            }
            else
            {
                // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                string updateSQL = @"
                UPDATE user_card_heroes
                SET quantity = @quantity
                WHERE user_id = @user_id AND card_hero_id = @card_hero_id;
            ";

                await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
                updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                updateCommand.Parameters.AddWithValue("@card_hero_id", cardHero.Id);
                updateCommand.Parameters.AddWithValue("@quantity", cardHero.Quantity);

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
    public async Task<bool> InsertOrUpdateUserCardHeroesBatchAsync(List<CardHeroes> cardHeroes)
    {
        if (cardHeroes == null || cardHeroes.Count == 0)
            return true;

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            int batchSize = 500; // vì nhiều column → giảm size

            for (int i = 0; i < cardHeroes.Count; i += batchSize)
            {
                var batch = cardHeroes.Skip(i).Take(batchSize).ToList();

                var stringBuilder = new System.Text.StringBuilder();
                var parameters = new List<MySqlParameter>();

                stringBuilder.Append(@"
                INSERT INTO user_card_heroes (
                    user_id, card_hero_id, rare, level, experiment, star, quality, block, quantity,
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
                    (@user_id, @card_hero_id_{j}, @rare_{j}, 0, 0, 0, @quality_{j}, 0, @quantity_{j},
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
                        new MySqlParameter($"@card_hero_id_{j}", c.Id),
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
                    quantity = user_card_heroes.quantity + VALUES(quantity);
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
    public async Task<bool> UpdateCardHeroLevelAsync(CardHeroes cardHero, int level)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string updateSQL = @"
            UPDATE user_card_heroes
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
            WHERE user_id = @user_id AND card_hero_id = @card_hero_id;
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);

            updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            updateCommand.Parameters.AddWithValue("@card_hero_id", cardHero.Id);
            updateCommand.Parameters.AddWithValue("@level", level);
            updateCommand.Parameters.AddWithValue("@power", cardHero.Power);
            updateCommand.Parameters.AddWithValue("@health", cardHero.Health);
            updateCommand.Parameters.AddWithValue("@physical_attack", cardHero.PhysicalAttack);
            updateCommand.Parameters.AddWithValue("@physical_defense", cardHero.PhysicalDefense);
            updateCommand.Parameters.AddWithValue("@magical_attack", cardHero.MagicalAttack);
            updateCommand.Parameters.AddWithValue("@magical_defense", cardHero.MagicalDefense);
            updateCommand.Parameters.AddWithValue("@chemical_attack", cardHero.ChemicalAttack);
            updateCommand.Parameters.AddWithValue("@chemical_defense", cardHero.ChemicalDefense);
            updateCommand.Parameters.AddWithValue("@atomic_attack", cardHero.AtomicAttack);
            updateCommand.Parameters.AddWithValue("@atomic_defense", cardHero.AtomicDefense);
            updateCommand.Parameters.AddWithValue("@mental_attack", cardHero.MentalAttack);
            updateCommand.Parameters.AddWithValue("@mental_defense", cardHero.MentalDefense);
            updateCommand.Parameters.AddWithValue("@speed", cardHero.Speed);
            updateCommand.Parameters.AddWithValue("@critical_damage_rate", cardHero.CriticalDamageRate);
            updateCommand.Parameters.AddWithValue("@critical_rate", cardHero.CriticalRate);
            updateCommand.Parameters.AddWithValue("@critical_resistance_rate", cardHero.CriticalResistanceRate);
            updateCommand.Parameters.AddWithValue("@ignore_critical_rate", cardHero.IgnoreCriticalRate);
            updateCommand.Parameters.AddWithValue("@penetration_rate", cardHero.PenetrationRate);
            updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", cardHero.PenetrationResistanceRate);
            updateCommand.Parameters.AddWithValue("@evasion_rate", cardHero.EvasionRate);
            updateCommand.Parameters.AddWithValue("@damage_absorption_rate", cardHero.DamageAbsorptionRate);
            updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardHero.IgnoreDamageAbsorptionRate);
            updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", cardHero.AbsorbedDamageRate);
            updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", cardHero.VitalityRegenerationRate);
            updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardHero.VitalityRegenerationResistanceRate);
            updateCommand.Parameters.AddWithValue("@accuracy_rate", cardHero.AccuracyRate);
            updateCommand.Parameters.AddWithValue("@lifesteal_rate", cardHero.LifestealRate);
            updateCommand.Parameters.AddWithValue("@shield_strength", cardHero.ShieldStrength);
            updateCommand.Parameters.AddWithValue("@tenacity", cardHero.Tenacity);
            updateCommand.Parameters.AddWithValue("@resistance_rate", cardHero.ResistanceRate);
            updateCommand.Parameters.AddWithValue("@combo_rate", cardHero.ComboRate);
            updateCommand.Parameters.AddWithValue("@ignore_combo_rate", cardHero.IgnoreComboRate);
            updateCommand.Parameters.AddWithValue("@combo_damage_rate", cardHero.ComboDamageRate);
            updateCommand.Parameters.AddWithValue("@combo_resistance_rate", cardHero.ComboResistanceRate);
            updateCommand.Parameters.AddWithValue("@stun_rate", cardHero.StunRate);
            updateCommand.Parameters.AddWithValue("@ignore_stun_rate", cardHero.IgnoreStunRate);
            updateCommand.Parameters.AddWithValue("@reflection_rate", cardHero.ReflectionRate);
            updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", cardHero.IgnoreReflectionRate);
            updateCommand.Parameters.AddWithValue("@reflection_damage_rate", cardHero.ReflectionDamageRate);
            updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", cardHero.ReflectionResistanceRate);
            updateCommand.Parameters.AddWithValue("@mana", cardHero.Mana);
            updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", cardHero.ManaRegenerationRate);
            updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", cardHero.DamageToDifferentFactionRate);
            updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardHero.ResistanceToDifferentFactionRate);
            updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", cardHero.DamageToSameFactionRate);
            updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardHero.ResistanceToSameFactionRate);
            updateCommand.Parameters.AddWithValue("@normal_damage_rate", cardHero.NormalDamageRate);
            updateCommand.Parameters.AddWithValue("@normal_resistance_rate", cardHero.NormalResistanceRate);
            updateCommand.Parameters.AddWithValue("@skill_damage_rate", cardHero.SkillDamageRate);
            updateCommand.Parameters.AddWithValue("@skill_resistance_rate", cardHero.SkillResistanceRate);

            await updateCommand.ExecuteNonQueryAsync();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return false;
        }

        return true;
    }
    public async Task<bool> UpdateCardHeroBreakthroughAsync(CardHeroes cardHero, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string updateSQL = @"
            UPDATE user_card_heroes
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
            WHERE user_id = @user_id AND card_hero_id = @card_hero_id;
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);

            updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            updateCommand.Parameters.AddWithValue("@card_hero_id", cardHero.Id);
            updateCommand.Parameters.AddWithValue("@star", star);
            updateCommand.Parameters.AddWithValue("@quantity", quantity);
            updateCommand.Parameters.AddWithValue("@power", cardHero.Power);
            updateCommand.Parameters.AddWithValue("@health", cardHero.Health);
            updateCommand.Parameters.AddWithValue("@physical_attack", cardHero.PhysicalAttack);
            updateCommand.Parameters.AddWithValue("@physical_defense", cardHero.PhysicalDefense);
            updateCommand.Parameters.AddWithValue("@magical_attack", cardHero.MagicalAttack);
            updateCommand.Parameters.AddWithValue("@magical_defense", cardHero.MagicalDefense);
            updateCommand.Parameters.AddWithValue("@chemical_attack", cardHero.ChemicalAttack);
            updateCommand.Parameters.AddWithValue("@chemical_defense", cardHero.ChemicalDefense);
            updateCommand.Parameters.AddWithValue("@atomic_attack", cardHero.AtomicAttack);
            updateCommand.Parameters.AddWithValue("@atomic_defense", cardHero.AtomicDefense);
            updateCommand.Parameters.AddWithValue("@mental_attack", cardHero.MentalAttack);
            updateCommand.Parameters.AddWithValue("@mental_defense", cardHero.MentalDefense);
            updateCommand.Parameters.AddWithValue("@speed", cardHero.Speed);
            updateCommand.Parameters.AddWithValue("@critical_damage_rate", cardHero.CriticalDamageRate);
            updateCommand.Parameters.AddWithValue("@critical_rate", cardHero.CriticalRate);
            updateCommand.Parameters.AddWithValue("@critical_resistance_rate", cardHero.CriticalResistanceRate);
            updateCommand.Parameters.AddWithValue("@ignore_critical_rate", cardHero.IgnoreCriticalRate);
            updateCommand.Parameters.AddWithValue("@penetration_rate", cardHero.PenetrationRate);
            updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", cardHero.PenetrationResistanceRate);
            updateCommand.Parameters.AddWithValue("@evasion_rate", cardHero.EvasionRate);
            updateCommand.Parameters.AddWithValue("@damage_absorption_rate", cardHero.DamageAbsorptionRate);
            updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardHero.IgnoreDamageAbsorptionRate);
            updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", cardHero.AbsorbedDamageRate);
            updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", cardHero.VitalityRegenerationRate);
            updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardHero.VitalityRegenerationResistanceRate);
            updateCommand.Parameters.AddWithValue("@accuracy_rate", cardHero.AccuracyRate);
            updateCommand.Parameters.AddWithValue("@lifesteal_rate", cardHero.LifestealRate);
            updateCommand.Parameters.AddWithValue("@shield_strength", cardHero.ShieldStrength);
            updateCommand.Parameters.AddWithValue("@tenacity", cardHero.Tenacity);
            updateCommand.Parameters.AddWithValue("@resistance_rate", cardHero.ResistanceRate);
            updateCommand.Parameters.AddWithValue("@combo_rate", cardHero.ComboRate);
            updateCommand.Parameters.AddWithValue("@ignore_combo_rate", cardHero.IgnoreComboRate);
            updateCommand.Parameters.AddWithValue("@combo_damage_rate", cardHero.ComboDamageRate);
            updateCommand.Parameters.AddWithValue("@combo_resistance_rate", cardHero.ComboResistanceRate);
            updateCommand.Parameters.AddWithValue("@stun_rate", cardHero.StunRate);
            updateCommand.Parameters.AddWithValue("@ignore_stun_rate", cardHero.IgnoreStunRate);
            updateCommand.Parameters.AddWithValue("@reflection_rate", cardHero.ReflectionRate);
            updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", cardHero.IgnoreReflectionRate);
            updateCommand.Parameters.AddWithValue("@reflection_damage_rate", cardHero.ReflectionDamageRate);
            updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", cardHero.ReflectionResistanceRate);
            updateCommand.Parameters.AddWithValue("@mana", cardHero.Mana);
            updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", cardHero.ManaRegenerationRate);
            updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", cardHero.DamageToDifferentFactionRate);
            updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardHero.ResistanceToDifferentFactionRate);
            updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", cardHero.DamageToSameFactionRate);
            updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardHero.ResistanceToSameFactionRate);
            updateCommand.Parameters.AddWithValue("@normal_damage_rate", cardHero.NormalDamageRate);
            updateCommand.Parameters.AddWithValue("@normal_resistance_rate", cardHero.NormalResistanceRate);
            updateCommand.Parameters.AddWithValue("@skill_damage_rate", cardHero.SkillDamageRate);
            updateCommand.Parameters.AddWithValue("@skill_resistance_rate", cardHero.SkillResistanceRate);

            await updateCommand.ExecuteNonQueryAsync();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return false;
        }

        return true;
    }
    public async Task<CardHeroes> GetUserCardHeroByIdAsync(string user_id, string Id)
    {
        CardHeroes cardHero = new CardHeroes();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT uc.*, c.image
            FROM user_card_heroes uc
            JOIN card_heroes c ON uc.card_hero_id = c.id
            WHERE uc.card_hero_id = @id AND uc.user_id = @user_id";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@id", Id);
            selectCommand.Parameters.AddWithValue("@user_id", user_id);

            await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                cardHero = new CardHeroes
                {
                    Id = reader.GetStringSafe("card_hero_id"),
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

        return cardHero;
    }
    public async Task<List<CardHeroes>> GetAllUserCardHeroesInTeamAsync(string user_id)
    {
        List<CardHeroes> cardHeroes = new List<CardHeroes>();
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
                        FROM card_hero_emblem che
                        JOIN emblems e ON che.emblem_id = e.id
                        WHERE che.card_hero_id = c.id
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
                        FROM card_hero_class chc
                        JOIN classes cl ON chc.class_id = cl.id
                        WHERE chc.card_hero_id = c.id
                    ) AS classes_json
                FROM user_card_heroes uc
                LEFT JOIN card_heroes c ON c.id = uc.card_hero_id 
                LEFT JOIN teams t ON t.team_id = uc.team_id
            WHERE uc.user_id = @user_id AND uc.team_id IS NOT NULL";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@user_id", user_id);

            await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                CardHeroes cardHero = new CardHeroes
                {
                    Id = reader.GetStringSafe("card_hero_id"),
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
                        cardHero.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                        cardHero.Emblems = new List<Emblems>();
                    }
                }

                string classesJson = reader.GetStringSafe("classes_json");

                if (!string.IsNullOrEmpty(classesJson))
                {
                    try
                    {
                        // Chuyển đổi chuỗi JSON thành List<Classes> trong C#
                        cardHero.Classes = JsonHelper.DeserializeClasses(classesJson);
                    }
                    catch
                    {
                        // Phòng trường hợp Hero không có class, MySQL sinh ra chuỗi "[null]"
                        cardHero.Classes = new List<Classes>();
                    }
                }

                cardHeroes.Add(cardHero);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return cardHeroes;
    }
}