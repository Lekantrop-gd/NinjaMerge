using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _reachDistance;
    [SerializeField] private float _detectingRadius;
    [SerializeField] private float _animationSpeed;
    [SerializeField] private float _health;

    public static event Action Won;
    public static event Action Defeat;

    private List<Enemy> _enemies = new List<Enemy>();
    private Coroutine _attacking;
    private Weapon _weapon;
    private Armor _armor;

    private void OnEnable()
    {
        Enemy.Died += OnEnemyDied;
        WeaponCell.WeaponSet += OnWeaponSet;
        ArmorCell.ArmorSet += OnArmorSet;
    }

    private void OnDisable()
    {
        Enemy.Died -= OnEnemyDied;
        WeaponCell.WeaponSet -= OnWeaponSet;
        ArmorCell.ArmorSet -= OnArmorSet;
    }

    private void OnWeaponSet(Weapon weapon)
    {
        _weapon = weapon;
    }

    private void OnArmorSet(Armor armor)
    {
        _armor = armor;
        _health = armor == null ? 0 : armor.ProtectionPoints;
    }

    private void OnEnemyDied(Enemy enemy) 
    {
        if (_enemies.Contains(enemy))
        {
            _enemies.Remove(enemy);
        }

        Fight();
    }

    public void StartFight()
    {
        Collider[] enemiesColliders = Physics.OverlapSphere(transform.position, _detectingRadius, _enemyLayer);
        
        foreach (Collider enemyCollider in enemiesColliders)
        {
            _enemies.Add(enemyCollider.GetComponent<Enemy>());
        }

        Fight();
    }

    private void Fight()
    {
        if (_attacking != null)
        {
            StopCoroutine(_attacking);
        }

        if (_enemies.Count > 0)
        {
            _attacking = StartCoroutine(Attack(_enemies[0]));
        }
        else
        {
            Won?.Invoke();
            Debug.Log("Won");
        }
    }

    private IEnumerator Attack(Enemy enemy)
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) > _reachDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, Time.deltaTime * _animationSpeed);
            }
            else
            {
                yield return new WaitForSeconds(1);
                DealDamage(enemy);
                yield return new WaitForSeconds(1);
            }

            yield return null;
        }
    }

    public void DealDamage(Enemy enemy)
    {
        enemy.TakeDamage(_weapon == null ? 0 : _weapon.Damage);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(damage);

        if (damage >= _health)
        {
            Defeat?.Invoke();
            Destroy(gameObject);
            Debug.Log("Defeat");
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
