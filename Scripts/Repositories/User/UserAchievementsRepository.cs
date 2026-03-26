using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UserAchievementsRepository : IUserAchievementsRepository
{
    public async Task<List<Achievements>> GetUserAchievementsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Achievements> achievementsList = new List<Achievements>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT uc.*, c.id, c.name, c.image, c.rare, c.description 
                             FROM achievements c, user_achievements uc 
                             WHERE uc.achievement_id = c.id 
                               AND uc.user_id = @userId 
                               AND (@rare = 'All' OR c.rare = @rare)
                               AND (@search = '' OR c.name LIKE CONCAT('%', @search, '%'))
                             LIMIT @limit OFFSET @offset";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@search", search);
                    command.Parameters.AddWithValue("@rare", rare);
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    await using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Achievements achievements = new Achievements
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

                            achievementsList.Add(achievements);
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

        return achievementsList;
    }
    public async Task<int> GetUserArchievementsCountAsync(string user_id, string search, string rare)
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
                FROM achievements c
                JOIN user_achievements uc ON c.id = uc.achievement_id
                WHERE uc.user_id = @userId 
                    AND (@search = '' OR c.name LIKE CONCAT('%', @search, '%'))
                AND (@rare = 'All' OR c.rare = @rare);";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@search", search);
                command.Parameters.AddWithValue("@rare", rare);

                object result = await command.ExecuteScalarAsync();
                count = Convert.ToInt32(result);

                return count;
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
    public async Task<bool> InsertUserAchievementsAsync(Achievements achievement, string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_achievements
                WHERE user_id = @user_id AND achievement_id = @achievement_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@achievement_id", achievement.Id);

                int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                if (count == 0)
                {
                    string query = @"
                    INSERT INTO user_achievements (
                        user_id, achievement_id, rare, level, experiment, star, quality, block, quantity,
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
                        @user_id, @achievement_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@achievement_id", achievement.Id);
                    command.Parameters.AddWithValue("@rare", achievement.Rare);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(achievement.Rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", achievement.Quantity);

                    command.Parameters.AddWithValue("@power", achievement.Power);
                    command.Parameters.AddWithValue("@health", achievement.Health);
                    command.Parameters.AddWithValue("@physical_attack", achievement.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", achievement.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", achievement.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", achievement.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", achievement.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", achievement.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", achievement.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", achievement.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", achievement.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", achievement.MentalDefense);

                    command.Parameters.AddWithValue("@speed", achievement.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", achievement.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", achievement.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", achievement.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", achievement.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", achievement.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", achievement.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", achievement.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", achievement.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", achievement.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", achievement.AbsorbedDamageRate);

                    command.Parameters.AddWithValue("@vitality_regeneration_rate", achievement.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", achievement.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", achievement.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", achievement.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", achievement.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", achievement.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", achievement.ResistanceRate);

                    command.Parameters.AddWithValue("@combo_rate", achievement.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", achievement.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", achievement.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", achievement.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", achievement.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", achievement.IgnoreStunRate);

                    command.Parameters.AddWithValue("@reflection_rate", achievement.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", achievement.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", achievement.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", achievement.ReflectionResistanceRate);

                    command.Parameters.AddWithValue("@mana", achievement.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", achievement.ManaRegenerationRate);

                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", achievement.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", achievement.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", achievement.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", achievement.ResistanceToSameFactionRate);

                    command.Parameters.AddWithValue("@normal_damage_rate", achievement.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", achievement.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", achievement.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", achievement.SkillResistanceRate);

                    await command.ExecuteNonQueryAsync();
                }
                else
                {
                    string updateQuery = @"
                    UPDATE user_achievements
                    SET quantity = @quantity
                    WHERE user_id = @user_id AND achievement_id = @achievement_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@achievement_id", achievement.Id);
                    updateCommand.Parameters.AddWithValue("@quantity", achievement.Quantity);

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
    public async Task<bool> UpdateAchievementLevelAsync(Achievements achievement, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_achievements
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
                WHERE user_id = @user_id AND achievement_id = @achievement_id;
            ";

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@achievement_id", achievement.Id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", achievement.Power);
                command.Parameters.AddWithValue("@health", achievement.Health);
                command.Parameters.AddWithValue("@physical_attack", achievement.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", achievement.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", achievement.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", achievement.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", achievement.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", achievement.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", achievement.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", achievement.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", achievement.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", achievement.MentalDefense);
                command.Parameters.AddWithValue("@speed", achievement.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", achievement.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", achievement.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", achievement.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", achievement.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", achievement.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", achievement.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", achievement.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", achievement.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", achievement.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", achievement.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", achievement.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", achievement.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", achievement.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", achievement.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", achievement.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", achievement.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", achievement.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", achievement.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", achievement.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", achievement.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", achievement.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", achievement.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", achievement.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", achievement.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", achievement.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", achievement.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", achievement.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", achievement.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", achievement.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", achievement.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", achievement.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", achievement.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", achievement.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", achievement.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", achievement.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", achievement.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", achievement.SkillResistanceRate);

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
    public async Task<bool> UpdateAchievementBreakthroughAsync(Achievements achievement, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_achievements
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
                WHERE user_id = @user_id AND achievement_id = @achievement_id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@achievement_id", achievement.Id);
                    command.Parameters.AddWithValue("@star", star);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@power", achievement.Power);
                    command.Parameters.AddWithValue("@health", achievement.Health);
                    command.Parameters.AddWithValue("@physical_attack", achievement.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", achievement.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", achievement.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", achievement.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", achievement.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", achievement.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", achievement.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", achievement.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", achievement.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", achievement.MentalDefense);
                    command.Parameters.AddWithValue("@speed", achievement.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", achievement.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", achievement.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", achievement.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", achievement.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", achievement.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", achievement.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", achievement.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", achievement.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", achievement.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", achievement.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", achievement.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", achievement.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", achievement.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", achievement.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", achievement.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", achievement.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", achievement.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", achievement.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", achievement.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", achievement.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", achievement.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", achievement.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", achievement.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", achievement.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", achievement.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", achievement.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", achievement.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", achievement.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", achievement.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", achievement.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", achievement.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", achievement.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", achievement.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", achievement.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", achievement.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", achievement.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", achievement.SkillResistanceRate);

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
    public async Task<Achievements> GetUserAchievementByIdAsync(string user_id, string id)
    {
        Achievements card = new Achievements();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT * 
                FROM user_achievements 
                WHERE achievement_id = @id AND user_id = @user_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@user_id", user_id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            card = new Achievements
                            {
                                Id = reader.GetStringSafe("achievement_id"),
                                Level = reader.GetIntSafe("level"),
                                Quality = reader.GetDoubleSafe("quality"),
                                Experiment = reader.GetDoubleSafe("experiment"),
                                Star = reader.GetIntSafe("star"),
                                Rare = reader.GetStringSafe("rare"),
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

        return card;
    }
    public async Task<Achievements> SumPowerUserAchievementsAsync()
    {
        Achievements sumAchievements = new Achievements();
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
                FROM user_achievements
                WHERE user_id = @user_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumAchievements.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            sumAchievements.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            sumAchievements.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDoubleSafe("total_mana");
                            sumAchievements.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            sumAchievements.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            sumAchievements.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            sumAchievements.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            sumAchievements.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            sumAchievements.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            sumAchievements.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            sumAchievements.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            sumAchievements.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            sumAchievements.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            sumAchievements.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            sumAchievements.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            sumAchievements.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            sumAchievements.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            sumAchievements.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            sumAchievements.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            sumAchievements.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            sumAchievements.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            sumAchievements.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            sumAchievements.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            sumAchievements.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            sumAchievements.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            sumAchievements.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            sumAchievements.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            sumAchievements.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            sumAchievements.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            sumAchievements.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            sumAchievements.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            sumAchievements.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            sumAchievements.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            sumAchievements.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            sumAchievements.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            sumAchievements.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            sumAchievements.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            sumAchievements.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            sumAchievements.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            sumAchievements.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            sumAchievements.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            sumAchievements.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            sumAchievements.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            sumAchievements.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            sumAchievements.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            sumAchievements.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            sumAchievements.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            sumAchievements.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            sumAchievements.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            sumAchievements.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
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

        return sumAchievements;
    }
}