using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelSystem: MonoBehaviour
{
    [SerializeField] private int menuSceneIndex = 0;
    [SerializeField] private int gameSceneIndex = 1;
    
    [SerializeField] private Dictionary<int, FoodMap> _levels;
    [SerializeField] private LevelData data;
    
    private LevelGenerator _generator;

    private void Awake()
    {
        _levels = new Dictionary<int, FoodMap>();
        _generator = new LevelGenerator();
    }

    public void EnableMenu()
    {
        SceneManager.LoadScene(menuSceneIndex);
    }

    public void EnableLevel(int level)
    {
        data.EnabledLevelKey = level;
        SceneManager.LoadScene(gameSceneIndex);
    }

    public void EnableNewLevel()
    {
        var levelKey = Random.Range(Int32.MinValue, Int32.MaxValue);
        var levelValue = _generator.Generate();
            
        _levels.Add(levelKey, levelValue);
        data.EnabledLevelKey = levelKey;
        data.EnabledLevelMap = levelValue;
        
        SceneManager.LoadScene(gameSceneIndex);
    }
 }