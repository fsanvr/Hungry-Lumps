using UnityEngine;

public struct FoodMap
{
    public Food[,] Food;
    public Vector2Int FinishCell;
    public Vector2Int StartCell;
    
    public int MinSatiety;
    public int MaxSatiety;
    public int StartSatiety;
    public int CostOfMove;
}