using System.Collections.Generic;

public interface ITitlesGalleryRepository
{
    List<Architectures> GetTitlesCollection(int pageSize, int offset, string rare);
    int GetTitlesCount(string rare);
    void InsertTitlesGallery(string Id, Architectures TitleFromDB);
    void UpdateStatusTitlesGallery(string Id);
    void UpdateStarTitlesGallery(string Id, double star);
    void UpdateTitlesGalleryPower(string Id, Architectures TitleFromDB);
    Architectures SumPowerTitlesGallery();
}