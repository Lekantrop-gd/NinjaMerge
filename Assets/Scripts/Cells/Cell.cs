using System;
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

    public virtual void Put(Mergable mergable)
    {
        _context = mergable;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, GetComponent<Collider>().bounds.size);
        
        if (_context != null)
        {
            Gizmos.DrawSphere(transform.position, 0.1f);
        }
    }
}
