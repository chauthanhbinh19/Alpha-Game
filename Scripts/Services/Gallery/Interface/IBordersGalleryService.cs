using System.Collections.Generic;

public interface IBordersGalleryService
{
    List<Borders> GetBordersCollection(int pageSize, int offset, string rare);
    int GetBordersCount(string rare);
    void InsertBordersGallery(string Id);
    void UpdateStatusBordersGallery(string Id);
    Borders SumPowerBordersGallery();
}