using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class Currency
{
    private int id1;
    private string name1;
    private string image1;
    private int quantity1;

    public int id { get => id1; set => id1 = value; }
    public string name { get => name1; set => name1 = value; }
    public string image { get => image1; set => image1 = value; }
    public int quantity { get => quantity1; set => quantity1 = value; }
    public List<Currency> GetUserCurrency()
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string currencyQuery = "SELECT c.image, c.name, uc.currency_id, uc.quantity FROM user_currency uc, currency c WHERE user_id = @userId and uc.currency_id=c.id";
                MySqlCommand currencyCommand = new MySqlCommand(currencyQuery, connection);
                currencyCommand.Parameters.AddWithValue("@userId", User.CurrentUserId);

                MySqlDataReader currencyReader = currencyCommand.ExecuteReader();
                while (currencyReader.Read())
                {
                    string image = currencyReader.GetString("image");
                    string name = currencyReader.GetString("name");
                    int currencyId = currencyReader.GetInt32("currency_id");
                    int quantity = currencyReader.GetInt32("quantity");
                    currencies.Add(new Currency
                    {
                        id = currencyId,
                        name = name,
                        image = image,
                        quantity = quantity
                    });
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public Currency GetUserCurrencyById(int Id)
    {
        Currency currencies = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string currencyQuery = "SELECT c.image, c.name, uc.currency_id, uc.quantity FROM user_currency uc, currency c WHERE user_id = @userId and uc.currency_id=c.id and c.id=@id;";
                MySqlCommand currencyCommand = new MySqlCommand(currencyQuery, connection);
                currencyCommand.Parameters.AddWithValue("@userId", User.CurrentUserId);
                currencyCommand.Parameters.AddWithValue("@id", Id);
                MySqlDataReader currencyReader = currencyCommand.ExecuteReader();
                while (currencyReader.Read())
                {
                    string image = currencyReader.GetString("image");
                    string name = currencyReader.GetString("name");
                    int currencyId = currencyReader.GetInt32("currency_id");
                    int quantity = currencyReader.GetInt32("quantity");
                    currencies = new Currency
                    {
                        id = currencyId,
                        name = name,
                        image = image,
                        quantity = quantity
                    };
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public void UpdateUserCurrency(int currency_id, double price)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                // Lấy quantity hiện tại
                string query = "SELECT quantity FROM user_currency WHERE user_id = @user_id AND currency_id = @currency_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@currency_id", currency_id);
                double currentQuantity = Convert.ToDouble(command.ExecuteScalar());
                double newQuantity = currentQuantity - price;

                query = "update user_currency set quantity=@quantity where user_id=@user_id and currency_id=@currency_id";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@quantity", newQuantity);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@currency_id", currency_id);
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
    public List<Currency> GetEquipmentsCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                            from equipments e, currency c, equipment_trade et, user_currency uc
                            where e.id=et.equipment_id and c.id=et.currency_id and e.type=@type and uc.currency_id=c.id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
                return currencies;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currencies;
    }
    public Currency GetEquipmentsPrice(string type, int equipment_id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, et.price 
                            from equipments e, currency c, equipment_trade et, user_currency uc
                            where e.id=et.equipment_id and c.id=et.currency_id and e.type=@type and uc.currency_id=c.id
                            and e.id=@equipment_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@equipment_id", equipment_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserEquipmentsPrice(string type, int equipment_id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                            from equipments e, currency c, equipment_trade et, user_currency uc
                            where e.id=et.equipment_id and c.id=et.currency_id and e.type=@type and uc.currency_id=c.id
                            and e.id=@equipment_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@equipment_id", equipment_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserCardHeroesPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM card_heroes ch
                left JOIN card_hero_trade et ON ch.id = et.card_hero_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserCardCaptainsPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM card_captains ch
                left JOIN card_captain_trade et ON ch.id = et.card_captain_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserCardColonelsPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM card_colonels ch
                left JOIN card_colonel_trade et ON ch.id = et.card_colonel_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserCardGeneralsPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM card_generals ch
                left JOIN card_general_trade et ON ch.id = et.card_general_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserCardAdmiralsPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM card_admirals ch
                left JOIN card_admiral_trade et ON ch.id = et.card_admiral_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserCardMonstersPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM card_monsters ch
                left JOIN card_monster_trade et ON ch.id = et.card_monster_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserCardMilitaryPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM card_military ch
                left JOIN card_military_trade et ON ch.id = et.card_military_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserCardSpellPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM card_spell ch
                left JOIN card_spell_trade et ON ch.id = et.card_spell_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserBooksPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM books ch
                left JOIN book_trade et ON ch.id = et.book_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserAchievementsPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM achievements ch
                left JOIN achievement_trade et ON ch.id = et.achievement_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserBordersPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM borders ch
                left JOIN border_trade et ON ch.id = et.border_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserCollaborationsPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM collaborations ch
                left JOIN collaboration_trade et ON ch.id = et.collaboration_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserCollaborationEquipmentsPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM collaboration_equipments ch
                left JOIN collaboration_equipment_trade et ON ch.id = et.collaboration_equipment_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserItemsPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM items ch
                left JOIN item_trade et ON ch.id = et.item_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserMagicFormationCirclePrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM magic_formation_circle ch
                left JOIN magic_formation_circle_trade et ON ch.id = et.mfc_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserMedalsPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM medals ch
                left JOIN medal_circle_trade et ON ch.id = et.medal_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserPetsPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM pets ch
                left JOIN pet_trade et ON ch.id = et.pet_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserRelicsPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM relics ch
                left JOIN relic_trade et ON ch.id = et.relic_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserSkillsPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM skills ch
                left JOIN skill_trade et ON ch.id = et.skill_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserSymbolsPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM symbols ch
                left JOIN symbol_trade et ON ch.id = et.symbol_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserTitlesPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM titles ch
                left JOIN title_trade et ON ch.id = et.title_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserTalismanPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM talisman ch
                left JOIN talisman_trade et ON ch.id = et.talisman_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserPuppetPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM puppet ch
                left JOIN puppet_trade et ON ch.id = et.puppet_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserAlchemyPrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM alchemy ch
                left JOIN alchemy_trade et ON ch.id = et.alchemy_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserForgePrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM forge ch
                left JOIN forge_trade et ON ch.id = et.forge_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserCardLifePrice(int Id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM card_life ch
                left JOIN card_life_trade et ON ch.id = et.card_life_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        name = reader.GetString("currency_name"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("trade_price"),
                    };
                };
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public List<Currency> GetAchievementsCurrency()
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from achievements a, achievement_trade at, currency c, user_currency uc
                where a.id=at.achievement_id and at.currency_id = c.id and c.id =uc.currency_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetBooksCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from books a, book_trade at, currency c, user_currency uc
                where a.id=at.book_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetCardHeroesCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from card_heroes a, card_hero_trade at, currency c, user_currency uc
                where a.id=at.card_hero_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetCardCaptainsCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from card_captains a, card_captain_trade at, currency c, user_currency uc
                where a.id=at.card_captain_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetCardColonelsCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from card_colonels a, card_colonel_trade at, currency c, user_currency uc
                where a.id=at.card_colonel_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetCardGeneralsCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from card_generals a, card_general_trade at, currency c, user_currency uc
                where a.id=at.card_general_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetCardAdmiralsCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from card_admirals a, card_admiral_trade at, currency c, user_currency uc
                where a.id=at.card_admiral_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetCardMonstersCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from card_monsters a, card_monster_trade at, currency c, user_currency uc
                where a.id=at.card_monster_id and at.currency_id = c.id and c.id =uc.currency_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetCardMilitaryCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from card_military a, card_military_trade at, currency c, user_currency uc
                where a.id=at.card_military_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetCardSpellCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from card_spell a, card_spell_trade at, currency c, user_currency uc
                where a.id=at.card_spell_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetCollaborationsCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from collaborations a, collaboration_trade at, currency c, user_currency uc
                where a.id=at.collaboration_id and at.currency_id = c.id and c.id =uc.currency_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetCollaborationEquipmentsCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from collaboration_equipments a, collaboration_equipment_trade at, currency c, user_currency uc
                where a.id=at.collaboration_equipment_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetBordersCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from borders a, border_trade at, currency c, user_currency uc
                where a.id=at.border_id and at.currency_id = c.id and c.id =uc.currency_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetItemsCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from items a, item_trade at, currency c, user_currency uc
                where a.id=at.item_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetMagicFormationCircleCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from magic_formation_circle a, magic_formation_circle_trade at, currency c, user_currency uc
                where a.id=at.mfc_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetMedalsCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from medals a, medal_trade at, currency c, user_currency uc
                where a.id=at.medal_id and at.currency_id = c.id and c.id =uc.currency_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetPetsCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from pets a, pet_trade at, currency c, user_currency uc
                where a.id=at.pet_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetRelicsCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from relics a, relic_trade at, currency c, user_currency uc
                where a.id=at.relic_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetSkillsCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from skills a, skill_trade at, currency c, user_currency uc
                where a.id=at.skill_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetSymbolsCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from symbols a, symbol_trade at, currency c, user_currency uc
                where a.id=at.symbol_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetTitlesCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from titles a, title_trade at, currency c, user_currency uc
                where a.id=at.title_id and at.currency_id = c.id and c.id =uc.currency_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetTalismanCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from talisman a, talisman_trade at, currency c, user_currency uc
                where a.id=at.talisman_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetPuppetCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from puppet a, puppet_trade at, currency c, user_currency uc
                where a.id=at.puppet_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetAlchemyCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from alchemy a, alchemy_trade at, currency c, user_currency uc
                where a.id=at.alchemy_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetForgeCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from forge a, forge_trade at, currency c, user_currency uc
                where a.id=at.forge_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
    public List<Currency> GetCardLifeCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from card_life a, card_life_trade at, currency c, user_currency uc
                where a.id=at.card_life_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return currencies;
    }
}