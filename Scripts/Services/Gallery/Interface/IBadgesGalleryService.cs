using System.Collections.Generic;

public interface IBadgesGalleryService
{
    List<Badges> GetBadgesCollection(int pageSize, int offset, string rare);
    int GetBadgesCount(string rare);
    void InsertBadgesGallery(string Id);
    void UpdateStatusBadgesGallery(string Id);
    void UpdateStarBadgesGallery(string Id, double star);
    void UpdateBadgesGalleryPower(string Id);
    Badges SumPowerBadgesGallery();
}