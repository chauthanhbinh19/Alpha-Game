using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class SkillsGalleryRepository : ISkillsGalleryRepository
{
    public async Task<List<Skills>> GetSkillsCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Skills> skills = new List<Skills>();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT m.*, mg.current_star, mg.temp_star,
                    CASE 
                        WHEN mg.skill_id IS NULL THEN 'block'
                        WHEN mg.status = 'pending' THEN 'pending'
                        WHEN mg.status = 'available' THEN 'available'
                    END AS status 
                FROM Skills m 
                LEFT JOIN skills_gallery mg 
                    ON m.id = mg.skill_id AND mg.user_id = @userId 
                WHERE 1=1";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND m.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND m.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND m.name LIKE CONCAT('%', @search, '%')";
                }

                selectSQL += " ORDER BY m.name";
                selectSQL += " LIMIT @limit OFFSET @offset";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
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
                    selectCommand.Parameters.AddWithValue("@userId", user_id);
                    selectCommand.Parameters.AddWithValue("@limit", pageSize);
                    selectCommand.Parameters.AddWithValue("@offset", offset);

                    await using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Skills skill = new Skills
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Quality = reader.GetDoubleSafe("quality"),
                                CurrentStar = reader.IsDBNull(reader.GetOrdinal("current_star")) ? 0 : reader.GetIntSafe("current_star"),
                                TempStar = reader.IsDBNull(reader.GetOrdinal("temp_star")) ? 0 : reader.GetIntSafe("temp_star"),
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

                                // PercentAllHealth = reader.GetDoubleSafe("percent_all_health"),
                                // PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack"),
                                // PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense"),
                                // PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack"),
                                // PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense"),
                                // PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack"),
                                // PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense"),
                                // PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack"),
                                // PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense"),
                                // PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack"),
                                // PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense"),

                                Description = reader.GetStringSafe("description"),
                                Status = reader.GetStringSafe("status"),
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

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"SELECT COUNT(*) FROM Skills
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

                MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
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
    public async Task InsertSkillGalleryAsync(string Id, Skills skillFromDB)
    {
        int percent = QualityEvaluatorHelper.CheckQuality(skillFromDB.Type);
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra bản ghi đã tồn tại
                string checkSQL = @"
                SELECT COUNT(*) 
                FROM skills_gallery 
                WHERE user_id = @user_id AND skill_id = @skill_id;
                ";

                MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@skill_id", Id);

                int recordCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                // Nếu chưa có thì insert
                if (recordCount == 0)
                {
                    string insertSQL = @"
                INSERT INTO skills_gallery (
                    user_id, skill_id, status, current_star, temp_star, power, health, 
                    physical_attack, physical_defense, magical_attack, magical_defense, 
                    chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                    mental_attack, mental_defense, speed, critical_damage_rate, critical_rate,
                    critical_resistance_rate, ignore_critical_rate, penetration_rate, 
                    penetration_resistance_rate, evasion_rate, damage_absorption_rate, 
                    ignore_damage_absorption_rate, absorbed_damage_rate, vitality_regeneration_rate, 
                    vitality_regeneration_resistance_rate, accuracy_rate, lifesteal_rate, 
                    shield_strength, tenacity, resistance_rate, combo_rate, ignore_combo_rate, 
                    combo_damage_rate, combo_resistance_rate, stun_rate, ignore_stun_rate, 
                    reflection_rate, ignore_reflection_rate, reflection_damage_rate, 
                    reflection_resistance_rate, mana, mana_regeneration_rate, 
                    damage_to_different_faction_rate, resistance_to_different_faction_rate, 
                    damage_to_same_faction_rate, resistance_to_same_faction_rate, 
                    normal_damage_rate, normal_resistance_rate, skill_damage_rate, 
                    skill_resistance_rate, percent_all_health, percent_all_physical_attack, 
                    percent_all_physical_defense, percent_all_magical_attack, 
                    percent_all_magical_defense, percent_all_chemical_attack, 
                    percent_all_chemical_defense, percent_all_atomic_attack, 
                    percent_all_atomic_defense, percent_all_mental_attack, 
                    percent_all_mental_defense
                )
                VALUES (
                    @user_id, @skill_id, @status, @current_star, @temp_star, @power, @health,
                    @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                    @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense,
                    @mental_attack, @mental_defense, @speed, @critical_damage_rate, @critical_rate,
                    @critical_resistance_rate, @ignore_critical_rate, @penetration_rate,
                    @penetration_resistance_rate, @evasion_rate, @damage_absorption_rate,
                    @ignore_damage_absorption_rate, @absorbed_damage_rate, @vitality_regeneration_rate,
                    @vitality_regeneration_resistance_rate, @accuracy_rate, @lifesteal_rate,
                    @shield_strength, @tenacity, @resistance_rate, @combo_rate, @ignore_combo_rate,
                    @combo_damage_rate, @combo_resistance_rate, @stun_rate, @ignore_stun_rate,
                    @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate,
                    @reflection_resistance_rate, @mana, @mana_regeneration_rate,
                    @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                    @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                    @normal_damage_rate, @normal_resistance_rate, @skill_damage_rate,
                    @skill_resistance_rate, @percent_all_health, @percent_all_physical_attack,
                    @percent_all_physical_defense, @percent_all_magical_attack,
                    @percent_all_magical_defense, @percent_all_chemical_attack,
                    @percent_all_chemical_defense, @percent_all_atomic_attack,
                    @percent_all_atomic_defense, @percent_all_mental_attack,
                    @percent_all_mental_defense
                );";

                    MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection);

                    // Thêm param
                    insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    insertCommand.Parameters.AddWithValue("@skill_id", Id);
                    insertCommand.Parameters.AddWithValue("@status", "pending");
                    insertCommand.Parameters.AddWithValue("@current_star", 0);
                    insertCommand.Parameters.AddWithValue("@temp_star", 0);

                    // Thuộc tính
                    insertCommand.Parameters.AddWithValue("@power", skillFromDB.Power);
                    insertCommand.Parameters.AddWithValue("@health", skillFromDB.Health);
                    insertCommand.Parameters.AddWithValue("@physical_attack", skillFromDB.PhysicalAttack);
                    insertCommand.Parameters.AddWithValue("@physical_defense", skillFromDB.PhysicalDefense);
                    insertCommand.Parameters.AddWithValue("@magical_attack", skillFromDB.MagicalAttack);
                    insertCommand.Parameters.AddWithValue("@magical_defense", skillFromDB.MagicalDefense);
                    insertCommand.Parameters.AddWithValue("@chemical_attack", skillFromDB.ChemicalAttack);
                    insertCommand.Parameters.AddWithValue("@chemical_defense", skillFromDB.ChemicalDefense);
                    insertCommand.Parameters.AddWithValue("@atomic_attack", skillFromDB.AtomicAttack);
                    insertCommand.Parameters.AddWithValue("@atomic_defense", skillFromDB.AtomicDefense);
                    insertCommand.Parameters.AddWithValue("@mental_attack", skillFromDB.MentalAttack);
                    insertCommand.Parameters.AddWithValue("@mental_defense", skillFromDB.MentalDefense);
                    insertCommand.Parameters.AddWithValue("@speed", skillFromDB.Speed);
                    insertCommand.Parameters.AddWithValue("@critical_damage_rate", skillFromDB.CriticalDamageRate);
                    insertCommand.Parameters.AddWithValue("@critical_rate", skillFromDB.CriticalRate);
                    insertCommand.Parameters.AddWithValue("@critical_resistance_rate", skillFromDB.CriticalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@ignore_critical_rate", skillFromDB.IgnoreCriticalRate);
                    insertCommand.Parameters.AddWithValue("@penetration_rate", skillFromDB.PenetrationRate);
                    insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", skillFromDB.PenetrationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@evasion_rate", skillFromDB.EvasionRate);
                    insertCommand.Parameters.AddWithValue("@damage_absorption_rate", skillFromDB.DamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", skillFromDB.IgnoreDamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", skillFromDB.AbsorbedDamageRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", skillFromDB.VitalityRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", skillFromDB.VitalityRegenerationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@accuracy_rate", skillFromDB.AccuracyRate);
                    insertCommand.Parameters.AddWithValue("@lifesteal_rate", skillFromDB.LifestealRate);
                    insertCommand.Parameters.AddWithValue("@shield_strength", skillFromDB.ShieldStrength);
                    insertCommand.Parameters.AddWithValue("@tenacity", skillFromDB.Tenacity);
                    insertCommand.Parameters.AddWithValue("@resistance_rate", skillFromDB.ResistanceRate);
                    insertCommand.Parameters.AddWithValue("@combo_rate", skillFromDB.ComboRate);
                    insertCommand.Parameters.AddWithValue("@ignore_combo_rate", skillFromDB.IgnoreComboRate);
                    insertCommand.Parameters.AddWithValue("@combo_damage_rate", skillFromDB.ComboDamageRate);
                    insertCommand.Parameters.AddWithValue("@combo_resistance_rate", skillFromDB.ComboResistanceRate);
                    insertCommand.Parameters.AddWithValue("@stun_rate", skillFromDB.StunRate);
                    insertCommand.Parameters.AddWithValue("@ignore_stun_rate", skillFromDB.IgnoreStunRate);
                    insertCommand.Parameters.AddWithValue("@reflection_rate", skillFromDB.ReflectionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", skillFromDB.IgnoreReflectionRate);
                    insertCommand.Parameters.AddWithValue("@reflection_damage_rate", skillFromDB.ReflectionDamageRate);
                    insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", skillFromDB.ReflectionResistanceRate);
                    insertCommand.Parameters.AddWithValue("@mana", skillFromDB.Mana);
                    insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", skillFromDB.ManaRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", skillFromDB.DamageToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", skillFromDB.ResistanceToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", skillFromDB.DamageToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", skillFromDB.ResistanceToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@normal_damage_rate", skillFromDB.NormalDamageRate);
                    insertCommand.Parameters.AddWithValue("@normal_resistance_rate", skillFromDB.NormalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@skill_damage_rate", skillFromDB.SkillDamageRate);
                    insertCommand.Parameters.AddWithValue("@skill_resistance_rate", skillFromDB.SkillResistanceRate);

                    // % buff theo quality
                    insertCommand.Parameters.AddWithValue("@percent_all_health", percent);
                    insertCommand.Parameters.AddWithValue("@percent_all_physical_attack", percent);
                    insertCommand.Parameters.AddWithValue("@percent_all_physical_defense", percent);
                    insertCommand.Parameters.AddWithValue("@percent_all_magical_attack", percent);
                    insertCommand.Parameters.AddWithValue("@percent_all_magical_defense", percent);
                    insertCommand.Parameters.AddWithValue("@percent_all_chemical_attack", percent);
                    insertCommand.Parameters.AddWithValue("@percent_all_chemical_defense", percent);
                    insertCommand.Parameters.AddWithValue("@percent_all_atomic_attack", percent);
                    insertCommand.Parameters.AddWithValue("@percent_all_atomic_defense", percent);
                    insertCommand.Parameters.AddWithValue("@percent_all_mental_attack", percent);
                    insertCommand.Parameters.AddWithValue("@percent_all_mental_defense", percent);

                    await insertCommand.ExecuteNonQueryAsync();
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
    public async Task UpdateStatusSkillGalleryAsync(string Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = "UPDATE skills_gallery SET status=@status WHERE user_id=@user_id AND skill_id=@skill_id";
                MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
                updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                updateCommand.Parameters.AddWithValue("@skill_id", Id);
                updateCommand.Parameters.AddWithValue("@status", "available");

                await updateCommand.ExecuteNonQueryAsync();
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
    public async Task UpdateStarSkillGalleryAsync(string Id, double star)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra bản ghi đã tồn tại và lấy temp_star hiện tại
                string checkSQL = @"
                SELECT current_star, temp_star
                FROM skills_gallery 
                WHERE user_id = @user_id AND skill_id = @skill_id;
            ";

                MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@skill_id", Id);

                await using (var reader = await checkCommand.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        double tempStar = reader.IsDBNull(reader.GetOrdinal("temp_star")) ? 0 : reader.GetDoubleSafe("temp_star");

                        if (tempStar < star)
                        {
                            reader.Close(); // Đóng reader trước khi thực hiện update

                            string updateSQL = @"
                            UPDATE skills_gallery 
                            SET temp_star = @temp_star 
                            WHERE user_id = @user_id AND skill_id = @skill_id;
                        ";

                            MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
                            updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCommand.Parameters.AddWithValue("@skill_id", Id);
                            updateCommand.Parameters.AddWithValue("@temp_star", star);

                            await updateCommand.ExecuteNonQueryAsync();
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
    public async Task UpdateSkillGalleryPowerAsync(string Id, Skills skillFromDB)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"UPDATE skills_gallery
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
                    percent_all_health = percent_all_health +  @percent_all_health,
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
                AND skill_id = @skill_id;
            ";

                MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
                updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                updateCommand.Parameters.AddWithValue("@skill_id", Id);
                updateCommand.Parameters.AddWithValue("@status", "pending");
                updateCommand.Parameters.AddWithValue("@current_star", 0);
                updateCommand.Parameters.AddWithValue("@power", skillFromDB.Power);
                updateCommand.Parameters.AddWithValue("@health", skillFromDB.Health);
                updateCommand.Parameters.AddWithValue("@physical_attack", skillFromDB.PhysicalAttack);
                updateCommand.Parameters.AddWithValue("@physical_defense", skillFromDB.PhysicalDefense);
                updateCommand.Parameters.AddWithValue("@magical_attack", skillFromDB.MagicalAttack);
                updateCommand.Parameters.AddWithValue("@magical_defense", skillFromDB.MagicalDefense);
                updateCommand.Parameters.AddWithValue("@chemical_attack", skillFromDB.ChemicalAttack);
                updateCommand.Parameters.AddWithValue("@chemical_defense", skillFromDB.ChemicalDefense);
                updateCommand.Parameters.AddWithValue("@atomic_attack", skillFromDB.AtomicAttack);
                updateCommand.Parameters.AddWithValue("@atomic_defense", skillFromDB.AtomicDefense);
                updateCommand.Parameters.AddWithValue("@mental_attack", skillFromDB.MentalAttack);
                updateCommand.Parameters.AddWithValue("@mental_defense", skillFromDB.MentalDefense);
                updateCommand.Parameters.AddWithValue("@speed", skillFromDB.Speed);
                updateCommand.Parameters.AddWithValue("@critical_damage_rate", skillFromDB.CriticalDamageRate);
                updateCommand.Parameters.AddWithValue("@critical_rate", skillFromDB.CriticalRate);
                updateCommand.Parameters.AddWithValue("@critical_resistance_rate", skillFromDB.CriticalResistanceRate);
                updateCommand.Parameters.AddWithValue("@ignore_critical_rate", skillFromDB.IgnoreCriticalRate);
                updateCommand.Parameters.AddWithValue("@penetration_rate", skillFromDB.PenetrationRate);
                updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", skillFromDB.PenetrationResistanceRate);
                updateCommand.Parameters.AddWithValue("@evasion_rate", skillFromDB.EvasionRate);
                updateCommand.Parameters.AddWithValue("@damage_absorption_rate", skillFromDB.DamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", skillFromDB.IgnoreDamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", skillFromDB.AbsorbedDamageRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", skillFromDB.VitalityRegenerationRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", skillFromDB.VitalityRegenerationResistanceRate);
                updateCommand.Parameters.AddWithValue("@accuracy_rate", skillFromDB.AccuracyRate);
                updateCommand.Parameters.AddWithValue("@lifesteal_rate", skillFromDB.LifestealRate);
                updateCommand.Parameters.AddWithValue("@shield_strength", skillFromDB.ShieldStrength);
                updateCommand.Parameters.AddWithValue("@tenacity", skillFromDB.Tenacity);
                updateCommand.Parameters.AddWithValue("@resistance_rate", skillFromDB.ResistanceRate);
                updateCommand.Parameters.AddWithValue("@combo_rate", skillFromDB.ComboRate);
                updateCommand.Parameters.AddWithValue("@ignore_combo_rate", skillFromDB.IgnoreComboRate);
                updateCommand.Parameters.AddWithValue("@combo_damage_rate", skillFromDB.ComboDamageRate);
                updateCommand.Parameters.AddWithValue("@combo_resistance_rate", skillFromDB.ComboResistanceRate);
                updateCommand.Parameters.AddWithValue("@stun_rate", skillFromDB.StunRate);
                updateCommand.Parameters.AddWithValue("@ignore_stun_rate", skillFromDB.IgnoreStunRate);
                updateCommand.Parameters.AddWithValue("@reflection_rate", skillFromDB.ReflectionRate);
                updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", skillFromDB.IgnoreReflectionRate);
                updateCommand.Parameters.AddWithValue("@reflection_damage_rate", skillFromDB.ReflectionDamageRate);
                updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", skillFromDB.ReflectionResistanceRate);
                updateCommand.Parameters.AddWithValue("@mana", skillFromDB.Mana);
                updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", skillFromDB.ManaRegenerationRate);
                updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", skillFromDB.DamageToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", skillFromDB.ResistanceToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", skillFromDB.DamageToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", skillFromDB.ResistanceToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@normal_damage_rate", skillFromDB.NormalDamageRate);
                updateCommand.Parameters.AddWithValue("@normal_resistance_rate", skillFromDB.NormalResistanceRate);
                updateCommand.Parameters.AddWithValue("@skill_damage_rate", skillFromDB.SkillDamageRate);
                updateCommand.Parameters.AddWithValue("@skill_resistance_rate", skillFromDB.SkillResistanceRate);
                updateCommand.Parameters.AddWithValue("@percent_all_health", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_physical_attack", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_physical_defense", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_magical_attack", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_magical_defense", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_chemical_attack", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_chemical_defense", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_atomic_attack", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_atomic_defense", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_mental_attack", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_mental_defense", 5);

                await updateCommand.ExecuteNonQueryAsync();
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
    public async Task<Skills> SumPowerSkillsGalleryAsync()
    {
        Skills sumSkills = new Skills();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"SELECT 
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
                SUM(ignore_damage_absorption_rate) AS total_ignore_damage_absorption_rate, SUM(absorbed_damage_rate) AS total_absorbed_damage_rate, 
                SUM(vitality_regeneration_rate) AS total_vitality_regeneration_rate, SUM(vitality_regeneration_resistance_rate) AS total_vitality_regeneration_resistance_rate,
                SUM(accuracy_rate) AS total_accuracy_rate, 
                SUM(lifesteal_rate) AS total_lifesteal_rate, SUM(shield_strength) AS total_shield_strength, 
                SUM(tenacity) AS total_tenacity, SUM(resistance_rate) AS total_resistance_rate, 
                SUM(combo_rate) AS total_combo_rate, SUM(ignore_combo_rate) AS total_ignore_combo_rate, SUM(combo_damage_rate) AS total_combo_damage_rate, 
                SUM(combo_resistance_rate) AS total_combo_resistance_rate, SUM(stun_rate) AS total_stun_rate, SUM(ignore_stun_rate) AS total_ignore_stun_rate, 
                SUM(reflection_rate) AS total_reflection_rate, SUM(ignore_reflection_rate) AS total_ignore_reflection_rate, 
                SUM(reflection_damage_rate) AS total_reflection_damage_rate, SUM(reflection_resistance_rate) AS total_reflection_resistance_rate, 
                SUM(mana_regeneration_rate) AS total_mana_regeneration_rate, 
                SUM(damage_to_different_faction_rate) AS total_damage_to_different_faction_rate, 
                SUM(resistance_to_different_faction_rate) AS total_resistance_to_different_faction_rate, 
                SUM(damage_to_same_faction_rate) AS total_damage_to_same_faction_rate, 
                SUM(resistance_to_same_faction_rate) AS total_resistance_to_same_faction_rate, 
                SUM(normal_damage_rate) AS total_normal_damage_rate, SUM(normal_resistance_rate) AS total_normal_resistance_rate, 
                SUM(skill_damage_rate) AS total_skill_damage_rate, SUM(skill_resistance_rate) AS total_skill_resistance_rate, 
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
            FROM skills_gallery 
            WHERE user_id = @user_id AND status = 'available';";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    await using (MySqlDataReader reader = (MySqlDataReader)await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumSkills.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            sumSkills.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            sumSkills.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            sumSkills.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            sumSkills.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            sumSkills.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            sumSkills.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            sumSkills.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            sumSkills.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            sumSkills.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            sumSkills.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            sumSkills.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            sumSkills.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            sumSkills.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            sumSkills.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            sumSkills.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            sumSkills.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            sumSkills.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            sumSkills.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            sumSkills.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            sumSkills.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            sumSkills.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            sumSkills.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            sumSkills.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            sumSkills.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            sumSkills.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            sumSkills.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            sumSkills.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            sumSkills.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            sumSkills.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            sumSkills.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            sumSkills.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            sumSkills.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            sumSkills.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            sumSkills.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            sumSkills.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            sumSkills.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            sumSkills.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            sumSkills.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            sumSkills.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            sumSkills.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDoubleSafe("total_mana");
                            sumSkills.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            sumSkills.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            sumSkills.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            sumSkills.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            sumSkills.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            sumSkills.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            sumSkills.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            sumSkills.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            sumSkills.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
                            sumSkills.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDoubleSafe("total_percent_all_health");
                            sumSkills.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_physical_attack");
                            sumSkills.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_physical_defense");
                            sumSkills.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_magical_attack");
                            sumSkills.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_magical_defense");
                            sumSkills.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_chemical_attack");
                            sumSkills.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_chemical_defense");
                            sumSkills.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_atomic_attack");
                            sumSkills.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_atomic_defense");
                            sumSkills.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_mental_attack");
                            sumSkills.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_mental_defense");
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

        return sumSkills;
    }
}