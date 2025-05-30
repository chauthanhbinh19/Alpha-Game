using System.Collections.Generic;

public class CollaborationGalleryService : ICollaborationGalleryService
{
    private readonly ICollaborationGalleryRepository _collaborationGalleryRepository;

    public CollaborationGalleryService(ICollaborationGalleryRepository collaborationGalleryRepository)
    {
        _collaborationGalleryRepository = collaborationGalleryRepository;
    }

    public static CollaborationGalleryService Create()
    {
        return new CollaborationGalleryService(new CollaborationGalleryRepository());
    }

    public List<Collaboration> GetCollaborationCollection(int pageSize, int offset)
    {
        List<Collaboration> list = _collaborationGalleryRepository.GetCollaborationCollection(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCollaborationCount()
    {
        return _collaborationGalleryRepository.GetCollaborationCount();
    }

    public void InsertCollaborationsGallery(string Id)
    {
        ICollaborationRepository _repository = new CollaborationRepository();
        CollaborationService _service = new CollaborationService(_repository);
        _collaborationGalleryRepository.InsertCollaborationsGallery(Id, _service.GetCollaborationsById(Id));
    }

    public void UpdateStatusCollaborationsGallery(string Id)
    {
        _collaborationGalleryRepository.UpdateStatusCollaborationsGallery(Id);
    }

    public Collaboration SumPowerCollaborationsGallery()
    {
        return _collaborationGalleryRepository.SumPowerCollaborationsGallery();
    }
}
