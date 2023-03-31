using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private ShopData shopData;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject contentView;

    private void Start()
    {
        LoadShop(contentView, shopData, itemPrefab);
    }

    private void LoadShop(GameObject view, ShopData data, GameObject prefab)
    {
        foreach (var item in data.items)
        {
            var itemGO = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            var spriteComponent = itemGO.transform.GetChild(0).GetComponent<Image>();
            var nameComponent = itemGO.transform.GetChild(1).GetComponent<Text>();
            var priceComponent = itemGO.transform.GetChild(2).GetComponent<Text>();
            spriteComponent.sprite = item.sprite;
            nameComponent.text = item.nickname;
            priceComponent.text = item.purchasePrice.ToString();
            
            itemGO.transform.SetParent(view.transform);
        }
    }
}
