using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserSpiritCardRepository : IUserSpiritCardRepository
{
    public List<SpiritCard> GetUserSpiritCard(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<SpiritCard> SpiritCardList = new List<SpiritCard>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select ut.*, t.id, t.name, t.image, t.rare, t.description from spirit_card t, user_spirit_card ut 
                where t.id=ut.spirit_card_id and ut.user_id=@userId AND t.type= @type AND (@rare = 'All' or t.rare = @rare)
                ORDER BY t.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(t.name, '[0-9]+$') AS UNSIGNED), t.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@rare", rare);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SpiritCard title = new SpiritCard
                    {
                        id = reader.GetString("spirit_card_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        quality = reader.GetInt32("quality"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
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
                        critical_resistance_rate = reader.GetDouble("critical_resistance_rate"),
                        ignore_critical_rate = reader.GetDouble("ignore_critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate"),
                        absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        ignore_combo_rate = reader.GetDouble("ignore_combo_rate"),
                        combo_damage_rate = reader.GetDouble("combo_damage_rate"),
                        combo_resistance_rate = reader.GetDouble("combo_resistance_rate"),
                        stun_rate = reader.GetDouble("stun_rate"),
                        ignore_stun_rate = reader.GetDouble("ignore_stun_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate"),
                        reflection_damage_rate = reader.GetDouble("reflection_damage_rate"),
                        reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        normal_damage_rate = reader.GetDouble("normal_damage_rate"),
                        normal_resistance_rate = reader.GetDouble("normal_resistance_rate"),
                        skill_damage_rate = reader.GetDouble("skill_damage_rate"),
                        skill_resistance_rate = reader.GetDouble("skill_resistance_rate"),
                        // percent_all_health = reader.GetDouble("percent_all_health"),
                        // percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        // percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        // percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        // percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        // percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        // percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        // percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        // percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        // percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        // percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                        description = reader.GetString("description")
                    };

                    SpiritCardList.Add(title);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return SpiritCardList;
    }
    public List<SpiritCard> GetAllUserSpiritCard(string user_id, int pageSize, int offset)
    {
        List<SpiritCard> SpiritCardList = new List<SpiritCard>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select ut.*, t.id, t.name, t.image, t.rare, t.description from spirit_card t, user_spirit_card ut 
                where t.id=ut.spirit_card_id and ut.user_id=@userId
                ORDER BY t.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(t.name, '[0-9]+$') AS UNSIGNED), t.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SpiritCard title = new SpiritCard
                    {
                        id = reader.GetString("spirit_card_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        quality = reader.GetInt32("quality"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
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
                        critical_resistance_rate = reader.GetDouble("critical_resistance_rate"),
                        ignore_critical_rate = reader.GetDouble("ignore_critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate"),
                        absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        ignore_combo_rate = reader.GetDouble("ignore_combo_rate"),
                        combo_damage_rate = reader.GetDouble("combo_damage_rate"),
                        combo_resistance_rate = reader.GetDouble("combo_resistance_rate"),
                        stun_rate = reader.GetDouble("stun_rate"),
                        ignore_stun_rate = reader.GetDouble("ignore_stun_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate"),
                        reflection_damage_rate = reader.GetDouble("reflection_damage_rate"),
                        reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        normal_damage_rate = reader.GetDouble("normal_damage_rate"),
                        normal_resistance_rate = reader.GetDouble("normal_resistance_rate"),
                        skill_damage_rate = reader.GetDouble("skill_damage_rate"),
                        skill_resistance_rate = reader.GetDouble("skill_resistance_rate"),
                        // percent_all_health = reader.GetDouble("percent_all_health"),
                        // percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        // percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        // percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        // percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        // percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        // percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        // percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        // percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        // percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        // percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                        description = reader.GetString("description")
                    };

                    SpiritCardList.Add(title);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return SpiritCardList;
    }
    public int GetUserSpiritCardCount(string user_id, string type, string rare)
    {
        int count = 0;
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select count(*) from spirit_card t, user_spirit_card ut 
                where t.id=ut.spirit_card_id and ut.user_id=@userId AND t.type= @type AND (@rare = 'All' or t.rare = @rare)";
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
        }
        return count;
    }
    public SpiritCard GetUserCardHeroesSpiritCard(string userId, CardHeroes cardHeroes)
    {
        SpiritCard spiritBeast = new SpiritCard();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select ue.*, e.*
                from spirit_card e 
                left join user_spirit_card ue 
                    on e.id = ue.spirit_card_id
                left join card_heroes_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId and che.user_card_hero_id = @user_card_hero_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    spiritBeast.id = reader.GetString("spirit_card_id");
                    spiritBeast.name = reader.GetString("name");
                    spiritBeast.image = reader.GetString("image");
                    spiritBeast.rare = reader.GetString("rare");
                    spiritBeast.type = reader.GetString("type");
                    spiritBeast.quality = reader.GetInt32("quality");
                    spiritBeast.star = reader.GetInt32("star");
                    spiritBeast.level = reader.GetInt32("level");
                    spiritBeast.experiment = reader.GetInt32("experiment");
                    spiritBeast.quantity = reader.GetInt32("quantity");
                    spiritBeast.power = reader.GetDouble("power");
                    spiritBeast.health = reader.GetDouble("health");
                    spiritBeast.physical_attack = reader.GetDouble("physical_attack");
                    spiritBeast.physical_defense = reader.GetDouble("physical_defense");
                    spiritBeast.magical_attack = reader.GetDouble("magical_attack");
                    spiritBeast.magical_defense = reader.GetDouble("magical_defense");
                    spiritBeast.chemical_attack = reader.GetDouble("chemical_attack");
                    spiritBeast.chemical_defense = reader.GetDouble("chemical_defense");
                    spiritBeast.atomic_attack = reader.GetDouble("atomic_attack");
                    spiritBeast.atomic_defense = reader.GetDouble("atomic_defense");
                    spiritBeast.mental_attack = reader.GetDouble("mental_attack");
                    spiritBeast.mental_defense = reader.GetDouble("mental_defense");
                    spiritBeast.speed = reader.GetDouble("speed");
                    spiritBeast.critical_damage_rate = reader.GetDouble("critical_damage_rate");
                    spiritBeast.critical_rate = reader.GetDouble("critical_rate");
                    spiritBeast.critical_resistance_rate = reader.GetDouble("critical_resistance_rate");
                    spiritBeast.ignore_critical_rate = reader.GetDouble("ignore_critical_rate");
                    spiritBeast.penetration_rate = reader.GetDouble("penetration_rate");
                    spiritBeast.penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate");
                    spiritBeast.evasion_rate = reader.GetDouble("evasion_rate");
                    spiritBeast.damage_absorption_rate = reader.GetDouble("damage_absorption_rate");
                    spiritBeast.ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate");
                    spiritBeast.absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate");
                    spiritBeast.vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate");
                    spiritBeast.vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate");
                    spiritBeast.accuracy_rate = reader.GetDouble("accuracy_rate");
                    spiritBeast.lifesteal_rate = reader.GetDouble("lifesteal_rate");
                    spiritBeast.shield_strength = reader.GetDouble("shield_strength");
                    spiritBeast.tenacity = reader.GetDouble("tenacity");
                    spiritBeast.resistance_rate = reader.GetDouble("resistance_rate");
                    spiritBeast.combo_rate = reader.GetDouble("combo_rate");
                    spiritBeast.ignore_combo_rate = reader.GetDouble("ignore_combo_rate");
                    spiritBeast.combo_damage_rate = reader.GetDouble("combo_damage_rate");
                    spiritBeast.combo_resistance_rate = reader.GetDouble("combo_resistance_rate");
                    spiritBeast.stun_rate = reader.GetDouble("stun_rate");
                    spiritBeast.ignore_stun_rate = reader.GetDouble("ignore_stun_rate");
                    spiritBeast.reflection_rate = reader.GetDouble("reflection_rate");
                    spiritBeast.ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate");
                    spiritBeast.reflection_damage_rate = reader.GetDouble("reflection_damage_rate");
                    spiritBeast.reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate");
                    spiritBeast.mana = reader.GetFloat("mana");
                    spiritBeast.mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate");
                    spiritBeast.damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate");
                    spiritBeast.resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate");
                    spiritBeast.damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate");
                    spiritBeast.resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate");
                    spiritBeast.normal_damage_rate = reader.GetDouble("normal_damage_rate");
                    spiritBeast.normal_resistance_rate = reader.GetDouble("normal_resistance_rate");
                    spiritBeast.skill_damage_rate = reader.GetDouble("skill_damage_rate");
                    spiritBeast.skill_resistance_rate = reader.GetDouble("skill_resistance_rate");
                    spiritBeast.percent_all_health = reader.GetDouble("percent_all_health");
                    spiritBeast.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    spiritBeast.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    spiritBeast.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    spiritBeast.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    spiritBeast.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    spiritBeast.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    spiritBeast.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    spiritBeast.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    spiritBeast.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    spiritBeast.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");

                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return spiritBeast;
    }
    public SpiritCard GetUserCardCaptainsSpiritCard(string userId, CardCaptains cardCaptains)
    {
        SpiritCard spiritBeast = new SpiritCard();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select ue.*, e.*
                from spirit_card e 
                left join user_spirit_card ue 
                    on e.id = ue.spirit_card_id
                left join card_captains_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId and che.user_card_captain_id = @user_card_captain_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    spiritBeast.id = reader.GetString("spirit_card_id");
                    spiritBeast.name = reader.GetString("name");
                    spiritBeast.image = reader.GetString("image");
                    spiritBeast.rare = reader.GetString("rare");
                    spiritBeast.type = reader.GetString("type");
                    spiritBeast.quality = reader.GetInt32("quality");
                    spiritBeast.star = reader.GetInt32("star");
                    spiritBeast.level = reader.GetInt32("level");
                    spiritBeast.experiment = reader.GetInt32("experiment");
                    spiritBeast.quantity = reader.GetInt32("quantity");
                    spiritBeast.power = reader.GetDouble("power");
                    spiritBeast.health = reader.GetDouble("health");
                    spiritBeast.physical_attack = reader.GetDouble("physical_attack");
                    spiritBeast.physical_defense = reader.GetDouble("physical_defense");
                    spiritBeast.magical_attack = reader.GetDouble("magical_attack");
                    spiritBeast.magical_defense = reader.GetDouble("magical_defense");
                    spiritBeast.chemical_attack = reader.GetDouble("chemical_attack");
                    spiritBeast.chemical_defense = reader.GetDouble("chemical_defense");
                    spiritBeast.atomic_attack = reader.GetDouble("atomic_attack");
                    spiritBeast.atomic_defense = reader.GetDouble("atomic_defense");
                    spiritBeast.mental_attack = reader.GetDouble("mental_attack");
                    spiritBeast.mental_defense = reader.GetDouble("mental_defense");
                    spiritBeast.speed = reader.GetDouble("speed");
                    spiritBeast.critical_damage_rate = reader.GetDouble("critical_damage_rate");
                    spiritBeast.critical_rate = reader.GetDouble("critical_rate");
                    spiritBeast.critical_resistance_rate = reader.GetDouble("critical_resistance_rate");
                    spiritBeast.ignore_critical_rate = reader.GetDouble("ignore_critical_rate");
                    spiritBeast.penetration_rate = reader.GetDouble("penetration_rate");
                    spiritBeast.penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate");
                    spiritBeast.evasion_rate = reader.GetDouble("evasion_rate");
                    spiritBeast.damage_absorption_rate = reader.GetDouble("damage_absorption_rate");
                    spiritBeast.ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate");
                    spiritBeast.absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate");
                    spiritBeast.vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate");
                    spiritBeast.vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate");
                    spiritBeast.accuracy_rate = reader.GetDouble("accuracy_rate");
                    spiritBeast.lifesteal_rate = reader.GetDouble("lifesteal_rate");
                    spiritBeast.shield_strength = reader.GetDouble("shield_strength");
                    spiritBeast.tenacity = reader.GetDouble("tenacity");
                    spiritBeast.resistance_rate = reader.GetDouble("resistance_rate");
                    spiritBeast.combo_rate = reader.GetDouble("combo_rate");
                    spiritBeast.ignore_combo_rate = reader.GetDouble("ignore_combo_rate");
                    spiritBeast.combo_damage_rate = reader.GetDouble("combo_damage_rate");
                    spiritBeast.combo_resistance_rate = reader.GetDouble("combo_resistance_rate");
                    spiritBeast.stun_rate = reader.GetDouble("stun_rate");
                    spiritBeast.ignore_stun_rate = reader.GetDouble("ignore_stun_rate");
                    spiritBeast.reflection_rate = reader.GetDouble("reflection_rate");
                    spiritBeast.ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate");
                    spiritBeast.reflection_damage_rate = reader.GetDouble("reflection_damage_rate");
                    spiritBeast.reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate");
                    spiritBeast.mana = reader.GetFloat("mana");
                    spiritBeast.mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate");
                    spiritBeast.damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate");
                    spiritBeast.resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate");
                    spiritBeast.damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate");
                    spiritBeast.resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate");
                    spiritBeast.normal_damage_rate = reader.GetDouble("normal_damage_rate");
                    spiritBeast.normal_resistance_rate = reader.GetDouble("normal_resistance_rate");
                    spiritBeast.skill_damage_rate = reader.GetDouble("skill_damage_rate");
                    spiritBeast.skill_resistance_rate = reader.GetDouble("skill_resistance_rate");
                    spiritBeast.percent_all_health = reader.GetDouble("percent_all_health");
                    spiritBeast.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    spiritBeast.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    spiritBeast.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    spiritBeast.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    spiritBeast.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    spiritBeast.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    spiritBeast.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    spiritBeast.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    spiritBeast.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    spiritBeast.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");

                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return spiritBeast;
    }
    public SpiritCard GetUserCardColonelsSpiritCard(string userId, CardColonels cardColonels)
    {
        SpiritCard spiritBeast = new SpiritCard();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select ue.*, e.*
                from spirit_card e 
                left join user_spirit_card ue 
                    on e.id = ue.spirit_card_id
                left join card_colonels_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId and che.user_card_colonel_id = @user_card_colonel_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    spiritBeast.id = reader.GetString("spirit_card_id");
                    spiritBeast.name = reader.GetString("name");
                    spiritBeast.image = reader.GetString("image");
                    spiritBeast.rare = reader.GetString("rare");
                    spiritBeast.type = reader.GetString("type");
                    spiritBeast.quality = reader.GetInt32("quality");
                    spiritBeast.star = reader.GetInt32("star");
                    spiritBeast.level = reader.GetInt32("level");
                    spiritBeast.experiment = reader.GetInt32("experiment");
                    spiritBeast.quantity = reader.GetInt32("quantity");
                    spiritBeast.power = reader.GetDouble("power");
                    spiritBeast.health = reader.GetDouble("health");
                    spiritBeast.physical_attack = reader.GetDouble("physical_attack");
                    spiritBeast.physical_defense = reader.GetDouble("physical_defense");
                    spiritBeast.magical_attack = reader.GetDouble("magical_attack");
                    spiritBeast.magical_defense = reader.GetDouble("magical_defense");
                    spiritBeast.chemical_attack = reader.GetDouble("chemical_attack");
                    spiritBeast.chemical_defense = reader.GetDouble("chemical_defense");
                    spiritBeast.atomic_attack = reader.GetDouble("atomic_attack");
                    spiritBeast.atomic_defense = reader.GetDouble("atomic_defense");
                    spiritBeast.mental_attack = reader.GetDouble("mental_attack");
                    spiritBeast.mental_defense = reader.GetDouble("mental_defense");
                    spiritBeast.speed = reader.GetDouble("speed");
                    spiritBeast.critical_damage_rate = reader.GetDouble("critical_damage_rate");
                    spiritBeast.critical_rate = reader.GetDouble("critical_rate");
                    spiritBeast.critical_resistance_rate = reader.GetDouble("critical_resistance_rate");
                    spiritBeast.ignore_critical_rate = reader.GetDouble("ignore_critical_rate");
                    spiritBeast.penetration_rate = reader.GetDouble("penetration_rate");
                    spiritBeast.penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate");
                    spiritBeast.evasion_rate = reader.GetDouble("evasion_rate");
                    spiritBeast.damage_absorption_rate = reader.GetDouble("damage_absorption_rate");
                    spiritBeast.ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate");
                    spiritBeast.absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate");
                    spiritBeast.vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate");
                    spiritBeast.vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate");
                    spiritBeast.accuracy_rate = reader.GetDouble("accuracy_rate");
                    spiritBeast.lifesteal_rate = reader.GetDouble("lifesteal_rate");
                    spiritBeast.shield_strength = reader.GetDouble("shield_strength");
                    spiritBeast.tenacity = reader.GetDouble("tenacity");
                    spiritBeast.resistance_rate = reader.GetDouble("resistance_rate");
                    spiritBeast.combo_rate = reader.GetDouble("combo_rate");
                    spiritBeast.ignore_combo_rate = reader.GetDouble("ignore_combo_rate");
                    spiritBeast.combo_damage_rate = reader.GetDouble("combo_damage_rate");
                    spiritBeast.combo_resistance_rate = reader.GetDouble("combo_resistance_rate");
                    spiritBeast.stun_rate = reader.GetDouble("stun_rate");
                    spiritBeast.ignore_stun_rate = reader.GetDouble("ignore_stun_rate");
                    spiritBeast.reflection_rate = reader.GetDouble("reflection_rate");
                    spiritBeast.ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate");
                    spiritBeast.reflection_damage_rate = reader.GetDouble("reflection_damage_rate");
                    spiritBeast.reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate");
                    spiritBeast.mana = reader.GetFloat("mana");
                    spiritBeast.mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate");
                    spiritBeast.damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate");
                    spiritBeast.resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate");
                    spiritBeast.damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate");
                    spiritBeast.resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate");
                    spiritBeast.normal_damage_rate = reader.GetDouble("normal_damage_rate");
                    spiritBeast.normal_resistance_rate = reader.GetDouble("normal_resistance_rate");
                    spiritBeast.skill_damage_rate = reader.GetDouble("skill_damage_rate");
                    spiritBeast.skill_resistance_rate = reader.GetDouble("skill_resistance_rate");
                    spiritBeast.percent_all_health = reader.GetDouble("percent_all_health");
                    spiritBeast.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    spiritBeast.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    spiritBeast.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    spiritBeast.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    spiritBeast.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    spiritBeast.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    spiritBeast.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    spiritBeast.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    spiritBeast.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    spiritBeast.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");

                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return spiritBeast;
    }
    public SpiritCard GetUserCardGeneralsSpiritCard(string userId, CardGenerals cardGenerals)
    {
        SpiritCard spiritBeast = new SpiritCard();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select ue.*, e.*
                from spirit_card e 
                left join user_spirit_card ue 
                    on e.id = ue.spirit_card_id
                left join card_generals_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId and che.user_card_general_id = @user_card_general_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@user_card_general_id", cardGenerals.id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    spiritBeast.id = reader.GetString("spirit_card_id");
                    spiritBeast.name = reader.GetString("name");
                    spiritBeast.image = reader.GetString("image");
                    spiritBeast.rare = reader.GetString("rare");
                    spiritBeast.type = reader.GetString("type");
                    spiritBeast.quality = reader.GetInt32("quality");
                    spiritBeast.star = reader.GetInt32("star");
                    spiritBeast.level = reader.GetInt32("level");
                    spiritBeast.experiment = reader.GetInt32("experiment");
                    spiritBeast.quantity = reader.GetInt32("quantity");
                    spiritBeast.power = reader.GetDouble("power");
                    spiritBeast.health = reader.GetDouble("health");
                    spiritBeast.physical_attack = reader.GetDouble("physical_attack");
                    spiritBeast.physical_defense = reader.GetDouble("physical_defense");
                    spiritBeast.magical_attack = reader.GetDouble("magical_attack");
                    spiritBeast.magical_defense = reader.GetDouble("magical_defense");
                    spiritBeast.chemical_attack = reader.GetDouble("chemical_attack");
                    spiritBeast.chemical_defense = reader.GetDouble("chemical_defense");
                    spiritBeast.atomic_attack = reader.GetDouble("atomic_attack");
                    spiritBeast.atomic_defense = reader.GetDouble("atomic_defense");
                    spiritBeast.mental_attack = reader.GetDouble("mental_attack");
                    spiritBeast.mental_defense = reader.GetDouble("mental_defense");
                    spiritBeast.speed = reader.GetDouble("speed");
                    spiritBeast.critical_damage_rate = reader.GetDouble("critical_damage_rate");
                    spiritBeast.critical_rate = reader.GetDouble("critical_rate");
                    spiritBeast.critical_resistance_rate = reader.GetDouble("critical_resistance_rate");
                    spiritBeast.ignore_critical_rate = reader.GetDouble("ignore_critical_rate");
                    spiritBeast.penetration_rate = reader.GetDouble("penetration_rate");
                    spiritBeast.penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate");
                    spiritBeast.evasion_rate = reader.GetDouble("evasion_rate");
                    spiritBeast.damage_absorption_rate = reader.GetDouble("damage_absorption_rate");
                    spiritBeast.ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate");
                    spiritBeast.absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate");
                    spiritBeast.vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate");
                    spiritBeast.vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate");
                    spiritBeast.accuracy_rate = reader.GetDouble("accuracy_rate");
                    spiritBeast.lifesteal_rate = reader.GetDouble("lifesteal_rate");
                    spiritBeast.shield_strength = reader.GetDouble("shield_strength");
                    spiritBeast.tenacity = reader.GetDouble("tenacity");
                    spiritBeast.resistance_rate = reader.GetDouble("resistance_rate");
                    spiritBeast.combo_rate = reader.GetDouble("combo_rate");
                    spiritBeast.ignore_combo_rate = reader.GetDouble("ignore_combo_rate");
                    spiritBeast.combo_damage_rate = reader.GetDouble("combo_damage_rate");
                    spiritBeast.combo_resistance_rate = reader.GetDouble("combo_resistance_rate");
                    spiritBeast.stun_rate = reader.GetDouble("stun_rate");
                    spiritBeast.ignore_stun_rate = reader.GetDouble("ignore_stun_rate");
                    spiritBeast.reflection_rate = reader.GetDouble("reflection_rate");
                    spiritBeast.ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate");
                    spiritBeast.reflection_damage_rate = reader.GetDouble("reflection_damage_rate");
                    spiritBeast.reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate");
                    spiritBeast.mana = reader.GetFloat("mana");
                    spiritBeast.mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate");
                    spiritBeast.damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate");
                    spiritBeast.resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate");
                    spiritBeast.damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate");
                    spiritBeast.resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate");
                    spiritBeast.normal_damage_rate = reader.GetDouble("normal_damage_rate");
                    spiritBeast.normal_resistance_rate = reader.GetDouble("normal_resistance_rate");
                    spiritBeast.skill_damage_rate = reader.GetDouble("skill_damage_rate");
                    spiritBeast.skill_resistance_rate = reader.GetDouble("skill_resistance_rate");
                    spiritBeast.percent_all_health = reader.GetDouble("percent_all_health");
                    spiritBeast.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    spiritBeast.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    spiritBeast.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    spiritBeast.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    spiritBeast.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    spiritBeast.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    spiritBeast.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    spiritBeast.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    spiritBeast.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    spiritBeast.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");

                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return spiritBeast;
    }
    public SpiritCard GetUserCardAdmiralsSpiritCard(string userId, CardAdmirals cardAdmirals)
    {
        SpiritCard spiritBeast = new SpiritCard();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select ue.*, e.*
                from spirit_card e 
                left join user_spirit_card ue 
                    on e.id = ue.spirit_card_id
                left join card_admirals_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId and che.user_card_admiral_id = @user_card_admiral_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    spiritBeast.id = reader.GetString("spirit_card_id");
                    spiritBeast.name = reader.GetString("name");
                    spiritBeast.image = reader.GetString("image");
                    spiritBeast.rare = reader.GetString("rare");
                    spiritBeast.type = reader.GetString("type");
                    spiritBeast.quality = reader.GetInt32("quality");
                    spiritBeast.star = reader.GetInt32("star");
                    spiritBeast.level = reader.GetInt32("level");
                    spiritBeast.experiment = reader.GetInt32("experiment");
                    spiritBeast.quantity = reader.GetInt32("quantity");
                    spiritBeast.power = reader.GetDouble("power");
                    spiritBeast.health = reader.GetDouble("health");
                    spiritBeast.physical_attack = reader.GetDouble("physical_attack");
                    spiritBeast.physical_defense = reader.GetDouble("physical_defense");
                    spiritBeast.magical_attack = reader.GetDouble("magical_attack");
                    spiritBeast.magical_defense = reader.GetDouble("magical_defense");
                    spiritBeast.chemical_attack = reader.GetDouble("chemical_attack");
                    spiritBeast.chemical_defense = reader.GetDouble("chemical_defense");
                    spiritBeast.atomic_attack = reader.GetDouble("atomic_attack");
                    spiritBeast.atomic_defense = reader.GetDouble("atomic_defense");
                    spiritBeast.mental_attack = reader.GetDouble("mental_attack");
                    spiritBeast.mental_defense = reader.GetDouble("mental_defense");
                    spiritBeast.speed = reader.GetDouble("speed");
                    spiritBeast.critical_damage_rate = reader.GetDouble("critical_damage_rate");
                    spiritBeast.critical_rate = reader.GetDouble("critical_rate");
                    spiritBeast.critical_resistance_rate = reader.GetDouble("critical_resistance_rate");
                    spiritBeast.ignore_critical_rate = reader.GetDouble("ignore_critical_rate");
                    spiritBeast.penetration_rate = reader.GetDouble("penetration_rate");
                    spiritBeast.penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate");
                    spiritBeast.evasion_rate = reader.GetDouble("evasion_rate");
                    spiritBeast.damage_absorption_rate = reader.GetDouble("damage_absorption_rate");
                    spiritBeast.ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate");
                    spiritBeast.absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate");
                    spiritBeast.vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate");
                    spiritBeast.vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate");
                    spiritBeast.accuracy_rate = reader.GetDouble("accuracy_rate");
                    spiritBeast.lifesteal_rate = reader.GetDouble("lifesteal_rate");
                    spiritBeast.shield_strength = reader.GetDouble("shield_strength");
                    spiritBeast.tenacity = reader.GetDouble("tenacity");
                    spiritBeast.resistance_rate = reader.GetDouble("resistance_rate");
                    spiritBeast.combo_rate = reader.GetDouble("combo_rate");
                    spiritBeast.ignore_combo_rate = reader.GetDouble("ignore_combo_rate");
                    spiritBeast.combo_damage_rate = reader.GetDouble("combo_damage_rate");
                    spiritBeast.combo_resistance_rate = reader.GetDouble("combo_resistance_rate");
                    spiritBeast.stun_rate = reader.GetDouble("stun_rate");
                    spiritBeast.ignore_stun_rate = reader.GetDouble("ignore_stun_rate");
                    spiritBeast.reflection_rate = reader.GetDouble("reflection_rate");
                    spiritBeast.ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate");
                    spiritBeast.reflection_damage_rate = reader.GetDouble("reflection_damage_rate");
                    spiritBeast.reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate");
                    spiritBeast.mana = reader.GetFloat("mana");
                    spiritBeast.mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate");
                    spiritBeast.damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate");
                    spiritBeast.resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate");
                    spiritBeast.damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate");
                    spiritBeast.resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate");
                    spiritBeast.normal_damage_rate = reader.GetDouble("normal_damage_rate");
                    spiritBeast.normal_resistance_rate = reader.GetDouble("normal_resistance_rate");
                    spiritBeast.skill_damage_rate = reader.GetDouble("skill_damage_rate");
                    spiritBeast.skill_resistance_rate = reader.GetDouble("skill_resistance_rate");
                    spiritBeast.percent_all_health = reader.GetDouble("percent_all_health");
                    spiritBeast.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    spiritBeast.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    spiritBeast.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    spiritBeast.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    spiritBeast.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    spiritBeast.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    spiritBeast.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    spiritBeast.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    spiritBeast.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    spiritBeast.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");

                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return spiritBeast;
    }
    public SpiritCard GetUserCardMilitarySpiritCard(string userId, CardMilitary cardMilitary)
    {
        SpiritCard spiritBeast = new SpiritCard();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select ue.*, e.*
                from spirit_card e 
                left join user_spirit_card ue 
                    on e.id = ue.spirit_card_id
                left join card_military_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId and che.user_card_military_id = @user_card_military_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@user_card_military_id", cardMilitary.id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    spiritBeast.id = reader.GetString("spirit_card_id");
                    spiritBeast.name = reader.GetString("name");
                    spiritBeast.image = reader.GetString("image");
                    spiritBeast.rare = reader.GetString("rare");
                    spiritBeast.type = reader.GetString("type");
                    spiritBeast.quality = reader.GetInt32("quality");
                    spiritBeast.star = reader.GetInt32("star");
                    spiritBeast.level = reader.GetInt32("level");
                    spiritBeast.experiment = reader.GetInt32("experiment");
                    spiritBeast.quantity = reader.GetInt32("quantity");
                    spiritBeast.power = reader.GetDouble("power");
                    spiritBeast.health = reader.GetDouble("health");
                    spiritBeast.physical_attack = reader.GetDouble("physical_attack");
                    spiritBeast.physical_defense = reader.GetDouble("physical_defense");
                    spiritBeast.magical_attack = reader.GetDouble("magical_attack");
                    spiritBeast.magical_defense = reader.GetDouble("magical_defense");
                    spiritBeast.chemical_attack = reader.GetDouble("chemical_attack");
                    spiritBeast.chemical_defense = reader.GetDouble("chemical_defense");
                    spiritBeast.atomic_attack = reader.GetDouble("atomic_attack");
                    spiritBeast.atomic_defense = reader.GetDouble("atomic_defense");
                    spiritBeast.mental_attack = reader.GetDouble("mental_attack");
                    spiritBeast.mental_defense = reader.GetDouble("mental_defense");
                    spiritBeast.speed = reader.GetDouble("speed");
                    spiritBeast.critical_damage_rate = reader.GetDouble("critical_damage_rate");
                    spiritBeast.critical_rate = reader.GetDouble("critical_rate");
                    spiritBeast.critical_resistance_rate = reader.GetDouble("critical_resistance_rate");
                    spiritBeast.ignore_critical_rate = reader.GetDouble("ignore_critical_rate");
                    spiritBeast.penetration_rate = reader.GetDouble("penetration_rate");
                    spiritBeast.penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate");
                    spiritBeast.evasion_rate = reader.GetDouble("evasion_rate");
                    spiritBeast.damage_absorption_rate = reader.GetDouble("damage_absorption_rate");
                    spiritBeast.ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate");
                    spiritBeast.absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate");
                    spiritBeast.vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate");
                    spiritBeast.vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate");
                    spiritBeast.accuracy_rate = reader.GetDouble("accuracy_rate");
                    spiritBeast.lifesteal_rate = reader.GetDouble("lifesteal_rate");
                    spiritBeast.shield_strength = reader.GetDouble("shield_strength");
                    spiritBeast.tenacity = reader.GetDouble("tenacity");
                    spiritBeast.resistance_rate = reader.GetDouble("resistance_rate");
                    spiritBeast.combo_rate = reader.GetDouble("combo_rate");
                    spiritBeast.ignore_combo_rate = reader.GetDouble("ignore_combo_rate");
                    spiritBeast.combo_damage_rate = reader.GetDouble("combo_damage_rate");
                    spiritBeast.combo_resistance_rate = reader.GetDouble("combo_resistance_rate");
                    spiritBeast.stun_rate = reader.GetDouble("stun_rate");
                    spiritBeast.ignore_stun_rate = reader.GetDouble("ignore_stun_rate");
                    spiritBeast.reflection_rate = reader.GetDouble("reflection_rate");
                    spiritBeast.ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate");
                    spiritBeast.reflection_damage_rate = reader.GetDouble("reflection_damage_rate");
                    spiritBeast.reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate");
                    spiritBeast.mana = reader.GetFloat("mana");
                    spiritBeast.mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate");
                    spiritBeast.damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate");
                    spiritBeast.resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate");
                    spiritBeast.damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate");
                    spiritBeast.resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate");
                    spiritBeast.normal_damage_rate = reader.GetDouble("normal_damage_rate");
                    spiritBeast.normal_resistance_rate = reader.GetDouble("normal_resistance_rate");
                    spiritBeast.skill_damage_rate = reader.GetDouble("skill_damage_rate");
                    spiritBeast.skill_resistance_rate = reader.GetDouble("skill_resistance_rate");
                    spiritBeast.percent_all_health = reader.GetDouble("percent_all_health");
                    spiritBeast.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    spiritBeast.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    spiritBeast.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    spiritBeast.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    spiritBeast.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    spiritBeast.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    spiritBeast.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    spiritBeast.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    spiritBeast.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    spiritBeast.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");

                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return spiritBeast;
    }
    public SpiritCard GetUserCardMonstersSpiritCard(string userId, CardMonsters cardMonsters)
    {
        SpiritCard spiritBeast = new SpiritCard();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select ue.*, e.*
                from spirit_card e 
                left join user_spirit_card ue 
                    on e.id = ue.spirit_card_id
                left join card_monsters_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId and che.user_card_monster_id = @user_card_monster_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    spiritBeast.id = reader.GetString("spirit_card_id");
                    spiritBeast.name = reader.GetString("name");
                    spiritBeast.image = reader.GetString("image");
                    spiritBeast.rare = reader.GetString("rare");
                    spiritBeast.type = reader.GetString("type");
                    spiritBeast.quality = reader.GetInt32("quality");
                    spiritBeast.star = reader.GetInt32("star");
                    spiritBeast.level = reader.GetInt32("level");
                    spiritBeast.experiment = reader.GetInt32("experiment");
                    spiritBeast.quantity = reader.GetInt32("quantity");
                    spiritBeast.power = reader.GetDouble("power");
                    spiritBeast.health = reader.GetDouble("health");
                    spiritBeast.physical_attack = reader.GetDouble("physical_attack");
                    spiritBeast.physical_defense = reader.GetDouble("physical_defense");
                    spiritBeast.magical_attack = reader.GetDouble("magical_attack");
                    spiritBeast.magical_defense = reader.GetDouble("magical_defense");
                    spiritBeast.chemical_attack = reader.GetDouble("chemical_attack");
                    spiritBeast.chemical_defense = reader.GetDouble("chemical_defense");
                    spiritBeast.atomic_attack = reader.GetDouble("atomic_attack");
                    spiritBeast.atomic_defense = reader.GetDouble("atomic_defense");
                    spiritBeast.mental_attack = reader.GetDouble("mental_attack");
                    spiritBeast.mental_defense = reader.GetDouble("mental_defense");
                    spiritBeast.speed = reader.GetDouble("speed");
                    spiritBeast.critical_damage_rate = reader.GetDouble("critical_damage_rate");
                    spiritBeast.critical_rate = reader.GetDouble("critical_rate");
                    spiritBeast.critical_resistance_rate = reader.GetDouble("critical_resistance_rate");
                    spiritBeast.ignore_critical_rate = reader.GetDouble("ignore_critical_rate");
                    spiritBeast.penetration_rate = reader.GetDouble("penetration_rate");
                    spiritBeast.penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate");
                    spiritBeast.evasion_rate = reader.GetDouble("evasion_rate");
                    spiritBeast.damage_absorption_rate = reader.GetDouble("damage_absorption_rate");
                    spiritBeast.ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate");
                    spiritBeast.absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate");
                    spiritBeast.vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate");
                    spiritBeast.vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate");
                    spiritBeast.accuracy_rate = reader.GetDouble("accuracy_rate");
                    spiritBeast.lifesteal_rate = reader.GetDouble("lifesteal_rate");
                    spiritBeast.shield_strength = reader.GetDouble("shield_strength");
                    spiritBeast.tenacity = reader.GetDouble("tenacity");
                    spiritBeast.resistance_rate = reader.GetDouble("resistance_rate");
                    spiritBeast.combo_rate = reader.GetDouble("combo_rate");
                    spiritBeast.ignore_combo_rate = reader.GetDouble("ignore_combo_rate");
                    spiritBeast.combo_damage_rate = reader.GetDouble("combo_damage_rate");
                    spiritBeast.combo_resistance_rate = reader.GetDouble("combo_resistance_rate");
                    spiritBeast.stun_rate = reader.GetDouble("stun_rate");
                    spiritBeast.ignore_stun_rate = reader.GetDouble("ignore_stun_rate");
                    spiritBeast.reflection_rate = reader.GetDouble("reflection_rate");
                    spiritBeast.ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate");
                    spiritBeast.reflection_damage_rate = reader.GetDouble("reflection_damage_rate");
                    spiritBeast.reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate");
                    spiritBeast.mana = reader.GetFloat("mana");
                    spiritBeast.mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate");
                    spiritBeast.damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate");
                    spiritBeast.resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate");
                    spiritBeast.damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate");
                    spiritBeast.resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate");
                    spiritBeast.normal_damage_rate = reader.GetDouble("normal_damage_rate");
                    spiritBeast.normal_resistance_rate = reader.GetDouble("normal_resistance_rate");
                    spiritBeast.skill_damage_rate = reader.GetDouble("skill_damage_rate");
                    spiritBeast.skill_resistance_rate = reader.GetDouble("skill_resistance_rate");
                    spiritBeast.percent_all_health = reader.GetDouble("percent_all_health");
                    spiritBeast.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    spiritBeast.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    spiritBeast.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    spiritBeast.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    spiritBeast.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    spiritBeast.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    spiritBeast.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    spiritBeast.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    spiritBeast.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    spiritBeast.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");

                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return spiritBeast;
    }
    public SpiritCard GetUserCardSpellSpiritCard(string userId, CardSpell cardSpell)
    {
        SpiritCard spiritBeast = new SpiritCard();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select ue.*, e.*
                from spirit_card e 
                left join user_spirit_card ue 
                    on e.id = ue.spirit_card_id
                left join card_spell_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId and che.user_card_spell_id = @user_card_spell_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@user_card_spell_id", cardSpell.id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    spiritBeast.id = reader.GetString("spirit_card_id");
                    spiritBeast.name = reader.GetString("name");
                    spiritBeast.image = reader.GetString("image");
                    spiritBeast.rare = reader.GetString("rare");
                    spiritBeast.type = reader.GetString("type");
                    spiritBeast.quality = reader.GetInt32("quality");
                    spiritBeast.star = reader.GetInt32("star");
                    spiritBeast.level = reader.GetInt32("level");
                    spiritBeast.experiment = reader.GetInt32("experiment");
                    spiritBeast.quantity = reader.GetInt32("quantity");
                    spiritBeast.power = reader.GetDouble("power");
                    spiritBeast.health = reader.GetDouble("health");
                    spiritBeast.physical_attack = reader.GetDouble("physical_attack");
                    spiritBeast.physical_defense = reader.GetDouble("physical_defense");
                    spiritBeast.magical_attack = reader.GetDouble("magical_attack");
                    spiritBeast.magical_defense = reader.GetDouble("magical_defense");
                    spiritBeast.chemical_attack = reader.GetDouble("chemical_attack");
                    spiritBeast.chemical_defense = reader.GetDouble("chemical_defense");
                    spiritBeast.atomic_attack = reader.GetDouble("atomic_attack");
                    spiritBeast.atomic_defense = reader.GetDouble("atomic_defense");
                    spiritBeast.mental_attack = reader.GetDouble("mental_attack");
                    spiritBeast.mental_defense = reader.GetDouble("mental_defense");
                    spiritBeast.speed = reader.GetDouble("speed");
                    spiritBeast.critical_damage_rate = reader.GetDouble("critical_damage_rate");
                    spiritBeast.critical_rate = reader.GetDouble("critical_rate");
                    spiritBeast.critical_resistance_rate = reader.GetDouble("critical_resistance_rate");
                    spiritBeast.ignore_critical_rate = reader.GetDouble("ignore_critical_rate");
                    spiritBeast.penetration_rate = reader.GetDouble("penetration_rate");
                    spiritBeast.penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate");
                    spiritBeast.evasion_rate = reader.GetDouble("evasion_rate");
                    spiritBeast.damage_absorption_rate = reader.GetDouble("damage_absorption_rate");
                    spiritBeast.ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate");
                    spiritBeast.absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate");
                    spiritBeast.vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate");
                    spiritBeast.vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate");
                    spiritBeast.accuracy_rate = reader.GetDouble("accuracy_rate");
                    spiritBeast.lifesteal_rate = reader.GetDouble("lifesteal_rate");
                    spiritBeast.shield_strength = reader.GetDouble("shield_strength");
                    spiritBeast.tenacity = reader.GetDouble("tenacity");
                    spiritBeast.resistance_rate = reader.GetDouble("resistance_rate");
                    spiritBeast.combo_rate = reader.GetDouble("combo_rate");
                    spiritBeast.ignore_combo_rate = reader.GetDouble("ignore_combo_rate");
                    spiritBeast.combo_damage_rate = reader.GetDouble("combo_damage_rate");
                    spiritBeast.combo_resistance_rate = reader.GetDouble("combo_resistance_rate");
                    spiritBeast.stun_rate = reader.GetDouble("stun_rate");
                    spiritBeast.ignore_stun_rate = reader.GetDouble("ignore_stun_rate");
                    spiritBeast.reflection_rate = reader.GetDouble("reflection_rate");
                    spiritBeast.ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate");
                    spiritBeast.reflection_damage_rate = reader.GetDouble("reflection_damage_rate");
                    spiritBeast.reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate");
                    spiritBeast.mana = reader.GetFloat("mana");
                    spiritBeast.mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate");
                    spiritBeast.damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate");
                    spiritBeast.resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate");
                    spiritBeast.damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate");
                    spiritBeast.resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate");
                    spiritBeast.normal_damage_rate = reader.GetDouble("normal_damage_rate");
                    spiritBeast.normal_resistance_rate = reader.GetDouble("normal_resistance_rate");
                    spiritBeast.skill_damage_rate = reader.GetDouble("skill_damage_rate");
                    spiritBeast.skill_resistance_rate = reader.GetDouble("skill_resistance_rate");
                    spiritBeast.percent_all_health = reader.GetDouble("percent_all_health");
                    spiritBeast.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    spiritBeast.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    spiritBeast.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    spiritBeast.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    spiritBeast.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    spiritBeast.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    spiritBeast.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    spiritBeast.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    spiritBeast.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    spiritBeast.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");

                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return spiritBeast;
    }
    public bool InsertUserSpiritCard(SpiritCard SpiritCard)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_spirit_card 
                WHERE user_id = @user_id AND spirit_card_id = @spirit_card_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@spirit_card_id", SpiritCard.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_spirit_card (
                    user_id, spirit_card_id, rare, level, experiment, star, quality, block, quantity,
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
                    @user_id, @spirit_card_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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
                    command.Parameters.AddWithValue("@spirit_card_id", SpiritCard.id);
                    command.Parameters.AddWithValue("@rare", SpiritCard.rare);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(SpiritCard.rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", SpiritCard.quantity);
                    command.Parameters.AddWithValue("@power", SpiritCard.power);
                    command.Parameters.AddWithValue("@health", SpiritCard.health);
                    command.Parameters.AddWithValue("@physical_attack", SpiritCard.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", SpiritCard.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", SpiritCard.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", SpiritCard.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", SpiritCard.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", SpiritCard.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", SpiritCard.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", SpiritCard.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", SpiritCard.mental_attack);
                    command.Parameters.AddWithValue("@mental_defense", SpiritCard.mental_defense);
                    command.Parameters.AddWithValue("@speed", SpiritCard.speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", SpiritCard.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", SpiritCard.critical_rate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", SpiritCard.critical_resistance_rate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", SpiritCard.ignore_critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", SpiritCard.penetration_rate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", SpiritCard.penetration_resistance_rate);
                    command.Parameters.AddWithValue("@evasion_rate", SpiritCard.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", SpiritCard.damage_absorption_rate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", SpiritCard.ignore_damage_absorption_rate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", SpiritCard.absorbed_damage_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", SpiritCard.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", SpiritCard.vitality_regeneration_resistance_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", SpiritCard.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", SpiritCard.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", SpiritCard.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", SpiritCard.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", SpiritCard.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", SpiritCard.combo_rate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", SpiritCard.ignore_combo_rate);
                    command.Parameters.AddWithValue("@combo_damage_rate", SpiritCard.combo_damage_rate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", SpiritCard.combo_resistance_rate);
                    command.Parameters.AddWithValue("@stun_rate", SpiritCard.stun_rate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", SpiritCard.ignore_stun_rate);
                    command.Parameters.AddWithValue("@reflection_rate", SpiritCard.reflection_rate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", SpiritCard.ignore_reflection_rate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", SpiritCard.reflection_damage_rate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", SpiritCard.reflection_resistance_rate);
                    command.Parameters.AddWithValue("@mana", SpiritCard.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", SpiritCard.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", SpiritCard.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", SpiritCard.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", SpiritCard.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", SpiritCard.resistance_to_same_faction_rate);
                    command.Parameters.AddWithValue("@normal_damage_rate", SpiritCard.normal_damage_rate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", SpiritCard.normal_resistance_rate);
                    command.Parameters.AddWithValue("@skill_damage_rate", SpiritCard.skill_damage_rate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", SpiritCard.skill_resistance_rate);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_spirit_card
                    SET quantity = @quantity
                    WHERE user_id = @user_id AND spirit_card_id = @spirit_card_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@spirit_card_id", SpiritCard.id);
                    updateCommand.Parameters.AddWithValue("@quantity", SpiritCard.quantity);

                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool UpdateSpiritCardLevel(SpiritCard SpiritCard, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_spirit_card
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
                WHERE user_id = @user_id AND spirit_card_id = @spirit_card_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@spirit_card_id", SpiritCard.id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", SpiritCard.power);
                command.Parameters.AddWithValue("@health", SpiritCard.health);
                command.Parameters.AddWithValue("@physical_attack", SpiritCard.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", SpiritCard.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", SpiritCard.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", SpiritCard.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", SpiritCard.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", SpiritCard.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", SpiritCard.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", SpiritCard.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", SpiritCard.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", SpiritCard.mental_defense);
                command.Parameters.AddWithValue("@speed", SpiritCard.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", SpiritCard.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", SpiritCard.critical_rate);
                command.Parameters.AddWithValue("@critical_resistance_rate", SpiritCard.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", SpiritCard.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", SpiritCard.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", SpiritCard.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", SpiritCard.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", SpiritCard.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", SpiritCard.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", SpiritCard.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", SpiritCard.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", SpiritCard.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", SpiritCard.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", SpiritCard.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", SpiritCard.shield_strength);
                command.Parameters.AddWithValue("@tenacity", SpiritCard.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", SpiritCard.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", SpiritCard.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", SpiritCard.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", SpiritCard.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", SpiritCard.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", SpiritCard.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", SpiritCard.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", SpiritCard.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", SpiritCard.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", SpiritCard.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", SpiritCard.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", SpiritCard.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", SpiritCard.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", SpiritCard.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", SpiritCard.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", SpiritCard.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", SpiritCard.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", SpiritCard.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", SpiritCard.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", SpiritCard.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", SpiritCard.skill_resistance_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
        }
        return true;
    }
    public bool UpdateSpiritCardBreakthrough(SpiritCard SpiritCard, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_spirit_card
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
                WHERE user_id = @user_id AND spirit_card_id = @spirit_card_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@spirit_card_id", SpiritCard.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", SpiritCard.power);
                command.Parameters.AddWithValue("@health", SpiritCard.health);
                command.Parameters.AddWithValue("@physical_attack", SpiritCard.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", SpiritCard.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", SpiritCard.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", SpiritCard.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", SpiritCard.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", SpiritCard.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", SpiritCard.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", SpiritCard.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", SpiritCard.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", SpiritCard.mental_defense);
                command.Parameters.AddWithValue("@speed", SpiritCard.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", SpiritCard.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", SpiritCard.critical_rate);
                command.Parameters.AddWithValue("@critical_resistance_rate", SpiritCard.critical_resistance_rate);
                command.Parameters.AddWithValue("@ignore_critical_rate", SpiritCard.ignore_critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", SpiritCard.penetration_rate);
                command.Parameters.AddWithValue("@penetration_resistance_rate", SpiritCard.penetration_resistance_rate);
                command.Parameters.AddWithValue("@evasion_rate", SpiritCard.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", SpiritCard.damage_absorption_rate);
                command.Parameters.AddWithValue("@ignore_damage_absorption_rate", SpiritCard.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@absorbed_damage_rate", SpiritCard.absorbed_damage_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", SpiritCard.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", SpiritCard.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@accuracy_rate", SpiritCard.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", SpiritCard.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", SpiritCard.shield_strength);
                command.Parameters.AddWithValue("@tenacity", SpiritCard.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", SpiritCard.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", SpiritCard.combo_rate);
                command.Parameters.AddWithValue("@ignore_combo_rate", SpiritCard.ignore_combo_rate);
                command.Parameters.AddWithValue("@combo_damage_rate", SpiritCard.combo_damage_rate);
                command.Parameters.AddWithValue("@combo_resistance_rate", SpiritCard.combo_resistance_rate);
                command.Parameters.AddWithValue("@stun_rate", SpiritCard.stun_rate);
                command.Parameters.AddWithValue("@ignore_stun_rate", SpiritCard.ignore_stun_rate);
                command.Parameters.AddWithValue("@reflection_rate", SpiritCard.reflection_rate);
                command.Parameters.AddWithValue("@ignore_reflection_rate", SpiritCard.ignore_reflection_rate);
                command.Parameters.AddWithValue("@reflection_damage_rate", SpiritCard.reflection_damage_rate);
                command.Parameters.AddWithValue("@reflection_resistance_rate", SpiritCard.reflection_resistance_rate);
                command.Parameters.AddWithValue("@mana", SpiritCard.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", SpiritCard.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", SpiritCard.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", SpiritCard.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", SpiritCard.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", SpiritCard.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@normal_damage_rate", SpiritCard.normal_damage_rate);
                command.Parameters.AddWithValue("@normal_resistance_rate", SpiritCard.normal_resistance_rate);
                command.Parameters.AddWithValue("@skill_damage_rate", SpiritCard.skill_damage_rate);
                command.Parameters.AddWithValue("@skill_resistance_rate", SpiritCard.skill_resistance_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
        }
        return true;
    }
    public bool InsertOrUpdateUserCardHeroesSpiritCard(string userId, CardHeroes cardHeroes, SpiritCard spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_heroes_spirit_card 
                WHERE user_id = @user_id AND user_card_hero_id = @user_card_hero_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                    INSERT INTO card_heroes_spirit_card (
                        user_id, user_card_hero_id, user_spirit_card_id
                    ) VALUES (
                        @user_id, @user_card_hero_id, @user_spirit_card_id
                    );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.id);
                    command.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE card_heroes_spirit_card
                    SET user_spirit_card_id = @user_spirit_card_id
                    WHERE user_id = @user_id AND user_card_hero_id = @user_card_hero_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.id);
                    updateCommand.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);

                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool InsertOrUpdateUserCardCaptainsSpiritCard(string userId, CardCaptains cardCaptains, SpiritCard spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_captains_spirit_card 
                WHERE user_id = @user_id AND user_card_captain_id = @user_card_captain_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                    INSERT INTO card_captains_spirit_card (
                        user_id, user_card_captain_id, user_spirit_card_id
                    ) VALUES (
                        @user_id, @user_card_captain_id, @user_spirit_card_id
                    );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.id);
                    command.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE card_captains_spirit_card
                    SET user_spirit_card_id = @user_spirit_card_id
                    WHERE user_id = @user_id AND user_card_captain_id = @user_card_captain_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.id);
                    updateCommand.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);

                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool InsertOrUpdateUserCardColonelsSpiritCard(string userId, CardColonels cardColonels, SpiritCard spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_colonels_spirit_card 
                WHERE user_id = @user_id AND user_card_colonel_id = @user_card_colonel_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                    INSERT INTO card_colonels_spirit_card (
                        user_id, user_card_colonel_id, user_spirit_card_id
                    ) VALUES (
                        @user_id, @user_card_colonel_id, @user_spirit_card_id
                    );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.id);
                    command.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE card_colonels_spirit_card
                    SET user_spirit_card_id = @user_spirit_card_id
                    WHERE user_id = @user_id AND user_card_colonel_id = @user_card_colonel_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.id);
                    updateCommand.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);

                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool InsertOrUpdateUserCardGeneralsSpiritCard(string userId, CardGenerals cardGenerals, SpiritCard spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_generals_spirit_card 
                WHERE user_id = @user_id AND user_card_general_id = @user_card_general_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@user_card_general_id", cardGenerals.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                    INSERT INTO card_generals_spirit_card (
                        user_id, user_card_general_id, user_spirit_card_id
                    ) VALUES (
                        @user_id, @user_card_general_id, @user_spirit_card_id
                    );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@user_card_general_id", cardGenerals.id);
                    command.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE card_generals_spirit_card
                    SET user_spirit_card_id = @user_spirit_card_id
                    WHERE user_id = @user_id AND user_card_general_id = @user_card_general_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@user_card_general_id", cardGenerals.id);
                    updateCommand.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);

                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool InsertOrUpdateUserCardAdmiralsSpiritCard(string userId, CardAdmirals cardAdmirals, SpiritCard spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_admirals_spirit_card 
                WHERE user_id = @user_id AND user_card_admiral_id = @user_card_admiral_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                    INSERT INTO card_admirals_spirit_card (
                        user_id, user_card_admiral_id, user_spirit_card_id
                    ) VALUES (
                        @user_id, @user_card_admiral_id, @user_spirit_card_id
                    );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@user_card_hero_id", cardAdmirals.id);
                    command.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE card_admirals_spirit_card
                    SET user_spirit_card_id = @user_spirit_card_id
                    WHERE user_id = @user_id AND user_card_admiral_id = @user_card_admiral_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.id);
                    updateCommand.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);

                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool InsertOrUpdateUserCardMilitarySpiritCard(string userId, CardMilitary cardMilitary, SpiritCard spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_military_spirit_card 
                WHERE user_id = @user_id AND user_card_military_id = @user_card_military_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitary.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                    INSERT INTO card_military_spirit_card (
                        user_id, user_card_military_id, user_spirit_card_id
                    ) VALUES (
                        @user_id, @user_card_military_id, @user_spirit_card_id
                    );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@user_card_military_id", cardMilitary.id);
                    command.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE card_military_spirit_card
                    SET user_spirit_card_id = @user_spirit_card_id
                    WHERE user_id = @user_id AND user_card_military_id = @user_card_military_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitary.id);
                    updateCommand.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);

                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool InsertOrUpdateUserCardMonstersSpiritCard(string userId, CardMonsters cardMonsters, SpiritCard spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_monsters_spirit_card 
                WHERE user_id = @user_id AND user_card_monster_id = @user_card_monster_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                    INSERT INTO card_monsters_spirit_card (
                        user_id, user_card_monster_id, user_spirit_card_id
                    ) VALUES (
                        @user_id, @user_card_monster_id, @user_spirit_card_id
                    );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.id);
                    command.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE card_monsters_spirit_card
                    SET user_spirit_card_id = @user_spirit_card_id
                    WHERE user_id = @user_id AND user_card_monster_id = @user_card_monster_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.id);
                    updateCommand.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);

                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool InsertOrUpdateUserCardSpellSpiritCard(string userId, CardSpell cardSpell, SpiritCard spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_spell_spirit_card 
                WHERE user_id = @user_id AND user_card_spell_id = @user_card_spell_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@user_card_spell_id", cardSpell.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                    INSERT INTO card_spell_spirit_card (
                        user_id, user_card_spell_id, user_spirit_card_id
                    ) VALUES (
                        @user_id, @user_card_spell_id, @user_spirit_card_id
                    );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@user_card_spell_id", cardSpell.id);
                    command.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE card_spell_spirit_card
                    SET user_spirit_card_id = @user_spirit_card_id
                    WHERE user_id = @user_id AND user_card_spell_id = @user_card_spell_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@user_card_spell_id", cardSpell.id);
                    updateCommand.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);

                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public List<SpiritCard> GetAllUserCardHeroesSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCard> SpiritCardList = new List<SpiritCard>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type,
                    case when che.user_spirit_card_id is null then 'NOT EQUIP' else 'EQUIP' END AS equip_status
                from spirit_card e 
                left join user_spirit_card ue 
                    on e.id = ue.spirit_card_id
                left join card_heroes_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @user_id 
                and (
                    @status = 'ALL' 
                    or (@status = 'EQUIP' and che.user_spirit_card_id is not null) 
                    or (@status = 'NOT EQUIP' and che.user_spirit_card_id is null)
                    )
                limit @limit offset @offset;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SpiritCard title = new SpiritCard
                    {
                        id = reader.GetString("spirit_card_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        quality = reader.GetInt32("quality"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
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
                        critical_resistance_rate = reader.GetDouble("critical_resistance_rate"),
                        ignore_critical_rate = reader.GetDouble("ignore_critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate"),
                        absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        ignore_combo_rate = reader.GetDouble("ignore_combo_rate"),
                        combo_damage_rate = reader.GetDouble("combo_damage_rate"),
                        combo_resistance_rate = reader.GetDouble("combo_resistance_rate"),
                        stun_rate = reader.GetDouble("stun_rate"),
                        ignore_stun_rate = reader.GetDouble("ignore_stun_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate"),
                        reflection_damage_rate = reader.GetDouble("reflection_damage_rate"),
                        reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        normal_damage_rate = reader.GetDouble("normal_damage_rate"),
                        normal_resistance_rate = reader.GetDouble("normal_resistance_rate"),
                        skill_damage_rate = reader.GetDouble("skill_damage_rate"),
                        skill_resistance_rate = reader.GetDouble("skill_resistance_rate"),
                        // percent_all_health = reader.GetDouble("percent_all_health"),
                        // percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        // percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        // percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        // percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        // percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        // percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        // percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        // percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        // percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        // percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                    };

                    SpiritCardList.Add(title);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return SpiritCardList;
    }
    public List<SpiritCard> GetAllUserCardCaptainsSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCard> SpiritCardList = new List<SpiritCard>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type,
                    case when che.user_spirit_card_id is null then 'NOT EQUIP' else 'EQUIP' END AS equip_status
                from spirit_card e 
                left join user_spirit_card ue 
                    on e.id = ue.spirit_card_id
                left join card_captains_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @user_id 
                and (
                    @status = 'ALL' 
                    or (@status = 'EQUIP' and che.user_spirit_card_id is not null) 
                    or (@status = 'NOT EQUIP' and che.user_spirit_card_id is null)
                    )
                limit @limit offset @offset;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SpiritCard title = new SpiritCard
                    {
                        id = reader.GetString("spirit_card_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        quality = reader.GetInt32("quality"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
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
                        critical_resistance_rate = reader.GetDouble("critical_resistance_rate"),
                        ignore_critical_rate = reader.GetDouble("ignore_critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate"),
                        absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        ignore_combo_rate = reader.GetDouble("ignore_combo_rate"),
                        combo_damage_rate = reader.GetDouble("combo_damage_rate"),
                        combo_resistance_rate = reader.GetDouble("combo_resistance_rate"),
                        stun_rate = reader.GetDouble("stun_rate"),
                        ignore_stun_rate = reader.GetDouble("ignore_stun_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate"),
                        reflection_damage_rate = reader.GetDouble("reflection_damage_rate"),
                        reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        normal_damage_rate = reader.GetDouble("normal_damage_rate"),
                        normal_resistance_rate = reader.GetDouble("normal_resistance_rate"),
                        skill_damage_rate = reader.GetDouble("skill_damage_rate"),
                        skill_resistance_rate = reader.GetDouble("skill_resistance_rate"),
                        // percent_all_health = reader.GetDouble("percent_all_health"),
                        // percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        // percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        // percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        // percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        // percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        // percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        // percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        // percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        // percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        // percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                    };

                    SpiritCardList.Add(title);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return SpiritCardList;
    }
    public List<SpiritCard> GetAllUserCardColonelsSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCard> SpiritCardList = new List<SpiritCard>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type,
                    case when che.user_spirit_card_id is null then 'NOT EQUIP' else 'EQUIP' END AS equip_status
                from spirit_card e 
                left join user_spirit_card ue 
                    on e.id = ue.spirit_card_id
                left join card_colonels_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @user_id 
                and (
                    @status = 'ALL' 
                    or (@status = 'EQUIP' and che.user_spirit_card_id is not null) 
                    or (@status = 'NOT EQUIP' and che.user_spirit_card_id is null)
                    )
                limit @limit offset @offset;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SpiritCard title = new SpiritCard
                    {
                        id = reader.GetString("spirit_card_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        quality = reader.GetInt32("quality"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
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
                        critical_resistance_rate = reader.GetDouble("critical_resistance_rate"),
                        ignore_critical_rate = reader.GetDouble("ignore_critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate"),
                        absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        ignore_combo_rate = reader.GetDouble("ignore_combo_rate"),
                        combo_damage_rate = reader.GetDouble("combo_damage_rate"),
                        combo_resistance_rate = reader.GetDouble("combo_resistance_rate"),
                        stun_rate = reader.GetDouble("stun_rate"),
                        ignore_stun_rate = reader.GetDouble("ignore_stun_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate"),
                        reflection_damage_rate = reader.GetDouble("reflection_damage_rate"),
                        reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        normal_damage_rate = reader.GetDouble("normal_damage_rate"),
                        normal_resistance_rate = reader.GetDouble("normal_resistance_rate"),
                        skill_damage_rate = reader.GetDouble("skill_damage_rate"),
                        skill_resistance_rate = reader.GetDouble("skill_resistance_rate"),
                        // percent_all_health = reader.GetDouble("percent_all_health"),
                        // percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        // percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        // percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        // percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        // percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        // percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        // percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        // percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        // percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        // percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                    };

                    SpiritCardList.Add(title);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return SpiritCardList;
    }
    public List<SpiritCard> GetAllUserCardGeneralsSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCard> SpiritCardList = new List<SpiritCard>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type,
                    case when che.user_spirit_card_id is null then 'NOT EQUIP' else 'EQUIP' END AS equip_status
                from spirit_card e 
                left join user_spirit_card ue 
                    on e.id = ue.spirit_card_id
                left join card_generals_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @user_id 
                and (
                    @status = 'ALL' 
                    or (@status = 'EQUIP' and che.user_spirit_card_id is not null) 
                    or (@status = 'NOT EQUIP' and che.user_spirit_card_id is null)
                    )
                limit @limit offset @offset;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SpiritCard title = new SpiritCard
                    {
                        id = reader.GetString("spirit_card_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        quality = reader.GetInt32("quality"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
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
                        critical_resistance_rate = reader.GetDouble("critical_resistance_rate"),
                        ignore_critical_rate = reader.GetDouble("ignore_critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate"),
                        absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        ignore_combo_rate = reader.GetDouble("ignore_combo_rate"),
                        combo_damage_rate = reader.GetDouble("combo_damage_rate"),
                        combo_resistance_rate = reader.GetDouble("combo_resistance_rate"),
                        stun_rate = reader.GetDouble("stun_rate"),
                        ignore_stun_rate = reader.GetDouble("ignore_stun_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate"),
                        reflection_damage_rate = reader.GetDouble("reflection_damage_rate"),
                        reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        normal_damage_rate = reader.GetDouble("normal_damage_rate"),
                        normal_resistance_rate = reader.GetDouble("normal_resistance_rate"),
                        skill_damage_rate = reader.GetDouble("skill_damage_rate"),
                        skill_resistance_rate = reader.GetDouble("skill_resistance_rate"),
                        // percent_all_health = reader.GetDouble("percent_all_health"),
                        // percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        // percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        // percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        // percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        // percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        // percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        // percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        // percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        // percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        // percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                    };

                    SpiritCardList.Add(title);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return SpiritCardList;
    }
    public List<SpiritCard> GetAllUserCardAdmiralsSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCard> SpiritCardList = new List<SpiritCard>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type,
                    case when che.user_spirit_card_id is null then 'NOT EQUIP' else 'EQUIP' END AS equip_status
                from spirit_card e 
                left join user_spirit_card ue 
                    on e.id = ue.spirit_card_id
                left join card_admirals_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @user_id 
                and (
                    @status = 'ALL' 
                    or (@status = 'EQUIP' and che.user_spirit_card_id is not null) 
                    or (@status = 'NOT EQUIP' and che.user_spirit_card_id is null)
                    )
                limit @limit offset @offset;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SpiritCard title = new SpiritCard
                    {
                        id = reader.GetString("spirit_card_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        quality = reader.GetInt32("quality"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
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
                        critical_resistance_rate = reader.GetDouble("critical_resistance_rate"),
                        ignore_critical_rate = reader.GetDouble("ignore_critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate"),
                        absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        ignore_combo_rate = reader.GetDouble("ignore_combo_rate"),
                        combo_damage_rate = reader.GetDouble("combo_damage_rate"),
                        combo_resistance_rate = reader.GetDouble("combo_resistance_rate"),
                        stun_rate = reader.GetDouble("stun_rate"),
                        ignore_stun_rate = reader.GetDouble("ignore_stun_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate"),
                        reflection_damage_rate = reader.GetDouble("reflection_damage_rate"),
                        reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        normal_damage_rate = reader.GetDouble("normal_damage_rate"),
                        normal_resistance_rate = reader.GetDouble("normal_resistance_rate"),
                        skill_damage_rate = reader.GetDouble("skill_damage_rate"),
                        skill_resistance_rate = reader.GetDouble("skill_resistance_rate"),
                        // percent_all_health = reader.GetDouble("percent_all_health"),
                        // percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        // percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        // percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        // percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        // percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        // percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        // percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        // percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        // percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        // percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                    };

                    SpiritCardList.Add(title);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return SpiritCardList;
    }
    public List<SpiritCard> GetAllUserCardMilitarySpiritCard(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCard> SpiritCardList = new List<SpiritCard>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type,
                    case when che.user_spirit_card_id is null then 'NOT EQUIP' else 'EQUIP' END AS equip_status
                from spirit_card e 
                left join user_spirit_card ue 
                    on e.id = ue.spirit_card_id
                left join card_military_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @user_id 
                and (
                    @status = 'ALL' 
                    or (@status = 'EQUIP' and che.user_spirit_card_id is not null) 
                    or (@status = 'NOT EQUIP' and che.user_spirit_card_id is null)
                    )
                limit @limit offset @offset;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SpiritCard title = new SpiritCard
                    {
                        id = reader.GetString("spirit_card_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        quality = reader.GetInt32("quality"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
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
                        critical_resistance_rate = reader.GetDouble("critical_resistance_rate"),
                        ignore_critical_rate = reader.GetDouble("ignore_critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate"),
                        absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        ignore_combo_rate = reader.GetDouble("ignore_combo_rate"),
                        combo_damage_rate = reader.GetDouble("combo_damage_rate"),
                        combo_resistance_rate = reader.GetDouble("combo_resistance_rate"),
                        stun_rate = reader.GetDouble("stun_rate"),
                        ignore_stun_rate = reader.GetDouble("ignore_stun_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate"),
                        reflection_damage_rate = reader.GetDouble("reflection_damage_rate"),
                        reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        normal_damage_rate = reader.GetDouble("normal_damage_rate"),
                        normal_resistance_rate = reader.GetDouble("normal_resistance_rate"),
                        skill_damage_rate = reader.GetDouble("skill_damage_rate"),
                        skill_resistance_rate = reader.GetDouble("skill_resistance_rate"),
                        // percent_all_health = reader.GetDouble("percent_all_health"),
                        // percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        // percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        // percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        // percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        // percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        // percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        // percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        // percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        // percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        // percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                    };

                    SpiritCardList.Add(title);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return SpiritCardList;
    }
    public List<SpiritCard> GetAllUserCardMonstersSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCard> SpiritCardList = new List<SpiritCard>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type,
                    case when che.user_spirit_card_id is null then 'NOT EQUIP' else 'EQUIP' END AS equip_status
                from spirit_card e 
                left join user_spirit_card ue 
                    on e.id = ue.spirit_card_id
                left join card_monsters_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @user_id 
                and (
                    @status = 'ALL' 
                    or (@status = 'EQUIP' and che.user_spirit_card_id is not null) 
                    or (@status = 'NOT EQUIP' and che.user_spirit_card_id is null)
                    )
                limit @limit offset @offset;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SpiritCard title = new SpiritCard
                    {
                        id = reader.GetString("spirit_card_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        quality = reader.GetInt32("quality"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
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
                        critical_resistance_rate = reader.GetDouble("critical_resistance_rate"),
                        ignore_critical_rate = reader.GetDouble("ignore_critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate"),
                        absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        ignore_combo_rate = reader.GetDouble("ignore_combo_rate"),
                        combo_damage_rate = reader.GetDouble("combo_damage_rate"),
                        combo_resistance_rate = reader.GetDouble("combo_resistance_rate"),
                        stun_rate = reader.GetDouble("stun_rate"),
                        ignore_stun_rate = reader.GetDouble("ignore_stun_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate"),
                        reflection_damage_rate = reader.GetDouble("reflection_damage_rate"),
                        reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        normal_damage_rate = reader.GetDouble("normal_damage_rate"),
                        normal_resistance_rate = reader.GetDouble("normal_resistance_rate"),
                        skill_damage_rate = reader.GetDouble("skill_damage_rate"),
                        skill_resistance_rate = reader.GetDouble("skill_resistance_rate"),
                        // percent_all_health = reader.GetDouble("percent_all_health"),
                        // percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        // percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        // percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        // percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        // percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        // percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        // percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        // percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        // percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        // percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                    };

                    SpiritCardList.Add(title);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return SpiritCardList;
    }
    public List<SpiritCard> GetAllUserCardSpellSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCard> SpiritCardList = new List<SpiritCard>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select e.name, ue.*, e.image, e.rare, e.type,
                    case when che.user_spirit_card_id is null then 'NOT EQUIP' else 'EQUIP' END AS equip_status
                from spirit_card e 
                left join user_spirit_card ue 
                    on e.id = ue.spirit_card_id
                left join card_spell_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @user_id 
                and (
                    @status = 'ALL' 
                    or (@status = 'EQUIP' and che.user_spirit_card_id is not null) 
                    or (@status = 'NOT EQUIP' and che.user_spirit_card_id is null)
                    )
                limit @limit offset @offset;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SpiritCard title = new SpiritCard
                    {
                        id = reader.GetString("spirit_card_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        quality = reader.GetInt32("quality"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
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
                        critical_resistance_rate = reader.GetDouble("critical_resistance_rate"),
                        ignore_critical_rate = reader.GetDouble("ignore_critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate"),
                        absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        ignore_combo_rate = reader.GetDouble("ignore_combo_rate"),
                        combo_damage_rate = reader.GetDouble("combo_damage_rate"),
                        combo_resistance_rate = reader.GetDouble("combo_resistance_rate"),
                        stun_rate = reader.GetDouble("stun_rate"),
                        ignore_stun_rate = reader.GetDouble("ignore_stun_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate"),
                        reflection_damage_rate = reader.GetDouble("reflection_damage_rate"),
                        reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        normal_damage_rate = reader.GetDouble("normal_damage_rate"),
                        normal_resistance_rate = reader.GetDouble("normal_resistance_rate"),
                        skill_damage_rate = reader.GetDouble("skill_damage_rate"),
                        skill_resistance_rate = reader.GetDouble("skill_resistance_rate"),
                        // percent_all_health = reader.GetDouble("percent_all_health"),
                        // percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        // percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        // percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        // percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        // percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        // percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        // percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        // percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        // percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        // percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                    };

                    SpiritCardList.Add(title);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return SpiritCardList;
    }
    public bool DeleteUserCardHeroesSpiritCard(string userId, CardHeroes cardHeroes, SpiritCard spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_heroes_spirit_card 
                WHERE user_id = @user_id AND user_card_hero_id = @user_card_hero_id AND user_spirit_card_id = @user_spirit_card_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.id);
                checkCommand.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count != 0)
                {
                    string query = @"
                    DELETE FROM card_heroes_spirit_card
                    WHERE user_id = @user_id AND user_card_hero_id = @user_card_hero_id AND user_spirit_card_id = @user_spirit_card_id;";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.id);
                    command.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                    command.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool DeleteUserCardCaptainsSpiritCard(string userId, CardCaptains cardCaptains, SpiritCard spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_captains_spirit_card 
                WHERE user_id = @user_id AND user_card_captain_id = @user_card_captain_id AND user_spirit_card_id = @user_spirit_card_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.id);
                checkCommand.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count != 0)
                {
                    string query = @"
                    DELETE FROM card_captains_spirit_card
                    WHERE user_id = @user_id AND user_card_captain_id = @user_card_captain_id AND user_spirit_card_id = @user_spirit_card_id;";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.id);
                    command.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                    command.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool DeleteUserCardColonelsSpiritCard(string userId, CardColonels cardColonels, SpiritCard spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_colonels_spirit_card 
                WHERE user_id = @user_id AND user_card_colonel_id = @user_card_colonel_id AND user_spirit_card_id = @user_spirit_card_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.id);
                checkCommand.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count != 0)
                {
                    string query = @"
                    DELETE FROM card_colonels_spirit_card
                    WHERE user_id = @user_id AND user_card_colonel_id = @user_card_colonel_id AND spiruser_spirit_card_idit_beast_id = @user_spirit_card_id;";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.id);
                    command.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                    command.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool DeleteUserCardGeneralsSpiritCard(string userId, CardGenerals cardGenerals, SpiritCard spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_generals_spirit_card 
                WHERE user_id = @user_id AND user_card_general_id = @user_card_general_id AND user_spirit_card_id = @user_spirit_card_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@user_card_general_id", cardGenerals.id);
                checkCommand.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count != 0)
                {
                    string query = @"
                    DELETE FROM card_generals_spirit_card
                    WHERE user_id = @user_id AND user_card_general_id = @user_card_general_id AND user_spirit_card_id = @user_spirit_card_id;";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@user_card_general_id", cardGenerals.id);
                    command.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                    command.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool DeleteUserCardAdmiralsSpiritCard(string userId, CardAdmirals cardAdmirals, SpiritCard spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_admirals_spirit_card 
                WHERE user_id = @user_id AND user_card_admiral_id = @user_card_admiral_id AND user_spirit_card_id = @user_spirit_card_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.id);
                checkCommand.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count != 0)
                {
                    string query = @"
                    DELETE FROM card_admirals_spirit_card
                    WHERE user_id = @user_id AND user_card_admiral_id = @user_card_admiral_id AND user_spirit_card_id = @user_spirit_card_id;";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.id);
                    command.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                    command.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool DeleteUserCardMilitarySpiritCard(string userId, CardMilitary cardMilitary, SpiritCard spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_military_spirit_card 
                WHERE user_id = @user_id AND user_card_military_id = @user_card_military_id AND user_spirit_card_id = @user_spirit_card_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitary.id);
                checkCommand.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count != 0)
                {
                    string query = @"
                    DELETE FROM card_military_spirit_card
                    WHERE user_id = @user_id AND user_card_military_id = @user_card_military_id AND user_spirit_card_id = @user_spirit_card_id;";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@user_card_military_id", cardMilitary.id);
                    command.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                    command.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool DeleteUserCardMonstersSpiritCard(string userId, CardMonsters cardMonsters, SpiritCard spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_monsters_spirit_card 
                WHERE user_id = @user_id AND user_card_monster_id = @user_card_monster_id AND user_spirit_card_id = @user_spirit_card_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.id);
                checkCommand.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count != 0)
                {
                    string query = @"
                    DELETE FROM card_monsters_spirit_card
                    WHERE user_id = @user_id AND user_card_monster_id = @user_card_monster_id AND user_spirit_card_id = @user_spirit_card_id;";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.id);
                    command.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                    command.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public bool DeleteUserCardSpellSpiritCard(string userId, CardSpell cardSpell, SpiritCard spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_spell_spirit_card 
                WHERE user_id = @user_id AND user_card_spell_id = @user_card_spell_id AND user_spirit_card_id = @user_spirit_card_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@user_card_spell_id", cardSpell.id);
                checkCommand.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count != 0)
                {
                    string query = @"
                    DELETE FROM card_spell_spirit_card
                    WHERE user_id = @user_id AND user_card_spell_id = @user_card_spell_id AND user_spirit_card_id = @user_spirit_card_id;";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@user_card_spell_id", cardSpell.id);
                    command.Parameters.AddWithValue("@user_spirit_card_id", spiritBeast.id);
                    command.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }

        }
        return true;
    }
    public SpiritCard GetUserSpiritCardById(string user_id, string Id)
    {
        SpiritCard card = new SpiritCard();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_spirit_card where user_spirit_card.spirit_card_id=@id 
                and user_spirit_card.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new SpiritCard
                    {
                        id = reader.GetString("spirit_card_id"),
                        level = reader.GetInt32("level"),
                        quality = reader.GetInt32("quality"),
                        experiment = reader.GetInt32("experiment"),
                        star = reader.GetInt32("star"),
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
                        critical_resistance_rate = reader.GetDouble("critical_resistance_rate"),
                        ignore_critical_rate = reader.GetDouble("ignore_critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate"),
                        absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        ignore_combo_rate = reader.GetDouble("ignore_combo_rate"),
                        combo_damage_rate = reader.GetDouble("combo_damage_rate"),
                        combo_resistance_rate = reader.GetDouble("combo_resistance_rate"),
                        stun_rate = reader.GetDouble("stun_rate"),
                        ignore_stun_rate = reader.GetDouble("ignore_stun_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate"),
                        reflection_damage_rate = reader.GetDouble("reflection_damage_rate"),
                        reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        normal_damage_rate = reader.GetDouble("normal_damage_rate"),
                        normal_resistance_rate = reader.GetDouble("normal_resistance_rate"),
                        skill_damage_rate = reader.GetDouble("skill_damage_rate"),
                        skill_resistance_rate = reader.GetDouble("skill_resistance_rate"),
                    };
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return card;
    }
    public SpiritCard SumPowerUserSpiritCard()
    {
        SpiritCard sumSpiritCard = new SpiritCard();
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
            FROM user_spirit_card
            WHERE user_id = @user_id;
            ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumSpiritCard.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumSpiritCard.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumSpiritCard.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumSpiritCard.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumSpiritCard.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumSpiritCard.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumSpiritCard.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumSpiritCard.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumSpiritCard.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumSpiritCard.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumSpiritCard.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumSpiritCard.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumSpiritCard.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumSpiritCard.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                        sumSpiritCard.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumSpiritCard.critical_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                        sumSpiritCard.ignore_critical_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                        sumSpiritCard.penetration_rate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                        sumSpiritCard.penetration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                        sumSpiritCard.evasion_rate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                        sumSpiritCard.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                        sumSpiritCard.ignore_damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                        sumSpiritCard.absorbed_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                        sumSpiritCard.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                        sumSpiritCard.vitality_regeneration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                        sumSpiritCard.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                        sumSpiritCard.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                        sumSpiritCard.shield_strength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                        sumSpiritCard.tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                        sumSpiritCard.resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                        sumSpiritCard.combo_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                        sumSpiritCard.ignore_combo_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                        sumSpiritCard.combo_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                        sumSpiritCard.combo_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                        sumSpiritCard.stun_rate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                        sumSpiritCard.ignore_stun_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                        sumSpiritCard.reflection_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                        sumSpiritCard.ignore_reflection_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                        sumSpiritCard.reflection_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                        sumSpiritCard.reflection_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                        sumSpiritCard.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                        sumSpiritCard.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                        sumSpiritCard.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                        sumSpiritCard.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                        sumSpiritCard.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                        sumSpiritCard.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                        sumSpiritCard.normal_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                        sumSpiritCard.normal_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                        sumSpiritCard.skill_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                        sumSpiritCard.skill_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
                    }
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return sumSpiritCard;
    }
}