using System.Collections.Generic;
using System.Threading.Tasks;

public class FashionsGalleryService : IFashionsGalleryService
{
    private static FashionsGalleryService _instance;
    private readonly IFashionsGalleryRepository _fashionsGalleryRepository;

    public FashionsGalleryService(IFashionsGalleryRepository fashionsGalleryRepository)
    {
        _fashionsGalleryRepository = fashionsGalleryRepository;
    }

    public static FashionsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new FashionsGalleryService(new FashionsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Fashions>> GetFashionsCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Fashions> list = await _fashionsGalleryRepository.GetFashionsCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFashionsCountAsync(string search, string type, string rare)
    {
        return await _fashionsGalleryRepository.GetFashionsCountAsync(search, type, rare);
    }

    public async Task InsertFashionGalleryAsync(string Id)
    {
        IFashionsRepository _repository = new FashionsRepository();
        FashionsService _service = new FashionsService(_repository);
        await _fashionsGalleryRepository.InsertFashionGalleryAsync(Id, await _service.GetFashionByIdAsync(Id));
    }

    public async Task UpdateStatusFashionGalleryAsync(string Id)
    {
        await _fashionsGalleryRepository.UpdateStatusFashionGalleryAsync(Id);
    }

    public async Task<Fashions> SumPowerFashionsGalleryAsync()
    {
        return await _fashionsGalleryRepository.SumPowerFashionsGalleryAsync();
    }

    public async Task UpdateStarFashionGalleryAsync(string Id, double star)
    {
        await _fashionsGalleryRepository.UpdateStarFashionGalleryAsync(Id, star);
    }

    public async Task UpdateFashionGalleryPowerAsync(string Id)
    {
        IFashionsRepository _repository = new FashionsRepository();
        FashionsService _service = new FashionsService(_repository);
        await _fashionsGalleryRepository.UpdateFashionGalleryPowerAsync(Id, await _service.GetFashionByIdAsync(Id));
    }
}
