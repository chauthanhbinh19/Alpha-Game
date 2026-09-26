using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHIDCsService : IUserHIDCsService
{
    private readonly IUserHIDCsRepository _userHIDCsRepository;

    public UserHIDCsService(IUserHIDCsRepository userHIDCsRepository)
    {
        _userHIDCsRepository = userHIDCsRepository;
    }

    public static IUserHIDCsService Create() => ServiceContainer.GetService<IUserHIDCsService>();

    public async Task<UserHIDCs> GetUserHIDCsAsync(string userId, string id)
    {
        return await _userHIDCsRepository.GetUserHIDCsAsync(userId, id);
    }

    public async Task<UserHIDCs> GetSumUserHIDCsAsync(string userId)
    {
        return await _userHIDCsRepository.GetSumUserHIDCsAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserHIDCsAsync(string userId, UserHIDCs HIDCs, string id)
    {
        var insertOrUpdateResult = await _userHIDCsRepository.InsertOrUpdateUserHIDCsAsync(userId, HIDCs, id);

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