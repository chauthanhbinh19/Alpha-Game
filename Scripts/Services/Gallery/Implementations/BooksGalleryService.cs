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

    public async Task<bool> InsertBookGalleryAsync(string userId, string Id)
    {
        var insertResult = await _booksGalleryRepository.InsertBookGalleryAsync(userId, Id, await _booksService.GetBookByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusBookGalleryAsync(string userId, string bookId)
    {
        var updateResult = await _booksGalleryRepository.UpdateStatusBookGalleryAsync(userId, bookId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Books bookGallery = await GetBookCollectionByIdAsync(userId, bookId) ?? new Books();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)bookGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusBooksGalleryAsync(string userId)
    {
        Books oldBook = await SumPowerBooksGalleryAsync(userId);

        var updateResult = await _booksGalleryRepository.UpdateBatchStatusBooksGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Books newBook = await SumPowerBooksGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newBook - (PowerManager)oldBook;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Books> SumPowerBooksGalleryAsync(string userId)
    {
        return await _booksGalleryRepository.SumPowerBooksGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarBookGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _booksGalleryRepository.UpdateStarBookGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarBookGalleryAsync(string userId, string bookId)
    {
        Books oldBook = await GetBookCollectionByIdAsync(userId, bookId) ?? new Books();

        var updateResult = await _booksGalleryRepository.UpdateCurrentStarBookGalleryAsync(userId, bookId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Books newBook = await GetBookCollectionByIdAsync(userId, bookId) ?? new Books();
        PowerManager deltaPower = (PowerManager)newBook - (PowerManager)oldBook;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarBooksGalleryAsync(string userId)
    {
        Books oldBook = await SumPowerBooksGalleryAsync(userId);

        var updateResult = await _booksGalleryRepository.UpdateBatchCurrentStarBooksGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Books newBook = await SumPowerBooksGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newBook - (PowerManager)oldBook;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchBooksGalleryAsync(string userId, List<Books> books)
    {
        var insertResult = await _booksGalleryRepository.InsertBatchBooksGalleryAsync(userId, books);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Books> GetBookCollectionByIdAsync(string userId, string bookId)
    {
        var result = await _booksGalleryRepository.GetBookCollectionByIdAsync(userId, bookId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateBookGalleryPowerAsync(string userId, string Id)
    {
        IBooksRepository _repository = new BooksRepository();
        BooksService _service = new BooksService(_repository);
        await _booksGalleryRepository.UpdateBookGalleryPowerAsync(userId, Id, await _service.GetBookByIdAsync(Id));
    }
}