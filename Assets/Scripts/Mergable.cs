using UnityEngine;
using UnityEngine.Events;

public class Mergable : MonoBehaviour
{
    [SerializeField] private Mergable _superior;
    [SerializeField] private ParticleSystem _appearParticles;

    public Mergable Superior => _superior;

    private void OnEnable()
    {
        GameObject particles = Instantiate(_appearParticles).gameObject;
        particles.transform.position = transform.position;

        particles.GetComponent<ParticleSystem>().Play();

        float totalDuration = particles.GetComponent<ParticleSystem>().main.duration + particles.GetComponent<ParticleSystem>().startLifetime;
        Destroy(particles, totalDuration);
    }

    private void OnDisable()
    {

    }
}
