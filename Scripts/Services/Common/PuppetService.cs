using System.Collections.Generic;

public class PuppetService : IPuppetService
{
    private readonly IPuppetRepository _puppetRepository;

    public PuppetService(IPuppetRepository puppetRepository)
    {
        _puppetRepository = puppetRepository;
    }

    public static PuppetService Create()
    {
        return new PuppetService(new PuppetRepository());
    }

    public List<string> GetUniquePuppetTypes()
    {
        return _puppetRepository.GetUniquePuppetTypes();
    }

    public List<Puppet> GetPuppet(string type, int pageSize, int offset)
    {
        List<Puppet> list = _puppetRepository.GetPuppet(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetPuppetCount(string type)
    {
        return _puppetRepository.GetPuppetCount(type);
    }

    public List<Puppet> GetPuppetWithPrice(string type, int pageSize, int offset)
    {
        List<Puppet> list = _puppetRepository.GetPuppetWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetPuppetWithPriceCount(string type)
    {
        return _puppetRepository.GetPuppetWithPriceCount(type);
    }

    public Puppet GetPuppetById(string Id)
    {
        return _puppetRepository.GetPuppetById(Id);
    }

    public Puppet SumPowerPuppetPercent()
    {
        return _puppetRepository.SumPowerPuppetPercent();
    }
}
