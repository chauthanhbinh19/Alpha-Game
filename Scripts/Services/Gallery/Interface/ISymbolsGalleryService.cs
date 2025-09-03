using System.Collections.Generic;

public interface ISymbolsGalleryService
{
    List<Symbols> GetSymbolsCollection(string type, int pageSize, int offset, string rare);
    int GetSymbolsCount(string type, string rare);
    void InsertSymbolsGallery(string Id);
    void UpdateStatusSymbolsGallery(string Id);
    void UpdateStarSymbolsGallery(string Id, double star);
    void UpdateSymbolsGalleryPower(string Id);
    Symbols SumPowerSymbolsGallery();
}