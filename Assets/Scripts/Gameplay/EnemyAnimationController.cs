using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    private bool _running;
    private bool _fighting;

    private void OnEnable()
    {
        _enemy.Run += Run;
        _enemy.Fight += Fight;
        _enemy.Died += Defeat;
        Player.Defeat += Won;
    }

    private void OnDisable()
    {
        _enemy.Run -= Run;
        _enemy.Fight -= Fight;
        _enemy.Died -= Defeat;
        Player.Defeat -= Won;
    }

    public void Run()
    {
        if (_running == false)
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger(nameof(Run));
            _running = true;
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

    public void Defeat(Enemy enemy)
    {
        transform.GetChild(0).GetComponent<Animator>().SetTrigger(nameof(Defeat));
        Destroy(this);
    }
}