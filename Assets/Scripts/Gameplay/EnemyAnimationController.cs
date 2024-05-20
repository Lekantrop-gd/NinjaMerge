using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator _animator;
    private bool _running;
    private bool _fighting;


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
        if (_running == false)
        {
            _animator = transform.GetChild(0).GetComponent<Animator>();
            _animator.SetTrigger(nameof(Run));
            _running = true;
        }
    }

    public void Fight()
    {
        if (_fighting == false)
        {
            _animator.SetTrigger(nameof(Fight));
            _fighting = true;
        }
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