using Items;
using UnityEngine;
using UnityEngine.UI;

namespace Merge
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private Mergable _mergable;

        public void Init(Mergable mergable)
        {
            _image.sprite = mergable?.Sprite;
            _mergable = mergable;
        }

        public bool Put(Mergable mergable)
        {
            if (mergable == null)
            {
                _mergable = mergable;
                return true;
            }
            else
            {
                return false;
            }
        }

        public Mergable Take()
        {
            Init(null);
            return _mergable;
        }
    }
}