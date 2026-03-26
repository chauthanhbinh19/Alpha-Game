using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UserAvatarsRepository : IUserAvatarsRepository
{
    public async Task<List<Avatars>> GetUserAvatarsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Avatars> avatars = new List<Avatars>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT um.*, m.name, m.image, m.rare, m.description
                FROM avatars m
                JOIN user_avatars um ON m.id = um.avatar_id
                WHERE um.user_id = @userId 
                    AND (@rare = 'All' OR m.rare = @rare)
                    AND (@search = '' OR m.name LIKE CONCAT('%', @search, '%'))
                ORDER BY 
                    m.name REGEXP '[0-9]+$', 
                    CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), 
                    m.name
                LIMIT @limit OFFSET @offset;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@search", search);
                    command.Parameters.AddWithValue("@rare", rare);
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Avatars avatar = new Avatars
                            {
                                Id = reader.GetString("avatar_id"),
                                Name = reader.GetString("name"),
                                Image = reader.GetString("image"),
                                Rare = reader.GetString("rare"),
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

                            avatars.Add(avatar);
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

        return avatars;
    }
    public async Task<int> GetUserAvatarsCountAsync(string user_id, string search, string rare)
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
                FROM avatars m
                JOIN user_avatars um ON m.id = um.avatar_id
                WHERE um.user_id = @userId 
                    AND (@rare = 'All' OR m.rare = @rare)
                    AND (@search = '' OR m.name LIKE CONCAT('%', @search, '%'));
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@search", search);
                    command.Parameters.AddWithValue("@rare", rare);

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
    public async Task<bool> InsertUserAvatarAsync(Avatars avatar, string userId)
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
                FROM user_avatars
                WHERE user_id = @user_id AND avatar_id = @avatar_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@avatar_id", avatar.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"
                        INSERT INTO user_avatars (
                            user_id, avatar_id, rare, level, experiment, star, quality, block, quantity,
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
                            @user_id, @avatar_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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

                        await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@user_id", userId);
                            insertCommand.Parameters.AddWithValue("@avatar_id", avatar.Id);
                            insertCommand.Parameters.AddWithValue("@rare", avatar.Rare);
                            insertCommand.Parameters.AddWithValue("@level", 0);
                            insertCommand.Parameters.AddWithValue("@experiment", 0);
                            insertCommand.Parameters.AddWithValue("@star", 0);
                            insertCommand.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(avatar.Rare));
                            insertCommand.Parameters.AddWithValue("@block", false);
                            insertCommand.Parameters.AddWithValue("@quantity", avatar.Quantity);
                            insertCommand.Parameters.AddWithValue("@power", avatar.Power);
                            insertCommand.Parameters.AddWithValue("@health", avatar.Health);
                            insertCommand.Parameters.AddWithValue("@physical_attack", avatar.PhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@physical_defense", avatar.PhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@magical_attack", avatar.MagicalAttack);
                            insertCommand.Parameters.AddWithValue("@magical_defense", avatar.MagicalDefense);
                            insertCommand.Parameters.AddWithValue("@chemical_attack", avatar.ChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@chemical_defense", avatar.ChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@atomic_attack", avatar.AtomicAttack);
                            insertCommand.Parameters.AddWithValue("@atomic_defense", avatar.AtomicDefense);
                            insertCommand.Parameters.AddWithValue("@mental_attack", avatar.MentalAttack);
                            insertCommand.Parameters.AddWithValue("@mental_defense", avatar.MentalDefense);
                            insertCommand.Parameters.AddWithValue("@speed", avatar.Speed);
                            insertCommand.Parameters.AddWithValue("@critical_damage_rate", avatar.CriticalDamageRate);
                            insertCommand.Parameters.AddWithValue("@critical_rate", avatar.CriticalRate);
                            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", avatar.CriticalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", avatar.IgnoreCriticalRate);
                            insertCommand.Parameters.AddWithValue("@penetration_rate", avatar.PenetrationRate);
                            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", avatar.PenetrationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@evasion_rate", avatar.EvasionRate);
                            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", avatar.DamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", avatar.IgnoreDamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", avatar.AbsorbedDamageRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", avatar.VitalityRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", avatar.VitalityRegenerationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@accuracy_rate", avatar.AccuracyRate);
                            insertCommand.Parameters.AddWithValue("@lifesteal_rate", avatar.LifestealRate);
                            insertCommand.Parameters.AddWithValue("@shield_strength", avatar.ShieldStrength);
                            insertCommand.Parameters.AddWithValue("@tenacity", avatar.Tenacity);
                            insertCommand.Parameters.AddWithValue("@resistance_rate", avatar.ResistanceRate);
                            insertCommand.Parameters.AddWithValue("@combo_rate", avatar.ComboRate);
                            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", avatar.IgnoreComboRate);
                            insertCommand.Parameters.AddWithValue("@combo_damage_rate", avatar.ComboDamageRate);
                            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", avatar.ComboResistanceRate);
                            insertCommand.Parameters.AddWithValue("@stun_rate", avatar.StunRate);
                            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", avatar.IgnoreStunRate);
                            insertCommand.Parameters.AddWithValue("@reflection_rate", avatar.ReflectionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", avatar.IgnoreReflectionRate);
                            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", avatar.ReflectionDamageRate);
                            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", avatar.ReflectionResistanceRate);
                            insertCommand.Parameters.AddWithValue("@mana", avatar.Mana);
                            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", avatar.ManaRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", avatar.DamageToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", avatar.ResistanceToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", avatar.DamageToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", avatar.ResistanceToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@normal_damage_rate", avatar.NormalDamageRate);
                            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", avatar.NormalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@skill_damage_rate", avatar.SkillDamageRate);
                            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", avatar.SkillResistanceRate);

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_avatars
                        SET quantity = @quantity
                        WHERE user_id = @user_id AND avatar_id = @avatar_id;
                    ";

                        await using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@user_id", userId);
                            updateCommand.Parameters.AddWithValue("@avatar_id", avatar.Id);
                            updateCommand.Parameters.AddWithValue("@quantity", avatar.Quantity);

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
    public async Task<bool> InsertUserAvatarByIdAsync(Avatars avatar, string userId)
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
                    FROM user_avatars
                    WHERE user_id = @user_id AND avatar_id = @avatar_id;
                ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@avatar_id", avatar.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"
                        INSERT INTO user_avatars (
                            user_id, avatar_id, rare, level, experiment, star, quality, block, quantity, is_used,
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
                            @user_id, @avatar_id, @rare, @level, @experiment, @star, @quality, @block, @quantity, @is_used,
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

                        await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@user_id", userId);
                            insertCommand.Parameters.AddWithValue("@avatar_id", avatar.Id);
                            insertCommand.Parameters.AddWithValue("@rare", avatar.Rare);
                            insertCommand.Parameters.AddWithValue("@level", 0);
                            insertCommand.Parameters.AddWithValue("@experiment", 0);
                            insertCommand.Parameters.AddWithValue("@star", 0);
                            insertCommand.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(avatar.Rare));
                            insertCommand.Parameters.AddWithValue("@block", false);
                            insertCommand.Parameters.AddWithValue("@is_used", false);
                            insertCommand.Parameters.AddWithValue("@quantity", 1);
                            insertCommand.Parameters.AddWithValue("@power", avatar.Power);
                            insertCommand.Parameters.AddWithValue("@health", avatar.Health);
                            insertCommand.Parameters.AddWithValue("@physical_attack", avatar.PhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@physical_defense", avatar.PhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@magical_attack", avatar.MagicalAttack);
                            insertCommand.Parameters.AddWithValue("@magical_defense", avatar.MagicalDefense);
                            insertCommand.Parameters.AddWithValue("@chemical_attack", avatar.ChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@chemical_defense", avatar.ChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@atomic_attack", avatar.AtomicAttack);
                            insertCommand.Parameters.AddWithValue("@atomic_defense", avatar.AtomicDefense);
                            insertCommand.Parameters.AddWithValue("@mental_attack", avatar.MentalAttack);
                            insertCommand.Parameters.AddWithValue("@mental_defense", avatar.MentalDefense);
                            insertCommand.Parameters.AddWithValue("@speed", avatar.Speed);
                            insertCommand.Parameters.AddWithValue("@critical_damage_rate", avatar.CriticalDamageRate);
                            insertCommand.Parameters.AddWithValue("@critical_rate", avatar.CriticalRate);
                            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", avatar.CriticalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", avatar.IgnoreCriticalRate);
                            insertCommand.Parameters.AddWithValue("@penetration_rate", avatar.PenetrationRate);
                            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", avatar.PenetrationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@evasion_rate", avatar.EvasionRate);
                            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", avatar.DamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", avatar.IgnoreDamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", avatar.AbsorbedDamageRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", avatar.VitalityRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", avatar.VitalityRegenerationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@accuracy_rate", avatar.AccuracyRate);
                            insertCommand.Parameters.AddWithValue("@lifesteal_rate", avatar.LifestealRate);
                            insertCommand.Parameters.AddWithValue("@shield_strength", avatar.ShieldStrength);
                            insertCommand.Parameters.AddWithValue("@tenacity", avatar.Tenacity);
                            insertCommand.Parameters.AddWithValue("@resistance_rate", avatar.ResistanceRate);
                            insertCommand.Parameters.AddWithValue("@combo_rate", avatar.ComboRate);
                            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", avatar.IgnoreComboRate);
                            insertCommand.Parameters.AddWithValue("@combo_damage_rate", avatar.ComboDamageRate);
                            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", avatar.ComboResistanceRate);
                            insertCommand.Parameters.AddWithValue("@stun_rate", avatar.StunRate);
                            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", avatar.IgnoreStunRate);
                            insertCommand.Parameters.AddWithValue("@reflection_rate", avatar.ReflectionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", avatar.IgnoreReflectionRate);
                            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", avatar.ReflectionDamageRate);
                            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", avatar.ReflectionResistanceRate);
                            insertCommand.Parameters.AddWithValue("@mana", avatar.Mana);
                            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", avatar.ManaRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", avatar.DamageToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", avatar.ResistanceToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", avatar.DamageToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", avatar.ResistanceToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@normal_damage_rate", avatar.NormalDamageRate);
                            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", avatar.NormalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@skill_damage_rate", avatar.SkillDamageRate);
                            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", avatar.SkillResistanceRate);

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_avatars
                        SET quantity = quantity + 1
                        WHERE user_id = @user_id AND avatar_id = @avatar_id;
                    ";

                        await using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@user_id", userId);
                            updateCommand.Parameters.AddWithValue("@avatar_id", avatar.Id);

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
    public async Task<Avatars> GetAvatarByUsedAsync(string user_id)
    {
        Avatars avatars = new Avatars();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT ub.*, b.image, b.rare 
                    FROM user_avatars ub
                    JOIN avatars b ON ub.avatar_id = b.id
                    WHERE ub.is_used = TRUE AND ub.user_id = @user_id;
                ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            avatars = new Avatars
                            {
                                Id = reader.GetString("avatar_id"),
                                Image = reader.GetString("image"),
                                Rare = reader.GetString("rare"),
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
                                SkillResistanceRate = reader.GetDouble("skill_resistance_rate")
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

        return avatars;
    }
    public async Task UpdateIsUsedAvatarAsync(string avatarId, string userId, bool is_used)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_avatars 
                SET is_used = @is_used 
                WHERE user_id = @user_id AND avatar_id = @avatar_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@avatar_id", avatarId);
                    command.Parameters.AddWithValue("@is_used", is_used);

                    await command.ExecuteNonQueryAsync();
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
    }
    public async Task<Avatars> SumPowerUserAvatarsAsync()
    {
        Avatars sumAvatars = new Avatars();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
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
                FROM user_avatars
                WHERE user_id = @user_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumAvatars.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                            sumAvatars.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                            sumAvatars.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDouble("total_mana");
                            sumAvatars.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                            sumAvatars.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                            sumAvatars.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                            sumAvatars.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                            sumAvatars.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                            sumAvatars.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                            sumAvatars.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                            sumAvatars.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                            sumAvatars.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                            sumAvatars.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                            sumAvatars.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                            sumAvatars.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                            sumAvatars.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                            sumAvatars.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                            sumAvatars.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                            sumAvatars.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                            sumAvatars.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                            sumAvatars.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                            sumAvatars.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                            sumAvatars.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                            sumAvatars.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                            sumAvatars.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                            sumAvatars.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                            sumAvatars.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                            sumAvatars.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                            sumAvatars.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                            sumAvatars.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                            sumAvatars.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                            sumAvatars.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                            sumAvatars.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                            sumAvatars.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                            sumAvatars.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                            sumAvatars.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                            sumAvatars.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                            sumAvatars.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                            sumAvatars.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                            sumAvatars.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                            sumAvatars.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                            sumAvatars.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                            sumAvatars.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                            sumAvatars.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                            sumAvatars.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                            sumAvatars.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                            sumAvatars.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                            sumAvatars.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                            sumAvatars.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                            sumAvatars.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
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

        return sumAvatars;
    }
}