using System.Collections.Generic;

public interface ISymbolsGalleryRepository
{
    List<Symbols> GetSymbolsCollection(string type, int pageSize, int offset, string rare);
    int GetSymbolsCount(string type, string rare);
    void InsertSymbolsGallery(string Id, Symbols SymbolFromDB);
    void UpdateStatusSymbolsGallery(string Id);
    Symbols SumPowerSymbolsGallery();
}