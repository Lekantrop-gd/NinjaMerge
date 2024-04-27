using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator _animator;

    private void OnEnable()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();

        Enemy.Run += Run;
        Enemy.Fight += Fight;
        Player.Defeat += Won;
        Enemy.Died += Defeat;
    }

    private void OnDisable()
    {
        Enemy.Run -= Run;
        Enemy.Fight -= Fight;
        Player.Defeat -= Won;
        Enemy.Died -= Defeat;
    }

    public void Run()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
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

    public void Defeat(Enemy enemy)
    {
        _animator.SetTrigger(nameof(Defeat));
    }
}