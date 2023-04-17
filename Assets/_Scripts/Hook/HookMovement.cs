using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HookMovement : MonoBehaviour
{
    public static Action<bool> OnHookEntersPlayer;
    public Action<Vector3> OnHookedVector;
    public Action OnHookReleased;
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

    }
    public void ChangeHookDirection(bool movingRight)
    {
        _isMovingForward = movingRight;
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Player")
        {
            Debug.Log("parent");
            _timesItColidesWithParent -= 1;
        }
        if (_timesItColidesWithParent <= 0)
        {
            OnHookEntersPlayer?.Invoke(true);
            Debug.Log("muelto");
        }
        // var hookeable = hitInfo.GetComponent<IHookeable>();
        // if (hookeable == null) return;
        // hookeable.TakeDamage();


    }
    void MoveForward(Vector3 forward)
    {
        _rigidBody2D.transform.position += (Vector3)forward * _speed * Time.deltaTime;
    }
    void ReturnToVector(Vector3 direccionDeVuelta)
    {
        _rigidBody2D.transform.position -= direccionDeVuelta.normalized * _speed * Time.deltaTime;
    }
    private Vector2 GetVectorDirection(Vector3 InitialPosition, Vector3 finalPosition)
    {
        Vector2 VectorDirection = finalPosition - InitialPosition;
        return VectorDirection;
    }
}
