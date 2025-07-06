using System.Collections.Generic;

public interface IObservableInventory
{
    IEnumerable<ItemSO> Items { get; }

    InventorySO InventorySO { get; }

    int GetQuantity(ItemSO item);

    event System.Action InventoryUpdated;
}