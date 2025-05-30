using System.Collections.Generic;

public interface IBooksGalleryService
{
    List<Books> GetBooksCollection(string type, int pageSize, int offset);
    int GetBooksCount(string type);
    void InsertBooksGallery(string Id);
    void UpdateStatusBooksGallery(string Id);
    Books SumPowerBooksGallery();
}