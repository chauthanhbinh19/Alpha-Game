using System.Collections.Generic;

public interface IBooksGalleryRepository
{
    List<Books> GetBooksCollection(string type, int pageSize, int offset);
    int GetBooksCount(string type);
    void InsertBooksGallery(string Id, Books BookFromDB);
    void UpdateStatusBooksGallery(string Id);
    Books SumPowerBooksGallery();
}