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
        _inventory.InventoryUpdated += RequiresUpdate;
        UpdateInventoryUI();
    }

    public void RequiresUpdate()
    {
        _requiresUpdate = true;
    }

    private void Update()
    {
        if (_requiresUpdate)
        {
            // Only update once per frame
            _requiresUpdate = false;
            UpdateInventoryUI();
        }
    }

    public void UpdateInventoryUI()
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
            itemUI.Initialize(_inventory, _trader, item);
        }

        foreach (var item in _itemUIMap
            .OrderByDescending(pair => pair.Key.SortPriority)
            .ThenByDescending(pair => pair.Key.Value))
        {
            item.Value.transform.SetAsLastSibling();
        }
    }
}