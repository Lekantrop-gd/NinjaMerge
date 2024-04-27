using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;

    private void OnEnable()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();

        PlayerApperanceChanger.AnimatorSet += OnAnimatorSet;
        Player.Damage += Fight;
        Player.Run += Run;
        Player.Won += Won;
        Player.Defeat += Defeat;
    }

    private void OnDisable()
    {
        PlayerApperanceChanger.AnimatorSet -= OnAnimatorSet;
        Player.Damage -= Fight;
        Player.Run -= Run;
        Player.Won -= Won;
        Player.Defeat -= Defeat;
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
}