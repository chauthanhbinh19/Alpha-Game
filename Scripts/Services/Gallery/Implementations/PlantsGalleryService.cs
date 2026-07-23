using System.Collections.Generic;
using System.Threading.Tasks;

public class PlantsGalleryService : IPlantsGalleryService
{
    private static PlantsGalleryService _instance;
    private readonly IPlantsGalleryRepository _plantsGalleryRepository;

    public PlantsGalleryService(IPlantsGalleryRepository plantsGalleryRepository)
    {
        _plantsGalleryRepository = plantsGalleryRepository;
    }

    public static PlantsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new PlantsGalleryService(new PlantsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Plants>> GetPlantsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Plants> list = await _plantsGalleryRepository.GetPlantsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPlantsCountAsync(string search, string rare)
    {
        return await _plantsGalleryRepository.GetPlantsCountAsync(search, rare);
    }

    public async Task InsertPlantGalleryAsync(string userId, string Id)
    {
        IPlantsRepository _repository = new PlantsRepository();
        PlantsService _service = new PlantsService(_repository);
        await _plantsGalleryRepository.InsertPlantGalleryAsync(userId, Id, await _service.GetPlantByIdAsync(Id));
    }

    public async Task UpdateStatusPlantGalleryAsync(string userId, string Id)
    {
        await _plantsGalleryRepository.UpdateStatusPlantGalleryAsync(userId, Id);
    }

    public async Task<Plants> SumPowerPlantsGalleryAsync(string userId)
    {
        return await _plantsGalleryRepository.SumPowerPlantsGalleryAsync(userId);
    }

    public async Task UpdateStarPlantGalleryAsync(string userId, string Id, double star)
    {
        await _plantsGalleryRepository.UpdateStarPlantGalleryAsync(userId, Id, star);
    }

    public async Task UpdatePlantGalleryPowerAsync(string userId, string Id)
    {
        IPlantsRepository _repository = new PlantsRepository();
        PlantsService _service = new PlantsService(_repository);
        await _plantsGalleryRepository.UpdatePlantGalleryPowerAsync(userId, Id, await _service.GetPlantByIdAsync(Id));
    }
}
