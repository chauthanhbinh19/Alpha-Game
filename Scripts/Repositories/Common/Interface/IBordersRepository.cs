using System.Collections.Generic;

public interface IBordersRepository
{
    List<string> GetUniqueBordersId();
    List<Borders> GetBorders(int pageSize, int offset, string rare);
    int GetBordersCount(string rare);
    List<Borders> GetBordersWithPrice(int pageSize, int offset);
    int GetBordersWithPriceCount();
    Borders GetBordersById(string Id);
    Borders SumPowerBordersPercent();
}