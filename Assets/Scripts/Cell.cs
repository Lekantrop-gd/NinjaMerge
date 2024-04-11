using UnityEngine;

public class Cell : MonoBehaviour
{
    public Mergable Context { get; private set; }

    private void Awake()
    {
        Take();
    }

    public bool Put(Mergable mergable)
    {
        if (Context == null)
        {
            Context = mergable;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Take()
    {
        Context = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, GetComponent<Collider>().bounds.size);
    }
}
