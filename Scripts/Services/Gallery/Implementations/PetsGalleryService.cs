using System.Collections.Generic;
using System.Threading.Tasks;

public class PetsGalleryService : IPetsGalleryService
{
    private static PetsGalleryService _instance;
    private readonly IPetsGalleryRepository _petsGalleryRepository;

    public PetsGalleryService(IPetsGalleryRepository petsGalleryRepository)
    {
        _petsGalleryRepository = petsGalleryRepository;
    }

    public static PetsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new PetsGalleryService(new PetsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Pets>> GetPetsCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Pets> list = await _petsGalleryRepository.GetPetsCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPetsCountAsync(string search, string type, string rare)
    {
        return await _petsGalleryRepository.GetPetsCountAsync(search, type, rare);
    }

    public async Task InsertPetGalleryAsync(string Id)
    {
        IPetsRepository _repository = new PetsRepository();
        PetsService _service = new PetsService(_repository);
        await _petsGalleryRepository.InsertPetGalleryAsync(Id, await _service.GetPetByIdAsync(Id));
    }

    public async Task UpdateStatusPetGalleryAsync(string Id)
    {
        await _petsGalleryRepository.UpdateStatusPetGalleryAsync(Id);
    }

    public async Task<Pets> SumPowerPetsGalleryAsync()
    {
        return await _petsGalleryRepository.SumPowerPetsGalleryAsync();
    }

    public async Task UpdateStarPetGalleryAsync(string Id, double star)
    {
        await _petsGalleryRepository.UpdateStarPetGalleryAsync(Id, star);
    }

    public async Task UpdatePetGalleryPowerAsync(string Id)
    {
        IPetsRepository _repository = new PetsRepository();
        PetsService _service = new PetsService(_repository);
        await _petsGalleryRepository.UpdatePetGalleryPowerAsync(Id, await _service.GetPetByIdAsync(Id));
    }
}
