using System.Collections.Generic;
using System.Threading.Tasks;

public class BordersGalleryService : IBordersGalleryService
{
    private IBordersGalleryRepository _bordersGalleryRepository;

    public BordersGalleryService(IBordersGalleryRepository bordersGalleryRepository)
    {
        _bordersGalleryRepository = bordersGalleryRepository;
    }

    public static BordersGalleryService Create()
    {
        return new BordersGalleryService(new BordersGalleryRepository());
    }

    public async Task<List<Borders>> GetBordersCollectionAsync(int pageSize, int offset, string rare)
    {
        List<Borders> list = await _bordersGalleryRepository.GetBordersCollectionAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBordersCountAsync(string rare)
    {
        return await _bordersGalleryRepository.GetBordersCountAsync(rare);
    }

    public async Task InsertBorderGalleryAsync(string Id)
    {
        IBordersRepository _repository = new BordersRepository();
        BordersService _service = new BordersService(_repository);
        await _bordersGalleryRepository.InsertBorderGalleryAsync(Id, await _service.GetBorderByIdAsync(Id));
    }

    public async Task UpdateStatusBorderGalleryAsync(string Id)
    {
        await _bordersGalleryRepository.UpdateStatusBorderGalleryAsync(Id);
    }

    public async Task<Borders> SumPowerBordersGalleryAsync()
    {
        return await _bordersGalleryRepository.SumPowerBordersGalleryAsync();
    }

    public async Task UpdateStarBorderGalleryAsync(string Id, double star)
    {
        await _bordersGalleryRepository.UpdateStarBorderGalleryAsync(Id, star);
    }

    public async Task UpdateBorderGalleryPowerAsync(string Id)
    {
        IBordersRepository _repository = new BordersRepository();
        BordersService _service = new BordersService(_repository);
        await _bordersGalleryRepository.UpdateBorderGalleryPowerAsync(Id, await _service.GetBorderByIdAsync(Id));
    }
}
