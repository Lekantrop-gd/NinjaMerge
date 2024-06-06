using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _itemsRoot;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private LayerMask _cellLayer;
    [SerializeField] private LayerMask _mergingDeskLayer;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private UnityEvent _take;
    [SerializeField] private UnityEvent _put;
    [SerializeField] private UnityEvent _merged;

    public static event Action Updated;

    private Collider _collider;
    private Coroutine _following;
    private Cell _previousCell;
    private bool _interacted = false;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnMouseDown()
    {
        _collider.enabled = false;
        Press();
    }

    private void OnMouseUp()
    {
        Release();
        _collider.enabled = true;
    }

    private void Press()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _cellLayer))
        {
            if (hit.collider.TryGetComponent(out Cell cell))
            {
                if (cell.Context != null)
                {
                    cell.Context.StopMoving();
                    _following = StartCoroutine(FollowPointer(cell.Context));
                    _previousCell = cell;
                    _interacted = true;
                    _take?.Invoke();
                }
            }
        }
    }

    private void Release()
    {
        if (_interacted == false)
            return;

        if (_following != null)
            StopCoroutine(_following);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _cellLayer))
        {
            if (hit.collider.TryGetComponent(out Cell cell))
            {
                if (_previousCell.Context == null)
                {
                    //pass
                }

                else if (cell.GetComponent<WeaponCell>() != null && _previousCell.Context is Armor)
                {
                    _previousCell.Context.MoveTo(_previousCell.transform.position);
                    return;
                }

                else if (cell.GetComponent<ArmorCell>() != null && _previousCell.Context is Weapon)
                {
                    _previousCell.Context.MoveTo(_previousCell.transform.position);
                    return;
                }

                else if (cell.Context is Weapon && _previousCell.GetComponent<ArmorCell>() != null)
                {
                    _previousCell.Context.MoveTo(_previousCell.transform.position);
                    return;
                }

                else if (cell.Context is Armor && _previousCell.GetComponent<WeaponCell>() != null)
                {
                    _previousCell.Context.MoveTo(_previousCell.transform.position);
                    return;
                }

                else if (cell.Context == null)
                {
                    _previousCell.Context.MoveTo(cell.transform.position);
                }

                else if (cell.Equals(_previousCell))
                {
                    _previousCell.Context.MoveTo(_previousCell.transform.position);
                }

                else if (cell.Context.Superior == _previousCell.Context.Superior)
                {
                    Destroy(_previousCell.Context.gameObject);
                    Destroy(cell.Context.gameObject);

                    Mergable superior = Instantiate(cell.Context.Superior, _itemsRoot);

                    superior.transform.position = cell.transform.position;

                    _merged?.Invoke();

                    _previousCell.Put(superior);
                    cell.Put(null);
                }
                
                else
                {
                    _previousCell.Context.MoveTo(cell.transform.position);
                    cell.Context.MoveTo(_previousCell.transform.position);
                }

                Mergable temporaryContext = _previousCell.Context;

                _previousCell.Put(cell.Context);
                cell.Put(temporaryContext);
                _put?.Invoke();
            }

            else
            {
                _previousCell.Context.MoveTo(_previousCell.transform.position);
            }
        }

        _interacted = false;
        Updated?.Invoke();
    }

    private IEnumerator FollowPointer(Mergable context)
    {
        RaycastHit hit;
        Ray ray;

        while (true)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, _mergingDeskLayer))
            {
                context.transform.position = Vector3.Lerp(context.transform.position, 
                    new Vector3(hit.point.x, hit.point.y, context.transform.position.z),
                    _movementSpeed * Time.deltaTime);
            }
            else
            {
                context.MoveTo(_previousCell.transform.position);
                StopCoroutine(_following);
            }

            yield return null;
        }
    }
}