using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private int _spreadIncrement;
    [SerializeField] private LayerMask _enemyLayer;

    public static event Action<Enemy> Spawned;

    private Vector3 _position = Vector3.zero;

    private void Awake()
    {
        Spawned?.Invoke(_prefab);
    }

    public void Spawn(Weapon weapon, Armor armor)
    {
        Enemy instance;

        if (_position == Vector3.zero)
        {
            instance = Instantiate(_prefab, transform);
            instance.transform.localPosition = _position;
            _position = new Vector3(_spreadIncrement, 0, 0);

            Spawned?.Invoke(instance);
        }
        else
        {
            instance = Instantiate(_prefab, transform);
            instance.transform.localPosition = _position;
            if (_position.x > 0)
            {
                _position.x *= -1;
            }
            else
            {
                _position.x *= -1;
                _position.x += _spreadIncrement;
            }

            Spawned?.Invoke(instance);
        }

        instance.Init(weapon, armor);
    }

    public void StartFight()
    {
        Collider[] enemiesColliders = Physics.OverlapSphere(transform.position, 100, _enemyLayer);

        for (int x = 0; x < enemiesColliders.Length; x++)
        {
            enemiesColliders[x].GetComponent<Enemy>().StartFight();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right);
    }
}