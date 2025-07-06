public interface IInventoryTrader
{
    void AttemptToSellItem(InventoryInstance sellerInventory, ItemSO item, int quantity);
}
