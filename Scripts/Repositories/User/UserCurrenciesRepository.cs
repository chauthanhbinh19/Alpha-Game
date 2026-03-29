using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UserCurrenciesRepository : IUserCurrenciesRepository
{
    public async Task<List<Currencies>> GetUserCurrencyAsync(string userId)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string currencyQuery = @"
                SELECT c.image, c.name, uc.currency_id, uc.quantity
                FROM user_currencies uc
                JOIN currencies c ON uc.currency_id = c.id
                WHERE uc.user_id = @userId";

                await using (MySqlCommand currencyCommand = new MySqlCommand(currencyQuery, connection))
                {
                    currencyCommand.Parameters.AddWithValue("@userId", userId);

                    await using (MySqlDataReader currencyReader = await currencyCommand.ExecuteReaderAsync())
                    {
                        while (await currencyReader.ReadAsync())
                        {
                            string image = currencyReader.GetStringSafe("image");
                            string name = currencyReader.GetStringSafe("name");
                            string currencyId = currencyReader.GetStringSafe("currency_id");
                            double quantity = currencyReader.GetDoubleSafe("quantity");

                            currencies.Add(new Currencies
                            {
                                Id = currencyId,
                                Name = name,
                                Image = image,
                                Quantity = quantity
                            });
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<Currencies> GetUserCurrencyByIdAsync(string id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string currencyQuery = @"
                SELECT c.image, c.name, uc.currency_id, uc.quantity
                FROM user_currencies uc
                JOIN currencies c ON uc.currency_id = c.id
                WHERE uc.user_id = @userId AND c.id = @id";

                await using (MySqlCommand currencyCommand = new MySqlCommand(currencyQuery, connection))
                {
                    currencyCommand.Parameters.AddWithValue("@userId", User.CurrentUserId);
                    currencyCommand.Parameters.AddWithValue("@id", id);

                    await using (MySqlDataReader currencyReader = await currencyCommand.ExecuteReaderAsync())
                    {
                        if (await currencyReader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = currencyReader.GetStringSafe("currency_id"),
                                Name = currencyReader.GetStringSafe("name"),
                                Image = currencyReader.GetStringSafe("image"),
                                Quantity = currencyReader.GetDoubleSafe("quantity")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserCurrencyByNameAsync(string currencyName)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string currencyQuery = @"
                SELECT c.image, c.name, uc.currency_id, uc.quantity
                FROM user_currencies uc
                JOIN currencies c ON uc.currency_id = c.id
                WHERE uc.user_id = @userId AND c.name = @name";

                await using (MySqlCommand currencyCommand = new MySqlCommand(currencyQuery, connection))
                {
                    currencyCommand.Parameters.AddWithValue("@userId", User.CurrentUserId);
                    currencyCommand.Parameters.AddWithValue("@name", currencyName);

                    await using (MySqlDataReader currencyReader = await currencyCommand.ExecuteReaderAsync())
                    {
                        if (await currencyReader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = currencyReader.GetStringSafe("currency_id"),
                                Name = currencyReader.GetStringSafe("name"),
                                Image = currencyReader.GetStringSafe("image"),
                                Quantity = currencyReader.GetDoubleSafe("quantity")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task InitiateUserCurrencyAsync(string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"INSERT INTO user_items (user_id, item_id, quantity)
                SELECT
                    @user_id,
                    i.id,
                    10000000000
                FROM items i
                ON DUPLICATE KEY UPDATE
                    quantity = 10000000000; ";
                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", userId);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task UpdateUserCurrencyAsync(string currency_id, double price)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Lấy quantity hiện tại
                string query = "SELECT quantity FROM user_currencies WHERE user_id = @user_id AND currency_id = @currency_id";
                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@currency_id", currency_id);

                    object result = await command.ExecuteScalarAsync();
                    double currentQuantity = result != null ? Convert.ToDouble(result) : 0;
                    double newQuantity = currentQuantity - price;

                    // Cập nhật quantity mới
                    string updateQuery = "UPDATE user_currencies SET quantity = @quantity WHERE user_id = @user_id AND currency_id = @currency_id";
                    await using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@quantity", newQuantity);
                        updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                        updateCommand.Parameters.AddWithValue("@currency_id", currency_id);

                        await updateCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task<List<Currencies>> GetEquipmentsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity 
                             FROM equipments e
                             JOIN equipment_trade et ON e.id = et.equipment_id
                             JOIN currencies c ON c.id = et.currency_id
                             JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE e.type = @type";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetDoubleSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<Currencies> GetEquipmentsPriceAsync(string type, string equipment_id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, et.price 
                             FROM equipments e
                             JOIN equipment_trade et ON e.id = et.equipment_id
                             JOIN currencies c ON c.id = et.currency_id
                             JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE e.type = @type AND e.id = @equipment_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@equipment_id", equipment_id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetDoubleSafe("price"), // price lưu vào Quantity
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserEquipmentPriceAsync(string type, string equipment_id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM equipments e
                             JOIN equipment_trade et ON e.id = et.equipment_id
                             JOIN currencies c ON c.id = et.currency_id
                             JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE e.type = @type AND e.id = @equipment_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@equipment_id", equipment_id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetDoubleSafe("quantity")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserCardHeroPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM card_heroes ch
                             LEFT JOIN card_hero_trade et ON ch.id = et.card_hero_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id = @id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserCardCaptainPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM card_captains ch
                             LEFT JOIN card_captain_trade et ON ch.id = et.card_captain_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id = @id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserCardColonelPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM card_colonels ch
                             LEFT JOIN card_colonel_trade et ON ch.id = et.card_colonel_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id = @id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserCardGeneralPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM card_generals ch
                             LEFT JOIN card_general_trade et ON ch.id = et.card_general_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id = @id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserCardAdmiralPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM card_admirals ch
                             LEFT JOIN card_admiral_trade et ON ch.id = et.card_admiral_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserCardMonsterPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM card_monsters ch
                             LEFT JOIN card_monster_trade et ON ch.id = et.card_monster_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserCardMilitaryPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM card_military ch
                             LEFT JOIN card_military_trade et ON ch.id = et.card_military_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserCardSpellPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM card_spell ch
                             LEFT JOIN card_spell_trade et ON ch.id = et.card_spell_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserBookPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM books ch
                             LEFT JOIN book_trade et ON ch.id = et.book_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserAchievementPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM achievements ch
                             LEFT JOIN achievement_trade et ON ch.id = et.achievement_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserBorderPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM borders ch
                             LEFT JOIN border_trade et ON ch.id = et.border_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserCollaborationPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM collaborations ch
                             LEFT JOIN collaboration_trade et ON ch.id = et.collaboration_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserCollaborationEquipmentPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM collaboration_equipments ch
                             LEFT JOIN collaboration_equipment_trade et ON ch.id = et.collaboration_equipment_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserItemPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM items ch
                             LEFT JOIN item_trade et ON ch.id = et.item_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserMagicFormationCirclePriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM magic_formation_circle ch
                             LEFT JOIN magic_formation_circle_trade et ON ch.id = et.mfc_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserMedalPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM medals ch
                             LEFT JOIN medal_circle_trade et ON ch.id = et.medal_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserPetPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM pets ch
                             LEFT JOIN pet_trade et ON ch.id = et.pet_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserRelicPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM relics ch
                             LEFT JOIN relic_trade et ON ch.id = et.relic_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserSkillPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM skills ch
                             LEFT JOIN skill_trade et ON ch.id = et.skill_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserSymbolPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM symbols ch
                             LEFT JOIN symbol_trade et ON ch.id = et.symbol_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserTitlePriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM titles ch
                             LEFT JOIN title_trade et ON ch.id = et.title_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserTalismanPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM talisman ch
                             LEFT JOIN talisman_trade et ON ch.id = et.talisman_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserPuppetPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM puppet ch
                             LEFT JOIN puppet_trade et ON ch.id = et.puppet_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserAlchemyPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM alchemy ch
                             LEFT JOIN alchemy_trade et ON ch.id = et.alchemy_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserForgePriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM forge ch
                             LEFT JOIN forge_trade et ON ch.id = et.forge_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserCardLifePriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM card_life ch
                             LEFT JOIN card_life_trade et ON ch.id = et.card_life_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserArtworkPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM artwork ch
                             LEFT JOIN artwork_trade et ON ch.id = et.artwork_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserSpiritBeastPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM spirit_beast ch
                             LEFT JOIN spirit_beast_trade et ON ch.id = et.spirit_beast_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserSpiritCardPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM spirit_card ch
                             LEFT JOIN spirit_card_trade et ON ch.id = et.spirit_card_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserArtifactPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM cards ch
                             LEFT JOIN card_trade et ON ch.id = et.card_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserArchitecturePriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM architectures ch
                             LEFT JOIN architecture_trade et ON ch.id = et.architecture_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserTechnologyPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM technologies ch
                             LEFT JOIN technology_trade et ON ch.id = et.technology_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserVehiclePriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM vehicles ch
                             LEFT JOIN vehicle_trade et ON ch.id = et.vehicle_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserCorePriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM cores ch
                             LEFT JOIN core_trade et ON ch.id = et.core_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserWeaponPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM weapons ch
                             LEFT JOIN weapon_trade et ON ch.id = et.weapon_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserRobotPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM robots ch
                             LEFT JOIN robot_trade et ON ch.id = et.robot_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserBadgePriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM badges ch
                             LEFT JOIN badge_trade et ON ch.id = et.badge_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserMechaBeastPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM mecha_beasts ch
                             LEFT JOIN mecha_beast_trade et ON ch.id = et.mecha_beast_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserRunePriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM runes ch
                             LEFT JOIN rune_trade et ON ch.id = et.rune_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserFurniturePriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM furnitures ch
                             LEFT JOIN furniture_trade et ON ch.id = et.furniture_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserFoodPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM foods ch
                             LEFT JOIN food_trade et ON ch.id = et.food_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserBeveragePriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM beverages ch
                             LEFT JOIN beverage_trade et ON ch.id = et.beverage_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserBuildingPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM buildings ch
                             LEFT JOIN building_trade et ON ch.id = et.building_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserPlantPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM plants ch
                             LEFT JOIN plant_trade et ON ch.id = et.plant_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserFashionPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM fashions ch
                             LEFT JOIN fashion_trade et ON ch.id = et.fashion_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<Currencies> GetUserEmojiPriceAsync(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                             FROM emojis ch
                             LEFT JOIN emoji_trade et ON ch.id = et.emoji_id
                             LEFT JOIN currencies c ON c.id = et.currency_id
                             LEFT JOIN user_currencies uc ON uc.currency_id = c.id
                             WHERE ch.id=@id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            currency = new Currencies
                            {
                                Id = reader.GetStringSafe("currency_id"),
                                Name = reader.GetStringSafe("currency_name"),
                                Image = reader.GetStringSafe("currency_image"),
                                Quantity = reader.GetDoubleSafe("trade_price")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currency;
    }
    public async Task<List<Currencies>> GetAchievementsCurrencyAsync()
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM achievements a
                             JOIN achievement_trade at ON a.id = at.achievement_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Currencies currency = new Currencies
                        {
                            Id = reader.GetStringSafe("id"),
                            Name = reader.GetStringSafe("name"),
                            Image = reader.GetStringSafe("image"),
                            Quantity = reader.GetIntSafe("quantity")
                        };
                        currencies.Add(currency);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetBooksCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM books a
                             JOIN book_trade at ON a.id = at.book_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetCardHeroesCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM card_heroes a
                             JOIN card_hero_trade at ON a.id = at.card_hero_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetCardCaptainsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM card_captains a
                             JOIN card_captain_trade at ON a.id = at.card_captain_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetCardColonelsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM card_colonels a
                             JOIN card_colonel_trade at ON a.id = at.card_colonel_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetCardGeneralsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM card_generals a
                             JOIN card_general_trade at ON a.id = at.card_general_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetCardAdmiralsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM card_admirals a
                             JOIN card_admiral_trade at ON a.id = at.card_admiral_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetCardMonstersCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM card_monsters a
                             JOIN card_monster_trade at ON a.id = at.card_monster_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Currencies currency = new Currencies
                        {
                            Id = reader.GetStringSafe("id"),
                            Name = reader.GetStringSafe("name"),
                            Image = reader.GetStringSafe("image"),
                            Quantity = reader.GetIntSafe("quantity"),
                        };
                        currencies.Add(currency);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetCardMilitariesCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM card_militaries a
                             JOIN card_military_trade at ON a.id = at.card_military_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetCardSpellsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM card_spells a
                             JOIN card_spell_trade at ON a.id = at.card_spell_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetCollaborationsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM collaborations a
                             JOIN collaboration_trade at ON a.id = at.collaboration_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetCollaborationEquipmentsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM collaboration_equipments a
                             JOIN collaboration_equipment_trade at ON a.id = at.collaboration_equipment_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetBordersCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM borders a
                             JOIN border_trade at ON a.id = at.border_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetItemsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM items a
                             JOIN item_trade at ON a.id = at.item_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetMagicFormationCirclesCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM magic_formation_circles a
                             JOIN magic_formation_circle_trade at ON a.id = at.mfc_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetMedalsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM medals a
                             JOIN medal_trade at ON a.id = at.medal_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetPetsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM pets a
                             JOIN pet_trade at ON a.id = at.pet_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetRelicsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM relics a
                             JOIN relic_trade at ON a.id = at.relic_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetSkillsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM skills a
                             JOIN skill_trade at ON a.id = at.skill_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetSymbolsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM symbols a
                             JOIN symbol_trade at ON a.id = at.symbol_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetTitlesCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM titles a
                             JOIN title_trade at ON a.id = at.title_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetTalismansCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM talismans a
                             JOIN talisman_trade at ON a.id = at.talisman_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetPuppetsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM puppets a
                             JOIN puppet_trade at ON a.id = at.puppet_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetAlchemiesCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM alchemies a
                             JOIN alchemy_trade at ON a.id = at.alchemy_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetForgesCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM forges a
                             JOIN forge_trade at ON a.id = at.forge_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetCardLivesCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM card_lives a
                             JOIN card_life_trade at ON a.id = at.card_life_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetArtworksCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM artworks a
                             JOIN artwork_trade at ON a.id = at.artwork_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id
                             WHERE a.type = @type;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetSpiritBeastsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM spirit_beasts a
                             JOIN spirit_beast_trade at ON a.id = at.spirit_beast_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Nếu cần filter theo type, thêm dòng này:
                    // command.Parameters.AddWithValue("@type", type);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetSpiritCardsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                             FROM spirit_cards a
                             JOIN spirit_card_trade at ON a.id = at.spirit_card_id
                             JOIN currencies c ON at.currency_id = c.id
                             JOIN user_currencies uc ON c.id = uc.currency_id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetArtifactsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                FROM cards a
                JOIN card_trade at ON a.id = at.card_id
                JOIN currencies c ON at.currency_id = c.id
                JOIN user_currencies uc ON c.id = uc.currency_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetArchitecturesCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                FROM architectures a
                JOIN architecture_trade at ON a.id = at.architecture_id
                JOIN currencies c ON at.currency_id = c.id
                JOIN user_currencies uc ON c.id = uc.currency_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetTechnologiesCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                FROM technologies a
                JOIN technology_trade at ON a.id = at.technology_id
                JOIN currencies c ON at.currency_id = c.id
                JOIN user_currencies uc ON c.id = uc.currency_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetVehiclesCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                FROM vehicles a
                JOIN vehicle_trade at ON a.id = at.vehicle_id
                JOIN currencies c ON at.currency_id = c.id
                JOIN user_currencies uc ON c.id = uc.currency_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetCoresCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                FROM cores a
                JOIN core_trade at ON a.id = at.core_id
                JOIN currencies c ON at.currency_id = c.id
                JOIN user_currencies uc ON c.id = uc.currency_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetWeaponsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                FROM weapons a
                JOIN weapon_trade at ON a.id = at.weapon_id
                JOIN currencies c ON at.currency_id = c.id
                JOIN user_currencies uc ON c.id = uc.currency_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetRobotsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                FROM robots a
                JOIN robot_trade at ON a.id = at.robot_id
                JOIN currencies c ON at.currency_id = c.id
                JOIN user_currencies uc ON c.id = uc.currency_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetBadgesCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                FROM badges a
                JOIN badge_trade at ON a.id = at.badge_id
                JOIN currencies c ON at.currency_id = c.id
                JOIN user_currencies uc ON c.id = uc.currency_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetMechaBeastsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                FROM mecha_beasts a
                JOIN mecha_beast_trade at ON a.id = at.mecha_beast_id
                JOIN currencies c ON at.currency_id = c.id
                JOIN user_currencies uc ON c.id = uc.currency_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetRunesCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                FROM runes a
                JOIN rune_trade at ON a.id = at.rune_id
                JOIN currencies c ON at.currency_id = c.id
                JOIN user_currencies uc ON c.id = uc.currency_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetFurnituresCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                FROM furnitures a
                JOIN furniture_trade at ON a.id = at.furniture_id
                JOIN currencies c ON at.currency_id = c.id
                JOIN user_currencies uc ON c.id = uc.currency_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetFoodsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                FROM foods a
                JOIN food_trade at ON a.id = at.food_id
                JOIN currencies c ON at.currency_id = c.id
                JOIN user_currencies uc ON c.id = uc.currency_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetBeveragesCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                FROM beverages a
                JOIN beverage_trade at ON a.id = at.beverage_id
                JOIN currencies c ON at.currency_id = c.id
                JOIN user_currencies uc ON c.id = uc.currency_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetBuildingsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                FROM buildings a
                JOIN building_trade at ON a.id = at.building_id
                JOIN currencies c ON at.currency_id = c.id
                JOIN user_currencies uc ON c.id = uc.currency_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetPlantsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                FROM plants a
                JOIN plant_trade at ON a.id = at.plant_id
                JOIN currencies c ON at.currency_id = c.id
                JOIN user_currencies uc ON c.id = uc.currency_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetFashionsCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                FROM fashions a
                JOIN fashion_trade at ON a.id = at.fashion_id
                JOIN currencies c ON at.currency_id = c.id
                JOIN user_currencies uc ON c.id = uc.currency_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
    public async Task<List<Currencies>> GetEmojisCurrencyAsync(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT DISTINCT c.id, c.image, c.name, uc.quantity
                FROM emojis a
                JOIN emoji_trade at ON a.id = at.emoji_id
                JOIN currencies c ON at.currency_id = c.id
                JOIN user_currencies uc ON c.id = uc.currency_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Currencies currency = new Currencies
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Quantity = reader.GetIntSafe("quantity"),
                            };
                            currencies.Add(currency);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return currencies;
    }
}