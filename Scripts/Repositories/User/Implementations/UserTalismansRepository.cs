using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;

public class UserTalismansRepository : IUserTalismansRepository
{
    public async Task<List<Talismans>> GetUserTalismansAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Talismans> talismans = new List<Talismans>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT um.*, m.id, m.name, m.image, m.rare, m.description 
                FROM Talismans m
                JOIN user_talismans um ON m.id = um.talisman_id
                WHERE um.user_id = @userId";

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

                selectSQL += @"
                ORDER BY m.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name
                LIMIT @limit OFFSET @offset";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", user_id);
                    
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

                    selectCommand.Parameters.AddWithValue("@limit", pageSize);
                    selectCommand.Parameters.AddWithValue("@offset", offset);
                    selectCommand.Parameters.AddWithValue("@search", search);
                    selectCommand.Parameters.AddWithValue("@type", type);
                    selectCommand.Parameters.AddWithValue("@rare", rare);
                    selectCommand.Parameters.AddWithValue("@limit", pageSize);
                    selectCommand.Parameters.AddWithValue("@offset", offset);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Talismans talisman = new Talismans
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

                            talismans.Add(talisman);
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

        return talismans;
    }
    public async Task<int> GetUserTalismansCountAsync(string user_id, string search, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT COUNT(*) 
                FROM Talismans m
                JOIN user_talismans um ON m.id = um.talisman_id
                WHERE um.user_id = @userId";

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

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", user_id);
                    
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
    public async Task<bool> InsertUserTalismanAsync(Talismans talisman, string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkSQL = @"
                SELECT COUNT(*) 
                FROM user_talismans 
                WHERE user_id = @user_id AND talisman_id = @talisman_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@talisman_id", talisman.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertSQL = @"
                        INSERT INTO user_talismans (
                            user_id, talisman_id, rare, level, experiment, star, quality, block, quantity,
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
                            @user_id, @talisman_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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
                        );
                    ";

                        await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@user_id", userId);
                            insertCommand.Parameters.AddWithValue("@talisman_id", talisman.Id);
                            insertCommand.Parameters.AddWithValue("@rare", talisman.Rare);
                            insertCommand.Parameters.AddWithValue("@level", 0);
                            insertCommand.Parameters.AddWithValue("@experiment", 0);
                            insertCommand.Parameters.AddWithValue("@star", 0);
                            insertCommand.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(talisman.Rare));
                            insertCommand.Parameters.AddWithValue("@block", false);
                            insertCommand.Parameters.AddWithValue("@quantity", talisman.Quantity);
                            insertCommand.Parameters.AddWithValue("@power", talisman.Power);
                            insertCommand.Parameters.AddWithValue("@health", talisman.Health);
                            insertCommand.Parameters.AddWithValue("@physical_attack", talisman.PhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@physical_defense", talisman.PhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@magical_attack", talisman.MagicalAttack);
                            insertCommand.Parameters.AddWithValue("@magical_defense", talisman.MagicalDefense);
                            insertCommand.Parameters.AddWithValue("@chemical_attack", talisman.ChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@chemical_defense", talisman.ChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@atomic_attack", talisman.AtomicAttack);
                            insertCommand.Parameters.AddWithValue("@atomic_defense", talisman.AtomicDefense);
                            insertCommand.Parameters.AddWithValue("@mental_attack", talisman.MentalAttack);
                            insertCommand.Parameters.AddWithValue("@mental_defense", talisman.MentalDefense);
                            insertCommand.Parameters.AddWithValue("@speed", talisman.Speed);
                            insertCommand.Parameters.AddWithValue("@critical_damage_rate", talisman.CriticalDamageRate);
                            insertCommand.Parameters.AddWithValue("@critical_rate", talisman.CriticalRate);
                            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", talisman.CriticalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", talisman.IgnoreCriticalRate);
                            insertCommand.Parameters.AddWithValue("@penetration_rate", talisman.PenetrationRate);
                            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", talisman.PenetrationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@evasion_rate", talisman.EvasionRate);
                            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", talisman.DamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", talisman.IgnoreDamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", talisman.AbsorbedDamageRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", talisman.VitalityRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", talisman.VitalityRegenerationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@accuracy_rate", talisman.AccuracyRate);
                            insertCommand.Parameters.AddWithValue("@lifesteal_rate", talisman.LifestealRate);
                            insertCommand.Parameters.AddWithValue("@shield_strength", talisman.ShieldStrength);
                            insertCommand.Parameters.AddWithValue("@tenacity", talisman.Tenacity);
                            insertCommand.Parameters.AddWithValue("@resistance_rate", talisman.ResistanceRate);
                            insertCommand.Parameters.AddWithValue("@combo_rate", talisman.ComboRate);
                            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", talisman.IgnoreComboRate);
                            insertCommand.Parameters.AddWithValue("@combo_damage_rate", talisman.ComboDamageRate);
                            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", talisman.ComboResistanceRate);
                            insertCommand.Parameters.AddWithValue("@stun_rate", talisman.StunRate);
                            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", talisman.IgnoreStunRate);
                            insertCommand.Parameters.AddWithValue("@reflection_rate", talisman.ReflectionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", talisman.IgnoreReflectionRate);
                            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", talisman.ReflectionDamageRate);
                            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", talisman.ReflectionResistanceRate);
                            insertCommand.Parameters.AddWithValue("@mana", talisman.Mana);
                            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", talisman.ManaRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", talisman.DamageToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", talisman.ResistanceToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", talisman.DamageToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", talisman.ResistanceToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@normal_damage_rate", talisman.NormalDamageRate);
                            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", talisman.NormalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@skill_damage_rate", talisman.SkillDamageRate);
                            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", talisman.SkillResistanceRate);

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                        string updateSQL = @"
                        UPDATE user_talismans
                        SET quantity = @quantity
                        WHERE user_id = @user_id AND talisman_id = @talisman_id;
                    ";

                        await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@user_id", userId);
                            updateCommand.Parameters.AddWithValue("@talisman_id", talisman.Id);
                            updateCommand.Parameters.AddWithValue("@quantity", talisman.Quantity);

                            await updateCommand.ExecuteNonQueryAsync();
                        }
                    }
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
    public async Task<bool> InsertOrUpdateUserTalismansBatchAsync(List<Talismans> talismans)
    {
        if (talismans == null || talismans.Count == 0)
            return true;

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            int batchSize = 500; // vì nhiều column → giảm size

            for (int i = 0; i < talismans.Count; i += batchSize)
            {
                var batch = talismans.Skip(i).Take(batchSize).ToList();

                var stringBuilder = new System.Text.StringBuilder();
                var parameters = new List<MySqlParameter>();

                stringBuilder.Append(@"
                INSERT INTO user_talismans (
                    user_id, talisman_id, rare, level, experiment, star, quality, block, quantity,
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
                ) VALUES ");

                for (int j = 0; j < batch.Count; j++)
                {
                    var c = batch[j];

                    stringBuilder.Append($@"
                    (@user_id, @talisman_id_{j}, @rare_{j}, 0, 0, 0, @quality_{j}, 0, @quantity_{j},
                    @power_{j}, @health_{j}, @physical_attack_{j}, @physical_defense_{j}, @magical_attack_{j}, @magical_defense_{j},
                    @chemical_attack_{j}, @chemical_defense_{j}, @atomic_attack_{j}, @atomic_defense_{j}, @mental_attack_{j}, @mental_defense_{j},
                    @speed_{j}, @critical_damage_rate_{j}, @critical_rate_{j}, @critical_resistance_rate_{j}, @ignore_critical_rate_{j},
                    @penetration_rate_{j}, @penetration_resistance_rate_{j},
                    @evasion_rate_{j}, @damage_absorption_rate_{j}, @ignore_damage_absorption_rate_{j}, @absorbed_damage_rate_{j},
                    @vitality_regeneration_rate_{j}, @vitality_regeneration_resistance_rate_{j},
                    @accuracy_rate_{j}, @lifesteal_rate_{j}, @shield_strength_{j}, @tenacity_{j}, @resistance_rate_{j},
                    @combo_rate_{j}, @ignore_combo_rate_{j}, @combo_damage_rate_{j}, @combo_resistance_rate_{j},
                    @stun_rate_{j}, @ignore_stun_rate_{j},
                    @reflection_rate_{j}, @ignore_reflection_rate_{j}, @reflection_damage_rate_{j}, @reflection_resistance_rate_{j},
                    @mana_{j}, @mana_regeneration_rate_{j},
                    @damage_to_different_faction_rate_{j}, @resistance_to_different_faction_rate_{j},
                    @damage_to_same_faction_rate_{j}, @resistance_to_same_faction_rate_{j},
                    @normal_damage_rate_{j}, @normal_resistance_rate_{j},
                    @skill_damage_rate_{j}, @skill_resistance_rate_{j}
                    ),");

                    parameters.AddRange(new[]
                    {
                        new MySqlParameter($"@talisman_id_{j}", c.Id),
                        new MySqlParameter($"@rare_{j}", c.Rare),
                        new MySqlParameter($"@quality_{j}", QualityEvaluatorHelper.CheckQuality(c.Rare)),
                        new MySqlParameter($"@quantity_{j}", c.Quantity),
                        new MySqlParameter($"@power_{j}", c.Power),
                        new MySqlParameter($"@health_{j}", c.Health),
                        new MySqlParameter($"@physical_attack_{j}", c.PhysicalAttack),
                        new MySqlParameter($"@physical_defense_{j}", c.PhysicalDefense),
                        new MySqlParameter($"@magical_attack_{j}", c.MagicalAttack),
                        new MySqlParameter($"@magical_defense_{j}", c.MagicalDefense),
                        new MySqlParameter($"@chemical_attack_{j}", c.ChemicalAttack),
                        new MySqlParameter($"@chemical_defense_{j}", c.ChemicalDefense),
                        new MySqlParameter($"@atomic_attack_{j}", c.AtomicAttack),
                        new MySqlParameter($"@atomic_defense_{j}", c.AtomicDefense),
                        new MySqlParameter($"@mental_attack_{j}", c.MentalAttack),
                        new MySqlParameter($"@mental_defense_{j}", c.MentalDefense),
                        new MySqlParameter($"@speed_{j}", c.Speed),
                        new MySqlParameter($"@critical_damage_rate_{j}", c.CriticalDamageRate),
                        new MySqlParameter($"@critical_rate_{j}", c.CriticalRate),
                        new MySqlParameter($"@critical_resistance_rate_{j}", c.CriticalResistanceRate),
                        new MySqlParameter($"@ignore_critical_rate_{j}", c.IgnoreCriticalRate),
                        new MySqlParameter($"@penetration_rate_{j}", c.PenetrationRate),
                        new MySqlParameter($"@penetration_resistance_rate_{j}", c.PenetrationResistanceRate),
                        new MySqlParameter($"@evasion_rate_{j}", c.EvasionRate),
                        new MySqlParameter($"@damage_absorption_rate_{j}", c.DamageAbsorptionRate),
                        new MySqlParameter($"@ignore_damage_absorption_rate_{j}", c.IgnoreDamageAbsorptionRate),
                        new MySqlParameter($"@absorbed_damage_rate_{j}", c.AbsorbedDamageRate),
                        new MySqlParameter($"@vitality_regeneration_rate_{j}", c.VitalityRegenerationRate),
                        new MySqlParameter($"@vitality_regeneration_resistance_rate_{j}", c.VitalityRegenerationResistanceRate),
                        new MySqlParameter($"@accuracy_rate_{j}", c.AccuracyRate),
                        new MySqlParameter($"@lifesteal_rate_{j}", c.LifestealRate),
                        new MySqlParameter($"@shield_strength_{j}", c.ShieldStrength),
                        new MySqlParameter($"@tenacity_{j}", c.Tenacity),
                        new MySqlParameter($"@resistance_rate_{j}", c.ResistanceRate),
                        new MySqlParameter($"@combo_rate_{j}", c.ComboRate),
                        new MySqlParameter($"@ignore_combo_rate_{j}", c.IgnoreComboRate),
                        new MySqlParameter($"@combo_damage_rate_{j}", c.ComboDamageRate),
                        new MySqlParameter($"@combo_resistance_rate_{j}", c.ComboResistanceRate),
                        new MySqlParameter($"@stun_rate_{j}", c.StunRate),
                        new MySqlParameter($"@ignore_stun_rate_{j}", c.IgnoreStunRate),
                        new MySqlParameter($"@reflection_rate_{j}", c.ReflectionRate),
                        new MySqlParameter($"@ignore_reflection_rate_{j}", c.IgnoreReflectionRate),
                        new MySqlParameter($"@reflection_damage_rate_{j}", c.ReflectionDamageRate),
                        new MySqlParameter($"@reflection_resistance_rate_{j}", c.ReflectionResistanceRate),
                        new MySqlParameter($"@mana_{j}", c.Mana),
                        new MySqlParameter($"@mana_regeneration_rate_{j}", c.ManaRegenerationRate),
                        new MySqlParameter($"@damage_to_different_faction_rate_{j}", c.DamageToDifferentFactionRate),
                        new MySqlParameter($"@resistance_to_different_faction_rate_{j}", c.ResistanceToDifferentFactionRate),
                        new MySqlParameter($"@damage_to_same_faction_rate_{j}", c.DamageToSameFactionRate),
                        new MySqlParameter($"@resistance_to_same_faction_rate_{j}", c.ResistanceToSameFactionRate),
                        new MySqlParameter($"@normal_damage_rate_{j}", c.NormalDamageRate),
                        new MySqlParameter($"@normal_resistance_rate_{j}", c.NormalResistanceRate),
                        new MySqlParameter($"@skill_damage_rate_{j}", c.SkillDamageRate),
                        new MySqlParameter($"@skill_resistance_rate_{j}", c.SkillResistanceRate),
                });
                }

                stringBuilder.Length--; // remove dấu ,

                stringBuilder.Append(@"
                ON DUPLICATE KEY UPDATE
                    quantity = user_talismans.quantity + VALUES(quantity);
                ");

                await using var command = new MySqlCommand(stringBuilder.ToString(), connection, (MySqlTransaction)transaction);

                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddRange(parameters.ToArray());

                await command.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            Debug.LogError("Batch Error: " + ex.Message);
            return false;
        }

        return true;
    }
    public async Task<bool> UpdateTalismanLevelAsync(Talismans talisman, int level)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_talismans
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
                WHERE user_id = @user_id AND talisman_id = @talisman_id;
            ";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@talisman_id", talisman.Id);
                    updateCommand.Parameters.AddWithValue("@level", level);
                    updateCommand.Parameters.AddWithValue("@power", talisman.Power);
                    updateCommand.Parameters.AddWithValue("@health", talisman.Health);
                    updateCommand.Parameters.AddWithValue("@physical_attack", talisman.PhysicalAttack);
                    updateCommand.Parameters.AddWithValue("@physical_defense", talisman.PhysicalDefense);
                    updateCommand.Parameters.AddWithValue("@magical_attack", talisman.MagicalAttack);
                    updateCommand.Parameters.AddWithValue("@magical_defense", talisman.MagicalDefense);
                    updateCommand.Parameters.AddWithValue("@chemical_attack", talisman.ChemicalAttack);
                    updateCommand.Parameters.AddWithValue("@chemical_defense", talisman.ChemicalDefense);
                    updateCommand.Parameters.AddWithValue("@atomic_attack", talisman.AtomicAttack);
                    updateCommand.Parameters.AddWithValue("@atomic_defense", talisman.AtomicDefense);
                    updateCommand.Parameters.AddWithValue("@mental_attack", talisman.MentalAttack);
                    updateCommand.Parameters.AddWithValue("@mental_defense", talisman.MentalDefense);
                    updateCommand.Parameters.AddWithValue("@speed", talisman.Speed);
                    updateCommand.Parameters.AddWithValue("@critical_damage_rate", talisman.CriticalDamageRate);
                    updateCommand.Parameters.AddWithValue("@critical_rate", talisman.CriticalRate);
                    updateCommand.Parameters.AddWithValue("@critical_resistance_rate", talisman.CriticalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@ignore_critical_rate", talisman.IgnoreCriticalRate);
                    updateCommand.Parameters.AddWithValue("@penetration_rate", talisman.PenetrationRate);
                    updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", talisman.PenetrationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@evasion_rate", talisman.EvasionRate);
                    updateCommand.Parameters.AddWithValue("@damage_absorption_rate", talisman.DamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", talisman.IgnoreDamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", talisman.AbsorbedDamageRate);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", talisman.VitalityRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", talisman.VitalityRegenerationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@accuracy_rate", talisman.AccuracyRate);
                    updateCommand.Parameters.AddWithValue("@lifesteal_rate", talisman.LifestealRate);
                    updateCommand.Parameters.AddWithValue("@shield_strength", talisman.ShieldStrength);
                    updateCommand.Parameters.AddWithValue("@tenacity", talisman.Tenacity);
                    updateCommand.Parameters.AddWithValue("@resistance_rate", talisman.ResistanceRate);
                    updateCommand.Parameters.AddWithValue("@combo_rate", talisman.ComboRate);
                    updateCommand.Parameters.AddWithValue("@ignore_combo_rate", talisman.IgnoreComboRate);
                    updateCommand.Parameters.AddWithValue("@combo_damage_rate", talisman.ComboDamageRate);
                    updateCommand.Parameters.AddWithValue("@combo_resistance_rate", talisman.ComboResistanceRate);
                    updateCommand.Parameters.AddWithValue("@stun_rate", talisman.StunRate);
                    updateCommand.Parameters.AddWithValue("@ignore_stun_rate", talisman.IgnoreStunRate);
                    updateCommand.Parameters.AddWithValue("@reflection_rate", talisman.ReflectionRate);
                    updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", talisman.IgnoreReflectionRate);
                    updateCommand.Parameters.AddWithValue("@reflection_damage_rate", talisman.ReflectionDamageRate);
                    updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", talisman.ReflectionResistanceRate);
                    updateCommand.Parameters.AddWithValue("@mana", talisman.Mana);
                    updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", talisman.ManaRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", talisman.DamageToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", talisman.ResistanceToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", talisman.DamageToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", talisman.ResistanceToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@normal_damage_rate", talisman.NormalDamageRate);
                    updateCommand.Parameters.AddWithValue("@normal_resistance_rate", talisman.NormalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@skill_damage_rate", talisman.SkillDamageRate);
                    updateCommand.Parameters.AddWithValue("@skill_resistance_rate", talisman.SkillResistanceRate);

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
    public async Task<bool> UpdateTalismanBreakthroughAsync(Talismans talisman, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_talismans
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
                WHERE user_id = @user_id AND talisman_id = @talisman_id;
            ";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@talisman_id", talisman.Id);
                    updateCommand.Parameters.AddWithValue("@star", star);
                    updateCommand.Parameters.AddWithValue("@quantity", quantity);
                    updateCommand.Parameters.AddWithValue("@power", talisman.Power);
                    updateCommand.Parameters.AddWithValue("@health", talisman.Health);
                    updateCommand.Parameters.AddWithValue("@physical_attack", talisman.PhysicalAttack);
                    updateCommand.Parameters.AddWithValue("@physical_defense", talisman.PhysicalDefense);
                    updateCommand.Parameters.AddWithValue("@magical_attack", talisman.MagicalAttack);
                    updateCommand.Parameters.AddWithValue("@magical_defense", talisman.MagicalDefense);
                    updateCommand.Parameters.AddWithValue("@chemical_attack", talisman.ChemicalAttack);
                    updateCommand.Parameters.AddWithValue("@chemical_defense", talisman.ChemicalDefense);
                    updateCommand.Parameters.AddWithValue("@atomic_attack", talisman.AtomicAttack);
                    updateCommand.Parameters.AddWithValue("@atomic_defense", talisman.AtomicDefense);
                    updateCommand.Parameters.AddWithValue("@mental_attack", talisman.MentalAttack);
                    updateCommand.Parameters.AddWithValue("@mental_defense", talisman.MentalDefense);
                    updateCommand.Parameters.AddWithValue("@speed", talisman.Speed);
                    updateCommand.Parameters.AddWithValue("@critical_damage_rate", talisman.CriticalDamageRate);
                    updateCommand.Parameters.AddWithValue("@critical_rate", talisman.CriticalRate);
                    updateCommand.Parameters.AddWithValue("@critical_resistance_rate", talisman.CriticalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@ignore_critical_rate", talisman.IgnoreCriticalRate);
                    updateCommand.Parameters.AddWithValue("@penetration_rate", talisman.PenetrationRate);
                    updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", talisman.PenetrationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@evasion_rate", talisman.EvasionRate);
                    updateCommand.Parameters.AddWithValue("@damage_absorption_rate", talisman.DamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", talisman.IgnoreDamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", talisman.AbsorbedDamageRate);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", talisman.VitalityRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", talisman.VitalityRegenerationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@accuracy_rate", talisman.AccuracyRate);
                    updateCommand.Parameters.AddWithValue("@lifesteal_rate", talisman.LifestealRate);
                    updateCommand.Parameters.AddWithValue("@shield_strength", talisman.ShieldStrength);
                    updateCommand.Parameters.AddWithValue("@tenacity", talisman.Tenacity);
                    updateCommand.Parameters.AddWithValue("@resistance_rate", talisman.ResistanceRate);
                    updateCommand.Parameters.AddWithValue("@combo_rate", talisman.ComboRate);
                    updateCommand.Parameters.AddWithValue("@ignore_combo_rate", talisman.IgnoreComboRate);
                    updateCommand.Parameters.AddWithValue("@combo_damage_rate", talisman.ComboDamageRate);
                    updateCommand.Parameters.AddWithValue("@combo_resistance_rate", talisman.ComboResistanceRate);
                    updateCommand.Parameters.AddWithValue("@stun_rate", talisman.StunRate);
                    updateCommand.Parameters.AddWithValue("@ignore_stun_rate", talisman.IgnoreStunRate);
                    updateCommand.Parameters.AddWithValue("@reflection_rate", talisman.ReflectionRate);
                    updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", talisman.IgnoreReflectionRate);
                    updateCommand.Parameters.AddWithValue("@reflection_damage_rate", talisman.ReflectionDamageRate);
                    updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", talisman.ReflectionResistanceRate);
                    updateCommand.Parameters.AddWithValue("@mana", talisman.Mana);
                    updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", talisman.ManaRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", talisman.DamageToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", talisman.ResistanceToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", talisman.DamageToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", talisman.ResistanceToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@normal_damage_rate", talisman.NormalDamageRate);
                    updateCommand.Parameters.AddWithValue("@normal_resistance_rate", talisman.NormalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@skill_damage_rate", talisman.SkillDamageRate);
                    updateCommand.Parameters.AddWithValue("@skill_resistance_rate", talisman.SkillResistanceRate);

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
    public async Task<Talismans> GetUserTalismanByIdAsync(string user_id, string Id)
    {
        Talismans talisman = null;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"SELECT * FROM user_talismans
                             WHERE talisman_id=@id AND user_id=@user_id";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@id", Id);
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);

                    await using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            talisman = new Talismans
                            {
                                Id = reader.GetStringSafe("talisman_id"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate")
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

        return talisman;
    }
    public async Task<Talismans> SumPowerUserTalismansAsync()
    {
        Talismans sumTalismans = new Talismans();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
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
                FROM user_talismans
                WHERE user_id = @user_id;";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    await using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumTalismans.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            sumTalismans.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            sumTalismans.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDoubleSafe("total_mana");
                            sumTalismans.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            sumTalismans.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            sumTalismans.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            sumTalismans.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            sumTalismans.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            sumTalismans.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            sumTalismans.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            sumTalismans.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            sumTalismans.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            sumTalismans.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            sumTalismans.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            sumTalismans.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            sumTalismans.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            sumTalismans.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            sumTalismans.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            sumTalismans.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            sumTalismans.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            sumTalismans.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            sumTalismans.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            sumTalismans.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            sumTalismans.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            sumTalismans.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            sumTalismans.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            sumTalismans.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            sumTalismans.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            sumTalismans.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            sumTalismans.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            sumTalismans.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            sumTalismans.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            sumTalismans.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            sumTalismans.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            sumTalismans.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            sumTalismans.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            sumTalismans.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            sumTalismans.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            sumTalismans.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            sumTalismans.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            sumTalismans.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            sumTalismans.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            sumTalismans.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            sumTalismans.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            sumTalismans.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            sumTalismans.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            sumTalismans.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            sumTalismans.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            sumTalismans.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            sumTalismans.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
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

        return sumTalismans;
    }
}