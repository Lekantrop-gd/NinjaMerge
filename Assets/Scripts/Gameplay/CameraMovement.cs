using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private Vector3 _endRotation;
    [SerializeField] private float _speed;

    public void StartMovement()
    {
        StartCoroutine(Moving());
    }

    private IEnumerator Moving()
    {
        Quaternion targetRotation = Quaternion.Euler(_endRotation);

        while (transform.position != _endPosition || transform.rotation != targetRotation)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                _endPosition, _speed * Time.deltaTime);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 
                _speed * Time.deltaTime);

            yield return null;
        }
    }
}
