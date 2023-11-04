using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDialog : Dialog
{
    public Transform gridRoot;
    public ShopItemUI itemUIPrefab;

    public override void Show(bool isShow)
    {
        base.Show(isShow);

        UpdateUI();
    }

    private void UpdateUI()
    {
        ShopItem[] items = ShopManager.Ins.items;

        if (items == null || items.Length <= 0 || !gridRoot || !itemUIPrefab) return;

        ClearChilds();

        for (int i = 0; i < items.Length; i++)
        {
            int index = i;

            ShopItem item = items[i];

            if(item != null)
            {
                ShopItemUI itemUIClone = Instantiate(itemUIPrefab, Vector3.zero, Quaternion.identity);

                itemUIClone.transform.SetParent(gridRoot);

                itemUIClone.transform.localPosition = Vector3.zero;

                itemUIClone.transform.localScale = Vector3.one;

                itemUIClone.UpdateUI(item, index);

                if (itemUIClone.btn)
                {
                    itemUIClone.btn.onClick.RemoveAllListeners();
                    itemUIClone.btn.onClick.AddListener(() => ItemEvent(item, index));
                }
            }
        }

    }


    void ItemEvent(ShopItem item, int shopItemId)
    {
        if (item == null) return;

        bool isUnlocked = Pref.GetBool(PrefConst.PLAYER_PEFIX + shopItemId);

        if (isUnlocked)
        {
            if (shopItemId == Pref.CurPlayerId) return;

            Pref.CurPlayerId = shopItemId;

            UpdateUI();

        }
        else
        {
            if(Pref.Coins >= item.price)
            {
                Pref.Coins -= item.price;

                Pref.SetBool(PrefConst.PLAYER_PEFIX + shopItemId, true);

                Pref.CurPlayerId = shopItemId;

                GUIManager.Ins.UpdateCoins();

                UpdateUI();
            }
            else
            {
                Debug.Log("You dont have enough coins!");
            }
        }
    }

    public void ClearChilds()
    {
        if (!gridRoot || gridRoot.childCount <= 0) return;

        for (int i = 0; i < gridRoot.childCount; i++)
        {
            var child = gridRoot.GetChild(i);

            if (child)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
