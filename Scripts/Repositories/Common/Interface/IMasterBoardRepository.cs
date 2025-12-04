using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMasterBoardRepository
{
    Task<List<string>> GetUniqueNameAsync();
}
