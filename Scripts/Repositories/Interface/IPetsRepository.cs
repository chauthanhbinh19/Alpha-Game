using System.Collections.Generic;

public interface IPetsRepository
{
    List<string> GetUniquePetsTypes();
    List<Pets> GetPets(string type, int pageSize, int offset);
    int GetPetsCount(string type);
    List<Pets> GetPetsWithPrice(string type, int pageSize, int offset);
    int GetPetsWithPriceCount(string type);
    Pets GetPetsById(string Id);
}
