using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // had to import so that I could make use of the arctan function which I used to resolve the angle of the vector to the horizontal


public class DashBoss : Enemy
{
    PlayerCombat player;
    Vector3 playerPos; 

    public float dashSpeed = 3f; // speed of the dash
    public bool canDash; // determines whether they can currently dash or not

    void Start()
    {
        player = FindObjectOfType<PlayerCombat>(); 
    }


    void Update()
    {
        if (shouldBeEnabled) // only works when the player is in the boss' room
        {
            timerForDash(); // starts the dash mechanic on the boss

            if (canDash) // only allows the dash to happen if the cooldown is up (i.e., canDash has been set to true)
            {
                transform.rotation = Quaternion.Euler(0, 0, (float)FindAngle()); // makes the boss point towards the player
                transform.Translate(Vector2.up * dashSpeed * Time.deltaTime); // makes the boss "dash" or move in a specific direction
                StartCoroutine(Wait(2)); // waits 2 seconds before the next dash is started
            }

            Health(); // calls health function inherited from the Enemy class
            DisableOnPlayerDeath(); // when the player dies it disables the enemies to prevent errors
        }
    }

    void DisableOnPlayerDeath()
    {
        if (player.playerHealth <= 0)
        {
            enabled = false; // disables the script so that the enemy will no longer be able to move once the player dies
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    // checks to see whether the enemy is in contact with the player and deals damage to them
    {
        if (collision.collider.tag == "Player")
        {
            player.PlayerTakeDamage(damage);
        }
    }

    private double FindAngle() // need to find angle between two 2d vectors so that I can direct the boss in the right direction
    // Copied over from projectile enemy script
    {
        Vector2 playerPos = GameObject.Find("Player").transform.position;
        Vector2 thisEnemyPos = gameObject.transform.position;
        Vector2 resultantVector = thisEnemyPos - playerPos;

        double angleToHorizontal = Math.Atan(resultantVector.y / resultantVector.x) * (180 / Math.PI);

        double angleToVertical = angleToHorizontal - 90; // works when player is on the right side of the enemy

        if (playerPos.x > thisEnemyPos.x)
        {
            double angle = angleToVertical; // only uses this angle when the player is on the right side so the shooting angle is correct
            return angle;
        }
        else
        {
            double angle = angleToHorizontal - 270; // had to offset the angle of the horizontal by 270 so that the projectile shoots at an angle to the left of the enemy
            return angle;
        }
    }

    float timeBtwDashes = 3; // set the time between dashes to 3 seconds
    float dashTimer = 3;
    private void timerForDash() // determines the interval between dashes
    {
        if (dashTimer <= 0)
        {
            canDash = true;
            dashTimer = timeBtwDashes; // resets the timer so that it can be used for the next dash
        }
        else
        {
            dashTimer -= Time.deltaTime;
        }
    }

    IEnumerator Wait(float time)  
    // waits a set amount of time before the dash action is stopped and the timer for the next dash then starts
    {
        yield return new WaitForSeconds(time);
        canDash = false;
        timerForDash();

    }
}
