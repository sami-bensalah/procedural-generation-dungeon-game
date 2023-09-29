using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    public int damage; // the damage the enemy deals to the player (value is set in the inspector)
    public int enemyHealth; // the health of the enemy (also set in the inspector)

    public bool shouldBeEnabled = false; 
    // used to determine when the enemy is allowed to move (set to true when the player is in the same room)

    public GameObject coin; // holds the coin gameobject
    public void FollowPlayer(float movementSpeed, Transform playerPos) // method which makes the enemy move towards the player
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, movementSpeed * Time.deltaTime); 
        // makes the enemy move towards the player's position
    }

    public void Health()
    {
        if (enemyHealth <= 0) // checks to see if the enemy health is less than or equal to 0 and if it is then
        {
            GameObject blood = Resources.Load("Prefabs/Blood") as GameObject; // retrieves blood particle effect
            Instantiate(blood, transform.position, transform.rotation); // instantiates the blood at the position of the enemy
            dropCoin(); // drops coin before the enemy is destroyed
            Destroy(gameObject); // destroys the enemy so that they disappear on the scene
        }
    }

    public void TakeDamage(int damage) // called in the Projectile class to deal damage to the enemy 
    {
        enemyHealth -= damage;
        print(enemyHealth);
        // decrements enemyHealth
    }

    public void Mutate() 
    {
        EnemyMutation _mutation = gameObject.AddComponent<EnemyMutation>(); // creates an instance of the Mutate class
        _mutation.Mutation(ref enemyHealth, ref damage); // passes in the attributes of enemy health and damage so that the enemy can be mutated
    }

    private void dropCoin() // called when the enemy dies
    {
        int noCoinsToDrop = Random.Range(1, 5); // will determine how many coins to drop (1-4 coins)

        for (int i = 0; i < noCoinsToDrop; i++) // iterates until i is equal to noCoinsToDrop
        {
            Instantiate(coin, gameObject.transform.position, transform.rotation);
            // creates a coin at the position of the enemy
        }
    }
}

