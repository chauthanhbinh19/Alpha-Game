using System.Collections.Generic;
using System.Threading.Tasks;
public class UserPrimordialImprintsService : IUserPrimordialImprintsService
{
    private readonly IUserPrimordialImprintsRepository _userPrimordialImprintsRepository;

    public UserPrimordialImprintsService(IUserPrimordialImprintsRepository userPrimordialImprintsRepository)
    {
        _userPrimordialImprintsRepository = userPrimordialImprintsRepository;
    }

    public static IUserPrimordialImprintsService Create() => ServiceContainer.GetService<IUserPrimordialImprintsService>();

    public async Task<UserPrimordialImprints> GetUserPrimordialImprintsAsync(string userId, string id)
    {
        return await _userPrimordialImprintsRepository.GetUserPrimordialImprintsAsync(userId, id);
    }

    public async Task<UserPrimordialImprints> GetSumUserPrimordialImprintsAsync(string userId)
    {
        return await _userPrimordialImprintsRepository.GetSumUserPrimordialImprintsAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPrimordialImprintsAsync(string userId, UserPrimordialImprints PrimordialImprints, string type, List<RecipeItemDetailDTO> totalDeductions)
    {
        var insertOrUpdateResult = await _userPrimordialImprintsRepository.InsertOrUpdateUserPrimordialImprintsAsync(userId, PrimordialImprints, type, totalDeductions);

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