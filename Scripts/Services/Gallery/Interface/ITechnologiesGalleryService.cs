using System.Collections.Generic;

public interface ITechnologiesGalleryService
{
    List<Technologies> GetTechnologiesCollection(int pageSize, int offset, string rare);
    int GetTechnologiesCount(string rare);
    void InsertTechnologiesGallery(string Id);
    void UpdateStatusTechnologiesGallery(string Id);
    void UpdateStarTechnologiesGallery(string Id, double star);
    void UpdateTechnologiesGalleryPower(string Id);
    Technologies SumPowerTechnologiesGallery();
}