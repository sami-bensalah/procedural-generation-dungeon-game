using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// imported so that the scene can be changed using SceneManager.LoadScene()

public class PlayerCombat : MonoBehaviour
{
    public int playerHealth = 5;
    // current health of the player
    void Update()
    {
        if (playerHealth <= 0) // checks whether the player should be dead
        {
            SceneManager.LoadScene("DeathScreen");
            Destroy(gameObject); // destroys or "kills" the player
        }
    }

    public void PlayerTakeDamage(int damage) // method which reduces the players health
    {
        playerHealth -= damage;
    }
}
