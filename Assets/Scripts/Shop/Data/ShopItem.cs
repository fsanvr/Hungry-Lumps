using System;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class ShopItem: ScriptableObject, IEquatable<ShopItem>
{
    public Sprite sprite;
    public string nickname;
    public int purchasePrice;
    public ShopItemStatus status;
    public bool Equals(ShopItem other)
    {
        return sprite == other.sprite && nickname == other.nickname && status == other.status 
               && purchasePrice == other.purchasePrice;
    }
}