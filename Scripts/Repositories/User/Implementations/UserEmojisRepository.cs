using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;

public class UserEmojisRepository : IUserEmojisRepository
{
    public async Task<List<Emojis>> GetUserEmojisAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Emojis> emojis = new List<Emojis>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT ut.*, t.id, t.name, t.image, t.rare, t.description 
                FROM Emojis t
                INNER JOIN user_emojis ut ON t.id = ut.emoji_id
                WHERE ut.user_id = @userId";

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND t.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND t.name LIKE CONCAT('%', @search, '%')";
                }

                selectSQL += @"
                ORDER BY t.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(t.name, '[0-9]+$') AS UNSIGNED), t.name
                LIMIT @limit OFFSET @offset;
            ";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", user_id);
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
                    Emojis emoji = new Emojis
                    {
                        Id = reader.GetStringSafe("id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rare = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        Experiment = reader.GetDoubleSafe("experiment"),
                        Quantity = reader.GetDoubleSafe("quantity"),
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

                    emojis.Add(emoji);
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

        return emojis;
    }
    public async Task<int> GetUserEmojisCountAsync(string user_id, string search, string rare)
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
                FROM Emojis t
                INNER JOIN user_emojis ut ON t.id = ut.emoji_id
                WHERE ut.user_id = @userId";

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND t.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND t.name LIKE CONCAT('%', @search, '%')";
                }

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", user_id);
                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectCommand.Parameters.AddWithValue("@rare", rare);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    selectCommand.Parameters.AddWithValue("@search", search);
                }

                object result = await selectCommand.ExecuteScalarAsync();
                if (result != null)
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
    public async Task<bool> InsertUserEmojiAsync(Emojis emoji, string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkSQL = @"
                SELECT COUNT(*) 
                FROM user_emojis 
                WHERE user_id = @user_id AND emoji_id = @emoji_id;
            ";

                await using var checkCommand = new MySqlCommand(checkSQL, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@emoji_id", emoji.Id);

                int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                if (count == 0)
                {
                    string insertSQL = @"
                    INSERT INTO user_emojis (
                        user_id, emoji_id, rare, level, experiment, star, quality, block, quantity,
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
                        @user_id, @emoji_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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

                    await using var insertCommand = new MySqlCommand(insertSQL, connection);

                    insertCommand.Parameters.AddWithValue("@user_id", userId);
                    insertCommand.Parameters.AddWithValue("@emoji_id", emoji.Id);
                    insertCommand.Parameters.AddWithValue("@rare", emoji.Rare);
                    insertCommand.Parameters.AddWithValue("@level", 0);
                    insertCommand.Parameters.AddWithValue("@experiment", 0);
                    insertCommand.Parameters.AddWithValue("@star", 0);
                    insertCommand.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(emoji.Rare));
                    insertCommand.Parameters.AddWithValue("@block", false);
                    insertCommand.Parameters.AddWithValue("@quantity", emoji.Quantity);
                    insertCommand.Parameters.AddWithValue("@power", emoji.Power);
                    insertCommand.Parameters.AddWithValue("@health", emoji.Health);
                    insertCommand.Parameters.AddWithValue("@physical_attack", emoji.PhysicalAttack);
                    insertCommand.Parameters.AddWithValue("@physical_defense", emoji.PhysicalDefense);
                    insertCommand.Parameters.AddWithValue("@magical_attack", emoji.MagicalAttack);
                    insertCommand.Parameters.AddWithValue("@magical_defense", emoji.MagicalDefense);
                    insertCommand.Parameters.AddWithValue("@chemical_attack", emoji.ChemicalAttack);
                    insertCommand.Parameters.AddWithValue("@chemical_defense", emoji.ChemicalDefense);
                    insertCommand.Parameters.AddWithValue("@atomic_attack", emoji.AtomicAttack);
                    insertCommand.Parameters.AddWithValue("@atomic_defense", emoji.AtomicDefense);
                    insertCommand.Parameters.AddWithValue("@mental_attack", emoji.MentalAttack);
                    insertCommand.Parameters.AddWithValue("@mental_defense", emoji.MentalDefense);
                    insertCommand.Parameters.AddWithValue("@speed", emoji.Speed);
                    insertCommand.Parameters.AddWithValue("@critical_damage_rate", emoji.CriticalDamageRate);
                    insertCommand.Parameters.AddWithValue("@critical_rate", emoji.CriticalRate);
                    insertCommand.Parameters.AddWithValue("@critical_resistance_rate", emoji.CriticalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@ignore_critical_rate", emoji.IgnoreCriticalRate);
                    insertCommand.Parameters.AddWithValue("@penetration_rate", emoji.PenetrationRate);
                    insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", emoji.PenetrationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@evasion_rate", emoji.EvasionRate);
                    insertCommand.Parameters.AddWithValue("@damage_absorption_rate", emoji.DamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", emoji.IgnoreDamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", emoji.AbsorbedDamageRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", emoji.VitalityRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", emoji.VitalityRegenerationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@accuracy_rate", emoji.AccuracyRate);
                    insertCommand.Parameters.AddWithValue("@lifesteal_rate", emoji.LifestealRate);
                    insertCommand.Parameters.AddWithValue("@shield_strength", emoji.ShieldStrength);
                    insertCommand.Parameters.AddWithValue("@tenacity", emoji.Tenacity);
                    insertCommand.Parameters.AddWithValue("@resistance_rate", emoji.ResistanceRate);
                    insertCommand.Parameters.AddWithValue("@combo_rate", emoji.ComboRate);
                    insertCommand.Parameters.AddWithValue("@ignore_combo_rate", emoji.IgnoreComboRate);
                    insertCommand.Parameters.AddWithValue("@combo_damage_rate", emoji.ComboDamageRate);
                    insertCommand.Parameters.AddWithValue("@combo_resistance_rate", emoji.ComboResistanceRate);
                    insertCommand.Parameters.AddWithValue("@stun_rate", emoji.StunRate);
                    insertCommand.Parameters.AddWithValue("@ignore_stun_rate", emoji.IgnoreStunRate);
                    insertCommand.Parameters.AddWithValue("@reflection_rate", emoji.ReflectionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", emoji.IgnoreReflectionRate);
                    insertCommand.Parameters.AddWithValue("@reflection_damage_rate", emoji.ReflectionDamageRate);
                    insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", emoji.ReflectionResistanceRate);
                    insertCommand.Parameters.AddWithValue("@mana", emoji.Mana);
                    insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", emoji.ManaRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", emoji.DamageToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", emoji.ResistanceToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", emoji.DamageToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", emoji.ResistanceToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@normal_damage_rate", emoji.NormalDamageRate);
                    insertCommand.Parameters.AddWithValue("@normal_resistance_rate", emoji.NormalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@skill_damage_rate", emoji.SkillDamageRate);
                    insertCommand.Parameters.AddWithValue("@skill_resistance_rate", emoji.SkillResistanceRate);

                    await insertCommand.ExecuteNonQueryAsync();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateSQL = @"
                    UPDATE user_emojis
                    SET quantity = @quantity
                    WHERE user_id = @user_id AND emoji_id = @emoji_id;
                ";

                    await using var updateCommand = new MySqlCommand(updateSQL, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@emoji_id", emoji.Id);
                    updateCommand.Parameters.AddWithValue("@quantity", emoji.Quantity);

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
    public async Task<bool> InsertOrUpdateUserEmojisBatchAsync(List<Emojis> emojis)
    {
        if (emojis == null || emojis.Count == 0)
            return true;

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            int batchSize = 500; // vì nhiều column → giảm size

            for (int i = 0; i < emojis.Count; i += batchSize)
            {
                var batch = emojis.Skip(i).Take(batchSize).ToList();

                var stringBuilder = new System.Text.StringBuilder();
                var parameters = new List<MySqlParameter>();

                stringBuilder.Append(@"
                INSERT INTO user_emojis (
                    user_id, emoji_id, rare, level, experiment, star, quality, block, quantity,
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
                    (@user_id, @emoji_id_{j}, @rare_{j}, 0, 0, 0, @quality_{j}, 0, @quantity_{j},
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
                        new MySqlParameter($"@emoji_id_{j}", c.Id),
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
                    quantity = user_emojis.quantity + VALUES(quantity);
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
    public async Task<bool> UpdateEmojiLevelAsync(Emojis emoji, int level)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_emojis
                SET 
                    level = @level, power = @power, health = @health, 
                    physical_attack = @physical_attack, physical_defense = @physical_defense, 
                    magical_attack = @magical_attack, magical_defense = @magical_defense, 
                    chemical_attack = @chemical_attack, chemical_defense = @chemical_defense, 
                    atomic_attack = @atomic_attack, atomic_defense = @atomic_defense, 
                    mental_attack = @mental_attack, mental_defense = @mental_defense, 
                    speed = @speed, critical_damage_rate = @critical_damage_rate, 
                    critical_rate = @critical_rate, critical_resistance_rate = @critical_resistance_rate, 
                    ignore_critical_rate = @ignore_critical_rate,
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
                WHERE user_id = @user_id AND emoji_id = @emoji_id;";

                await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
                updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                updateCommand.Parameters.AddWithValue("@emoji_id", emoji.Id);
                updateCommand.Parameters.AddWithValue("@level", level);
                updateCommand.Parameters.AddWithValue("@power", emoji.Power);
                updateCommand.Parameters.AddWithValue("@health", emoji.Health);
                updateCommand.Parameters.AddWithValue("@physical_attack", emoji.PhysicalAttack);
                updateCommand.Parameters.AddWithValue("@physical_defense", emoji.PhysicalDefense);
                updateCommand.Parameters.AddWithValue("@magical_attack", emoji.MagicalAttack);
                updateCommand.Parameters.AddWithValue("@magical_defense", emoji.MagicalDefense);
                updateCommand.Parameters.AddWithValue("@chemical_attack", emoji.ChemicalAttack);
                updateCommand.Parameters.AddWithValue("@chemical_defense", emoji.ChemicalDefense);
                updateCommand.Parameters.AddWithValue("@atomic_attack", emoji.AtomicAttack);
                updateCommand.Parameters.AddWithValue("@atomic_defense", emoji.AtomicDefense);
                updateCommand.Parameters.AddWithValue("@mental_attack", emoji.MentalAttack);
                updateCommand.Parameters.AddWithValue("@mental_defense", emoji.MentalDefense);
                updateCommand.Parameters.AddWithValue("@speed", emoji.Speed);
                updateCommand.Parameters.AddWithValue("@critical_damage_rate", emoji.CriticalDamageRate);
                updateCommand.Parameters.AddWithValue("@critical_rate", emoji.CriticalRate);
                updateCommand.Parameters.AddWithValue("@critical_resistance_rate", emoji.CriticalResistanceRate);
                updateCommand.Parameters.AddWithValue("@ignore_critical_rate", emoji.IgnoreCriticalRate);
                updateCommand.Parameters.AddWithValue("@penetration_rate", emoji.PenetrationRate);
                updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", emoji.PenetrationResistanceRate);
                updateCommand.Parameters.AddWithValue("@evasion_rate", emoji.EvasionRate);
                updateCommand.Parameters.AddWithValue("@damage_absorption_rate", emoji.DamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", emoji.IgnoreDamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", emoji.AbsorbedDamageRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", emoji.VitalityRegenerationRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", emoji.VitalityRegenerationResistanceRate);
                updateCommand.Parameters.AddWithValue("@accuracy_rate", emoji.AccuracyRate);
                updateCommand.Parameters.AddWithValue("@lifesteal_rate", emoji.LifestealRate);
                updateCommand.Parameters.AddWithValue("@shield_strength", emoji.ShieldStrength);
                updateCommand.Parameters.AddWithValue("@tenacity", emoji.Tenacity);
                updateCommand.Parameters.AddWithValue("@resistance_rate", emoji.ResistanceRate);
                updateCommand.Parameters.AddWithValue("@combo_rate", emoji.ComboRate);
                updateCommand.Parameters.AddWithValue("@ignore_combo_rate", emoji.IgnoreComboRate);
                updateCommand.Parameters.AddWithValue("@combo_damage_rate", emoji.ComboDamageRate);
                updateCommand.Parameters.AddWithValue("@combo_resistance_rate", emoji.ComboResistanceRate);
                updateCommand.Parameters.AddWithValue("@stun_rate", emoji.StunRate);
                updateCommand.Parameters.AddWithValue("@ignore_stun_rate", emoji.IgnoreStunRate);
                updateCommand.Parameters.AddWithValue("@reflection_rate", emoji.ReflectionRate);
                updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", emoji.IgnoreReflectionRate);
                updateCommand.Parameters.AddWithValue("@reflection_damage_rate", emoji.ReflectionDamageRate);
                updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", emoji.ReflectionResistanceRate);
                updateCommand.Parameters.AddWithValue("@mana", emoji.Mana);
                updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", emoji.ManaRegenerationRate);
                updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", emoji.DamageToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", emoji.ResistanceToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", emoji.DamageToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", emoji.ResistanceToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@normal_damage_rate", emoji.NormalDamageRate);
                updateCommand.Parameters.AddWithValue("@normal_resistance_rate", emoji.NormalResistanceRate);
                updateCommand.Parameters.AddWithValue("@skill_damage_rate", emoji.SkillDamageRate);
                updateCommand.Parameters.AddWithValue("@skill_resistance_rate", emoji.SkillResistanceRate);

                await updateCommand.ExecuteNonQueryAsync();
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
    public async Task<bool> UpdateEmojiBreakthroughAsync(Emojis emoji, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_emojis
                SET 
                    star = @star, quantity = @quantity, power=@power, health = @health, 
                    physical_attack = @physical_attack, physical_defense = @physical_defense, 
                    magical_attack = @magical_attack, magical_defense = @magical_defense, 
                    chemical_attack = @chemical_attack, chemical_defense = @chemical_defense, 
                    atomic_attack = @atomic_attack, atomic_defense = @atomic_defense, 
                    mental_attack = @mental_attack, mental_defense = @mental_defense, 
                    speed = @speed, critical_damage_rate = @critical_damage_rate, 
                    critical_rate = @critical_rate, critical_resistance_rate = @critical_resistance_rate, 
                    ignore_critical_rate = @ignore_critical_rate,
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
                WHERE user_id = @user_id AND emoji_id = @emoji_id;";

                await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
                updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                updateCommand.Parameters.AddWithValue("@emoji_id", emoji.Id);
                updateCommand.Parameters.AddWithValue("@star", star);
                updateCommand.Parameters.AddWithValue("@quantity", quantity);
                updateCommand.Parameters.AddWithValue("@power", emoji.Power);
                updateCommand.Parameters.AddWithValue("@health", emoji.Health);
                updateCommand.Parameters.AddWithValue("@physical_attack", emoji.PhysicalAttack);
                updateCommand.Parameters.AddWithValue("@physical_defense", emoji.PhysicalDefense);
                updateCommand.Parameters.AddWithValue("@magical_attack", emoji.MagicalAttack);
                updateCommand.Parameters.AddWithValue("@magical_defense", emoji.MagicalDefense);
                updateCommand.Parameters.AddWithValue("@chemical_attack", emoji.ChemicalAttack);
                updateCommand.Parameters.AddWithValue("@chemical_defense", emoji.ChemicalDefense);
                updateCommand.Parameters.AddWithValue("@atomic_attack", emoji.AtomicAttack);
                updateCommand.Parameters.AddWithValue("@atomic_defense", emoji.AtomicDefense);
                updateCommand.Parameters.AddWithValue("@mental_attack", emoji.MentalAttack);
                updateCommand.Parameters.AddWithValue("@mental_defense", emoji.MentalDefense);
                updateCommand.Parameters.AddWithValue("@speed", emoji.Speed);
                updateCommand.Parameters.AddWithValue("@critical_damage_rate", emoji.CriticalDamageRate);
                updateCommand.Parameters.AddWithValue("@critical_rate", emoji.CriticalRate);
                updateCommand.Parameters.AddWithValue("@critical_resistance_rate", emoji.CriticalResistanceRate);
                updateCommand.Parameters.AddWithValue("@ignore_critical_rate", emoji.IgnoreCriticalRate);
                updateCommand.Parameters.AddWithValue("@penetration_rate", emoji.PenetrationRate);
                updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", emoji.PenetrationResistanceRate);
                updateCommand.Parameters.AddWithValue("@evasion_rate", emoji.EvasionRate);
                updateCommand.Parameters.AddWithValue("@damage_absorption_rate", emoji.DamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", emoji.IgnoreDamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", emoji.AbsorbedDamageRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", emoji.VitalityRegenerationRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", emoji.VitalityRegenerationResistanceRate);
                updateCommand.Parameters.AddWithValue("@accuracy_rate", emoji.AccuracyRate);
                updateCommand.Parameters.AddWithValue("@lifesteal_rate", emoji.LifestealRate);
                updateCommand.Parameters.AddWithValue("@shield_strength", emoji.ShieldStrength);
                updateCommand.Parameters.AddWithValue("@tenacity", emoji.Tenacity);
                updateCommand.Parameters.AddWithValue("@resistance_rate", emoji.ResistanceRate);
                updateCommand.Parameters.AddWithValue("@combo_rate", emoji.ComboRate);
                updateCommand.Parameters.AddWithValue("@ignore_combo_rate", emoji.IgnoreComboRate);
                updateCommand.Parameters.AddWithValue("@combo_damage_rate", emoji.ComboDamageRate);
                updateCommand.Parameters.AddWithValue("@combo_resistance_rate", emoji.ComboResistanceRate);
                updateCommand.Parameters.AddWithValue("@stun_rate", emoji.StunRate);
                updateCommand.Parameters.AddWithValue("@ignore_stun_rate", emoji.IgnoreStunRate);
                updateCommand.Parameters.AddWithValue("@reflection_rate", emoji.ReflectionRate);
                updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", emoji.IgnoreReflectionRate);
                updateCommand.Parameters.AddWithValue("@reflection_damage_rate", emoji.ReflectionDamageRate);
                updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", emoji.ReflectionResistanceRate);
                updateCommand.Parameters.AddWithValue("@mana", emoji.Mana);
                updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", emoji.ManaRegenerationRate);
                updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", emoji.DamageToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", emoji.ResistanceToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", emoji.DamageToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", emoji.ResistanceToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@normal_damage_rate", emoji.NormalDamageRate);
                updateCommand.Parameters.AddWithValue("@normal_resistance_rate", emoji.NormalResistanceRate);
                updateCommand.Parameters.AddWithValue("@skill_damage_rate", emoji.SkillDamageRate);
                updateCommand.Parameters.AddWithValue("@skill_resistance_rate", emoji.SkillResistanceRate);

                await updateCommand.ExecuteNonQueryAsync();
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
    public async Task<Emojis> GetUserEmojiByIdAsync(string user_id, string Id)
    {
        Emojis emoji = null;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"SELECT * FROM user_emojis 
                             WHERE emoji_id = @id AND user_id = @user_id";

                await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", Id);
                selectCommand.Parameters.AddWithValue("@user_id", user_id);

                await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    emoji = new Emojis
                    {
                        Id = reader.GetStringSafe("emoji_id"),
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

        return emoji;
    }
    public async Task<Emojis> SumPowerUserEmojisAsync()
    {
        Emojis sumEmojis = new Emojis();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT 
                    SUM(power * (1 + quality / 10.0)) AS total_power,
                    SUM(health * (1 + quality / 10.0)) AS total_health,
                    SUM(mana * (1 + quality / 10.0)) AS total_mana,
                    SUM(physical_attack * (1 + quality / 10.0)) AS total_physical_attack,
                    SUM(physical_defense * (1 + quality / 10.0)) AS total_physical_defense,
                    SUM(magical_attack * (1 + quality / 10.0)) AS total_magical_attack,
                    SUM(magical_defense * (1 + quality / 10.0)) AS total_magical_defense,
                    SUM(chemical_attack * (1 + quality / 10.0)) AS total_chemical_attack,
                    SUM(chemical_defense * (1 + quality / 10.0)) AS total_chemical_defense,
                    SUM(atomic_attack * (1 + quality / 10.0)) AS total_atomic_attack,
                    SUM(atomic_defense * (1 + quality / 10.0)) AS total_atomic_defense,
                    SUM(mental_attack * (1 + quality / 10.0)) AS total_mental_attack,
                    SUM(mental_defense * (1 + quality / 10.0)) AS total_mental_defense,
                    SUM(speed * (1 + quality / 10.0)) AS total_speed,
                    SUM(critical_damage_rate * (1 + quality / 10.0)) AS total_critical_damage_rate,
                    SUM(critical_rate * (1 + quality / 10.0)) AS total_critical_rate,
                    SUM(critical_resistance_rate * (1 + quality / 10.0)) AS total_critical_resistance_rate,
                    SUM(ignore_critical_rate * (1 + quality / 10.0)) AS total_ignore_critical_rate,
                    SUM(penetration_rate * (1 + quality / 10.0)) AS total_penetration_rate,
                    SUM(penetration_resistance_rate * (1 + quality / 10.0)) AS total_penetration_resistance_rate,
                    SUM(evasion_rate * (1 + quality / 10.0)) AS total_evasion_rate,
                    SUM(damage_absorption_rate * (1 + quality / 10.0)) AS total_damage_absorption_rate,
                    SUM(ignore_damage_absorption_rate * (1 + quality / 10.0)) AS total_ignore_damage_absorption_rate,
                    SUM(absorbed_damage_rate * (1 + quality / 10.0)) AS total_absorbed_damage_rate,
                    SUM(vitality_regeneration_rate * (1 + quality / 10.0)) AS total_vitality_regeneration_rate,
                    SUM(vitality_regeneration_resistance_rate * (1 + quality / 10.0)) AS total_vitality_regeneration_resistance_rate,
                    SUM(accuracy_rate * (1 + quality / 10.0)) AS total_accuracy_rate,
                    SUM(lifesteal_rate * (1 + quality / 10.0)) AS total_lifesteal_rate,
                    SUM(shield_strength * (1 + quality / 10.0)) AS total_shield_strength,
                    SUM(tenacity * (1 + quality / 10.0)) AS total_tenacity,
                    SUM(resistance_rate * (1 + quality / 10.0)) AS total_resistance_rate,
                    SUM(combo_rate * (1 + quality / 10.0)) AS total_combo_rate,
                    SUM(ignore_combo_rate * (1 + quality / 10.0)) AS total_ignore_combo_rate,
                    SUM(combo_damage_rate * (1 + quality / 10.0)) AS total_combo_damage_rate,
                    SUM(combo_resistance_rate * (1 + quality / 10.0)) AS total_combo_resistance_rate,
                    SUM(stun_rate * (1 + quality / 10.0)) AS total_stun_rate,
                    SUM(ignore_stun_rate * (1 + quality / 10.0)) AS total_ignore_stun_rate,
                    SUM(reflection_rate * (1 + quality / 10.0)) AS total_reflection_rate,
                    SUM(ignore_reflection_rate * (1 + quality / 10.0)) AS total_ignore_reflection_rate,
                    SUM(reflection_damage_rate * (1 + quality / 10.0)) AS total_reflection_damage_rate,
                    SUM(reflection_resistance_rate * (1 + quality / 10.0)) AS total_reflection_resistance_rate,
                    SUM(mana_regeneration_rate * (1 + quality / 10.0)) AS total_mana_regeneration_rate,
                    SUM(damage_to_different_faction_rate * (1 + quality / 10.0)) AS total_damage_to_different_faction_rate,
                    SUM(resistance_to_different_faction_rate * (1 + quality / 10.0)) AS total_resistance_to_different_faction_rate,
                    SUM(damage_to_same_faction_rate * (1 + quality / 10.0)) AS total_damage_to_same_faction_rate,
                    SUM(resistance_to_same_faction_rate * (1 + quality / 10.0)) AS total_resistance_to_same_faction_rate,
                    SUM(normal_damage_rate * (1 + quality / 10.0)) AS total_normal_damage_rate,
                    SUM(normal_resistance_rate * (1 + quality / 10.0)) AS total_normal_resistance_rate,
                    SUM(skill_damage_rate * (1 + quality / 10.0)) AS total_skill_damage_rate,
                    SUM(skill_resistance_rate * (1 + quality / 10.0)) AS total_skill_resistance_rate
                FROM user_emojis
                WHERE user_id = @user_id;
            ";

                await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    sumEmojis.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                    sumEmojis.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                    sumEmojis.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                    sumEmojis.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                    sumEmojis.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                    sumEmojis.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                    sumEmojis.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                    sumEmojis.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                    sumEmojis.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                    sumEmojis.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                    sumEmojis.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                    sumEmojis.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                    sumEmojis.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                    sumEmojis.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                    sumEmojis.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                    sumEmojis.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                    sumEmojis.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                    sumEmojis.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                    sumEmojis.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                    sumEmojis.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                    sumEmojis.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                    sumEmojis.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                    sumEmojis.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                    sumEmojis.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                    sumEmojis.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                    sumEmojis.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                    sumEmojis.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                    sumEmojis.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                    sumEmojis.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                    sumEmojis.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                    sumEmojis.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                    sumEmojis.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                    sumEmojis.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                    sumEmojis.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                    sumEmojis.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                    sumEmojis.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                    sumEmojis.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                    sumEmojis.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                    sumEmojis.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                    sumEmojis.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                    sumEmojis.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDoubleSafe("total_mana");
                    sumEmojis.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                    sumEmojis.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                    sumEmojis.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                    sumEmojis.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                    sumEmojis.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                    sumEmojis.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                    sumEmojis.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                    sumEmojis.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                    sumEmojis.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
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

        return sumEmojis;
    }
}