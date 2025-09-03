using System.Collections.Generic;

public interface ICollaborationGalleryRepository
{
    List<Collaboration> GetCollaborationCollection(int pageSize, int offset, string rare);
    int GetCollaborationCount(string rare);
    void InsertCollaborationsGallery(string Id, Collaboration collaborationFromDB);
    void UpdateStatusCollaborationsGallery(string Id);
    void UpdateStarCollaborationsGallery(string Id, double star);
    void UpdateCollaborationsGalleryPower(string Id, Collaboration CollaborationFromDB);
    Collaboration SumPowerCollaborationsGallery();
}