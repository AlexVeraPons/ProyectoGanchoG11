using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hook : MonoBehaviour
{
    // Reference scripts needed

    [SerializeField] InputActionReference _hookInputReference; // Pending for modification

    public GameObject Owner => _grapplingGunOwner;
    GameObject _grapplingGunOwner;
    Rigidbody2D _grapplingGunRigidbody2D;
    GrapplingGun _grapplingGun;

    Rigidbody2D _rigidBody2D;

    private Rigidbody2D _targetRigidbody2D;

    // Referenced values on the inspector

    [SerializeField] float _accelerationMultiplier = 1f;
    [SerializeField] float _maxSpeed = 1f;
    [SerializeField] float _returnSpeedMultiplier = 1f;
    [SerializeField] float _returnThreshold = 0.3f;
    [SerializeField] float _pushStrength = 5f;

    [SerializeField] LayerMask _collisionLayers;

    // Other referenced values on the script
    
    public HookState State = HookState.Waiting;

    float _timer;
    float _currentSpeed = 0f;
    Vector2 _direction;
    Vector2 _origin;
    private IHookable _hookedObject;

    // Actions executed on "OnEnterState" method

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

                MoveForward();
                break;

            case HookState.Returning:
                if (CanAccelerate())
                {
                    Accelerate();
                }

                MoveBackwards();
                break;

            case HookState.RetrievingOwner:
                RetrieveOwner();
                break;

            case HookState.RetrievingTarget:
                MoveBackwards();
                RetrieveTarget();
                break;

            default:
                break;
        }
    }

    private void Update()
    {
        switch (State)
        {
            case HookState.Going:

                if (HasBeenCancelled())
                {
                    SwitchState(HookState.Returning);
                }
                else if (HasCollided())
                {
                    Collide();
                }
                else if (HasReachedPeak())
                {
                    SwitchState(HookState.Returning);
                }
                break;

            case HookState.Returning:
                if (IsCloseToGrapplingGun())
                {
                    SwitchState(HookState.Waiting);
                }
                break;

            case HookState.RetrievingOwner:
                if (IsCloseToGrapplingGun())
                {
                    _grapplingGunRigidbody2D.velocity = Vector2.zero;
                    SwitchState(HookState.Waiting);
                }
                break;

            case HookState.RetrievingTarget:
                if (IsCloseToGrapplingGun())
                {
                    _grapplingGunRigidbody2D.velocity = Vector2.zero;
                    _targetRigidbody2D.velocity = Vector2.zero;
                    PushTargetAway();
                    SwitchState(HookState.Waiting);
                }
                break;

            default:
                break;
        }
    }

    //In here there's all the bool logics
    bool CanAccelerate()
    {
        return _currentSpeed < _maxSpeed;
    }

    bool HasBeenCancelled()
    {
        return _hookInputReference.action.ReadValue<float>() == 0 && _timer > _returnThreshold;
    }

    bool HasCollided()
    {
        var results = Physics2D.OverlapCircleAll((Vector2)this.gameObject.transform.position, 0.5f, _collisionLayers);
        if (results.Length > 0)
        {
            foreach (var result in results)
            {
                if (result.gameObject != _grapplingGun.gameObject)
                {
                    _hookedObject = result.GetComponent<IHookable>();
                    return true;
                }
            }
        }

        return false;
    }

    bool HasReachedPeak()
    {
        return Vector2.Distance(_origin, transform.position) > _grapplingGun.PeakDistance;
    }

    bool IsCloseToGrapplingGun()
    {
        return Vector2.Distance(_grapplingGun.gameObject.transform.position, this.transform.position) < _grapplingGun.GrabDistance;
    }

    //In here there's all the physics logic, methods called continuously depending on the state
    void Accelerate() // Used in Going state
    {
        if (_timer < 1) { _timer += Time.deltaTime * _accelerationMultiplier; }
        else { _timer = 1f; }

        _currentSpeed = Mathf.Lerp(0, _maxSpeed, _timer);
    }

    void MoveForward() // Used in Going state
    {
        _rigidBody2D.velocity = _direction * _currentSpeed * Time.deltaTime;
    }

    void MoveBackwards() // Used in Returning state
    {
        Vector2 vec = (Vector2)_grapplingGunOwner.transform.position - (Vector2)this.transform.position;
        vec = vec.normalized;
        _rigidBody2D.velocity = vec * _currentSpeed * _returnSpeedMultiplier * Time.deltaTime;
    }

    void RetrieveOwner() // Used in RetrieveOwner state
    {
        Vector2 dir = (Vector2)this.transform.position - (Vector2)_grapplingGun.gameObject.transform.position;
        dir = dir.normalized;
        _grapplingGunRigidbody2D.velocity = dir * _maxSpeed * Time.deltaTime;
    }

    void RetrieveTarget() // Used in RetrieveTarget state
    {
        Vector2 dir = (Vector2)_grapplingGun.gameObject.transform.position - (Vector2)_targetRigidbody2D.transform.position;
        dir = dir.normalized;
        _targetRigidbody2D.velocity = dir * _maxSpeed * _returnSpeedMultiplier * Time.deltaTime;
    }

    //In here there's all the void methods that get called in only 1 frame
    void Collide()
    {
        _hookedObject.OnHook(this);
    }

    void PushTargetAway()
    {
        Vector2 dir = (Vector2)_targetRigidbody2D.transform.position - (Vector2)_grapplingGun.gameObject.transform.position;
        dir = dir.normalized;
        _targetRigidbody2D.AddForce(dir * _pushStrength);
    }

    void ResetSelf()
    {
        _grapplingGun.ResetSelf();
        _currentSpeed = 0f;
        _timer = 0f;
        this.gameObject.SetActive(false);
    }

    public void Launch(Vector2 position, Vector2 newDirection)
    {
        OnLaunch?.Invoke();
        SwitchState(HookState.Going);
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

    public void AssignTarget(Rigidbody2D targetRigidbody2D)
    {
        this._targetRigidbody2D = targetRigidbody2D;
    }

    public void SwitchState(HookState newState, GameObject gameObject = null)
    {
        State = newState;
        OnEnterState(newState);
    }

    void OnEnterState(HookState newState, GameObject gameObject = null)
    {
        switch (newState)
        {
            case HookState.Going:
                OnLaunch?.Invoke();
                break;

            case HookState.Returning:
                OnPeakReached?.Invoke();
                break;

            case HookState.RetrievingOwner:
                OnPeakReached?.Invoke();
                _rigidBody2D.velocity = Vector2.zero;
                break;

            case HookState.RetrievingTarget:
                break;

            case HookState.Waiting:
                OnFinish?.Invoke();
                ResetSelf();
                break;

            default: break;
        }
    }

}

public enum HookState
{
    Waiting,
    Going,
    Returning,
    RetrievingOwner,
    RetrievingTarget
}
