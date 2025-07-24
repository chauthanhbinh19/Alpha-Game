using System.Collections.Generic;

public interface ICollaborationGalleryService
{
    List<Collaboration> GetCollaborationCollection(int pageSize, int offset, string rare);
    int GetCollaborationCount(string rare);
    void InsertCollaborationsGallery(string Id);
    void UpdateStatusCollaborationsGallery(string Id);
    Collaboration SumPowerCollaborationsGallery();
}