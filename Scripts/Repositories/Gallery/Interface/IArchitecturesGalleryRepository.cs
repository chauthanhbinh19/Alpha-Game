using System.Collections.Generic;

public interface IArchitecturesGalleryRepository
{
    List<Architectures> GetArchitecturesCollection(int pageSize, int offset, string rare);
    int GetArchitecturesCount(string rare);
    void InsertArchitecturesGallery(string Id, Architectures TitleFromDB);
    void UpdateStatusArchitecturesGallery(string Id);
    void UpdateStarArchitecturesGallery(string Id, double star);
    void UpdateArchitecturesGalleryPower(string Id, Architectures TitleFromDB);
    Architectures SumPowerArchitecturesGallery();
}