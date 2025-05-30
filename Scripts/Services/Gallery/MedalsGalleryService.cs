using System.Collections.Generic;

public class MedalsGalleryService : IMedalsGalleryService
{
    private IMedalsGalleryRepository _medalsGalleryRepository;

    public MedalsGalleryService(IMedalsGalleryRepository medalsGalleryRepository)
    {
        _medalsGalleryRepository = medalsGalleryRepository;
    }

    public static MedalsGalleryService Create()
    {
        return new MedalsGalleryService(new MedalsGalleryRepository());
    }

    public List<Medals> GetMedalsCollection(int pageSize, int offset)
    {
        List<Medals> list = _medalsGalleryRepository.GetMedalsCollection(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetMedalsCount()
    {
        return _medalsGalleryRepository.GetMedalsCount();
    }

    public void InsertMedalsGallery(string Id)
    {
        IMedalsRepository _repository = new MedalsRepository();
        MedalsService _service = new MedalsService(_repository);
        _medalsGalleryRepository.InsertMedalsGallery(Id, _service.GetMedalsById(Id));
    }

    public void UpdateStatusMedalsGallery(string Id)
    {
        _medalsGalleryRepository.UpdateStatusMedalsGallery(Id);
    }

    public Medals SumPowerMedalsGallery()
    {
        return _medalsGalleryRepository.SumPowerMedalsGallery();
    }
}
