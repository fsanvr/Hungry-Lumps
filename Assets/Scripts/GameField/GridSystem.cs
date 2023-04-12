using System;
using System.Linq;
using UnityEngine;

public class GridSystem : InitializableBehaviour
{
    public Cell[,] Cells { get; private set; }

    public bool IsNeighbour(Vector2 position, Cell cell)
    {
        const float epsilon = 0.01f;
        return Math.Abs((position - cell.position).magnitude - 1.0f) < epsilon;
    }

    protected override void MyInit(LevelData data)
    {
        InitField(data);
    }

    private void InitField(LevelData data)
    {
        var grid = data.Grid;
        var cellPrefab = data.CellPrefab;
        var shape = new Vector2Int(grid.Cells.GetLength(0), grid.Cells.GetLength(1));
        Cells = new Cell[shape.x, shape.y];

        foreach (var x in Enumerable.Range(0, shape.x))
        {
            foreach (var y in Enumerable.Range(0, shape.y))
            {
                var cell = grid.Cells[x, y];
                var position = cell.Position;
                var cellGO = InitCell(cellPrefab, position);
                var cellComponent = cellGO.GetComponent<Cell>();
                if (cellComponent && !cellComponent.ContainsObject() && cell.ObjectComponent is not null)
                {
                    cellComponent.SetObject((ObjectData)cell.ObjectComponent);
                }
            }
        }
    }

    private GameObject InitCell(GameObject prefab, Vector2Int position)
    {
        var cell = Instantiate(prefab, new Vector3(position.x, position.y, 0),
            Quaternion.identity);
        var cellComponent = cell.GetComponent<Cell>();
        
        Cells[position.x, position.y] = cellComponent;
        cellComponent.position = cell.transform.position;
        cell.transform.parent = this.transform;

        return cell;
    }
}