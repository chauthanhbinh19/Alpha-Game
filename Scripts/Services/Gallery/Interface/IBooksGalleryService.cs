using System.Collections.Generic;

public interface IBooksGalleryService
{
    List<Books> GetBooksCollection(string type, int pageSize, int offset, string rare);
    int GetBooksCount(string type, string rare);
    void InsertBooksGallery(string Id);
    void UpdateStatusBooksGallery(string Id);
    Books SumPowerBooksGallery();
}