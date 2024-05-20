using System;
using UnityEngine;

public class EnemyEventHandler : MonoBehaviour
{
    public static event Action Damage;

    public void DealDamage()
    {
        Damage?.Invoke();
    }
}