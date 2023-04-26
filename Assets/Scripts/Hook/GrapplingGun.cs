using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingGun : MonoBehaviour
{
    // Input Action references
    [SerializeField] InputActionReference _hookInputReference;
    [SerializeField] InputActionReference _movementInputReference; // Pending change

    // Referenced values in inspector
    [SerializeField] GameObject _hookObject;
    [SerializeField] GrapplingGunState _state = GrapplingGunState.Waiting;
    [SerializeField] float _peakDistance = 10f;
    [SerializeField] float _grabDistance = 0.5f;
    
    // Public references
    public float PeakDistance => _peakDistance;
    public float GrabDistance => _grabDistance;
    
    public bool HookHeld => _hookActionHeld;

    // Private references
    private Vector2 _lastInput = Vector2.right;
    private bool _hookActionHeld = false;
    HookBehaviour _hook;

    // Unity functions
    private void Awake()
    {
        _hook = _hookObject.GetComponent<HookBehaviour>();
    }
    private void Start()
    {
        SetGrapplingGunOwner();
    }
    private void Update()
    {
        _hookActionHeld = HookActionHeld();

        if (MovementInputTriggered() == true)
        {
            if (MovementInput() != Vector2.zero)
            {
                AssignLastInput();
            }
        }

        switch(_state)
        {
            case GrapplingGunState.Waiting:
                if (CanLaunchSelf() == true && HasPressedButton() == true)
                {
                    LaunchHook();
                }
            break;
        }
    }

    // In here there's all the bool logic
    bool MovementInputTriggered()
    {
        return _movementInputReference.action.triggered;
    }

    bool HookActionHeld()
    {
        return  _hookInputReference.action.ReadValue<float>() == 1;
    }

    bool HasPressedButton()
    {
        return _hookInputReference.action.triggered;
    }
    bool CanLaunchSelf()
    {
        return _state == GrapplingGunState.Waiting
;
    }
    bool IsHookTriggered()
    {
        return _hookInputReference.action.triggered;
    }

    // In here there's all the vector2 logic (Only used to read the values of the input movement)
    Vector2 MovementInput()
    {
        return _movementInputReference.action.ReadValue<Vector2>();
    }

    // In here there's functions called every frame
    void AssignLastInput()
    {
        _lastInput = _movementInputReference.action.ReadValue<Vector2>();
    }

    // In here there's functions only called once
    private void LaunchHook()
    {
        _hook.gameObject.SetActive(true);
        _state = GrapplingGunState.InProgress;
        _hook.Launch(newDirection: _lastInput);
    }
    public void ResetSelf()
    {
        _state = GrapplingGunState.Waiting
;
    }

    //In here there's all the assignment logic, only called once ever
    private void SetGrapplingGunOwner()
    {
        _hook.SetHook(this);
    }
}

public enum GrapplingGunState
{
    Waiting,
    InProgress,
    Jammed,
}