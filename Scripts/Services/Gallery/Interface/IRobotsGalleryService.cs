using System.Collections.Generic;

public interface IRobotsGalleryService
{
    List<Robots> GetRobotsCollection(int pageSize, int offset, string rare);
    int GetRobotsCount(string rare);
    void InsertRobotsGallery(string Id);
    void UpdateStatusRobotsGallery(string Id);
    void UpdateStarRobotsGallery(string Id, double star);
    void UpdateRobotsGalleryPower(string Id);
    Robots SumPowerRobotsGallery();
}