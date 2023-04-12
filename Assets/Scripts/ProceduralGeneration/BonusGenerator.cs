using UnityEngine;

public class BonusGenerator
{
    private GridSystem _grid;

    public BonusGenerator(GridSystem grid)
    {
        _grid = grid;
    }

    public void GenerateBonus()
    {
        var cells = _grid.Cells;
        var prefab = Resources.Load<GameObject>("Prefabs/BonusPrefab");
        var sprite = Resources.Load<Sprite>("Food/Roll");
        var bonusComponent = new BonusData
        {
            Prefab = prefab,
            Sprite = sprite,
            Buff = new Buff(x => 0.1f * x, "SatietySystem.DecreaseSpeed")
        };
        cells[2,2].SetBonus(bonusComponent);
    }
}