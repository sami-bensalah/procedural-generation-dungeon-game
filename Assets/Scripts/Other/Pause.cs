using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool isPaused = false;
    //boolean used to toggle the pause

    public GameObject pausedText;
    // reference to pause text

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        // checks to see if the game is not paused and the Esc is being pressed
        {
            isPaused = true; // makes it so that the script knows the game is paused
            pausedText.SetActive(true); // makes the text that tells the player it is paused visible
            Time.timeScale = 0; // sets the time scale to 0 so no gameobjects can move
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        // checks to see if the game is paused and the Esc is being pressed
        {
            isPaused = false; // makes it so that the script knows the game is not paused
            pausedText.SetActive(false); // hides the text as the game is no longer paused
            Time.timeScale = 1; // sets the time scale back to normal so the gameobjects can move
        }
    }
}
