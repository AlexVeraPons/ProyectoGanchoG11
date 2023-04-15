using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HookMovement : MonoBehaviour
{
    public static Action<bool> OnHookEntersPlayer;
    public DistanceJoint2D _Distancejoint;
    private Rigidbody2D _rigidBody2D;
    public bool MovingRight = false;
    private float _speed;
    private Vector2 _direction;
    private Vector2 _direction2;
    private int _timesItColidesWithParent = 2;
    public LineRenderer _lineRenderer;
    //---
    private float timer = 2;

    void OnEnable()
    {
        ThowHook.hookIsMovingRight += ChangeHookDirection;
    }
    void OnDisable()
    {
        ThowHook.hookIsMovingRight -= ChangeHookDirection;

    }
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _speed = 100;
        _lineRenderer.enabled = true;
    }

    void FixedUpdate()
    {
        if (MovingRight)
        {
            _rigidBody2D.velocity = Vector2.right * _speed * Time.deltaTime;
        }
        else 
        {
            var padrePos = this.transform.parent.transform.position;
            var directionDeVuelta = padrePos - this.transform.position;
            _rigidBody2D.velocity = directionDeVuelta.normalized * _speed * Time.deltaTime;
            
        }
    }
    void Update()
    {
        timer -= Time.deltaTime;
        _lineRenderer.SetPosition(0, this.transform.parent.transform.position); //pos final de la linea, mouse
        _lineRenderer.SetPosition(1, transform.position); //pos inicial de la linea, pj
        
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
            _lineRenderer.enabled = false;
            OnHookEntersPlayer?.Invoke(true);
            Destroy(gameObject);

        }
        //Instantiate(impactEffect, transform.position, transform.rotation);
    }
}
