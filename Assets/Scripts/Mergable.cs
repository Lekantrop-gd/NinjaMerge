using UnityEngine;
using UnityEngine.UI;

namespace Merge
{
    public class Mergable : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void Init(Sprite image)
        {
            _image.sprite = image;
        }

        public void Put(Mergable mergable)
        {

        }

        public Mergable Take()
        {
            return this;
        }
    }
}