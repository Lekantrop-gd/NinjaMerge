using UnityEngine;

public class Armor : Mergable
{
    [SerializeField] private int _damageReducePercentage;

    public int DamageReducePercentage => _damageReducePercentage;
}