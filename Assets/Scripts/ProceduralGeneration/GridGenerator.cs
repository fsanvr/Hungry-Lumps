using System.Linq;
using UnityEngine;

public class GridGenerator
{
    private readonly GameObject _cellPrefab;
    private readonly Sprite[] _cells;
    private readonly Sprite[] _walls;
    public GridGenerator(LevelData data)
    {
        _cellPrefab = data.cellPrefab;
        _cells = data.spritesData.cells;
        _walls = data.spritesData.walls;
    }
    public GenerateGrid Generate()
    {
        var cells = new GenerateCell[5, 5];
        foreach (var x in Enumerable.Range(0, cells.GetLength(0)))
        {
            foreach (var y in Enumerable.Range(0, cells.GetLength(1)))
            {
                WallData? data = null;
                if (Random.Range(0, 7) == 2)
                {
                    data = new WallData
                    {
                        Prefab = _cellPrefab,
                        Sprite = _walls[Random.Range(0, _walls.GetLength(0))],
                        Passable = false
                    };
                }
                cells[x, y] = new GenerateCell
                {
                    ObjectComponent = data,
                    Position = new Vector2Int(x, y),
                    Sprite = _cells[Random.Range(0, _cells.GetLength(0))]
                };
            }
        }
        
        return new GenerateGrid
        {
            Cells = cells,
            StartPosition = new Vector2Int(0, 0)
        };
    }
}