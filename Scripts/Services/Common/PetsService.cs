using System.Collections.Generic;
using System.Threading.Tasks;

public class PetsService : IPetsService
{
    private readonly IPetsRepository _petsRepository;

    public PetsService(IPetsRepository petsRepository)
    {
        _petsRepository = petsRepository;
    }

    public static PetsService Create()
    {
        return new PetsService(new PetsRepository());
    }

    public async Task<List<string>> GetUniquePetsTypesAsync()
    {
        return await _petsRepository.GetUniquePetsTypesAsync();
    }

    public async Task<List<Pets>> GetPetsAsync(string type, int pageSize, int offset, string rare)
    {
        List<Pets> list = await _petsRepository.GetPetsAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPetsCountAsync(string type, string rare)
    {
        return await _petsRepository.GetPetsCountAsync(type, rare);
    }

    public async Task<List<Pets>> GetPetsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Pets> list = await _petsRepository.GetPetsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPetsWithPriceCountAsync(string type)
    {
        return await _petsRepository.GetPetsWithPriceCountAsync(type);
    }

    public async Task<Pets> GetPetByIdAsync(string Id)
    {
        return await _petsRepository.GetPetByIdAsync(Id);
    }

    public async Task<List<string>> GetUniquePetsIdAsync()
    {
        return await _petsRepository.GetUniquePetsIdAsync();
    }
}
