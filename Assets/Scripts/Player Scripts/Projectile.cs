using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public float lifetime;
    public int projectileDamage;

    public string targetObject;
    public string shootingObject;
    void Start()
    {
        Invoke("Destroy", lifetime); // calls the Destroy function once lifetime equals 0
    }

    void Update()
    {
        transform.Translate(Vector2.up * projectileSpeed * Time.deltaTime);
        // moves the projectile in the direction it is facing at a set speed
    }

    private void Destroy()
    {
        Destroy(gameObject);
        // destroys the projectile
    }

    private void OnTriggerEnter2D(Collider2D collision)  // used to deal damage to the relevant target
    {
        if (collision.tag == targetObject) // References the inputted tag name for the target 
        {
            if (targetObject == "Enemy") 
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(projectileDamage); 
                // deals damage to the enemy is the target object of the projectile is set to the enemy
            }
            else if (targetObject == "Player")
            {
                collision.gameObject.GetComponent<PlayerCombat>().PlayerTakeDamage(projectileDamage); 
                // accesses the player's damage function and reduces the health by the value of projectile damage
            }
        }
        if (collision.tag != shootingObject) 
        {
            Destroy(gameObject);
            // destroys if it collides with anything that isn't the object which created it
        }
    }
}
