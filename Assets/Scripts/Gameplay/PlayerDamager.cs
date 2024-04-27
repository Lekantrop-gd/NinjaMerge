using System;
using UnityEngine;

public class PlayerDamager : MonoBehaviour
{
    public static event Action Damage;

    public void DealDamage()
    {
        Damage?.Invoke();
    }
}