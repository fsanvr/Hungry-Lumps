using UnityEngine;

[System.Serializable]
public struct GenerateGrid
{
    public GenerateCell[,] Cells;
    public Vector2Int StartPosition;
}