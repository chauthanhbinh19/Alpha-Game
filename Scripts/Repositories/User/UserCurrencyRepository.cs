using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserCurrencyRepository : IUserCurrencyRepository
{
    public List<Currencies> GetUserCurrency(string userId)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string currencyQuery = "SELECT c.image, c.name, uc.currency_id, uc.quantity FROM user_currency uc, currency c WHERE user_id = @userId and uc.currency_id=c.id";
                MySqlCommand currencyCommand = new MySqlCommand(currencyQuery, connection);
                currencyCommand.Parameters.AddWithValue("@userId", userId);

                MySqlDataReader currencyReader = currencyCommand.ExecuteReader();
                while (currencyReader.Read())
                {
                    string image = currencyReader.GetString("image");
                    string name = currencyReader.GetString("name");
                    string currencyId = currencyReader.GetString("currency_id");
                    double quantity = currencyReader.GetDouble("quantity");
                    currencies.Add(new Currencies
                    {
                        Id = currencyId,
                        Name = name,
                        Image = image,
                        Quantity = quantity
                    });
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
        return currencies;
    }
    public Currencies GetUserCurrencyById(string Id)
    {
        Currencies currencies = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string currencyQuery = @"SELECT c.image, c.name, uc.currency_id, uc.quantity 
                FROM user_currency uc, currency c 
                WHERE user_id = @userId and uc.currency_id=c.id and c.id=@id;";
                MySqlCommand currencyCommand = new MySqlCommand(currencyQuery, connection);
                currencyCommand.Parameters.AddWithValue("@userId", User.CurrentUserId);
                currencyCommand.Parameters.AddWithValue("@id", Id);
                MySqlDataReader currencyReader = currencyCommand.ExecuteReader();
                while (currencyReader.Read())
                {
                    string image = currencyReader.GetString("image");
                    string name = currencyReader.GetString("name");
                    string currencyId = currencyReader.GetString("currency_id");
                    double quantity = currencyReader.GetDouble("quantity");
                    currencies = new Currencies
                    {
                        Id = currencyId,
                        Name = name,
                        Image = image,
                        Quantity = quantity
                    };
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
        return currencies;
    }
    public Currencies GetUserCurrencyByName(string currencyName)
    {
        Currencies currencies = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string currencyQuery = @"SELECT c.image, c.name, uc.currency_id, uc.quantity 
                FROM user_currency uc, currency c 
                WHERE user_id = @userId and uc.currency_id=c.id and c.name=@name;";
                MySqlCommand currencyCommand = new MySqlCommand(currencyQuery, connection);
                currencyCommand.Parameters.AddWithValue("@userId", User.CurrentUserId);
                currencyCommand.Parameters.AddWithValue("@name", currencyName);
                MySqlDataReader currencyReader = currencyCommand.ExecuteReader();
                while (currencyReader.Read())
                {
                    string image = currencyReader.GetString("image");
                    string name = currencyReader.GetString("name");
                    string currencyId = currencyReader.GetString("currency_id");
                    double quantity = currencyReader.GetDouble("quantity");
                    currencies = new Currencies
                    {
                        Id = currencyId,
                        Name = name,
                        Image = image,
                        Quantity = quantity
                    };
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
        return currencies;
    }
    public void UpdateUserCurrency(string currency_id, double price)
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
    public List<Currencies> GetEquipmentsCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetDouble("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
                return currencies;
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
        return currencies;
    }
    public Currencies GetEquipmentsPrice(string type, string equipment_id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetDouble("price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserEquipmentsPrice(string type, string equipment_id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetDouble("quantity"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserCardHeroesPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserCardCaptainsPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserCardColonelsPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserCardGeneralsPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserCardAdmiralsPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserCardMonstersPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserCardMilitaryPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserCardSpellPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserBooksPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserAchievementsPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserBordersPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserCollaborationsPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserCollaborationEquipmentsPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserItemsPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserMagicFormationCirclePrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserMedalsPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserPetsPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserRelicsPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserSkillsPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserSymbolsPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserTitlesPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserTalismanPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserPuppetPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserAlchemyPrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserForgePrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserCardLifePrice(string Id)
    {
        Currencies currency = new Currencies();
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
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserArtworkPrice(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM artwork ch
                left JOIN artwork_trade et ON ch.id = et.artwork_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserSpiritBeastPrice(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM spirit_beast ch
                left JOIN spirit_beast_trade et ON ch.id = et.spirit_beast_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserSpiritCardPrice(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM spirit_card ch
                left JOIN spirit_card_trade et ON ch.id = et.spirit_card_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserCardsPrice(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM cards ch
                left JOIN card_trade et ON ch.id = et.card_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserArchitecturesPrice(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM architectures ch
                left JOIN architecture_trade et ON ch.id = et.architecture_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserTechnologiesPrice(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM technologies ch
                left JOIN technology_trade et ON ch.id = et.technology_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserVehiclesPrice(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM vehicles ch
                left JOIN vehicle_trade et ON ch.id = et.vehicle_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserCoresPrice(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM cores ch
                left JOIN core_trade et ON ch.id = et.core_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserWeaponsPrice(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM weapons ch
                left JOIN weapon_trade et ON ch.id = et.weapon_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public Currencies GetUserRobotsPrice(string Id)
    {
        Currencies currency = new Currencies();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT DISTINCT c.id AS currency_id, c.image AS currency_image, c.name AS currency_name, uc.quantity AS trade_price
                FROM robots ch
                left JOIN robot_trade et ON ch.id = et.robot_id
                left JOIN currency c ON c.id = et.currency_id
                left JOIN user_currency uc ON uc.currency_id = c.id
                where ch.id=@id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency = new Currencies
                    {
                        Id = reader.GetString("currency_id"),
                        Name = reader.GetString("currency_name"),
                        Image = reader.GetString("currency_image"),
                        Quantity = reader.GetDouble("trade_price"),
                    };
                }
                ;
                return currency;
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
        return currency;
    }
    public List<Currencies> GetAchievementsCurrency()
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetBooksCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetCardHeroesCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetCardCaptainsCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetCardColonelsCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetCardGeneralsCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetCardAdmiralsCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetCardMonstersCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetCardMilitaryCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetCardSpellCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetCollaborationsCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetCollaborationEquipmentsCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetBordersCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetItemsCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetMagicFormationCircleCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetMedalsCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetPetsCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetRelicsCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetSkillsCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetSymbolsCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetTitlesCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetTalismanCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetPuppetCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetAlchemyCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetForgeCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetCardLifeCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
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
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetArtworkCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from artwork a, artwork_trade at, currency c, user_currency uc
                where a.id=at.artwork_id and at.currency_id = c.id and c.id =uc.currency_id and a.type=@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetSpiritBeastCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from spirit_beast a, spirit_beast_trade at, currency c, user_currency uc
                where a.id=at.spirit_beast_id and at.currency_id = c.id and c.id =uc.currency_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetSpiritCardCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from spirit_card a, spirit_card_trade at, currency c, user_currency uc
                where a.id=at.spirit_card_id and at.currency_id = c.id and c.id =uc.currency_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetCardsCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from cards a, card_trade at, currency c, user_currency uc
                where a.id=at.card_id and at.currency_id = c.id and c.id =uc.currency_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetArchitecturesCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from architectures a, architecture_trade at, currency c, user_currency uc
                where a.id=at.architecture_id and at.currency_id = c.id and c.id =uc.currency_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetTechnologiesCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from technologies a, technology_trade at, currency c, user_currency uc
                where a.id=at.technology_id and at.currency_id = c.id and c.id =uc.currency_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetVehiclesCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from vehicles a, vehicle_trade at, currency c, user_currency uc
                where a.id=at.vehicle_id and at.currency_id = c.id and c.id =uc.currency_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetCoresCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from cores a, core_trade at, currency c, user_currency uc
                where a.id=at.core_id and at.currency_id = c.id and c.id =uc.currency_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetWeaponsCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from weapons a, weapon_trade at, currency c, user_currency uc
                where a.id=at.weapon_id and at.currency_id = c.id and c.id =uc.currency_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
    public List<Currencies> GetRobotsCurrency(string type)
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                from robots a, robot_trade at, currency c, user_currency uc
                where a.id=at.robot_id and at.currency_id = c.id and c.id =uc.currency_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currencies currency = new Currencies
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                }
                ;
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
        return currencies;
    }
}