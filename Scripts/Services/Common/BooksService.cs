using System.Collections.Generic;
using System.Threading.Tasks;

public class BooksService : IBooksService
{
    private static BooksService _instance;
    private readonly IBooksRepository _booksRepository;

    public BooksService(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    public static BooksService Create()
    {
        if (_instance == null)
        {
            _instance = new BooksService(new BooksRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueBooksTypesAsync()
    {
        return await _booksRepository.GetUniqueBooksTypesAsync();
    }

    public async Task<List<Books>> GetBooksAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Books> list = await _booksRepository.GetBooksAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBooksCountAsync(string search, string type, string rare)
    {
        return await _booksRepository.GetBooksCountAsync(search, type, rare);
    }

    public async Task<List<Books>> GetBooksRandomAsync(string type, int pageSize)
    {
        return await _booksRepository.GetBooksRandomAsync(type, pageSize);
    }

    public async Task<List<Books>> GetAllBooksAsync(string type)
    {
        return await _booksRepository.GetAllBooksAsync(type);
    }

    public async Task<Books> GetBookByIdAsync(string Id)
    {
        return await _booksRepository.GetBookByIdAsync(Id);
    }

    public async Task<List<Books>> GetBooksWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Books> list = await _booksRepository.GetBooksWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBooksWithPriceCountAsync(string type)
    {
        return await _booksRepository.GetBooksWithPriceCountAsync(type);
    }

    public async Task<List<string>> GetUniqueBooksIdAsync()
    {
        return await _booksRepository.GetUniqueBooksIdAsync();
    }
}