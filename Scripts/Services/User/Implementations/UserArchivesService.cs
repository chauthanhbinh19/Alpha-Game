using System.Collections.Generic;
using System.Threading.Tasks;
public class UserArchivesService : IUserArchivesService
{
    private readonly IUserArchivesRepository _userArchivesRepository;

    public UserArchivesService(IUserArchivesRepository userArchivesRepository)
    {
        _userArchivesRepository = userArchivesRepository;
    }

    public static IUserArchivesService Create() => ServiceContainer.GetService<IUserArchivesService>();

    public async Task<UserArchives> GetUserArchivesAsync(string userId, string id)
    {
        return await _userArchivesRepository.GetUserArchivesAsync(userId, id);
    }

    public async Task<UserArchives> GetSumUserArchivesAsync(string userId)
    {
        return await _userArchivesRepository.GetSumUserArchivesAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArchivesAsync(string userId, UserArchives Archives, string id)
    {
        var insertOrUpdateResult = await _userArchivesRepository.InsertOrUpdateUserArchivesAsync(userId, Archives, id);

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