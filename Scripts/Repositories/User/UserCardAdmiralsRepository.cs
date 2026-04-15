using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserCardAdmiralsRepository : IUserCardAdmiralsRepository
{
    public async Task<List<CardAdmirals>> GetUserCardAdmiralsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardAdmirals> cardAdmirals = new List<CardAdmirals>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT uc.*, c.name, c.image, c.type, c.description, COALESCE(t.team_number, 0) AS team_number,
                    JSON_ARRAYAGG(
                       JSON_OBJECT(
                           'id', e.id,
                           'name', e.name,
                           'image', e.image,
                           'type', e.type
                       )
                   ) AS emblems_json
                FROM user_card_admirals uc
                LEFT JOIN card_admirals c ON c.id = uc.card_admiral_id 
                LEFT JOIN teams t on t.team_id = uc.team_id
                LEFT JOIN card_admiral_emblem che ON c.id = che.card_admiral_id
                LEFT JOIN emblems e ON che.emblem_id = e.id
                WHERE uc.user_id = @userId 
            ";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    query += " AND c.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    query += " AND c.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND c.name LIKE CONCAT('%', @search, '%')";
                }

                query += " GROUP BY uc.card_admiral_id, c.id, t.team_number";
                query += " ORDER BY c.name";
                query += " LIMIT @limit OFFSET @offset";

                await using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    command.Parameters.AddWithValue("@type", type);
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    command.Parameters.AddWithValue("@rare", rare);
                }

                if (!string.IsNullOrEmpty(search))
                {
                    command.Parameters.AddWithValue("@search", search);
                }
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);

                await using MySqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    CardAdmirals cardAdmiral = new CardAdmirals
                    {
                        Id = reader.GetStringSafe("card_admiral_id"),
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
                            cardAdmiral.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                        }
                        catch
                        {
                            // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                            cardAdmiral.Emblems = new List<Emblems>();
                        }
                    }

                    cardAdmirals.Add(cardAdmiral);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        return cardAdmirals;
    }
    public async Task<List<CardAdmirals>> GetUserCardAdmiralsTeamAsync(string user_id, string teamId, string position)
    {
        List<CardAdmirals> cardAdmirals = new List<CardAdmirals>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT uc.*, c.name, c.image, c.type, c.description,
                    JSON_ARRAYAGG(
                       JSON_OBJECT(
                           'id', e.id,
                           'name', e.name,
                           'image', e.image,
                           'type', e.type
                       )
                   ) AS emblems_json
                FROM user_card_admirals uc
                LEFT JOIN card_admirals c ON c.id = uc.card_admiral_id 
                LEFT JOIN card_admiral_emblem che ON c.id = che.card_admiral_id
                LEFT JOIN emblems e ON che.emblem_id = e.id
                WHERE uc.user_id = @userId AND uc.team_id = @team_id AND SUBSTRING_INDEX(uc.position, '-', 1) = @position
                GROUP BY uc.card_admiral_id, c.id
                ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name;
            ";

                await using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@team_id", teamId);
                command.Parameters.AddWithValue("@position", position);

                await using MySqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    CardAdmirals cardAdmiral = new CardAdmirals
                    {
                        Id = reader.GetStringSafe("card_admiral_id"),
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
                            cardAdmiral.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                        }
                        catch
                        {
                            // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                            cardAdmiral.Emblems = new List<Emblems>();
                        }
                    }

                    cardAdmirals.Add(cardAdmiral);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        return cardAdmirals;
    }
    public async Task<List<CardAdmirals>> GetUserCardAdmiralsTeamWithoutPositionAsync(string user_id, string teamId)
    {
        List<CardAdmirals> cardAdmirals = new List<CardAdmirals>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT uc.*, c.name, c.image, c.type, c.description,
                    JSON_ARRAYAGG(
                       JSON_OBJECT(
                           'id', e.id,
                           'name', e.name,
                           'image', e.image,
                           'type', e.type
                       )
                   ) AS emblems_json
                FROM user_card_admirals uc
                LEFT JOIN card_admirals c ON c.id = uc.card_admiral_id 
                LEFT JOIN card_admiral_emblem che ON c.id = che.card_admiral_id
                LEFT JOIN emblems e ON che.emblem_id = e.id
                WHERE uc.user_id = @userId AND uc.team_id = @team_id
                GROUP BY uc.card_admiral_id, c.id
                ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name;
            ";

                await using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@team_id", teamId);

                await using MySqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    CardAdmirals cardAdmiral = new CardAdmirals
                    {
                        Id = reader.GetStringSafe("card_admiral_id"),
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
                            cardAdmiral.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                        }
                        catch
                        {
                            // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                            cardAdmiral.Emblems = new List<Emblems>();
                        }
                    }

                    cardAdmirals.Add(cardAdmiral);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        return cardAdmirals;
    }
    public async Task<Dictionary<string, int>> GetUniqueCardAdmiralsTypesTeamAsync(string teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT c.type, COUNT(c.type) AS number
                FROM user_card_admirals uc
                LEFT JOIN card_admirals c ON uc.card_admiral_id = c.id 
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
            finally
            {
                await connection.CloseAsync();
            }
        }
        return result;
    }
    public async Task<bool> UpdateTeamCardAdmiralAsync(string team_id, string position, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
            UPDATE user_card_admirals 
            SET team_id = @team_id, position = @position 
            WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id;
        ";

                await using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_admiral_id", card_id);

                await command.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        return true;
    }
    public async Task<int> GetUserCardAdmiralsCountAsync(string user_id, string search, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT COUNT(*) 
                FROM card_admirals c
                JOIN user_card_admirals uc ON c.id = uc.card_admiral_id
                WHERE uc.user_id = @userId 
            ";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    query += " AND c.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    query += " AND c.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND c.name LIKE CONCAT('%', @search, '%')";
                }

                await using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    command.Parameters.AddWithValue("@type", type);
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    command.Parameters.AddWithValue("@rare", rare);
                }

                if (!string.IsNullOrEmpty(search))
                {
                    command.Parameters.AddWithValue("@search", search);
                }

                object result = await command.ExecuteScalarAsync();
                count = Convert.ToInt32(result);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        return count;
    }
    public async Task<int> GetUserCardAdmiralsTeamsPositionCountAsync(string user_id, string team_id, string position)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
            SELECT COUNT(*) 
            FROM user_card_admirals
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
            finally
            {
                await connection.CloseAsync();
            }
        }
        return count;
    }
    public async Task<int> GetUserCardAdmiralsTeamsCountAsync(string user_id, string team_id)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
            SELECT COUNT(*) 
            FROM user_card_admirals
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
            finally
            {
                await connection.CloseAsync();
            }
        }
        return count;
    }
    public async Task<bool> InsertUserCardAdmiralAsync(CardAdmirals cardAdmiral)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
            SELECT COUNT(*) 
            FROM user_card_admirals
            WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id;
        ";

                await using MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_admiral_id", cardAdmiral.Id);

                int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                if (count == 0)
                {
                    string insertQuery = @"
                INSERT INTO user_card_admirals (
                    user_id, card_admiral_id, rare, level, experiment, star, quality, block, quantity,
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
                    @user_id, @card_admiral_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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
                    insertCommand.Parameters.AddWithValue("@card_admiral_id", cardAdmiral.Id);
                    insertCommand.Parameters.AddWithValue("@rare", cardAdmiral.Rare);
                    insertCommand.Parameters.AddWithValue("@level", 0);
                    insertCommand.Parameters.AddWithValue("@experiment", 0);
                    insertCommand.Parameters.AddWithValue("@star", 0);
                    insertCommand.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(cardAdmiral.Rare));
                    insertCommand.Parameters.AddWithValue("@block", false);
                    insertCommand.Parameters.AddWithValue("@quantity", cardAdmiral.Quantity);
                    insertCommand.Parameters.AddWithValue("@power", cardAdmiral.Power);
                    insertCommand.Parameters.AddWithValue("@health", cardAdmiral.Health);
                    insertCommand.Parameters.AddWithValue("@physical_attack", cardAdmiral.PhysicalAttack);
                    insertCommand.Parameters.AddWithValue("@physical_defense", cardAdmiral.PhysicalDefense);
                    insertCommand.Parameters.AddWithValue("@magical_attack", cardAdmiral.MagicalAttack);
                    insertCommand.Parameters.AddWithValue("@magical_defense", cardAdmiral.MagicalDefense);
                    insertCommand.Parameters.AddWithValue("@chemical_attack", cardAdmiral.ChemicalAttack);
                    insertCommand.Parameters.AddWithValue("@chemical_defense", cardAdmiral.ChemicalDefense);
                    insertCommand.Parameters.AddWithValue("@atomic_attack", cardAdmiral.AtomicAttack);
                    insertCommand.Parameters.AddWithValue("@atomic_defense", cardAdmiral.AtomicDefense);
                    insertCommand.Parameters.AddWithValue("@mental_attack", cardAdmiral.MentalAttack);
                    insertCommand.Parameters.AddWithValue("@mental_defense", cardAdmiral.MentalDefense);
                    insertCommand.Parameters.AddWithValue("@speed", cardAdmiral.Speed);
                    insertCommand.Parameters.AddWithValue("@critical_damage_rate", cardAdmiral.CriticalDamageRate);
                    insertCommand.Parameters.AddWithValue("@critical_rate", cardAdmiral.CriticalRate);
                    insertCommand.Parameters.AddWithValue("@critical_resistance_rate", cardAdmiral.CriticalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@ignore_critical_rate", cardAdmiral.IgnoreCriticalRate);
                    insertCommand.Parameters.AddWithValue("@penetration_rate", cardAdmiral.PenetrationRate);
                    insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", cardAdmiral.PenetrationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@evasion_rate", cardAdmiral.EvasionRate);
                    insertCommand.Parameters.AddWithValue("@damage_absorption_rate", cardAdmiral.DamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardAdmiral.IgnoreDamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", cardAdmiral.AbsorbedDamageRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", cardAdmiral.VitalityRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardAdmiral.VitalityRegenerationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@accuracy_rate", cardAdmiral.AccuracyRate);
                    insertCommand.Parameters.AddWithValue("@lifesteal_rate", cardAdmiral.LifestealRate);
                    insertCommand.Parameters.AddWithValue("@shield_strength", cardAdmiral.ShieldStrength);
                    insertCommand.Parameters.AddWithValue("@tenacity", cardAdmiral.Tenacity);
                    insertCommand.Parameters.AddWithValue("@resistance_rate", cardAdmiral.ResistanceRate);
                    insertCommand.Parameters.AddWithValue("@combo_rate", cardAdmiral.ComboRate);
                    insertCommand.Parameters.AddWithValue("@ignore_combo_rate", cardAdmiral.IgnoreComboRate);
                    insertCommand.Parameters.AddWithValue("@combo_damage_rate", cardAdmiral.ComboDamageRate);
                    insertCommand.Parameters.AddWithValue("@combo_resistance_rate", cardAdmiral.ComboResistanceRate);
                    insertCommand.Parameters.AddWithValue("@stun_rate", cardAdmiral.StunRate);
                    insertCommand.Parameters.AddWithValue("@ignore_stun_rate", cardAdmiral.IgnoreStunRate);
                    insertCommand.Parameters.AddWithValue("@reflection_rate", cardAdmiral.ReflectionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", cardAdmiral.IgnoreReflectionRate);
                    insertCommand.Parameters.AddWithValue("@reflection_damage_rate", cardAdmiral.ReflectionDamageRate);
                    insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", cardAdmiral.ReflectionResistanceRate);
                    insertCommand.Parameters.AddWithValue("@mana", cardAdmiral.Mana);
                    insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", cardAdmiral.ManaRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", cardAdmiral.DamageToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardAdmiral.ResistanceToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", cardAdmiral.DamageToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardAdmiral.ResistanceToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@normal_damage_rate", cardAdmiral.NormalDamageRate);
                    insertCommand.Parameters.AddWithValue("@normal_resistance_rate", cardAdmiral.NormalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@skill_damage_rate", cardAdmiral.SkillDamageRate);
                    insertCommand.Parameters.AddWithValue("@skill_resistance_rate", cardAdmiral.SkillResistanceRate);

                    await insertCommand.ExecuteNonQueryAsync();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                UPDATE user_card_admirals
                SET quantity = @quantity
                WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id;
            ";

                    await using MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@card_admiral_id", cardAdmiral.Id);
                    updateCommand.Parameters.AddWithValue("@quantity", cardAdmiral.Quantity);

                    await updateCommand.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        return true;
    }
    public async Task<bool> UpdateCardAdmiralLevelAsync(CardAdmirals cardAdmiral, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
            UPDATE user_card_admirals
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
            WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id;
        ";

                await using MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_admiral_id", cardAdmiral.Id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", cardAdmiral.Power);
                command.Parameters.AddWithValue("@health", cardAdmiral.Health);
                command.Parameters.AddWithValue("@physical_attack", cardAdmiral.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", cardAdmiral.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", cardAdmiral.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", cardAdmiral.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", cardAdmiral.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", cardAdmiral.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", cardAdmiral.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", cardAdmiral.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", cardAdmiral.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", cardAdmiral.MentalDefense);
                command.Parameters.AddWithValue("@speed", cardAdmiral.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", cardAdmiral.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", cardAdmiral.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", cardAdmiral.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", cardAdmiral.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", cardAdmiral.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", cardAdmiral.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", cardAdmiral.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardAdmiral.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardAdmiral.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", cardAdmiral.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardAdmiral.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardAdmiral.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", cardAdmiral.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardAdmiral.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", cardAdmiral.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", cardAdmiral.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardAdmiral.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", cardAdmiral.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", cardAdmiral.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", cardAdmiral.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", cardAdmiral.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", cardAdmiral.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", cardAdmiral.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", cardAdmiral.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", cardAdmiral.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", cardAdmiral.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", cardAdmiral.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", cardAdmiral.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardAdmiral.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardAdmiral.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardAdmiral.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardAdmiral.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardAdmiral.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", cardAdmiral.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", cardAdmiral.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", cardAdmiral.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", cardAdmiral.SkillResistanceRate);

                await command.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        return true;
    }
    public async Task<bool> UpdateCardAdmiralBreakthroughAsync(CardAdmirals cardAdmiral, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
            UPDATE user_card_admirals
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
            WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id;
        ";

                await using MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_admiral_id", cardAdmiral.Id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", cardAdmiral.Power);
                command.Parameters.AddWithValue("@health", cardAdmiral.Health);
                command.Parameters.AddWithValue("@physical_attack", cardAdmiral.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", cardAdmiral.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", cardAdmiral.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", cardAdmiral.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", cardAdmiral.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", cardAdmiral.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", cardAdmiral.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", cardAdmiral.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", cardAdmiral.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", cardAdmiral.MentalDefense);
                command.Parameters.AddWithValue("@speed", cardAdmiral.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", cardAdmiral.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", cardAdmiral.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", cardAdmiral.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", cardAdmiral.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", cardAdmiral.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", cardAdmiral.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", cardAdmiral.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardAdmiral.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardAdmiral.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", cardAdmiral.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardAdmiral.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardAdmiral.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", cardAdmiral.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardAdmiral.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", cardAdmiral.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", cardAdmiral.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardAdmiral.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", cardAdmiral.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", cardAdmiral.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", cardAdmiral.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", cardAdmiral.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", cardAdmiral.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", cardAdmiral.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", cardAdmiral.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", cardAdmiral.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", cardAdmiral.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", cardAdmiral.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", cardAdmiral.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardAdmiral.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardAdmiral.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardAdmiral.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardAdmiral.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardAdmiral.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", cardAdmiral.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", cardAdmiral.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", cardAdmiral.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", cardAdmiral.SkillResistanceRate);

                await command.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        return true;
    }
    public async Task<CardAdmirals> GetUserCardAdmiralByIdAsync(string user_id, string Id)
    {
        CardAdmirals card = new CardAdmirals();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
            SELECT uc.*, c.image
            FROM user_card_admirals uc
            JOIN card_admirals c ON uc.card_admiral_id = c.id
            WHERE uc.card_admiral_id = @id AND uc.user_id = @user_id";

                await using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);

                await using MySqlDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    card = new CardAdmirals
                    {
                        Id = reader.GetStringSafe("card_admiral_id"),
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
            finally
            {
                await connection.CloseAsync();
            }
        }
        return card;
    }
    public async Task<List<CardAdmirals>> GetAllUserCardAdmiralsInTeamAsync(string user_id)
    {
        List<CardAdmirals> cardAdmirals = new List<CardAdmirals>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string userQuery = @"
                SELECT uc.*, c.name, c.image, c.type, c.description,
                    JSON_ARRAYAGG(
                       JSON_OBJECT(
                           'id', e.id,
                           'name', e.name,
                           'image', e.image,
                           'type', e.type
                       )
                   ) AS emblems_json
                FROM user_card_admirals uc
                LEFT JOIN card_admirals c ON uc.card_admiral_id = c.id 
                LEFT JOIN card_admiral_emblem che ON c.id = che.card_admiral_id
                LEFT JOIN emblems e ON che.emblem_id = e.id
                WHERE uc.user_id = @user_id AND uc.team_id IS NOT NULL
                GROUP BY uc.card_admiral_id, c.id";

                await using MySqlCommand command = new MySqlCommand(userQuery, connection);
                command.Parameters.AddWithValue("@user_id", user_id);

                await using MySqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    CardAdmirals cardAdmiral = new CardAdmirals
                    {
                        Id = reader.GetStringSafe("card_admiral_id"),
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
                            cardAdmiral.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                        }
                        catch
                        {
                            // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                            cardAdmiral.Emblems = new List<Emblems>();
                        }
                    }

                    cardAdmirals.Add(cardAdmiral);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        return cardAdmirals;
    }
}