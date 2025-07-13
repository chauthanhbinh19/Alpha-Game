using System.Collections.Generic;

public class ItemsService : IItemsService
{
    private readonly IItemsRepository _itemsRepository;

    public ItemsService(IItemsRepository itemsRepository)
    {
        _itemsRepository = itemsRepository;
    }

    public static ItemsService Create()
    {
        return new ItemsService(new ItemsRepository());
    }

    public List<Items> GetItems()
    {
        return _itemsRepository.GetItems();
    }

    public List<string> GetUniqueItemId()
    {
        return _itemsRepository.GetUniqueItemId();
    }
}
