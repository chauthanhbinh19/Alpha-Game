using System.Collections.Generic;

public class BooksService : IBooksService
{
    private readonly IBooksRepository _booksRepository;

    public BooksService(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    public static BooksService Create()
    {
        return new BooksService(new BooksRepository());
    }

    public List<string> GetUniqueBookTypes()
    {
        return _booksRepository.GetUniqueBookTypes();
    }

    public List<Books> GetBooks(string type, int pageSize, int offset)
    {
        List<Books> list = _booksRepository.GetBooks(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetBooksCount(string type)
    {
        return _booksRepository.GetBooksCount(type);
    }

    public List<Books> GetBooksRandom(string type, int pageSize)
    {
        return _booksRepository.GetBooksRandom(type, pageSize);
    }

    public List<Books> GetAllBooks(string type)
    {
        return _booksRepository.GetAllBooks(type);
    }

    public Books GetBooksById(string Id)
    {
        return _booksRepository.GetBooksById(Id);
    }

    public List<Books> GetBooksWithPrice(string type, int pageSize, int offset)
    {
        List<Books> list = _booksRepository.GetBooksWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetBookssWithPriceCount(string type)
    {
        return _booksRepository.GetBookssWithPriceCount(type);
    }

}