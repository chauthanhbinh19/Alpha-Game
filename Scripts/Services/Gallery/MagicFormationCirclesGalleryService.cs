using System.Collections.Generic;
using System.Threading.Tasks;

public class MagicFormationCirclesGalleryService : IMagicFormationCirclesGalleryService
{
    private IMagicFormationCirclesGalleryRepository _magicFormationCircleRepository;

    public MagicFormationCirclesGalleryService(IMagicFormationCirclesGalleryRepository magicFormationCircleRepository)
    {
        _magicFormationCircleRepository = magicFormationCircleRepository;
    }

    public static MagicFormationCirclesGalleryService Create()
    {
        return new MagicFormationCirclesGalleryService(new MagicFormationCirclesGalleryRepository());
    }

    public async Task<List<MagicFormationCircles>> GetMagicFormationCirclesCollectionAsync(string type, int pageSize, int offset, string rare)
    {
        List<MagicFormationCircles> list = await _magicFormationCircleRepository.GetMagicFormationCirclesCollectionAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMagicFormationCirclesCountAsync(string type, string rare)
    {
        return await _magicFormationCircleRepository.GetMagicFormationCirclesCountAsync(type, rare);
    }

    public async Task InsertMagicFormationCircleGalleryAsync(string Id)
    {
        IMagicFormationCirclesRepository _repository = new MagicFormationCirclesRepository();
        MagicFormationCirclesService _service = new MagicFormationCirclesService(_repository);
        await _magicFormationCircleRepository.InsertMagicFormationCircleGalleryAsync(Id, await _service.GetMagicFormationCircleByIdAsync(Id));
    }

    public async Task UpdateStatusMagicFormationCircleGalleryAsync(string Id)
    {
        await _magicFormationCircleRepository.UpdateStatusMagicFormationCircleGalleryAsync(Id);
    }

    public async Task<MagicFormationCircles> SumPowerMagicFormationCirclesGalleryAsync()
    {
        return await _magicFormationCircleRepository.SumPowerMagicFormationCirclesGalleryAsync();
    }

    public async Task UpdateStarMagicFormationCircleGalleryAsync(string Id, double star)
    {
        await _magicFormationCircleRepository.UpdateStarMagicFormationCircleGalleryAsync(Id, star);
    }

    public async Task UpdateMagicFormationCircleGalleryPowerAsync(string Id)
    {
        IMagicFormationCirclesRepository _repository = new MagicFormationCirclesRepository();
        MagicFormationCirclesService _service = new MagicFormationCirclesService(_repository);
        await _magicFormationCircleRepository.UpdateMagicFormationCircleGalleryPowerAsync(Id, await _service.GetMagicFormationCircleByIdAsync(Id));
    }
}
