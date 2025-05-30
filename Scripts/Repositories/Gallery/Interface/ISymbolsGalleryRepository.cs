using System.Collections.Generic;

public interface ISymbolsGalleryRepository
{
    List<Symbols> GetSymbolsCollection(string type, int pageSize, int offset);
    int GetSymbolsCount(string type);
    void InsertSymbolsGallery(string Id, Symbols SymbolFromDB);
    void UpdateStatusSymbolsGallery(string Id);
    Symbols SumPowerSymbolsGallery();
}