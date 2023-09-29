using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    int startNoHearts;

    public Image heartSprite;
    public List<Image> hearts;

    public List<Vector3> heartPos;

    bool canexecute = true;

    public GameObject myCanvas;

    void Start()
    {
        startNoHearts = health;
        print(startNoHearts);
    }
    
    void Update()
    {
        while (hearts.Count < startNoHearts)
        {
            hearts.Add(heartSprite);
        }
        
        if (canexecute)
        {
            hearts[0] = Instantiate(Resources.Load("Prefabs/Heart")) as Image;
            Instantiate(hearts[0], heartPosCalculator(), transform.rotation);
            if (hearts[0] != null)
            {
                print("reached");
                hearts[0].transform.parent = myCanvas.transform;
            } 
            
            canexecute = false;
        }
    }

    private Vector3 heartPosCalculator()
    {
        heartPos.Add(new Vector3(20, 500, 0));
        return heartPos[0];
    }
}
