using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBar : MonoBehaviour
{
    [SerializeField] GameObject bg;
    [SerializeField] Shop.ShopType type;
    public Shop.ShopType Type => type;

    Shop shop;

    public void SetShop(Shop shop)
    {
        this.shop = shop;
    }

    public void Select()
    {
        shop.SelectBar(this);
    }

    public void Active(bool active)
    {
        bg.SetActive(!active);
    }
}
