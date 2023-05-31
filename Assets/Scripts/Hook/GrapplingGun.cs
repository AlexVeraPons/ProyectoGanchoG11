using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingGun : MonoBehaviour
{
    //[Header("STANDARD VALUES")]
    // Referenced values in inspector
    //[Tooltip("Minimum distance at which the hook will be retrieved")]
    //[SerializeField] float _grabDistance = 0.5f;

    public bool HookHeld => _hookActionHeld;

    // Private references
    private Vector2 _lastInput = Vector2.right;
    private bool _hookActionHeld = false;
    HookBehaviour _hook;

    // Input Action references
    [Space(10)]
    [Header("PERMANENT VALUES")]
    [Tooltip("The input reference used for the keyboard")]
    [SerializeField] InputActionReference _hookKeyboardInputReference;
    [Tooltip("The input reference used for the gamepad")]
    [SerializeField] InputActionReference _hookGamepadInputReference;
    [Tooltip("The hook object")]
    [SerializeField] GameObject _hookObject;
    [Tooltip("The current state of the gun")]
    [SerializeField] GrapplingGunState _state = GrapplingGunState.Waiting;
    public GrapplingGunState State => _state;

    public ScriptableVector2 Direction => _referenceDirection;
    [SerializeField] private ScriptableVector2 _referenceDirection;

    PlayerInput _input;

    // Unity functions
    private void Awake()
    {
        _hook = _hookObject.GetComponent<HookBehaviour>();
        _input = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        SetGrapplingGunOwner();
    }

    private void OnEnable()
    {
        LifeComponent.OnDeath += JamGun;
    }

    private void OnDisable()
    {
        LifeComponent.OnDeath -= JamGun;
    }

    private void Update()
    {
        _hookActionHeld = HookActionHeld();

        UpdateVectorValue();

        switch (_state)
        {
            case GrapplingGunState.Waiting:

                if (CanLaunchSelf() == true
                    && HasPressedButton() == true
                    && VectorIsNotZero() == true)
                {
                    LaunchHook();
                }
                break;
        }
    }

    bool VectorIsNotZero()
    {
        Vector2 direction;

        if (_input.currentControlScheme == "Keyboard&Mouse")
        {
            direction = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - (Vector2)this.transform.position;
            direction = direction.normalized;
        }
        else
        {
            direction = _hookGamepadInputReference.action.ReadValue<Vector2>().normalized;
        }

        if (direction == Vector2.zero) { return false; }

        return true;
    }

    bool HookActionHeld()
    {
        return _hookKeyboardInputReference.action.ReadValue<float>() == 1;
    }

    bool HasPressedButton()
    {
        return _hookKeyboardInputReference.action.triggered;
    }
    bool CanLaunchSelf()
    {
        return _state == GrapplingGunState.Waiting
;
    }
    bool IsHookTriggered()
    {
        return _hookKeyboardInputReference.action.triggered;
    }

    void UpdateVectorValue()
    {
        if (_input.currentControlScheme == "Keyboard&Mouse")
        {
            _referenceDirection.Value = Vector2.zero;
        }
        else
        {
            _referenceDirection.Value = _hookGamepadInputReference.action.ReadValue<Vector2>().normalized;
        }

    }

    // In here there's functions only called once
    private void LaunchHook()
    {
        _hook.gameObject.SetActive(true);
        _state = GrapplingGunState.InProgress;

        Vector2 direction;

        if (_input.currentControlScheme == "Keyboard&Mouse")
        {
            direction = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - (Vector2)this.transform.position;
            direction = direction.normalized;
        }
        else
        {
            direction = _hookGamepadInputReference.action.ReadValue<Vector2>();
        }

        _hook.Launch(newDirection: direction);
    }
    public void ResetSelf()
    {
        _state = GrapplingGunState.Waiting;
    }

    //In here there's all the assignment logic, only called once ever
    private void SetGrapplingGunOwner()
    {
        _hook.SetHook(this);
    }

    public void JamGun()
    {
        _state = GrapplingGunState.Jammed;
    }

    public void UnJamGun()
    {
        _state = GrapplingGunState.Waiting;
    }
}

public enum GrapplingGunState
{
    Waiting,
    InProgress,
    Jammed,
}