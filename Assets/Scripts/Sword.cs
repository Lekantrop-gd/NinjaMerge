using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New Sword", menuName = "Items/Sword")]
    public class Sword : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _name;
        [SerializeField] private int _damage;

        public Sprite Sprite { get; private set; }
        public string Name { get; private set; }
        public int Damage { get; private set; }
    }
}