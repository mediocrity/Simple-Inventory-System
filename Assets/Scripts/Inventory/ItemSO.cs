using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/ItemSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private string _displayName = null;
    [SerializeField] private string _description = null;
    [SerializeField] private Sprite _icon = null;
    [SerializeField] private int _value = 1;
    [SerializeField] private bool _isTradable = true;

    [Tooltip("Higher values will sort to the top in lists.")]
    [SerializeField] private int _sortPriority = 0;

    public string DisplayName => _displayName;
    public string Description => _description;
    public Sprite Icon => _icon;
    public int Value => _value;

    public bool IsTradable => _isTradable;
    public int SortPriority => _sortPriority;
}