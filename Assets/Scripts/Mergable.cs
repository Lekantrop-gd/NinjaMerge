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
                }
                else
                {
                    transform.position = _startPosition;
                }
            }
            else
            {
                transform.position = _startPosition;
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
}
