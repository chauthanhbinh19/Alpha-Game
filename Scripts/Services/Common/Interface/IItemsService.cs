using System.Collections.Generic;

public interface IItemsService
{
    List<string> GetUniqueItemTypes();
    List<string> GetUniqueItemId();
    List<Items> GetItems();
}
