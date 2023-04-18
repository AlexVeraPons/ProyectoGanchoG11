using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HookMovement : MonoBehaviour
{
    public static Action<bool> OnHookEntersPlayer;
    public Action<Transform> OnHookedTransform;
    public Action OnHookReleased;
    public GameObject PlayerPos;
    //---------
    [SerializeField]
    private float _speed = 10;
    //---------
    private Rigidbody2D _rigidBody2D;
    private LineRenderer _lineRenderer;
    private bool _isMovingForward = false;
    private Vector2 _direction => _hookDirection.GetDirectionVector();
    private Vector2 _finalDirection;
    private CollisionDetector _collisionDetector;
    //---------
    private int _timesItColidesWithParent = 2;
    public HookDirection _hookDirection;
    private bool _canMove = true;


    void OnEnable()
    {
        ThowHook.hookIsMovingRight += ChangeHookDirection;
        ThowHook.hookCanMove += SetCanMove;
    }
    void OnDisable()
    {
        ThowHook.hookIsMovingRight -= ChangeHookDirection;
        ThowHook.hookCanMove -= SetCanMove;
    }

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();
        _collisionDetector = GetComponent<CollisionDetector>();
        //_hookDirection = GetComponent<HookDirection>();
        _lineRenderer.enabled = false;
    }

    public void StealDirection()
    {
        _finalDirection = (Vector3)_direction;
    }
    public void SetPosition()
    {
        if (!_collisionDetector.IsTouchingWall() && !_collisionDetector.IsGrounded())
        {
            transform.position = PlayerPos.transform.position + 2 * Vector3.up;
        }
    }
    void FixedUpdate()
    {
        if (_canMove)
        {
            if (_isMovingForward)
            {
                MoveForward(_finalDirection);

            }
            else if (!_isMovingForward)
            {
                ReturnToVector(GetVectorDirection(PlayerPos.transform.position, this.transform.position));
            }
        }
    }
    void Update()
    {
            if(Input.GetKeyUp(KeyCode.Space))
            {
                OnHookReleased?.Invoke();
            }
    }
    public void ChangeHookDirection(bool movingRight)
    {
        _isMovingForward = movingRight;
    }
    void SetCanMove(bool canMove)
    {
        _canMove = canMove;
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (_collisionDetector.IsTouchingWall() || _collisionDetector.IsGrounded())
        {
            OnHookedTransform?.Invoke(transform);
            _canMove = false;

        }
        if (_collisionDetector.IsTouchingMovableObject())
        {
            //OnHookedTransform?.Invoke(PlayerPos.transform);
        }

        if (hitInfo.tag == "Player")
        {
            _timesItColidesWithParent -= 1;
            _canMove = true;
        }
        if (_timesItColidesWithParent <= 0)
        {
            OnHookEntersPlayer?.Invoke(true);
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