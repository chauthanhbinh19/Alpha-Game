using System.Collections.Generic;

public interface ICollaborationGalleryRepository
{
    List<Collaboration> GetCollaborationCollection(int pageSize, int offset);
    int GetCollaborationCount();
    void InsertCollaborationsGallery(string Id, Collaboration collaborationFromDB);
    void UpdateStatusCollaborationsGallery(string Id);
    Collaboration SumPowerCollaborationsGallery();
}