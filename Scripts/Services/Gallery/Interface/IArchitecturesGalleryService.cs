using System.Collections.Generic;

public interface IArchitecturesGalleryService
{
    List<Architectures> GetArchitecturesCollection(int pageSize, int offset, string rare);
    int GetArchitecturesCount(string rare);
    void InsertArchitecturesGallery(string Id);
    void UpdateStatusArchitecturesGallery(string Id);
    void UpdateStarArchitecturesGallery(string Id, double star);
    void UpdateArchitecturesGalleryPower(string Id);
    Architectures SumPowerArchitecturesGallery();
}