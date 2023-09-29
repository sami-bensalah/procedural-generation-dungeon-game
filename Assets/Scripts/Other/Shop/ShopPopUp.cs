using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPopUp : MonoBehaviour
{
    bool isShopUp; // bool used to determine the state of the shop (active or hidden)

    public GameObject[] disabledObjects; 
    // a list containing all objects which have to be disabled to hide the shop
    private void Start()
    {
        isShopUp = false;
        // set it to false when the game first starts
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && isShopUp == false)
        // checks to see whether p is being pressed and the shop is currently hidden
        {
            isShopUp = true; // set to true so that it can be hidden when p is pressed again
            foreach (GameObject item in disabledObjects)
            // goes through every item and enables them in the scene
            {
                item.SetActive(true); // enables the shop, so it can be seen in the scene
            }

        }
        else if (Input.GetKeyDown(KeyCode.P) && isShopUp == true)
        // checks to see whether p is being pressed and the shop is currently active
        {
            foreach (GameObject item in disabledObjects)
            // goes through every item and disables them in the scene
            {
                item.SetActive(false); // hides the shop in the scene
            }
            isShopUp = false; // sets it to false so the shop can be enabled again
        }
    }
}
