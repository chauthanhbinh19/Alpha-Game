using System.Collections.Generic;
using System.Threading.Tasks;

public class WorldsService : IWorldsService
{
    private readonly IWorldsRepository _WorldsRepository;

    public WorldsService(IWorldsRepository titleRepository)
    {
        _WorldsRepository = titleRepository;
    }

    public static WorldsService Create()
    {
        return new WorldsService(new WorldsRepository());
    }

    public async Task<List<Worlds>> GetWorldsAsync(string userId, int pageSize, int offset)
    {
        List<Worlds> list = await _WorldsRepository.GetWorldsAsync(userId, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetWorldsCountAsync(string rare)
    {
        return await _WorldsRepository.GetWorldsCountAsync(rare);
    }

    public async Task<List<Worlds>> GetWorldsWithPriceAsync(int pageSize, int offset)
    {
        List<Worlds> list = await _WorldsRepository.GetWorldsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetWorldsWithPriceCountAsync()
    {
        return await _WorldsRepository.GetWorldsWithPriceCountAsync();
    }

    public async Task<Worlds> GetWorldByIdAsync(string Id)
    {
        return await _WorldsRepository.GetWorldByIdAsync(Id);
    }

    public async Task<Worlds> SumPowerWorldsPercentAsync()
    {
        return await _WorldsRepository.SumPowerWorldsPercentAsync();
    }

    public async Task<List<string>> GetUniqueWorldsIdAsync()
    {
        return await _WorldsRepository.GetUniqueWorldsIdAsync();
    }
}
