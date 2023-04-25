using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms;
using UnityEngine;
using Random = UnityEngine.Random;


public class GridGenerator
{
    private readonly GameObject _wallPrefab;
    private readonly Sprite[] _cells;
    private readonly Sprite[] _walls;

    private readonly int _levelNumber;
    public GridGenerator(LevelData data, PlayerData playerData)
    {
        _wallPrefab = data.objectPrefab;
        _cells = data.spritesData.cells;
        _walls = data.spritesData.walls;

        _levelNumber = playerData.completedLevelsCount + 1;
    }
    public GenerateGrid Generate()
    {
        var maxGenerationTry = 20;
        GenerateGrid grid = GenerateGrid();
        
        while (maxGenerationTry-- > 0)
        {
            var graph = CreateGraph(grid);
            var componentsCount = graph.StronglyConnectedComponents(out var components);
            if (componentsCount == 1)
            {
                return grid;
            }

            grid = GenerateGrid();
        }

        return grid;
    }

    private GenerateGrid GenerateGrid()
    {
        var shape = GetRandomSquareShape();
        var cells = new GenerateCell[shape.x, shape.y];
        var startPosition = new Vector2Int(0, 0);

        foreach (var x in Enumerable.Range(0, shape.x))
        {
            foreach (var y in Enumerable.Range(0, shape.y))
            {
                WallData? data = null;
                if (new Vector2Int(x, y) != startPosition && Random.Range(0, 7) == 2)
                {
                    data = new WallData
                    {
                        Prefab = _wallPrefab,
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
            StartPosition = startPosition
        };
    }

    private Vector2Int GetRandomSquareShape()
    {
        var minShape = Random.Range(3, 5);
        var levelShape = Random.Range(_levelNumber - 2, _levelNumber + 5);
        var shape = Mathf.Max(minShape, levelShape);
        return new Vector2Int(shape, shape);
    }

    private AdjacencyGraph<int, Edge<int>> CreateGraph(GenerateGrid grid)
    {
        var cells = grid.Cells;
        var shape = new Vector2Int(cells.GetLength(0), cells.GetLength(1));
        var graph = new AdjacencyGraph<int, Edge<int>>();
        
        foreach (var x in Enumerable.Range(0, shape.x))
        {
            foreach (var y in Enumerable.Range(0, shape.y))
            {
                if (!cells[x, y].ObjectComponent.Equals(null))
                {
                    continue;
                }
                
                graph.AddVertex(PosToIndex(shape, x, y));
            }
        }

        foreach (var x in Enumerable.Range(0, shape.x))
        {
            foreach (var y in Enumerable.Range(0, shape.y))
            {
                if (!cells[x, y].ObjectComponent.Equals(null))
                {
                    continue;
                }
                
                if (x != shape.x - 1 && cells[x + 1, y].ObjectComponent.Equals(null))
                {
                    graph.AddEdge(new Edge<int>(
                        PosToIndex(shape, x, y),
                        PosToIndex(shape, x + 1, y)
                        )); //right
                }

                if (x != 0 && cells[x - 1, y].ObjectComponent.Equals(null))
                {
                    graph.AddEdge(new Edge<int>(
                        PosToIndex(shape, x, y),
                        PosToIndex(shape, x - 1, y)
                        )); //left
                }

                if (y != shape.y - 1 && cells[x, y + 1].ObjectComponent.Equals(null))
                {
                    graph.AddEdge(new Edge<int>(
                        PosToIndex(shape, x, y),
                        PosToIndex(shape, x, y + 1)
                        ));  //top
                }

                if (y != 0 && cells[x, y - 1].ObjectComponent.Equals(null))
                {
                    graph.AddEdge(new Edge<int>(
                        PosToIndex(shape, x, y),
                        PosToIndex(shape, x, y - 1)
                        )); //bottom
                }
            }
        }
        
        return graph;
    }

    private int PosToIndex(Vector2Int shape, int x, int y)
    {
        return y * shape.y + x;
    }
}