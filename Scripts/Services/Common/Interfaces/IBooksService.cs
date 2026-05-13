using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBooksService
{
    Task<List<string>> GetUniqueBooksTypesAsync();
    Task<List<string>> GetUniqueBooksIdAsync();
    Task<List<Books>> GetBooksAsync(string search, string type, string rare, int pageSize, int offset);
    Task<List<Books>> GetBooksWithoutLimitAsync();
    Task<int> GetBooksCountAsync(string search, string type, string rare);
    Task<List<Books>> GetBooksRandomAsync(string type, int pageSize);
    Task<List<Books>> GetAllBooksAsync(string type);
    Task<Books> GetBookByIdAsync(string Id);
    Task<List<Books>> GetBooksWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetBooksWithPriceCountAsync(string type);
}