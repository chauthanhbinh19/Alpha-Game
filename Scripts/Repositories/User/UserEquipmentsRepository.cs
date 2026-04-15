using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;

public class UserEquipmentsRepository : IUserEquipmentsRepository
{
    public async Task<List<Equipments>> GetUserEquipmentsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT e.id, e.name, ue.*, e.image, e.rare, e.type
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                WHERE ue.user_id = @userId";

                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    query += " AND e.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    query += " AND e.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND e.name LIKE CONCAT('%', @search, '%')";
                }

                query += @"LIMIT @limit OFFSET @offset";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
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
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Quality = reader.GetDoubleSafe("quality"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),

                                BaseStats = new BaseStats
                                {
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
                                }
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<List<Equipments>> GetUserAllEquipmentsAsync(string user_id)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT e.id, e.name, ue.*, e.image, e.rare, e.type
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                WHERE ue.user_id = @userId;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Quality = reader.GetDoubleSafe("quality"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),

                                BaseStats = new BaseStats
                                {
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
                                }
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<int> GetUserEquipmentsCountAsync(string user_id, string search, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT COUNT(*)
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                WHERE ue.user_id = @userId
            ";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    query += " AND e.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    query += " AND e.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND e.name LIKE CONCAT('%', @search, '%')";
                }

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
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
    public async Task<Equipments> GetUserEquipmentsByIdAsync(string user_id, string Id)
    {
        Equipments equipment = new Equipments();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT * 
                             FROM user_equipments 
                             WHERE equipment_id = @id AND user_id = @user_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@user_id", user_id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),

                                BaseStats = new BaseStats
                                {
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
                                }
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

        return equipment;
    }
    public async Task<bool> BuyEquipmentAsync(string Id, Equipments equipmentFromDB, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string existsQuery = @"
                SELECT COUNT(*) 
                FROM user_equipments 
                WHERE user_id = @user_id AND equipment_id = @equipment_id";

                await using (MySqlCommand existsCmd = new MySqlCommand(existsQuery, connection))
                {
                    existsCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    existsCmd.Parameters.AddWithValue("@equipment_id", Id);

                    var existingCount = Convert.ToInt32(await existsCmd.ExecuteScalarAsync());
                    if (existingCount > 0)
                    {
                        string updateQuery = @"
                        UPDATE user_equipments
                        SET quality = quality + @quantity
                        WHERE user_id = @user_id AND equipment_id = @equipment_id";

                        await using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCmd.Parameters.AddWithValue("@equipment_id", Id);
                            updateCmd.Parameters.AddWithValue("@quantity", quantity);
                            await updateCmd.ExecuteNonQueryAsync();
                        }

                        return true;
                    }
                }

                string insertQuery = @"
                INSERT INTO user_equipments (
                    user_id, equipment_id, rare, level, experiment, star, quality, block, quantity,
                    power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                    chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                    speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                    penetration_rate, penetration_resistance_rate, evasion_rate, damage_absorption_rate, 
                    ignore_damage_absorption_rate, absorbed_damage_rate, vitality_regeneration_rate, 
                    vitality_regeneration_resistance_rate, accuracy_rate, lifesteal_rate, shield_strength, tenacity,
                    resistance_rate, combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                    stun_rate, ignore_stun_rate, reflection_rate, ignore_reflection_rate, reflection_damage_rate,
                    reflection_resistance_rate, mana, mana_regeneration_rate, damage_to_different_faction_rate,
                    resistance_to_different_faction_rate, damage_to_same_faction_rate, resistance_to_same_faction_rate,
                    normal_damage_rate, normal_resistance_rate, skill_damage_rate, skill_resistance_rate,
                    special_health, special_physical_attack, special_physical_defense, special_magical_attack,
                    special_magical_defense, special_chemical_attack, special_chemical_defense, special_atomic_attack,
                    special_atomic_defense, special_mental_attack, special_mental_defense, special_speed
                ) VALUES (
                    @user_id, @equipment_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
                    @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                    @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                    @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                    @penetration_rate, @penetration_resistance_rate, @evasion_rate, @damage_absorption_rate, 
                    @ignore_damage_absorption_rate, @absorbed_damage_rate, @vitality_regeneration_rate, 
                    @vitality_regeneration_resistance_rate, @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity,
                    @resistance_rate, @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                    @stun_rate, @ignore_stun_rate, @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate,
                    @reflection_resistance_rate, @mana, @mana_regeneration_rate, @damage_to_different_faction_rate,
                    @resistance_to_different_faction_rate, @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                    @normal_damage_rate, @normal_resistance_rate, @skill_damage_rate, @skill_resistance_rate,
                    @special_health, @special_physical_attack, @special_physical_defense, @special_magical_attack,
                    @special_magical_defense, @special_chemical_attack, @special_chemical_defense, @special_atomic_attack,
                    @special_atomic_defense, @special_mental_attack, @special_mental_defense, @special_speed
                );";

                await using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                {
                    insertCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    insertCmd.Parameters.AddWithValue("@equipment_id", Id);
                    insertCmd.Parameters.AddWithValue("@rare", equipmentFromDB.Rare);
                    insertCmd.Parameters.AddWithValue("@level", 0);
                    insertCmd.Parameters.AddWithValue("@experiment", 0);
                    insertCmd.Parameters.AddWithValue("@star", 0);
                    insertCmd.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(equipmentFromDB.Rare));
                    insertCmd.Parameters.AddWithValue("@block", false);
                    insertCmd.Parameters.AddWithValue("@quantity", quantity);
                    insertCmd.Parameters.AddWithValue("@power", equipmentFromDB.Power);
                    insertCmd.Parameters.AddWithValue("@health", equipmentFromDB.Health);
                    insertCmd.Parameters.AddWithValue("@physical_attack", equipmentFromDB.PhysicalAttack);
                    insertCmd.Parameters.AddWithValue("@physical_defense", equipmentFromDB.PhysicalDefense);
                    insertCmd.Parameters.AddWithValue("@magical_attack", equipmentFromDB.MagicalAttack);
                    insertCmd.Parameters.AddWithValue("@magical_defense", equipmentFromDB.MagicalDefense);
                    insertCmd.Parameters.AddWithValue("@chemical_attack", equipmentFromDB.ChemicalAttack);
                    insertCmd.Parameters.AddWithValue("@chemical_defense", equipmentFromDB.ChemicalDefense);
                    insertCmd.Parameters.AddWithValue("@atomic_attack", equipmentFromDB.AtomicAttack);
                    insertCmd.Parameters.AddWithValue("@atomic_defense", equipmentFromDB.AtomicDefense);
                    insertCmd.Parameters.AddWithValue("@mental_attack", equipmentFromDB.MentalAttack);
                    insertCmd.Parameters.AddWithValue("@mental_defense", equipmentFromDB.MentalDefense);
                    insertCmd.Parameters.AddWithValue("@speed", equipmentFromDB.Speed);
                    insertCmd.Parameters.AddWithValue("@critical_damage_rate", equipmentFromDB.CriticalDamageRate);
                    insertCmd.Parameters.AddWithValue("@critical_rate", equipmentFromDB.CriticalRate);
                    insertCmd.Parameters.AddWithValue("@critical_resistance_rate", equipmentFromDB.CriticalResistanceRate);
                    insertCmd.Parameters.AddWithValue("@ignore_critical_rate", equipmentFromDB.IgnoreCriticalRate);
                    insertCmd.Parameters.AddWithValue("@penetration_rate", equipmentFromDB.PenetrationRate);
                    insertCmd.Parameters.AddWithValue("@penetration_resistance_rate", equipmentFromDB.PenetrationResistanceRate);
                    insertCmd.Parameters.AddWithValue("@evasion_rate", equipmentFromDB.EvasionRate);
                    insertCmd.Parameters.AddWithValue("@damage_absorption_rate", equipmentFromDB.DamageAbsorptionRate);
                    insertCmd.Parameters.AddWithValue("@ignore_damage_absorption_rate", equipmentFromDB.IgnoreDamageAbsorptionRate);
                    insertCmd.Parameters.AddWithValue("@absorbed_damage_rate", equipmentFromDB.AbsorbedDamageRate);
                    insertCmd.Parameters.AddWithValue("@vitality_regeneration_rate", equipmentFromDB.VitalityRegenerationRate);
                    insertCmd.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", equipmentFromDB.VitalityRegenerationResistanceRate);
                    insertCmd.Parameters.AddWithValue("@accuracy_rate", equipmentFromDB.AccuracyRate);
                    insertCmd.Parameters.AddWithValue("@lifesteal_rate", equipmentFromDB.LifestealRate);
                    insertCmd.Parameters.AddWithValue("@shield_strength", equipmentFromDB.ShieldStrength);
                    insertCmd.Parameters.AddWithValue("@tenacity", equipmentFromDB.Tenacity);
                    insertCmd.Parameters.AddWithValue("@resistance_rate", equipmentFromDB.ResistanceRate);
                    insertCmd.Parameters.AddWithValue("@combo_rate", equipmentFromDB.ComboRate);
                    insertCmd.Parameters.AddWithValue("@ignore_combo_rate", equipmentFromDB.IgnoreComboRate);
                    insertCmd.Parameters.AddWithValue("@combo_damage_rate", equipmentFromDB.ComboDamageRate);
                    insertCmd.Parameters.AddWithValue("@combo_resistance_rate", equipmentFromDB.ComboResistanceRate);
                    insertCmd.Parameters.AddWithValue("@stun_rate", equipmentFromDB.StunRate);
                    insertCmd.Parameters.AddWithValue("@ignore_stun_rate", equipmentFromDB.IgnoreStunRate);
                    insertCmd.Parameters.AddWithValue("@reflection_rate", equipmentFromDB.ReflectionRate);
                    insertCmd.Parameters.AddWithValue("@ignore_reflection_rate", equipmentFromDB.IgnoreReflectionRate);
                    insertCmd.Parameters.AddWithValue("@reflection_damage_rate", equipmentFromDB.ReflectionDamageRate);
                    insertCmd.Parameters.AddWithValue("@reflection_resistance_rate", equipmentFromDB.ReflectionResistanceRate);
                    insertCmd.Parameters.AddWithValue("@mana", equipmentFromDB.Mana);
                    insertCmd.Parameters.AddWithValue("@mana_regeneration_rate", equipmentFromDB.ManaRegenerationRate);
                    insertCmd.Parameters.AddWithValue("@damage_to_different_faction_rate", equipmentFromDB.DamageToDifferentFactionRate);
                    insertCmd.Parameters.AddWithValue("@resistance_to_different_faction_rate", equipmentFromDB.ResistanceToDifferentFactionRate);
                    insertCmd.Parameters.AddWithValue("@damage_to_same_faction_rate", equipmentFromDB.DamageToSameFactionRate);
                    insertCmd.Parameters.AddWithValue("@resistance_to_same_faction_rate", equipmentFromDB.ResistanceToSameFactionRate);
                    insertCmd.Parameters.AddWithValue("@normal_damage_rate", equipmentFromDB.NormalDamageRate);
                    insertCmd.Parameters.AddWithValue("@normal_resistance_rate", equipmentFromDB.NormalResistanceRate);
                    insertCmd.Parameters.AddWithValue("@skill_damage_rate", equipmentFromDB.SkillDamageRate);
                    insertCmd.Parameters.AddWithValue("@skill_resistance_rate", equipmentFromDB.SkillResistanceRate);
                    insertCmd.Parameters.AddWithValue("@special_health", equipmentFromDB.SpecialHealth);
                    insertCmd.Parameters.AddWithValue("@special_physical_attack", equipmentFromDB.SpecialPhysicalAttack);
                    insertCmd.Parameters.AddWithValue("@special_physical_defense", equipmentFromDB.SpecialPhysicalDefense);
                    insertCmd.Parameters.AddWithValue("@special_magical_attack", equipmentFromDB.SpecialMagicalAttack);
                    insertCmd.Parameters.AddWithValue("@special_magical_defense", equipmentFromDB.SpecialMagicalDefense);
                    insertCmd.Parameters.AddWithValue("@special_chemical_attack", equipmentFromDB.SpecialChemicalAttack);
                    insertCmd.Parameters.AddWithValue("@special_chemical_defense", equipmentFromDB.SpecialChemicalDefense);
                    insertCmd.Parameters.AddWithValue("@special_atomic_attack", equipmentFromDB.SpecialAtomicAttack);
                    insertCmd.Parameters.AddWithValue("@special_atomic_defense", equipmentFromDB.SpecialAtomicDefense);
                    insertCmd.Parameters.AddWithValue("@special_mental_attack", equipmentFromDB.SpecialMentalAttack);
                    insertCmd.Parameters.AddWithValue("@special_mental_defense", equipmentFromDB.SpecialMentalDefense);
                    insertCmd.Parameters.AddWithValue("@special_speed", equipmentFromDB.SpecialSpeed);

                    await insertCmd.ExecuteNonQueryAsync();
                }

                return true;
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
    }
    public async Task<bool> UpdateEquipmentsLevelAsync(Equipments equipment, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_equipments
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
                WHERE user_id = @user_id AND equipment_id = @equipment_id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@equipment_id", equipment.Id);
                    command.Parameters.AddWithValue("@level", cardLevel);
                    command.Parameters.AddWithValue("@power", equipment.Power);
                    command.Parameters.AddWithValue("@health", equipment.Health);
                    command.Parameters.AddWithValue("@physical_attack", equipment.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", equipment.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", equipment.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", equipment.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", equipment.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", equipment.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", equipment.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", equipment.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", equipment.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", equipment.MentalDefense);
                    command.Parameters.AddWithValue("@speed", equipment.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", equipment.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", equipment.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", equipment.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", equipment.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", equipment.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", equipment.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", equipment.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", equipment.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", equipment.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", equipment.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", equipment.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", equipment.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", equipment.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", equipment.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", equipment.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", equipment.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", equipment.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", equipment.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", equipment.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", equipment.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", equipment.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", equipment.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", equipment.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", equipment.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", equipment.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", equipment.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", equipment.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", equipment.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", equipment.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", equipment.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", equipment.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", equipment.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", equipment.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", equipment.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", equipment.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", equipment.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", equipment.SkillResistanceRate);

                    await command.ExecuteNonQueryAsync();
                }

                return true;
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
    }
    public async Task<bool> UpdateEquipmentsBreakthroughAsync(Equipments equipment, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_equipments
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
                WHERE user_id = @user_id AND equipment_id = @equipment_id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@equipment_id", equipment.Id);
                    command.Parameters.AddWithValue("@star", star);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@power", equipment.Power);
                    command.Parameters.AddWithValue("@health", equipment.Health);
                    command.Parameters.AddWithValue("@physical_attack", equipment.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", equipment.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", equipment.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", equipment.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", equipment.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", equipment.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", equipment.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", equipment.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", equipment.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", equipment.MentalDefense);
                    command.Parameters.AddWithValue("@speed", equipment.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", equipment.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", equipment.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", equipment.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", equipment.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", equipment.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", equipment.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", equipment.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", equipment.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", equipment.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", equipment.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", equipment.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", equipment.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", equipment.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", equipment.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", equipment.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", equipment.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", equipment.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", equipment.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", equipment.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", equipment.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", equipment.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", equipment.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", equipment.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", equipment.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", equipment.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", equipment.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", equipment.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", equipment.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", equipment.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", equipment.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", equipment.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", equipment.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", equipment.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", equipment.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", equipment.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", equipment.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", equipment.SkillResistanceRate);

                    await command.ExecuteNonQueryAsync();
                }

                return true;
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
    }
    public async Task UpdateUserCurrencyAsync(string Id, double amount)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Lấy currency_id
                string query = @"SELECT et.currency_id 
                             FROM equipments e
                             JOIN equipment_trade et ON e.id = et.equipment_id
                             WHERE e.id = @id";
                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        int currencyId = 0;

                        if (await reader.ReadAsync())
                        {
                            currencyId = reader.GetIntSafe("currency_id");
                        }

                        await reader.CloseAsync();

                        // Lấy quantity hiện tại
                        query = "SELECT quantity FROM user_currencies WHERE user_id = @user_id AND currency_id = @currency_id";
                        await using (MySqlCommand cmd2 = new MySqlCommand(query, connection))
                        {
                            cmd2.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            cmd2.Parameters.AddWithValue("@currency_id", currencyId);

                            object result = await cmd2.ExecuteScalarAsync();
                            double currentQuantity = result != DBNull.Value && result != null ? Convert.ToDouble(result) : 0;
                            double newQuantity = currentQuantity - amount;

                            // Cập nhật quantity mới
                            query = "UPDATE user_currencies SET quantity=@quantity WHERE user_id=@user_id AND currency_id=@currency_id";
                            await using (MySqlCommand cmd3 = new MySqlCommand(query, connection))
                            {
                                cmd3.Parameters.AddWithValue("@quantity", newQuantity);
                                cmd3.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                                cmd3.Parameters.AddWithValue("@currency_id", currencyId);

                                await cmd3.ExecuteNonQueryAsync();
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
    public async Task InsertCardHeroEquipmentsAsync(string Id, Equipments equipment, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem equipment_id có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_heroes_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteQuery = @"DELETE FROM card_heroes_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertQuery = @"INSERT INTO card_heroes_equipment 
                                       (user_id, card_hero_id, equipment_id, position)
                                       VALUES (@user_id, @card_hero_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@card_hero_id", Id);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);

                        await insertCommand.ExecuteNonQueryAsync();
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
    public async Task InsertCardCaptainEquipmentsAsync(string Id, Equipments equipment, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem equipment_id có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_captains_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteQuery = @"DELETE FROM card_captains_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertQuery = @"INSERT INTO card_captains_equipment 
                                       (user_id, card_captain_id, equipment_id, position)
                                       VALUES (@user_id, @card_captain_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@card_captain_id", Id);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);

                        await insertCommand.ExecuteNonQueryAsync();
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
    public async Task InsertCardColonelEquipmentsAsync(string Id, Equipments equipment, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem equipment_id   có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_colonels_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteQuery = @"DELETE FROM card_colonels_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertQuery = @"INSERT INTO card_colonels_equipment 
                                       (user_id, card_colonel_id, equipment_id, position)
                                       VALUES (@user_id, @card_colonel_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@card_colonel_id", Id);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);

                        await insertCommand.ExecuteNonQueryAsync();
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
    public async Task InsertCardGeneralEquipmentsAsync(string Id, Equipments equipment, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem equipment_id   có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_generals_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteQuery = @"DELETE FROM card_generals_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertQuery = @"INSERT INTO card_generals_equipment 
                                       (user_id, card_general_id, equipment_id, position)
                                       VALUES (@user_id, @card_general_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@card_general_id", Id);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);

                        await insertCommand.ExecuteNonQueryAsync();
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
    public async Task InsertCardAdmiralEquipmentsAsync(string Id, Equipments equipment, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem equipment_id   có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_admirals_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteQuery = @"DELETE FROM card_admirals_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertQuery = @"INSERT INTO card_admirals_equipment 
                                       (user_id, card_admiral_id, equipment_id, position)
                                       VALUES (@user_id, @card_admiral_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@card_admiral_id", Id);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);

                        await insertCommand.ExecuteNonQueryAsync();
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
    public async Task InsertCardMonsterEquipmentsAsync(string Id, Equipments equipment, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem equipment_id   có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_monsters_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteQuery = @"DELETE FROM card_monsters_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertQuery = @"INSERT INTO card_monsters_equipment 
                                       (user_id, card_monster_id, equipment_id, position)
                                       VALUES (@user_id, @card_monster_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@card_monster_id", Id);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);

                        await insertCommand.ExecuteNonQueryAsync();
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
    public async Task InsertCardMilitaryEquipmentsAsync(string Id, Equipments equipment, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem equipment_id   có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_military_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteQuery = @"DELETE FROM card_military_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertQuery = @"INSERT INTO card_military_equipment 
                                       (user_id, card_military_id, equipment_id, position)
                                       VALUES (@user_id, @card_military_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@card_military_id", Id);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);

                        await insertCommand.ExecuteNonQueryAsync();
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
    public async Task InsertCardSpellEquipmentsAsync(string Id, Equipments equipment, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem equipment_id   có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_spell_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteQuery = @"DELETE FROM card_spell_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertQuery = @"INSERT INTO card_spell_equipment 
                                       (user_id, card_spell_id, equipment_id, position)
                                       VALUES (@user_id, @card_spell_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@card_spell_id", Id);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);

                        await insertCommand.ExecuteNonQueryAsync();
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
    public async Task InsertBookEquipmentsAsync(string Id, Equipments equipment, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem equipment_id   có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM books_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteQuery = @"DELETE FROM books_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertQuery = @"INSERT INTO books_equipment 
                                       (user_id, book_id, equipment_id, position)
                                       VALUES (@user_id, @book_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@book_id", Id);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);

                        await insertCommand.ExecuteNonQueryAsync();
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
    public async Task InsertPetEquipmentsAsync(string Id, Equipments equipment, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem equipment_id   có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM pets_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteQuery = @"DELETE FROM pets_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertQuery = @"INSERT INTO pets_equipment 
                                       (user_id, pet_id, equipment_id, position)
                                       VALUES (@user_id, @pet_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@pet_id", Id);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);

                        await insertCommand.ExecuteNonQueryAsync();
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
    public async Task<List<Equipments>> GetCardHeroesEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_heroes_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.card_hero_id = @card_hero_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_hero_id", card_id);
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position"),
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<List<Equipments>> GetCardCaptainsEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_captains_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.card_captain_id = @card_captain_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_captain_id", card_id);
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position")
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<List<Equipments>> GetCardColonelsEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_colonels_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.card_colonel_id = @card_colonel_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_colonel_id", card_id);
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position")
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<List<Equipments>> GetCardGeneralsEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_generals_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.card_general_id = @card_general_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_general_id", card_id);
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position")
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<List<Equipments>> GetCardAdmiralsEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_admirals_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.card_admiral_id = @card_admiral_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_admiral_id", card_id);
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position")
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<List<Equipments>> GetCardMonstersEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_monsters_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.card_monster_id = @card_monster_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_monster_id", card_id);
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position")
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<List<Equipments>> GetCardMilitariesEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_military_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.card_military_id = @card_military_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_military_id", card_id);
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position")
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
    public async Task<List<Equipments>> GetCardSpellsEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_spell_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.card_spell_id = @card_spell_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_spell_id", card_id);
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position")
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<List<Equipments>> GetBooksEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_books_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.book_id = @book_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@book_id", card_id);
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position")
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<List<Equipments>> GetPetsEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_pets_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.pet_id = @pet_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@pet_id", card_id);
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position")
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<List<Equipments>> GetAllCardHeroesEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT 
                    e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet,
                    CASE WHEN che.equipment_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS STATUS
                FROM equipments e
                LEFT JOIN user_equipments ue ON e.id = ue.equipment_id
                LEFT JOIN card_heroes_equipment che 
                    ON che.equipment_id = ue.equipment_id 
                    AND che.user_id = ue.user_id
                WHERE ue.user_id = @user_id 
                    AND e.type = @type
                    AND (@status = 'ALL' 
                         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
                         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL))
                LIMIT @limit OFFSET @offset;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@limit", limit);
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("equipment_id")) ? null : reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position"),
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<List<Equipments>> GetAllCardCaptainsEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT 
                    e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet,
                    CASE WHEN che.equipment_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS STATUS
                FROM equipments e
                LEFT JOIN user_equipments ue ON e.id = ue.equipment_id
                LEFT JOIN card_captains_equipment che 
                    ON che.equipment_id = ue.equipment_id 
                    AND che.user_id = ue.user_id
                WHERE ue.user_id = @user_id 
                    AND e.type = @type
                    AND (@status = 'ALL' 
                         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
                         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL))
                LIMIT @limit OFFSET @offset;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@limit", limit);
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("equipment_id")) ? null : reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position"),
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<List<Equipments>> GetAllCardColonelsEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, 
                       CASE WHEN che.equipment_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS STATUS
                FROM equipments e
                LEFT JOIN user_equipments ue ON e.id = ue.equipment_id
                LEFT JOIN card_colonels_equipment che 
                    ON che.equipment_id = ue.equipment_id 
                   AND che.user_id = ue.user_id
                WHERE ue.user_id = @user_id 
                  AND e.type = @type 
                  AND (@status = 'ALL' 
                       OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
                       OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL))
                LIMIT @limit OFFSET @offset";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@limit", limit);
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position"),
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<List<Equipments>> GetAllCardGeneralsEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, 
                       CASE WHEN che.equipment_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS STATUS
                FROM equipments e
                LEFT JOIN user_equipments ue ON e.id = ue.equipment_id
                LEFT JOIN card_generals_equipment che 
                    ON che.equipment_id = ue.equipment_id 
                   AND che.user_id = ue.user_id
                WHERE ue.user_id = @user_id 
                  AND e.type = @type 
                  AND (@status = 'ALL' 
                       OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
                       OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL))
                LIMIT @limit OFFSET @offset";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@limit", limit);
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position"),
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
    public async Task<List<Equipments>> GetAllCardAdmiralsEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, 
                       CASE WHEN che.equipment_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS STATUS
                FROM equipments e
                LEFT JOIN user_equipments ue ON e.id = ue.equipment_id
                LEFT JOIN card_admirals_equipment che 
                    ON che.equipment_id = ue.equipment_id 
                   AND che.user_id = ue.user_id
                WHERE ue.user_id = @user_id 
                  AND e.type = @type 
                  AND (@status = 'ALL' 
                       OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
                       OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL))
                LIMIT @limit OFFSET @offset";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@limit", limit);
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position"),
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<List<Equipments>> GetAllCardMonstersEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, 
                       CASE WHEN che.equipment_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS STATUS
                FROM equipments e
                LEFT JOIN user_equipments ue ON e.id = ue.equipment_id
                LEFT JOIN card_monsters_equipment che 
                    ON che.equipment_id = ue.equipment_id 
                   AND che.user_id = ue.user_id
                WHERE ue.user_id = @user_id 
                  AND e.type = @type 
                  AND (@status = 'ALL' 
                       OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
                       OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL))
                LIMIT @limit OFFSET @offset";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@limit", limit);
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position"),
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<List<Equipments>> GetAllCardMilitariesEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, 
                       CASE WHEN che.equipment_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS STATUS
                FROM equipments e
                LEFT JOIN user_equipments ue ON e.id = ue.equipment_id
                LEFT JOIN card_military_equipment che 
                    ON che.equipment_id = ue.equipment_id 
                   AND che.user_id = ue.user_id
                WHERE ue.user_id = @user_id 
                  AND e.type = @type 
                  AND (@status = 'ALL' 
                       OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
                       OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL))
                LIMIT @limit OFFSET @offset";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@limit", limit);
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position"),
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<List<Equipments>> GetAllCardSpellsEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, 
                       CASE WHEN che.equipment_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS STATUS
                FROM equipments e
                LEFT JOIN user_equipments ue ON e.id = ue.equipment_id
                LEFT JOIN card_spell_equipment che 
                    ON che.equipment_id = ue.equipment_id 
                   AND che.user_id = ue.user_id
                WHERE ue.user_id = @user_id 
                  AND e.type = @type 
                  AND (@status = 'ALL' 
                       OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
                       OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL))
                LIMIT @limit OFFSET @offset";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@limit", limit);
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position"),
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<List<Equipments>> GetAllBooksEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, 
                       CASE WHEN che.equipment_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS STATUS
                FROM equipments e
                LEFT JOIN user_equipments ue ON e.id = ue.equipment_id
                LEFT JOIN books_equipment che 
                    ON che.equipment_id = ue.equipment_id 
                   AND che.user_id = ue.user_id
                WHERE ue.user_id = @user_id 
                  AND e.type = @type 
                  AND (@status = 'ALL' 
                       OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
                       OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL))
                LIMIT @limit OFFSET @offset";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@limit", limit);
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position"),
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public async Task<List<Equipments>> GetAllPetsEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipments = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, 
                       CASE WHEN che.equipment_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS STATUS
                FROM equipments e
                LEFT JOIN user_equipments ue ON e.id = ue.equipment_id
                LEFT JOIN pets_equipment che 
                    ON che.equipment_id = ue.equipment_id 
                   AND che.user_id = ue.user_id
                WHERE ue.user_id = @user_id 
                  AND e.type = @type 
                  AND (@status = 'ALL' 
                       OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
                       OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL))
                LIMIT @limit OFFSET @offset";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@limit", limit);
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments equipment = new Equipments
                            {
                                Id = reader.GetStringSafe("equipment_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Set = reader.GetStringSafe("equipmentSet"),
                                Level = reader.GetIntSafe("level"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.GetDoubleSafe("special_speed"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetIntSafe("position"),
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
            finally
            {
                await connection.CloseAsync();
            }
        }

        return equipments;
    }
    public Equipments ChangeValueToZero(Equipments equipments)
    {
        equipments.Power = 0;
        equipments.Health = 0;
        equipments.PhysicalAttack = 0;
        equipments.PhysicalDefense = 0;
        equipments.MagicalAttack = 0;
        equipments.MagicalDefense = 0;
        equipments.ChemicalAttack = 0;
        equipments.ChemicalDefense = 0;
        equipments.AtomicAttack = 0;
        equipments.AtomicDefense = 0;
        equipments.MentalAttack = 0;
        equipments.MentalDefense = 0;
        equipments.Speed = 0;
        equipments.CriticalDamageRate = 0;
        equipments.CriticalRate = 0;
        equipments.PenetrationRate = 0;
        equipments.EvasionRate = 0;
        equipments.DamageAbsorptionRate = 0;
        equipments.VitalityRegenerationRate = 0;
        equipments.AccuracyRate = 0;
        equipments.LifestealRate = 0;
        equipments.ShieldStrength = 0;
        equipments.Tenacity = 0;
        equipments.ResistanceRate = 0;
        equipments.ComboRate = 0;
        equipments.ReflectionRate = 0;
        equipments.Mana = 0;
        equipments.ManaRegenerationRate = 0;
        equipments.DamageToDifferentFactionRate = 0;
        equipments.ResistanceToDifferentFactionRate = 0;
        equipments.DamageToSameFactionRate = 0;
        equipments.ResistanceToSameFactionRate = 0;
        equipments.SpecialHealth = 0;
        equipments.SpecialPhysicalAttack = 0;
        equipments.SpecialPhysicalDefense = 0;
        equipments.SpecialMagicalAttack = 0;
        equipments.SpecialMagicalDefense = 0;
        equipments.SpecialChemicalAttack = 0;
        equipments.SpecialChemicalDefense = 0;
        equipments.SpecialAtomicAttack = 0;
        equipments.SpecialAtomicDefense = 0;
        equipments.SpecialMentalAttack = 0;
        equipments.SpecialMentalDefense = 0;
        equipments.SpecialSpeed = 0;
        return equipments;
    }
    public async Task<Equipments> GetAllEquipmentsByCardHeroIdAsync(string user_id, string cardHeroId)
    {
        Equipments equipment = new Equipments();
        equipment = ChangeValueToZero(equipment); // reset tất cả giá trị về 0
        List<Equipments> equipments = new List<Equipments>();

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT ue.*
                FROM user_card_heroes uc
                JOIN card_heroes c ON uc.card_hero_id = c.id
                JOIN card_heroes_equipment che ON uc.card_hero_id = che.card_hero_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.card_hero_id = @card_hero_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_hero_id", cardHeroId);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments tmpEquipments = new Equipments
                            {
                                Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDoubleSafe("power"),
                                Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDoubleSafe("health"),
                                PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDoubleSafe("physical_attack"),
                                PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDoubleSafe("physical_defense"),
                                MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDoubleSafe("magical_attack"),
                                MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDoubleSafe("magical_defense"),
                                ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDoubleSafe("chemical_attack"),
                                ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDoubleSafe("chemical_defense"),
                                AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDoubleSafe("atomic_attack"),
                                AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDoubleSafe("atomic_defense"),
                                MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDoubleSafe("mental_attack"),
                                MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDoubleSafe("mental_defense"),
                                Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDoubleSafe("speed"),
                                CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDoubleSafe("critical_damage_rate"),
                                CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDoubleSafe("critical_rate"),
                                CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("critical_resistance_rate"),
                                IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("ignore_critical_rate"),
                                PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDoubleSafe("penetration_rate"),
                                PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("penetration_resistance_rate"),
                                EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDoubleSafe("evasion_rate"),
                                DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("damage_absorption_rate"),
                                IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                                AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("absorbed_damage_rate"),
                                VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_rate"),
                                VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                                AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDoubleSafe("accuracy_rate"),
                                LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDoubleSafe("lifesteal_rate"),
                                ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDoubleSafe("shield_strength"),
                                Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDoubleSafe("tenacity"),
                                ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDoubleSafe("resistance_rate"),
                                ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDoubleSafe("combo_rate"),
                                IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("ignore_combo_rate"),
                                ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDoubleSafe("combo_damage_rate"),
                                ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("combo_resistance_rate"),
                                StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDoubleSafe("stun_rate"),
                                IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("ignore_stun_rate"),
                                ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDoubleSafe("reflection_rate"),
                                IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("ignore_reflection_rate"),
                                ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("reflection_damage_rate"),
                                ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("reflection_resistance_rate"),
                                Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetDoubleSafe("mana"),
                                ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDoubleSafe("normal_damage_rate"),
                                NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("normal_resistance_rate"),
                                SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDoubleSafe("skill_damage_rate"),
                                SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDoubleSafe("special_speed")
                            };

                            equipments.Add(tmpEquipments);
                        }
                        foreach (Equipments e in equipments)
                        {
                            equipment.Power += e.Power;
                            equipment.Health += e.Health;
                            equipment.PhysicalAttack += e.PhysicalAttack;
                            equipment.PhysicalDefense += e.PhysicalDefense;
                            equipment.MagicalAttack += e.MagicalAttack;
                            equipment.MagicalDefense += e.MagicalDefense;
                            equipment.ChemicalAttack += e.ChemicalAttack;
                            equipment.ChemicalDefense += e.ChemicalDefense;
                            equipment.AtomicAttack += e.AtomicAttack;
                            equipment.AtomicDefense += e.AtomicDefense;
                            equipment.MentalAttack += e.MentalAttack;
                            equipment.MentalDefense += e.MentalDefense;
                            equipment.Speed += e.Speed;
                            equipment.CriticalDamageRate += e.CriticalDamageRate;
                            equipment.CriticalRate += e.CriticalRate;
                            equipment.CriticalResistanceRate += e.CriticalResistanceRate;
                            equipment.IgnoreCriticalRate += e.IgnoreCriticalRate;
                            equipment.PenetrationRate += e.PenetrationRate;
                            equipment.PenetrationResistanceRate += e.PenetrationResistanceRate;
                            equipment.EvasionRate += e.EvasionRate;
                            equipment.DamageAbsorptionRate += e.DamageAbsorptionRate;
                            equipment.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                            equipment.AbsorbedDamageRate += e.AbsorbedDamageRate;
                            equipment.VitalityRegenerationRate += e.VitalityRegenerationRate;
                            equipment.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                            equipment.AccuracyRate += e.AccuracyRate;
                            equipment.LifestealRate += e.LifestealRate;
                            equipment.ShieldStrength += e.ShieldStrength;
                            equipment.Tenacity += e.Tenacity;
                            equipment.ResistanceRate += e.ResistanceRate;
                            equipment.ComboRate += e.ComboRate;
                            equipment.IgnoreComboRate += e.IgnoreComboRate;
                            equipment.ComboDamageRate += e.ComboDamageRate;
                            equipment.ComboResistanceRate += e.ComboResistanceRate;
                            equipment.StunRate += e.StunRate;
                            equipment.IgnoreStunRate += e.IgnoreStunRate;
                            equipment.ReflectionRate += e.ReflectionRate;
                            equipment.IgnoreReflectionRate += e.IgnoreReflectionRate;
                            equipment.ReflectionDamageRate += e.ReflectionDamageRate;
                            equipment.ReflectionResistanceRate += e.ReflectionResistanceRate;
                            equipment.Mana += e.Mana;
                            equipment.ManaRegenerationRate += e.ManaRegenerationRate;
                            equipment.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                            equipment.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                            equipment.DamageToSameFactionRate += e.DamageToSameFactionRate;
                            equipment.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                            equipment.NormalDamageRate += e.NormalDamageRate;
                            equipment.NormalResistanceRate += e.NormalResistanceRate;
                            equipment.SkillDamageRate += e.SkillDamageRate;
                            equipment.SkillResistanceRate += e.SkillResistanceRate;
                            equipment.SpecialHealth += e.SpecialHealth;
                            equipment.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                            equipment.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                            equipment.SpecialMagicalAttack += e.SpecialMagicalAttack;
                            equipment.SpecialMagicalDefense += e.SpecialMagicalDefense;
                            equipment.SpecialChemicalAttack += e.SpecialChemicalAttack;
                            equipment.SpecialChemicalDefense += e.SpecialChemicalDefense;
                            equipment.SpecialAtomicAttack += e.SpecialAtomicAttack;
                            equipment.SpecialAtomicDefense += e.SpecialAtomicDefense;
                            equipment.SpecialMentalAttack += e.SpecialMentalAttack;
                            equipment.SpecialMentalDefense += e.SpecialMentalDefense;
                            equipment.SpecialSpeed += e.Speed;
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
        return equipment;
    }
    public async Task<Equipments> GetAllEquipmentsByCardCaptainIdAsync(string user_id, string cardCaptainId)
    {
        Equipments equipment = new Equipments();
        equipment = ChangeValueToZero(equipment); // reset tất cả giá trị về 0
        List<Equipments> equipments = new List<Equipments>();

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT ue.*
                FROM user_card_captains uc
                JOIN card_captains c ON uc.card_captain_id = c.id
                JOIN card_captains_equipment che ON uc.card_captain_id = che.card_captain_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.card_captain_id = @card_captain_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_captain_id", cardCaptainId);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments tmpEquipments = new Equipments
                            {
                                Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDoubleSafe("power"),
                                Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDoubleSafe("health"),
                                PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDoubleSafe("physical_attack"),
                                PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDoubleSafe("physical_defense"),
                                MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDoubleSafe("magical_attack"),
                                MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDoubleSafe("magical_defense"),
                                ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDoubleSafe("chemical_attack"),
                                ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDoubleSafe("chemical_defense"),
                                AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDoubleSafe("atomic_attack"),
                                AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDoubleSafe("atomic_defense"),
                                MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDoubleSafe("mental_attack"),
                                MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDoubleSafe("mental_defense"),
                                Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDoubleSafe("speed"),
                                CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDoubleSafe("critical_damage_rate"),
                                CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDoubleSafe("critical_rate"),
                                CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("critical_resistance_rate"),
                                IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("ignore_critical_rate"),
                                PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDoubleSafe("penetration_rate"),
                                PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("penetration_resistance_rate"),
                                EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDoubleSafe("evasion_rate"),
                                DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("damage_absorption_rate"),
                                IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                                AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("absorbed_damage_rate"),
                                VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_rate"),
                                VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                                AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDoubleSafe("accuracy_rate"),
                                LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDoubleSafe("lifesteal_rate"),
                                ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDoubleSafe("shield_strength"),
                                Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDoubleSafe("tenacity"),
                                ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDoubleSafe("resistance_rate"),
                                ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDoubleSafe("combo_rate"),
                                IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("ignore_combo_rate"),
                                ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDoubleSafe("combo_damage_rate"),
                                ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("combo_resistance_rate"),
                                StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDoubleSafe("stun_rate"),
                                IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("ignore_stun_rate"),
                                ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDoubleSafe("reflection_rate"),
                                IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("ignore_reflection_rate"),
                                ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("reflection_damage_rate"),
                                ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("reflection_resistance_rate"),
                                Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetDoubleSafe("mana"),
                                ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDoubleSafe("normal_damage_rate"),
                                NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("normal_resistance_rate"),
                                SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDoubleSafe("skill_damage_rate"),
                                SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDoubleSafe("special_speed")
                            };

                            equipments.Add(tmpEquipments);
                        }
                        foreach (Equipments e in equipments)
                        {
                            equipment.Power += e.Power;
                            equipment.Health += e.Health;
                            equipment.PhysicalAttack += e.PhysicalAttack;
                            equipment.PhysicalDefense += e.PhysicalDefense;
                            equipment.MagicalAttack += e.MagicalAttack;
                            equipment.MagicalDefense += e.MagicalDefense;
                            equipment.ChemicalAttack += e.ChemicalAttack;
                            equipment.ChemicalDefense += e.ChemicalDefense;
                            equipment.AtomicAttack += e.AtomicAttack;
                            equipment.AtomicDefense += e.AtomicDefense;
                            equipment.MentalAttack += e.MentalAttack;
                            equipment.MentalDefense += e.MentalDefense;
                            equipment.Speed += e.Speed;
                            equipment.CriticalDamageRate += e.CriticalDamageRate;
                            equipment.CriticalRate += e.CriticalRate;
                            equipment.CriticalResistanceRate += e.CriticalResistanceRate;
                            equipment.IgnoreCriticalRate += e.IgnoreCriticalRate;
                            equipment.PenetrationRate += e.PenetrationRate;
                            equipment.PenetrationResistanceRate += e.PenetrationResistanceRate;
                            equipment.EvasionRate += e.EvasionRate;
                            equipment.DamageAbsorptionRate += e.DamageAbsorptionRate;
                            equipment.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                            equipment.AbsorbedDamageRate += e.AbsorbedDamageRate;
                            equipment.VitalityRegenerationRate += e.VitalityRegenerationRate;
                            equipment.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                            equipment.AccuracyRate += e.AccuracyRate;
                            equipment.LifestealRate += e.LifestealRate;
                            equipment.ShieldStrength += e.ShieldStrength;
                            equipment.Tenacity += e.Tenacity;
                            equipment.ResistanceRate += e.ResistanceRate;
                            equipment.ComboRate += e.ComboRate;
                            equipment.IgnoreComboRate += e.IgnoreComboRate;
                            equipment.ComboDamageRate += e.ComboDamageRate;
                            equipment.ComboResistanceRate += e.ComboResistanceRate;
                            equipment.StunRate += e.StunRate;
                            equipment.IgnoreStunRate += e.IgnoreStunRate;
                            equipment.ReflectionRate += e.ReflectionRate;
                            equipment.IgnoreReflectionRate += e.IgnoreReflectionRate;
                            equipment.ReflectionDamageRate += e.ReflectionDamageRate;
                            equipment.ReflectionResistanceRate += e.ReflectionResistanceRate;
                            equipment.Mana += e.Mana;
                            equipment.ManaRegenerationRate += e.ManaRegenerationRate;
                            equipment.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                            equipment.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                            equipment.DamageToSameFactionRate += e.DamageToSameFactionRate;
                            equipment.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                            equipment.NormalDamageRate += e.NormalDamageRate;
                            equipment.NormalResistanceRate += e.NormalResistanceRate;
                            equipment.SkillDamageRate += e.SkillDamageRate;
                            equipment.SkillResistanceRate += e.SkillResistanceRate;
                            equipment.SpecialHealth += e.SpecialHealth;
                            equipment.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                            equipment.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                            equipment.SpecialMagicalAttack += e.SpecialMagicalAttack;
                            equipment.SpecialMagicalDefense += e.SpecialMagicalDefense;
                            equipment.SpecialChemicalAttack += e.SpecialChemicalAttack;
                            equipment.SpecialChemicalDefense += e.SpecialChemicalDefense;
                            equipment.SpecialAtomicAttack += e.SpecialAtomicAttack;
                            equipment.SpecialAtomicDefense += e.SpecialAtomicDefense;
                            equipment.SpecialMentalAttack += e.SpecialMentalAttack;
                            equipment.SpecialMentalDefense += e.SpecialMentalDefense;
                            equipment.SpecialSpeed += e.Speed;
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
        return equipment;
    }
    public async Task<Equipments> GetAllEquipmentsByCardColonelIdAsync(string user_id, string cardColonelId)
    {
        Equipments equipment = new Equipments();
        equipment = ChangeValueToZero(equipment); // reset tất cả giá trị về 0
        List<Equipments> equipments = new List<Equipments>();

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT ue.*
                FROM user_card_colonels uc
                JOIN card_colonels c ON uc.card_colonel_id = c.id
                JOIN card_colonels_equipment che ON uc.card_colonel_id = che.card_colonel_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.card_colonel_id = @card_colonel_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_colonel_id", cardColonelId);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments tmpEquipments = new Equipments
                            {
                                Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDoubleSafe("power"),
                                Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDoubleSafe("health"),
                                PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDoubleSafe("physical_attack"),
                                PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDoubleSafe("physical_defense"),
                                MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDoubleSafe("magical_attack"),
                                MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDoubleSafe("magical_defense"),
                                ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDoubleSafe("chemical_attack"),
                                ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDoubleSafe("chemical_defense"),
                                AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDoubleSafe("atomic_attack"),
                                AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDoubleSafe("atomic_defense"),
                                MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDoubleSafe("mental_attack"),
                                MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDoubleSafe("mental_defense"),
                                Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDoubleSafe("speed"),
                                CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDoubleSafe("critical_damage_rate"),
                                CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDoubleSafe("critical_rate"),
                                CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("critical_resistance_rate"),
                                IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("ignore_critical_rate"),
                                PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDoubleSafe("penetration_rate"),
                                PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("penetration_resistance_rate"),
                                EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDoubleSafe("evasion_rate"),
                                DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("damage_absorption_rate"),
                                IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                                AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("absorbed_damage_rate"),
                                VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_rate"),
                                VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                                AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDoubleSafe("accuracy_rate"),
                                LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDoubleSafe("lifesteal_rate"),
                                ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDoubleSafe("shield_strength"),
                                Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDoubleSafe("tenacity"),
                                ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDoubleSafe("resistance_rate"),
                                ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDoubleSafe("combo_rate"),
                                IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("ignore_combo_rate"),
                                ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDoubleSafe("combo_damage_rate"),
                                ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("combo_resistance_rate"),
                                StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDoubleSafe("stun_rate"),
                                IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("ignore_stun_rate"),
                                ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDoubleSafe("reflection_rate"),
                                IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("ignore_reflection_rate"),
                                ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("reflection_damage_rate"),
                                ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("reflection_resistance_rate"),
                                Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetDoubleSafe("mana"),
                                ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDoubleSafe("normal_damage_rate"),
                                NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("normal_resistance_rate"),
                                SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDoubleSafe("skill_damage_rate"),
                                SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDoubleSafe("special_speed")
                            };

                            equipments.Add(tmpEquipments);
                        }
                        foreach (Equipments e in equipments)
                        {
                            equipment.Power += e.Power;
                            equipment.Health += e.Health;
                            equipment.PhysicalAttack += e.PhysicalAttack;
                            equipment.PhysicalDefense += e.PhysicalDefense;
                            equipment.MagicalAttack += e.MagicalAttack;
                            equipment.MagicalDefense += e.MagicalDefense;
                            equipment.ChemicalAttack += e.ChemicalAttack;
                            equipment.ChemicalDefense += e.ChemicalDefense;
                            equipment.AtomicAttack += e.AtomicAttack;
                            equipment.AtomicDefense += e.AtomicDefense;
                            equipment.MentalAttack += e.MentalAttack;
                            equipment.MentalDefense += e.MentalDefense;
                            equipment.Speed += e.Speed;
                            equipment.CriticalDamageRate += e.CriticalDamageRate;
                            equipment.CriticalRate += e.CriticalRate;
                            equipment.CriticalResistanceRate += e.CriticalResistanceRate;
                            equipment.IgnoreCriticalRate += e.IgnoreCriticalRate;
                            equipment.PenetrationRate += e.PenetrationRate;
                            equipment.PenetrationResistanceRate += e.PenetrationResistanceRate;
                            equipment.EvasionRate += e.EvasionRate;
                            equipment.DamageAbsorptionRate += e.DamageAbsorptionRate;
                            equipment.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                            equipment.AbsorbedDamageRate += e.AbsorbedDamageRate;
                            equipment.VitalityRegenerationRate += e.VitalityRegenerationRate;
                            equipment.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                            equipment.AccuracyRate += e.AccuracyRate;
                            equipment.LifestealRate += e.LifestealRate;
                            equipment.ShieldStrength += e.ShieldStrength;
                            equipment.Tenacity += e.Tenacity;
                            equipment.ResistanceRate += e.ResistanceRate;
                            equipment.ComboRate += e.ComboRate;
                            equipment.IgnoreComboRate += e.IgnoreComboRate;
                            equipment.ComboDamageRate += e.ComboDamageRate;
                            equipment.ComboResistanceRate += e.ComboResistanceRate;
                            equipment.StunRate += e.StunRate;
                            equipment.IgnoreStunRate += e.IgnoreStunRate;
                            equipment.ReflectionRate += e.ReflectionRate;
                            equipment.IgnoreReflectionRate += e.IgnoreReflectionRate;
                            equipment.ReflectionDamageRate += e.ReflectionDamageRate;
                            equipment.ReflectionResistanceRate += e.ReflectionResistanceRate;
                            equipment.Mana += e.Mana;
                            equipment.ManaRegenerationRate += e.ManaRegenerationRate;
                            equipment.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                            equipment.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                            equipment.DamageToSameFactionRate += e.DamageToSameFactionRate;
                            equipment.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                            equipment.NormalDamageRate += e.NormalDamageRate;
                            equipment.NormalResistanceRate += e.NormalResistanceRate;
                            equipment.SkillDamageRate += e.SkillDamageRate;
                            equipment.SkillResistanceRate += e.SkillResistanceRate;
                            equipment.SpecialHealth += e.SpecialHealth;
                            equipment.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                            equipment.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                            equipment.SpecialMagicalAttack += e.SpecialMagicalAttack;
                            equipment.SpecialMagicalDefense += e.SpecialMagicalDefense;
                            equipment.SpecialChemicalAttack += e.SpecialChemicalAttack;
                            equipment.SpecialChemicalDefense += e.SpecialChemicalDefense;
                            equipment.SpecialAtomicAttack += e.SpecialAtomicAttack;
                            equipment.SpecialAtomicDefense += e.SpecialAtomicDefense;
                            equipment.SpecialMentalAttack += e.SpecialMentalAttack;
                            equipment.SpecialMentalDefense += e.SpecialMentalDefense;
                            equipment.SpecialSpeed += e.Speed;
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
        return equipment;
    }
    public async Task<Equipments> GetAllEquipmentsByCardGeneralIdAsync(string user_id, string cardGeneralId)
    {
        Equipments equipment = new Equipments();
        equipment = ChangeValueToZero(equipment); // reset tất cả giá trị về 0
        List<Equipments> equipments = new List<Equipments>();

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT ue.*
                FROM user_card_generals uc
                JOIN card_generals c ON uc.card_general_id = c.id
                JOIN card_generals_equipment che ON uc.card_general_id = che.card_general_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.card_general_id = @card_general_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_general_id", cardGeneralId);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments tmpEquipments = new Equipments
                            {
                                Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDoubleSafe("power"),
                                Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDoubleSafe("health"),
                                PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDoubleSafe("physical_attack"),
                                PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDoubleSafe("physical_defense"),
                                MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDoubleSafe("magical_attack"),
                                MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDoubleSafe("magical_defense"),
                                ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDoubleSafe("chemical_attack"),
                                ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDoubleSafe("chemical_defense"),
                                AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDoubleSafe("atomic_attack"),
                                AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDoubleSafe("atomic_defense"),
                                MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDoubleSafe("mental_attack"),
                                MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDoubleSafe("mental_defense"),
                                Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDoubleSafe("speed"),
                                CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDoubleSafe("critical_damage_rate"),
                                CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDoubleSafe("critical_rate"),
                                CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("critical_resistance_rate"),
                                IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("ignore_critical_rate"),
                                PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDoubleSafe("penetration_rate"),
                                PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("penetration_resistance_rate"),
                                EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDoubleSafe("evasion_rate"),
                                DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("damage_absorption_rate"),
                                IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                                AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("absorbed_damage_rate"),
                                VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_rate"),
                                VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                                AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDoubleSafe("accuracy_rate"),
                                LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDoubleSafe("lifesteal_rate"),
                                ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDoubleSafe("shield_strength"),
                                Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDoubleSafe("tenacity"),
                                ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDoubleSafe("resistance_rate"),
                                ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDoubleSafe("combo_rate"),
                                IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("ignore_combo_rate"),
                                ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDoubleSafe("combo_damage_rate"),
                                ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("combo_resistance_rate"),
                                StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDoubleSafe("stun_rate"),
                                IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("ignore_stun_rate"),
                                ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDoubleSafe("reflection_rate"),
                                IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("ignore_reflection_rate"),
                                ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("reflection_damage_rate"),
                                ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("reflection_resistance_rate"),
                                Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetDoubleSafe("mana"),
                                ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDoubleSafe("normal_damage_rate"),
                                NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("normal_resistance_rate"),
                                SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDoubleSafe("skill_damage_rate"),
                                SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDoubleSafe("special_speed")
                            };

                            equipments.Add(tmpEquipments);
                        }
                        foreach (Equipments e in equipments)
                        {
                            equipment.Power += e.Power;
                            equipment.Health += e.Health;
                            equipment.PhysicalAttack += e.PhysicalAttack;
                            equipment.PhysicalDefense += e.PhysicalDefense;
                            equipment.MagicalAttack += e.MagicalAttack;
                            equipment.MagicalDefense += e.MagicalDefense;
                            equipment.ChemicalAttack += e.ChemicalAttack;
                            equipment.ChemicalDefense += e.ChemicalDefense;
                            equipment.AtomicAttack += e.AtomicAttack;
                            equipment.AtomicDefense += e.AtomicDefense;
                            equipment.MentalAttack += e.MentalAttack;
                            equipment.MentalDefense += e.MentalDefense;
                            equipment.Speed += e.Speed;
                            equipment.CriticalDamageRate += e.CriticalDamageRate;
                            equipment.CriticalRate += e.CriticalRate;
                            equipment.CriticalResistanceRate += e.CriticalResistanceRate;
                            equipment.IgnoreCriticalRate += e.IgnoreCriticalRate;
                            equipment.PenetrationRate += e.PenetrationRate;
                            equipment.PenetrationResistanceRate += e.PenetrationResistanceRate;
                            equipment.EvasionRate += e.EvasionRate;
                            equipment.DamageAbsorptionRate += e.DamageAbsorptionRate;
                            equipment.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                            equipment.AbsorbedDamageRate += e.AbsorbedDamageRate;
                            equipment.VitalityRegenerationRate += e.VitalityRegenerationRate;
                            equipment.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                            equipment.AccuracyRate += e.AccuracyRate;
                            equipment.LifestealRate += e.LifestealRate;
                            equipment.ShieldStrength += e.ShieldStrength;
                            equipment.Tenacity += e.Tenacity;
                            equipment.ResistanceRate += e.ResistanceRate;
                            equipment.ComboRate += e.ComboRate;
                            equipment.IgnoreComboRate += e.IgnoreComboRate;
                            equipment.ComboDamageRate += e.ComboDamageRate;
                            equipment.ComboResistanceRate += e.ComboResistanceRate;
                            equipment.StunRate += e.StunRate;
                            equipment.IgnoreStunRate += e.IgnoreStunRate;
                            equipment.ReflectionRate += e.ReflectionRate;
                            equipment.IgnoreReflectionRate += e.IgnoreReflectionRate;
                            equipment.ReflectionDamageRate += e.ReflectionDamageRate;
                            equipment.ReflectionResistanceRate += e.ReflectionResistanceRate;
                            equipment.Mana += e.Mana;
                            equipment.ManaRegenerationRate += e.ManaRegenerationRate;
                            equipment.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                            equipment.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                            equipment.DamageToSameFactionRate += e.DamageToSameFactionRate;
                            equipment.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                            equipment.NormalDamageRate += e.NormalDamageRate;
                            equipment.NormalResistanceRate += e.NormalResistanceRate;
                            equipment.SkillDamageRate += e.SkillDamageRate;
                            equipment.SkillResistanceRate += e.SkillResistanceRate;
                            equipment.SpecialHealth += e.SpecialHealth;
                            equipment.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                            equipment.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                            equipment.SpecialMagicalAttack += e.SpecialMagicalAttack;
                            equipment.SpecialMagicalDefense += e.SpecialMagicalDefense;
                            equipment.SpecialChemicalAttack += e.SpecialChemicalAttack;
                            equipment.SpecialChemicalDefense += e.SpecialChemicalDefense;
                            equipment.SpecialAtomicAttack += e.SpecialAtomicAttack;
                            equipment.SpecialAtomicDefense += e.SpecialAtomicDefense;
                            equipment.SpecialMentalAttack += e.SpecialMentalAttack;
                            equipment.SpecialMentalDefense += e.SpecialMentalDefense;
                            equipment.SpecialSpeed += e.Speed;
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
        return equipment;
    }
    public async Task<Equipments> GetAllEquipmentsByCardAdmiralIdAsync(string user_id, string cardAdmiralId)
    {
        Equipments equipment = new Equipments();
        equipment = ChangeValueToZero(equipment); // reset tất cả giá trị về 0
        List<Equipments> equipments = new List<Equipments>();

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT ue.*
                FROM user_card_admirals uc
                JOIN card_admirals c ON uc.card_admiral_id = c.id
                JOIN card_admirals_equipment che ON uc.card_admiral_id = che.card_admiral_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.card_admiral_id = @card_admiral_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_admiral_id", cardAdmiralId);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments tmpEquipments = new Equipments
                            {
                                Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDoubleSafe("power"),
                                Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDoubleSafe("health"),
                                PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDoubleSafe("physical_attack"),
                                PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDoubleSafe("physical_defense"),
                                MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDoubleSafe("magical_attack"),
                                MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDoubleSafe("magical_defense"),
                                ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDoubleSafe("chemical_attack"),
                                ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDoubleSafe("chemical_defense"),
                                AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDoubleSafe("atomic_attack"),
                                AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDoubleSafe("atomic_defense"),
                                MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDoubleSafe("mental_attack"),
                                MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDoubleSafe("mental_defense"),
                                Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDoubleSafe("speed"),
                                CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDoubleSafe("critical_damage_rate"),
                                CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDoubleSafe("critical_rate"),
                                CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("critical_resistance_rate"),
                                IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("ignore_critical_rate"),
                                PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDoubleSafe("penetration_rate"),
                                PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("penetration_resistance_rate"),
                                EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDoubleSafe("evasion_rate"),
                                DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("damage_absorption_rate"),
                                IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                                AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("absorbed_damage_rate"),
                                VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_rate"),
                                VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                                AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDoubleSafe("accuracy_rate"),
                                LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDoubleSafe("lifesteal_rate"),
                                ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDoubleSafe("shield_strength"),
                                Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDoubleSafe("tenacity"),
                                ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDoubleSafe("resistance_rate"),
                                ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDoubleSafe("combo_rate"),
                                IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("ignore_combo_rate"),
                                ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDoubleSafe("combo_damage_rate"),
                                ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("combo_resistance_rate"),
                                StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDoubleSafe("stun_rate"),
                                IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("ignore_stun_rate"),
                                ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDoubleSafe("reflection_rate"),
                                IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("ignore_reflection_rate"),
                                ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("reflection_damage_rate"),
                                ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("reflection_resistance_rate"),
                                Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetDoubleSafe("mana"),
                                ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDoubleSafe("normal_damage_rate"),
                                NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("normal_resistance_rate"),
                                SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDoubleSafe("skill_damage_rate"),
                                SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDoubleSafe("special_speed")
                            };

                            equipments.Add(tmpEquipments);
                        }
                        foreach (Equipments e in equipments)
                        {
                            equipment.Power += e.Power;
                            equipment.Health += e.Health;
                            equipment.PhysicalAttack += e.PhysicalAttack;
                            equipment.PhysicalDefense += e.PhysicalDefense;
                            equipment.MagicalAttack += e.MagicalAttack;
                            equipment.MagicalDefense += e.MagicalDefense;
                            equipment.ChemicalAttack += e.ChemicalAttack;
                            equipment.ChemicalDefense += e.ChemicalDefense;
                            equipment.AtomicAttack += e.AtomicAttack;
                            equipment.AtomicDefense += e.AtomicDefense;
                            equipment.MentalAttack += e.MentalAttack;
                            equipment.MentalDefense += e.MentalDefense;
                            equipment.Speed += e.Speed;
                            equipment.CriticalDamageRate += e.CriticalDamageRate;
                            equipment.CriticalRate += e.CriticalRate;
                            equipment.CriticalResistanceRate += e.CriticalResistanceRate;
                            equipment.IgnoreCriticalRate += e.IgnoreCriticalRate;
                            equipment.PenetrationRate += e.PenetrationRate;
                            equipment.PenetrationResistanceRate += e.PenetrationResistanceRate;
                            equipment.EvasionRate += e.EvasionRate;
                            equipment.DamageAbsorptionRate += e.DamageAbsorptionRate;
                            equipment.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                            equipment.AbsorbedDamageRate += e.AbsorbedDamageRate;
                            equipment.VitalityRegenerationRate += e.VitalityRegenerationRate;
                            equipment.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                            equipment.AccuracyRate += e.AccuracyRate;
                            equipment.LifestealRate += e.LifestealRate;
                            equipment.ShieldStrength += e.ShieldStrength;
                            equipment.Tenacity += e.Tenacity;
                            equipment.ResistanceRate += e.ResistanceRate;
                            equipment.ComboRate += e.ComboRate;
                            equipment.IgnoreComboRate += e.IgnoreComboRate;
                            equipment.ComboDamageRate += e.ComboDamageRate;
                            equipment.ComboResistanceRate += e.ComboResistanceRate;
                            equipment.StunRate += e.StunRate;
                            equipment.IgnoreStunRate += e.IgnoreStunRate;
                            equipment.ReflectionRate += e.ReflectionRate;
                            equipment.IgnoreReflectionRate += e.IgnoreReflectionRate;
                            equipment.ReflectionDamageRate += e.ReflectionDamageRate;
                            equipment.ReflectionResistanceRate += e.ReflectionResistanceRate;
                            equipment.Mana += e.Mana;
                            equipment.ManaRegenerationRate += e.ManaRegenerationRate;
                            equipment.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                            equipment.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                            equipment.DamageToSameFactionRate += e.DamageToSameFactionRate;
                            equipment.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                            equipment.NormalDamageRate += e.NormalDamageRate;
                            equipment.NormalResistanceRate += e.NormalResistanceRate;
                            equipment.SkillDamageRate += e.SkillDamageRate;
                            equipment.SkillResistanceRate += e.SkillResistanceRate;
                            equipment.SpecialHealth += e.SpecialHealth;
                            equipment.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                            equipment.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                            equipment.SpecialMagicalAttack += e.SpecialMagicalAttack;
                            equipment.SpecialMagicalDefense += e.SpecialMagicalDefense;
                            equipment.SpecialChemicalAttack += e.SpecialChemicalAttack;
                            equipment.SpecialChemicalDefense += e.SpecialChemicalDefense;
                            equipment.SpecialAtomicAttack += e.SpecialAtomicAttack;
                            equipment.SpecialAtomicDefense += e.SpecialAtomicDefense;
                            equipment.SpecialMentalAttack += e.SpecialMentalAttack;
                            equipment.SpecialMentalDefense += e.SpecialMentalDefense;
                            equipment.SpecialSpeed += e.Speed;
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
        return equipment;
    }
    public async Task<Equipments> GetAllEquipmentsByCardMonsterIdAsync(string user_id, string cardMonsterId)
    {
        Equipments equipment = new Equipments();
        equipment = ChangeValueToZero(equipment); // reset tất cả giá trị về 0
        List<Equipments> equipments = new List<Equipments>();

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT ue.*
                FROM user_card_monsters uc
                JOIN card_monsters c ON uc.card_monster_id = c.id
                JOIN card_monsters_equipment che ON uc.card_monster_id = che.card_monster_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.card_monster_id = @card_monster_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_monster_id", cardMonsterId);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments tmpEquipments = new Equipments
                            {
                                Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDoubleSafe("power"),
                                Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDoubleSafe("health"),
                                PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDoubleSafe("physical_attack"),
                                PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDoubleSafe("physical_defense"),
                                MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDoubleSafe("magical_attack"),
                                MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDoubleSafe("magical_defense"),
                                ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDoubleSafe("chemical_attack"),
                                ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDoubleSafe("chemical_defense"),
                                AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDoubleSafe("atomic_attack"),
                                AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDoubleSafe("atomic_defense"),
                                MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDoubleSafe("mental_attack"),
                                MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDoubleSafe("mental_defense"),
                                Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDoubleSafe("speed"),
                                CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDoubleSafe("critical_damage_rate"),
                                CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDoubleSafe("critical_rate"),
                                CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("critical_resistance_rate"),
                                IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("ignore_critical_rate"),
                                PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDoubleSafe("penetration_rate"),
                                PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("penetration_resistance_rate"),
                                EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDoubleSafe("evasion_rate"),
                                DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("damage_absorption_rate"),
                                IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                                AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("absorbed_damage_rate"),
                                VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_rate"),
                                VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                                AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDoubleSafe("accuracy_rate"),
                                LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDoubleSafe("lifesteal_rate"),
                                ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDoubleSafe("shield_strength"),
                                Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDoubleSafe("tenacity"),
                                ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDoubleSafe("resistance_rate"),
                                ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDoubleSafe("combo_rate"),
                                IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("ignore_combo_rate"),
                                ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDoubleSafe("combo_damage_rate"),
                                ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("combo_resistance_rate"),
                                StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDoubleSafe("stun_rate"),
                                IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("ignore_stun_rate"),
                                ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDoubleSafe("reflection_rate"),
                                IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("ignore_reflection_rate"),
                                ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("reflection_damage_rate"),
                                ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("reflection_resistance_rate"),
                                Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetDoubleSafe("mana"),
                                ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDoubleSafe("normal_damage_rate"),
                                NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("normal_resistance_rate"),
                                SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDoubleSafe("skill_damage_rate"),
                                SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDoubleSafe("special_speed")
                            };

                            equipments.Add(tmpEquipments);
                        }
                        foreach (Equipments e in equipments)
                        {
                            equipment.Power += e.Power;
                            equipment.Health += e.Health;
                            equipment.PhysicalAttack += e.PhysicalAttack;
                            equipment.PhysicalDefense += e.PhysicalDefense;
                            equipment.MagicalAttack += e.MagicalAttack;
                            equipment.MagicalDefense += e.MagicalDefense;
                            equipment.ChemicalAttack += e.ChemicalAttack;
                            equipment.ChemicalDefense += e.ChemicalDefense;
                            equipment.AtomicAttack += e.AtomicAttack;
                            equipment.AtomicDefense += e.AtomicDefense;
                            equipment.MentalAttack += e.MentalAttack;
                            equipment.MentalDefense += e.MentalDefense;
                            equipment.Speed += e.Speed;
                            equipment.CriticalDamageRate += e.CriticalDamageRate;
                            equipment.CriticalRate += e.CriticalRate;
                            equipment.CriticalResistanceRate += e.CriticalResistanceRate;
                            equipment.IgnoreCriticalRate += e.IgnoreCriticalRate;
                            equipment.PenetrationRate += e.PenetrationRate;
                            equipment.PenetrationResistanceRate += e.PenetrationResistanceRate;
                            equipment.EvasionRate += e.EvasionRate;
                            equipment.DamageAbsorptionRate += e.DamageAbsorptionRate;
                            equipment.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                            equipment.AbsorbedDamageRate += e.AbsorbedDamageRate;
                            equipment.VitalityRegenerationRate += e.VitalityRegenerationRate;
                            equipment.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                            equipment.AccuracyRate += e.AccuracyRate;
                            equipment.LifestealRate += e.LifestealRate;
                            equipment.ShieldStrength += e.ShieldStrength;
                            equipment.Tenacity += e.Tenacity;
                            equipment.ResistanceRate += e.ResistanceRate;
                            equipment.ComboRate += e.ComboRate;
                            equipment.IgnoreComboRate += e.IgnoreComboRate;
                            equipment.ComboDamageRate += e.ComboDamageRate;
                            equipment.ComboResistanceRate += e.ComboResistanceRate;
                            equipment.StunRate += e.StunRate;
                            equipment.IgnoreStunRate += e.IgnoreStunRate;
                            equipment.ReflectionRate += e.ReflectionRate;
                            equipment.IgnoreReflectionRate += e.IgnoreReflectionRate;
                            equipment.ReflectionDamageRate += e.ReflectionDamageRate;
                            equipment.ReflectionResistanceRate += e.ReflectionResistanceRate;
                            equipment.Mana += e.Mana;
                            equipment.ManaRegenerationRate += e.ManaRegenerationRate;
                            equipment.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                            equipment.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                            equipment.DamageToSameFactionRate += e.DamageToSameFactionRate;
                            equipment.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                            equipment.NormalDamageRate += e.NormalDamageRate;
                            equipment.NormalResistanceRate += e.NormalResistanceRate;
                            equipment.SkillDamageRate += e.SkillDamageRate;
                            equipment.SkillResistanceRate += e.SkillResistanceRate;
                            equipment.SpecialHealth += e.SpecialHealth;
                            equipment.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                            equipment.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                            equipment.SpecialMagicalAttack += e.SpecialMagicalAttack;
                            equipment.SpecialMagicalDefense += e.SpecialMagicalDefense;
                            equipment.SpecialChemicalAttack += e.SpecialChemicalAttack;
                            equipment.SpecialChemicalDefense += e.SpecialChemicalDefense;
                            equipment.SpecialAtomicAttack += e.SpecialAtomicAttack;
                            equipment.SpecialAtomicDefense += e.SpecialAtomicDefense;
                            equipment.SpecialMentalAttack += e.SpecialMentalAttack;
                            equipment.SpecialMentalDefense += e.SpecialMentalDefense;
                            equipment.SpecialSpeed += e.Speed;
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
        return equipment;
    }
    public async Task<Equipments> GetAllEquipmentsByCardMilitaryIdAsync(string user_id, string cardMilitaryId)
    {
        Equipments equipment = new Equipments();
        equipment = ChangeValueToZero(equipment); // reset tất cả giá trị về 0
        List<Equipments> equipments = new List<Equipments>();

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT ue.*
                FROM user_card_militaries uc
                JOIN card_militaries c ON uc.card_military_id = c.id
                JOIN card_militaries_equipment che ON uc.card_military_id = che.card_military_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.card_military_id = @card_military_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_military_id", cardMilitaryId);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments tmpEquipments = new Equipments
                            {
                                Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDoubleSafe("power"),
                                Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDoubleSafe("health"),
                                PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDoubleSafe("physical_attack"),
                                PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDoubleSafe("physical_defense"),
                                MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDoubleSafe("magical_attack"),
                                MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDoubleSafe("magical_defense"),
                                ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDoubleSafe("chemical_attack"),
                                ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDoubleSafe("chemical_defense"),
                                AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDoubleSafe("atomic_attack"),
                                AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDoubleSafe("atomic_defense"),
                                MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDoubleSafe("mental_attack"),
                                MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDoubleSafe("mental_defense"),
                                Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDoubleSafe("speed"),
                                CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDoubleSafe("critical_damage_rate"),
                                CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDoubleSafe("critical_rate"),
                                CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("critical_resistance_rate"),
                                IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("ignore_critical_rate"),
                                PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDoubleSafe("penetration_rate"),
                                PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("penetration_resistance_rate"),
                                EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDoubleSafe("evasion_rate"),
                                DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("damage_absorption_rate"),
                                IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                                AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("absorbed_damage_rate"),
                                VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_rate"),
                                VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                                AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDoubleSafe("accuracy_rate"),
                                LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDoubleSafe("lifesteal_rate"),
                                ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDoubleSafe("shield_strength"),
                                Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDoubleSafe("tenacity"),
                                ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDoubleSafe("resistance_rate"),
                                ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDoubleSafe("combo_rate"),
                                IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("ignore_combo_rate"),
                                ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDoubleSafe("combo_damage_rate"),
                                ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("combo_resistance_rate"),
                                StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDoubleSafe("stun_rate"),
                                IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("ignore_stun_rate"),
                                ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDoubleSafe("reflection_rate"),
                                IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("ignore_reflection_rate"),
                                ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("reflection_damage_rate"),
                                ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("reflection_resistance_rate"),
                                Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetDoubleSafe("mana"),
                                ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDoubleSafe("normal_damage_rate"),
                                NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("normal_resistance_rate"),
                                SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDoubleSafe("skill_damage_rate"),
                                SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDoubleSafe("special_speed")
                            };

                            equipments.Add(tmpEquipments);
                        }
                        foreach (Equipments e in equipments)
                        {
                            equipment.Power += e.Power;
                            equipment.Health += e.Health;
                            equipment.PhysicalAttack += e.PhysicalAttack;
                            equipment.PhysicalDefense += e.PhysicalDefense;
                            equipment.MagicalAttack += e.MagicalAttack;
                            equipment.MagicalDefense += e.MagicalDefense;
                            equipment.ChemicalAttack += e.ChemicalAttack;
                            equipment.ChemicalDefense += e.ChemicalDefense;
                            equipment.AtomicAttack += e.AtomicAttack;
                            equipment.AtomicDefense += e.AtomicDefense;
                            equipment.MentalAttack += e.MentalAttack;
                            equipment.MentalDefense += e.MentalDefense;
                            equipment.Speed += e.Speed;
                            equipment.CriticalDamageRate += e.CriticalDamageRate;
                            equipment.CriticalRate += e.CriticalRate;
                            equipment.CriticalResistanceRate += e.CriticalResistanceRate;
                            equipment.IgnoreCriticalRate += e.IgnoreCriticalRate;
                            equipment.PenetrationRate += e.PenetrationRate;
                            equipment.PenetrationResistanceRate += e.PenetrationResistanceRate;
                            equipment.EvasionRate += e.EvasionRate;
                            equipment.DamageAbsorptionRate += e.DamageAbsorptionRate;
                            equipment.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                            equipment.AbsorbedDamageRate += e.AbsorbedDamageRate;
                            equipment.VitalityRegenerationRate += e.VitalityRegenerationRate;
                            equipment.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                            equipment.AccuracyRate += e.AccuracyRate;
                            equipment.LifestealRate += e.LifestealRate;
                            equipment.ShieldStrength += e.ShieldStrength;
                            equipment.Tenacity += e.Tenacity;
                            equipment.ResistanceRate += e.ResistanceRate;
                            equipment.ComboRate += e.ComboRate;
                            equipment.IgnoreComboRate += e.IgnoreComboRate;
                            equipment.ComboDamageRate += e.ComboDamageRate;
                            equipment.ComboResistanceRate += e.ComboResistanceRate;
                            equipment.StunRate += e.StunRate;
                            equipment.IgnoreStunRate += e.IgnoreStunRate;
                            equipment.ReflectionRate += e.ReflectionRate;
                            equipment.IgnoreReflectionRate += e.IgnoreReflectionRate;
                            equipment.ReflectionDamageRate += e.ReflectionDamageRate;
                            equipment.ReflectionResistanceRate += e.ReflectionResistanceRate;
                            equipment.Mana += e.Mana;
                            equipment.ManaRegenerationRate += e.ManaRegenerationRate;
                            equipment.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                            equipment.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                            equipment.DamageToSameFactionRate += e.DamageToSameFactionRate;
                            equipment.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                            equipment.NormalDamageRate += e.NormalDamageRate;
                            equipment.NormalResistanceRate += e.NormalResistanceRate;
                            equipment.SkillDamageRate += e.SkillDamageRate;
                            equipment.SkillResistanceRate += e.SkillResistanceRate;
                            equipment.SpecialHealth += e.SpecialHealth;
                            equipment.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                            equipment.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                            equipment.SpecialMagicalAttack += e.SpecialMagicalAttack;
                            equipment.SpecialMagicalDefense += e.SpecialMagicalDefense;
                            equipment.SpecialChemicalAttack += e.SpecialChemicalAttack;
                            equipment.SpecialChemicalDefense += e.SpecialChemicalDefense;
                            equipment.SpecialAtomicAttack += e.SpecialAtomicAttack;
                            equipment.SpecialAtomicDefense += e.SpecialAtomicDefense;
                            equipment.SpecialMentalAttack += e.SpecialMentalAttack;
                            equipment.SpecialMentalDefense += e.SpecialMentalDefense;
                            equipment.SpecialSpeed += e.Speed;
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
        return equipment;
    }
    public async Task<Equipments> GetAllEquipmentsByCardSpellIdAsync(string user_id, string cardSpellId)
    {
        Equipments equipment = new Equipments();
        equipment = ChangeValueToZero(equipment); // reset tất cả giá trị về 0
        List<Equipments> equipments = new List<Equipments>();

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT ue.*
                FROM user_card_spells uc
                JOIN card_spells c ON uc.card_spell_id = c.id
                JOIN card_spells_equipment che ON uc.card_spell_id = che.card_spell_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.card_spell_id = @card_spell_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_spell_id", cardSpellId);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments tmpEquipments = new Equipments
                            {
                                Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDoubleSafe("power"),
                                Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDoubleSafe("health"),
                                PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDoubleSafe("physical_attack"),
                                PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDoubleSafe("physical_defense"),
                                MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDoubleSafe("magical_attack"),
                                MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDoubleSafe("magical_defense"),
                                ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDoubleSafe("chemical_attack"),
                                ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDoubleSafe("chemical_defense"),
                                AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDoubleSafe("atomic_attack"),
                                AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDoubleSafe("atomic_defense"),
                                MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDoubleSafe("mental_attack"),
                                MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDoubleSafe("mental_defense"),
                                Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDoubleSafe("speed"),
                                CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDoubleSafe("critical_damage_rate"),
                                CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDoubleSafe("critical_rate"),
                                CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("critical_resistance_rate"),
                                IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("ignore_critical_rate"),
                                PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDoubleSafe("penetration_rate"),
                                PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("penetration_resistance_rate"),
                                EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDoubleSafe("evasion_rate"),
                                DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("damage_absorption_rate"),
                                IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                                AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("absorbed_damage_rate"),
                                VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_rate"),
                                VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                                AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDoubleSafe("accuracy_rate"),
                                LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDoubleSafe("lifesteal_rate"),
                                ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDoubleSafe("shield_strength"),
                                Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDoubleSafe("tenacity"),
                                ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDoubleSafe("resistance_rate"),
                                ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDoubleSafe("combo_rate"),
                                IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("ignore_combo_rate"),
                                ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDoubleSafe("combo_damage_rate"),
                                ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("combo_resistance_rate"),
                                StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDoubleSafe("stun_rate"),
                                IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("ignore_stun_rate"),
                                ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDoubleSafe("reflection_rate"),
                                IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("ignore_reflection_rate"),
                                ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("reflection_damage_rate"),
                                ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("reflection_resistance_rate"),
                                Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetDoubleSafe("mana"),
                                ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDoubleSafe("normal_damage_rate"),
                                NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("normal_resistance_rate"),
                                SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDoubleSafe("skill_damage_rate"),
                                SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDoubleSafe("special_speed")
                            };

                            equipments.Add(tmpEquipments);
                        }
                        foreach (Equipments e in equipments)
                        {
                            equipment.Power += e.Power;
                            equipment.Health += e.Health;
                            equipment.PhysicalAttack += e.PhysicalAttack;
                            equipment.PhysicalDefense += e.PhysicalDefense;
                            equipment.MagicalAttack += e.MagicalAttack;
                            equipment.MagicalDefense += e.MagicalDefense;
                            equipment.ChemicalAttack += e.ChemicalAttack;
                            equipment.ChemicalDefense += e.ChemicalDefense;
                            equipment.AtomicAttack += e.AtomicAttack;
                            equipment.AtomicDefense += e.AtomicDefense;
                            equipment.MentalAttack += e.MentalAttack;
                            equipment.MentalDefense += e.MentalDefense;
                            equipment.Speed += e.Speed;
                            equipment.CriticalDamageRate += e.CriticalDamageRate;
                            equipment.CriticalRate += e.CriticalRate;
                            equipment.CriticalResistanceRate += e.CriticalResistanceRate;
                            equipment.IgnoreCriticalRate += e.IgnoreCriticalRate;
                            equipment.PenetrationRate += e.PenetrationRate;
                            equipment.PenetrationResistanceRate += e.PenetrationResistanceRate;
                            equipment.EvasionRate += e.EvasionRate;
                            equipment.DamageAbsorptionRate += e.DamageAbsorptionRate;
                            equipment.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                            equipment.AbsorbedDamageRate += e.AbsorbedDamageRate;
                            equipment.VitalityRegenerationRate += e.VitalityRegenerationRate;
                            equipment.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                            equipment.AccuracyRate += e.AccuracyRate;
                            equipment.LifestealRate += e.LifestealRate;
                            equipment.ShieldStrength += e.ShieldStrength;
                            equipment.Tenacity += e.Tenacity;
                            equipment.ResistanceRate += e.ResistanceRate;
                            equipment.ComboRate += e.ComboRate;
                            equipment.IgnoreComboRate += e.IgnoreComboRate;
                            equipment.ComboDamageRate += e.ComboDamageRate;
                            equipment.ComboResistanceRate += e.ComboResistanceRate;
                            equipment.StunRate += e.StunRate;
                            equipment.IgnoreStunRate += e.IgnoreStunRate;
                            equipment.ReflectionRate += e.ReflectionRate;
                            equipment.IgnoreReflectionRate += e.IgnoreReflectionRate;
                            equipment.ReflectionDamageRate += e.ReflectionDamageRate;
                            equipment.ReflectionResistanceRate += e.ReflectionResistanceRate;
                            equipment.Mana += e.Mana;
                            equipment.ManaRegenerationRate += e.ManaRegenerationRate;
                            equipment.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                            equipment.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                            equipment.DamageToSameFactionRate += e.DamageToSameFactionRate;
                            equipment.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                            equipment.NormalDamageRate += e.NormalDamageRate;
                            equipment.NormalResistanceRate += e.NormalResistanceRate;
                            equipment.SkillDamageRate += e.SkillDamageRate;
                            equipment.SkillResistanceRate += e.SkillResistanceRate;
                            equipment.SpecialHealth += e.SpecialHealth;
                            equipment.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                            equipment.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                            equipment.SpecialMagicalAttack += e.SpecialMagicalAttack;
                            equipment.SpecialMagicalDefense += e.SpecialMagicalDefense;
                            equipment.SpecialChemicalAttack += e.SpecialChemicalAttack;
                            equipment.SpecialChemicalDefense += e.SpecialChemicalDefense;
                            equipment.SpecialAtomicAttack += e.SpecialAtomicAttack;
                            equipment.SpecialAtomicDefense += e.SpecialAtomicDefense;
                            equipment.SpecialMentalAttack += e.SpecialMentalAttack;
                            equipment.SpecialMentalDefense += e.SpecialMentalDefense;
                            equipment.SpecialSpeed += e.Speed;
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
        return equipment;
    }
    public async Task<Equipments> GetAllEquipmentsByBookIdAsync(string user_id, string bookId)
    {
        Equipments equipment = new Equipments();
        equipment = ChangeValueToZero(equipment); // reset tất cả giá trị về 0
        List<Equipments> equipments = new List<Equipments>();

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT ue.*
                FROM user_books uc
                JOIN books c ON uc.book_id = c.id
                JOIN books_equipment che ON uc.book_id = che.book_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.book_id = @book_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@book_id", bookId);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments tmpEquipments = new Equipments
                            {
                                Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDoubleSafe("power"),
                                Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDoubleSafe("health"),
                                PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDoubleSafe("physical_attack"),
                                PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDoubleSafe("physical_defense"),
                                MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDoubleSafe("magical_attack"),
                                MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDoubleSafe("magical_defense"),
                                ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDoubleSafe("chemical_attack"),
                                ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDoubleSafe("chemical_defense"),
                                AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDoubleSafe("atomic_attack"),
                                AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDoubleSafe("atomic_defense"),
                                MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDoubleSafe("mental_attack"),
                                MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDoubleSafe("mental_defense"),
                                Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDoubleSafe("speed"),
                                CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDoubleSafe("critical_damage_rate"),
                                CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDoubleSafe("critical_rate"),
                                CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("critical_resistance_rate"),
                                IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("ignore_critical_rate"),
                                PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDoubleSafe("penetration_rate"),
                                PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("penetration_resistance_rate"),
                                EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDoubleSafe("evasion_rate"),
                                DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("damage_absorption_rate"),
                                IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                                AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("absorbed_damage_rate"),
                                VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_rate"),
                                VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                                AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDoubleSafe("accuracy_rate"),
                                LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDoubleSafe("lifesteal_rate"),
                                ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDoubleSafe("shield_strength"),
                                Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDoubleSafe("tenacity"),
                                ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDoubleSafe("resistance_rate"),
                                ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDoubleSafe("combo_rate"),
                                IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("ignore_combo_rate"),
                                ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDoubleSafe("combo_damage_rate"),
                                ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("combo_resistance_rate"),
                                StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDoubleSafe("stun_rate"),
                                IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("ignore_stun_rate"),
                                ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDoubleSafe("reflection_rate"),
                                IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("ignore_reflection_rate"),
                                ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("reflection_damage_rate"),
                                ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("reflection_resistance_rate"),
                                Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetDoubleSafe("mana"),
                                ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDoubleSafe("normal_damage_rate"),
                                NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("normal_resistance_rate"),
                                SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDoubleSafe("skill_damage_rate"),
                                SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDoubleSafe("special_speed")
                            };

                            equipments.Add(tmpEquipments);
                        }
                        foreach (Equipments e in equipments)
                        {
                            equipment.Power += e.Power;
                            equipment.Health += e.Health;
                            equipment.PhysicalAttack += e.PhysicalAttack;
                            equipment.PhysicalDefense += e.PhysicalDefense;
                            equipment.MagicalAttack += e.MagicalAttack;
                            equipment.MagicalDefense += e.MagicalDefense;
                            equipment.ChemicalAttack += e.ChemicalAttack;
                            equipment.ChemicalDefense += e.ChemicalDefense;
                            equipment.AtomicAttack += e.AtomicAttack;
                            equipment.AtomicDefense += e.AtomicDefense;
                            equipment.MentalAttack += e.MentalAttack;
                            equipment.MentalDefense += e.MentalDefense;
                            equipment.Speed += e.Speed;
                            equipment.CriticalDamageRate += e.CriticalDamageRate;
                            equipment.CriticalRate += e.CriticalRate;
                            equipment.CriticalResistanceRate += e.CriticalResistanceRate;
                            equipment.IgnoreCriticalRate += e.IgnoreCriticalRate;
                            equipment.PenetrationRate += e.PenetrationRate;
                            equipment.PenetrationResistanceRate += e.PenetrationResistanceRate;
                            equipment.EvasionRate += e.EvasionRate;
                            equipment.DamageAbsorptionRate += e.DamageAbsorptionRate;
                            equipment.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                            equipment.AbsorbedDamageRate += e.AbsorbedDamageRate;
                            equipment.VitalityRegenerationRate += e.VitalityRegenerationRate;
                            equipment.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                            equipment.AccuracyRate += e.AccuracyRate;
                            equipment.LifestealRate += e.LifestealRate;
                            equipment.ShieldStrength += e.ShieldStrength;
                            equipment.Tenacity += e.Tenacity;
                            equipment.ResistanceRate += e.ResistanceRate;
                            equipment.ComboRate += e.ComboRate;
                            equipment.IgnoreComboRate += e.IgnoreComboRate;
                            equipment.ComboDamageRate += e.ComboDamageRate;
                            equipment.ComboResistanceRate += e.ComboResistanceRate;
                            equipment.StunRate += e.StunRate;
                            equipment.IgnoreStunRate += e.IgnoreStunRate;
                            equipment.ReflectionRate += e.ReflectionRate;
                            equipment.IgnoreReflectionRate += e.IgnoreReflectionRate;
                            equipment.ReflectionDamageRate += e.ReflectionDamageRate;
                            equipment.ReflectionResistanceRate += e.ReflectionResistanceRate;
                            equipment.Mana += e.Mana;
                            equipment.ManaRegenerationRate += e.ManaRegenerationRate;
                            equipment.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                            equipment.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                            equipment.DamageToSameFactionRate += e.DamageToSameFactionRate;
                            equipment.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                            equipment.NormalDamageRate += e.NormalDamageRate;
                            equipment.NormalResistanceRate += e.NormalResistanceRate;
                            equipment.SkillDamageRate += e.SkillDamageRate;
                            equipment.SkillResistanceRate += e.SkillResistanceRate;
                            equipment.SpecialHealth += e.SpecialHealth;
                            equipment.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                            equipment.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                            equipment.SpecialMagicalAttack += e.SpecialMagicalAttack;
                            equipment.SpecialMagicalDefense += e.SpecialMagicalDefense;
                            equipment.SpecialChemicalAttack += e.SpecialChemicalAttack;
                            equipment.SpecialChemicalDefense += e.SpecialChemicalDefense;
                            equipment.SpecialAtomicAttack += e.SpecialAtomicAttack;
                            equipment.SpecialAtomicDefense += e.SpecialAtomicDefense;
                            equipment.SpecialMentalAttack += e.SpecialMentalAttack;
                            equipment.SpecialMentalDefense += e.SpecialMentalDefense;
                            equipment.SpecialSpeed += e.Speed;
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
        return equipment;
    }
    public async Task<Equipments> GetAllEquipmentsByPetIdAsync(string user_id, string petId)
    {
        Equipments equipment = new Equipments();
        equipment = ChangeValueToZero(equipment); // reset tất cả giá trị về 0
        List<Equipments> equipments = new List<Equipments>();

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT ue.*
                FROM user_pets uc
                JOIN pets c ON uc.pet_id = c.id
                JOIN pets_equipment che ON uc.pet_id = che.pet_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.pet_id = @pet_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@pet_id", petId);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Equipments tmpEquipments = new Equipments
                            {
                                Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDoubleSafe("power"),
                                Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDoubleSafe("health"),
                                PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDoubleSafe("physical_attack"),
                                PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDoubleSafe("physical_defense"),
                                MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDoubleSafe("magical_attack"),
                                MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDoubleSafe("magical_defense"),
                                ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDoubleSafe("chemical_attack"),
                                ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDoubleSafe("chemical_defense"),
                                AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDoubleSafe("atomic_attack"),
                                AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDoubleSafe("atomic_defense"),
                                MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDoubleSafe("mental_attack"),
                                MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDoubleSafe("mental_defense"),
                                Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDoubleSafe("speed"),
                                CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDoubleSafe("critical_damage_rate"),
                                CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDoubleSafe("critical_rate"),
                                CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("critical_resistance_rate"),
                                IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("ignore_critical_rate"),
                                PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDoubleSafe("penetration_rate"),
                                PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("penetration_resistance_rate"),
                                EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDoubleSafe("evasion_rate"),
                                DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("damage_absorption_rate"),
                                IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                                AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("absorbed_damage_rate"),
                                VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_rate"),
                                VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                                AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDoubleSafe("accuracy_rate"),
                                LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDoubleSafe("lifesteal_rate"),
                                ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDoubleSafe("shield_strength"),
                                Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDoubleSafe("tenacity"),
                                ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDoubleSafe("resistance_rate"),
                                ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDoubleSafe("combo_rate"),
                                IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("ignore_combo_rate"),
                                ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDoubleSafe("combo_damage_rate"),
                                ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("combo_resistance_rate"),
                                StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDoubleSafe("stun_rate"),
                                IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("ignore_stun_rate"),
                                ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDoubleSafe("reflection_rate"),
                                IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("ignore_reflection_rate"),
                                ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("reflection_damage_rate"),
                                ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("reflection_resistance_rate"),
                                Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetDoubleSafe("mana"),
                                ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDoubleSafe("normal_damage_rate"),
                                NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("normal_resistance_rate"),
                                SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDoubleSafe("skill_damage_rate"),
                                SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("skill_resistance_rate"),
                                SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDoubleSafe("special_health"),
                                SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDoubleSafe("special_physical_attack"),
                                SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDoubleSafe("special_physical_defense"),
                                SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDoubleSafe("special_magical_attack"),
                                SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDoubleSafe("special_magical_defense"),
                                SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDoubleSafe("special_chemical_attack"),
                                SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDoubleSafe("special_chemical_defense"),
                                SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDoubleSafe("special_atomic_attack"),
                                SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDoubleSafe("special_atomic_defense"),
                                SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDoubleSafe("special_mental_attack"),
                                SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDoubleSafe("special_mental_defense"),
                                SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDoubleSafe("special_speed")
                            };

                            equipments.Add(tmpEquipments);
                        }
                        foreach (Equipments e in equipments)
                        {
                            equipment.Power += e.Power;
                            equipment.Health += e.Health;
                            equipment.PhysicalAttack += e.PhysicalAttack;
                            equipment.PhysicalDefense += e.PhysicalDefense;
                            equipment.MagicalAttack += e.MagicalAttack;
                            equipment.MagicalDefense += e.MagicalDefense;
                            equipment.ChemicalAttack += e.ChemicalAttack;
                            equipment.ChemicalDefense += e.ChemicalDefense;
                            equipment.AtomicAttack += e.AtomicAttack;
                            equipment.AtomicDefense += e.AtomicDefense;
                            equipment.MentalAttack += e.MentalAttack;
                            equipment.MentalDefense += e.MentalDefense;
                            equipment.Speed += e.Speed;
                            equipment.CriticalDamageRate += e.CriticalDamageRate;
                            equipment.CriticalRate += e.CriticalRate;
                            equipment.CriticalResistanceRate += e.CriticalResistanceRate;
                            equipment.IgnoreCriticalRate += e.IgnoreCriticalRate;
                            equipment.PenetrationRate += e.PenetrationRate;
                            equipment.PenetrationResistanceRate += e.PenetrationResistanceRate;
                            equipment.EvasionRate += e.EvasionRate;
                            equipment.DamageAbsorptionRate += e.DamageAbsorptionRate;
                            equipment.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                            equipment.AbsorbedDamageRate += e.AbsorbedDamageRate;
                            equipment.VitalityRegenerationRate += e.VitalityRegenerationRate;
                            equipment.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                            equipment.AccuracyRate += e.AccuracyRate;
                            equipment.LifestealRate += e.LifestealRate;
                            equipment.ShieldStrength += e.ShieldStrength;
                            equipment.Tenacity += e.Tenacity;
                            equipment.ResistanceRate += e.ResistanceRate;
                            equipment.ComboRate += e.ComboRate;
                            equipment.IgnoreComboRate += e.IgnoreComboRate;
                            equipment.ComboDamageRate += e.ComboDamageRate;
                            equipment.ComboResistanceRate += e.ComboResistanceRate;
                            equipment.StunRate += e.StunRate;
                            equipment.IgnoreStunRate += e.IgnoreStunRate;
                            equipment.ReflectionRate += e.ReflectionRate;
                            equipment.IgnoreReflectionRate += e.IgnoreReflectionRate;
                            equipment.ReflectionDamageRate += e.ReflectionDamageRate;
                            equipment.ReflectionResistanceRate += e.ReflectionResistanceRate;
                            equipment.Mana += e.Mana;
                            equipment.ManaRegenerationRate += e.ManaRegenerationRate;
                            equipment.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                            equipment.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                            equipment.DamageToSameFactionRate += e.DamageToSameFactionRate;
                            equipment.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                            equipment.NormalDamageRate += e.NormalDamageRate;
                            equipment.NormalResistanceRate += e.NormalResistanceRate;
                            equipment.SkillDamageRate += e.SkillDamageRate;
                            equipment.SkillResistanceRate += e.SkillResistanceRate;
                            equipment.SpecialHealth += e.SpecialHealth;
                            equipment.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                            equipment.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                            equipment.SpecialMagicalAttack += e.SpecialMagicalAttack;
                            equipment.SpecialMagicalDefense += e.SpecialMagicalDefense;
                            equipment.SpecialChemicalAttack += e.SpecialChemicalAttack;
                            equipment.SpecialChemicalDefense += e.SpecialChemicalDefense;
                            equipment.SpecialAtomicAttack += e.SpecialAtomicAttack;
                            equipment.SpecialAtomicDefense += e.SpecialAtomicDefense;
                            equipment.SpecialMentalAttack += e.SpecialMentalAttack;
                            equipment.SpecialMentalDefense += e.SpecialMentalDefense;
                            equipment.SpecialSpeed += e.Speed;
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
        return equipment;
    }

    public async Task<bool> EquipAllEquipmentsOfTypeToCardHeroAsync(string cardHeroId, string type, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Bước 1: Lấy max_positions cho type từ bảng equipment_type
                string getMaxPositionsQuery = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand getMaxCmd = new MySqlCommand(getMaxPositionsQuery, connection))
                {
                    getMaxCmd.Parameters.AddWithValue("@type", type);
                    object result = await getMaxCmd.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                    {
                        maxPositions = Convert.ToInt32(result);
                    }
                    else
                    {
                        Debug.LogError($"Type '{type}' not found in equipment_type table.");
                        return false;
                    }
                }

                // Bước 2: Lấy danh sách position đã dùng cho card hero và type này
                string getUsedPositionsQuery = @"
                SELECT che.position
                FROM card_heroes_equipment che
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                JOIN Equipments e ON e.id = ue.equipment_id
                WHERE che.card_hero_id = @card_hero_id AND e.type = @type AND ue.user_id = @user_id";
                HashSet<int> usedPositions = new HashSet<int>();
                await using (MySqlCommand getUsedCmd = new MySqlCommand(getUsedPositionsQuery, connection))
                {
                    getUsedCmd.Parameters.AddWithValue("@card_hero_id", cardHeroId);
                    getUsedCmd.Parameters.AddWithValue("@type", type);
                    getUsedCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    await using (MySqlDataReader reader = await getUsedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            usedPositions.Add(reader.GetIntSafe("position"));
                        }
                    }
                }

                // Bước 3: Filter availableEquipments để loại bỏ equipment đã equip cho card này và chỉ lấy type khớp
                List<string> equippedEquipmentIds = new List<string>();
                string getEquippedQuery = @"
                SELECT equipment_id 
                FROM card_heroes_equipment 
                WHERE card_hero_id = @card_hero_id";
                await using (MySqlCommand getEquippedCmd = new MySqlCommand(getEquippedQuery, connection))
                {
                    getEquippedCmd.Parameters.AddWithValue("@card_hero_id", cardHeroId);
                    await using (MySqlDataReader reader = await getEquippedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            equippedEquipmentIds.Add(reader.GetStringSafe("equipment_id"));
                        }
                    }
                }

                List<Equipments> filteredEquipments = availableEquipments
                    .Where(e => e.Type == type && !equippedEquipmentIds.Contains(e.Id))
                    .OrderByDescending(e => e.Power) // Đảm bảo sort giảm dần (list đã sort, nhưng để an toàn)
                    .ToList();

                // Bước 4: Equip equipment vào vị trí trống
                int equippedCount = 0;
                foreach (var equipment in filteredEquipments)
                {
                    if (equippedCount >= maxPositions - usedPositions.Count) break; // Đủ slot

                    int position = 1;
                    while (usedPositions.Contains(position) && position <= maxPositions)
                    {
                        position++;
                    }
                    if (position > maxPositions) break; // Không còn slot trống

                    // Insert vào card_heroes_equipment
                    string insertQuery = @"
                    INSERT INTO card_heroes_equipment
                        (user_id, card_hero_id, equipment_id, position)
                    VALUES
                        (@user_id, @card_hero_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@card_hero_id", cardHeroId);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);
                        await insertCommand.ExecuteNonQueryAsync();
                    }

                    usedPositions.Add(position);
                    equippedCount++;
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments of type: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task<bool> EquipAllEquipmentsToCardHeroAsync(string cardHeroId, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Lấy tất cả type từ equipment_type
                string getTypesQuery = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand getTypesCmd = new MySqlCommand(getTypesQuery, connection))
                {
                    await using (MySqlDataReader reader = await getTypesCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            types.Add(reader.GetStringSafe("type"));
                        }
                    }
                }

                // Cho mỗi type, gọi hàm EquipAllEquipmentsOfTypeToCardHeroAsync với list đã filter
                foreach (string type in types)
                {
                    bool success = await EquipAllEquipmentsOfTypeToCardHeroAsync(cardHeroId, type, availableEquipments);
                    if (!success)
                    {
                        Debug.LogWarning($"Failed to equip type '{type}' for card hero '{cardHeroId}'.");
                        // Tiếp tục với type khác
                    }
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }

    public async Task<bool> EquipAllEquipmentsOfTypeToCardCaptainAsync(string cardCaptainId, string type, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Bước 1: Lấy max_positions cho type từ bảng equipment_type
                string getMaxPositionsQuery = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand getMaxCmd = new MySqlCommand(getMaxPositionsQuery, connection))
                {
                    getMaxCmd.Parameters.AddWithValue("@type", type);
                    object result = await getMaxCmd.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                    {
                        maxPositions = Convert.ToInt32(result);
                    }
                    else
                    {
                        Debug.LogError($"Type '{type}' not found in equipment_type table.");
                        return false;
                    }
                }

                // Bước 2: Lấy danh sách position đã dùng cho card hero và type này
                string getUsedPositionsQuery = @"
                SELECT che.position
                FROM card_captains_equipment che
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                JOIN Equipments e ON e.id = ue.equipment_id
                WHERE che.card_captain_id = @card_captain_id AND e.type = @type AND ue.user_id = @user_id";
                HashSet<int> usedPositions = new HashSet<int>();
                await using (MySqlCommand getUsedCmd = new MySqlCommand(getUsedPositionsQuery, connection))
                {
                    getUsedCmd.Parameters.AddWithValue("@card_captain_id", cardCaptainId);
                    getUsedCmd.Parameters.AddWithValue("@type", type);
                    getUsedCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    await using (MySqlDataReader reader = await getUsedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            usedPositions.Add(reader.GetIntSafe("position"));
                        }
                    }
                }

                // Bước 3: Filter availableEquipments để loại bỏ equipment đã equip cho card này và chỉ lấy type khớp
                List<string> equippedEquipmentIds = new List<string>();
                string getEquippedQuery = @"
                SELECT equipment_id 
                FROM card_captains_equipment 
                WHERE card_captain_id = @card_captain_id";
                await using (MySqlCommand getEquippedCmd = new MySqlCommand(getEquippedQuery, connection))
                {
                    getEquippedCmd.Parameters.AddWithValue("@card_captain_id", cardCaptainId);
                    await using (MySqlDataReader reader = await getEquippedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            equippedEquipmentIds.Add(reader.GetStringSafe("equipment_id"));
                        }
                    }
                }

                List<Equipments> filteredEquipments = availableEquipments
                    .Where(e => e.Type == type && !equippedEquipmentIds.Contains(e.Id))
                    .OrderByDescending(e => e.Power) // Đảm bảo sort giảm dần (list đã sort, nhưng để an toàn)
                    .ToList();

                // Bước 4: Equip equipment vào vị trí trống
                int equippedCount = 0;
                foreach (var equipment in filteredEquipments)
                {
                    if (equippedCount >= maxPositions - usedPositions.Count) break; // Đủ slot

                    int position = 1;
                    while (usedPositions.Contains(position) && position <= maxPositions)
                    {
                        position++;
                    }
                    if (position > maxPositions) break; // Không còn slot trống

                    // Insert vào card_captains_equipment
                    string insertQuery = @"
                    INSERT INTO card_captains_equipment
                        (user_id, card_captain_id, equipment_id, position)
                    VALUES
                        (@user_id, @card_captain_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@card_captain_id", cardCaptainId);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);
                        await insertCommand.ExecuteNonQueryAsync();
                    }

                    usedPositions.Add(position);
                    equippedCount++;
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments of type: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task<bool> EquipAllEquipmentsToCardCaptainAsync(string cardCaptainId, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Lấy tất cả type từ equipment_type
                string getTypesQuery = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand getTypesCmd = new MySqlCommand(getTypesQuery, connection))
                {
                    await using (MySqlDataReader reader = await getTypesCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            types.Add(reader.GetStringSafe("type"));
                        }
                    }
                }

                // Cho mỗi type, gọi hàm EquipAllEquipmentsOfTypeToCardHeroAsync với list đã filter
                foreach (string type in types)
                {
                    bool success = await EquipAllEquipmentsOfTypeToCardCaptainAsync(cardCaptainId, type, availableEquipments);
                    if (!success)
                    {
                        Debug.LogWarning($"Failed to equip type '{type}' for card hero '{cardCaptainId}'.");
                        // Tiếp tục với type khác
                    }
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }

    public async Task<bool> EquipAllEquipmentsOfTypeToCardColonelAsync(string cardColonelId, string type, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Bước 1: Lấy max_positions cho type từ bảng equipment_type
                string getMaxPositionsQuery = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand getMaxCmd = new MySqlCommand(getMaxPositionsQuery, connection))
                {
                    getMaxCmd.Parameters.AddWithValue("@type", type);
                    object result = await getMaxCmd.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                    {
                        maxPositions = Convert.ToInt32(result);
                    }
                    else
                    {
                        Debug.LogError($"Type '{type}' not found in equipment_type table.");
                        return false;
                    }
                }

                // Bước 2: Lấy danh sách position đã dùng cho card hero và type này
                string getUsedPositionsQuery = @"
                SELECT cce.position
                FROM card_captains_equipment cce
                JOIN user_equipments ue ON cce.equipment_id = ue.equipment_id 
                JOIN Equipments e ON e.id = ue.equipment_id
                WHERE cce.card_captain_id = @card_captain_id AND e.type = @type AND ue.user_id = @user_id";
                HashSet<int> usedPositions = new HashSet<int>();
                await using (MySqlCommand getUsedCmd = new MySqlCommand(getUsedPositionsQuery, connection))
                {
                    getUsedCmd.Parameters.AddWithValue("@card_captain_id", cardColonelId);
                    getUsedCmd.Parameters.AddWithValue("@type", type);
                    getUsedCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    await using (MySqlDataReader reader = await getUsedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            usedPositions.Add(reader.GetIntSafe("position"));
                        }
                    }
                }

                // Bước 3: Filter availableEquipments để loại bỏ equipment đã equip cho card này và chỉ lấy type khớp
                List<string> equippedEquipmentIds = new List<string>();
                string getEquippedQuery = @"
                SELECT equipment_id 
                FROM card_captains_equipment 
                WHERE card_captain_id = @card_captain_id";
                await using (MySqlCommand getEquippedCmd = new MySqlCommand(getEquippedQuery, connection))
                {
                    getEquippedCmd.Parameters.AddWithValue("@card_captain_id", cardColonelId);
                    await using (MySqlDataReader reader = await getEquippedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            equippedEquipmentIds.Add(reader.GetStringSafe("equipment_id"));
                        }
                    }
                }

                List<Equipments> filteredEquipments = availableEquipments
                    .Where(e => e.Type == type && !equippedEquipmentIds.Contains(e.Id))
                    .OrderByDescending(e => e.Power) // Đảm bảo sort giảm dần (list đã sort, nhưng để an toàn)
                    .ToList();

                // Bước 4: Equip equipment vào vị trí trống
                int equippedCount = 0;
                foreach (var equipment in filteredEquipments)
                {
                    if (equippedCount >= maxPositions - usedPositions.Count) break; // Đủ slot

                    int position = 1;
                    while (usedPositions.Contains(position) && position <= maxPositions)
                    {
                        position++;
                    }
                    if (position > maxPositions) break; // Không còn slot trống

                    // Insert vào card_captains_equipment
                    string insertQuery = @"
                    INSERT INTO card_captains_equipment
                        (user_id, card_captain_id, equipment_id, position)
                    VALUES
                        (@user_id, @card_captain_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@card_captain_id", cardColonelId);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);
                        await insertCommand.ExecuteNonQueryAsync();
                    }

                    usedPositions.Add(position);
                    equippedCount++;
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments of type: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task<bool> EquipAllEquipmentsToCardColonelAsync(string cardColonelId, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Lấy tất cả type từ equipment_type
                string getTypesQuery = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand getTypesCmd = new MySqlCommand(getTypesQuery, connection))
                {
                    await using (MySqlDataReader reader = await getTypesCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            types.Add(reader.GetStringSafe("type"));
                        }
                    }
                }

                // Cho mỗi type, gọi hàm EquipAllEquipmentsOfTypeToCardHeroAsync với list đã filter
                foreach (string type in types)
                {
                    bool success = await EquipAllEquipmentsOfTypeToCardColonelAsync(cardColonelId, type, availableEquipments);
                    if (!success)
                    {
                        Debug.LogWarning($"Failed to equip type '{type}' for card colonel '{cardColonelId}'.");
                        // Tiếp tục với type khác
                    }
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }

    public async Task<bool> EquipAllEquipmentsOfTypeToCardGeneralAsync(string cardGeneralId, string type, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Bước 1: Lấy max_positions cho type từ bảng equipment_type
                string getMaxPositionsQuery = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand getMaxCmd = new MySqlCommand(getMaxPositionsQuery, connection))
                {
                    getMaxCmd.Parameters.AddWithValue("@type", type);
                    object result = await getMaxCmd.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                    {
                        maxPositions = Convert.ToInt32(result);
                    }
                    else
                    {
                        Debug.LogError($"Type '{type}' not found in equipment_type table.");
                        return false;
                    }
                }

                // Bước 2: Lấy danh sách position đã dùng cho card hero và type này
                string getUsedPositionsQuery = @"
                SELECT cge.position
                FROM card_generals_equipment cge
                JOIN user_equipments ue ON cge.equipment_id = ue.equipment_id 
                JOIN Equipments e ON e.id = ue.equipment_id
                WHERE cge.card_general_id = @card_general_id AND e.type = @type AND ue.user_id = @user_id";
                HashSet<int> usedPositions = new HashSet<int>();
                await using (MySqlCommand getUsedCmd = new MySqlCommand(getUsedPositionsQuery, connection))
                {
                    getUsedCmd.Parameters.AddWithValue("@card_general_id", cardGeneralId);
                    getUsedCmd.Parameters.AddWithValue("@type", type);
                    getUsedCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    await using (MySqlDataReader reader = await getUsedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            usedPositions.Add(reader.GetIntSafe("position"));
                        }
                    }
                }

                // Bước 3: Filter availableEquipments để loại bỏ equipment đã equip cho card này và chỉ lấy type khớp
                List<string> equippedEquipmentIds = new List<string>();
                string getEquippedQuery = @"
                SELECT equipment_id 
                FROM card_generals_equipment 
                WHERE card_general_id = @card_general_id";
                await using (MySqlCommand getEquippedCmd = new MySqlCommand(getEquippedQuery, connection))
                {
                    getEquippedCmd.Parameters.AddWithValue("@card_general_id", cardGeneralId);
                    await using (MySqlDataReader reader = await getEquippedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            equippedEquipmentIds.Add(reader.GetStringSafe("equipment_id"));
                        }
                    }
                }

                List<Equipments> filteredEquipments = availableEquipments
                    .Where(e => e.Type == type && !equippedEquipmentIds.Contains(e.Id))
                    .OrderByDescending(e => e.Power) // Đảm bảo sort giảm dần (list đã sort, nhưng để an toàn)
                    .ToList();

                // Bước 4: Equip equipment vào vị trí trống
                int equippedCount = 0;
                foreach (var equipment in filteredEquipments)
                {
                    if (equippedCount >= maxPositions - usedPositions.Count) break; // Đủ slot

                    int position = 1;
                    while (usedPositions.Contains(position) && position <= maxPositions)
                    {
                        position++;
                    }
                    if (position > maxPositions) break; // Không còn slot trống

                    // Insert vào card_generals_equipment
                    string insertQuery = @"
                    INSERT INTO card_generals_equipment
                        (user_id, card_general_id, equipment_id, position)
                    VALUES
                        (@user_id, @card_general_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@card_general_id", cardGeneralId);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);
                        await insertCommand.ExecuteNonQueryAsync();
                    }

                    usedPositions.Add(position);
                    equippedCount++;
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments of type: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task<bool> EquipAllEquipmentsToCardGeneralAsync(string cardGeneralId, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Lấy tất cả type từ equipment_type
                string getTypesQuery = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand getTypesCmd = new MySqlCommand(getTypesQuery, connection))
                {
                    await using (MySqlDataReader reader = await getTypesCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            types.Add(reader.GetStringSafe("type"));
                        }
                    }
                }

                // Cho mỗi type, gọi hàm EquipAllEquipmentsOfTypeToCardHeroAsync với list đã filter
                foreach (string type in types)
                {
                    bool success = await EquipAllEquipmentsOfTypeToCardGeneralAsync(cardGeneralId, type, availableEquipments);
                    if (!success)
                    {
                        Debug.LogWarning($"Failed to equip type '{type}' for card general '{cardGeneralId}'.");
                        // Tiếp tục với type khác
                    }
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }

    public async Task<bool> EquipAllEquipmentsOfTypeToCardAdmiralAsync(string cardAdmiralId, string type, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Bước 1: Lấy max_positions cho type từ bảng equipment_type
                string getMaxPositionsQuery = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand getMaxCmd = new MySqlCommand(getMaxPositionsQuery, connection))
                {
                    getMaxCmd.Parameters.AddWithValue("@type", type);
                    object result = await getMaxCmd.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                    {
                        maxPositions = Convert.ToInt32(result);
                    }
                    else
                    {
                        Debug.LogError($"Type '{type}' not found in equipment_type table.");
                        return false;
                    }
                }

                // Bước 2: Lấy danh sách position đã dùng cho card hero và type này
                string getUsedPositionsQuery = @"
                SELECT cge.position
                FROM card_admirals_equipment cge
                JOIN user_equipments ue ON cge.equipment_id = ue.equipment_id 
                JOIN Equipments e ON e.id = ue.equipment_id
                WHERE cge.card_admiral_id = @card_admiral_id AND e.type = @type AND ue.user_id = @user_id";
                HashSet<int> usedPositions = new HashSet<int>();
                await using (MySqlCommand getUsedCmd = new MySqlCommand(getUsedPositionsQuery, connection))
                {
                    getUsedCmd.Parameters.AddWithValue("@card_admiral_id", cardAdmiralId);
                    getUsedCmd.Parameters.AddWithValue("@type", type);
                    getUsedCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    await using (MySqlDataReader reader = await getUsedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            usedPositions.Add(reader.GetIntSafe("position"));
                        }
                    }
                }

                // Bước 3: Filter availableEquipments để loại bỏ equipment đã equip cho card này và chỉ lấy type khớp
                List<string> equippedEquipmentIds = new List<string>();
                string getEquippedQuery = @"
                SELECT equipment_id 
                FROM card_admirals_equipment 
                WHERE card_admiral_id = @card_admiral_id";
                await using (MySqlCommand getEquippedCmd = new MySqlCommand(getEquippedQuery, connection))
                {
                    getEquippedCmd.Parameters.AddWithValue("@card_admiral_id", cardAdmiralId);
                    await using (MySqlDataReader reader = await getEquippedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            equippedEquipmentIds.Add(reader.GetStringSafe("equipment_id"));
                        }
                    }
                }

                List<Equipments> filteredEquipments = availableEquipments
                    .Where(e => e.Type == type && !equippedEquipmentIds.Contains(e.Id))
                    .OrderByDescending(e => e.Power) // Đảm bảo sort giảm dần (list đã sort, nhưng để an toàn)
                    .ToList();

                // Bước 4: Equip equipment vào vị trí trống
                int equippedCount = 0;
                foreach (var equipment in filteredEquipments)
                {
                    if (equippedCount >= maxPositions - usedPositions.Count) break; // Đủ slot

                    int position = 1;
                    while (usedPositions.Contains(position) && position <= maxPositions)
                    {
                        position++;
                    }
                    if (position > maxPositions) break; // Không còn slot trống

                    // Insert vào card_admirals_equipment
                    string insertQuery = @"
                    INSERT INTO card_admirals_equipment
                        (user_id, card_admiral_id, equipment_id, position)
                    VALUES
                        (@user_id, @card_admiral_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@card_admiral_id", cardAdmiralId);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);
                        await insertCommand.ExecuteNonQueryAsync();
                    }

                    usedPositions.Add(position);
                    equippedCount++;
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments of type: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task<bool> EquipAllEquipmentsToCardAdmiralAsync(string cardAdmiralId, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Lấy tất cả type từ equipment_type
                string getTypesQuery = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand getTypesCmd = new MySqlCommand(getTypesQuery, connection))
                {
                    await using (MySqlDataReader reader = await getTypesCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            types.Add(reader.GetStringSafe("type"));
                        }
                    }
                }

                // Cho mỗi type, gọi hàm EquipAllEquipmentsOfTypeToCardHeroAsync với list đã filter
                foreach (string type in types)
                {
                    bool success = await EquipAllEquipmentsOfTypeToCardAdmiralAsync(cardAdmiralId, type, availableEquipments);
                    if (!success)
                    {
                        Debug.LogWarning($"Failed to equip type '{type}' for card admiral '{cardAdmiralId}'.");
                        // Tiếp tục với type khác
                    }
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }

    public async Task<bool> EquipAllEquipmentsOfTypeToCardMonsterAsync(string cardMonsterId, string type, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Bước 1: Lấy max_positions cho type từ bảng equipment_type
                string getMaxPositionsQuery = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand getMaxCmd = new MySqlCommand(getMaxPositionsQuery, connection))
                {
                    getMaxCmd.Parameters.AddWithValue("@type", type);
                    object result = await getMaxCmd.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                    {
                        maxPositions = Convert.ToInt32(result);
                    }
                    else
                    {
                        Debug.LogError($"Type '{type}' not found in equipment_type table.");
                        return false;
                    }
                }

                // Bước 2: Lấy danh sách position đã dùng cho card hero và type này
                string getUsedPositionsQuery = @"
                SELECT che.position
                FROM card_monsters_equipment che
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                JOIN Equipments e ON e.id = ue.equipment_id
                WHERE che.card_monster_id = @card_monster_id AND e.type = @type AND ue.user_id = @user_id";
                HashSet<int> usedPositions = new HashSet<int>();
                await using (MySqlCommand getUsedCmd = new MySqlCommand(getUsedPositionsQuery, connection))
                {
                    getUsedCmd.Parameters.AddWithValue("@card_monster_id", cardMonsterId);
                    getUsedCmd.Parameters.AddWithValue("@type", type);
                    getUsedCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    await using (MySqlDataReader reader = await getUsedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            usedPositions.Add(reader.GetIntSafe("position"));
                        }
                    }
                }

                // Bước 3: Filter availableEquipments để loại bỏ equipment đã equip cho card này và chỉ lấy type khớp
                List<string> equippedEquipmentIds = new List<string>();
                string getEquippedQuery = @"
                SELECT equipment_id 
                FROM card_monsters_equipment 
                WHERE card_monster_id = @card_monster_id";
                await using (MySqlCommand getEquippedCmd = new MySqlCommand(getEquippedQuery, connection))
                {
                    getEquippedCmd.Parameters.AddWithValue("@card_monster_id", cardMonsterId);
                    await using (MySqlDataReader reader = await getEquippedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            equippedEquipmentIds.Add(reader.GetStringSafe("equipment_id"));
                        }
                    }
                }

                List<Equipments> filteredEquipments = availableEquipments
                    .Where(e => e.Type == type && !equippedEquipmentIds.Contains(e.Id))
                    .OrderByDescending(e => e.Power) // Đảm bảo sort giảm dần (list đã sort, nhưng để an toàn)
                    .ToList();

                // Bước 4: Equip equipment vào vị trí trống
                int equippedCount = 0;
                foreach (var equipment in filteredEquipments)
                {
                    if (equippedCount >= maxPositions - usedPositions.Count) break; // Đủ slot

                    int position = 1;
                    while (usedPositions.Contains(position) && position <= maxPositions)
                    {
                        position++;
                    }
                    if (position > maxPositions) break; // Không còn slot trống

                    // Insert vào card_monsters_equipment
                    string insertQuery = @"
                    INSERT INTO card_monsters_equipment
                        (user_id, card_monster_id, equipment_id, position)
                    VALUES
                        (@user_id, @card_monster_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@card_monster_id", cardMonsterId);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);
                        await insertCommand.ExecuteNonQueryAsync();
                    }

                    usedPositions.Add(position);
                    equippedCount++;
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments of type: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task<bool> EquipAllEquipmentsToCardMonsterAsync(string cardMonsterId, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Lấy tất cả type từ equipment_type
                string getTypesQuery = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand getTypesCmd = new MySqlCommand(getTypesQuery, connection))
                {
                    await using (MySqlDataReader reader = await getTypesCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            types.Add(reader.GetStringSafe("type"));
                        }
                    }
                }

                // Cho mỗi type, gọi hàm EquipAllEquipmentsOfTypeToCardHeroAsync với list đã filter
                foreach (string type in types)
                {
                    bool success = await EquipAllEquipmentsOfTypeToCardMonsterAsync(cardMonsterId, type, availableEquipments);
                    if (!success)
                    {
                        Debug.LogWarning($"Failed to equip type '{type}' for card monster '{cardMonsterId}'.");
                        // Tiếp tục với type khác
                    }
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }

    public async Task<bool> EquipAllEquipmentsOfTypeToCardMilitaryAsync(string cardMilitaryId, string type, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Bước 1: Lấy max_positions cho type từ bảng equipment_type
                string getMaxPositionsQuery = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand getMaxCmd = new MySqlCommand(getMaxPositionsQuery, connection))
                {
                    getMaxCmd.Parameters.AddWithValue("@type", type);
                    object result = await getMaxCmd.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                    {
                        maxPositions = Convert.ToInt32(result);
                    }
                    else
                    {
                        Debug.LogError($"Type '{type}' not found in equipment_type table.");
                        return false;
                    }
                }

                // Bước 2: Lấy danh sách position đã dùng cho card hero và type này
                string getUsedPositionsQuery = @"
                SELECT cme.position
                FROM card_militaries_equipment cme
                JOIN user_equipments ue ON cme.equipment_id = ue.equipment_id 
                JOIN Equipments e ON e.id = ue.equipment_id
                WHERE cme.card_military_id = @card_military_id AND e.type = @type AND ue.user_id = @user_id";
                HashSet<int> usedPositions = new HashSet<int>();
                await using (MySqlCommand getUsedCmd = new MySqlCommand(getUsedPositionsQuery, connection))
                {
                    getUsedCmd.Parameters.AddWithValue("@card_military_id", cardMilitaryId);
                    getUsedCmd.Parameters.AddWithValue("@type", type);
                    getUsedCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    await using (MySqlDataReader reader = await getUsedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            usedPositions.Add(reader.GetIntSafe("position"));
                        }
                    }
                }

                // Bước 3: Filter availableEquipments để loại bỏ equipment đã equip cho card này và chỉ lấy type khớp
                List<string> equippedEquipmentIds = new List<string>();
                string getEquippedQuery = @"
                SELECT equipment_id 
                FROM card_militaries_equipment 
                WHERE card_military_id = @card_military_id";
                await using (MySqlCommand getEquippedCmd = new MySqlCommand(getEquippedQuery, connection))
                {
                    getEquippedCmd.Parameters.AddWithValue("@card_military_id", cardMilitaryId);
                    await using (MySqlDataReader reader = await getEquippedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            equippedEquipmentIds.Add(reader.GetStringSafe("equipment_id"));
                        }
                    }
                }

                List<Equipments> filteredEquipments = availableEquipments
                    .Where(e => e.Type == type && !equippedEquipmentIds.Contains(e.Id))
                    .OrderByDescending(e => e.Power) // Đảm bảo sort giảm dần (list đã sort, nhưng để an toàn)
                    .ToList();

                // Bước 4: Equip equipment vào vị trí trống
                int equippedCount = 0;
                foreach (var equipment in filteredEquipments)
                {
                    if (equippedCount >= maxPositions - usedPositions.Count) break; // Đủ slot

                    int position = 1;
                    while (usedPositions.Contains(position) && position <= maxPositions)
                    {
                        position++;
                    }
                    if (position > maxPositions) break; // Không còn slot trống

                    // Insert vào card_militaries_equipment
                    string insertQuery = @"
                    INSERT INTO card_militaries_equipment
                        (user_id, card_military_id, equipment_id, position)
                    VALUES
                        (@user_id, @card_military_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@card_military_id", cardMilitaryId);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);
                        await insertCommand.ExecuteNonQueryAsync();
                    }

                    usedPositions.Add(position);
                    equippedCount++;
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments of type: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task<bool> EquipAllEquipmentsToCardMilitaryAsync(string cardMilitaryId, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Lấy tất cả type từ equipment_type
                string getTypesQuery = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand getTypesCmd = new MySqlCommand(getTypesQuery, connection))
                {
                    await using (MySqlDataReader reader = await getTypesCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            types.Add(reader.GetStringSafe("type"));
                        }
                    }
                }

                // Cho mỗi type, gọi hàm EquipAllEquipmentsOfTypeToCardHeroAsync với list đã filter
                foreach (string type in types)
                {
                    bool success = await EquipAllEquipmentsOfTypeToCardMilitaryAsync(cardMilitaryId, type, availableEquipments);
                    if (!success)
                    {
                        Debug.LogWarning($"Failed to equip type '{type}' for card military '{cardMilitaryId}'.");
                        // Tiếp tục với type khác
                    }
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }

    public async Task<bool> EquipAllEquipmentsOfTypeToCardSpellAsync(string cardSpellId, string type, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Bước 1: Lấy max_positions cho type từ bảng equipment_type
                string getMaxPositionsQuery = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand getMaxCmd = new MySqlCommand(getMaxPositionsQuery, connection))
                {
                    getMaxCmd.Parameters.AddWithValue("@type", type);
                    object result = await getMaxCmd.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                    {
                        maxPositions = Convert.ToInt32(result);
                    }
                    else
                    {
                        Debug.LogError($"Type '{type}' not found in equipment_type table.");
                        return false;
                    }
                }

                // Bước 2: Lấy danh sách position đã dùng cho card hero và type này
                string getUsedPositionsQuery = @"
                SELECT che.position
                FROM card_spells_equipment che
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                JOIN Equipments e ON e.id = ue.equipment_id
                WHERE che.card_spell_id = @card_spell_id AND e.type = @type AND ue.user_id = @user_id";
                HashSet<int> usedPositions = new HashSet<int>();
                await using (MySqlCommand getUsedCmd = new MySqlCommand(getUsedPositionsQuery, connection))
                {
                    getUsedCmd.Parameters.AddWithValue("@card_spell_id", cardSpellId);
                    getUsedCmd.Parameters.AddWithValue("@type", type);
                    getUsedCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    await using (MySqlDataReader reader = await getUsedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            usedPositions.Add(reader.GetIntSafe("position"));
                        }
                    }
                }

                // Bước 3: Filter availableEquipments để loại bỏ equipment đã equip cho card này và chỉ lấy type khớp
                List<string> equippedEquipmentIds = new List<string>();
                string getEquippedQuery = @"
                SELECT equipment_id 
                FROM card_spells_equipment 
                WHERE card_spell_id = @card_spell_id";
                await using (MySqlCommand getEquippedCmd = new MySqlCommand(getEquippedQuery, connection))
                {
                    getEquippedCmd.Parameters.AddWithValue("@card_spell_id", cardSpellId);
                    await using (MySqlDataReader reader = await getEquippedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            equippedEquipmentIds.Add(reader.GetStringSafe("equipment_id"));
                        }
                    }
                }

                List<Equipments> filteredEquipments = availableEquipments
                    .Where(e => e.Type == type && !equippedEquipmentIds.Contains(e.Id))
                    .OrderByDescending(e => e.Power) // Đảm bảo sort giảm dần (list đã sort, nhưng để an toàn)
                    .ToList();

                // Bước 4: Equip equipment vào vị trí trống
                int equippedCount = 0;
                foreach (var equipment in filteredEquipments)
                {
                    if (equippedCount >= maxPositions - usedPositions.Count) break; // Đủ slot

                    int position = 1;
                    while (usedPositions.Contains(position) && position <= maxPositions)
                    {
                        position++;
                    }
                    if (position > maxPositions) break; // Không còn slot trống

                    // Insert vào card_spells_equipment
                    string insertQuery = @"
                    INSERT INTO card_spells_equipment
                        (user_id, card_spell_id, equipment_id, position)
                    VALUES
                        (@user_id, @card_spell_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@card_spell_id", cardSpellId);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);
                        await insertCommand.ExecuteNonQueryAsync();
                    }

                    usedPositions.Add(position);
                    equippedCount++;
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments of type: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task<bool> EquipAllEquipmentsToCardSpellAsync(string cardSpellId, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Lấy tất cả type từ equipment_type
                string getTypesQuery = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand getTypesCmd = new MySqlCommand(getTypesQuery, connection))
                {
                    await using (MySqlDataReader reader = await getTypesCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            types.Add(reader.GetStringSafe("type"));
                        }
                    }
                }

                // Cho mỗi type, gọi hàm EquipAllEquipmentsOfTypeToCardSpellAsync với list đã filter
                foreach (string type in types)
                {
                    bool success = await EquipAllEquipmentsOfTypeToCardSpellAsync(cardSpellId, type, availableEquipments);
                    if (!success)
                    {
                        Debug.LogWarning($"Failed to equip type '{type}' for card spell '{cardSpellId}'.");
                        // Tiếp tục với type khác
                    }
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }

    public async Task<bool> EquipAllEquipmentsOfTypeToBookAsync(string bookId, string type, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Bước 1: Lấy max_positions cho type từ bảng equipment_type
                string getMaxPositionsQuery = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand getMaxCmd = new MySqlCommand(getMaxPositionsQuery, connection))
                {
                    getMaxCmd.Parameters.AddWithValue("@type", type);
                    object result = await getMaxCmd.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                    {
                        maxPositions = Convert.ToInt32(result);
                    }
                    else
                    {
                        Debug.LogError($"Type '{type}' not found in equipment_type table.");
                        return false;
                    }
                }

                // Bước 2: Lấy danh sách position đã dùng cho card hero và type này
                string getUsedPositionsQuery = @"
                SELECT che.position
                FROM books_equipment che
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                JOIN Equipments e ON e.id = ue.equipment_id
                WHERE che.book_id = @book_id AND e.type = @type AND ue.user_id = @user_id";
                HashSet<int> usedPositions = new HashSet<int>();
                await using (MySqlCommand getUsedCmd = new MySqlCommand(getUsedPositionsQuery, connection))
                {
                    getUsedCmd.Parameters.AddWithValue("@book_id", bookId);
                    getUsedCmd.Parameters.AddWithValue("@type", type);
                    getUsedCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    await using (MySqlDataReader reader = await getUsedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            usedPositions.Add(reader.GetIntSafe("position"));
                        }
                    }
                }

                // Bước 3: Filter availableEquipments để loại bỏ equipment đã equip cho card này và chỉ lấy type khớp
                List<string> equippedEquipmentIds = new List<string>();
                string getEquippedQuery = @"
                SELECT equipment_id 
                FROM books_equipment 
                WHERE book_id = @book_id";
                await using (MySqlCommand getEquippedCmd = new MySqlCommand(getEquippedQuery, connection))
                {
                    getEquippedCmd.Parameters.AddWithValue("@book_id", bookId);
                    await using (MySqlDataReader reader = await getEquippedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            equippedEquipmentIds.Add(reader.GetStringSafe("equipment_id"));
                        }
                    }
                }

                List<Equipments> filteredEquipments = availableEquipments
                    .Where(e => e.Type == type && !equippedEquipmentIds.Contains(e.Id))
                    .OrderByDescending(e => e.Power) // Đảm bảo sort giảm dần (list đã sort, nhưng để an toàn)
                    .ToList();

                // Bước 4: Equip equipment vào vị trí trống
                int equippedCount = 0;
                foreach (var equipment in filteredEquipments)
                {
                    if (equippedCount >= maxPositions - usedPositions.Count) break; // Đủ slot

                    int position = 1;
                    while (usedPositions.Contains(position) && position <= maxPositions)
                    {
                        position++;
                    }
                    if (position > maxPositions) break; // Không còn slot trống

                    // Insert vào card_heroes_equipment
                    string insertQuery = @"
                    INSERT INTO books_equipment
                        (user_id, book_id, equipment_id, position)
                    VALUES
                        (@user_id, @book_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@book_id", bookId);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);
                        await insertCommand.ExecuteNonQueryAsync();
                    }

                    usedPositions.Add(position);
                    equippedCount++;
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments of type: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task<bool> EquipAllEquipmentsToBookAsync(string bookId, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Lấy tất cả type từ equipment_type
                string getTypesQuery = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand getTypesCmd = new MySqlCommand(getTypesQuery, connection))
                {
                    await using (MySqlDataReader reader = await getTypesCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            types.Add(reader.GetStringSafe("type"));
                        }
                    }
                }

                // Cho mỗi type, gọi hàm EquipAllEquipmentsOfTypeToBookAsync với list đã filter
                foreach (string type in types)
                {
                    bool success = await EquipAllEquipmentsOfTypeToBookAsync(bookId, type, availableEquipments);
                    if (!success)
                    {
                        Debug.LogWarning($"Failed to equip type '{type}' for card book '{bookId}'.");
                        // Tiếp tục với type khác
                    }
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }

    public async Task<bool> EquipAllEquipmentsOfTypeToPetAsync(string petId, string type, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Bước 1: Lấy max_positions cho type từ bảng equipment_type
                string getMaxPositionsQuery = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand getMaxCmd = new MySqlCommand(getMaxPositionsQuery, connection))
                {
                    getMaxCmd.Parameters.AddWithValue("@type", type);
                    object result = await getMaxCmd.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                    {
                        maxPositions = Convert.ToInt32(result);
                    }
                    else
                    {
                        Debug.LogError($"Type '{type}' not found in equipment_type table.");
                        return false;
                    }
                }

                // Bước 2: Lấy danh sách position đã dùng cho card hero và type này
                string getUsedPositionsQuery = @"
                SELECT che.position
                FROM pets_equipment che
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                JOIN Equipments e ON e.id = ue.equipment_id
                WHERE che.pet_id = @pet_id AND e.type = @type AND ue.user_id = @user_id";
                HashSet<int> usedPositions = new HashSet<int>();
                await using (MySqlCommand getUsedCmd = new MySqlCommand(getUsedPositionsQuery, connection))
                {
                    getUsedCmd.Parameters.AddWithValue("@pet_id", petId);
                    getUsedCmd.Parameters.AddWithValue("@type", type);
                    getUsedCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    await using (MySqlDataReader reader = await getUsedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            usedPositions.Add(reader.GetIntSafe("position"));
                        }
                    }
                }

                // Bước 3: Filter availableEquipments để loại bỏ equipment đã equip cho card này và chỉ lấy type khớp
                List<string> equippedEquipmentIds = new List<string>();
                string getEquippedQuery = @"
                SELECT equipment_id 
                FROM pets_equipment 
                WHERE pet_id = @pet_id";
                await using (MySqlCommand getEquippedCmd = new MySqlCommand(getEquippedQuery, connection))
                {
                    getEquippedCmd.Parameters.AddWithValue("@pet_id", petId);
                    await using (MySqlDataReader reader = await getEquippedCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            equippedEquipmentIds.Add(reader.GetStringSafe("equipment_id"));
                        }
                    }
                }

                List<Equipments> filteredEquipments = availableEquipments
                    .Where(e => e.Type == type && !equippedEquipmentIds.Contains(e.Id))
                    .OrderByDescending(e => e.Power) // Đảm bảo sort giảm dần (list đã sort, nhưng để an toàn)
                    .ToList();

                // Bước 4: Equip equipment vào vị trí trống
                int equippedCount = 0;
                foreach (var equipment in filteredEquipments)
                {
                    if (equippedCount >= maxPositions - usedPositions.Count) break; // Đủ slot

                    int position = 1;
                    while (usedPositions.Contains(position) && position <= maxPositions)
                    {
                        position++;
                    }
                    if (position > maxPositions) break; // Không còn slot trống

                    // Insert vào card_heroes_equipment
                    string insertQuery = @"
                    INSERT INTO pets_equipment
                        (user_id, pet_id, equipment_id, position)
                    VALUES
                        (@user_id, @pet_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        insertCommand.Parameters.AddWithValue("@pet_id", petId);
                        insertCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                        insertCommand.Parameters.AddWithValue("@position", position);
                        await insertCommand.ExecuteNonQueryAsync();
                    }

                    usedPositions.Add(position);
                    equippedCount++;
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments of type: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task<bool> EquipAllEquipmentsToPetAsync(string petId, List<Equipments> availableEquipments)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Lấy tất cả type từ equipment_type
                string getTypesQuery = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand getTypesCmd = new MySqlCommand(getTypesQuery, connection))
                {
                    await using (MySqlDataReader reader = await getTypesCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            types.Add(reader.GetStringSafe("type"));
                        }
                    }
                }

                // Cho mỗi type, gọi hàm EquipAllEquipmentsOfTypeToPetAsync với list đã filter
                foreach (string type in types)
                {
                    bool success = await EquipAllEquipmentsOfTypeToPetAsync(petId, type, availableEquipments);
                    if (!success)
                    {
                        Debug.LogWarning($"Failed to equip type '{type}' for card hero '{petId}'.");
                        // Tiếp tục với type khác
                    }
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error equipping all equipments: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}