using System;
using System.Linq;
using UnityEngine;
using Random = System.Random;

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
                var cell = InitCell(levelInitData.cellPrefab, position);
                var cellPosition = new Vector2Int((int)cell.transform.position.x, (int)cell.transform.position.y);
                if (cellPosition != levelInitData.playerSpawnPoint)
                {
                    AddFood(cell);
                }
            }
        }
    }

    private GameObject InitCell(GameObject prefab, Vector2Int position)
    {
        var cell = Instantiate(prefab, new Vector3(position.x, position.y, 0),
            Quaternion.identity);
        cell.SetActive(false);
        cell.SetActive(true);
        var cellComponent = cell.GetComponent<Cell>();
        
        _cells[position.x, position.y] = cellComponent;
        cellComponent.position = cell.transform.position;
        cell.transform.parent = this.transform;

        return cell;
    }

    private void AddFood(GameObject cell)
    {
        var cellComponent = cell.GetComponent<Cell>();
        cellComponent.SetFood(new Random().Next(5, 8));
        //TODO: food sprite
    }
}