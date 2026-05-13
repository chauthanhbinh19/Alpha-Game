using System.Collections.Generic;
using System.Threading.Tasks;

public class BuildingsService : IBuildingsService
{
    private static BuildingsService _instance;
    private readonly IBuildingsRepository _buildingsRepository;

    public BuildingsService(IBuildingsRepository buildingsRepository)
    {
        _buildingsRepository = buildingsRepository;
    }

    public static BuildingsService Create()
    {
        if (_instance == null)
        {
            _instance = new BuildingsService(new BuildingsRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueBuildingsTypesAsync()
    {
        return await _buildingsRepository.GetUniqueBuildingsTypesAsync();
    }

    public async Task<List<Buildings>> GetBuildingsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Buildings> list = await _buildingsRepository.GetBuildingsAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBuildingsCountAsync(string search, string type, string rare)
    {
        return await _buildingsRepository.GetBuildingsCountAsync(search, type, rare);
    }

    public async Task<List<Buildings>> GetBuildingsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Buildings> list = await _buildingsRepository.GetBuildingsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBuildingsWithPriceCountAsync(string type)
    {
        return await _buildingsRepository.GetBuildingsWithPriceCountAsync(type);
    }

    public async Task<Buildings> GetBuildingByIdAsync(string Id)
    {
        return await _buildingsRepository.GetBuildingByIdAsync(Id);
    }

    public async Task<Buildings> SumPowerBuildingsPercentAsync()
    {
        return await _buildingsRepository.SumPowerBuildingsPercentAsync();
    }

    public async Task<List<string>> GetUniqueBuildingsIdAsync()
    {
        return await _buildingsRepository.GetUniqueBuildingsIdAsync();
    }

    public async Task<List<Buildings>> GetBuildingsWithoutLimitAsync()
    {
        return await _buildingsRepository.GetBuildingsWithoutLimitAsync();
    }
}
