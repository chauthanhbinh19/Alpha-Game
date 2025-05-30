using System.Collections.Generic;

public class SkillsGalleryService : ISkillsGalleryService
{
    private readonly ISkillsGalleryRepository _skillsGalleryRepository;

    public SkillsGalleryService(ISkillsGalleryRepository skillsGalleryRepository)
    {
        _skillsGalleryRepository = skillsGalleryRepository;
    }

    public static SkillsGalleryService Create()
    {
        return new SkillsGalleryService(new SkillsGalleryRepository());
    }

    public List<Skills> GetSkillsCollection(string type, int pageSize, int offset)
    {
        List<Skills> list = _skillsGalleryRepository.GetSkillsCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetSkillsCount(string type)
    {
        return _skillsGalleryRepository.GetSkillsCount(type);
    }

    public void InsertSkillsGallery(string Id)
    {
        ISkillsRepository _repository = new SkillsRepository();
        SkillsService _service = new SkillsService(_repository);
        _skillsGalleryRepository.InsertSkillsGallery(Id, _service.GetSkillsById(Id));
    }

    public void UpdateStatusSkillsGallery(string Id)
    {
        _skillsGalleryRepository.UpdateStatusSkillsGallery(Id);
    }

    public Skills SumPowerSkillsGallery()
    {
        return _skillsGalleryRepository.SumPowerSkillsGallery();
    }
}
