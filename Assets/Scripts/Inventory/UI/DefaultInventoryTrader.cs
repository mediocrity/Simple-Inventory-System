using UnityEngine;

public class DefaultInventoryTrader : IInventoryTrader
{
    private ItemSO _tradingCurrency;
    private InventoryInstance _buyerInventory;

    public DefaultInventoryTrader(ItemSO tradingCurrency, InventoryInstance buyerInventory)
    {
        _tradingCurrency = tradingCurrency;
        _buyerInventory = buyerInventory;
    }

    public void AttemptToSellItem(InventoryInstance sellerInventory, ItemSO item, int quantity)
    {
        int totalCost = item.Value * quantity;

        bool hasItemAvailable = sellerInventory.GetQuantity(item) >= quantity;
        if (!hasItemAvailable)
        {
            Debug.LogWarning($"Not enough {item.DisplayName} in inventory to sell {quantity}.");
            return;
        }
        bool canAffordItems = _buyerInventory.GetQuantity(_tradingCurrency) >= totalCost;
        if (!canAffordItems)
        {
            Debug.LogWarning($"Not enough {_tradingCurrency.DisplayName} to buy {quantity} {item.DisplayName}.");
            return;
        }

        // Transfer items
        sellerInventory.RemoveItem(item, quantity);
        _buyerInventory.AddItem(item, quantity);

        // Transfer currency
        sellerInventory.AddItem(_tradingCurrency, totalCost);
        _buyerInventory.RemoveItem(_tradingCurrency, totalCost);
    }
}
