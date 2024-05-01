using System;
using UnityEngine;

public class ArmorCell : Cell
{
    [SerializeField] private GameObject _indicator;

    public static Action<Armor> ArmorSet;

    public override void Put(Mergable mergable)
    {
        base.Put(mergable);
        ArmorSet?.Invoke(Context as Armor);

        _indicator.SetActive(mergable == null);
    }
}