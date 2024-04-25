using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private int _spreadIncrement;

    private Vector3 _position = Vector3.zero;

    public void Spawn(Weapon weapon, Armor armor)
    {
        Enemy instance;

        if (_position == Vector3.zero)
        {
            instance = Instantiate(_prefab, transform);
            instance.transform.localPosition = _position;
            _position = new Vector3(_spreadIncrement, 0, 0);
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
        }

        instance.Init(weapon, armor);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right);
    }
}