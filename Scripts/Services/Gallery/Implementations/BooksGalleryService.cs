using System.Collections.Generic;
using System.Threading.Tasks;

public class BooksGalleryService : IBooksGalleryService
{
    private static BooksGalleryService _instance;
    private readonly IBooksGalleryRepository _booksGalleryRepository;

    public BooksGalleryService(IBooksGalleryRepository booksGalleryRepository)
    {
        _booksGalleryRepository = booksGalleryRepository;
    }

    public static BooksGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new BooksGalleryService(new BooksGalleryRepository());
        }
        return _instance;
    }

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

    public async Task InsertBookGalleryAsync(string userId, string Id)
    {
        IBooksRepository _repository = new BooksRepository();
        BooksService _service = new BooksService(_repository);
        await _booksGalleryRepository.InsertBookGalleryAsync(userId, Id, await _service.GetBookByIdAsync(Id));
    }

    public async Task UpdateStatusBookGalleryAsync(string userId, string Id)
    {
        await _booksGalleryRepository.UpdateStatusBookGalleryAsync(userId, Id);
    }

    public async Task<Books> SumPowerBooksGalleryAsync(string userId)
    {
        return await _booksGalleryRepository.SumPowerBooksGalleryAsync(userId);
    }

    public async Task UpdateStarBookGalleryAsync(string userId, string Id, double star)
    {
        await _booksGalleryRepository.UpdateStarBookGalleryAsync(userId, Id, star);
    }

    public async Task UpdateBookGalleryPowerAsync(string userId, string Id)
    {
        IBooksRepository _repository = new BooksRepository();
        BooksService _service = new BooksService(_repository);
        await _booksGalleryRepository.UpdateBookGalleryPowerAsync(userId, Id, await _service.GetBookByIdAsync(Id));
    }
}