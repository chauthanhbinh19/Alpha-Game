
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserArtworksService : IUserArtworksService
{
    private readonly IUserArtworksRepository _userArtworksRepository;
    private readonly IArtworksGalleryService _artworksGalleryService;
    private readonly IArtworksService _artworksService;
    private readonly IPowerManagerService _powerManagerService;

    public UserArtworksService(
        IUserArtworksRepository userArtworksRepository,
        IArtworksGalleryService artworksGalleryService,
        IArtworksService artworksService,
        IPowerManagerService powerManagerService)
    {
        _userArtworksRepository = userArtworksRepository;
        _artworksGalleryService = artworksGalleryService;
        _artworksService = artworksService;
        _powerManagerService = powerManagerService;
    }

    public static IUserArtworksService Create() => ServiceContainer.GetService<IUserArtworksService>();

    public async Task<List<Artworks>> GetUserArtworksAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Artworks> list = await _userArtworksRepository.GetUserArtworksAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserArtworksCountAsync(string userId, string search, string type, string rare)
    {
        return await _userArtworksRepository.GetUserArtworksCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArtworkAsync(string userId, Artworks artwork)
    {
        Artworks oldArtwork = await _artworksService.SumPowerArtworksPercentAsync(userId);
        var insertOrUpdateResult = await _userArtworksRepository.InsertOrUpdateUserArtworkAsync(userId, artwork);

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

        await _artworksGalleryService.InsertArtworkGalleryAsync(userId, artwork.Id);

        Artworks newArtwork = await _artworksService.SumPowerArtworksPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newArtwork - (PowerManager)oldArtwork;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArtworksBatchAsync(string userId, List<Artworks> artworkes)
    {
        Artworks oldArtwork = await _artworksService.SumPowerArtworksPercentAsync(userId);
        var repositoryResult = await _userArtworksRepository.InsertOrUpdateUserArtworksBatchAsync(userId, artworkes);

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
            await _artworksGalleryService.InsertBatchArtworksGalleryAsync(userId, newlyInsertedCards);
        }

        Artworks newArtwork = await _artworksService.SumPowerArtworksPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newArtwork - (PowerManager)oldArtwork;

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

    public async Task<bool> UpdateUserArtworkLevelAsync(string userId, Artworks artwork)
    {
        var updateResult = await _userArtworksRepository.UpdateUserArtworkLevelAsync(userId, artwork);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserArtworkStarAsync(string userId, Artworks artwork)
    {
        var updateResult = await _userArtworksRepository.UpdateUserArtworkStarAsync(userId, artwork);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _artworksGalleryService.UpdateTempStarArtworkGalleryAsync(userId, artwork.Id, artwork.Star);

        return true;
    }

    public async Task<Artworks> GetUserArtworkByIdAsync(string userId, string Id)
    {
        var result = await _userArtworksRepository.GetUserArtworkByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Artworks> SumPowerUserArtworksAsync(string userId)
    {
        return await _userArtworksRepository.SumPowerUserArtworksAsync(userId);
    }
}
