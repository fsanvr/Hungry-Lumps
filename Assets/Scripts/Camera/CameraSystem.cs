using System;
using UnityEngine;

public class CameraSystem : InitializableBehaviour
{
    protected override void MyInit(LevelData data)
    {
        CenterCamera(data);
    }

    private static void CenterCamera(LevelData data)
    {
        var grid = data.Grid.Cells;
        var gridShape = new Vector2Int(grid.GetLength(0) - 1, grid.GetLength(1) - 1);
        var center = new Vector3(gridShape.x / 2.0f, gridShape.y / 2.0f);
        var avgShape = (gridShape.x + gridShape.y) / 2.0f;
        
        SetCameraPosition(center);
        SetCameraSize(avgShape >= 4.0f ? avgShape : 4.0f);
    }

    private static void SetCameraPosition(Vector2 position)
    {
        if (Camera.main is null)
        {
            return;
        }
        
        var cameraPositionZ = Camera.main.transform.position.z;
        Camera.main.transform.position = new Vector3(position.x, position.y, cameraPositionZ);
    }

    private static void SetCameraSize(float size)
    {
        if (Camera.main is null)
        {
            return;
        }

        Camera.main.orthographicSize = size;
    }
}