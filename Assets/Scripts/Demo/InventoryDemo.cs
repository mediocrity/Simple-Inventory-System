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
        var _shopInventoryInstance = new InventoryInstance(_shopInventorySO);
        var _playerInventoryInstance = new InventoryInstance(_playerInventorySO);

        // Create trader instances
        var playerTrader = new DefaultInventoryTrader(_playerInventoryInstance, _tradingCurrency, "Buy");
        var shopTrader = new DefaultInventoryTrader(_shopInventoryInstance, _tradingCurrency, "Sell");

        // Initialize the inventory UIs
        _shopInventoryUI.Initialize(_shopInventoryInstance, playerTrader);
        _playerInventoryUI.Initialize(_playerInventoryInstance, shopTrader);
    }
}