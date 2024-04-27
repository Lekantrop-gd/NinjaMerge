using UnityEngine;

public class PlayerApperanceChanger : ApperanceChanger
{
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
}