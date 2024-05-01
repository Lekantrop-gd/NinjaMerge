using System;
using UnityEngine;

public class WeaponCell : Cell
{
    [SerializeField] private GameObject _indicator;

    public static Action<Weapon> WeaponSet;

    public override void Put(Mergable mergable)
    {
        base.Put(mergable);
        WeaponSet?.Invoke(Context as Weapon);

        _indicator.SetActive(mergable == null);
    }
}