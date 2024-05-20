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

    public override void AddEventHandler()
    {
        _model.AddComponent<PlayerEventHandler>();
        _model.GetComponent<Animator>().runtimeAnimatorController = _animator;
    }
}