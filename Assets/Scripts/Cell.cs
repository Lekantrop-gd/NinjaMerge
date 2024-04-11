using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool Empty { get; private set; }

    private void Awake()
    {
        Empty = true;
    }

    public bool Put(Mergable mergable)
    {
        if (Empty)
        {
            Empty = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Take()
    {
        Empty = true;
    }
}
