using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using UnityEngine; // Dùng cho Debug.LogError

public class TradingsRepository : ITradingsRepository
{
    private readonly string _connectionString = DatabaseConfig.ConnectionString;

    public async Task<Tradings> GetTradingAsync(string codeName, string tradingType, string userId)
    {
        Tradings trading = null;

        string sql = @"
        -- 1. Thông tin Trading chính
        SELECT * FROM tradings 
        WHERE code_name = @CodeName AND trading_type = @TradingType AND is_deleted = 0 
        LIMIT 1;

        -- 2. Danh sách Required Materials kèm Image & Quantity
        SELECT 
            tmr.*,
            gic.image AS object_image,
            COALESCE(ugic.quantity, 0) AS object_quantity
        FROM trading_materials_required tmr
        INNER JOIN tradings t ON t.id = tmr.trading_id
        LEFT JOIN game_item_catalog gic 
            ON tmr.selection_type = 'SPECIFIC' 
            AND tmr.object_id = gic.object_id 
            AND tmr.object_type = gic.object_type
        LEFT JOIN user_game_item_catalog ugic 
            ON tmr.selection_type = 'SPECIFIC' 
            AND ugic.user_id = @UserId
            AND tmr.object_id = ugic.object_id 
            AND tmr.object_type = ugic.object_type
        WHERE t.code_name = @CodeName AND t.trading_type = @TradingType AND t.is_deleted = 0;

        -- 3. Danh sách Produced Materials kèm Image & Quantity
        SELECT 
            tmp.*,
            gic.image AS object_image,
            COALESCE(ugic.quantity, 0) AS object_quantity
        FROM trading_materials_produced tmp
        INNER JOIN tradings t ON t.id = tmp.trading_id
        LEFT JOIN game_item_catalog gic 
            ON tmp.selection_type = 'SPECIFIC' 
            AND tmp.object_id = gic.object_id 
            AND tmp.object_type = gic.object_type
        LEFT JOIN user_game_item_catalog ugic 
            ON tmp.selection_type = 'SPECIFIC' 
            AND ugic.user_id = @UserId
            AND tmp.object_id = ugic.object_id 
            AND tmp.object_type = ugic.object_type
        WHERE t.code_name = @CodeName AND t.trading_type = @TradingType AND t.is_deleted = 0;";

        await using var connection = new MySqlConnection(_connectionString);
        try
        {
            await connection.OpenAsync();
            await using var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@CodeName", codeName);
            cmd.Parameters.AddWithValue("@TradingType", tradingType);
            cmd.Parameters.AddWithValue("@UserId", userId);

            await using var reader = await cmd.ExecuteReaderAsync();

            // Đọc Result Set 1: Tradings
            if (await reader.ReadAsync())
            {
                trading = new Tradings
                {
                    Id = reader.GetString("id"),
                    CodeName = reader.GetString("code_name"),
                    Name = reader.GetString("name"),
                    Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString("description"),
                    TradingType = reader.GetString("trading_type"),
                    MinLevel = reader.GetInt32("min_level"),
                    MaxLevel = reader.GetInt32("max_level"),
                    IsActive = reader.GetBoolean("is_active"),
                    IsDeleted = reader.GetBoolean("is_deleted"),
                    CreatedAt = reader.GetDateTime("created_at"),
                    UpdatedAt = reader.GetDateTime("updated_at"),
                    RequiredMaterials = new List<TradingMaterialsRequired>(),
                    ProducedMaterials = new List<TradingMaterialsProduced>()
                };
            }

            if (trading == null) return null;

            // Đọc Result Set 2: RequiredMaterials
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    trading.RequiredMaterials.Add(new TradingMaterialsRequired
                    {
                        Id = reader.GetString("id"),
                        TradingId = reader.GetString("trading_id"),
                        ObjectType = reader.GetString("object_type"),
                        ObjectId = reader.IsDBNull(reader.GetOrdinal("object_id")) ? null : reader.GetString("object_id"),
                        SelectionType = reader.GetString("selection_type"),
                        ObjectValue = reader.GetDouble("object_value"),
                        RequiredQuantity = reader.GetDouble("required_quantity"),
                        ObjectImage = reader.IsDBNull(reader.GetOrdinal("object_image")) ? null : reader.GetString("object_image"),
                        ObjectQuantity = reader.GetDouble("object_quantity"),
                        CreatedAt = reader.GetDateTime("created_at")
                    });
                }
            }

