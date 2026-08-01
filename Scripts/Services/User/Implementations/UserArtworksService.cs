
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
        var oldArtworkTask = _artworksService.SumPowerArtworksPercentAsync(userId);
        var oldUserArtworkTask = _userArtworksRepository.SumPowerUserArtworksAsync(userId);

        await Task.WhenAll(oldArtworkTask, oldUserArtworkTask);

        Artworks oldArtwork = oldArtworkTask.Result;
        Artworks oldUserArtwork = oldUserArtworkTask.Result;

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

        var newArtworkTask = _artworksService.SumPowerArtworksPercentAsync(userId);
        var newUserArtworkTask = _userArtworksRepository.SumPowerUserArtworksAsync(userId);

        await Task.WhenAll(newArtworkTask, newUserArtworkTask);

        PowerManager deltaPower = (PowerManager)newArtworkTask.Result - (PowerManager)oldArtwork;
        PowerManager deltaUserPower = (PowerManager)newUserArtworkTask.Result - (PowerManager)oldUserArtwork;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArtworksBatchAsync(string userId, List<Artworks> artworks)
    {
        var oldArtworkTask = _artworksService.SumPowerArtworksPercentAsync(userId);
        var oldUserArtworkTask = _userArtworksRepository.SumPowerUserArtworksAsync(userId);

        await Task.WhenAll(oldArtworkTask, oldUserArtworkTask);

        Artworks oldArtwork = oldArtworkTask.Result;
        Artworks oldUserArtwork = oldUserArtworkTask.Result;

        var insertOrUpdateResult = await _userArtworksRepository.InsertOrUpdateUserArtworksBatchAsync(userId, artworks);

        if (insertOrUpdateResult?.Data == null || !insertOrUpdateResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        var newlyInsertedCards = insertOrUpdateResult.Data.InsertedItems;
        bool hasNewInserts = newlyInsertedCards != null && newlyInsertedCards.Count > 0;

        if (hasNewInserts)
        {
            await _artworksGalleryService.InsertBatchArtworksGalleryAsync(userId, newlyInsertedCards);

            var newArtworkTask = _artworksService.SumPowerArtworksPercentAsync(userId);
            var newUserArtworkTask = _userArtworksRepository.SumPowerUserArtworksAsync(userId);

            await Task.WhenAll(newArtworkTask, newUserArtworkTask);

            PowerManager deltaPower = (PowerManager)newArtworkTask.Result - (PowerManager)oldArtwork;
            PowerManager deltaUserPower = (PowerManager)newUserArtworkTask.Result - (PowerManager)oldUserArtwork;

            PowerManager totalDelta = new PowerManager();
            if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
            if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

            if (totalDelta.HasAnyPositiveStat())
            {
                PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
                PowerManager updatedPower = currentPower + totalDelta;
                await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
            }
        }

        return insertOrUpdateResult.OperationType switch
        {
            DatabaseOperationType.Mixed => InsertOrUpdateResult<bool>.Mixed(true),
            DatabaseOperationType.Inserted => InsertOrUpdateResult<bool>.Inserted(true),
            DatabaseOperationType.Updated => InsertOrUpdateResult<bool>.Updated(true),
            _ => new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<bool> UpdateUserArtworkLevelAsync(string userId, Artworks artwork)
    {
        Artworks oldUserArtwork = await _userArtworksRepository.SumPowerUserArtworksAsync(userId);

        var updateResult = await _userArtworksRepository.UpdateUserArtworkLevelAsync(userId, artwork);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Artworks newUserArtwork = await _userArtworksRepository.SumPowerUserArtworksAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserArtwork - (PowerManager)oldUserArtwork;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserArtworkStarAsync(string userId, Artworks artwork)
    {
        Artworks oldUserArtwork = await _userArtworksRepository.SumPowerUserArtworksAsync(userId);

        var updateResult = await _userArtworksRepository.UpdateUserArtworkStarAsync(userId, artwork);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _artworksGalleryService.UpdateTempStarArtworkGalleryAsync(userId, artwork.Id, artwork.Star);

        Artworks newUserArtwork = await _userArtworksRepository.SumPowerUserArtworksAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserArtwork - (PowerManager)oldUserArtwork;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

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
