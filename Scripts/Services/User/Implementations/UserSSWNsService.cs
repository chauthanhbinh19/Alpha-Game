using System.Collections.Generic;
using System.Threading.Tasks;
public class UserSSWNsService : IUserSSWNsService
{
    private readonly IUserSSWNsRepository _userSSWNsRepository;

    public UserSSWNsService(IUserSSWNsRepository userSSWNsRepository)
    {
        _userSSWNsRepository = userSSWNsRepository;
    }

    public static IUserSSWNsService Create() => ServiceContainer.GetService<IUserSSWNsService>();

    public async Task<UserSSWNs> GetUserSSWNsAsync(string userId, string id)
    {
        return await _userSSWNsRepository.GetUserSSWNsAsync(userId, id);
    }

    public async Task<UserSSWNs> GetSumUserSSWNsAsync(string userId)
    {
        return await _userSSWNsRepository.GetSumUserSSWNsAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSSWNsAsync(string userId, UserSSWNs SSWNs, string id)
    {
        var insertOrUpdateResult = await _userSSWNsRepository.InsertOrUpdateUserSSWNsAsync(userId, SSWNs, id);

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