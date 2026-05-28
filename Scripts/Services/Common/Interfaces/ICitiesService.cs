using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICitiesService
{
    Task<List<Cities>> GetCitiesAsync(string userId, int pageSize, int offset);
    Task<int> GetCitiesCountAsync(string rare);
    Task<List<Cities>> GetCitiesWithPriceAsync(int pageSize, int offset);
    Task<int> GetCitiesWithPriceCountAsync();
    Task<Cities> GetCityByIdAsync(string Id);
    Task<Cities> SumPowerCitiesPercentAsync();
}
