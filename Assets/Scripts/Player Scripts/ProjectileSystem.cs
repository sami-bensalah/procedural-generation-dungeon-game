using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSystem : MonoBehaviour
{
    public GameObject bullet;
    public float timeBtwShots;
    float timerValue;

    // changed getkeydown to getkey, to allow the player to shoot constantly and only in one direction at a time
    void Start()
    {
        // sets the value of timerValue to the float value I entered in the inspector
        timerValue = timeBtwShots;
    }
    void Update()
    {
        // check to see whether timer value <= 0 if it is then the player can shoot
        if (timerValue <= 0)
        {
            if (IsArrowKeyDown())
            {
                ShotControl();
                timerValue = timeBtwShots; // sets timerValue back to its starting value 
            }

        }
        else
        {
            timerValue -= Time.deltaTime; // decrements the value of timerValue
        }
    }
    
    private bool IsArrowKeyDown() 
    {
        // checks for input of any of the arrow keys and returns a boolean value
        return Input.GetKey(KeyCode.RightArrow) ||
        Input.GetKey(KeyCode.LeftArrow) ||
        Input.GetKey(KeyCode.DownArrow) ||
        Input.GetKey(KeyCode.UpArrow);
    }

    private void ShotControl() 
    {
        // Allows the player shoot in any of the four direction given that the shot cooldown is equal to zero and the correct key is beng pressed down
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            CreateBullet(90); // the parameter being passed in determines the rotation of the projectile being instantiated
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            CreateBullet(270);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            CreateBullet(0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            CreateBullet(180);
        }
    }

    private void CreateBullet(float z)
    {
        // instantiates the projectile with a specified rotation on the z axis
        // Quaternion is used to set the rotation of the projectile so that it shoots and points in the correct direction when instantiated
        Instantiate(bullet, transform.position, Quaternion.Euler(SetVector(z)));
    }

    private Vector3 SetVector(float z) 
    {
        // sets the value of the vector being passed into the CreateBullet function
        return new Vector3(0, 0, z);
    }

    public void cooldownDecrease(float cooldownMultiplier) // method needed for the speed increase ability
    // multiplies the timeBtwShots by a decimal value to decrease timeBtwShots
    {
        timeBtwShots = timeBtwShots * cooldownMultiplier;
    }

}
