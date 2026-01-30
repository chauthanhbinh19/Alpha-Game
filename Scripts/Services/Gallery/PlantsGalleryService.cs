using System.Collections.Generic;
using System.Threading.Tasks;

public class PlantsGalleryService : IPlantsGalleryService
{
    private readonly IPlantsGalleryRepository _PlantsGalleryRepository;

    public PlantsGalleryService(IPlantsGalleryRepository PlantsGalleryRepository)
    {
        _PlantsGalleryRepository = PlantsGalleryRepository;
    }

    public static PlantsGalleryService Create()
    {
        return new PlantsGalleryService(new PlantsGalleryRepository());
    }

    public async Task<List<Plants>> GetPlantsCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Plants> list = await _PlantsGalleryRepository.GetPlantsCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPlantsCountAsync(string search, string rare)
    {
        return await _PlantsGalleryRepository.GetPlantsCountAsync(search, rare);
    }

    public async Task InsertPlantGalleryAsync(string Id)
    {
        IPlantsRepository _repository = new PlantsRepository();
        PlantsService _service = new PlantsService(_repository);
        await _PlantsGalleryRepository.InsertPlantGalleryAsync(Id, await _service.GetPlantByIdAsync(Id));
    }

    public async Task UpdateStatusPlantGalleryAsync(string Id)
    {
        await _PlantsGalleryRepository.UpdateStatusPlantGalleryAsync(Id);
    }

    public async Task<Plants> SumPowerPlantsGalleryAsync()
    {
        return await _PlantsGalleryRepository.SumPowerPlantsGalleryAsync();
    }

    public async Task UpdateStarPlantGalleryAsync(string Id, double star)
    {
        await _PlantsGalleryRepository.UpdateStarPlantGalleryAsync(Id, star);
    }

    public async Task UpdatePlantGalleryPowerAsync(string Id)
    {
        IPlantsRepository _repository = new PlantsRepository();
        PlantsService _service = new PlantsService(_repository);
        await _PlantsGalleryRepository.UpdatePlantGalleryPowerAsync(Id, await _service.GetPlantByIdAsync(Id));
    }
}
