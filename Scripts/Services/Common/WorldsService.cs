using System.Collections.Generic;
using System.Threading.Tasks;

public class WorldsService : IWorldsService
{
    private static WorldsService _instance;
    private readonly IWorldsRepository _worldsRepository;

    public WorldsService(IWorldsRepository worldsRepository)
    {
        _worldsRepository = worldsRepository;
    }

    public static WorldsService Create()
    {
        if (_instance == null)
        {
            _instance = new WorldsService(new WorldsRepository());
        }
        return _instance;
    }

    public async Task<List<Worlds>> GetWorldsAsync(string userId, int pageSize, int offset)
    {
        List<Worlds> list = await _worldsRepository.GetWorldsAsync(userId, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetWorldsCountAsync(string rare)
    {
        return await _worldsRepository.GetWorldsCountAsync(rare);
    }

    public async Task<List<Worlds>> GetWorldsWithPriceAsync(int pageSize, int offset)
    {
        List<Worlds> list = await _worldsRepository.GetWorldsWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetWorldsWithPriceCountAsync()
    {
        return await _worldsRepository.GetWorldsWithPriceCountAsync();
    }

    public async Task<Worlds> GetWorldByIdAsync(string Id)
    {
        return await _worldsRepository.GetWorldByIdAsync(Id);
    }

    public async Task<Worlds> SumPowerWorldsPercentAsync()
    {
        return await _worldsRepository.SumPowerWorldsPercentAsync();
    }

    public async Task<List<string>> GetUniqueWorldsIdAsync()
    {
        return await _worldsRepository.GetUniqueWorldsIdAsync();
    }
}
