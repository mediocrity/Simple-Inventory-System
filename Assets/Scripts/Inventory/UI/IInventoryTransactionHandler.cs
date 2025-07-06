public interface IInventoryTransactionHandler
{
    string TransactionText { get; }
    ItemSO TransactionCurrency { get; }

    public bool CanTransact(ItemSO item, int quantity);

    void AttemptToTransact(ItemSO item, int quantity);

    int GetValue(ItemSO item, int quantity);
    int GetValue(ItemSO so, object tranactionQuantity);
}