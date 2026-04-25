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

                string selectSQL = "SELECT * FROM users WHERE username = @username";
                await using (var selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@username", username);

                    await using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User
                            {
                                Id = reader["id"].ToString(),
                                Username = reader.GetStringSafe("username"),
                                Password = reader.GetStringSafe("password"),
                                Name = reader["name"].ToString(),
                                Level = reader.GetIntSafe("level"),
                                Experiment = reader.GetDoubleSafe("experiment"),
                                Vip = reader.GetIntSafe("vip"),
                                Power = reader.GetDoubleSafe("power")
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
            string checkSQL = "SELECT COUNT(*) FROM Users WHERE username = @username";
            using (var checkCommand = new MySqlCommand(checkSQL, connection))
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
            string selectSQL = @"
            INSERT INTO users (id, username, password, name, level, experiment, vip, power) 
            VALUES (@id, @username, @password, @name, @level, @experiment, @vip, @power)";

            using (var selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@id", userId);
                selectCommand.Parameters.AddWithValue("@username", username);
                selectCommand.Parameters.AddWithValue("@password", password);
                selectCommand.Parameters.AddWithValue("@name", "");
                selectCommand.Parameters.AddWithValue("@level", 1);
                selectCommand.Parameters.AddWithValue("@experiment", 0);
                selectCommand.Parameters.AddWithValue("@vip", 0);
                selectCommand.Parameters.AddWithValue("@power", 0);

                try
                {
                    await selectCommand.ExecuteNonQueryAsync(); // chạy selectSQL async
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
            string selectSQL = "SELECT * FROM Users WHERE username = @username AND password = @password";
            using (var selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@username", username);
                selectCommand.Parameters.AddWithValue("@password", password);

                using (var reader = await selectCommand.ExecuteReaderAsync())
                {
                    if (!await reader.ReadAsync())
                        return null; // đăng nhập thất bại

                    string userId = reader.GetStringSafe("id");
                    string name = reader.GetStringSafe("name");
                    string Username = reader.GetStringSafe("username");
                    string Password = reader.GetStringSafe("password");
                    int level = reader.GetIntSafe("level");
                    int vip = reader.GetIntSafe("vip");
                    double power = reader.GetDoubleSafe("power");
                    double experiment = reader.GetDoubleSafe("experiment");

                    // Cập nhật các biến static của User
                    User.CurrentUserId = userId;
                    User.CurrentUserName = name;
                    User.SavedUsername = Username;
                    User.SavedPassword = Password;
                    User.CurrentUserLevel = level;
                    User.CurrentUserPower = power;

                    reader.Close(); // đóng reader trước khi truy vấn khác

                    // --- Lấy thông tin user_currencies ---
                    string currencyQuery = @"
                    SELECT c.image, c.name, uc.currency_id, uc.quantity 
                    FROM user_currencies uc
                    JOIN currencies c ON uc.currency_id = c.id
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
                                    Id = currencyReader.GetStringSafe("currency_id"),
                                    Name = currencyReader.GetStringSafe("name"),
                                    Image = currencyReader.GetStringSafe("image"),
                                    Quantity = currencyReader.GetDoubleSafe("quantity")
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
            string selectSQL = "SELECT * FROM Users WHERE id = @id";
            using (var selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@id", userId);

                using (var reader = await selectCommand.ExecuteReaderAsync())
                {
                    if (!await reader.ReadAsync())
                        return null; // không tìm thấy user

                    string id = reader.GetStringSafe("id");
                    string name = reader.GetStringSafe("name");
                    string username = reader.GetStringSafe("username");
                    string password = reader.GetStringSafe("password");
                    int level = reader.GetIntSafe("level");
                    int vip = reader.GetIntSafe("vip");
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

                    // --- Lấy thông tin user_currencies ---
                    string currencyQuery = @"
                    SELECT c.image, c.name, uc.currency_id, uc.quantity 
                    FROM user_currencies uc
                    JOIN currencies c ON uc.currency_id = c.id
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
                                    Id = currencyReader.GetStringSafe("currency_id"),
                                    Name = currencyReader.GetStringSafe("name"),
                                    Image = currencyReader.GetStringSafe("image"),
                                    Quantity = currencyReader.GetDoubleSafe("quantity")
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
            string selectSQL = "SELECT * FROM Users WHERE id=@id";
            using (var selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@id", Id);

                using (var reader = await selectCommand.ExecuteReaderAsync())
                {
                    if (!await reader.ReadAsync())
                        return null; // không tìm thấy user

                    string userId = reader.GetStringSafe("id");
                    string Name = reader.GetStringSafe("name");
                    string username = reader.GetStringSafe("username");
                    string password = reader.GetStringSafe("password");
                    int Level = reader.GetIntSafe("level");
                    int Vip = reader.GetIntSafe("vip");
                    double Experiment = reader.GetDoubleSafe("experiment");

                    double Power = await TeamsService.Create().GetTeamsPowerAsync(Id);

                    reader.Close(); // đóng reader trước khi truy vấn khác

                    // --- Lấy thông tin user_currencies ---
                    string currencyQuery = @"SELECT c.image, c.name, uc.currency_id, uc.quantity 
                                         FROM user_currencies uc
                                         JOIN currencies c ON uc.currency_id = c.id
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
                                    Id = currencyReader.GetStringSafe("currency_id"),
                                    Name = currencyReader.GetStringSafe("name"),
                                    Image = currencyReader.GetStringSafe("image"),
                                    Quantity = currencyReader.GetIntSafe("quantity")
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

                string updateSQL = "UPDATE Users SET name = @name WHERE id = @id";
                await using (var updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@name", new_name);
                    updateCommand.Parameters.AddWithValue("@id", user_id);

                    await updateCommand.ExecuteNonQueryAsync(); // chạy selectSQL async
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
    public async Task UpdateUserPowerAsync(string user_id, double power)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync(); // mở kết nối async

                string updateSQL = "UPDATE Users SET power = @power WHERE id = @id";
                await using (var updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@power", power);
                    updateCommand.Parameters.AddWithValue("@id", user_id);

                    await updateCommand.ExecuteNonQueryAsync(); // chạy selectSQL async
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

                for (int currencyId = 1; currencyId <= 70; currencyId++)
                {
                    string insertSQL = "INSERT INTO user_currencies (user_id, currency_id, quantity) VALUES (@id, @currency_id, @quantity)";
                    await using (var insertCommand = new MySqlCommand(insertSQL, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@id", Id);
                        insertCommand.Parameters.AddWithValue("@currency_id", currencyId);
                        insertCommand.Parameters.AddWithValue("@quantity", 1000000000);

                        await insertCommand.ExecuteNonQueryAsync(); // insert async
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

                string selectSQL = "SELECT COUNT(*) FROM users WHERE name = @name";
                await using (var selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@name", name);

                    object result = await selectCommand.ExecuteScalarAsync(); // chạy selectSQL async
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