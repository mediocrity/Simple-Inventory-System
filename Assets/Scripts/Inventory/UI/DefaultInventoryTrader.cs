using UnityEngine;

public class DefaultInventoryTrader : IInventoryTrader
{
    private InventoryInstance _buyerInventory;
    public ItemSO TradeCurrency { get; private set; }
    public string TradeText { get; private set; }

    public DefaultInventoryTrader(InventoryInstance traderInventory, ItemSO tradingCurrency, string tradeText)
    {
        _buyerInventory = traderInventory;
        TradeCurrency = tradingCurrency;
        TradeText = tradeText;
    }

    public bool CanSellToTrader(InventoryInstance sellerInventory, ItemSO item, int quantity)
    {
        int totalCost = item.Value * quantity;

        bool hasItemAvailable = sellerInventory.GetQuantity(item) >= quantity;
        if (!hasItemAvailable)
        {
            return false;
        }
        bool canAffordItems = _buyerInventory.GetQuantity(TradeCurrency) >= totalCost;
        if (!canAffordItems)
        {
            return false;
        }

        return true;
    }

    public void AttemptToSellToTrader(InventoryInstance sellerInventory, ItemSO item, int quantity)
    {
        int totalCost = item.Value * quantity;

        if (!CanSellToTrader(sellerInventory, item, quantity))
        {
            Debug.LogWarning($"Cannot sell {quantity} of {item.DisplayName}. Not enough items or currency.");
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