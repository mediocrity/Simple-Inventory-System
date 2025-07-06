using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryItemUI _itemUIPrefab;
    [SerializeField] private Transform _itemContainer;

    private InventoryInstance _inventory;
    private IInventoryTrader _trader;

    private Dictionary<ItemSO, InventoryItemUI> _itemUIMap = new Dictionary<ItemSO, InventoryItemUI>();
    private bool _requiresUpdate;

    private void Awake()
    {
        foreach (Transform child in _itemContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void Initialize(InventoryInstance inventory, IInventoryTrader trader = null)
    {
        _inventory = inventory;
        _trader = trader;
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
            itemUI.UpdateUI(_inventory, _trader, item);
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