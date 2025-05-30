using System.Collections.Generic;

public interface ICollaborationGalleryService
{
    List<Collaboration> GetCollaborationCollection(int pageSize, int offset);
    int GetCollaborationCount();
    void InsertCollaborationsGallery(string Id);
    void UpdateStatusCollaborationsGallery(string Id);
    Collaboration SumPowerCollaborationsGallery();
}