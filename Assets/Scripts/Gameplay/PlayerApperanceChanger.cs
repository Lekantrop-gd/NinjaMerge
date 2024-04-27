using UnityEngine;

public class PlayerApperanceChanger : ApperanceChanger
{
    [SerializeField] private RuntimeAnimatorController _playerAnimator;

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

    public override void SetArmor(Armor armor)
    {
        base.SetArmor(armor);

        _playerModel.GetComponent<Animator>().runtimeAnimatorController = _playerAnimator;
    }
}