using System.Collections.Generic;
using System.Threading.Tasks;

public class CitiesService : ICitiesService
{
    private readonly ICitiesRepository _CitiesRepository;

    public CitiesService(ICitiesRepository titleRepository)
    {
        _CitiesRepository = titleRepository;
    }

    public static CitiesService Create()
    {
        return new CitiesService(new CitiesRepository());
    }

    public async Task<List<Cities>> GetCitiesAsync(string userId, int pageSize, int offset)
    {
        List<Cities> list = await _CitiesRepository.GetCitiesAsync(userId, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCitiesCountAsync(string rare)
    {
        return await _CitiesRepository.GetCitiesCountAsync(rare);
    }

    public async Task<List<Cities>> GetCitiesWithPriceAsync(int pageSize, int offset)
    {
        List<Cities> list = await _CitiesRepository.GetCitiesWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCitiesWithPriceCountAsync()
    {
        return await _CitiesRepository.GetCitiesWithPriceCountAsync();
    }

    public async Task<Cities> GetCityByIdAsync(string Id)
    {
        return await _CitiesRepository.GetCityByIdAsync(Id);
    }

    public async Task<Cities> SumPowerCitiesPercentAsync()
    {
        return await _CitiesRepository.SumPowerCitiesPercentAsync();
    }

    public async Task<List<string>> GetUniqueCitieIdAsync()
    {
        return await _CitiesRepository.GetUniqueCitiesIdAsync();
    }
}
