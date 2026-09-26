using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHIHNsService : IUserHIHNsService
{
    private readonly IUserHIHNsRepository _userHIHNsRepository;

    public UserHIHNsService(IUserHIHNsRepository userHIHNsRepository)
    {
        _userHIHNsRepository = userHIHNsRepository;
    }

    public static IUserHIHNsService Create() => ServiceContainer.GetService<IUserHIHNsService>();

    public async Task<UserHIHNs> GetUserHIHNsAsync(string userId, string id)
    {
        return await _userHIHNsRepository.GetUserHIHNsAsync(userId, id);
    }

    public async Task<UserHIHNs> GetSumUserHIHNsAsync(string userId)
    {
        return await _userHIHNsRepository.GetSumUserHIHNsAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserHIHNsAsync(string userId, UserHIHNs HIHNs, string id)
    {
        var insertOrUpdateResult = await _userHIHNsRepository.InsertOrUpdateUserHIHNsAsync(userId, HIHNs, id);

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