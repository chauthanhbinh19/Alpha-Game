using System.Collections.Generic;

public interface IBordersGalleryRepository
{
    List<Borders> GetBordersCollection(int pageSize, int offset, string rare);
    int GetBordersCount(string rare);
    void InsertBordersGallery(string Id, Borders BorderFromDB);
    void UpdateStatusBordersGallery(string Id);
    void UpdateStarBordersGallery(string Id, double star);
    void UpdateBordersGalleryPower(string Id, Borders BorderFromDB);
    Borders SumPowerBordersGallery();
}