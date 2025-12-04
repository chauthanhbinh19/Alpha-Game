using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;
using UnityEditor.MemoryProfiler;

public class UserRepository : IUserRepository
{
    public async Task<User> GetUserByUsernameAsync(string username)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM users WHERE username = @username";
                await using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User
                            {
                                Id = reader["id"].ToString(),
                                Username = reader.GetString("username"),
                                Password = reader.GetString("password"),
                                Name = reader["name"].ToString(),
                                Level = reader.GetInt32("level"),
                                Experiment = reader.GetInt32("experiment"),
                                Vip = reader.GetInt32("vip"),
                                Power = reader.GetDouble("power")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return null;
    }
    public async Task<string> RegisterUserAsync(string username, string password)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync(); // mở connection async

            // --- Kiểm tra username đã tồn tại ---
            string checkQuery = "SELECT COUNT(*) FROM Users WHERE username = @username";
            using (var checkCommand = new MySqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@username", username);

                object result = await checkCommand.ExecuteScalarAsync();
                int count = Convert.ToInt32(result);

                if (count > 0)
                {
                    return null; // username đã tồn tại
                }
            }

            // --- Tạo user mới ---
            string userId = DateTime.Now.Ticks.ToString();
            string query = @"
            INSERT INTO users (id, username, password, name, level, experiment, vip, power) 
            VALUES (@id, @username, @password, @name, @level, @experiment, @vip, @power)";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", userId);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@name", "");
                command.Parameters.AddWithValue("@level", 1);
                command.Parameters.AddWithValue("@experiment", 0);
                command.Parameters.AddWithValue("@vip", 0);
                command.Parameters.AddWithValue("@power", 0);

                try
                {
                    await command.ExecuteNonQueryAsync(); // chạy query async
                    Debug.Log("User registered successfully!");
                    return userId;
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error while registering user: " + ex.Message);
                    return null;
                }
            }
        }
    }
    public async Task<User> SignInWithUsernameAndPasswordAsync(string username, string password)
    {
        if (string.IsNullOrEmpty(username)) username = User.SavedUsername;
        if (string.IsNullOrEmpty(password)) password = User.SavedPassword;

        string connectionString = DatabaseConfig.ConnectionString;

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync(); // mở connection async

            // --- Lấy thông tin user ---
            string userQuery = "SELECT * FROM Users WHERE username = @username AND password = @password";
            using (var userCommand = new MySqlCommand(userQuery, connection))
            {
                userCommand.Parameters.AddWithValue("@username", username);
                userCommand.Parameters.AddWithValue("@password", password);

                using (var reader = await userCommand.ExecuteReaderAsync())
                {
                    if (!await reader.ReadAsync())
                        return null; // đăng nhập thất bại

                    string userId = reader.GetString("id");
                    string name = reader.GetString("name");
                    string Username = reader.GetString("username");
                    string Password = reader.GetString("password");
                    int level = reader.GetInt32("level");
                    int vip = reader.GetInt32("vip");
                    double power = reader.GetDouble("power");
                    double experiment = reader.GetDouble("experiment");

                    // Cập nhật các biến static của User
                    User.CurrentUserId = userId;
                    User.CurrentUserName = name;
                    User.SavedUsername = Username;
                    User.SavedPassword = Password;
                    User.CurrentUserLevel = level;
                    User.CurrentUserPower = power;

                    reader.Close(); // đóng reader trước khi truy vấn khác

                    // --- Lấy thông tin user_currency ---
                    string currencyQuery = @"
                    SELECT c.image, c.name, uc.currency_id, uc.quantity 
                    FROM user_currency uc
                    JOIN currency c ON uc.currency_id = c.id
                    WHERE uc.user_id = @userId";

                    using (var currencyCommand = new MySqlCommand(currencyQuery, connection))
                    {
                        currencyCommand.Parameters.AddWithValue("@userId", userId);

                        using (var currencyReader = await currencyCommand.ExecuteReaderAsync())
                        {
                            var currencies = new List<Currencies>();
                            while (await currencyReader.ReadAsync())
                            {
                                currencies.Add(new Currencies
                                {
                                    Id = currencyReader.GetString("currency_id"),
                                    Name = currencyReader.GetString("name"),
                                    Image = currencyReader.GetString("image"),
                                    Quantity = currencyReader.GetDouble("quantity")
                                });
                            }

                            // --- Tạo object user ---
                            var user = new User
                            {
                                Id = userId,
                                Name = name,
                                Username = username,
                                Password = password,
                                Level = level,
                                Vip = vip,
                                Experiment = experiment,
                                Power = power,
                                Image = "",
                                Border = "",
                                Currencies = currencies
                            };

                            return user;
                        }
                    }
                }
            }
        }
    }
    public async Task<User> SignInWithoutUsernameAndPasswordAsync(string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync(); // mở connection async

            // --- Lấy thông tin user ---
            string userQuery = "SELECT * FROM Users WHERE id = @id";
            using (var userCommand = new MySqlCommand(userQuery, connection))
            {
                userCommand.Parameters.AddWithValue("@id", userId);

                using (var reader = await userCommand.ExecuteReaderAsync())
                {
                    if (!await reader.ReadAsync())
                        return null; // không tìm thấy user

                    string id = reader.GetString("id");
                    string name = reader.GetString("name");
                    string username = reader.GetString("username");
                    string password = reader.GetString("password");
                    int level = reader.GetInt32("level");
                    int vip = reader.GetInt32("vip");
                    double power = reader["power"] != DBNull.Value ? Convert.ToDouble(reader["power"]) : 0;
                    double experiment = reader["experiment"] != DBNull.Value ? Convert.ToDouble(reader["experiment"]) : 0;

                    // Cập nhật các biến static của User
                    User.CurrentUserId = userId;
                    User.CurrentUserName = name;
                    User.SavedUsername = username;
                    User.SavedPassword = password;
                    User.CurrentUserLevel = level;
                    User.CurrentUserPower = power;

                    reader.Close(); // đóng reader trước khi thực hiện truy vấn khác

                    // --- Lấy thông tin user_currency ---
                    string currencyQuery = @"
                    SELECT c.image, c.name, uc.currency_id, uc.quantity 
                    FROM user_currency uc
                    JOIN currency c ON uc.currency_id = c.id
                    WHERE uc.user_id = @userId";

                    using (var currencyCommand = new MySqlCommand(currencyQuery, connection))
                    {
                        currencyCommand.Parameters.AddWithValue("@userId", userId);

                        using (var currencyReader = await currencyCommand.ExecuteReaderAsync())
                        {
                            var currencies = new List<Currencies>();
                            while (await currencyReader.ReadAsync())
                            {
                                currencies.Add(new Currencies
                                {
                                    Id = currencyReader.GetString("currency_id"),
                                    Name = currencyReader.GetString("name"),
                                    Image = currencyReader.GetString("image"),
                                    Quantity = currencyReader.GetDouble("quantity")
                                });
                            }

                            // --- Tạo object user ---
                            var user = new User
                            {
                                Id = userId,
                                Name = name,
                                Username = username,
                                Password = password,
                                Level = level,
                                Vip = vip,
                                Experiment = experiment,
                                Power = power,
                                Image = "",
                                Border = "",
                                Currencies = currencies
                            };

                            return user;
                        }
                    }
                }
            }
        }
    }
    public async Task<User> GetUserByIdAsync(string Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync(); // mở kết nối async

            // --- Lấy thông tin user ---
            string userQuery = "SELECT * FROM Users WHERE id=@id";
            using (var userCommand = new MySqlCommand(userQuery, connection))
            {
                userCommand.Parameters.AddWithValue("@id", Id);

                using (var reader = await userCommand.ExecuteReaderAsync())
                {
                    if (!await reader.ReadAsync())
                        return null; // không tìm thấy user

                    string userId = reader.GetString("id");
                    string Name = reader.GetString("name");
                    string username = reader.GetString("username");
                    string password = reader.GetString("password");
                    int Level = reader.GetInt32("level");
                    int Vip = reader.GetInt32("vip");
                    int Experiment = reader.GetInt32("experiment");

                    double Power = await TeamsService.Create().GetTeamsPowerAsync(Id);

                    reader.Close(); // đóng reader trước khi truy vấn khác

                    // --- Lấy thông tin user_currency ---
                    string currencyQuery = @"SELECT c.image, c.name, uc.currency_id, uc.quantity 
                                         FROM user_currency uc
                                         JOIN currency c ON uc.currency_id = c.id
                                         WHERE uc.user_id = @userId";

                    using (var currencyCommand = new MySqlCommand(currencyQuery, connection))
                    {
                        currencyCommand.Parameters.AddWithValue("@userId", userId);

                        using (var currencyReader = await currencyCommand.ExecuteReaderAsync())
                        {
                            var currencies = new List<Currencies>();
                            while (await currencyReader.ReadAsync())
                            {
                                currencies.Add(new Currencies
                                {
                                    Id = currencyReader.GetString("currency_id"),
                                    Name = currencyReader.GetString("name"),
                                    Image = currencyReader.GetString("image"),
                                    Quantity = currencyReader.GetInt32("quantity")
                                });
                            }

                            // --- Tạo object user ---
                            var user = new User
                            {
                                Id = userId,
                                Name = Name,
                                Username = username,
                                Password = password,
                                Level = Level,
                                Vip = Vip,
                                Experiment = Experiment,
                                Power = Power,
                                Image = "",
                                Border = "",
                                Currencies = currencies
                            };

                            return user;
                        }
                    }
                }
            }
        }
    }
    public async Task UpdateUserNameAsync(string user_id, string new_name)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync(); // mở kết nối async

                string updateQuery = "UPDATE Users SET name = @name WHERE id = @id";
                await using (var command = new MySqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@name", new_name);
                    command.Parameters.AddWithValue("@id", user_id);

                    await command.ExecuteNonQueryAsync(); // chạy query async
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        // Thao tác UI / logic sau khi update
        AuthenticationManager.Instance.DeleteCreateNamePanel();
    }
    public async Task UpdateUserPowerAsync(string user_id, double power)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync(); // mở kết nối async

                string updateQuery = "UPDATE Users SET power = @power WHERE id = @id";
                await using (var command = new MySqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@power", power);
                    command.Parameters.AddWithValue("@id", user_id);

                    await command.ExecuteNonQueryAsync(); // chạy query async
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task CreateUserCurrencyAsync(string Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync(); // mở connection async

                for (int currencyId = 1; currencyId <= 73; currencyId++)
                {
                    string insertQuery = "INSERT INTO user_currency (user_id, currency_id, quantity) VALUES (@id, @currency_id, @quantity)";
                    await using (var command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", Id);
                        command.Parameters.AddWithValue("@currency_id", currencyId);
                        command.Parameters.AddWithValue("@quantity", 1000000000);

                        await command.ExecuteNonQueryAsync(); // insert async
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task<bool> CheckNameExistsAsync(string name)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync(); // mở connection async

                string query = "SELECT COUNT(*) FROM users WHERE name = @name";
                await using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);

                    object result = await command.ExecuteScalarAsync(); // chạy query async
                    int count = Convert.ToInt32(result);

                    return count > 0; // Nếu > 0 nghĩa là tồn tại Name
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}