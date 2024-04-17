using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> Died;

    public void TakeDamage(int damage)
    {
        Destroy(gameObject);
        Died?.Invoke(this);
    }
}
