using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class ArtworksGalleryRepository : IArtworksGalleryRepository
{
    public async Task<List<Artworks>> GetArtworksCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Artworks> artworks = new List<Artworks>();
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
                        WHEN mg.artwork_id IS NULL THEN 'block'
                        WHEN mg.status = 'pending' THEN 'pending'
                        WHEN mg.status = 'available' THEN 'available'
                    END AS status 
                FROM Artworks m 
                LEFT JOIN artworks_gallery mg 
                    ON m.id = mg.artwork_id AND mg.user_id = @userId 
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
                    selectCommand.Parameters.AddWithValue("@userId", user_id);
                    selectCommand.Parameters.AddWithValue("@limit", pageSize);
                    selectCommand.Parameters.AddWithValue("@offset", offset);

                    using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Artworks Artwork = new Artworks
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

                            artworks.Add(Artwork);
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
        return artworks;
    }
    public async Task<int> GetArtworksCountAsync(string search, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"SELECT COUNT(*) FROM Artworks 
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
    public async Task InsertArtworkGalleryAsync(string Id, Artworks artworkFromDB)
    {
        int percent = QualityEvaluatorHelper.CheckQuality(artworkFromDB.Type);
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra bản ghi đã tồn tại
                string checkSQL = @"
                SELECT COUNT(*) 
                FROM artworks_gallery 
                WHERE user_id = @user_id AND artwork_id = @artwork_id;
                ";

                MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@artwork_id", Id);

                int recordCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                // Nếu chưa có thì insert
                if (recordCount == 0)
                {
                    string insertSQL = @"
                INSERT INTO artworks_gallery (
                    user_id, artwork_id, status, current_star, temp_star, power, health, 
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
                    @user_id, @artwork_id, @status, @current_star, @temp_star, @power, @health,
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
                    insertCommand.Parameters.AddWithValue("@artwork_id", Id);
                    insertCommand.Parameters.AddWithValue("@status", "pending");
                    insertCommand.Parameters.AddWithValue("@current_star", 0);
                    insertCommand.Parameters.AddWithValue("@temp_star", 0);

                    // Thuộc tính
                    insertCommand.Parameters.AddWithValue("@power", artworkFromDB.Power);
                    insertCommand.Parameters.AddWithValue("@health", artworkFromDB.Health);
                    insertCommand.Parameters.AddWithValue("@physical_attack", artworkFromDB.PhysicalAttack);
                    insertCommand.Parameters.AddWithValue("@physical_defense", artworkFromDB.PhysicalDefense);
                    insertCommand.Parameters.AddWithValue("@magical_attack", artworkFromDB.MagicalAttack);
                    insertCommand.Parameters.AddWithValue("@magical_defense", artworkFromDB.MagicalDefense);
                    insertCommand.Parameters.AddWithValue("@chemical_attack", artworkFromDB.ChemicalAttack);
                    insertCommand.Parameters.AddWithValue("@chemical_defense", artworkFromDB.ChemicalDefense);
                    insertCommand.Parameters.AddWithValue("@atomic_attack", artworkFromDB.AtomicAttack);
                    insertCommand.Parameters.AddWithValue("@atomic_defense", artworkFromDB.AtomicDefense);
                    insertCommand.Parameters.AddWithValue("@mental_attack", artworkFromDB.MentalAttack);
                    insertCommand.Parameters.AddWithValue("@mental_defense", artworkFromDB.MentalDefense);
                    insertCommand.Parameters.AddWithValue("@speed", artworkFromDB.Speed);
                    insertCommand.Parameters.AddWithValue("@critical_damage_rate", artworkFromDB.CriticalDamageRate);
                    insertCommand.Parameters.AddWithValue("@critical_rate", artworkFromDB.CriticalRate);
                    insertCommand.Parameters.AddWithValue("@critical_resistance_rate", artworkFromDB.CriticalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@ignore_critical_rate", artworkFromDB.IgnoreCriticalRate);
                    insertCommand.Parameters.AddWithValue("@penetration_rate", artworkFromDB.PenetrationRate);
                    insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", artworkFromDB.PenetrationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@evasion_rate", artworkFromDB.EvasionRate);
                    insertCommand.Parameters.AddWithValue("@damage_absorption_rate", artworkFromDB.DamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", artworkFromDB.IgnoreDamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", artworkFromDB.AbsorbedDamageRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", artworkFromDB.VitalityRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", artworkFromDB.VitalityRegenerationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@accuracy_rate", artworkFromDB.AccuracyRate);
                    insertCommand.Parameters.AddWithValue("@lifesteal_rate", artworkFromDB.LifestealRate);
                    insertCommand.Parameters.AddWithValue("@shield_strength", artworkFromDB.ShieldStrength);
                    insertCommand.Parameters.AddWithValue("@tenacity", artworkFromDB.Tenacity);
                    insertCommand.Parameters.AddWithValue("@resistance_rate", artworkFromDB.ResistanceRate);
                    insertCommand.Parameters.AddWithValue("@combo_rate", artworkFromDB.ComboRate);
                    insertCommand.Parameters.AddWithValue("@ignore_combo_rate", artworkFromDB.IgnoreComboRate);
                    insertCommand.Parameters.AddWithValue("@combo_damage_rate", artworkFromDB.ComboDamageRate);
                    insertCommand.Parameters.AddWithValue("@combo_resistance_rate", artworkFromDB.ComboResistanceRate);
                    insertCommand.Parameters.AddWithValue("@stun_rate", artworkFromDB.StunRate);
                    insertCommand.Parameters.AddWithValue("@ignore_stun_rate", artworkFromDB.IgnoreStunRate);
                    insertCommand.Parameters.AddWithValue("@reflection_rate", artworkFromDB.ReflectionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", artworkFromDB.IgnoreReflectionRate);
                    insertCommand.Parameters.AddWithValue("@reflection_damage_rate", artworkFromDB.ReflectionDamageRate);
                    insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", artworkFromDB.ReflectionResistanceRate);
                    insertCommand.Parameters.AddWithValue("@mana", artworkFromDB.Mana);
                    insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", artworkFromDB.ManaRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", artworkFromDB.DamageToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", artworkFromDB.ResistanceToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", artworkFromDB.DamageToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", artworkFromDB.ResistanceToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@normal_damage_rate", artworkFromDB.NormalDamageRate);
                    insertCommand.Parameters.AddWithValue("@normal_resistance_rate", artworkFromDB.NormalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@skill_damage_rate", artworkFromDB.SkillDamageRate);
                    insertCommand.Parameters.AddWithValue("@skill_resistance_rate", artworkFromDB.SkillResistanceRate);

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
    public async Task UpdateStatusArtworkGalleryAsync(string Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"UPDATE artworks_gallery SET status=@status WHERE user_id=@user_id AND artwork_id=@artwork_id";
                MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
                updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                updateCommand.Parameters.AddWithValue("@artwork_id", Id);
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
    public async Task UpdateStarArtworkGalleryAsync(string Id, double star)
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
                FROM artworks_gallery 
                WHERE user_id = @user_id AND artwork_id = @artwork_id;
            ";

                MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@artwork_id", Id);

                await using (var reader = await checkCommand.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        double tempStar = reader.IsDBNull(reader.GetOrdinal("temp_star")) ? 0 : reader.GetDoubleSafe("temp_star");

                        if (tempStar < star)
                        {
                            reader.Close(); // Đóng reader trước khi thực hiện update

                            string updateSQL = @"
                            UPDATE artworks_gallery 
                            SET temp_star = @temp_star 
                            WHERE user_id = @user_id AND artwork_id = @artwork_id;
                        ";

                            MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
                            updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCommand.Parameters.AddWithValue("@artwork_id", Id);
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
    public async Task UpdateArtworkGalleryPowerAsync(string Id, Artworks artworkFromDB)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"UPDATE artworks_gallery
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
                AND artwork_id = @artwork_id;
            ";

                MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
                updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                updateCommand.Parameters.AddWithValue("@artwork_id", Id);
                updateCommand.Parameters.AddWithValue("@status", "pending");
                updateCommand.Parameters.AddWithValue("@current_star", 0);
                updateCommand.Parameters.AddWithValue("@power", artworkFromDB.Power);
                updateCommand.Parameters.AddWithValue("@health", artworkFromDB.Health);
                updateCommand.Parameters.AddWithValue("@physical_attack", artworkFromDB.PhysicalAttack);
                updateCommand.Parameters.AddWithValue("@physical_defense", artworkFromDB.PhysicalDefense);
                updateCommand.Parameters.AddWithValue("@magical_attack", artworkFromDB.MagicalAttack);
                updateCommand.Parameters.AddWithValue("@magical_defense", artworkFromDB.MagicalDefense);
                updateCommand.Parameters.AddWithValue("@chemical_attack", artworkFromDB.ChemicalAttack);
                updateCommand.Parameters.AddWithValue("@chemical_defense", artworkFromDB.ChemicalDefense);
                updateCommand.Parameters.AddWithValue("@atomic_attack", artworkFromDB.AtomicAttack);
                updateCommand.Parameters.AddWithValue("@atomic_defense", artworkFromDB.AtomicDefense);
                updateCommand.Parameters.AddWithValue("@mental_attack", artworkFromDB.MentalAttack);
                updateCommand.Parameters.AddWithValue("@mental_defense", artworkFromDB.MentalDefense);
                updateCommand.Parameters.AddWithValue("@speed", artworkFromDB.Speed);
                updateCommand.Parameters.AddWithValue("@critical_damage_rate", artworkFromDB.CriticalDamageRate);
                updateCommand.Parameters.AddWithValue("@critical_rate", artworkFromDB.CriticalRate);
                updateCommand.Parameters.AddWithValue("@critical_resistance_rate", artworkFromDB.CriticalResistanceRate);
                updateCommand.Parameters.AddWithValue("@ignore_critical_rate", artworkFromDB.IgnoreCriticalRate);
                updateCommand.Parameters.AddWithValue("@penetration_rate", artworkFromDB.PenetrationRate);
                updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", artworkFromDB.PenetrationResistanceRate);
                updateCommand.Parameters.AddWithValue("@evasion_rate", artworkFromDB.EvasionRate);
                updateCommand.Parameters.AddWithValue("@damage_absorption_rate", artworkFromDB.DamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", artworkFromDB.IgnoreDamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", artworkFromDB.AbsorbedDamageRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", artworkFromDB.VitalityRegenerationRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", artworkFromDB.VitalityRegenerationResistanceRate);
                updateCommand.Parameters.AddWithValue("@accuracy_rate", artworkFromDB.AccuracyRate);
                updateCommand.Parameters.AddWithValue("@lifesteal_rate", artworkFromDB.LifestealRate);
                updateCommand.Parameters.AddWithValue("@shield_strength", artworkFromDB.ShieldStrength);
                updateCommand.Parameters.AddWithValue("@tenacity", artworkFromDB.Tenacity);
                updateCommand.Parameters.AddWithValue("@resistance_rate", artworkFromDB.ResistanceRate);
                updateCommand.Parameters.AddWithValue("@combo_rate", artworkFromDB.ComboRate);
                updateCommand.Parameters.AddWithValue("@ignore_combo_rate", artworkFromDB.IgnoreComboRate);
                updateCommand.Parameters.AddWithValue("@combo_damage_rate", artworkFromDB.ComboDamageRate);
                updateCommand.Parameters.AddWithValue("@combo_resistance_rate", artworkFromDB.ComboResistanceRate);
                updateCommand.Parameters.AddWithValue("@stun_rate", artworkFromDB.StunRate);
                updateCommand.Parameters.AddWithValue("@ignore_stun_rate", artworkFromDB.IgnoreStunRate);
                updateCommand.Parameters.AddWithValue("@reflection_rate", artworkFromDB.ReflectionRate);
                updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", artworkFromDB.IgnoreReflectionRate);
                updateCommand.Parameters.AddWithValue("@reflection_damage_rate", artworkFromDB.ReflectionDamageRate);
                updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", artworkFromDB.ReflectionResistanceRate);
                updateCommand.Parameters.AddWithValue("@mana", artworkFromDB.Mana);
                updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", artworkFromDB.ManaRegenerationRate);
                updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", artworkFromDB.DamageToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", artworkFromDB.ResistanceToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", artworkFromDB.DamageToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", artworkFromDB.ResistanceToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@normal_damage_rate", artworkFromDB.NormalDamageRate);
                updateCommand.Parameters.AddWithValue("@normal_resistance_rate", artworkFromDB.NormalResistanceRate);
                updateCommand.Parameters.AddWithValue("@skill_damage_rate", artworkFromDB.SkillDamageRate);
                updateCommand.Parameters.AddWithValue("@skill_resistance_rate", artworkFromDB.SkillResistanceRate);
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
    public async Task<Artworks> SumPowerArtworksGalleryAsync()
    {
        Artworks sumArtworks = new Artworks();
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
                FROM artworks_gallery 
                WHERE user_id = @user_id AND status = 'available';";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    await using (MySqlDataReader reader = (MySqlDataReader)await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumArtworks.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            sumArtworks.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            sumArtworks.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            sumArtworks.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            sumArtworks.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            sumArtworks.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            sumArtworks.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            sumArtworks.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            sumArtworks.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            sumArtworks.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            sumArtworks.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            sumArtworks.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            sumArtworks.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            sumArtworks.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            sumArtworks.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            sumArtworks.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            sumArtworks.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            sumArtworks.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            sumArtworks.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            sumArtworks.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            sumArtworks.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            sumArtworks.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            sumArtworks.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            sumArtworks.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            sumArtworks.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            sumArtworks.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            sumArtworks.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            sumArtworks.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            sumArtworks.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            sumArtworks.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            sumArtworks.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            sumArtworks.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            sumArtworks.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            sumArtworks.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            sumArtworks.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            sumArtworks.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            sumArtworks.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            sumArtworks.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            sumArtworks.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            sumArtworks.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            sumArtworks.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDoubleSafe("total_mana");
                            sumArtworks.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            sumArtworks.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            sumArtworks.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            sumArtworks.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            sumArtworks.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            sumArtworks.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            sumArtworks.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            sumArtworks.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            sumArtworks.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
                            sumArtworks.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDoubleSafe("total_percent_all_health");
                            sumArtworks.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_physical_attack");
                            sumArtworks.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_physical_defense");
                            sumArtworks.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_magical_attack");
                            sumArtworks.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_magical_defense");
                            sumArtworks.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_chemical_attack");
                            sumArtworks.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_chemical_defense");
                            sumArtworks.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_atomic_attack");
                            sumArtworks.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_atomic_defense");
                            sumArtworks.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_mental_attack");
                            sumArtworks.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_mental_defense");
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

        return sumArtworks;
    }
}