using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryItemUI _itemUIPrefab;
    [SerializeField] private Transform _itemContainer;
    private InventoryInstance _inventory;
    private ItemSO _tradingCurrency;
    private InventoryInstance _tradingTargetInventory;
    public bool TradingEnabled { get; private set; }

    public void Initialize(InventoryInstance inventory)
    {
        _inventory = inventory;
        _inventory.InventoryUpdated += UpdateInventoryUI;
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        foreach (Transform child in _itemContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in _inventory.ItemToQuantity)
        {
            var itemUI = Instantiate(_itemUIPrefab, _itemContainer);
            itemUI.Initialize(this, item.Key, item.Value);
        }
    }

    public void SetTradingTarget(ItemSO tradingCurrency, InventoryInstance targetInventory)
    {
        _tradingCurrency = tradingCurrency;
        _tradingTargetInventory = targetInventory;
        TradingEnabled = true;
        UpdateInventoryUI();
    }

    public void AttemptToSellItem(ItemSO item, int quantity)
    {
        int totalCost = item.Value * quantity;

        bool hasItemAvailable = _inventory.GetQuantity(item) >= quantity;
        if (!hasItemAvailable)
        {
            Debug.LogWarning($"Not enough {item.DisplayName} in inventory to sell {quantity}.");
            return;
        }
        bool canAffordItems = _tradingTargetInventory.GetQuantity(_tradingCurrency) >= totalCost;
        if (!canAffordItems)
        {
            Debug.LogWarning($"Not enough {_tradingCurrency.DisplayName} to buy {quantity} {item.DisplayName}.");
            return;
        }

        // Transfer items
        _inventory.RemoveItem(item, quantity);
        _tradingTargetInventory.AddItem(item, quantity);

        // Transfer currency
        _inventory.AddItem(_tradingCurrency, totalCost);
        _tradingTargetInventory.RemoveItem(_tradingCurrency, totalCost);
    }
}