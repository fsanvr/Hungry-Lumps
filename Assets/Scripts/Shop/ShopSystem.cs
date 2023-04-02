using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    public readonly ShopUpdateEvent ShopChanged = new ShopUpdateEvent();
    [SerializeField] private ShopData shopData;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private UIInputSystem input;

    private void Awake()
    {
        if (input)
        {
            input.MouseClicked.AddListener(OnClicked);
        }
    }

    private void OnDestroy()
    {
        if (input)
        {
            input.MouseClicked.RemoveListener(OnClicked);
        }
    }

    private void Start()
    {
        UpdateShop();
    }
    
    private void OnChanged()
    {
        ShopChanged.Invoke(shopData);
    }

    private void OnClicked(GameObject coll)
    {
        if (IsShopItem(coll.gameObject))
        {
            var itemComponent = coll.GetComponent<ShopItemComponent>();
            ProcessClick(itemComponent.item);
        }
    }
    
    private static bool IsShopItem(GameObject go)
    {
        return go.GetComponent<ShopItemComponent>();
    }
    
    private void ProcessClick(ShopItem item)
    {
        if (item.status.Equals(ShopItemStatus.Active) ||
            item.status.Equals(ShopItemStatus.Unavailable))
        {
            return;
        }

        if (item.status.Equals(ShopItemStatus.Purchased))
        {
            SetActive(item);
        }

        if (item.status.Equals(ShopItemStatus.NotPurchased))
        {
            Purchase(item);
        }
    }

    private void SetActive(ShopItem activated)
    {
        foreach (var item in shopData.items)
        {
            if (item.status.Equals(ShopItemStatus.Active))
            {
                item.status = ShopItemStatus.Purchased;
            }
        }

        activated.status = ShopItemStatus.Active;
        OnChanged();
    }
    
    private void Purchase(ShopItem purchased)
    {
        if (playerData.satiety < purchased.purchasePrice)
        {
            return;
        }
        
        playerData.satiety -= purchased.purchasePrice;
        SetPurchased(purchased);
    }

    private void SetPurchased(ShopItem purchased)
    {
        purchased.status = ShopItemStatus.Purchased;
        OnChanged();
    }
    
    private void SetNotPurchased(ShopItem purchased)
    {
        purchased.status = ShopItemStatus.NotPurchased;
        OnChanged();
    }
    
    private void SetUnavailable(ShopItem purchased)
    {
        purchased.status = ShopItemStatus.Unavailable;
        OnChanged();
    }

    private void UpdateShop()
    {
        foreach (var item in shopData.items)
        {
            if (item.status.Equals(ShopItemStatus.Unavailable) &&
                playerData.satiety >= item.purchasePrice)
            {
                SetNotPurchased(item);
            }

            if (item.status.Equals(ShopItemStatus.NotPurchased) &&
                playerData.satiety < item.purchasePrice)
            {
                SetUnavailable(item);
            }
        }
        OnChanged();
    }
}