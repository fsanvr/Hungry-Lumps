using System;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    public readonly ShopUpdateEvent shopChanged = new ShopUpdateEvent();
    [SerializeField] private ShopData defaultData;
    [SerializeField] private ShopData playerData;

    private void Start()
    {
        OnChanged();
    }

    private void OnChanged()
    {
        shopChanged.Invoke(playerData);
    }
}