using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private string shopSystemName = "ShopSystem";
    [SerializeField] private GameObject contentView;
    [SerializeField] private GameObject itemPrefab;

    private void Awake()
    {
        var shop = GameObject.Find(shopSystemName).GetComponent<ShopSystem>();
        if (shop)
        {
            shop.GetComponent<ShopSystem>().shopChanged.AddListener(Show);
        }
    }

    private void OnDestroy()
    {
        
        var shop = GameObject.Find(shopSystemName);
        if (shop)
        {
            shop.GetComponent<ShopSystem>().shopChanged.RemoveListener(Show);
        }
    }

    private void Show(ShopData data)
    {
        ClearView();
        foreach (var item in data.items)
        {
            var itemGO = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            ConfigureItem(itemGO, item);
            itemGO.transform.SetParent(contentView.transform);
        }
    }

    private static void ConfigureItem(GameObject go, ShopItem data)
    {
        var spriteComponent = go.transform.GetChild(0).GetComponent<Image>();
        var nameComponent = go.transform.GetChild(1).GetComponent<Text>();
        var priceComponent = go.transform.GetChild(2).GetComponent<Text>();
        spriteComponent.sprite = data.sprite;
        nameComponent.text = data.nickname;
        priceComponent.text = data.purchasePrice.ToString();
    }

    private void ClearView()
    {
        foreach (Transform item in contentView.transform)
        {
            Destroy(item.gameObject);
        }
    }
}
