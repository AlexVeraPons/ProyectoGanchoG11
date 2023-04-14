using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HookMovement : MonoBehaviour
{
    public static Action OnHookEntersPlayer;
    private Rigidbody2D _rigidBody2D;
    private float _speed;
    public bool MovingRight = false;
    private int _timesItColidesWithParent = 2;
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
        MovingRight = movingRight;
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (this.transform.parent == hitInfo.transform)
        {
            Debug.Log("parent");
            _timesItColidesWithParent -= 1;
        }
        if (_timesItColidesWithParent <= 0)
        {
            Destroy(gameObject);

        }
        //Instantiate(impactEffect, transform.position, transform.rotation);
    }
}
