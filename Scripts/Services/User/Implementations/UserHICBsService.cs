using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHICBsService : IUserHICBsService
{
    private readonly IUserHICBsRepository _userHICBsRepository;

    public UserHICBsService(IUserHICBsRepository userHICBsRepository)
    {
        _userHICBsRepository = userHICBsRepository;
    }

    public static IUserHICBsService Create() => ServiceContainer.GetService<IUserHICBsService>();

    public async Task<UserHICBs> GetUserHICBsAsync(string userId, string id)
    {
        return await _userHICBsRepository.GetUserHICBsAsync(userId, id);
    }

    public async Task<UserHICBs> GetSumUserHICBsAsync(string userId)
    {
        return await _userHICBsRepository.GetSumUserHICBsAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserHICBsAsync(string userId, UserHICBs HICBs, string id)
    {
        var insertOrUpdateResult = await _userHICBsRepository.InsertOrUpdateUserHICBsAsync(userId, HICBs, id);

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