using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public TMP_Text priceText;
    public Image hud;
    public Button btn;

    public void UpdateUI(ShopItem item, int shopItemId)
    {
        if (item == null) return;

        if(hud)
        {
            hud.sprite = item.hud;
        }

        bool isUnlocked = Pref.GetBool(PrefConst.PLAYER_PEFIX + shopItemId);


        if (isUnlocked)
        {
            if(shopItemId == Pref.CurPlayerId)
            {
                if(priceText)
                    priceText.text = "Active";
                
            }
            else
            {
                if (priceText)
                    priceText.text = "OWNER";
                
            }
        }
        else
        {
            if (priceText)
                priceText.text = item.price.ToString();
        }

    }

}
