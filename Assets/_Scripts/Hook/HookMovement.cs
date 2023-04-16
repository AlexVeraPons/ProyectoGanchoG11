using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HookMovement : MonoBehaviour
{
    public static Action<bool> OnHookEntersPlayer;
    private Rigidbody2D _rigidBody2D;
    public bool MovingForward = false;
    public float _speed;
    private Vector2 _direction => hookDirection.GetDirectionVector();
    private Vector2 _finalDirection;
    private int _timesItColidesWithParent = 2;
    //---
    public HookDirection hookDirection;
    public GameObject pos;
    public float t;


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
        _speed = 2;
        //_lineRenderer.enabled = true;
    }

    public void StealDirection()
    {
        _finalDirection = _direction;
        Debug.Log(_finalDirection + "final");
    }
    void FixedUpdate()
    {

        // if (MovingForward)
        // {
        // _rigidBody2D.transform.position += (Vector3)_finalDirection * _speed * Time.deltaTime;
        // Debug.Log(_direction);

        //}
        // else
        // {
        //     var padrePos = this.transform.parent.transform.position;
        //     var directionDeVuelta = padrePos - this.transform.position;
        //     _rigidBody2D.velocity = directionDeVuelta.normalized * _speed * Time.deltaTime;

        // }
    }
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(pos.transform.position, this.transform.position, _speed * Time.deltaTime).normalized;
        //_lineRenderer.SetPosition(0, this.transform.parent.transform.position); //pos final de la linea, mouse
        //_lineRenderer.SetPosition(1, transform.position); //pos inicial de la linea, pj

    }
    void ChangeHookDirection(bool movingRight)
    {
        MovingForward = movingRight;
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
            OnHookEntersPlayer?.Invoke(true);
            Destroy(gameObject);

        }
        //Instantiate(impactEffect, transform.position, transform.rotation);
    }
}
