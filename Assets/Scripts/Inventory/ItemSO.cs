using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/ItemSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private string _displayName = null;
    [SerializeField] private string _description = null;
    [SerializeField] private Sprite _icon = null;
    [SerializeField] private int _value = 1;

    public string DisplayName => _displayName;
    public string Description => _description;
    public Sprite Icon => _icon;
    public int Value => _value;
}