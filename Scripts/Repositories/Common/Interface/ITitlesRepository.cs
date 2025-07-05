using System.Collections.Generic;

public interface ITitlesRepository
{
    List<string> GetUniqueTitleId();
    List<Titles> GetTitles(int pageSize, int offset);
    int GetTitlesCount();
    List<Titles> GetTitlesWithPrice(int pageSize, int offset);
    int GetTitlesWithPriceCount();
    Titles GetTitlesById(string Id);
    Titles SumPowerTitlesPercent();
}
