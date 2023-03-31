using UnityEngine;

[System.Serializable]
public struct ShopItem
{
    public Sprite sprite;
    public string nickname;
    public int purchasePrice;
    public ShopItemStatus status;
}