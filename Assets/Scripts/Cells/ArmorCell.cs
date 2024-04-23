using System;

public class ArmorCell : Cell
{
    public static Action<Armor> ArmorSet;

    public override void Put(Mergable mergable)
    {
        base.Put(mergable);
        ArmorSet?.Invoke(Context as Armor);
    }
}