using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class BuildingsRepository : IBuildingsRepository
{ 
    public async Task<List<string>> GetUniqueBuildingsTypesAsync()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT DISTINCT type FROM Buildings";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            using (MySqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    typeList.Add(reader.GetString(0));
                }
            }
        }

        return typeList;
    }
    public async Task<List<string>> GetUniqueBuildingsIdAsync()
    {
        List<string> idList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT DISTINCT id FROM Buildings";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            using (MySqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    idList.Add(reader.GetString(0));
                }
            }
        }

        return idList;
    }
    public async Task<List<Buildings>> GetBuildingsAsync(string type, int pageSize, int offset, string rare)
    {
        List<Buildings> Buildings = new List<Buildings>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT * FROM Buildings 
                             WHERE type = @type AND (@rare = 'All' OR rare = @rare)
                             ORDER BY Buildings.name REGEXP '[0-9]+$', 
                                      CAST(REGEXP_SUBSTR(Buildings.name, '[0-9]+$') AS UNSIGNED), 
                                      Buildings.name 
                             LIMIT @limit OFFSET @offset";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@rare", rare);
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Buildings symbol = new Buildings
                            {
                                Id = reader.GetString("id"),
                                Name = reader.GetString("name"),
                                Image = reader.GetString("image"),
                                Rare = reader.GetString("rare"),
                                Quality = reader.GetDouble("quality"),
                                Type = reader.GetString("type"),
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
                                Description = reader.GetString("description")
                            };

                            Buildings.Add(symbol);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return Buildings;
    }
    public async Task<int> GetBuildingsCountAsync(string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = "SELECT COUNT(*) FROM Buildings WHERE type = @type AND (@rare = 'All' OR rare = @rare)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@rare", rare);

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
    public async Task<List<Buildings>> GetBuildingsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Buildings> Buildings = new List<Buildings>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT s.*, st.price, cu.image AS currency_image, cu.id AS currency_id
                FROM Buildings s
                JOIN Building_trade st ON s.id = st.Building_id
                JOIN currencies cu ON st.currency_id = cu.id
                WHERE s.type = @type
                ORDER BY s.name REGEXP '[0-9]+$',
                         CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED),
                         s.name
                LIMIT @limit OFFSET @offset";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Buildings symbol = new Buildings
                            {
                                Id = reader.GetString("id"),
                                Name = reader.GetString("name"),
                                Rare = reader.GetString("rare"),
                                Image = reader.GetString("image"),
                                Type = reader.GetString("type"),
                                Quality = reader.GetDouble("quality"),
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
                                Description = reader.GetString("description")
                            };

                            symbol.Currency = new Currencies
                            {
                                Id = reader.GetString("currency_id"),
                                Image = reader.GetString("currency_image"),
                                Quantity = reader.GetInt32("price")
                            };

                            Buildings.Add(symbol);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return Buildings;
    }
    public async Task<int> GetBuildingsWithPriceCountAsync(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT COUNT(*)
                FROM Buildings s
                JOIN Building_trade st ON s.id = st.Building_id
                JOIN currencies cu ON st.currency_id = cu.id
                WHERE s.type = @type;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);
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
    public async Task<Buildings> GetBuildingByIdAsync(string Id)
    {
        Buildings symbol = new Buildings();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Buildings WHERE id=@id";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            symbol = new Buildings
                            {
                                Id = reader.GetString("id"),
                                Name = reader.GetString("name"),
                                Image = reader.GetString("image"),
                                Quality = reader.GetDouble("quality"),
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
                                Description = reader.GetString("description")
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

        return symbol;
    }
    public async Task<Buildings> SumPowerBuildingsPercentAsync()
    {
        Buildings sumBuildings = new Buildings();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
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
                FROM Buildings a
                JOIN user_Buildings ua ON a.id = ua.Building_id
                WHERE ua.user_id = @user_id;
            ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumBuildings.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDouble("total_percent_all_health");
                            sumBuildings.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDouble("total_percent_all_physical_attack");
                            sumBuildings.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDouble("total_percent_all_physical_defense");
                            sumBuildings.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDouble("total_percent_all_magical_attack");
                            sumBuildings.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDouble("total_percent_all_magical_defense");
                            sumBuildings.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDouble("total_percent_all_chemical_attack");
                            sumBuildings.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDouble("total_percent_all_chemical_defense");
                            sumBuildings.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDouble("total_percent_all_atomic_attack");
                            sumBuildings.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDouble("total_percent_all_atomic_defense");
                            sumBuildings.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDouble("total_percent_all_mental_attack");
                            sumBuildings.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDouble("total_percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return sumBuildings;
    }
}