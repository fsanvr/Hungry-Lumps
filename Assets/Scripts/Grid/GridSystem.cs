using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridSystem : InitializableBehaviour
{
    public Cell[,] Cells { get; private set; }

    public Cell GetCell(Vector2 position)
    {
        return Cells[Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y)];
    }

    public List<Cell> GetCellBetween(Vector2 pos1, Vector2 pos2)
    {
        var dx = Mathf.FloorToInt(pos2.x - pos1.x);
        var dy = Mathf.FloorToInt(pos2.y - pos1.y);
        if (dx != 0 && dy != 0)
        {
            return null;
        }

        var cells = new List<Cell>();

        if (dx == 0 && dy == 0)
        {
            var pos = new Vector2Int(
                Mathf.FloorToInt(pos1.x), 
                Mathf.FloorToInt(pos1.y)
            );
            cells.Add(Cells[pos.x, pos.y]);
        }
        else if (dx != 0 && dy == 0)
        {
            for (var x = 0; Math.Abs(x) <= Math.Abs(dx); x += Math.Sign(dx))
            {
                var pos = new Vector2Int(
                    Mathf.FloorToInt(pos1.x + x), 
                    Mathf.FloorToInt(pos1.y)
                    );
                cells.Add(Cells[pos.x, pos.y]);
            }
        }
        else if (dy != 0 && dx == 0)
        {
            for (var y = 0; Math.Abs(y) <= Math.Abs(dy); y += Math.Sign(dy))
            {
                var pos = new Vector2Int(
                    Mathf.FloorToInt(pos1.x), 
                    Mathf.FloorToInt(pos1.y + y)
                );
                cells.Add(Cells[pos.x, pos.y]);
            }
        }

        return cells;
    }
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
                if (cellComponent && !cellComponent.ContainsWall() && cell.ObjectComponent is not null)
                {
                    cellComponent.SetWall((WallData)cell.ObjectComponent);
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