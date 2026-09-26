using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHISNsService : IUserHISNsService
{
    private readonly IUserHISNsRepository _userHISNsRepository;

    public UserHISNsService(IUserHISNsRepository userHISNsRepository)
    {
        _userHISNsRepository = userHISNsRepository;
    }

    public static IUserHISNsService Create() => ServiceContainer.GetService<IUserHISNsService>();

    public async Task<UserHISNs> GetUserHISNsAsync(string userId, string id)
    {
        return await _userHISNsRepository.GetUserHISNsAsync(userId, id);
    }

    public async Task<UserHISNs> GetSumUserHISNsAsync(string userId)
    {
        return await _userHISNsRepository.GetSumUserHISNsAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserHISNsAsync(string userId, UserHISNs HISNs, string id)
    {
        var insertOrUpdateResult = await _userHISNsRepository.InsertOrUpdateUserHISNsAsync(userId, HISNs, id);

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