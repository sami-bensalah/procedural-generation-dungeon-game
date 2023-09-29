using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    PlayerCombat player;
    Transform playerPos;
    
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        // finds the player gameobject and accesses its transform component 

        player = FindObjectOfType<PlayerCombat>(); 
        // reference to PlayerCombat script so that the enemy can deal damage

        Mutate(); // calls the inherited mutate method, so that the enemy can mutate
    }

    
    void Update()
    {
        if (shouldBeEnabled) // done so that the enemy only functions when the player is in the room
        {
            Health(); // calls health function inherited from the Enemy class
            DisableOnPlayerDeath(); // when the player dies it disables the enemies to prevent errors
            FollowPlayer(2, playerPos); // follows the player at a speed of 2

            if (isTouching) // had to add as only want it to occur during contact 
            {
                StationaryPlayerDamage(); // deals damage to the player periodically while in contact
            }
        }
    }



    bool isTouching = false;
    private void OnCollisionEnter2D(Collision2D collision) 
    // checks to see whether the enemy is in contact with the player and deals damage to them
    {
        if (collision.collider.tag == "Player")
        {
            isTouching = true;
            player.PlayerTakeDamage(damage);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) 
    // checks to see whether the enemy is no longer in contact with the player 
    {
        if (collision.collider.tag == "Player")
        {
            isTouching = false;
        }
    }

    void DisableOnPlayerDeath() 
    {
        if (player.playerHealth <= 0)
        {
            enabled = false;
            // disables the script so that the enemy will no longer be able to move once the player dies
        }
    }

    float nextActionTime = 2.0f;
    float period = 2.0f; 
    // had to create period so that the nextActionTime had a value to refer to when being reset in the timer

    void StationaryPlayerDamage() // deals damage to the player for every 2 seconds the enemy is in contact with them
    {
        
        if (nextActionTime <= 0) // checks to see if the timer is at 0 seconds
        {
            player.PlayerTakeDamage(damage); // deals damage to the player
            nextActionTime = period; // resets the timer so it can be used again
        }
        else
        {
            nextActionTime -= Time.deltaTime; // decrements the timer by time
        }
    }
}
