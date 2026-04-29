using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITrainsRepository
{
    Task<List<string>> GetUniqueTrainsIdAsync();
    Task<List<Trains>> GetTrainsAsync(string userId, int pageSize, int offset);
    Task<int> GetTrainsCountAsync(string rare);
    Task<List<Trains>> GetTrainsWithPriceAsync(int pageSize, int offset);
    Task<int> GetTrainsWithPriceCountAsync();
    Task<Trains> GetTrainByIdAsync(string id);
    Task<Trains> SumPowerTrainsPercentAsync();
}
