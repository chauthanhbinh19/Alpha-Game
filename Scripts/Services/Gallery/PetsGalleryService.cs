using System.Collections.Generic;

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

    public List<Pets> GetPetsCollection(string type, int pageSize, int offset)
    {
        List<Pets> list = _petsGalleryRepository.GetPetsCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetPetsCount(string type)
    {
        return _petsGalleryRepository.GetPetsCount(type);
    }

    public void InsertPetsGallery(string Id)
    {
        IPetsRepository _repository = new PetsRepository();
        PetsService _service = new PetsService(_repository);
        _petsGalleryRepository.InsertPetsGallery(Id, _service.GetPetsById(Id));
    }

    public void UpdateStatusPetsGallery(string Id)
    {
        _petsGalleryRepository.UpdateStatusPetsGallery(Id);
    }

    public Pets SumPowerPetsGallery()
    {
        return _petsGalleryRepository.SumPowerPetsGallery();
    }
}
