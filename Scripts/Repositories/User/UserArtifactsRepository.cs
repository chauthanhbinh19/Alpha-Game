using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UserArtifactsRepository : IUserArtifactsRepository
{
    public async Task<List<Artifacts>> GetUserArtifactsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Artifacts> artifacts = new List<Artifacts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT ut.*, t.id, t.name, t.image, t.rare, t.description
                FROM Artifacts t
                INNER JOIN user_artifacts ut ON t.id = ut.artifact_id
                WHERE ut.user_id = @userId";

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    query += " AND t.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND t.name LIKE CONCAT('%', @search, '%')";
                }

                query += @"
                ORDER BY t.name REGEXP '[0-9]+$',
                         CAST(REGEXP_SUBSTR(t.name, '[0-9]+$') AS UNSIGNED),
                         t.name
                LIMIT @limit OFFSET @offset;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
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

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Artifacts artifact = new Artifacts
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

                            artifacts.Add(artifact);
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

        return artifacts;
    }
    public async Task<int> GetUserArtifactsCountAsync(string user_id, string search, string rare)
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
                FROM Artifacts t
                INNER JOIN user_artifacts ut ON t.id = ut.artifact_id
                WHERE ut.user_id = @userId";

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    query += " AND t.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND t.name LIKE CONCAT('%', @search, '%')";
                }

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
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
    public async Task<bool> InsertUserArtifactAsync(Artifacts artifact, string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_artifacts 
                WHERE user_id = @user_id AND artifact_id = @artifact_id;";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@artifact_id", artifact.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"
                        INSERT INTO user_artifacts (
                            user_id, artifact_id, rare, level, experiment, star, quality, block, quantity,
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
                            @user_id, @artifact_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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

                        await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@user_id", userId);
                            insertCommand.Parameters.AddWithValue("@artifact_id", artifact.Id);
                            insertCommand.Parameters.AddWithValue("@rare", artifact.Rare);
                            insertCommand.Parameters.AddWithValue("@level", 0);
                            insertCommand.Parameters.AddWithValue("@experiment", 0);
                            insertCommand.Parameters.AddWithValue("@star", 0);
                            insertCommand.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(artifact.Rare));
                            insertCommand.Parameters.AddWithValue("@block", false);
                            insertCommand.Parameters.AddWithValue("@quantity", artifact.Quantity);
                            insertCommand.Parameters.AddWithValue("@power", artifact.Power);
                            insertCommand.Parameters.AddWithValue("@health", artifact.Health);
                            insertCommand.Parameters.AddWithValue("@physical_attack", artifact.PhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@physical_defense", artifact.PhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@magical_attack", artifact.MagicalAttack);
                            insertCommand.Parameters.AddWithValue("@magical_defense", artifact.MagicalDefense);
                            insertCommand.Parameters.AddWithValue("@chemical_attack", artifact.ChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@chemical_defense", artifact.ChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@atomic_attack", artifact.AtomicAttack);
                            insertCommand.Parameters.AddWithValue("@atomic_defense", artifact.AtomicDefense);
                            insertCommand.Parameters.AddWithValue("@mental_attack", artifact.MentalAttack);
                            insertCommand.Parameters.AddWithValue("@mental_defense", artifact.MentalDefense);
                            insertCommand.Parameters.AddWithValue("@speed", artifact.Speed);
                            insertCommand.Parameters.AddWithValue("@critical_damage_rate", artifact.CriticalDamageRate);
                            insertCommand.Parameters.AddWithValue("@critical_rate", artifact.CriticalRate);
                            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", artifact.CriticalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", artifact.IgnoreCriticalRate);
                            insertCommand.Parameters.AddWithValue("@penetration_rate", artifact.PenetrationRate);
                            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", artifact.PenetrationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@evasion_rate", artifact.EvasionRate);
                            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", artifact.DamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", artifact.IgnoreDamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", artifact.AbsorbedDamageRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", artifact.VitalityRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", artifact.VitalityRegenerationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@accuracy_rate", artifact.AccuracyRate);
                            insertCommand.Parameters.AddWithValue("@lifesteal_rate", artifact.LifestealRate);
                            insertCommand.Parameters.AddWithValue("@shield_strength", artifact.ShieldStrength);
                            insertCommand.Parameters.AddWithValue("@tenacity", artifact.Tenacity);
                            insertCommand.Parameters.AddWithValue("@resistance_rate", artifact.ResistanceRate);
                            insertCommand.Parameters.AddWithValue("@combo_rate", artifact.ComboRate);
                            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", artifact.IgnoreComboRate);
                            insertCommand.Parameters.AddWithValue("@combo_damage_rate", artifact.ComboDamageRate);
                            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", artifact.ComboResistanceRate);
                            insertCommand.Parameters.AddWithValue("@stun_rate", artifact.StunRate);
                            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", artifact.IgnoreStunRate);
                            insertCommand.Parameters.AddWithValue("@reflection_rate", artifact.ReflectionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", artifact.IgnoreReflectionRate);
                            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", artifact.ReflectionDamageRate);
                            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", artifact.ReflectionResistanceRate);
                            insertCommand.Parameters.AddWithValue("@mana", artifact.Mana);
                            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", artifact.ManaRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", artifact.DamageToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", artifact.ResistanceToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", artifact.DamageToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", artifact.ResistanceToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@normal_damage_rate", artifact.NormalDamageRate);
                            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", artifact.NormalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@skill_damage_rate", artifact.SkillDamageRate);
                            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", artifact.SkillResistanceRate);

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_artifacts
                        SET quantity = @quantity
                        WHERE user_id = @user_id AND artifact_id = @artifact_id;";

                        await using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@user_id", userId);
                            updateCommand.Parameters.AddWithValue("@artifact_id", artifact.Id);
                            updateCommand.Parameters.AddWithValue("@quantity", artifact.Quantity);

                            await updateCommand.ExecuteNonQueryAsync();
                        }
                    }
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
    public async Task<bool> UpdateArtifactLevelAsync(Artifacts artifact, int artifactLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string query = @"
                UPDATE user_artifacts
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
                    combo_rate = @comboRate, ignore_combo_rate = @ignore_combo_rate, combo_damage_rate = @combo_damage_rate, combo_resistance_rate = @combo_resistance_rate,
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
                WHERE user_id = @user_id AND artifact_id = @artifact_id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@artifact_id", artifact.Id);
                    command.Parameters.AddWithValue("@level", artifactLevel);
                    command.Parameters.AddWithValue("@power", artifact.Power);
                    command.Parameters.AddWithValue("@health", artifact.Health);
                    command.Parameters.AddWithValue("@physical_attack", artifact.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", artifact.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", artifact.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", artifact.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", artifact.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", artifact.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", artifact.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", artifact.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", artifact.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", artifact.MentalDefense);
                    command.Parameters.AddWithValue("@speed", artifact.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", artifact.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", artifact.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", artifact.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", artifact.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", artifact.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", artifact.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", artifact.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", artifact.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", artifact.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", artifact.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", artifact.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", artifact.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", artifact.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", artifact.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", artifact.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", artifact.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", artifact.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", artifact.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", artifact.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", artifact.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", artifact.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", artifact.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", artifact.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", artifact.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", artifact.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", artifact.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", artifact.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", artifact.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", artifact.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", artifact.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", artifact.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", artifact.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", artifact.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", artifact.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", artifact.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", artifact.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", artifact.SkillResistanceRate);

                    await command.ExecuteNonQueryAsync();
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
    public async Task<bool> UpdateArtifactBreakthroughAsync(Artifacts artifact, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string query = @"
                UPDATE user_artifacts
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
                    combo_rate = @comboRate, ignore_combo_rate = @ignore_combo_rate, combo_damage_rate = @combo_damage_rate, combo_resistance_rate = @combo_resistance_rate,
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
                WHERE user_id = @user_id AND artifact_id = @artifact_id;";
                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@artifact_id", artifact.Id);
                    command.Parameters.AddWithValue("@star", star);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@power", artifact.Power);
                    command.Parameters.AddWithValue("@health", artifact.Health);
                    command.Parameters.AddWithValue("@physical_attack", artifact.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", artifact.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", artifact.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", artifact.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", artifact.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", artifact.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", artifact.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", artifact.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", artifact.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", artifact.MentalDefense);
                    command.Parameters.AddWithValue("@speed", artifact.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", artifact.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", artifact.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", artifact.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", artifact.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", artifact.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", artifact.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", artifact.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", artifact.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", artifact.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", artifact.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", artifact.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", artifact.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", artifact.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", artifact.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", artifact.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", artifact.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", artifact.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", artifact.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", artifact.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", artifact.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", artifact.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", artifact.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", artifact.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", artifact.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", artifact.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", artifact.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", artifact.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", artifact.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", artifact.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", artifact.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", artifact.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", artifact.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", artifact.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", artifact.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", artifact.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", artifact.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", artifact.SkillResistanceRate);

                    await command.ExecuteNonQueryAsync();
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
    public async Task<Artifacts> GetUserArtifactByIdAsync(string user_id, string Id)
    {
        Artifacts artifact = new Artifacts();
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string query = @"Select * from user_artifacts where user_artifacts.artifact_id=@id 
                and user_artifacts.user_id=@user_id";
                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@user_id", user_id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            artifact = new Artifacts
                            {
                                Id = reader.GetStringSafe("artifact_id"),
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
        return artifact;
    }
    public async Task<Artifacts> SumPowerUserArtifactsAsync()
    {
        Artifacts sumArtifacts = new Artifacts();
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string query = @"SELECT 
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
            FROM user_artifacts
            WHERE user_id = @user_id;
            ";
                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumArtifacts.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            sumArtifacts.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            sumArtifacts.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            sumArtifacts.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            sumArtifacts.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            sumArtifacts.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            sumArtifacts.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            sumArtifacts.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            sumArtifacts.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            sumArtifacts.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            sumArtifacts.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            sumArtifacts.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            sumArtifacts.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            sumArtifacts.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            sumArtifacts.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            sumArtifacts.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            sumArtifacts.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            sumArtifacts.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            sumArtifacts.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            sumArtifacts.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            sumArtifacts.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            sumArtifacts.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            sumArtifacts.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            sumArtifacts.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            sumArtifacts.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            sumArtifacts.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            sumArtifacts.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            sumArtifacts.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            sumArtifacts.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            sumArtifacts.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            sumArtifacts.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            sumArtifacts.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            sumArtifacts.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            sumArtifacts.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            sumArtifacts.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            sumArtifacts.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            sumArtifacts.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            sumArtifacts.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            sumArtifacts.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            sumArtifacts.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            sumArtifacts.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDoubleSafe("total_mana");
                            sumArtifacts.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            sumArtifacts.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            sumArtifacts.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            sumArtifacts.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            sumArtifacts.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            sumArtifacts.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            sumArtifacts.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            sumArtifacts.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            sumArtifacts.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
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
        return sumArtifacts;
    }
}