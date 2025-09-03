using System.Collections.Generic;

public interface ITitlesGalleryRepository
{
    List<Titles> GetTitlesCollection(int pageSize, int offset, string rare);
    int GetTitlesCount(string rare);
    void InsertTitlesGallery(string Id, Titles TitleFromDB);
    void UpdateStatusTitlesGallery(string Id);
    void UpdateStarTitlesGallery(string Id, double star);
    void UpdateTitlesGalleryPower(string Id, Titles TitleFromDB);
    Titles SumPowerTitlesGallery();
}