using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMPro.TMP_Text _name;
    [SerializeField] private TMPro.TMP_Text _description;
    [SerializeField] private TMPro.TMP_Text _value;
    [SerializeField] private TMPro.TMP_Text _quantity;

    public void Initialize(ItemSO so, int quantity)
    {
        _icon.sprite = so.Icon;
        _name.SetText(so.DisplayName);
        _description.SetText(so.Description);
        _value.SetText($"{so.Value}");
        _quantity.SetText($"x{quantity}");
    }
}