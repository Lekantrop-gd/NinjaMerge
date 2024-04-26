using UnityEngine;

public class ApperanceChanger : MonoBehaviour
{
    [SerializeField] private Transform _weapon;
    [SerializeField] private Transform _armor;
    [SerializeField] private Transform _defaultArmorlessHair;
    [SerializeField] private Transform _defaultArmorHair;
    [SerializeField] private Transform _playerModel;
    [SerializeField] private Transform _defaultPlayerModel;
    [SerializeField] private Transform _accesories;
    [SerializeField] private WeaponSet _weaponSet;
    [SerializeField] private ArmorSet _armorSet;

    public void SetWeapon(Weapon weapon)
    {
        for (int child = _weapon.childCount - 1; child >= 0; child--)
        {
            Destroy(_weapon.GetChild(child).gameObject);
        }

        if (weapon == null)
        {
            return;
        }

        for (int x = 0; x < _weaponSet.WeaponLinks.Length; x++)
        {
            if (weapon.Damage == _weaponSet.WeaponLinks[x].Weapon.Damage)
            {
                Instantiate(_weaponSet.WeaponLinks[x].Model, _weapon);
            }
        }
    }

    public void SetArmor(Armor armor)
    {
        for (int child = _armor.childCount - 1; child >= 0; child--)
        {
            Destroy(_armor.GetChild(child).gameObject);
        }

        if (armor == null)
        {
            Transform newPlayerModel = Instantiate(_defaultPlayerModel, transform);
            _accesories.parent = newPlayerModel;
            Destroy(_playerModel.gameObject);
            _playerModel = newPlayerModel;

            Instantiate(_defaultArmorlessHair, _armor);
            return;
        }

        bool set = false;

        for (int x = 0; x < _armorSet.ArmorLinks.Length - 1; x++)
        {
            if (armor.ProtectionPoints == _armorSet.ArmorLinks[x].Armor.ProtectionPoints)
            {
                Transform newPlayerModel = Instantiate(_armorSet.ArmorLinks[x].ArmorModel, transform);
                _accesories.parent = newPlayerModel;
                Destroy(_playerModel.gameObject);
                _playerModel = newPlayerModel;

                Instantiate(_defaultArmorHair, _armor);
                Instantiate(_armorSet.ArmorLinks[x].HatModel, _armor);
                set = true;
            }
        }

        if (set == false)
        {
            Instantiate(_defaultArmorlessHair, _armor);
        }
    }
}
