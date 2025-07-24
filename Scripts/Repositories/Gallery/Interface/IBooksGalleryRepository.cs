using System.Collections.Generic;

public interface IBooksGalleryRepository
{
    List<Books> GetBooksCollection(string type, int pageSize, int offset, string rare);
    int GetBooksCount(string type, string rare);
    void InsertBooksGallery(string Id, Books BookFromDB);
    void UpdateStatusBooksGallery(string Id);
    Books SumPowerBooksGallery();
}