using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRobotsRepository
{
    Task<List<string>> GetUniqueRobotsIdAsync();
    Task<List<Robots>> GetRobotsAsync(int pageSize, int offset, string rare);
    Task<int> GetRobotsCountAsync(string rare);
    Task<List<Robots>> GetRobotsWithPriceAsync(int pageSize, int offset);
    Task<int> GetRobotsWithPriceCountAsync();
    Task<Robots> GetRobotByIdAsync(string Id);
    Task<Robots> SumPowerRobotsPercentAsync();
}
