using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private UnityEvent _startGearShowcase;
    [SerializeField] private UnityEvent _endGearShowcase;

    private Animator _animator;

    private void OnEnable()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();

        PlayerApperanceChanger.AnimatorSet += OnAnimatorSet;
        Player.Damage += Fight;
        Player.Run += Run;
        Player.Won += Won;
        Player.Defeat += Defeat;
        PlayerEventHandler.StartGearShowcase += StartGearShowcase;
        PlayerEventHandler.EndGearShowcase += EndGearShowcase;
    }

    private void OnDisable()
    {
        PlayerApperanceChanger.AnimatorSet -= OnAnimatorSet;
        Player.Damage -= Fight;
        Player.Run -= Run;
        Player.Won -= Won;
        Player.Defeat -= Defeat;
        PlayerEventHandler.StartGearShowcase -= StartGearShowcase;
        PlayerEventHandler.EndGearShowcase -= EndGearShowcase;
    }

    public void OnAnimatorSet(Animator animator)
    {
        _animator = animator;
        _animator.SetTrigger("GearShowcase");
    }

    public void Run()
    {
        _animator.SetTrigger(nameof(Run));
    }

    public void Fight()
    {
        _animator.SetTrigger(nameof(Fight));
    }

    public void Won()
    {
        _animator.SetTrigger(nameof(Won));
    }

    public void Defeat()
    {
        _animator.SetTrigger(nameof(Defeat));
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