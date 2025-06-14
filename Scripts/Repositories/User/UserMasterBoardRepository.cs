using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserMasterBoardRepository : IUserMasterBoardRepository
{
    public List<MasterBoard> GetUserMasterBoard(string user_id, string name)
    {
        List<MasterBoard> masterBoards = new List<MasterBoard>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select mb.*, CASE WHEN umb.name IS NULL THEN 'block' ELSE 'available' END AS status 
                from master_board mb left join user_master_board umb on mb.name = umb.name and mb.node_id = umb.node_id and umb.user_id = @userId
                where mb.name = @name";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@name", name);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MasterBoard masterBoard = new MasterBoard
                    {
                        id = reader.GetString("node_id"),
                        name = reader.GetString("name"),
                        rank_level = reader.GetString("rank_level"),
                        type = reader.GetString("type"),
                        position_x = reader.GetInt32("position_x"),
                        position_y = reader.GetInt32("position_y"),
                        status = reader.GetString("status"),
                        power = reader.GetDouble("power"),
                        health = reader.GetDouble("health"),
                        physical_attack = reader.GetDouble("physical_attack"),
                        physical_defense = reader.GetDouble("physical_defense"),
                        magical_attack = reader.GetDouble("magical_attack"),
                        magical_defense = reader.GetDouble("magical_defense"),
                        chemical_attack = reader.GetDouble("chemical_attack"),
                        chemical_defense = reader.GetDouble("chemical_defense"),
                        atomic_attack = reader.GetDouble("atomic_attack"),
                        atomic_defense = reader.GetDouble("atomic_defense"),
                        mental_attack = reader.GetDouble("mental_attack"),
                        mental_defense = reader.GetDouble("mental_defense"),
                        speed = reader.GetDouble("speed"),
                        critical_damage_rate = reader.GetDouble("critical_damage_rate"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        percent_all_health = reader.GetDouble("percent_all_health"),
                        percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                    };

                    masterBoards.Add(masterBoard);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return masterBoards;
    }
    public void InsertUserMasterBoard(string user_id, MasterBoard masterBoard)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                INSERT INTO user_master_board (
                    user_id, name, node_id, rank_level,
                    power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                    chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                    speed, critical_damage_rate, critical_rate, penetration_rate, evasion_rate, damage_absorption_rate,
                    vitality_regeneration_rate, accuracy_rate, lifesteal_rate, shield_strength, tenacity,
                    resistance_rate, combo_rate, reflection_rate, mana, mana_regeneration_rate,
                    damage_to_different_faction_rate, resistance_to_different_faction_rate,
                    damage_to_same_faction_rate, resistance_to_same_faction_rate,
                    percent_all_health, percent_all_physical_attack, percent_all_physical_defense,
                    percent_all_magical_attack, percent_all_magical_defense,
                    percent_all_chemical_attack, percent_all_chemical_defense,
                    percent_all_atomic_attack, percent_all_atomic_defense,
                    percent_all_mental_attack, percent_all_mental_defense
                )
                VALUES (
                    @user_id, @name, @node_id, @rank_level,
                    @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                    @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                    @speed, @critical_damage_rate, @critical_rate, @penetration_rate, @evasion_rate, @damage_absorption_rate,
                    @vitality_regeneration_rate, @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity,
                    @resistance_rate, @combo_rate, @reflection_rate, @mana, @mana_regeneration_rate,
                    @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                    @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                    @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense,
                    @percent_all_magical_attack, @percent_all_magical_defense,
                    @percent_all_chemical_attack, @percent_all_chemical_defense,
                    @percent_all_atomic_attack, @percent_all_atomic_defense,
                    @percent_all_mental_attack, @percent_all_mental_defense
                )";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@name", masterBoard.name);
                command.Parameters.AddWithValue("@node_id", masterBoard.id);
                command.Parameters.AddWithValue("@rank_level", masterBoard.rank_level);

                command.Parameters.AddWithValue("@power", masterBoard.power);
                command.Parameters.AddWithValue("@health", masterBoard.health);
                command.Parameters.AddWithValue("@physical_attack", masterBoard.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", masterBoard.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", masterBoard.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", masterBoard.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", masterBoard.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", masterBoard.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", masterBoard.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", masterBoard.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", masterBoard.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", masterBoard.mental_attack);
                command.Parameters.AddWithValue("@speed", masterBoard.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", masterBoard.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", masterBoard.critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", masterBoard.penetration_rate);
                command.Parameters.AddWithValue("@evasion_rate", masterBoard.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", masterBoard.damage_absorption_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", masterBoard.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@accuracy_rate", masterBoard.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", masterBoard.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", masterBoard.shield_strength);
                command.Parameters.AddWithValue("@tenacity", masterBoard.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", masterBoard.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", masterBoard.combo_rate);
                command.Parameters.AddWithValue("@reflection_rate", masterBoard.reflection_rate);
                command.Parameters.AddWithValue("@mana", masterBoard.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", masterBoard.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", masterBoard.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", masterBoard.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", masterBoard.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", masterBoard.resistance_to_same_faction_rate);

                command.Parameters.AddWithValue("@percent_all_health", masterBoard.percent_all_health);
                command.Parameters.AddWithValue("@percent_all_physical_attack", masterBoard.percent_all_physical_attack);
                command.Parameters.AddWithValue("@percent_all_physical_defense", masterBoard.percent_all_physical_defense);
                command.Parameters.AddWithValue("@percent_all_magical_attack", masterBoard.percent_all_magical_attack);
                command.Parameters.AddWithValue("@percent_all_magical_defense", masterBoard.percent_all_magical_defense);
                command.Parameters.AddWithValue("@percent_all_chemical_attack", masterBoard.percent_all_chemical_attack);
                command.Parameters.AddWithValue("@percent_all_chemical_defense", masterBoard.percent_all_chemical_defense);
                command.Parameters.AddWithValue("@percent_all_atomic_attack", masterBoard.percent_all_atomic_attack);
                command.Parameters.AddWithValue("@percent_all_atomic_defense", masterBoard.percent_all_atomic_defense);
                command.Parameters.AddWithValue("@percent_all_mental_attack", masterBoard.percent_all_mental_attack);
                command.Parameters.AddWithValue("@percent_all_mental_defense", masterBoard.percent_all_mental_defense);

                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
    }
    public void UpdateUserMasterBoard(string user_id, MasterBoard masterBoard)
    {
        int multiplier = QualityEvaluator.CheckQuality(masterBoard.rank_level);
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_master_board SET
                rank_level = @rank_level,
                power = @power, health = @health,
                physical_attack = @physical_attack, physical_defense = @physical_defense,
                magical_attack = @magical_attack, magical_defense = @magical_defense,
                chemical_attack = @chemical_attack, chemical_defense = @chemical_defense,
                atomic_attack = @atomic_attack, atomic_defense = @atomic_defense,
                mental_attack = @mental_attack, mental_defense = @mental_defense,
                speed = @speed, critical_damage_rate = @critical_damage_rate, critical_rate = @critical_rate,
                penetration_rate = @penetration_rate, evasion_rate = @evasion_rate,
                damage_absorption_rate = @damage_absorption_rate,
                vitality_regeneration_rate = @vitality_regeneration_rate,
                accuracy_rate = @accuracy_rate, lifesteal_rate = @lifesteal_rate,
                shield_strength = @shield_strength, tenacity = @tenacity,
                resistance_rate = @resistance_rate, combo_rate = @combo_rate,
                reflection_rate = @reflection_rate, mana = @mana,
                mana_regeneration_rate = @mana_regeneration_rate,
                damage_to_different_faction_rate = @damage_to_different_faction_rate,
                resistance_to_different_faction_rate = @resistance_to_different_faction_rate,
                damage_to_same_faction_rate = @damage_to_same_faction_rate,
                resistance_to_same_faction_rate = @resistance_to_same_faction_rate,
                percent_all_health = @percent_all_health,
                percent_all_physical_attack = @percent_all_physical_attack,
                percent_all_physical_defense = @percent_all_physical_defense,
                percent_all_magical_attack = @percent_all_magical_attack,
                percent_all_magical_defense = @percent_all_magical_defense,
                percent_all_chemical_attack = @percent_all_chemical_attack,
                percent_all_chemical_defense = @percent_all_chemical_defense,
                percent_all_atomic_attack = @percent_all_atomic_attack,
                percent_all_atomic_defense = @percent_all_atomic_defense,
                percent_all_mental_attack = @percent_all_mental_attack,
                percent_all_mental_defense = @percent_all_mental_defense
            WHERE
                user_id = @user_id AND name = @name AND node_id = @node_id
            ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@name", masterBoard.name);
                command.Parameters.AddWithValue("@node_id", masterBoard.id);
                command.Parameters.AddWithValue("@rank_level", masterBoard.rank_level);

                command.Parameters.AddWithValue("@power", masterBoard.power);
                command.Parameters.AddWithValue("@health", masterBoard.health * multiplier);
                command.Parameters.AddWithValue("@physical_attack", masterBoard.physical_attack * multiplier);
                command.Parameters.AddWithValue("@physical_defense", masterBoard.physical_defense * multiplier);
                command.Parameters.AddWithValue("@magical_attack", masterBoard.magical_attack * multiplier);
                command.Parameters.AddWithValue("@magical_defense", masterBoard.magical_defense * multiplier);
                command.Parameters.AddWithValue("@chemical_attack", masterBoard.chemical_attack * multiplier);
                command.Parameters.AddWithValue("@chemical_defense", masterBoard.chemical_defense * multiplier);
                command.Parameters.AddWithValue("@atomic_attack", masterBoard.atomic_attack * multiplier);
                command.Parameters.AddWithValue("@atomic_defense", masterBoard.atomic_defense * multiplier);
                command.Parameters.AddWithValue("@mental_attack", masterBoard.mental_attack * multiplier);
                command.Parameters.AddWithValue("@mental_defense", masterBoard.mental_attack * multiplier);
                command.Parameters.AddWithValue("@speed", masterBoard.speed * multiplier);
                command.Parameters.AddWithValue("@critical_damage_rate", masterBoard.critical_damage_rate * multiplier);
                command.Parameters.AddWithValue("@critical_rate", masterBoard.critical_rate * multiplier);
                command.Parameters.AddWithValue("@penetration_rate", masterBoard.penetration_rate * multiplier);
                command.Parameters.AddWithValue("@evasion_rate", masterBoard.evasion_rate * multiplier);
                command.Parameters.AddWithValue("@damage_absorption_rate", masterBoard.damage_absorption_rate * multiplier);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", masterBoard.vitality_regeneration_rate * multiplier);
                command.Parameters.AddWithValue("@accuracy_rate", masterBoard.accuracy_rate * multiplier);
                command.Parameters.AddWithValue("@lifesteal_rate", masterBoard.lifesteal_rate * multiplier);
                command.Parameters.AddWithValue("@shield_strength", masterBoard.shield_strength * multiplier);
                command.Parameters.AddWithValue("@tenacity", masterBoard.tenacity * multiplier);
                command.Parameters.AddWithValue("@resistance_rate", masterBoard.resistance_rate * multiplier);
                command.Parameters.AddWithValue("@combo_rate", masterBoard.combo_rate * multiplier);
                command.Parameters.AddWithValue("@reflection_rate", masterBoard.reflection_rate * multiplier);
                command.Parameters.AddWithValue("@mana", masterBoard.mana * multiplier);
                command.Parameters.AddWithValue("@mana_regeneration_rate", masterBoard.mana_regeneration_rate * multiplier);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", masterBoard.damage_to_different_faction_rate * multiplier);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", masterBoard.resistance_to_different_faction_rate * multiplier);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", masterBoard.damage_to_same_faction_rate * multiplier);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", masterBoard.resistance_to_same_faction_rate * multiplier);

                command.Parameters.AddWithValue("@percent_all_health", masterBoard.percent_all_health * multiplier);
                command.Parameters.AddWithValue("@percent_all_physical_attack", masterBoard.percent_all_physical_attack * multiplier);
                command.Parameters.AddWithValue("@percent_all_physical_defense", masterBoard.percent_all_physical_defense * multiplier);
                command.Parameters.AddWithValue("@percent_all_magical_attack", masterBoard.percent_all_magical_attack * multiplier);
                command.Parameters.AddWithValue("@percent_all_magical_defense", masterBoard.percent_all_magical_defense * multiplier);
                command.Parameters.AddWithValue("@percent_all_chemical_attack", masterBoard.percent_all_chemical_attack * multiplier);
                command.Parameters.AddWithValue("@percent_all_chemical_defense", masterBoard.percent_all_chemical_defense * multiplier);
                command.Parameters.AddWithValue("@percent_all_atomic_attack", masterBoard.percent_all_atomic_attack * multiplier);
                command.Parameters.AddWithValue("@percent_all_atomic_defense", masterBoard.percent_all_atomic_defense * multiplier);
                command.Parameters.AddWithValue("@percent_all_mental_attack", masterBoard.percent_all_mental_attack * multiplier);
                command.Parameters.AddWithValue("@percent_all_mental_defense", masterBoard.percent_all_mental_defense * multiplier);

                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
    }
}