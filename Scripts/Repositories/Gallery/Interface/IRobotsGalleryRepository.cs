using System.Collections.Generic;

public interface IRobotsGalleryRepository
{
    List<Robots> GetRobotsCollection(int pageSize, int offset, string rare);
    int GetRobotsCount(string rare);
    void InsertRobotsGallery(string Id, Robots TitleFromDB);
    void UpdateStatusRobotsGallery(string Id);
    void UpdateStarRobotsGallery(string Id, double star);
    void UpdateRobotsGalleryPower(string Id, Robots TitleFromDB);
    Robots SumPowerRobotsGallery();
}