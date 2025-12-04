using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class RelicsGalleryRepository : IRelicsGalleryRepository
{
    public async Task<List<Relics>> GetRelicsCollectionAsync(string type, int pageSize, int offset, string rare)
    {
        List<Relics> relics = new List<Relics>();
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
                        WHEN mg.Relic_id IS NULL THEN 'block'
                        WHEN mg.status = 'pending' THEN 'pending'
                        WHEN mg.status = 'available' THEN 'available'
                    END AS status 
                FROM Relics m 
                LEFT JOIN Relics_gallery mg 
                    ON m.id = mg.Relic_id AND mg.user_id = @userId 
                WHERE m.type = @type 
                    AND (@rare = 'All' OR m.rare = @rare)
                ORDER BY 
                    m.name REGEXP '[0-9]+$',
                    CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED),
                    m.name
                LIMIT @limit OFFSET @offset";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@rare", rare);
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Relics relic = new Relics
                            {
                                Id = reader.GetString("id"),
                                Name = reader.GetString("name"),
                                Image = reader.GetString("image"),
                                Rare = reader.GetString("rare"),
                                Quality = reader.GetDouble("quality"),
                                CurrentStar = reader.IsDBNull(reader.GetOrdinal("current_star")) ? 0 : reader.GetInt32("current_star"),
                                TempStar = reader.IsDBNull(reader.GetOrdinal("temp_star")) ? 0 : reader.GetInt32("temp_star"),
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

                            relics.Add(relic);
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
        return relics;
    }
    public async Task<int> GetRelicsCountAsync(string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = "SELECT COUNT(*) FROM Relics WHERE type = @type AND (@rare = 'All' OR rare = @rare)";
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@rare", rare);

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
    public async Task InsertRelicGalleryAsync(string Id, Relics RelicFromDB)
    {
        int percent = QualityEvaluator.CheckQuality(RelicFromDB.Type);
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra bản ghi đã tồn tại
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM Relics_gallery 
                WHERE user_id = @user_id AND Relic_id = @Relic_id;
                ";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@Relic_id", Id);

                int recordCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                // Nếu chưa có thì insert
                if (recordCount == 0)
                {
                    string query = @"
                INSERT INTO relics_gallery (
                    user_id, Relic_id, status, current_star, temp_star, power, health, 
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
                    @user_id, @Relic_id, @status, @current_star, @temp_star, @power, @health,
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
                    command.Parameters.AddWithValue("@Relic_id", Id);
                    command.Parameters.AddWithValue("@status", "pending");
                    command.Parameters.AddWithValue("@current_star", 0);
                    command.Parameters.AddWithValue("@temp_star", 0);

                    // Thuộc tính
                    command.Parameters.AddWithValue("@power", RelicFromDB.Power);
                    command.Parameters.AddWithValue("@health", RelicFromDB.Health);
                    command.Parameters.AddWithValue("@physical_attack", RelicFromDB.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", RelicFromDB.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", RelicFromDB.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", RelicFromDB.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", RelicFromDB.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", RelicFromDB.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", RelicFromDB.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", RelicFromDB.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", RelicFromDB.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", RelicFromDB.MentalDefense);
                    command.Parameters.AddWithValue("@speed", RelicFromDB.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", RelicFromDB.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", RelicFromDB.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", RelicFromDB.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", RelicFromDB.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", RelicFromDB.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", RelicFromDB.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", RelicFromDB.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", RelicFromDB.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", RelicFromDB.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", RelicFromDB.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", RelicFromDB.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", RelicFromDB.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", RelicFromDB.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", RelicFromDB.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", RelicFromDB.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", RelicFromDB.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", RelicFromDB.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", RelicFromDB.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", RelicFromDB.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", RelicFromDB.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", RelicFromDB.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", RelicFromDB.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", RelicFromDB.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", RelicFromDB.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", RelicFromDB.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", RelicFromDB.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", RelicFromDB.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", RelicFromDB.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", RelicFromDB.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", RelicFromDB.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", RelicFromDB.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", RelicFromDB.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", RelicFromDB.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", RelicFromDB.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", RelicFromDB.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", RelicFromDB.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", RelicFromDB.SkillResistanceRate);

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
    public async Task UpdateStatusRelicGalleryAsync(string Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = "UPDATE Relics_gallery SET status=@status WHERE user_id=@user_id AND Relic_id=@Relic_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@Relic_id", Id);
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
    public async Task UpdateStarRelicGalleryAsync(string Id, double star)
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
                FROM Relics_gallery 
                WHERE user_id = @user_id AND Relic_id = @Relic_id;
            ";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@Relic_id", Id);

                await using (var reader = await checkCommand.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        double tempStar = reader.IsDBNull(reader.GetOrdinal("temp_star")) ? 0 : reader.GetDouble("temp_star");

                        if (tempStar < star)
                        {
                            reader.Close(); // Đóng reader trước khi thực hiện update

                            string updateQuery = @"
                            UPDATE relics_gallery 
                            SET temp_star = @temp_star 
                            WHERE user_id = @user_id AND Relic_id = @Relic_id;
                        ";

                            MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                            updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCommand.Parameters.AddWithValue("@Relic_id", Id);
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
    public async Task UpdateRelicGalleryPowerAsync(string Id, Relics RelicFromDB)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"UPDATE Relics_gallery
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
                AND Relic_id = @Relic_id;
            ";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@Relic_id", Id);
                command.Parameters.AddWithValue("@status", "pending");
                command.Parameters.AddWithValue("@current_star", 0);
                command.Parameters.AddWithValue("@power", RelicFromDB.Power);
                command.Parameters.AddWithValue("@health", RelicFromDB.Health);
                command.Parameters.AddWithValue("@physical_attack", RelicFromDB.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", RelicFromDB.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", RelicFromDB.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", RelicFromDB.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", RelicFromDB.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", RelicFromDB.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", RelicFromDB.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", RelicFromDB.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", RelicFromDB.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", RelicFromDB.MentalDefense);
                command.Parameters.AddWithValue("@speed", RelicFromDB.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", RelicFromDB.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", RelicFromDB.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", RelicFromDB.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", RelicFromDB.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", RelicFromDB.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", RelicFromDB.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", RelicFromDB.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", RelicFromDB.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", RelicFromDB.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", RelicFromDB.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", RelicFromDB.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", RelicFromDB.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", RelicFromDB.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", RelicFromDB.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", RelicFromDB.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", RelicFromDB.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", RelicFromDB.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", RelicFromDB.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", RelicFromDB.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", RelicFromDB.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", RelicFromDB.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", RelicFromDB.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", RelicFromDB.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", RelicFromDB.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", RelicFromDB.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", RelicFromDB.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", RelicFromDB.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", RelicFromDB.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", RelicFromDB.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", RelicFromDB.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", RelicFromDB.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", RelicFromDB.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", RelicFromDB.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", RelicFromDB.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", RelicFromDB.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", RelicFromDB.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", RelicFromDB.SkillResistanceRate);
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
    public async Task<Relics> SumPowerRelicsGalleryAsync()
    {
        Relics sumRelics = new Relics();
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
            FROM Relics_gallery 
            WHERE user_id = @user_id AND status = 'available';";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    await using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumRelics.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                            sumRelics.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                            sumRelics.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                            sumRelics.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                            sumRelics.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                            sumRelics.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                            sumRelics.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                            sumRelics.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                            sumRelics.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                            sumRelics.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                            sumRelics.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                            sumRelics.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                            sumRelics.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                            sumRelics.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                            sumRelics.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                            sumRelics.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                            sumRelics.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                            sumRelics.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                            sumRelics.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                            sumRelics.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                            sumRelics.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                            sumRelics.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                            sumRelics.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                            sumRelics.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                            sumRelics.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                            sumRelics.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                            sumRelics.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                            sumRelics.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                            sumRelics.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                            sumRelics.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                            sumRelics.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                            sumRelics.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                            sumRelics.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                            sumRelics.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                            sumRelics.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                            sumRelics.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                            sumRelics.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                            sumRelics.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                            sumRelics.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                            sumRelics.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                            sumRelics.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDouble("total_mana");
                            sumRelics.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                            sumRelics.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                            sumRelics.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                            sumRelics.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                            sumRelics.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                            sumRelics.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                            sumRelics.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                            sumRelics.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                            sumRelics.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
                            sumRelics.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDouble("total_percent_all_health");
                            sumRelics.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDouble("total_percent_all_physical_attack");
                            sumRelics.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDouble("total_percent_all_physical_defense");
                            sumRelics.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDouble("total_percent_all_magical_attack");
                            sumRelics.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDouble("total_percent_all_magical_defense");
                            sumRelics.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDouble("total_percent_all_chemical_attack");
                            sumRelics.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDouble("total_percent_all_chemical_defense");
                            sumRelics.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDouble("total_percent_all_atomic_attack");
                            sumRelics.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDouble("total_percent_all_atomic_defense");
                            sumRelics.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDouble("total_percent_all_mental_attack");
                            sumRelics.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDouble("total_percent_all_mental_defense");
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

        return sumRelics;
    }
}