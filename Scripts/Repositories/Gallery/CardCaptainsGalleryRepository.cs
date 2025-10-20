using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class CardCaptainsGalleryRepository : ICardCaptainsGalleryRepository
{
    public List<CardCaptains> GetCardCaptainsCollection(string type, int pageSize, int offset, string rare)
    {
        List<CardCaptains> CardCaptainsList = new List<CardCaptains>();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT c.*, cg.current_star, cg.temp_star, CASE WHEN cg.card_captain_id IS NULL THEN 'block' WHEN cg.status = 'pending' THEN 'pending' WHEN cg.status = 'available' THEN 'available' END AS status
                FROM card_captains c LEFT JOIN card_captains_gallery cg ON c.id = cg.card_captain_id and cg.user_id = @userId 
                where c.type=@type AND (@rare = 'All' or c.rare = @rare)
                ORDER BY c.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@rare", rare);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardCaptains captain = new CardCaptains
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Rare = reader.GetString("rare"),
                        Quality = reader.GetInt32("quality"),
                        Type = reader.GetString("type"),
                        CurrentStar = reader.IsDBNull(reader.GetOrdinal("current_star")) ? 0 : reader.GetInt32("current_star"),
                        TempStar = reader.IsDBNull(reader.GetOrdinal("temp_star")) ? 0 : reader.GetInt32("temp_star"),
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
                        Description = reader.GetString("description"),
                        Status = reader.GetString("status")
                    };

                    CardCaptainsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardCaptainsList;
    }
    public int GetCardCaptainsCount(string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from card_captains where type= @type AND (@rare = 'All' or rare = @rare)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@rare", rare);
                count = Convert.ToInt32(command.ExecuteScalar());

                return count;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return count;
    }
    public void InsertCardCaptainsGallery(string Id, CardCaptains CaptainFromDB)
    {
        // CardCaptains CaptainFromDB = GetCardCaptainsById(Id);
        int percent = QualityEvaluator.CheckQuality(CaptainFromDB.Type);
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra bản ghi đã tồn tại
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM card_captains_gallery 
                WHERE user_id = @user_id AND card_captain_id = @card_captain_id;
                ";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_captain_id", Id);

                int recordCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (recordCount == 0)
                {
                    string query = @"
                    INSERT INTO card_captains_gallery (
                        user_id, card_captain_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
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
                        @user_id, @card_captain_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
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
                    command.Parameters.AddWithValue("@card_captain_id", Id);
                    command.Parameters.AddWithValue("@status", "pending");
                    command.Parameters.AddWithValue("@current_star", 0);
                    command.Parameters.AddWithValue("@temp_star", 0);
                    command.Parameters.AddWithValue("@power", CaptainFromDB.Power);
                    command.Parameters.AddWithValue("@health", CaptainFromDB.Health);
                    command.Parameters.AddWithValue("@physical_attack", CaptainFromDB.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", CaptainFromDB.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", CaptainFromDB.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", CaptainFromDB.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", CaptainFromDB.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", CaptainFromDB.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", CaptainFromDB.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", CaptainFromDB.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", CaptainFromDB.MagicalAttack);
                    command.Parameters.AddWithValue("@mental_defense", CaptainFromDB.MagicalDefense);
                    command.Parameters.AddWithValue("@speed", CaptainFromDB.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", CaptainFromDB.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", CaptainFromDB.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", CaptainFromDB.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", CaptainFromDB.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", CaptainFromDB.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", CaptainFromDB.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", CaptainFromDB.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", CaptainFromDB.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", CaptainFromDB.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", CaptainFromDB.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", CaptainFromDB.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", CaptainFromDB.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", CaptainFromDB.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", CaptainFromDB.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", CaptainFromDB.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", CaptainFromDB.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", CaptainFromDB.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", CaptainFromDB.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", CaptainFromDB.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", CaptainFromDB.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", CaptainFromDB.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", CaptainFromDB.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", CaptainFromDB.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", CaptainFromDB.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", CaptainFromDB.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", CaptainFromDB.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", CaptainFromDB.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", CaptainFromDB.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", CaptainFromDB.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", CaptainFromDB.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", CaptainFromDB.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", CaptainFromDB.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", CaptainFromDB.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", CaptainFromDB.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", CaptainFromDB.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", CaptainFromDB.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", CaptainFromDB.SkillResistanceRate);
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
    public void UpdateStatusCardCaptainsGallery(string Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "update card_captain_gallery set status=@status where user_id=@user_id and card_captain_id=@card_captain_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_captain_id", Id);
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
    public void UpdateStarCardCaptainsGallery(string Id, double star)
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
                FROM card_captains_gallery 
                WHERE user_id = @user_id AND card_captain_id = @card_captain_id;
                ";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_captain_id", Id);

                MySqlDataReader reader = checkCommand.ExecuteReader();
                if (reader != null)
                {
                    var currentStar = reader.GetDouble("current_star");
                    var tempStar = reader.GetDouble("temp_star");
                    if (tempStar < star)
                    {
                        string updateQuery = @"update card_captains_gallery 
                        set temp_star=@temp_star 
                        where user_id=@user_id and card_captain_id=@card_captain_id";

                        MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        updateCommand.Parameters.AddWithValue("@card_captain_id", Id);
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
    public void UpdateCardCaptainsGalleryPower(string Id, CardCaptains CardCaptainFromDB)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"UPDATE card_captains_gallery
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
                AND card_captain_id = @card_captain_id;
                ";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_captain_id", Id);
                command.Parameters.AddWithValue("@status", "pending");
                command.Parameters.AddWithValue("@current_star", 0);
                command.Parameters.AddWithValue("@power", CardCaptainFromDB.Power);
                command.Parameters.AddWithValue("@health", CardCaptainFromDB.Health);
                command.Parameters.AddWithValue("@physical_attack", CardCaptainFromDB.PhysicalAttack);
                command.Parameters.AddWithValue("@physical_defense", CardCaptainFromDB.PhysicalDefense);
                command.Parameters.AddWithValue("@magical_attack", CardCaptainFromDB.MagicalAttack);
                command.Parameters.AddWithValue("@magical_defense", CardCaptainFromDB.MagicalDefense);
                command.Parameters.AddWithValue("@chemical_attack", CardCaptainFromDB.ChemicalAttack);
                command.Parameters.AddWithValue("@chemical_defense", CardCaptainFromDB.ChemicalDefense);
                command.Parameters.AddWithValue("@atomic_attack", CardCaptainFromDB.AtomicAttack);
                command.Parameters.AddWithValue("@atomic_defense", CardCaptainFromDB.AtomicDefense);
                command.Parameters.AddWithValue("@mental_attack", CardCaptainFromDB.MagicalAttack);
                command.Parameters.AddWithValue("@mental_defense", CardCaptainFromDB.MagicalDefense);
                command.Parameters.AddWithValue("@speed", CardCaptainFromDB.Speed);
                command.Parameters.AddWithValue("@critical_damage_rate", CardCaptainFromDB.CriticalDamageRate);
                command.Parameters.AddWithValue("@critical_rate", CardCaptainFromDB.CriticalRate);
                command.Parameters.AddWithValue("@critical_resistance_rate", CardCaptainFromDB.CriticalResistanceRate);
                command.Parameters.AddWithValue("@ignore_critical_rate", CardCaptainFromDB.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@penetration_rate", CardCaptainFromDB.PenetrationRate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", CardCaptainFromDB.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@evasion_rate", CardCaptainFromDB.EvasionRate);
                command.Parameters.AddWithValue("@damage_absorption_rate", CardCaptainFromDB.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", CardCaptainFromDB.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", CardCaptainFromDB.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", CardCaptainFromDB.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", CardCaptainFromDB.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@accuracy_rate", CardCaptainFromDB.AccuracyRate);
                command.Parameters.AddWithValue("@lifesteal_rate", CardCaptainFromDB.LifestealRate);
                command.Parameters.AddWithValue("@shield_strength", CardCaptainFromDB.ShieldStrength);
                command.Parameters.AddWithValue("@tenacity", CardCaptainFromDB.Tenacity);
                command.Parameters.AddWithValue("@resistance_rate", CardCaptainFromDB.ResistanceRate);
                command.Parameters.AddWithValue("@combo_rate", CardCaptainFromDB.ComboRate);
                command.Parameters.AddWithValue("@ignore_combo_rate", CardCaptainFromDB.IgnoreComboRate);
                command.Parameters.AddWithValue("@combo_damage_rate", CardCaptainFromDB.ComboDamageRate);
                command.Parameters.AddWithValue("@combo_resistance_rate", CardCaptainFromDB.ComboResistanceRate);
                command.Parameters.AddWithValue("@stun_rate", CardCaptainFromDB.StunRate);
                command.Parameters.AddWithValue("@ignore_stun_rate", CardCaptainFromDB.IgnoreStunRate);
                command.Parameters.AddWithValue("@reflection_rate", CardCaptainFromDB.ReflectionRate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", CardCaptainFromDB.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@reflection_damage_rate", CardCaptainFromDB.ReflectionDamageRate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", CardCaptainFromDB.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@mana", CardCaptainFromDB.Mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", CardCaptainFromDB.ManaRegenerationRate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", CardCaptainFromDB.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", CardCaptainFromDB.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", CardCaptainFromDB.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", CardCaptainFromDB.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@normal_damage_rate", CardCaptainFromDB.NormalDamageRate);
                command.Parameters.AddWithValue("@normal_resistance_rate", CardCaptainFromDB.NormalResistanceRate);
                command.Parameters.AddWithValue("@skill_damage_rate", CardCaptainFromDB.SkillDamageRate);
                command.Parameters.AddWithValue("@skill_resistance_rate", CardCaptainFromDB.SkillResistanceRate);
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
    public CardCaptains SumPowerCardCaptainsGallery()
    {
        CardCaptains sumCardCaptains = new CardCaptains();
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
            FROM card_captains_gallery 
            WHERE user_id = @user_id AND status = 'available';";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumCardCaptains.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumCardCaptains.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumCardCaptains.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumCardCaptains.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumCardCaptains.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumCardCaptains.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumCardCaptains.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumCardCaptains.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumCardCaptains.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumCardCaptains.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumCardCaptains.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumCardCaptains.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumCardCaptains.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumCardCaptains.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                        sumCardCaptains.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumCardCaptains.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                        sumCardCaptains.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                        sumCardCaptains.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                        sumCardCaptains.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                        sumCardCaptains.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                        sumCardCaptains.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                        sumCardCaptains.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                        sumCardCaptains.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                        sumCardCaptains.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                        sumCardCaptains.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                        sumCardCaptains.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                        sumCardCaptains.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                        sumCardCaptains.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                        sumCardCaptains.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                        sumCardCaptains.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                        sumCardCaptains.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                        sumCardCaptains.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                        sumCardCaptains.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                        sumCardCaptains.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                        sumCardCaptains.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                        sumCardCaptains.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                        sumCardCaptains.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                        sumCardCaptains.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                        sumCardCaptains.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                        sumCardCaptains.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                        sumCardCaptains.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                        sumCardCaptains.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                        sumCardCaptains.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                        sumCardCaptains.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                        sumCardCaptains.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                        sumCardCaptains.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                        sumCardCaptains.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                        sumCardCaptains.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                        sumCardCaptains.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                        sumCardCaptains.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
                        sumCardCaptains.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDouble("total_percent_all_health");
                        sumCardCaptains.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDouble("total_percent_all_physical_attack");
                        sumCardCaptains.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDouble("total_percent_all_physical_defense");
                        sumCardCaptains.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDouble("total_percent_all_magical_attack");
                        sumCardCaptains.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDouble("total_percent_all_magical_defense");
                        sumCardCaptains.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDouble("total_percent_all_chemical_attack");
                        sumCardCaptains.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDouble("total_percent_all_chemical_defense");
                        sumCardCaptains.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDouble("total_percent_all_atomic_attack");
                        sumCardCaptains.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDouble("total_percent_all_atomic_defense");
                        sumCardCaptains.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDouble("total_percent_all_mental_attack");
                        sumCardCaptains.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDouble("total_percent_all_mental_defense");
                    }
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return sumCardCaptains;
    }
}