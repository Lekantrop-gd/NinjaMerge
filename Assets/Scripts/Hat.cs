using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New Hat", menuName = "Items/Hat")]
    public class Hat : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _name;
        [SerializeField] private int _damageDecrease;

        public Sprite Sprite { get { return _sprite; } }
        public string Name { get { return _name; } }
        public int DamageDecrease { get { return _damageDecrease; } }
    }
}