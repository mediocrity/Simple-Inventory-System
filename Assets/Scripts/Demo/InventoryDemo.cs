using UnityEngine;

public class InventoryDemo : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private InventorySO _shopInventorySO = null;
    [SerializeField] private InventorySO _playerInventorySO = null;


    [Header("UI References")]
    [SerializeField] private InventoryUI _shopInventoryUI = null;
    [SerializeField] private InventoryUI _playerInventoryUI = null;

    private void Start()
    {
        _shopInventoryUI.Initialize(new InventoryInstance(_shopInventorySO));
        _playerInventoryUI.Initialize(new InventoryInstance(_playerInventorySO));
    }
}