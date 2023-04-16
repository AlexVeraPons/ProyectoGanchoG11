using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHook : MonoBehaviour
{
    private Rigidbody2D rb;
    public int speed = 100;
    private bool MovingRight = false;
    // Start is called before the first frame update
    void Start()
    {
        MovingRight = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (MovingRight)
        //{
            rb.velocity = Vector2.right * speed * Time.deltaTime;

        //}
    }
    void ChangeHookDirection(bool movingRight)
    {
        MovingRight = movingRight;
    }
}
