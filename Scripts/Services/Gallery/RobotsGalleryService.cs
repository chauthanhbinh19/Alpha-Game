using System.Collections.Generic;
using System.Threading.Tasks;

public class RobotsGalleryService : IRobotsGalleryService
{
    private readonly IRobotsGalleryRepository _RobotsGalleryRepository;

    public RobotsGalleryService(IRobotsGalleryRepository RobotsGalleryRepository)
    {
        _RobotsGalleryRepository = RobotsGalleryRepository;
    }

    public static RobotsGalleryService Create()
    {
        return new RobotsGalleryService(new RobotsGalleryRepository());
    }

    public async Task<List<Robots>> GetRobotsCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Robots> list = await _RobotsGalleryRepository.GetRobotsCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRobotsCountAsync(string search, string rare)
    {
        return await _RobotsGalleryRepository.GetRobotsCountAsync(search, rare);
    }

    public async Task InsertRobotGalleryAsync(string Id)
    {
        IRobotsRepository _repository = new RobotsRepository();
        RobotsService _service = new RobotsService(_repository);
        await _RobotsGalleryRepository.InsertRobotGalleryAsync(Id, await _service.GetRobotByIdAsync(Id));
    }

    public async Task UpdateStatusRobotGalleryAsync(string Id)
    {
        await _RobotsGalleryRepository.UpdateStatusRobotGalleryAsync(Id);
    }

    public async Task<Robots> SumPowerRobotsGalleryAsync()
    {
        return await _RobotsGalleryRepository.SumPowerRobotsGalleryAsync();
    }

    public async Task UpdateStarRobotGalleryAsync(string Id, double star)
    {
        await _RobotsGalleryRepository.UpdateStarRobotGalleryAsync(Id, star);
    }

    public async Task UpdateRobotGalleryPowerAsync(string Id)
    {
        IRobotsRepository _repository = new RobotsRepository();
        RobotsService _service = new RobotsService(_repository);
        await _RobotsGalleryRepository.UpdateRobotGalleryPowerAsync(Id, await _service.GetRobotByIdAsync(Id));
    }
}
