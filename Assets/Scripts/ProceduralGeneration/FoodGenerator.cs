using UnityEngine;

public class FoodGenerator
{
    private GameField _grid;

    public FoodGenerator(GameField grid)
    {
        _grid = grid;
    }

    public void GenerateFood()
    {
        var cells = _grid.GetCells();
        var prefab = Resources.Load<GameObject>("Prefabs/FoodPrefab");
        var sprite = Resources.Load<Sprite>("Food/Avocado");
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