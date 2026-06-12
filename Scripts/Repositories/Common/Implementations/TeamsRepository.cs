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