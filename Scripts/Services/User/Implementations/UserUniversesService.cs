using System.Collections.Generic;
using System.Threading.Tasks;
public class UserUniversesService : IUserUniversesService
{
    private readonly IUserUniversesRepository _userUniversesRepository;

    public UserUniversesService(IUserUniversesRepository userUniversesRepository)
    {
        _userUniversesRepository = userUniversesRepository;
    }

    public static IUserUniversesService Create() => ServiceContainer.GetService<IUserUniversesService>();

    public async Task<UserUniverses> GetUserUniversesAsync(string userId, string id)
    {
        return await _userUniversesRepository.GetUserUniversesAsync(userId, id);
    }

    public async Task<UserUniverses> GetSumUserUniversesAsync(string userId)
    {
        return await _userUniversesRepository.GetSumUserUniversesAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserUniversesAsync(string userId, UserUniverses Universes, string id)
    {
        var insertOrUpdateResult = await _userUniversesRepository.InsertOrUpdateUserUniversesAsync(userId, Universes, id);

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