using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryItemUI _itemUIPrefab;
    [SerializeField] private Transform _itemContainer;
    private InventoryInstance _inventory;

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

        foreach (var item in _inventory.Items)
        {
            var itemUI = Instantiate(_itemUIPrefab, _itemContainer);
            itemUI.Initialize(item);
        }
    }
}