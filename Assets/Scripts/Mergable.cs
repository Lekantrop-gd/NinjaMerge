using UnityEngine;

public class Mergable : MonoBehaviour
{
    [SerializeField] private Mergable _superior;
    [SerializeField] private ParticleSystem _appearParticles;

    public Mergable Superior => _superior;

    private void OnEnable()
    {
        Transform particles = Instantiate(_appearParticles, _appearParticles.transform.position, _appearParticles.transform.rotation).gameObject.transform;
        particles.parent = transform;
        particles.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.01f, transform.localPosition.z);
    }
}
