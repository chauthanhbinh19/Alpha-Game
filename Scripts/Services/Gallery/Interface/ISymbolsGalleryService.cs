using System.Collections.Generic;

public interface ISymbolsGalleryService
{
    List<Symbols> GetSymbolsCollection(string type, int pageSize, int offset);
    int GetSymbolsCount(string type);
    void InsertSymbolsGallery(string Id);
    void UpdateStatusSymbolsGallery(string Id);
    Symbols SumPowerSymbolsGallery();
}