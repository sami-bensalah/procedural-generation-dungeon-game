using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    Movement playerMovement;
    ProjectileSystem playerShooting;

    Rigidbody2D rb;
    private void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>(); 
        // retrives the rigidbody component from the player object
        playerShooting = FindObjectOfType<ProjectileSystem>();
        playerMovement = FindObjectOfType<Movement>();
        // references to the Movement and ProjectileSystem classes
    }

    public void speedIncreaseAbility()
    // increases the movement speed and decreases the time between shots
    {
        playerMovement.increaseMovementSpeed(1.2f);
        // increases movement speed by 20%
        playerShooting.cooldownDecrease(0.8f);
        // decreases time btwn shots by 20%
    }

    private void Update()
    {
        // stuff in ActivateDash() used to be here
    }

    public void ActivateDash() 
    {
        if (Input.GetKey(KeyCode.LeftShift) && dashAllowed)
        {
            Dash(); // makes the player dash if L-Shift is pressed and they are allowed to dash
        }
        ResetDash(); // prevents another ash from being done for a set time
    }
    
    float dashForce = 50f; // the amount of force which acts on the player
    private void Dash() 
    {
        Vector3 direction = playerMovement.MovementDirection(); // determines the direction the player is moving in
        rb.AddForce(direction * dashForce); // adds a force in the determined direction
        DashTimer(); // starts the timer to stop the force from acting after a set time
    }

    float dashTime = 0.2f; // sets the time the dash lasts for to 0.2 seconds
    float dashTimer = 0.2f; // value that is reduced by time.deltatime every frame
    bool dashAllowed = true; // determines whether the player can dash or not
    private void DashTimer() 
    // makes dash only last for a specific amount of time (time is defined by the dashTime variable)
    {
        if (dashTimer <= 0 && dashAllowed == true)
        // only sets the speed to 0 if the dash is currently allowed
        {
            dashAllowed = false; // ensures that the player can not dash immediately after
            rb.velocity = Vector3.zero; // reduces the speed so the player doesn't continue to move after the dash is finished
            dashTimer = dashTime; // resets the timer so that it can be used again
        }
        else
        {
            dashTimer -= Time.deltaTime; // decrements the timer by time
        }
    }

    float timeBtwDashes = 10; // determines the time between the dashes
    float timeBtwDashesTimer = 10; // value that is reduced by time.deltatime every frame
    private void ResetDash() 
    // used to allow the player to dash again after their previous dash but only after a set amount of time (i.e., timeBtwDashes)
    {
        if (dashAllowed == false) 
        // only starts the timer if the dash is currently not allowd (i.e., has just been used)
        {
            if (timeBtwDashesTimer <= 0)
            {
                dashAllowed = true; // allows the player to dash again
                timeBtwDashesTimer = timeBtwDashes; // resets the timer so that it can be used again
            }
            else
            {
                timeBtwDashesTimer -= Time.deltaTime; // decrements the timer by time
            }
        }
    }
}
