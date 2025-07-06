using UnityEngine;

public class InventoryDemo : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private ItemSO _tradingCurrency = null;

    [SerializeField] private InventorySO _shopInventorySO = null;
    [SerializeField] private InventorySO _playerInventorySO = null;

    [Header("UI References")]
    [SerializeField] private InventoryUI _shopInventoryUI = null;

    [SerializeField] private InventoryUI _playerInventoryUI = null;

    private void Start()
    {
        // Create inventory instances
        var shopInventoryInstance = new InventoryInstance(_shopInventorySO);
        var playerInventoryInstance = new InventoryInstance(_playerInventorySO);

        // Create transaction handlers
        var shopInventoryTransaction = new TradeTransactionHandler(shopInventoryInstance, playerInventoryInstance, _tradingCurrency, "Buy");
        var playerInventoryTransaction = new TradeTransactionHandler(playerInventoryInstance, shopInventoryInstance, _tradingCurrency, "Sell");

        // Initialize UI with inventory instances and transaction handlers
        _shopInventoryUI.Initialize(shopInventoryInstance, shopInventoryTransaction);
        _playerInventoryUI.Initialize(playerInventoryInstance, playerInventoryTransaction);
    }
}