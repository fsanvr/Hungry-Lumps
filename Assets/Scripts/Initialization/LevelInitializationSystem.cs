using System.Collections.Generic;
using UnityEngine;

public class LevelInitializationSystem : MonoBehaviour
{
    [SerializeField] private List<InitializableBehaviour> systemsToInit = new List<InitializableBehaviour>();
    [SerializeField] private LevelData levelData;
    private void Start()
    {
        InitSystems();
    }

    private void InitSystems()
    {
        foreach (var system in systemsToInit)
        {
            system.Init(levelData);
        }
    }
}