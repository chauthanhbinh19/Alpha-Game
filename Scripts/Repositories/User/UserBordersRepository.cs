using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserBordersRepository : IUserBordersRepository
{
    public async Task<List<Borders>> GetUserBordersAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Borders> borders = new List<Borders>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
            SELECT um.*, m.id, m.name, m.image, m.rare, m.description 
            FROM borders m
            JOIN user_borders um ON m.id = um.border_id
            WHERE um.user_id = @userId 
                AND (@rare = 'All' OR m.rare = @rare)
                AND (@search = '' OR m.name LIKE CONCAT('%', @search, '%'))
            ORDER BY m.name REGEXP '[0-9]+$', 
                     CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), 
                     m.name
            LIMIT @limit OFFSET @offset";

                await using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@search", search);
                command.Parameters.AddWithValue("@rare", rare);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);

                await using MySqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Borders border = new Borders
                    {
                        Id = reader.GetString("id"),
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
    public async Task<int> GetUserBordersCountAsync(string user_id, string search, string rare)
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
                FROM Medals m
                JOIN user_medals um ON m.id = um.medal_id
                WHERE um.user_id = @userId 
                    AND (@rare = 'All' OR m.rare = @rare)
                    AND (@search = '' OR m.name LIKE CONCAT('%', @search, '%'))";

                await using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@search", search);
                command.Parameters.AddWithValue("@rare", rare);

                object result = await command.ExecuteScalarAsync();
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
    public async Task<bool> InsertUserBorderAsync(Borders border, string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
            SELECT COUNT(*) FROM user_borders 
            WHERE user_id = @user_id AND border_id = @border_id;";

                await using MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
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
                    string insertQuery = @"
                INSERT INTO user_borders (
                    user_id, border_id, rare, level, experiment, star, quality, block, quantity,
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
                    @user_id, @border_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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

                    await using MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@user_id", userId);
                    insertCommand.Parameters.AddWithValue("@border_id", border.Id);
                    insertCommand.Parameters.AddWithValue("@rare", border.Rare);
                    insertCommand.Parameters.AddWithValue("@level", 0);
                    insertCommand.Parameters.AddWithValue("@experiment", 0);
                    insertCommand.Parameters.AddWithValue("@star", 0);
                    insertCommand.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(border.Rare));
                    insertCommand.Parameters.AddWithValue("@block", false);
                    insertCommand.Parameters.AddWithValue("@quantity", border.Quantity);
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
                    string updateQuery = @"
                UPDATE user_borders
                SET quantity = @quantity
                WHERE user_id = @user_id AND border_id = @border_id;";

                    await using MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@border_id", border.Id);
                    updateCommand.Parameters.AddWithValue("@quantity", border.Quantity);

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
    public async Task<bool> InsertUserBorderByIdAsync(Borders border, string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
            SELECT COUNT(*) FROM user_borders 
            WHERE user_id = @user_id AND border_id = @border_id;";

                await using MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
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
                    string insertQuery = @"
                INSERT INTO user_borders (
                    user_id, border_id, rare, level, experiment, star, quality, block, quantity, is_used,
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
                    @user_id, @border_id, @rare, @level, @experiment, @star, @quality, @block, @quantity, @is_used,
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

                    await using MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@user_id", userId);
                    insertCommand.Parameters.AddWithValue("@border_id", border.Id);
                    insertCommand.Parameters.AddWithValue("@rare", border.Rare);
                    insertCommand.Parameters.AddWithValue("@level", 0);
                    insertCommand.Parameters.AddWithValue("@experiment", 0);
                    insertCommand.Parameters.AddWithValue("@star", 0);
                    insertCommand.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(border.Rare));
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
                    string updateQuery = @"
                UPDATE user_borders
                SET quantity = quantity + 1
                WHERE user_id = @user_id AND border_id = @border_id;";

                    await using MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
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
    public async Task UpdateIsUsedBorderAsync(string borderId, string userId, bool is_used)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = "UPDATE user_borders SET is_used=@is_used WHERE user_id=@user_id AND border_id=@border_id";

                await using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", userId);
                command.Parameters.AddWithValue("@border_id", borderId);
                command.Parameters.AddWithValue("@is_used", is_used);

                await command.ExecuteNonQueryAsync();
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
    public async Task<Borders> GetBorderByUsedAsync(string user_id)
    {
        Borders borders = new Borders();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
            SELECT ub.*, b.image, b.rare 
            FROM user_borders ub
            JOIN borders b ON ub.border_id = b.id
            WHERE ub.is_used = TRUE AND ub.user_id = @user_id";

                await using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);

                await using MySqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    borders = new Borders
                    {
                        Id = reader.GetString("border_id"),
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
        return borders;
    }
    public async Task<Borders> SumPowerUserBordersAsync()
    {
        Borders sumBorders = new Borders();
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
            FROM user_borders
            WHERE user_id = @user_id;";

                await using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                await using MySqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    sumBorders.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                    sumBorders.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                    sumBorders.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                    sumBorders.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                    sumBorders.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                    sumBorders.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                    sumBorders.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                    sumBorders.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                    sumBorders.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                    sumBorders.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                    sumBorders.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                    sumBorders.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                    sumBorders.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                    sumBorders.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                    sumBorders.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                    sumBorders.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                    sumBorders.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                    sumBorders.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                    sumBorders.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                    sumBorders.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                    sumBorders.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                    sumBorders.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                    sumBorders.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                    sumBorders.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                    sumBorders.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                    sumBorders.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                    sumBorders.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                    sumBorders.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                    sumBorders.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                    sumBorders.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                    sumBorders.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                    sumBorders.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                    sumBorders.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                    sumBorders.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                    sumBorders.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                    sumBorders.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                    sumBorders.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                    sumBorders.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                    sumBorders.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                    sumBorders.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                    sumBorders.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDouble("total_mana");
                    sumBorders.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                    sumBorders.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                    sumBorders.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                    sumBorders.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                    sumBorders.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                    sumBorders.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                    sumBorders.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                    sumBorders.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                    sumBorders.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
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