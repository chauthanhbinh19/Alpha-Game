using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;

public class UserSkillsRepository : IUserSkillsRepository
{
    public async Task<List<Skills>> GetUserSkillsAsync(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description 
                FROM skills s
                INNER JOIN user_skills us ON s.id = us.skill_id
                WHERE us.user_id = @userId 
                  AND s.type = @type 
                  AND (@rare = 'All' OR s.rare = @rare)
                ORDER BY s.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name
                LIMIT @limit OFFSET @offset;";

                await using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@rare", rare);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);

                await using var reader = await command.ExecuteReaderAsync();
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
    public async Task<int> GetUserSkillsCountAsync(string user_id, string type, string rare)
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
                FROM skills s
                INNER JOIN user_skills us ON s.id = us.skill_id
                WHERE us.user_id = @userId 
                AND s.type = @type 
                AND (@rare = 'All' OR s.rare = @rare);";

                await using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@rare", rare);

                var result = await command.ExecuteScalarAsync();
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
    public async Task<bool> InsertUserSkillAsync(Skills skills)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_skills 
                WHERE user_id = @user_id AND skill_id = @skill_id;";

                await using var checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@skill_id", skills.Id);

                var countObj = await checkCommand.ExecuteScalarAsync();
                int count = Convert.ToInt32(countObj);

                if (count == 0)
                {
                    string query = @"
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

                    await using var command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@skill_id", skills.Id);
                    command.Parameters.AddWithValue("@rare", skills.Rare);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(skills.Rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", skills.Quantity);
                    command.Parameters.AddWithValue("@power", skills.Power);
                    command.Parameters.AddWithValue("@health", skills.Health);
                    command.Parameters.AddWithValue("@physical_attack", skills.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", skills.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", skills.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", skills.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", skills.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", skills.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", skills.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", skills.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", skills.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", skills.MentalDefense);
                    command.Parameters.AddWithValue("@speed", skills.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", skills.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", skills.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", skills.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", skills.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", skills.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", skills.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", skills.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", skills.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", skills.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", skills.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", skills.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", skills.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", skills.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", skills.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", skills.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", skills.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", skills.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", skills.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", skills.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", skills.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", skills.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", skills.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", skills.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", skills.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", skills.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", skills.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", skills.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", skills.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", skills.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", skills.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", skills.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", skills.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", skills.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", skills.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", skills.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", skills.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", skills.SkillResistanceRate);

                    await command.ExecuteNonQueryAsync();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_skills
                    SET quantity = @quantity
                    WHERE user_id = @user_id AND skill_id = @skill_id;";

                    await using var updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@skill_id", skills.Id);
                    updateCommand.Parameters.AddWithValue("@quantity", skills.Quantity);

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
    public async Task<bool> UpdateSkillLevelAsync(Skills skills, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
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

                await using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@skill_id", skills.Id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", skills.Power);
                command.Parameters.AddWithValue("@health", skills.Health);
                command.Parameters.AddWithValue("@physical_attack", skills.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", skills.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", skills.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", skills.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", skills.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", skills.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", skills.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", skills.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", skills.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", skills.MentalDefense);
                command.Parameters.AddWithValue("@speed", skills.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", skills.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", skills.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", skills.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", skills.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", skills.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", skills.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", skills.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", skills.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", skills.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", skills.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", skills.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", skills.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", skills.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", skills.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", skills.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", skills.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", skills.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", skills.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", skills.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", skills.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", skills.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", skills.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", skills.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", skills.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", skills.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", skills.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", skills.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", skills.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", skills.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", skills.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", skills.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", skills.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", skills.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", skills.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", skills.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", skills.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", skills.SkillResistanceRate);

                await command.ExecuteNonQueryAsync();
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
    public async Task<bool> UpdateSkillBreakthroughAsync(Skills skills, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
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

                await using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@skill_id", skills.Id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", skills.Power);
                command.Parameters.AddWithValue("@health", skills.Health);
                command.Parameters.AddWithValue("@physical_attack", skills.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", skills.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", skills.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", skills.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", skills.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", skills.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", skills.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", skills.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", skills.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", skills.MentalDefense);
                command.Parameters.AddWithValue("@speed", skills.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", skills.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", skills.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", skills.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", skills.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", skills.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", skills.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", skills.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", skills.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", skills.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", skills.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", skills.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", skills.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", skills.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", skills.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", skills.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", skills.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", skills.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", skills.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", skills.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", skills.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", skills.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", skills.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", skills.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", skills.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", skills.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", skills.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", skills.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", skills.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", skills.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", skills.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", skills.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", skills.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", skills.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", skills.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", skills.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", skills.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", skills.SkillResistanceRate);

                await command.ExecuteNonQueryAsync();
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
    public async Task<Skills> GetUserSkillsByIdAsync(string user_id, string Id)
    {
        Skills skill = new Skills();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT * FROM user_skills WHERE skill_id = @id AND user_id = @user_id";

                await using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);

                await using var reader = await command.ExecuteReaderAsync();
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
    public async Task<List<Skills>> GetUserCardHeroesSkillsAsync(string user_id, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_heroes_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_hero_id = @card_hero_id
                WHERE us.user_id = @userId
                ORDER BY s.name REGEXP '[0-9]+$', 
                         CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name;";

                await using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@card_hero_id", cardId);

                await using var reader = await command.ExecuteReaderAsync();
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
                skills = await LoadSkillsWithEffectsAsync(user_id, skills, connection);
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
    public async Task<List<Skills>> GetUserCardCaptainsSkillsAsync(string user_id, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_captains_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_captain_id = @card_captain_id
                WHERE us.user_id = @userId
                ORDER BY s.name REGEXP '[0-9]+$', 
                         CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name;";

                await using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@card_captain_id", cardId);

                await using var reader = await command.ExecuteReaderAsync();
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
                skills = await LoadSkillsWithEffectsAsync(user_id, skills, connection);
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
    public async Task<List<Skills>> GetUserCardColonelsSkillsAsync(string user_id, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_colonels_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_colonel_id = @card_colonel_id
                WHERE us.user_id = @userId
                ORDER BY s.name REGEXP '[0-9]+$', 
                         CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name;";

                await using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@card_colonel_id", cardId);

                await using var reader = await command.ExecuteReaderAsync();
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
                skills = await LoadSkillsWithEffectsAsync(user_id, skills, connection);
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
    public async Task<List<Skills>> GetUserCardGeneralsSkillsAsync(string user_id, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_generals_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_general_id = @card_general_id
                WHERE us.user_id = @userId
                ORDER BY s.name REGEXP '[0-9]+$', 
                         CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name;";

                await using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@card_general_id", cardId);

                await using var reader = await command.ExecuteReaderAsync();
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
                skills = await LoadSkillsWithEffectsAsync(user_id, skills, connection);
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
    public async Task<List<Skills>> GetUserCardAdmiralsSkillsAsync(string user_id, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_admirals_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_admiral_id = @card_admiral_id
                WHERE us.user_id = @userId
                ORDER BY s.name REGEXP '[0-9]+$', 
                         CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name;";

                await using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@card_admiral_id", cardId);

                await using var reader = await command.ExecuteReaderAsync();
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
                skills = await LoadSkillsWithEffectsAsync(user_id, skills, connection);
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
    public async Task<List<Skills>> GetUserCardMilitariesSkillsAsync(string user_id, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_militaries_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_military_id = @card_military_id
                WHERE us.user_id = @userId
                ORDER BY s.name REGEXP '[0-9]+$', 
                         CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name;";

                await using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@card_military_id", cardId);

                await using var reader = await command.ExecuteReaderAsync();
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
                skills = await LoadSkillsWithEffectsAsync(user_id, skills, connection);
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
    public async Task<List<Skills>> GetUserCardMonstersSkillsAsync(string user_id, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_monsters_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_monster_id = @card_monster_id
                WHERE us.user_id = @userId
                ORDER BY s.name REGEXP '[0-9]+$', 
                         CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name;";

                await using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@card_monster_id", cardId);

                await using var reader = await command.ExecuteReaderAsync();
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
                skills = await LoadSkillsWithEffectsAsync(user_id, skills, connection);
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
    public async Task<List<Skills>> GetUserCardSpellsSkillsAsync(string user_id, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_spells_skills chs 
                    ON chs.skill_id = us.skill_id AND chs.card_spell_id = @card_spell_id
                WHERE us.user_id = @userId
                ORDER BY s.name REGEXP '[0-9]+$', 
                         CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name;";

                await using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@card_spell_id", cardId);

                await using var reader = await command.ExecuteReaderAsync();
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
                skills = await LoadSkillsWithEffectsAsync(user_id, skills, connection);
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

        await using var command = new MySqlCommand(combinedQuery, connection);
        await using var reader = await command.ExecuteReaderAsync();

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
            string checkQuery = @"
            SELECT COUNT(*) 
            FROM card_heroes_skills 
            WHERE user_id = @user_id AND card_hero_id = @card_hero_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkQuery, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_hero_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertQuery = @"
                INSERT INTO card_heroes_skills (
                    user_id, card_hero_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_hero_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", userId);
                insertCommand.Parameters.AddWithValue("@card_hero_id", cardId);
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
            string checkQuery = @"
            SELECT COUNT(*) 
            FROM card_captains_skills 
            WHERE user_id = @user_id AND card_captain_id = @card_captain_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkQuery, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_captain_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertQuery = @"
                INSERT INTO card_captains_skills (
                    user_id, card_captain_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_captain_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertQuery, connection);
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
            string checkQuery = @"
            SELECT COUNT(*) 
            FROM card_colonels_skills 
            WHERE user_id = @user_id AND card_colonel_id = @card_colonel_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkQuery, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_colonel_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertQuery = @"
                INSERT INTO card_colonels_skills (
                    user_id, card_colonel_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_colonel_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertQuery, connection);
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
            string checkQuery = @"
            SELECT COUNT(*) 
            FROM card_generals_skills 
            WHERE user_id = @user_id AND card_general_id = @card_general_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkQuery, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_general_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertQuery = @"
                INSERT INTO card_generals_skills (
                    user_id, card_general_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_general_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertQuery, connection);
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
            string checkQuery = @"
            SELECT COUNT(*) 
            FROM card_admirals_skills 
            WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkQuery, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_admiral_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertQuery = @"
                INSERT INTO card_admirals_skills (
                    user_id, card_admiral_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_admiral_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertQuery, connection);
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
            string checkQuery = @"
            SELECT COUNT(*) 
            FROM card_militaries_skills 
            WHERE user_id = @user_id AND card_military_id = @card_military_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkQuery, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_military_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertQuery = @"
                INSERT INTO card_militaries_skills (
                    user_id, card_military_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_military_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertQuery, connection);
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
            string checkQuery = @"
            SELECT COUNT(*) 
            FROM card_monsters_skills 
            WHERE user_id = @user_id AND card_monster_id = @card_monster_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkQuery, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_monster_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertQuery = @"
                INSERT INTO card_monsters_skills (
                    user_id, card_monster_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_monster_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertQuery, connection);
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
            string checkQuery = @"
            SELECT COUNT(*) 
            FROM card_spells_skills 
            WHERE user_id = @user_id AND card_spell_id = @card_spell_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkQuery, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_spell_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertQuery = @"
                INSERT INTO card_spells_skills (
                    user_id, card_spell_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_spell_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertQuery, connection);
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

            string deleteQuery = @"
            DELETE FROM card_heroes_skills 
            WHERE user_id = @user_id AND card_hero_id = @card_hero_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteQuery, connection);
            deleteCommand.Parameters.AddWithValue("@user_id", userId);
            deleteCommand.Parameters.AddWithValue("@card_hero_id", cardId);
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

            string deleteQuery = @"
            DELETE FROM card_captains_skills 
            WHERE user_id = @user_id AND card_captain_id = @card_captain_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteQuery, connection);
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

            string deleteQuery = @"
            DELETE FROM card_colonels_skills 
            WHERE user_id = @user_id AND card_colonel_id = @card_colonel_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteQuery, connection);
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

            string deleteQuery = @"
            DELETE FROM card_generals_skills 
            WHERE user_id = @user_id AND card_general_id = @card_general_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteQuery, connection);
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

            string deleteQuery = @"
            DELETE FROM card_admirals_skills 
            WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteQuery, connection);
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

            string deleteQuery = @"
            DELETE FROM card_monsters_skills 
            WHERE user_id = @user_id AND card_monster_id = @card_monster_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteQuery, connection);
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

            string deleteQuery = @"
            DELETE FROM card_militaries_skills 
            WHERE user_id = @user_id AND card_military_id = @card_military_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteQuery, connection);
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

            string deleteQuery = @"
            DELETE FROM card_spells_skills 
            WHERE user_id = @user_id AND card_spell_id = @card_spell_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteQuery, connection);
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