using System.Collections.Generic;
using System.Threading.Tasks;

public class PuppetsService : IPuppetsService
{
    private readonly IPuppetsRepository _puppetRepository;

    public PuppetsService(IPuppetsRepository puppetRepository)
    {
        _puppetRepository = puppetRepository;
    }

    public static PuppetsService Create()
    {
        return new PuppetsService(new PuppetsRepository());
    }

    public async Task<List<string>> GetUniquePuppetsTypesAsync()
    {
        return await _puppetRepository.GetUniquePuppetsTypesAsync();
    }

    public async Task<List<Puppets>> GetPuppetsAsync(string type, int pageSize, int offset, string rare)
    {
        List<Puppets> list = await _puppetRepository.GetPuppetsAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPuppetsCountAsync(string type, string rare)
    {
        return await _puppetRepository.GetPuppetsCountAsync(type, rare);
    }

    public async Task<List<Puppets>> GetPuppetsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Puppets> list = await _puppetRepository.GetPuppetsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPuppetsWithPriceCountAsync(string type)
    {
        return await _puppetRepository.GetPuppetsWithPriceCountAsync(type);
    }

    public async Task<Puppets> GetPuppetByIdAsync(string Id)
    {
        return await _puppetRepository.GetPuppetByIdAsync(Id);
    }

    public async Task<Puppets> SumPowerPuppetsPercentAsync()
    {
        return await _puppetRepository.SumPowerPuppetsPercentAsync();
    }

    public async Task<List<string>> GetUniquePuppetsIdAsync()
    {
        return await _puppetRepository.GetUniquePuppetsIdAsync();
    }
}
