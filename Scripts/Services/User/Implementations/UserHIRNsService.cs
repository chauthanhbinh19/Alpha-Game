using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHIRNsService : IUserHIRNsService
{
    private readonly IUserHIRNsRepository _userHIRNsRepository;

    public UserHIRNsService(IUserHIRNsRepository userHIRNsRepository)
    {
        _userHIRNsRepository = userHIRNsRepository;
    }

    public static IUserHIRNsService Create() => ServiceContainer.GetService<IUserHIRNsService>();

    public async Task<UserHIRNs> GetUserHIRNsAsync(string userId, string id)
    {
        return await _userHIRNsRepository.GetUserHIRNsAsync(userId, id);
    }

    public async Task<UserHIRNs> GetSumUserHIRNsAsync(string userId)
    {
        return await _userHIRNsRepository.GetSumUserHIRNsAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserHIRNsAsync(string userId, UserHIRNs HIRNs, string id)
    {
        var insertOrUpdateResult = await _userHIRNsRepository.InsertOrUpdateUserHIRNsAsync(userId, HIRNs, id);

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