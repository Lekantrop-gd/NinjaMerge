using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Mergable _context;

    public virtual Mergable Context => _context;

    private void Awake()
    {
        if (_context != null)
        {
            _context.transform.position = transform.position;
        }
    }

    public void Put(Mergable mergable)
    {
        _context = mergable;
    }

    public void Take()
    {
        _context = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, GetComponent<Collider>().bounds.size);
        
        if (_context != null)
        {
            Gizmos.DrawSphere(transform.position, 0.01f);
        }
    }
}