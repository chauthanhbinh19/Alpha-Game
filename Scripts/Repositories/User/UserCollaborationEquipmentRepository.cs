using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserCollaborationEquipmentRepository : IUserCollaborationEquipmentRepository
{
    public List<CollaborationEquipments> GetUserCollaborationEquipments(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<CollaborationEquipments> collaborationEquipmentList = new List<CollaborationEquipments>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select uce.*, ce.image, ce.rare, ce.type, ce.name, ce.description from collaboration_equipments ce, user_collaboration_equipments uce 
                where ce.id=uce.collaboration_equipment_id and uce.user_id=@userId and ce.type= @type AND (@rare = 'All' or ce.rare = @rare)
                ORDER BY ce.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(ce.name, '[0-9]+$') AS UNSIGNED), ce.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@rare", rare);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CollaborationEquipments collaborationEquipment = new CollaborationEquipments
                    {
                        Id = reader.GetString("collaboration_equipment_id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        Star = reader.GetInt32("star"),
                        Level = reader.GetInt32("level"),
                        Experiment = reader.GetInt32("experiment"),
                        Quantity = reader.GetInt32("quantity"),
                        Block = reader.GetBoolean("block"),
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
                        Description = reader.GetString("description")
                    };

                    collaborationEquipmentList.Add(collaborationEquipment);
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
        return collaborationEquipmentList;
    }
    public int GetUserCollaborationEquipmentCount(string user_id, string type, string rare)
    {
        int count = 0;
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select count(*) from collaboration_equipments ce, user_collaboration_equipments uce 
                where ce.id=uce.collaboration_equipment_id and uce.user_id=@userId and type= @type AND (@rare = 'All' or ce.rare = @rare)";
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
    public bool InsertUserCollaborationEquipments(CollaborationEquipments collaborationEquipment)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string checkQuery = @"
                SELECT COUNT(*) FROM user_collaboration_equipments 
                WHERE user_id = @user_id AND collaboration_equipment_id = @collaboration_equipment_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@collaboration_equipment_id", collaborationEquipment.Id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_collaboration_equipments (
                    user_id, collaboration_equipment_id, rare, level, experiment, star, quality, block, quantity,
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
                    @user_id, @collaboration_equipment_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@collaboration_equipment_id", collaborationEquipment.Id);
                    command.Parameters.AddWithValue("@rare", collaborationEquipment.Rare);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(collaborationEquipment.Rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", collaborationEquipment.Quantity);
                    command.Parameters.AddWithValue("@power", collaborationEquipment.Power);
                    command.Parameters.AddWithValue("@health", collaborationEquipment.Health);
                    command.Parameters.AddWithValue("@physical_attack", collaborationEquipment.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", collaborationEquipment.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", collaborationEquipment.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", collaborationEquipment.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", collaborationEquipment.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", collaborationEquipment.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", collaborationEquipment.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", collaborationEquipment.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", collaborationEquipment.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", collaborationEquipment.MentalDefense);
                    command.Parameters.AddWithValue("@speed", collaborationEquipment.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", collaborationEquipment.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", collaborationEquipment.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", collaborationEquipment.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", collaborationEquipment.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", collaborationEquipment.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", collaborationEquipment.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", collaborationEquipment.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", collaborationEquipment.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", collaborationEquipment.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", collaborationEquipment.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", collaborationEquipment.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", collaborationEquipment.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", collaborationEquipment.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", collaborationEquipment.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", collaborationEquipment.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", collaborationEquipment.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", collaborationEquipment.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", collaborationEquipment.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", collaborationEquipment.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", collaborationEquipment.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", collaborationEquipment.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", collaborationEquipment.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", collaborationEquipment.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", collaborationEquipment.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", collaborationEquipment.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", collaborationEquipment.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", collaborationEquipment.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", collaborationEquipment.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", collaborationEquipment.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", collaborationEquipment.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", collaborationEquipment.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", collaborationEquipment.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", collaborationEquipment.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", collaborationEquipment.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", collaborationEquipment.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", collaborationEquipment.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", collaborationEquipment.SkillResistanceRate);
                    command.ExecuteNonQuery();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_collaboration_equipments
                    SET quantity = @quantity
                    WHERE user_id = @user_id AND collaboration_equipment_id = @collaboration_equipment_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@collaboration_equipment_id", collaborationEquipment.Id);
                    updateCommand.Parameters.AddWithValue("@quantity", collaborationEquipment.Quantity);

                    updateCommand.ExecuteNonQuery();
                }

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
    public bool UpdateCollaborationEquipmentsLevel(CollaborationEquipments collaborationEquipment, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_collaboration_equipments
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
                WHERE user_id = @user_id AND collaboration_equipment_id = @collaboration_equipment_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@collaboration_equipment_id", collaborationEquipment.Id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", collaborationEquipment.Power);
                command.Parameters.AddWithValue("@health", collaborationEquipment.Health);
                command.Parameters.AddWithValue("@physical_attack", collaborationEquipment.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", collaborationEquipment.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", collaborationEquipment.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", collaborationEquipment.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", collaborationEquipment.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", collaborationEquipment.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", collaborationEquipment.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", collaborationEquipment.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", collaborationEquipment.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", collaborationEquipment.MentalDefense);
                command.Parameters.AddWithValue("@speed", collaborationEquipment.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", collaborationEquipment.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", collaborationEquipment.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", collaborationEquipment.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", collaborationEquipment.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", collaborationEquipment.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", collaborationEquipment.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", collaborationEquipment.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", collaborationEquipment.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", collaborationEquipment.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", collaborationEquipment.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", collaborationEquipment.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", collaborationEquipment.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", collaborationEquipment.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", collaborationEquipment.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", collaborationEquipment.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", collaborationEquipment.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", collaborationEquipment.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", collaborationEquipment.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", collaborationEquipment.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", collaborationEquipment.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", collaborationEquipment.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", collaborationEquipment.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", collaborationEquipment.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", collaborationEquipment.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", collaborationEquipment.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", collaborationEquipment.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", collaborationEquipment.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", collaborationEquipment.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", collaborationEquipment.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", collaborationEquipment.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", collaborationEquipment.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", collaborationEquipment.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", collaborationEquipment.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", collaborationEquipment.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", collaborationEquipment.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", collaborationEquipment.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", collaborationEquipment.SkillResistanceRate);
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
    public bool UpdateCollaborationEquipmentsBreakthrough(CollaborationEquipments collaborationEquipment, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_collaboration_equipments
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
                WHERE user_id = @user_id AND collaboration_equipment_id = @collaboration_equipment_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@collaboration_equipment_id", collaborationEquipment.Id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", collaborationEquipment.Power);
                command.Parameters.AddWithValue("@health", collaborationEquipment.Health);
                command.Parameters.AddWithValue("@physical_attack", collaborationEquipment.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", collaborationEquipment.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", collaborationEquipment.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", collaborationEquipment.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", collaborationEquipment.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", collaborationEquipment.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", collaborationEquipment.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", collaborationEquipment.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", collaborationEquipment.MentalAttack);
                command.Parameters.AddWithValue("@mental_defense", collaborationEquipment.MentalDefense);
                command.Parameters.AddWithValue("@speed", collaborationEquipment.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", collaborationEquipment.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", collaborationEquipment.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", collaborationEquipment.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", collaborationEquipment.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", collaborationEquipment.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", collaborationEquipment.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", collaborationEquipment.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", collaborationEquipment.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", collaborationEquipment.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", collaborationEquipment.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", collaborationEquipment.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", collaborationEquipment.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", collaborationEquipment.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", collaborationEquipment.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", collaborationEquipment.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", collaborationEquipment.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", collaborationEquipment.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", collaborationEquipment.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", collaborationEquipment.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", collaborationEquipment.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", collaborationEquipment.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", collaborationEquipment.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", collaborationEquipment.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", collaborationEquipment.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", collaborationEquipment.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", collaborationEquipment.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", collaborationEquipment.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", collaborationEquipment.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", collaborationEquipment.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", collaborationEquipment.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", collaborationEquipment.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", collaborationEquipment.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", collaborationEquipment.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", collaborationEquipment.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", collaborationEquipment.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", collaborationEquipment.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", collaborationEquipment.SkillResistanceRate);
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
    public CollaborationEquipments GetUserCollaborationEquipmentsById(string user_id, string Id)
    {
        CollaborationEquipments card = new CollaborationEquipments();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_collaboration_equipments where user_collaboration_equipments.collaboration_equipment_id=@id 
                and user_collaboration_equipments.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new CollaborationEquipments
                    {
                        Id = reader.GetString("collaboration_equipment_id"),
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
    public CollaborationEquipments SumPowerUserCollaborationEquipments()
    {
        CollaborationEquipments sumCollaborationEquipments = new CollaborationEquipments();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
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
            FROM user_collaboration_equipments
            WHERE user_id = @user_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumCollaborationEquipments.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumCollaborationEquipments.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumCollaborationEquipments.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumCollaborationEquipments.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumCollaborationEquipments.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumCollaborationEquipments.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumCollaborationEquipments.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumCollaborationEquipments.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumCollaborationEquipments.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumCollaborationEquipments.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumCollaborationEquipments.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumCollaborationEquipments.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumCollaborationEquipments.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumCollaborationEquipments.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                        sumCollaborationEquipments.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumCollaborationEquipments.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                        sumCollaborationEquipments.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                        sumCollaborationEquipments.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                        sumCollaborationEquipments.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                        sumCollaborationEquipments.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                        sumCollaborationEquipments.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                        sumCollaborationEquipments.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                        sumCollaborationEquipments.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                        sumCollaborationEquipments.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                        sumCollaborationEquipments.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                        sumCollaborationEquipments.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                        sumCollaborationEquipments.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                        sumCollaborationEquipments.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                        sumCollaborationEquipments.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                        sumCollaborationEquipments.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                        sumCollaborationEquipments.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                        sumCollaborationEquipments.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                        sumCollaborationEquipments.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                        sumCollaborationEquipments.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                        sumCollaborationEquipments.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                        sumCollaborationEquipments.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                        sumCollaborationEquipments.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                        sumCollaborationEquipments.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                        sumCollaborationEquipments.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                        sumCollaborationEquipments.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                        sumCollaborationEquipments.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                        sumCollaborationEquipments.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                        sumCollaborationEquipments.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                        sumCollaborationEquipments.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                        sumCollaborationEquipments.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                        sumCollaborationEquipments.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                        sumCollaborationEquipments.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                        sumCollaborationEquipments.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                        sumCollaborationEquipments.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                        sumCollaborationEquipments.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
                    }
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
        return sumCollaborationEquipments;
    }
}