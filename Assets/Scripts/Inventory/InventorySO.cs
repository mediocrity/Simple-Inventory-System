using UnityEngine;

[CreateAssetMenu(fileName = "InventorySO", menuName = "ScriptableObjects/InventorySO")]
public class InventorySO : ScriptableObject
{
    [SerializeField] private ItemDefeinition[] _initialItems;

    public ItemDefeinition[] InitialItems => _initialItems;

    [System.Serializable]
    public class ItemDefeinition
    {
        public ItemSO Item;
        public int Quantity;
    }
}