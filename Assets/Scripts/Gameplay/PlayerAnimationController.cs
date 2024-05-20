using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private UnityEvent _startGearShowcase;
    [SerializeField] private UnityEvent _endGearShowcase;

    private Animator _animator;
    private bool _runinng;
    private bool _fighting;

    private void OnEnable()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();

        Player.Damage += Fight;
        Player.Run += Run;
        Player.Won += Won;
        Player.Defeat += Defeat;
        PlayerEventHandler.StartGearShowcase += StartGearShowcase;
        PlayerEventHandler.EndGearShowcase += EndGearShowcase;
    }

    private void OnDisable()
    {
        Player.Damage -= Fight;
        Player.Run -= Run;
        Player.Won -= Won;
        Player.Defeat -= Defeat;
        PlayerEventHandler.StartGearShowcase -= StartGearShowcase;
        PlayerEventHandler.EndGearShowcase -= EndGearShowcase;
    }

    public void Run()
    {
        if (_runinng == false)
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger(nameof(Run));
            _runinng = true;
        }
    }

    public void Fight()
    {
        if (_fighting == false)
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger(nameof(Fight));
            _fighting = true;
        }
    }

    public void Won()
    {
        transform.GetChild(0).GetComponent<Animator>().SetTrigger(nameof(Won));
    }

    public void Defeat()
    {
        transform.GetChild(0).GetComponent<Animator>().SetTrigger(nameof(Defeat));
    }

    public void StartGearShowcase()
    {
        _startGearShowcase?.Invoke();
    }

    public void EndGearShowcase()
    {
        _endGearShowcase?.Invoke();
    }
}