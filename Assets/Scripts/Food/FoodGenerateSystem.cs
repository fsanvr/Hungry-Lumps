using System;
using System.Collections;
using UnityEngine;

public class FoodGenerateSystem : MonoBehaviour
{
    [SerializeField] private GameField grid;
    private FoodGenerator _generator;
    [SerializeField] private float timeToGenerate = 2.0f;

    private void Start()
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