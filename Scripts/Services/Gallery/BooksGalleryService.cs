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

    public List<Books> GetBooksCollection(string type, int pageSize, int offset, string rare)
    {
        List<Books> list = _booksGalleryRepository.GetBooksCollection(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetBooksCount(string type, string rare)
    {
        return _booksGalleryRepository.GetBooksCount(type, rare);
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

    public void UpdateStarBooksGallery(string Id, double star)
    {
        _booksGalleryRepository.UpdateStarBooksGallery(Id, star);
    }

    public void UpdateBooksGalleryPower(string Id)
    {
        IBooksRepository _repository = new BooksRepository();
        BooksService _service = new BooksService(_repository);
        _booksGalleryRepository.UpdateBooksGalleryPower(Id, _service.GetBooksById(Id));
    }
}