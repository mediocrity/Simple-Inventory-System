using UnityEngine;

public class TradeTransactionHandler : IInventoryTransactionHandler
{
    private InventoryInstance _sellerInventory;
    private InventoryInstance _buyerInventory;
    public ItemSO TransactionCurrency { get; private set; }
    public string TransactionText { get; private set; }

    public TradeTransactionHandler(InventoryInstance sellerInventory, InventoryInstance buyerInventory, ItemSO transactionCurrency, string transactionText)
    {
        _sellerInventory = sellerInventory;
        _buyerInventory = buyerInventory;
        TransactionCurrency = transactionCurrency;
        TransactionText = transactionText;
    }

    public bool CanTransact(ItemSO item, int quantity)
    {
        int totalCost = GetValue(item, quantity);

        bool hasItemAvailable = _sellerInventory.GetQuantity(item) >= quantity;
        if (!hasItemAvailable)
        {
            return false;
        }
        bool canAffordItems = _buyerInventory.GetQuantity(TransactionCurrency) >= totalCost;
        if (!canAffordItems)
        {
            return false;
        }

        return true;
    }

    public void AttemptToTransact(ItemSO item, int quantity)
    {
        int totalCost = GetValue(item, quantity);

        if (!CanTransact(item, quantity))
        {
            Debug.LogWarning($"Cannot complete transaction for {quantity} of {item.DisplayName}. Not enough items or currency.");
            return;
        }

        // Transfer items
        _sellerInventory.RemoveItem(item, quantity);
        _buyerInventory.AddItem(item, quantity);

        // Transfer currency
        _sellerInventory.AddItem(TransactionCurrency, totalCost);
        _buyerInventory.RemoveItem(TransactionCurrency, totalCost);
    }

    public int GetValue(ItemSO item, int quantity)
    {
        return Mathf.CeilToInt(item.Value * quantity * _sellerInventory.InventorySO.ValueModifier);
    }
}