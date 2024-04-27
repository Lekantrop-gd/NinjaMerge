using System;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public static event Action Damage;

    public void DealDamage()
    {
        Damage?.Invoke();
    }
}