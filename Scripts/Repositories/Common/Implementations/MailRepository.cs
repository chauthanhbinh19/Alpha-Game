using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

public class MailRepository : IMailRepository
{
    #region GET MAIL METHODS (FOR UI)

    /// <summary>
    /// 1. Lấy danh sách hòm thư đến của Receiver kèm theo danh sách items (Phân trang)
    /// </summary>
    public async Task<List<Mail>> GetUserMailsAsync(string receiverId, string type, int page = 1, int pageSize = 20)
    {
        var mailMap = new Dictionary<string, Mail>();
        int offset = (page - 1) * pageSize;
        string connectionString = DatabaseConfig.ConnectionString;

        // Query lấy thông tin mail và JOIN với bảng mail_items
        string sql = @"
        SELECT 
            m.id, m.receiver_id, m.sender_id, m.subject, m.body, m.type, 
            m.is_read, m.is_claimed, m.created_at, m.is_deleted, m.is_active,
            mi.id AS item_id, mi.object_id, mi.object_type, mi.quantity,
            cat.name AS object_name,
            cat.image AS object_image,
        FROM (
            SELECT id, receiver_id, sender_id, subject, body, type, is_read, is_claimed, created_at, is_deleted, is_active
            FROM mail
            WHERE receiver_id = @ReceiverId 
              AND type = @Type
              AND is_deleted = FALSE 
              AND is_active = TRUE
            ORDER BY created_at DESC
            LIMIT @Limit OFFSET @Offset
        ) m
        LEFT JOIN mail_items mi ON m.id = mi.mail_id
        LEFT JOIN game_item_catalog cat 
            ON mi.object_id = cat.object_id 
            AND mi.object_type = cat.object_type
        ORDER BY m.created_at DESC;";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@ReceiverId", receiverId);
                command.Parameters.AddWithValue("@Type", type);
                command.Parameters.AddWithValue("@Limit", pageSize);
                command.Parameters.AddWithValue("@Offset", offset);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string mailId = reader.GetString("id");

                        if (!mailMap.TryGetValue(mailId, out var mail))
                        {
                            mail = MapReaderToMail(reader);
                            mailMap.Add(mailId, mail);
                        }

