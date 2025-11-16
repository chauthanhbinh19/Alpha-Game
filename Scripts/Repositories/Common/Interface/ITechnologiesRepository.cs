using System.Collections.Generic;

public interface ITechnologiesRepository
{
    List<string> GetUniqueTechnologyId();
    List<Technologies> GetTechnologies(int pageSize, int offset, string rare);
    int GetTechnologiesCount(string rare);
    List<Technologies> GetTechnologiesWithPrice(int pageSize, int offset);
    int GetTechnologiesWithPriceCount();
    Technologies GetTechnologiesById(string Id);
    Technologies SumPowerTechnologiesPercent();
}
