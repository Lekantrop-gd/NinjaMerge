using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private LayerMask _cellLayer;
    [SerializeField] private LayerMask _mergingDeskLayer;

    private Coroutine _following;
    private Cell _previousCell;

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
                    _following = StartCoroutine(FollowPointer(cell.Context));
                    _previousCell = cell;
                }
            }
        }
    }

    private void Release(Finger finger)
    {
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
                    return;
                }

                else if (cell.Context == null)
                {
                    StartCoroutine(MoveTo(_previousCell.Context.transform,
                                          cell.transform.position));
                }

                else if (cell.Equals(_previousCell))
                {
                    StartCoroutine(MoveTo(_previousCell.Context.transform, 
                                          _previousCell.transform.position));
                    return;
                }

                else if (cell.Context.Superior == _previousCell.Context.Superior)
                {
                    Destroy(_previousCell.Context.gameObject);
                    Destroy(cell.Context.gameObject);

                    _previousCell.Put(null);
                    cell.Put(null);
                }
                
                else
                {
                    StartCoroutine(MoveTo(_previousCell.Context.transform,
                                          cell.transform.position));

                    StartCoroutine(MoveTo(cell.Context.transform,
                                          _previousCell.transform.position));
                }

                Mergable temporaryContext = _previousCell.Context;

                _previousCell.Put(cell.Context);
                cell.Put(temporaryContext);
            }
        }
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
                    new Vector3(hit.point.x, context.transform.position.y, hit.point.z), 
                    Time.deltaTime * _movementSpeed);
            }

            yield return null;
        }
    }

    private IEnumerator MoveTo(Transform movable, Vector3 position)
    {
        while (movable.position !=  position)
        {
            movable.position = Vector3.MoveTowards(movable.position, position, _movementSpeed * Time.deltaTime);
            yield return null;
        }
    }
}