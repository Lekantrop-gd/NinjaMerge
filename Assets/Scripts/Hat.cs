using System.Collections;
using UnityEngine;

namespace Items
{
    public class Hat : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _name;
        [SerializeField] private int _damageDecrease;

        public Sprite Sprite { get; private set; }
        public string Name { get; private set; }
        public int DamageDecrease { get; private set; }
    }
}