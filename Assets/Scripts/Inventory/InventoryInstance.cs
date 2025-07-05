using System.Collections.Generic;
using UnityEngine;

public class InventoryInstance
{
    private List<ItemSO> _items = new List<ItemSO>();

    public event System.Action InventoryUpdated;

    public IReadOnlyList<ItemSO> Items => _items.AsReadOnly();

    public InventoryInstance(InventorySO inventorySO)
    {
        foreach (var item in inventorySO.Items)
        {
            _items.Add(item);
        }
    }

    public void AddItem(ItemSO item)
    {
        _items.Add(item);
        InventoryUpdated?.Invoke();
    }

    public void RemoveItem(ItemSO item)
    {
        if (_items.Contains(item))
        {
            _items.Remove(item);
            InventoryUpdated?.Invoke();
        }
    }
}