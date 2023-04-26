using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookBehaviour : MonoBehaviour
{
    const int _distance = 80;

    [SerializeField] LayerMask _collidableLayers;

    public HookState _state;
    public float _speed;

    GrapplingGun _grapplingGun;
    Vector2 _direction;
    Rigidbody2D _rigidbody2D;
    Rigidbody2D _grapplingGunRigidbody2D;

    Transform _playerTransform;

    Vector2 _impactPosition;
    Vector2 _launchPosition;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        switch(_state)
        {
            case HookState.Going:
                MoveSelf();
            break;

            case HookState.Returning:
                Move(
                targetRigidbody2D: this._rigidbody2D,
                from: this.transform.position,
                to: _playerTransform.position
                );
            break;
        }
    }

    void Update()
    {
        switch(_state)
        {
            case HookState.Going:
                if(_grapplingGun.HookHeld == true)
                {
                    if(ReachedEnd() == true)
                    {
                        SwitchState(HookState.Stuck);
                    }
                }
                else
                {
                    SwitchState(HookState.Returning);
                }
            break;

            case HookState.Returning:
                if(ReachedEnd() == true)
                {
                    SwitchState(HookState.NotActive);
                }
            break;
        }
    }

    public void SwitchState(HookState newState)
    {
        OnEnterState(newState);
        _state = newState;
    }

    void OnEnterState(HookState newState)
    {
        switch(newState)
        {
            case HookState.NotActive:
            _grapplingGun.ResetSelf();
            this.gameObject.SetActive(false);
            break;

            case HookState.Going:
            break;

            case HookState.Stuck:
            _rigidbody2D.velocity = Vector2.zero; //Reset velocity to 0
            AlignPositionToImpact();
            break;

            case HookState.Returning:
            break;

            default: break;
        }
    }

    bool ReachedEnd()
    {
        float distanceBetweenPoints = Vector2.Distance(_launchPosition, _impactPosition);
        float distanceFromLaunch = Vector2.Distance(_launchPosition, this.transform.position);
        float distanceFromEnd = Vector2.Distance(_impactPosition, this.transform.position);

        
        return distanceFromLaunch > distanceBetweenPoints
            || distanceFromEnd > distanceBetweenPoints - 0.1f;
    }

    void Move(Rigidbody2D targetRigidbody2D, Vector2 from, Vector2 to)
    {
        Vector2 direction = (to - from).normalized;
        targetRigidbody2D.velocity = direction * _speed * Time.deltaTime;
    }

    void MoveSelf()
    {
        _rigidbody2D.velocity = _direction * _speed * Time.deltaTime;
    }

    void AssignNewDirection(Vector2 newDirection)
    {
        _direction = newDirection;
    }

    void AssignNewPosition()
    {
        this.transform.position = _playerTransform.position;
    }

    void SetImpactPosition()
    {
        var result = Physics2D.Raycast(this.transform.position,
        _direction, _distance, _collidableLayers);
        if(result.collider != null)
        {
            _impactPosition = result.point;
            print(_impactPosition);
        }
    }

    void AlignPositionToImpact()
    {
        this.transform.position = _impactPosition;
    }

    void SetLaunchPosition()
    {
        _launchPosition = this.transform.position;
    }

    public void SetHook(GrapplingGun gun)
    {
        _grapplingGun = gun;
        _playerTransform = _grapplingGun.gameObject.transform;
        _grapplingGunRigidbody2D = _grapplingGun.gameObject.GetComponent<Rigidbody2D>();
        this.gameObject.SetActive(false);
    }

    public void Launch(Vector2 newDirection)
    {
        AssignNewPosition();
        AssignNewDirection(newDirection);
        SetLaunchPosition();
        SetImpactPosition();
        SwitchState(HookState.Going);
    }

    void OnDrawGizmos()
    {
        if(_impactPosition != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_impactPosition, 0.15f);
        }
    }
}

public enum HookState
{
    NotActive,
    Going,
    Stuck,
    Returning
}
