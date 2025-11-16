using System.Collections.Generic;

public interface ITechnologiesGalleryRepository
{
    List<Technologies> GetTechnologiesCollection(int pageSize, int offset, string rare);
    int GetTechnologiesCount(string rare);
    void InsertTechnologiesGallery(string Id, Technologies TitleFromDB);
    void UpdateStatusTechnologiesGallery(string Id);
    void UpdateStarTechnologiesGallery(string Id, double star);
    void UpdateTechnologiesGalleryPower(string Id, Technologies TitleFromDB);
    Technologies SumPowerTechnologiesGallery();
}