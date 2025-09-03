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

    public List<Collaboration> GetCollaborationCollection(int pageSize, int offset, string rare)
    {
        List<Collaboration> list = _collaborationGalleryRepository.GetCollaborationCollection(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCollaborationCount(string rare)
    {
        return _collaborationGalleryRepository.GetCollaborationCount(rare);
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

    public void UpdateStarCollaborationsGallery(string Id, double star)
    {
        _collaborationGalleryRepository.UpdateStarCollaborationsGallery(Id, star);
    }

    public void UpdateCollaborationsGalleryPower(string Id)
    {
        ICollaborationRepository _repository = new CollaborationRepository();
        CollaborationService _service = new CollaborationService(_repository);
        _collaborationGalleryRepository.UpdateCollaborationsGalleryPower(Id, _service.GetCollaborationsById(Id));
    }
}
