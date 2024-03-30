using UnityEngine;
using UnityEngine.UI;

namespace Merge
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void Init(Sprite image)
        {
            _image.sprite = image;
        }

        public void Put(Slot mergable)
        {

        }

        public Slot Take()
        {
            _image.sprite = null;
            return this;
        }
    }
}