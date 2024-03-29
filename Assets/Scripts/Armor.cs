using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "New Armor", menuName = "Items/Armor")]
    public class Armor : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _name;
        [SerializeField] private int _damageDecrease;

        public Sprite Sprite { get; private set; }
        public string Name { get; private set; }
        public int DamageDecrease { get; private set; }
    }
}