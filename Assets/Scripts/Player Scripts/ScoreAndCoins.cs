using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAndCoins : MonoBehaviour
{
    int coinCount; // the number of coins the player has collected
    int Score;

    public void incrementCoinCount() 
    // used to increase the coin count when a coin is "picked up"
    {
        coinCount += 1;
    }

    public int getCoinCount() 
    // returns the number of coins so it can be used in other classes
    {
        return coinCount;
    }

    public void decrementCoinCount(int price)
    // used to decrease the coin count when an ability is purchased
    {
        coinCount -= price;
    }
}
