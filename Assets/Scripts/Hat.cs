using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New Hat", menuName = "Items/Hat")]
    public class Hat : Mergable
    {
        [SerializeField] private int _damageDecrease;

        public int DamageDecrease { get { return _damageDecrease; } }
    }
}