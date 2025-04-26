using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class Teams
{
    private int user_id1;
    private int team_id1;

    public int user_id { get => user_id1; set => user_id1 = value; }
    public int team_id { get => team_id1; set => team_id1 = value; }
    public Teams()
    {

    }
    public List<Teams> GetUserTeams(int user_id)
    {
        List<Teams> teams = new List<Teams>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = "SELECT * FROM Teams WHERE user_id=@user_id";
            MySqlCommand userCommand = new MySqlCommand(userQuery, connection);
            userCommand.Parameters.AddWithValue("@user_id", user_id);
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
    public bool InsertUserTeams(int user_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = "INSERT INTO TEAMS VALUES (@user_id, @team_id)";
            MySqlCommand userCommand = new MySqlCommand(userQuery, connection);
            userCommand.Parameters.AddWithValue("@user_id", user_id);
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
    public double GetTeamsPower(int user_id)
    {
        double totalPower = 0;
        List<CardHeroes> CardHeroesList = new List<CardHeroes>();
        List<CardCaptains> CardCaptainsList = new List<CardCaptains>();
        List<CardColonels> CardColonelsList = new List<CardColonels>();
        List<CardGenerals> CardGeneralsList = new List<CardGenerals>();
        List<CardAdmirals> CardAdmiralsList = new List<CardAdmirals>();
        List<CardMonsters> CardMonstersList = new List<CardMonsters>();
        List<CardMilitary> CardMilitaryList = new List<CardMilitary>();
        List<CardSpell> CardSpellList = new List<CardSpell>();
        List<Books> BooksList = new List<Books>();
        List<Pets> PetsList = new List<Pets>();
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
            command.Parameters.AddWithValue("@user_id", user_id);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardHeroes card = new CardHeroes
                {
                    id = reader.GetInt32("card_hero_id"),
                    name = reader.GetString("name"),
                    image = reader.GetString("image"),
                    rare = reader.GetString("rare"),
                    quality = reader.GetInt32("quality"),
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
                    all_critical_damage_rate = reader.GetDouble("all_critical_damage_rate"),
                    all_critical_rate = reader.GetDouble("all_critical_rate"),
                    all_penetration_rate = reader.GetDouble("all_penetration_rate"),
                    all_evasion_rate = reader.GetDouble("all_evasion_rate"),
                    all_damage_absorption_rate = reader.GetDouble("all_damage_absorption_rate"),
                    all_vitality_regeneration_rate = reader.GetDouble("all_vitality_regeneration_rate"),
                    all_accuracy_rate = reader.GetDouble("all_accuracy_rate"),
                    all_lifesteal_rate = reader.GetDouble("all_lifesteal_rate"),
                    all_shield_strength = reader.GetDouble("all_shield_strength"),
                    all_tenacity = reader.GetDouble("all_tenacity"),
                    all_resistance_rate = reader.GetDouble("all_resistance_rate"),
                    all_combo_rate = reader.GetDouble("all_combo_rate"),
                    all_reflection_rate = reader.GetDouble("all_reflection_rate"),
                    all_mana = reader.GetFloat("all_mana"),
                    all_mana_regeneration_rate = reader.GetDouble("all_mana_regeneration_rate"),
                    all_damage_to_different_faction_rate = reader.GetDouble("all_damage_to_different_faction_rate"),
                    all_resistance_to_different_faction_rate = reader.GetDouble("all_resistance_to_different_faction_rate"),
                    all_damage_to_same_faction_rate = reader.GetDouble("all_damage_to_same_faction_rate"),
                    all_resistance_to_same_faction_rate = reader.GetDouble("all_resistance_to_same_faction_rate"),
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
            command.Parameters.AddWithValue("@user_id", user_id);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardCaptains captain = new CardCaptains
                {
                    id = reader.GetInt32("card_captain_id"),
                    name = reader.GetString("name"),
                    image = reader.GetString("image"),
                    rare = reader.GetString("rare"),
                    quality = reader.GetInt32("quality"),
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
                    all_critical_damage_rate = reader.GetDouble("all_critical_damage_rate"),
                    all_critical_rate = reader.GetDouble("all_critical_rate"),
                    all_penetration_rate = reader.GetDouble("all_penetration_rate"),
                    all_evasion_rate = reader.GetDouble("all_evasion_rate"),
                    all_damage_absorption_rate = reader.GetDouble("all_damage_absorption_rate"),
                    all_vitality_regeneration_rate = reader.GetDouble("all_vitality_regeneration_rate"),
                    all_accuracy_rate = reader.GetDouble("all_accuracy_rate"),
                    all_lifesteal_rate = reader.GetDouble("all_lifesteal_rate"),
                    all_shield_strength = reader.GetDouble("all_shield_strength"),
                    all_tenacity = reader.GetDouble("all_tenacity"),
                    all_resistance_rate = reader.GetDouble("all_resistance_rate"),
                    all_combo_rate = reader.GetDouble("all_combo_rate"),
                    all_reflection_rate = reader.GetDouble("all_reflection_rate"),
                    all_mana = reader.GetFloat("all_mana"),
                    all_mana_regeneration_rate = reader.GetDouble("all_mana_regeneration_rate"),
                    all_damage_to_different_faction_rate = reader.GetDouble("all_damage_to_different_faction_rate"),
                    all_resistance_to_different_faction_rate = reader.GetDouble("all_resistance_to_different_faction_rate"),
                    all_damage_to_same_faction_rate = reader.GetDouble("all_damage_to_same_faction_rate"),
                    all_resistance_to_same_faction_rate = reader.GetDouble("all_resistance_to_same_faction_rate"),
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
            command.Parameters.AddWithValue("@user_id", user_id);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardColonels captain = new CardColonels
                {
                    id = reader.GetInt32("card_colonel_id"),
                    name = reader.GetString("name"),
                    image = reader.GetString("image"),
                    rare = reader.GetString("rare"),
                    quality = reader.GetInt32("quality"),
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
                    all_critical_damage_rate = reader.GetDouble("all_critical_damage_rate"),
                    all_critical_rate = reader.GetDouble("all_critical_rate"),
                    all_penetration_rate = reader.GetDouble("all_penetration_rate"),
                    all_evasion_rate = reader.GetDouble("all_evasion_rate"),
                    all_damage_absorption_rate = reader.GetDouble("all_damage_absorption_rate"),
                    all_vitality_regeneration_rate = reader.GetDouble("all_vitality_regeneration_rate"),
                    all_accuracy_rate = reader.GetDouble("all_accuracy_rate"),
                    all_lifesteal_rate = reader.GetDouble("all_lifesteal_rate"),
                    all_shield_strength = reader.GetDouble("all_shield_strength"),
                    all_tenacity = reader.GetDouble("all_tenacity"),
                    all_resistance_rate = reader.GetDouble("all_resistance_rate"),
                    all_combo_rate = reader.GetDouble("all_combo_rate"),
                    all_reflection_rate = reader.GetDouble("all_reflection_rate"),
                    all_mana = reader.GetFloat("all_mana"),
                    all_mana_regeneration_rate = reader.GetDouble("all_mana_regeneration_rate"),
                    all_damage_to_different_faction_rate = reader.GetDouble("all_damage_to_different_faction_rate"),
                    all_resistance_to_different_faction_rate = reader.GetDouble("all_resistance_to_different_faction_rate"),
                    all_damage_to_same_faction_rate = reader.GetDouble("all_damage_to_same_faction_rate"),
                    all_resistance_to_same_faction_rate = reader.GetDouble("all_resistance_to_same_faction_rate"),
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
            command.Parameters.AddWithValue("@user_id", user_id);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardGenerals generals = new CardGenerals
                {
                    id = reader.GetInt32("card_general_id"),
                    name = reader.GetString("name"),
                    image = reader.GetString("image"),
                    rare = reader.GetString("rare"),
                    quality = reader.GetInt32("quality"),
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
                    all_critical_damage_rate = reader.GetDouble("all_critical_damage_rate"),
                    all_critical_rate = reader.GetDouble("all_critical_rate"),
                    all_penetration_rate = reader.GetDouble("all_penetration_rate"),
                    all_evasion_rate = reader.GetDouble("all_evasion_rate"),
                    all_damage_absorption_rate = reader.GetDouble("all_damage_absorption_rate"),
                    all_vitality_regeneration_rate = reader.GetDouble("all_vitality_regeneration_rate"),
                    all_accuracy_rate = reader.GetDouble("all_accuracy_rate"),
                    all_lifesteal_rate = reader.GetDouble("all_lifesteal_rate"),
                    all_shield_strength = reader.GetDouble("all_shield_strength"),
                    all_tenacity = reader.GetDouble("all_tenacity"),
                    all_resistance_rate = reader.GetDouble("all_resistance_rate"),
                    all_combo_rate = reader.GetDouble("all_combo_rate"),
                    all_reflection_rate = reader.GetDouble("all_reflection_rate"),
                    all_mana = reader.GetFloat("all_mana"),
                    all_mana_regeneration_rate = reader.GetDouble("all_mana_regeneration_rate"),
                    all_damage_to_different_faction_rate = reader.GetDouble("all_damage_to_different_faction_rate"),
                    all_resistance_to_different_faction_rate = reader.GetDouble("all_resistance_to_different_faction_rate"),
                    all_damage_to_same_faction_rate = reader.GetDouble("all_damage_to_same_faction_rate"),
                    all_resistance_to_same_faction_rate = reader.GetDouble("all_resistance_to_same_faction_rate"),
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
            command.Parameters.AddWithValue("@user_id", user_id);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardAdmirals admirals = new CardAdmirals
                {
                    id = reader.GetInt32("card_admiral_id"),
                    name = reader.GetString("name"),
                    image = reader.GetString("image"),
                    rare = reader.GetString("rare"),
                    quality = reader.GetInt32("quality"),
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
                    all_critical_damage_rate = reader.GetDouble("all_critical_damage_rate"),
                    all_critical_rate = reader.GetDouble("all_critical_rate"),
                    all_penetration_rate = reader.GetDouble("all_penetration_rate"),
                    all_evasion_rate = reader.GetDouble("all_evasion_rate"),
                    all_damage_absorption_rate = reader.GetDouble("all_damage_absorption_rate"),
                    all_vitality_regeneration_rate = reader.GetDouble("all_vitality_regeneration_rate"),
                    all_accuracy_rate = reader.GetDouble("all_accuracy_rate"),
                    all_lifesteal_rate = reader.GetDouble("all_lifesteal_rate"),
                    all_shield_strength = reader.GetDouble("all_shield_strength"),
                    all_tenacity = reader.GetDouble("all_tenacity"),
                    all_resistance_rate = reader.GetDouble("all_resistance_rate"),
                    all_combo_rate = reader.GetDouble("all_combo_rate"),
                    all_reflection_rate = reader.GetDouble("all_reflection_rate"),
                    all_mana = reader.GetFloat("all_mana"),
                    all_mana_regeneration_rate = reader.GetDouble("all_mana_regeneration_rate"),
                    all_damage_to_different_faction_rate = reader.GetDouble("all_damage_to_different_faction_rate"),
                    all_resistance_to_different_faction_rate = reader.GetDouble("all_resistance_to_different_faction_rate"),
                    all_damage_to_same_faction_rate = reader.GetDouble("all_damage_to_same_faction_rate"),
                    all_resistance_to_same_faction_rate = reader.GetDouble("all_resistance_to_same_faction_rate"),
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
            command.Parameters.AddWithValue("@user_id", user_id);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardMonsters monsters = new CardMonsters
                {
                    id = reader.GetInt32("card_monster_id"),
                    name = reader.GetString("name"),
                    image = reader.GetString("image"),
                    rare = reader.GetString("rare"),
                    quality = reader.GetInt32("quality"),
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
                    all_critical_damage_rate = reader.GetDouble("all_critical_damage_rate"),
                    all_critical_rate = reader.GetDouble("all_critical_rate"),
                    all_penetration_rate = reader.GetDouble("all_penetration_rate"),
                    all_evasion_rate = reader.GetDouble("all_evasion_rate"),
                    all_damage_absorption_rate = reader.GetDouble("all_damage_absorption_rate"),
                    all_vitality_regeneration_rate = reader.GetDouble("all_vitality_regeneration_rate"),
                    all_accuracy_rate = reader.GetDouble("all_accuracy_rate"),
                    all_lifesteal_rate = reader.GetDouble("all_lifesteal_rate"),
                    all_shield_strength = reader.GetDouble("all_shield_strength"),
                    all_tenacity = reader.GetDouble("all_tenacity"),
                    all_resistance_rate = reader.GetDouble("all_resistance_rate"),
                    all_combo_rate = reader.GetDouble("all_combo_rate"),
                    all_reflection_rate = reader.GetDouble("all_reflection_rate"),
                    all_mana = reader.GetFloat("all_mana"),
                    all_mana_regeneration_rate = reader.GetDouble("all_mana_regeneration_rate"),
                    all_damage_to_different_faction_rate = reader.GetDouble("all_damage_to_different_faction_rate"),
                    all_resistance_to_different_faction_rate = reader.GetDouble("all_resistance_to_different_faction_rate"),
                    all_damage_to_same_faction_rate = reader.GetDouble("all_damage_to_same_faction_rate"),
                    all_resistance_to_same_faction_rate = reader.GetDouble("all_resistance_to_same_faction_rate"),
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
            command.Parameters.AddWithValue("@user_id", user_id);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardMilitary military = new CardMilitary
                {
                    id = reader.GetInt32("card_military_id"),
                    name = reader.GetString("name"),
                    image = reader.GetString("image"),
                    rare = reader.GetString("rare"),
                    quality = reader.GetInt32("quality"),
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
                    all_critical_damage_rate = reader.GetDouble("all_critical_damage_rate"),
                    all_critical_rate = reader.GetDouble("all_critical_rate"),
                    all_penetration_rate = reader.GetDouble("all_penetration_rate"),
                    all_evasion_rate = reader.GetDouble("all_evasion_rate"),
                    all_damage_absorption_rate = reader.GetDouble("all_damage_absorption_rate"),
                    all_vitality_regeneration_rate = reader.GetDouble("all_vitality_regeneration_rate"),
                    all_accuracy_rate = reader.GetDouble("all_accuracy_rate"),
                    all_lifesteal_rate = reader.GetDouble("all_lifesteal_rate"),
                    all_shield_strength = reader.GetDouble("all_shield_strength"),
                    all_tenacity = reader.GetDouble("all_tenacity"),
                    all_resistance_rate = reader.GetDouble("all_resistance_rate"),
                    all_combo_rate = reader.GetDouble("all_combo_rate"),
                    all_reflection_rate = reader.GetDouble("all_reflection_rate"),
                    all_mana = reader.GetFloat("all_mana"),
                    all_mana_regeneration_rate = reader.GetDouble("all_mana_regeneration_rate"),
                    all_damage_to_different_faction_rate = reader.GetDouble("all_damage_to_different_faction_rate"),
                    all_resistance_to_different_faction_rate = reader.GetDouble("all_resistance_to_different_faction_rate"),
                    all_damage_to_same_faction_rate = reader.GetDouble("all_damage_to_same_faction_rate"),
                    all_resistance_to_same_faction_rate = reader.GetDouble("all_resistance_to_same_faction_rate"),
                };

                CardMilitaryList.Add(military);
            }
            reader.Close();
            userQuery = @"SELECT uc.*, c.*, fch.*
                FROM user_books uc
                LEFT JOIN books c ON uc.book_id = c.id 
                LEFT JOIN fact_books fch ON fch.user_id = uc.user_id AND fch.user_book_id = uc.book_id
                WHERE uc.user_id = @user_id and fch.team_id IS NOT null";
            command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Books books1 = new Books
                {
                    id = reader.GetInt32("book_id"),
                    name = reader.GetString("name"),
                    image = reader.GetString("image"),
                    rare = reader.GetString("rare"),
                    quality = reader.GetInt32("quality"),
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
                    all_critical_damage_rate = reader.GetDouble("all_critical_damage_rate"),
                    all_critical_rate = reader.GetDouble("all_critical_rate"),
                    all_penetration_rate = reader.GetDouble("all_penetration_rate"),
                    all_evasion_rate = reader.GetDouble("all_evasion_rate"),
                    all_damage_absorption_rate = reader.GetDouble("all_damage_absorption_rate"),
                    all_vitality_regeneration_rate = reader.GetDouble("all_vitality_regeneration_rate"),
                    all_accuracy_rate = reader.GetDouble("all_accuracy_rate"),
                    all_lifesteal_rate = reader.GetDouble("all_lifesteal_rate"),
                    all_shield_strength = reader.GetDouble("all_shield_strength"),
                    all_tenacity = reader.GetDouble("all_tenacity"),
                    all_resistance_rate = reader.GetDouble("all_resistance_rate"),
                    all_combo_rate = reader.GetDouble("all_combo_rate"),
                    all_reflection_rate = reader.GetDouble("all_reflection_rate"),
                    all_mana = reader.GetFloat("all_mana"),
                    all_mana_regeneration_rate = reader.GetDouble("all_mana_regeneration_rate"),
                    all_damage_to_different_faction_rate = reader.GetDouble("all_damage_to_different_faction_rate"),
                    all_resistance_to_different_faction_rate = reader.GetDouble("all_resistance_to_different_faction_rate"),
                    all_damage_to_same_faction_rate = reader.GetDouble("all_damage_to_same_faction_rate"),
                    all_resistance_to_same_faction_rate = reader.GetDouble("all_resistance_to_same_faction_rate"),
                };

                BooksList.Add(books1);
            }
            reader.Close();
            userQuery = @"SELECT uc.*, c.*, fch.*
                FROM user_pets uc
                LEFT JOIN pets c ON uc.pet_id = c.id 
                LEFT JOIN fact_pets fch ON fch.user_id = uc.user_id AND fch.user_pet_id = uc.pet_id
                WHERE uc.user_id = @user_id and fch.team_id IS NOT null";
            command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Pets pets1 = new Pets
                {
                    id = reader.GetInt32("pet_id"),
                    name = reader.GetString("name"),
                    image = reader.GetString("image"),
                    rare = reader.GetString("rare"),
                    quality = reader.GetInt32("quality"),
                    type = reader.GetString("type"),
                    star = reader.GetInt32("star"),
                    level = reader.GetInt32("level"),
                    experiment = reader.GetInt32("experiment"),
                    quantity = reader.GetInt32("quantity"),
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
                    all_critical_damage_rate = reader.GetDouble("all_critical_damage_rate"),
                    all_critical_rate = reader.GetDouble("all_critical_rate"),
                    all_penetration_rate = reader.GetDouble("all_penetration_rate"),
                    all_evasion_rate = reader.GetDouble("all_evasion_rate"),
                    all_damage_absorption_rate = reader.GetDouble("all_damage_absorption_rate"),
                    all_vitality_regeneration_rate = reader.GetDouble("all_vitality_regeneration_rate"),
                    all_accuracy_rate = reader.GetDouble("all_accuracy_rate"),
                    all_lifesteal_rate = reader.GetDouble("all_lifesteal_rate"),
                    all_shield_strength = reader.GetDouble("all_shield_strength"),
                    all_tenacity = reader.GetDouble("all_tenacity"),
                    all_resistance_rate = reader.GetDouble("all_resistance_rate"),
                    all_combo_rate = reader.GetDouble("all_combo_rate"),
                    all_reflection_rate = reader.GetDouble("all_reflection_rate"),
                    all_mana = reader.GetFloat("all_mana"),
                    all_mana_regeneration_rate = reader.GetDouble("all_mana_regeneration_rate"),
                    all_damage_to_different_faction_rate = reader.GetDouble("all_damage_to_different_faction_rate"),
                    all_resistance_to_different_faction_rate = reader.GetDouble("all_resistance_to_different_faction_rate"),
                    all_damage_to_same_faction_rate = reader.GetDouble("all_damage_to_same_faction_rate"),
                    all_resistance_to_same_faction_rate = reader.GetDouble("all_resistance_to_same_faction_rate"),
                };

                PetsList.Add(pets1);
            }
            reader.Close();
            userQuery = @"SELECT uc.*, c.*, fch.*
                FROM user_card_spell uc
                LEFT JOIN card_spell c ON uc.card_spell_id = c.id 
                LEFT JOIN fact_card_spell fch ON fch.user_id = uc.user_id AND fch.user_card_spell_id = uc.card_spell_id
                WHERE uc.user_id = @user_id and fch.team_id IS NOT null";
            command = new MySqlCommand(userQuery, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                CardSpell cardSpell = new CardSpell
                {
                    id = reader.GetInt32("card_spell_id"),
                    name = reader.GetString("name"),
                    image = reader.GetString("image"),
                    rare = reader.GetString("rare"),
                    quality = reader.GetInt32("quality"),
                    type = reader.GetString("type"),
                    star = reader.GetInt32("star"),
                    level = reader.GetInt32("level"),
                    experiment = reader.GetInt32("experiment"),
                    quantity = reader.GetInt32("quantity"),
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
                    all_critical_damage_rate = reader.GetDouble("all_critical_damage_rate"),
                    all_critical_rate = reader.GetDouble("all_critical_rate"),
                    all_penetration_rate = reader.GetDouble("all_penetration_rate"),
                    all_evasion_rate = reader.GetDouble("all_evasion_rate"),
                    all_damage_absorption_rate = reader.GetDouble("all_damage_absorption_rate"),
                    all_vitality_regeneration_rate = reader.GetDouble("all_vitality_regeneration_rate"),
                    all_accuracy_rate = reader.GetDouble("all_accuracy_rate"),
                    all_lifesteal_rate = reader.GetDouble("all_lifesteal_rate"),
                    all_shield_strength = reader.GetDouble("all_shield_strength"),
                    all_tenacity = reader.GetDouble("all_tenacity"),
                    all_resistance_rate = reader.GetDouble("all_resistance_rate"),
                    all_combo_rate = reader.GetDouble("all_combo_rate"),
                    all_reflection_rate = reader.GetDouble("all_reflection_rate"),
                    all_mana = reader.GetFloat("all_mana"),
                    all_mana_regeneration_rate = reader.GetDouble("all_mana_regeneration_rate"),
                    all_damage_to_different_faction_rate = reader.GetDouble("all_damage_to_different_faction_rate"),
                    all_resistance_to_different_faction_rate = reader.GetDouble("all_resistance_to_different_faction_rate"),
                    all_damage_to_same_faction_rate = reader.GetDouble("all_damage_to_same_faction_rate"),
                    all_resistance_to_same_faction_rate = reader.GetDouble("all_resistance_to_same_faction_rate"),
                };
                CardSpellList.Add(cardSpell);
            }
            CardHeroes cardHeroes = new CardHeroes();
            CardHeroesList = cardHeroes.GetFinalPower(user_id, CardHeroesList);
            CardHeroesList = cardHeroes.GetAllEquipmentPower(user_id, CardHeroesList);
            CardHeroesList = cardHeroes.GetAllRankPower(user_id, CardHeroesList);
            CardHeroesList = cardHeroes.GetAllAnimeStatsPower(user_id, CardHeroesList);
            CardHeroesList = cardHeroes.GetQualityPower(CardHeroesList);

            CardCaptains cardCaptains = new CardCaptains();
            CardCaptainsList = cardCaptains.GetFinalPower(user_id, CardCaptainsList);
            CardCaptainsList = cardCaptains.GetAllEquipmentPower(user_id, CardCaptainsList);
            CardCaptainsList = cardCaptains.GetAllRankPower(user_id, CardCaptainsList);
            CardCaptainsList = cardCaptains.GetAllAnimeStatsPower(user_id, CardCaptainsList);
            CardCaptainsList = cardCaptains.GetQualityPower(CardCaptainsList);

            CardColonels cardColonels = new CardColonels();
            CardColonelsList = cardColonels.GetFinalPower(user_id, CardColonelsList);
            CardColonelsList = cardColonels.GetAllEquipmentPower(user_id, CardColonelsList);
            CardColonelsList = cardColonels.GetAllRankPower(user_id, CardColonelsList);
            CardColonelsList = cardColonels.GetAllAnimeStatsPower(user_id, CardColonelsList);
            CardColonelsList = cardColonels.GetQualityPower(CardColonelsList);

            CardGenerals cardGenerals = new CardGenerals();
            CardGeneralsList = cardGenerals.GetFinalPower(user_id, CardGeneralsList);
            CardGeneralsList = cardGenerals.GetAllEquipmentPower(user_id, CardGeneralsList);
            CardGeneralsList = cardGenerals.GetAllRankPower(user_id, CardGeneralsList);
            CardGeneralsList = cardGenerals.GetAllAnimeStatsPower(user_id, CardGeneralsList);
            CardGeneralsList = cardGenerals.GetQualityPower(CardGeneralsList);

            CardAdmirals cardAdmirals = new CardAdmirals();
            CardAdmiralsList = cardAdmirals.GetFinalPower(user_id, CardAdmiralsList);
            CardAdmiralsList = cardAdmirals.GetAllEquipmentPower(user_id, CardAdmiralsList);
            CardAdmiralsList = cardAdmirals.GetAllRankPower(user_id, CardAdmiralsList);
            CardAdmiralsList = cardAdmirals.GetAllAnimeStatsPower(user_id, CardAdmiralsList);
            CardAdmiralsList = cardAdmirals.GetQualityPower(CardAdmiralsList);

            CardMonsters cardMonsters = new CardMonsters();
            CardMonstersList = cardMonsters.GetFinalPower(user_id, CardMonstersList);
            CardMonstersList = cardMonsters.GetAllEquipmentPower(user_id, CardMonstersList);
            CardMonstersList = cardMonsters.GetAllRankPower(user_id, CardMonstersList);
            CardMonstersList = cardMonsters.GetAllAnimeStatsPower(user_id, CardMonstersList);
            CardMonstersList = cardMonsters.GetQualityPower(CardMonstersList);

            CardMilitary cardMilitary = new CardMilitary();
            CardMilitaryList = cardMilitary.GetFinalPower(user_id, CardMilitaryList);
            CardMilitaryList = cardMilitary.GetAllEquipmentPower(user_id, CardMilitaryList);
            CardMilitaryList = cardMilitary.GetAllRankPower(user_id, CardMilitaryList);
            CardMilitaryList = cardMilitary.GetAllAnimeStatsPower(user_id, CardMilitaryList);
            CardMilitaryList = cardMilitary.GetQualityPower(CardMilitaryList);

            CardSpell cardSpell1 = new CardSpell();
            CardSpellList = cardSpell1.GetFinalPower(user_id, CardSpellList);
            CardSpellList = cardSpell1.GetAllEquipmentPower(user_id, CardSpellList);
            CardSpellList = cardSpell1.GetAllRankPower(user_id, CardSpellList);
            CardSpellList = cardSpell1.GetAllAnimeStatsPower(user_id, CardSpellList);
            CardSpellList = cardSpell1.GetQualityPower(CardSpellList);

            Books books = new Books();
            BooksList = books.GetFinalPower(user_id, BooksList);
            BooksList = books.GetAllEquipmentPower(user_id, BooksList);
            BooksList = books.GetAllRankPower(user_id, BooksList);
            BooksList = books.GetAllAnimeStatsPower(user_id, BooksList);
            BooksList = books.GetQualityPower(BooksList);

            Pets pets = new Pets();
            PetsList = pets.GetFinalPower(user_id, PetsList);
            PetsList = pets.GetAllEquipmentPower(user_id, PetsList);
            PetsList = pets.GetAllRankPower(user_id, PetsList);
            PetsList = pets.GetAllAnimeStatsPower(user_id, PetsList);
            PetsList = pets.GetQualityPower(PetsList);

            foreach (CardHeroes c in CardHeroesList)
            {
                totalPower = totalPower + c.all_power;
            }
            foreach (CardCaptains c in CardCaptainsList)
            {
                totalPower = totalPower + c.all_power;
            }
            foreach (CardColonels c in CardColonelsList)
            {
                totalPower = totalPower + c.all_power;
            }
            foreach (CardGenerals c in CardGeneralsList)
            {
                totalPower = totalPower + c.all_power;
            }
            foreach (CardAdmirals c in CardAdmiralsList)
            {
                totalPower = totalPower + c.all_power;
            }
            foreach (CardMonsters c in CardMonstersList)
            {
                totalPower = totalPower + c.all_power;
            }
            foreach (CardMilitary c in CardMilitaryList)
            {
                totalPower = totalPower + c.all_power;
            }
            foreach (CardSpell c in CardSpellList)
            {
                totalPower = totalPower + c.all_power;
            }
            foreach (Books c in BooksList)
            {
                totalPower = totalPower + c.all_power;
            }
            foreach (Pets c in PetsList)
            {
                totalPower = totalPower + c.all_power;
            }
        }
        return totalPower;
    }
}
