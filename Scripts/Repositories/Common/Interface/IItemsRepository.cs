using System.Collections.Generic;

public interface IItemsRepository
{
    List<string> GetUniqueItemId();
    List<Items> GetItems();
}
