using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class RunesGalleryRepository : IRunesGalleryRepository
{
    public async Task<List<Runes>> GetRunesCollectionAsync(int pageSize, int offset, string rare)
    {
        List<Runes> Runes = new List<Runes>();
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
                           WHEN cg.rune_id IS NULL THEN 'block' 
                           WHEN cg.status = 'pending' THEN 'pending' 
                           WHEN cg.status = 'available' THEN 'available' 
                       END AS status 
                FROM Runes c 
                LEFT JOIN runes_gallery cg 
                       ON c.id = cg.rune_id AND cg.user_id = @userId 
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
                            Runes Rune = new Runes
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

                            Runes.Add(Rune);
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

        return Runes;
    }
    public async Task<int> GetRunesCountAsync(string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = "SELECT COUNT(*) FROM Runes WHERE (@rare = 'All' OR rare = @rare)";
                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@rare", rare);

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
    public async Task InsertRuneGalleryAsync(string Id, Runes RuneFromDB)
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
                FROM Runes_gallery 
                WHERE user_id = @user_id AND Rune_id = @Rune_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCommand.Parameters.AddWithValue("@Rune_id", Id);

                    int recordCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu chưa có → INSERT
                    if (recordCount == 0)
                    {
                        string query = @"
                    INSERT INTO Runes_gallery (
                        user_id, Rune_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
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
                        @user_id, @Rune_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
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
                            command.Parameters.AddWithValue("@Rune_id", Id);
                            command.Parameters.AddWithValue("@status", "pending");
                            command.Parameters.AddWithValue("@current_star", 0);
                            command.Parameters.AddWithValue("@temp_star", 0);

                            command.Parameters.AddWithValue("@power", RuneFromDB.Power);
                            command.Parameters.AddWithValue("@health", RuneFromDB.Health);
                            command.Parameters.AddWithValue("@physical_attack", RuneFromDB.PhysicalAttack);
                            command.Parameters.AddWithValue("@physical_defense", RuneFromDB.PhysicalDefense);
                            command.Parameters.AddWithValue("@magical_attack", RuneFromDB.MagicalAttack);
                            command.Parameters.AddWithValue("@magical_defense", RuneFromDB.MagicalDefense);
                            command.Parameters.AddWithValue("@chemical_attack", RuneFromDB.ChemicalAttack);
                            command.Parameters.AddWithValue("@chemical_defense", RuneFromDB.ChemicalDefense);
                            command.Parameters.AddWithValue("@atomic_attack", RuneFromDB.AtomicAttack);
                            command.Parameters.AddWithValue("@atomic_defense", RuneFromDB.AtomicDefense);

                            command.Parameters.AddWithValue("@mental_attack", RuneFromDB.MentalAttack);
                            command.Parameters.AddWithValue("@mental_defense", RuneFromDB.MentalDefense);

                            command.Parameters.AddWithValue("@speed", RuneFromDB.Speed);
                            command.Parameters.AddWithValue("@critical_damage_rate", RuneFromDB.CriticalDamageRate);
                            command.Parameters.AddWithValue("@critical_rate", RuneFromDB.CriticalRate);
                            command.Parameters.AddWithValue("@critical_resistance_rate", RuneFromDB.CriticalResistanceRate);
                            command.Parameters.AddWithValue("@ignore_critical_rate", RuneFromDB.IgnoreCriticalRate);
                            command.Parameters.AddWithValue("@penetration_rate", RuneFromDB.PenetrationRate);
                            command.Parameters.AddWithValue("@penetration_resistance_rate", RuneFromDB.PenetrationResistanceRate);
                            command.Parameters.AddWithValue("@evasion_rate", RuneFromDB.EvasionRate);
                            command.Parameters.AddWithValue("@damage_absorption_rate", RuneFromDB.DamageAbsorptionRate);
                            command.Parameters.AddWithValue("@ignore_damage_absorption_rate", RuneFromDB.IgnoreDamageAbsorptionRate);
                            command.Parameters.AddWithValue("@absorbed_damage_rate", RuneFromDB.AbsorbedDamageRate);

                            command.Parameters.AddWithValue("@vitality_regeneration_rate", RuneFromDB.VitalityRegenerationRate);
                            command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", RuneFromDB.VitalityRegenerationResistanceRate);

                            command.Parameters.AddWithValue("@accuracy_rate", RuneFromDB.AccuracyRate);
                            command.Parameters.AddWithValue("@lifesteal_rate", RuneFromDB.LifestealRate);
                            command.Parameters.AddWithValue("@shield_strength", RuneFromDB.ShieldStrength);
                            command.Parameters.AddWithValue("@tenacity", RuneFromDB.Tenacity);
                            command.Parameters.AddWithValue("@resistance_rate", RuneFromDB.ResistanceRate);
                            command.Parameters.AddWithValue("@combo_rate", RuneFromDB.ComboRate);
                            command.Parameters.AddWithValue("@ignore_combo_rate", RuneFromDB.IgnoreComboRate);
                            command.Parameters.AddWithValue("@combo_damage_rate", RuneFromDB.ComboDamageRate);
                            command.Parameters.AddWithValue("@combo_resistance_rate", RuneFromDB.ComboResistanceRate);

                            command.Parameters.AddWithValue("@stun_rate", RuneFromDB.StunRate);
                            command.Parameters.AddWithValue("@ignore_stun_rate", RuneFromDB.IgnoreStunRate);
                            command.Parameters.AddWithValue("@reflection_rate", RuneFromDB.ReflectionRate);
                            command.Parameters.AddWithValue("@ignore_reflection_rate", RuneFromDB.IgnoreReflectionRate);
                            command.Parameters.AddWithValue("@reflection_damage_rate", RuneFromDB.ReflectionDamageRate);
                            command.Parameters.AddWithValue("@reflection_resistance_rate", RuneFromDB.ReflectionResistanceRate);

                            command.Parameters.AddWithValue("@mana", RuneFromDB.Mana);
                            command.Parameters.AddWithValue("@mana_regeneration_rate", RuneFromDB.ManaRegenerationRate);

                            command.Parameters.AddWithValue("@damage_to_different_faction_rate", RuneFromDB.DamageToDifferentFactionRate);
                            command.Parameters.AddWithValue("@resistance_to_different_faction_rate", RuneFromDB.ResistanceToDifferentFactionRate);
                            command.Parameters.AddWithValue("@damage_to_same_faction_rate", RuneFromDB.DamageToSameFactionRate);
                            command.Parameters.AddWithValue("@resistance_to_same_faction_rate", RuneFromDB.ResistanceToSameFactionRate);

                            command.Parameters.AddWithValue("@normal_damage_rate", RuneFromDB.NormalDamageRate);
                            command.Parameters.AddWithValue("@normal_resistance_rate", RuneFromDB.NormalResistanceRate);
                            command.Parameters.AddWithValue("@skill_damage_rate", RuneFromDB.SkillDamageRate);
                            command.Parameters.AddWithValue("@skill_resistance_rate", RuneFromDB.SkillResistanceRate);

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
    public async Task UpdateStatusRuneGalleryAsync(string Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"UPDATE Runes_gallery 
                             SET status=@status 
                             WHERE user_id=@user_id AND Rune_id=@Rune_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@Rune_id", Id);
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
    public async Task UpdateStarRuneGalleryAsync(string id, double star)
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
                FROM Runes_gallery 
                WHERE user_id = @user_id AND Rune_id = @Rune_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCommand.Parameters.AddWithValue("@Rune_id", id);

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
                                UPDATE Runes_gallery 
                                SET temp_star = @temp_star 
                                WHERE user_id = @user_id AND Rune_id = @Rune_id;
                            ";

                                await using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                                    updateCommand.Parameters.AddWithValue("@Rune_id", id);
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
    public async Task UpdateRuneGalleryPowerAsync(string id, Runes RuneFromDB)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"UPDATE Runes_gallery
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
                AND Rune_id = @Rune_id;
            ";

                MySqlCommand command = new MySqlCommand(query, connection);

                // IDs
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@Rune_id", id);

                // Base flags
                command.Parameters.AddWithValue("@status", "pending");
                command.Parameters.AddWithValue("@current_star", 0);

                // Stats
                command.Parameters.AddWithValue("@power", RuneFromDB.Power);
                command.Parameters.AddWithValue("@health", RuneFromDB.Health);
                command.Parameters.AddWithValue("@physical_attack", RuneFromDB.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", RuneFromDB.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", RuneFromDB.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", RuneFromDB.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", RuneFromDB.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", RuneFromDB.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", RuneFromDB.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", RuneFromDB.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", RuneFromDB.MagicalAttack);
                command.Parameters.AddWithValue("@mental_defense", RuneFromDB.MagicalDefense);
                command.Parameters.AddWithValue("@speed", RuneFromDB.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", RuneFromDB.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", RuneFromDB.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", RuneFromDB.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", RuneFromDB.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", RuneFromDB.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", RuneFromDB.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", RuneFromDB.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", RuneFromDB.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", RuneFromDB.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", RuneFromDB.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", RuneFromDB.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", RuneFromDB.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", RuneFromDB.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", RuneFromDB.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", RuneFromDB.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", RuneFromDB.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", RuneFromDB.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", RuneFromDB.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", RuneFromDB.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", RuneFromDB.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", RuneFromDB.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", RuneFromDB.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", RuneFromDB.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", RuneFromDB.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", RuneFromDB.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", RuneFromDB.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", RuneFromDB.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", RuneFromDB.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", RuneFromDB.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", RuneFromDB.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", RuneFromDB.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", RuneFromDB.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", RuneFromDB.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", RuneFromDB.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", RuneFromDB.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", RuneFromDB.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", RuneFromDB.SkillResistanceRate);

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
    public async Task<Runes> SumPowerRunesGalleryAsync()
    {
        Runes sumRunes = new Runes();
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
                FROM Runes_gallery 
                WHERE user_id = @user_id AND status = 'available';
            ";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        sumRunes.Power = reader["total_power"] as double? ?? 0;
                        sumRunes.Health = reader["total_health"] as double? ?? 0;
                        sumRunes.Mana = reader["total_mana"] as double? ?? 0f;

                        sumRunes.PhysicalAttack = reader["total_physical_attack"] as double? ?? 0;
                        sumRunes.PhysicalDefense = reader["total_physical_defense"] as double? ?? 0;
                        sumRunes.MagicalAttack = reader["total_magical_attack"] as double? ?? 0;
                        sumRunes.MagicalDefense = reader["total_magical_defense"] as double? ?? 0;
                        sumRunes.ChemicalAttack = reader["total_chemical_attack"] as double? ?? 0;
                        sumRunes.ChemicalDefense = reader["total_chemical_defense"] as double? ?? 0;
                        sumRunes.AtomicAttack = reader["total_atomic_attack"] as double? ?? 0;
                        sumRunes.AtomicDefense = reader["total_atomic_defense"] as double? ?? 0;
                        sumRunes.MentalAttack = reader["total_mental_attack"] as double? ?? 0;
                        sumRunes.MentalDefense = reader["total_mental_defense"] as double? ?? 0;

                        sumRunes.Speed = reader["total_speed"] as double? ?? 0;
                        sumRunes.CriticalDamageRate = reader["total_critical_damage_rate"] as double? ?? 0;
                        sumRunes.CriticalRate = reader["total_critical_rate"] as double? ?? 0;
                        sumRunes.CriticalResistanceRate = reader["total_critical_resistance_rate"] as double? ?? 0;

                        sumRunes.IgnoreCriticalRate = reader["total_ignore_critical_rate"] as double? ?? 0;
                        sumRunes.PenetrationRate = reader["total_penetration_rate"] as double? ?? 0;
                        sumRunes.PenetrationResistanceRate = reader["total_penetration_resistance_rate"] as double? ?? 0;

                        sumRunes.EvasionRate = reader["total_evasion_rate"] as double? ?? 0;
                        sumRunes.DamageAbsorptionRate = reader["total_damage_absorption_rate"] as double? ?? 0;
                        sumRunes.IgnoreDamageAbsorptionRate = reader["total_ignore_damage_absorption_rate"] as double? ?? 0;
                        sumRunes.AbsorbedDamageRate = reader["total_absorbed_damage_rate"] as double? ?? 0;

                        sumRunes.VitalityRegenerationRate = reader["total_vitality_regeneration_rate"] as double? ?? 0;
                        sumRunes.VitalityRegenerationResistanceRate = reader["total_vitality_regeneration_resistance_rate"] as double? ?? 0;

                        sumRunes.AccuracyRate = reader["total_accuracy_rate"] as double? ?? 0;
                        sumRunes.LifestealRate = reader["total_lifesteal_rate"] as double? ?? 0;
                        sumRunes.ShieldStrength = reader["total_shield_strength"] as double? ?? 0;

                        sumRunes.Tenacity = reader["total_tenacity"] as double? ?? 0;
                        sumRunes.ResistanceRate = reader["total_resistance_rate"] as double? ?? 0;

                        sumRunes.ComboRate = reader["total_combo_rate"] as double? ?? 0;
                        sumRunes.IgnoreComboRate = reader["total_ignore_combo_rate"] as double? ?? 0;
                        sumRunes.ComboDamageRate = reader["total_combo_damage_rate"] as double? ?? 0;
                        sumRunes.ComboResistanceRate = reader["total_combo_resistance_rate"] as double? ?? 0;

                        sumRunes.StunRate = reader["total_stun_rate"] as double? ?? 0;
                        sumRunes.IgnoreStunRate = reader["total_ignore_stun_rate"] as double? ?? 0;

                        sumRunes.ReflectionRate = reader["total_reflection_rate"] as double? ?? 0;
                        sumRunes.IgnoreReflectionRate = reader["total_ignore_reflection_rate"] as double? ?? 0;
                        sumRunes.ReflectionDamageRate = reader["total_reflection_damage_rate"] as double? ?? 0;
                        sumRunes.ReflectionResistanceRate = reader["total_reflection_resistance_rate"] as double? ?? 0;

                        sumRunes.ManaRegenerationRate = reader["total_mana_regeneration_rate"] as double? ?? 0;

                        sumRunes.DamageToDifferentFactionRate = reader["total_damage_to_different_faction_rate"] as double? ?? 0;
                        sumRunes.ResistanceToDifferentFactionRate = reader["total_resistance_to_different_faction_rate"] as double? ?? 0;

                        sumRunes.DamageToSameFactionRate = reader["total_damage_to_same_faction_rate"] as double? ?? 0;
                        sumRunes.ResistanceToSameFactionRate = reader["total_resistance_to_same_faction_rate"] as double? ?? 0;

                        sumRunes.NormalDamageRate = reader["total_normal_damage_rate"] as double? ?? 0;
                        sumRunes.NormalResistanceRate = reader["total_normal_resistance_rate"] as double? ?? 0;

                        sumRunes.SkillDamageRate = reader["total_skill_damage_rate"] as double? ?? 0;
                        sumRunes.SkillResistanceRate = reader["total_skill_resistance_rate"] as double? ?? 0;

                        sumRunes.PercentAllHealth = reader["total_percent_all_health"] as double? ?? 0;
                        sumRunes.PercentAllPhysicalAttack = reader["total_percent_all_physical_attack"] as double? ?? 0;
                        sumRunes.PercentAllPhysicalDefense = reader["total_percent_all_physical_defense"] as double? ?? 0;
                        sumRunes.PercentAllMagicalAttack = reader["total_percent_all_magical_attack"] as double? ?? 0;
                        sumRunes.PercentAllMagicalDefense = reader["total_percent_all_magical_defense"] as double? ?? 0;
                        sumRunes.PercentAllChemicalAttack = reader["total_percent_all_chemical_attack"] as double? ?? 0;
                        sumRunes.PercentAllChemicalDefense = reader["total_percent_all_chemical_defense"] as double? ?? 0;
                        sumRunes.PercentAllAtomicAttack = reader["total_percent_all_atomic_attack"] as double? ?? 0;
                        sumRunes.PercentAllAtomicDefense = reader["total_percent_all_atomic_defense"] as double? ?? 0;
                        sumRunes.PercentAllMentalAttack = reader["total_percent_all_mental_attack"] as double? ?? 0;
                        sumRunes.PercentAllMentalDefense = reader["total_percent_all_mental_defense"] as double? ?? 0;
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

        return sumRunes;
    }
}