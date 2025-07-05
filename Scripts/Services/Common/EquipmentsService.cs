using System.Collections.Generic;

public class EquipmentsService : IEquipmentsService
{
    private readonly IEquipmentsRepository _equipmentsRepository;

    public EquipmentsService(IEquipmentsRepository equipmentsRepository)
    {
        _equipmentsRepository = equipmentsRepository;
    }

    public static EquipmentsService Create()
    {
        return new EquipmentsService(new EquipmentsRepository());
    }

    public List<string> GetUniqueEquipmentsTypes()
    {
        return _equipmentsRepository.GetUniqueEquipmentsTypes();
    }

    public List<Equipments> GetEquipments(string type, int pageSize, int offset)
    {
        List<Equipments> list = _equipmentsRepository.GetEquipments(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetEquipmentsCount(string type)
    {
        return _equipmentsRepository.GetEquipmentsCount(type);
    }

    public List<Equipments> GetEquipmentsWithCurrency(string type, int pageSize, int offset)
    {
        return _equipmentsRepository.GetEquipmentsWithCurrency(type, pageSize, offset);
    }

    public List<string> GetEquipmentsSet(string type)
    {
        return _equipmentsRepository.GetEquipmentsSet(type);
    }

    public Equipments GetEquipmentById(string Id)
    {
        return _equipmentsRepository.GetEquipmentById(Id);
    }

    public List<string> GetUniqueEquipmentsId()
    {
        return _equipmentsRepository.GetUniqueEquipmentsId();
    }
}
