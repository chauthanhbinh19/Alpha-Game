using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class ArchitecturesGalleryRepository : IArchitecturesGalleryRepository
{
    public async Task<List<Architectures>> GetArchitecturesCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Architectures> architectures = new List<Architectures>();
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
                           WHEN cg.architecture_id IS NULL THEN 'block' 
                           WHEN cg.status = 'pending' THEN 'pending' 
                           WHEN cg.status = 'available' THEN 'available' 
                       END AS status 
                FROM Architectures c 
                LEFT JOIN architectures_gallery cg 
                       ON c.id = cg.architecture_id AND cg.user_id = @userId 
                WHERE 1=1";
                
                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    query += " AND rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND name LIKE CONCAT('%', @search, '%')";
                }

                query += @"
                ORDER BY 
                    c.name REGEXP '[0-9]+$',
                    CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED),
                    c.name
                LIMIT @limit OFFSET @offset";

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

                    await using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Architectures Architecture = new Architectures
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Quality = reader.GetDoubleSafe("quality"),
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
                                PercentAllHealth = reader.GetDoubleSafe("percent_all_health"),
                                PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack"),
                                PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense"),
                                PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack"),
                                PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense"),
                                PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack"),
                                PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense"),
                                PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack"),
                                PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense"),
                                PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack"),
                                PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense"),
                                Description = reader.GetStringSafe("description"),
                                Status = reader.GetStringSafe("status"),
                            };

                            architectures.Add(Architecture);
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

        return architectures;
    }
    public async Task<int> GetArchitecturesCountAsync(string search, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT COUNT(*) FROM Architectures WHERE 1=1";

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    query += " AND rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND name LIKE CONCAT('%', @search, '%')";
                }

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
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
    public async Task InsertArchitectureGalleryAsync(string Id, Architectures architectureFromDB)
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
                FROM Architectures_gallery 
                WHERE user_id = @user_id AND Architecture_id = @Architecture_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCommand.Parameters.AddWithValue("@Architecture_id", Id);

                    int recordCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu chưa có → INSERT
                    if (recordCount == 0)
                    {
                        string query = @"
                    INSERT INTO Architectures_gallery (
                        user_id, Architecture_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
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
                        @user_id, @Architecture_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
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

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            command.Parameters.AddWithValue("@Architecture_id", Id);
                            command.Parameters.AddWithValue("@status", "pending");
                            command.Parameters.AddWithValue("@current_star", 0);
                            command.Parameters.AddWithValue("@temp_star", 0);

                            command.Parameters.AddWithValue("@power", architectureFromDB.Power);
                            command.Parameters.AddWithValue("@health", architectureFromDB.Health);
                            command.Parameters.AddWithValue("@physical_attack", architectureFromDB.PhysicalAttack);
                            command.Parameters.AddWithValue("@physical_defense", architectureFromDB.PhysicalDefense);
                            command.Parameters.AddWithValue("@magical_attack", architectureFromDB.MagicalAttack);
                            command.Parameters.AddWithValue("@magical_defense", architectureFromDB.MagicalDefense);
                            command.Parameters.AddWithValue("@chemical_attack", architectureFromDB.ChemicalAttack);
                            command.Parameters.AddWithValue("@chemical_defense", architectureFromDB.ChemicalDefense);
                            command.Parameters.AddWithValue("@atomic_attack", architectureFromDB.AtomicAttack);
                            command.Parameters.AddWithValue("@atomic_defense", architectureFromDB.AtomicDefense);

                            command.Parameters.AddWithValue("@mental_attack", architectureFromDB.MentalAttack);
                            command.Parameters.AddWithValue("@mental_defense", architectureFromDB.MentalDefense);

                            command.Parameters.AddWithValue("@speed", architectureFromDB.Speed);
                            command.Parameters.AddWithValue("@critical_damage_rate", architectureFromDB.CriticalDamageRate);
                            command.Parameters.AddWithValue("@critical_rate", architectureFromDB.CriticalRate);
                            command.Parameters.AddWithValue("@critical_resistance_rate", architectureFromDB.CriticalResistanceRate);
                            command.Parameters.AddWithValue("@ignore_critical_rate", architectureFromDB.IgnoreCriticalRate);
                            command.Parameters.AddWithValue("@penetration_rate", architectureFromDB.PenetrationRate);
                            command.Parameters.AddWithValue("@penetration_resistance_rate", architectureFromDB.PenetrationResistanceRate);
                            command.Parameters.AddWithValue("@evasion_rate", architectureFromDB.EvasionRate);
                            command.Parameters.AddWithValue("@damage_absorption_rate", architectureFromDB.DamageAbsorptionRate);
                            command.Parameters.AddWithValue("@ignore_damage_absorption_rate", architectureFromDB.IgnoreDamageAbsorptionRate);
                            command.Parameters.AddWithValue("@absorbed_damage_rate", architectureFromDB.AbsorbedDamageRate);

                            command.Parameters.AddWithValue("@vitality_regeneration_rate", architectureFromDB.VitalityRegenerationRate);
                            command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", architectureFromDB.VitalityRegenerationResistanceRate);

                            command.Parameters.AddWithValue("@accuracy_rate", architectureFromDB.AccuracyRate);
                            command.Parameters.AddWithValue("@lifesteal_rate", architectureFromDB.LifestealRate);
                            command.Parameters.AddWithValue("@shield_strength", architectureFromDB.ShieldStrength);
                            command.Parameters.AddWithValue("@tenacity", architectureFromDB.Tenacity);
                            command.Parameters.AddWithValue("@resistance_rate", architectureFromDB.ResistanceRate);
                            command.Parameters.AddWithValue("@combo_rate", architectureFromDB.ComboRate);
                            command.Parameters.AddWithValue("@ignore_combo_rate", architectureFromDB.IgnoreComboRate);
                            command.Parameters.AddWithValue("@combo_damage_rate", architectureFromDB.ComboDamageRate);
                            command.Parameters.AddWithValue("@combo_resistance_rate", architectureFromDB.ComboResistanceRate);

                            command.Parameters.AddWithValue("@stun_rate", architectureFromDB.StunRate);
                            command.Parameters.AddWithValue("@ignore_stun_rate", architectureFromDB.IgnoreStunRate);
                            command.Parameters.AddWithValue("@reflection_rate", architectureFromDB.ReflectionRate);
                            command.Parameters.AddWithValue("@ignore_reflection_rate", architectureFromDB.IgnoreReflectionRate);
                            command.Parameters.AddWithValue("@reflection_damage_rate", architectureFromDB.ReflectionDamageRate);
                            command.Parameters.AddWithValue("@reflection_resistance_rate", architectureFromDB.ReflectionResistanceRate);

                            command.Parameters.AddWithValue("@mana", architectureFromDB.Mana);
                            command.Parameters.AddWithValue("@mana_regeneration_rate", architectureFromDB.ManaRegenerationRate);

                            command.Parameters.AddWithValue("@damage_to_different_faction_rate", architectureFromDB.DamageToDifferentFactionRate);
                            command.Parameters.AddWithValue("@resistance_to_different_faction_rate", architectureFromDB.ResistanceToDifferentFactionRate);
                            command.Parameters.AddWithValue("@damage_to_same_faction_rate", architectureFromDB.DamageToSameFactionRate);
                            command.Parameters.AddWithValue("@resistance_to_same_faction_rate", architectureFromDB.ResistanceToSameFactionRate);

                            command.Parameters.AddWithValue("@normal_damage_rate", architectureFromDB.NormalDamageRate);
                            command.Parameters.AddWithValue("@normal_resistance_rate", architectureFromDB.NormalResistanceRate);
                            command.Parameters.AddWithValue("@skill_damage_rate", architectureFromDB.SkillDamageRate);
                            command.Parameters.AddWithValue("@skill_resistance_rate", architectureFromDB.SkillResistanceRate);

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
    public async Task UpdateStatusArchitectureGalleryAsync(string Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"UPDATE Architectures_gallery 
                             SET status=@status 
                             WHERE user_id=@user_id AND Architecture_id=@Architecture_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@Architecture_id", Id);
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
    public async Task UpdateStarArchitectureGalleryAsync(string id, double star)
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
                    FROM Architectures_gallery 
                    WHERE user_id = @user_id AND Architecture_id = @Architecture_id;
                ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCommand.Parameters.AddWithValue("@Architecture_id", id);

                    await using (var reader = await checkCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            double tempStar = reader.GetDoubleSafe("temp_star");

                            // Nếu star mới cao hơn star tạm, cập nhật
                            if (tempStar < star)
                            {
                                reader.Close(); // đóng trước khi chạy lệnh khác

                                string updateQuery = @"
                                UPDATE Architectures_gallery 
                                SET temp_star = @temp_star 
                                WHERE user_id = @user_id AND Architecture_id = @Architecture_id;
                            ";

                                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                                    updateCommand.Parameters.AddWithValue("@Architecture_id", id);
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
    public async Task UpdateArchitectureGalleryPowerAsync(string id, Architectures architectureFromDB)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"UPDATE Architectures_gallery
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
                AND Architecture_id = @Architecture_id;
            ";

                MySqlCommand command = new MySqlCommand(query, connection);

                // IDs
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@Architecture_id", id);

                // Base flags
                command.Parameters.AddWithValue("@status", "pending");
                command.Parameters.AddWithValue("@current_star", 0);

                // Stats
                command.Parameters.AddWithValue("@power", architectureFromDB.Power);
                command.Parameters.AddWithValue("@health", architectureFromDB.Health);
                command.Parameters.AddWithValue("@physical_attack", architectureFromDB.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", architectureFromDB.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", architectureFromDB.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", architectureFromDB.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", architectureFromDB.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", architectureFromDB.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", architectureFromDB.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", architectureFromDB.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", architectureFromDB.MagicalAttack);
                command.Parameters.AddWithValue("@mental_defense", architectureFromDB.MagicalDefense);
                command.Parameters.AddWithValue("@speed", architectureFromDB.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", architectureFromDB.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", architectureFromDB.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", architectureFromDB.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", architectureFromDB.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", architectureFromDB.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", architectureFromDB.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", architectureFromDB.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", architectureFromDB.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", architectureFromDB.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", architectureFromDB.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", architectureFromDB.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", architectureFromDB.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", architectureFromDB.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", architectureFromDB.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", architectureFromDB.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", architectureFromDB.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", architectureFromDB.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", architectureFromDB.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", architectureFromDB.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", architectureFromDB.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", architectureFromDB.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", architectureFromDB.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", architectureFromDB.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", architectureFromDB.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", architectureFromDB.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", architectureFromDB.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", architectureFromDB.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", architectureFromDB.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", architectureFromDB.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", architectureFromDB.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", architectureFromDB.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", architectureFromDB.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", architectureFromDB.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", architectureFromDB.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", architectureFromDB.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", architectureFromDB.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", architectureFromDB.SkillResistanceRate);

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
    public async Task<Architectures> SumPowerArchitecturesGalleryAsync()
    {
        Architectures sumArchitectures = new Architectures();
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
                FROM Architectures_gallery 
                WHERE user_id = @user_id AND status = 'available';
            ";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        sumArchitectures.Power = reader["total_power"] as double? ?? 0;
                        sumArchitectures.Health = reader["total_health"] as double? ?? 0;
                        sumArchitectures.Mana = reader["total_mana"] as double? ?? 0f;

                        sumArchitectures.PhysicalAttack = reader["total_physical_attack"] as double? ?? 0;
                        sumArchitectures.PhysicalDefense = reader["total_physical_defense"] as double? ?? 0;
                        sumArchitectures.MagicalAttack = reader["total_magical_attack"] as double? ?? 0;
                        sumArchitectures.MagicalDefense = reader["total_magical_defense"] as double? ?? 0;
                        sumArchitectures.ChemicalAttack = reader["total_chemical_attack"] as double? ?? 0;
                        sumArchitectures.ChemicalDefense = reader["total_chemical_defense"] as double? ?? 0;
                        sumArchitectures.AtomicAttack = reader["total_atomic_attack"] as double? ?? 0;
                        sumArchitectures.AtomicDefense = reader["total_atomic_defense"] as double? ?? 0;
                        sumArchitectures.MentalAttack = reader["total_mental_attack"] as double? ?? 0;
                        sumArchitectures.MentalDefense = reader["total_mental_defense"] as double? ?? 0;

                        sumArchitectures.Speed = reader["total_speed"] as double? ?? 0;
                        sumArchitectures.CriticalDamageRate = reader["total_critical_damage_rate"] as double? ?? 0;
                        sumArchitectures.CriticalRate = reader["total_critical_rate"] as double? ?? 0;
                        sumArchitectures.CriticalResistanceRate = reader["total_critical_resistance_rate"] as double? ?? 0;

                        sumArchitectures.IgnoreCriticalRate = reader["total_ignore_critical_rate"] as double? ?? 0;
                        sumArchitectures.PenetrationRate = reader["total_penetration_rate"] as double? ?? 0;
                        sumArchitectures.PenetrationResistanceRate = reader["total_penetration_resistance_rate"] as double? ?? 0;

                        sumArchitectures.EvasionRate = reader["total_evasion_rate"] as double? ?? 0;
                        sumArchitectures.DamageAbsorptionRate = reader["total_damage_absorption_rate"] as double? ?? 0;
                        sumArchitectures.IgnoreDamageAbsorptionRate = reader["total_ignore_damage_absorption_rate"] as double? ?? 0;
                        sumArchitectures.AbsorbedDamageRate = reader["total_absorbed_damage_rate"] as double? ?? 0;

                        sumArchitectures.VitalityRegenerationRate = reader["total_vitality_regeneration_rate"] as double? ?? 0;
                        sumArchitectures.VitalityRegenerationResistanceRate = reader["total_vitality_regeneration_resistance_rate"] as double? ?? 0;

                        sumArchitectures.AccuracyRate = reader["total_accuracy_rate"] as double? ?? 0;
                        sumArchitectures.LifestealRate = reader["total_lifesteal_rate"] as double? ?? 0;
                        sumArchitectures.ShieldStrength = reader["total_shield_strength"] as double? ?? 0;

                        sumArchitectures.Tenacity = reader["total_tenacity"] as double? ?? 0;
                        sumArchitectures.ResistanceRate = reader["total_resistance_rate"] as double? ?? 0;

                        sumArchitectures.ComboRate = reader["total_combo_rate"] as double? ?? 0;
                        sumArchitectures.IgnoreComboRate = reader["total_ignore_combo_rate"] as double? ?? 0;
                        sumArchitectures.ComboDamageRate = reader["total_combo_damage_rate"] as double? ?? 0;
                        sumArchitectures.ComboResistanceRate = reader["total_combo_resistance_rate"] as double? ?? 0;

                        sumArchitectures.StunRate = reader["total_stun_rate"] as double? ?? 0;
                        sumArchitectures.IgnoreStunRate = reader["total_ignore_stun_rate"] as double? ?? 0;

                        sumArchitectures.ReflectionRate = reader["total_reflection_rate"] as double? ?? 0;
                        sumArchitectures.IgnoreReflectionRate = reader["total_ignore_reflection_rate"] as double? ?? 0;
                        sumArchitectures.ReflectionDamageRate = reader["total_reflection_damage_rate"] as double? ?? 0;
                        sumArchitectures.ReflectionResistanceRate = reader["total_reflection_resistance_rate"] as double? ?? 0;

                        sumArchitectures.ManaRegenerationRate = reader["total_mana_regeneration_rate"] as double? ?? 0;

                        sumArchitectures.DamageToDifferentFactionRate = reader["total_damage_to_different_faction_rate"] as double? ?? 0;
                        sumArchitectures.ResistanceToDifferentFactionRate = reader["total_resistance_to_different_faction_rate"] as double? ?? 0;

                        sumArchitectures.DamageToSameFactionRate = reader["total_damage_to_same_faction_rate"] as double? ?? 0;
                        sumArchitectures.ResistanceToSameFactionRate = reader["total_resistance_to_same_faction_rate"] as double? ?? 0;

                        sumArchitectures.NormalDamageRate = reader["total_normal_damage_rate"] as double? ?? 0;
                        sumArchitectures.NormalResistanceRate = reader["total_normal_resistance_rate"] as double? ?? 0;

                        sumArchitectures.SkillDamageRate = reader["total_skill_damage_rate"] as double? ?? 0;
                        sumArchitectures.SkillResistanceRate = reader["total_skill_resistance_rate"] as double? ?? 0;

                        sumArchitectures.PercentAllHealth = reader["total_percent_all_health"] as double? ?? 0;
                        sumArchitectures.PercentAllPhysicalAttack = reader["total_percent_all_physical_attack"] as double? ?? 0;
                        sumArchitectures.PercentAllPhysicalDefense = reader["total_percent_all_physical_defense"] as double? ?? 0;
                        sumArchitectures.PercentAllMagicalAttack = reader["total_percent_all_magical_attack"] as double? ?? 0;
                        sumArchitectures.PercentAllMagicalDefense = reader["total_percent_all_magical_defense"] as double? ?? 0;
                        sumArchitectures.PercentAllChemicalAttack = reader["total_percent_all_chemical_attack"] as double? ?? 0;
                        sumArchitectures.PercentAllChemicalDefense = reader["total_percent_all_chemical_defense"] as double? ?? 0;
                        sumArchitectures.PercentAllAtomicAttack = reader["total_percent_all_atomic_attack"] as double? ?? 0;
                        sumArchitectures.PercentAllAtomicDefense = reader["total_percent_all_atomic_defense"] as double? ?? 0;
                        sumArchitectures.PercentAllMentalAttack = reader["total_percent_all_mental_attack"] as double? ?? 0;
                        sumArchitectures.PercentAllMentalDefense = reader["total_percent_all_mental_defense"] as double? ?? 0;
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

        return sumArchitectures;
    }
}