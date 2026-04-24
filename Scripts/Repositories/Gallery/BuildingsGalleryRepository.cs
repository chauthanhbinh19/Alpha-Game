using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class BuildingsGalleryRepository : IBuildingsGalleryRepository
{
    public async Task<List<Buildings>> GetBuildingsCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Buildings> Buildings = new List<Buildings>();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT m.*, mg.current_star, mg.temp_star,
                    CASE 
                        WHEN mg.building_id IS NULL THEN 'block'
                        WHEN mg.status = 'pending' THEN 'pending'
                        WHEN mg.status = 'available' THEN 'available'
                    END AS status 
                FROM Buildings m 
                LEFT JOIN buildings_gallery mg 
                    ON m.id = mg.building_id AND mg.user_id = @userId 
                WHERE 1=1";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    query += " AND m.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    query += " AND m.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND m.name LIKE CONCAT('%', @search, '%')";
                }

                query += " ORDER BY m.name";
                query += " LIMIT @limit OFFSET @offset";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    if (!string.IsNullOrEmpty(type) && type != "All")
                    {
                        command.Parameters.AddWithValue("@type", type);
                    }

                    if (!string.IsNullOrEmpty(rare) && rare != "All")
                    {
                        command.Parameters.AddWithValue("@rare", rare);
                    }

                    if (!string.IsNullOrEmpty(search))
                    {
                        command.Parameters.AddWithValue("@search", search);
                    }
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Buildings Building = new Buildings
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

                            Buildings.Add(Building);
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
        return Buildings;
    }
    public async Task<int> GetBuildingsCountAsync(string search, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT COUNT(*) FROM Buildings 
                WHERE 1=1";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    query += " AND type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    query += " AND rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND name LIKE CONCAT('%', @search, '%')";
                }

                MySqlCommand command = new MySqlCommand(query, connection);
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    command.Parameters.AddWithValue("@type", type);
                }

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
    public async Task InsertBuildingGalleryAsync(string Id, Buildings buildingFromDB)
    {
        int percent = QualityEvaluatorHelper.CheckQuality(buildingFromDB.Type);
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra bản ghi đã tồn tại
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM buildings_gallery 
                WHERE user_id = @user_id AND building_id = @building_id;
                ";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@building_id", Id);

                int recordCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                // Nếu chưa có thì insert
                if (recordCount == 0)
                {
                    string query = @"
                INSERT INTO buildings_gallery (
                    user_id, building_id, status, current_star, temp_star, power, health, 
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
                    @user_id, @building_id, @status, @current_star, @temp_star, @power, @health,
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

                    MySqlCommand command = new MySqlCommand(query, connection);

                    // Thêm param
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@building_id", Id);
                    command.Parameters.AddWithValue("@status", "pending");
                    command.Parameters.AddWithValue("@current_star", 0);
                    command.Parameters.AddWithValue("@temp_star", 0);

                    // Thuộc tính
                    command.Parameters.AddWithValue("@power", buildingFromDB.Power);
                    command.Parameters.AddWithValue("@health", buildingFromDB.Health);
                    command.Parameters.AddWithValue("@physical_attack", buildingFromDB.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", buildingFromDB.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", buildingFromDB.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", buildingFromDB.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", buildingFromDB.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", buildingFromDB.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", buildingFromDB.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", buildingFromDB.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", buildingFromDB.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", buildingFromDB.MentalDefense);
                    command.Parameters.AddWithValue("@speed", buildingFromDB.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", buildingFromDB.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", buildingFromDB.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", buildingFromDB.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", buildingFromDB.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", buildingFromDB.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", buildingFromDB.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", buildingFromDB.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", buildingFromDB.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", buildingFromDB.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", buildingFromDB.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", buildingFromDB.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", buildingFromDB.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", buildingFromDB.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", buildingFromDB.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", buildingFromDB.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", buildingFromDB.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", buildingFromDB.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", buildingFromDB.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", buildingFromDB.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", buildingFromDB.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", buildingFromDB.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", buildingFromDB.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", buildingFromDB.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", buildingFromDB.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", buildingFromDB.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", buildingFromDB.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", buildingFromDB.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", buildingFromDB.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", buildingFromDB.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", buildingFromDB.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", buildingFromDB.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", buildingFromDB.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", buildingFromDB.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", buildingFromDB.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", buildingFromDB.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", buildingFromDB.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", buildingFromDB.SkillResistanceRate);

                    // % buff theo quality
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
    public async Task UpdateStatusBuildingGalleryAsync(string Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = "UPDATE buildings_gallery SET status=@status WHERE user_id=@user_id AND building_id=@building_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@building_id", Id);
                command.Parameters.AddWithValue("@status", "available");

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
    public async Task UpdateStarBuildingGalleryAsync(string Id, double star)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra bản ghi đã tồn tại và lấy temp_star hiện tại
                string checkQuery = @"
                SELECT current_star, temp_star
                FROM buildings_gallery 
                WHERE user_id = @user_id AND building_id = @building_id;
            ";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@building_id", Id);

                await using (var reader = await checkCommand.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        double tempStar = reader.IsDBNull(reader.GetOrdinal("temp_star")) ? 0 : reader.GetDoubleSafe("temp_star");

                        if (tempStar < star)
                        {
                            reader.Close(); // Đóng reader trước khi thực hiện update

                            string updateQuery = @"
                            UPDATE buildings_gallery 
                            SET temp_star = @temp_star 
                            WHERE user_id = @user_id AND building_id = @building_id;
                        ";

                            MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                            updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCommand.Parameters.AddWithValue("@building_id", Id);
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
    public async Task UpdateBuildingGalleryPowerAsync(string Id, Buildings buildingFromDB)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"UPDATE buildings_gallery
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
                AND building_id = @building_id;
            ";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@building_id", Id);
                command.Parameters.AddWithValue("@status", "pending");
                command.Parameters.AddWithValue("@current_star", 0);
                command.Parameters.AddWithValue("@power", buildingFromDB.Power);
                command.Parameters.AddWithValue("@health", buildingFromDB.Health);
                command.Parameters.AddWithValue("@physical_attack", buildingFromDB.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", buildingFromDB.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", buildingFromDB.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", buildingFromDB.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", buildingFromDB.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", buildingFromDB.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", buildingFromDB.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", buildingFromDB.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", buildingFromDB.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", buildingFromDB.MentalDefense);
                command.Parameters.AddWithValue("@speed", buildingFromDB.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", buildingFromDB.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", buildingFromDB.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", buildingFromDB.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", buildingFromDB.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", buildingFromDB.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", buildingFromDB.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", buildingFromDB.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", buildingFromDB.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", buildingFromDB.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", buildingFromDB.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", buildingFromDB.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", buildingFromDB.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", buildingFromDB.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", buildingFromDB.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", buildingFromDB.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", buildingFromDB.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", buildingFromDB.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", buildingFromDB.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", buildingFromDB.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", buildingFromDB.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", buildingFromDB.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", buildingFromDB.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", buildingFromDB.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", buildingFromDB.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", buildingFromDB.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", buildingFromDB.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", buildingFromDB.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", buildingFromDB.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", buildingFromDB.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", buildingFromDB.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", buildingFromDB.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", buildingFromDB.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", buildingFromDB.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", buildingFromDB.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", buildingFromDB.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", buildingFromDB.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", buildingFromDB.SkillResistanceRate);
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
    public async Task<Buildings> SumPowerBuildingsGalleryAsync()
    {
        Buildings sumBuildings = new Buildings();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT 
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
            FROM buildings_gallery 
            WHERE user_id = @user_id AND status = 'available';";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    await using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumBuildings.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            sumBuildings.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            sumBuildings.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            sumBuildings.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            sumBuildings.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            sumBuildings.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            sumBuildings.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            sumBuildings.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            sumBuildings.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            sumBuildings.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            sumBuildings.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            sumBuildings.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            sumBuildings.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            sumBuildings.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            sumBuildings.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            sumBuildings.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            sumBuildings.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            sumBuildings.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            sumBuildings.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            sumBuildings.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            sumBuildings.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            sumBuildings.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            sumBuildings.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            sumBuildings.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            sumBuildings.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            sumBuildings.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            sumBuildings.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            sumBuildings.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            sumBuildings.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            sumBuildings.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            sumBuildings.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            sumBuildings.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            sumBuildings.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            sumBuildings.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            sumBuildings.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            sumBuildings.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            sumBuildings.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            sumBuildings.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            sumBuildings.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            sumBuildings.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            sumBuildings.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDoubleSafe("total_mana");
                            sumBuildings.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            sumBuildings.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            sumBuildings.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            sumBuildings.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            sumBuildings.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            sumBuildings.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            sumBuildings.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            sumBuildings.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            sumBuildings.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
                            sumBuildings.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDoubleSafe("total_percent_all_health");
                            sumBuildings.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_physical_attack");
                            sumBuildings.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_physical_defense");
                            sumBuildings.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_magical_attack");
                            sumBuildings.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_magical_defense");
                            sumBuildings.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_chemical_attack");
                            sumBuildings.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_chemical_defense");
                            sumBuildings.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_atomic_attack");
                            sumBuildings.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_atomic_defense");
                            sumBuildings.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_mental_attack");
                            sumBuildings.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_mental_defense");
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

        return sumBuildings;
    }
}