using System;
using System.Linq;
using UnityEngine;

public class CheckWinConditionSystem : InitializableBehaviour
{
    [SerializeField] private GridSystem grid;
    [SerializeField] private LevelEndSystem levelEndSystem;
    [SerializeField] private PlayerData playerData;

    private int _foodCount;
    private int _levelIndex;

    private Func<(int, int), int> winSatietyFunc = levelFood => (int)Mathf.Pow((float)levelFood.Item1, Mathf.Log10(levelFood.Item2)) + levelFood.Item1;

    protected override void MyInit(LevelData data)
    {
        _foodCount = FoodCoundInCells();
        _levelIndex = playerData.completedLevelsCount + 1;
    }

    protected override void MyUpdate()
    {
        base.MyUpdate();
        
        CheckWin();
    }

    private void CheckWin()
    {
        if (IsNotFoodInCells())
        {
            Win();
        }
    }

    private void Win()
    {
        if (levelEndSystem.IsLevelEnded())
        {
            return;
        }
        
        var winSatiety = winSatietyFunc((_levelIndex, _foodCount));
        playerData.satiety += winSatiety;
        playerData.completedLevelsCount++;
        
        levelEndSystem.LevelWin(winSatiety);
    }

    private bool IsFoodInCells()
    {
        return grid.Cells.Cast<Cell>().Any(cell => cell.ContainsFood());
    }

    private bool IsNotFoodInCells()
    {
        return !IsFoodInCells();
    }

    private int FoodCoundInCells()
    {
        return grid.Cells.Cast<Cell>().Aggregate(0, (current, cell) => cell.ContainsFood() ? current + 1 : current);
    }
}