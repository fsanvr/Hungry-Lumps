using System;
using System.Linq;
using UnityEngine;

public class GameField : MonoBehaviour
{
    public LevelInitData levelInitData;
    private Cell[,] _cells;
    
    public bool IsNeighbour(Vector2 position, Cell cell)
    {
        const float epsilon = 0.01f;
        return Math.Abs((position - cell.position).magnitude - 1.0f) < epsilon;
    }
    private void Start()
    {
        InitField();
    }

    private void InitField()
    {
        var fieldShape = levelInitData.gameFieldShape;
        _cells = new Cell[fieldShape.x, fieldShape.y];
        foreach (var x in Enumerable.Range(0, fieldShape.x))
        {
            foreach (var y in Enumerable.Range(0, fieldShape.y))
            {
                var position = new Vector2Int(x, y);
                InitCell(levelInitData.cellPrefab, position);
            }
        }
    }

    private void InitCell(GameObject prefab, Vector2Int position)
    {
        var cell = Instantiate(prefab, new Vector3(position.x, position.y, 0),
            Quaternion.identity);
        var cellComponent = cell.GetComponent<Cell>();
        
        _cells[position.x, position.y] = cellComponent;
        cellComponent.position = cell.transform.position;
        cell.transform.parent = this.transform;
    }
}