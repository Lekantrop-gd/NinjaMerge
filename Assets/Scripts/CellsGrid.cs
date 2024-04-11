using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellsGrid : MonoBehaviour
{
    [SerializeField] private Mergable _mergablePrefab;
    [SerializeField] private Cell _cellPreafab;
    [SerializeField] private int _rows;
    [SerializeField] private int _columns;

    private List<Cell> _cells;

    [Button]
    private void Align()
    {
        for (int row = 0; row < _rows; row++)
        {
            for (int column = 0; column < _columns; column++)
            {
                Cell cell = Instantiate(_cellPreafab, transform);
                cell.transform.localPosition = new Vector3(column, 0, row);
                _cells.Add(cell);
            }
        }
    }
}
