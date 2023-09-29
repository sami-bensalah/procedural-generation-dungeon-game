using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
// imported so that the UI elements can be manipulated through code

public class HealthUI : MonoBehaviour
{
    PlayerCombat player;
    public Image heartSprite;
    public List<Image> hearts;

    private float x;
    void Start()
    {
        player = FindObjectOfType<PlayerCombat>(); 
        // reference to PlayerCombat class so that the health value can be accessed

        for (int i = 0; i < player.playerHealth; i++)
        // iterates as many times as the player health so that an equal number of hearts are created
        {
            CreateHeart(-700, 100, i);
            // creates a heart that is offset from x = -700 by 100*i
            // this will result in the hearts being generated in a line
        }
    }


    void Update()
    {   
        for (int i = 0; i < hearts.Count; i++)
        // iterates through every inde in the hearts list
        {
            if (i < player.playerHealth)
            // if the player's health is currently greater than the number of hearts displayed
            {
                hearts[i].enabled = true;
                // makes the heart appear so the number of hearts displayed match the player health
            }
            else
            {
                hearts[i].enabled = false;
                // if the player health is less than the number of hearts displayed
                // then the hearts will be hidden to display the same number as the health
            }
        }
    }

    void CreateHeart(float x, float changeInX, int i)  // method used to create a heart at a specific position
    {
        if (i == 0)
        // if it is the first heart generated
        {
            changeInX = 0;
            // it will have no offset from the starting x value determined by float x
        }
        else 
        {
            changeInX *= i;
            // if it is not the first heart then it will be offset by the passed in paramater for distance between hearts
            // multiplied by the number of hearts from the startign heart it is
        }

        Image newHeart = Instantiate(heartSprite, transform.position, Quaternion.identity);
        // instantiates the heart image and assigns it to the local variable newHeart

        hearts.Add(newHeart);
        // the local variable is then added to the hearts list

        hearts[i].transform.SetParent(GameObject.Find("Canvas").transform);
        // the heart is then set to be the child of the canvas so that it can be displayed
        // as if it isnt it won't be seen as UI elements can only be seen if they are a child of the canvas
        
        x += changeInX; 
        // the value for x is then incremented by chageInX to determine the new x position of this newly created heart

        hearts[i].GetComponent<RectTransform>().localPosition = new Vector3(x, 380, 0);
        // the heart is then instantiated at the previously calculated position
    }
}
