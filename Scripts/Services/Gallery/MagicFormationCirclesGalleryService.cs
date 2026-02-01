using System.Collections.Generic;
using System.Threading.Tasks;

public class MagicFormationCirclesGalleryService : IMagicFormationCirclesGalleryService
{
    private static MagicFormationCirclesGalleryService _instance;
    private IMagicFormationCirclesGalleryRepository _magicFormationCirclesRepository;

    public MagicFormationCirclesGalleryService(IMagicFormationCirclesGalleryRepository magicFormationCirclesRepository)
    {
        _magicFormationCirclesRepository = magicFormationCirclesRepository;
    }

    public static MagicFormationCirclesGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new MagicFormationCirclesGalleryService(new MagicFormationCirclesGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<MagicFormationCircles>> GetMagicFormationCirclesCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<MagicFormationCircles> list = await _magicFormationCirclesRepository.GetMagicFormationCirclesCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMagicFormationCirclesCountAsync(string search, string type, string rare)
    {
        return await _magicFormationCirclesRepository.GetMagicFormationCirclesCountAsync(search, type, rare);
    }

    public async Task InsertMagicFormationCircleGalleryAsync(string Id)
    {
        IMagicFormationCirclesRepository _repository = new MagicFormationCirclesRepository();
        MagicFormationCirclesService _service = new MagicFormationCirclesService(_repository);
        await _magicFormationCirclesRepository.InsertMagicFormationCircleGalleryAsync(Id, await _service.GetMagicFormationCircleByIdAsync(Id));
    }

    public async Task UpdateStatusMagicFormationCircleGalleryAsync(string Id)
    {
        await _magicFormationCirclesRepository.UpdateStatusMagicFormationCircleGalleryAsync(Id);
    }

    public async Task<MagicFormationCircles> SumPowerMagicFormationCirclesGalleryAsync()
    {
        return await _magicFormationCirclesRepository.SumPowerMagicFormationCirclesGalleryAsync();
    }

    public async Task UpdateStarMagicFormationCircleGalleryAsync(string Id, double star)
    {
        await _magicFormationCirclesRepository.UpdateStarMagicFormationCircleGalleryAsync(Id, star);
    }

    public async Task UpdateMagicFormationCircleGalleryPowerAsync(string Id)
    {
        IMagicFormationCirclesRepository _repository = new MagicFormationCirclesRepository();
        MagicFormationCirclesService _service = new MagicFormationCirclesService(_repository);
        await _magicFormationCirclesRepository.UpdateMagicFormationCircleGalleryPowerAsync(Id, await _service.GetMagicFormationCircleByIdAsync(Id));
    }
}
