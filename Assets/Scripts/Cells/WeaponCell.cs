using System;

public class WeaponCell : Cell
{
    public static Action<Weapon> WeaponSet;

    public override void Put(Mergable mergable)
    {
        base.Put(mergable);
        WeaponSet?.Invoke(Context as Weapon);
    }
}