using UnityEngine;

public class FoodGenerator
{
    private readonly GameObject _foodPrefab;
    private readonly Sprite[] _food;
    private GridSystem _grid;

    public FoodGenerator(LevelData data, GridSystem grid)
    {
        _foodPrefab = data.foodPrefab;
        _food = data.spritesData.food;
        _grid = grid;
    }

    public void GenerateFood()
    {
        var cells = _grid.Cells;
        var prefab = _foodPrefab;
        var sprite = _food[Random.Range(0, _food.GetLength(0))];
        var prize = 1.0f;
        var timeToDestroy = 3.0f;
        var foodComponent = new FoodData
        {
            Prefab = prefab,
            Sprite = sprite, 
            Prize = prize, 
            TimeToDestroy = timeToDestroy
        };
        cells[0,0].SetFood(foodComponent);
    }
}