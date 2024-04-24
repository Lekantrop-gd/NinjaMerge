using System;
using UnityEditor;
using UnityEngine;

public class ApperanceChanger : MonoBehaviour
{
    [SerializeField] private Transform _weapon;
    [SerializeField] private Transform _armor;
    [SerializeField] private Transform _defaultArmorlessHair;
    [SerializeField] private Transform _defaultArmorHair;
    [SerializeField] private WeaponSet[] _weaponModels;
    [SerializeField] private ArmorSet[] _armorModels;

    [Serializable]
    public struct WeaponSet
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Transform _model;

        public Weapon Weapon => _weapon;
        public Transform Model => _model;
    }

    [Serializable]
    public struct ArmorSet
    {
        [SerializeField] private Armor _armor;
        [SerializeField] private Transform _hatModel;
        [SerializeField] private Transform _armorModel;

        public Armor Armor => _armor;
        public Transform HatModel => _hatModel;
        public Transform ArmorModel => _armorModel;
    }

    private void OnEnable()
    {
        WeaponCell.WeaponSet += OnWeaponSet;
        ArmorCell.ArmorSet += OnArmorSet;
    }

    private void OnDisable()
    {
        WeaponCell.WeaponSet += OnWeaponSet;
        ArmorCell.ArmorSet -= OnArmorSet;
    }

    private void OnWeaponSet(Weapon weapon)
    {
        for (int child = _weapon.childCount - 1; child >= 0; child--)
        {
            Destroy(_weapon.GetChild(child).gameObject);
        }

        if (weapon == null)
        {
            return;
        }

        for (int x = 0; x < _weaponModels.Length; x++)
        {
            if (weapon.Damage == _weaponModels[x].Weapon.Damage)
            {
                Instantiate(_weaponModels[x].Model, _weapon);
            }
        }
    }

    private void OnArmorSet(Armor armor)
    {
        for (int child = _armor.childCount - 1; child >= 0; child--)
        {
            Destroy(_armor.GetChild(child).gameObject);
        }

        if (armor == null)
        {
            Instantiate(_defaultArmorlessHair, _armor);
            return;
        }

        bool set = false;

        for (int x = 0; x < _armorModels.Length; x++)
        {
            if (armor.ProtectionPoints == _armorModels[x].Armor.ProtectionPoints)
            {
                Instantiate(_defaultArmorHair, _armor);
                Instantiate(_armorModels[x].HatModel, _armor);
                PrefabUtility.ReplacePrefab(gameObject, _armorModels[x].ArmorModel);
            }
        }

        if (set == false)
        {
            Instantiate(_defaultArmorlessHair, _armor);
        }
    }
}
