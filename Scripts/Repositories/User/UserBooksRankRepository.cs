using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
public class UserBooksRankRepository : IUserBooksRankRepository
{
    public Rank GetBooksRank(string type, string card_id)
    {
        Rank rank = new Rank();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select *
                from user_books_rank
                where user_id = @user_id AND rank_type = @type AND user_book_id = @card_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@card_id", card_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // rank.id = reader.GetString("id");
                    rank.type = reader.GetString("rank_type");
                    rank.level = reader.GetInt32("rank_level");
                    rank.power = reader.GetDouble("power");
                    rank.health = reader.GetDouble("health");
                    rank.physical_attack = reader.GetDouble("physical_attack");
                    rank.physical_defense = reader.GetDouble("physical_defense");
                    rank.magical_attack = reader.GetDouble("magical_attack");
                    rank.magical_defense = reader.GetDouble("magical_defense");
                    rank.chemical_attack = reader.GetDouble("chemical_attack");
                    rank.chemical_defense = reader.GetDouble("chemical_defense");
                    rank.atomic_attack = reader.GetDouble("atomic_attack");
                    rank.atomic_defense = reader.GetDouble("atomic_defense");
                    rank.mental_attack = reader.GetDouble("mental_attack");
                    rank.mental_defense = reader.GetDouble("mental_defense");
                    rank.speed = reader.GetDouble("speed");
                    rank.critical_damage_rate = reader.GetDouble("critical_damage_rate");
                    rank.critical_rate = reader.GetDouble("critical_rate");
                    rank.penetration_rate = reader.GetDouble("penetration_rate");
                    rank.evasion_rate = reader.GetDouble("evasion_rate");
                    rank.damage_absorption_rate = reader.GetDouble("damage_absorption_rate");
                    rank.vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate");
                    rank.accuracy_rate = reader.GetDouble("accuracy_rate");
                    rank.lifesteal_rate = reader.GetDouble("lifesteal_rate");
                    rank.shield_strength = reader.GetDouble("shield_strength");
                    rank.tenacity = reader.GetDouble("tenacity");
                    rank.resistance_rate = reader.GetDouble("resistance_rate");
                    rank.combo_rate = reader.GetDouble("combo_rate");
                    rank.reflection_rate = reader.GetDouble("reflection_rate");
                    rank.mana = reader.GetFloat("mana");
                    rank.mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate");
                    rank.damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate");
                    rank.resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate");
                    rank.damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate");
                    rank.resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate");
                    rank.percent_all_health = reader.GetDouble("percent_all_health");
                    rank.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    rank.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    rank.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    rank.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    rank.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    rank.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    rank.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    rank.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    rank.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    rank.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return rank;
    }
    public void InsertOrUpdateBooksRank(Rank rank, string type, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string checkQuery = @"
                SELECT COUNT(*) FROM user_books_rank 
                WHERE user_id = @user_id AND user_book_id = @card_id AND rank_type = @rank_type";

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCmd.Parameters.AddWithValue("@card_id", card_id);
                    checkCmd.Parameters.AddWithValue("@rank_type", type);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Nếu tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_books_rank
                        SET rank_level = @rank_level, power = @power, health = @health, mana = @mana, speed = @speed,  
                            physical_attack = @physical_attack, physical_defense = @physical_defense,  
                            magical_attack = @magical_attack, magical_defense = @magical_defense,  
                            chemical_attack = @chemical_attack, chemical_defense = @chemical_defense,  
                            atomic_attack = @atomic_attack, atomic_defense = @atomic_defense,  
                            mental_attack = @mental_attack, mental_defense = @mental_defense,  
                            critical_damage_rate = @critical_damage_rate, critical_rate = @critical_rate,  
                            penetration_rate = @penetration_rate, evasion_rate = @evasion_rate,  
                            damage_absorption_rate = @damage_absorption_rate, vitality_regeneration_rate = @vitality_regeneration_rate,  
                            accuracy_rate = @accuracy_rate, lifesteal_rate = @lifesteal_rate,  
                            shield_strength = @shield_strength, tenacity = @tenacity, resistance_rate = @resistance_rate,  
                            combo_rate = @combo_rate, reflection_rate = @reflection_rate,  
                            mana_regeneration_rate = @mana_regeneration_rate,  
                            damage_to_different_faction_rate = @damage_to_different_faction_rate,  
                            resistance_to_different_faction_rate = @resistance_to_different_faction_rate,  
                            damage_to_same_faction_rate = @damage_to_same_faction_rate,  
                            resistance_to_same_faction_rate = @resistance_to_same_faction_rate,  
                            percent_all_health = @percent_all_health, percent_all_physical_attack = @percent_all_physical_attack,  
                            percent_all_physical_defense = @percent_all_physical_defense, percent_all_magical_attack = @percent_all_magical_attack,  
                            percent_all_magical_defense = @percent_all_magical_defense, percent_all_chemical_attack = @percent_all_chemical_attack,  
                            percent_all_chemical_defense = @percent_all_chemical_defense, percent_all_atomic_attack = @percent_all_atomic_attack,  
                            percent_all_atomic_defense = @percent_all_atomic_defense, percent_all_mental_attack = @percent_all_mental_attack,  
                            percent_all_mental_defense = @percent_all_mental_defense
                        WHERE user_id = @user_id AND user_book_id = @card_id AND rank_type = @rank_type;
                        ";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCmd.Parameters.AddWithValue("@card_id", card_id);
                            updateCmd.Parameters.AddWithValue("@rank_type", type);
                            updateCmd.Parameters.AddWithValue("@rank_level", rank.level);
                            updateCmd.Parameters.AddWithValue("@power", rank.power);
                            updateCmd.Parameters.AddWithValue("@health", rank.health);
                            updateCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            updateCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            updateCmd.Parameters.AddWithValue("@magical_attack", rank.magical_attack);
                            updateCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            updateCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            updateCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            updateCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            updateCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            updateCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            updateCmd.Parameters.AddWithValue("@speed", rank.speed);
                            updateCmd.Parameters.AddWithValue("@critical_damage_rate", rank.critical_damage_rate);
                            updateCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            updateCmd.Parameters.AddWithValue("@penetration_rate", rank.penetration_rate);
                            updateCmd.Parameters.AddWithValue("@evasion_rate", rank.evasion_rate);
                            updateCmd.Parameters.AddWithValue("@damage_absorption_rate", rank.damage_absorption_rate);
                            updateCmd.Parameters.AddWithValue("@vitality_regeneration_rate", rank.vitality_regeneration_rate);
                            updateCmd.Parameters.AddWithValue("@accuracy_rate", rank.accuracy_rate);
                            updateCmd.Parameters.AddWithValue("@lifesteal_rate", rank.lifesteal_rate);
                            updateCmd.Parameters.AddWithValue("@shield_strength", rank.shield_strength);
                            updateCmd.Parameters.AddWithValue("@tenacity", rank.tenacity);
                            updateCmd.Parameters.AddWithValue("@resistance_rate", rank.resistance_rate);
                            updateCmd.Parameters.AddWithValue("@combo_rate", rank.combo_rate);
                            updateCmd.Parameters.AddWithValue("@reflection_rate", rank.reflection_rate);
                            updateCmd.Parameters.AddWithValue("@mana", rank.mana);
                            updateCmd.Parameters.AddWithValue("@mana_regeneration_rate", rank.mana_regeneration_rate);
                            updateCmd.Parameters.AddWithValue("@damage_to_different_faction_rate", rank.damage_to_different_faction_rate);
                            updateCmd.Parameters.AddWithValue("@resistance_to_different_faction_rate", rank.resistance_to_different_faction_rate);
                            updateCmd.Parameters.AddWithValue("@damage_to_same_faction_rate", rank.damage_to_same_faction_rate);
                            updateCmd.Parameters.AddWithValue("@resistance_to_same_faction_rate", rank.resistance_to_same_faction_rate);
                            updateCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertQuery = @"
                        INSERT INTO user_books_rank 
                        (user_id, user_book_id, rank_type, rank_level, power, health, mana, speed, 
                        physical_attack, physical_defense, magical_attack, magical_defense, 
                        chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                        mental_attack, mental_defense, 
                        critical_damage_rate, critical_rate, penetration_rate, evasion_rate, 
                        damage_absorption_rate, vitality_regeneration_rate, accuracy_rate, lifesteal_rate, 
                        shield_strength, tenacity, resistance_rate, combo_rate, reflection_rate, 
                        mana_regeneration_rate, damage_to_different_faction_rate, resistance_to_different_faction_rate, 
                        damage_to_same_faction_rate, resistance_to_same_faction_rate, 
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                        percent_all_mental_attack, percent_all_mental_defense) 
                        VALUES 
                        (@user_id, @card_id, @rank_type, @rank_level, @power, @health, @mana, @speed, 
                        @physical_attack, @physical_defense, @magical_attack, @magical_defense, 
                        @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, 
                        @mental_attack, @mental_defense, 
                        @critical_damage_rate, @critical_rate, @penetration_rate, @evasion_rate, 
                        @damage_absorption_rate, @vitality_regeneration_rate, @accuracy_rate, @lifesteal_rate, 
                        @shield_strength, @tenacity, @resistance_rate, @combo_rate, @reflection_rate, 
                        @mana_regeneration_rate, @damage_to_different_faction_rate, @resistance_to_different_faction_rate, 
                        @damage_to_same_faction_rate, @resistance_to_same_faction_rate, 
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, 
                        @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, 
                        @percent_all_mental_attack, @percent_all_mental_defense);
                        ";

                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                        {
                            // Thêm các tham số như trên
                            insertCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            insertCmd.Parameters.AddWithValue("@card_id", card_id);
                            insertCmd.Parameters.AddWithValue("@rank_type", type);
                            insertCmd.Parameters.AddWithValue("@rank_level", rank.level == 0 ? 1 : rank.level);
                            insertCmd.Parameters.AddWithValue("@power", rank.power);
                            insertCmd.Parameters.AddWithValue("@health", rank.health);
                            insertCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            insertCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            insertCmd.Parameters.AddWithValue("@magical_attack", rank.magical_attack);
                            insertCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            insertCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            insertCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            insertCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            insertCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            insertCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            insertCmd.Parameters.AddWithValue("@speed", rank.speed);
                            insertCmd.Parameters.AddWithValue("@critical_damage_rate", rank.critical_damage_rate);
                            insertCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            insertCmd.Parameters.AddWithValue("@penetration_rate", rank.penetration_rate);
                            insertCmd.Parameters.AddWithValue("@evasion_rate", rank.evasion_rate);
                            insertCmd.Parameters.AddWithValue("@damage_absorption_rate", rank.damage_absorption_rate);
                            insertCmd.Parameters.AddWithValue("@vitality_regeneration_rate", rank.vitality_regeneration_rate);
                            insertCmd.Parameters.AddWithValue("@accuracy_rate", rank.accuracy_rate);
                            insertCmd.Parameters.AddWithValue("@lifesteal_rate", rank.lifesteal_rate);
                            insertCmd.Parameters.AddWithValue("@shield_strength", rank.shield_strength);
                            insertCmd.Parameters.AddWithValue("@tenacity", rank.tenacity);
                            insertCmd.Parameters.AddWithValue("@resistance_rate", rank.resistance_rate);
                            insertCmd.Parameters.AddWithValue("@combo_rate", rank.combo_rate);
                            insertCmd.Parameters.AddWithValue("@reflection_rate", rank.reflection_rate);
                            insertCmd.Parameters.AddWithValue("@mana", rank.mana);
                            insertCmd.Parameters.AddWithValue("@mana_regeneration_rate", rank.mana_regeneration_rate);
                            insertCmd.Parameters.AddWithValue("@damage_to_different_faction_rate", rank.damage_to_different_faction_rate);
                            insertCmd.Parameters.AddWithValue("@resistance_to_different_faction_rate", rank.resistance_to_different_faction_rate);
                            insertCmd.Parameters.AddWithValue("@damage_to_same_faction_rate", rank.damage_to_same_faction_rate);
                            insertCmd.Parameters.AddWithValue("@resistance_to_same_faction_rate", rank.resistance_to_same_faction_rate);
                            insertCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public Rank GetSumBooksRank(string user_id, string card_id)
    {
        Rank rank = new Rank();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                SUM(power) AS power, SUM(health) AS health,
                SUM(physical_attack) AS physical_attack, SUM(physical_defense) AS physical_defense,
                SUM(magical_attack) AS magical_attack, SUM(magical_defense) AS magical_defense,
                SUM(chemical_attack) AS chemical_attack, SUM(chemical_defense) AS chemical_defense,
                SUM(atomic_attack) AS atomic_attack, SUM(atomic_defense) AS atomic_defense,
                SUM(mental_attack) AS mental_attack, SUM(mental_defense) AS mental_defense,
                SUM(speed) AS speed, SUM(critical_damage_rate) AS critical_damage_rate,
                SUM(critical_rate) AS critical_rate, SUM(penetration_rate) AS penetration_rate,
                SUM(evasion_rate) AS evasion_rate, SUM(damage_absorption_rate) AS damage_absorption_rate,
                SUM(vitality_regeneration_rate) AS vitality_regeneration_rate, SUM(accuracy_rate) AS accuracy_rate,
                SUM(lifesteal_rate) AS lifesteal_rate, SUM(shield_strength) AS shield_strength, 
                SUM(tenacity) AS tenacity, SUM(resistance_rate) AS resistance_rate,
                SUM(combo_rate) AS combo_rate, SUM(reflection_rate) AS reflection_rate,
                SUM(mana) AS mana, SUM(mana_regeneration_rate) AS mana_regeneration_rate,
                SUM(damage_to_different_faction_rate) AS damage_to_different_faction_rate, 
                SUM(resistance_to_different_faction_rate) AS resistance_to_different_faction_rate,
                SUM(damage_to_same_faction_rate) AS damage_to_same_faction_rate, 
                SUM(resistance_to_same_faction_rate) AS resistance_to_same_faction_rate,
                SUM(percent_all_health) AS percent_all_health,
                SUM(percent_all_physical_attack) AS percent_all_physical_attack, SUM(percent_all_physical_defense) AS percent_all_physical_defense,
                SUM(percent_all_magical_attack) AS percent_all_magical_attack, SUM(percent_all_magical_defense) AS percent_all_magical_defense,
                SUM(percent_all_chemical_attack) AS percent_all_chemical_attack, SUM(percent_all_chemical_defense) AS percent_all_chemical_defense,
                SUM(percent_all_atomic_attack) AS percent_all_atomic_attack, SUM(percent_all_atomic_defense) AS percent_all_atomic_defense,
                SUM(percent_all_mental_attack) AS percent_all_mental_attack, SUM(percent_all_mental_defense) AS percent_all_mental_defense
            FROM user_books_rank 
            WHERE user_id = @user_id AND user_book_id = @card_id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_id", card_id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            rank.power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                            rank.health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                            rank.physical_attack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                            rank.physical_defense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                            rank.magical_attack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                            rank.magical_defense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                            rank.chemical_attack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                            rank.chemical_defense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                            rank.atomic_attack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                            rank.atomic_defense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                            rank.mental_attack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                            rank.mental_defense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                            rank.speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                            rank.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                            rank.critical_rate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                            rank.penetration_rate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                            rank.evasion_rate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                            rank.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                            rank.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                            rank.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                            rank.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                            rank.shield_strength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                            rank.tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                            rank.resistance_rate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                            rank.combo_rate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                            rank.reflection_rate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                            rank.mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                            rank.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                            rank.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                            rank.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                            rank.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                            rank.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                            rank.percent_all_health = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDouble("percent_all_health");
                            rank.percent_all_physical_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDouble("percent_all_physical_attack");
                            rank.percent_all_physical_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDouble("percent_all_physical_defense");
                            rank.percent_all_magical_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDouble("percent_all_magical_attack");
                            rank.percent_all_magical_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDouble("percent_all_magical_defense");
                            rank.percent_all_chemical_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDouble("percent_all_chemical_attack");
                            rank.percent_all_chemical_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDouble("percent_all_chemical_defense");
                            rank.percent_all_atomic_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDouble("percent_all_atomic_attack");
                            rank.percent_all_atomic_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDouble("percent_all_atomic_defense");
                            rank.percent_all_mental_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDouble("percent_all_mental_attack");
                            rank.percent_all_mental_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDouble("percent_all_mental_defense");

                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return rank;
    }
}