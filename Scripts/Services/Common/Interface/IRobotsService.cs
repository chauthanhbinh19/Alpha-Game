using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRobotsService
{
    Task<List<string>> GetUniqueRobotsIdAsync();
    Task<List<Robots>> GetRobotsAsync(string search, string rare, int pageSize, int offset);
    Task<int> GetRobotsCountAsync(string search, string rare);
    Task<List<Robots>> GetRobotsWithPriceAsync(int pageSize, int offset);
    Task<int> GetRobotsWithPriceCountAsync();
    Task<Robots> GetRobotByIdAsync(string Id);
    Task<Robots> SumPowerRobotsPercentAsync();
}
