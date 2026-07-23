using System.Collections.Generic;
using System.Threading.Tasks;

public class BordersGalleryService : IBordersGalleryService
{
    private static BordersGalleryService _instance;
    private readonly IBordersGalleryRepository _bordersGalleryRepository;

    public BordersGalleryService(IBordersGalleryRepository bordersGalleryRepository)
    {
        _bordersGalleryRepository = bordersGalleryRepository;
    }

    public static BordersGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new BordersGalleryService(new BordersGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Borders>> GetBordersCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Borders> list = await _bordersGalleryRepository.GetBordersCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBordersCountAsync(string search, string rare)
    {
        return await _bordersGalleryRepository.GetBordersCountAsync(search, rare);
    }

    public async Task InsertBorderGalleryAsync(string userId, string Id)
    {
        IBordersRepository _repository = new BordersRepository();
        BordersService _service = new BordersService(_repository);
        await _bordersGalleryRepository.InsertBorderGalleryAsync(userId, Id, await _service.GetBorderByIdAsync(Id));
    }

    public async Task UpdateStatusBorderGalleryAsync(string userId, string Id)
    {
        await _bordersGalleryRepository.UpdateStatusBorderGalleryAsync(userId, Id);
    }

    public async Task<Borders> SumPowerBordersGalleryAsync(string userId)
    {
        return await _bordersGalleryRepository.SumPowerBordersGalleryAsync(userId);
    }

    public async Task UpdateStarBorderGalleryAsync(string userId, string Id, double star)
    {
        await _bordersGalleryRepository.UpdateStarBorderGalleryAsync(userId, Id, star);
    }

    public async Task UpdateBorderGalleryPowerAsync(string userId, string Id)
    {
        IBordersRepository _repository = new BordersRepository();
        BordersService _service = new BordersService(_repository);
        await _bordersGalleryRepository.UpdateBorderGalleryPowerAsync(userId, Id, await _service.GetBorderByIdAsync(Id));
    }
}
