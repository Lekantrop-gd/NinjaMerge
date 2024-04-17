using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class CellsGrid : MonoBehaviour
{
    [SerializeField] private Mergable _mergablePrefab;
    [SerializeField] private Cell _cellPreafab;
    [SerializeField] private Transform _itemsRoot;
    [SerializeField] private int _rows;
    [SerializeField] private int _columns;

    private List<Cell> _cells = new List<Cell>();

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

    [Button]
    public bool SpawnRandomWeapon()
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

        Cell randomCell = emptyCells[Random.Range(0, emptyCells.Count)];

        Mergable mergable = Instantiate(_mergablePrefab, _itemsRoot);

        mergable.transform.position = randomCell.transform.position;

        randomCell.Put(mergable);

        return true;
    }
}
