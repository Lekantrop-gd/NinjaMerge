using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private int _effectiveValue;
    [SerializeField] private ItemType _itemType;

    public enum ItemType
    {
        Sword,
        Armor
    }
}