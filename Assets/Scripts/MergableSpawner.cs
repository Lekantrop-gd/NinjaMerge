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
        [SerializeField] private Slot _slotPrefab;
        [SerializeField] private Sword[] _swords;
        [SerializeField] private Armor[] _armors;
        [SerializeField] private Hat[] _hats;
        [SerializeField] private int _maxItems;

        [Button]
        public void Init()
        {
            while (_mergingDesk.transform.childCount < _maxItems)
            {
                Instantiate(_slotPrefab, _mergingDesk.transform);
            }
        }

        [Button]
        public void SpawnRandowSword()
        {
            Slot slot;
            do
            {
                int slotIndex = Random.Range(0, _mergingDesk.transform.childCount);
                slot = _mergingDesk.transform.GetChild(slotIndex).GetComponent<Slot>();
            }
            while (slot.Take() != null);

            slot.Init(_swords[Random.Range(0, _swords.Length)]);
        }

        [Button]
        public void SpawnRandowArmor()
        {
            if (_mergingDesk.transform.childCount < _maxItems)
            {
                int slotIndex = Random.Range(0, _mergingDesk.transform.childCount);
                Slot slot = _mergingDesk.transform.GetChild(slotIndex).GetComponent<Slot>();

                slot.Init(_armors[Random.Range(0, _armors.Length)]);
            }
        }

        [Button]
        public void SpawnRandowHat()
        {
            if (_mergingDesk.transform.childCount < _maxItems)
            {
                int slotIndex = Random.Range(0, _mergingDesk.transform.childCount);
                Slot slot = _mergingDesk.transform.GetChild(slotIndex).GetComponent<Slot>();
                
                slot.Init(_hats[Random.Range(0, _hats.Length)]);
            }
        }
    }
}