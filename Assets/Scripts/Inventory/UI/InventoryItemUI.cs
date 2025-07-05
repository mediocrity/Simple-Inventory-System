using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMPro.TMP_Text _name;
    [SerializeField] private TMPro.TMP_Text _description;
    [SerializeField] private TMPro.TMP_Text _value;
    [SerializeField] private TMPro.TMP_Text _quantity;
    [SerializeField] private Button _buyButton;

    private ItemSO _itemSO;
    private int _quantityValue;
    private InventoryUI _inventoryUI;

    public void Initialize(InventoryUI inventoryUI, ItemSO so, int quantity)
    {
        _itemSO = so;
        _quantityValue = quantity;

        _inventoryUI = inventoryUI;
        _icon.sprite = so.Icon;
        _name.SetText(so.DisplayName);
        _description.SetText(so.Description);
        _value.SetText($"{so.Value}");
        _quantity.SetText($"x{quantity}");

        _buyButton.gameObject.SetActive(_itemSO.IsTradable);

        _buyButton.interactable = inventoryUI.TradingEnabled;
    }

    public void PressedBuyButton()
    {
        _inventoryUI.AttemptToSellItem(_itemSO, _quantityValue);
    }
}