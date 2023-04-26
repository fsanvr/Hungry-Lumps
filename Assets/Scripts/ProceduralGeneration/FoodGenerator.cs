using System;
using System.Collections.Generic;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodGenerator
{
    private readonly GameObject _foodPrefab;
    private readonly Sprite[] _food;
    private GridSystem _grid;

    private AdjacencyGraph<int, Edge<int>> _graph;
    private PlayerComponent _player;

    public FoodGenerator(LevelData data, GridSystem grid, PlayerComponent player)
    {
        _foodPrefab = data.foodPrefab;
        _food = data.spritesData.food;
        _grid = grid;
        
        _graph = data.graph;
        _player = player;
    }

    public void GenerateFood()
    {
        var cells = _grid.Cells;
        var playerPosition = _player.transform.position;
        
        var foodPositions = FindPositionToFood(playerPosition);
        foreach (var position in foodPositions)
        {
            var data = GenerateFoodData();
            cells[position.x, position.y].SetFood(data);
        }
    }

    private FoodData GenerateFoodData()
    {
        var sprite = _food[Random.Range(0, _food.GetLength(0))];
        var prize = 1.0f;
        var timeToDestroy = Random.Range(9.0f, 12.0f);
        var foodData = new FoodData
        {
            Prefab = _foodPrefab,
            Sprite = sprite, 
            Prize = prize, 
            TimeToDestroy = timeToDestroy
        };

        return foodData;
    }

    private IEnumerable<Vector2Int> FindPositionToFood(Vector3 startPosition)
    {
        var shape = new Vector2Int(_grid.Cells.GetLength(0), _grid.Cells.GetLength(1));
        var rootCell = PosToIndex(shape, (int)startPosition.x, (int)startPosition.y);
        var lengths = GetPathLengthToCells(rootCell);

        var samplingInterval = shape.x <= 3 ? 3 : shape.x;
        var positions = new List<Vector2Int>();

        var count = -2;
        foreach (var (vertex, lenght) in lengths)
        {
            ++count;
            if (lenght < 2 || count % samplingInterval != 0)
            {
                continue;
            }

            var position = IndexToPos(shape, vertex);
            positions.Add(position);
        }

        return positions;
    }

    private IEnumerable<(int, int)> GetPathLengthToCells(int root)
    {
        Func<Edge<int>, double> edgeCost = edge => 1.0f;

        var shortestPaths = _graph.ShortestPathsDijkstra(edgeCost, root);
        var lengths = new List<(int, int)>();
        foreach (var vertex in _graph.Vertices)
        {
            if (vertex.Equals(root))
            {
                continue;
            }
            
            if (shortestPaths(vertex, out var path))
            {
                var length = path.Count();
                lengths.Add((vertex, length));
            }
        }

        return lengths;
    }

    private int PosToIndex(Vector2Int shape, int x, int y)
    {
        return y * shape.y + x;
    }
    
    private Vector2Int IndexToPos(Vector2Int shape, int index)
    {
        return new Vector2Int(index % shape.y, index / shape.y);
    }
}