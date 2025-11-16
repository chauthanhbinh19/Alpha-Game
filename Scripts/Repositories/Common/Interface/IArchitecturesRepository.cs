using System.Collections.Generic;

public interface IArchitecturesRepository
{
    List<string> GetUniqueArchitectureId();
    List<Architectures> GetArchitectures(int pageSize, int offset, string rare);
    int GetArchitecturesCount(string rare);
    List<Architectures> GetArchitecturesWithPrice(int pageSize, int offset);
    int GetArchitecturesWithPriceCount();
    Architectures GetArchitecturesById(string Id);
    Architectures SumPowerArchitecturesPercent();
}
