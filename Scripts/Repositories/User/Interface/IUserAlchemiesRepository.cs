using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserAlchemiesRepository
{
    Task<List<Alchemies>> GetUserAlchemiesAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserAlchemiesCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserAlchemyAsync(Alchemies Alchemy, string userId);
    Task<bool> UpdateAlchemyLevelAsync(Alchemies Alchemy, int cardLevel);
    Task<bool> UpdateAlchemyBreakthroughAsync(Alchemies Alchemy, int star, double quantity);
    Task<Alchemies> GetUserAlchemyByIdAsync(string user_id, string Id);
    Task<Alchemies> SumPowerUserAlchemiesAsync();
}
