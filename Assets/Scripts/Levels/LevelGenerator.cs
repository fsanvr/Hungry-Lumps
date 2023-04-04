using System.Linq;
using UnityEngine;

public class LevelGenerator
{
    public FoodMap Generate()
    {
        Debug.Log("Generate!");
        var foodSprite = Resources.Load<Sprite>("Food/Apple");
        
        var shape = new Vector2Int(Random.Range(2, 4), Random.Range(2, 4));
        var food = new Food[shape.x, shape.y];
        var sumSatiety = 0;
            
        foreach (var x in Enumerable.Range(0, shape.x))
        {
            foreach (var y in Enumerable.Range(0, shape.y))
            {
                var satiety = Random.Range(4, 12);
                sumSatiety += satiety;
                food[x, y] = new Food
                {
                    Value = satiety,
                    Sprite = foodSprite
                };
            }
        }
            
        var minSatiety = 0;
        var maxSatiety = sumSatiety;
        var costOfMove = 4;
        var startSatiety = 4;

        return new FoodMap
        {
            Food = food,
            FinishCell = new Vector2Int(shape.x -1, shape.y - 1),
            StartCell = new Vector2Int(0, 0),
            MinSatiety = minSatiety,
            MaxSatiety = maxSatiety,
            StartSatiety = startSatiety,
            CostOfMove = costOfMove
        };
    }
}