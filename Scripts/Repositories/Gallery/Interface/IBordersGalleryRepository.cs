using System.Collections.Generic;

public interface IBordersGalleryRepository
{
    List<Borders> GetBordersCollection(int pageSize, int offset);
    int GetBordersCount();
    void InsertBordersGallery(string Id, Borders BorderFromDB);
    void UpdateStatusBordersGallery(string Id);
    Borders SumPowerBordersGallery();
}