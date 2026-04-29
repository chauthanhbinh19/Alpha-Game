using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class SpiritCardsGalleryRepository : ISpiritCardsGalleryRepository
{
    public async Task<List<SpiritCards>> GetSpiritCardsCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<SpiritCards> spiritCards = new List<SpiritCards>();
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
                        WHEN mg.spirit_card_id IS NULL THEN 'block'
                        WHEN mg.status = 'pending' THEN 'pending'
                        WHEN mg.status = 'available' THEN 'available'
                    END AS status 
                FROM spirit_cards m 
                LEFT JOIN spirit_cards_gallery mg 
                    ON m.id = mg.spirit_card_id AND mg.user_id = @userId 
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
                            SpiritCards spiritCard = new SpiritCards
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

                            spiritCards.Add(spiritCard);
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
        return spiritCards;
    }
    public async Task<int> GetSpiritCardsCountAsync(string search, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"SELECT COUNT(*) FROM spirit_cards 
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
    public async Task InsertSpiritCardGalleryAsync(string Id, SpiritCards spiritCardFromDB)
    {
        int percent = QualityEvaluatorHelper.CheckQuality(spiritCardFromDB.Type);
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra bản ghi đã tồn tại
                string checkSQL = @"
                SELECT COUNT(*) 
                FROM spirit_cards_gallery 
                WHERE user_id = @user_id AND spirit_card_id = @spirit_card_id;
                ";

                MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@spirit_card_id", Id);

                int recordCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                // Nếu chưa có thì insert
                if (recordCount == 0)
                {
                    string insertSQL = @"
                INSERT INTO spirit_cards_gallery (
                    user_id, spirit_card_id, status, current_star, temp_star, power, health, 
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
                    @user_id, @spirit_card_id, @status, @current_star, @temp_star, @power, @health,
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
                    insertCommand.Parameters.AddWithValue("@spirit_card_id", Id);
                    insertCommand.Parameters.AddWithValue("@status", "pending");
                    insertCommand.Parameters.AddWithValue("@current_star", 0);
                    insertCommand.Parameters.AddWithValue("@temp_star", 0);

                    // Thuộc tính
                    insertCommand.Parameters.AddWithValue("@power", spiritCardFromDB.Power);
                    insertCommand.Parameters.AddWithValue("@health", spiritCardFromDB.Health);
                    insertCommand.Parameters.AddWithValue("@physical_attack", spiritCardFromDB.PhysicalAttack);
                    insertCommand.Parameters.AddWithValue("@physical_defense", spiritCardFromDB.PhysicalDefense);
                    insertCommand.Parameters.AddWithValue("@magical_attack", spiritCardFromDB.MagicalAttack);
                    insertCommand.Parameters.AddWithValue("@magical_defense", spiritCardFromDB.MagicalDefense);
                    insertCommand.Parameters.AddWithValue("@chemical_attack", spiritCardFromDB.ChemicalAttack);
                    insertCommand.Parameters.AddWithValue("@chemical_defense", spiritCardFromDB.ChemicalDefense);
                    insertCommand.Parameters.AddWithValue("@atomic_attack", spiritCardFromDB.AtomicAttack);
                    insertCommand.Parameters.AddWithValue("@atomic_defense", spiritCardFromDB.AtomicDefense);
                    insertCommand.Parameters.AddWithValue("@mental_attack", spiritCardFromDB.MentalAttack);
                    insertCommand.Parameters.AddWithValue("@mental_defense", spiritCardFromDB.MentalDefense);
                    insertCommand.Parameters.AddWithValue("@speed", spiritCardFromDB.Speed);
                    insertCommand.Parameters.AddWithValue("@critical_damage_rate", spiritCardFromDB.CriticalDamageRate);
                    insertCommand.Parameters.AddWithValue("@critical_rate", spiritCardFromDB.CriticalRate);
                    insertCommand.Parameters.AddWithValue("@critical_resistance_rate", spiritCardFromDB.CriticalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@ignore_critical_rate", spiritCardFromDB.IgnoreCriticalRate);
                    insertCommand.Parameters.AddWithValue("@penetration_rate", spiritCardFromDB.PenetrationRate);
                    insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", spiritCardFromDB.PenetrationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@evasion_rate", spiritCardFromDB.EvasionRate);
                    insertCommand.Parameters.AddWithValue("@damage_absorption_rate", spiritCardFromDB.DamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", spiritCardFromDB.IgnoreDamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", spiritCardFromDB.AbsorbedDamageRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", spiritCardFromDB.VitalityRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", spiritCardFromDB.VitalityRegenerationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@accuracy_rate", spiritCardFromDB.AccuracyRate);
                    insertCommand.Parameters.AddWithValue("@lifesteal_rate", spiritCardFromDB.LifestealRate);
                    insertCommand.Parameters.AddWithValue("@shield_strength", spiritCardFromDB.ShieldStrength);
                    insertCommand.Parameters.AddWithValue("@tenacity", spiritCardFromDB.Tenacity);
                    insertCommand.Parameters.AddWithValue("@resistance_rate", spiritCardFromDB.ResistanceRate);
                    insertCommand.Parameters.AddWithValue("@combo_rate", spiritCardFromDB.ComboRate);
                    insertCommand.Parameters.AddWithValue("@ignore_combo_rate", spiritCardFromDB.IgnoreComboRate);
                    insertCommand.Parameters.AddWithValue("@combo_damage_rate", spiritCardFromDB.ComboDamageRate);
                    insertCommand.Parameters.AddWithValue("@combo_resistance_rate", spiritCardFromDB.ComboResistanceRate);
                    insertCommand.Parameters.AddWithValue("@stun_rate", spiritCardFromDB.StunRate);
                    insertCommand.Parameters.AddWithValue("@ignore_stun_rate", spiritCardFromDB.IgnoreStunRate);
                    insertCommand.Parameters.AddWithValue("@reflection_rate", spiritCardFromDB.ReflectionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", spiritCardFromDB.IgnoreReflectionRate);
                    insertCommand.Parameters.AddWithValue("@reflection_damage_rate", spiritCardFromDB.ReflectionDamageRate);
                    insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", spiritCardFromDB.ReflectionResistanceRate);
                    insertCommand.Parameters.AddWithValue("@mana", spiritCardFromDB.Mana);
                    insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", spiritCardFromDB.ManaRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", spiritCardFromDB.DamageToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", spiritCardFromDB.ResistanceToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", spiritCardFromDB.DamageToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", spiritCardFromDB.ResistanceToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@normal_damage_rate", spiritCardFromDB.NormalDamageRate);
                    insertCommand.Parameters.AddWithValue("@normal_resistance_rate", spiritCardFromDB.NormalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@skill_damage_rate", spiritCardFromDB.SkillDamageRate);
                    insertCommand.Parameters.AddWithValue("@skill_resistance_rate", spiritCardFromDB.SkillResistanceRate);

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
    public async Task UpdateStatusSpiritCardGalleryAsync(string Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = "UPDATE spirit_cards_gallery SET status=@status WHERE user_id=@user_id AND spirit_card_id=@spirit_card_id";
                MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
                updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                updateCommand.Parameters.AddWithValue("@spirit_card_id", Id);
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
    public async Task UpdateStarSpiritCardGalleryAsync(string Id, double star)
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
                FROM spirit_cards_gallery 
                WHERE user_id = @user_id AND spirit_card_id = @spirit_card_id;
            ";

                MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@spirit_card_id", Id);

                await using (var reader = await checkCommand.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        double tempStar = reader.IsDBNull(reader.GetOrdinal("temp_star")) ? 0 : reader.GetDoubleSafe("temp_star");

                        if (tempStar < star)
                        {
                            reader.Close(); // Đóng reader trước khi thực hiện update

                            string updateSQL = @"
                            UPDATE spirit_cards_gallery 
                            SET temp_star = @temp_star 
                            WHERE user_id = @user_id AND spirit_card_id = @spirit_card_id;
                        ";

                            MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
                            updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCommand.Parameters.AddWithValue("@spirit_card_id", Id);
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
    public async Task UpdateSpiritCardGalleryPowerAsync(string Id, SpiritCards spiritCardFromDB)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"UPDATE spirit_cards_gallery
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
                AND spirit_card_id = @spirit_card_id;
            ";

                MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
                updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                updateCommand.Parameters.AddWithValue("@spirit_card_id", Id);
                updateCommand.Parameters.AddWithValue("@status", "pending");
                updateCommand.Parameters.AddWithValue("@current_star", 0);
                updateCommand.Parameters.AddWithValue("@power", spiritCardFromDB.Power);
                updateCommand.Parameters.AddWithValue("@health", spiritCardFromDB.Health);
                updateCommand.Parameters.AddWithValue("@physical_attack", spiritCardFromDB.PhysicalAttack);
                updateCommand.Parameters.AddWithValue("@physical_defense", spiritCardFromDB.PhysicalDefense);
                updateCommand.Parameters.AddWithValue("@magical_attack", spiritCardFromDB.MagicalAttack);
                updateCommand.Parameters.AddWithValue("@magical_defense", spiritCardFromDB.MagicalDefense);
                updateCommand.Parameters.AddWithValue("@chemical_attack", spiritCardFromDB.ChemicalAttack);
                updateCommand.Parameters.AddWithValue("@chemical_defense", spiritCardFromDB.ChemicalDefense);
                updateCommand.Parameters.AddWithValue("@atomic_attack", spiritCardFromDB.AtomicAttack);
                updateCommand.Parameters.AddWithValue("@atomic_defense", spiritCardFromDB.AtomicDefense);
                updateCommand.Parameters.AddWithValue("@mental_attack", spiritCardFromDB.MentalAttack);
                updateCommand.Parameters.AddWithValue("@mental_defense", spiritCardFromDB.MentalDefense);
                updateCommand.Parameters.AddWithValue("@speed", spiritCardFromDB.Speed);
                updateCommand.Parameters.AddWithValue("@critical_damage_rate", spiritCardFromDB.CriticalDamageRate);
                updateCommand.Parameters.AddWithValue("@critical_rate", spiritCardFromDB.CriticalRate);
                updateCommand.Parameters.AddWithValue("@critical_resistance_rate", spiritCardFromDB.CriticalResistanceRate);
                updateCommand.Parameters.AddWithValue("@ignore_critical_rate", spiritCardFromDB.IgnoreCriticalRate);
                updateCommand.Parameters.AddWithValue("@penetration_rate", spiritCardFromDB.PenetrationRate);
                updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", spiritCardFromDB.PenetrationResistanceRate);
                updateCommand.Parameters.AddWithValue("@evasion_rate", spiritCardFromDB.EvasionRate);
                updateCommand.Parameters.AddWithValue("@damage_absorption_rate", spiritCardFromDB.DamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", spiritCardFromDB.IgnoreDamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", spiritCardFromDB.AbsorbedDamageRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", spiritCardFromDB.VitalityRegenerationRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", spiritCardFromDB.VitalityRegenerationResistanceRate);
                updateCommand.Parameters.AddWithValue("@accuracy_rate", spiritCardFromDB.AccuracyRate);
                updateCommand.Parameters.AddWithValue("@lifesteal_rate", spiritCardFromDB.LifestealRate);
                updateCommand.Parameters.AddWithValue("@shield_strength", spiritCardFromDB.ShieldStrength);
                updateCommand.Parameters.AddWithValue("@tenacity", spiritCardFromDB.Tenacity);
                updateCommand.Parameters.AddWithValue("@resistance_rate", spiritCardFromDB.ResistanceRate);
                updateCommand.Parameters.AddWithValue("@combo_rate", spiritCardFromDB.ComboRate);
                updateCommand.Parameters.AddWithValue("@ignore_combo_rate", spiritCardFromDB.IgnoreComboRate);
                updateCommand.Parameters.AddWithValue("@combo_damage_rate", spiritCardFromDB.ComboDamageRate);
                updateCommand.Parameters.AddWithValue("@combo_resistance_rate", spiritCardFromDB.ComboResistanceRate);
                updateCommand.Parameters.AddWithValue("@stun_rate", spiritCardFromDB.StunRate);
                updateCommand.Parameters.AddWithValue("@ignore_stun_rate", spiritCardFromDB.IgnoreStunRate);
                updateCommand.Parameters.AddWithValue("@reflection_rate", spiritCardFromDB.ReflectionRate);
                updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", spiritCardFromDB.IgnoreReflectionRate);
                updateCommand.Parameters.AddWithValue("@reflection_damage_rate", spiritCardFromDB.ReflectionDamageRate);
                updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", spiritCardFromDB.ReflectionResistanceRate);
                updateCommand.Parameters.AddWithValue("@mana", spiritCardFromDB.Mana);
                updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", spiritCardFromDB.ManaRegenerationRate);
                updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", spiritCardFromDB.DamageToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", spiritCardFromDB.ResistanceToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", spiritCardFromDB.DamageToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", spiritCardFromDB.ResistanceToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@normal_damage_rate", spiritCardFromDB.NormalDamageRate);
                updateCommand.Parameters.AddWithValue("@normal_resistance_rate", spiritCardFromDB.NormalResistanceRate);
                updateCommand.Parameters.AddWithValue("@skill_damage_rate", spiritCardFromDB.SkillDamageRate);
                updateCommand.Parameters.AddWithValue("@skill_resistance_rate", spiritCardFromDB.SkillResistanceRate);
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
    public async Task<SpiritCards> SumPowerSpiritCardsGalleryAsync()
    {
        SpiritCards sumSpiritCards = new SpiritCards();
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
            FROM spirit_cards_gallery 
            WHERE user_id = @user_id AND status = 'available';";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    await using (MySqlDataReader reader = (MySqlDataReader)await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumSpiritCards.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            sumSpiritCards.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            sumSpiritCards.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            sumSpiritCards.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            sumSpiritCards.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            sumSpiritCards.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            sumSpiritCards.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            sumSpiritCards.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            sumSpiritCards.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            sumSpiritCards.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            sumSpiritCards.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            sumSpiritCards.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            sumSpiritCards.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            sumSpiritCards.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            sumSpiritCards.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            sumSpiritCards.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            sumSpiritCards.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            sumSpiritCards.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            sumSpiritCards.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            sumSpiritCards.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            sumSpiritCards.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            sumSpiritCards.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            sumSpiritCards.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            sumSpiritCards.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            sumSpiritCards.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            sumSpiritCards.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            sumSpiritCards.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            sumSpiritCards.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            sumSpiritCards.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            sumSpiritCards.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            sumSpiritCards.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            sumSpiritCards.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            sumSpiritCards.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            sumSpiritCards.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            sumSpiritCards.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            sumSpiritCards.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            sumSpiritCards.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            sumSpiritCards.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            sumSpiritCards.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            sumSpiritCards.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            sumSpiritCards.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDoubleSafe("total_mana");
                            sumSpiritCards.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            sumSpiritCards.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            sumSpiritCards.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            sumSpiritCards.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            sumSpiritCards.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            sumSpiritCards.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            sumSpiritCards.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            sumSpiritCards.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            sumSpiritCards.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
                            sumSpiritCards.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDoubleSafe("total_percent_all_health");
                            sumSpiritCards.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_physical_attack");
                            sumSpiritCards.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_physical_defense");
                            sumSpiritCards.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_magical_attack");
                            sumSpiritCards.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_magical_defense");
                            sumSpiritCards.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_chemical_attack");
                            sumSpiritCards.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_chemical_defense");
                            sumSpiritCards.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_atomic_attack");
                            sumSpiritCards.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_atomic_defense");
                            sumSpiritCards.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_mental_attack");
                            sumSpiritCards.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_mental_defense");
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

        return sumSpiritCards;
    }
}