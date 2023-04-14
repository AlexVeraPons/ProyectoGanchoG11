using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    private float _speed;
    public bool MovingRight = false;
    void OnEnable()
    {
        ThowHook.hookIsMovingRight += ChangeHookDirection;
    }
    void OnDisable()
    {
        ThowHook.hookIsMovingRight -= ChangeHookDirection;

    }
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _speed = 100;
    }

    void FixedUpdate()
    {
        if (MovingRight)
        {
            _rigidBody2D.velocity = Vector2.right * _speed * Time.deltaTime;
        }
        else
        {
            _rigidBody2D.velocity = Vector2.left * _speed * Time.deltaTime;
        }
    }
    void ChangeHookDirection(bool movingRight)
    {
        MovingRight = !MovingRight;
        Debug.Log("Move direvtion is: " + MovingRight);
    }
}
