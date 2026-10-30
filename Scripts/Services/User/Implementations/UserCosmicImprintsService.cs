using System.Collections.Generic;
using System.Threading.Tasks;
public class UserCosmicImprintsService : IUserCosmicImprintsService
{
    private readonly IUserCosmicImprintsRepository _userCosmicImprintsRepository;

    public UserCosmicImprintsService(IUserCosmicImprintsRepository userCosmicImprintsRepository)
    {
        _userCosmicImprintsRepository = userCosmicImprintsRepository;
    }

    public static IUserCosmicImprintsService Create() => ServiceContainer.GetService<IUserCosmicImprintsService>();

    public async Task<UserCosmicImprints> GetUserCosmicImprintsAsync(string userId, string id)
    {
        return await _userCosmicImprintsRepository.GetUserCosmicImprintsAsync(userId, id);
    }

    public async Task<UserCosmicImprints> GetSumUserCosmicImprintsAsync(string userId)
    {
        return await _userCosmicImprintsRepository.GetSumUserCosmicImprintsAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCosmicImprintsAsync(string userId, UserCosmicImprints CosmicImprints, string type, List<RecipeItemDetailDTO> totalDeductions)
    {
        var insertOrUpdateResult = await _userCosmicImprintsRepository.InsertOrUpdateUserCosmicImprintsAsync(userId, CosmicImprints, type, totalDeductions);

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