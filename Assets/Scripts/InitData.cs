using System.Linq;
using UnityEngine;

public static class InitData
{
    private static int _shape = 1;
    private static readonly FoodMap[] _maps;
    private static GameObject _cellPrefab;

    static InitData()
    {
        _cellPrefab = Resources.Load<GameObject>("Prefabs/Cell");
        var foodSprite = Resources.Load<Sprite>("Food/Apple");

        _maps = new FoodMap[_shape];
        foreach (var mapIndex in Enumerable.Range(0, _maps.GetLength(0)))
        {
            var shape = new Vector2Int(Random.Range(2, 5), Random.Range(2, 5));
            var food = new Food[shape.x, shape.y];
            var sumSatiety = 0;
            
            foreach (var x in Enumerable.Range(0, shape.x))
            {
                foreach (var y in Enumerable.Range(0, shape.y))
                {
                    var satiety = Random.Range(4, 10);
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
            var startSatiety = 5;

            _maps[mapIndex] = new FoodMap
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

    public static FoodMap GetFoodMap(int level)
    {
        return _maps[level];
    }

    public static GameObject GetCellPrefab(int level)
    {
        return _cellPrefab;
    }
}