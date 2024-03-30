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
        [SerializeField] private Slot _mergable;
        [SerializeField] private Sword[] _swords;
        [SerializeField] private Armor[] _armors;
        [SerializeField] private Hat[] _hats;
        [SerializeField] private int _maxItems;

        [Button]
        public void SpawnRandowSword()
        {
            if (_mergingDesk.transform.childCount < _maxItems)
            {
                Slot mergable = Instantiate(_mergable, _mergingDesk.transform);
                mergable.Init(_swords[Random.Range(0, _swords.Length)].Sprite);
            }
        }

        [Button]
        public void SpawnRandowArmor()
        {
            if (_mergingDesk.transform.childCount < _maxItems)
            {
                Slot mergable = Instantiate(_mergable, _mergingDesk.transform);
                mergable.Init(_armors[Random.Range(0, _armors.Length)].Sprite);
            }
        }

        [Button]
        public void SpawnRandowHat()
        {
            if (_mergingDesk.transform.childCount < _maxItems)
            {
                Slot mergable = Instantiate(_mergable, _mergingDesk.transform);
                mergable.Init(_hats[Random.Range(0, _hats.Length)].Sprite);
            }
        }
    }
}