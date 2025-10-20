using System.Collections.Generic;

public interface ICollaborationGalleryRepository
{
    List<Collaborations> GetCollaborationCollection(int pageSize, int offset, string rare);
    int GetCollaborationCount(string rare);
    void InsertCollaborationsGallery(string Id, Collaborations collaborationFromDB);
    void UpdateStatusCollaborationsGallery(string Id);
    void UpdateStarCollaborationsGallery(string Id, double star);
    void UpdateCollaborationsGalleryPower(string Id, Collaborations CollaborationFromDB);
    Collaborations SumPowerCollaborationsGallery();
}