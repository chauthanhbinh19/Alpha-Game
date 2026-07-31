using System.Collections.Generic;
using System.Threading.Tasks;

public class UserArtifactsService : IUserArtifactsService
{
    private readonly IUserArtifactsRepository _userArtifactsRepository;
    private readonly IArtifactsGalleryService _artifactsGalleryService;
    private readonly IArtifactsService _artifactsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserArtifactsService(
        IUserArtifactsRepository userArtifactsRepository,
        IArtifactsGalleryService artifactsGalleryService,
        IArtifactsService artifactsService,
        IPowerManagerService powerManagerService)
    {
        _userArtifactsRepository = userArtifactsRepository;
        _artifactsGalleryService = artifactsGalleryService;
        _artifactsService = artifactsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserArtifactsService Create() => ServiceContainer.GetService<IUserArtifactsService>();

    public async Task<List<Artifacts>> GetUserArtifactsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Artifacts> list = await _userArtifactsRepository.GetUserArtifactsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserArtifactsCountAsync(string userId, string search, string rare)
    {
        return await _userArtifactsRepository.GetUserArtifactsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArtifactAsync(string userId, Artifacts artifact)
    {
        Artifacts oldArtifact = await _artifactsService.SumPowerArtifactsPercentAsync(userId);
        var insertOrUpdateResult = await _userArtifactsRepository.InsertOrUpdateUserArtifactAsync(userId, artifact);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _artifactsGalleryService.InsertArtifactGalleryAsync(userId, artifact.Id);

        Artifacts newArtifact = await _artifactsService.SumPowerArtifactsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newArtifact - (PowerManager)oldArtifact;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArtifactsBatchAsync(string userId, List<Artifacts> artifactes)
    {
        Artifacts oldArtifact = await _artifactsService.SumPowerArtifactsPercentAsync(userId);
        var repositoryResult = await _userArtifactsRepository.InsertOrUpdateUserArtifactsBatchAsync(userId, artifactes);

        // 1. Kiểm tra Null hoặc nếu Repository trả về không thành công
        if (repositoryResult?.Data == null || !repositoryResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = repositoryResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        // 2. Gộp logic xử lý Gallery nếu có thẻ mới được Insert (dùng cho cả Inserted và Mixed)
        var newlyInsertedCards = repositoryResult.Data.InsertedItems;
        if (newlyInsertedCards != null && newlyInsertedCards.Count > 0)
        {
            await _artifactsGalleryService.InsertBatchArtifactsGalleryAsync(userId, newlyInsertedCards);
        }

        Artifacts newArtifact = await _artifactsService.SumPowerArtifactsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newArtifact - (PowerManager)oldArtifact;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        // 3. Mapping kết quả OperationType trả về gọn gàng
        return repositoryResult.OperationType switch
        {
            DatabaseOperationType.Mixed => InsertOrUpdateResult<bool>.Mixed(true),
            DatabaseOperationType.Inserted => InsertOrUpdateResult<bool>.Inserted(true),
            DatabaseOperationType.Updated => InsertOrUpdateResult<bool>.Updated(true),
            _ => new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = repositoryResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<bool> UpdateUserArtifactLevelAsync(string userId, Artifacts artifact)
    {
        var updateResult = await _userArtifactsRepository.UpdateUserArtifactLevelAsync(userId, artifact);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserArtifactStarAsync(string userId, Artifacts artifact)
    {
        var updateResult = await _userArtifactsRepository.UpdateUserArtifactStarAsync(userId, artifact);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _artifactsGalleryService.UpdateTempStarArtifactGalleryAsync(userId, artifact.Id, artifact.Star);

        return true;
    }

    public async Task<Artifacts> GetUserArtifactByIdAsync(string userId, string Id)
    {
        var result = await _userArtifactsRepository.GetUserArtifactByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Artifacts> SumPowerUserArtifactsAsync(string userId)
    {
        return await _userArtifactsRepository.SumPowerUserArtifactsAsync(userId);
    }
}
