using System.Collections.Generic;
using System.Threading.Tasks;

public class RobotsGalleryService : IRobotsGalleryService
{
    private static RobotsGalleryService _instance;
    private readonly IRobotsGalleryRepository _robotsGalleryRepository;

    public RobotsGalleryService(IRobotsGalleryRepository robotsGalleryRepository)
    {
        _robotsGalleryRepository = robotsGalleryRepository;
    }

    public static RobotsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new RobotsGalleryService(new RobotsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Robots>> GetRobotsCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Robots> list = await _robotsGalleryRepository.GetRobotsCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRobotsCountAsync(string search, string rare)
    {
        return await _robotsGalleryRepository.GetRobotsCountAsync(search, rare);
    }

    public async Task InsertRobotGalleryAsync(string Id)
    {
        IRobotsRepository _repository = new RobotsRepository();
        RobotsService _service = new RobotsService(_repository);
        await _robotsGalleryRepository.InsertRobotGalleryAsync(Id, await _service.GetRobotByIdAsync(Id));
    }

    public async Task UpdateStatusRobotGalleryAsync(string Id)
    {
        await _robotsGalleryRepository.UpdateStatusRobotGalleryAsync(Id);
    }

    public async Task<Robots> SumPowerRobotsGalleryAsync()
    {
        return await _robotsGalleryRepository.SumPowerRobotsGalleryAsync();
    }

    public async Task UpdateStarRobotGalleryAsync(string Id, double star)
    {
        await _robotsGalleryRepository.UpdateStarRobotGalleryAsync(Id, star);
    }

    public async Task UpdateRobotGalleryPowerAsync(string Id)
    {
        IRobotsRepository _repository = new RobotsRepository();
        RobotsService _service = new RobotsService(_repository);
        await _robotsGalleryRepository.UpdateRobotGalleryPowerAsync(Id, await _service.GetRobotByIdAsync(Id));
    }
}
