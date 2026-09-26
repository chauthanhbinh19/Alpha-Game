using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHITNsService : IUserHITNsService
{
    private readonly IUserHITNsRepository _userHITNsRepository;

    public UserHITNsService(IUserHITNsRepository userHITNsRepository)
    {
        _userHITNsRepository = userHITNsRepository;
    }

    public static IUserHITNsService Create() => ServiceContainer.GetService<IUserHITNsService>();

    public async Task<UserHITNs> GetUserHITNsAsync(string userId, string id)
    {
        return await _userHITNsRepository.GetUserHITNsAsync(userId, id);
    }

    public async Task<UserHITNs> GetSumUserHITNsAsync(string userId)
    {
        return await _userHITNsRepository.GetSumUserHITNsAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserHITNsAsync(string userId, UserHITNs HITNs, string id)
    {
        var insertOrUpdateResult = await _userHITNsRepository.InsertOrUpdateUserHITNsAsync(userId, HITNs, id);

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