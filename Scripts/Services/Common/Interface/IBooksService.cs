using System.Collections.Generic;

public interface IBooksService
{
    List<string> GetUniqueBookTypes();
    List<Books> GetBooks(string type, int pageSize, int offset);
    int GetBooksCount(string type);
    List<Books> GetBooksRandom(string type, int pageSize);
    List<Books> GetAllBooks(string type);
    Books GetBooksById(string Id);
    List<Books> GetBooksWithPrice(string type, int pageSize, int offset);
    int GetBookssWithPriceCount(string type);
}