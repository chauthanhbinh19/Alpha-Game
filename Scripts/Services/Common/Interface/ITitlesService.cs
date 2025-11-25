using System.Collections.Generic;

public interface ITitlesService
{
    List<string> GetUniqueTitleId();
    List<Titles> GetTitles(int pageSize, int offset, string rare);
    int GetTitlesCount(string rare);
    List<Titles> GetTitlesWithPrice(int pageSize, int offset);
    int GetTitlesWithPriceCount();
    Titles GetTitlesById(string Id);
    Titles SumPowerTitlesPercent();
}
