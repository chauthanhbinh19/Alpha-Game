using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class CardHeroes
{
    public int id { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public string rare { get; set; }
    public string type { get; set; }
    public int star { get; set; }
    public int level { get; set; }
    public int experiment { get; set; }
    public int quantity { get; set; }
    public bool block { get; set; }
    public string position { get; set; }
    public double power { get; set; }
    public double health { get; set; }
    public double physical_attack { get; set; }
    public double physical_defense { get; set; }
    public double magical_attack { get; set; }
    public double magical_defense { get; set; }
    public double chemical_attack { get; set; }
    public double chemical_defense { get; set; }
    public double atomic_attack { get; set; }
    public double atomic_defense { get; set; }
    public double mental_attack { get; set; }
    public double mental_defense { get; set; }
    public double speed { get; set; }
    public double critical_damage { get; set; }
    public double critical_rate { get; set; }
    public double armor_penetration { get; set; }
    public double avoid { get; set; }
    public double absorbs_damage { get; set; }
    public double regenerate_vitality { get; set; }
    public double accuracy { get; set; }
    public float mana { get; set; }
    public double all_power { get; set; }
    public double all_health { get; set; }
    public double all_physical_attack { get; set; }
    public double all_physical_defense { get; set; }
    public double all_magical_attack { get; set; }
    public double all_magical_defense { get; set; }
    public double all_chemical_attack { get; set; }
    public double all_chemical_defense { get; set; }
    public double all_atomic_attack { get; set; }
    public double all_atomic_defense { get; set; }
    public double all_mental_attack { get; set; }
    public double all_mental_defense { get; set; }
    public double all_speed { get; set; }
    public double all_critical_damage { get; set; }
    public double all_critical_rate { get; set; }
    public double all_armor_penetration { get; set; }
    public double all_avoid { get; set; }
    public double all_absorbs_damage { get; set; }
    public double all_regenerate_vitality { get; set; }
    public double all_accuracy { get; set; }
    public float all_mana { get; set; }
    public string description { get; set; }
    public string status { get; set; }
    public int team_id { get; set; }
    public double percent_all_health { get; set; }
    public double percent_all_physical_attack { get; set; }
    public double percent_all_physical_defense { get; set; }
    public double percent_all_magical_attack { get; set; }
    public double percent_all_magical_defense { get; set; }
    public double percent_all_chemical_attack { get; set; }
    public double percent_all_chemical_defense { get; set; }
    public double percent_all_atomic_attack { get; set; }
    public double percent_all_atomic_defense { get; set; }
    public double percent_all_mental_attack { get; set; }
    public double percent_all_mental_defense { get; set; }
    public Currency currency { get; set; }
    public CardHeroes()
    {
        power = -1;
        health = -1;
        physical_attack = -1;
        physical_defense = -1;
        magical_attack = -1;
        magical_defense = -1;
        chemical_attack = -1;
        chemical_defense = -1;
        atomic_attack = -1;
        atomic_defense = -1;
        mental_attack = -1;
        mental_defense = -1;
        speed = -1;
        critical_damage = -1;
        critical_rate = -1;
        armor_penetration = -1;
        avoid = -1;
        absorbs_damage = -1;
        regenerate_vitality = -1;
        accuracy = -1;
        all_power = -1;
        all_health = -1;
        all_physical_attack = -1;
        all_physical_defense = -1;
        all_magical_attack = -1;
        all_magical_defense = -1;
        all_chemical_attack = -1;
        all_chemical_defense = -1;
        all_atomic_attack = -1;
        all_atomic_defense = -1;
        all_mental_attack = -1;
        all_mental_defense = -1;
        all_speed = -1;
        all_critical_damage = -1;
        all_critical_rate = -1;
        all_armor_penetration = -1;
        all_avoid = -1;
        all_absorbs_damage = -1;
        all_regenerate_vitality = -1;
        all_accuracy = -1;
        team_id = -1;
        percent_all_health = -1;
        percent_all_physical_attack = -1;
        percent_all_physical_defense = -1;
        percent_all_magical_attack = -1;
        percent_all_magical_defense = -1;
        percent_all_chemical_attack = -1;
        percent_all_chemical_defense = -1;
        percent_all_atomic_attack = -1;
        percent_all_atomic_defense = -1;
        percent_all_mental_attack = -1;
        percent_all_mental_defense = -1;
    }
    public List<CardHeroes> GetFinalPower(List<CardHeroes> CardHeroesList)
    {
        PowerManager powerManager = new PowerManager();
        powerManager = powerManager.GetUserStats();
        foreach (var c in CardHeroesList)
        {
            c.all_power = powerManager.GetFinalCardHeroesPower(c);
            c.all_health = c.all_health + powerManager.health + c.all_health * powerManager.percent_all_health / 100;
            c.all_physical_attack = c.all_physical_attack + powerManager.physical_attack + c.physical_attack * powerManager.percent_all_physical_attack / 100;
            c.all_physical_defense = c.all_physical_defense + powerManager.physical_defense + c.physical_defense * powerManager.percent_all_physical_defense / 100;
            c.all_magical_attack = c.all_magical_attack + powerManager.magical_attack + c.magical_attack * powerManager.percent_all_magical_attack / 100;
            c.all_magical_defense = c.all_magical_defense + powerManager.magical_defense + c.magical_defense * powerManager.percent_all_magical_defense / 100;
            c.all_chemical_attack = c.all_chemical_attack + powerManager.chemical_attack + c.chemical_attack * powerManager.percent_all_chemical_attack / 100;
            c.all_chemical_defense = c.all_chemical_defense + powerManager.chemical_defense + c.chemical_defense * powerManager.percent_all_chemical_defense / 100;
            c.all_atomic_attack = c.all_atomic_attack + powerManager.atomic_attack + c.atomic_attack * powerManager.percent_all_atomic_attack / 100;
            c.all_atomic_defense = c.all_atomic_defense + powerManager.atomic_defense + c.atomic_defense * powerManager.percent_all_atomic_defense / 100;
            c.all_mental_attack = c.all_mental_attack + powerManager.mental_attack + c.mental_attack * powerManager.percent_all_mental_attack / 100;
            c.all_mental_defense = c.all_mental_defense + powerManager.mental_defense + c.mental_defense * powerManager.percent_all_mental_defense / 100;
            c.all_speed = c.all_speed + powerManager.speed;
            c.all_critical_damage = c.all_critical_damage + powerManager.critical_damage;
            c.all_critical_rate = c.all_critical_rate + powerManager.critical_rate;
            c.all_armor_penetration = c.all_armor_penetration + powerManager.armor_penetration;
            c.all_avoid = c.all_avoid + powerManager.avoid;
            c.all_absorbs_damage = c.all_absorbs_damage + powerManager.absorbs_damage;
            c.all_regenerate_vitality = c.all_regenerate_vitality + powerManager.regenerate_vitality;
            c.all_accuracy = c.all_accuracy + powerManager.accuracy;
            c.all_mana = c.all_mana + powerManager.mana;
        }
        return CardHeroesList;
    }
    public CardHeroes GetNewPower(CardHeroes c, double coefficient)
    {
        CardHeroes orginCard = new CardHeroes();
        orginCard = orginCard.GetCardHeroesById(c.id);
        CardHeroes cardHeroes = new CardHeroes
        {
            id = c.id,
            health = c.health + orginCard.health * coefficient,
            physical_attack = c.physical_attack + orginCard.physical_attack * coefficient,
            physical_defense = c.physical_defense + orginCard.physical_defense * coefficient,
            magical_attack = c.magical_attack + orginCard.magical_attack * coefficient,
            magical_defense = c.magical_defense + orginCard.magical_defense * coefficient,
            chemical_attack = c.chemical_attack + orginCard.chemical_attack * coefficient,
            chemical_defense = c.chemical_defense + orginCard.chemical_defense * coefficient,
            atomic_attack = c.atomic_attack + orginCard.atomic_attack * coefficient,
            atomic_defense = c.atomic_defense + orginCard.atomic_defense * coefficient,
            mental_attack = c.mental_attack + orginCard.mental_attack * coefficient,
            mental_defense = c.mental_defense + orginCard.mental_defense * coefficient,
            speed = c.speed + orginCard.speed * coefficient,
            critical_damage = c.critical_damage + orginCard.critical_damage * coefficient,
            critical_rate = c.critical_rate + orginCard.critical_rate * coefficient,
            armor_penetration = c.armor_penetration + orginCard.armor_penetration * coefficient,
            avoid = c.avoid + orginCard.avoid * coefficient,
            absorbs_damage = c.absorbs_damage + orginCard.absorbs_damage * coefficient,
            regenerate_vitality = c.regenerate_vitality + orginCard.regenerate_vitality * coefficient,
            accuracy = c.accuracy + orginCard.accuracy * coefficient,
            mana = c.mana + orginCard.mana * (float)coefficient
        };
        cardHeroes.power = 0.5 * (
            cardHeroes.health +
            cardHeroes.physical_attack +
            cardHeroes.physical_defense +
            cardHeroes.magical_attack +
            cardHeroes.magical_defense +
            cardHeroes.chemical_attack +
            cardHeroes.chemical_defense +
            cardHeroes.atomic_attack +
            cardHeroes.atomic_defense +
            cardHeroes.mental_attack +
            cardHeroes.mental_defense +
            cardHeroes.speed +
            cardHeroes.critical_damage +
            cardHeroes.critical_rate +
            cardHeroes.armor_penetration +
            cardHeroes.avoid +
            cardHeroes.absorbs_damage +
            cardHeroes.regenerate_vitality +
            cardHeroes.accuracy +
            cardHeroes.mana
        );
        return cardHeroes;
    }
    public static List<string> GetUniqueCardHeroTypes()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "Select distinct type from card_heroes";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                typeList.Add(reader.GetString(0));
            }
        }
        return typeList;
    }
    public List<CardHeroes> GetCardHeroes(string type, int pageSize, int offset)
    {
        List<CardHeroes> CardHeroesList = new List<CardHeroes>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from card_heroes where type= @type 
                ORDER BY card_heroes.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(card_heroes.name, '[0-9]+$') AS UNSIGNED), card_heroes.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardHeroes card = new CardHeroes
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
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
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description")
                    };

                    CardHeroesList.Add(card);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardHeroesList;
    }
    public int GetCardHeroesCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from card_heroes where type= @type";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
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
    public List<CardHeroes> GetCardHeroesCollection(string type, int pageSize, int offset)
    {
        List<CardHeroes> CardHeroesList = new List<CardHeroes>();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT c.*, CASE WHEN cg.card_hero_id IS NULL THEN 'block' WHEN cg.status = 'pending' THEN 'pending' WHEN cg.status = 'available' THEN 'available' END AS status 
                FROM card_heroes c LEFT JOIN card_heroes_gallery cg ON c.id = cg.card_hero_id and cg.user_id = @userId where c.type=@type
                ORDER BY c.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardHeroes card = new CardHeroes
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
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
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description"),
                        status = reader.GetString("status"),
                    };

                    CardHeroesList.Add(card);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardHeroesList;
    }
    public List<CardHeroes> GetUserCardHeroes(string type, int pageSize, int offset)
    {
        List<CardHeroes> CardHeroesList = new List<CardHeroes>();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT uc.*, c.*, fch.*
                FROM user_card_heroes uc
                LEFT JOIN card_heroes c ON uc.card_hero_id = c.id 
                LEFT JOIN fact_card_heroes fch ON fch.user_id = uc.user_id AND fch.user_card_hero_id = uc.card_hero_id
                WHERE uc.user_id = @userId AND c.type = @type
                ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name
                LIMIT @limit OFFSET @offset;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardHeroes card = new CardHeroes
                    {
                        id = reader.GetInt32("card_hero_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
                        block = reader.GetBoolean("block"),
                        team_id = reader.IsDBNull(reader.GetOrdinal("team_id")) ? -1 : reader.GetInt32("team_id"),
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString("position"),
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
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description"),
                        all_power = reader.GetDouble("all_power"),
                        all_health = reader.GetDouble("all_health"),
                        all_physical_attack = reader.GetDouble("all_physical_attack"),
                        all_physical_defense = reader.GetDouble("all_physical_defense"),
                        all_magical_attack = reader.GetDouble("all_magical_attack"),
                        all_magical_defense = reader.GetDouble("all_magical_defense"),
                        all_chemical_attack = reader.GetDouble("all_chemical_attack"),
                        all_chemical_defense = reader.GetDouble("all_chemical_defense"),
                        all_atomic_attack = reader.GetDouble("all_atomic_attack"),
                        all_atomic_defense = reader.GetDouble("all_atomic_defense"),
                        all_mental_attack = reader.GetDouble("all_mental_attack"),
                        all_mental_defense = reader.GetDouble("all_mental_defense"),
                        all_speed = reader.GetDouble("all_speed"),
                        all_critical_damage = reader.GetDouble("all_critical_damage"),
                        all_critical_rate = reader.GetDouble("all_critical_rate"),
                        all_armor_penetration = reader.GetDouble("all_armor_penetration"),
                        all_avoid = reader.GetDouble("all_avoid"),
                        all_absorbs_damage = reader.GetDouble("all_absorbs_damage"),
                        all_regenerate_vitality = reader.GetDouble("all_regenerate_vitality"),
                        all_accuracy = reader.GetDouble("all_accuracy"),
                        all_mana = reader.GetFloat("all_mana"),
                    };

                    CardHeroesList.Add(card);
                }
                CardHeroesList = GetFinalPower(CardHeroesList);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardHeroesList;
    }
    public List<CardHeroes> GetUserCardHeroesTeam(int teamId)
    {
        List<CardHeroes> CardHeroesList = new List<CardHeroes>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT uc.*, c.*, fch.*
                FROM user_card_heroes uc
                LEFT JOIN card_heroes c ON uc.card_hero_id = c.id 
                LEFT JOIN fact_card_heroes fch ON fch.user_id = uc.user_id AND fch.user_card_hero_id = uc.card_hero_id
                WHERE uc.user_id = @userId and fch.team_id=@team_id
                ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", User.CurrentUserId);
                command.Parameters.AddWithValue("@team_id", teamId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardHeroes card = new CardHeroes
                    {
                        id = reader.GetInt32("card_hero_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
                        block = reader.GetBoolean("block"),
                        team_id = reader.IsDBNull(reader.GetOrdinal("team_id")) ? -1 : reader.GetInt32("team_id"),
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString("position"),
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
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description"),
                        all_power = reader.GetDouble("all_power"),
                        all_health = reader.GetDouble("all_health"),
                        all_physical_attack = reader.GetDouble("all_physical_attack"),
                        all_physical_defense = reader.GetDouble("all_physical_defense"),
                        all_magical_attack = reader.GetDouble("all_magical_attack"),
                        all_magical_defense = reader.GetDouble("all_magical_defense"),
                        all_chemical_attack = reader.GetDouble("all_chemical_attack"),
                        all_chemical_defense = reader.GetDouble("all_chemical_defense"),
                        all_atomic_attack = reader.GetDouble("all_atomic_attack"),
                        all_atomic_defense = reader.GetDouble("all_atomic_defense"),
                        all_mental_attack = reader.GetDouble("all_mental_attack"),
                        all_mental_defense = reader.GetDouble("all_mental_defense"),
                        all_speed = reader.GetDouble("all_speed"),
                        all_critical_damage = reader.GetDouble("all_critical_damage"),
                        all_critical_rate = reader.GetDouble("all_critical_rate"),
                        all_armor_penetration = reader.GetDouble("all_armor_penetration"),
                        all_avoid = reader.GetDouble("all_avoid"),
                        all_absorbs_damage = reader.GetDouble("all_absorbs_damage"),
                        all_regenerate_vitality = reader.GetDouble("all_regenerate_vitality"),
                        all_accuracy = reader.GetDouble("all_accuracy"),
                        all_mana = reader.GetFloat("all_mana"),
                    };

                    CardHeroesList.Add(card);
                }
                CardHeroesList = GetFinalPower(CardHeroesList);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardHeroesList;
    }
    public Dictionary<string, int> GetUniqueCardHeroTypesTeam(int teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = @"SELECT distinct c.type, count(c.type) as number
            FROM user_card_heroes uc
            LEFT JOIN card_heroes c ON uc.card_hero_id = c.id 
            LEFT JOIN fact_card_heroes fch ON fch.user_id = uc.user_id AND fch.user_card_hero_id = uc.card_hero_id
            WHERE uc.user_id =@userId and fch.team_id=@team_id
            group by c.type, c.type";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", User.CurrentUserId);
            command.Parameters.AddWithValue("@team_id", teamId);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string type = reader["type"].ToString();
                int number = Convert.ToInt32(reader["number"]);

                result[type] = number;
            }
        }
        return result;
    }
    public int GetUserCardHeroesCount(string type)
    {
        int count = 0;
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from card_heroes c, user_card_heroes uc where c.id=uc.card_hero_id and uc.user_id=@userId and type= @type";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
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
    public List<CardHeroes> GetCardHeroesRandom(string type, int pageSize)
    {
        List<CardHeroes> CardHeroesList = new List<CardHeroes>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from card_heroes where type= @type ORDER BY RAND() limit @limit";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardHeroes card = new CardHeroes
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
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
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description")
                    };

                    CardHeroesList.Add(card);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardHeroesList;
    }
    public List<CardHeroes> GetAllCardHeroes(string type)
    {
        List<CardHeroes> CardHeroesList = new List<CardHeroes>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from card_heroes where type= @type";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardHeroes card = new CardHeroes
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
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
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description")
                    };

                    CardHeroesList.Add(card);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardHeroesList;
    }
    public bool InsertUserCardHeroes(CardHeroes CardHeroes)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_heroes
                WHERE user_id = @user_id AND card_hero_id = @card_hero_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_hero_id", CardHeroes.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_card_heroes (
                    user_id, card_hero_id, level, experiment, star, block, quantity, power, health, physical_attack, 
                    physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, 
                    atomic_defense, mental_attack, mental_defense, speed, critical_damage, critical_rate, 
                    armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana
                ) VALUES (
                    @user_id, @card_hero_id, @level, @experiment, @star, @block, @quantity, @power, @health, @physical_attack, 
                    @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, 
                    @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage, @critical_rate, 
                    @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@card_hero_id", CardHeroes.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", 0);
                    command.Parameters.AddWithValue("@power", CardHeroes.power);
                    command.Parameters.AddWithValue("@health", CardHeroes.health);
                    command.Parameters.AddWithValue("@physical_attack", CardHeroes.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", CardHeroes.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", CardHeroes.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", CardHeroes.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", CardHeroes.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", CardHeroes.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", CardHeroes.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", CardHeroes.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", CardHeroes.mental_attack);
                    command.Parameters.AddWithValue("@mental_defense", CardHeroes.mental_defense);
                    command.Parameters.AddWithValue("@speed", CardHeroes.speed);
                    command.Parameters.AddWithValue("@critical_damage", CardHeroes.critical_damage);
                    command.Parameters.AddWithValue("@critical_rate", CardHeroes.critical_rate);
                    command.Parameters.AddWithValue("@armor_penetration", CardHeroes.armor_penetration);
                    command.Parameters.AddWithValue("@avoid", CardHeroes.avoid);
                    command.Parameters.AddWithValue("@absorbs_damage", CardHeroes.absorbs_damage);
                    command.Parameters.AddWithValue("@regenerate_vitality", CardHeroes.regenerate_vitality);
                    command.Parameters.AddWithValue("@accuracy", CardHeroes.accuracy);
                    command.Parameters.AddWithValue("@mana", CardHeroes.mana);
                    MySqlDataReader reader = command.ExecuteReader();
                    InsertFactCardHeroes(CardHeroes);
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_card_heroes
                    SET quantity = quantity + 1
                    WHERE user_id = @user_id AND card_hero_id = @card_hero_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@card_hero_id", CardHeroes.id);

                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return true;
    }
    public bool UpdateCardHeroesLevel(CardHeroes cardHeroes, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_heroes
                SET level = @level,
                    power = @power, health = @health, physical_attack = @physicalAttack,
                    physical_defense = @physicalDefense, magical_attack = @magicalAttack,
                    magical_defense = @magicalDefense, chemical_attack = @chemicalAttack,
                    chemical_defense = @chemicalDefense, atomic_attack = @atomicAttack,
                    atomic_defense = @atomicDefense, mental_attack = @mentalAttack,
                    mental_defense = @mentalDefense, speed = @speed, critical_damage = @criticalDamage,
                    critical_rate = @criticalRate, armor_penetration = @armorPenetration,
                    avoid = @avoid, absorbs_damage = @absorbsDamage, regenerate_vitality = @regenerateVitality, 
                    accuracy = @accuracy, mana = @mana
                WHERE 
                    user_id = @user_id AND card_hero_id = @card_hero_id;;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_hero_id", cardHeroes.id);
                command.Parameters.AddWithValue("@level", cardLevel + 1);
                command.Parameters.AddWithValue("@power", cardHeroes.power);
                command.Parameters.AddWithValue("@health", cardHeroes.health);
                command.Parameters.AddWithValue("@physicalAttack", cardHeroes.physical_attack);
                command.Parameters.AddWithValue("@physicalDefense", cardHeroes.physical_defense);
                command.Parameters.AddWithValue("@magicalAttack", cardHeroes.magical_attack);
                command.Parameters.AddWithValue("@magicalDefense", cardHeroes.magical_defense);
                command.Parameters.AddWithValue("@chemicalAttack", cardHeroes.chemical_attack);
                command.Parameters.AddWithValue("@chemicalDefense", cardHeroes.chemical_defense);
                command.Parameters.AddWithValue("@atomicAttack", cardHeroes.atomic_attack);
                command.Parameters.AddWithValue("@atomicDefense", cardHeroes.atomic_defense);
                command.Parameters.AddWithValue("@mentalAttack", cardHeroes.mental_attack);
                command.Parameters.AddWithValue("@mentalDefense", cardHeroes.mental_defense);
                command.Parameters.AddWithValue("@speed", cardHeroes.speed);
                command.Parameters.AddWithValue("@criticalDamage", cardHeroes.critical_damage);
                command.Parameters.AddWithValue("@criticalRate", cardHeroes.critical_rate);
                command.Parameters.AddWithValue("@armorPenetration", cardHeroes.armor_penetration);
                command.Parameters.AddWithValue("@avoid", cardHeroes.avoid);
                command.Parameters.AddWithValue("@absorbsDamage", cardHeroes.absorbs_damage);
                command.Parameters.AddWithValue("@regenerateVitality", cardHeroes.regenerate_vitality);
                command.Parameters.AddWithValue("@accuracy", cardHeroes.accuracy);
                command.Parameters.AddWithValue("@mana", cardHeroes.mana);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool InsertFactCardHeroes(CardHeroes cardHeroes)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                INSERT INTO fact_card_heroes (
                    user_id, user_card_hero_id, all_power,
                    all_health, all_physical_attack, all_physical_defense, all_magical_attack, all_magical_defense,
                    all_chemical_attack, all_chemical_defense, all_atomic_attack, all_atomic_defense,
                    all_mental_attack, all_mental_defense, all_speed, all_critical_damage, all_critical_rate,
                    all_armor_penetration, all_avoid, all_absorbs_damage, all_regenerate_vitality, all_accuracy, all_mana
                ) VALUES (
                    @user_id, @user_card_hero_id, @all_power,
                    @all_health, @all_physical_attack, @all_physical_defense, @all_magical_attack, @all_magical_defense,
                    @all_chemical_attack, @all_chemical_defense, @all_atomic_attack, @all_atomic_defense,
                    @all_mental_attack, @all_mental_defense, @all_speed, @all_critical_damage, @all_critical_rate,
                    @all_armor_penetration, @all_avoid, @all_absorbs_damage, @all_regenerate_vitality, @all_accuracy, @all_mana
                );";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.id);
                command.Parameters.AddWithValue("@all_power", cardHeroes.power);
                command.Parameters.AddWithValue("@all_health", cardHeroes.health);
                command.Parameters.AddWithValue("@all_physical_attack", cardHeroes.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", cardHeroes.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", cardHeroes.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", cardHeroes.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", cardHeroes.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", cardHeroes.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", cardHeroes.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", cardHeroes.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", cardHeroes.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", cardHeroes.mental_defense);
                command.Parameters.AddWithValue("@all_speed", cardHeroes.speed);
                command.Parameters.AddWithValue("@all_critical_damage", cardHeroes.critical_damage);
                command.Parameters.AddWithValue("@all_critical_rate", cardHeroes.critical_rate);
                command.Parameters.AddWithValue("@all_armor_penetration", cardHeroes.armor_penetration);
                command.Parameters.AddWithValue("@all_avoid", cardHeroes.avoid);
                command.Parameters.AddWithValue("@all_absorbs_damage", cardHeroes.absorbs_damage);
                command.Parameters.AddWithValue("@all_regenerate_vitality", cardHeroes.regenerate_vitality);
                command.Parameters.AddWithValue("@all_accuracy", cardHeroes.accuracy);
                command.Parameters.AddWithValue("@all_mana", cardHeroes.mana);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateFactCardHeroes(CardHeroes cardHeroes)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE fact_card_heroes
                SET 
                    all_power = @all_power, all_health = @all_health, all_physical_attack = @all_physical_attack,
                    all_physical_defense = @all_physical_defense, all_magical_attack = @all_magical_attack,
                    all_magical_defense = @all_magical_defense, all_chemical_attack = @all_chemical_attack,
                    all_chemical_defense = @all_chemical_defense, all_atomic_attack = @all_atomic_attack,
                    all_atomic_defense = @all_atomic_defense, all_mental_attack = @all_mental_attack,
                    all_mental_defense = @all_mental_defense, all_speed = @all_speed, all_critical_damage = @all_critical_damage,
                    all_critical_rate = @all_critical_rate, all_armor_penetration = @all_armor_penetration,
                    all_avoid = @all_avoid, all_absorbs_damage = @all_absorbs_damage, 
                    all_regenerate_vitality = @all_regenerate_vitality, 
                    all_accuracy = @all_accuracy, all_mana = @all_mana
                WHERE 
                    user_id = @user_id AND user_card_hero_id = @user_card_hero_id;;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.id);
                command.Parameters.AddWithValue("@all_power", cardHeroes.power);
                command.Parameters.AddWithValue("@all_health", cardHeroes.health);
                command.Parameters.AddWithValue("@all_physical_attack", cardHeroes.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", cardHeroes.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", cardHeroes.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", cardHeroes.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", cardHeroes.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", cardHeroes.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", cardHeroes.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", cardHeroes.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", cardHeroes.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", cardHeroes.mental_defense);
                command.Parameters.AddWithValue("@all_speed", cardHeroes.speed);
                command.Parameters.AddWithValue("@all_critical_damage", cardHeroes.critical_damage);
                command.Parameters.AddWithValue("@all_critical_rate", cardHeroes.critical_rate);
                command.Parameters.AddWithValue("@all_armor_penetration", cardHeroes.armor_penetration);
                command.Parameters.AddWithValue("@all_avoid", cardHeroes.avoid);
                command.Parameters.AddWithValue("@all_absorbs_damage", cardHeroes.absorbs_damage);
                command.Parameters.AddWithValue("@all_regenerate_vitality", cardHeroes.regenerate_vitality);
                command.Parameters.AddWithValue("@all_accuracy", cardHeroes.accuracy);
                command.Parameters.AddWithValue("@all_mana", cardHeroes.mana);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateTeamFactCardHeroes(int? team_id, string position, int card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                Update fact_card_heroes set team_id=@team_id, position=@position where user_id=@user_id 
                and user_card_hero_id=@user_card_hero_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_hero_id", card_id);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public int GetMaxQuantity(int Id)
    {
        int count = 0;
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select MAX(quantity) from user_card_heroes uc where uc.user_id=@userId and uc.card_hero_id=@card_hero_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@card_hero_id", Id);
                object result = command.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                return 0; // Nếu bảng rỗng, trả về 0
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return count;
    }
    public CardHeroes GetCardHeroesById(int Id)
    {
        CardHeroes card = new CardHeroes();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from card_heroes where card_heroes.id=@id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new CardHeroes
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
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
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description")
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
    public CardHeroes GetUserCardHeroesById(int Id)
    {
        CardHeroes card = new CardHeroes();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_card_heroes where user_card_heroes.card_hero_id=@id 
                and user_card_heroes.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new CardHeroes
                    {
                        id = reader.GetInt32("card_hero_id"),
                        level = reader.GetInt32("level"),
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
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana")
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
    public void InsertCardHeroesGallery(int Id)
    {
        CardHeroes CardFromDB = GetCardHeroesById(Id);
        int percent = 0;
        if (CardFromDB.rare.Equals("LG"))
        {
            percent = 20;
        }
        else if (CardFromDB.rare.Equals("UR"))
        {
            percent = 10;
        }
        else if (CardFromDB.rare.Equals("SSR"))
        {
            percent = 5;
        }
        else if (CardFromDB.rare.Equals("SR"))
        {
            percent = 2;
        }
        else if (CardFromDB.rare.Equals("MR"))
        {
            percent = 30;
        }
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra bản ghi đã tồn tại
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM card_heroes_gallery 
                WHERE user_id = @user_id AND card_hero_id = @card_hero_id;
                ";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_hero_id", Id);

                int recordCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (recordCount == 0)
                {
                    string query = @"
                    INSERT INTO card_heroes_gallery (
                        user_id, card_hero_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
                        magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                        mental_attack, mental_defense, speed, critical_damage, critical_rate, armor_penetration, avoid, 
                        absorbs_damage, regenerate_vitality, accuracy, mana, percent_all_health, percent_all_physical_attack, 
                        percent_all_physical_defense, percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, percent_all_mental_attack, 
                        percent_all_mental_defense
                    ) VALUES (
                        @user_id, @card_hero_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
                        @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, 
                        @mental_attack, @mental_defense, @speed, @critical_damage, @critical_rate, @armor_penetration, @avoid, 
                        @absorbs_damage, @regenerate_vitality, @accuracy, @mana, @percent_all_health, @percent_all_physical_attack, 
                        @percent_all_physical_defense, @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, @percent_all_mental_attack, 
                        @percent_all_mental_defense
                    );
                    ";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@card_hero_id", Id);
                    command.Parameters.AddWithValue("@status", "pending");
                    command.Parameters.AddWithValue("@current_star", 0);
                    command.Parameters.AddWithValue("@temp_star", 0);
                    command.Parameters.AddWithValue("@power", CardFromDB.power);
                    command.Parameters.AddWithValue("@health", CardFromDB.health);
                    command.Parameters.AddWithValue("@physical_attack", CardFromDB.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", CardFromDB.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", CardFromDB.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", CardFromDB.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", CardFromDB.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", CardFromDB.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", CardFromDB.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", CardFromDB.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", CardFromDB.magical_attack);
                    command.Parameters.AddWithValue("@mental_defense", CardFromDB.magical_defense);
                    command.Parameters.AddWithValue("@speed", CardFromDB.speed);
                    command.Parameters.AddWithValue("@critical_damage", CardFromDB.critical_damage);
                    command.Parameters.AddWithValue("@critical_rate", CardFromDB.critical_rate);
                    command.Parameters.AddWithValue("@armor_penetration", CardFromDB.armor_penetration);
                    command.Parameters.AddWithValue("@avoid", CardFromDB.avoid);
                    command.Parameters.AddWithValue("@absorbs_damage", CardFromDB.absorbs_damage);
                    command.Parameters.AddWithValue("@regenerate_vitality", CardFromDB.regenerate_vitality);
                    command.Parameters.AddWithValue("@accuracy", CardFromDB.accuracy);
                    command.Parameters.AddWithValue("@mana", CardFromDB.mana);
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
    public void UpdateStatusCardHeroesGallery(int Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "update card_heroes_gallery set status=@status where user_id=@user_id and card_hero_id=@card_hero_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_hero_id", Id);
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
    public List<CardHeroes> GetCardHeroesWithPrice(string type, int pageSize, int offset)
    {
        List<CardHeroes> CardHeroesList = new List<CardHeroes>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select c.*, ct.price, cu.image as currency_image, cu.id as currency_id
                from card_heroes c, card_hero_trade ct, currency cu
                where c.id=ct.card_hero_id and ct.currency_id = cu.id and c.type =@type
                ORDER BY c.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name limit @limit offset @offset;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardHeroes card = new CardHeroes
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
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
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description")
                    };
                    card.currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("price")
                    };

                    CardHeroesList.Add(card);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardHeroesList;
    }
    public int GetCardHeroesWithPriceCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*)
                from card_heroes c, card_hero_trade ct, currency cu
                where c.id=ct.card_hero_id and ct.currency_id = cu.id and c.type =@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
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
    public CardHeroes SumPowerCardHeroesGallery()
    {
        CardHeroes sumCardHeroes = new CardHeroes();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                SUM(power) AS total_power, SUM(health) AS total_health, SUM(physical_attack) AS total_physical_attack,
                SUM(physical_defense) AS total_physical_defense, SUM(magical_attack) AS total_magical_attack, SUM(magical_defense) AS total_magical_defense,
                SUM(chemical_attack) AS total_chemical_attack, SUM(chemical_defense) AS total_chemical_defense, SUM(atomic_attack) AS total_atomic_attack,
                SUM(atomic_defense) AS total_atomic_defense, SUM(mental_attack) AS total_mental_attack, SUM(mental_defense) AS total_mental_defense,
                SUM(speed) AS total_speed, SUM(critical_damage) AS total_critical_damage, SUM(critical_rate) AS total_critical_rate,
                SUM(armor_penetration) AS total_armor_penetration, SUM(avoid) AS total_avoid, SUM(absorbs_damage) AS total_absorbs_damage,
                SUM(regenerate_vitality) AS total_regenerate_vitality, SUM(accuracy) AS total_accuracy, SUM(mana) AS total_mana,    
                SUM(percent_all_health) AS total_percent_all_health, SUM(percent_all_physical_attack) AS total_percent_all_physical_attack,
                SUM(percent_all_physical_defense) AS total_percent_all_physical_defense, SUM(percent_all_magical_attack) AS total_percent_all_magical_attack,
                SUM(percent_all_magical_defense) AS total_percent_all_magical_defense, SUM(percent_all_chemical_attack) AS total_percent_all_chemical_attack,
                SUM(percent_all_chemical_defense) AS total_percent_all_chemical_defense, SUM(percent_all_atomic_attack) AS total_percent_all_atomic_attack,
                SUM(percent_all_atomic_defense) AS total_percent_all_atomic_defense, SUM(percent_all_mental_attack) AS total_percent_all_mental_attack,
                SUM(percent_all_mental_defense) AS total_percent_all_mental_defense
                FROM card_heroes_gallery where user_id=@user_id and status = 'available';";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumCardHeroes.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumCardHeroes.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumCardHeroes.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumCardHeroes.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumCardHeroes.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumCardHeroes.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumCardHeroes.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumCardHeroes.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumCardHeroes.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumCardHeroes.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumCardHeroes.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumCardHeroes.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumCardHeroes.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumCardHeroes.critical_damage = reader.IsDBNull(reader.GetOrdinal("total_critical_damage")) ? 0 : reader.GetDouble("total_critical_damage");
                        sumCardHeroes.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumCardHeroes.armor_penetration = reader.IsDBNull(reader.GetOrdinal("total_armor_penetration")) ? 0 : reader.GetDouble("total_armor_penetration");
                        sumCardHeroes.avoid = reader.IsDBNull(reader.GetOrdinal("total_avoid")) ? 0 : reader.GetDouble("total_avoid");
                        sumCardHeroes.absorbs_damage = reader.IsDBNull(reader.GetOrdinal("total_absorbs_damage")) ? 0 : reader.GetDouble("total_absorbs_damage");
                        sumCardHeroes.regenerate_vitality = reader.IsDBNull(reader.GetOrdinal("total_regenerate_vitality")) ? 0 : reader.GetDouble("total_regenerate_vitality");
                        sumCardHeroes.accuracy = reader.IsDBNull(reader.GetOrdinal("total_accuracy")) ? 0 : reader.GetDouble("total_accuracy");
                        sumCardHeroes.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetInt32("total_mana");
                        sumCardHeroes.percent_all_health = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDouble("total_percent_all_health");
                        sumCardHeroes.percent_all_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDouble("total_percent_all_physical_attack");
                        sumCardHeroes.percent_all_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDouble("total_percent_all_physical_defense");
                        sumCardHeroes.percent_all_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDouble("total_percent_all_magical_attack");
                        sumCardHeroes.percent_all_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDouble("total_percent_all_magical_defense");
                        sumCardHeroes.percent_all_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDouble("total_percent_all_chemical_attack");
                        sumCardHeroes.percent_all_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDouble("total_percent_all_chemical_defense");
                        sumCardHeroes.percent_all_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDouble("total_percent_all_atomic_attack");
                        sumCardHeroes.percent_all_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDouble("total_percent_all_atomic_defense");
                        sumCardHeroes.percent_all_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDouble("total_percent_all_mental_attack");
                        sumCardHeroes.percent_all_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDouble("total_percent_all_mental_defense");
                    }
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return sumCardHeroes;
    }
}
