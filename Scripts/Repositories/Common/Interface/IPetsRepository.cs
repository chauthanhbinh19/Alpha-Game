using System.Collections.Generic;

public interface IPetsRepository
{
    List<string> GetUniquePetsTypes();
    List<string> GetUniquePetsId();
    List<Pets> GetPets(string type, int pageSize, int offset, string rare);
    int GetPetsCount(string type, string rare);
    List<Pets> GetPetsWithPrice(string type, int pageSize, int offset);
    int GetPetsWithPriceCount(string type);
    Pets GetPetsById(string Id);
}
