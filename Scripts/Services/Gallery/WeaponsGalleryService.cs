using System.Collections.Generic;
using System.Threading.Tasks;

public class WeaponsGalleryService : IWeaponsGalleryService
{
    private static WeaponsGalleryService _instance;
    private readonly IWeaponsGalleryRepository _weaponsGalleryRepository;

    public WeaponsGalleryService(IWeaponsGalleryRepository weaponsGalleryRepository)
    {
        _weaponsGalleryRepository = weaponsGalleryRepository;
    }

    public static WeaponsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new WeaponsGalleryService(new WeaponsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Weapons>> GetWeaponsCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Weapons> list = await _weaponsGalleryRepository.GetWeaponsCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetWeaponsCountAsync(string search, string rare)
    {
        return await _weaponsGalleryRepository.GetWeaponsCountAsync(search, rare);
    }

    public async Task InsertWeaponGalleryAsync(string Id)
    {
        IWeaponsRepository _repository = new WeaponsRepository();
        WeaponsService _service = new WeaponsService(_repository);
        await _weaponsGalleryRepository.InsertWeaponGalleryAsync(Id, await _service.GetWeaponByIdAsync(Id));
    }

    public async Task UpdateStatusWeaponGalleryAsync(string Id)
    {
        await _weaponsGalleryRepository.UpdateStatusWeaponGalleryAsync(Id);
    }

    public async Task<Weapons> SumPowerWeaponsGalleryAsync()
    {
        return await _weaponsGalleryRepository.SumPowerWeaponsGalleryAsync();
    }

    public async Task UpdateStarWeaponGalleryAsync(string Id, double star)
    {
        await _weaponsGalleryRepository.UpdateStarWeaponGalleryAsync(Id, star);
    }

    public async Task UpdateWeaponGalleryPowerAsync(string Id)
    {
        IWeaponsRepository _repository = new WeaponsRepository();
        WeaponsService _service = new WeaponsService(_repository);
        await _weaponsGalleryRepository.UpdateWeaponGalleryPowerAsync(Id, await _service.GetWeaponByIdAsync(Id));
    }
}
