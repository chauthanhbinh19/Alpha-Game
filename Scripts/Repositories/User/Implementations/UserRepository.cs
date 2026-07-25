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
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string sql = "SELECT * FROM users WHERE username = @username LIMIT 1";
                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;

                        return new User
                        {
                            Id = reader["id"] != DBNull.Value ? reader["id"].ToString() : "",
                            Name = reader["name"] != DBNull.Value ? reader["name"].ToString() : "",
                            Username = reader["username"] != DBNull.Value ? reader["username"].ToString() : "",
                            Password = reader["password"] != DBNull.Value ? reader["password"].ToString() : "",
                            Level = reader["level"] != DBNull.Value ? Convert.ToInt32(reader["level"]) : 1,
                            Vip = reader["vip"] != DBNull.Value ? Convert.ToInt32(reader["vip"]) : 0,
                            Power = reader["power"] != DBNull.Value ? Convert.ToDouble(reader["power"]) : 0,
                            Experiment = reader["experience"] != DBNull.Value ? Convert.ToDouble(reader["experience"]) : 0,
                            Image = "",
                            Border = ""
                        };
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[GetUserByUsername Exception]: {ex.Message}");
            return null;
        }
    }
    public async Task<AuthResult> RegisterUserAsync(string username, string email, string password)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // 1. Kiểm tra Username hoặc Email đã tồn tại chưa
                string checkSQL = "SELECT username, email FROM Users WHERE username = @username OR email = @email LIMIT 1";
                using (var checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@username", username);
                    checkCommand.Parameters.AddWithValue("@email", email ?? "");

                    using (var reader = await checkCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            string existingUsername = reader.GetStringSafe("username");
                            string existingEmail = reader.GetStringSafe("email");

                            // Trùng Username
                            if (existingUsername.Equals(username, StringComparison.OrdinalIgnoreCase))
                            {
                                return new AuthResult
                                {
                                    Success = false,
                                    ErrorField = AppConstants.MainType.USERNAME,
                                    ErrorMessage = MessageConstants.USERNAME_ALREADY_EXIST, // Hoặc "Tên đăng nhập đã tồn tại!"
                                    User = null
                                };
                            }

                            // Trùng Email
                            if (!string.IsNullOrEmpty(email) && existingEmail.Equals(email, StringComparison.OrdinalIgnoreCase))
                            {
                                return new AuthResult
                                {
                                    Success = false,
                                    ErrorField = AppConstants.MainType.EMAIL,
                                    ErrorMessage = MessageConstants.EMAIL_ALREADY_EXIST,
                                    User = null
                                };
                            }
                        }
                    }
                }

                // 2. Insert User mới vào Database
                string userId = DateTime.Now.Ticks.ToString();
                string insertSQL = @"
                    INSERT INTO Users (id, username, email, password, name, level, experience, vip, power) 
                    VALUES (@id, @username, @email, @password, @name, @level, @experience, @vip, @power)";

                using (var insertCommand = new MySqlCommand(insertSQL, connection))
                {
                    insertCommand.Parameters.AddWithValue("@id", userId);
                    insertCommand.Parameters.AddWithValue("@username", username);
                    insertCommand.Parameters.AddWithValue("@email", email ?? "");
                    insertCommand.Parameters.AddWithValue("@password", password);
                    insertCommand.Parameters.AddWithValue("@name", "");
                    insertCommand.Parameters.AddWithValue("@level", 1);
                    insertCommand.Parameters.AddWithValue("@experience", 0);
                    insertCommand.Parameters.AddWithValue("@vip", 0);
                    insertCommand.Parameters.AddWithValue("@power", 0);

                    await insertCommand.ExecuteNonQueryAsync();
                    Debug.Log($"User [{username}] registered successfully with ID: {userId}");

                    User newUser = new User
                    {
                        Id = userId,
                        Username = username,
                        Name = "",
                        Level = 1,
                        Power = 0
                    };

                    return new AuthResult
                    {
                        Success = true,
                        ErrorField = "",
                        ErrorMessage = "",
                        User = newUser
                    };
                }
            }
        }
        catch (MySqlException ex)
        {
            // Bắt lỗi Unique Index trong MySQL (Mã lỗi 1062)
            if (ex.Number == 1062)
            {
                if (ex.Message.Contains("username"))
                {
                    return new AuthResult
                    {
                        Success = false,
                        ErrorField = AppConstants.MainType.USERNAME,
                        ErrorMessage = MessageConstants.USERNAME_ALREADY_EXIST,
                        User = null
                    };
                }

                return new AuthResult
                {
                    Success = false,
                    ErrorField = AppConstants.MainType.EMAIL,
                    ErrorMessage = "Email đã được sử dụng!",
                    User = null
                };
            }

            Debug.LogError($"[Register MySqlException]: {ex.Message}");
            return new AuthResult
            {
                Success = false,
                ErrorField = "",
                ErrorMessage = "Lỗi kết nối cơ sở dữ liệu!",
                User = null
            };
        }
        catch (Exception ex)
        {
            Debug.LogError($"[Register Exception]: {ex.Message}");
            return new AuthResult
            {
                Success = false,
                ErrorField = "",
                ErrorMessage = "Lỗi hệ thống không xác định!",
                User = null
            };
        }
    }
    public async Task<User> SignInWithUsernameAndPasswordAsync(string username, string password)
    {
        if (string.IsNullOrEmpty(username)) username = User.SavedUsername;
        if (string.IsNullOrEmpty(password)) password = User.SavedPassword;

        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM users WHERE username = @username AND password = @password LIMIT 1";
                using (var selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@username", username);
                    selectCommand.Parameters.AddWithValue("@password", password);

                    using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;

                        User user = new User
                        {
                            Id = reader["id"] != DBNull.Value ? reader["id"].ToString() : "",
                            Name = reader["name"] != DBNull.Value ? reader["name"].ToString() : "",
                            Username = reader["username"] != DBNull.Value ? reader["username"].ToString() : "",
                            Password = reader["password"] != DBNull.Value ? reader["password"].ToString() : "",
                            Level = reader["level"] != DBNull.Value ? Convert.ToInt32(reader["level"]) : 1,
                            Vip = reader["vip"] != DBNull.Value ? Convert.ToInt32(reader["vip"]) : 0,
                            Power = reader["power"] != DBNull.Value ? Convert.ToDouble(reader["power"]) : 0,
                            Experiment = reader["experience"] != DBNull.Value ? Convert.ToDouble(reader["experience"]) : 0,
                            Image = "",
                            Border = ""
                        };

                        // Cập nhật Cache Static
                        User.CurrentUserId = user.Id;
                        User.CurrentUserName = user.Name;
                        User.SavedUsername = user.Username;
                        User.SavedPassword = user.Password;
                        User.CurrentUserLevel = user.Level;
                        User.CurrentUserPower = user.Power;

                        return user;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[SignInWithUsernameAndPassword Exception]: {ex.Message}");
            return null;
        }
    }

    public async Task<User> SignInWithoutUsernameAndPasswordAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId)) return null;

        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM users WHERE id = @id LIMIT 1";
                using (var selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@id", userId);

                    using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;

                        User user = new User
                        {
                            Id = reader["id"] != DBNull.Value ? reader["id"].ToString() : "",
                            Name = reader["name"] != DBNull.Value ? reader["name"].ToString() : "",
                            Username = reader["username"] != DBNull.Value ? reader["username"].ToString() : "",
                            Password = reader["password"] != DBNull.Value ? reader["password"].ToString() : "",
                            Level = reader["level"] != DBNull.Value ? Convert.ToInt32(reader["level"]) : 1,
                            Vip = reader["vip"] != DBNull.Value ? Convert.ToInt32(reader["vip"]) : 0,
                            Power = reader["power"] != DBNull.Value ? Convert.ToDouble(reader["power"]) : 0,
                            Experiment = reader["experience"] != DBNull.Value ? Convert.ToDouble(reader["experience"]) : 0,
                            Image = "",
                            Border = ""
                        };

                        // Cập nhật Cache Static
                        User.CurrentUserId = user.Id;
                        User.CurrentUserName = user.Name;
                        User.SavedUsername = user.Username;
                        User.SavedPassword = user.Password;
                        User.CurrentUserLevel = user.Level;
                        User.CurrentUserPower = user.Power;

                        return user;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[SignInWithoutUsernameAndPassword Exception]: {ex.Message}");
            return null;
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
                    double Experiment = reader.GetDoubleSafe("experience");

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
    public async Task UpdateUserNameAsync(string userId, string new_name)
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
                    updateCommand.Parameters.AddWithValue("@id", userId);

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
    public async Task UpdateUserPowerAsync(string userId, double power)
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
                    updateCommand.Parameters.AddWithValue("@id", userId);

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
    public async Task CreateUserCurrencyAsync(string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync(); // mở connection async

                string insertSQL = @"
                INSERT INTO user_currencies (
                    user_id,
                    currency_id,
                    quantity
                )
                SELECT
                    @userId,
                    c.id,
                    0
                FROM currencies c;";
                await using (var insertCommand = new MySqlCommand(insertSQL, connection))
                {
                    insertCommand.Parameters.AddWithValue("@userId", userId);

                    await insertCommand.ExecuteNonQueryAsync(); // insert async
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