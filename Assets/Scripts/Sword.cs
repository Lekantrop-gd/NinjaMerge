using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New Sword", menuName = "Items/Sword")]
    public class Sword : Mergable
    {
        [SerializeField] private int _damage;
        
        public int Damage { get { return _damage; } }
    }
}