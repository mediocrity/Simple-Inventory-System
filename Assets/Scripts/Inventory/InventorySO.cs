using UnityEngine;

[CreateAssetMenu(fileName = "InventorySO", menuName = "ScriptableObjects/InventorySO")]
public class InventorySO : ScriptableObject
{
    public float ValueModifier => _valueModifier;

    [SerializeField] private ItemDefeinition[] _initialItems;
    [SerializeField] private float _valueModifier = 1f;

    public ItemDefeinition[] InitialItems => _initialItems;

    [System.Serializable]
    public class ItemDefeinition
    {
        public ItemSO Item;
        public int Quantity;
    }
}