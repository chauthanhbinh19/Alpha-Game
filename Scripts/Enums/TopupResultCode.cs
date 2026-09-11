public enum TopupResultCode
{
    /// <summary>
        /// Xử lý thành công / Đã cộng tiền/vật phẩm vào tài khoản
        /// </summary>
        Success = 0,

        /// <summary>
        /// Gói nạp không tồn tại hoặc không ở trạng thái hoạt động (is_active = 0)
        /// </summary>
        PackageNotFound = 1,

        /// <summary>
        /// Giao dịch đã được xử lý trước đó (Idempotency check từ DB)
        /// </summary>
        AlreadyProcessed = 2,

        /// <summary>
        /// Người chơi không tồn tại trong hệ thống
        /// </summary>
        PlayerNotFound = 3,

        /// <summary>
        /// Đã khởi tạo giao dịch ở trạng thái PENDING thành công (Dùng cho cổng VNPAY/Momo/QR)
        /// </summary>
        PendingCreated = 4,

        /// <summary>
        /// Không tìm thấy giao dịch PENDING tương ứng để FULFILL
        /// </summary>
        PendingNotFound = 5,

        /// <summary>
        /// Action Type truyền vào Stored Procedure không hợp lệ
        /// </summary>
        InvalidActionType = 98,

        /// <summary>
        /// Lỗi hệ thống hoặc Exception cơ sở dữ liệu
        /// </summary>
        DatabaseError = 99
}