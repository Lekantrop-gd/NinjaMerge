using System.Collections;
using UnityEngine;

public class Mergable : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private LayerMask _mergable;
    [SerializeField] private LayerMask _cell;
    [SerializeField] private Mergable _superior;

    public Mergable Superior => _superior;

    private Coroutine _following;
    private Vector3 _startPosition;
    private Cell _previousCell;

    public void Init(Vector3 startPosition)
    {
        transform.localPosition = startPosition;
        _startPosition = startPosition;
    }

    private void OnMouseDown()
    {
        Debug.Log("Pick Up");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        GetComponent<Collider>().enabled = false;

        if (Physics.Raycast(ray, out hit, _cell))
        {
            if (hit.collider.TryGetComponent(out Cell cell))
            {
                _previousCell = cell;
                _startPosition = transform.position;
                _following = StartCoroutine(Following());
            }
        }

        GetComponent<Collider>().enabled = true;
    }

    private void OnMouseUp()
    {
        Debug.Log("Put Down");

        StopCoroutine(_following);
        
        GetComponent<Collider>().enabled = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
            
        if (Physics.Raycast(ray, out hit, _cell))
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
                    if (cell.Context.Superior == _superior)
                    {
                        Destroy(cell.Context.gameObject);
                        Destroy(gameObject);

                        _previousCell.Take();
                        cell.Take();

                        Instantiate(_superior, transform.parent);
                    }
                    else
                    {
                        StartCoroutine(MoveTo(_startPosition));
                    }
                }
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
