using System.Collections.Generic;

public interface IPuppetRepository
{
    List<string> GetUniquePuppetTypes();
    List<string> GetUniquePuppetId();
    List<Puppets> GetPuppet(string type, int pageSize, int offset, string rare);
    int GetPuppetCount(string type, string rare);
    List<Puppets> GetPuppetWithPrice(string type, int pageSize, int offset);
    int GetPuppetWithPriceCount(string type);
    Puppets GetPuppetById(string Id);
    Puppets SumPowerPuppetPercent();
}
