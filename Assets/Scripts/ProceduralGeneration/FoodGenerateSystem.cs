using System.Collections;
using UnityEngine;

public class FoodGenerateSystem : InitializableBehaviour
{
    [SerializeField] private GridSystem grid;
    [SerializeField] private float timeToGenerate = 2.0f;
    private FoodGenerator _generator;

    protected override void MyInit(LevelData data)
    {
        _generator = new FoodGenerator(grid);
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        yield return new WaitForSeconds(timeToGenerate);
        _generator.GenerateFood();
        
    }
}