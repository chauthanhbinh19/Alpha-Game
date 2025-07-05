using System.Collections.Generic;

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

    public List<string> GetUniquePetsTypes()
    {
        return _petsRepository.GetUniquePetsTypes();
    }

    public List<Pets> GetPets(string type, int pageSize, int offset)
    {
        List<Pets> list = _petsRepository.GetPets(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetPetsCount(string type)
    {
        return _petsRepository.GetPetsCount(type);
    }

    public List<Pets> GetPetsWithPrice(string type, int pageSize, int offset)
    {
        List<Pets> list = _petsRepository.GetPetsWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetPetsWithPriceCount(string type)
    {
        return _petsRepository.GetPetsWithPriceCount(type);
    }

    public Pets GetPetsById(string Id)
    {
        return _petsRepository.GetPetsById(Id);
    }

    public List<string> GetUniquePetsId()
    {
        return _petsRepository.GetUniquePetsId();
    }
}
