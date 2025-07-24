using System.Collections.Generic;

public interface IBordersGalleryRepository
{
    List<Borders> GetBordersCollection(int pageSize, int offset, string rare);
    int GetBordersCount(string rare);
    void InsertBordersGallery(string Id, Borders BorderFromDB);
    void UpdateStatusBordersGallery(string Id);
    Borders SumPowerBordersGallery();
}