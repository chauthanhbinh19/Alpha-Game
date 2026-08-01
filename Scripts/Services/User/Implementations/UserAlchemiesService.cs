
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserAlchemiesService : IUserAlchemiesService
{
    private readonly IUserAlchemiesRepository _userAlchemiesRepository;
    private readonly IAlchemiesGalleryService _alchemiesGalleryService;
    private readonly IAlchemiesService _alchemiesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserAlchemiesService(
        IUserAlchemiesRepository userAlchemiesRepository,
        IAlchemiesGalleryService alchemiesGalleryService,
        IAlchemiesService alchemiesService,
        IPowerManagerService powerManagerService)
    {
        _userAlchemiesRepository = userAlchemiesRepository;
        _alchemiesGalleryService = alchemiesGalleryService;
        _alchemiesService = alchemiesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserAlchemiesService Create() => ServiceContainer.GetService<IUserAlchemiesService>();

    public async Task<List<Alchemies>> GetUserAlchemiesAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Alchemies> list = await _userAlchemiesRepository.GetUserAlchemiesAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserAlchemiesCountAsync(string userId, string search, string type, string rare)
    {
        return await _userAlchemiesRepository.GetUserAlchemiesCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAlchemyAsync(string userId, Alchemies alchemy)
    {
        var oldAlchemyTask = _alchemiesService.SumPowerAlchemiesPercentAsync(userId);
        var oldUserAlchemyTask = _userAlchemiesRepository.SumPowerUserAlchemiesAsync(userId);

        await Task.WhenAll(oldAlchemyTask, oldUserAlchemyTask);

        Alchemies oldAlchemy = oldAlchemyTask.Result;
        Alchemies oldUserAlchemy = oldUserAlchemyTask.Result;

        var insertOrUpdateResult = await _userAlchemiesRepository.InsertOrUpdateUserAlchemyAsync(userId, alchemy);

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

        await _alchemiesGalleryService.InsertAlchemyGalleryAsync(userId, alchemy.Id);

        var newAlchemyTask = _alchemiesService.SumPowerAlchemiesPercentAsync(userId);
        var newUserAlchemyTask = _userAlchemiesRepository.SumPowerUserAlchemiesAsync(userId);

        await Task.WhenAll(newAlchemyTask, newUserAlchemyTask);

        PowerManager deltaPower = (PowerManager)newAlchemyTask.Result - (PowerManager)oldAlchemy;
        PowerManager deltaUserPower = (PowerManager)newUserAlchemyTask.Result - (PowerManager)oldUserAlchemy;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAlchemiesBatchAsync(string userId, List<Alchemies> alchemies)
    {
        var oldAlchemiesTask = _alchemiesService.SumPowerAlchemiesPercentAsync(userId);
        var oldUserAlchemiesTask = _userAlchemiesRepository.SumPowerUserAlchemiesAsync(userId);

        await Task.WhenAll(oldAlchemiesTask, oldUserAlchemiesTask);

        Alchemies oldAlchemy = oldAlchemiesTask.Result;
        Alchemies oldUserAlchemy = oldUserAlchemiesTask.Result;

        var insertOrUpdateResult = await _userAlchemiesRepository.InsertOrUpdateUserAlchemiesBatchAsync(userId, alchemies);

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
            await _alchemiesGalleryService.InsertBatchAlchemiesGalleryAsync(userId, newlyInsertedCards);

            var newAlchemyTask = _alchemiesService.SumPowerAlchemiesPercentAsync(userId);
            var newUserAlchemyTask = _userAlchemiesRepository.SumPowerUserAlchemiesAsync(userId);

            await Task.WhenAll(newAlchemyTask, newUserAlchemyTask);

            PowerManager deltaPower = (PowerManager)newAlchemyTask.Result - (PowerManager)oldAlchemy;
            PowerManager deltaUserPower = (PowerManager)newUserAlchemyTask.Result - (PowerManager)oldUserAlchemy;

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

    public async Task<bool> UpdateUserAlchemyLevelAsync(string userId, Alchemies alchemy)
    {
        Alchemies oldUserAlchemy = await _userAlchemiesRepository.SumPowerUserAlchemiesAsync(userId);

        var updateResult = await _userAlchemiesRepository.UpdateUserAlchemyLevelAsync(userId, alchemy);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Alchemies newUserAlchemy = await _userAlchemiesRepository.SumPowerUserAlchemiesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserAlchemy - (PowerManager)oldUserAlchemy;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserAlchemyStarAsync(string userId, Alchemies alchemy)
    {
        Alchemies oldUserAlchemy = await _userAlchemiesRepository.SumPowerUserAlchemiesAsync(userId);

        var updateResult = await _userAlchemiesRepository.UpdateUserAlchemyStarAsync(userId, alchemy);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _alchemiesGalleryService.UpdateTempStarAlchemyGalleryAsync(userId, alchemy.Id, alchemy.Star);

        Alchemies newUserAlchemy = await _userAlchemiesRepository.SumPowerUserAlchemiesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserAlchemy - (PowerManager)oldUserAlchemy;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Alchemies> GetUserAlchemyByIdAsync(string userId, string Id)
    {
        var result = await _userAlchemiesRepository.GetUserAlchemyByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Alchemies> SumPowerUserAlchemiesAsync(string userId)
    {
        return await _userAlchemiesRepository.SumPowerUserAlchemiesAsync(userId);
    }
}
