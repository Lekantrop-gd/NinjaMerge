using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New Sword", menuName = "Items/Sword")]
    public class Sword : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _name;
        [SerializeField] private int _damage;

        public Sprite Sprite { get { return _sprite; } }
        public string Name { get { return _name; } }
        public int Damage { get { return _damage; } }
    }
}