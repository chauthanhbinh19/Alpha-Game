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

                string selectSQL = @"
                SELECT e.id, e.name, ue.*, e.image, e.rare, e.type
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                WHERE ue.user_id = @userId";

                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND e.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND e.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND e.name LIKE CONCAT('%', @search, '%')";
                }

                selectSQL += @"LIMIT @limit OFFSET @offset";

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

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT e.id, e.name, ue.*, e.image, e.rare, e.type
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                WHERE ue.user_id = @userId;
            ";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", user_id);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT COUNT(*)
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                WHERE ue.user_id = @userId
            ";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND e.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND e.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND e.name LIKE CONCAT('%', @search, '%')";
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
    public async Task<Equipments> GetUserEquipmentsByIdAsync(string user_id, string Id)
    {
        Equipments equipment = new Equipments();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"SELECT * 
                             FROM user_equipments 
                             WHERE equipment_id = @id AND user_id = @user_id";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@id", Id);
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string checkSQL = @"
                SELECT COUNT(*) 
                FROM user_equipments 
                WHERE user_id = @user_id AND equipment_id = @equipment_id";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCommand.Parameters.AddWithValue("@equipment_id", Id);

                    var existingCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());
                    if (existingCount > 0)
                    {
                        string updateSQL = @"
                        UPDATE user_equipments
                        SET quality = quality + @quantity
                        WHERE user_id = @user_id AND equipment_id = @equipment_id";

                        await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCommand.Parameters.AddWithValue("@equipment_id", Id);
                            updateCommand.Parameters.AddWithValue("@quantity", quantity);
                            await updateCommand.ExecuteNonQueryAsync();
                        }

                        return true;
                    }
                }

                string insertSQL = @"
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

                await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
                {
                    insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    insertCommand.Parameters.AddWithValue("@equipment_id", Id);
                    insertCommand.Parameters.AddWithValue("@rare", equipmentFromDB.Rare);
                    insertCommand.Parameters.AddWithValue("@level", 0);
                    insertCommand.Parameters.AddWithValue("@experiment", 0);
                    insertCommand.Parameters.AddWithValue("@star", 0);
                    insertCommand.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(equipmentFromDB.Rare));
                    insertCommand.Parameters.AddWithValue("@block", false);
                    insertCommand.Parameters.AddWithValue("@quantity", quantity);
                    insertCommand.Parameters.AddWithValue("@power", equipmentFromDB.Power);
                    insertCommand.Parameters.AddWithValue("@health", equipmentFromDB.Health);
                    insertCommand.Parameters.AddWithValue("@physical_attack", equipmentFromDB.PhysicalAttack);
                    insertCommand.Parameters.AddWithValue("@physical_defense", equipmentFromDB.PhysicalDefense);
                    insertCommand.Parameters.AddWithValue("@magical_attack", equipmentFromDB.MagicalAttack);
                    insertCommand.Parameters.AddWithValue("@magical_defense", equipmentFromDB.MagicalDefense);
                    insertCommand.Parameters.AddWithValue("@chemical_attack", equipmentFromDB.ChemicalAttack);
                    insertCommand.Parameters.AddWithValue("@chemical_defense", equipmentFromDB.ChemicalDefense);
                    insertCommand.Parameters.AddWithValue("@atomic_attack", equipmentFromDB.AtomicAttack);
                    insertCommand.Parameters.AddWithValue("@atomic_defense", equipmentFromDB.AtomicDefense);
                    insertCommand.Parameters.AddWithValue("@mental_attack", equipmentFromDB.MentalAttack);
                    insertCommand.Parameters.AddWithValue("@mental_defense", equipmentFromDB.MentalDefense);
                    insertCommand.Parameters.AddWithValue("@speed", equipmentFromDB.Speed);
                    insertCommand.Parameters.AddWithValue("@critical_damage_rate", equipmentFromDB.CriticalDamageRate);
                    insertCommand.Parameters.AddWithValue("@critical_rate", equipmentFromDB.CriticalRate);
                    insertCommand.Parameters.AddWithValue("@critical_resistance_rate", equipmentFromDB.CriticalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@ignore_critical_rate", equipmentFromDB.IgnoreCriticalRate);
                    insertCommand.Parameters.AddWithValue("@penetration_rate", equipmentFromDB.PenetrationRate);
                    insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", equipmentFromDB.PenetrationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@evasion_rate", equipmentFromDB.EvasionRate);
                    insertCommand.Parameters.AddWithValue("@damage_absorption_rate", equipmentFromDB.DamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", equipmentFromDB.IgnoreDamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", equipmentFromDB.AbsorbedDamageRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", equipmentFromDB.VitalityRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", equipmentFromDB.VitalityRegenerationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@accuracy_rate", equipmentFromDB.AccuracyRate);
                    insertCommand.Parameters.AddWithValue("@lifesteal_rate", equipmentFromDB.LifestealRate);
                    insertCommand.Parameters.AddWithValue("@shield_strength", equipmentFromDB.ShieldStrength);
                    insertCommand.Parameters.AddWithValue("@tenacity", equipmentFromDB.Tenacity);
                    insertCommand.Parameters.AddWithValue("@resistance_rate", equipmentFromDB.ResistanceRate);
                    insertCommand.Parameters.AddWithValue("@combo_rate", equipmentFromDB.ComboRate);
                    insertCommand.Parameters.AddWithValue("@ignore_combo_rate", equipmentFromDB.IgnoreComboRate);
                    insertCommand.Parameters.AddWithValue("@combo_damage_rate", equipmentFromDB.ComboDamageRate);
                    insertCommand.Parameters.AddWithValue("@combo_resistance_rate", equipmentFromDB.ComboResistanceRate);
                    insertCommand.Parameters.AddWithValue("@stun_rate", equipmentFromDB.StunRate);
                    insertCommand.Parameters.AddWithValue("@ignore_stun_rate", equipmentFromDB.IgnoreStunRate);
                    insertCommand.Parameters.AddWithValue("@reflection_rate", equipmentFromDB.ReflectionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", equipmentFromDB.IgnoreReflectionRate);
                    insertCommand.Parameters.AddWithValue("@reflection_damage_rate", equipmentFromDB.ReflectionDamageRate);
                    insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", equipmentFromDB.ReflectionResistanceRate);
                    insertCommand.Parameters.AddWithValue("@mana", equipmentFromDB.Mana);
                    insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", equipmentFromDB.ManaRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", equipmentFromDB.DamageToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", equipmentFromDB.ResistanceToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", equipmentFromDB.DamageToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", equipmentFromDB.ResistanceToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@normal_damage_rate", equipmentFromDB.NormalDamageRate);
                    insertCommand.Parameters.AddWithValue("@normal_resistance_rate", equipmentFromDB.NormalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@skill_damage_rate", equipmentFromDB.SkillDamageRate);
                    insertCommand.Parameters.AddWithValue("@skill_resistance_rate", equipmentFromDB.SkillResistanceRate);
                    insertCommand.Parameters.AddWithValue("@special_health", equipmentFromDB.SpecialHealth);
                    insertCommand.Parameters.AddWithValue("@special_physical_attack", equipmentFromDB.SpecialPhysicalAttack);
                    insertCommand.Parameters.AddWithValue("@special_physical_defense", equipmentFromDB.SpecialPhysicalDefense);
                    insertCommand.Parameters.AddWithValue("@special_magical_attack", equipmentFromDB.SpecialMagicalAttack);
                    insertCommand.Parameters.AddWithValue("@special_magical_defense", equipmentFromDB.SpecialMagicalDefense);
                    insertCommand.Parameters.AddWithValue("@special_chemical_attack", equipmentFromDB.SpecialChemicalAttack);
                    insertCommand.Parameters.AddWithValue("@special_chemical_defense", equipmentFromDB.SpecialChemicalDefense);
                    insertCommand.Parameters.AddWithValue("@special_atomic_attack", equipmentFromDB.SpecialAtomicAttack);
                    insertCommand.Parameters.AddWithValue("@special_atomic_defense", equipmentFromDB.SpecialAtomicDefense);
                    insertCommand.Parameters.AddWithValue("@special_mental_attack", equipmentFromDB.SpecialMentalAttack);
                    insertCommand.Parameters.AddWithValue("@special_mental_defense", equipmentFromDB.SpecialMentalDefense);
                    insertCommand.Parameters.AddWithValue("@special_speed", equipmentFromDB.SpecialSpeed);

                    await insertCommand.ExecuteNonQueryAsync();
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
    public async Task<bool> InsertOrUpdateUserEquipmentsBatchAsync(List<(string equipmentId, Equipments data, double quantity)> list)
    {
        if (list == null || list.Count == 0)
            return true;

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();
            await using var transaction = await connection.BeginTransactionAsync();

            int batchSize = 300; // nhiều column → giảm batch

            for (int i = 0; i < list.Count; i += batchSize)
            {
                var batch = list.Skip(i).Take(batchSize).ToList();

                var stringBuilder = new System.Text.StringBuilder();
                var parameters = new List<MySqlParameter>();

                stringBuilder.Append(@"
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
                ) VALUES ");

                for (int j = 0; j < batch.Count; j++)
                {
                    var item = batch[j];
                    var e = item.data;

                    stringBuilder.Append($@"
                    (@user_id, @equipment_id_{j}, @rare_{j}, 0, 0, 0, @quality_{j}, 0, @quantity_{j},
                    @power_{j}, @health_{j}, @physical_attack_{j}, @physical_defense_{j}, @magical_attack_{j}, @magical_defense_{j},
                    @chemical_attack_{j}, @chemical_defense_{j}, @atomic_attack_{j}, @atomic_defense_{j}, @mental_attack_{j}, @mental_defense_{j},
                    @speed_{j}, @critical_damage_rate_{j}, @critical_rate_{j}, @critical_resistance_rate_{j}, @ignore_critical_rate_{j},
                    @penetration_rate_{j}, @penetration_resistance_rate_{j}, @evasion_rate_{j}, @damage_absorption_rate_{j},
                    @ignore_damage_absorption_rate_{j}, @absorbed_damage_rate_{j}, @vitality_regeneration_rate_{j},
                    @vitality_regeneration_resistance_rate_{j}, @accuracy_rate_{j}, @lifesteal_rate_{j}, @shield_strength_{j}, @tenacity_{j},
                    @resistance_rate_{j}, @combo_rate_{j}, @ignore_combo_rate_{j}, @combo_damage_rate_{j}, @combo_resistance_rate_{j},
                    @stun_rate_{j}, @ignore_stun_rate_{j}, @reflection_rate_{j}, @ignore_reflection_rate_{j}, @reflection_damage_rate_{j},
                    @reflection_resistance_rate_{j}, @mana_{j}, @mana_regeneration_rate_{j}, @damage_to_different_faction_rate_{j},
                    @resistance_to_different_faction_rate_{j}, @damage_to_same_faction_rate_{j}, @resistance_to_same_faction_rate_{j},
                    @normal_damage_rate_{j}, @normal_resistance_rate_{j}, @skill_damage_rate_{j}, @skill_resistance_rate_{j},
                    @special_health_{j}, @special_physical_attack_{j}, @special_physical_defense_{j}, @special_magical_attack_{j},
                    @special_magical_defense_{j}, @special_chemical_attack_{j}, @special_chemical_defense_{j}, @special_atomic_attack_{j},
                    @special_atomic_defense_{j}, @special_mental_attack_{j}, @special_mental_defense_{j}, @special_speed_{j}
                    ),");

                    parameters.AddRange(new[]
                    {
                    new MySqlParameter($"@equipment_id_{j}", item.equipmentId),
                    new MySqlParameter($"@rare_{j}", e.Rare),
                    new MySqlParameter($"@quality_{j}", QualityEvaluatorHelper.CheckQuality(e.Rare)),
                    new MySqlParameter($"@quantity_{j}", item.quantity),
                    new MySqlParameter($"@power_{j}", e.Power),
                    new MySqlParameter($"@health_{j}", e.Health),
                    new MySqlParameter($"@physical_attack_{j}", e.PhysicalAttack),
                    new MySqlParameter($"@physical_defense_{j}", e.PhysicalDefense),
                    new MySqlParameter($"@magical_attack_{j}", e.MagicalAttack),
                    new MySqlParameter($"@magical_defense_{j}", e.MagicalDefense),
                    new MySqlParameter($"@chemical_attack_{j}", e.ChemicalAttack),
                    new MySqlParameter($"@chemical_defense_{j}", e.ChemicalDefense),
                    new MySqlParameter($"@atomic_attack_{j}", e.AtomicAttack),
                    new MySqlParameter($"@atomic_defense_{j}", e.AtomicDefense),
                    new MySqlParameter($"@mental_attack_{j}", e.MentalAttack),
                    new MySqlParameter($"@mental_defense_{j}", e.MentalDefense),
                    new MySqlParameter($"@speed_{j}", e.Speed),
                    new MySqlParameter($"@critical_damage_rate_{j}", e.CriticalDamageRate),
                    new MySqlParameter($"@critical_rate_{j}", e.CriticalRate),
                    new MySqlParameter($"@critical_resistance_rate_{j}", e.CriticalResistanceRate),
                    new MySqlParameter($"@ignore_critical_rate_{j}", e.IgnoreCriticalRate),
                    new MySqlParameter($"@penetration_rate_{j}", e.PenetrationRate),
                    new MySqlParameter($"@penetration_resistance_rate_{j}", e.PenetrationResistanceRate),
                    new MySqlParameter($"@evasion_rate_{j}", e.EvasionRate),
                    new MySqlParameter($"@damage_absorption_rate_{j}", e.DamageAbsorptionRate),
                    new MySqlParameter($"@ignore_damage_absorption_rate_{j}", e.IgnoreDamageAbsorptionRate),
                    new MySqlParameter($"@absorbed_damage_rate_{j}", e.AbsorbedDamageRate),
                    new MySqlParameter($"@vitality_regeneration_rate_{j}", e.VitalityRegenerationRate),
                    new MySqlParameter($"@vitality_regeneration_resistance_rate_{j}", e.VitalityRegenerationResistanceRate),
                    new MySqlParameter($"@accuracy_rate_{j}", e.AccuracyRate),
                    new MySqlParameter($"@lifesteal_rate_{j}", e.LifestealRate),
                    new MySqlParameter($"@shield_strength_{j}", e.ShieldStrength),
                    new MySqlParameter($"@tenacity_{j}", e.Tenacity),
                    new MySqlParameter($"@resistance_rate_{j}", e.ResistanceRate),
                    new MySqlParameter($"@combo_rate_{j}", e.ComboRate),
                    new MySqlParameter($"@ignore_combo_rate_{j}", e.IgnoreComboRate),
                    new MySqlParameter($"@combo_damage_rate_{j}", e.ComboDamageRate),
                    new MySqlParameter($"@combo_resistance_rate_{j}", e.ComboResistanceRate),
                    new MySqlParameter($"@stun_rate_{j}", e.StunRate),
                    new MySqlParameter($"@ignore_stun_rate_{j}", e.IgnoreStunRate),
                    new MySqlParameter($"@reflection_rate_{j}", e.ReflectionRate),
                    new MySqlParameter($"@ignore_reflection_rate_{j}", e.IgnoreReflectionRate),
                    new MySqlParameter($"@reflection_damage_rate_{j}", e.ReflectionDamageRate),
                    new MySqlParameter($"@reflection_resistance_rate_{j}", e.ReflectionResistanceRate),
                    new MySqlParameter($"@mana_{j}", e.Mana),
                    new MySqlParameter($"@mana_regeneration_rate_{j}", e.ManaRegenerationRate),
                    new MySqlParameter($"@damage_to_different_faction_rate_{j}", e.DamageToDifferentFactionRate),
                    new MySqlParameter($"@resistance_to_different_faction_rate_{j}", e.ResistanceToDifferentFactionRate),
                    new MySqlParameter($"@damage_to_same_faction_rate_{j}", e.DamageToSameFactionRate),
                    new MySqlParameter($"@resistance_to_same_faction_rate_{j}", e.ResistanceToSameFactionRate),
                    new MySqlParameter($"@normal_damage_rate_{j}", e.NormalDamageRate),
                    new MySqlParameter($"@normal_resistance_rate_{j}", e.NormalResistanceRate),
                    new MySqlParameter($"@skill_damage_rate_{j}", e.SkillDamageRate),
                    new MySqlParameter($"@skill_resistance_rate_{j}", e.SkillResistanceRate),
                    new MySqlParameter($"@special_health_{j}", e.SpecialHealth),
                    new MySqlParameter($"@special_physical_attack_{j}", e.SpecialPhysicalAttack),
                    new MySqlParameter($"@special_physical_defense_{j}", e.SpecialPhysicalDefense),
                    new MySqlParameter($"@special_magical_attack_{j}", e.SpecialMagicalAttack),
                    new MySqlParameter($"@special_magical_defense_{j}", e.SpecialMagicalDefense),
                    new MySqlParameter($"@special_chemical_attack_{j}", e.SpecialChemicalAttack),
                    new MySqlParameter($"@special_chemical_defense_{j}", e.SpecialChemicalDefense),
                    new MySqlParameter($"@special_atomic_attack_{j}", e.SpecialAtomicAttack),
                    new MySqlParameter($"@special_atomic_defense_{j}", e.SpecialAtomicDefense),
                    new MySqlParameter($"@special_mental_attack_{j}", e.SpecialMentalAttack),
                    new MySqlParameter($"@special_mental_defense_{j}", e.SpecialMentalDefense),
                    new MySqlParameter($"@special_speed_{j}", e.SpecialSpeed),
                });
                }

                stringBuilder.Length--;

                stringBuilder.Append(@"
                ON DUPLICATE KEY UPDATE
                    quantity = user_equipments.quantity + VALUES(quantity),
                    quality = user_equipments.quality + VALUES(quantity);
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
    public async Task<bool> UpdateEquipmentsLevelAsync(Equipments equipment, int level)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
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

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                    updateCommand.Parameters.AddWithValue("@level", level);
                    updateCommand.Parameters.AddWithValue("@power", equipment.Power);
                    updateCommand.Parameters.AddWithValue("@health", equipment.Health);
                    updateCommand.Parameters.AddWithValue("@physical_attack", equipment.PhysicalAttack);
                    updateCommand.Parameters.AddWithValue("@physical_defense", equipment.PhysicalDefense);
                    updateCommand.Parameters.AddWithValue("@magical_attack", equipment.MagicalAttack);
                    updateCommand.Parameters.AddWithValue("@magical_defense", equipment.MagicalDefense);
                    updateCommand.Parameters.AddWithValue("@chemical_attack", equipment.ChemicalAttack);
                    updateCommand.Parameters.AddWithValue("@chemical_defense", equipment.ChemicalDefense);
                    updateCommand.Parameters.AddWithValue("@atomic_attack", equipment.AtomicAttack);
                    updateCommand.Parameters.AddWithValue("@atomic_defense", equipment.AtomicDefense);
                    updateCommand.Parameters.AddWithValue("@mental_attack", equipment.MentalAttack);
                    updateCommand.Parameters.AddWithValue("@mental_defense", equipment.MentalDefense);
                    updateCommand.Parameters.AddWithValue("@speed", equipment.Speed);
                    updateCommand.Parameters.AddWithValue("@critical_damage_rate", equipment.CriticalDamageRate);
                    updateCommand.Parameters.AddWithValue("@critical_rate", equipment.CriticalRate);
                    updateCommand.Parameters.AddWithValue("@critical_resistance_rate", equipment.CriticalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@ignore_critical_rate", equipment.IgnoreCriticalRate);
                    updateCommand.Parameters.AddWithValue("@penetration_rate", equipment.PenetrationRate);
                    updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", equipment.PenetrationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@evasion_rate", equipment.EvasionRate);
                    updateCommand.Parameters.AddWithValue("@damage_absorption_rate", equipment.DamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", equipment.IgnoreDamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", equipment.AbsorbedDamageRate);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", equipment.VitalityRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", equipment.VitalityRegenerationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@accuracy_rate", equipment.AccuracyRate);
                    updateCommand.Parameters.AddWithValue("@lifesteal_rate", equipment.LifestealRate);
                    updateCommand.Parameters.AddWithValue("@shield_strength", equipment.ShieldStrength);
                    updateCommand.Parameters.AddWithValue("@tenacity", equipment.Tenacity);
                    updateCommand.Parameters.AddWithValue("@resistance_rate", equipment.ResistanceRate);
                    updateCommand.Parameters.AddWithValue("@combo_rate", equipment.ComboRate);
                    updateCommand.Parameters.AddWithValue("@ignore_combo_rate", equipment.IgnoreComboRate);
                    updateCommand.Parameters.AddWithValue("@combo_damage_rate", equipment.ComboDamageRate);
                    updateCommand.Parameters.AddWithValue("@combo_resistance_rate", equipment.ComboResistanceRate);
                    updateCommand.Parameters.AddWithValue("@stun_rate", equipment.StunRate);
                    updateCommand.Parameters.AddWithValue("@ignore_stun_rate", equipment.IgnoreStunRate);
                    updateCommand.Parameters.AddWithValue("@reflection_rate", equipment.ReflectionRate);
                    updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", equipment.IgnoreReflectionRate);
                    updateCommand.Parameters.AddWithValue("@reflection_damage_rate", equipment.ReflectionDamageRate);
                    updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", equipment.ReflectionResistanceRate);
                    updateCommand.Parameters.AddWithValue("@mana", equipment.Mana);
                    updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", equipment.ManaRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", equipment.DamageToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", equipment.ResistanceToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", equipment.DamageToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", equipment.ResistanceToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@normal_damage_rate", equipment.NormalDamageRate);
                    updateCommand.Parameters.AddWithValue("@normal_resistance_rate", equipment.NormalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@skill_damage_rate", equipment.SkillDamageRate);
                    updateCommand.Parameters.AddWithValue("@skill_resistance_rate", equipment.SkillResistanceRate);

                    await updateCommand.ExecuteNonQueryAsync();
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

                string updateSQL = @"
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

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                    updateCommand.Parameters.AddWithValue("@star", star);
                    updateCommand.Parameters.AddWithValue("@quantity", quantity);
                    updateCommand.Parameters.AddWithValue("@power", equipment.Power);
                    updateCommand.Parameters.AddWithValue("@health", equipment.Health);
                    updateCommand.Parameters.AddWithValue("@physical_attack", equipment.PhysicalAttack);
                    updateCommand.Parameters.AddWithValue("@physical_defense", equipment.PhysicalDefense);
                    updateCommand.Parameters.AddWithValue("@magical_attack", equipment.MagicalAttack);
                    updateCommand.Parameters.AddWithValue("@magical_defense", equipment.MagicalDefense);
                    updateCommand.Parameters.AddWithValue("@chemical_attack", equipment.ChemicalAttack);
                    updateCommand.Parameters.AddWithValue("@chemical_defense", equipment.ChemicalDefense);
                    updateCommand.Parameters.AddWithValue("@atomic_attack", equipment.AtomicAttack);
                    updateCommand.Parameters.AddWithValue("@atomic_defense", equipment.AtomicDefense);
                    updateCommand.Parameters.AddWithValue("@mental_attack", equipment.MentalAttack);
                    updateCommand.Parameters.AddWithValue("@mental_defense", equipment.MentalDefense);
                    updateCommand.Parameters.AddWithValue("@speed", equipment.Speed);
                    updateCommand.Parameters.AddWithValue("@critical_damage_rate", equipment.CriticalDamageRate);
                    updateCommand.Parameters.AddWithValue("@critical_rate", equipment.CriticalRate);
                    updateCommand.Parameters.AddWithValue("@critical_resistance_rate", equipment.CriticalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@ignore_critical_rate", equipment.IgnoreCriticalRate);
                    updateCommand.Parameters.AddWithValue("@penetration_rate", equipment.PenetrationRate);
                    updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", equipment.PenetrationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@evasion_rate", equipment.EvasionRate);
                    updateCommand.Parameters.AddWithValue("@damage_absorption_rate", equipment.DamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", equipment.IgnoreDamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", equipment.AbsorbedDamageRate);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", equipment.VitalityRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", equipment.VitalityRegenerationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@accuracy_rate", equipment.AccuracyRate);
                    updateCommand.Parameters.AddWithValue("@lifesteal_rate", equipment.LifestealRate);
                    updateCommand.Parameters.AddWithValue("@shield_strength", equipment.ShieldStrength);
                    updateCommand.Parameters.AddWithValue("@tenacity", equipment.Tenacity);
                    updateCommand.Parameters.AddWithValue("@resistance_rate", equipment.ResistanceRate);
                    updateCommand.Parameters.AddWithValue("@combo_rate", equipment.ComboRate);
                    updateCommand.Parameters.AddWithValue("@ignore_combo_rate", equipment.IgnoreComboRate);
                    updateCommand.Parameters.AddWithValue("@combo_damage_rate", equipment.ComboDamageRate);
                    updateCommand.Parameters.AddWithValue("@combo_resistance_rate", equipment.ComboResistanceRate);
                    updateCommand.Parameters.AddWithValue("@stun_rate", equipment.StunRate);
                    updateCommand.Parameters.AddWithValue("@ignore_stun_rate", equipment.IgnoreStunRate);
                    updateCommand.Parameters.AddWithValue("@reflection_rate", equipment.ReflectionRate);
                    updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", equipment.IgnoreReflectionRate);
                    updateCommand.Parameters.AddWithValue("@reflection_damage_rate", equipment.ReflectionDamageRate);
                    updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", equipment.ReflectionResistanceRate);
                    updateCommand.Parameters.AddWithValue("@mana", equipment.Mana);
                    updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", equipment.ManaRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", equipment.DamageToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", equipment.ResistanceToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", equipment.DamageToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", equipment.ResistanceToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@normal_damage_rate", equipment.NormalDamageRate);
                    updateCommand.Parameters.AddWithValue("@normal_resistance_rate", equipment.NormalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@skill_damage_rate", equipment.SkillDamageRate);
                    updateCommand.Parameters.AddWithValue("@skill_resistance_rate", equipment.SkillResistanceRate);

                    await updateCommand.ExecuteNonQueryAsync();
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
                string selectSQL = @"SELECT et.currency_id 
                             FROM equipments e
                             JOIN equipment_trade et ON e.id = et.equipment_id
                             WHERE e.id = @id";
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@id", Id);
                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        int currencyId = 0;

                        if (await reader.ReadAsync())
                        {
                            currencyId = reader.GetIntSafe("currency_id");
                        }

                        await reader.CloseAsync();

                        // Lấy quantity hiện tại
                        selectSQL = "SELECT quantity FROM user_currencies WHERE user_id = @user_id AND currency_id = @currency_id";
                        await using (MySqlCommand cmd2 = new MySqlCommand(selectSQL, connection))
                        {
                            cmd2.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            cmd2.Parameters.AddWithValue("@currency_id", currencyId);

                            object result = await cmd2.ExecuteScalarAsync();
                            double currentQuantity = result != DBNull.Value && result != null ? Convert.ToDouble(result) : 0;
                            double newQuantity = currentQuantity - amount;

                            // Cập nhật quantity mới
                            string updateSQL = "UPDATE user_currencies SET quantity=@quantity WHERE user_id=@user_id AND currency_id=@currency_id";
                            await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@quantity", newQuantity);
                                updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                                updateCommand.Parameters.AddWithValue("@currency_id", currencyId);

                                await updateCommand.ExecuteNonQueryAsync();
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
                string checkSQL = @"SELECT COUNT(*) FROM card_heroes_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteSQL = @"DELETE FROM card_heroes_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertSQL = @"INSERT INTO card_heroes_equipment 
                                       (user_id, card_hero_id, equipment_id, position)
                                       VALUES (@user_id, @card_hero_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string checkSQL = @"SELECT COUNT(*) FROM card_captains_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteSQL = @"DELETE FROM card_captains_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertSQL = @"INSERT INTO card_captains_equipment 
                                       (user_id, card_captain_id, equipment_id, position)
                                       VALUES (@user_id, @card_captain_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string checkSQL = @"SELECT COUNT(*) FROM card_colonels_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteSQL = @"DELETE FROM card_colonels_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertSQL = @"INSERT INTO card_colonels_equipment 
                                       (user_id, card_colonel_id, equipment_id, position)
                                       VALUES (@user_id, @card_colonel_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string checkSQL = @"SELECT COUNT(*) FROM card_generals_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteSQL = @"DELETE FROM card_generals_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertSQL = @"INSERT INTO card_generals_equipment 
                                       (user_id, card_general_id, equipment_id, position)
                                       VALUES (@user_id, @card_general_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string checkSQL = @"SELECT COUNT(*) FROM card_admirals_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteSQL = @"DELETE FROM card_admirals_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertSQL = @"INSERT INTO card_admirals_equipment 
                                       (user_id, card_admiral_id, equipment_id, position)
                                       VALUES (@user_id, @card_admiral_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string checkSQL = @"SELECT COUNT(*) FROM card_monsters_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteSQL = @"DELETE FROM card_monsters_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertSQL = @"INSERT INTO card_monsters_equipment 
                                       (user_id, card_monster_id, equipment_id, position)
                                       VALUES (@user_id, @card_monster_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string checkSQL = @"SELECT COUNT(*) FROM card_military_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteSQL = @"DELETE FROM card_military_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertSQL = @"INSERT INTO card_military_equipment 
                                       (user_id, card_military_id, equipment_id, position)
                                       VALUES (@user_id, @card_military_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string checkSQL = @"SELECT COUNT(*) FROM card_spell_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteSQL = @"DELETE FROM card_spell_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertSQL = @"INSERT INTO card_spell_equipment 
                                       (user_id, card_spell_id, equipment_id, position)
                                       VALUES (@user_id, @card_spell_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string checkSQL = @"SELECT COUNT(*) FROM books_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteSQL = @"DELETE FROM books_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertSQL = @"INSERT INTO books_equipment 
                                       (user_id, book_id, equipment_id, position)
                                       VALUES (@user_id, @book_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string checkSQL = @"SELECT COUNT(*) FROM pets_equipment 
                                  WHERE equipment_id = @equipment_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    // Nếu tồn tại, xóa các bản ghi cũ trước
                    if (count > 0)
                    {
                        string deleteSQL = @"DELETE FROM pets_equipment 
                                           WHERE equipment_id = @equipment_id";
                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@equipment_id", equipment.Id);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }

                    // Chèn dữ liệu mới vào bảng
                    string insertSQL = @"INSERT INTO pets_equipment 
                                       (user_id, pet_id, equipment_id, position)
                                       VALUES (@user_id, @pet_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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

                string selectSQL = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_heroes_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.card_hero_id = @card_hero_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@card_hero_id", card_id);
                    selectCommand.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_captains_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.card_captain_id = @card_captain_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@card_captain_id", card_id);
                    selectCommand.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_colonels_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.card_colonel_id = @card_colonel_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@card_colonel_id", card_id);
                    selectCommand.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_generals_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.card_general_id = @card_general_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@card_general_id", card_id);
                    selectCommand.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_admirals_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.card_admiral_id = @card_admiral_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@card_admiral_id", card_id);
                    selectCommand.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_monsters_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.card_monster_id = @card_monster_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@card_monster_id", card_id);
                    selectCommand.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_military_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.card_military_id = @card_military_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@card_military_id", card_id);
                    selectCommand.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_spell_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.card_spell_id = @card_spell_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@card_spell_id", card_id);
                    selectCommand.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_books_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.book_id = @book_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@book_id", card_id);
                    selectCommand.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_pets_equipment che ON che.equipment_id = ue.equipment_id 
                WHERE che.pet_id = @pet_id
                AND ue.user_id = @user_id
                AND e.type = @type;";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@pet_id", card_id);
                    selectCommand.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
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

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@type", type);
                    selectCommand.Parameters.AddWithValue("@limit", limit);
                    selectCommand.Parameters.AddWithValue("@offset", offset);
                    selectCommand.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
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

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@type", type);
                    selectCommand.Parameters.AddWithValue("@limit", limit);
                    selectCommand.Parameters.AddWithValue("@offset", offset);
                    selectCommand.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
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

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@type", type);
                    selectCommand.Parameters.AddWithValue("@limit", limit);
                    selectCommand.Parameters.AddWithValue("@offset", offset);
                    selectCommand.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
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

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@type", type);
                    selectCommand.Parameters.AddWithValue("@limit", limit);
                    selectCommand.Parameters.AddWithValue("@offset", offset);
                    selectCommand.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
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

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@type", type);
                    selectCommand.Parameters.AddWithValue("@limit", limit);
                    selectCommand.Parameters.AddWithValue("@offset", offset);
                    selectCommand.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
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

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@type", type);
                    selectCommand.Parameters.AddWithValue("@limit", limit);
                    selectCommand.Parameters.AddWithValue("@offset", offset);
                    selectCommand.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
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

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@type", type);
                    selectCommand.Parameters.AddWithValue("@limit", limit);
                    selectCommand.Parameters.AddWithValue("@offset", offset);
                    selectCommand.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
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

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@type", type);
                    selectCommand.Parameters.AddWithValue("@limit", limit);
                    selectCommand.Parameters.AddWithValue("@offset", offset);
                    selectCommand.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
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

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@type", type);
                    selectCommand.Parameters.AddWithValue("@limit", limit);
                    selectCommand.Parameters.AddWithValue("@offset", offset);
                    selectCommand.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
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

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@type", type);
                    selectCommand.Parameters.AddWithValue("@limit", limit);
                    selectCommand.Parameters.AddWithValue("@offset", offset);
                    selectCommand.Parameters.AddWithValue("@status", status);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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
    public Equipments ChangeValueToZero(Equipments equipment)
    {
        equipment.Power = 0;
        equipment.Health = 0;
        equipment.PhysicalAttack = 0;
        equipment.PhysicalDefense = 0;
        equipment.MagicalAttack = 0;
        equipment.MagicalDefense = 0;
        equipment.ChemicalAttack = 0;
        equipment.ChemicalDefense = 0;
        equipment.AtomicAttack = 0;
        equipment.AtomicDefense = 0;
        equipment.MentalAttack = 0;
        equipment.MentalDefense = 0;
        equipment.Speed = 0;
        equipment.CriticalDamageRate = 0;
        equipment.CriticalRate = 0;
        equipment.PenetrationRate = 0;
        equipment.EvasionRate = 0;
        equipment.DamageAbsorptionRate = 0;
        equipment.VitalityRegenerationRate = 0;
        equipment.AccuracyRate = 0;
        equipment.LifestealRate = 0;
        equipment.ShieldStrength = 0;
        equipment.Tenacity = 0;
        equipment.ResistanceRate = 0;
        equipment.ComboRate = 0;
        equipment.ReflectionRate = 0;
        equipment.Mana = 0;
        equipment.ManaRegenerationRate = 0;
        equipment.DamageToDifferentFactionRate = 0;
        equipment.ResistanceToDifferentFactionRate = 0;
        equipment.DamageToSameFactionRate = 0;
        equipment.ResistanceToSameFactionRate = 0;
        equipment.SpecialHealth = 0;
        equipment.SpecialPhysicalAttack = 0;
        equipment.SpecialPhysicalDefense = 0;
        equipment.SpecialMagicalAttack = 0;
        equipment.SpecialMagicalDefense = 0;
        equipment.SpecialChemicalAttack = 0;
        equipment.SpecialChemicalDefense = 0;
        equipment.SpecialAtomicAttack = 0;
        equipment.SpecialAtomicDefense = 0;
        equipment.SpecialMentalAttack = 0;
        equipment.SpecialMentalDefense = 0;
        equipment.SpecialSpeed = 0;
        return equipment;
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

                string selectSQL = @"
                SELECT ue.*
                FROM user_card_heroes uc
                JOIN card_heroes c ON uc.card_hero_id = c.id
                JOIN card_heroes_equipment che ON uc.card_hero_id = che.card_hero_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.card_hero_id = @card_hero_id";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@card_hero_id", cardHeroId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT ue.*
                FROM user_card_captains uc
                JOIN card_captains c ON uc.card_captain_id = c.id
                JOIN card_captains_equipment che ON uc.card_captain_id = che.card_captain_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.card_captain_id = @card_captain_id";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@card_captain_id", cardCaptainId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT ue.*
                FROM user_card_colonels uc
                JOIN card_colonels c ON uc.card_colonel_id = c.id
                JOIN card_colonels_equipment che ON uc.card_colonel_id = che.card_colonel_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.card_colonel_id = @card_colonel_id";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@card_colonel_id", cardColonelId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT ue.*
                FROM user_card_generals uc
                JOIN card_generals c ON uc.card_general_id = c.id
                JOIN card_generals_equipment che ON uc.card_general_id = che.card_general_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.card_general_id = @card_general_id";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@card_general_id", cardGeneralId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT ue.*
                FROM user_card_admirals uc
                JOIN card_admirals c ON uc.card_admiral_id = c.id
                JOIN card_admirals_equipment che ON uc.card_admiral_id = che.card_admiral_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.card_admiral_id = @card_admiral_id";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@card_admiral_id", cardAdmiralId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT ue.*
                FROM user_card_monsters uc
                JOIN card_monsters c ON uc.card_monster_id = c.id
                JOIN card_monsters_equipment che ON uc.card_monster_id = che.card_monster_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.card_monster_id = @card_monster_id";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@card_monster_id", cardMonsterId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT ue.*
                FROM user_card_militaries uc
                JOIN card_militaries c ON uc.card_military_id = c.id
                JOIN card_militaries_equipment che ON uc.card_military_id = che.card_military_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.card_military_id = @card_military_id";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@card_military_id", cardMilitaryId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT ue.*
                FROM user_card_spells uc
                JOIN card_spells c ON uc.card_spell_id = c.id
                JOIN card_spells_equipment che ON uc.card_spell_id = che.card_spell_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.card_spell_id = @card_spell_id";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@card_spell_id", cardSpellId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT ue.*
                FROM user_books uc
                JOIN books c ON uc.book_id = c.id
                JOIN books_equipment che ON uc.book_id = che.book_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.book_id = @book_id";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@book_id", bookId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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

                string selectSQL = @"
                SELECT ue.*
                FROM user_pets uc
                JOIN pets c ON uc.pet_id = c.id
                JOIN pets_equipment che ON uc.pet_id = che.pet_id
                JOIN user_equipments ue ON che.equipment_id = ue.equipment_id 
                WHERE uc.user_id = @user_id AND uc.pet_id = @pet_id";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@pet_id", petId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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
                string selectSQL = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@type", type);
                    object result = await selectCommand.ExecuteScalarAsync();
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
                    string insertSQL = @"
                    INSERT INTO card_heroes_equipment
                        (user_id, card_hero_id, equipment_id, position)
                    VALUES
                        (@user_id, @card_hero_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string selectSQL = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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
                string selectSQL = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@type", type);
                    object result = await selectCommand.ExecuteScalarAsync();
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
                    string insertSQL = @"
                    INSERT INTO card_captains_equipment
                        (user_id, card_captain_id, equipment_id, position)
                    VALUES
                        (@user_id, @card_captain_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string selectSQL = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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
                string selectSQL = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@type", type);
                    object result = await selectCommand.ExecuteScalarAsync();
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
                    string insertSQL = @"
                    INSERT INTO card_captains_equipment
                        (user_id, card_captain_id, equipment_id, position)
                    VALUES
                        (@user_id, @card_captain_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string selectSQL = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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
                string selectSQL = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@type", type);
                    object result = await selectCommand.ExecuteScalarAsync();
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
                    string insertSQL = @"
                    INSERT INTO card_generals_equipment
                        (user_id, card_general_id, equipment_id, position)
                    VALUES
                        (@user_id, @card_general_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string selectSQL = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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
                string selectSQL = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@type", type);
                    object result = await selectCommand.ExecuteScalarAsync();
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
                    string insertSQL = @"
                    INSERT INTO card_admirals_equipment
                        (user_id, card_admiral_id, equipment_id, position)
                    VALUES
                        (@user_id, @card_admiral_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string selectSQL = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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
                string selectSQL = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@type", type);
                    object result = await selectCommand.ExecuteScalarAsync();
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
                    string insertSQL = @"
                    INSERT INTO card_monsters_equipment
                        (user_id, card_monster_id, equipment_id, position)
                    VALUES
                        (@user_id, @card_monster_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string selectSQL = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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
                string selectSQL = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@type", type);
                    object result = await selectCommand.ExecuteScalarAsync();
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
                    string insertSQL = @"
                    INSERT INTO card_militaries_equipment
                        (user_id, card_military_id, equipment_id, position)
                    VALUES
                        (@user_id, @card_military_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string selectSQL = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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
                string selectSQL = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@type", type);
                    object result = await selectCommand.ExecuteScalarAsync();
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
                    string insertSQL = @"
                    INSERT INTO card_spells_equipment
                        (user_id, card_spell_id, equipment_id, position)
                    VALUES
                        (@user_id, @card_spell_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string selectSQL = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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
                string selectSQL = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@type", type);
                    object result = await selectCommand.ExecuteScalarAsync();
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
                    string insertSQL = @"
                    INSERT INTO books_equipment
                        (user_id, book_id, equipment_id, position)
                    VALUES
                        (@user_id, @book_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string selectSQL = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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
                string selectSQL = "SELECT max_positions FROM equipment_type WHERE type = @type";
                int maxPositions = 0;
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@type", type);
                    object result = await selectCommand.ExecuteScalarAsync();
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
                    string insertSQL = @"
                    INSERT INTO pets_equipment
                        (user_id, pet_id, equipment_id, position)
                    VALUES
                        (@user_id, @pet_id, @equipment_id, @position)";
                    await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
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
                string selectSQL = "SELECT type FROM equipment_type";
                List<string> types = new List<string>();
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
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