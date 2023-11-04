using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : Singleton<GUIManager>
{
    public TMP_Text coinCountingText;

    public void UpdateCoins()
    {
        if (coinCountingText)
            coinCountingText.text = "Coins: " + Pref.Coins;
    }
}
