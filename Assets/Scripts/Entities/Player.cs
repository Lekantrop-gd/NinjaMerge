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

    public static event Action Won;
    public static event Action Defeat;
    public static event Action Damage;
    public static event Action Run;

    private Weapon _weapon;
    private Armor _armor;
    private Enemy _enemy;
    private int _health;

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

    private void OnWeaponSet(Weapon weapon)
    {
        _weapon = weapon;
    }

    private void OnArmorSet(Armor armor)
    {
        _armor = armor;
        _health = armor == null ? _startHealth : armor.ProtectionPoints;
    }

    public void Fight()
    {
        Collider[] enemiesColliders = Physics.OverlapSphere(transform.position, 
                                                            _detectingRadius, 
                                                            _enemyLayer);

        if (enemiesColliders.Length > 0)
        {
            StartCoroutine(Attack(enemiesColliders[0].GetComponent<Enemy>()));
        }
        else
        {
            if (_health > 0)
            {
                Won?.Invoke();
            }
            return;
        }
    }

    private IEnumerator Attack(Enemy enemy)
    {
        Collider enemyCollider = enemy.GetComponent<Collider>();

        while (enemy != null && enemyCollider.enabled)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) > _reachDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, 
                                                          enemy.transform.position, 
                                                          _animationSpeed * Time.deltaTime);
                
                Quaternion targetRotation = Quaternion.LookRotation(enemy.transform.position - 
                                                                    transform.position);
                
                transform.rotation = Quaternion.Slerp(transform.rotation, 
                                                       targetRotation, 
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

    public void DealDamage()
    {
        _enemy.TakeDamage(_weapon == null ? 0 : _weapon.Damage);
        _fightSounds[UnityEngine.Random.Range(0, _fightSounds.Length)].Play();
    }

    public void TakeDamage(int damage)
    {
        if (damage >= _health)
        {
            _health = 0;
            Defeat?.Invoke();
        }
        else
        {
            _health -= damage;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _detectingRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _reachDistance);
    }
}