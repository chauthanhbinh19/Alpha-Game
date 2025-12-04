using System.Collections.Generic;
using System.Threading.Tasks;

public class BooksGalleryService : IBooksGalleryService
{
    private readonly IBooksGalleryRepository _booksGalleryRepository;

    public BooksGalleryService(IBooksGalleryRepository booksGalleryRepository)
    {
        _booksGalleryRepository = booksGalleryRepository;
    }

    public static BooksGalleryService Create()
    {
        return new BooksGalleryService(new BooksGalleryRepository());
    }

    public async Task<List<Books>> GetBooksCollectionAsync(string type, int pageSize, int offset, string rare)
    {
        List<Books> list = await _booksGalleryRepository.GetBooksCollectionAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBooksCountAsync(string type, string rare)
    {
        return await _booksGalleryRepository.GetBooksCountAsync(type, rare);
    }

    public async Task InsertBookGalleryAsync(string Id)
    {
        IBooksRepository _repository = new BooksRepository();
        BooksService _service = new BooksService(_repository);
        await _booksGalleryRepository.InsertBookGalleryAsync(Id, await _service.GetBookByIdAsync(Id));
    }

    public async Task UpdateStatusBookGalleryAsync(string Id)
    {
        await _booksGalleryRepository.UpdateStatusBookGalleryAsync(Id);
    }

    public async Task<Books> SumPowerBooksGalleryAsync()
    {
        return await _booksGalleryRepository.SumPowerBooksGalleryAsync();
    }

    public async Task UpdateStarBookGalleryAsync(string Id, double star)
    {
        await _booksGalleryRepository.UpdateStarBookGalleryAsync(Id, star);
    }

    public async Task UpdateBookGalleryPowerAsync(string Id)
    {
        IBooksRepository _repository = new BooksRepository();
        BooksService _service = new BooksService(_repository);
        await _booksGalleryRepository.UpdateBookGalleryPowerAsync(Id, await _service.GetBookByIdAsync(Id));
    }
}