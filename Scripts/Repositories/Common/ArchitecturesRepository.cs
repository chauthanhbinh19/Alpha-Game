using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class ArchitecturesRepository : IArchitecturesRepository
{
    public async Task<List<string>> GetUniqueArchitecturesIdAsync()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = "SELECT DISTINCT id FROM Architectures";

                await using var command = new MySqlCommand(query, connection);
                await using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    typeList.Add(reader.GetString(0));
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return typeList;
    }
    public async Task<List<Architectures>> GetArchitecturesAsync(int pageSize, int offset, string rare)
    {
        List<Architectures> architectures = new List<Architectures>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        string query = @"
        SELECT * 
        FROM architectures 
        WHERE (@rare = 'All' OR rare = @rare)
        ORDER BY 
            architectures.name REGEXP '[0-9]+$',
            CAST(REGEXP_SUBSTR(architectures.name, '[0-9]+$') AS UNSIGNED),
            architectures.name
        LIMIT @limit OFFSET @offset";

        await using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@rare", rare);
        command.Parameters.AddWithValue("@limit", pageSize);
        command.Parameters.AddWithValue("@offset", offset);

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            Architectures architecture = new Architectures
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
                Description = reader.GetStringSafe("description")
            };

            architectures.Add(architecture);
        }

        return architectures;
    }
    public async Task<int> GetArchitecturesCountAsync(string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT COUNT(*) 
            FROM architectures 
            WHERE (@rare = 'All' OR rare = @rare)";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@rare", rare);

            object result = await command.ExecuteScalarAsync();
            count = Convert.ToInt32(result);
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return count;
    }
    public async Task<List<Architectures>> GetArchitecturesWithPriceAsync(int pageSize, int offset)
    {
        List<Architectures> architectures = new List<Architectures>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT t.*, tt.price, cu.image AS currency_image, cu.id AS currency_id
            FROM architectures t
            JOIN architecture_trade tt ON t.id = tt.architecture_id
            JOIN currencies cu ON tt.currency_id = cu.id
            ORDER BY t.name REGEXP '[0-9]+$',
                     CAST(REGEXP_SUBSTR(t.name, '[0-9]+$') AS UNSIGNED),
                     t.name
            LIMIT @limit OFFSET @offset;
        ";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Architectures architecture = new Architectures
                {
                    Id = reader.GetStringSafe("id"),
                    Name = reader.GetStringSafe("name"),
                    Rare = reader.GetStringSafe("rare"),
                    Image = reader.GetStringSafe("image"),
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

                    Currency = new Currencies
                    {
                        Id = reader.GetStringSafe("currency_id"),
                        Image = reader.GetStringSafe("currency_image"),
                        Quantity = reader.GetIntSafe("price")
                    }
                };

                architectures.Add(architecture);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return architectures;
    }
    public async Task<int> GetArchitecturesWithPriceCountAsync()
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                select count(*)
                from architectures t, architecture_trade tt, currency cu
                where t.id = tt.architecture_id and tt.currency_id = cu.id;";

                await using (var command = new MySqlCommand(query, connection))
                {
                    object result = await command.ExecuteScalarAsync();
                    count = Convert.ToInt32(result);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return count;
    }
    public async Task<Architectures> GetArchitectureByIdAsync(string id)
    {
        Architectures architecture = null;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM architectures WHERE id = @id";

                await using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            architecture = new Architectures
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
                                Description = reader.GetStringSafe("description")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return architecture;
    }
    public async Task<Architectures> SumPowerArchitecturesPercentAsync()
    {
        Architectures sumArchitectures = new Architectures();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT 
                    SUM(a.percent_all_health) AS total_percent_all_health,
                    SUM(a.percent_all_physical_attack) AS total_percent_all_physical_attack,
                    SUM(a.percent_all_physical_defense) AS total_percent_all_physical_defense,
                    SUM(a.percent_all_magical_attack) AS total_percent_all_magical_attack,
                    SUM(a.percent_all_magical_defense) AS total_percent_all_magical_defense,
                    SUM(a.percent_all_chemical_attack) AS total_percent_all_chemical_attack,
                    SUM(a.percent_all_chemical_defense) AS total_percent_all_chemical_defense,
                    SUM(a.percent_all_atomic_attack) AS total_percent_all_atomic_attack,
                    SUM(a.percent_all_atomic_defense) AS total_percent_all_atomic_defense,
                    SUM(a.percent_all_mental_attack) AS total_percent_all_mental_attack,
                    SUM(a.percent_all_mental_defense) AS total_percent_all_mental_defense
                FROM architectures a
                JOIN user_architectures ua ON a.id = ua.architecture_id
                WHERE ua.user_id = @user_id;
            ";

                await using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumArchitectures.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDoubleSafe("total_percent_all_health");
                            sumArchitectures.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_physical_attack");
                            sumArchitectures.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_physical_defense");
                            sumArchitectures.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_magical_attack");
                            sumArchitectures.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_magical_defense");
                            sumArchitectures.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_chemical_attack");
                            sumArchitectures.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_chemical_defense");
                            sumArchitectures.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_atomic_attack");
                            sumArchitectures.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_atomic_defense");
                            sumArchitectures.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("total_percent_all_mental_attack");
                            sumArchitectures.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("total_percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return sumArchitectures;
    }

}