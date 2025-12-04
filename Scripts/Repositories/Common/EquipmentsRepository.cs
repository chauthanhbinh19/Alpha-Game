using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class EquipmentsRepository : IEquipmentsRepository
{
    public async Task<List<string>> GetUniqueEquipmentsTypesAsync()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = @"
            SELECT DISTINCT type 
            FROM Equipments
            ORDER BY 
                type REGEXP '[0-9]+$', 
                CAST(REGEXP_SUBSTR(type, '[0-9]+$') AS UNSIGNED), 
                type;
        ";

            await using (var command = new MySqlCommand(query, connection))
            await using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    typeList.Add(reader.GetString(0));
                }
            }
        }

        return typeList;
    }
    public async Task<List<string>> GetUniqueEquipmentsIdAsync()
    {
        List<string> idList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT DISTINCT id FROM Equipments";

            await using (var command = new MySqlCommand(query, connection))
            await using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    idList.Add(reader.GetString(0));
                }
            }
        }

        return idList;
    }
    public async Task<List<Equipments>> GetEquipmentsAsync(string type, int pageSize, int offset, string rare)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = @"SELECT * FROM Equipments 
                         WHERE type = @type AND (@rare = 'All' OR rare = @rare)
                         LIMIT @limit OFFSET @offset";

            await using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@rare", rare);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);

                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Equipments equipment = new Equipments
                        {
                            Id = reader.GetString("id"),
                            Name = reader.GetString("name"),
                            Image = reader.GetString("image"),
                            Rare = reader.GetString("rare"),
                            Quality = reader.GetInt32("quality"),
                            Type = reader.GetString("type"),
                            Star = reader.GetInt32("star"),
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
                            Mana = reader.GetFloat("mana"),
                            ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                            DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                            ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                            DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                            ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                            NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                            NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                            SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                            SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                            SpecialHealth = reader.GetDouble("special_health"),
                            SpecialPhysicalAttack = reader.GetDouble("special_physical_attack"),
                            SpecialPhysicalDefense = reader.GetDouble("special_physical_defense"),
                            SpecialMagicalAttack = reader.GetDouble("special_magical_attack"),
                            SpecialMagicalDefense = reader.GetDouble("special_magical_defense"),
                            SpecialChemicalAttack = reader.GetDouble("special_chemical_attack"),
                            SpecialChemicalDefense = reader.GetDouble("special_chemical_defense"),
                            SpecialAtomicAttack = reader.GetDouble("special_atomic_attack"),
                            SpecialAtomicDefense = reader.GetDouble("special_atomic_defense"),
                            SpecialMentalAttack = reader.GetDouble("special_mental_attack"),
                            SpecialMentalDefense = reader.GetDouble("special_mental_defense"),
                            SpecialSpeed = reader.GetDouble("special_speed"),
                            Description = reader.GetString("description")
                        };

                        equipments.Add(equipment);
                    }
                }
            }
        }

        return equipments;
    }
    public async Task<int> GetEquipmentsCountAsync(string type, string rare)
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
                FROM Equipments 
                WHERE type = @type 
                AND (@rare = 'All' OR rare = @rare)";

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
    public async Task<List<Equipments>> GetEquipmentsWithCurrencyAsync(string type, int pageSize, int offset)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT e.*, c.image AS currency_image, et.price 
                FROM equipments e
                JOIN equipment_trade et ON e.id = et.equipment_id
                JOIN currencies c ON c.id = et.currency_id
                WHERE e.type = @type
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
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetString("id"),
                                Name = reader.GetString("name"),
                                Image = reader.GetString("image"),
                                Rare = reader.GetString("rare"),
                                Quality = reader.GetInt32("quality"),
                                Type = reader.GetString("type"),
                                Star = reader.GetInt32("star"),
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
                                Mana = reader.GetFloat("mana"),
                                ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                                NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                                SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                                SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                                SpecialHealth = reader.GetDouble("special_health"),
                                SpecialPhysicalAttack = reader.GetDouble("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDouble("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDouble("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDouble("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDouble("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDouble("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDouble("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDouble("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDouble("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDouble("special_mental_defense"),
                                SpecialSpeed = reader.GetDouble("special_speed"),
                                Description = reader.GetString("description"),
                                CurrencyImage = reader.GetString("currency_image"),
                                Price = reader.GetDouble("price"),
                            };

                            equipments.Add(equipment);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return equipments;
    }
    public async Task<List<string>> GetEquipmentsSetAsync(string type)
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT DISTINCT e.equipmentSet
                FROM Equipments e
                WHERE e.type = @type";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            typeList.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return typeList;
    }
    public async Task<Equipments> GetEquipmentByIdAsync(string id)
    {
        Equipments equipment = null;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM equipments WHERE id = @id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            equipment = new Equipments
                            {
                                Id = reader.GetString("id"),
                                Name = reader.GetString("name"),
                                Image = reader.GetString("image"),
                                Rare = reader.GetString("rare"),
                                Quality = reader.GetInt32("quality"),
                                Type = reader.GetString("type"),
                                Star = reader.GetInt32("star"),
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
                                Mana = reader.GetFloat("mana"),
                                ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                                NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                                SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                                SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                                SpecialHealth = reader.GetDouble("special_health"),
                                SpecialPhysicalAttack = reader.GetDouble("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDouble("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDouble("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDouble("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDouble("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDouble("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDouble("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDouble("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDouble("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDouble("special_mental_defense"),
                                SpecialSpeed = reader.GetDouble("special_speed"),
                                Description = reader.GetString("description"),
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

        return equipment;
    }
}