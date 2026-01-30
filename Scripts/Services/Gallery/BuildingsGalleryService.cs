using System.Collections.Generic;
using System.Threading.Tasks;

public class BuildingsGalleryService : IBuildingsGalleryService
{
    private readonly IBuildingsGalleryRepository _BuildingGalleryRepository;

    public BuildingsGalleryService(IBuildingsGalleryRepository BuildingGalleryRepository)
    {
        _BuildingGalleryRepository = BuildingGalleryRepository;
    }

    public static BuildingsGalleryService Create()
    {
        return new BuildingsGalleryService(new BuildingsGalleryRepository());
    }

    public async Task<List<Buildings>> GetBuildingsCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Buildings> list = await _BuildingGalleryRepository.GetBuildingsCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBuildingsCountAsync(string search, string type, string rare)
    {
        return await _BuildingGalleryRepository.GetBuildingsCountAsync(search, type, rare);
    }

    public async Task InsertBuildingGalleryAsync(string Id)
    {
        IBuildingsRepository _repository = new BuildingsRepository();
        BuildingsService _service = new BuildingsService(_repository);
        await _BuildingGalleryRepository.InsertBuildingGalleryAsync(Id, await _service.GetBuildingByIdAsync(Id));
    }

    public async Task UpdateStatusBuildingGalleryAsync(string Id)
    {
        await _BuildingGalleryRepository.UpdateStatusBuildingGalleryAsync(Id);
    }

    public async Task<Buildings> SumPowerBuildingsGalleryAsync()
    {
        return await _BuildingGalleryRepository.SumPowerBuildingsGalleryAsync();
    }

    public async Task UpdateStarBuildingGalleryAsync(string Id, double star)
    {
        await _BuildingGalleryRepository.UpdateStarBuildingGalleryAsync(Id, star);
    }

    public async Task UpdateBuildingGalleryPowerAsync(string Id)
    {
        IBuildingsRepository _repository = new BuildingsRepository();
        BuildingsService _service = new BuildingsService(_repository);
        await _BuildingGalleryRepository.UpdateBuildingGalleryPowerAsync(Id, await _service.GetBuildingByIdAsync(Id));
    }
}
