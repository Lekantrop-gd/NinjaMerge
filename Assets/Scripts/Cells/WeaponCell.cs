using System;
using UnityEngine;
using UnityEngine.Events;

public class WeaponCell : Cell
{
    [SerializeField] private GameObject _indicator;
    [SerializeField] private UnityEvent _put;

    public static Action<Weapon> WeaponSet;

    public override void Put(Mergable mergable)
    {
        base.Put(mergable);
        WeaponSet?.Invoke(Context as Weapon);
        _put.Invoke();

        _indicator.SetActive(mergable == null);
    }
}