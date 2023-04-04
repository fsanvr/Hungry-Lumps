using System;
using System.Linq;
using UnityEngine;

public class GameField : MonoBehaviour
{
    private Cell[,] _cells;
    
    public bool IsNeighbour(Vector2 position, Cell cell)
    {
        const float epsilon = 0.01f;
        return Math.Abs((position - cell.position).magnitude - 1.0f) < epsilon;
    }
    public void InitField(FoodMap map)
    {
        var level = 0;
        
        var fieldShape = new Vector2Int(map.Food.GetLength(0), map.Food.GetLength(1));
        _cells = new Cell[fieldShape.x, fieldShape.y];
        
        foreach (var x in Enumerable.Range(0, fieldShape.x))
        {
            foreach (var y in Enumerable.Range(0, fieldShape.y))
            {
                var position = new Vector2Int(x, y);
                var isFinish = map.FinishCell == position;
                var cell = InitCell(InitData.GetCellPrefab(level), position, isFinish);
                if (position != map.StartCell)
                {
                    AddFood(cell, map.Food[x, y]);
                }
            }
        }
    }

    private GameObject InitCell(GameObject prefab, Vector2Int position, bool isFinish)
    {
        var cell = Instantiate(prefab, new Vector3(position.x, position.y, 0),
            Quaternion.identity);
        var cellComponent = cell.GetComponent<Cell>();
        
        _cells[position.x, position.y] = cellComponent;
        cellComponent.position = cell.transform.position;
        cellComponent.isFinish = isFinish;
        cell.transform.parent = this.transform;

        return cell;
    }

    private void AddFood(GameObject cell, Food food)
    {
        var cellComponent = cell.GetComponent<Cell>();
        cellComponent.SetFood(food.Value);
        var foodComponent = cell.transform.GetChild(0).GetComponent<SpriteRenderer>();
        foodComponent.sprite = food.Sprite;
    }
}