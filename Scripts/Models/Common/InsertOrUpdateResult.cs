public class InsertOrUpdateResult<T>
{
    /// <summary>
    /// Dữ liệu Entity sau khi Insert hoặc Update thành công
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// Loại hành động: Inserted, Updated, hoặc Failed
    /// </summary>
    public DatabaseOperationType OperationType { get; set; }

    /// <summary>
    /// Trạng thái thành công hay thất bại
    /// </summary>
    public bool IsSuccess => OperationType == DatabaseOperationType.Inserted 
                          || OperationType == DatabaseOperationType.Updated;

    /// <summary>
    /// Thông báo hoặc lỗi (nếu có)
    /// </summary>
    public string Message { get; set; }

    // === Factory Methods giúp tạo object nhanh & viết code sạch hơn ===

    public static InsertOrUpdateResult<T> Inserted(T data, string message = MessageConstants.INSERTED_SUCCESSFULLY)
    {
        return new InsertOrUpdateResult<T>
        {
            Data = data,
            OperationType = DatabaseOperationType.Inserted,
            Message = message
        };
    }

    public static InsertOrUpdateResult<T> Updated(T data, string message = MessageConstants.UPDATED_SUCCESSFULLY)
    {
        return new InsertOrUpdateResult<T>
        {
            Data = data,
            OperationType = DatabaseOperationType.Updated,
            Message = message
        };
    }

    public static InsertOrUpdateResult<T> Mixed(T data, string message = MessageConstants.UPDATED_SUCCESSFULLY)
    {
        return new InsertOrUpdateResult<T>
        {
            Data = data,
            OperationType = DatabaseOperationType.Mixed,
            Message = message
        };
    }

    public static InsertOrUpdateResult<T> Failure(string errorMessage = MessageConstants.FAILED_TO_EXECUTE_ACTION)
    {
        return new InsertOrUpdateResult<T>
        {
            Data = default,
            OperationType = DatabaseOperationType.Failed,
            Message = errorMessage
        };
    }
}