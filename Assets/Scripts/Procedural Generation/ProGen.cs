using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // had to import to make use of Cast<GameObject>.ToArray()

public class ProGen : MonoBehaviour
{
    Room room;

    int roomCount = 1;
    int maxRoomCount = 12; // sets the max number of rooms to be generated

    private List<GameObject> roomTypes; // Defines a list which is to contain all possible rooms
    private List<Vector3> allRoomPositions = new List<Vector3>(); // Defines list which will store positions of spawned rooms to prevent them from spwaning on top of eachother as it ran errors when the program started as it wasnt defined
    public List<GameObject> rooms = new List<GameObject>(); // stores all the rooms present in the scene

    GameObject startingRoom;

    GameObject currentRoom;
    private void Start()
    {
        SpawnStartRoom(); // creates the starting room first
    }


    private void Update()
    {
        if (roomCount != maxRoomCount) // makes sure that only the maximum number of rooms are spawned
        {
            SpawnNextRoom(); // next room is then spawned
        }
    }

    private void SpawnStartRoom()
    {
        roomTypes = Resources.LoadAll("RoomTemplate", typeof(GameObject)).Cast<GameObject>().ToList();  // returned error without casting so added cast to gameobject
        
        int startingRoomIndex = 0; // chooses the default room
        startingRoom = roomTypes[startingRoomIndex]; // sets the startign room to the corresponding room template in the list
        Instantiate(startingRoom, new Vector3(0, 0, 0), transform.rotation); // instantiates the room at the center of the scene

        currentRoom = startingRoom; // sets the starting room to the current room so the other rooms can be spawned from it 

        rooms.Add(currentRoom); // adds the current room to the room list

        allRoomPositions.Add(currentRoom.transform.position);// needed so that te starting room pos is also added to the list
        currentRoom.GetComponent<Room>().setRoomNumber(1); // sets the value in room class for room number to the roomCount
    }

    private void SpawnNextRoom()
    {
        roomCount += 1; // increments the count by one so that only a set number of rooms are generated
        currentRoom = Instantiate(roomTypes[0], posCheck(), transform.rotation); // sets the newly spwaned room to the new current room
        rooms.Add(currentRoom);
        allRoomPositions.Add(currentRoom.transform.position); // Adds the newly spawned room's position to the list to prevent others from spawning in the same position
        currentRoom.GetComponent<Room>().setRoomNumber(roomCount); // sets the value in room class for room number to the roomCount
    }

    private Vector3 nextRoomPos() // determines a possible position of the next room
    {
        Vector3 prevRoomPos = currentRoom.transform.position; // set to the posiion of the previous room

        int rndm = Random.Range(1, 5); // generates a random number between 1 and 4, and uses the number as the key in the dictionary

        Dictionary<int, Vector3> posChange = new Dictionary<int, Vector3>(); // Dictionary which contains the position a room can have (right, left, down and up)
        posChange.Add(1, new Vector3(10, 0, 0)); // right
        posChange.Add(2, new Vector3(-10, 0, 0)); // left
        posChange.Add(3, new Vector3(0, 10, 0)); // up
        posChange.Add(4, new Vector3(0, -10, 0)); // down

      
        Vector3 newPos = posChange[rndm] + prevRoomPos;  // returns random position change from index in the dictionary and adds to previous room pos to find new pos
        return newPos;
    }

    private Vector3 posCheck() // checks to see whether the position the next room wants to spawn in is empty or not
    {
        Vector3 pos = nextRoomPos();
        if (allRoomPositions.Contains(pos)) // if the room position is taken then it will re-determine the value for pos until it finds an empty slot
        {
            return posCheck();
        }
        return pos;
    }

    public bool AllRoomsGenerated() //checks to see if all the rooms have been generated
    {
        if (roomCount == maxRoomCount)
        {
            return true;
        }
        return false;
    }

    public int getMaxRoomCount() 
    // returns maxRoomCount so that 
    {
        return maxRoomCount;
    }
    
}
