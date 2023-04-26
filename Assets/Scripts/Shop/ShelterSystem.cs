using System;
using UnityEngine;

public class ShelterSystem : MonoBehaviour
{
    public readonly ShelterUpdateEvent ShelterChanged = new ShelterUpdateEvent();
    [SerializeField] private MenuSystem menuSystem;
    [SerializeField] private GameObject petScreen;
    [SerializeField] private UIInputSystem input;
    
    [SerializeField] private ShelterData shelterData;
    [SerializeField] private PlayerData playerData;

    private void Awake()
    {
        if (shelterData.allPets.Count != shelterData.levelsForPets.Count)
        {
            Debug.Log("The number of pets and the number of levels for them do not match!");
            throw new ArgumentException();
        }
        
        if (input)
        {
            input.MouseClicked.AddListener(OnClicked);
        }
    }

    private void Start()
    {
        Updated();
    }

    private void OnDestroy()
    {
        if (input)
        {
            input.MouseClicked.RemoveListener(OnClicked);
        }
    }

    public void UpgradeSatiety()
    {
        ref var satietyComponent = ref playerData.activePet.satietyComponent;
        if (satietyComponent.GetUpgradeCost() > playerData.satiety)
        {
            return;
        }

        playerData.satiety -= satietyComponent.GetUpgradeCost();
        satietyComponent.Upgrade();
        Updated();
    }
    
    public void UpgradeMoveSpeed()
    {
        ref var moveComponent = ref playerData.activePet.moveComponent;
        if (moveComponent.GetMoveSpeedUpgradeCost() > playerData.satiety)
        {
            return;
        }

        playerData.satiety -= moveComponent.GetMoveSpeedUpgradeCost();
        moveComponent.UpgradeMoveSpeed();
        Updated();
    }
    
    public void UpgradeMoveCost()
    {
        ref var moveComponent = ref playerData.activePet.moveComponent;
        if (moveComponent.GetMoveCostUpgradeCost() > playerData.satiety)
        {
            return;
        }

        playerData.satiety -= moveComponent.GetMoveCostUpgradeCost();
        moveComponent.UpgradeMoveCost();
        Updated();
    }

    public void UpgradeShelter()
    {
        var totalLevel = shelterData.currentShelterLevel + shelterData.currentPrestigeLevel;
        var upgradeCost = shelterData.costOfUpgrade(totalLevel);
        if (playerData.satiety < upgradeCost)
        {
            Debug.Log("Not enough money to upgrade shelter");
            return;
        }

        playerData.satiety -= upgradeCost;
        if (shelterData.currentShelterLevel == shelterData.maxShelterLevel)
        {
            shelterData.currentPrestigeLevel++;
        }
        else
        {
            shelterData.currentShelterLevel++;
            if (shelterData.levelsForPets.Contains(shelterData.currentShelterLevel))
            {
                var index = shelterData.levelsForPets.IndexOf(shelterData.currentShelterLevel);
                var pet = shelterData.allPets[index];
                shelterData.activePets.Add(pet);
            }
        }
        
        Updated();
    }
    
    private void OnClicked(GameObject coll)
    {
        if (IsPet(coll.gameObject))
        {
            var itemComponent = coll.GetComponent<ShelterItemComponent>();
            ProcessClick(itemComponent.pet);
        }
    }
    
    private static bool IsPet(GameObject go)
    {
        return go.GetComponent<ShelterItemComponent>();
    }
    
    private void ProcessClick(Pet pet)
    {
        if (IsNotActivePet(pet))
        {
            SetActive(pet);
        }
        else if (IsActivePet(pet))
        {
            menuSystem.ActiveOnly(petScreen);
        }
    }

    private bool IsActivePet(Pet pet)
    {
        return playerData.activePet.Equals(pet);
    }

    private bool IsNotActivePet(Pet pet)
    {
        return !IsActivePet(pet);
    }

    private void SetActive(Pet pet)
    {
        playerData.activePet = pet;
        Updated();
    }

    private void Updated()
    {
        ShelterChanged.Invoke(shelterData, playerData);
    }
}