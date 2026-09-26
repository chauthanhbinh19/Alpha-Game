using System.Collections.Generic;
using System.Threading.Tasks;
public class UserResearchsService : IUserResearchsService
{
    private readonly IUserResearchsRepository _userResearchsRepository;

    public UserResearchsService(IUserResearchsRepository userResearchsRepository)
    {
        _userResearchsRepository = userResearchsRepository;
    }

    public static IUserResearchsService Create() => ServiceContainer.GetService<IUserResearchsService>();

    public async Task<UserResearchs> GetUserResearchsAsync(string userId, string id)
    {
        return await _userResearchsRepository.GetUserResearchsAsync(userId, id);
    }

    public async Task<UserResearchs> GetSumUserResearchsAsync(string userId)
    {
        return await _userResearchsRepository.GetSumUserResearchsAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserResearchsAsync(string userId, UserResearchs Researchs, string id)
    {
        var insertOrUpdateResult = await _userResearchsRepository.InsertOrUpdateUserResearchsAsync(userId, Researchs, id);

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