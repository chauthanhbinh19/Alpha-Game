using System.Collections.Generic;

public interface ITitlesGalleryService
{
    List<Titles> GetTitlesCollection(int pageSize, int offset);
    int GetTitlesCount();
    void InsertTitlesGallery(string Id);
    void UpdateStatusTitlesGallery(string Id);
    Titles SumPowerTitlesGallery();
}