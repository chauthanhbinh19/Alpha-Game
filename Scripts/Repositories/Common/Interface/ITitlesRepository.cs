using System.Collections.Generic;

public interface ITitlesRepository
{
    List<string> GetUniqueTitleId();
    List<Architectures> GetTitles(int pageSize, int offset, string rare);
    int GetTitlesCount(string rare);
    List<Architectures> GetTitlesWithPrice(int pageSize, int offset);
    int GetTitlesWithPriceCount();
    Architectures GetTitlesById(string Id);
    Architectures SumPowerTitlesPercent();
}
