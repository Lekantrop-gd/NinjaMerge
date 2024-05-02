using System;
using UnityEngine;

public class PlayerEventHandler : MonoBehaviour
{
    public static event Action Damage;
    public static event Action StartGearShowcase;
    public static event Action EndGearShowcase;

    public void DealDamage()
    {
        Damage?.Invoke();
    }

    public void StartGerShowcase()
    {
        StartGearShowcase?.Invoke();
    }

    public void EndGerShowcase()
    {
        EndGearShowcase?.Invoke();
    }
}