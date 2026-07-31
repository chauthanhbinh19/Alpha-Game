using System.Collections.Generic;
using System.Threading.Tasks;

public class UserBordersService : IUserBordersService
{
    private readonly IUserBordersRepository _userBordersRepository;
    private readonly IBordersGalleryService _bordersGalleryService;
    private readonly IBordersService _bordersService;
    private readonly IPowerManagerService _powerManagerService;

    public UserBordersService(
        IUserBordersRepository userBordersRepository,
        IBordersGalleryService bordersGalleryService,
        IBordersService bordersService,
        IPowerManagerService powerManagerService)
    {
        _userBordersRepository = userBordersRepository;
        _bordersGalleryService = bordersGalleryService;
        _bordersService = bordersService;
        _powerManagerService = powerManagerService;
    }

    public static IUserBordersService Create() => ServiceContainer.GetService<IUserBordersService>();

    public async Task<List<Borders>> GetUserBordersAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Borders> list = await _userBordersRepository.GetUserBordersAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserBordersCountAsync(string userId, string search, string rare)
    {
        return await _userBordersRepository.GetUserBordersCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserBorderByIdAsync(string borderId, string userId)
    {
        IBordersRepository _repository = new BordersRepository();
        BordersService _service = new BordersService(_repository);
        return await _userBordersRepository.InsertUserBorderByIdAsync(await _service.GetBorderByIdAsync(borderId), userId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBorderAsync(string userId, Borders border)
    {
        Borders oldBorder = await _bordersService.SumPowerBordersPercentAsync(userId);
        var insertOrUpdateResult = await _userBordersRepository.InsertOrUpdateUserBorderAsync(userId, border);

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

        await _bordersGalleryService.InsertBorderGalleryAsync(userId, border.Id);

        Borders newBorder = await _bordersService.SumPowerBordersPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newBorder - (PowerManager)oldBorder;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBordersBatchAsync(string userId, List<Borders> borderes)
    {
        Borders oldBorder = await _bordersService.SumPowerBordersPercentAsync(userId);
        var repositoryResult = await _userBordersRepository.InsertOrUpdateUserBordersBatchAsync(userId, borderes);

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
            await _bordersGalleryService.InsertBatchBordersGalleryAsync(userId, newlyInsertedCards);
        }

        Borders newBorder = await _bordersService.SumPowerBordersPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newBorder - (PowerManager)oldBorder;

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

    public async Task<bool> UpdateUserBorderLevelAsync(string userId, Borders border)
    {
        var updateResult = await _userBordersRepository.UpdateUserBorderLevelAsync(userId, border);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserBorderStarAsync(string userId, Borders border)
    {
        var updateResult = await _userBordersRepository.UpdateUserBorderStarAsync(userId, border);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _bordersGalleryService.UpdateTempStarBorderGalleryAsync(userId, border.Id, border.Star);

        return true;
    }

    public async Task<Borders> GetUserBorderByUsedAsync(string userId)
    {
        return await _userBordersRepository.GetUserBorderByUsedAsync(userId);
    }

    public async Task UpdateIsUsedUserBorderAsync(string borderId, string userId, bool is_used)
    {
        await _userBordersRepository.UpdateIsUsedUserBorderAsync(borderId, userId, is_used);
    }

    public async Task<Borders> SumPowerUserBordersAsync(string userId)
    {
        return await _userBordersRepository.SumPowerUserBordersAsync(userId);
    }
}
