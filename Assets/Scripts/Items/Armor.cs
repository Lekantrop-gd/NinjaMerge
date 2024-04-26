using UnityEngine;

public class Armor : Mergable
{
    [SerializeField] private int _protectionPoints;

    public int ProtectionPoints => _protectionPoints;
}