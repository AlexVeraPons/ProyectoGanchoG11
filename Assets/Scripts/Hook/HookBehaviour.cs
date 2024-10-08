using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookBehaviour : MonoBehaviour
{
    //Serialized variables for the inspector
    [Header("STANDARD VALUES")]
    [Tooltip("The total distance the hook will travel")]
    [SerializeField] int _distance;
    [Tooltip("The speed on which the hook will move")]
    [SerializeField] float _travelSpeed;
    [Tooltip("The speed on which the Grapple Gun Owner will move")]
    [SerializeField] float _hookSpeed;
    [Tooltip("Time that takes from the STUCK state to the RETRIEVE (If held). Set to 0 to disable.")]
    [SerializeField] float _maxStickTime;
    public Vector2 LaunchOffset => _launchOffset;
    [SerializeField] Vector2 _launchOffset;
    float _stickTimer;

    [Space(10)]
    [Header("LOGIC RELATED VALUES")]
    [Tooltip("Layers affected by the raycast")]
    [SerializeField] LayerMask _collidableLayers;
    [Tooltip("Layers that can just be collided and wont make the hook stuck")]
    [SerializeField] LayerMask _returnableLayers;
    [SerializeField] LayerMask _playerLayer;
    [Tooltip("The current state of the hook")]
    [SerializeField] public HookState _state;
    [Tooltip("The transform associated to the Grapple Gun Owner")]
    [SerializeField] Transform _playerTransform;

    //Private Grappling Gun related variables
    GrapplingGun _grapplingGun;
    Rigidbody2D _grapplingGunRigidbody2D;
    [Tooltip("The transform used as the impact of the raycast")]
    [SerializeField] Transform _impactTransform;

    [Space(10)]
    [Header("PARTICLES")]
    [SerializeField] private ParticleSystem _tongueStuckParticle;
    [SerializeField] private ParticleSystem _tongueThrowParticle;

    //Private Hook related variables
    Rigidbody2D _rigidbody2D;
    Vector2 _direction;
    Vector2 _launchPosition;
    IInteractable _impactInteractable;
    bool _hasHitObject = false;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        switch (_state)
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
                this.transform.position = _impactTransform.position;

                Move(
                    targetRigidbody2D: _grapplingGunRigidbody2D,
                    from: _playerTransform.position,
                    to: _impactTransform.position,
                    with: _travelSpeed
                );
                break;
        }
    }

    void Update()
    {
        switch (_state)
        {
            case HookState.Going:
                if (_grapplingGun.HookHeld == true)
                {
                    if (ReachedEnd() == true)
                    {
                        if (_hasHitObject == true)
                        {
                            if (HitObjectIsReturnable() == false)
                            {
                                if (HitObjectIsInteractable() == true
                                && _impactInteractable != null)
                                {
                                    ExecuteDoInteraction();
                                }

                                SwitchState(HookState.Stuck);
                            }
                            else
                            {
                                this.transform.position = _impactTransform.position;
                                SwitchState(HookState.Returning);
                            }
                        }
                        else
                        {
                            SwitchState(HookState.Returning);
                        }
                    }
                    else
                    {
                        if (_hasHitObject == true)
                        {
                            if (ParentTransformIsStillActive() == false)
                            {
                                SwitchState(HookState.Returning);
                            }
                        }
                    }
                }
                else
                {
                    SwitchState(HookState.Returning);
                }
                break;

            case HookState.Stuck:
                if (_grapplingGun.HookHeld == false
                || _grapplingGun.State == GrapplingGunState.Jammed)
                {
                    if (HitObjectIsInteractable() == true
                    && _impactInteractable != null)
                    {
                        ExecuteUndoInteraction();
                        _impactInteractable = null;
                    }

                    SwitchState(HookState.Returning);
                }
                else
                {
                    if (PlayerReachedEnd() == true && StickTimeIsEnabled() == true)
                    {
                        if (StickPeriodOver() == true)
                        {
                            SwitchState(HookState.Returning);
                        }
                    }

                    if (ParentTransformIsStillActive() == false)
                    {
                        SwitchState(HookState.Returning);
                    }
                }
                break;

            case HookState.Returning:
                if (ReachedEnd() == true)
                {
                    SwitchState(HookState.NotActive);
                }
                break;

            default: break;
        }
    }

    private bool HitObjectIsReturnable()
    {
        var result = Physics2D.OverlapCircle(_impactTransform.position, 0.3f, _returnableLayers);
        if (result != null)
        {
            return true;
        }

        return false;
    }

    private bool StickTimeIsEnabled()
    {
        return _maxStickTime != 0;
    }

    private bool StickPeriodOver()
    {
        _stickTimer += Time.deltaTime;
        if (_stickTimer > _maxStickTime)
        {
            return true;
        }

        return false;
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

    private bool PlayerReachedEnd()
    {
        var result = Physics2D.OverlapCircleAll(_impactTransform.position, 0.5f, _playerLayer);
        if (result.Length > 0)
        {
            return true;
        }

        return false;
    }

    public void SwitchState(HookState newState)
    {
        OnEnterState(newState);
        _state = newState;
    }

    void OnEnterState(HookState newState)
    {
        switch (newState)
        {
            case HookState.Going:
                _tongueThrowParticle.Play();
                _stickTimer = 0f;
                AudioManager._instance.PlaySingleSound(SingleSound.HookLaunch);
                break;

            case HookState.NotActive:
                if (_grapplingGun.State != GrapplingGunState.Jammed)
                {
                    _grapplingGun.ResetSelf();
                }
                this.transform.SetParent(_grapplingGun.transform.parent);
                this.gameObject.SetActive(false);
                break;

            case HookState.Stuck:
                _tongueStuckParticle.Play(); //Play tongue particle on hit
                AudioManager._instance.PlaySingleSound(SingleSound.HookStuck);
                _rigidbody2D.velocity = Vector2.zero; //Reset velocity to 0
                AlignPositionToImpact();
                break;

            case HookState.Returning:

                //SoundManager._instance.PlaySingleSound(SingleSound.HookRetrieving);
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
        float distanceBetweenPoints = Vector2.Distance(_launchPosition, _impactTransform.position);

        switch (_state)
        {
            case HookState.Going:
                float distanceFromLaunch = Vector2.Distance(_launchPosition, this.transform.position);
                return distanceFromLaunch > distanceBetweenPoints;

            case HookState.Returning:
                var results = Physics2D.OverlapCircleAll(_playerTransform.position, 1f);
                if (results.Length > 0)
                {
                    foreach (var resultObject in results)
                    {
                        //Esto hay que cambiarlo por el layer
                        if (resultObject.gameObject == this.gameObject)
                        {
                            return true;
                        }
                    }
                }

                return false;

            case HookState.Stuck:
                var otherResult = Physics2D.OverlapCircleAll(_impactTransform.position, 0.3f);
                if (otherResult.Length > 0)
                {
                    foreach (var resultObject in otherResult)
                    {
                        //Esto hay que cambiarlo por el layer
                        if (resultObject.gameObject == this.gameObject)
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

    bool ParentTransformIsStillActive()
    {
        var results = Physics2D.OverlapCircleAll(_impactTransform.position, 0.3f, _collidableLayers);
        if (results.Length > 0)
        {
            foreach (var result in results)
            {
                if (result.transform == _impactTransform.parent)
                {
                    return true;
                }
            }
        }

        return false;
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
        this.transform.position = (Vector2)_playerTransform.position + _launchOffset;
    }

    /// <summary>
    ///Assigns the _impactPosition value by creating a raycast.
    ///Depending if it hit something compatible or not
    ///the _impactInteractable reference will be asigned
    /// </summary>
    void SetImpactPosition()
    {
        var result = Physics2D.Raycast(_launchPosition,
        _direction, _distance, _collidableLayers);
        if (result.collider != null)
        {
            _impactInteractable = result.collider.gameObject.GetComponent<IInteractable>();
            _impactTransform.position = result.point;
            _impactTransform.SetParent(result.collider.transform);
            _hasHitObject = true;
        }
        else
        {
            _impactInteractable = null;
            _impactTransform.SetParent(null);
            _impactTransform.position = (Vector2)this.transform.position + _direction * _distance;
            _hasHitObject = false;
        }
    }

    /// <summary>
    ///Aligns the position of the hook to it's _impactPostion when
    ///the end is reached
    /// </summary>
    void AlignPositionToImpact()
    {
        this.transform.position = _impactTransform.position;
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
        if (_impactTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_impactTransform.position, 0.15f);
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
