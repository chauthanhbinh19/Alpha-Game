using System.Collections.Generic;

public interface IBordersGalleryService
{
    List<Borders> GetBordersCollection(int pageSize, int offset);
    int GetBordersCount();
    void InsertBordersGallery(string Id);
    void UpdateStatusBordersGallery(string Id);
    Borders SumPowerBordersGallery();
}