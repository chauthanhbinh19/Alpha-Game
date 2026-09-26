using System.Collections.Generic;
using System.Threading.Tasks;
public class UserAnimesService : IUserAnimesService
{
    private readonly IUserAnimesRepository _userAnimesRepository;

    public UserAnimesService(IUserAnimesRepository userAnimesRepository)
    {
        _userAnimesRepository = userAnimesRepository;
    }

    public static IUserAnimesService Create() => ServiceContainer.GetService<IUserAnimesService>();

    public async Task<UserAnimes> GetUserAnimesAsync(string userId, string id)
    {
        return await _userAnimesRepository.GetUserAnimesAsync(userId, id);
    }

    public async Task<UserAnimes> GetSumUserAnimesAsync(string userId)
    {
        return await _userAnimesRepository.GetSumUserAnimesAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAnimesAsync(string userId, UserAnimes Animes, string id)
    {
        var insertOrUpdateResult = await _userAnimesRepository.InsertOrUpdateUserAnimesAsync(userId, Animes, id);

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