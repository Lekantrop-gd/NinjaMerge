using System.Collections;
using TMPro;
using UnityEngine;

public class Mergable : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    
    private Coroutine _following;

    private void OnMouseDown()
    {
        _following = StartCoroutine(Following());
    }

    private void OnMouseUp()
    {
        StopCoroutine(_following);
    }

    private IEnumerator Following()
    {
        RaycastHit hit;
        Ray ray;

        while (true)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(hit.point.x, transform.position.y, hit.point.z), Time.deltaTime * _movementSpeed);
            }
            
            yield return null;
        }
    }
}
