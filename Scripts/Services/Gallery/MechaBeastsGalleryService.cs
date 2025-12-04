using System.Collections.Generic;
using System.Threading.Tasks;

public class MechaBeastsGalleryService : IMechaBeastsGalleryService
{
    private readonly IMechaBeastsGalleryRepository _MechaBeastsGalleryRepository;

    public MechaBeastsGalleryService(IMechaBeastsGalleryRepository MechaBeastsGalleryRepository)
    {
        _MechaBeastsGalleryRepository = MechaBeastsGalleryRepository;
    }

    public static MechaBeastsGalleryService Create()
    {
        return new MechaBeastsGalleryService(new MechaBeastsGalleryRepository());
    }

    public async Task<List<MechaBeasts>> GetMechaBeastsCollectionAsync(int pageSize, int offset, string rare)
    {
        List<MechaBeasts> list = await _MechaBeastsGalleryRepository.GetMechaBeastsCollectionAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMechaBeastsCountAsync(string rare)
    {
        return await _MechaBeastsGalleryRepository.GetMechaBeastsCountAsync(rare);
    }

    public async Task InsertMechaBeastGalleryAsync(string Id)
    {
        IMechaBeastsRepository _repository = new MechaBeastsRepository();
        MechaBeastsService _service = new MechaBeastsService(_repository);
        await _MechaBeastsGalleryRepository.InsertMechaBeastGalleryAsync(Id, await _service.GetMechaBeastByIdAsync(Id));
    }

    public async Task UpdateStatusMechaBeastGalleryAsync(string Id)
    {
        await _MechaBeastsGalleryRepository.UpdateStatusMechaBeastGalleryAsync(Id);
    }

    public async Task<MechaBeasts> SumPowerMechaBeastsGalleryAsync()
    {
        return await _MechaBeastsGalleryRepository.SumPowerMechaBeastsGalleryAsync();
    }

    public async Task UpdateStarMechaBeastGalleryAsync(string Id, double star)
    {
        await _MechaBeastsGalleryRepository.UpdateStarMechaBeastGalleryAsync(Id, star);
    }

    public async Task UpdateMechaBeastGalleryPowerAsync(string Id)
    {
        IMechaBeastsRepository _repository = new MechaBeastsRepository();
        MechaBeastsService _service = new MechaBeastsService(_repository);
        await _MechaBeastsGalleryRepository.UpdateMechaBeastGalleryPowerAsync(Id, await _service.GetMechaBeastByIdAsync(Id));
    }
}
