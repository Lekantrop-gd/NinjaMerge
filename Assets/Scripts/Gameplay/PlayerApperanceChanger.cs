using Unity.VisualScripting;
using UnityEngine;

public class PlayerApperanceChanger : ApperanceChanger
{
    [SerializeField] private RuntimeAnimatorController _animator;

    private void OnEnable()
    {
        WeaponCell.WeaponSet += SetWeapon;
        ArmorCell.ArmorSet += SetArmor;
    }

    private void OnDisable()
    {
        WeaponCell.WeaponSet -= SetWeapon;
        ArmorCell.ArmorSet -= SetArmor;
    }

    public override void AddDamager()
    {
        _playerModel.AddComponent<PlayerEventHandler>();
        _playerModel.GetComponent<Animator>().runtimeAnimatorController = _animator;
    }
}