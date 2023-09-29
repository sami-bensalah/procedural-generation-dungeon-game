using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
// had to import so that I could make use of the arctan function which I used to resolve the angle of the vector to the horizontal

public class ProjectileEnemy : Enemy
{
    GameObject projectile;

    public float detectionRadius;

    public LayerMask player; // had to create new layer called player in inspector and set the value of this to player for it to work

    void Start()
    {
        timer = timeBtwShots;
        projectile = Resources.Load("Prefabs/EnemyBullet") as GameObject; // finds the projectile asset and sets it to a GameObject variable 
    }

    
    void Update()
    {
        if (shouldBeEnabled) // done so that the enemy only functions when the player is in the room
        {
            Health(); // calls the function which allows the enemy to recieve damage
            ShootAtPlayer(); // detects the enemy and shoots at them
            cooldown(); // starts a timer so that bullets can not be fired until it is over
        }
    }

    private void ShootAtPlayer() 
    {
        bool playerInRange = Physics2D.OverlapCircle(transform.position, detectionRadius, player); 
        // checks to see whether an object with the "player" layer is within the bounds of the circle

        if (playerInRange && cooldownOff) // checks that the cooldown is up and the player is in range
        {
            Shoot(); // fires a projectile towards the player
        }
    }

    private void Shoot() 
    {
        Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, (float)FindAngle()));
        // spawns in the projectile at the calculated angle
    }

    private double FindAngle() // need to find angle between two 2d vectors so that I can direct the projectile in the right direction
    {
        Vector2 playerPos = GameObject.Find("Player").transform.position;
        Vector2 thisEnemyPos = gameObject.transform.position;
        Vector2 resultantVector = thisEnemyPos - playerPos;// finds resultant vector
        
        double angleToHorizontal = Math.Atan(resultantVector.y / resultantVector.x) * (180 / Math.PI);
        // finds the angle to the horizontal
        double angleToVertical = angleToHorizontal - 90; // works when player is on the right side of the enemy
        
        if (playerPos.x > thisEnemyPos.x)
        {
            double angle = angleToVertical; // only uses this angle when the player is on the right side so the shooting angle is correct
            return angle;
        }
        else {
            double angle = angleToHorizontal - 270; // had to offset the angle of the horizontal by 270 so that the projectile shoots at an angle to the left of the enemy
            return angle;
        }
    }

    float timeBtwShots = 1.0f; // no. seconds between shots
    float timer = 1.0f;
    bool cooldownOff; // tells the program if the enemy can shoot
    private void cooldown() // timer between the enemy's shots
    {
        cooldownOff = false; 
        // done so as soon as it is set to true it is set to false, this results in only enough time for one bullet to be fired
        if (timer <= 0)
        {
            timer = timeBtwShots; // resets the timer
            cooldownOff = true; // sets it to true so the enemy can shoot
        }
        else
        {
            timer -= Time.deltaTime; // decrements by time
        }
    }

    private void OnDrawGizmos() 
    // draws a visible circle in the inspector so that I can see the area that the projectile enemy detects visually to allow for easier editing of the detection radius
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
