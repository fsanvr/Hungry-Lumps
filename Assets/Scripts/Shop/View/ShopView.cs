using UnityEngine;
using UnityEngine.UI;
using Transform = UnityEngine.Transform;

public class ShopView : MonoBehaviour
{
    [SerializeField] private string shopSystemName = "ShopSystem";
    [SerializeField] private GameObject contentView;
    [SerializeField] private GameObject prefabActive;
    [SerializeField] private GameObject prefabNotPurchased;
    [SerializeField] private GameObject prefabPurchased;
    [SerializeField] private GameObject prefabUnavailable;

    private void Awake()
    {
        var shop = GameObject.Find(shopSystemName).GetComponent<ShopSystem>();
        if (shop)
        {
            shop.GetComponent<ShopSystem>().ShopChanged.AddListener(Show);
        }
    }

    private void OnDestroy()
    {
        
        var shop = GameObject.Find(shopSystemName);
        if (shop)
        {
            shop.GetComponent<ShopSystem>().ShopChanged.RemoveListener(Show);
        }
    }

    private void Show(ShopData data)
    {
        ClearView();
        foreach (var item in data.items)
        {
            var prefab = SelectPrefab(item);
            var itemGO = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            ConfigureItem(itemGO, item);
            itemGO.transform.SetParent(contentView.transform);
        }
    }
    
    private GameObject SelectPrefab(ShopItem item)
    {
        return item.status switch
        {
            ShopItemStatus.Active => prefabActive,
            ShopItemStatus.Unavailable => prefabUnavailable,
            ShopItemStatus.Purchased => prefabPurchased,
            _ => prefabNotPurchased
        };
    }

    private static void ConfigureItem(GameObject go, ShopItem data)
    {
        var itemComponent = go.GetComponent<ShopItemComponent>();
        if (itemComponent)
        {
            itemComponent.item = data;
        }
        
        foreach (Transform child in go.transform)
        {
            if (child.name == "Sprite")
            {
                var spriteComponent = child.GetComponent<Image>();
                spriteComponent.sprite = data.sprite;
            }

            if (child.name == "Name")
            {
                var nameComponent = child.GetComponent<Text>();
                nameComponent.text = data.nickname;
            }

            if (child.name == "Cost")
            {
                var priceComponent = child.GetComponent<Text>();
                priceComponent.text = data.purchasePrice.ToString();
            }
        }
    }
    
    private void ClearView()
    {
        foreach (Transform item in contentView.transform)
        {
            Destroy(item.gameObject);
        }
    }
}
