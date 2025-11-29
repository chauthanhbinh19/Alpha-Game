using System.Collections.Generic;

public interface IResearchsRepository
{
    List<string> GetUniqueResearchId();
    List<Researchs> GetResearchs(string userId, int pageSize, int offset);
    int GetResearchsCount(string rare);
    List<Researchs> GetResearchsWithPrice(int pageSize, int offset);
    int GetResearchsWithPriceCount();
    Researchs GetResearchsById(string Id);
    Researchs SumPowerResearchsPercent();
}
