using UnityEngine;

public interface IInventoryTrader
{
    string TradeText { get; }
    ItemSO TradeCurrency { get; }

    public bool CanSellToTrader(InventoryInstance sellerInventory, ItemSO item, int quantity);

    void AttemptToSellToTrader(InventoryInstance sellerInventory, ItemSO item, int quantity);
}