using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHIINsService : IUserHIINsService
{
    private readonly IUserHIINsRepository _userHIINsRepository;

    public UserHIINsService(IUserHIINsRepository userHIINsRepository)
    {
        _userHIINsRepository = userHIINsRepository;
    }

    public static IUserHIINsService Create() => ServiceContainer.GetService<IUserHIINsService>();

    public async Task<UserHIINs> GetUserHIINsAsync(string userId, string id)
    {
        return await _userHIINsRepository.GetUserHIINsAsync(userId, id);
    }

    public async Task<UserHIINs> GetSumUserHIINsAsync(string userId)
    {
        return await _userHIINsRepository.GetSumUserHIINsAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserHIINsAsync(string userId, UserHIINs HIINs, string id)
    {
        var insertOrUpdateResult = await _userHIINsRepository.InsertOrUpdateUserHIINsAsync(userId, HIINs, id);

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