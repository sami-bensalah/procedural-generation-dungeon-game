using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    ProGen progen;

    public bool passed;
    public int roomNumber;
    public bool playerInRoom;

    public LayerMask playerLayer;
    ContactFilter2D cf = new ContactFilter2D();

    public List<Collider2D> objectsInRoom = new List<Collider2D>();

    int numOfEnemies;

    public GameObject dashBoss;
    private void Start()
    {
        progen = FindObjectOfType<ProGen>();

        if (roomNumber == 0 || roomNumber == 1) // if it is the starting room 
        {
            numOfEnemies = 0;
            // then no enemies will be spawned into it
        }
        else if (roomNumber == progen.getMaxRoomCount())
        // spawns a boss at the center of the room if it is the last room
        {
            Instantiate(dashBoss, transform.position, transform.rotation);
        }
        else
        {
            numOfEnemies = Random.Range(0, 5); // randomly assigns a value btwn 0 and 4 to numOfEnemies
        }

        for (int i = 0; i < numOfEnemies; i++) // spawns a random number of enemies in the room (0-4)
        {
            SpawnItems();
        }
    }
    private void Update()
    {
        playerInRoom = Physics2D.OverlapBox(gameObject.transform.position, new Vector2(10, 10), 0, playerLayer);
        // sets the dimensions of the detection area and what object is being detected, used to check if the player has entered the room
        Physics2D.OverlapBox(gameObject.transform.position, new Vector2(7.5f, 7.5f), 0, cf, objectsInRoom);
        // adds all colliders of gameobjects to the list objectsInRoom that are in a 7.5x7.5 unit area
        // used 7.5x7.5 instead of 10x10 as I don't want to include the colliders of the walls and doors
        disableEnemy();
    }

    public void setRoomNumber(int roomNum) // method used in ProGen class to set the room num for each room generated, making them easier to identify
    {
        roomNumber = roomNum;
    }

    public int getRoomNumber() 
    {
        return roomNumber;
    }
    public bool isPlayerInRoom() // returns whether the player is in the room or not
    {
        return playerInRoom;
    }

    public GameObject LowDamageEnemy;
    public GameObject HighDamageEnemy;
    public GameObject projectileEnemy;

    int enemyCounter = 0;
    public void SpawnItems() // spawns random enemy in the room
    {
        Vector3 randomPosition = gameObject.transform.position + new Vector3(Random.Range(-3.25f, 3.25f), Random.Range(-3.25f, 3.25f));
        // determines a random position in the room by adding randomly determined offset vector to the room's central position
        // chose random value between -3.25 and 3.25 as the area of the room which I allow for enemies to spawn is a 7.5x7.5 area
        if (enemyCounter != 4)
        // plan for there to be a max of 4 enemies per room hence why it only branches if there are less than four enemies
        {
            int enemyType = Random.Range(0, 3);
            // determines a random number in the range 0-3, so 0, 1 and 2
            if (enemyType == 0)
            {
                Instantiate(LowDamageEnemy, randomPosition, transform.rotation);
                // if the num generated is 0 then the enemy type is a low damage enemy
            }
            else if(enemyType == 1)
            {
                Instantiate(HighDamageEnemy, randomPosition, transform.rotation);
                // if the num generated is 1 then the enemy type is a high damage enemy
            }
            else
            {
                Instantiate(projectileEnemy, randomPosition, transform.rotation);
                // if the num generated is 3 then the enemy type is a projectile enemy
            }
            enemyCounter += 1; // increments so only four enemies are instantiated
        }
    }

    private void disableEnemy() // if the enemy isn't in the same room as the player then they are disabled
    {
        foreach (Collider2D item in objectsInRoom)
        {
            if (item.gameObject.tag == "Enemy" && playerInRoom == false) 
            // if the player is not in room then and the item its checking is an enemy then it is disabled
            {
                item.gameObject.GetComponent<Enemy>().shouldBeEnabled = false;
            }
            else if (item.gameObject.tag == "Enemy" && playerInRoom == true)
            {
                item.gameObject.GetComponent<Enemy>().shouldBeEnabled = true;
                // otherwise it is kept as enabled 
            }
        }
    }
    
}
