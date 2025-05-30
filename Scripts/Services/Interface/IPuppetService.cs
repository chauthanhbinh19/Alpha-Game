using System.Collections.Generic;

public interface IPuppetService
{
    List<string> GetUniquePuppetTypes();
    List<Puppet> GetPuppet(string type, int pageSize, int offset);
    int GetPuppetCount(string type);
    List<Puppet> GetPuppetWithPrice(string type, int pageSize, int offset);
    int GetPuppetWithPriceCount(string type);
    Puppet GetPuppetById(string Id);
    Puppet SumPowerPuppetPercent();
}
