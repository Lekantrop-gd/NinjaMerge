using System;
using UnityEngine;
using UnityEngine.Events;

public class ArmorCell : Cell
{
    [SerializeField] private GameObject _indicator;
    [SerializeField] private UnityEvent _put;

    public static Action<Armor> ArmorSet;

    public override void Put(Mergable mergable)
    {
        base.Put(mergable);
        ArmorSet?.Invoke(Context as Armor);
        _put.Invoke();

        _indicator.SetActive(mergable == null);
    }
}