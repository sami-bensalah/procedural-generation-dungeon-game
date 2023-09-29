using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOrWallCheck : MonoBehaviour
{
    Dictionary<int, Vector3> distAdjacentDoors = new Dictionary<int, Vector3>(); 
    // Dictionary which contains the position a room can have (right, left, down and up)

    ListDoors listDoors; 
    ProGen proGen;

    int i = 0; // used as a key for the dictionary

    private void Start()
    {
        distAdjacentDoors.Add(0, new Vector3(1, 0, 0)); // checks to the right
        distAdjacentDoors.Add(1, new Vector3(-1, 0, 0)); // checks to the left
        distAdjacentDoors.Add(2, new Vector3(0, 1, 0)); // checks up
        distAdjacentDoors.Add(3, new Vector3(0, -1, 0)); // checks down

        listDoors = FindObjectOfType<ListDoors>(); // reference to ListDoors class
        proGen = FindObjectOfType<ProGen>(); // reference to ProGen class


        listDoors.AddToList(transform.parent.transform.gameObject);
        // adds the door being checked to the door list in the ListDoors class
    }


    private void Update()
    {
        if (proGen.AllRoomsGenerated()) // checks to see whether all rooms have been generated before branching
        {
            if (i < 4) // prevents i from becomng too big and becomng out of range of the dictionary
            {
                Vector2 posToCheck = distAdjacentDoors[i] + transform.parent.position; // sum to find possible position of a door

                listDoors.checkPos(posToCheck); // checks to see if there is a door at the calculated position
                i += 1;
            }
        }
    }
}


