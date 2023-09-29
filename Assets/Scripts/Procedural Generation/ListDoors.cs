using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListDoors : MonoBehaviour
{
    public List<GameObject> doors = new List<GameObject>(); // defknes a list containign all doors

    public List<GameObject> doorsToDestroy = new List<GameObject>(); // defines a list which will contain all doors to be destroyed

    float timer = 10f; // decided on 2 seconds as it is a value which worked with 20 rooms and the game will never generate that many
                      // However, if more rooms are needed to be generated then the value for this would need to be increased

    ProGen proGen;
    private void Start()
    {
        proGen = FindObjectOfType<ProGen>(); // needed to find the script and reference it in the gamescene
    }

    private void Update()
    {
        if (proGen.AllRoomsGenerated())
        // checks to see if all the rooms have been generated before destroying them
        {
            waitBeforeDestroy();
        }
    }

    public void AddToList(GameObject item) // adds parameter to doors list
    {
        doors.Add(item); 
    }

    private void destroyItems() 
    {
        foreach (GameObject item in doorsToDestroy) 
        // will take each item in the list and destroy it
        {
            Destroy(item);
        }
    }

    private void waitBeforeDestroy() 
    {
        if (timer <= 0) // without the timer still had the problem of half the doors being estroyed as in procedural generation the rooms are not generated concurrently but in sequence
                        // so some rooms will run code before others which doesnt allow time for other doors to be generated
        {
            destroyItems(); // destroys all items in the list
        }
        else
        {
            timer -= Time.deltaTime; // decrements the timer value
        }
    }

    public void checkPos(Vector3 posToCheck)  
    {
        foreach (GameObject door in doors) // iterates through for every single item in the doors list
        {
            if (door.transform.position == posToCheck)
            // compares the position of the door to the parameter to see if there is a door at that position
            {
                doorsToDestroy.Add(door);
                // if the position does have a door then it is added to the destruction list
            }
        }
    }
}
