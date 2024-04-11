using System.Collections;
using UnityEngine;

public class Mergable : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private LayerMask _mergable;
    [SerializeField] private LayerMask _cell;

    private Coroutine _following;
    private Vector3 _startPosition;
    private Cell _previousCell;

    public void Init(Vector3 startPosition)
    {
        transform.position = startPosition;
        _startPosition = startPosition;
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        GetComponent<Collider>().enabled = false;

        if (Physics.Raycast(ray, out hit, _cell))
        {
            if (hit.collider.TryGetComponent(out Cell cell))
            {
                _previousCell = cell;
            }
        }

        GetComponent<Collider>().enabled = true;

        _startPosition = transform.position;
        _following = StartCoroutine(Following());
    }

    private void OnMouseUp()
    {
        StopCoroutine(_following);
        
        GetComponent<Collider>().enabled = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
            
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out Cell cell))
            {
                if (cell.Put(this))
                {
                    _previousCell.Take();
                    StartCoroutine(MoveTo(cell.transform.position));
                }
                else
                {
                    StartCoroutine(MoveTo(_startPosition));
                }
            }
            else
            {
                StartCoroutine(MoveTo(_startPosition));
            }
        }

        GetComponent<Collider>().enabled = true;
    }

    private IEnumerator Following()
    {
        RaycastHit hit;
        Ray ray;

        while (true)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, _mergable))
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(hit.point.x, transform.position.y, hit.point.z), Time.deltaTime * _movementSpeed);
            }
            
            yield return null;
        }
    }

    public IEnumerator MoveTo(Vector3 target)
    {
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * _movementSpeed);

            yield return null;
        }
    }
}
