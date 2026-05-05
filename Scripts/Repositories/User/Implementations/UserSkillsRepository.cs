using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;

public class UserSkillsRepository : IUserSkillsRepository
{
    public async Task<List<Skills>> GetUserSkillsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description 
                FROM skills s
                INNER JOIN user_skills us ON s.id = us.skill_id
                WHERE us.user_id = @userId";

                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND s.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND s.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND s.name LIKE CONCAT('%', @search, '%')";
                }

                selectSQL += @"
                ORDER BY s.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name
                LIMIT @limit OFFSET @offset";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
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

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rare = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        SkillType = reader.GetStringSafe("skill_type"),
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

                    skills.Add(skill);
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

        return skills;
    }
    public async Task<int> GetUserSkillsCountAsync(string userId, string search, string type, string rare)
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
                FROM skills s
                INNER JOIN user_skills us ON s.id = us.skill_id
                WHERE us.user_id = @userId ";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND s.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND s.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND s.name LIKE CONCAT('%', @search, '%')";
                }

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
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

                var result = await selectCommand.ExecuteScalarAsync();
                count = Convert.ToInt32(result);

                return count;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return 0;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task<bool> InsertUserSkillAsync(Skills skill)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkSQL = @"
                SELECT COUNT(*) FROM user_skills 
                WHERE user_id = @user_id AND skill_id = @skill_id;";

                await using var checkCommand = new MySqlCommand(checkSQL, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@skill_id", skill.Id);

                var countObj = await checkCommand.ExecuteScalarAsync();
                int count = Convert.ToInt32(countObj);

                if (count == 0)
                {
                    string insertSQL = @"
                INSERT INTO user_skills (
                    user_id, skill_id, rare, level, experiment, star, quality, block, quantity,
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
                    @user_id, @skill_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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

                    insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    insertCommand.Parameters.AddWithValue("@skill_id", skill.Id);
                    insertCommand.Parameters.AddWithValue("@rare", skill.Rare);
                    insertCommand.Parameters.AddWithValue("@level", 0);
                    insertCommand.Parameters.AddWithValue("@experiment", 0);
                    insertCommand.Parameters.AddWithValue("@star", 0);
                    insertCommand.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(skill.Rare));
                    insertCommand.Parameters.AddWithValue("@block", false);
                    insertCommand.Parameters.AddWithValue("@quantity", skill.Quantity);
                    insertCommand.Parameters.AddWithValue("@power", skill.Power);
                    insertCommand.Parameters.AddWithValue("@health", skill.Health);
                    insertCommand.Parameters.AddWithValue("@physical_attack", skill.PhysicalAttack);
                    insertCommand.Parameters.AddWithValue("@physical_defense", skill.PhysicalDefense);
                    insertCommand.Parameters.AddWithValue("@magical_attack", skill.MagicalAttack);
                    insertCommand.Parameters.AddWithValue("@magical_defense", skill.MagicalDefense);
                    insertCommand.Parameters.AddWithValue("@chemical_attack", skill.ChemicalAttack);
                    insertCommand.Parameters.AddWithValue("@chemical_defense", skill.ChemicalDefense);
                    insertCommand.Parameters.AddWithValue("@atomic_attack", skill.AtomicAttack);
                    insertCommand.Parameters.AddWithValue("@atomic_defense", skill.AtomicDefense);
                    insertCommand.Parameters.AddWithValue("@mental_attack", skill.MentalAttack);
                    insertCommand.Parameters.AddWithValue("@mental_defense", skill.MentalDefense);
                    insertCommand.Parameters.AddWithValue("@speed", skill.Speed);
                    insertCommand.Parameters.AddWithValue("@critical_damage_rate", skill.CriticalDamageRate);
                    insertCommand.Parameters.AddWithValue("@critical_rate", skill.CriticalRate);
                    insertCommand.Parameters.AddWithValue("@critical_resistance_rate", skill.CriticalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@ignore_critical_rate", skill.IgnoreCriticalRate);
                    insertCommand.Parameters.AddWithValue("@penetration_rate", skill.PenetrationRate);
                    insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", skill.PenetrationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@evasion_rate", skill.EvasionRate);
                    insertCommand.Parameters.AddWithValue("@damage_absorption_rate", skill.DamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", skill.IgnoreDamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", skill.AbsorbedDamageRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", skill.VitalityRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", skill.VitalityRegenerationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@accuracy_rate", skill.AccuracyRate);
                    insertCommand.Parameters.AddWithValue("@lifesteal_rate", skill.LifestealRate);
                    insertCommand.Parameters.AddWithValue("@shield_strength", skill.ShieldStrength);
                    insertCommand.Parameters.AddWithValue("@tenacity", skill.Tenacity);
                    insertCommand.Parameters.AddWithValue("@resistance_rate", skill.ResistanceRate);
                    insertCommand.Parameters.AddWithValue("@combo_rate", skill.ComboRate);
                    insertCommand.Parameters.AddWithValue("@ignore_combo_rate", skill.IgnoreComboRate);
                    insertCommand.Parameters.AddWithValue("@combo_damage_rate", skill.ComboDamageRate);
                    insertCommand.Parameters.AddWithValue("@combo_resistance_rate", skill.ComboResistanceRate);
                    insertCommand.Parameters.AddWithValue("@stun_rate", skill.StunRate);
                    insertCommand.Parameters.AddWithValue("@ignore_stun_rate", skill.IgnoreStunRate);
                    insertCommand.Parameters.AddWithValue("@reflection_rate", skill.ReflectionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", skill.IgnoreReflectionRate);
                    insertCommand.Parameters.AddWithValue("@reflection_damage_rate", skill.ReflectionDamageRate);
                    insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", skill.ReflectionResistanceRate);
                    insertCommand.Parameters.AddWithValue("@mana", skill.Mana);
                    insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", skill.ManaRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", skill.DamageToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", skill.ResistanceToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", skill.DamageToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", skill.ResistanceToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@normal_damage_rate", skill.NormalDamageRate);
                    insertCommand.Parameters.AddWithValue("@normal_resistance_rate", skill.NormalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@skill_damage_rate", skill.SkillDamageRate);
                    insertCommand.Parameters.AddWithValue("@skill_resistance_rate", skill.SkillResistanceRate);

                    await insertCommand.ExecuteNonQueryAsync();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateSQL = @"
                    UPDATE user_skills
                    SET quantity = @quantity
                    WHERE user_id = @user_id AND skill_id = @skill_id;";

                    await using var updateCommand = new MySqlCommand(updateSQL, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@skill_id", skill.Id);
                    updateCommand.Parameters.AddWithValue("@quantity", skill.Quantity);

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
    public async Task<bool> InsertOrUpdateUserSkillsBatchAsync(List<Skills> skills)
    {
        if (skills == null || skills.Count == 0)
            return true;

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            int batchSize = 500; // vì nhiều column → giảm size

            for (int i = 0; i < skills.Count; i += batchSize)
            {
                var batch = skills.Skip(i).Take(batchSize).ToList();

                var stringBuilder = new System.Text.StringBuilder();
                var parameters = new List<MySqlParameter>();

                stringBuilder.Append(@"
                INSERT INTO user_skills (
                    user_id, skill_id, rare, level, experiment, star, quality, block, quantity,
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
                    (@user_id, @skill_id_{j}, @rare_{j}, 0, 0, 0, @quality_{j}, 0, @quantity_{j},
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
                        new MySqlParameter($"@skill_id_{j}", c.Id),
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
                    quantity = user_skills.quantity + VALUES(quantity);
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
    public async Task<bool> UpdateSkillLevelAsync(Skills skill, int level)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_skills
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
                WHERE user_id = @user_id AND skill_id = @skill_id;";

                await using var updateCommand = new MySqlCommand(updateSQL, connection);

                updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                updateCommand.Parameters.AddWithValue("@skill_id", skill.Id);
                updateCommand.Parameters.AddWithValue("@level", level);
                updateCommand.Parameters.AddWithValue("@power", skill.Power);
                updateCommand.Parameters.AddWithValue("@health", skill.Health);
                updateCommand.Parameters.AddWithValue("@physical_attack", skill.PhysicalAttack);
                updateCommand.Parameters.AddWithValue("@physical_defense", skill.PhysicalDefense);
                updateCommand.Parameters.AddWithValue("@magical_attack", skill.MagicalAttack);
                updateCommand.Parameters.AddWithValue("@magical_defense", skill.MagicalDefense);
                updateCommand.Parameters.AddWithValue("@chemical_attack", skill.ChemicalAttack);
                updateCommand.Parameters.AddWithValue("@chemical_defense", skill.ChemicalDefense);
                updateCommand.Parameters.AddWithValue("@atomic_attack", skill.AtomicAttack);
                updateCommand.Parameters.AddWithValue("@atomic_defense", skill.AtomicDefense);
                updateCommand.Parameters.AddWithValue("@mental_attack", skill.MentalAttack);
                updateCommand.Parameters.AddWithValue("@mental_defense", skill.MentalDefense);
                updateCommand.Parameters.AddWithValue("@speed", skill.Speed);
                updateCommand.Parameters.AddWithValue("@critical_damage_rate", skill.CriticalDamageRate);
                updateCommand.Parameters.AddWithValue("@critical_rate", skill.CriticalRate);
                updateCommand.Parameters.AddWithValue("@critical_resistance_rate", skill.CriticalResistanceRate);
                updateCommand.Parameters.AddWithValue("@ignore_critical_rate", skill.IgnoreCriticalRate);
                updateCommand.Parameters.AddWithValue("@penetration_rate", skill.PenetrationRate);
                updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", skill.PenetrationResistanceRate);
                updateCommand.Parameters.AddWithValue("@evasion_rate", skill.EvasionRate);
                updateCommand.Parameters.AddWithValue("@damage_absorption_rate", skill.DamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", skill.IgnoreDamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", skill.AbsorbedDamageRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", skill.VitalityRegenerationRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", skill.VitalityRegenerationResistanceRate);
                updateCommand.Parameters.AddWithValue("@accuracy_rate", skill.AccuracyRate);
                updateCommand.Parameters.AddWithValue("@lifesteal_rate", skill.LifestealRate);
                updateCommand.Parameters.AddWithValue("@shield_strength", skill.ShieldStrength);
                updateCommand.Parameters.AddWithValue("@tenacity", skill.Tenacity);
                updateCommand.Parameters.AddWithValue("@resistance_rate", skill.ResistanceRate);
                updateCommand.Parameters.AddWithValue("@combo_rate", skill.ComboRate);
                updateCommand.Parameters.AddWithValue("@ignore_combo_rate", skill.IgnoreComboRate);
                updateCommand.Parameters.AddWithValue("@combo_damage_rate", skill.ComboDamageRate);
                updateCommand.Parameters.AddWithValue("@combo_resistance_rate", skill.ComboResistanceRate);
                updateCommand.Parameters.AddWithValue("@stun_rate", skill.StunRate);
                updateCommand.Parameters.AddWithValue("@ignore_stun_rate", skill.IgnoreStunRate);
                updateCommand.Parameters.AddWithValue("@reflection_rate", skill.ReflectionRate);
                updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", skill.IgnoreReflectionRate);
                updateCommand.Parameters.AddWithValue("@reflection_damage_rate", skill.ReflectionDamageRate);
                updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", skill.ReflectionResistanceRate);
                updateCommand.Parameters.AddWithValue("@mana", skill.Mana);
                updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", skill.ManaRegenerationRate);
                updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", skill.DamageToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", skill.ResistanceToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", skill.DamageToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", skill.ResistanceToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@normal_damage_rate", skill.NormalDamageRate);
                updateCommand.Parameters.AddWithValue("@normal_resistance_rate", skill.NormalResistanceRate);
                updateCommand.Parameters.AddWithValue("@skill_damage_rate", skill.SkillDamageRate);
                updateCommand.Parameters.AddWithValue("@skill_resistance_rate", skill.SkillResistanceRate);

                await updateCommand.ExecuteNonQueryAsync();
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
    public async Task<bool> UpdateSkillBreakthroughAsync(Skills skill, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_skills
                SET 
                    star = @star, quantity = @quantity, power = @power, health = @health, 
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
                WHERE user_id = @user_id AND skill_id = @skill_id;";

                await using var updateCommand = new MySqlCommand(updateSQL, connection);

                updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                updateCommand.Parameters.AddWithValue("@skill_id", skill.Id);
                updateCommand.Parameters.AddWithValue("@star", star);
                updateCommand.Parameters.AddWithValue("@quantity", quantity);
                updateCommand.Parameters.AddWithValue("@power", skill.Power);
                updateCommand.Parameters.AddWithValue("@health", skill.Health);
                updateCommand.Parameters.AddWithValue("@physical_attack", skill.PhysicalAttack);
                updateCommand.Parameters.AddWithValue("@physical_defense", skill.PhysicalDefense);
                updateCommand.Parameters.AddWithValue("@magical_attack", skill.MagicalAttack);
                updateCommand.Parameters.AddWithValue("@magical_defense", skill.MagicalDefense);
                updateCommand.Parameters.AddWithValue("@chemical_attack", skill.ChemicalAttack);
                updateCommand.Parameters.AddWithValue("@chemical_defense", skill.ChemicalDefense);
                updateCommand.Parameters.AddWithValue("@atomic_attack", skill.AtomicAttack);
                updateCommand.Parameters.AddWithValue("@atomic_defense", skill.AtomicDefense);
                updateCommand.Parameters.AddWithValue("@mental_attack", skill.MentalAttack);
                updateCommand.Parameters.AddWithValue("@mental_defense", skill.MentalDefense);
                updateCommand.Parameters.AddWithValue("@speed", skill.Speed);
                updateCommand.Parameters.AddWithValue("@critical_damage_rate", skill.CriticalDamageRate);
                updateCommand.Parameters.AddWithValue("@critical_rate", skill.CriticalRate);
                updateCommand.Parameters.AddWithValue("@critical_resistance_rate", skill.CriticalResistanceRate);
                updateCommand.Parameters.AddWithValue("@ignore_critical_rate", skill.IgnoreCriticalRate);
                updateCommand.Parameters.AddWithValue("@penetration_rate", skill.PenetrationRate);
                updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", skill.PenetrationResistanceRate);
                updateCommand.Parameters.AddWithValue("@evasion_rate", skill.EvasionRate);
                updateCommand.Parameters.AddWithValue("@damage_absorption_rate", skill.DamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", skill.IgnoreDamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", skill.AbsorbedDamageRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", skill.VitalityRegenerationRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", skill.VitalityRegenerationResistanceRate);
                updateCommand.Parameters.AddWithValue("@accuracy_rate", skill.AccuracyRate);
                updateCommand.Parameters.AddWithValue("@lifesteal_rate", skill.LifestealRate);
                updateCommand.Parameters.AddWithValue("@shield_strength", skill.ShieldStrength);
                updateCommand.Parameters.AddWithValue("@tenacity", skill.Tenacity);
                updateCommand.Parameters.AddWithValue("@resistance_rate", skill.ResistanceRate);
                updateCommand.Parameters.AddWithValue("@combo_rate", skill.ComboRate);
                updateCommand.Parameters.AddWithValue("@ignore_combo_rate", skill.IgnoreComboRate);
                updateCommand.Parameters.AddWithValue("@combo_damage_rate", skill.ComboDamageRate);
                updateCommand.Parameters.AddWithValue("@combo_resistance_rate", skill.ComboResistanceRate);
                updateCommand.Parameters.AddWithValue("@stun_rate", skill.StunRate);
                updateCommand.Parameters.AddWithValue("@ignore_stun_rate", skill.IgnoreStunRate);
                updateCommand.Parameters.AddWithValue("@reflection_rate", skill.ReflectionRate);
                updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", skill.IgnoreReflectionRate);
                updateCommand.Parameters.AddWithValue("@reflection_damage_rate", skill.ReflectionDamageRate);
                updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", skill.ReflectionResistanceRate);
                updateCommand.Parameters.AddWithValue("@mana", skill.Mana);
                updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", skill.ManaRegenerationRate);
                updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", skill.DamageToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", skill.ResistanceToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", skill.DamageToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", skill.ResistanceToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@normal_damage_rate", skill.NormalDamageRate);
                updateCommand.Parameters.AddWithValue("@normal_resistance_rate", skill.NormalResistanceRate);
                updateCommand.Parameters.AddWithValue("@skill_damage_rate", skill.SkillDamageRate);
                updateCommand.Parameters.AddWithValue("@skill_resistance_rate", skill.SkillResistanceRate);

                await updateCommand.ExecuteNonQueryAsync();
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
    public async Task<Skills> GetUserSkillsByIdAsync(string userId, string Id)
    {
        Skills skill = new Skills();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"SELECT * FROM user_skills WHERE skill_id = @id AND user_id = @user_id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", Id);
                selectCommand.Parameters.AddWithValue("@user_id", userId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
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

        return skill;
    }
    public async Task<List<Skills>> GetUserCardHeroesSkillsAsync(string userId, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_heroes_skills chs
                    ON chs.skill_id = us.skill_id AND chs.skill_id = @skill_id
                WHERE us.user_id = @userId
                ORDER BY s.name REGEXP '[0-9]+$', 
                         CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name;";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                selectCommand.Parameters.AddWithValue("@skill_id", cardId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rare = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        Position = reader.GetIntSafe("position"),
                        SkillType = reader.GetStringSafe("skill_type"),
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

                    skills.Add(skill);
                }

                // Load Effects cho toàn bộ Skills
                skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
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

        return skills;
    }
    public async Task<List<Skills>> GetUserCardCaptainsSkillsAsync(string userId, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_captains_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_captain_id = @card_captain_id
                WHERE us.user_id = @userId
                ORDER BY s.name REGEXP '[0-9]+$', 
                         CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name;";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                selectCommand.Parameters.AddWithValue("@card_captain_id", cardId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rare = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        Position = reader.GetIntSafe("position"),
                        SkillType = reader.GetStringSafe("skill_type"),
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

                    skills.Add(skill);
                }

                // Load Effects cho toàn bộ Skills
                skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
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

        return skills;
    }
    public async Task<List<Skills>> GetUserCardColonelsSkillsAsync(string userId, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_colonels_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_colonel_id = @card_colonel_id
                WHERE us.user_id = @userId
                ORDER BY s.name REGEXP '[0-9]+$', 
                         CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name;";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                selectCommand.Parameters.AddWithValue("@card_colonel_id", cardId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rare = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        Position = reader.GetIntSafe("position"),
                        SkillType = reader.GetStringSafe("skill_type"),
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

                    skills.Add(skill);
                }

                // Load Effects cho toàn bộ Skills
                skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
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

        return skills;
    }
    public async Task<List<Skills>> GetUserCardGeneralsSkillsAsync(string userId, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_generals_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_general_id = @card_general_id
                WHERE us.user_id = @userId
                ORDER BY s.name REGEXP '[0-9]+$', 
                         CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name;";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                selectCommand.Parameters.AddWithValue("@card_general_id", cardId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rare = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        Position = reader.GetIntSafe("position"),
                        SkillType = reader.GetStringSafe("skill_type"),
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

                    skills.Add(skill);
                }

                // Load Effects cho toàn bộ Skills
                skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
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

        return skills;
    }
    public async Task<List<Skills>> GetUserCardAdmiralsSkillsAsync(string userId, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_admirals_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_admiral_id = @card_admiral_id
                WHERE us.user_id = @userId
                ORDER BY s.name REGEXP '[0-9]+$', 
                         CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name;";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                selectCommand.Parameters.AddWithValue("@card_admiral_id", cardId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rare = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        Position = reader.GetIntSafe("position"),
                        SkillType = reader.GetStringSafe("skill_type"),
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

                    skills.Add(skill);
                }

                // Load Effects cho toàn bộ Skills đã lấy
                skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
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

        return skills;
    }
    public async Task<List<Skills>> GetUserCardMilitariesSkillsAsync(string userId, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_militaries_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_military_id = @card_military_id
                WHERE us.user_id = @userId
                ORDER BY s.name REGEXP '[0-9]+$', 
                         CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name;";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                selectCommand.Parameters.AddWithValue("@card_military_id", cardId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rare = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        Position = reader.GetIntSafe("position"),
                        SkillType = reader.GetStringSafe("skill_type"),
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

                    skills.Add(skill);
                }

                // Load Effects cho toàn bộ Skills đã lấy
                skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
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

        return skills;
    }
    public async Task<List<Skills>> GetUserCardMonstersSkillsAsync(string userId, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_monsters_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_monster_id = @card_monster_id
                WHERE us.user_id = @userId
                ORDER BY s.name REGEXP '[0-9]+$', 
                         CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name;";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                selectCommand.Parameters.AddWithValue("@card_monster_id", cardId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rare = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        Position = reader.GetIntSafe("position"),
                        SkillType = reader.GetStringSafe("skill_type"),
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

                    skills.Add(skill);
                }

                // Load Effects cho toàn bộ Skills đã lấy
                skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
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

        return skills;
    }
    public async Task<List<Skills>> GetUserCardSpellsSkillsAsync(string userId, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_spells_skills chs 
                    ON chs.skill_id = us.skill_id AND chs.card_spell_id = @card_spell_id
                WHERE us.user_id = @userId
                ORDER BY s.name REGEXP '[0-9]+$', 
                         CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name;";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                selectCommand.Parameters.AddWithValue("@card_spell_id", cardId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rare = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        Position = reader.GetIntSafe("position"),
                        SkillType = reader.GetStringSafe("skill_type"),
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

                    skills.Add(skill);
                }

                // Load Effects cho toàn bộ Skills đã lấy
                skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
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

        return skills;
    }
    public async Task<List<Skills>> LoadSkillsWithEffectsAsync(string userId, List<Skills> skillsList, MySqlConnection connection)
    {
        // Kiểm tra danh sách Skills
        var skillIds = skillsList.Select(s => s.Id).ToList();
        if (!skillIds.Any()) return skillsList;

        // Chuyển danh sách ID sang chuỗi cho WHERE IN
        string skillIdInClause = string.Join(",", skillIds.Select(id => $"'{id}'"));

        string combinedQuery = $@"
        SELECT 
            s.id AS Skill_Id,
            e.*, 
            ep.*, 
            ea.*
        FROM skills s
        JOIN skill_effect se ON s.id = se.skill_id
        JOIN effects e ON se.effect_id = e.id
        JOIN effect_property_action epa ON e.id = epa.effect_id
        JOIN effect_property ep ON epa.property_id = ep.property_id
        JOIN effect_action ea ON epa.action_id = ea.action_id
        WHERE s.id IN ({skillIdInClause});";

        // Tạo dictionary Skill ID → Skill
        var skillDict = skillsList.ToDictionary(s => s.Id);

        await using var selectCommand = new MySqlCommand(combinedQuery, connection);
        await using var reader = await selectCommand.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            string currentSkillId = reader.GetStringSafe("Skill_Id");
            if (!skillDict.TryGetValue(currentSkillId, out Skills currentSkill)) continue;

            var newEffect = new Effects
            {
                Id = reader.GetIntSafe("id"),
                Name = reader.GetStringSafe("name"),
                EffectType = reader.GetStringSafe("effect_type"),
                Duration = reader.IsDBNull(reader.GetOrdinal("duration")) ? 0 : reader.GetIntSafe("duration"),
                Description = reader.GetStringSafe("description"),
                EffectProperty = new EffectProperty
                {
                    PropertyId = reader.GetIntSafe("property_id"),
                    PropertyCode = reader.GetStringSafe("property_code"),
                    PropertyName = reader.GetStringSafe("property_name"),
                },
                EffectAction = new EffectAction
                {
                    ActionId = reader.GetIntSafe("action_id"),
                    ActionCode = reader.GetStringSafe("action_code"),
                    ActionName = reader.GetStringSafe("action_name"),
                }
            };

            currentSkill.Effects.Add(newEffect);
        }

        return skillsList;
    }
    public async Task<bool> InsertUserCardHeroSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM card_heroes_skills 
            WHERE user_id = @user_id AND skill_id = @skill_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@skill_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO card_heroes_skills (
                    user_id, skill_id, skill_id, level, position
                ) VALUES (
                    @user_id, @skill_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertSQL, connection);
                insertCommand.Parameters.AddWithValue("@user_id", userId);
                insertCommand.Parameters.AddWithValue("@skill_id", cardId);
                insertCommand.Parameters.AddWithValue("@skill_id", skillId);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@position", position);

                await insertCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> InsertUserCardCaptainSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM card_captains_skills 
            WHERE user_id = @user_id AND card_captain_id = @card_captain_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_captain_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO card_captains_skills (
                    user_id, card_captain_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_captain_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertSQL, connection);
                insertCommand.Parameters.AddWithValue("@user_id", userId);
                insertCommand.Parameters.AddWithValue("@card_captain_id", cardId);
                insertCommand.Parameters.AddWithValue("@skill_id", skillId);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@position", position);

                await insertCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> InsertUserCardColonelSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM card_colonels_skills 
            WHERE user_id = @user_id AND card_colonel_id = @card_colonel_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_colonel_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO card_colonels_skills (
                    user_id, card_colonel_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_colonel_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertSQL, connection);
                insertCommand.Parameters.AddWithValue("@user_id", userId);
                insertCommand.Parameters.AddWithValue("@card_colonel_id", cardId);
                insertCommand.Parameters.AddWithValue("@skill_id", skillId);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@position", position);

                await insertCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> InsertUserCardGeneralSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM card_generals_skills 
            WHERE user_id = @user_id AND card_general_id = @card_general_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_general_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO card_generals_skills (
                    user_id, card_general_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_general_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertSQL, connection);
                insertCommand.Parameters.AddWithValue("@user_id", userId);
                insertCommand.Parameters.AddWithValue("@card_general_id", cardId);
                insertCommand.Parameters.AddWithValue("@skill_id", skillId);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@position", position);

                await insertCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> InsertUserCardAdmiralSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM card_admirals_skills 
            WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_admiral_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO card_admirals_skills (
                    user_id, card_admiral_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_admiral_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertSQL, connection);
                insertCommand.Parameters.AddWithValue("@user_id", userId);
                insertCommand.Parameters.AddWithValue("@card_admiral_id", cardId);
                insertCommand.Parameters.AddWithValue("@skill_id", skillId);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@position", position);

                await insertCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> InsertUserCardMilitarySkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM card_militaries_skills 
            WHERE user_id = @user_id AND card_military_id = @card_military_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_military_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO card_militaries_skills (
                    user_id, card_military_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_military_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertSQL, connection);
                insertCommand.Parameters.AddWithValue("@user_id", userId);
                insertCommand.Parameters.AddWithValue("@card_military_id", cardId);
                insertCommand.Parameters.AddWithValue("@skill_id", skillId);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@position", position);

                await insertCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> InsertUserCardMonsterSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM card_monsters_skills 
            WHERE user_id = @user_id AND card_monster_id = @card_monster_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_monster_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO card_monsters_skills (
                    user_id, card_monster_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_monster_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertSQL, connection);
                insertCommand.Parameters.AddWithValue("@user_id", userId);
                insertCommand.Parameters.AddWithValue("@card_monster_id", cardId);
                insertCommand.Parameters.AddWithValue("@skill_id", skillId);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@position", position);

                await insertCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> InsertUserCardSpellSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM card_spells_skills 
            WHERE user_id = @user_id AND card_spell_id = @card_spell_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_spell_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO card_spells_skills (
                    user_id, card_spell_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_spell_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertSQL, connection);
                insertCommand.Parameters.AddWithValue("@user_id", userId);
                insertCommand.Parameters.AddWithValue("@card_spell_id", cardId);
                insertCommand.Parameters.AddWithValue("@skill_id", skillId);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@position", position);

                await insertCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> DeleteUserCardHeroSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string deleteSQL = @"
            DELETE FROM card_heroes_skills 
            WHERE user_id = @user_id AND skill_id = @skill_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteSQL, connection);
            deleteCommand.Parameters.AddWithValue("@user_id", userId);
            deleteCommand.Parameters.AddWithValue("@skill_id", cardId);
            deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
            deleteCommand.Parameters.AddWithValue("@position", position);

            await deleteCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> DeleteUserCardCaptainSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string deleteSQL = @"
            DELETE FROM card_captains_skills 
            WHERE user_id = @user_id AND card_captain_id = @card_captain_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteSQL, connection);
            deleteCommand.Parameters.AddWithValue("@user_id", userId);
            deleteCommand.Parameters.AddWithValue("@card_captain_id", cardId);
            deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
            deleteCommand.Parameters.AddWithValue("@position", position);

            await deleteCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> DeleteUserCardColonelSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string deleteSQL = @"
            DELETE FROM card_colonels_skills 
            WHERE user_id = @user_id AND card_colonel_id = @card_colonel_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteSQL, connection);
            deleteCommand.Parameters.AddWithValue("@user_id", userId);
            deleteCommand.Parameters.AddWithValue("@card_colonel_id", cardId);
            deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
            deleteCommand.Parameters.AddWithValue("@position", position);

            await deleteCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> DeleteUserCardGeneralSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string deleteSQL = @"
            DELETE FROM card_generals_skills 
            WHERE user_id = @user_id AND card_general_id = @card_general_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteSQL, connection);
            deleteCommand.Parameters.AddWithValue("@user_id", userId);
            deleteCommand.Parameters.AddWithValue("@card_general_id", cardId);
            deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
            deleteCommand.Parameters.AddWithValue("@position", position);

            await deleteCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> DeleteUserCardAdmiralSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string deleteSQL = @"
            DELETE FROM card_admirals_skills 
            WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteSQL, connection);
            deleteCommand.Parameters.AddWithValue("@user_id", userId);
            deleteCommand.Parameters.AddWithValue("@card_admiral_id", cardId);
            deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
            deleteCommand.Parameters.AddWithValue("@position", position);

            await deleteCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> DeleteUserCardMonsterSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string deleteSQL = @"
            DELETE FROM card_monsters_skills 
            WHERE user_id = @user_id AND card_monster_id = @card_monster_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteSQL, connection);
            deleteCommand.Parameters.AddWithValue("@user_id", userId);
            deleteCommand.Parameters.AddWithValue("@card_monster_id", cardId);
            deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
            deleteCommand.Parameters.AddWithValue("@position", position);

            await deleteCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> DeleteUserCardMilitarySkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string deleteSQL = @"
            DELETE FROM card_militaries_skills 
            WHERE user_id = @user_id AND card_military_id = @card_military_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteSQL, connection);
            deleteCommand.Parameters.AddWithValue("@user_id", userId);
            deleteCommand.Parameters.AddWithValue("@card_military_id", cardId);
            deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
            deleteCommand.Parameters.AddWithValue("@position", position);

            await deleteCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> DeleteUserCardSpellSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string deleteSQL = @"
            DELETE FROM card_spells_skills 
            WHERE user_id = @user_id AND card_spell_id = @card_spell_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteSQL, connection);
            deleteCommand.Parameters.AddWithValue("@user_id", userId);
            deleteCommand.Parameters.AddWithValue("@card_spell_id", cardId);
            deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
            deleteCommand.Parameters.AddWithValue("@position", position);

            await deleteCommand.ExecuteNonQueryAsync();
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

        return true;
    }
}