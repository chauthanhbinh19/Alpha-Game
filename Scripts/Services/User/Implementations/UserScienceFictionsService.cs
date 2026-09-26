using System.Collections.Generic;
using System.Threading.Tasks;
public class UserScienceFictionsService : IUserScienceFictionsService
{
    private readonly IUserScienceFictionsRepository _userScienceFictionsRepository;

    public UserScienceFictionsService(IUserScienceFictionsRepository scienceFictionsRepository)
    {
        _userScienceFictionsRepository = scienceFictionsRepository;
    }

    public static IUserScienceFictionsService Create() => ServiceContainer.GetService<IUserScienceFictionsService>();

    public async Task<UserScienceFictions> GetUserScienceFictionsAsync(string userId, string id)
    {
        return await _userScienceFictionsRepository.GetUserScienceFictionsAsync(userId, id);
    }

    public async Task<UserScienceFictions> GetSumUserScienceFictionsAsync(string userId)
    {
        return await _userScienceFictionsRepository.GetSumUserScienceFictionsAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserScienceFictionsAsync(string userId, UserScienceFictions ScienceFictions, string id)
    {
        var insertOrUpdateResult = await _userScienceFictionsRepository.InsertOrUpdateUserScienceFictionsAsync(userId, ScienceFictions, id);

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