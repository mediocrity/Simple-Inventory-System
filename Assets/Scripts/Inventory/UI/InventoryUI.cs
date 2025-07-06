using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryItemUI _itemUIPrefab;
    [SerializeField] private Transform _itemContainer;

    private IObservableInventory _inventory;
    private IInventoryTransactionHandler _transactionHandler;

    private Dictionary<ItemSO, InventoryItemUI> _itemUIMap = new Dictionary<ItemSO, InventoryItemUI>();
    private bool _requiresUpdate;

    private void Awake()
    {
        foreach (Transform child in _itemContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void Initialize(IObservableInventory inventory, IInventoryTransactionHandler transactionHandler = null)
    {
        _inventory = inventory;
        _transactionHandler = transactionHandler;
        _inventory.InventoryUpdated += TriggerUIUpdate;
        UpdateInventoryUI();
    }

    public void TriggerUIUpdate()
    {
        _requiresUpdate = true;
    }

    private void Update()
    {
        // Only update once per frame
        if (_requiresUpdate)
        {
            _requiresUpdate = false;
            UpdateInventoryUI();
        }
    }

    public void UpdateInventoryUI()
    {
        InitializeItemsUI();
        UpdateUISorting();
    }

    private void InitializeItemsUI()
    {
        foreach (var item in _inventory.Items)
        {
            InventoryItemUI itemUI;
            if (_itemUIMap.ContainsKey(item))
            {
                itemUI = _itemUIMap[item];
            }
            else
            {
                itemUI = Instantiate(_itemUIPrefab, _itemContainer);
                _itemUIMap[item] = itemUI;
            }
            itemUI.UpdateUI(_inventory, _transactionHandler, item);
        }
    }

    private void UpdateUISorting()
    {
        foreach (var item in _itemUIMap
            .OrderByDescending(pair => pair.Key.SortPriority)
            .ThenByDescending(pair => pair.Key.Value))
        {
            item.Value.transform.SetAsLastSibling();
        }
    }
}