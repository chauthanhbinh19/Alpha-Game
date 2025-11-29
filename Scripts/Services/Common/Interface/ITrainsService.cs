using System.Collections.Generic;

public interface ITrainsService
{
    List<string> GetUniqueTrainId();
    List<Trains> GetTrains(string userId, int pageSize, int offset);
    int GetTrainsCount(string rare);
    List<Trains> GetTrainsWithPrice(int pageSize, int offset);
    int GetTrainsWithPriceCount();
    Trains GetTrainsById(string Id);
    Trains SumPowerTrainsPercent();
}
