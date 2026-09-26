using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHIENsService : IUserHIENsService
{
    private readonly IUserHIENsRepository _userHIENsRepository;

    public UserHIENsService(IUserHIENsRepository userHIENsRepository)
    {
        _userHIENsRepository = userHIENsRepository;
    }

    public static IUserHIENsService Create() => ServiceContainer.GetService<IUserHIENsService>();

    public async Task<UserHIENs> GetUserHIENsAsync(string userId, string id)
    {
        return await _userHIENsRepository.GetUserHIENsAsync(userId, id);
    }

    public async Task<UserHIENs> GetSumUserHIENsAsync(string userId)
    {
        return await _userHIENsRepository.GetSumUserHIENsAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserHIENsAsync(string userId, UserHIENs HIENs, string id)
    {
        var insertOrUpdateResult = await _userHIENsRepository.InsertOrUpdateUserHIENsAsync(userId, HIENs, id);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = true,
                OperationType = DatabaseOperationType.Updated,
                IsChangePower = true,
                Message = MessageConstants.UPDATED_SUCCESSFULLY
            };
        }

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = true,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    
}