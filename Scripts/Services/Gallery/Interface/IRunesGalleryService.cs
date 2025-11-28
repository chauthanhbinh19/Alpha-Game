using System.Collections.Generic;

public interface IRunesGalleryService
{
    List<Runes> GetRunesCollection(int pageSize, int offset, string rare);
    int GetRunesCount(string rare);
    void InsertRunesGallery(string Id);
    void UpdateStatusRunesGallery(string Id);
    void UpdateStarRunesGallery(string Id, double star);
    void UpdateRunesGalleryPower(string Id);
    Runes SumPowerRunesGallery();
}