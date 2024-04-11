using NaughtyAttributes;
using UnityEngine;

public class CellsGrid : MonoBehaviour
{
    [SerializeField] private Mergable _prefab;
    [SerializeField] private Cell[] _cells;
    [SerializeField] private int _rows;
    [SerializeField] private int _columns;

    [Button]
    private void Align()
    {
        if (_cells == null)
            return;

        int counter = 0;

        for (int row = 0; row < _rows; row++)
        {
            for (int column = 0; column < _columns; column++)
            {
                _cells[counter].transform.position = new Vector3(column, 0, row);
                counter++;
            }
        }
    }

    [Button]
    public void AddItem()
    {
        Mergable item = Instantiate(_prefab, transform);

        Cell randomCell;
        do
        {
            randomCell = _cells[Random.Range(0, _cells.Length)];
        }
        while (randomCell.Empty);

        randomCell.Put(item);
    }
}
