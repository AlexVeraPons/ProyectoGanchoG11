using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HookMovement : MonoBehaviour
{
    public static Action<bool> OnHookEntersPlayer;
    public GameObject PlayerPos;
    //---------
    [SerializeField]
    private float _speed = 10;
    //---------
    private Rigidbody2D _rigidBody2D;
    private LineRenderer _lineRenderer;
    private bool _isMovingForward = false;
    private Vector2 _direction => hookDirection.GetDirectionVector();
    private Vector2 _finalDirection;
    //---------
    public HookDirection hookDirection;
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
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
    }

    public void StealDirection()
    {
        _finalDirection = (Vector3)_direction;
        //Debug.Log(_finalDirection + "final");
    }
    void FixedUpdate()
    {

        if (_isMovingForward)
        {
            MoveForward(_finalDirection);

        }
        else
        {
            ReturnToVector(GetVectorDirection(PlayerPos.transform.position, this.transform.position));
        }
    }
    void Update()
    {
        //this.transform.position = Vector3.MoveTowards(pos.transform.position, this.transform.position, _speed * Time.deltaTime).normalized;
        //_lineRenderer.SetPosition(0, this.transform.parent.transform.position); //pos final de la linea, mouse
        //_lineRenderer.SetPosition(1, transform.position); //pos inicial de la linea, pj

    }
    public void ChangeHookDirection(bool movingRight)
    {
        _isMovingForward = movingRight;
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (this.transform.position == PlayerPos.transform.position)
        {
            Debug.Log("parent");
            _timesItColidesWithParent -= 1;
        }
        if (_timesItColidesWithParent <= 0)
        {
            OnHookEntersPlayer?.Invoke(true);
            Debug.Log("muelto");
            Destroy(gameObject);

        }
        //Instantiate(impactEffect, transform.position, transform.rotation);
    }
    void MoveForward(Vector3 forward)
    {
        _rigidBody2D.transform.position += (Vector3)forward * _speed * Time.deltaTime;
    }
    void ReturnToVector(Vector3 direccionDeVuelta)
    {
        //_rigidBody2D.transform.position += (Vector3)_finalDirection * -_speed * Time.deltaTime;
        _rigidBody2D.transform.position -= direccionDeVuelta.normalized * _speed * Time.deltaTime;
    }
    private Vector2 GetVectorDirection(Vector3 InitialPosition, Vector3 finalPosition)
    {
        Vector2 VectorDirection = finalPosition - InitialPosition;
        return VectorDirection;
    }
}
