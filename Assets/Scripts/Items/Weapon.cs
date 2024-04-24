using UnityEngine;

public class Weapon : Mergable
{
    [SerializeField] private int _damage;

    public int Damage => _damage;
}