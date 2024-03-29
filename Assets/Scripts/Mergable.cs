using UnityEngine;

namespace Merge
{
    public class Mergable : MonoBehaviour
    {
        public void Put(Mergable mergable)
        {

        }

        public Mergable Take()
        {
            return this;
        }
    }
}