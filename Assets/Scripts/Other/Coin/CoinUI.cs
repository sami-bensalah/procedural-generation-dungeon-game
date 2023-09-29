using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // needed to manipulate the text displayed (i.e., the coin count)

public class CoinUI : MonoBehaviour
{
    TextMeshProUGUI coinCountUI;
    ScoreAndCoins coins;
    private void Start()
    {
        coinCountUI = GetComponent<TextMeshProUGUI>();
        // gets the text component from the object the script is attached to (i.e., the healthUI)
        coins = FindObjectOfType<ScoreAndCoins>();
        // referencess ScoreAndCoins class so that the coinCount can be read
    }

    private void Update()
    {
        coinCountUI.text = coins.getCoinCount().ToString();
        // gets the coinCount from the ScoreAndCoin class and converts it to a string 
        // so that it can be displayed as text
    }
}
