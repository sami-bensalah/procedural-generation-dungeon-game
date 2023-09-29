using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour // used to set the position of the camera to the position of the room the player is currently in
{
    ProGen proGen;
    private void Start()
    {
        proGen = FindObjectOfType<ProGen>();  
        // reference to ProGen class so that the rooms list can be accessed
    }
    void Update()
    {
        foreach (GameObject room in proGen.rooms) // checks every room in the gamescene
        {
            room.GetComponent<Room>().passed = true; // used to check whether the room is actually visited by the foreach
            bool playerInRoom = room.GetComponent<Room>().isPlayerInRoom(); // sets the value of the plyerInRoom to the value of the getter method for the boolean variable playerInRoom in the Room class
            
            if (playerInRoom)
            {
                gameObject.transform.position = new Vector3(room.transform.position.x, room.transform.position.y, -10); // sets the position of the camera to the position of the room
                print("Entered Room: " + room.GetComponent<Room>().getRoomNumber()); // check to make sure that the camera is set to the correct room
            }
        }
    }
}


