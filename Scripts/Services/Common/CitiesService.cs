using System.Collections.Generic;

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

    public List<Cities> GetCities(string userId, int pageSize, int offset)
    {
        List<Cities> list = _CitiesRepository.GetCities(userId, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCitiesCount(string rare)
    {
        return _CitiesRepository.GetCitiesCount(rare);
    }

    public List<Cities> GetCitiesWithPrice(int pageSize, int offset)
    {
        List<Cities> list = _CitiesRepository.GetCitiesWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCitiesWithPriceCount()
    {
        return _CitiesRepository.GetCitiesWithPriceCount();
    }

    public Cities GetCitiesById(string Id)
    {
        return _CitiesRepository.GetCitiesById(Id);
    }

    public Cities SumPowerCitiesPercent()
    {
        return _CitiesRepository.SumPowerCitiesPercent();
    }

    public List<string> GetUniqueCitieId()
    {
        return _CitiesRepository.GetUniqueCitieId();
    }
}
