using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level System", menuName = "LevelSystem")]
public class LevelSystem : ScriptableObject
{
    [SerializeField] private Level[] _levels;

    public Level[] Levels => _levels;

    [Serializable]
    public struct Level
    {
        [SerializeField] private EnemyInfo[] _enemies;

        public EnemyInfo[] Enemies => _enemies;
    }

    [Serializable]
    public struct EnemyInfo
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Armor _armor;

        public Weapon Weapon => _weapon;
        public Armor Armor => _armor;
    }
}
