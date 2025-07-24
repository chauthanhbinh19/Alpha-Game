using System.Collections.Generic;

public interface ITitlesGalleryService
{
    List<Titles> GetTitlesCollection(int pageSize, int offset, string rare);
    int GetTitlesCount(string rare);
    void InsertTitlesGallery(string Id);
    void UpdateStatusTitlesGallery(string Id);
    Titles SumPowerTitlesGallery();
}