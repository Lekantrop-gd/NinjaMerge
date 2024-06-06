using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _reachDistance;
    [SerializeField] private float _detectingRadius;
    [SerializeField] private float _animationSpeed;
    [SerializeField] private int _startHealth;
    [SerializeField] private AudioSource[] _fightSounds;

    private Weapon _weapon;
    private Armor _armor;
    private Enemy _enemy;
    private int _health;

    public bool Alive { get; private set; }

    public static event Action Won;
    public static event Action Defeat;
    public static event Action Damage;
    public static event Action Run;

    private void OnEnable()
    {
        _health = _startHealth;
        WeaponCell.WeaponSet += OnWeaponSet;
        ArmorCell.ArmorSet += OnArmorSet;
        PlayerEventHandler.Damage += DealDamage;
    }

    private void OnDisable()
    {
        WeaponCell.WeaponSet -= OnWeaponSet;
        ArmorCell.ArmorSet -= OnArmorSet;
        PlayerEventHandler.Damage -= DealDamage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _detectingRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _reachDistance);
    }

    public void TakeDamage(int damage)
    {
        if (_health == 0)
        {
            return;
        }
        if (damage >= _health)
        {
            _health = 0;
            Alive = false;
            Defeat?.Invoke();
            Debug.Log("Defeat");
        }
        else
        {
            _health -= damage;
        }
    }

    public void DealDamage()
    {
        _enemy.TakeDamage(_weapon == null ? 0 : _weapon.Damage);
        _fightSounds[UnityEngine.Random.Range(0, _fightSounds.Length)].Play();
    }

    public void Fight()
    {
        Alive = true;

        Collider[] enemiesColliders = Physics.OverlapSphere(transform.position, 
            _detectingRadius, _enemyLayer);

        if (enemiesColliders.Length > 0)
        {
            StartCoroutine(Attack(enemiesColliders[0].GetComponent<Enemy>()));
        }
        else
        {
            if (_health > 0)
            {
                Won?.Invoke();
                StartCoroutine(Win());
                Debug.Log("Won");
            }
            return;
        }
    }

    private IEnumerator Attack(Enemy enemy)
    {
        while (enemy != null && enemy.Alive)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) > _reachDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, 
                    enemy.transform.position, _animationSpeed * Time.deltaTime);

                Quaternion targetRotation = Quaternion.LookRotation(enemy.transform.position -
                        transform.position);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                    _animationSpeed * Time.deltaTime);

                Run?.Invoke();
            }
            else
            {
                _enemy = enemy;
                Damage?.Invoke();
            }

            yield return null;
        }

        Fight();
    }

    private IEnumerator Win()
    {
        while (true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 
                Time.deltaTime * _animationSpeed / 2);

            transform.GetChild(0).transform.rotation = Quaternion.Slerp(
                transform.GetChild(0).transform.rotation, Quaternion.identity,
                Time.deltaTime * _animationSpeed / 2);

            yield return null;
        }
    }

    private void OnWeaponSet(Weapon weapon)
    {
        _weapon = weapon;
    }

    private void OnArmorSet(Armor armor)
    {
        _armor = armor;
        _health = armor == null ? _startHealth : armor.ProtectionPoints;
    }
}