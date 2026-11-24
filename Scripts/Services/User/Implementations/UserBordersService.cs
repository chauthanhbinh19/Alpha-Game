using System.Collections.Generic;
using System.Threading.Tasks;

public class UserBordersService : IUserBordersService
{
    private readonly IUserBordersRepository _userBordersRepository;
    private readonly IBordersGalleryService _bordersGalleryService;
    private readonly IBordersService _bordersService;

    public UserBordersService(
        IUserBordersRepository userBordersRepository,
        IBordersGalleryService bordersGalleryService,
        IBordersService bordersService)
    {
        _userBordersRepository = userBordersRepository;
        _bordersGalleryService = bordersGalleryService;
        _bordersService = bordersService;
    }

    public static IUserBordersService Create() => ServiceContainer.GetService<IUserBordersService>();

    public async Task<List<Borders>> GetUserBordersAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Borders> result = await _userBordersRepository.GetUserBordersAsync(userId, search, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserBordersCountAsync(string userId, string search, string rare)
    {
        return await _userBordersRepository.GetUserBordersCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertUserBorderByIdAsync(string borderId, string userId)
    {
        // IBordersRepository _repository = new BordersRepository();
        // BordersService _service = new BordersService(_repository);
        // return await _userBordersRepository.InsertUserBorderByIdAsync(await _service.GetBorderByIdAsync(borderId), userId);
        var checkBorderResult = await _bordersService.IsBorderDeletedOrInactiveAsync(borderId);
        if (checkBorderResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var border = await _bordersService.GetBorderByIdAsync(borderId);

        var insertOrUpdateResult = await _userBordersRepository.InsertOrUpdateUserBorderAsync(userId, border);

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
                IsChangePower = false,
                Message = MessageConstants.UPDATED_SUCCESSFULLY
            };
        }

        await _bordersGalleryService.InsertBorderGalleryAsync(userId, border.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBorderAsync(string userId, Borders border)
    {
        var checkBorderResult = await _bordersService.IsBorderDeletedOrInactiveAsync(border.Id);
        if (checkBorderResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userBordersRepository.InsertOrUpdateUserBorderAsync(userId, border);

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
                IsChangePower = false,
                Message = MessageConstants.UPDATED_SUCCESSFULLY
            };
        }

        await _bordersGalleryService.InsertBorderGalleryAsync(userId, border.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBordersBatchAsync(string userId, List<Borders> borders)
    {
        var insertOrUpdateResult = await _userBordersRepository.InsertOrUpdateUserBordersBatchAsync(userId, borders);

        if (insertOrUpdateResult?.Data == null || !insertOrUpdateResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        var newlyInsertedCards = insertOrUpdateResult.Data.InsertedItems;
        bool hasNewInserts = newlyInsertedCards != null && newlyInsertedCards.Count > 0;

        if (hasNewInserts)
        {
            await _bordersGalleryService.InsertBatchBordersGalleryAsync(userId, newlyInsertedCards);
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
                IsChangePower = false,
                Message = insertOrUpdateResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserBorderLevelAsync(string userId, Borders border, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (border == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Border is required.");
        }

        var checkResult = await _bordersService.IsBorderDeletedOrInactiveAsync(border.Id);
        if (checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userBordersRepository.UpdateUserBorderLevelAsync(
            userId,
            border,
            totalDeductions);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserBorderStarAsync(string userId, Borders border)
    {
        var checkBorderResult = await _bordersService.IsBorderDeletedOrInactiveAsync(border.Id);
        if (checkBorderResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userBordersRepository.UpdateUserBorderStarAsync(userId, border);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        await _bordersGalleryService.UpdateTempStarBorderGalleryAsync(userId, border.Id, border.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserBorderQuantityAsync(string userId, Borders border, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userBordersRepository.UpdateUserBorderQuantityAsync(userId, border, totalDeductions);

        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserBorderAffinityAsync(string userId, Borders border, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (border == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Border is required.");
        }

        var checkResult = await _bordersService.IsBorderDeletedOrInactiveAsync(border.Id);
        if (checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userBordersRepository.UpdateUserBorderAffinityAsync(
            userId,
            border,
            totalDeductions);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<Borders> GetUserBorderByUsedAsync(string userId)
    {
        var result = await _userBordersRepository.GetUserBorderByUsedAsync(userId);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task UpdateIsUsedUserBorderAsync(string borderId, string userId, bool is_used)
    {
        await _userBordersRepository.UpdateIsUsedUserBorderAsync(borderId, userId, is_used);
    }
    public async Task<Borders> GetUserBorderByIdAsync(string userId, string Id)
    {
        var result = await _userBordersRepository.GetUserBorderByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<Borders> SumPowerUserBordersAsync(string userId)
    {
        return await _userBordersRepository.SumPowerUserBordersAsync(userId);
    }
}
