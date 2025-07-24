using System.Collections.Generic;

public interface ISymbolsGalleryService
{
    List<Symbols> GetSymbolsCollection(string type, int pageSize, int offset, string rare);
    int GetSymbolsCount(string type, string rare);
    void InsertSymbolsGallery(string Id);
    void UpdateStatusSymbolsGallery(string Id);
    Symbols SumPowerSymbolsGallery();
}