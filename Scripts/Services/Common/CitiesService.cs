using System.Collections.Generic;
using System.Threading.Tasks;

public class CitiesService : ICitiesService
{
    private static CitiesService _instance;
    private readonly ICitiesRepository _citiesRepository;

    public CitiesService(ICitiesRepository citiesRepository)
    {
        _citiesRepository = citiesRepository;
    }

    public static CitiesService Create()
    {
        if (_instance == null)
        {
            _instance = new CitiesService(new CitiesRepository());
        }
        return _instance;
    }

    public async Task<List<Cities>> GetCitiesAsync(string userId, int pageSize, int offset)
    {
        List<Cities> list = await _citiesRepository.GetCitiesAsync(userId, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCitiesCountAsync(string rare)
    {
        return await _citiesRepository.GetCitiesCountAsync(rare);
    }

    public async Task<List<Cities>> GetCitiesWithPriceAsync(int pageSize, int offset)
    {
        List<Cities> list = await _citiesRepository.GetCitiesWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCitiesWithPriceCountAsync()
    {
        return await _citiesRepository.GetCitiesWithPriceCountAsync();
    }

    public async Task<Cities> GetCityByIdAsync(string Id)
    {
        return await _citiesRepository.GetCityByIdAsync(Id);
    }

    public async Task<Cities> SumPowerCitiesPercentAsync()
    {
        return await _citiesRepository.SumPowerCitiesPercentAsync();
    }

    public async Task<List<string>> GetUniqueCitieIdAsync()
    {
        return await _citiesRepository.GetUniqueCitiesIdAsync();
    }
}
