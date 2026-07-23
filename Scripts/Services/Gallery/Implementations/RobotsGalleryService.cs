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

    public async Task<List<Robots>> GetRobotsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Robots> list = await _robotsGalleryRepository.GetRobotsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRobotsCountAsync(string search, string rare)
    {
        return await _robotsGalleryRepository.GetRobotsCountAsync(search, rare);
    }

    public async Task InsertRobotGalleryAsync(string userId, string Id)
    {
        IRobotsRepository _repository = new RobotsRepository();
        RobotsService _service = new RobotsService(_repository);
        await _robotsGalleryRepository.InsertRobotGalleryAsync(userId, Id, await _service.GetRobotByIdAsync(Id));
    }

    public async Task UpdateStatusRobotGalleryAsync(string userId, string Id)
    {
        await _robotsGalleryRepository.UpdateStatusRobotGalleryAsync(userId, Id);
    }

    public async Task<Robots> SumPowerRobotsGalleryAsync(string userId)
    {
        return await _robotsGalleryRepository.SumPowerRobotsGalleryAsync(userId);
    }

    public async Task UpdateStarRobotGalleryAsync(string userId, string Id, double star)
    {
        await _robotsGalleryRepository.UpdateStarRobotGalleryAsync(userId, Id, star);
    }

    public async Task UpdateRobotGalleryPowerAsync(string userId, string Id)
    {
        IRobotsRepository _repository = new RobotsRepository();
        RobotsService _service = new RobotsService(_repository);
        await _robotsGalleryRepository.UpdateRobotGalleryPowerAsync(userId, Id, await _service.GetRobotByIdAsync(Id));
    }
}
