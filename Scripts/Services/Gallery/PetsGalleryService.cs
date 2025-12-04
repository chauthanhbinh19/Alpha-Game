using System.Collections.Generic;
using System.Threading.Tasks;

public class PetsGalleryService : IPetsGalleryService
{
    private readonly IPetsGalleryRepository _petsGalleryRepository;

    public PetsGalleryService(IPetsGalleryRepository petsGalleryRepository)
    {
        _petsGalleryRepository = petsGalleryRepository;
    }

    public static PetsGalleryService Create()
    {
        return new PetsGalleryService(new PetsGalleryRepository());
    }

    public async Task<List<Pets>> GetPetsCollectionAsync(string type, int pageSize, int offset, string rare)
    {
        List<Pets> list = await _petsGalleryRepository.GetPetsCollectionAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPetsCountAsync(string type, string rare)
    {
        return await _petsGalleryRepository.GetPetsCountAsync(type, rare);
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
