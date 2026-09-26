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

    public bool IsChangePower { get; set; }

    /// <summary>
    /// Thông báo hoặc lỗi (nếu có)
    /// </summary>
    public string Message { get; set; }

    // === Factory Methods giúp tạo object nhanh & viết code sạch hơn ===
    public static InsertOrUpdateResult<T> Success(T data, string message = MessageConstants.INSERTED_SUCCESSFULLY, bool isChangePower = false)
    {
        return new InsertOrUpdateResult<T>
        {
            Data = data,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = isChangePower,
            Message = message
        };
    }


    public static InsertOrUpdateResult<T> Inserted(T data, string message = MessageConstants.INSERTED_SUCCESSFULLY, bool isChangePower = false)
    {
        return new InsertOrUpdateResult<T>
        {
            Data = data,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = isChangePower,
            Message = message
        };
    }

    public static InsertOrUpdateResult<T> Updated(T data, string message = MessageConstants.UPDATED_SUCCESSFULLY, bool isChangePower = false)
    {
        return new InsertOrUpdateResult<T>
        {
            Data = data,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = isChangePower,
            Message = message
        };
    }

    public static InsertOrUpdateResult<T> Mixed(T data, string message = MessageConstants.UPDATED_SUCCESSFULLY, bool isChangePower = false)
    {
        return new InsertOrUpdateResult<T>
        {
            Data = data,
            OperationType = DatabaseOperationType.Mixed,
            IsChangePower = isChangePower,
            Message = message
        };
    }

    public static InsertOrUpdateResult<T> Failure(string errorMessage = MessageConstants.FAILED_TO_EXECUTE_ACTION, bool isChangePower = false)
    {
        return new InsertOrUpdateResult<T>
        {
            Data = default,
            OperationType = DatabaseOperationType.Failed,
            IsChangePower = isChangePower,
            Message = errorMessage
        };
    }
}