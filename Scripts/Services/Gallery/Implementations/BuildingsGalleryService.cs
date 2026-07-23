using System.Collections.Generic;
using System.Threading.Tasks;

public class BuildingsGalleryService : IBuildingsGalleryService
{
    private static BuildingsGalleryService _instance;
    private readonly IBuildingsGalleryRepository _buildingsGalleryRepository;

    public BuildingsGalleryService(IBuildingsGalleryRepository buildingsGalleryRepository)
    {
        _buildingsGalleryRepository = buildingsGalleryRepository;
    }

    public static BuildingsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new BuildingsGalleryService(new BuildingsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Buildings>> GetBuildingsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Buildings> list = await _buildingsGalleryRepository.GetBuildingsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBuildingsCountAsync(string search, string type, string rare)
    {
        return await _buildingsGalleryRepository.GetBuildingsCountAsync(search, type, rare);
    }

    public async Task InsertBuildingGalleryAsync(string userId, string Id)
    {
        IBuildingsRepository _repository = new BuildingsRepository();
        BuildingsService _service = new BuildingsService(_repository);
        await _buildingsGalleryRepository.InsertBuildingGalleryAsync(userId, Id, await _service.GetBuildingByIdAsync(Id));
    }

    public async Task UpdateStatusBuildingGalleryAsync(string userId, string Id)
    {
        await _buildingsGalleryRepository.UpdateStatusBuildingGalleryAsync(userId, Id);
    }

    public async Task<Buildings> SumPowerBuildingsGalleryAsync(string userId)
    {
        return await _buildingsGalleryRepository.SumPowerBuildingsGalleryAsync(userId);
    }

    public async Task UpdateStarBuildingGalleryAsync(string userId, string Id, double star)
    {
        await _buildingsGalleryRepository.UpdateStarBuildingGalleryAsync(userId, Id, star);
    }

    public async Task UpdateBuildingGalleryPowerAsync(string userId, string Id)
    {
        IBuildingsRepository _repository = new BuildingsRepository();
        BuildingsService _service = new BuildingsService(_repository);
        await _buildingsGalleryRepository.UpdateBuildingGalleryPowerAsync(userId, Id, await _service.GetBuildingByIdAsync(Id));
    }
}