                        // Nếu có vật phẩm đi kèm, map và add vào danh sách Items
                        if (!reader.IsDBNull(reader.GetOrdinal("item_id")))
                        {
                            mail.Items.Add(MapReaderToMailItem(reader));
                        }
                    }
                }
            }
        }

        return new List<Mail>(mailMap.Values);
    }

    /// <summary>
    /// 2. Lấy chi tiết 1 thư theo MailId (bao gồm đầy đủ danh sách items)
    /// </summary>
    public async Task<Mail> GetMailByIdAsync(string mailId)
    {
        Mail mail = null;
        string connectionString = DatabaseConfig.ConnectionString;

        string sql = @"
        SELECT 
            m.id, m.receiver_id, m.sender_id, m.subject, m.body, m.type, 
            m.is_read, m.is_claimed, m.created_at, m.is_deleted, m.is_active,
            mi.id AS item_id, mi.object_id, mi.object_type, mi.quantity,
            cat.name AS object_name,
            cat.image AS object_image,
        FROM mail m
        LEFT JOIN mail_items mi ON m.id = mi.mail_id
        LEFT JOIN game_item_catalog cat 
            ON mi.object_id = cat.object_id 
            AND mi.object_type = cat.object_type
        WHERE m.id = @MailId 
          AND m.is_deleted = FALSE 
        LIMIT 1;";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@MailId", mailId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        if (mail == null)
                        {
                            mail = MapReaderToMail(reader);
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("item_id")))
                        {
                            mail.Items.Add(MapReaderToMailItem(reader));
                        }
                    }
                }
            }
        }

        return mail;
    }

    /// <summary>
    /// 3. Đếm số thư CHƯA ĐỌC để hiển thị Badge chấm đỏ trên UI
    /// </summary>
    public async Task<int> GetUnreadMailCountAsync(string receiverId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        string sql = @"
        SELECT COUNT(1) 
        FROM mail 
        WHERE receiver_id = @ReceiverId 
          AND is_read = FALSE 
          AND is_deleted = FALSE 
          AND is_active = TRUE;";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@ReceiverId", receiverId);
                object result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }
    }

    #endregion

    #region 1. INSERT MAIL WITH ITEMS

    /// <summary>
    /// Thêm một thư mới và danh sách vật phẩm đi kèm vào hệ thống (Transaction)
    /// </summary>
    public async Task<bool> InsertAsync(Mail mail)
    {
        if (string.IsNullOrEmpty(mail.Id))
        {
            mail.Id = Guid.NewGuid().ToString("N");
        }

        string connectionString = DatabaseConfig.ConnectionString;

        string insertMailSql = @"
        INSERT INTO mail (
            id, receiver_id, sender_id, subject, body, type, 
            is_read, is_claimed, is_deleted, is_active
        ) VALUES (
            @Id, @ReceiverId, @SenderId, @Subject, @Body, @Type, 
            @IsRead, @IsClaimed, @IsDeleted, @IsActive
        );";

        string insertItemSql = @"
        INSERT INTO mail_items (id, mail_id, object_id, object_type, quantity)
        VALUES (@Id, @MailId, @ObjectId, @ObjectType, @Quantity);";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var transaction = await connection.BeginTransactionAsync())
            {
                try
                {
                    // 1. Thêm Thư vào bảng mail
                    using (var cmdMail = new MySqlCommand(insertMailSql, connection, transaction))
                    {
                        AddMailParameters(cmdMail, mail);
                        await cmdMail.ExecuteNonQueryAsync();
                    }

                    // 2. Thêm từng Vật phẩm vào bảng mail_items
                    if (mail.Items != null && mail.Items.Count > 0)
                    {
                        foreach (var item in mail.Items)
                        {
                            using (var cmdItem = new MySqlCommand(insertItemSql, connection, transaction))
                            {
                                cmdItem.Parameters.AddWithValue("@Id", string.IsNullOrEmpty(item.Id) ? Guid.NewGuid().ToString("N") : item.Id);
                                cmdItem.Parameters.AddWithValue("@MailId", mail.Id);
                                cmdItem.Parameters.AddWithValue("@ObjectId", item.ObjectId);
                                cmdItem.Parameters.AddWithValue("@ObjectType", item.ObjectType);
                                cmdItem.Parameters.AddWithValue("@Quantity", item.Quantity);

                                await cmdItem.ExecuteNonQueryAsync();
                            }
                        }
                    }

                    await transaction.CommitAsync();
                    return true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }

    #endregion

    #region 2. UPDATE MAIL WITH ITEMS

    /// <summary>
    /// Cập nhật thông tin thư và danh sách vật phẩm (Transaction)
    /// </summary>
    public async Task<bool> UpdateAsync(Mail mail)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        string updateMailSql = @"
        UPDATE mail 
        SET 
            receiver_id = @ReceiverId,
            sender_id   = @SenderId,
            subject     = @Subject,
            body        = @Body,
            type        = @Type,
            is_read     = @IsRead,
            is_claimed  = @IsClaimed,
            is_active   = @IsActive
        WHERE id = @Id AND is_deleted = FALSE;";

        string deleteOldItemsSql = "DELETE FROM mail_items WHERE mail_id = @MailId;";

        string insertItemSql = @"
        INSERT INTO mail_items (id, mail_id, object_id, object_type, quantity)
        VALUES (@Id, @MailId, @ObjectId, @ObjectType, @Quantity);";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var transaction = await connection.BeginTransactionAsync())
            {
                try
                {
                    // 1. Update thông tin bảng mail
                    int rowsAffected;
                    using (var cmdMail = new MySqlCommand(updateMailSql, connection, transaction))
                    {
                        AddMailParameters(cmdMail, mail);
                        rowsAffected = await cmdMail.ExecuteNonQueryAsync();
                    }

                    if (rowsAffected == 0)
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }

                    // 2. Làm sạch items cũ để chèn lại items mới
                    using (var cmdDelete = new MySqlCommand(deleteOldItemsSql, connection, transaction))
                    {
                        cmdDelete.Parameters.AddWithValue("@MailId", mail.Id);
                        await cmdDelete.ExecuteNonQueryAsync();
                    }

                    // 3. Re-insert danh sách items mới
                    if (mail.Items != null && mail.Items.Count > 0)
                    {
                        foreach (var item in mail.Items)
                        {
                            using (var cmdItem = new MySqlCommand(insertItemSql, connection, transaction))
                            {
                                cmdItem.Parameters.AddWithValue("@Id", string.IsNullOrEmpty(item.Id) ? Guid.NewGuid().ToString("N") : item.Id);
                                cmdItem.Parameters.AddWithValue("@MailId", mail.Id);
                                cmdItem.Parameters.AddWithValue("@ObjectId", item.ObjectId);
                                cmdItem.Parameters.AddWithValue("@ObjectType", item.ObjectType);
                                cmdItem.Parameters.AddWithValue("@Quantity", item.Quantity);

                                await cmdItem.ExecuteNonQueryAsync();
                            }
                        }
                    }

                    await transaction.CommitAsync();
                    return true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }

    /// <summary>
    /// Cập nhật nhanh trạng thái Đã đọc (IsRead)
    /// </summary>
    public async Task<bool> MarkAsReadAsync(string mailId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        string sql = "UPDATE mail SET is_read = TRUE WHERE id = @Id AND is_deleted = FALSE;";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Id", mailId);
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }
    }

    /// <summary>
    /// Cập nhật trạng thái Đã nhận quà (IsClaimed)
    /// </summary>
    public async Task<bool> MarkAsClaimedAsync(string mailId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        string sql = "UPDATE mail SET is_claimed = TRUE, is_read = TRUE WHERE id = @Id AND is_deleted = FALSE;";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Id", mailId);
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }
    }

    /// <summary>
    /// Đánh dấu ĐÃ ĐỌC TẤT CẢ thư chưa đọc của Receiver
    /// </summary>
    public async Task<int> MarkAllAsReadByReceiverAsync(string receiverId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        string sql = @"
        UPDATE mail 
        SET is_read = TRUE 
        WHERE receiver_id = @ReceiverId 
          AND is_deleted = FALSE 
          AND is_read = FALSE;";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@ReceiverId", receiverId);
                return await command.ExecuteNonQueryAsync();
            }
        }
    }

    #endregion

    #region 3. DELETE MAIL

    /// <summary>
    /// Xóa mềm thư (Soft Delete)
    /// </summary>
    public async Task<bool> SoftDeleteAsync(string mailId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        string sql = "UPDATE mail SET is_deleted = TRUE, is_active = FALSE WHERE id = @Id;";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Id", mailId);
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }
    }

    /// <summary>
    /// Xóa cứng khỏi Database (Hard Delete cascade xóa luôn mail_items)
    /// </summary>
    public async Task<bool> HardDeleteAsync(string mailId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        string sql = "DELETE FROM mail WHERE id = @Id;";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Id", mailId);
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }
    }

    #endregion

    #region HELPER MAPPERS

    private Mail MapReaderToMail(MySqlDataReader reader)
    {
        return new Mail
        {
            Id = reader.GetString("id"),
            ReceiverId = reader.GetString("receiver_id"),
            SenderId = reader.GetString("sender_id"),
            Subject = reader.GetString("subject"),
            Body = reader.IsDBNull(reader.GetOrdinal("body")) ? null : reader.GetString("body"),
            Type = reader.IsDBNull(reader.GetOrdinal("type")) ? null : reader.GetString("type"),
            IsRead = reader.GetBoolean("is_read"),
            IsClaimed = reader.GetBoolean("is_claimed"),
            CreatedAt = reader.GetDateTime("create_at"),
            IsDeleted = reader.GetBoolean("is_deleted"),
            IsActive = reader.GetBoolean("is_active"),
            Items = new List<MailItems>()
        };
    }

    private MailItems MapReaderToMailItem(MySqlDataReader reader)
    {
        return new MailItems
        {
            Id = reader.GetString("item_id"),
            MailId = reader.GetString("id"),
            ObjectId = reader.GetString("object_id"),
            ObjectType = reader.GetString("object_type"),
            Quantity = reader.GetInt32("quantity"),
            ObjectName = reader.GetString("object_name"),
            ObjectImage = reader.GetString("object_image")
        };
    }

    private void AddMailParameters(MySqlCommand command, Mail mail)
    {
        command.Parameters.AddWithValue("@Id", mail.Id ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@ReceiverId", mail.ReceiverId ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@SenderId", mail.SenderId ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@Subject", mail.Subject ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@Body", mail.Body ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@Type", mail.Type ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@IsRead", mail.IsRead);
        command.Parameters.AddWithValue("@IsClaimed", mail.IsClaimed);
        command.Parameters.AddWithValue("@IsDeleted", mail.IsDeleted);
        command.Parameters.AddWithValue("@IsActive", mail.IsActive);
    }

    #endregion
}