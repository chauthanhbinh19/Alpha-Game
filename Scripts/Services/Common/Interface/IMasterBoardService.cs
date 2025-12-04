using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMasterBoardService
{
    Task<List<string>> GetUniqueNameAsync();
}
