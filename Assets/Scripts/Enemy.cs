using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _detectingRadius;
    [SerializeField] private float _reachDistance;
    [SerializeField] private float _animationSpeed;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Mergable _sword;

    public static event Action<Enemy> Died;

    public void StartFight()
    {
        Collider playerCollider = Physics.OverlapSphere(transform.position, _detectingRadius, _playerLayer)[0];
        Player player = playerCollider.GetComponent<Player>();

        StartCoroutine(Attack(player));
    }

    private IEnumerator Attack(Player player)
    {
        while (player.Dead == false)
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
        player.TakeDamage(_sword.Damage);
    }

    public void TakeDamage(int damage)
    {
        Destroy(gameObject);
        Died?.Invoke(this);
    }
}
