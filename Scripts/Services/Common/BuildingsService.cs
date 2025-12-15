using System.Collections.Generic;
using System.Threading.Tasks;

public class BuildingsService : IBuildingsService
{
    private readonly IBuildingsRepository _BuildingsRepository;

    public BuildingsService(IBuildingsRepository BuildingsRepository)
    {
        _BuildingsRepository = BuildingsRepository;
    }

    public static BuildingsService Create()
    {
        return new BuildingsService(new BuildingsRepository());
    }

    public async Task<List<string>> GetUniqueBuildingsTypesAsync()
    {
        return await _BuildingsRepository.GetUniqueBuildingsTypesAsync();
    }

    public async Task<List<Buildings>> GetBuildingsAsync(string type, int pageSize, int offset, string rare)
    {
        List<Buildings> list = await _BuildingsRepository.GetBuildingsAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBuildingsCountAsync(string type, string rare)
    {
        return await _BuildingsRepository.GetBuildingsCountAsync(type, rare);
    }

    public async Task<List<Buildings>> GetBuildingsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Buildings> list = await _BuildingsRepository.GetBuildingsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBuildingsWithPriceCountAsync(string type)
    {
        return await _BuildingsRepository.GetBuildingsWithPriceCountAsync(type);
    }

    public async Task<Buildings> GetBuildingByIdAsync(string Id)
    {
        return await _BuildingsRepository.GetBuildingByIdAsync(Id);
    }

    public async Task<Buildings> SumPowerBuildingsPercentAsync()
    {
        return await _BuildingsRepository.SumPowerBuildingsPercentAsync();
    }

    public async Task<List<string>> GetUniqueBuildingsIdAsync()
    {
        return await _BuildingsRepository.GetUniqueBuildingsIdAsync();
    }
}
