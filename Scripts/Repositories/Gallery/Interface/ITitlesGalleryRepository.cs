using System.Collections.Generic;

public interface ITitlesGalleryRepository
{
    List<Titles> GetTitlesCollection(int pageSize, int offset);
    int GetTitlesCount();
    void InsertTitlesGallery(string Id, Titles TitleFromDB);
    void UpdateStatusTitlesGallery(string Id);
    Titles SumPowerTitlesGallery();
}