using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    ScoreAndCoins coins; // reference to ScoreAndCoins script

    private void Start()
    {
        coins = FindObjectOfType<ScoreAndCoins>();
        // finds script attached to the player
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") // if the player collides with the coin
        {
            coins.incrementCoinCount(); // increases coin count
            Destroy(gameObject); // destroys the coin once it is collided with 
            // gives impression that it has been collected
        }
    }
}
