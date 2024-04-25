using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _detectingRadius;
    [SerializeField] private float _reachDistance;
    [SerializeField] private float _animationSpeed;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Armor _armor;

    public static event Action<Enemy> Died;
    private Coroutine _attacking;

    public void Init(Weapon weapon, Armor armor)
    {
        _weapon = weapon;
        _armor = armor;
    }

    public void StartFight()
    {
        Collider playerCollider = Physics.OverlapSphere(transform.position, _detectingRadius, _playerLayer)[0];
        Player player = playerCollider.GetComponent<Player>();

        _attacking = StartCoroutine(Attack(player));
    }

    private IEnumerator Attack(Player player)
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > _reachDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * _animationSpeed);
            }
            else
            {
                yield return new WaitForSeconds(1);
                DealDamage(player);
                yield return new WaitForSeconds(1);
            }

            yield return null;
        }
    }

    public void DealDamage(Player player)
    {
        player.TakeDamage(_weapon == null ? 0 : _weapon.Damage);
    }

    public void TakeDamage(int damage)
    {
        Destroy(gameObject);
        Died?.Invoke(this);
    }

    private void OnEnable()
    {
        Player.Defeat += WinnerDance;
    }

    private void OnDisable()
    {
        Player.Defeat -= WinnerDance;
    }

    private void WinnerDance()
    {
        StopCoroutine(_attacking);
    }
}