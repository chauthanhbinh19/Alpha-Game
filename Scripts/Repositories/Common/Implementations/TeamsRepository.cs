using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class TeamsRepository : ITeamsRepository
{
    public async Task<List<Teams>> GetUserTeamsAsync(string user_id)
    {
        var teams = new List<Teams>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync(); // mở connection async

            string selectSQL = "SELECT * FROM Teams WHERE user_id=@user_id ORDER BY team_number ASC";
            using (var selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@user_id", user_id);

                using (var reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        teams.Add(new Teams
                        {
                            UserId = reader.GetString("user_id"),
                            TeamId = reader.GetString("team_id"),
                            TeamNumber = reader.GetInt32("team_number"),
                            TeamAvatar = reader.GetString("team_avatar"),
                            TeamBorder = reader.GetString("team_border"),
                        });
                    }
                }
            }
        }

        return teams;
    }
    public async Task<bool> InsertUserTeamsAsync(string user_id, int team_number = 1)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync(); // mở connection async

            string insertSQL = @"
            INSERT INTO TEAMS (user_id, team_id, team_number, team_avatar, team_border) 
            VALUES (@user_id, @team_id, @team_number, @team_avatar, @team_border)";

            using (var insertCommand = new MySqlCommand(insertSQL, connection))
            {
                insertCommand.Parameters.AddWithValue("@user_id", user_id);
                insertCommand.Parameters.AddWithValue("@team_id", Guid.NewGuid().ToString());
                insertCommand.Parameters.AddWithValue("@team_number", team_number);
                insertCommand.Parameters.AddWithValue("@team_avatar", "Team/Avatar/Team_Avatar_1");
                insertCommand.Parameters.AddWithValue("@team_border", "Team/Border/Team_Border_1");

                await insertCommand.ExecuteNonQueryAsync(); // chạy async
            }
        }

        return true;
    }
    public async Task<int> GetMaxTeamIdAsync(MySqlConnection connection)
    {
        string selectSQL = "SELECT MAX(team_id) FROM teams WHERE user_id = @user_id";
        using (var selectCommand = new MySqlCommand(selectSQL, connection))
        {
            selectCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);

            object result = await selectCommand.ExecuteScalarAsync(); // async call

            if (result != DBNull.Value && result != null)
            {
                return Convert.ToInt32(result);
            }
            return 0; // nếu bảng rỗng
        }
    }
    public double GetTeamsPower(string user_id)
    {
        double totalPower = 0;

        return totalPower;
    }
    public async Task<List<TeamEmblems>> GetUserTeamEmblemsAsync(string user_id, string team_id, int position, string cardType)
    {
        var teamEmblem = new List<TeamEmblems>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync(); // mở connection async

            string selectSQL = @"SELECT te.emblem_id, e.name, e.type, e.image, te.emblem_quantity
            FROM team_emblems te
            LEFT JOIN emblems e 
                ON te.emblem_id = e.id
            WHERE te.user_id = @user_id 
                AND te.team_id = @team_id 
                AND te.position = @position
                AND te.card_type = @card_type";
            using (var selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@user_id", user_id);
                selectCommand.Parameters.AddWithValue("@team_id", team_id);
                selectCommand.Parameters.AddWithValue("@position", position);
                selectCommand.Parameters.AddWithValue("@card_type", cardType);

                using (var reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        teamEmblem.Add(new TeamEmblems
                        {
                            UserId = user_id,
                            TeamId = team_id,
                            Position = position,
                            CardType = cardType,
                            EmblemId = reader.GetStringSafe("emblem_id"),
                            EmblemQuantity = reader.GetIntSafe("emblem_quantity"),
                            Emblem = new Emblems
                            {
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Type = reader.GetStringSafe("type"),
                            }
                        });
                    }
                }
            }
        }

        return teamEmblem;
    }
    public async Task<List<Objects>> GetUserObjectsInTeamAsync(string userId)
    {
        List<Objects> objects = new List<Objects>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT 'CARD_HEROES' AS object_type, uc.card_hero_id AS card_id, uc.level, uc.quality, uc.star, uc.rare, uc.team_id, uc.position, 
                    COALESCE(am.total_module_mult, 0) AS module_multiplier, COALESCE(au.total_upgrade_mult, 0) AS upgrade_multiplier,
                    c.power, c.health, c.physical_attack, c.physical_defense, c.magical_attack, c.magical_defense, 
                    c.chemical_attack, c.chemical_defense, c.atomic_attack, c.atomic_defense, c.mental_attack, c.mental_defense, 
                    c.speed, c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate, 
                    c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate, c.damage_absorption_rate, 
                    c.ignore_damage_absorption_rate, c.absorbed_damage_rate, c.vitality_regeneration_rate, 
                    c.vitality_regeneration_resistance_rate, c.accuracy_rate, c.lifesteal_rate, c.shield_strength, 
                    c.tenacity, c.resistance_rate, c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, 
                    c.combo_resistance_rate, c.stun_rate, c.ignore_stun_rate, c.reflection_rate, c.ignore_reflection_rate, 
                    c.reflection_damage_rate, c.reflection_resistance_rate, c.mana, c.mana_regeneration_rate, 
                    c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate, c.damage_to_same_faction_rate, 
                    c.resistance_to_same_faction_rate, c.normal_damage_rate, c.normal_resistance_rate, c.skill_damage_rate, c.skill_resistance_rate
            FROM user_card_heroes uc
            INNER JOIN card_heroes c ON uc.card_hero_id = c.id
            LEFT JOIN (SELECT user_card_hero_id, SUM(current_multiplier) AS total_module_mult FROM user_card_heroes_module GROUP BY user_card_hero_id) am ON uc.card_hero_id = am.user_card_hero_id
            LEFT JOIN (SELECT user_card_hero_id, SUM(current_multiplier) AS total_upgrade_mult FROM user_card_heroes_upgrade GROUP BY user_card_hero_id) au ON uc.card_hero_id = au.user_card_hero_id
            LEFT JOIN teams t ON t.team_id = uc.team_id
            WHERE uc.user_id = @userId AND t.is_main = 1 AND c.is_active = TRUE AND c.is_deleted = FALSE

            UNION ALL

            SELECT 'CARD_CAPTAINS' AS object_type, uc.card_captain_id AS card_id, uc.level, uc.quality, uc.star, uc.rare, uc.team_id, uc.position, 
                    COALESCE(am.total_module_mult, 0), COALESCE(au.total_upgrade_mult, 0),
                    c.power, c.health, c.physical_attack, c.physical_defense, c.magical_attack, c.magical_defense, 
                    c.chemical_attack, c.chemical_defense, c.atomic_attack, c.atomic_defense, c.mental_attack, c.mental_defense, 
                    c.speed, c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate, 
                    c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate, c.damage_absorption_rate, 
                    c.ignore_damage_absorption_rate, c.absorbed_damage_rate, c.vitality_regeneration_rate, 
                    c.vitality_regeneration_resistance_rate, c.accuracy_rate, c.lifesteal_rate, c.shield_strength, 
                    c.tenacity, c.resistance_rate, c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, 
                    c.combo_resistance_rate, c.stun_rate, c.ignore_stun_rate, c.reflection_rate, c.ignore_reflection_rate, 
                    c.reflection_damage_rate, c.reflection_resistance_rate, c.mana, c.mana_regeneration_rate, 
                    c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate, c.damage_to_same_faction_rate, 
                    c.resistance_to_same_faction_rate, c.normal_damage_rate, c.normal_resistance_rate, c.skill_damage_rate, c.skill_resistance_rate
            FROM user_card_captains uc
            INNER JOIN card_captains c ON uc.card_captain_id = c.id
            LEFT JOIN (SELECT user_card_captain_id, SUM(current_multiplier) AS total_module_mult FROM user_card_captains_module GROUP BY user_card_captain_id) am ON uc.card_captain_id = am.user_card_captain_id
            LEFT JOIN (SELECT user_card_captain_id, SUM(current_multiplier) AS total_upgrade_mult FROM user_card_captains_upgrade GROUP BY user_card_captain_id) au ON uc.card_captain_id = au.user_card_captain_id
            LEFT JOIN teams t ON t.team_id = uc.team_id
            WHERE uc.user_id = @userId AND t.is_main = 1 AND c.is_active = TRUE AND c.is_deleted = FALSE

            UNION ALL

            SELECT 'CARD_COLONELS' AS object_type, uc.card_colonel_id AS card_id, uc.level, uc.quality, uc.star, uc.rare, uc.team_id, uc.position, 
                    COALESCE(am.total_module_mult, 0), COALESCE(au.total_upgrade_mult, 0),
                    c.power, c.health, c.physical_attack, c.physical_defense, c.magical_attack, c.magical_defense, 
                    c.chemical_attack, c.chemical_defense, c.atomic_attack, c.atomic_defense, c.mental_attack, c.mental_defense, 
                    c.speed, c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate, 
                    c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate, c.damage_absorption_rate, 
                    c.ignore_damage_absorption_rate, c.absorbed_damage_rate, c.vitality_regeneration_rate, 
                    c.vitality_regeneration_resistance_rate, c.accuracy_rate, c.lifesteal_rate, c.shield_strength, 
                    c.tenacity, c.resistance_rate, c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, 
                    c.combo_resistance_rate, c.stun_rate, c.ignore_stun_rate, c.reflection_rate, c.ignore_reflection_rate, 
                    c.reflection_damage_rate, c.reflection_resistance_rate, c.mana, c.mana_regeneration_rate, 
                    c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate, c.damage_to_same_faction_rate, 
                    c.resistance_to_same_faction_rate, c.normal_damage_rate, c.normal_resistance_rate, c.skill_damage_rate, c.skill_resistance_rate
            FROM user_card_colonels uc
            INNER JOIN card_colonels c ON uc.card_colonel_id = c.id
            LEFT JOIN (SELECT user_card_colonel_id, SUM(current_multiplier) AS total_module_mult FROM user_card_colonels_module GROUP BY user_card_colonel_id) am ON uc.card_colonel_id = am.user_card_colonel_id
            LEFT JOIN (SELECT user_card_colonel_id, SUM(current_multiplier) AS total_upgrade_mult FROM user_card_colonels_upgrade GROUP BY user_card_colonel_id) au ON uc.card_colonel_id = au.user_card_colonel_id
            LEFT JOIN teams t ON t.team_id = uc.team_id
            WHERE uc.user_id = @userId AND t.is_main = 1 AND c.is_active = TRUE AND c.is_deleted = FALSE

            UNION ALL

            SELECT 'CARD_GENERALS' AS object_type, uc.card_general_id AS card_id, uc.level, uc.quality, uc.star, uc.rare, uc.team_id, uc.position, 
                    COALESCE(am.total_module_mult, 0), COALESCE(au.total_upgrade_mult, 0),
                    c.power, c.health, c.physical_attack, c.physical_defense, c.magical_attack, c.magical_defense, 
                    c.chemical_attack, c.chemical_defense, c.atomic_attack, c.atomic_defense, c.mental_attack, c.mental_defense, 
                    c.speed, c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate, 
                    c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate, c.damage_absorption_rate, 
                    c.ignore_damage_absorption_rate, c.absorbed_damage_rate, c.vitality_regeneration_rate, 
                    c.vitality_regeneration_resistance_rate, c.accuracy_rate, c.lifesteal_rate, c.shield_strength, 
                    c.tenacity, c.resistance_rate, c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, 
                    c.combo_resistance_rate, c.stun_rate, c.ignore_stun_rate, c.reflection_rate, c.ignore_reflection_rate, 
                    c.reflection_damage_rate, c.reflection_resistance_rate, c.mana, c.mana_regeneration_rate, 
                    c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate, c.damage_to_same_faction_rate, 
                    c.resistance_to_same_faction_rate, c.normal_damage_rate, c.normal_resistance_rate, c.skill_damage_rate, c.skill_resistance_rate
            FROM user_card_generals uc
            INNER JOIN card_generals c ON uc.card_general_id = c.id
            LEFT JOIN (SELECT user_card_general_id, SUM(current_multiplier) AS total_module_mult FROM user_card_generals_module GROUP BY user_card_general_id) am ON uc.card_general_id = am.user_card_general_id
            LEFT JOIN (SELECT user_card_general_id, SUM(current_multiplier) AS total_upgrade_mult FROM user_card_generals_upgrade GROUP BY user_card_general_id) au ON uc.card_general_id = au.user_card_general_id
            LEFT JOIN teams t ON t.team_id = uc.team_id
            WHERE uc.user_id = @userId AND t.is_main = 1 AND c.is_active = TRUE AND c.is_deleted = FALSE

            UNION ALL

            SELECT 'CARD_ADMIRALS' AS object_type, uc.card_admiral_id AS card_id, uc.level, uc.quality, uc.star, uc.rare, uc.team_id, uc.position, 
                    COALESCE(am.total_module_mult, 0), COALESCE(au.total_upgrade_mult, 0),
                    c.power, c.health, c.physical_attack, c.physical_defense, c.magical_attack, c.magical_defense, 
                    c.chemical_attack, c.chemical_defense, c.atomic_attack, c.atomic_defense, c.mental_attack, c.mental_defense, 
                    c.speed, c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate, 
                    c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate, c.damage_absorption_rate, 
                    c.ignore_damage_absorption_rate, c.absorbed_damage_rate, c.vitality_regeneration_rate, 
                    c.vitality_regeneration_resistance_rate, c.accuracy_rate, c.lifesteal_rate, c.shield_strength, 
                    c.tenacity, c.resistance_rate, c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, 
                    c.combo_resistance_rate, c.stun_rate, c.ignore_stun_rate, c.reflection_rate, c.ignore_reflection_rate, 
                    c.reflection_damage_rate, c.reflection_resistance_rate, c.mana, c.mana_regeneration_rate, 
                    c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate, c.damage_to_same_faction_rate, 
                    c.resistance_to_same_faction_rate, c.normal_damage_rate, c.normal_resistance_rate, c.skill_damage_rate, c.skill_resistance_rate
            FROM user_card_admirals uc
            INNER JOIN card_admirals c ON uc.card_admiral_id = c.id
            LEFT JOIN (SELECT user_card_admiral_id, SUM(current_multiplier) AS total_module_mult FROM user_card_admirals_module GROUP BY user_card_admiral_id) am ON uc.card_admiral_id = am.user_card_admiral_id
            LEFT JOIN (SELECT user_card_admiral_id, SUM(current_multiplier) AS total_upgrade_mult FROM user_card_admirals_upgrade GROUP BY user_card_admiral_id) au ON uc.card_admiral_id = au.user_card_admiral_id
            LEFT JOIN teams t ON t.team_id = uc.team_id
            WHERE uc.user_id = @userId AND t.is_main = 1 AND c.is_active = TRUE AND c.is_deleted = FALSE

            UNION ALL

            SELECT 'CARD_MONSTERS' AS object_type, uc.card_monster_id AS card_id, uc.level, uc.quality, uc.star, uc.rare, uc.team_id, uc.position, 
                    COALESCE(am.total_module_mult, 0), COALESCE(au.total_upgrade_mult, 0),
                    c.power, c.health, c.physical_attack, c.physical_defense, c.magical_attack, c.magical_defense, 
                    c.chemical_attack, c.chemical_defense, c.atomic_attack, c.atomic_defense, c.mental_attack, c.mental_defense, 
                    c.speed, c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate, 
                    c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate, c.damage_absorption_rate, 
                    c.ignore_damage_absorption_rate, c.absorbed_damage_rate, c.vitality_regeneration_rate, 
                    c.vitality_regeneration_resistance_rate, c.accuracy_rate, c.lifesteal_rate, c.shield_strength, 
                    c.tenacity, c.resistance_rate, c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, 
                    c.combo_resistance_rate, c.stun_rate, c.ignore_stun_rate, c.reflection_rate, c.ignore_reflection_rate, 
                    c.reflection_damage_rate, c.reflection_resistance_rate, c.mana, c.mana_regeneration_rate, 
                    c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate, c.damage_to_same_faction_rate, 
                    c.resistance_to_same_faction_rate, c.normal_damage_rate, c.normal_resistance_rate, c.skill_damage_rate, c.skill_resistance_rate
            FROM user_card_monsters uc
            INNER JOIN card_monsters c ON uc.card_monster_id = c.id
            LEFT JOIN (SELECT user_card_monster_id, SUM(current_multiplier) AS total_module_mult FROM user_card_monsters_module GROUP BY user_card_monster_id) am ON uc.card_monster_id = am.user_card_monster_id
            LEFT JOIN (SELECT user_card_monster_id, SUM(current_multiplier) AS total_upgrade_mult FROM user_card_monsters_upgrade GROUP BY user_card_monster_id) au ON uc.card_monster_id = au.user_card_monster_id
            LEFT JOIN teams t ON t.team_id = uc.team_id
            WHERE uc.user_id = @userId AND t.is_main = 1 AND c.is_active = TRUE AND c.is_deleted = FALSE

            UNION ALL

            SELECT 'CARD_MILITARIES' AS object_type, uc.card_military_id AS card_id, uc.level, uc.quality, uc.star, uc.rare, uc.team_id, uc.position, 
                    COALESCE(am.total_module_mult, 0), COALESCE(au.total_upgrade_mult, 0),
                    c.power, c.health, c.physical_attack, c.physical_defense, c.magical_attack, c.magical_defense, 
                    c.chemical_attack, c.chemical_defense, c.atomic_attack, c.atomic_defense, c.mental_attack, c.mental_defense, 
                    c.speed, c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate, 
                    c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate, c.damage_absorption_rate, 
                    c.ignore_damage_absorption_rate, c.absorbed_damage_rate, c.vitality_regeneration_rate, 
                    c.vitality_regeneration_resistance_rate, c.accuracy_rate, c.lifesteal_rate, c.shield_strength, 
                    c.tenacity, c.resistance_rate, c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, 
                    c.combo_resistance_rate, c.stun_rate, c.ignore_stun_rate, c.reflection_rate, c.ignore_reflection_rate, 
                    c.reflection_damage_rate, c.reflection_resistance_rate, c.mana, c.mana_regeneration_rate, 
                    c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate, c.damage_to_same_faction_rate, 
                    c.resistance_to_same_faction_rate, c.normal_damage_rate, c.normal_resistance_rate, c.skill_damage_rate, c.skill_resistance_rate
            FROM user_card_militaries uc
            INNER JOIN card_militaries c ON uc.card_military_id = c.id
            LEFT JOIN (SELECT user_card_military_id, SUM(current_multiplier) AS total_module_mult FROM user_card_militaries_module GROUP BY user_card_military_id) am ON uc.card_military_id = am.user_card_military_id
            LEFT JOIN (SELECT user_card_military_id, SUM(current_multiplier) AS total_upgrade_mult FROM user_card_militaries_upgrade GROUP BY user_card_military_id) au ON uc.card_military_id = au.user_card_military_id
            LEFT JOIN teams t ON t.team_id = uc.team_id
            WHERE uc.user_id = @userId AND t.is_main = 1 AND c.is_active = TRUE AND c.is_deleted = FALSE

            UNION ALL

            SELECT 'CARD_SOLDIERS' AS object_type, uc.card_soldier_id AS card_id, uc.level, uc.quality, uc.star, uc.rare, uc.team_id, uc.position, 
                    COALESCE(am.total_module_mult, 0), COALESCE(au.total_upgrade_mult, 0),
                    c.power, c.health, c.physical_attack, c.physical_defense, c.magical_attack, c.magical_defense, 
                    c.chemical_attack, c.chemical_defense, c.atomic_attack, c.atomic_defense, c.mental_attack, c.mental_defense, 
                    c.speed, c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate, 
                    c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate, c.damage_absorption_rate, 
                    c.ignore_damage_absorption_rate, c.absorbed_damage_rate, c.vitality_regeneration_rate, 
                    c.vitality_regeneration_resistance_rate, c.accuracy_rate, c.lifesteal_rate, c.shield_strength, 
                    c.tenacity, c.resistance_rate, c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, 
                    c.combo_resistance_rate, c.stun_rate, c.ignore_stun_rate, c.reflection_rate, c.ignore_reflection_rate, 
                    c.reflection_damage_rate, c.reflection_resistance_rate, c.mana, c.mana_regeneration_rate, 
                    c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate, c.damage_to_same_faction_rate, 
                    c.resistance_to_same_faction_rate, c.normal_damage_rate, c.normal_resistance_rate, c.skill_damage_rate, c.skill_resistance_rate
            FROM user_card_soldiers uc
            INNER JOIN card_soldiers c ON uc.card_soldier_id = c.id
            LEFT JOIN (SELECT user_card_soldier_id, SUM(current_multiplier) AS total_module_mult FROM user_card_soldiers_module GROUP BY user_card_soldier_id) am ON uc.card_soldier_id = am.user_card_soldier_id
            LEFT JOIN (SELECT user_card_soldier_id, SUM(current_multiplier) AS total_upgrade_mult FROM user_card_soldiers_upgrade GROUP BY user_card_soldier_id) au ON uc.card_soldier_id = au.user_card_soldier_id
            LEFT JOIN teams t ON t.team_id = uc.team_id
            WHERE uc.user_id = @userId AND t.is_main = 1 AND c.is_active = TRUE AND c.is_deleted = FALSE

            UNION ALL

            SELECT 'CARD_SPELLS' AS object_type, uc.card_spell_id AS card_id, uc.level, uc.quality, uc.star, uc.rare, uc.team_id, uc.position, 
                    COALESCE(am.total_module_mult, 0), COALESCE(au.total_upgrade_mult, 0),
                    c.power, c.health, c.physical_attack, c.physical_defense, c.magical_attack, c.magical_defense, 
                    c.chemical_attack, c.chemical_defense, c.atomic_attack, c.atomic_defense, c.mental_attack, c.mental_defense, 
                    c.speed, c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate, 
                    c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate, c.damage_absorption_rate, 
                    c.ignore_damage_absorption_rate, c.absorbed_damage_rate, c.vitality_regeneration_rate, 
                    c.vitality_regeneration_resistance_rate, c.accuracy_rate, c.lifesteal_rate, c.shield_strength, 
                    c.tenacity, c.resistance_rate, c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, 
                    c.combo_resistance_rate, c.stun_rate, c.ignore_stun_rate, c.reflection_rate, c.ignore_reflection_rate, 
                    c.reflection_damage_rate, c.reflection_resistance_rate, c.mana, c.mana_regeneration_rate, 
                    c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate, c.damage_to_same_faction_rate, 
                    c.resistance_to_same_faction_rate, c.normal_damage_rate, c.normal_resistance_rate, c.skill_damage_rate, c.skill_resistance_rate
            FROM user_card_spells uc
            INNER JOIN card_spells c ON uc.card_spell_id = c.id
            LEFT JOIN (SELECT user_card_spell_id, SUM(current_multiplier) AS total_module_mult FROM user_card_spells_module GROUP BY user_card_spell_id) am ON uc.card_spell_id = am.user_card_spell_id
            LEFT JOIN (SELECT user_card_spell_id, SUM(current_multiplier) AS total_upgrade_mult FROM user_card_spells_upgrade GROUP BY user_card_spell_id) au ON uc.card_spell_id = au.user_card_spell_id
            LEFT JOIN teams t ON t.team_id = uc.team_id
            WHERE uc.user_id = @userId AND t.is_main = 1 AND c.is_active = TRUE AND c.is_deleted = FALSE;
        ";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@userId", userId);

            await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Objects card = new Objects
                {
                    Id = reader.GetStringSafe("card_id"),
                    Rarity = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    ObjectType = reader.GetStringSafe("object_type"),
                    Star = reader.GetIntSafe("star"),
                    Level = reader.GetIntSafe("level"),
                    TeamId = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetStringSafe("team_id"),
                    Position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetStringSafe("position"),

                    Power = reader.GetDoubleSafe("power"),
                    Health = reader.GetDoubleSafe("health"),
                    PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                    PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                    MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                    MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                    ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                    ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                    AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                    AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                    MentalAttack = reader.GetDoubleSafe("mental_attack"),
                    MentalDefense = reader.GetDoubleSafe("mental_defense"),
                    Speed = reader.GetDoubleSafe("speed"),
                    CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                    CriticalRate = reader.GetDoubleSafe("critical_rate"),
                    CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                    PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                    EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                    LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                    ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                    Tenacity = reader.GetDoubleSafe("tenacity"),
                    ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                    ComboRate = reader.GetDoubleSafe("combo_rate"),
                    IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                    StunRate = reader.GetDoubleSafe("stun_rate"),
                    IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                    ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                    Mana = reader.GetDoubleSafe("mana"),
                    ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                    Description = reader.GetStringSafe("description"),

                    BaseStats = new BaseStats
                    {
                        Power = reader.GetDoubleSafe("power"),
                        Health = reader.GetDoubleSafe("health"),
                        PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                        PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                        MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                        MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                        ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                        ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                        AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                        AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                        MentalAttack = reader.GetDoubleSafe("mental_attack"),
                        MentalDefense = reader.GetDoubleSafe("mental_defense"),
                        Speed = reader.GetDoubleSafe("speed"),
                        CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                        CriticalRate = reader.GetDoubleSafe("critical_rate"),
                        CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                        IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                        PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                        PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                        EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                        DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                        IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                        AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                        VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                        VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                        AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                        LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                        ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                        Tenacity = reader.GetDoubleSafe("tenacity"),
                        ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                        ComboRate = reader.GetDoubleSafe("combo_rate"),
                        IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                        ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                        ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                        StunRate = reader.GetDoubleSafe("stun_rate"),
                        IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                        ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                        IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                        ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                        ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                        Mana = reader.GetDoubleSafe("mana"),
                        ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                        DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                        ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                        DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                        ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                        NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                        NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                        SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                        SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                    }
                };

                // Đọc chuỗi JSON từ Database
                // string emblemsJson = reader.GetStringSafe("emblems_json");

                // if (!string.IsNullOrEmpty(emblemsJson))
                // {
                //     try
                //     {
                //         // Chuyển đổi chuỗi JSON thành List<Emblem> trong C#
                //         cardHero.Emblems = JsonHelper.DeserializeEmblems(emblemsJson);
                //     }
                //     catch
                //     {
                //         // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                //         cardHero.Emblems = new List<Emblems>();
                //     }
                // }

                // string classesJson = reader.GetStringSafe("classes_json");

                // if (!string.IsNullOrEmpty(classesJson))
                // {
                //     try
                //     {
                //         // Chuyển đổi chuỗi JSON thành List<Classes> trong C#
                //         cardHero.Class = JsonHelper.DeserializeClasses(classesJson);
                //     }
                //     catch
                //     {
                //         // Phòng trường hợp Hero không có class, MySQL sinh ra chuỗi "[null]"
                //         cardHero.Class = new Classes();
                //     }
                // }
                UserModules userModule = new UserModules
                {
                    CurrentMultiplier = reader.GetDoubleSafe("module_multiplier"),
                };

                UserUpgrades userUpgrade = new UserUpgrades
                {
                    CurrentMultiplier = reader.GetDoubleSafe("upgrade_multiplier"),
                };

                card.UserModules = userModule;
                card.UserUpgrades = userUpgrade;

                objects.Add(card);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return objects;
    }
    public async Task<bool> InsertUserTeamEmblemsAsync(string user_id, string teamId, int position, EmblemDTO emblemDTO)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync(); // mở connection async

            string insertSQL = @"
            INSERT INTO TEAM_EMBLEMS (user_id, team_id, position, card_type, emblem_id, emblem_quantity) 
            VALUES (@user_id, @team_id, @position, @card_type, @emblem_id, @emblem_quantity)";

            using (var insertCommand = new MySqlCommand(insertSQL, connection))
            {
                insertCommand.Parameters.AddWithValue("@user_id", user_id);
                insertCommand.Parameters.AddWithValue("@team_id", teamId);
                insertCommand.Parameters.AddWithValue("@position", position);
                insertCommand.Parameters.AddWithValue("@card_type", emblemDTO.CardType);
                insertCommand.Parameters.AddWithValue("@emblem_id", emblemDTO.EmblemId);
                insertCommand.Parameters.AddWithValue("@emblem_quantity", emblemDTO.Count);

                await insertCommand.ExecuteNonQueryAsync(); // chạy async
            }
        }

        return true;
    }
    public async Task<bool> DeleteUserTeamEmblemsAsync(string user_id, string teamId, int position, string cardType)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            // Câu lệnh SQL DELETE với các điều kiện WHERE cụ thể
            string deleteSQL = @"
            DELETE FROM TEAM_EMBLEMS 
            WHERE user_id = @user_id 
              AND team_id = @team_id 
              AND position = @position 
              AND card_type = @card_type";

            using (var deleteCommand = new MySqlCommand(deleteSQL, connection))
            {
                // Thêm các tham số để tránh SQL Injection
                deleteCommand.Parameters.AddWithValue("@user_id", user_id);
                deleteCommand.Parameters.AddWithValue("@team_id", teamId);
                deleteCommand.Parameters.AddWithValue("@position", position);
                deleteCommand.Parameters.AddWithValue("@card_type", cardType);

                // Thực thi lệnh xóa và lấy số lượng dòng bị ảnh hưởng
                int rowsAffected = await deleteCommand.ExecuteNonQueryAsync();

                // Trả về true nếu có ít nhất một dòng bị xóa, ngược lại false
                return rowsAffected > 0;
            }
        }
    }
    public async Task<bool> UpdateUserCardHeroesTeamPositionsAsync(string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        try
        {
            const string sql = @"
            UPDATE user_card_heroes uch
            JOIN (
                SELECT
                    h.card_hero_id,
                    t.team_id,
                    CONCAT(
                        FLOOR(((h.rn - 1) % 100) / 10) + 1,
                        '-',
                        ((h.rn - 1) % 10) + 1
                    ) AS position
                FROM (
                    SELECT
                        card_hero_id,
                        ROW_NUMBER() OVER (ORDER BY card_hero_id) AS rn
                    FROM user_card_heroes
                    WHERE user_id = @userId
                ) h
                JOIN (
                    SELECT
                        team_id,
                        ROW_NUMBER() OVER (ORDER BY team_id) AS team_rn
                    FROM teams
                    WHERE user_id = @userId
                ) t
                    ON FLOOR((h.rn - 1) / 100) + 1 = t.team_rn
            ) x
            ON uch.card_hero_id = x.card_hero_id
            AND uch.user_id = @userId
            SET
                uch.team_id = x.team_id,
                uch.position = x.position;";

            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                await using var command = new MySqlCommand(sql, connection, (MySqlTransaction)transaction);
                command.Parameters.AddWithValue("@userId", userId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                Console.WriteLine($"Updated {rowsAffected} rows for user {userId}");
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"MySQL Error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> UpdateUserCardCaptainsTeamPositionsAsync(string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        try
        {
            const string sql = @"
            UPDATE user_card_captains uch
            JOIN (
                SELECT
                    h.card_captain_id,
                    t.team_id,
                    CONCAT(
                        FLOOR(((h.rn - 1) % 100) / 10) + 1,
                        '-',
                        ((h.rn - 1) % 10) + 1
                    ) AS position
                FROM (
                    SELECT
                        card_captain_id,
                        ROW_NUMBER() OVER (ORDER BY card_captain_id) AS rn
                    FROM user_card_captains
                    WHERE user_id = @userId
                ) h
                JOIN (
                    SELECT
                        team_id,
                        ROW_NUMBER() OVER (ORDER BY team_id) AS team_rn
                    FROM teams
                    WHERE user_id = @userId
                ) t
                    ON FLOOR((h.rn - 1) / 100) + 1 = t.team_rn
            ) x
            ON uch.card_captain_id = x.card_captain_id
            AND uch.user_id = @userId
            SET
                uch.team_id = x.team_id,
                uch.position = x.position;";

            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                await using var command = new MySqlCommand(sql, connection, (MySqlTransaction)transaction);
                command.Parameters.AddWithValue("@userId", userId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                Console.WriteLine($"Updated {rowsAffected} rows for user {userId}");
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"MySQL Error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> UpdateUserCardColonelsTeamPositionsAsync(string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        try
        {
            const string sql = @"
            UPDATE user_card_colonels uch
            JOIN (
                SELECT
                    h.card_colonel_id,
                    t.team_id,
                    CONCAT(
                        FLOOR(((h.rn - 1) % 100) / 10) + 1,
                        '-',
                        ((h.rn - 1) % 10) + 1
                    ) AS position
                FROM (
                    SELECT
                        card_colonel_id,
                        ROW_NUMBER() OVER (ORDER BY card_colonel_id) AS rn
                    FROM user_card_colonels
                    WHERE user_id = @userId
                ) h
                JOIN (
                    SELECT
                        team_id,
                        ROW_NUMBER() OVER (ORDER BY team_id) AS team_rn
                    FROM teams
                    WHERE user_id = @userId
                ) t
                    ON FLOOR((h.rn - 1) / 100) + 1 = t.team_rn
            ) x
            ON uch.card_colonel_id = x.card_colonel_id
            AND uch.user_id = @userId
            SET
                uch.team_id = x.team_id,
                uch.position = x.position;";

            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                await using var command = new MySqlCommand(sql, connection, (MySqlTransaction)transaction);
                command.Parameters.AddWithValue("@userId", userId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                Console.WriteLine($"Updated {rowsAffected} rows for user {userId}");
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"MySQL Error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> UpdateUserCardGeneralsTeamPositionsAsync(string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        try
        {
            const string sql = @"
            UPDATE user_card_generals uch
            JOIN (
                SELECT
                    h.card_general_id,
                    t.team_id,
                    CONCAT(
                        FLOOR(((h.rn - 1) % 100) / 10) + 1,
                        '-',
                        ((h.rn - 1) % 10) + 1
                    ) AS position
                FROM (
                    SELECT
                        card_general_id,
                        ROW_NUMBER() OVER (ORDER BY card_general_id) AS rn
                    FROM user_card_generals
                    WHERE user_id = @userId
                ) h
                JOIN (
                    SELECT
                        team_id,
                        ROW_NUMBER() OVER (ORDER BY team_id) AS team_rn
                    FROM teams
                    WHERE user_id = @userId
                ) t
                    ON FLOOR((h.rn - 1) / 100) + 1 = t.team_rn
            ) x
            ON uch.card_general_id = x.card_general_id
            AND uch.user_id = @userId
            SET
                uch.team_id = x.team_id,
                uch.position = x.position;";

            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                await using var command = new MySqlCommand(sql, connection, (MySqlTransaction)transaction);
                command.Parameters.AddWithValue("@userId", userId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                Console.WriteLine($"Updated {rowsAffected} rows for user {userId}");
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"MySQL Error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> UpdateUserCardAdmiralsTeamPositionsAsync(string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        try
        {
            const string sql = @"
            UPDATE user_card_admirals uch
            JOIN (
                SELECT
                    h.card_admiral_id,
                    t.team_id,
                    CONCAT(
                        FLOOR(((h.rn - 1) % 100) / 10) + 1,
                        '-',
                        ((h.rn - 1) % 10) + 1
                    ) AS position
                FROM (
                    SELECT
                        card_admiral_id,
                        ROW_NUMBER() OVER (ORDER BY card_admiral_id) AS rn
                    FROM user_card_admirals
                    WHERE user_id = @userId
                ) h
                JOIN (
                    SELECT
                        team_id,
                        ROW_NUMBER() OVER (ORDER BY team_id) AS team_rn
                    FROM teams
                    WHERE user_id = @userId
                ) t
                    ON FLOOR((h.rn - 1) / 100) + 1 = t.team_rn
            ) x
            ON uch.card_admiral_id = x.card_admiral_id
            AND uch.user_id = @userId
            SET
                uch.team_id = x.team_id,
                uch.position = x.position;";

            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                await using var command = new MySqlCommand(sql, connection, (MySqlTransaction)transaction);
                command.Parameters.AddWithValue("@userId", userId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                Console.WriteLine($"Updated {rowsAffected} rows for user {userId}");
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"MySQL Error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> UpdateUserCardMonstersTeamPositionsAsync(string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        try
        {
            const string sql = @"
            UPDATE user_card_monsters uch
            JOIN (
                SELECT
                    h.card_monster_id,
                    t.team_id,
                    CONCAT(
                        FLOOR(((h.rn - 1) % 100) / 10) + 1,
                        '-',
                        ((h.rn - 1) % 10) + 1
                    ) AS position
                FROM (
                    SELECT
                        card_monster_id,
                        ROW_NUMBER() OVER (ORDER BY card_monster_id) AS rn
                    FROM user_card_monsters
                    WHERE user_id = @userId
                ) h
                JOIN (
                    SELECT
                        team_id,
                        ROW_NUMBER() OVER (ORDER BY team_id) AS team_rn
                    FROM teams
                    WHERE user_id = @userId
                ) t
                    ON FLOOR((h.rn - 1) / 100) + 1 = t.team_rn
            ) x
            ON uch.card_monster_id = x.card_monster_id
            AND uch.user_id = @userId
            SET
                uch.team_id = x.team_id,
                uch.position = x.position;";

            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                await using var command = new MySqlCommand(sql, connection, (MySqlTransaction)transaction);
                command.Parameters.AddWithValue("@userId", userId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                Console.WriteLine($"Updated {rowsAffected} rows for user {userId}");
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"MySQL Error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> UpdateUserCardMilitariesTeamPositionsAsync(string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        try
        {
            const string sql = @"
            UPDATE user_card_militaries uch
            JOIN (
                SELECT
                    h.card_military_id,
                    t.team_id,
                    CONCAT(
                        FLOOR(((h.rn - 1) % 100) / 10) + 1,
                        '-',
                        ((h.rn - 1) % 10) + 1
                    ) AS position
                FROM (
                    SELECT
                        card_military_id,
                        ROW_NUMBER() OVER (ORDER BY card_military_id) AS rn
                    FROM user_card_militaries
                    WHERE user_id = @userId
                ) h
                JOIN (
                    SELECT
                        team_id,
                        ROW_NUMBER() OVER (ORDER BY team_id) AS team_rn
                    FROM teams
                    WHERE user_id = @userId
                ) t
                    ON FLOOR((h.rn - 1) / 100) + 1 = t.team_rn
            ) x
            ON uch.card_military_id = x.card_military_id
            AND uch.user_id = @userId
            SET
                uch.team_id = x.team_id,
                uch.position = x.position;";

            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                await using var command = new MySqlCommand(sql, connection, (MySqlTransaction)transaction);
                command.Parameters.AddWithValue("@userId", userId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                Console.WriteLine($"Updated {rowsAffected} rows for user {userId}");
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"MySQL Error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> UpdateUserCardSoldiersTeamPositionsAsync(string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        try
        {
            const string sql = @"
            UPDATE user_card_soldiers uch
            JOIN (
                SELECT
                    h.card_soldier_id,
                    t.team_id,
                    CONCAT(
                        FLOOR(((h.rn - 1) % 100) / 10) + 1,
                        '-',
                        ((h.rn - 1) % 10) + 1
                    ) AS position
                FROM (
                    SELECT
                        card_soldier_id,
                        ROW_NUMBER() OVER (ORDER BY card_soldier_id) AS rn
                    FROM user_card_soldiers
                    WHERE user_id = @userId
                ) h
                JOIN (
                    SELECT
                        team_id,
                        ROW_NUMBER() OVER (ORDER BY team_id) AS team_rn
                    FROM teams
                    WHERE user_id = @userId
                ) t
                    ON FLOOR((h.rn - 1) / 100) + 1 = t.team_rn
            ) x
            ON uch.card_soldier_id = x.card_soldier_id
            AND uch.user_id = @userId
            SET
                uch.team_id = x.team_id,
                uch.position = x.position;";

            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                await using var command = new MySqlCommand(sql, connection, (MySqlTransaction)transaction);
                command.Parameters.AddWithValue("@userId", userId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                Console.WriteLine($"Updated {rowsAffected} rows for user {userId}");
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"MySQL Error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> UpdateUserCardSpellsTeamPositionsAsync(string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        try
        {
            const string sql = @"
            UPDATE user_card_spells uch
            JOIN (
                SELECT
                    h.card_spell_id,
                    t.team_id,
                    CONCAT(
                        FLOOR(((h.rn - 1) % 100) / 10) + 1,
                        '-',
                        ((h.rn - 1) % 10) + 1
                    ) AS position
                FROM (
                    SELECT
                        card_spell_id,
                        ROW_NUMBER() OVER (ORDER BY card_spell_id) AS rn
                    FROM user_card_spells
                    WHERE user_id = @userId
                ) h
                JOIN (
                    SELECT
                        team_id,
                        ROW_NUMBER() OVER (ORDER BY team_id) AS team_rn
                    FROM teams
                    WHERE user_id = @userId
                ) t
                    ON FLOOR((h.rn - 1) / 100) + 1 = t.team_rn
            ) x
            ON uch.card_spell_id = x.card_spell_id
            AND uch.user_id = @userId
            SET
                uch.team_id = x.team_id,
                uch.position = x.position;";

            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                await using var command = new MySqlCommand(sql, connection, (MySqlTransaction)transaction);
                command.Parameters.AddWithValue("@userId", userId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                Console.WriteLine($"Updated {rowsAffected} rows for user {userId}");
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"MySQL Error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
            return false;
        }
    }
}