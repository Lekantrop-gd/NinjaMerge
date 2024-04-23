using UnityEngine;

public class Weapon : Mergable
{
    [SerializeField] private int _damage;
    [SerializeField] private GameObject _model;

    public int Damage => _damage;
    public GameObject Model => _model;
}