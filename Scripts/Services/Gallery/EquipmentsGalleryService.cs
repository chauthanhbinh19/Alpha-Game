using System.Collections.Generic;

public class EquipmentsGalleryService : IEquipmentsGalleryService
{
    private readonly IEquipmentsGalleryRepository _equipmentsGalleryRepository;

    public EquipmentsGalleryService(IEquipmentsGalleryRepository equipmentsGalleryRepository)
    {
        _equipmentsGalleryRepository = equipmentsGalleryRepository;
    }

    public static EquipmentsGalleryService Create()
    {
        return new EquipmentsGalleryService(new EquipmentsGalleryRepository());
    }

    public List<Equipments> GetEquipmentsCollection(string type, int pageSize, int offset)
    {
        List<Equipments> list = _equipmentsGalleryRepository.GetEquipmentsCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetEquipmentsCount(string type)
    {
        return _equipmentsGalleryRepository.GetEquipmentsCount(type);
    }

    public void InsertEquipmentsGallery(string Id)
    {
        IEquipmentsRepository _repository = new EquipmentsRepository();
        EquipmentsService _service = new EquipmentsService(_repository);
        _equipmentsGalleryRepository.InsertEquipmentsGallery(Id, _service.GetEquipmentById(Id));
    }

    public void UpdateStatusEquipmentsGallery(string Id)
    {
        _equipmentsGalleryRepository.UpdateStatusEquipmentsGallery(Id);
    }

    public Equipments SumPowerEquipmentsGallery()
    {
        return _equipmentsGalleryRepository.SumPowerEquipmentsGallery();
    }
}
