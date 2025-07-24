using System.Collections.Generic;

public interface IBooksRepository
{
    List<string> GetUniqueBookTypes();
    List<string> GetUniqueBookId();
    List<Books> GetBooks(string type, int pageSize, int offset, string rare);
    int GetBooksCount(string type, string rare);
    List<Books> GetBooksRandom(string type, int pageSize);
    List<Books> GetAllBooks(string type);
    Books GetBooksById(string Id);
    List<Books> GetBooksWithPrice(string type, int pageSize, int offset);
    int GetBookssWithPriceCount(string type);
}