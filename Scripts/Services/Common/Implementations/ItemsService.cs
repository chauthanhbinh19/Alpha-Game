using System.Collections.Generic;
using System.Threading.Tasks;

public class ItemsService : IItemsService
{
    private readonly IItemsRepository _itemsRepository;

    public ItemsService(IItemsRepository itemsRepository)
    {
        _itemsRepository = itemsRepository;
    }

    public static IItemsService Create() => ServiceContainer.GetService<IItemsService>();

    public async Task<List<Items>> GetItemsAsync()
    {
        return await _itemsRepository.GetItemsAsync();
    }

    public async Task<List<string>> GetUniqueItemsTypesAsync()
    {
        return await _itemsRepository.GetUniqueItemsTypesAsync();
    }

    public async Task<List<string>> GetUniqueItemsIdAsync()
    {
        return await _itemsRepository.GetUniqueItemsIdAsync();
    }
}
