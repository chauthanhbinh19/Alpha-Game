using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

public class UserMailsRepository : IUserMailsRepository
{
    private readonly string _connectionString = DatabaseConfig.ConnectionString;

    #region 1. GET MAIL METHODS (FOR USER UI)

    /// <summary>
    /// Lấy danh sách hòm thư đến của User kèm vật phẩm (Phân trang)
    /// Hỗ trợ lấy cả Mail cá nhân và Mail Global chưa hết hạn
    /// </summary>
    public async Task<List<Mails>> GetUserInboxAsync(string userId, string type = null, int page = 1, int pageSize = 20)
    {
        var mailMap = new Dictionary<string, Mails>();
        int offset = (page - 1) * pageSize;

        // Query kết hợp user_mails và mails, lọc theo Type nếu truyền vào
        string sql = @"
        SELECT 
            m.id, m.sender_id, m.subject, m.body, m.type, m.is_global, m.expired_at,
            um.is_read, um.is_claimed, um.created_at AS received_at,
            mi.id AS item_id, mi.object_id, mi.object_type, mi.quantity,
            cat.name AS object_name,
            cat.image AS object_image
        FROM (
            SELECT um.mail_id, um.is_read, um.is_claimed, um.created_at
            FROM user_mails um
            INNER JOIN mails m_sub ON um.mail_id = m_sub.id
            WHERE um.user_id = @UserId 
              AND um.is_deleted = FALSE
              AND (@Type IS NULL OR m_sub.type = @Type)
              AND (m_sub.expired_at IS NULL OR m_sub.expired_at > NOW())
            ORDER BY um.created_at DESC
            LIMIT @Limit OFFSET @Offset
        ) um_paged
        INNER JOIN mails m ON um_paged.mail_id = m.id
        LEFT JOIN mail_items mi ON m.id = mi.mail_id
        LEFT JOIN game_item_catalog cat 
            ON mi.object_id = cat.object_id 
            AND mi.object_type = cat.object_type
        ORDER BY um_paged.created_at DESC;";

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Type", (object)type ?? DBNull.Value);
                command.Parameters.AddWithValue("@Limit", pageSize);
                command.Parameters.AddWithValue("@Offset", offset);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string mailId = reader.GetString("id");

                        if (!mailMap.TryGetValue(mailId, out var mail))
                        {
                            mail = new Mails
                            {
                                Id = mailId,
                                SenderId = reader.IsDBNull(reader.GetOrdinal("sender_id")) ? null : reader.GetString("sender_id"),
                                Subject = reader.GetString("subject"),
                                Body = reader.IsDBNull(reader.GetOrdinal("body")) ? null : reader.GetString("body"),
                                Type = reader.GetString("type"),
                                IsGlobal = reader.GetBoolean("is_global"),
                                ExpiredAt = reader.IsDBNull(reader.GetOrdinal("expired_at")) ? null : reader.GetDateTime("expired_at"),
                                Items = new List<MailItems>()
                            };

                            // Map thông tin trạng thái cá nhân từ user_mails vào DTO/Entity
                            // (Gắn UserMail tương ứng vào navigation property)
                            mail.UserMails.Add(new UserMails
                            {
                                MailId = mailId,
                                UserId = userId,
                                IsRead = reader.GetBoolean("is_read"),
                                IsClaimed = reader.GetBoolean("is_claimed"),
                                CreatedAt = reader.GetDateTime("received_at")
                            });

                            mailMap.Add(mailId, mail);
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("item_id")))
                        {
                            mail.Items.Add(new MailItems
                            {
                                Id = reader.GetString("item_id"),
                                MailId = mailId,
                                ObjectId = reader.GetString("object_id"),
                                ObjectType = reader.GetString("object_type"),
                                Quantity = reader.GetInt32("quantity"),
                                ObjectName = reader.IsDBNull(reader.GetOrdinal("object_name")) ? null : reader.GetString("object_name"),
                                ObjectImage = reader.IsDBNull(reader.GetOrdinal("object_image")) ? null : reader.GetString("object_image")
                            });
                        }
                    }
                }
            }
        }

        return new List<Mails>(mailMap.Values);
    }

    /// <summary>
    /// Đếm số mail CHƯA ĐỌC để hiển thị Badge chấm đỏ trên UI
    /// </summary>
    public async Task<int> GetUnreadMailCountAsync(string userId)
    {
        string sql = @"
        SELECT COUNT(1) 
        FROM user_mails um
        INNER JOIN mails m ON um.mail_id = m.id
        WHERE um.user_id = @UserId 
          AND um.is_read = FALSE 
          AND um.is_deleted = FALSE
          AND (m.expired_at IS NULL OR m.expired_at > NOW());";

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                object result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }
    }

    #endregion

    #region 2. USER ACTIONS (MARK AS READ / CLAIM / DELETE)

    /// <summary>
    /// Đánh dấu một thư là ĐÃ ĐỌC
    /// </summary>
    public async Task<bool> MarkAsReadAsync(string userId, string mailId)
    {
        string sql = @"
        UPDATE user_mails 
        SET is_read = TRUE, 
            read_at = CURRENT_TIMESTAMP 
        WHERE user_id = @UserId 
          AND mail_id = @MailId 
          AND is_deleted = FALSE;";

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@MailId", mailId);
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }
    }

    /// <summary>
    /// Đánh dấu tất cả thư trong hòm thư là ĐÃ ĐỌC
    /// </summary>
    public async Task<int> MarkAllAsReadAsync(string userId)
    {
        string sql = @"
        UPDATE user_mails 
        SET is_read = TRUE, 
            read_at = CURRENT_TIMESTAMP 
        WHERE user_id = @UserId 
          AND is_read = FALSE 
          AND is_deleted = FALSE;";

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                return await command.ExecuteNonQueryAsync();
            }
        }
    }

    /// <summary>
    /// Đánh dấu ĐÃ NHẬN QUÀ (is_claimed = TRUE).
    /// Thường được gọi sau khi Inventory Service cộng quà thành công cho Player.
    /// </summary>
    public async Task<bool> MarkAsClaimedAsync(string userId, string mailId)
    {
        string sql = @"
        UPDATE user_mails 
        SET is_claimed = TRUE, 
            is_read = TRUE, 
            claimed_at = CURRENT_TIMESTAMP,
            read_at = COALESCE(read_at, CURRENT_TIMESTAMP)
        WHERE user_id = @UserId 
          AND mail_id = @MailId 
          AND is_claimed = FALSE 
          AND is_deleted = FALSE;";

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@MailId", mailId);
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }
    }

    /// <summary>
    /// Xóa mềm thư ở hòm thư cá nhân của User (Soft Delete)
    /// </summary>
    public async Task<bool> SoftDeleteAsync(string userId, string mailId)
    {
        string sql = @"
        UPDATE user_mails 
        SET is_deleted = TRUE 
        WHERE user_id = @UserId AND mail_id = @MailId;";

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@MailId", mailId);
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }
    }

    #endregion
}