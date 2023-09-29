using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputHori = Input.GetAxis("Horizontal");
        float inputVert = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(inputHori * speed, inputVert * speed);
    }
}
