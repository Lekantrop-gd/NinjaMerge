using Items;
using Merge;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MergableSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _mergingDesk;
        [SerializeField] private Mergable _mergable;
        [SerializeField] private Sword[] _swords;
        [SerializeField] private Armor[] _armor;
        [SerializeField] private Hat[] _armorHat;
        [SerializeField] private int _maxItems;

        [Button]
        public void SpawnRandowSword()
        {
            if (_mergingDesk.transform.childCount < _maxItems)
            {
                Mergable mergable = Instantiate(_mergable, _mergingDesk.transform);
                mergable.Init(_swords[0].Sprite);
            }
        }

        [Button]
        public void SpawnRandowArmor()
        {

        }

        [Button]
        public void SpawnRandowHat()
        {

        }
    }
}