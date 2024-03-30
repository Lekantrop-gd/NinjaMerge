using UnityEngine;

namespace Items
{
    public abstract class Mergable : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _name;

        public Sprite Sprite { get { return _sprite; } }
        public string Name { get { return _name; } }
    }
}