using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

public class MailsRepository : IMailsRepository
{
    private readonly string _connectionString = DatabaseConfig.ConnectionString;

    #region 1. MANAGEMENT METHODS FOR ADMIN (CREATE / UPDATE / DELETE MAILS)

    /// <summary>
    /// Admin tạo mẫu mail mới kèm danh sách quà đính kèm
    /// </summary>
    public async Task<bool> CreateMailAsync(Mails mail)
    {
        if (string.IsNullOrEmpty(mail.Id))
            mail.Id = Guid.NewGuid().ToString("N");

        string insertMailSql = @"
        INSERT INTO mails (id, sender_id, subject, body, type, is_global, expired_at)
        VALUES (@Id, @SenderId, @Subject, @Body, @Type, @IsGlobal, @ExpiredAt);";

        string insertItemSql = @"
        INSERT INTO mail_items (id, mail_id, object_id, object_type, quantity)
        VALUES (@Id, @MailId, @ObjectId, @ObjectType, @Quantity);";

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var transaction = await connection.BeginTransactionAsync())
            {
                try
                {
                    // 1. Chèn vào bảng mails
                    using (var cmdMail = new MySqlCommand(insertMailSql, connection, transaction))
                    {
                        cmdMail.Parameters.AddWithValue("@Id", mail.Id);
                        cmdMail.Parameters.AddWithValue("@SenderId", (object)mail.SenderId ?? DBNull.Value);
                        cmdMail.Parameters.AddWithValue("@Subject", mail.Subject);
                        cmdMail.Parameters.AddWithValue("@Body", (object)mail.Body ?? DBNull.Value);
                        cmdMail.Parameters.AddWithValue("@Type", mail.Type);
                        cmdMail.Parameters.AddWithValue("@IsGlobal", mail.IsGlobal);
                        cmdMail.Parameters.AddWithValue("@ExpiredAt", (object)mail.ExpiredAt ?? DBNull.Value);
                        await cmdMail.ExecuteNonQueryAsync();
                    }

                    // 2. Chèn danh sách vật phẩm đính kèm
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
    /// Admin cập nhật nội dung thư mẫu và danh sách vật phẩm
    /// </summary>
    public async Task<bool> UpdateMailAsync(Mails mail)
    {
        string updateMailSql = @"
        UPDATE mails 
        SET sender_id = @SenderId,
            subject   = @Subject,
            body      = @Body,
            type      = @Type,
            is_global = @IsGlobal,
            expired_at = @ExpiredAt
        WHERE id = @Id;";

        string deleteOldItemsSql = "DELETE FROM mail_items WHERE mail_id = @MailId;";

        string insertItemSql = @"
        INSERT INTO mail_items (id, mail_id, object_id, object_type, quantity)
        VALUES (@Id, @MailId, @ObjectId, @ObjectType, @Quantity);";

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var transaction = await connection.BeginTransactionAsync())
            {
                try
                {
                    // 1. Cập nhật bảng mails
                    int rowsAffected;
                    using (var cmdMail = new MySqlCommand(updateMailSql, connection, transaction))
                    {
                        cmdMail.Parameters.AddWithValue("@Id", mail.Id);
                        cmdMail.Parameters.AddWithValue("@SenderId", (object)mail.SenderId ?? DBNull.Value);
                        cmdMail.Parameters.AddWithValue("@Subject", mail.Subject);
                        cmdMail.Parameters.AddWithValue("@Body", (object)mail.Body ?? DBNull.Value);
                        cmdMail.Parameters.AddWithValue("@Type", mail.Type);
                        cmdMail.Parameters.AddWithValue("@IsGlobal", mail.IsGlobal);
                        cmdMail.Parameters.AddWithValue("@ExpiredAt", (object)mail.ExpiredAt ?? DBNull.Value);
                        rowsAffected = await cmdMail.ExecuteNonQueryAsync();
                    }

                    if (rowsAffected == 0)
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }

                    // 2. Xóa các items cũ
                    using (var cmdDelete = new MySqlCommand(deleteOldItemsSql, connection, transaction))
                    {
                        cmdDelete.Parameters.AddWithValue("@MailId", mail.Id);
                        await cmdDelete.ExecuteNonQueryAsync();
                    }

                    // 3. Chèn danh sách items mới
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
    /// Admin thu hồi / xóa cứng mẫu mail (Nhờ ON DELETE CASCADE sẽ tự động làm sạch mail_items & user_mails)
    /// </summary>
    public async Task<bool> DeleteMailAsync(string mailId)
    {
        string sql = "DELETE FROM mails WHERE id = @Id;";

        using (var connection = new MySqlConnection(_connectionString))
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

    #region 2. SEND MAIL TO USERS (DISPATCHING)

    /// <summary>
    /// Gửi một mẫu thư sẵn có tới danh sách User (Batch Insert)
    /// </summary>
    public async Task<bool> SendMailToUsersAsync(string mailId, List<string> userIds)
    {
        if (userIds == null || userIds.Count == 0) return false;

        string sql = @"
        INSERT INTO user_mails (id, user_id, mail_id) 
        VALUES (@Id, @UserId, @MailId)
        ON DUPLICATE KEY UPDATE is_deleted = FALSE;"; // Nếu đã gửi rồi thì khôi phục lại trạng thái chưa xóa

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var transaction = await connection.BeginTransactionAsync())
            {
                try
                {
                    foreach (var userId in userIds)
                    {
                        using (var command = new MySqlCommand(sql, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Id", Guid.NewGuid().ToString("N"));
                            command.Parameters.AddWithValue("@UserId", userId);
                            command.Parameters.AddWithValue("@MailId", mailId);
                            await command.ExecuteNonQueryAsync();
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

    #region 3. ADMIN QUERY METHODS

    /// <summary>
    /// Lấy danh sách tất cả các Mẫu thư đã tạo trong hệ thống Admin (Phân trang)
    /// </summary>
    public async Task<List<Mails>> GetMailTemplatesAsync(int page = 1, int pageSize = 20)
    {
        var mailMap = new Dictionary<string, Mails>();
        int offset = (page - 1) * pageSize;

        string sql = @"
        SELECT 
            m.id, m.sender_id, m.subject, m.body, m.type, m.is_global, m.expired_at,
            mi.id AS item_id, mi.object_id, mi.object_type, mi.quantity,
            cat.name AS object_name,
            cat.image AS object_image
        FROM (
            SELECT id, sender_id, subject, body, type, is_global, expired_at, created_at
            FROM mails
            ORDER BY created_at DESC
            LIMIT @Limit OFFSET @Offset
        ) m
        LEFT JOIN mail_items mi ON m.id = mi.mail_id
        LEFT JOIN game_item_catalog cat 
            ON mi.object_id = cat.object_id 
            AND mi.object_type = cat.object_type
        ORDER BY m.created_at DESC;";

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(sql, connection))
            {
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

    #endregion
}