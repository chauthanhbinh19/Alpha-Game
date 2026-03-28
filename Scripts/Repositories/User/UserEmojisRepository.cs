using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UserEmojisRepository : IUserEmojisRepository
{
    public async Task<List<Emojis>> GetUserEmojisAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Emojis> cores = new List<Emojis>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT ut.*, t.id, t.name, t.image, t.rare, t.description 
                FROM Emojis t
                INNER JOIN user_Emojis ut ON t.id = ut.core_id
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
                ORDER BY t.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(t.name, '[0-9]+$') AS UNSIGNED), t.name
                LIMIT @limit OFFSET @offset;
            ";

                await using var command = new MySqlCommand(query, connection);
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

                await using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Emojis core = new Emojis
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

                    cores.Add(core);
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

        return cores;
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

                string query = @"
                SELECT COUNT(*) 
                FROM Emojis t
                INNER JOIN user_Emojis ut ON t.id = ut.core_id
                WHERE ut.user_id = @userId";

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    query += " AND t.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND t.name LIKE CONCAT('%', @search, '%')";
                }

                await using var command = new MySqlCommand(query, connection);
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
    public async Task<bool> InsertUserEmojiAsync(Emojis core, string userId)
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
                FROM user_Emojis 
                WHERE user_id = @user_id AND core_id = @core_id;
            ";

                await using var checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@core_id", core.Id);

                int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                if (count == 0)
                {
                    string insertQuery = @"
                    INSERT INTO user_Emojis (
                        user_id, core_id, rare, level, experiment, star, quality, block, quantity,
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
                        @user_id, @core_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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

                    await using var insertCommand = new MySqlCommand(insertQuery, connection);

                    insertCommand.Parameters.AddWithValue("@user_id", userId);
                    insertCommand.Parameters.AddWithValue("@core_id", core.Id);
                    insertCommand.Parameters.AddWithValue("@rare", core.Rare);
                    insertCommand.Parameters.AddWithValue("@level", 0);
                    insertCommand.Parameters.AddWithValue("@experiment", 0);
                    insertCommand.Parameters.AddWithValue("@star", 0);
                    insertCommand.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(core.Rare));
                    insertCommand.Parameters.AddWithValue("@block", false);
                    insertCommand.Parameters.AddWithValue("@quantity", core.Quantity);
                    insertCommand.Parameters.AddWithValue("@power", core.Power);
                    insertCommand.Parameters.AddWithValue("@health", core.Health);
                    insertCommand.Parameters.AddWithValue("@physical_attack", core.PhysicalAttack);
                    insertCommand.Parameters.AddWithValue("@physical_defense", core.PhysicalDefense);
                    insertCommand.Parameters.AddWithValue("@magical_attack", core.MagicalAttack);
                    insertCommand.Parameters.AddWithValue("@magical_defense", core.MagicalDefense);
                    insertCommand.Parameters.AddWithValue("@chemical_attack", core.ChemicalAttack);
                    insertCommand.Parameters.AddWithValue("@chemical_defense", core.ChemicalDefense);
                    insertCommand.Parameters.AddWithValue("@atomic_attack", core.AtomicAttack);
                    insertCommand.Parameters.AddWithValue("@atomic_defense", core.AtomicDefense);
                    insertCommand.Parameters.AddWithValue("@mental_attack", core.MentalAttack);
                    insertCommand.Parameters.AddWithValue("@mental_defense", core.MentalDefense);
                    insertCommand.Parameters.AddWithValue("@speed", core.Speed);
                    insertCommand.Parameters.AddWithValue("@critical_damage_rate", core.CriticalDamageRate);
                    insertCommand.Parameters.AddWithValue("@critical_rate", core.CriticalRate);
                    insertCommand.Parameters.AddWithValue("@critical_resistance_rate", core.CriticalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@ignore_critical_rate", core.IgnoreCriticalRate);
                    insertCommand.Parameters.AddWithValue("@penetration_rate", core.PenetrationRate);
                    insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", core.PenetrationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@evasion_rate", core.EvasionRate);
                    insertCommand.Parameters.AddWithValue("@damage_absorption_rate", core.DamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", core.IgnoreDamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", core.AbsorbedDamageRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", core.VitalityRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", core.VitalityRegenerationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@accuracy_rate", core.AccuracyRate);
                    insertCommand.Parameters.AddWithValue("@lifesteal_rate", core.LifestealRate);
                    insertCommand.Parameters.AddWithValue("@shield_strength", core.ShieldStrength);
                    insertCommand.Parameters.AddWithValue("@tenacity", core.Tenacity);
                    insertCommand.Parameters.AddWithValue("@resistance_rate", core.ResistanceRate);
                    insertCommand.Parameters.AddWithValue("@combo_rate", core.ComboRate);
                    insertCommand.Parameters.AddWithValue("@ignore_combo_rate", core.IgnoreComboRate);
                    insertCommand.Parameters.AddWithValue("@combo_damage_rate", core.ComboDamageRate);
                    insertCommand.Parameters.AddWithValue("@combo_resistance_rate", core.ComboResistanceRate);
                    insertCommand.Parameters.AddWithValue("@stun_rate", core.StunRate);
                    insertCommand.Parameters.AddWithValue("@ignore_stun_rate", core.IgnoreStunRate);
                    insertCommand.Parameters.AddWithValue("@reflection_rate", core.ReflectionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", core.IgnoreReflectionRate);
                    insertCommand.Parameters.AddWithValue("@reflection_damage_rate", core.ReflectionDamageRate);
                    insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", core.ReflectionResistanceRate);
                    insertCommand.Parameters.AddWithValue("@mana", core.Mana);
                    insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", core.ManaRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", core.DamageToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", core.ResistanceToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", core.DamageToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", core.ResistanceToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@normal_damage_rate", core.NormalDamageRate);
                    insertCommand.Parameters.AddWithValue("@normal_resistance_rate", core.NormalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@skill_damage_rate", core.SkillDamageRate);
                    insertCommand.Parameters.AddWithValue("@skill_resistance_rate", core.SkillResistanceRate);

                    await insertCommand.ExecuteNonQueryAsync();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_Emojis
                    SET quantity = @quantity
                    WHERE user_id = @user_id AND core_id = @core_id;
                ";

                    await using var updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@core_id", core.Id);
                    updateCommand.Parameters.AddWithValue("@quantity", core.Quantity);

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
    public async Task<bool> UpdateEmojiLevelAsync(Emojis core, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_Emojis
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
                WHERE user_id = @user_id AND core_id = @core_id;";

                await using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@core_id", core.Id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", core.Power);
                command.Parameters.AddWithValue("@health", core.Health);
                command.Parameters.AddWithValue("@physical_attack", core.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", core.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", core.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", core.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", core.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", core.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", core.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", core.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", core.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", core.MentalDefense);
                command.Parameters.AddWithValue("@speed", core.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", core.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", core.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", core.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", core.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", core.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", core.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", core.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", core.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", core.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", core.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", core.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", core.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", core.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", core.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", core.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", core.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", core.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", core.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", core.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", core.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", core.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", core.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", core.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", core.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", core.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", core.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", core.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", core.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", core.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", core.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", core.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", core.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", core.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", core.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", core.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", core.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", core.SkillResistanceRate);

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
    public async Task<bool> UpdateEmojiBreakthroughAsync(Emojis core, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_Emojis
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
                WHERE user_id = @user_id AND core_id = @core_id;";

                await using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@core_id", core.Id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", core.Power);
                command.Parameters.AddWithValue("@health", core.Health);
                command.Parameters.AddWithValue("@physical_attack", core.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", core.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", core.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", core.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", core.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", core.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", core.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", core.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", core.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", core.MentalDefense);
                command.Parameters.AddWithValue("@speed", core.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", core.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", core.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", core.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", core.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", core.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", core.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", core.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", core.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", core.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", core.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", core.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", core.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", core.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", core.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", core.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", core.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", core.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", core.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", core.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", core.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", core.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", core.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", core.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", core.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", core.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", core.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", core.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", core.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", core.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", core.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", core.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", core.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", core.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", core.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", core.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", core.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", core.SkillResistanceRate);

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
    public async Task<Emojis> GetUserEmojiByIdAsync(string user_id, string Id)
    {
        Emojis core = null;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT * FROM user_Emojis 
                             WHERE core_id = @id AND user_id = @user_id";

                await using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);

                await using MySqlDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    core = new Emojis
                    {
                        Id = reader.GetStringSafe("core_id"),
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

        return core;
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
                FROM user_Emojis
                WHERE user_id = @user_id;
            ";

                await using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                await using MySqlDataReader reader = await command.ExecuteReaderAsync();
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