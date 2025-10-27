using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserEquipmentsRepository : IUserEquipmentsRepository
{
    public List<Equipments> GetUserEquipments(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select e.id, e.name, ue.*, e.image, e.rare, e.type from Equipments e, user_equipments ue 
                where e.id=ue.equipment_id and ue.user_id=@userId and e.type= @type AND (@rare = 'All' or e.rare = @rare) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@rare", rare);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
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

                        BaseStats = new BaseStats
                        {
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
                        }
                    };

                    equipmentList.Add(equipments);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        return equipmentList;
    }
    public int GetUserEquipmentsCount(string user_id, string type, string rare)
    {
        int count = 0;
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select count(*) from Equipments e, user_equipments ue 
                where e.id=ue.equipment_id and ue.user_id=@userId and e.type= @type AND (@rare = 'All' or e.rare = @rare)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@rare", rare);
                count = Convert.ToInt32(command.ExecuteScalar());

                return count;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return count;
    }
    public Equipments GetUserEquipmentsById(string user_id, string Id)
    {
        Equipments card = new Equipments();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_equipments where equipment_id=@id 
                and user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Level = reader.GetInt32("level"),
                        Quality = reader.GetInt32("quality"),
                        Experiment = reader.GetInt32("experiment"),
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

                        BaseStats = new BaseStats
                        {
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
                        }
                    };
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        return card;
    }
    public bool BuyEquipment(string Id, Equipments EquipmentFromDB)
    {
        // Equipments EquipmentFromDB = GetEquipmentById(Id);
        // Debug.Log(EquipmentFromDB);
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                INSERT INTO user_equipments (
                    user_id, equipment_id, rare, sequence, level, experiment, star, quality, block,
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
                    skill_damage_rate, skill_resistance_rate,
                    special_health, special_physical_attack, special_physical_defense, special_magical_attack,
                    special_magical_defense, special_chemical_attack, special_chemical_defense, special_atomic_attack,
                    special_atomic_defense, special_mental_attack, special_mental_defense, special_speed
                ) VALUES (
                    @user_id, @equipment_id, @rare, @sequence, @level, @experiment, @star, @quality, @block, 
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
                    @skill_damage_rate, @skill_resistance_rate,
                    @special_health, @special_physical_attack, @special_physical_defense, @special_magical_attack,
                    @special_magical_defense, @special_chemical_attack, @special_chemical_defense, @special_atomic_attack,
                    @special_atomic_defense, @special_mental_attack, @special_mental_defense, @special_speed
                )";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@equipment_id", Id);
                command.Parameters.AddWithValue("@rare", EquipmentFromDB.Rare);
                command.Parameters.AddWithValue("@sequence", GetMaxSequence(connection, Id) + 1);
                command.Parameters.AddWithValue("@level", 0);
                command.Parameters.AddWithValue("@experiment", 0);
                command.Parameters.AddWithValue("@star", 0);
                command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(EquipmentFromDB.Rare));
                command.Parameters.AddWithValue("@block", false);
                command.Parameters.AddWithValue("@power", EquipmentFromDB.Power);
                command.Parameters.AddWithValue("@health", EquipmentFromDB.Health);
                command.Parameters.AddWithValue("@physical_attack", EquipmentFromDB.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", EquipmentFromDB.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", EquipmentFromDB.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", EquipmentFromDB.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", EquipmentFromDB.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", EquipmentFromDB.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", EquipmentFromDB.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", EquipmentFromDB.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", EquipmentFromDB.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", EquipmentFromDB.MentalDefense);
                command.Parameters.AddWithValue("@speed", EquipmentFromDB.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", EquipmentFromDB.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", EquipmentFromDB.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", EquipmentFromDB.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", EquipmentFromDB.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", EquipmentFromDB.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", EquipmentFromDB.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", EquipmentFromDB.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", EquipmentFromDB.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", EquipmentFromDB.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", EquipmentFromDB.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", EquipmentFromDB.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", EquipmentFromDB.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", EquipmentFromDB.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", EquipmentFromDB.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", EquipmentFromDB.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", EquipmentFromDB.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", EquipmentFromDB.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", EquipmentFromDB.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", EquipmentFromDB.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", EquipmentFromDB.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", EquipmentFromDB.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", EquipmentFromDB.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", EquipmentFromDB.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", EquipmentFromDB.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", EquipmentFromDB.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", EquipmentFromDB.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", EquipmentFromDB.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", EquipmentFromDB.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", EquipmentFromDB.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", EquipmentFromDB.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", EquipmentFromDB.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", EquipmentFromDB.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", EquipmentFromDB.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", EquipmentFromDB.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", EquipmentFromDB.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", EquipmentFromDB.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", EquipmentFromDB.SkillResistanceRate);
                command.Parameters.AddWithValue("@special_health", EquipmentFromDB.SpecialHealth);
                command.Parameters.AddWithValue("@special_physical_attack", EquipmentFromDB.SpecialPhysicalAttack);
                command.Parameters.AddWithValue("@special_physical_defense", EquipmentFromDB.SpecialPhysicalDefense);
                command.Parameters.AddWithValue("@special_magical_attack", EquipmentFromDB.SpecialMagicalAttack);
                command.Parameters.AddWithValue("@special_magical_defense", EquipmentFromDB.SpecialMagicalDefense);
                command.Parameters.AddWithValue("@special_chemical_attack", EquipmentFromDB.SpecialChemicalAttack);
                command.Parameters.AddWithValue("@special_chemical_defense", EquipmentFromDB.SpecialChemicalDefense);
                command.Parameters.AddWithValue("@special_atomic_attack", EquipmentFromDB.SpecialAtomicAttack);
                command.Parameters.AddWithValue("@special_atomic_defense", EquipmentFromDB.SpecialAtomicDefense);
                command.Parameters.AddWithValue("@special_mental_attack", EquipmentFromDB.SpecialMentalAttack);
                command.Parameters.AddWithValue("@special_mental_defense", EquipmentFromDB.SpecialMentalDefense);
                command.Parameters.AddWithValue("@special_speed", EquipmentFromDB.SpecialSpeed);
                command.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return false;
    }
    public bool UpdateEquipmentsLevel(Equipments equipments, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
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
                    critical_rate = @critical_rate, critical_resistance_rate = @critical_resistance_rate, ignore_critical_rate = @ignore_critical_rate,
                    penetration_rate = @penetration_rate, penetration_resistance_rate = @penetration_resistance_rate,
                    evasion_rate = @evasion_rate, damage_absorption_rate = @damage_absorption_rate, 
                    ignore_damage_absorption_rate = @ignore_damage_absorption_rate, absorbed_damage_rate = @absorbed_damage_rate,
                    vitality_regeneration_rate = @vitality_regeneration_rate, vitality_regeneration_resistance_rate = @vitality_regeneration_resistance_rate, 
                    accuracy_rate = @accuracy_rate, lifesteal_rate = @lifesteal_rate, shield_strength = @shield_strength, 
                    tenacity = @tenacity, resistance_rate = @resistance_rate, 
                    combo_rate = @comboRate, ignore_combo_rate = @ignore_combo_rate, combo_damage_rate = @combo_damage_rate, combo_resistance_rate = @combo_resistance_rate,
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
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@equipment_id", equipments.Id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", equipments.Power);
                command.Parameters.AddWithValue("@health", equipments.Health);
                command.Parameters.AddWithValue("@physical_attack", equipments.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", equipments.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", equipments.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", equipments.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", equipments.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", equipments.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", equipments.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", equipments.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", equipments.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", equipments.MentalDefense);
                command.Parameters.AddWithValue("@speed", equipments.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", equipments.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", equipments.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", equipments.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", equipments.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", equipments.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", equipments.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", equipments.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", equipments.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", equipments.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", equipments.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", equipments.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", equipments.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", equipments.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", equipments.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", equipments.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", equipments.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", equipments.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", equipments.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", equipments.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", equipments.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", equipments.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", equipments.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", equipments.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", equipments.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", equipments.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", equipments.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", equipments.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", equipments.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", equipments.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", equipments.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", equipments.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", equipments.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", equipments.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", equipments.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", equipments.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", equipments.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", equipments.SkillResistanceRate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        return true;
    }
    public bool UpdateEquipmentsBreakthrough(Equipments equipments, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
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
                    combo_rate = @comboRate, ignore_combo_rate = @ignore_combo_rate, combo_damage_rate = @combo_damage_rate, combo_resistance_rate = @combo_resistance_rate,
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
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@equipment_id", equipments.Id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", equipments.Power);
                command.Parameters.AddWithValue("@health", equipments.Health);
                command.Parameters.AddWithValue("@physical_attack", equipments.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", equipments.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", equipments.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", equipments.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", equipments.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", equipments.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", equipments.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", equipments.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", equipments.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", equipments.MentalDefense);
                command.Parameters.AddWithValue("@speed", equipments.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", equipments.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", equipments.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", equipments.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", equipments.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", equipments.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", equipments.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", equipments.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", equipments.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", equipments.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", equipments.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", equipments.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", equipments.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", equipments.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", equipments.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", equipments.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", equipments.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", equipments.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", equipments.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", equipments.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", equipments.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", equipments.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", equipments.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", equipments.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", equipments.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", equipments.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", equipments.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", equipments.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", equipments.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", equipments.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", equipments.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", equipments.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", equipments.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", equipments.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", equipments.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", equipments.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", equipments.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", equipments.SkillResistanceRate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        return true;
    }
    private int GetMaxSequence(MySqlConnection connection, string equipment_id)
    {
        string query = "SELECT MAX(sequence) FROM user_equipments ue where ue.equipment_id=@equipment_id and ue.user_id=@user_id";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@equipment_id", equipment_id);
        command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
        object result = command.ExecuteScalar();

        if (result != DBNull.Value)
        {
            return Convert.ToInt32(result);
        }
        return 0; // Nếu bảng rỗng, trả về 0
    }
    public void UpdateUserCurrency(string Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "select et.currency_id, et.price from equipments e, equipment_trade et where e.id=et.equipment_id and e.id=@id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                int currencyId = 0;
                double price = 0;

                if (reader.Read())
                {
                    currencyId = reader.GetInt32("currency_id");
                    price = reader.GetDouble("price");
                }
                reader.Close();

                // Lấy quantity hiện tại
                query = "SELECT quantity FROM user_currency WHERE user_id = @user_id AND currency_id = @currency_id";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@currency_id", currencyId);
                double currentQuantity = Convert.ToDouble(command.ExecuteScalar());
                double newQuantity = currentQuantity - price;

                query = "update user_currency set quantity=@quantity where user_id=@user_id and currency_id=@currency_id";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@quantity", newQuantity);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@currency_id", currencyId);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
    public void InsertCardHeroesEquipments(string Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_heroes_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM card_heroes_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO card_heroes_equipment (user_id, card_hero_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @card_hero_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@card_hero_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
    public void InsertCardCaptainsEquipments(string Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_captains_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM card_captains_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO card_captains_equipment (user_id, card_captain_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @card_captain_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@card_captain_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
    public void InsertCardColonelsEquipments(string Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_colonels_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM card_colonels_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO card_colonels_equipment (user_id, card_colonel_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @card_colonel_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@card_colonel_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
    public void InsertCardGeneralsEquipments(string Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_generals_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM card_generals_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO card_generals_equipment (user_id, card_general_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @card_general_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@card_general_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
    public void InsertCardAdmiralsEquipments(string Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_admirals_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM card_admirals_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO card_admirals_equipment (user_id, card_admiral_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @card_admiral_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@card_admiral_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
    public void InsertCardMonstersEquipments(string Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_monsters_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM card_monsters_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO card_monsters_equipment (user_id, card_monster_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @card_monster_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@card_monster_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
    public void InsertCardMilitaryEquipments(string Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_military_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM card_military_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO card_military_equipment (user_id, card_military_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @card_military_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@card_military_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
    public void InsertCardSpellEquipments(string Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM card_spell_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM card_spell_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO card_spell_equipment (user_id, card_spell_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @card_spell_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@card_spell_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
    public void InsertBooksEquipments(string Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM books_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM books_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO books_equipment (user_id, book_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @book_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@book_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
    public void InsertPetsEquipments(string Id, Equipments equipments, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem equipment_id và sequence có tồn tại trong bảng không
                string checkQuery = @"SELECT COUNT(*) FROM pets_equipment 
                                  WHERE equipment_id = @equipment_id AND sequence = @sequence";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                checkCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // Nếu tồn tại, xóa các bản ghi cũ trước
                if (count > 0)
                {
                    string deleteQuery = @"DELETE FROM pets_equipment 
                                       WHERE equipment_id = @equipment_id AND sequence = @sequence";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                    deleteCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                    deleteCommand.ExecuteNonQuery();
                }

                // Chèn dữ liệu mới vào bảng
                string insertQuery = @"INSERT INTO pets_equipment (user_id, pet_id, equipment_id, sequence, position)
                                   VALUES (@user_id, @pet_id, @equipment_id, @sequence, @position)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@pet_id", Id);
                insertCommand.Parameters.AddWithValue("@equipment_id", equipments.Id);
                insertCommand.Parameters.AddWithValue("@sequence", equipments.Sequence);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
    public List<Equipments> GetCardHeroesEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_heroes_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.card_hero_id = @card_hero_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_hero_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetCardCaptainsEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_captains_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.card_captain_id = @card_captain_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_captain_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.GetInt32("position")
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetCardColonelsEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_colonels_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.card_colonel_id = @card_colonel_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_colonel_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.GetInt32("position")
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetCardGeneralsEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_generals_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.card_general_id = @card_general_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_general_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.GetInt32("position")
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetCardAdmiralsEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_admirals_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.card_admiral_id = @card_admiral_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_admiral_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.GetInt32("position")
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetCardMonstersEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_monsters_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.card_monster_id = @card_monster_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_monster_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.GetInt32("position")
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetCardMilitaryEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_military_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.card_military_id = @card_military_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_military_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.GetInt32("position")
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetCardSpellEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_spell_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.card_spell_id = @card_spell_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_spell_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.GetInt32("position")
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetBooksEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_books_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.book_id = @book_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@book_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.GetInt32("position")
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetPetsEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                    e.id, e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet
                FROM Equipments e
                JOIN user_equipments ue ON e.id = ue.equipment_id
                JOIN card_pets_equipment che ON che.equipment_id = ue.equipment_id 
                    AND che.sequence = ue.sequence
                WHERE che.pet_id = @pet_id
                AND ue.user_id = @user_id
                AND e.type = @type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@pet_id", card_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.GetInt32("position")
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetAllCardHeroesEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join card_heroes_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetAllCardCaptainsEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join card_captains_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetAllCardColonelsEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join card_colonels_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetAllCardGeneralsEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join card_generals_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetAllCardAdmiralsEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join card_admirals_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetAllCardMonstersEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join card_monsters_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetAllCardMilitaryEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join card_military_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetAllCardSpellEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join card_spell_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetAllBooksEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join books_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
    }
    public List<Equipments> GetAllPetsEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type, che.position, e.equipmentSet, case when che.equipment_id is null then 'NOT EQUIP' else 'EQUIP' END AS STATUS
                from equipments e left join user_equipments ue on e.id = ue.equipment_id
                left join pets_equipment che on che.equipment_id = ue.equipment_id and che.sequence = ue.sequence 
                and che.user_id = ue.user_id
                where ue.user_id = @user_id and e.type = @type AND (@status = 'ALL' 
         OR (@status = 'EQUIP' AND che.equipment_id IS NOT NULL) 
         OR (@status = 'NOT EQUIP' AND che.equipment_id IS NULL)) limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments equipments = new Equipments
                    {
                        Id = reader.GetString("equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Type = reader.GetString("type"),
                        Set = reader.GetString("equipmentSet"),
                        Level = reader.GetInt32("level"),
                        Star = reader.GetInt32("star"),
                        Sequence = reader.GetInt32("sequence"),
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
                        Position = reader.IsDBNull(reader.GetOrdinal("position")) ? 0 : reader.GetInt32("position"),
                    };

                    equipmentList.Add(equipments);
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipmentList;
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
    public Equipments GetAllEquipmentsByCardHeoresId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT ue.*
                FROM user_card_heroes uc, card_heroes c, card_heroes_equipment che, user_equipments ue
                WHERE uc.card_hero_id = c.id AND uc.card_hero_id = che.card_hero_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_hero_id = @card_hero_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_hero_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);
                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.Power += e.Power;
                    equipments.Health += e.Health;
                    equipments.PhysicalAttack += e.PhysicalAttack;
                    equipments.PhysicalDefense += e.PhysicalDefense;
                    equipments.MagicalAttack += e.MagicalAttack;
                    equipments.MagicalDefense += e.MagicalDefense;
                    equipments.ChemicalAttack += e.ChemicalAttack;
                    equipments.ChemicalDefense += e.ChemicalDefense;
                    equipments.AtomicAttack += e.AtomicAttack;
                    equipments.AtomicDefense += e.AtomicDefense;
                    equipments.MentalAttack += e.MentalAttack;
                    equipments.MentalDefense += e.MentalDefense;
                    equipments.Speed += e.Speed;
                    equipments.CriticalDamageRate += e.CriticalDamageRate;
                    equipments.CriticalRate += e.CriticalRate;
                    equipments.CriticalResistanceRate += e.CriticalResistanceRate;
                    equipments.IgnoreCriticalRate += e.IgnoreCriticalRate;
                    equipments.PenetrationRate += e.PenetrationRate;
                    equipments.PenetrationResistanceRate += e.PenetrationResistanceRate;
                    equipments.EvasionRate += e.EvasionRate;
                    equipments.DamageAbsorptionRate += e.DamageAbsorptionRate;
                    equipments.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                    equipments.AbsorbedDamageRate += e.AbsorbedDamageRate;
                    equipments.VitalityRegenerationRate += e.VitalityRegenerationRate;
                    equipments.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                    equipments.AccuracyRate += e.AccuracyRate;
                    equipments.LifestealRate += e.LifestealRate;
                    equipments.ShieldStrength += e.ShieldStrength;
                    equipments.Tenacity += e.Tenacity;
                    equipments.ResistanceRate += e.ResistanceRate;
                    equipments.ComboRate += e.ComboRate;
                    equipments.IgnoreComboRate += e.IgnoreComboRate;
                    equipments.ComboDamageRate += e.ComboDamageRate;
                    equipments.ComboResistanceRate += e.ComboResistanceRate;
                    equipments.StunRate += e.StunRate;
                    equipments.IgnoreStunRate += e.IgnoreStunRate;
                    equipments.ReflectionRate += e.ReflectionRate;
                    equipments.IgnoreReflectionRate += e.IgnoreReflectionRate;
                    equipments.ReflectionDamageRate += e.ReflectionDamageRate;
                    equipments.ReflectionResistanceRate += e.ReflectionResistanceRate;
                    equipments.Mana += e.Mana;
                    equipments.ManaRegenerationRate += e.ManaRegenerationRate;
                    equipments.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                    equipments.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                    equipments.DamageToSameFactionRate += e.DamageToSameFactionRate;
                    equipments.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                    equipments.NormalDamageRate += e.NormalDamageRate;
                    equipments.NormalResistanceRate += e.NormalResistanceRate;
                    equipments.SkillDamageRate += e.SkillDamageRate;
                    equipments.SkillResistanceRate += e.SkillResistanceRate;
                    equipments.SpecialHealth += e.SpecialHealth;
                    equipments.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                    equipments.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                    equipments.SpecialMagicalAttack += e.SpecialMagicalAttack;
                    equipments.SpecialMagicalDefense += e.SpecialMagicalDefense;
                    equipments.SpecialChemicalAttack += e.SpecialChemicalAttack;
                    equipments.SpecialChemicalDefense += e.SpecialChemicalDefense;
                    equipments.SpecialAtomicAttack += e.SpecialAtomicAttack;
                    equipments.SpecialAtomicDefense += e.SpecialAtomicDefense;
                    equipments.SpecialMentalAttack += e.SpecialMentalAttack;
                    equipments.SpecialMentalDefense += e.SpecialMentalDefense;
                    equipments.SpecialSpeed += e.Speed;
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipments;
    }
    public Equipments GetAllEquipmentsByCardCaptainsId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT *
                FROM user_card_captains uc, card_captains c, card_captains_equipment che, user_equipments ue
                WHERE uc.card_captain_id = c.id AND uc.card_captain_id = che.card_captain_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_captain_id = @card_captain_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_captain_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);

                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.Power += e.Power;
                    equipments.Health += e.Health;
                    equipments.PhysicalAttack += e.PhysicalAttack;
                    equipments.PhysicalDefense += e.PhysicalDefense;
                    equipments.MagicalAttack += e.MagicalAttack;
                    equipments.MagicalDefense += e.MagicalDefense;
                    equipments.ChemicalAttack += e.ChemicalAttack;
                    equipments.ChemicalDefense += e.ChemicalDefense;
                    equipments.AtomicAttack += e.AtomicAttack;
                    equipments.AtomicDefense += e.AtomicDefense;
                    equipments.MentalAttack += e.MentalAttack;
                    equipments.MentalDefense += e.MentalDefense;
                    equipments.Speed += e.Speed;
                    equipments.CriticalDamageRate += e.CriticalDamageRate;
                    equipments.CriticalRate += e.CriticalRate;
                    equipments.CriticalResistanceRate += e.CriticalResistanceRate;
                    equipments.IgnoreCriticalRate += e.IgnoreCriticalRate;
                    equipments.PenetrationRate += e.PenetrationRate;
                    equipments.PenetrationResistanceRate += e.PenetrationResistanceRate;
                    equipments.EvasionRate += e.EvasionRate;
                    equipments.DamageAbsorptionRate += e.DamageAbsorptionRate;
                    equipments.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                    equipments.AbsorbedDamageRate += e.AbsorbedDamageRate;
                    equipments.VitalityRegenerationRate += e.VitalityRegenerationRate;
                    equipments.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                    equipments.AccuracyRate += e.AccuracyRate;
                    equipments.LifestealRate += e.LifestealRate;
                    equipments.ShieldStrength += e.ShieldStrength;
                    equipments.Tenacity += e.Tenacity;
                    equipments.ResistanceRate += e.ResistanceRate;
                    equipments.ComboRate += e.ComboRate;
                    equipments.IgnoreComboRate += e.IgnoreComboRate;
                    equipments.ComboDamageRate += e.ComboDamageRate;
                    equipments.ComboResistanceRate += e.ComboResistanceRate;
                    equipments.StunRate += e.StunRate;
                    equipments.IgnoreStunRate += e.IgnoreStunRate;
                    equipments.ReflectionRate += e.ReflectionRate;
                    equipments.IgnoreReflectionRate += e.IgnoreReflectionRate;
                    equipments.ReflectionDamageRate += e.ReflectionDamageRate;
                    equipments.ReflectionResistanceRate += e.ReflectionResistanceRate;
                    equipments.Mana += e.Mana;
                    equipments.ManaRegenerationRate += e.ManaRegenerationRate;
                    equipments.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                    equipments.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                    equipments.DamageToSameFactionRate += e.DamageToSameFactionRate;
                    equipments.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                    equipments.NormalDamageRate += e.NormalDamageRate;
                    equipments.NormalResistanceRate += e.NormalResistanceRate;
                    equipments.SkillDamageRate += e.SkillDamageRate;
                    equipments.SkillResistanceRate += e.SkillResistanceRate;
                    equipments.SpecialHealth += e.SpecialHealth;
                    equipments.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                    equipments.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                    equipments.SpecialMagicalAttack += e.SpecialMagicalAttack;
                    equipments.SpecialMagicalDefense += e.SpecialMagicalDefense;
                    equipments.SpecialChemicalAttack += e.SpecialChemicalAttack;
                    equipments.SpecialChemicalDefense += e.SpecialChemicalDefense;
                    equipments.SpecialAtomicAttack += e.SpecialAtomicAttack;
                    equipments.SpecialAtomicDefense += e.SpecialAtomicDefense;
                    equipments.SpecialMentalAttack += e.SpecialMentalAttack;
                    equipments.SpecialMentalDefense += e.SpecialMentalDefense;
                    equipments.SpecialSpeed += e.Speed;
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipments;
    }
    public Equipments GetAllEquipmentsByCardColonelsId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT *
                FROM user_card_colonels uc, card_colonels c, card_colonels_equipment che, user_equipments ue
                WHERE uc.card_colonel_id = c.id AND uc.card_colonel_id = che.card_colonel_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_colonel_id = @card_colonel_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_colonel_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);

                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.Power += e.Power;
                    equipments.Health += e.Health;
                    equipments.PhysicalAttack += e.PhysicalAttack;
                    equipments.PhysicalDefense += e.PhysicalDefense;
                    equipments.MagicalAttack += e.MagicalAttack;
                    equipments.MagicalDefense += e.MagicalDefense;
                    equipments.ChemicalAttack += e.ChemicalAttack;
                    equipments.ChemicalDefense += e.ChemicalDefense;
                    equipments.AtomicAttack += e.AtomicAttack;
                    equipments.AtomicDefense += e.AtomicDefense;
                    equipments.MentalAttack += e.MentalAttack;
                    equipments.MentalDefense += e.MentalDefense;
                    equipments.Speed += e.Speed;
                    equipments.CriticalDamageRate += e.CriticalDamageRate;
                    equipments.CriticalRate += e.CriticalRate;
                    equipments.CriticalResistanceRate += e.CriticalResistanceRate;
                    equipments.IgnoreCriticalRate += e.IgnoreCriticalRate;
                    equipments.PenetrationRate += e.PenetrationRate;
                    equipments.PenetrationResistanceRate += e.PenetrationResistanceRate;
                    equipments.EvasionRate += e.EvasionRate;
                    equipments.DamageAbsorptionRate += e.DamageAbsorptionRate;
                    equipments.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                    equipments.AbsorbedDamageRate += e.AbsorbedDamageRate;
                    equipments.VitalityRegenerationRate += e.VitalityRegenerationRate;
                    equipments.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                    equipments.AccuracyRate += e.AccuracyRate;
                    equipments.LifestealRate += e.LifestealRate;
                    equipments.ShieldStrength += e.ShieldStrength;
                    equipments.Tenacity += e.Tenacity;
                    equipments.ResistanceRate += e.ResistanceRate;
                    equipments.ComboRate += e.ComboRate;
                    equipments.IgnoreComboRate += e.IgnoreComboRate;
                    equipments.ComboDamageRate += e.ComboDamageRate;
                    equipments.ComboResistanceRate += e.ComboResistanceRate;
                    equipments.StunRate += e.StunRate;
                    equipments.IgnoreStunRate += e.IgnoreStunRate;
                    equipments.ReflectionRate += e.ReflectionRate;
                    equipments.IgnoreReflectionRate += e.IgnoreReflectionRate;
                    equipments.ReflectionDamageRate += e.ReflectionDamageRate;
                    equipments.ReflectionResistanceRate += e.ReflectionResistanceRate;
                    equipments.Mana += e.Mana;
                    equipments.ManaRegenerationRate += e.ManaRegenerationRate;
                    equipments.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                    equipments.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                    equipments.DamageToSameFactionRate += e.DamageToSameFactionRate;
                    equipments.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                    equipments.NormalDamageRate += e.NormalDamageRate;
                    equipments.NormalResistanceRate += e.NormalResistanceRate;
                    equipments.SkillDamageRate += e.SkillDamageRate;
                    equipments.SkillResistanceRate += e.SkillResistanceRate;
                    equipments.SpecialHealth += e.SpecialHealth;
                    equipments.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                    equipments.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                    equipments.SpecialMagicalAttack += e.SpecialMagicalAttack;
                    equipments.SpecialMagicalDefense += e.SpecialMagicalDefense;
                    equipments.SpecialChemicalAttack += e.SpecialChemicalAttack;
                    equipments.SpecialChemicalDefense += e.SpecialChemicalDefense;
                    equipments.SpecialAtomicAttack += e.SpecialAtomicAttack;
                    equipments.SpecialAtomicDefense += e.SpecialAtomicDefense;
                    equipments.SpecialMentalAttack += e.SpecialMentalAttack;
                    equipments.SpecialMentalDefense += e.SpecialMentalDefense;
                    equipments.SpecialSpeed += e.Speed;
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipments;
    }
    public Equipments GetAllEquipmentsByCardGeneralsId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT *
                FROM user_card_generals uc, card_generals c, card_generals_equipment che, user_equipments ue
                WHERE uc.card_general_id = c.id AND uc.card_general_id = che.card_general_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_general_id = @card_general_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_general_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);

                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.Power += e.Power;
                    equipments.Health += e.Health;
                    equipments.PhysicalAttack += e.PhysicalAttack;
                    equipments.PhysicalDefense += e.PhysicalDefense;
                    equipments.MagicalAttack += e.MagicalAttack;
                    equipments.MagicalDefense += e.MagicalDefense;
                    equipments.ChemicalAttack += e.ChemicalAttack;
                    equipments.ChemicalDefense += e.ChemicalDefense;
                    equipments.AtomicAttack += e.AtomicAttack;
                    equipments.AtomicDefense += e.AtomicDefense;
                    equipments.MentalAttack += e.MentalAttack;
                    equipments.MentalDefense += e.MentalDefense;
                    equipments.Speed += e.Speed;
                    equipments.CriticalDamageRate += e.CriticalDamageRate;
                    equipments.CriticalRate += e.CriticalRate;
                    equipments.CriticalResistanceRate += e.CriticalResistanceRate;
                    equipments.IgnoreCriticalRate += e.IgnoreCriticalRate;
                    equipments.PenetrationRate += e.PenetrationRate;
                    equipments.PenetrationResistanceRate += e.PenetrationResistanceRate;
                    equipments.EvasionRate += e.EvasionRate;
                    equipments.DamageAbsorptionRate += e.DamageAbsorptionRate;
                    equipments.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                    equipments.AbsorbedDamageRate += e.AbsorbedDamageRate;
                    equipments.VitalityRegenerationRate += e.VitalityRegenerationRate;
                    equipments.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                    equipments.AccuracyRate += e.AccuracyRate;
                    equipments.LifestealRate += e.LifestealRate;
                    equipments.ShieldStrength += e.ShieldStrength;
                    equipments.Tenacity += e.Tenacity;
                    equipments.ResistanceRate += e.ResistanceRate;
                    equipments.ComboRate += e.ComboRate;
                    equipments.IgnoreComboRate += e.IgnoreComboRate;
                    equipments.ComboDamageRate += e.ComboDamageRate;
                    equipments.ComboResistanceRate += e.ComboResistanceRate;
                    equipments.StunRate += e.StunRate;
                    equipments.IgnoreStunRate += e.IgnoreStunRate;
                    equipments.ReflectionRate += e.ReflectionRate;
                    equipments.IgnoreReflectionRate += e.IgnoreReflectionRate;
                    equipments.ReflectionDamageRate += e.ReflectionDamageRate;
                    equipments.ReflectionResistanceRate += e.ReflectionResistanceRate;
                    equipments.Mana += e.Mana;
                    equipments.ManaRegenerationRate += e.ManaRegenerationRate;
                    equipments.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                    equipments.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                    equipments.DamageToSameFactionRate += e.DamageToSameFactionRate;
                    equipments.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                    equipments.NormalDamageRate += e.NormalDamageRate;
                    equipments.NormalResistanceRate += e.NormalResistanceRate;
                    equipments.SkillDamageRate += e.SkillDamageRate;
                    equipments.SkillResistanceRate += e.SkillResistanceRate;
                    equipments.SpecialHealth += e.SpecialHealth;
                    equipments.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                    equipments.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                    equipments.SpecialMagicalAttack += e.SpecialMagicalAttack;
                    equipments.SpecialMagicalDefense += e.SpecialMagicalDefense;
                    equipments.SpecialChemicalAttack += e.SpecialChemicalAttack;
                    equipments.SpecialChemicalDefense += e.SpecialChemicalDefense;
                    equipments.SpecialAtomicAttack += e.SpecialAtomicAttack;
                    equipments.SpecialAtomicDefense += e.SpecialAtomicDefense;
                    equipments.SpecialMentalAttack += e.SpecialMentalAttack;
                    equipments.SpecialMentalDefense += e.SpecialMentalDefense;
                    equipments.SpecialSpeed += e.Speed;
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipments;
    }
    public Equipments GetAllEquipmentsByCardAdmiralsId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT *
                FROM user_card_admirals uc, card_admirals c, card_admirals_equipment che, user_equipments ue
                WHERE uc.card_admiral_id = c.id AND uc.card_admiral_id = che.card_admiral_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_admiral_id = @card_admiral_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_admiral_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);

                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.Power += e.Power;
                    equipments.Health += e.Health;
                    equipments.PhysicalAttack += e.PhysicalAttack;
                    equipments.PhysicalDefense += e.PhysicalDefense;
                    equipments.MagicalAttack += e.MagicalAttack;
                    equipments.MagicalDefense += e.MagicalDefense;
                    equipments.ChemicalAttack += e.ChemicalAttack;
                    equipments.ChemicalDefense += e.ChemicalDefense;
                    equipments.AtomicAttack += e.AtomicAttack;
                    equipments.AtomicDefense += e.AtomicDefense;
                    equipments.MentalAttack += e.MentalAttack;
                    equipments.MentalDefense += e.MentalDefense;
                    equipments.Speed += e.Speed;
                    equipments.CriticalDamageRate += e.CriticalDamageRate;
                    equipments.CriticalRate += e.CriticalRate;
                    equipments.CriticalResistanceRate += e.CriticalResistanceRate;
                    equipments.IgnoreCriticalRate += e.IgnoreCriticalRate;
                    equipments.PenetrationRate += e.PenetrationRate;
                    equipments.PenetrationResistanceRate += e.PenetrationResistanceRate;
                    equipments.EvasionRate += e.EvasionRate;
                    equipments.DamageAbsorptionRate += e.DamageAbsorptionRate;
                    equipments.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                    equipments.AbsorbedDamageRate += e.AbsorbedDamageRate;
                    equipments.VitalityRegenerationRate += e.VitalityRegenerationRate;
                    equipments.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                    equipments.AccuracyRate += e.AccuracyRate;
                    equipments.LifestealRate += e.LifestealRate;
                    equipments.ShieldStrength += e.ShieldStrength;
                    equipments.Tenacity += e.Tenacity;
                    equipments.ResistanceRate += e.ResistanceRate;
                    equipments.ComboRate += e.ComboRate;
                    equipments.IgnoreComboRate += e.IgnoreComboRate;
                    equipments.ComboDamageRate += e.ComboDamageRate;
                    equipments.ComboResistanceRate += e.ComboResistanceRate;
                    equipments.StunRate += e.StunRate;
                    equipments.IgnoreStunRate += e.IgnoreStunRate;
                    equipments.ReflectionRate += e.ReflectionRate;
                    equipments.IgnoreReflectionRate += e.IgnoreReflectionRate;
                    equipments.ReflectionDamageRate += e.ReflectionDamageRate;
                    equipments.ReflectionResistanceRate += e.ReflectionResistanceRate;
                    equipments.Mana += e.Mana;
                    equipments.ManaRegenerationRate += e.ManaRegenerationRate;
                    equipments.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                    equipments.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                    equipments.DamageToSameFactionRate += e.DamageToSameFactionRate;
                    equipments.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                    equipments.NormalDamageRate += e.NormalDamageRate;
                    equipments.NormalResistanceRate += e.NormalResistanceRate;
                    equipments.SkillDamageRate += e.SkillDamageRate;
                    equipments.SkillResistanceRate += e.SkillResistanceRate;
                    equipments.SpecialHealth += e.SpecialHealth;
                    equipments.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                    equipments.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                    equipments.SpecialMagicalAttack += e.SpecialMagicalAttack;
                    equipments.SpecialMagicalDefense += e.SpecialMagicalDefense;
                    equipments.SpecialChemicalAttack += e.SpecialChemicalAttack;
                    equipments.SpecialChemicalDefense += e.SpecialChemicalDefense;
                    equipments.SpecialAtomicAttack += e.SpecialAtomicAttack;
                    equipments.SpecialAtomicDefense += e.SpecialAtomicDefense;
                    equipments.SpecialMentalAttack += e.SpecialMentalAttack;
                    equipments.SpecialMentalDefense += e.SpecialMentalDefense;
                    equipments.SpecialSpeed += e.Speed;
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipments;
    }
    public Equipments GetAllEquipmentsByCardMonstersId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT *
                FROM user_card_monsters uc, card_monsters c, card_monsters_equipment che, user_equipments ue
                WHERE uc.card_monster_id = c.id AND uc.card_monster_id = che.card_monster_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_monster_id = @card_monster_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_monster_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);

                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.Power += e.Power;
                    equipments.Health += e.Health;
                    equipments.PhysicalAttack += e.PhysicalAttack;
                    equipments.PhysicalDefense += e.PhysicalDefense;
                    equipments.MagicalAttack += e.MagicalAttack;
                    equipments.MagicalDefense += e.MagicalDefense;
                    equipments.ChemicalAttack += e.ChemicalAttack;
                    equipments.ChemicalDefense += e.ChemicalDefense;
                    equipments.AtomicAttack += e.AtomicAttack;
                    equipments.AtomicDefense += e.AtomicDefense;
                    equipments.MentalAttack += e.MentalAttack;
                    equipments.MentalDefense += e.MentalDefense;
                    equipments.Speed += e.Speed;
                    equipments.CriticalDamageRate += e.CriticalDamageRate;
                    equipments.CriticalRate += e.CriticalRate;
                    equipments.CriticalResistanceRate += e.CriticalResistanceRate;
                    equipments.IgnoreCriticalRate += e.IgnoreCriticalRate;
                    equipments.PenetrationRate += e.PenetrationRate;
                    equipments.PenetrationResistanceRate += e.PenetrationResistanceRate;
                    equipments.EvasionRate += e.EvasionRate;
                    equipments.DamageAbsorptionRate += e.DamageAbsorptionRate;
                    equipments.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                    equipments.AbsorbedDamageRate += e.AbsorbedDamageRate;
                    equipments.VitalityRegenerationRate += e.VitalityRegenerationRate;
                    equipments.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                    equipments.AccuracyRate += e.AccuracyRate;
                    equipments.LifestealRate += e.LifestealRate;
                    equipments.ShieldStrength += e.ShieldStrength;
                    equipments.Tenacity += e.Tenacity;
                    equipments.ResistanceRate += e.ResistanceRate;
                    equipments.ComboRate += e.ComboRate;
                    equipments.IgnoreComboRate += e.IgnoreComboRate;
                    equipments.ComboDamageRate += e.ComboDamageRate;
                    equipments.ComboResistanceRate += e.ComboResistanceRate;
                    equipments.StunRate += e.StunRate;
                    equipments.IgnoreStunRate += e.IgnoreStunRate;
                    equipments.ReflectionRate += e.ReflectionRate;
                    equipments.IgnoreReflectionRate += e.IgnoreReflectionRate;
                    equipments.ReflectionDamageRate += e.ReflectionDamageRate;
                    equipments.ReflectionResistanceRate += e.ReflectionResistanceRate;
                    equipments.Mana += e.Mana;
                    equipments.ManaRegenerationRate += e.ManaRegenerationRate;
                    equipments.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                    equipments.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                    equipments.DamageToSameFactionRate += e.DamageToSameFactionRate;
                    equipments.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                    equipments.NormalDamageRate += e.NormalDamageRate;
                    equipments.NormalResistanceRate += e.NormalResistanceRate;
                    equipments.SkillDamageRate += e.SkillDamageRate;
                    equipments.SkillResistanceRate += e.SkillResistanceRate;
                    equipments.SpecialHealth += e.SpecialHealth;
                    equipments.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                    equipments.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                    equipments.SpecialMagicalAttack += e.SpecialMagicalAttack;
                    equipments.SpecialMagicalDefense += e.SpecialMagicalDefense;
                    equipments.SpecialChemicalAttack += e.SpecialChemicalAttack;
                    equipments.SpecialChemicalDefense += e.SpecialChemicalDefense;
                    equipments.SpecialAtomicAttack += e.SpecialAtomicAttack;
                    equipments.SpecialAtomicDefense += e.SpecialAtomicDefense;
                    equipments.SpecialMentalAttack += e.SpecialMentalAttack;
                    equipments.SpecialMentalDefense += e.SpecialMentalDefense;
                    equipments.SpecialSpeed += e.Speed;
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipments;
    }
    public Equipments GetAllEquipmentsByCardMilitaryId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT *
                FROM user_card_military uc, card_military c, card_military_equipment che, user_equipments ue
                WHERE uc.card_military_id = c.id AND uc.card_military_id = che.card_military_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_military_id = @card_military_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_military_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);

                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.Power += e.Power;
                    equipments.Health += e.Health;
                    equipments.PhysicalAttack += e.PhysicalAttack;
                    equipments.PhysicalDefense += e.PhysicalDefense;
                    equipments.MagicalAttack += e.MagicalAttack;
                    equipments.MagicalDefense += e.MagicalDefense;
                    equipments.ChemicalAttack += e.ChemicalAttack;
                    equipments.ChemicalDefense += e.ChemicalDefense;
                    equipments.AtomicAttack += e.AtomicAttack;
                    equipments.AtomicDefense += e.AtomicDefense;
                    equipments.MentalAttack += e.MentalAttack;
                    equipments.MentalDefense += e.MentalDefense;
                    equipments.Speed += e.Speed;
                    equipments.CriticalDamageRate += e.CriticalDamageRate;
                    equipments.CriticalRate += e.CriticalRate;
                    equipments.CriticalResistanceRate += e.CriticalResistanceRate;
                    equipments.IgnoreCriticalRate += e.IgnoreCriticalRate;
                    equipments.PenetrationRate += e.PenetrationRate;
                    equipments.PenetrationResistanceRate += e.PenetrationResistanceRate;
                    equipments.EvasionRate += e.EvasionRate;
                    equipments.DamageAbsorptionRate += e.DamageAbsorptionRate;
                    equipments.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                    equipments.AbsorbedDamageRate += e.AbsorbedDamageRate;
                    equipments.VitalityRegenerationRate += e.VitalityRegenerationRate;
                    equipments.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                    equipments.AccuracyRate += e.AccuracyRate;
                    equipments.LifestealRate += e.LifestealRate;
                    equipments.ShieldStrength += e.ShieldStrength;
                    equipments.Tenacity += e.Tenacity;
                    equipments.ResistanceRate += e.ResistanceRate;
                    equipments.ComboRate += e.ComboRate;
                    equipments.IgnoreComboRate += e.IgnoreComboRate;
                    equipments.ComboDamageRate += e.ComboDamageRate;
                    equipments.ComboResistanceRate += e.ComboResistanceRate;
                    equipments.StunRate += e.StunRate;
                    equipments.IgnoreStunRate += e.IgnoreStunRate;
                    equipments.ReflectionRate += e.ReflectionRate;
                    equipments.IgnoreReflectionRate += e.IgnoreReflectionRate;
                    equipments.ReflectionDamageRate += e.ReflectionDamageRate;
                    equipments.ReflectionResistanceRate += e.ReflectionResistanceRate;
                    equipments.Mana += e.Mana;
                    equipments.ManaRegenerationRate += e.ManaRegenerationRate;
                    equipments.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                    equipments.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                    equipments.DamageToSameFactionRate += e.DamageToSameFactionRate;
                    equipments.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                    equipments.NormalDamageRate += e.NormalDamageRate;
                    equipments.NormalResistanceRate += e.NormalResistanceRate;
                    equipments.SkillDamageRate += e.SkillDamageRate;
                    equipments.SkillResistanceRate += e.SkillResistanceRate;
                    equipments.SpecialHealth += e.SpecialHealth;
                    equipments.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                    equipments.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                    equipments.SpecialMagicalAttack += e.SpecialMagicalAttack;
                    equipments.SpecialMagicalDefense += e.SpecialMagicalDefense;
                    equipments.SpecialChemicalAttack += e.SpecialChemicalAttack;
                    equipments.SpecialChemicalDefense += e.SpecialChemicalDefense;
                    equipments.SpecialAtomicAttack += e.SpecialAtomicAttack;
                    equipments.SpecialAtomicDefense += e.SpecialAtomicDefense;
                    equipments.SpecialMentalAttack += e.SpecialMentalAttack;
                    equipments.SpecialMentalDefense += e.SpecialMentalDefense;
                    equipments.SpecialSpeed += e.Speed;
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipments;
    }
    public Equipments GetAllEquipmentsByCardSpellId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT *
                FROM user_card_spell uc, card_spell c, card_spell_equipment che, user_equipments ue
                WHERE uc.card_spell_id = c.id AND uc.card_spell_id = che.card_spell_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.card_spell_id = @card_spell_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@card_spell_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);

                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.Power += e.Power;
                    equipments.Health += e.Health;
                    equipments.PhysicalAttack += e.PhysicalAttack;
                    equipments.PhysicalDefense += e.PhysicalDefense;
                    equipments.MagicalAttack += e.MagicalAttack;
                    equipments.MagicalDefense += e.MagicalDefense;
                    equipments.ChemicalAttack += e.ChemicalAttack;
                    equipments.ChemicalDefense += e.ChemicalDefense;
                    equipments.AtomicAttack += e.AtomicAttack;
                    equipments.AtomicDefense += e.AtomicDefense;
                    equipments.MentalAttack += e.MentalAttack;
                    equipments.MentalDefense += e.MentalDefense;
                    equipments.Speed += e.Speed;
                    equipments.CriticalDamageRate += e.CriticalDamageRate;
                    equipments.CriticalRate += e.CriticalRate;
                    equipments.CriticalResistanceRate += e.CriticalResistanceRate;
                    equipments.IgnoreCriticalRate += e.IgnoreCriticalRate;
                    equipments.PenetrationRate += e.PenetrationRate;
                    equipments.PenetrationResistanceRate += e.PenetrationResistanceRate;
                    equipments.EvasionRate += e.EvasionRate;
                    equipments.DamageAbsorptionRate += e.DamageAbsorptionRate;
                    equipments.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                    equipments.AbsorbedDamageRate += e.AbsorbedDamageRate;
                    equipments.VitalityRegenerationRate += e.VitalityRegenerationRate;
                    equipments.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                    equipments.AccuracyRate += e.AccuracyRate;
                    equipments.LifestealRate += e.LifestealRate;
                    equipments.ShieldStrength += e.ShieldStrength;
                    equipments.Tenacity += e.Tenacity;
                    equipments.ResistanceRate += e.ResistanceRate;
                    equipments.ComboRate += e.ComboRate;
                    equipments.IgnoreComboRate += e.IgnoreComboRate;
                    equipments.ComboDamageRate += e.ComboDamageRate;
                    equipments.ComboResistanceRate += e.ComboResistanceRate;
                    equipments.StunRate += e.StunRate;
                    equipments.IgnoreStunRate += e.IgnoreStunRate;
                    equipments.ReflectionRate += e.ReflectionRate;
                    equipments.IgnoreReflectionRate += e.IgnoreReflectionRate;
                    equipments.ReflectionDamageRate += e.ReflectionDamageRate;
                    equipments.ReflectionResistanceRate += e.ReflectionResistanceRate;
                    equipments.Mana += e.Mana;
                    equipments.ManaRegenerationRate += e.ManaRegenerationRate;
                    equipments.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                    equipments.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                    equipments.DamageToSameFactionRate += e.DamageToSameFactionRate;
                    equipments.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                    equipments.NormalDamageRate += e.NormalDamageRate;
                    equipments.NormalResistanceRate += e.NormalResistanceRate;
                    equipments.SkillDamageRate += e.SkillDamageRate;
                    equipments.SkillResistanceRate += e.SkillResistanceRate;
                    equipments.SpecialHealth += e.SpecialHealth;
                    equipments.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                    equipments.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                    equipments.SpecialMagicalAttack += e.SpecialMagicalAttack;
                    equipments.SpecialMagicalDefense += e.SpecialMagicalDefense;
                    equipments.SpecialChemicalAttack += e.SpecialChemicalAttack;
                    equipments.SpecialChemicalDefense += e.SpecialChemicalDefense;
                    equipments.SpecialAtomicAttack += e.SpecialAtomicAttack;
                    equipments.SpecialAtomicDefense += e.SpecialAtomicDefense;
                    equipments.SpecialMentalAttack += e.SpecialMentalAttack;
                    equipments.SpecialMentalDefense += e.SpecialMentalDefense;
                    equipments.SpecialSpeed += e.Speed;
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipments;
    }
    public Equipments GetAllEquipmentsByBooksId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT *
                FROM user_books uc, books c, books_equipment che, user_equipments ue
                WHERE uc.book_id = c.id AND uc.book_id = che.book_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.book_id = @book_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@book_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);

                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.Power += e.Power;
                    equipments.Health += e.Health;
                    equipments.PhysicalAttack += e.PhysicalAttack;
                    equipments.PhysicalDefense += e.PhysicalDefense;
                    equipments.MagicalAttack += e.MagicalAttack;
                    equipments.MagicalDefense += e.MagicalDefense;
                    equipments.ChemicalAttack += e.ChemicalAttack;
                    equipments.ChemicalDefense += e.ChemicalDefense;
                    equipments.AtomicAttack += e.AtomicAttack;
                    equipments.AtomicDefense += e.AtomicDefense;
                    equipments.MentalAttack += e.MentalAttack;
                    equipments.MentalDefense += e.MentalDefense;
                    equipments.Speed += e.Speed;
                    equipments.CriticalDamageRate += e.CriticalDamageRate;
                    equipments.CriticalRate += e.CriticalRate;
                    equipments.CriticalResistanceRate += e.CriticalResistanceRate;
                    equipments.IgnoreCriticalRate += e.IgnoreCriticalRate;
                    equipments.PenetrationRate += e.PenetrationRate;
                    equipments.PenetrationResistanceRate += e.PenetrationResistanceRate;
                    equipments.EvasionRate += e.EvasionRate;
                    equipments.DamageAbsorptionRate += e.DamageAbsorptionRate;
                    equipments.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                    equipments.AbsorbedDamageRate += e.AbsorbedDamageRate;
                    equipments.VitalityRegenerationRate += e.VitalityRegenerationRate;
                    equipments.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                    equipments.AccuracyRate += e.AccuracyRate;
                    equipments.LifestealRate += e.LifestealRate;
                    equipments.ShieldStrength += e.ShieldStrength;
                    equipments.Tenacity += e.Tenacity;
                    equipments.ResistanceRate += e.ResistanceRate;
                    equipments.ComboRate += e.ComboRate;
                    equipments.IgnoreComboRate += e.IgnoreComboRate;
                    equipments.ComboDamageRate += e.ComboDamageRate;
                    equipments.ComboResistanceRate += e.ComboResistanceRate;
                    equipments.StunRate += e.StunRate;
                    equipments.IgnoreStunRate += e.IgnoreStunRate;
                    equipments.ReflectionRate += e.ReflectionRate;
                    equipments.IgnoreReflectionRate += e.IgnoreReflectionRate;
                    equipments.ReflectionDamageRate += e.ReflectionDamageRate;
                    equipments.ReflectionResistanceRate += e.ReflectionResistanceRate;
                    equipments.Mana += e.Mana;
                    equipments.ManaRegenerationRate += e.ManaRegenerationRate;
                    equipments.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                    equipments.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                    equipments.DamageToSameFactionRate += e.DamageToSameFactionRate;
                    equipments.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                    equipments.NormalDamageRate += e.NormalDamageRate;
                    equipments.NormalResistanceRate += e.NormalResistanceRate;
                    equipments.SkillDamageRate += e.SkillDamageRate;
                    equipments.SkillResistanceRate += e.SkillResistanceRate;
                    equipments.SpecialHealth += e.SpecialHealth;
                    equipments.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                    equipments.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                    equipments.SpecialMagicalAttack += e.SpecialMagicalAttack;
                    equipments.SpecialMagicalDefense += e.SpecialMagicalDefense;
                    equipments.SpecialChemicalAttack += e.SpecialChemicalAttack;
                    equipments.SpecialChemicalDefense += e.SpecialChemicalDefense;
                    equipments.SpecialAtomicAttack += e.SpecialAtomicAttack;
                    equipments.SpecialAtomicDefense += e.SpecialAtomicDefense;
                    equipments.SpecialMentalAttack += e.SpecialMentalAttack;
                    equipments.SpecialMentalDefense += e.SpecialMentalDefense;
                    equipments.SpecialSpeed += e.Speed;
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipments;
    }
    public Equipments GetAllEquipmentsByPetsId(string user_id, string Id)
    {
        Equipments equipments = new Equipments();
        equipments = ChangeValueToZero(equipments);
        List<Equipments> equipmentList = new List<Equipments>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT *
                FROM user_pets uc, pets c, pets_equipment che, user_equipments ue
                WHERE uc.pet_id = c.id AND uc.pet_id = che.pet_id 
                AND che.equipment_id = ue.equipment_id AND che.sequence = ue.sequence
                AND uc.user_id = @user_id and uc.pet_id = @pet_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@pet_id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Equipments tmpEquipments = new Equipments();
                    tmpEquipments.Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                    tmpEquipments.Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                    tmpEquipments.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                    tmpEquipments.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                    tmpEquipments.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                    tmpEquipments.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                    tmpEquipments.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                    tmpEquipments.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                    tmpEquipments.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                    tmpEquipments.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                    tmpEquipments.MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                    tmpEquipments.MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                    tmpEquipments.Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                    tmpEquipments.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                    tmpEquipments.CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                    tmpEquipments.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                    tmpEquipments.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                    tmpEquipments.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                    tmpEquipments.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                    tmpEquipments.EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                    tmpEquipments.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                    tmpEquipments.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                    tmpEquipments.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                    tmpEquipments.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                    tmpEquipments.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                    tmpEquipments.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                    tmpEquipments.LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                    tmpEquipments.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                    tmpEquipments.Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                    tmpEquipments.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                    tmpEquipments.ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                    tmpEquipments.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                    tmpEquipments.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                    tmpEquipments.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                    tmpEquipments.StunRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                    tmpEquipments.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                    tmpEquipments.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                    tmpEquipments.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                    tmpEquipments.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                    tmpEquipments.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                    tmpEquipments.Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                    tmpEquipments.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                    tmpEquipments.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                    tmpEquipments.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                    tmpEquipments.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                    tmpEquipments.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                    tmpEquipments.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                    tmpEquipments.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                    tmpEquipments.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                    tmpEquipments.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                    tmpEquipments.SpecialHealth = reader.IsDBNull(reader.GetOrdinal("special_health")) ? 0 : reader.GetDouble("special_health");
                    tmpEquipments.SpecialPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("special_physical_attack")) ? 0 : reader.GetDouble("special_physical_attack");
                    tmpEquipments.SpecialPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("special_physical_defense")) ? 0 : reader.GetDouble("special_physical_defense");
                    tmpEquipments.SpecialMagicalAttack = reader.IsDBNull(reader.GetOrdinal("special_magical_attack")) ? 0 : reader.GetDouble("special_magical_attack");
                    tmpEquipments.SpecialMagicalDefense = reader.IsDBNull(reader.GetOrdinal("special_magical_defense")) ? 0 : reader.GetDouble("special_magical_defense");
                    tmpEquipments.SpecialChemicalAttack = reader.IsDBNull(reader.GetOrdinal("special_chemical_attack")) ? 0 : reader.GetDouble("special_chemical_attack");
                    tmpEquipments.SpecialChemicalDefense = reader.IsDBNull(reader.GetOrdinal("special_chemical_defense")) ? 0 : reader.GetDouble("special_chemical_defense");
                    tmpEquipments.SpecialAtomicAttack = reader.IsDBNull(reader.GetOrdinal("special_atomic_attack")) ? 0 : reader.GetDouble("special_atomic_attack");
                    tmpEquipments.SpecialAtomicDefense = reader.IsDBNull(reader.GetOrdinal("special_atomic_defense")) ? 0 : reader.GetDouble("special_atomic_defense");
                    tmpEquipments.SpecialMentalAttack = reader.IsDBNull(reader.GetOrdinal("special_mental_attack")) ? 0 : reader.GetDouble("special_mental_attack");
                    tmpEquipments.SpecialMentalDefense = reader.IsDBNull(reader.GetOrdinal("special_mental_defense")) ? 0 : reader.GetDouble("special_mental_defense");
                    tmpEquipments.SpecialSpeed = reader.IsDBNull(reader.GetOrdinal("special_speed")) ? 0 : reader.GetDouble("special_speed");
                    equipmentList.Add(tmpEquipments);

                }

                foreach (Equipments e in equipmentList)
                {
                    equipments.Power += e.Power;
                    equipments.Health += e.Health;
                    equipments.PhysicalAttack += e.PhysicalAttack;
                    equipments.PhysicalDefense += e.PhysicalDefense;
                    equipments.MagicalAttack += e.MagicalAttack;
                    equipments.MagicalDefense += e.MagicalDefense;
                    equipments.ChemicalAttack += e.ChemicalAttack;
                    equipments.ChemicalDefense += e.ChemicalDefense;
                    equipments.AtomicAttack += e.AtomicAttack;
                    equipments.AtomicDefense += e.AtomicDefense;
                    equipments.MentalAttack += e.MentalAttack;
                    equipments.MentalDefense += e.MentalDefense;
                    equipments.Speed += e.Speed;
                    equipments.CriticalDamageRate += e.CriticalDamageRate;
                    equipments.CriticalRate += e.CriticalRate;
                    equipments.CriticalResistanceRate += e.CriticalResistanceRate;
                    equipments.IgnoreCriticalRate += e.IgnoreCriticalRate;
                    equipments.PenetrationRate += e.PenetrationRate;
                    equipments.PenetrationResistanceRate += e.PenetrationResistanceRate;
                    equipments.EvasionRate += e.EvasionRate;
                    equipments.DamageAbsorptionRate += e.DamageAbsorptionRate;
                    equipments.IgnoreDamageAbsorptionRate += e.IgnoreDamageAbsorptionRate;
                    equipments.AbsorbedDamageRate += e.AbsorbedDamageRate;
                    equipments.VitalityRegenerationRate += e.VitalityRegenerationRate;
                    equipments.VitalityRegenerationResistanceRate += e.VitalityRegenerationResistanceRate;
                    equipments.AccuracyRate += e.AccuracyRate;
                    equipments.LifestealRate += e.LifestealRate;
                    equipments.ShieldStrength += e.ShieldStrength;
                    equipments.Tenacity += e.Tenacity;
                    equipments.ResistanceRate += e.ResistanceRate;
                    equipments.ComboRate += e.ComboRate;
                    equipments.IgnoreComboRate += e.IgnoreComboRate;
                    equipments.ComboDamageRate += e.ComboDamageRate;
                    equipments.ComboResistanceRate += e.ComboResistanceRate;
                    equipments.StunRate += e.StunRate;
                    equipments.IgnoreStunRate += e.IgnoreStunRate;
                    equipments.ReflectionRate += e.ReflectionRate;
                    equipments.IgnoreReflectionRate += e.IgnoreReflectionRate;
                    equipments.ReflectionDamageRate += e.ReflectionDamageRate;
                    equipments.ReflectionResistanceRate += e.ReflectionResistanceRate;
                    equipments.Mana += e.Mana;
                    equipments.ManaRegenerationRate += e.ManaRegenerationRate;
                    equipments.DamageToDifferentFactionRate += e.DamageToDifferentFactionRate;
                    equipments.ResistanceToDifferentFactionRate += e.ResistanceToDifferentFactionRate;
                    equipments.DamageToSameFactionRate += e.DamageToSameFactionRate;
                    equipments.ResistanceToSameFactionRate += e.ResistanceToSameFactionRate;
                    equipments.NormalDamageRate += e.NormalDamageRate;
                    equipments.NormalResistanceRate += e.NormalResistanceRate;
                    equipments.SkillDamageRate += e.SkillDamageRate;
                    equipments.SkillResistanceRate += e.SkillResistanceRate;
                    equipments.SpecialHealth += e.SpecialHealth;
                    equipments.SpecialPhysicalAttack += e.SpecialPhysicalAttack;
                    equipments.SpecialPhysicalDefense += e.SpecialPhysicalDefense;
                    equipments.SpecialMagicalAttack += e.SpecialMagicalAttack;
                    equipments.SpecialMagicalDefense += e.SpecialMagicalDefense;
                    equipments.SpecialChemicalAttack += e.SpecialChemicalAttack;
                    equipments.SpecialChemicalDefense += e.SpecialChemicalDefense;
                    equipments.SpecialAtomicAttack += e.SpecialAtomicAttack;
                    equipments.SpecialAtomicDefense += e.SpecialAtomicDefense;
                    equipments.SpecialMentalAttack += e.SpecialMentalAttack;
                    equipments.SpecialMentalDefense += e.SpecialMentalDefense;
                    equipments.SpecialSpeed += e.Speed;
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return equipments;
    }
}