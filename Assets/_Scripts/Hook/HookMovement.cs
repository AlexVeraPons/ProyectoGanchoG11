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
    public bool IsMovingForward => _movingForward;
    private bool _movingForward = false;
    private Vector2 _direction => _hookDirection.GetDirectionVector();
    private Vector2 _finalDirection;
    private CollisionDetector _collisionDetector;
    [SerializeField]
    private float _hookMaxDistance = 5;
    private float _hookCurrentDistance = 0;
    //---------
    public HookDirection _hookDirection;
    public ThowHook _thowHook;
    private bool _canMove = true;
    [SerializeField]
    private float _returnSpeedModifier = 1f;

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
            transform.position = PlayerPos.transform.position;
        }
    }
    void FixedUpdate()
    {
        if (_canMove)
        {
            if (_movingForward)
            {
                MoveForward(_finalDirection);

            }
            else if (!_movingForward)
            {
                ReturnToVector(GetVectorDirection(PlayerPos.transform.position, this.transform.position));
            }
        }
    }
    void Update()
    {
        _hookCurrentDistance = Vector3.Distance(PlayerPos.transform.position, this.transform.position);
        if (_hookCurrentDistance >= _hookMaxDistance)
        {
            _movingForward = false;
        }

        if (!_collisionDetector.IsTouchingWall() && !_collisionDetector.IsGrounded() && _collisionDetector.IsTouchingMovableObject() && !_movingForward)
        {
            _thowHook.DisableHook();
            OnHookReleased?.Invoke();
            OnHookEntersPlayer?.Invoke(true);
            _movingForward = true;
        }
    }
    public void ChangeHookDirection(bool movingRight)
    {
        _movingForward = movingRight;
    }
    void SetCanMove(bool canMove)
    {
        _canMove = canMove;
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (IsMovingForward)
        {
            if (_collisionDetector.IsTouchingWall() || _collisionDetector.IsGrounded())
            {
                OnHookedTransform?.Invoke(transform);
                _canMove = false;
            }

        }

        if (_collisionDetector.IsTouchingMovableObject())
        {
            if (!_movingForward && IsSelfShooter(hitInfo))
            {
                _canMove = true;
                OnHookEntersPlayer?.Invoke(true);
                OnHookReleased?.Invoke();

            }
            if (!IsSelfShooter(hitInfo))
            {
                OnHookedTransform?.Invoke(transform);
                _canMove = false;
                var stunneable = hitInfo.GetComponent<IStunneable>();
                if (stunneable == null) return;
                stunneable.Stun();
            }
        }
    }

    void MoveForward(Vector3 forward)
    {
        _rigidBody2D.transform.position += (Vector3)forward * _speed * Time.deltaTime;
    }
    void ReturnToVector(Vector3 direccionDeVuelta)
    {
        _rigidBody2D.transform.position -= direccionDeVuelta.normalized * _speed * _returnSpeedModifier * Time.deltaTime;
    }
    private Vector2 GetVectorDirection(Vector3 InitialPosition, Vector3 finalPosition)
    {
        Vector2 VectorDirection = finalPosition - InitialPosition;
        return VectorDirection;
    }
    bool IsSelfShooter(Collider2D Player)
    {
        if (PlayerPos.transform == Player.transform)
        {
            return true;
        }
        return false;
    }

}