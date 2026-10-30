using System.Collections.Generic;
using System.Threading.Tasks;
public class UserPathOfLordImprintsService : IUserPathOfLordImprintsService
{
    private readonly IUserPathOfLordImprintsRepository _userPathOfLordImprintsRepository;

    public UserPathOfLordImprintsService(IUserPathOfLordImprintsRepository userPathOfLordImprintsRepository)
    {
        _userPathOfLordImprintsRepository = userPathOfLordImprintsRepository;
    }

    public static IUserPathOfLordImprintsService Create() => ServiceContainer.GetService<IUserPathOfLordImprintsService>();

    public async Task<UserPathOfLordImprints> GetUserPathOfLordImprintsAsync(string userId, string id)
    {
        return await _userPathOfLordImprintsRepository.GetUserPathOfLordImprintsAsync(userId, id);
    }

    public async Task<UserPathOfLordImprints> GetSumUserPathOfLordImprintsAsync(string userId)
    {
        return await _userPathOfLordImprintsRepository.GetSumUserPathOfLordImprintsAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPathOfLordImprintsAsync(string userId, UserPathOfLordImprints PathOfLordImprints, string type, List<RecipeItemDetailDTO> totalDeductions)
    {
        var insertOrUpdateResult = await _userPathOfLordImprintsRepository.InsertOrUpdateUserPathOfLordImprintsAsync(userId, PathOfLordImprints, type, totalDeductions);

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