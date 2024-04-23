using UnityEngine;

public class Armor : Mergable
{
    [SerializeField] private int _protectionPoints;
    [SerializeField] private GameObject _model;

    public int ProtectionPoints => _protectionPoints;
    public GameObject Model => _model;
}