using UnityEngine;

[CreateAssetMenu(fileName = "InventorySO", menuName = "ScriptableObjects/InventorySO")]
public class InventorySO : ScriptableObject
{
    [SerializeField] private ItemSO[] _items;

    public ItemSO[] Items => _items;
}