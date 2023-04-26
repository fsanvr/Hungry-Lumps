using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShelterView : MonoBehaviour
{
    [SerializeField] private ShelterSystem shelterSystem;
    [SerializeField] private GameObject contentView;
    [SerializeField] private GameObject petScreen;
    
    [SerializeField] private Text satietyView;
    [SerializeField] private GameObject prefabActive;
    [SerializeField] private GameObject prefabNotActive;
    
    [SerializeField] private GameObject moveSpeedProgress;
    [SerializeField] private Button moveSpeedUpgradeButton;
    [SerializeField] private GameObject moveCostProgress;
    [SerializeField] private Button moveCostUpgradeButton;
    [SerializeField] private GameObject satietyProgress;
    [SerializeField] private Button satietyUpgradeButton;
    
    private void Awake()
    {
        if (shelterSystem)
        {
            shelterSystem.ShelterChanged.AddListener(Show);
        }
    }
    
    private void OnDestroy()
    {
        
        if (shelterSystem)
        {
            shelterSystem.ShelterChanged.RemoveListener(Show);
        }
    }
    
    private void Show(ShelterData shelterData, PlayerData playerData)
    {
        SetLevel(playerData.satiety);
        ClearView();
        ConfigureActivePet(playerData);
        foreach (var pet in shelterData.activePets)
        {
            var prefab = SelectPrefab(pet, playerData);
            var petGO = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            ConfigureItem(petGO, pet);
            const bool worldPositionStays = false;
            petGO.transform.SetParent(contentView.transform, worldPositionStays);
        }
    }

    private void SetLevel(int satiety)
    {
        satietyView.text = satiety.ToString();
    }
    
    private void ClearView()
    {
        foreach (Transform item in contentView.transform)
        {
            Destroy(item.gameObject);
        }
    }
    
    private void ConfigureActivePet(PlayerData playerData)
    {
        var activePet = playerData.activePet;
        foreach (Transform child in petScreen.transform)
        {
            if (child.name == "Pet")
            {
                foreach (Transform childPet in child.transform)
                {
                    if (childPet.name == "Sprite")
                    {
                        var spriteComponent = childPet.GetComponent<Image>();
                        spriteComponent.sprite = activePet.sprite;
                    }
                    if (childPet.name == "Name")
                    {
                        var nameComponent = childPet.GetComponent<TextMeshProUGUI>();
                        nameComponent.text = activePet.petName;
                    }
                }
            }
        }
        
        ClearColorsInButtons();
        ConfigureSatiety(playerData);
        ConfigureMoveCost(playerData);
        ConfigureMoveSpeed(playerData);
    }
    
    private void ClearColorsInButtons()
    {
        var satietyButtonImage = satietyUpgradeButton.gameObject.GetComponent<Image>();
        satietyButtonImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        var moveSpeedButtonImage = satietyUpgradeButton.gameObject.GetComponent<Image>();
        moveSpeedButtonImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        var moveCostButtonImage = satietyUpgradeButton.gameObject.GetComponent<Image>();
        moveCostButtonImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    private void ConfigureSatiety(PlayerData playerData)
    {
        var activePet = playerData.activePet;
        
        var scale = satietyProgress.transform.localScale;
        scale.x = (1.0f * activePet.satietyComponent.upgradeStepCurrent) / activePet.satietyComponent.upgradeSteps;
        satietyProgress.transform.localScale = scale;

        if (activePet.satietyComponent.IsMaxUpgradeStep())
        {
            satietyUpgradeButton.gameObject.SetActive(false);
        }
        else if (activePet.satietyComponent.GetUpgradeCost() > playerData.satiety)
        {
            satietyUpgradeButton.interactable = false;
            
            var buttonImage = satietyUpgradeButton.gameObject.GetComponent<Image>();
            buttonImage.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
            var buttonIconImage = satietyUpgradeButton.transform.GetChild(0).GetComponent<Image>();
            buttonIconImage.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
        }
    }
    
    private void ConfigureMoveSpeed(PlayerData playerData)
    {
        var activePet = playerData.activePet;
        
        var scale = moveSpeedProgress.transform.localScale;
        scale.x = (1.0f * activePet.moveComponent.moveData.moveDurationUpgradeStepCurrent) / activePet.moveComponent.moveData.moveDurationUpgradeSteps;
        moveSpeedProgress.transform.localScale = scale;

        if (activePet.moveComponent.IsMoveSpeedMaxUpgradeStep())
        {
            moveSpeedUpgradeButton.gameObject.SetActive(false);
        }
        else if (activePet.moveComponent.GetMoveSpeedUpgradeCost() > playerData.satiety)
        {
            moveSpeedUpgradeButton.interactable = false;
            
            var buttonImage = moveSpeedUpgradeButton.gameObject.GetComponent<Image>();
            buttonImage.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
            var buttonIconImage = moveSpeedUpgradeButton.transform.GetChild(0).GetComponent<Image>();
            buttonIconImage.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
        }
    }
    
    private void ConfigureMoveCost(PlayerData playerData)
    {
        var activePet = playerData.activePet;
        
        var scale = moveCostProgress.transform.localScale;
        scale.x = (1.0f * activePet.moveComponent.moveData.moveCostUpgradeStepCurrent) / activePet.moveComponent.moveData.moveCostUpgradeSteps;
        moveCostProgress.transform.localScale = scale;

        if (activePet.moveComponent.IsMoveCostMaxUpgradeStep())
        {
            moveCostUpgradeButton.gameObject.SetActive(false);
        }
        else if (activePet.moveComponent.GetMoveCostUpgradeCost() > playerData.satiety)
        {
            moveCostUpgradeButton.interactable = false;
            
            var buttonImage = moveCostUpgradeButton.gameObject.GetComponent<Image>();
            buttonImage.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
            var buttonIconImage = moveCostUpgradeButton.transform.GetChild(0).GetComponent<Image>();
            buttonIconImage.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
        }
    }

    private GameObject SelectPrefab(Pet pet, PlayerData playerData)
    {
        return playerData.activePet.Equals(pet) ? prefabActive : prefabNotActive;
    }
    
    private static void ConfigureItem(GameObject go, Pet pet)
    {
        var petComponent = go.GetComponent<ShelterItemComponent>();
        if (petComponent)
        {
            petComponent.pet = pet;
        }
        
        foreach (Transform child in go.transform)
        {
            if (child.name == "Sprite")
            {
                var spriteComponent = child.GetComponent<Image>();
                spriteComponent.sprite = pet.sprite;
            }

            if (child.name == "Name")
            {
                var nameComponent = child.GetComponent<TextMeshProUGUI>();
                nameComponent.text = pet.petName;
            }
        }
    }
}