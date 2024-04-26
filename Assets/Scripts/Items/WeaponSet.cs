﻿using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponSet", menuName = "Items/WeaponSet")]
public class WeaponSet : ScriptableObject
{
    [SerializeField] private WeaponLink[] _weaponLinks;

    public WeaponLink[] WeaponLinks => _weaponLinks;

    [Serializable]
    public struct WeaponLink
    {
        [SerializeField] private int _id;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Transform _model;

        public int Id => _id;
        public Weapon Weapon => _weapon;
        public Transform Model => _model;
    }
}