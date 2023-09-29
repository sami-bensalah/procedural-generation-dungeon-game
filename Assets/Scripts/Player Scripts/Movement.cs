using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;

    private float horizontalMovement;
    private float verticalMovement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        // accesses the rigidbody component in the player
        // rigidbody allows for physics to act on gameobjects
    }

   
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        // Input.GetAxis() returns value between -1 and 1 depending on the key pressed
        // e.g. pressing the A key will cause horizontalMovement to equal -1

        rb.velocity = new Vector2(horizontalMovement * speed, verticalMovement * speed);
        // adds a velocity which acts in the direction specified by the horizontalMovement and verticalMovement
    }

    public void increaseMovementSpeed(float speedMultiplier) // used for the speed increase ability
    {
        speed = speed * speedMultiplier;
        // increases speed by the multiplier passed in
    }

    public Vector2 MovementDirection() // returns the direction of movement as a vector
    // used for the Dash ability in abilities class
    {
        return new Vector3(horizontalMovement, verticalMovement);
    }
}
