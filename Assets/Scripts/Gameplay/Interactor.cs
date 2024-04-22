using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _itemsRoot;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private LayerMask _cellLayer;
    [SerializeField] private LayerMask _mergingDeskLayer;
    [SerializeField] private LayerMask _playerLayer;

    private Coroutine _following;
    private Cell _previousCell;
    private bool _interacted = false;

    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();

        EnhancedTouch.Touch.onFingerDown += Press;
        EnhancedTouch.Touch.onFingerUp += Release;
    }

    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
    }

    private void Press(Finger finger)
    {
        Ray ray = Camera.main.ScreenPointToRay(finger.screenPosition);
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
                }
            }
        }
    }

    private void Release(Finger finger)
    {
        if (_interacted == false)
            return;

        if (_following != null)
            StopCoroutine(_following);

        Ray ray = Camera.main.ScreenPointToRay(finger.screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _cellLayer))
        {
            if (hit.collider.TryGetComponent(out Cell cell))
            {
                if (_previousCell.Context == null)
                {
                    //pass
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
            }

            else
            {
                _previousCell.Context.MoveTo(_previousCell.transform.position);
            }
        }

        _interacted = false;
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