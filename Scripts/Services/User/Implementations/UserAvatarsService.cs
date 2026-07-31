using System.Collections.Generic;
using System.Threading.Tasks;

public class UserAvatarsService : IUserAvatarsService
{
    private static UserAvatarsService _instance;
    private readonly IUserAvatarsRepository _userAvatarsRepository;

    public UserAvatarsService(IUserAvatarsRepository userAvatarsRepository)
    {
        _userAvatarsRepository = userAvatarsRepository;
    }

    public static UserAvatarsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserAvatarsService(new UserAvatarsRepository());
        }
        return _instance;
    }

    public async Task<List<Avatars>> GetUserAvatarsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Avatars> list = await _userAvatarsRepository.GetUserAvatarsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserAvatarsCountAsync(string userId, string search, string rare)
    {
        return await _userAvatarsRepository.GetUserAvatarsCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserAvatarByIdAsync(string avatarId, string userId)
    {
        IAvatarsRepository _repository = new AvatarsRepository();
        AvatarsService _service = new AvatarsService(_repository);
        return await _userAvatarsRepository.InsertUserAvatarByIdAsync(await _service.GetAvatarByIdAsync(avatarId), userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardLifeAsync(string userId, CardLives cardLife)
    {
        CardLives oldCardLife = await _cardLivesService.SumPowerCardLivesPercentAsync(userId);
        var insertOrUpdateResult = await _userCardLivesRepository.InsertOrUpdateUserCardLifeAsync(userId, cardLife);

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

        await _cardLivesGalleryService.InsertCardLifeGalleryAsync(userId, cardLife.Id);

        CardLives newCardLife = await _cardLivesService.SumPowerCardLivesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardLife - (PowerManager)oldCardLife;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardLivesBatchAsync(string userId, List<CardLives> cardLifees)
    {
        CardLives oldCardLife = await _cardLivesService.SumPowerCardLivesPercentAsync(userId);
        var repositoryResult = await _userCardLivesRepository.InsertOrUpdateUserCardLivesBatchAsync(userId, cardLifees);

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
            await _cardLivesGalleryService.InsertBatchCardLivesGalleryAsync(userId, newlyInsertedCards);
        }

        CardLives newCardLife = await _cardLivesService.SumPowerCardLivesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newCardLife - (PowerManager)oldCardLife;

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

    public async Task<bool> UpdateUserCardLifeLevelAsync(string userId, CardLives cardLife)
    {
        var updateResult = await _userCardLivesRepository.UpdateUserCardLifeLevelAsync(userId, cardLife);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserCardLifeStarAsync(string userId, CardLives cardLife)
    {
        var updateResult = await _userCardLivesRepository.UpdateUserCardLifeStarAsync(userId, cardLife);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _cardLivesGalleryService.UpdateTempStarCardLifeGalleryAsync(userId, cardLife.Id, cardLife.Star);

        return true;
    }

    public async Task<Avatars> GetUserAvatarByUsedAsync(string userId)
    {
        return await _userAvatarsRepository.GetUserAvatarByUsedAsync(userId);
    }

    public async Task UpdateIsUsedUserAvatarAsync(string avatarId, string userId, bool is_used)
    {
        await _userAvatarsRepository.UpdateIsUsedUserAvatarAsync(avatarId, userId, is_used);
    }

    public async Task<Avatars> SumPowerUserAvatarsAsync(string userId)
    {
        return await _userAvatarsRepository.SumPowerUserAvatarsAsync(userId);
    }
}