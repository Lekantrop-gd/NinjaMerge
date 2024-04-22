using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level System", menuName = "LevelSystem")]
public class LevelSystem : ScriptableObject
{
    [SerializeField] private Level[] _levels;

    [Serializable]
    private struct Level
    {
        [SerializeField] private EnemyInfo[] _enemies;
    }

    [Serializable]
    private struct EnemyInfo
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Armor _armor;
    }
}
