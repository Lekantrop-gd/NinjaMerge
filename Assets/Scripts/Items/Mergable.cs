using System.Collections;
using UnityEngine;

public class Mergable : MonoBehaviour
{
    [SerializeField] private Mergable _superior;
    [SerializeField] private ParticleSystem _appearParticles;
    [SerializeField] private float _movingSpeed;

    public Mergable Superior => _superior;

    private Coroutine _moving;

    private void OnEnable()
    {
        Transform particles = Instantiate(_appearParticles, 
                                          _appearParticles.transform.position, 
                                          _appearParticles.transform.rotation).gameObject.transform;
        particles.parent = transform;
        particles.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 0.5f);
    }

    public void StopMoving()
    {
        if (_moving != null)
            StopCoroutine(_moving);
    }

    public void MoveTo(Vector3 target)
    {
        StopMoving();

        _moving = StartCoroutine(PerformMoving(target));
    }

    private IEnumerator PerformMoving(Vector3 target)
    {
        while (transform.position !=  target)
        {
            transform.position = Vector3.Lerp(transform.position, target, _movingSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
