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
    //---------
    public HookDirection _hookDirection;
    public ThowHook _thowHook;
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
        if (_collisionDetector.IsTouchingWall() || _collisionDetector.IsGrounded())
        {
            OnHookedTransform?.Invoke(transform);
            _canMove = false;
            if (_collisionDetector.IsTouchingMovableObject())
            {
                //es desactiva pero el seguent cop que s'activa esta al lloc on ha colisionat, no al lloc del player
                // _thowHook.DisableHook();
                // OnHookReleased?.Invoke();
                // OnHookEntersPlayer?.Invoke(true);
                // _movingForward = true;
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
                Debug.Log(gameObject + "El mamahuevo del enemigo");
                var stunneable = hitInfo.GetComponent<IStunneable>();
                if (stunneable == null) return;
                stunneable.Stun();
            }
            //OnHookedTransform?.Invoke(PlayerPos.transform);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (!_collisionDetector.IsTouchingWall() && !_collisionDetector.IsGrounded() && !_movingForward)
        {
            //Debug.Log("solta");
        }
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
    bool IsSelfShooter(Collider2D Player)
    {
        if (PlayerPos.transform == Player.transform)
        {
            Debug.Log(gameObject + "player que ha disparat");
            return true;
        }
        return false;
    }

}