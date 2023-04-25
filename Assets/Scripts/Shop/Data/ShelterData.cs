using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu]
public class ShelterData : ScriptableObject
{
    [SerializeField] public List<Pet> allPets;
    [SerializeField] public List<int> levelsForPets;
    [SerializeField] public List<Pet> activePets;

    //[SerializeField] public List<int> costOfUpgrade;
    public Func<int, int> costOfUpgrade = x => Mathf.FloorToInt(5 * Mathf.Exp(Mathf.Pow(x, 0.33f)));
    
    [SerializeField] public int maxShelterLevel;
    [SerializeField] public int currentShelterLevel;
    [SerializeField] public int currentPrestigeLevel;
}