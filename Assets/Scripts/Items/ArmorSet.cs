using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Set", menuName = "Items/ArmorSet")]
public class ArmorSet : ScriptableObject
{
    [SerializeField] private ArmorLink[] _armorLinks;

    public ArmorLink[] ArmorLinks => _armorLinks;

    [Serializable]
    public struct ArmorLink
    {
        [SerializeField] private int _id;
        [SerializeField] private Armor _armor;
        [SerializeField] private Transform _hatModel;
        [SerializeField] private Transform _armorModel;

        public int Id => _id;
        public Armor Armor => _armor;
        public Transform HatModel => _hatModel;
        public Transform ArmorModel => _armorModel;
    }
}