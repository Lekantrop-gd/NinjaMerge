using Unity.VisualScripting;

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

    public override void AddDamager()
    {
        _playerModel.AddComponent<PlayerDamager>();
    }
}