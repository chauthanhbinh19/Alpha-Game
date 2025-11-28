using System.Collections.Generic;

public interface IBadgesGalleryRepository
{
    List<Badges> GetBadgesCollection(int pageSize, int offset, string rare);
    int GetBadgesCount(string rare);
    void InsertBadgesGallery(string Id, Badges TitleFromDB);
    void UpdateStatusBadgesGallery(string Id);
    void UpdateStarBadgesGallery(string Id, double star);
    void UpdateBadgesGalleryPower(string Id, Badges TitleFromDB);
    Badges SumPowerBadgesGallery();
}