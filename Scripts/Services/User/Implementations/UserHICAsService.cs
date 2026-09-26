using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHICAsService : IUserHICAsService
{
    private readonly IUserHICAsRepository _userHICAsRepository;

    public UserHICAsService(IUserHICAsRepository userHICAsRepository)
    {
        _userHICAsRepository = userHICAsRepository;
    }

    public static IUserHICAsService Create() => ServiceContainer.GetService<IUserHICAsService>();

    public async Task<UserHICAs> GetUserHICAsAsync(string userId, string id)
    {
        return await _userHICAsRepository.GetUserHICAsAsync(userId, id);
    }

    public async Task<UserHICAs> GetSumUserHICAsAsync(string userId)
    {
        return await _userHICAsRepository.GetSumUserHICAsAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserHICAsAsync(string userId, UserHICAs HICAs, string id)
    {
        var insertOrUpdateResult = await _userHICAsRepository.InsertOrUpdateUserHICAsAsync(userId, HICAs, id);

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