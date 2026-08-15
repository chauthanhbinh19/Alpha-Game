using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;
public class UserBordersRepository : IUserBordersRepository
{
    public async Task<List<Borders>> GetUserBordersAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Borders> borders = new List<Borders>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                WITH AggregatedModules AS (
                    SELECT user_border_id, SUM(current_multiplier) AS total_module_mult
                    FROM user_borders_module
                    GROUP BY user_border_id
                ),
                AggregatedUpgrades AS (
                    SELECT user_border_id, SUM(current_multiplier) AS total_upgrade_mult
                    FROM user_borders_upgrade
                    GROUP BY user_border_id
                )
                SELECT 
                    uc.*, 
                    c.id AS base_border_id, 
                    c.name, 
                    c.image, 
                    c.rare, 
                    c.description,
                    COALESCE(am.total_module_mult, 0) AS module_multiplier,
                    COALESCE(au.total_upgrade_mult, 0) AS upgrade_multiplier
                FROM user_borders uc
                INNER JOIN borders c ON uc.border_id = c.id
                LEFT JOIN AggregatedModules am ON uc.border_id = am.user_border_id
                LEFT JOIN AggregatedUpgrades au ON uc.border_id = au.user_border_id
                WHERE uc.user_id = @userId";

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND m.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND m.name LIKE CONCAT('%', @search, '%')";
                }

                selectSQL += @" LIMIT @limit OFFSET @offset";

                await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
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
                    Borders border = new Borders
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rarity = reader.GetString("rare"),
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
                        Description = reader.GetString("description")
                    };

                    UserModules userModule = new UserModules
                    {
                        CurrentMultiplier = reader.GetDoubleSafe("module_multiplier"),
                    };

                    UserUpgrades userUpgrade = new UserUpgrades
                    {
                        CurrentMultiplier = reader.GetDoubleSafe("upgrade_multiplier"),
                    };

                    border.UserModules = userModule;
                    border.UserUpgrades = userUpgrade;

                    borders.Add(border);
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
        return borders;
    }
    public async Task<int> GetUserBordersCountAsync(string userId, string search, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT COUNT(*) 
                FROM Medals m
                JOIN user_medals um ON m.id = um.medal_id
                WHERE um.user_id = @userId";

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND m.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND m.name LIKE CONCAT('%', @search, '%')";
                }

                await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectCommand.Parameters.AddWithValue("@rare", rare);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    selectCommand.Parameters.AddWithValue("@search", search);
                }

                object result = await selectCommand.ExecuteScalarAsync();
                if (result != null && int.TryParse(result.ToString(), out int parsedCount))
                {
                    count = parsedCount;
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
        return count;
    }
    public async Task<bool> InsertUserBorderByIdAsync(Borders border, string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkSQL = @"
            SELECT COUNT(*) FROM user_borders 
            WHERE user_id = @user_id AND border_id = @border_id;";

                await using MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@border_id", border.Id);

                object result = await checkCommand.ExecuteScalarAsync();
                int count = 0;
                if (result != null && int.TryParse(result.ToString(), out int parsedCount))
                {
                    count = parsedCount;
                }

                if (count == 0)
                {
                    string insertSQL = @"
                INSERT INTO user_borders (
                    user_id, border_id, rare, level, experience, star, quality, block, quantity, is_used,
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
                    @user_id, @border_id, @rare, @level, @experience, @star, @quality, @block, @quantity, @is_used,
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
                );";

                    await using MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection);
                    insertCommand.Parameters.AddWithValue("@user_id", userId);
                    insertCommand.Parameters.AddWithValue("@border_id", border.Id);
                    insertCommand.Parameters.AddWithValue("@rare", border.Rarity);
                    insertCommand.Parameters.AddWithValue("@level", 0);
                    insertCommand.Parameters.AddWithValue("@experience", 0);
                    insertCommand.Parameters.AddWithValue("@star", 0);
                    insertCommand.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(border.Rarity));
                    insertCommand.Parameters.AddWithValue("@block", false);
                    insertCommand.Parameters.AddWithValue("@is_used", false);
                    insertCommand.Parameters.AddWithValue("@quantity", 1);
                    insertCommand.Parameters.AddWithValue("@power", border.Power);
                    insertCommand.Parameters.AddWithValue("@health", border.Health);
                    insertCommand.Parameters.AddWithValue("@physical_attack", border.PhysicalAttack);
                    insertCommand.Parameters.AddWithValue("@physical_defense", border.PhysicalDefense);
                    insertCommand.Parameters.AddWithValue("@magical_attack", border.MagicalAttack);
                    insertCommand.Parameters.AddWithValue("@magical_defense", border.MagicalDefense);
                    insertCommand.Parameters.AddWithValue("@chemical_attack", border.ChemicalAttack);
                    insertCommand.Parameters.AddWithValue("@chemical_defense", border.ChemicalDefense);
                    insertCommand.Parameters.AddWithValue("@atomic_attack", border.AtomicAttack);
                    insertCommand.Parameters.AddWithValue("@atomic_defense", border.AtomicDefense);
                    insertCommand.Parameters.AddWithValue("@mental_attack", border.MentalAttack);
                    insertCommand.Parameters.AddWithValue("@mental_defense", border.MentalDefense);
                    insertCommand.Parameters.AddWithValue("@speed", border.Speed);
                    insertCommand.Parameters.AddWithValue("@critical_damage_rate", border.CriticalDamageRate);
                    insertCommand.Parameters.AddWithValue("@critical_rate", border.CriticalRate);
                    insertCommand.Parameters.AddWithValue("@critical_resistance_rate", border.CriticalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@ignore_critical_rate", border.IgnoreCriticalRate);
                    insertCommand.Parameters.AddWithValue("@penetration_rate", border.PenetrationRate);
                    insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", border.PenetrationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@evasion_rate", border.EvasionRate);
                    insertCommand.Parameters.AddWithValue("@damage_absorption_rate", border.DamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", border.IgnoreDamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", border.AbsorbedDamageRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", border.VitalityRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", border.VitalityRegenerationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@accuracy_rate", border.AccuracyRate);
                    insertCommand.Parameters.AddWithValue("@lifesteal_rate", border.LifestealRate);
                    insertCommand.Parameters.AddWithValue("@shield_strength", border.ShieldStrength);
                    insertCommand.Parameters.AddWithValue("@tenacity", border.Tenacity);
                    insertCommand.Parameters.AddWithValue("@resistance_rate", border.ResistanceRate);
                    insertCommand.Parameters.AddWithValue("@combo_rate", border.ComboRate);
                    insertCommand.Parameters.AddWithValue("@ignore_combo_rate", border.IgnoreComboRate);
                    insertCommand.Parameters.AddWithValue("@combo_damage_rate", border.ComboDamageRate);
                    insertCommand.Parameters.AddWithValue("@combo_resistance_rate", border.ComboResistanceRate);
                    insertCommand.Parameters.AddWithValue("@stun_rate", border.StunRate);
                    insertCommand.Parameters.AddWithValue("@ignore_stun_rate", border.IgnoreStunRate);
                    insertCommand.Parameters.AddWithValue("@reflection_rate", border.ReflectionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", border.IgnoreReflectionRate);
                    insertCommand.Parameters.AddWithValue("@reflection_damage_rate", border.ReflectionDamageRate);
                    insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", border.ReflectionResistanceRate);
                    insertCommand.Parameters.AddWithValue("@mana", border.Mana);
                    insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", border.ManaRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", border.DamageToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", border.ResistanceToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", border.DamageToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", border.ResistanceToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@normal_damage_rate", border.NormalDamageRate);
                    insertCommand.Parameters.AddWithValue("@normal_resistance_rate", border.NormalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@skill_damage_rate", border.SkillDamageRate);
                    insertCommand.Parameters.AddWithValue("@skill_resistance_rate", border.SkillResistanceRate);

                    await insertCommand.ExecuteNonQueryAsync();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateSQL = @"
                UPDATE user_borders
                SET quantity = quantity + 1
                WHERE user_id = @user_id AND border_id = @border_id;";

                    await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@border_id", border.Id);

                    await updateCommand.ExecuteNonQueryAsync();
                }

                return true;
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
    }
    public async Task<InsertOrUpdateResult<Borders>> InsertOrUpdateUserBorderAsync(string userId, Borders border)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Query thực hiện Insert hoặc Update nếu đã tồn tại Composite Primary Key (user_id, border_id)
            string upsertSQL = @"
            INSERT INTO user_borders (
                user_id, border_id, rare, level, experience, star, quality, block, quantity,
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
                @user_id, @border_id, @rare, 0, 0, 0, @quality, false, @quantity,
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
            command.Parameters.AddWithValue("@border_id", border.Id);
            command.Parameters.AddWithValue("@rare", border.Rarity);
            command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(border.Rarity));
            command.Parameters.AddWithValue("@quantity", border.Quantity);
            command.Parameters.AddWithValue("@power", border.Power);
            command.Parameters.AddWithValue("@health", border.Health);
            command.Parameters.AddWithValue("@physical_attack", border.PhysicalAttack);
            command.Parameters.AddWithValue("@physical_defense", border.PhysicalDefense);
            command.Parameters.AddWithValue("@magical_attack", border.MagicalAttack);
            command.Parameters.AddWithValue("@magical_defense", border.MagicalDefense);
            command.Parameters.AddWithValue("@chemical_attack", border.ChemicalAttack);
            command.Parameters.AddWithValue("@chemical_defense", border.ChemicalDefense);
            command.Parameters.AddWithValue("@atomic_attack", border.AtomicAttack);
            command.Parameters.AddWithValue("@atomic_defense", border.AtomicDefense);
            command.Parameters.AddWithValue("@mental_attack", border.MentalAttack);
            command.Parameters.AddWithValue("@mental_defense", border.MentalDefense);
            command.Parameters.AddWithValue("@speed", border.Speed);
            command.Parameters.AddWithValue("@critical_damage_rate", border.CriticalDamageRate);
            command.Parameters.AddWithValue("@critical_rate", border.CriticalRate);
            command.Parameters.AddWithValue("@critical_resistance_rate", border.CriticalResistanceRate);
            command.Parameters.AddWithValue("@ignore_critical_rate", border.IgnoreCriticalRate);
            command.Parameters.AddWithValue("@penetration_rate", border.PenetrationRate);
            command.Parameters.AddWithValue("@penetration_resistance_rate", border.PenetrationResistanceRate);
            command.Parameters.AddWithValue("@evasion_rate", border.EvasionRate);
            command.Parameters.AddWithValue("@damage_absorption_rate", border.DamageAbsorptionRate);
            command.Parameters.AddWithValue("@ignore_damage_absorption_rate", border.IgnoreDamageAbsorptionRate);
            command.Parameters.AddWithValue("@absorbed_damage_rate", border.AbsorbedDamageRate);
            command.Parameters.AddWithValue("@vitality_regeneration_rate", border.VitalityRegenerationRate);
            command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", border.VitalityRegenerationResistanceRate);
            command.Parameters.AddWithValue("@accuracy_rate", border.AccuracyRate);
            command.Parameters.AddWithValue("@lifesteal_rate", border.LifestealRate);
            command.Parameters.AddWithValue("@shield_strength", border.ShieldStrength);
            command.Parameters.AddWithValue("@tenacity", border.Tenacity);
            command.Parameters.AddWithValue("@resistance_rate", border.ResistanceRate);
            command.Parameters.AddWithValue("@combo_rate", border.ComboRate);
            command.Parameters.AddWithValue("@ignore_combo_rate", border.IgnoreComboRate);
            command.Parameters.AddWithValue("@combo_damage_rate", border.ComboDamageRate);
            command.Parameters.AddWithValue("@combo_resistance_rate", border.ComboResistanceRate);
            command.Parameters.AddWithValue("@stun_rate", border.StunRate);
            command.Parameters.AddWithValue("@ignore_stun_rate", border.IgnoreStunRate);
            command.Parameters.AddWithValue("@reflection_rate", border.ReflectionRate);
            command.Parameters.AddWithValue("@ignore_reflection_rate", border.IgnoreReflectionRate);
            command.Parameters.AddWithValue("@reflection_damage_rate", border.ReflectionDamageRate);
            command.Parameters.AddWithValue("@reflection_resistance_rate", border.ReflectionResistanceRate);
            command.Parameters.AddWithValue("@mana", border.Mana);
            command.Parameters.AddWithValue("@mana_regeneration_rate", border.ManaRegenerationRate);
            command.Parameters.AddWithValue("@damage_to_different_faction_rate", border.DamageToDifferentFactionRate);
            command.Parameters.AddWithValue("@resistance_to_different_faction_rate", border.ResistanceToDifferentFactionRate);
            command.Parameters.AddWithValue("@damage_to_same_faction_rate", border.DamageToSameFactionRate);
            command.Parameters.AddWithValue("@resistance_to_same_faction_rate", border.ResistanceToSameFactionRate);
            command.Parameters.AddWithValue("@normal_damage_rate", border.NormalDamageRate);
            command.Parameters.AddWithValue("@normal_resistance_rate", border.NormalResistanceRate);
            command.Parameters.AddWithValue("@skill_damage_rate", border.SkillDamageRate);
            command.Parameters.AddWithValue("@skill_resistance_rate", border.SkillResistanceRate);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            // MySQL quy ước: Insert mới = 1, Update = 2, Không thay đổi = 0
            if (rowsAffected == 1)
            {
                return InsertOrUpdateResult<Borders>.Inserted(border);
            }
            else if (rowsAffected == 2 || rowsAffected == 0)
            {
                return InsertOrUpdateResult<Borders>.Updated(border);
            }

            return InsertOrUpdateResult<Borders>.Failure();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Database Error: " + ex.Message);
            return InsertOrUpdateResult<Borders>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<BatchOperationResultDTO<Borders>>> InsertOrUpdateUserBordersBatchAsync(
    string userId, List<Borders> borders)
    {
        if (borders == null || borders.Count == 0)
        {
            return new InsertOrUpdateResult<BatchOperationResultDTO<Borders>>
            {
                Data = new BatchOperationResultDTO<Borders>(),
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // 1. Query lấy TOÀN BỘ border_id hiện có của User (Cực nhanh nhờ Index user_id)
            var existingIds = new HashSet<string>();
            string checkSql = "SELECT border_id FROM user_borders WHERE user_id = @user_id;";

            await using (var checkCmd = new MySqlCommand(checkSql, connection))
            {
                checkCmd.Parameters.AddWithValue("@user_id", userId);
                await using var reader = await checkCmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    existingIds.Add(reader.GetString(0));
                }
            }

            // 2. Phân loại Borders giữ NGUYÊN VẸN OBJECT thuộc tính trong RAM C#
            var batchResult = new BatchOperationResultDTO<Borders>();
            foreach (var card in borders)
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

            int batchSize = 300; // Giảm batchSize vì câu lệnh có nhiều cột

            for (int i = 0; i < borders.Count; i += batchSize)
            {
                var batch = borders.Skip(i).Take(batchSize).ToList();

                var stringBuilder = new System.Text.StringBuilder();
                var parameters = new List<MySqlParameter>();

                stringBuilder.Append(@"
            INSERT INTO user_borders (
                user_id, border_id, rare, level, experience, star, quality, block, quantity,
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
                (@user_id, @border_id_{j}, @rare_{j}, 0, 0, 0, @quality_{j}, 0, @quantity_{j},
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
                    new MySqlParameter($"@border_id_{j}", c.Id),
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
                quantity = COALESCE(user_borders.quantity, 0) + VALUES(quantity);
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

            return new InsertOrUpdateResult<BatchOperationResultDTO<Borders>>
            {
                Data = batchResult,
                OperationType = operationType
            };
        }
        catch (Exception ex)
        {
            Debug.LogError("Batch Error: " + ex.Message);
            return InsertOrUpdateResult<BatchOperationResultDTO<Borders>>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<bool>> UpdateUserBorderLevelAsync(string userId, Borders border)
    {
        if (border == null)
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
            UPDATE user_borders
            SET 
                level = @level, 
                experience = @experience
            WHERE user_id = @user_id 
              AND border_id = @border_id
              AND (level != @level OR experience != @experience);
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);

            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@border_id", border.Id);
            updateCommand.Parameters.AddWithValue("@level", border.Level);
            updateCommand.Parameters.AddWithValue("@experience", border.Experience);

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
            Debug.LogError("Error UpdateUserBorderLevel: " + ex.Message);
            return InsertOrUpdateResult<bool>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<bool>> UpdateUserBorderStarAsync(string userId, Borders border)
    {
        if (border == null)
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
            UPDATE user_borders
            SET 
                star = @star, 
                quantity = @quantity
            WHERE user_id = @user_id 
              AND border_id = @border_id
              AND (star != @star OR quantity != @quantity);
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);

            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@border_id", border.Id);
            updateCommand.Parameters.AddWithValue("@star", border.Star);
            updateCommand.Parameters.AddWithValue("@quantity", border.Quantity);

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
            Debug.LogError("Error UpdateUserBorderStar: " + ex.Message);
            return InsertOrUpdateResult<bool>.Failure(ex.Message);
        }
    }
    public async Task UpdateIsUsedUserBorderAsync(string borderId, string userId, bool is_used)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = "UPDATE user_borders SET is_used=@is_used WHERE user_id=@user_id AND border_id=@border_id";

                await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
                updateCommand.Parameters.AddWithValue("@user_id", userId);
                updateCommand.Parameters.AddWithValue("@border_id", borderId);
                updateCommand.Parameters.AddWithValue("@is_used", is_used);

                await updateCommand.ExecuteNonQueryAsync();
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
    }
    public async Task<Borders> GetUserBorderByUsedAsync(string userId)
    {
        Borders border = new Borders();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT ub.*, b.image, b.rare 
                FROM user_borders ub
                JOIN borders b ON ub.border_id = b.id
                WHERE ub.is_used = TRUE AND ub.user_id = @user_id";

                await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@user_id", userId);

                await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    border = new Borders
                    {
                        Id = reader.GetString("border_id"),
                        Image = reader.GetString("image"),
                        Rarity = reader.GetString("rare"),
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
                    };
                    // UserModules userModule = new UserModules
                    // {
                    //     CurrentMultiplier = reader.GetDoubleSafe("module_multiplier"),
                    // };

                    // UserUpgrades userUpgrade = new UserUpgrades
                    // {
                    //     CurrentMultiplier = reader.GetDoubleSafe("upgrade_multiplier"),
                    // };

                    // border.UserModules = userModule;
                    // border.UserUpgrades = userUpgrade;
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
        return border;
    }
    public async Task<Borders> GetUserBorderByIdAsync(string userId, string Id)
    {
        Borders border = null;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                WITH AggregatedModules AS (
                    SELECT user_border_id, SUM(current_multiplier) AS total_module_mult
                    FROM user_borders_module
                    GROUP BY user_border_id
                ),
                AggregatedUpgrades AS (
                    SELECT user_border_id, SUM(current_multiplier) AS total_upgrade_mult
                    FROM user_borders_upgrade
                    GROUP BY user_border_id
                )
                SELECT uc.* ,
                    COALESCE(am.total_module_mult, 0) AS module_multiplier,
                    COALESCE(au.total_upgrade_mult, 0) AS upgrade_multiplier
                FROM user_borders uc
                LEFT JOIN AggregatedModules am ON uc.border_id = am.user_border_id
                LEFT JOIN AggregatedUpgrades au ON uc.border_id = au.user_border_id
                WHERE uc.border_id = @id AND uc.user_id = @user_id";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@id", Id);
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    await using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            border = new Borders
                            {
                                Id = reader.GetStringSafe("border_id"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate")
                            };
                            UserModules userModule = new UserModules
                            {
                                CurrentMultiplier = reader.GetDoubleSafe("module_multiplier"),
                            };

                            UserUpgrades userUpgrade = new UserUpgrades
                            {
                                CurrentMultiplier = reader.GetDoubleSafe("upgrade_multiplier"),
                            };

                            border.UserModules = userModule;
                            border.UserUpgrades = userUpgrade;
                        }
                    }
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

        return border;
    }
    public async Task<Borders> SumPowerUserBordersAsync(string userId)
    {
        Borders sumBorders = new Borders();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
            WITH CalculatedObjects AS (
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
                    FROM user_borders uc
                    LEFT JOIN user_borders_module ubm ON uc.border_id = ubm.user_border_id
                    LEFT JOIN user_borders_upgrade ubu ON uc.border_id = ubu.user_border_id
                    WHERE uc.user_id = @user_id
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
                FROM CalculatedObjects;";

                await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@user_id", userId);

                await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    sumBorders.Health = reader.GetDoubleSafe("health");
                    sumBorders.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                    sumBorders.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                    sumBorders.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                    sumBorders.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                    sumBorders.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                    sumBorders.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                    sumBorders.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                    sumBorders.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                    sumBorders.MentalAttack = reader.GetDoubleSafe("mental_attack");
                    sumBorders.MentalDefense = reader.GetDoubleSafe("mental_defense");
                    sumBorders.Speed = reader.GetDoubleSafe("speed");
                    sumBorders.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                    sumBorders.CriticalRate = reader.GetDoubleSafe("critical_rate");
                    sumBorders.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                    sumBorders.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                    sumBorders.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                    sumBorders.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                    sumBorders.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                    sumBorders.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                    sumBorders.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                    sumBorders.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                    sumBorders.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                    sumBorders.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                    sumBorders.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                    sumBorders.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                    sumBorders.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                    sumBorders.Tenacity = reader.GetDoubleSafe("tenacity");
                    sumBorders.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                    sumBorders.ComboRate = reader.GetDoubleSafe("combo_rate");
                    sumBorders.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                    sumBorders.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                    sumBorders.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                    sumBorders.StunRate = reader.GetDoubleSafe("stun_rate");
                    sumBorders.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                    sumBorders.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                    sumBorders.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                    sumBorders.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                    sumBorders.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                    sumBorders.Mana = reader.GetDoubleSafe("mana");
                    sumBorders.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                    sumBorders.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                    sumBorders.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                    sumBorders.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                    sumBorders.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                    sumBorders.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                    sumBorders.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                    sumBorders.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                    sumBorders.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
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
        return sumBorders;
    }
}