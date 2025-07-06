using UnityEngine;

public interface IInventoryTrader
{
    string TradeText { get; }
    ItemSO TradeCurrency { get; }

    void AttemptToSellItem(InventoryInstance sellerInventory, ItemSO item, int quantity);
}