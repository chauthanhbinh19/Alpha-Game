using System.Collections.Generic;

public class SymbolsGalleryService : ISymbolsGalleryService
{
    private readonly ISymbolsGalleryRepository _symbolsGalleryRepository;

    public SymbolsGalleryService(ISymbolsGalleryRepository symbolsGalleryRepository)
    {
        _symbolsGalleryRepository = symbolsGalleryRepository;
    }

    public static SymbolsGalleryService Create()
    {
        return new SymbolsGalleryService(new SymbolsGalleryRepository());
    }

    public List<Symbols> GetSymbolsCollection(string type, int pageSize, int offset)
    {
        List<Symbols> list = _symbolsGalleryRepository.GetSymbolsCollection(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetSymbolsCount(string type)
    {
        return _symbolsGalleryRepository.GetSymbolsCount(type);
    }

    public void InsertSymbolsGallery(string Id)
    {
        ISymbolsRepository _repository = new SymbolsRepository();
        SymbolsService _service = new SymbolsService(_repository);
        _symbolsGalleryRepository.InsertSymbolsGallery(Id, _service.GetSymbolsById(Id));
    }

    public void UpdateStatusSymbolsGallery(string Id)
    {
        _symbolsGalleryRepository.UpdateStatusSymbolsGallery(Id);
    }

    public Symbols SumPowerSymbolsGallery()
    {
        return _symbolsGalleryRepository.SumPowerSymbolsGallery();
    }
}
