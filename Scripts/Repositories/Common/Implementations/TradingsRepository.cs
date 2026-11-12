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

        await using (var connection = new MySqlConnection(_connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // 1. Lấy thông tin Trading chính
                string selectTradingSql = "SELECT * FROM tradings WHERE code_name = @CodeName AND trading_type = @TradingType AND is_deleted = 0 LIMIT 1;";
                
                await using (var cmd = new MySqlCommand(selectTradingSql, connection))
                {
                    cmd.Parameters.AddWithValue("@CodeName", codeName);
                    cmd.Parameters.AddWithValue("@TradingType", tradingType);

                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
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
                                UpdatedAt = reader.GetDateTime("updated_at")
                            };
                        }
                    }
                }

                if (trading != null)
                {
                    // 2. Lấy danh sách Required Materials thô
                    trading.RequiredMaterials = await GetRequiredMaterialsRawAsync(connection, trading.Id);

                    // 3. Lấy danh sách Produced Materials thô
                    trading.ProducedMaterials = await GetProducedMaterialsRawAsync(connection, trading.Id);

                    // 4. Batch Enrich: Gom nhóm và nạp Image + Quantity chỉ bằng vài câu lệnh SQL (không dùng vòng lặp query lẻ)
                    await EnrichSpecificMaterialsBatchAsync(connection, trading.RequiredMaterials, userId);
                    await EnrichSpecificMaterialsBatchAsync(connection, trading.ProducedMaterials, userId);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error GetTradingAsync: " + ex.Message);
            }
        }

        return trading;
    }

    private async Task<List<TradingMaterialsRequired>> GetRequiredMaterialsRawAsync(MySqlConnection connection, string tradingId)
    {
        var list = new List<TradingMaterialsRequired>();
        string sql = "SELECT * FROM trading_materials_required WHERE trading_id = @TradingId;";

        await using (var cmd = new MySqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@TradingId", tradingId);
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    list.Add(new TradingMaterialsRequired
                    {
                        Id = reader.GetString("id"),
                        TradingId = reader.GetString("trading_id"),
                        ObjectType = reader.GetString("object_type"),
                        ObjectId = reader.IsDBNull(reader.GetOrdinal("object_id")) ? null : reader.GetString("object_id"),
                        SelectionType = reader.GetString("selection_type"),
                        ObjectValue = reader.GetDouble("object_value"),
                        RequiredQuantity = reader.GetDouble("required_quantity"),
                        CreatedAt = reader.GetDateTime("created_at")
                    });
                }
            }
        }
        return list;
    }

    private async Task<List<TradingMaterialsProduced>> GetProducedMaterialsRawAsync(MySqlConnection connection, string tradingId)
    {
        var list = new List<TradingMaterialsProduced>();
        string sql = "SELECT * FROM trading_materials_produced WHERE trading_id = @TradingId;";

        await using (var cmd = new MySqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@TradingId", tradingId);
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    list.Add(new TradingMaterialsProduced
                    {
                        Id = reader.GetString("id"),
                        TradingId = reader.GetString("trading_id"),
                        ObjectType = reader.GetString("object_type"),
                        ObjectId = reader.IsDBNull(reader.GetOrdinal("object_id")) ? null : reader.GetString("object_id"),
                        SelectionType = reader.GetString("selection_type"),
                        ObjectValue = reader.GetDouble("object_value"),
                        ProducedQuantity = reader.GetDouble("produced_quantity"),
                        CreatedAt = reader.GetDateTime("created_at")
                    });
                }
            }
        }
        return list;
    }

    /// <summary>
    /// Thuật toán Batch Enrich: Gom các item SPECIFIC theo ObjectType để query dạng `IN (...)` 
    /// giúp giảm số lần truy cập Database xuống tối thiểu tuyệt đối.
    /// </summary>
    private async Task EnrichSpecificMaterialsBatchAsync<T>(MySqlConnection connection, List<T> materials, string userId) where T : class
    {
        // Chỉ lấy những item là SPECIFIC và có ObjectId hợp lệ
        var specificItems = materials.Where(m => 
        {
            var selType = m.GetType().GetProperty("SelectionType")?.GetValue(m) as string;
            var objId = m.GetType().GetProperty("ObjectId")?.GetValue(m) as string;
            return selType == "SPECIFIC" && !string.IsNullOrEmpty(objId);
        }).ToList();

        if (specificItems.Count == 0) return;

        // Gom nhóm theo ObjectType (Ví dụ: Tất cả đồ WEAPON chung 1 nhóm, CARD_HEROES chung 1 nhóm)
        var groupedByType = specificItems.GroupBy(m => m.GetType().GetProperty("ObjectType")?.GetValue(m) as string);

        foreach (var group in groupedByType)
        {
            string objectType = group.Key;
            string tableName = DatabaseHelper.GetTableNameByObjectType(objectType);
            string userTableName = DatabaseHelper.GetUserTableNameByObjectType(objectType);
            string idColumn = DatabaseHelper.GetIdColumnNameByObjectType(objectType);

            var objectIds = group.Select(m => m.GetType().GetProperty("ObjectId")?.GetValue(m) as string).Distinct().ToList();
            if (objectIds.Count == 0) continue;

            // Tạo danh sách parameters động cho câu lệnh IN (@id0, @id1, ...)
            var paramNames = new List<string>();
            var imageMap = new Dictionary<string, string>();
            var quantityMap = new Dictionary<string, double>();

            // 1. Query lấy Image hàng loạt từ bảng danh mục
            string inSqlParameters = "";
            for (int i = 0; i < objectIds.Count; i++)
            {
                paramNames.Add($"@id{i}");
            }
            inSqlParameters = string.Join(",", paramNames);

            string imageSql = $"SELECT {idColumn}, image FROM {tableName} WHERE {idColumn} IN ({inSqlParameters});";
            await using (var cmd = new MySqlCommand(imageSql, connection))
            {
                for (int i = 0; i < objectIds.Count; i++)
                {
                    cmd.Parameters.AddWithValue(paramNames[i], objectIds[i]);
                }

                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string idVal = reader.GetString(0);
                        string imgVal = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        imageMap[idVal] = imgVal;
                    }
                }
            }

            // 2. Query lấy Quantity hàng loạt từ bảng kho người dùng (USER_...)
            string userQtySql = $"SELECT {idColumn}, quantity FROM {userTableName} WHERE user_id = @UserId AND {idColumn} IN ({inSqlParameters});";
            await using (var cmd = new MySqlCommand(userQtySql, connection))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                for (int i = 0; i < objectIds.Count; i++)
                {
                    cmd.Parameters.AddWithValue(paramNames[i], objectIds[i]);
                }

                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string idVal = reader.GetString(0);
                        double qtyVal = reader.GetDouble(1);
                        quantityMap[idVal] = qtyVal;
                    }
                }
            }

            // 3. Map ngược dữ liệu vào danh sách material object tương ứng
            foreach (var item in group)
            {
                var idProp = item.GetType().GetProperty("ObjectId");
                var imgProp = item.GetType().GetProperty("ObjectImage");
                var qtyProp = item.GetType().GetProperty("ObjectQuantity");

                string objId = idProp?.GetValue(item) as string;
                if (!string.IsNullOrEmpty(objId))
                {
                    if (imageMap.ContainsKey(objId))
                        imgProp?.SetValue(item, imageMap[objId]);

                    if (quantityMap.ContainsKey(objId))
                        qtyProp?.SetValue(item, quantityMap[objId]);
                    else
                        qtyProp?.SetValue(item, 0.0); // Nếu trong kho chưa có thì mặc định là 0
                }
            }
        }
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