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
    [SerializeField] private ApperanceChanger _apperanceChanger;

    public static event Action<Enemy> Died;
    public static event Action Run;
    public static event Action Fight;

    private Coroutine _attacking;
    private int _health = 0;

    public void Init(Weapon weapon, Armor armor)
    {
        _weapon = weapon;

        if (armor != null)
            _health = armor.ProtectionPoints;

        _apperanceChanger.SetWeapon(weapon);
        _apperanceChanger.SetArmor(armor);
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
                transform.position = Vector3.MoveTowards(transform.position, 
                                                         player.transform.position,
                                                         _animationSpeed * Time.deltaTime);

                Quaternion targetRotation = Quaternion.LookRotation(player.transform.position -
                                                                    transform.position);

                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      targetRotation,
                                                      _animationSpeed * Time.deltaTime);
                Run?.Invoke();
            }
            else
            {
                Fight?.Invoke();
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
        if (damage <= 0)
            return;
        else if (damage >= _health)
        {
            GetComponent<Collider>().enabled = false;
            Died?.Invoke(this);
        }
        else
        {
            _health -= damage;
        }
    }

    private void OnEnable()
    {
        Player.Won += OnWon;
        Player.Defeat += OnDefeat;
    }

    private void OnDisable()
    {
        Player.Won -= OnWon;
        Player.Defeat -= OnDefeat;
    }

    private void OnDefeat()
    {
        StopCoroutine(_attacking);
    }

    private void OnWon()
    {
        StopCoroutine(_attacking);
    }
}