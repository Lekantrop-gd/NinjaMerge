using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private int _effectiveValue;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private Item _nextItem;

    public enum ItemType
    {
        Sword,
        Armor
    }
}