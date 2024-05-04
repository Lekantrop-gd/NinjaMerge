using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CellsGrid : MonoBehaviour
{
    [SerializeField] private Weapon _weaponPrefab;
    [SerializeField] private Armor _armorPrefab;
    [SerializeField] private WeaponSet _weaponSet;
    [SerializeField] private ArmorSet _armorSet;
    [SerializeField] private WeaponCell _weaponCell;
    [SerializeField] private ArmorCell _armorCell;
    [SerializeField] private Cell _cellPreafab;
    [SerializeField] private Transform _itemsRoot;
    
    [SerializeField] private int _rows;
    [SerializeField] private int _columns;

    public readonly string CellsKey = nameof(CellsKey);
    public readonly string WeaponKey = nameof(WeaponKey);
    public readonly string ArmorKey = nameof(ArmorKey);

    private List<Cell> _cells = new List<Cell>();

    [Serializable]
    public class SavingData
    {
        public List<string> data = new List<string>();
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey(WeaponKey))
        {
            if (PlayerPrefs.GetString(WeaponKey) != null)
            {
                for (int x = 0; x < _weaponSet.WeaponLinks.Length; x++)
                {
                    if (PlayerPrefs.GetString(WeaponKey) == _weaponSet.WeaponLinks[x].Weapon.name)
                    {
                        Mergable mergable = Instantiate(_weaponSet.WeaponLinks[x].Weapon, Vector3.zero, Quaternion.identity);

                        mergable.transform.position = _weaponCell.transform.position;

                        _weaponCell.Put(mergable);
                    }
                }
            }
        }

        if (PlayerPrefs.HasKey(ArmorKey))
        {
            if (PlayerPrefs.GetString(ArmorKey) != null)
            {
                for (int x = 0; x < _armorSet.ArmorLinks.Length; x++)
                {
                    if (PlayerPrefs.GetString(ArmorKey) == _armorSet.ArmorLinks[x].Armor.name)
                    {
                        Mergable mergable = Instantiate(_armorSet.ArmorLinks[x].Armor, Vector3.zero, Quaternion.identity);

                        mergable.transform.position = _armorCell.transform.position;

                        _armorCell.Put(mergable);
                    }
                }
            }
        }

        if (PlayerPrefs.HasKey(CellsKey))
        {
            SavingData data = JsonUtility.FromJson<SavingData>(PlayerPrefs.GetString(CellsKey));

            for (int x = 0; x < data.data.Count; x++)
            {
                if (data.data[x] != "" || data.data != null)
                {
                    for (int y = 0; y < _weaponSet.WeaponLinks.Length; y++)
                    {
                        if (data.data[x] == _weaponSet.WeaponLinks[y].Weapon.name)
                        {
                            SpawnItem(_weaponSet.WeaponLinks[y].Weapon, x);
                            break;
                        }
                    }
                    for (int y = 0; y < _armorSet.ArmorLinks.Length; y++)
                    {
                        if (data.data[x] == _armorSet.ArmorLinks[y].Armor.name)
                        {
                            SpawnItem(_armorSet.ArmorLinks[y].Armor, x);
                        }
                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        Interactor.Merged += Save;
    }

    private void OnDisable()
    {
        Interactor.Merged -= Save;
    }

    public void Save()
    {
        SavingData data = new SavingData();
        for (int x = 0; x < _cells.Count; x++)
        {
            data.data.Add(null);
        }

        for (int x = 0; x < _cells.Count; x++)
        {
            if (_cells[x].Context != null)
            {
                if (_cells[x].Context.name.Contains("(Clone)"))
                {
                    data.data[x] = _cells[x].Context.name.Replace("(Clone)", "");
                }
            }
        }

        PlayerPrefs.SetString(CellsKey, JsonUtility.ToJson(data));
        
        if (_weaponCell.Context != null)
            PlayerPrefs.SetString(WeaponKey, _weaponCell.Context.name.Replace("(Clone)", ""));
        else
            PlayerPrefs.SetString(WeaponKey, null);

        if (_armorCell.Context != null)
            PlayerPrefs.SetString(ArmorKey, _armorCell.Context.name.Replace("(Clone)", ""));
        else
            PlayerPrefs.SetString(ArmorKey, null);

        PlayerPrefs.Save();
    }

    [Button]
    private void Align()
    {
        _cells.Clear();
        for (int child = transform.childCount - 1; child >= 0; child--)
        {
            DestroyImmediate(transform.GetChild(child).gameObject);
        }

        for (int child = _itemsRoot.childCount - 1; child >= 0; child--)
        {
            DestroyImmediate(_itemsRoot.GetChild(child).gameObject);
        }

        for (int row = 0; row < _rows; row++)
        {
            for (int column = 0; column < _columns; column++)
            {
                Cell cell = Instantiate(_cellPreafab, transform);
                cell.transform.localPosition = new Vector3(-column, 0, row);
                _cells.Add(cell);
            }
        }
    }

    public bool SpawnWeapon()
    {
        return SpawnItem(_weaponPrefab);
    }

    [Button]
    public bool SpawnArmor()
    {
        return SpawnItem(_armorPrefab);
    }

    public bool SpawnItem(Mergable item)
    {
        if (_cells == null || _cells.Count < 1)
        {
            Align();
        }

        List<Cell> emptyCells = new List<Cell>();
        for (int x = 0; x < _cells.Count; x++)
        {
            if (_cells[x].Context == null)
            {
                emptyCells.Add(_cells[x]);
            }
        }

        if (emptyCells.Count == 0)
        {
            return false;
        }

        Cell randomCell = emptyCells[UnityEngine.Random.Range(0, emptyCells.Count)];

        Mergable mergable = Instantiate(item, _itemsRoot);

        mergable.transform.position = randomCell.transform.position;

        randomCell.Put(mergable);

        Save();

        return true;
    }

    public void SpawnItem(Mergable item, int position)
    {
        if (_cells == null || _cells.Count < 1)
        {
            Align();
        }

        Mergable mergable = Instantiate(item, _itemsRoot);

        mergable.transform.position = _cells[position].transform.position;

        _cells[position].Put(mergable);
    }
}