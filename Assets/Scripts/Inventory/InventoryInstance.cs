using System.Collections.Generic;
using UnityEngine;

public class InventoryInstance
{
    private Dictionary<ItemSO, int> _items = new();

    public event System.Action InventoryUpdated;

    public IEnumerable<ItemSO> Items => _items.Keys;

    public InventoryInstance(InventorySO inventorySO)
    {
        foreach (var item in inventorySO.InitialItems)
        {
            AddItem(item.Item, item.Quantity);
        }
    }

    public void AddItem(ItemSO item, int quantity)
    {
        if (_items.ContainsKey(item))
        {
            _items[item] += quantity;
        }
        else
        {
            _items.Add(item, quantity);
        }
        InventoryUpdated?.Invoke();
    }

    public void RemoveItem(ItemSO item, int quantity)
    {
        if (_items.ContainsKey(item))
        {
            _items[item] = Mathf.Max(0, _items[item] - quantity);
            InventoryUpdated?.Invoke();
        }
    }

    public int GetQuantity(ItemSO item)
    {
        return _items.GetValueOrDefault(item, 0);
    }
}