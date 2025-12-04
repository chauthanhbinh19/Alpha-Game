using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class AchievementsGalleryRepository : IAchievementsGalleryRepository
{
    public async Task<List<Achievements>> GetAchievementCollectionAsync(int pageSize, int offset, string rare)
    {
        List<Achievements> achievements = new List<Achievements>();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT c.*, 
                        CASE 
                            WHEN cg.achievement_id IS NULL THEN 'block' 
                            WHEN cg.status = 'pending' THEN 'pending' 
                            WHEN cg.status = 'available' THEN 'available' 
                        END AS status 
                    FROM achievements c 
                    LEFT JOIN achievements_gallery cg 
                        ON c.id = cg.achievement_id AND cg.user_id = @userId 
                    WHERE (@rare = 'All' OR c.rare = @rare) 
                    LIMIT @limit OFFSET @offset;
                ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@rare", rare);
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    await using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Achievements achievement = new Achievements
                            {
                                Id = reader.GetString("id"),
                                Name = reader.GetString("name"),
                                Image = reader.GetString("image"),
                                Rare = reader.GetString("rare"),
                                Quality = reader.GetInt32("quality"),
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
                                Mana = reader.GetFloat("mana"),
                                ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                                NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                                SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                                SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                                PercentAllHealth = reader.GetDouble("percent_all_health"),
                                PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack"),
                                PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense"),
                                PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack"),
                                PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense"),
                                PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack"),
                                PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense"),
                                PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack"),
                                PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense"),
                                PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack"),
                                PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense"),
                                Description = reader.GetString("description"),
                                Status = reader.GetString("status"),
                            };

                            achievements.Add(achievement);
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

        return achievements;
    }
    public async Task<int> GetAchievementsCountAsync(string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = "SELECT COUNT(*) FROM achievements WHERE (@rare = 'All' OR rare = @rare)";
                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@rare", rare);

                    object result = await command.ExecuteScalarAsync();
                    count = Convert.ToInt32(result);
                }

                await connection.CloseAsync();
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
    public async Task InsertAchievementsGalleryAsync(string Id, Achievements AchievementFromDB)
    {
        int percent = 20;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra bản ghi tồn tại
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM achievements_gallery 
                WHERE user_id = @user_id AND achievement_id = @achievement_id;
                ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCommand.Parameters.AddWithValue("@achievement_id", Id);

                    int recordCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu chưa có → INSERT
                    if (recordCount == 0)
                    {
                        string query = @"
                        INSERT INTO achievements_gallery (
                            user_id, achievement_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
                            magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                            mental_attack, mental_defense, speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate, 
                            penetration_rate, penetration_resistance_rate, evasion_rate, 
                            damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate, vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                            accuracy_rate, lifesteal_rate, shield_strength, tenacity, 
                            resistance_rate, combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate, stun_rate, ignore_stun_rate, 
                            reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate, mana, mana_regeneration_rate, 
                            damage_to_different_faction_rate, resistance_to_different_faction_rate, 
                            damage_to_same_faction_rate, resistance_to_same_faction_rate, 
                            normal_damage_rate, normal_resistance_rate, 
                            skill_damage_rate, skill_resistance_rate, 
                            percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                            percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                            percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                            percent_all_mental_attack, percent_all_mental_defense
                        ) VALUES (
                            @user_id, @achievement_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
                            @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, 
                            @mental_attack, @mental_defense, @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate, 
                            @penetration_rate, @penetration_resistance_rate, @evasion_rate, 
                            @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate, @vitality_regeneration_rate, @vitality_regeneration_resistance_rate, 
                            @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, 
                            @resistance_rate, @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate, @stun_rate, @ignore_stun_rate, 
                            @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate, @mana, @mana_regeneration_rate, 
                            @damage_to_different_faction_rate, @resistance_to_different_faction_rate, 
                            @damage_to_same_faction_rate, @resistance_to_same_faction_rate, 
                            @normal_damage_rate, @normal_resistance_rate, 
                            @skill_damage_rate, @skill_resistance_rate, 
                            @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, 
                            @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                            @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, 
                            @percent_all_mental_attack, @percent_all_mental_defense
                        );
                        ";

                        await using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            command.Parameters.AddWithValue("@achievement_id", Id);
                            command.Parameters.AddWithValue("@status", "pending");
                            command.Parameters.AddWithValue("@current_star", 0);
                            command.Parameters.AddWithValue("@temp_star", 0);

                            command.Parameters.AddWithValue("@power", AchievementFromDB.Power);
                            command.Parameters.AddWithValue("@health", AchievementFromDB.Health);
                            command.Parameters.AddWithValue("@physical_attack", AchievementFromDB.PhysicalAttack);
                            command.Parameters.AddWithValue("@physical_defense", AchievementFromDB.PhysicalDefense);
                            command.Parameters.AddWithValue("@magical_attack", AchievementFromDB.MagicalAttack);
                            command.Parameters.AddWithValue("@magical_defense", AchievementFromDB.MagicalDefense);
                            command.Parameters.AddWithValue("@chemical_attack", AchievementFromDB.ChemicalAttack);
                            command.Parameters.AddWithValue("@chemical_defense", AchievementFromDB.ChemicalDefense);
                            command.Parameters.AddWithValue("@atomic_attack", AchievementFromDB.AtomicAttack);
                            command.Parameters.AddWithValue("@atomic_defense", AchievementFromDB.AtomicDefense);

                            command.Parameters.AddWithValue("@mental_attack", AchievementFromDB.MentalAttack);
                            command.Parameters.AddWithValue("@mental_defense", AchievementFromDB.MentalDefense);

                            command.Parameters.AddWithValue("@speed", AchievementFromDB.Speed);
                            command.Parameters.AddWithValue("@critical_damage_rate", AchievementFromDB.CriticalDamageRate);
                            command.Parameters.AddWithValue("@critical_rate", AchievementFromDB.CriticalRate);
                            command.Parameters.AddWithValue("@critical_resistance_rate", AchievementFromDB.CriticalResistanceRate);
                            command.Parameters.AddWithValue("@ignore_critical_rate", AchievementFromDB.IgnoreCriticalRate);
                            command.Parameters.AddWithValue("@penetration_rate", AchievementFromDB.PenetrationRate);
                            command.Parameters.AddWithValue("@penetration_resistance_rate", AchievementFromDB.PenetrationResistanceRate);
                            command.Parameters.AddWithValue("@evasion_rate", AchievementFromDB.EvasionRate);
                            command.Parameters.AddWithValue("@damage_absorption_rate", AchievementFromDB.DamageAbsorptionRate);
                            command.Parameters.AddWithValue("@ignore_damage_absorption_rate", AchievementFromDB.IgnoreDamageAbsorptionRate);
                            command.Parameters.AddWithValue("@absorbed_damage_rate", AchievementFromDB.AbsorbedDamageRate);

                            command.Parameters.AddWithValue("@vitality_regeneration_rate", AchievementFromDB.VitalityRegenerationRate);
                            command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", AchievementFromDB.VitalityRegenerationResistanceRate);

                            command.Parameters.AddWithValue("@accuracy_rate", AchievementFromDB.AccuracyRate);
                            command.Parameters.AddWithValue("@lifesteal_rate", AchievementFromDB.LifestealRate);
                            command.Parameters.AddWithValue("@shield_strength", AchievementFromDB.ShieldStrength);
                            command.Parameters.AddWithValue("@tenacity", AchievementFromDB.Tenacity);
                            command.Parameters.AddWithValue("@resistance_rate", AchievementFromDB.ResistanceRate);
                            command.Parameters.AddWithValue("@combo_rate", AchievementFromDB.ComboRate);
                            command.Parameters.AddWithValue("@ignore_combo_rate", AchievementFromDB.IgnoreComboRate);
                            command.Parameters.AddWithValue("@combo_damage_rate", AchievementFromDB.ComboDamageRate);
                            command.Parameters.AddWithValue("@combo_resistance_rate", AchievementFromDB.ComboResistanceRate);

                            command.Parameters.AddWithValue("@stun_rate", AchievementFromDB.StunRate);
                            command.Parameters.AddWithValue("@ignore_stun_rate", AchievementFromDB.IgnoreStunRate);
                            command.Parameters.AddWithValue("@reflection_rate", AchievementFromDB.ReflectionRate);
                            command.Parameters.AddWithValue("@ignore_reflection_rate", AchievementFromDB.IgnoreReflectionRate);
                            command.Parameters.AddWithValue("@reflection_damage_rate", AchievementFromDB.ReflectionDamageRate);
                            command.Parameters.AddWithValue("@reflection_resistance_rate", AchievementFromDB.ReflectionResistanceRate);

                            command.Parameters.AddWithValue("@mana", AchievementFromDB.Mana);
                            command.Parameters.AddWithValue("@mana_regeneration_rate", AchievementFromDB.ManaRegenerationRate);

                            command.Parameters.AddWithValue("@damage_to_different_faction_rate", AchievementFromDB.DamageToDifferentFactionRate);
                            command.Parameters.AddWithValue("@resistance_to_different_faction_rate", AchievementFromDB.ResistanceToDifferentFactionRate);
                            command.Parameters.AddWithValue("@damage_to_same_faction_rate", AchievementFromDB.DamageToSameFactionRate);
                            command.Parameters.AddWithValue("@resistance_to_same_faction_rate", AchievementFromDB.ResistanceToSameFactionRate);

                            command.Parameters.AddWithValue("@normal_damage_rate", AchievementFromDB.NormalDamageRate);
                            command.Parameters.AddWithValue("@normal_resistance_rate", AchievementFromDB.NormalResistanceRate);
                            command.Parameters.AddWithValue("@skill_damage_rate", AchievementFromDB.SkillDamageRate);
                            command.Parameters.AddWithValue("@skill_resistance_rate", AchievementFromDB.SkillResistanceRate);

                            command.Parameters.AddWithValue("@percent_all_health", percent);
                            command.Parameters.AddWithValue("@percent_all_physical_attack", percent);
                            command.Parameters.AddWithValue("@percent_all_physical_defense", percent);
                            command.Parameters.AddWithValue("@percent_all_magical_attack", percent);
                            command.Parameters.AddWithValue("@percent_all_magical_defense", percent);
                            command.Parameters.AddWithValue("@percent_all_chemical_attack", percent);
                            command.Parameters.AddWithValue("@percent_all_chemical_defense", percent);
                            command.Parameters.AddWithValue("@percent_all_atomic_attack", percent);
                            command.Parameters.AddWithValue("@percent_all_atomic_defense", percent);
                            command.Parameters.AddWithValue("@percent_all_mental_attack", percent);
                            command.Parameters.AddWithValue("@percent_all_mental_defense", percent);

                            await command.ExecuteNonQueryAsync();
                        }
                    }
                }

                await connection.CloseAsync();
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
    public async Task UpdateStatusAchievementsGalleryAsync(string Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"UPDATE achievements_gallery 
                             SET status=@status 
                             WHERE user_id=@user_id AND achievement_id=@achievement_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@achievement_id", Id);
                    command.Parameters.AddWithValue("@status", "available");

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
    public async Task UpdateStarAchievementsGalleryAsync(string id, double star)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Lấy current_star và temp_star
                string checkQuery = @"
                SELECT current_star, temp_star 
                FROM achievements_gallery 
                WHERE user_id = @user_id AND achievement_id = @achievement_id;
                ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCommand.Parameters.AddWithValue("@achievement_id", id);

                    await using (var reader = await checkCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            double tempStar = reader.GetDouble("temp_star");

                            // Nếu star mới cao hơn star tạm, cập nhật
                            if (tempStar < star)
                            {
                                reader.Close(); // đóng trước khi chạy lệnh khác

                                string updateQuery = @"
                                UPDATE achievements_gallery 
                                SET temp_star = @temp_star 
                                WHERE user_id = @user_id AND achievement_id = @achievement_id;
                            ";

                                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                                    updateCommand.Parameters.AddWithValue("@achievement_id", id);
                                    updateCommand.Parameters.AddWithValue("@temp_star", star);

                                    await updateCommand.ExecuteNonQueryAsync();
                                }
                            }
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
    }
    public async Task UpdateAchievementsGalleryPowerAsync(string id, Achievements AchievementFromDB)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"UPDATE achievements_gallery
                SET 
                    status = @status,
                    current_star = @current_star,
                    power = @power,
                    health = health + @health,
                    physical_attack = physical_attack + @physical_attack,
                    physical_defense = physical_defense + @physical_defense,
                    magical_attack = magical_attack + @magical_attack,
                    magical_defense = magical_defense + @magical_defense,
                    chemical_attack = chemical_attack + @chemical_attack,
                    chemical_defense = chemical_defense + @chemical_defense,
                    atomic_attack = atomic_attack + @atomic_attack,
                    atomic_defense = atomic_defense + @atomic_defense,
                    mental_attack = mental_attack + @mental_attack,
                    mental_defense = mental_defense + @mental_defense,
                    speed = speed + @speed,
                    critical_damage_rate = critical_damage_rate + @critical_damage_rate,
                    critical_rate = critical_rate + @critical_rate,
                    critical_resistance_rate = critical_resistance_rate + @critical_resistance_rate,
                    ignore_critical_rate = ignore_critical_rate + @ignore_critical_rate,
                    penetration_rate = penetration_rate + @penetration_rate,
                    penetration_resistance_rate = penetration_resistance_rate + @penetration_resistance_rate,
                    evasion_rate = evasion_rate + @evasion_rate,
                    damage_absorption_rate = damage_absorption_rate + @damage_absorption_rate,
                    ignore_damage_absorption_rate = ignore_damage_absorption_rate + @ignore_damage_absorption_rate,
                    absorbed_damage_rate = absorbed_damage_rate + @absorbed_damage_rate,
                    vitality_regeneration_rate = vitality_regeneration_rate + @vitality_regeneration_rate,
                    vitality_regeneration_resistance_rate = vitality_regeneration_resistance_rate + @vitality_regeneration_resistance_rate,
                    accuracy_rate = accuracy_rate + @accuracy_rate,
                    lifesteal_rate = lifesteal_rate + @lifesteal_rate,
                    shield_strength = shield_strength + @shield_strength,
                    tenacity = tenacity + @tenacity,
                    resistance_rate = resistance_rate + @resistance_rate,
                    combo_rate = combo_rate + @combo_rate,
                    ignore_combo_rate = ignore_combo_rate + @ignore_combo_rate,
                    combo_damage_rate = combo_damage_rate + @combo_damage_rate,
                    combo_resistance_rate = combo_resistance_rate + @combo_resistance_rate,
                    stun_rate = stun_rate + @stun_rate,
                    ignore_stun_rate = ignore_stun_rate + @ignore_stun_rate,
                    reflection_rate = reflection_rate + @reflection_rate,
                    ignore_reflection_rate = ignore_reflection_rate + @ignore_reflection_rate,
                    reflection_damage_rate = reflection_damage_rate + @reflection_damage_rate,
                    reflection_resistance_rate = reflection_resistance_rate + @reflection_resistance_rate,
                    mana = mana + @mana,
                    mana_regeneration_rate = mana_regeneration_rate + @mana_regeneration_rate,
                    damage_to_different_faction_rate = damage_to_different_faction_rate + @damage_to_different_faction_rate,
                    resistance_to_different_faction_rate = resistance_to_different_faction_rate + @resistance_to_different_faction_rate,
                    damage_to_same_faction_rate = damage_to_same_faction_rate + @damage_to_same_faction_rate,
                    resistance_to_same_faction_rate = resistance_to_same_faction_rate + @resistance_to_same_faction_rate,
                    normal_damage_rate = normal_damage_rate + @normal_damage_rate,
                    normal_resistance_rate = normal_resistance_rate + @normal_resistance_rate,
                    skill_damage_rate = skill_damage_rate + @skill_damage_rate,
                    skill_resistance_rate = skill_resistance_rate + @skill_resistance_rate,
                    percent_all_health = percent_all_health + @percent_all_health,
                    percent_all_physical_attack = percent_all_physical_attack + @percent_all_physical_attack,
                    percent_all_physical_defense = percent_all_physical_defense + @percent_all_physical_defense,
                    percent_all_magical_attack = percent_all_magical_attack + @percent_all_magical_attack,
                    percent_all_magical_defense = percent_all_magical_defense + @percent_all_magical_defense,
                    percent_all_chemical_attack = percent_all_chemical_attack + @percent_all_chemical_attack,
                    percent_all_chemical_defense = percent_all_chemical_defense + @percent_all_chemical_defense,
                    percent_all_atomic_attack = percent_all_atomic_attack + @percent_all_atomic_attack,
                    percent_all_atomic_defense = percent_all_atomic_defense + @percent_all_atomic_defense,
                    percent_all_mental_attack = percent_all_mental_attack + @percent_all_mental_attack,
                    percent_all_mental_defense = percent_all_mental_defense + @percent_all_mental_defense
                WHERE user_id = @user_id
                AND achievement_id = @achievement_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // IDs
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@achievement_id", id);

                    // Base flags
                    command.Parameters.AddWithValue("@status", "pending");
                    command.Parameters.AddWithValue("@current_star", 0);

                    // Stats
                    command.Parameters.AddWithValue("@power", AchievementFromDB.Power);
                    command.Parameters.AddWithValue("@health", AchievementFromDB.Health);
                    command.Parameters.AddWithValue("@physical_attack", AchievementFromDB.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", AchievementFromDB.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", AchievementFromDB.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", AchievementFromDB.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", AchievementFromDB.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", AchievementFromDB.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", AchievementFromDB.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", AchievementFromDB.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", AchievementFromDB.MagicalAttack);
                    command.Parameters.AddWithValue("@mental_defense", AchievementFromDB.MagicalDefense);
                    command.Parameters.AddWithValue("@speed", AchievementFromDB.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", AchievementFromDB.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", AchievementFromDB.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", AchievementFromDB.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", AchievementFromDB.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", AchievementFromDB.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", AchievementFromDB.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", AchievementFromDB.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", AchievementFromDB.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", AchievementFromDB.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", AchievementFromDB.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", AchievementFromDB.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", AchievementFromDB.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", AchievementFromDB.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", AchievementFromDB.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", AchievementFromDB.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", AchievementFromDB.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", AchievementFromDB.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", AchievementFromDB.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", AchievementFromDB.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", AchievementFromDB.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", AchievementFromDB.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", AchievementFromDB.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", AchievementFromDB.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", AchievementFromDB.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", AchievementFromDB.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", AchievementFromDB.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", AchievementFromDB.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", AchievementFromDB.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", AchievementFromDB.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", AchievementFromDB.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", AchievementFromDB.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", AchievementFromDB.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", AchievementFromDB.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", AchievementFromDB.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", AchievementFromDB.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", AchievementFromDB.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", AchievementFromDB.SkillResistanceRate);

                    // Percent bonuses (hard-coded)
                    command.Parameters.AddWithValue("@percent_all_health", 5);
                    command.Parameters.AddWithValue("@percent_all_physical_attack", 5);
                    command.Parameters.AddWithValue("@percent_all_physical_defense", 5);
                    command.Parameters.AddWithValue("@percent_all_magical_attack", 5);
                    command.Parameters.AddWithValue("@percent_all_magical_defense", 5);
                    command.Parameters.AddWithValue("@percent_all_chemical_attack", 5);
                    command.Parameters.AddWithValue("@percent_all_chemical_defense", 5);
                    command.Parameters.AddWithValue("@percent_all_atomic_attack", 5);
                    command.Parameters.AddWithValue("@percent_all_atomic_defense", 5);
                    command.Parameters.AddWithValue("@percent_all_mental_attack", 5);
                    command.Parameters.AddWithValue("@percent_all_mental_defense", 5);

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
    public async Task<Achievements> SumPowerAchievementsGalleryAsync()
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
                SUM(power) AS total_power, SUM(health) AS total_health, SUM(mana) AS total_mana, 
                SUM(physical_attack) AS total_physical_attack, SUM(physical_defense) AS total_physical_defense, 
                SUM(magical_attack) AS total_magical_attack, SUM(magical_defense) AS total_magical_defense, 
                SUM(chemical_attack) AS total_chemical_attack, SUM(chemical_defense) AS total_chemical_defense, 
                SUM(atomic_attack) AS total_atomic_attack, SUM(atomic_defense) AS total_atomic_defense, 
                SUM(mental_attack) AS total_mental_attack, SUM(mental_defense) AS total_mental_defense, 
                SUM(speed) AS total_speed, SUM(critical_damage_rate) AS total_critical_damage_rate, 
                SUM(critical_rate) AS total_critical_rate, SUM(critical_resistance_rate) AS total_critical_resistance_rate,
                SUM(ignore_critical_rate) AS total_ignore_critical_rate,
                SUM(penetration_rate) AS total_penetration_rate, SUM(penetration_resistance_rate) AS total_penetration_resistance_rate, 
                SUM(evasion_rate) AS total_evasion_rate, SUM(damage_absorption_rate) AS total_damage_absorption_rate, 
                SUM(ignore_damage_absorption_rate) AS total_ignore_damage_absorption_rate, 
                SUM(absorbed_damage_rate) AS total_absorbed_damage_rate, 
                SUM(vitality_regeneration_rate) AS total_vitality_regeneration_rate, 
                SUM(vitality_regeneration_resistance_rate) AS total_vitality_regeneration_resistance_rate,
                SUM(accuracy_rate) AS total_accuracy_rate, 
                SUM(lifesteal_rate) AS total_lifesteal_rate, SUM(shield_strength) AS total_shield_strength, 
                SUM(tenacity) AS total_tenacity, SUM(resistance_rate) AS total_resistance_rate, 
                SUM(combo_rate) AS total_combo_rate, SUM(ignore_combo_rate) AS total_ignore_combo_rate, 
                SUM(combo_damage_rate) AS total_combo_damage_rate, 
                SUM(combo_resistance_rate) AS total_combo_resistance_rate, 
                SUM(stun_rate) AS total_stun_rate, SUM(ignore_stun_rate) AS total_ignore_stun_rate, 
                SUM(reflection_rate) AS total_reflection_rate, SUM(ignore_reflection_rate) AS total_ignore_reflection_rate, 
                SUM(reflection_damage_rate) AS total_reflection_damage_rate, SUM(reflection_resistance_rate) AS total_reflection_resistance_rate, 
                SUM(mana_regeneration_rate) AS total_mana_regeneration_rate, 
                SUM(damage_to_different_faction_rate) AS total_damage_to_different_faction_rate, 
                SUM(resistance_to_different_faction_rate) AS total_resistance_to_different_faction_rate, 
                SUM(damage_to_same_faction_rate) AS total_damage_to_same_faction_rate, 
                SUM(resistance_to_same_faction_rate) AS total_resistance_to_same_faction_rate, 
                SUM(normal_damage_rate) AS total_normal_damage_rate, 
                SUM(normal_resistance_rate) AS total_normal_resistance_rate, 
                SUM(skill_damage_rate) AS total_skill_damage_rate, 
                SUM(skill_resistance_rate) AS total_skill_resistance_rate, 
                SUM(percent_all_health) AS total_percent_all_health, 
                SUM(percent_all_physical_attack) AS total_percent_all_physical_attack, 
                SUM(percent_all_physical_defense) AS total_percent_all_physical_defense, 
                SUM(percent_all_magical_attack) AS total_percent_all_magical_attack, 
                SUM(percent_all_magical_defense) AS total_percent_all_magical_defense, 
                SUM(percent_all_chemical_attack) AS total_percent_all_chemical_attack, 
                SUM(percent_all_chemical_defense) AS total_percent_all_chemical_defense, 
                SUM(percent_all_atomic_attack) AS total_percent_all_atomic_attack, 
                SUM(percent_all_atomic_defense) AS total_percent_all_atomic_defense, 
                SUM(percent_all_mental_attack) AS total_percent_all_mental_attack, 
                SUM(percent_all_mental_defense) AS total_percent_all_mental_defense
                FROM achievements_gallery 
                WHERE user_id = @user_id AND status = 'available';
            ";

                await using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        sumAchievements.Power = reader["total_power"] as double? ?? 0;
                        sumAchievements.Health = reader["total_health"] as double? ?? 0;
                        sumAchievements.Mana = reader["total_mana"] as float? ?? 0f;

                        sumAchievements.PhysicalAttack = reader["total_physical_attack"] as double? ?? 0;
                        sumAchievements.PhysicalDefense = reader["total_physical_defense"] as double? ?? 0;
                        sumAchievements.MagicalAttack = reader["total_magical_attack"] as double? ?? 0;
                        sumAchievements.MagicalDefense = reader["total_magical_defense"] as double? ?? 0;
                        sumAchievements.ChemicalAttack = reader["total_chemical_attack"] as double? ?? 0;
                        sumAchievements.ChemicalDefense = reader["total_chemical_defense"] as double? ?? 0;
                        sumAchievements.AtomicAttack = reader["total_atomic_attack"] as double? ?? 0;
                        sumAchievements.AtomicDefense = reader["total_atomic_defense"] as double? ?? 0;
                        sumAchievements.MentalAttack = reader["total_mental_attack"] as double? ?? 0;
                        sumAchievements.MentalDefense = reader["total_mental_defense"] as double? ?? 0;

                        sumAchievements.Speed = reader["total_speed"] as double? ?? 0;
                        sumAchievements.CriticalDamageRate = reader["total_critical_damage_rate"] as double? ?? 0;
                        sumAchievements.CriticalRate = reader["total_critical_rate"] as double? ?? 0;
                        sumAchievements.CriticalResistanceRate = reader["total_critical_resistance_rate"] as double? ?? 0;

                        sumAchievements.IgnoreCriticalRate = reader["total_ignore_critical_rate"] as double? ?? 0;
                        sumAchievements.PenetrationRate = reader["total_penetration_rate"] as double? ?? 0;
                        sumAchievements.PenetrationResistanceRate = reader["total_penetration_resistance_rate"] as double? ?? 0;

                        sumAchievements.EvasionRate = reader["total_evasion_rate"] as double? ?? 0;
                        sumAchievements.DamageAbsorptionRate = reader["total_damage_absorption_rate"] as double? ?? 0;
                        sumAchievements.IgnoreDamageAbsorptionRate = reader["total_ignore_damage_absorption_rate"] as double? ?? 0;
                        sumAchievements.AbsorbedDamageRate = reader["total_absorbed_damage_rate"] as double? ?? 0;

                        sumAchievements.VitalityRegenerationRate = reader["total_vitality_regeneration_rate"] as double? ?? 0;
                        sumAchievements.VitalityRegenerationResistanceRate = reader["total_vitality_regeneration_resistance_rate"] as double? ?? 0;

                        sumAchievements.AccuracyRate = reader["total_accuracy_rate"] as double? ?? 0;
                        sumAchievements.LifestealRate = reader["total_lifesteal_rate"] as double? ?? 0;
                        sumAchievements.ShieldStrength = reader["total_shield_strength"] as double? ?? 0;

                        sumAchievements.Tenacity = reader["total_tenacity"] as double? ?? 0;
                        sumAchievements.ResistanceRate = reader["total_resistance_rate"] as double? ?? 0;

                        sumAchievements.ComboRate = reader["total_combo_rate"] as double? ?? 0;
                        sumAchievements.IgnoreComboRate = reader["total_ignore_combo_rate"] as double? ?? 0;
                        sumAchievements.ComboDamageRate = reader["total_combo_damage_rate"] as double? ?? 0;
                        sumAchievements.ComboResistanceRate = reader["total_combo_resistance_rate"] as double? ?? 0;

                        sumAchievements.StunRate = reader["total_stun_rate"] as double? ?? 0;
                        sumAchievements.IgnoreStunRate = reader["total_ignore_stun_rate"] as double? ?? 0;

                        sumAchievements.ReflectionRate = reader["total_reflection_rate"] as double? ?? 0;
                        sumAchievements.IgnoreReflectionRate = reader["total_ignore_reflection_rate"] as double? ?? 0;
                        sumAchievements.ReflectionDamageRate = reader["total_reflection_damage_rate"] as double? ?? 0;
                        sumAchievements.ReflectionResistanceRate = reader["total_reflection_resistance_rate"] as double? ?? 0;

                        sumAchievements.ManaRegenerationRate = reader["total_mana_regeneration_rate"] as double? ?? 0;

                        sumAchievements.DamageToDifferentFactionRate = reader["total_damage_to_different_faction_rate"] as double? ?? 0;
                        sumAchievements.ResistanceToDifferentFactionRate = reader["total_resistance_to_different_faction_rate"] as double? ?? 0;

                        sumAchievements.DamageToSameFactionRate = reader["total_damage_to_same_faction_rate"] as double? ?? 0;
                        sumAchievements.ResistanceToSameFactionRate = reader["total_resistance_to_same_faction_rate"] as double? ?? 0;

                        sumAchievements.NormalDamageRate = reader["total_normal_damage_rate"] as double? ?? 0;
                        sumAchievements.NormalResistanceRate = reader["total_normal_resistance_rate"] as double? ?? 0;

                        sumAchievements.SkillDamageRate = reader["total_skill_damage_rate"] as double? ?? 0;
                        sumAchievements.SkillResistanceRate = reader["total_skill_resistance_rate"] as double? ?? 0;

                        sumAchievements.PercentAllHealth = reader["total_percent_all_health"] as double? ?? 0;
                        sumAchievements.PercentAllPhysicalAttack = reader["total_percent_all_physical_attack"] as double? ?? 0;
                        sumAchievements.PercentAllPhysicalDefense = reader["total_percent_all_physical_defense"] as double? ?? 0;
                        sumAchievements.PercentAllMagicalAttack = reader["total_percent_all_magical_attack"] as double? ?? 0;
                        sumAchievements.PercentAllMagicalDefense = reader["total_percent_all_magical_defense"] as double? ?? 0;
                        sumAchievements.PercentAllChemicalAttack = reader["total_percent_all_chemical_attack"] as double? ?? 0;
                        sumAchievements.PercentAllChemicalDefense = reader["total_percent_all_chemical_defense"] as double? ?? 0;
                        sumAchievements.PercentAllAtomicAttack = reader["total_percent_all_atomic_attack"] as double? ?? 0;
                        sumAchievements.PercentAllAtomicDefense = reader["total_percent_all_atomic_defense"] as double? ?? 0;
                        sumAchievements.PercentAllMentalAttack = reader["total_percent_all_mental_attack"] as double? ?? 0;
                        sumAchievements.PercentAllMentalDefense = reader["total_percent_all_mental_defense"] as double? ?? 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("MySQL Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return sumAchievements;
    }
}