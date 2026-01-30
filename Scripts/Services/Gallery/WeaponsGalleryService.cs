using System.Collections.Generic;
using System.Threading.Tasks;

public class WeaponsGalleryService : IWeaponsGalleryService
{
    private readonly IWeaponsGalleryRepository _WeaponsGalleryRepository;

    public WeaponsGalleryService(IWeaponsGalleryRepository WeaponsGalleryRepository)
    {
        _WeaponsGalleryRepository = WeaponsGalleryRepository;
    }

    public static WeaponsGalleryService Create()
    {
        return new WeaponsGalleryService(new WeaponsGalleryRepository());
    }

    public async Task<List<Weapons>> GetWeaponsCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Weapons> list = await _WeaponsGalleryRepository.GetWeaponsCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetWeaponsCountAsync(string search, string rare)
    {
        return await _WeaponsGalleryRepository.GetWeaponsCountAsync(search, rare);
    }

    public async Task InsertWeaponGalleryAsync(string Id)
    {
        IWeaponsRepository _repository = new WeaponsRepository();
        WeaponsService _service = new WeaponsService(_repository);
        await _WeaponsGalleryRepository.InsertWeaponGalleryAsync(Id, await _service.GetWeaponByIdAsync(Id));
    }

    public async Task UpdateStatusWeaponGalleryAsync(string Id)
    {
        await _WeaponsGalleryRepository.UpdateStatusWeaponGalleryAsync(Id);
    }

    public async Task<Weapons> SumPowerWeaponsGalleryAsync()
    {
        return await _WeaponsGalleryRepository.SumPowerWeaponsGalleryAsync();
    }

    public async Task UpdateStarWeaponGalleryAsync(string Id, double star)
    {
        await _WeaponsGalleryRepository.UpdateStarWeaponGalleryAsync(Id, star);
    }

    public async Task UpdateWeaponGalleryPowerAsync(string Id)
    {
        IWeaponsRepository _repository = new WeaponsRepository();
        WeaponsService _service = new WeaponsService(_repository);
        await _WeaponsGalleryRepository.UpdateWeaponGalleryPowerAsync(Id, await _service.GetWeaponByIdAsync(Id));
    }
}
