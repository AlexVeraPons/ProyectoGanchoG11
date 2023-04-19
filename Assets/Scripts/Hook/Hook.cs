using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    GameObject _grapplingGunOwner;
    Rigidbody2D _grapplingGunRigidbody2D;
    GrapplingGun _grapplingGun;

    Rigidbody2D _rigidBody2D;

    public HookState State = HookState.Waiting;

    [SerializeField] float _accelerationMultiplier = 1f;
    [SerializeField] float _maxSpeed = 1f;
    float _currentSpeed = 0f;

    [SerializeField] LayerMask _collisionLayers;
    private IHookable _hookedObject;

    float _timer;
    Vector2 _direction;
    Vector2 _origin;

    public Action OnLaunch;
    public Action OnPeakReached;
    public Action OnFinish;


    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        switch (State)
        {
            case HookState.Going:
                if (CanAccelerate())
                {
                    Accelerate();
                }

                MoveSelf();

                if (HasCollided())
                {
                    Collide();
                }
                else if (HasReachedPeak())
                {
                    OnPeakReached?.Invoke();
                    SwitchState(HookState.Returning);
                }
                break;

            case HookState.Returning:
                if (CanAccelerate())
                {
                    Accelerate();
                }

                RetrieveSelf();

                if (IsCloseToGrapplingGun())
                {
                    OnFinish?.Invoke();
                    SwitchState(HookState.Waiting);
                    ResetSelf();
                }
                break;

            case HookState.RetrievingSelf:
                RetrieveOwner();

                if (IsCloseToGrapplingGun())
                {
                    OnFinish?.Invoke();
                    SwitchState(HookState.Waiting);
                    ResetSelf();
                }
                break;

            default:
                break;
        }
    }

    void RetrieveSelf()
    {
        _rigidBody2D.velocity = -_direction * _currentSpeed * Time.deltaTime;
    }

    void MoveSelf()
    {
        _rigidBody2D.velocity = _direction * _currentSpeed * Time.deltaTime;
    }

    bool CanAccelerate()
    {
        return _currentSpeed < _maxSpeed;
    }

    void Accelerate()
    {
        if(_timer < 1) { _timer += Time.deltaTime * _accelerationMultiplier; }
        else { _timer = 1f; }

        _currentSpeed = Mathf.Lerp(0, _maxSpeed, _timer);
    }

    public void Launch(Vector2 position, Vector2 newDirection)
    {
        OnLaunch?.Invoke();
        State = HookState.Going;
        _origin = position;
        this.transform.position = _origin;
        AssignNewDirection(newDirection);
    }

    public void AssignNewDirection(Vector2 newDirection)
    {
        this._direction = newDirection;
    }

    public void AssignGrapplingHook(GrapplingGun grapplingGun)
    {
        _grapplingGunOwner = grapplingGun.gameObject;
        this._grapplingGun = grapplingGun;
        _grapplingGunRigidbody2D = grapplingGun.gameObject.GetComponent<Rigidbody2D>();
    }

    public void SwitchState(HookState newState)
    {
        State = newState;
        OnEnterState(newState);
    }

    void OnEnterState(HookState newState)
    {
        switch (newState)
        {
            case HookState.RetrievingSelf:
                _rigidBody2D.velocity = Vector2.zero;
                break;
            default: break;
        }
    }

    bool HasCollided()
    {
        var results = Physics2D.OverlapCircleAll((Vector2)this.gameObject.transform.position, 0.5f, _collisionLayers);
        if(results.Length > 0)
        {
            foreach(var result in results)
            {
                if(result.gameObject != _grapplingGun.gameObject)
                {
                    _hookedObject = result.GetComponent<IHookable>();
                    return true;
                }
            }
        }

        return false;
    }

    void Collide()
    {
        _hookedObject.OnHook(this);
    }

    bool HasReachedPeak()
    {
        return Vector2.Distance(_origin, transform.position) > _grapplingGun.PeakDistance;
    }

    bool IsCloseToGrapplingGun()
    {
        return Vector2.Distance(_grapplingGun.gameObject.transform.position, this.transform.position) < _grapplingGun.GrabDistance;
    }

    void ResetSelf()
    {
        _grapplingGun.ResetSelf();
        this.gameObject.SetActive(false);
    }

    void RetrieveOwner()
    {
        Vector2 dir = (Vector2)this.transform.position - (Vector2)_grapplingGun.gameObject.transform.position;
        dir = dir.normalized;
        _grapplingGunRigidbody2D.velocity = dir * _maxSpeed * Time.deltaTime;
    }
}

public enum HookState
{
    Waiting,
    Going,
    Returning,
    RetrievingSelf,
    RetrievingOther
}
