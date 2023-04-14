using System.Collections;
using UnityEngine;

public class BonusGenerateSystem : InitializableBehaviour
{
    [SerializeField] private GridSystem grid;
    [SerializeField] private float timeToGenerate = 1.0f;
    private BonusGenerator _generator;

    protected override void MyInit(LevelData data)
    {
        _generator = new BonusGenerator(data, grid);
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        yield return new WaitForSeconds(timeToGenerate);
        _generator.GenerateBonus();
        
    }
}