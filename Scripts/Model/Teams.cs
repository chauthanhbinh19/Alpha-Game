using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class Teams
{
    public int user_id { get; set; }
    public int team_id { get; set; }
    public Teams()
    {

    }
    public List<Teams> GetUserTeams()
    {
        List<Teams> teams = new List<Teams>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = "SELECT * FROM Teams WHERE user_id=@user_id";
            MySqlCommand userCommand = new MySqlCommand(userQuery, connection);
            userCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            MySqlDataReader reader = userCommand.ExecuteReader();
            while (reader.Read())
            {
                teams.Add(new Teams
                {
                    user_id = reader.GetInt32("user_id"),
                    team_id = reader.GetInt32("team_id")
                });
            }
        }
        return teams;
    }
    public bool InsertUserTeams()
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = "INSERT INTO TEAMS VALUES (@user_id, @team_id)";
            MySqlCommand userCommand = new MySqlCommand(userQuery, connection);
            userCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            userCommand.Parameters.AddWithValue("@team_id", GetMaxTeamId(connection) + 1);
            userCommand.ExecuteNonQuery();
        }
        return true;
    }
    public int GetMaxTeamId(MySqlConnection connection)
    {
        string query = "SELECT MAX(team_id) FROM teams where user_id=@user_id";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
        object result = command.ExecuteScalar();

        if (result != DBNull.Value)
        {
            return Convert.ToInt32(result);
        }
        return 0; // Nếu bảng rỗng, trả về 0
    }
    public double GetTeamsPower()
    {
        double totalPower = 0;
        List<CardHeroes> CardHeroesList = new List<CardHeroes>();
        List<CardCaptains> CardCaptainsList = new List<CardCaptains>();
        List<CardColonels> CardColonelsList = new List<CardColonels>();
        List<CardGenerals> CardGeneralsList = new List<CardGenerals>();
        List<CardAdmirals> CardAdmiralsList = new List<CardAdmirals>();
        List<CardMonsters> CardMonstersList = new List<CardMonsters>();
        List<CardMilitary> CardMilitaryList = new List<CardMilitary>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = @"SELECT uc.*, c.*, fch.*
                FROM user_card_heroes uc
                LEFT JOIN card_heroes c ON uc.card_hero_id = c.id 
                LEFT JOIN fact_card_heroes fch ON fch.user_id = uc.user_id AND fch.user_card_hero_id = uc.card_hero_id
                WHERE uc.user_id = @user_id and fch.team_id IS NOT null";
            MySqlCommand command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
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
            reader.Close();
            userQuery = @"SELECT uc.*, c.*, fch.*
                FROM user_card_captains uc
                LEFT JOIN card_captains c ON uc.card_captain_id = c.id 
                LEFT JOIN fact_card_captains fch ON fch.user_id = uc.user_id AND fch.user_card_captain_id = uc.card_captain_id
                WHERE uc.user_id = @user_id and fch.team_id IS NOT null";
            command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardCaptains captain = new CardCaptains
                {
                    id = reader.GetInt32("card_captain_id"),
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

                CardCaptainsList.Add(captain);
            }
            reader.Close();
            userQuery = @"SELECT uc.*, c.*, fch.*
                FROM user_card_colonels uc
                LEFT JOIN card_colonels c ON uc.card_colonel_id = c.id 
                LEFT JOIN fact_card_colonels fch ON fch.user_id = uc.user_id AND fch.user_card_colonel_id = uc.card_colonel_id
                WHERE uc.user_id = @user_id and fch.team_id IS NOT null";
            command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardColonels captain = new CardColonels
                {
                    id = reader.GetInt32("card_colonel_id"),
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

                CardColonelsList.Add(captain);
            }
            reader.Close();
            userQuery = @"SELECT uc.*, c.*, fch.*
                FROM user_card_generals uc
                LEFT JOIN card_generals c ON uc.card_general_id = c.id 
                LEFT JOIN fact_card_generals fch ON fch.user_id = uc.user_id AND fch.user_card_general_id = uc.card_general_id
                WHERE uc.user_id = @user_id and fch.team_id IS NOT null";
            command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardGenerals generals = new CardGenerals
                {
                    id = reader.GetInt32("card_general_id"),
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

                CardGeneralsList.Add(generals);
            }
            reader.Close();
            userQuery = @"SELECT uc.*, c.*, fch.*
                FROM user_card_admirals uc
                LEFT JOIN card_admirals c ON uc.card_admiral_id = c.id 
                LEFT JOIN fact_card_admirals fch ON fch.user_id = uc.user_id AND fch.user_card_admiral_id = uc.card_admiral_id
                WHERE uc.user_id = @user_id and fch.team_id IS NOT null";
            command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardAdmirals admirals = new CardAdmirals
                {
                    id = reader.GetInt32("card_admiral_id"),
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

                CardAdmiralsList.Add(admirals);
            }
            reader.Close();
            userQuery = @"SELECT uc.*, c.*, fch.*
                FROM user_card_monsters uc
                LEFT JOIN card_monsters c ON uc.card_monster_id = c.id 
                LEFT JOIN fact_card_monsters fch ON fch.user_id = uc.user_id AND fch.user_card_monster_id = uc.card_monster_id
                WHERE uc.user_id = @user_id and fch.team_id IS NOT null";
            command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardMonsters monsters = new CardMonsters
                {
                    id = reader.GetInt32("card_monster_id"),
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

                CardMonstersList.Add(monsters);
            }
            reader.Close();
            userQuery = @"SELECT uc.*, c.*, fch.*
                FROM user_card_military uc
                LEFT JOIN card_military c ON uc.card_military_id = c.id 
                LEFT JOIN fact_card_military fch ON fch.user_id = uc.user_id AND fch.user_card_military_id = uc.card_military_id
                WHERE uc.user_id = @user_id and fch.team_id IS NOT null";
            command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardMilitary military = new CardMilitary
                {
                    id = reader.GetInt32("card_military_id"),
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

                CardMilitaryList.Add(military);
            }
            reader.Close();
            userQuery = "select sum(all_power) as all_power from fact_card_spell fch, teams t where fch.user_id=@user_id and fch.team_id=t.team_id";
            command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                double power = reader.IsDBNull(reader.GetOrdinal("all_power")) ? 0 : reader.GetDouble("all_power");
                totalPower = totalPower + power;
            }
            CardHeroes cardHeroes = new CardHeroes();
            CardHeroesList = cardHeroes.GetFinalPower(CardHeroesList);

            CardCaptains cardCaptains = new CardCaptains();
            CardCaptainsList = cardCaptains.GetFinalPower(CardCaptainsList);

            CardColonels cardColonels = new CardColonels();
            CardColonelsList = cardColonels.GetFinalPower(CardColonelsList);

            CardGenerals cardGenerals = new CardGenerals();
            CardGeneralsList = cardGenerals.GetFinalPower(CardGeneralsList);

            CardAdmirals cardAdmirals = new CardAdmirals();
            CardAdmiralsList = cardAdmirals.GetFinalPower(CardAdmiralsList);

            CardMonsters cardMonsters = new CardMonsters();
            CardMonstersList = cardMonsters.GetFinalPower(CardMonstersList);

            CardMilitary cardMilitary = new CardMilitary();
            CardMilitaryList = cardMilitary.GetFinalPower(CardMilitaryList);

            foreach(CardHeroes c in CardHeroesList){
                totalPower = totalPower + c.all_power;
            }
            foreach(CardCaptains c in CardCaptainsList){
                totalPower = totalPower + c.all_power;
            }
            foreach(CardColonels c in CardColonelsList){
                totalPower = totalPower + c.all_power;
            }
            foreach(CardGenerals c in CardGeneralsList){
                totalPower = totalPower + c.all_power;
            }
            foreach(CardAdmirals c in CardAdmiralsList){
                totalPower = totalPower + c.all_power;
            }
            foreach(CardMonsters c in CardMonstersList){
                totalPower = totalPower + c.all_power;
            }
            foreach(CardMilitary c in CardMilitaryList){
                totalPower = totalPower + c.all_power;
            }
        }
        return totalPower;
    }
}
