using System.Linq;
using UnityEngine;

public class GridGenerator
{
    public GenerateGrid Generate()
    {
        var cells = new GenerateCell[3, 3];
        foreach (var x in Enumerable.Range(0, cells.GetLength(0)))
        {
            foreach (var y in Enumerable.Range(0, cells.GetLength(1)))
            {
                WallData? data = null;
                if (Random.Range(0, 7) == 2)
                {
                    data = new WallData
                    {
                        Prefab = Resources.Load<GameObject>("Prefabs/ObjectPrefab"),
                        Sprite = Resources.Load<Sprite>("Food/Lemon"),
                        Passable = false
                    };
                }
                cells[x, y] = new GenerateCell
                {
                    ObjectComponent = data,
                    Position = new Vector2Int(x, y)
                };
            }
        }
        
        return new GenerateGrid
        {
            Cells = cells,
            StartPosition = new Vector2Int(1, 1)
        };
    }
}