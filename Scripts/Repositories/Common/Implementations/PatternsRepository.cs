using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class PatternsRepository : IPatternsRepository
{
    public async Task<List<Patterns>> GetAllPatternsAsync()
    {
        var patternList = new List<Patterns>();

        string connectionString = DatabaseConfig.ConnectionString;
        // Câu lệnh SQL nâng cao: 
        // 1. Sắp xếp theo phần số đứng TRƯỚC chữ 'P' (Ví dụ: 1, 2, 3... 10) bằng CAST AS UNSIGNED
        // 2. Sau đó sắp xếp theo phần số thứ tự của Pattern đứng SAU chữ 'P'
        string query = @"
            SELECT p.id AS pattern_id, p.name AS pattern_name
            FROM patterns p
            ORDER BY 
                CAST(SUBSTRING_INDEX(p.id, 'P', 1) AS UNSIGNED) ASC,
                CAST(SUBSTRING_INDEX(p.id, 'P', -1) AS UNSIGNED) ASC;";

        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var pattern = new Patterns
                            {
                                Id = reader.GetString("pattern_id"),
                                Name = reader.GetString("pattern_name"),
                                Cells = new List<PatternCells>() // Khởi tạo list rỗng để tránh lỗi NullReference sau này
                            };
                            
                            patternList.Add(pattern);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[PatternsRepository] Lỗi khi lấy danh sách patterns đã sort: {ex.Message}");
            throw;
        }

        return patternList;
    }
    public async Task<Patterns> GetPatternByIdAsync(string patternId)
    {
        if (string.IsNullOrEmpty(patternId)) return null;

        Patterns pattern = null;

        string connectionString = DatabaseConfig.ConnectionString;
        // Câu lệnh SQL JOIN tối ưu để lấy cả cha lẫn con trong 1 lần Query
        string query = @"
            SELECT p.id AS pattern_id, p.name AS pattern_name, 
                   c.id AS cell_id, c.offset_x, c.offset_y, c.is_main
            FROM patterns p
            LEFT JOIN pattern_cells c ON p.id = c.pattern_id
            WHERE p.id = @PatternId;";

        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatternId", patternId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Khởi tạo đối tượng Patterns ở dòng đầu tiên đọc được
                            if (pattern == null)
                            {
                                pattern = new Patterns
                                {
                                    Id = reader.GetString("pattern_id"),
                                    Name = reader.GetString("pattern_name"),
                                    Cells = new List<PatternCells>()
                                };
                            }

                            // Kiểm tra nếu có dữ liệu Cell đi kèm (tránh lỗi null khi dùng LEFT JOIN)
                            if (!reader.IsDBNull(reader.GetOrdinal("cell_id")))
                            {
                                var cell = new PatternCells
                                {
                                    Id = reader.GetString("cell_id"),
                                    PatternId = pattern.Id,
                                    OffsetX = reader.GetInt32("offset_x"),
                                    OffsetY = reader.GetInt32("offset_y"),
                                    IsMain = reader.GetBoolean("is_main")
                                };
                                
                                pattern.Cells.Add(cell);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[PatternsRepository] Lỗi khi lấy pattern với ID {patternId}: {ex.Message}");
            throw;
        }

        return pattern;
    }
}