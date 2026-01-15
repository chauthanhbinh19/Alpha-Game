using System;
using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    private readonly List<Items> _items = new();

    // ========= GET =========

    public Items GetById(string id)
        => _items.FirstOrDefault(i => i.Id == id);

    public Items GetByName(string name)
        => _items.FirstOrDefault(i =>
            i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

    public double GetQuantityById(string id)
        => GetById(id)?.Quantity ?? 0;

    public double GetQuantityByName(string name)
        => GetByName(name)?.Quantity ?? 0;

    // ========= ADD =========

    public void Add(string id, string name, double quantity)
    {
        var item = GetById(id);

        if (item == null)
        {
            _items.Add(new Items
            {
                Id = id,
                Name = name,
                Quantity = quantity
            });
        }
        else
        {
            item.Quantity += quantity;
        }
    }

    public void AddByName(string name, double quantity)
    {
        var item = GetByName(name);
        if (item == null)
            throw new Exception($"Item '{name}' not found");

        item.Quantity += quantity;
    }

    // ========= REMOVE =========

    public bool RemoveById(string id, double quantity)
    {
        var item = GetById(id);
        if (item == null || item.Quantity < quantity)
            return false;

        item.Quantity -= quantity;
        if (item.Quantity <= 0)
            _items.Remove(item);

        return true;
    }

    public bool RemoveByName(string name, double quantity)
    {
        var item = GetByName(name);
        if (item == null || item.Quantity < quantity)
            return false;

        item.Quantity -= quantity;
        if (item.Quantity <= 0)
            _items.Remove(item);

        return true;
    }

    public void Clear()
    {
        _items.Clear();
    }

    // ========= CLONE =========

    public Inventory Clone()
    {
        var inv = new Inventory();
        foreach (var i in _items)
        {
            inv.Add(i.Id, i.Name, i.Quantity);
        }
        return inv;
    }

    public IEnumerable<Items> All() => _items;
}