            // Đọc Result Set 3: ProducedMaterials
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    trading.ProducedMaterials.Add(new TradingMaterialsProduced
                    {
                        Id = reader.GetString("id"),
                        TradingId = reader.GetString("trading_id"),
                        ObjectType = reader.GetString("object_type"),
                        ObjectId = reader.IsDBNull(reader.GetOrdinal("object_id")) ? null : reader.GetString("object_id"),
                        SelectionType = reader.GetString("selection_type"),
                        ObjectValue = reader.GetDouble("object_value"),
                        ProducedQuantity = reader.GetDouble("produced_quantity"),
                        ObjectImage = reader.IsDBNull(reader.GetOrdinal("object_image")) ? null : reader.GetString("object_image"),
                        ObjectQuantity = reader.GetDouble("object_quantity"),
                        CreatedAt = reader.GetDateTime("created_at")
                    });
                }
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error GetTradingAsync: " + ex.Message);
        }

        return trading;
    }

    public async Task<List<Tradings>> GetAllAsync()
    {
        var list = new List<Tradings>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string sql = "SELECT * FROM tradings WHERE is_deleted = 0;";

                await using (var cmd = new MySqlCommand(sql, connection))
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new Tradings
                        {
                            Id = reader.GetString("id"),
                            CodeName = reader.GetString("code_name"),
                            Name = reader.GetString("name"),
                            Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString("description"),
                            TradingType = reader.GetString("trading_type"),
                            MinLevel = reader.GetInt32("min_level"),
                            MaxLevel = reader.GetInt32("max_level"),
                            IsActive = reader.GetBoolean("is_active")
                        });
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error GetAllAsync: " + ex.Message);
            }
        }

        return list;
    }

    public async Task<InsertOrUpdateResult<Tradings>> InsertTradingAsync(Tradings model)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            await using (var transaction = await connection.BeginTransactionAsync())
            {
                try
                {
                    // 1. Insert bảng chính Tradings
                    string insertTradingSql = @"
                        INSERT INTO tradings (code_name, name, description, trading_type, min_level, max_level, is_active, is_deleted, created_at, updated_at)
                        VALUES (@CodeName, @Name, @Description, @TradingType, @MinLevel, @MaxLevel, @IsActive, 0, NOW(), NOW());
                        SELECT LAST_INSERT_ID();";

                    string tradingId;
                    await using (var cmd = new MySqlCommand(insertTradingSql, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@CodeName", model.CodeName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Name", model.Name ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TradingType", model.TradingType ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@MinLevel", model.MinLevel);
                        cmd.Parameters.AddWithValue("@MaxLevel", model.MaxLevel);
                        cmd.Parameters.AddWithValue("@IsActive", model.IsActive);

                        tradingId = Convert.ToInt32(await cmd.ExecuteScalarAsync()).ToString();
                        model.Id = tradingId;
                    }

                    // 2. Insert Required Materials
                    if (model.RequiredMaterials != null)
                    {
                        foreach (var req in model.RequiredMaterials)
                        {
                            string insertReqSql = @"
                                INSERT INTO trading_materials_required (trading_id, object_type, object_id, selection_type, object_value, required_quantity, created_at)
                                VALUES (@TradingId, @ObjectType, @ObjectId, @SelectionType, @ObjectValue, @RequiredQuantity, NOW());";

                            await using (var cmd = new MySqlCommand(insertReqSql, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@TradingId", tradingId);
                                cmd.Parameters.AddWithValue("@ObjectType", req.ObjectType ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@ObjectId", req.ObjectId ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@SelectionType", req.SelectionType ?? "SPECIFIC");
                                cmd.Parameters.AddWithValue("@ObjectValue", req.ObjectValue);
                                cmd.Parameters.AddWithValue("@RequiredQuantity", req.RequiredQuantity);

                                await cmd.ExecuteNonQueryAsync();
                            }
                        }
                    }

                    // 3. Insert Produced Materials
                    if (model.ProducedMaterials != null)
                    {
                        foreach (var prod in model.ProducedMaterials)
                        {
                            string insertProdSql = @"
                                INSERT INTO trading_materials_produced (trading_id, object_type, object_id, selection_type, object_value, produced_quantity, created_at)
                                VALUES (@TradingId, @ObjectType, @ObjectId, @SelectionType, @ObjectValue, @ProducedQuantity, NOW());";

                            await using (var cmd = new MySqlCommand(insertProdSql, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@TradingId", tradingId);
                                cmd.Parameters.AddWithValue("@ObjectType", prod.ObjectType ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@ObjectId", prod.ObjectId ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@SelectionType", prod.SelectionType ?? "SPECIFIC");
                                cmd.Parameters.AddWithValue("@ObjectValue", prod.ObjectValue);
                                cmd.Parameters.AddWithValue("@ProducedQuantity", prod.ProducedQuantity);

                                await cmd.ExecuteNonQueryAsync();
                            }
                        }
                    }

                    await transaction.CommitAsync();
                    return InsertOrUpdateResult<Tradings>.Inserted(model, MessageConstants.INSERTED_SUCCESSFULLY);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Debug.LogError("Error InsertTradingAsync: " + ex.Message);
                    return InsertOrUpdateResult<Tradings>.Failure(ex.Message);
                }
            }
        }
    }

    public async Task<InsertOrUpdateResult<Tradings>> UpdateTradingAsync(Tradings model)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            await using (var transaction = await connection.BeginTransactionAsync())
            {
                try
                {
                    // 1. Update bảng chính
                    string updateTradingSql = @"
                        UPDATE tradings 
                        SET name = @Name, 
                            description = @Description, 
                            trading_type = @TradingType, 
                            min_level = @MinLevel, 
                            max_level = @MaxLevel, 
                            is_active = @IsActive, 
                            updated_at = NOW()
                        WHERE id = @Id;";

                    int rowsAffected;
                    await using (var cmd = new MySqlCommand(updateTradingSql, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Id", model.Id);
                        cmd.Parameters.AddWithValue("@Name", model.Name ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Description", model.Description ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TradingType", model.TradingType ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@MinLevel", model.MinLevel);
                        cmd.Parameters.AddWithValue("@MaxLevel", model.MaxLevel);
                        cmd.Parameters.AddWithValue("@IsActive", model.IsActive);

                        rowsAffected = await cmd.ExecuteNonQueryAsync();
                    }

                    if (rowsAffected == 0)
                    {
                        await transaction.RollbackAsync();
                        return InsertOrUpdateResult<Tradings>.Failure("Trading record not found.");
                    }

                    // 2. Xóa sạch vật liệu cũ để ghi đè danh sách mới
                    string deleteReqSql = "DELETE FROM trading_materials_required WHERE trading_id = @Id;";
                    await using (var cmd = new MySqlCommand(deleteReqSql, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Id", model.Id);
                        await cmd.ExecuteNonQueryAsync();
                    }

                    string deleteProdSql = "DELETE FROM trading_materials_produced WHERE trading_id = @Id;";
                    await using (var cmd = new MySqlCommand(deleteProdSql, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Id", model.Id);
                        await cmd.ExecuteNonQueryAsync();
                    }

                    // 3. Insert lại Required Materials mới
                    if (model.RequiredMaterials != null)
                    {
                        foreach (var req in model.RequiredMaterials)
                        {
                            string insertReqSql = @"
                                INSERT INTO trading_materials_required (trading_id, object_type, object_id, selection_type, object_value, required_quantity, created_at)
                                VALUES (@TradingId, @ObjectType, @ObjectId, @SelectionType, @ObjectValue, @RequiredQuantity, NOW());";

                            await using (var cmd = new MySqlCommand(insertReqSql, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@TradingId", model.Id);
                                cmd.Parameters.AddWithValue("@ObjectType", req.ObjectType ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@ObjectId", req.ObjectId ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@SelectionType", req.SelectionType ?? "SPECIFIC");
                                cmd.Parameters.AddWithValue("@ObjectValue", req.ObjectValue);
                                cmd.Parameters.AddWithValue("@RequiredQuantity", req.RequiredQuantity);

                                await cmd.ExecuteNonQueryAsync();
                            }
                        }
                    }

                    // 4. Insert lại Produced Materials mới
                    if (model.ProducedMaterials != null)
                    {
                        foreach (var prod in model.ProducedMaterials)
                        {
                            string insertProdSql = @"
                                INSERT INTO trading_materials_produced (trading_id, object_type, object_id, selection_type, object_value, produced_quantity, created_at)
                                VALUES (@TradingId, @ObjectType, @ObjectId, @SelectionType, @ObjectValue, @ProducedQuantity, NOW());";

                            await using (var cmd = new MySqlCommand(insertProdSql, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@TradingId", model.Id);
                                cmd.Parameters.AddWithValue("@ObjectType", prod.ObjectType ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@ObjectId", prod.ObjectId ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@SelectionType", prod.SelectionType ?? "SPECIFIC");
                                cmd.Parameters.AddWithValue("@ObjectValue", prod.ObjectValue);
                                cmd.Parameters.AddWithValue("@ProducedQuantity", prod.ProducedQuantity);

                                await cmd.ExecuteNonQueryAsync();
                            }
                        }
                    }

                    await transaction.CommitAsync();
                    return InsertOrUpdateResult<Tradings>.Updated(model, MessageConstants.UPDATED_SUCCESSFULLY);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Debug.LogError("Error UpdateTradingAsync: " + ex.Message);
                    return InsertOrUpdateResult<Tradings>.Failure(ex.Message);
                }
            }
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string sql = "UPDATE tradings SET is_deleted = 1, updated_at = NOW() WHERE id = @Id;";

                await using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error DeleteAsync: " + ex.Message);
                return false;
            }
        }
    }
}