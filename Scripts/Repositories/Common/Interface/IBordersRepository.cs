using System.Collections.Generic;

public interface IBordersRepository
{
    List<string> GetUniqueBordersId();
    List<Borders> GetBorders(int pageSize, int offset);
    int GetBordersCount();
    List<Borders> GetBordersWithPrice(int pageSize, int offset);
    int GetBordersWithPriceCount();
    Borders GetBordersById(string Id);
    Borders SumPowerBordersPercent();
}