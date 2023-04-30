using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookBehaviour : MonoBehaviour
{
    //Serialized variables for the inspector
    [SerializeField] int _distance;
    [SerializeField] float _travelSpeed;
    [SerializeField] float _hookSpeed;
    [SerializeField] LayerMask _collidableLayers;
    [SerializeField] public HookState _state;

    //Private Grappling Gun related variables
    GrapplingGun _grapplingGun;
    Transform _playerTransform;
    Rigidbody2D _grapplingGunRigidbody2D;

    //Private Hook related variables
    Rigidbody2D _rigidbody2D;
    Vector2 _direction;
    Vector2 _impactPosition;
    Vector2 _launchPosition;
    IInteractable _impactInteractable;
    bool _hasHitObject = false;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        switch(_state)
        {
            case HookState.NotActive:

            break;

            case HookState.Going:
                MoveSelf();
            break;

            case HookState.Returning:
                Move(
                    targetRigidbody2D: this._rigidbody2D,
                    from: this.transform.position,
                    to: _playerTransform.position,
                    with: _hookSpeed
                );
            break;

            case HookState.Stuck:
                Move(
                    targetRigidbody2D: _grapplingGunRigidbody2D,
                    from: _playerTransform.position,
                    to: _impactPosition,
                    with: _travelSpeed
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
                        if(_hasHitObject == true)
                        {
                            if(HitObjectIsInteractable() == true
                            && _impactInteractable != null)
                            {
                                ExecuteDoInteraction();
                            }

                            SwitchState(HookState.Stuck);
                        }
                        else
                        {
                            SwitchState(HookState.Returning);
                        }
                    }
                }
                else
                {
                    SwitchState(HookState.Returning);
                }
            break;

            case HookState.Stuck:
                if(_grapplingGun.HookHeld == false
                || _grapplingGun.State == GrapplingGunState.Jammed)
                {
                    if(HitObjectIsInteractable() == true
                    && _impactInteractable != null)
                    {
                        ExecuteUndoInteraction();
                        _impactInteractable = null;
                    }

                    SwitchState(HookState.Returning);
                }
            break;

            case HookState.Returning:
                if(ReachedEnd() == true)
                {
                    if(HitObjectIsInteractable() == true
                    && _impactInteractable != null)
                    {
                        ExecuteUndoInteraction();
                        _impactInteractable = null;
                        //We have to make sure _impactInteractable
                        //is after everything or it will call a function
                        //that doesn't exist
                    }

                    SwitchState(HookState.NotActive);
                }
            break;
        }
    }

    /// <summary>
    /// Executes the "ExecuteUndoInteraction" function of the _impactInteractable reference
    /// </summary>
    private void ExecuteUndoInteraction()
    {
        _impactInteractable.UndoInteraction();
    }

    /// <summary>
    /// Executes the "ExecuteDoInteraction" function of the _impactInteractable reference
    /// </summary>
    private void ExecuteDoInteraction()
    {
        _impactInteractable.DoInteraction();
    }

    /// <summary>
    ///Checks if the object that the reference stored when the raycast
    ///hit the object is actually there or not
    /// </summary>
    private bool HitObjectIsInteractable()
    {
        return _impactInteractable != null;
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
            if(_grapplingGun.State != GrapplingGunState.Jammed)
            {
                _grapplingGun.ResetSelf();
            }
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

    /// <summary>
    ///Checks if the hook has reached the end of it's trajectory
    ///by creating an OverlapCircle from the _impactPosition value
    /// </summary>
    bool ReachedEnd()
    {
        float distanceBetweenPoints = Vector2.Distance(_launchPosition, _impactPosition);

        switch(_state)
        {
            case HookState.Going:
                float distanceFromLaunch = Vector2.Distance(_launchPosition, this.transform.position);
                return distanceFromLaunch > distanceBetweenPoints;

            case HookState.Returning:
                var results = Physics2D.OverlapCircleAll(_playerTransform.position, 1f);
                if(results.Length > 0)
                {
                    foreach(var resultObject in results)
                    {
                        //Esto hay que cambiarlo por el layer
                        if(resultObject.gameObject.name == "Hook")
                        {
                            return true;
                        }
                    }
                }

                return false;

            default: 
                print("Error"); 
                return false;
        }
    }

    /// <summary>
    ///Function that moves a target rigidbody from a position to
    ///a position and the reference value of speed
    /// </summary>
    void Move(Rigidbody2D targetRigidbody2D, Vector2 from, Vector2 to, float with)
    {
        float value = with;
        Vector2 direction = (to - from).normalized;
        targetRigidbody2D.velocity = direction * value * Time.deltaTime;
    }

    /// <summary>
    ///Function that only moves the hook itself
    /// </summary>
    void MoveSelf()
    {
        _rigidbody2D.velocity = _direction * _hookSpeed * Time.deltaTime;
    }

    /// <summary>
    ///Assigns a new direction that the hook will take on launch
    /// </summary>
    void AssignNewDirection(Vector2 newDirection)
    {
        _direction = newDirection;
    }

    /// <summary>
    ///Assigns the position the owner of the grappling gun is in
    ///when the hook is launched
    /// </summary>
    void AssignNewPosition()
    {
        this.transform.position = _playerTransform.position;
    }

    /// <summary>
    ///Assigns the _impactPosition value by creating a raycast.
    ///Depending if it hit something compatible or not
    ///the _impactInteractable reference will be asigned
    /// </summary>
    void SetImpactPosition()
    {
        var result = Physics2D.Raycast(this.transform.position,
        _direction, _distance, _collidableLayers);
        if(result.collider != null)
        {
            _impactInteractable = result.collider.gameObject.GetComponent<IInteractable>();
            _impactPosition = result.point;
            _hasHitObject = true;
        }
        else
        {
            _impactInteractable = null;
            _impactPosition = (Vector2)this.transform.position + _direction * _distance;
            _hasHitObject = false;
        }
    }

    /// <summary>
    ///Aligns the position of the hook to it's _impactPostion when
    ///the end is reached
    /// </summary>
    void AlignPositionToImpact()
    {
        this.transform.position = _impactPosition;
    }

    /// <summary>
    ///Stores the _launchPosition of the hook for reference
    /// </summary>
    void SetLaunchPosition()
    {
        _launchPosition = this.transform.position;
    }

    /// <summary>
    ///Assigns the Grappling Gun that will throw this hook
    /// </summary>
    public void SetHook(GrapplingGun gun)
    {
        _grapplingGun = gun;
        _playerTransform = _grapplingGun.gameObject.transform;
        _grapplingGunRigidbody2D = _grapplingGun.gameObject.GetComponent<Rigidbody2D>();
        this.gameObject.SetActive(false);
    }

    /// <summary>
    ///Launches the hook in the desired direction
    /// </summary>
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
