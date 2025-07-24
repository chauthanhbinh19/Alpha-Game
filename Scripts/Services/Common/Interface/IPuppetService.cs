using System.Collections.Generic;

public interface IPuppetService
{
    List<string> GetUniquePuppetTypes();
    List<string> GetUniquePuppetId();
    List<Puppet> GetPuppet(string type, int pageSize, int offset, string rare);
    int GetPuppetCount(string type, string rare);
    List<Puppet> GetPuppetWithPrice(string type, int pageSize, int offset);
    int GetPuppetWithPriceCount(string type);
    Puppet GetPuppetById(string Id);
    Puppet SumPowerPuppetPercent();
}
