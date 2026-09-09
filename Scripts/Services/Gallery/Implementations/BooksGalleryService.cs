using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class BooksGalleryService : IBooksGalleryService
{
    private readonly IBooksGalleryRepository _booksGalleryRepository;
    private readonly IBooksService _booksService;
    private readonly IPowerManagerService _powerManagerService;

    public BooksGalleryService(
        IBooksGalleryRepository booksGalleryRepository,
        IBooksService booksService,
        IPowerManagerService powerManagerService)
    {
        _booksGalleryRepository = booksGalleryRepository;
        _booksService = booksService;
        _powerManagerService = powerManagerService;
    }

    public static IBooksGalleryService Create() => ServiceContainer.GetService<IBooksGalleryService>();

    public async Task<List<Books>> GetBooksCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Books> list = await _booksGalleryRepository.GetBooksCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBooksCountAsync(string search, string type, string rare)
    {
        return await _booksGalleryRepository.GetBooksCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertBookGalleryAsync(string userId, string Id)
    {
        var checkResult = await _booksService.IsBookDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _booksGalleryRepository.InsertBookGalleryAsync(userId, Id, await _booksService.GetBookByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusBookGalleryAsync(string userId, string bookId)
    {
        var checkResult = await _booksService.IsBookDeletedOrInactiveAsync(bookId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _booksGalleryRepository.UpdateStatusBookGalleryAsync(userId, bookId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Books bookGallery = await GetBookCollectionByIdAsync(userId, bookId) ?? new Books();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)bookGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusBooksGalleryAsync(string userId)
    {
        Books oldBook = await SumPowerBooksGalleryAsync(userId);

        var updateResult = await _booksGalleryRepository.UpdateBatchStatusBooksGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Books newBook = await SumPowerBooksGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newBook - (PowerManager)oldBook;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<Books> SumPowerBooksGalleryAsync(string userId)
    {
        return await _booksGalleryRepository.SumPowerBooksGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarBookGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _booksService.IsBookDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _booksGalleryRepository.UpdateTempStarBookGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarBookGalleryAsync(string userId, string bookId)
    {
        var checkResult = await _booksService.IsBookDeletedOrInactiveAsync(bookId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Books oldBook = await GetBookCollectionByIdAsync(userId, bookId) ?? new Books();

        var updateResult = await _booksGalleryRepository.UpdateCurrentStarBookGalleryAsync(userId, bookId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Books newBook = await GetBookCollectionByIdAsync(userId, bookId) ?? new Books();
        PowerManager deltaPower = (PowerManager)newBook - (PowerManager)oldBook;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarBooksGalleryAsync(string userId)
    {
        Books oldBook = await SumPowerBooksGalleryAsync(userId);

        var updateResult = await _booksGalleryRepository.UpdateBatchCurrentStarBooksGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Books newBook = await SumPowerBooksGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newBook - (PowerManager)oldBook;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertBatchBooksGalleryAsync(string userId, List<Books> books)
    {
        var insertResult = await _booksGalleryRepository.InsertBatchBooksGalleryAsync(userId, books);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<Books> GetBookCollectionByIdAsync(string userId, string bookId)
    {
        var result = await _booksGalleryRepository.GetBookCollectionByIdAsync(userId, bookId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBookGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _booksService.IsBookDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IBooksRepository _repository = new BooksRepository();
        BooksService _service = new BooksService(_repository);
        await _booksGalleryRepository.UpdateBookGalleryPowerAsync(userId, Id, await _service.GetBookByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}