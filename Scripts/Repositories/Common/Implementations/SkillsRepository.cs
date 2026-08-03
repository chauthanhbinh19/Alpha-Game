using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class SkillsRepository : ISkillsRepository
{
    public async Task<List<string>> GetUniqueSkillsTypesAsync()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = "SELECT DISTINCT type FROM Skills";
            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    typeList.Add(reader.GetString(0));
                }
            }
        }

        return typeList;
    }
    public async Task<List<string>> GetUniqueSkillsIdAsync()
    {
        List<string> idList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = "SELECT DISTINCT id FROM Skills";
            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    idList.Add(reader.GetString(0));
                }
            }
        }

        return idList;
    }
    public async Task<List<Skills>> GetSkillsAsync(string search, string rare, string type, int pageSize, int offset)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT * FROM Skills
                WHERE 1=1";

                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND name LIKE CONCAT('%', @search, '%')";
                }

                selectSQL += " LIMIT @limit OFFSET @offset";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
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

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Skills skill = new Skills
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rarity = reader.GetStringSafe("rare"),
                                Quality = reader.GetDoubleSafe("quality"),
                                Quantity = 1,
                                Type = reader.GetStringSafe("type"),
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
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return skills;
    }
    public async Task<List<Skills>> GetSkillsWithoutLimitAsync()
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"SELECT * FROM skills";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Skills skill = new Skills
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rarity = reader.GetStringSafe("rare"),
                                Quality = reader.GetDoubleSafe("quality"),
                                Quantity = 1,
                                Type = reader.GetStringSafe("type"),
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

                            skills.Add(skill);
                        }
                    }
                }
            }
            catch (MySqlConnector.MySqlException ex)
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
    public async Task<int> GetSkillsCountAsync(string search, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"SELECT COUNT(*) FROM skills WHERE 1=1";

                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND name LIKE CONCAT('%', @search, '%')";
                }

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
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
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return count;
    }
    public async Task<InsertOrUpdateResult<Skills>> InsertSkillAsync(Skills entity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        const string sql = @"
        INSERT INTO skills (
            id, name, availability_type, image, rare, quality, type, skill_type,
            skill_sub_type, target_type, target_count, power, health,
            physical_attack, physical_defense, magical_attack, magical_defense,
            chemical_attack, chemical_defense, atomic_attack, atomic_defense,
            mental_attack, mental_defense, speed, critical_damage_rate,
            critical_rate, critical_resistance_rate, ignore_critical_rate,
            penetration_rate, penetration_resistance_rate, evasion_rate,
            damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
            vitality_regeneration_rate, vitality_regeneration_resistance_rate,
            accuracy_rate, lifesteal_rate, shield_strength, tenacity,
            resistance_rate, combo_rate, ignore_combo_rate, combo_damage_rate,
            combo_resistance_rate, stun_rate, ignore_stun_rate, reflection_rate,
            ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
            mana, mana_regeneration_rate, damage_to_different_faction_rate,
            resistance_to_different_faction_rate, damage_to_same_faction_rate,
            resistance_to_same_faction_rate, normal_damage_rate, normal_resistance_rate,
            skill_damage_rate, skill_resistance_rate, description, is_deleted, is_active
        ) VALUES (
            @id, @name, @availability_type, @image, @rare, @quality, @type, @skill_type,
            @skill_sub_type, @target_type, @target_count, @power, @health,
            @physical_attack, @physical_defense, @magical_attack, @magical_defense,
            @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense,
            @mental_attack, @mental_defense, @speed, @critical_damage_rate,
            @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
            @penetration_rate, @penetration_resistance_rate, @evasion_rate,
            @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
            @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
            @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity,
            @resistance_rate, @combo_rate, @ignore_combo_rate, @combo_damage_rate,
            @combo_resistance_rate, @stun_rate, @ignore_stun_rate, @reflection_rate,
            @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
            @mana, @mana_regeneration_rate, @damage_to_different_faction_rate,
            @resistance_to_different_faction_rate, @damage_to_same_faction_rate,
            @resistance_to_same_faction_rate, @normal_damage_rate, @normal_resistance_rate,
            @skill_damage_rate, @skill_resistance_rate, @description, @is_deleted, @is_active
        );";

        try
        {
            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                AddParameters(cmd, entity);
                await conn.OpenAsync();

                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                if (rowsAffected > 0)
                {
                    return InsertOrUpdateResult<Skills>.Inserted(entity);
                }

                return InsertOrUpdateResult<Skills>.Failure();
            }
        }
        catch (Exception ex)
        {
            return InsertOrUpdateResult<Skills>.Failure($"Failed When Insert Skill: {ex.Message}");
        }
    }
    public async Task<InsertOrUpdateResult<Skills>> UpdateSkillAsync(Skills entity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        const string sql = @"
        UPDATE skills SET
            name = @name,
            availability_type = @availability_type,
            image = @image,
            rare = @rare,
            quality = @quality,
            type = @type,
            skill_type = @skill_type,
            skill_sub_type = @skill_sub_type,
            target_type = @target_type,
            target_count = @target_count,
            power = @power,
            health = @health,
            physical_attack = @physical_attack,
            physical_defense = @physical_defense,
            magical_attack = @magical_attack,
            magical_defense = @magical_defense,
            chemical_attack = @chemical_attack,
            chemical_defense = @chemical_defense,
            atomic_attack = @atomic_attack,
            atomic_defense = @atomic_defense,
            mental_attack = @mental_attack,
            mental_defense = @mental_defense,
            speed = @speed,
            critical_damage_rate = @critical_damage_rate,
            critical_rate = @critical_rate,
            critical_resistance_rate = @critical_resistance_rate,
            ignore_critical_rate = @ignore_critical_rate,
            penetration_rate = @penetration_rate,
            penetration_resistance_rate = @penetration_resistance_rate,
            evasion_rate = @evasion_rate,
            damage_absorption_rate = @damage_absorption_rate,
            ignore_damage_absorption_rate = @ignore_damage_absorption_rate,
            absorbed_damage_rate = @absorbed_damage_rate,
            vitality_regeneration_rate = @vitality_regeneration_rate,
            vitality_regeneration_resistance_rate = @vitality_regeneration_resistance_rate,
            accuracy_rate = @accuracy_rate,
            lifesteal_rate = @lifesteal_rate,
            shield_strength = @shield_strength,
            tenacity = @tenacity,
            resistance_rate = @resistance_rate,
            combo_rate = @combo_rate,
            ignore_combo_rate = @ignore_combo_rate,
            combo_damage_rate = @combo_damage_rate,
            combo_resistance_rate = @combo_resistance_rate,
            stun_rate = @stun_rate,
            ignore_stun_rate = @ignore_stun_rate,
            reflection_rate = @reflection_rate,
            ignore_reflection_rate = @ignore_reflection_rate,
            reflection_damage_rate = @reflection_damage_rate,
            reflection_resistance_rate = @reflection_resistance_rate,
            mana = @mana,
            mana_regeneration_rate = @mana_regeneration_rate,
            damage_to_different_faction_rate = @damage_to_different_faction_rate,
            resistance_to_different_faction_rate = @resistance_to_different_faction_rate,
            damage_to_same_faction_rate = @damage_to_same_faction_rate,
            resistance_to_same_faction_rate = @resistance_to_same_faction_rate,
            normal_damage_rate = @normal_damage_rate,
            normal_resistance_rate = @normal_resistance_rate,
            skill_damage_rate = @skill_damage_rate,
            skill_resistance_rate = @skill_resistance_rate,
            description = @description,
            is_deleted = @is_deleted,
            is_active = @is_active
        WHERE id = @id;";

        try
        {
            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                AddParameters(cmd, entity);
                await conn.OpenAsync();

                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                if (rowsAffected > 0)
                {
                    return InsertOrUpdateResult<Skills>.Updated(entity);
                }

                return InsertOrUpdateResult<Skills>.Failure();
            }
        }
        catch (Exception ex)
        {
            return InsertOrUpdateResult<Skills>.Failure($"Failed When Update Skill: {ex.Message}");
        }
    }
    private void AddParameters(MySqlCommand command, Skills entity)
    {
        command.Parameters.AddWithValue("@id", entity.Id ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@name", entity.Name ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@availability_type", entity.AvailabilityType ?? "Normal");
        command.Parameters.AddWithValue("@image", entity.Image ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@rare", entity.Rarity ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@quality", entity.Quality);
        command.Parameters.AddWithValue("@type", entity.Type ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@skill_type", entity.SkillType ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@skill_sub_type", entity.SkillSubType.SubTypeCode ?? "DAMAGE");
        command.Parameters.AddWithValue("@target_type", entity.TargetType ?? "self");
        command.Parameters.AddWithValue("@target_count", entity.TargetCount);
        command.Parameters.AddWithValue("@power", entity.Power);
        command.Parameters.AddWithValue("@health", entity.Health);
        command.Parameters.AddWithValue("@physical_attack", entity.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", entity.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", entity.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", entity.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", entity.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", entity.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", entity.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", entity.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", entity.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", entity.MentalDefense);
        command.Parameters.AddWithValue("@speed", entity.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", entity.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", entity.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", entity.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", entity.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", entity.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", entity.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", entity.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", entity.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", entity.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", entity.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", entity.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", entity.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", entity.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", entity.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", entity.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", entity.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", entity.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", entity.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", entity.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", entity.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", entity.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", entity.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", entity.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", entity.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", entity.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", entity.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", entity.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", entity.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", entity.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", entity.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", entity.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", entity.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", entity.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", entity.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", entity.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", entity.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", entity.SkillResistanceRate);
        command.Parameters.AddWithValue("@description", entity.Description ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@is_deleted", entity.IsDeleted);
        command.Parameters.AddWithValue("@is_active", entity.IsActive);
    }
    public async Task<List<Skills>> GetSkillsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT s.*, st.price, cu.image AS currency_image, cu.id AS currency_id
                FROM skills s
                JOIN skill_trade st ON s.id = st.skill_id
                JOIN currencies cu ON st.currency_id = cu.id
                WHERE s.type = @type
                LIMIT @limit OFFSET @offset";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@type", type);
                    selectCommand.Parameters.AddWithValue("@limit", pageSize);
                    selectCommand.Parameters.AddWithValue("@offset", offset);

                    using (MySqlDataReader reader = (MySqlDataReader)await selectCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Skills skill = new Skills
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rarity = reader.GetStringSafe("rare"),
                                Quality = reader.GetDoubleSafe("quality"),
                                Type = reader.GetStringSafe("type"),
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

                            skill.Currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetIntSafe("price")
                            };

                            skills.Add(skill);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return skills;
    }
    public async Task<int> GetSkillsWithPriceCountAsync(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT COUNT(*)
                FROM skills s
                JOIN skill_trade st ON s.id = st.skill_id
                JOIN currencies cu ON st.currency_id = cu.id
                WHERE s.type = @type;";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@type", type);
                    object result = await selectCommand.ExecuteScalarAsync();
                    count = Convert.ToInt32(result);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return count;
    }
    public async Task<Skills> GetSkillByIdAsync(string Id)
    {
        Skills skill = new Skills();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM skills WHERE id = @id";
                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@id", Id);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            skill = new Skills
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rarity = reader.GetStringSafe("rare"),
                                Quality = reader.GetDoubleSafe("quality"),
                                Type = reader.GetStringSafe("type"),
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
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return skill;
    }
}