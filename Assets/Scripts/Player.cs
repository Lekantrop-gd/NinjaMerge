using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _reachDistance;
    [SerializeField] private float _detectingRadius;
    [SerializeField] private float _animationSpeed;
    [SerializeField] private float _health;
    [SerializeField] private WeaponCell _weaponCell;
    [SerializeField] private ArmorCell _armorCell;

    public bool Dead => _dead;
    
    private List<Enemy> _enemies = new List<Enemy>();
    private Coroutine _attacking;
    private bool _dead;

    private void OnEnable()
    {
        Enemy.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        Enemy.Died -= OnEnemyDied;
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
        enemy.TakeDamage(_weaponCell.Weapon.Damage);
    }

    public void TakeDamage(int damage)
    {
        if (damage >= _health)
        {
            Destroy(gameObject);
            _dead = true;
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
