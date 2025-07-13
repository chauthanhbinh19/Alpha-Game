using System.Collections.Generic;

public interface IItemsService
{
    List<string> GetUniqueItemId();
    List<Items> GetItems();
}
