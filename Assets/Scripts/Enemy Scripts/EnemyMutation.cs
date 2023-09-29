using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMutation : MonoBehaviour
{

    private float _mutationChance = 0.3f; // chance for the specific enemy of spawning as a mutant
    public void Mutation(ref int enemyHealth, ref int enemyDamage) 
    // assuming the enemy can be mutated the program will change the color of the enemy sprite and change either the health or damage value
    {
        if (MutationChance())
        {
            string mutationType = MutationType(); // determines the mutation type

            if (mutationType == "fake mutation") { 
                gameObject.GetComponent<SpriteRenderer>().color = FakeMutationColor();
                // changes the color of the "fake" mutant to imitate the others
            }
            else if (mutationType == "double health") { 
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue; 
                enemyHealth *= 2; 
                // doubles the health and makes the enemy blue
            }
            else if (mutationType == "double damage") { 
                gameObject.GetComponent<SpriteRenderer>().color = Color.magenta; 
                enemyDamage *= 2; 
                // doubles the damage and makes the enemy pink
            }
        }
    }

    private bool MutationChance() // determines whether a mutation will be occuring in the first place
    {
        float rand = Random.value;
        return rand <= _mutationChance;
        // if rand is less than or equal to mutation chance then it will return true
    }

    private string MutationType() // determines which type of mutation the enemy will undergo
    {
        float randType = Random.value;

        if (randType <= 0.4f)
        {
            return "double damage"; // 40% of occuring for each mutation and 20% chance for a fake
        }
        if (randType <= 0.8f)
        {
            return "double health";
        }
        return "fake mutation"; // added to make the game ore unpredictable, makes the player second guess themselves
    }

    private Color FakeMutationColor() // determines which mutatation the "fake" will imitate
    {
        float randColor = Random.value;

        if (randColor <= 0.5f)
        {
            return Color.magenta;
        }
        return Color.blue;
    }
}
