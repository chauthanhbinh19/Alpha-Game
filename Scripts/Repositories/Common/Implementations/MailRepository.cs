using System;
using System.Threading.Tasks;
using MySqlConnector;

public class MailRepository : IMailRepository
{
    #region 1. INSERT MAIL
    /// <summary>
    /// Thêm một thư mới vào hệ thống
    /// </summary>
    public async Task<bool> InsertAsync(Mail mail)
    {
        // Tự động sinh Guid nếu chưa có Id
        if (string.IsNullOrEmpty(mail.Id))
        {
            mail.Id = Guid.NewGuid().ToString("N"); // Tạo chuỗi 32 ký tự không dấu -
        }
        string connectionString = DatabaseConfig.ConnectionString;

        string sql = @"
            INSERT INTO mail (
                id, receiver_id, sender_id, subject, body, type, 
                object_id, object_type, is_read, is_deleted, is_active
            ) VALUES (
                @Id, @ReceiverId, @SenderId, @Subject, @Body, @Type, 
                @ObjectId, @ObjectType, @IsRead, @IsDeleted, @IsActive
            );";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(sql, connection))
            {
                AddMailParameters(command, mail);

                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
    #endregion

    #region 2. UPDATE MAIL
    /// <summary>
    /// Cập nhật thông tin thư (Subject, Body, Type, IsRead, ...)
    /// </summary>
    public async Task<bool> UpdateAsync(Mail mail)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        string sql = @"
            UPDATE mail 
            SET 
                receiver_id = @ReceiverId,
                sender_id   = @SenderId,
                subject     = @Subject,
                body        = @Body,
                type        = @Type,
                object_id   = @ObjectId,
                object_type = @ObjectType,
                is_read     = @IsRead,
                is_active   = @IsActive
            WHERE id = @Id AND is_deleted = FALSE;";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(sql, connection))
            {
                AddMailParameters(command, mail);

                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }

    /// <summary>
    /// Cập nhật nhanh trạng thái Đã đọc (IsRead) của thư
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
                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
    #endregion

    #region MARK READ METHODS
    /// <summary>
    /// 2. Đánh dấu ĐÃ ĐỌC TẤT CẢ thư chưa đọc của một người nhận (ReceiverId)
    /// Trả về số lượng thư đã được cập nhật
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

                // Trả về số lượng dòng (thư) vừa được đổi trạng thái thành Đã đọc
                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected;
            }
        }
    }

    #endregion

    #region 3. DELETE MAIL
    /// <summary>
    /// Xóa mềm thư (Soft Delete - Đổi is_deleted thành TRUE)
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
                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }

    /// <summary>
    /// Xóa cứng khỏi Database (Hard Delete)
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
                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
    #endregion

    #region HELPER METHOD
    private void AddMailParameters(MySqlCommand command, Mail mail)
    {
        command.Parameters.AddWithValue("@Id", mail.Id ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@ReceiverId", mail.ReceiverId ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@SenderId", mail.SenderId ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@Subject", mail.Subject ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@Body", mail.Body ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@Type", mail.Type ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@ObjectId", mail.ObjectId ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@ObjectType", mail.ObjectType ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@IsRead", mail.IsRead);
        command.Parameters.AddWithValue("@IsDeleted", mail.IsDeleted);
        command.Parameters.AddWithValue("@IsActive", mail.IsActive);
    }
    #endregion
}