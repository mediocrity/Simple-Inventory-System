using System.Collections.Generic;

public class InventoryInstance
{
    private Dictionary<ItemSO, int> _items = new Dictionary<ItemSO, int>();

    public event System.Action InventoryUpdated;

    public IReadOnlyDictionary<ItemSO, int> ItemToQuantity => _items;

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
            _items[item] -= quantity;
            if (_items[item] <= 0)
            {
                _items.Remove(item);
            }
            InventoryUpdated?.Invoke();
        }
    }
}