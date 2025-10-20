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

    public List<Puppets> GetPuppet(string type, int pageSize, int offset, string rare)
    {
        List<Puppets> list = _puppetRepository.GetPuppet(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetPuppetCount(string type, string rare)
    {
        return _puppetRepository.GetPuppetCount(type, rare);
    }

    public List<Puppets> GetPuppetWithPrice(string type, int pageSize, int offset)
    {
        List<Puppets> list = _puppetRepository.GetPuppetWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetPuppetWithPriceCount(string type)
    {
        return _puppetRepository.GetPuppetWithPriceCount(type);
    }

    public Puppets GetPuppetById(string Id)
    {
        return _puppetRepository.GetPuppetById(Id);
    }

    public Puppets SumPowerPuppetPercent()
    {
        return _puppetRepository.SumPowerPuppetPercent();
    }

    public List<string> GetUniquePuppetId()
    {
        return _puppetRepository.GetUniquePuppetId();
    }
}
