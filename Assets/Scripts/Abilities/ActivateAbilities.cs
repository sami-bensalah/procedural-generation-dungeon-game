using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAbilities : MonoBehaviour
{
    Abilities ability;
    ScoreAndCoins playerCoins;

    int abilityPrice = 2; // price of the ability

    int pCoinCount; // player coin count
    void Start()
    {
        ability = FindObjectOfType<Abilities>(); // references Abilities class
        playerCoins = FindObjectOfType<ScoreAndCoins>(); 
        // references ScoreAndCoins class so the coinCount can be accessed
    }


    int i = 0;
    void Update()
    {
        pCoinCount = playerCoins.getCoinCount(); 
        // sets the coin count variable in this class equal to the actual coin count, refreshes every frame

        if (dashActivated) // if the player pressed the button or "purchased" the ability
        {
            ability.ActivateDash(); // then the ability is called from the Abilities class for use
        }

        if (speedBoostActivated && i == 0) // if the player presses the button and it hasn't been called yet then
        {
            ability.speedIncreaseAbility(); // activates speed boost ability
            i += 1;  // needed so it is only called once otherwise it continously increases the speed and decreases the time btwn shots
        }

    }

    public bool dashActivated; // needed to act as a switch which activates the dash ability if all the conditions are met
    public void activateDash() // method called when the "Dash" button is pressed
    {
        if (pCoinCount >= abilityPrice) // checks whether the player can afford the ability
        {
            dashActivated = true; // sets the bool to true so the ability can be called in the Update() function
            playerCoins.decrementCoinCount(abilityPrice); // decreases the number of coins the player has by the ability's price
        }
    }

    public bool speedBoostActivated; // needed to act as a switch which activates the dash ability if all the conditions are met
    public void activateSpeedBoost() // method called when the "Speed Increase" button is pressed
    {
        if (pCoinCount >= abilityPrice) // checks whether the player can afford the ability
        {
            speedBoostActivated = true; // sets the bool to true so the ability can be called in the Update() function
            playerCoins.decrementCoinCount(abilityPrice); // decreases the number of coins the player has by the ability's price
        }
    }
}
