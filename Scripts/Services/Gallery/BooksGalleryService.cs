using System.Collections.Generic;

public class BooksGalleryService : IBooksGalleryService
{
    private readonly IBooksGalleryRepository _booksGalleryRepository;

    public BooksGalleryService(IBooksGalleryRepository booksGalleryRepository)
    {
        _booksGalleryRepository = booksGalleryRepository;
    }

    public static BooksGalleryService Create()
    {
        return new BooksGalleryService(new BooksGalleryRepository());
    }

    public List<Books> GetBooksCollection(string type, int pageSize, int offset)
    {
        List<Books> list = _booksGalleryRepository.GetBooksCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetBooksCount(string type)
    {
        return _booksGalleryRepository.GetBooksCount(type);
    }

    public void InsertBooksGallery(string Id)
    {
        IBooksRepository _repository = new BooksRepository();
        BooksService _service = new BooksService(_repository);
        _booksGalleryRepository.InsertBooksGallery(Id, _service.GetBooksById(Id));
    }

    public void UpdateStatusBooksGallery(string Id)
    {
        _booksGalleryRepository.UpdateStatusBooksGallery(Id);
    }

    public Books SumPowerBooksGallery()
    {
        return _booksGalleryRepository.SumPowerBooksGallery();
    }
}