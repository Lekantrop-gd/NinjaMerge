using UnityEngine;

public class WeaponCell : Cell
{
    public Weapon Weapon => (Weapon)Context;
}