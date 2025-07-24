using System.Collections.Generic;

public interface ITitlesGalleryRepository
{
    List<Titles> GetTitlesCollection(int pageSize, int offset, string rare);
    int GetTitlesCount(string rare);
    void InsertTitlesGallery(string Id, Titles TitleFromDB);
    void UpdateStatusTitlesGallery(string Id);
    Titles SumPowerTitlesGallery();
}