using UnityEngine;

public class BonusGenerator
{
    private readonly GameObject _bonusPrefab;
    private readonly Sprite[] _bonuses;
    private GridSystem _grid;

    public BonusGenerator(LevelData data,GridSystem grid)
    {
        _bonusPrefab = data.bonusPrefab;
        _bonuses = data.spritesData.bonuses;
        _grid = grid;
    }

    public void GenerateBonus()
    {
        var cells = _grid.Cells;
        var prefab = _bonusPrefab;
        var sprite = _bonuses[Random.Range(0, _bonuses.GetLength(0))];
        var bonusComponent = new BonusData
        {
            Prefab = prefab,
            Sprite = sprite,
            Buff = new Buff(x => 0.1f * x, "SatietySystem.DecreaseSpeed")
        };
        cells[2,2].SetBonus(bonusComponent);
    }
}