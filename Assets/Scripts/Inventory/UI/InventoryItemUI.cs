using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMPro.TMP_Text _name;
    [SerializeField] private TMPro.TMP_Text _description;
    [SerializeField] private Image _valueIcon = null;
    [SerializeField] private TMPro.TMP_Text _value;
    [SerializeField] private TMPro.TMP_Text _quantity;
    [SerializeField] private Button _tradeButton;
    [SerializeField] private TMP_Text _tradeButtonText;
    [SerializeField] private LayoutElement _layoutElement;

    private InventoryInstance _inventory;
    private IInventoryTrader _trader;
    private ItemSO _itemSO;
    private int _previousQuantity;
    private RectTransform _rt;
    private Vector3 _defautlSize;

    private const float _animationTransferDelay = 0.25f;
    private const float _animationDuration = 0.7f;

    private void Awake()
    {
        _rt = transform as RectTransform;
        _defautlSize = _rt.sizeDelta;
        _layoutElement.minHeight = 0;
        transform.transform.localScale = Vector3.zero;
    }

    public void Initialize(InventoryInstance inventory, IInventoryTrader trader, ItemSO so)
    {
        int quantity = inventory.GetQuantity(so);
        HandleAnimation(quantity);

        _inventory = inventory;
        _trader = trader;

        _itemSO = so;
        _previousQuantity = quantity;

        _icon.sprite = so.Icon;
        _name.SetText(so.DisplayName);
        _description.SetText(so.Description);
        _quantity.SetText($"x{quantity}");

        bool canTrade = trader != null && so.IsTradable;
        _value.SetText($"{so.Value}");
        _value.gameObject.SetActive(canTrade);
        _valueIcon.gameObject.SetActive(canTrade);
        _valueIcon.sprite = canTrade ? _trader.TradeCurrency.Icon : null;
        _tradeButtonText.SetText(canTrade ? trader.TradeText : "?");
        _tradeButton.gameObject.SetActive(canTrade);

        _tradeButton.interactable = canTrade;
    }

    private void HandleAnimation(int quantity)
    {
        bool wasActive = _itemSO && _previousQuantity > 0;

        if (wasActive)
        {
            if (quantity == 0)
            {
                AnimateOut();
            }
            else if (quantity > _previousQuantity)
            {
                AnimateQuantityIncrease();
            }
            else if (quantity < _previousQuantity)
            {
                AnimateQuantityDecrease();
            }
        }
        else
        {
            if (quantity > 0)
            {
                AnimateIn();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void AnimateIn()
    {
        transform.SetAsLastSibling();
        gameObject.SetActive(true);
        _layoutElement.DOKill();
        _layoutElement.minHeight = 0;
        _layoutElement.DOMinSize(_defautlSize, _animationDuration).SetDelay(_animationTransferDelay).SetEase(Ease.OutBack);

        transform.DOKill();
        transform.localScale = new Vector3(1, 0, 1);
        transform.DOScaleY(1, _animationDuration).SetDelay(_animationTransferDelay).SetEase(Ease.OutBack)
            .OnComplete(() => _rt.sizeDelta = _defautlSize);
    }

    public void AnimateOut()
    {
        _layoutElement.DOKill();
        _layoutElement.DOMinSize(new Vector2(_defautlSize.x, 0), _animationDuration).SetEase(Ease.InBack)
            .OnComplete(() => gameObject.SetActive(false));

        transform.DOKill();
        transform.DOScaleY(0, _animationDuration).SetEase(Ease.InBack)
            .OnComplete(() => _rt.sizeDelta = new Vector2(_rt.sizeDelta.x, 0));
    }

    public void AnimateQuantityIncrease()
    {
        _icon.transform.DOKill();
        _icon.transform.localScale = Vector2.one;
        _icon.transform.DOPunchScale(transform.localScale * 0.1f, _animationDuration, 0, 1).SetDelay(_animationTransferDelay)
        .OnComplete(() => _icon.transform.localScale = Vector3.one);
    }

    public void AnimateQuantityDecrease()
    {
        _icon.transform.DOKill();
        _icon.transform.DOPunchScale(transform.localScale * -0.1f, _animationDuration, 0, 1)
            .OnComplete(() => _icon.transform.localScale = Vector3.one);
    }

    public void PressedBuyButton()
    {
        _trader.AttemptToSellItem(_inventory, _itemSO, 1);
    }
}