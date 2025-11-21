using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class CoresGalleryRepository : ICoresGalleryRepository
{
    public List<Cores> GetCoresCollection(int pageSize, int offset, string rare)
    {
        List<Cores> CoresList = new List<Cores>();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT t.*, tg.current_star, tg.temp_star, CASE WHEN tg.card_id IS NULL THEN 'block' WHEN tg.status = 'pending' THEN 'pending' WHEN tg.status = 'available' THEN 'available' END AS status 
                FROM Cores t LEFT JOIN Cores_gallery tg ON t.id = tg.card_id and tg.user_id = @userId 
                Where (@rare = 'All' or t.rare = @rare)
                ORDER BY t.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(t.name, '[0-9]+$') AS UNSIGNED), t.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@rare", rare);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Cores card = new Cores
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Power = reader.GetDouble("power"),
                        CurrentStar = reader.IsDBNull(reader.GetOrdinal("current_star")) ? 0 : reader.GetInt32("current_star"),
                        TempStar = reader.IsDBNull(reader.GetOrdinal("temp_star")) ? 0 : reader.GetInt32("temp_star"),
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
                        Status = reader.GetString("status")
                    };

                    CoresList.Add(card);
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
        return CoresList;
    }
    public int GetCoresCount(string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from Cores Where (@rare = 'All' or rare = @rare)";
                MySqlCommand command = new MySqlCommand(query, connection);
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
    public void InsertCoresGallery(string Id, Cores cardFromDB)
    {
        // Cores cardFromDB = GetCoresById(Id);
        int percent = 20;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra bản ghi đã tồn tại
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM Cores_gallery 
                WHERE user_id = @user_id AND card_id = @card_id;
                ";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_id", Id);

                int recordCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (recordCount == 0)
                {
                    string query = @"
                    INSERT INTO Cores_gallery (
                        user_id, card_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
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
                        @user_id, @card_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
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

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@card_id", Id);
                    command.Parameters.AddWithValue("@status", "pending");
                    command.Parameters.AddWithValue("@current_star", 0);
                    command.Parameters.AddWithValue("@temp_star", 0);
                    command.Parameters.AddWithValue("@power", cardFromDB.Power);
                    command.Parameters.AddWithValue("@health", cardFromDB.Health);
                    command.Parameters.AddWithValue("@physical_attack", cardFromDB.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", cardFromDB.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", cardFromDB.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", cardFromDB.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", cardFromDB.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", cardFromDB.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", cardFromDB.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", cardFromDB.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", cardFromDB.MagicalAttack);
                    command.Parameters.AddWithValue("@mental_defense", cardFromDB.MagicalDefense);
                    command.Parameters.AddWithValue("@speed", cardFromDB.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", cardFromDB.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", cardFromDB.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", cardFromDB.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", cardFromDB.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", cardFromDB.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", cardFromDB.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", cardFromDB.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", cardFromDB.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardFromDB.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", cardFromDB.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", cardFromDB.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardFromDB.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", cardFromDB.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", cardFromDB.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", cardFromDB.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", cardFromDB.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", cardFromDB.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", cardFromDB.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", cardFromDB.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", cardFromDB.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", cardFromDB.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", cardFromDB.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", cardFromDB.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", cardFromDB.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", cardFromDB.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", cardFromDB.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", cardFromDB.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", cardFromDB.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", cardFromDB.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardFromDB.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardFromDB.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardFromDB.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardFromDB.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", cardFromDB.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", cardFromDB.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", cardFromDB.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", cardFromDB.SkillResistanceRate);
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
                    command.ExecuteNonQuery();
                    Debug.Log("Data inserted successfully.");
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
    }
    public void UpdateStatusCoresGallery(string Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "update Cores_gallery set status=@status where user_id=@user_id and card_id=@card_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_id", Id);
                command.Parameters.AddWithValue("@status", "available");
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
    public void UpdateStarCoresGallery(string Id, double star)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                // Kiểm tra bản ghi đã tồn tại
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM Cores_gallery 
                WHERE user_id = @user_id AND card_id = @card_id;
                ";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_id", Id);

                MySqlDataReader reader = checkCommand.ExecuteReader();
                if (reader != null)
                {
                    var currentStar = reader.GetDouble("current_star");
                    var tempStar = reader.GetDouble("temp_star");
                    if (tempStar < star)
                    {
                        string updateQuery = @"update Cores_gallery 
                        set temp_star=@temp_star 
                        where user_id=@user_id and card_id=@card_id";

                        MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        updateCommand.Parameters.AddWithValue("@card_id", Id);
                        updateCommand.Parameters.AddWithValue("@temp_star", star);
                        updateCommand.ExecuteNonQuery();
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
    }
    public void UpdateCoresGalleryPower(string Id, Cores cardFromDB)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"UPDATE Cores_gallery
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
                AND card_id = @card_id;
                ";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_id", Id);
                command.Parameters.AddWithValue("@status", "pending");
                command.Parameters.AddWithValue("@current_star", 0);
                command.Parameters.AddWithValue("@power", cardFromDB.Power);
                command.Parameters.AddWithValue("@health", cardFromDB.Health);
                command.Parameters.AddWithValue("@physical_attack", cardFromDB.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", cardFromDB.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", cardFromDB.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", cardFromDB.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", cardFromDB.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", cardFromDB.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", cardFromDB.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", cardFromDB.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", cardFromDB.MagicalAttack);
                command.Parameters.AddWithValue("@mental_defense", cardFromDB.MagicalDefense);
                command.Parameters.AddWithValue("@speed", cardFromDB.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", cardFromDB.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", cardFromDB.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", cardFromDB.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", cardFromDB.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", cardFromDB.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", cardFromDB.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", cardFromDB.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", cardFromDB.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardFromDB.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", cardFromDB.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", cardFromDB.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardFromDB.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", cardFromDB.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", cardFromDB.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", cardFromDB.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", cardFromDB.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", cardFromDB.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", cardFromDB.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", cardFromDB.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", cardFromDB.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", cardFromDB.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", cardFromDB.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", cardFromDB.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", cardFromDB.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", cardFromDB.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", cardFromDB.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", cardFromDB.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", cardFromDB.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", cardFromDB.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardFromDB.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardFromDB.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardFromDB.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardFromDB.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", cardFromDB.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", cardFromDB.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", cardFromDB.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", cardFromDB.SkillResistanceRate);
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
    public Cores SumPowerCoresGallery()
    {
        Cores sumCores = new Cores();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
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
            FROM Cores_gallery 
            WHERE user_id = @user_id AND status = 'available';
            ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumCores.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumCores.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumCores.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumCores.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumCores.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumCores.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumCores.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumCores.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumCores.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumCores.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumCores.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumCores.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumCores.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumCores.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                        sumCores.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumCores.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                        sumCores.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                        sumCores.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                        sumCores.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                        sumCores.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                        sumCores.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                        sumCores.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                        sumCores.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                        sumCores.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                        sumCores.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                        sumCores.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                        sumCores.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                        sumCores.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                        sumCores.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                        sumCores.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                        sumCores.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                        sumCores.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                        sumCores.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                        sumCores.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                        sumCores.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                        sumCores.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                        sumCores.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                        sumCores.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                        sumCores.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                        sumCores.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                        sumCores.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                        sumCores.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                        sumCores.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                        sumCores.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                        sumCores.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                        sumCores.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                        sumCores.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                        sumCores.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                        sumCores.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                        sumCores.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
                        sumCores.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDouble("total_percent_all_health");
                        sumCores.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDouble("total_percent_all_physical_attack");
                        sumCores.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDouble("total_percent_all_physical_defense");
                        sumCores.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDouble("total_percent_all_magical_attack");
                        sumCores.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDouble("total_percent_all_magical_defense");
                        sumCores.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDouble("total_percent_all_chemical_attack");
                        sumCores.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDouble("total_percent_all_chemical_defense");
                        sumCores.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDouble("total_percent_all_atomic_attack");
                        sumCores.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDouble("total_percent_all_atomic_defense");
                        sumCores.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDouble("total_percent_all_mental_attack");
                        sumCores.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDouble("total_percent_all_mental_defense");
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
        return sumCores;
    }
}