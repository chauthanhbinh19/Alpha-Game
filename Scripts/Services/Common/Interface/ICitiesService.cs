using System.Collections.Generic;

public interface ICitiesService
{
    List<string> GetUniqueCitieId();
    List<Cities> GetCities(string userId, int pageSize, int offset);
    int GetCitiesCount(string rare);
    List<Cities> GetCitiesWithPrice(int pageSize, int offset);
    int GetCitiesWithPriceCount();
    Cities GetCitiesById(string Id);
    Cities SumPowerCitiesPercent();
}
