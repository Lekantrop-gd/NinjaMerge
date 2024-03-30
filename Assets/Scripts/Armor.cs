using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New Armor", menuName = "Items/Armor")]
    public class Armor : Mergable
    {
        [SerializeField] private int _damageDecrease;

        public int DamageDecrease { get { return _damageDecrease; } }
    }
}