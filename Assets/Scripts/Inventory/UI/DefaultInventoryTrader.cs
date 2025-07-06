using UnityEngine;

public class DefaultInventoryTrader : IInventoryTrader
{
    private InventoryInstance _buyerInventory;
    public ItemSO TradeCurrency { get; private set; }
    public string TradeText { get; private set; }

    public DefaultInventoryTrader(ItemSO tradingCurrency, InventoryInstance buyerInventory, string tradeText)
    {
        TradeCurrency = tradingCurrency;
        _buyerInventory = buyerInventory;
        TradeText = tradeText;
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
        bool canAffordItems = _buyerInventory.GetQuantity(TradeCurrency) >= totalCost;
        if (!canAffordItems)
        {
            Debug.LogWarning($"Not enough {TradeCurrency.DisplayName} to buy {quantity} {item.DisplayName}.");
            return;
        }

        // Transfer items
        sellerInventory.RemoveItem(item, quantity);
        _buyerInventory.AddItem(item, quantity);

        // Transfer currency
        sellerInventory.AddItem(TradeCurrency, totalCost);
        _buyerInventory.RemoveItem(TradeCurrency, totalCost);
    }
}